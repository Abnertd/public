<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%   Public.CheckLogin("237da5cb-1fa2-4862-be25-d83077adeb01");
     ITools tools;
     AD Ad = new AD();
     tools = ToolsFactory.CreateTools();
     string Ad_Kind;
     int ad_channel_id = 0;
     string keyword, defaultkey, ReqURL;
     defaultkey = "";
     keyword = Request["keyword"];
     Ad_Kind = Request["Ad_Kind"];
     ad_channel_id = tools.CheckInt(Request["adpositionchannel"]);
     if (keyword != "输入广告名称搜索" && keyword != null)
     {
         keyword = keyword;
     }
     else
     {
         keyword = "输入广告名称搜索";
     }
     if (keyword == "输入广告名称搜索")
     {
         defaultkey = "";
     }
     else
     {
         defaultkey = keyword;
     }

     string Audit = tools.CheckStr(Request["Audit"]);

     ReqURL = "keyword=" + Server.UrlEncode(defaultkey) + "&Ad_Kind=" + Ad_Kind + "&ad_channel_id=" + ad_channel_id;
    
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
    function getChannelID(obj)
    {
       $("#adposition_position").load("/AD/Ad_do.aspx?action=adpositionchannel&channel_id="+obj+"&timer=" + Math.random());
    }
    </script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">管理广告</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <form action="ad.aspx" method="post" name="frm_sch" id="frm_sch">
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				  <tr bgcolor="#F5F9FC" >
					<td align="right">

					
					<span class="left_nav">位置</span> 
					<%=Ad.AD_Position_Select(ad_channel_id)%>&nbsp;<span id="adposition_position"><%Ad.Select_AD_Position1("Ad_Kind", Ad_Kind, ad_channel_id);%></span>
					<span class="left_nav">搜索</span> 
					 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入广告名称搜索'){this.value='';}" value="<% =keyword %>" /> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
        url: 'Ad_do.aspx?action=list&<%=ReqURL %>',
			datatype: "json",
            colNames: ["ID", "广告名称","位置代号","类型","频率","权重","显示","点击", "起止时间","缩略图", "操作"],
            colModel: [
				{width:40, name: 'ADInfo.Ad_ID', index: 'ADInfo.Ad_ID', align: 'center'},
				{ name: 'ADInfo.Ad_Title', index: 'ADInfo.Ad_Title'},
				{width:110,align:'center', name: 'ADInfo.Ad_Kind', index: 'ADInfo.Ad_Kind'},
				{width:40,align:'center', name: 'ADInfo.Ad_MediaKind', index: 'ADInfo.Ad_MediaKind'},
				{width:30, name: 'ADInfo.Ad_Show_Freq', index: 'ADInfo.Ad_Show_Freq', align: 'center'},
				{width:30, name: 'ADInfo.Ad_Sort', index: 'ADInfo.Ad_Sort', align: 'center'},
				{width:40, name: 'ADInfo.Ad_Show_times', index: 'ADInfo.Ad_Show_times', align: 'center'},
				{width:40, name: 'ADInfo.Ad_Hits', index: 'ADInfo.Ad_Hits', align: 'center'},
				{ width: 130, align: 'center', name: 'ADInfo.Ad_StartDate', index: 'ADInfo.Ad_StartDate' },
                { width: 130, align: 'center', name: 'ADInfo.Ad_Img', index: 'ADInfo.Ad_Img' },
				{width:80, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'ADInfo.Ad_ID',
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
      </td>
    </tr>
  </table>
</div>
</body>
</html>
