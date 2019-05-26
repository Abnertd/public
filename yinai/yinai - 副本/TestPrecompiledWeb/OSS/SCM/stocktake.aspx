<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    string keyword, defaultkey;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("849805bd-ba21-4508-a803-9e0e5cc33b66");
        keyword = Request["keyword"];
        if (keyword != "输入商品编码、商品名称进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入商品编码、商品名称进行搜索";
        }
        if (keyword == "输入商品编码、商品名称进行搜索")
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

<style type="text/css">
    /*显示分店库存使用*/
    #divmsg{ position:absolute; display:none; width:300px; min-height:10px; border:solid 2px #ccc; background-color:#eee; z-index:1000; padding:0px;}
    #divmsg .tdtit{ background-color:#fff; width:230px;}
    #divmsg .tdamount{ text-align:center; color:#090; font-weight:bold; background-color:#fff;}
    #divmsg dl{ margin:0px 5px 5px 5px; padding:0;}
    #divmsg dt{ height:30px; line-height:30px; font-size:14px; font-weight:bold; color:#f00;}
    #divmsg dt a{ background:url(/images/btn_move.gif); width:16px; height:16px; float:right; display:block; margin-top:5px; cursor:pointer;}
    #divmsg dd{margin:0;}
    /*显示分店库存使用*/
</style>

</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">库存盘点</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
        <form action="stocktake.aspx" method="post" name="frm_sch" id="frm_sch" >
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
	      <tr bgcolor="#F5F9FC" >
		    <td align="right"><span class="left_nav">搜索</span> 

		     <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入商品编码、商品名称进行搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
	      </tr>
	    </table>
	    </form>
    </td></tr>
    <tr>
      <td class="content_content">
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
            jQuery("#list").jqGrid({
                url: 'stocktake_do.aspx?action=list&keyword=<%=Server.UrlEncode(defaultkey)%>',
                datatype: "json",
                colNames: ["ID", "商品编码", "商品名称", "规格", "产地", "库存", "操作"],
                colModel: [
				{ width: 40, name: 'ProductInfo.Product_ID', index: 'ProductInfo.Product_ID', align: 'center' },
				{ width: 90, align: 'center', name: 'ProductInfo.Product_Code', index: 'ProductInfo.Product_Code' },
				{ name: 'ProductInfo.Product_Name', index: 'ProductInfo.Product_Name' },
				{ width: 80, align: 'center', name: 'ProductInfo.Product_Spec', index: 'ProductInfo.Product_Spec' },
				{ name: 'ProductInfo.Product_Maker', index: 'ProductInfo.Product_Maker' },
				{ width: 50, name: 'ProductInfo.Product_StockAmount', index: 'ProductInfo.Product_StockAmount', align: 'center' },
				{ width: 50, name: 'Operate', index: 'Operate', align: 'center', sortable: false },
			    ],
                sortname: 'ProductInfo.Product_ID',
                sortorder: "desc",
                rowNum: GetrowNum(),
                rowList: GetrowList(),
                pager: 'pager',
                multiselect: false,
                viewsortcols: [false, 'horizontal', true],
                width: getTotalWidth() - 35,
                height: "100%"
            });
        </script>
      </td>
    </tr>
  </table>
</div>

<script type="text/javascript">
    var delayTime = null;
    function openupStock(procuct_code, obj) {
        clearTimeout(delayTime);

        var html = "";
        html += "<div id=\"divmsg\" onmouseover=\"KeepStock();\" onmouseout=\"CloseStock();\">";
        html += "    <dl>";
        html += "        <dt>各仓库库存一览</dt>";
        html += "        <dd></dd>";
        html += "    </dl>";
        html += "</div>";
        $("body").append(html);

        html = "";
        $.ajax({
            url: encodeURI("stocktake_do.aspx?action=showstock&product_code=" + procuct_code + "&date=" + new Date().getTime()),
            async: false,
            global: false,
            dataType: "json",
            success: function(data) {
                if (data != null) {
                    html += "<table width=\"100%\" border=\"0\" cellpadding=\"3\" cellspacing=\"1\" bgcolor=\"#cccccc\">";
                    $.each(data, function(entityIndex, entity) {
                        html += "   <tr>";
                        html += "       <td class=\"tdtit\">" + entity["Depot_Name"] + "</td>";
                        html += "       <td class=\"tdamount\">" + entity["Depot_Amount"] + "</td>";
                        html += "   </tr>";
                    })
                    html += "</table>";
                }
                $("#divmsg dd").html(html);
            }
        });
        
        $("#divmsg").css({
            "display": "block",
            "top": $(obj).offset().top,
            "left": $(obj).offset().left - $("#divmsg").width()
        });

    }
    function CloseStock() { delayTime = setTimeout(function() { CloseStraight(); }, 300); }
    function KeepStock() { clearTimeout(delayTime); }
    function CloseStraight() { $("#divmsg").remove(); }
</script>

</body>
</html>