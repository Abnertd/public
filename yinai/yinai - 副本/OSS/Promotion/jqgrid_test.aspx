<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<% Public.CheckLogin("9b17d437-fb2a-4caa-821e-daf13d9efae4");
   string keyword, defaultkey,ReqURL,brandid,cateid;
   defaultkey = "";
   ITools tools;
   tools = ToolsFactory.CreateTools();
   keyword = Request["keyword"];
   brandid = tools.NullStr(Request["brandid"]);
   cateid = tools.NullStr(Request["cateid"]);
   if (keyword != "输入品牌名称进行搜索" && keyword != null)
   {
       keyword = keyword;
   }
   else
   {
       keyword = "输入品牌名称进行搜索";
   }
   if (keyword == "输入品牌名称进行搜索")
   {
       defaultkey = "";
   }
   else
   {
       defaultkey = keyword;
   }
   ReqURL = "keyword=" + Server.UrlEncode(defaultkey) + "&cateid=" + cateid;
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
<!--

function MM_ReturnID(url){ //v3.0
	$('#favor_brandid').val(jQuery('#list').jqGrid('getGridParam','selarrrow'));
	$('#brand_picker').load("picker_do.aspx?action=showbrand&brand_id=" + $('#favor_brandid').val()+"&timer=" + Math.random());
	close_picker();
}
//-->
</script>
</head>
<body>

  <table width="600" border="0" cellpadding="0" cellspacing="0">
    <tr>
      <td>
      <table width="100%" cellpadding="0" cellspacing="0" border="0" class="picker_tittab">
      <tr><td class="picker_tit">品牌选择</td><td width="30" align="center"><a href="javascript:void(0);" onclick="close_picker();"><img src="/images/close.gif" border="0"/></a></td></tr>
      </table>
      </td>
    </tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				  <tr>
					<td align="right"><span class="left_nav">搜索</span> 
					
					 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入品牌名称进行搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" onclick="refresh_brandlist();" /></td>
				  </tr>
				</table>
    </td></tr>
    <tr>
      <td id="brandlist" height="330" valign="top">
        <input type="hidden" id="all_flag" value="0" />
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'picker_do.aspx?action=brandlist&<%=ReqURL %>',
			datatype: "json",
            colNames: ['ID','品牌名称'],
            colModel: [
                {width:30,align:'center', name: 'id', index: 'id',sortable:false},
				{align:'left', name: 'BrandInfo.Brand_Name', index: 'BrandInfo.Brand_Name',sortable:false}
			],
            sortname: 'BrandInfo.Brand_ID',
			sortorder: "desc",
			rowNum: 20,
			rowList:[10,20,40], 
			pager: 'pager', 
			multiselect: true,
			viewsortcols: [false,'horizontal',true],
			width: 597,
			viewrecords:true,
			height: "100%",
			loadComplete:function(){
			    if($("#all_flag").val()=="1")
			    {
			        $('.cbox').attr("checked","true");
			        var ids=jQuery('#list').jqGrid('getDataIDs');
			        for(var i=0;i<ids.length;i++)
                    {
                        jQuery("#list").jqGrid('setSelection',ids[i]);
                    }
			    }
            }
        });
        </script>
        
        <%//if (brandid.Length>0)
          //{
          //    Response.Write("<script type=\"text/javascript\">jQuery(\"#list\").jqGrid('setSelection','" + brandid + "');</script>");
          //}%>
        
        
      </td>
    </tr>
    <tr><td height="30" align="center"><input type="button" name="btn_sch" class="btn_01" value="确定" onclick="MM_ReturnID();"/></td></tr>
  </table>
</body>
</html>
