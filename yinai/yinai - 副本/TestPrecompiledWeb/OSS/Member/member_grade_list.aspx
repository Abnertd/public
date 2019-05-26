<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("1c955ea6-881f-48d8-ba8d-c5aa7ce9cfea");
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
      <td class="content_title">采购商等级管理 [<a href="member_grade_add.aspx">添加等级</a>]</td>
    </tr>
    <tr>
      <td class="content_content">
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'member_grade_do.aspx?action=list',
			datatype: "json",
            colNames: ['ID', '等级名称', '优惠百分比', '是否为默认', '所需积分', '积分比率', '添加时间', "操作"],
            colModel: [
				{width:50, name: 'MemberGradeInfo.Member_Grade_ID', index: 'MemberGradeInfo.Member_Grade_ID', align: 'center'},
				{width:80, name: 'MemberGradeInfo.Member_Grade_Name', index: 'MemberGradeInfo.Member_Grade_Name', align: 'center'},
				{width:80, name: 'MemberGradeInfo.Member_Grade_Percent', index: 'MemberGradeInfo.Member_Grade_Percent', align: 'center'},
				{width:80, name: 'MemberGradeInfo.Member_Grade_Default', index: 'MemberGradeInfo.Member_Grade_Default', align: 'center'},
				{width:80, name: 'MemberGradeInfo.Member_Grade_RequiredCoin', index: 'MemberGradeInfo.Member_Grade_RequiredCoin', align: 'center'},
				{width:80, name: 'MemberGradeInfo.Member_Grade_CoinRate', index: 'MemberGradeInfo.Member_Grade_CoinRate', align: 'center'},
				{width:80, name: 'MemberGradeInfo.Member_Grade_Addtime', index: 'MemberGradeInfo.Member_Grade_Addtime', align: 'center'},
				{width:80, name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'MemberGradeInfo.Member_Grade_ID',
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