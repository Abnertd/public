<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private ITools tools;
    string keyword, defaultkey, ReqURL;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("all");
        tools = ToolsFactory.CreateTools();

        defaultkey = "";
        keyword = Request["keyword"];

        if (keyword != "输入会员名称搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入会员名称搜索";
        }
        if (keyword == "输入会员名称搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }

        ReqURL = "keyword=" + Server.UrlEncode(defaultkey);
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css" />
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
      <td class="content_title">商家中信支付管理</td>
    </tr>
    <tr><td height="5"></td></tr>
    <tr><td>
    <table width="100%" border="0" cellspacing="0" cellpadding="5">
				<form action="?" method="post" name="frm_sch" id="frm_sch" >
				  <tr bgcolor="#F5F9FC" >
				  <td align="left" style="color:Red">注意 ：请不要重复点击推送</td>
					<td align="right">

					<span class="left_nav">搜索</span> 
					
					 <input type="text" name="keyword" size="50" id="keyword" onfocus="if(this.value=='输入会员名称搜索'){this.value='';}" value="<% =keyword %>"> <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
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
                  url: 'zhongxin_do.aspx?action=list&<%=ReqURL %>',
                  datatype: "json",
                  colNames: ["ID", "商家", "公司名称", "出金开户名", "中信虚拟账号", "出金收款账号", "出金银行", "出金银行名称", "出金银行行号", "审核状态", "推送状态", "操作"],
                  colModel: [
				        { width: 40, name: 'ZhongXinInfo.ID', index: 'ZhongXinInfo.ID', align: 'center' },
                        { width: 150, name: 'ZhongXinInfo.SupplierID', index: 'ZhongXinInfo.SupplierID' },
                        { width: 150, name: 'ZhongXinInfo.CompanyName', index: 'ZhongXinInfo.CompanyName' },
                        { name: 'ZhongXinInfo.OpenAccountName', index: 'ZhongXinInfo.OpenAccountName' },
                        { width: 160, name: 'ZhongXinInfo.SubAccount', index: 'ZhongXinInfo.SubAccount', align: 'center' },
                        { width: 160, name: 'ZhongXinInfo.ReceiptAccount', index: 'ZhongXinInfo.ReceiptAccount', align: 'center' },
				        { width: 60, name: 'ZhongXinInfo.ReceiptBank', index: 'ZhongXinInfo.ReceiptBank', align: 'center' },
                        { width: 80, name: 'ZhongXinInfo.BankName', index: 'ZhongXinInfo.BankName', align: 'center' },
				        { width: 80, name: 'ZhongXinInfo.BankCode', index: 'ZhongXinInfo.BankCode', align: 'center' },  
                        { width: 40, name: 'ZhongXinInfo.Audit', index: 'ZhongXinInfo.Audit', align: 'center' },
                         { width: 40, name: 'ZhongXinInfo.Register', index: 'ZhongXinInfo.Register', align: 'center' },
				        { width: 80, name: 'Operate', index: 'Operate', align: 'center', sortable: false },
			        ],
                  sortname: 'ZhongXinInfo.ID',
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
</body>
</html>