<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    ITools tools;
    int Parent_ID;
    HomeLeftCate myApp;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("d843afda-7680-45fa-bc00-32278bf77ae8");
        
        tools = ToolsFactory.CreateTools();
        myApp = new HomeLeftCate();
        
        Parent_ID = tools.CheckInt(Request.QueryString["Parent_ID"]);
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>首页左侧分类管理</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/common.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<link href="/Scripts/jqGrid/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jqGrid/grid.locale-zh_CN.js" type="text/javascript"></script>
<script src="/Scripts/jqGrid/jquery.jqGrid.min.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">首页左侧分类管理 </td>
    </tr>
    <tr>
      <td class="content_content">
        <div style="font-size:14px; margin-bottom:5px;">
            <a href="homeleftcate_list.aspx?parnet_id=0">根类别</a>：<% =myApp.CateRecursion(Parent_ID, "homeleftcate_list.aspx?parent_id={cate_id}")%> <input type="button" name="btn_sch" class="btn_01" id="btn_sch" value="在本类别添加" onclick="location='homeleftcate_add.aspx?parent_id=<%=Parent_ID %>';" />
        </div>
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
            jQuery("#list").jqGrid({
                url: 'homeleftcate_do.aspx?action=list&parent_id=<% =Parent_ID%>',
                datatype: "json",
                colNames: ['ID', '类别名称', '排序', "操作"],
                colModel: [
				{ width: 50, name: 'HomeLeftCateInfo.Home_Left_Cate_ID', index: 'HomeLeftCateInfo.Home_Left_Cate_ID', align: 'center' },
				{ name: 'HomeLeftCateInfo.Home_Left_Cate_Name', index: 'HomeLeftCateInfo.Home_Left_Cate_Name', align: 'center' },
				{ width: 100, name: 'HomeLeftCateInfo.Home_Left_Cate_Sort', index: 'HomeLeftCateInfo.Home_Left_Cate_Sort', align: 'center' },
				{ width: 80, name: 'Operate', index: 'Operate', align: 'center', sortable: false },
			],
                sortname: 'HomeLeftCateInfo.Home_Left_Cate_ID',
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
        <div style="margin-top:5px;">
        <% if (Public.CheckPrivilege("8738cd22-6808-4fdd-94f4-d9bb51b64509"))
           { %>
        <input type="button" id="Button3" class="bt_orange" value="导入商品分类" onclick="location='homeleftcate_do.aspx?action=inputcate';" /> <span class="tip">导入分类会清空现有数据，数据深度支持三级</span>
        <%} %>
        </div>
      </td>
    </tr>
  </table>
</div>
</body>
</html>