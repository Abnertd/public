<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="System.Linq" %>
<%  Public.CheckLogin("db71e6f9-f858-4469-b45e-b6ab55412853");
    string keyword, defaultkey, ReqURL;
    int Fee_multi, policy_multi, gift_multi;
    ITools tools;
    tools = ToolsFactory.CreateTools();
    defaultkey = "";
    keyword = Request["keyword"];
    if (keyword != "输入商品编码、商品名称、通用名、生产企业、拼音首字母进行搜索" && keyword != null && keyword != "")
    {
        keyword = keyword;
    }
    else
    {
        keyword = "输入商品编码、商品名称、通用名、生产企业、拼音首字母进行搜索";
    }
    if (keyword == "输入商品编码、商品名称、通用名、生产企业、拼音首字母进行搜索")
    {
        defaultkey = "";
    }
    else
    {
        defaultkey = keyword;
    }
    Fee_multi = tools.CheckInt(Request["Fee_multi"]);
    policy_multi = tools.CheckInt(Request["policy_multi"]);
    gift_multi = tools.CheckInt(Request["gift_multi"]);
    if (Fee_multi == 0 && policy_multi == 0 && gift_multi == 0)
    {
        Fee_multi = 1;
    }
    ReqURL = "keyword=" + Server.UrlEncode(defaultkey) + "&Fee_multi=" + Fee_multi + "&policy_multi=" + policy_multi + "&gift_multi=" + gift_multi;
    
    %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
<script type="text/javascript">
change_inputcss();
function promotionexcept(promotion_type,promotion_id,product_id)
{
    $.ajax({
    async: false,
    type: "POST",
    url: "Promotion_Except_do.aspx?action=exceptproductid&promotion_id="+promotion_id+"&product_id="+product_id+"&timer=" + Math.random(),
    data: "promotion_type=" +  promotion_type
    });
    var subgrid_id="list_"+product_id;
    var subgrid_table_id;   
    subgrid_table_id = "list_"+product_id + "_t";    
       
    var subgrid_pager_id;   
    subgrid_pager_id = "list_"+product_id + "_pgr"  
                   $("#" + subgrid_id).html("<table id='"+subgrid_table_id+"' class='scroll'></table>");   
    $("#" + subgrid_table_id).jqGrid({   
        url: "Promotion_Except_do.aspx?action=sublist&id="+product_id+"&promotion_type="+promotion_type,  
        datatype: "json",   
        colNames: ['ID','促销活动','操作'],   
        colModel: [   
            {name:"id",index:"id",width:55,key:true,align:"center", sortable:false},   
            {name:"promotion",index:"promotion",width:500,align:"left", sortable:false},   
            {name:"operate",index:"operate",width:80,align:"center", sortable:false},   
        ],     
        
        viewrecords: true,
        height: "100%"
   });   
        

}
</script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">促销优惠产品排除</td>
    </tr>
    <tr>
      <td class="content_content">
      <table width="100%" border="0" cellpadding="0" cellspacing="0">
      <tr><td valign="top" height="31" class="opt_foot">
      <table border="0" cellpadding="0" cellspacing="0">
      <tr>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(Fee_multi==1){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?Fee_multi=1&policy_multi=0&gift_multi=0", "单运费优惠产品")%>
      </td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(Fee_multi==2){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?Fee_multi=2&policy_multi=0&gift_multi=0", "多运费优惠产品")%></td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(policy_multi==1){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?Fee_multi=0&policy_multi=1&gift_multi=0", "单优惠政策产品")%></td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(policy_multi==2){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?Fee_multi=0&policy_multi=2&gift_multi=0", "多优惠政策产品")%></td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(gift_multi==1){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?Fee_multi=0&policy_multi=0&gift_multi=1", "单赠品优惠产品")%></td>
      <td class="opt_gap">&nbsp;</td>
      <td class="<%if(gift_multi==2){Response.Write("opt_cur");}else{Response.Write("opt_uncur");} %>">
      <%=Public.Page_Option("?Fee_multi=0&policy_multi=0&gift_multi=2", "多赠品优惠产品")%></td>
      </tr>
      </table>
      </td></tr>
      <tr><td>
      <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="?<%=ReqURL.Replace("keyword=" + Server.UrlEncode(defaultkey) + "&","") %>" method="post" name="frm_sch" id="frm_sch" >
				  <tr>
				  
					<td align="left">
					<span class="left_nav">搜索</span> 
					
					 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入商品编码、商品名称、通用名、生产企业、拼音首字母进行搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
				  </tr>
				  </form>
				</table>
		</td></tr>
		<tr><td>
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'Promotion_Except_do.aspx?action=list&<%=ReqURL %>',
			datatype: "json",
            colNames: ['ID', '商品编号', "商品名称","规格","生产企业"],
            colModel: [
				{width:50, name: 'ProductInfo.Product_ID', index: 'ProductInfo.Product_ID', align: 'center'},
				{width:100, align:'center', name: 'ProductInfo.Product_Code', index: 'ProductInfo.Product_Code', sortable:false},
				{align:'center',name: 'ProductInfo.Product_Name', index: 'ProductInfo.Product_Name', sortable:false},
				{align:'center',name: 'ProductInfo.Product_Spec', index: 'ProductInfo.Product_Spec', sortable:false},
				{ align:'center',name: 'ProductInfo.Product_Maker', index: 'ProductInfo.Product_Maker', sortable:false},
			],
            sortname: 'ProductInfo.Product_ID',
			sortorder: "desc",
			rowNum: GetrowNum(),
			rowList: GetrowList(), 
			pager: 'pager', 
			multiselect: false,
			

			viewsortcols: [false,'horizontal',true],
			width: getTotalWidth() - 35,
			subGrid : true,
			subGridUrl: 'Promotion_Except_do.aspx?action=sublist&<%=ReqURL %>',
            subGridModel: [{ name  : ['ID','促销活动','操作'], 
                    width : [55,500,80],
                    align : ['center','left','center']} 
                    
    ],

			height: "100%"
        });
        </script>
        </td></tr></table>
      </td>
    </tr>
  </table>
</div>

</body>
</html>
