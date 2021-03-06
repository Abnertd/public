﻿<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<% Public.CheckLogin("9b17d437-fb2a-4caa-821e-daf13d9efae4");
   string keyword, defaultkey, ReqURL, memberid;
   defaultkey = "";
   ITools tools;
   tools = ToolsFactory.CreateTools();
   Promotion myApp;
   myApp = new Promotion();
   keyword = Request["keyword"];
   memberid = tools.NullStr(Session["selected_memberid"]);
   if (keyword != "输入昵称、邮箱、姓名、电话、手机进行搜索" && keyword != null)
   {
       keyword = keyword;
   }
   else
   {
       keyword = "输入昵称、邮箱、姓名、电话、手机进行搜索";
   }
   if (keyword == "输入昵称、邮箱、姓名、电话、手机进行搜索")
   {
       defaultkey = "";
   }
   else
   {
       defaultkey = keyword;
   }
   ReqURL = "keyword=" + Server.UrlEncode(defaultkey);

    %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/promotion.js" type="text/javascript"></script>
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

	$('#favor_memberid').val($("#selarrow").val());
	$.ajax({
	async: false,
    type: "POST",
    url: "picker_do.aspx?action=savememberid&timer=" + Math.random(),
    data: "memberid=" +  $("#selarrow").val()
    });
    //$('#member_picker').load("<span class=\"pickertip\">选择项目更新中...</span>");
	$('#member_picker').load("picker_do.aspx?action=showmember&timer=" + Math.random());
	if($('#favor_memberid').val()!="")
	{
	    $('#favor_memberall0').attr("checked",true);
	    $('#favor_memberall1').attr("checked",false);
	}
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
      <tr><td class="picker_tit">会员选择</td><td width="30" align="center"><a href="javascript:void(0);" onclick="close_picker();"><img src="/images/close.gif" border="0"/></a></td></tr>
      </table>
      </td>
    </tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				  <tr>
					<td align="right"><span class="left_nav">搜索</span> 
					
					 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入昵称、邮箱、姓名、电话、手机进行搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" onclick="refresh_memberlist();" /></td>
				  </tr>
				</table>
    </td></tr>
    <tr>
      <td id="memberlist" height="360" valign="top">
        <input type="hidden" id="all_flag" value="0" />
        <input type="hidden" id="allids" value="<%=myApp.Get_MemberList_IDs() %>" />
        <input type="hidden" id="selarrow" value="0,<%=memberid %>" />
        <div class="list_tip_div" id="list_seltip"></div>
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'picker_do.aspx?action=memberlist&<%=ReqURL %>',
			datatype: "json",
            colNames: ['ID','昵称','注册邮箱','会员等级'],
            colModel: [
                {width:30,align:'center', name: 'id', index: 'id',sortable:false},
				{align:'left', name: 'MemberInfo.Member_NickName', index: 'MemberInfo.Member_NickName',sortable:false},
				{align:'left', name: 'MemberInfo.Member_Email', index: 'MemberInfo.Member_Email',sortable:false},
				{align:'left', name: 'MemberInfo.Member_Grade', index: 'MemberInfo.Member_Grade',sortable:false}
			],
            sortname: 'MemberInfo.Member_ID',
			sortorder: "desc",
			rowNum: 10,
			//rowList:[10,20,40], 
			pager: 'pager', 
			multiselect: true,
			viewrecords:true,
			viewsortcols: [false,'horizontal',true],
			width: 597,
			height: "100%",
			onSelectRow: function(id,status){  
			 jqgrid_rowclick(id,status);
			 jqgrid_seltip_display();
			 
           }, 
			loadComplete:function(){
                jqgrid_selarry();
			    //列表选择记录提示
			    jqgrid_seltip_display();
            }
        });
        //设置全选标记 
        jqgrid_allclick();
        </script>
      </td>
    </tr>
    <tr><td height="30" align="center" bgcolor="#EAF4FD"><input type="button" name="btn_sch" class="btn_01" value="确定" onclick="MM_ReturnID();"/></td></tr>
  </table>
</body>
</html>
