<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<script runat="server">
    private ProductReview myReview;
    private int recommend_status, audit_status, view_status;
    private ITools tools;
    private string keyword, defaultkey, ReqURL;
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        Public.CheckLogin("cb1e9c33-7ac5-4939-8520-a0e192cb0129");
        myReview=new ProductReview();
        string action=Request["action"];
        if(action=="recommend"||action=="recommendcancel"||action=="show"||action=="showcancel")
        {
            myReview.EditProductReview(action);
        }

        defaultkey = "";
        keyword = Request["keyword"];
        recommend_status = tools.CheckInt(Request["recommend_status"]);
        audit_status = tools.CheckInt(Request["audit_status"]);
        view_status = tools.CheckInt(Request["view_status"]);
        if (keyword != "输入评论人、产品名称进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入评论人、产品名称进行搜索";
        }
        if (keyword == "输入评论人、产品名称进行搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }

        ReqURL = "keyword=" + Server.UrlEncode(defaultkey) + "&recommend_status=" + recommend_status + "&audit_status=" + audit_status + "&view_status=" + view_status;
    }
    
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"/>
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
      <td class="content_title">产品评论管理</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="product_review_list.aspx" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
					<td align="right">
					<span class="left_nav">推荐状态</span> 
					 <select name="recommend_status">
					 <option value="0" <%=Public.CheckedSelected("0",recommend_status.ToString()) %>>全部</option>
					 <option value="1" <%=Public.CheckedSelected("1",recommend_status.ToString()) %>>已推荐</option>
					 <option value="2" <%=Public.CheckedSelected("2",recommend_status.ToString()) %>>未推荐</option>
					 </select>
					 <span class="left_nav">审核状态</span> 
					 <select name="audit_status">
					 <option value="0" <%=Public.CheckedSelected("0",audit_status.ToString()) %>>全部</option>
					 <option value="1" <%=Public.CheckedSelected("1",audit_status.ToString()) %>>已审核</option>
					 <option value="2" <%=Public.CheckedSelected("2",audit_status.ToString()) %>>未审核</option>
					 </select>
					 <span class="left_nav">查看状态</span> 
					 <select name="view_status">
					 <option value="0" <%=Public.CheckedSelected("0",view_status.ToString()) %>>全部</option>
					 <option value="1" <%=Public.CheckedSelected("1",view_status.ToString()) %>>已查看</option>
					 <option value="2" <%=Public.CheckedSelected("2",view_status.ToString()) %>>未查看</option>
					 </select>
					 <span class="left_nav">搜索</span> 
					 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入评论人、产品名称进行搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
				  </tr>
				  </form>
				</table>
    </td></tr>
    <tr>
      <td class="content_content">
        <table id="list"></table>
        <div id="pager"></div>
        <script type="text/javascript">
        jQuery("#list").jqGrid({
        url: 'product_review_do.aspx?action=list&<%=ReqURL %>',
			datatype: "json",
            colNames: ['ID', '会员', "评分","产品","推荐状态","审核状态","查看状态", "操作"],
            colModel: [
				{ width:50,name: 'ProductReviewInfo.Product_Review_ID', index: 'ProductReviewInfo.Product_Review_ID', align: 'center'},
				{ width:100,align:'center',name: 'ProductReviewInfo.Product_Review_MemberID', index: 'ProductReviewInfo.Product_Review_MemberID'},
				{ width:30,name: 'ProductReviewInfo.Product_Review_Star', index: 'ProductReviewInfo.Product_Review_Star', align: 'center'},
				{ name: 'ProductReviewInfo.Product_Review_ProductID', index: 'ProductReviewInfo.Product_Review_ProductID'},
				{ width:50,name: 'ProductReviewInfo.Product_Review_IsRecommend', index: 'ProductReviewInfo.Product_Review_IsRecommend', align: 'center'},
				{ width:50,name: 'ProductReviewInfo.Product_Review_IsShow', index: 'ProductReviewInfo.Product_Review_IsShow', align: 'center'},
				{ width:50,name: 'ProductReviewInfo.Product_Review_IsView', index: 'ProductReviewInfo.Product_Review_IsView', align: 'center'},
				{ width:80,name: 'Operate', index: 'Operate', align: 'center', sortable:false},
			],
            sortname: 'ProductReviewInfo.Product_Review_ID',
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
