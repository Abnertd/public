<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    string pagerul = "";
    string keyword, date_start, date_end;
    int status = 0, areaid, usergrade, deliverystatus, paymentstatus, contracttype, confirm;
    int invoiceapply = 0;
    ITools tools;
    Contract myApp;

    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        Public.CheckLogin("a3465003-08b3-4a31-9103-28d16c57f2c8");
        status = tools.CheckInt(Request["status"]);
        contracttype = tools.CheckInt(Request["contracttype"]);
        myApp = new Contract();
        invoiceapply = tools.CheckInt(Request["invoiceapply"]);
        pagerul = "contract_do.aspx?action=list&status=" + status + "&contracttype=" + contracttype;

        keyword = Request["keyword"];

        if (keyword != "输入合同编号、用户名称进行搜索" && keyword != null)
        {
            pagerul += "&keyword=" + Server.UrlEncode(keyword);
        }
        else
        {
            keyword = "输入合同编号、用户名称进行搜索";
            pagerul += "&keyword=";
        }

        deliverystatus = tools.CheckInt(Request["deliverystatus"]);
        paymentstatus = tools.CheckInt(Request["paymentstatus"]);
        pagerul += "&areaid=" + areaid + "&usergrade=" + usergrade + "&deliverystatus=" + deliverystatus + "&paymentstatus=" + paymentstatus;

        //客户确认
        confirm = tools.CheckInt(Request["confirm"]);
        pagerul += "&confirm=" + confirm;

        //开始时间
        date_start = tools.CheckStr(Request["date_start"]);

        //结束时间
        date_end = tools.CheckStr(Request["date_end"]);

        if (tools.CheckStr(Request["main"]) == "today")
        {
            date_start = DateTime.Now.ToString("yyyy-MM-dd");
            date_end = DateTime.Now.ToString("yyyy-MM-dd");
        }
        pagerul += "&date_start=" + date_start;
        pagerul += "&date_end=" + date_end;
        //开票申请
        pagerul += "&invoiceapply=" + invoiceapply;
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
                <td class="content_title">合同管理</td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="5">
                        <form action="contract_list.aspx?status=<%=status%>" method="post" name="frm_sch" id="frm_sch">
                            <tr bgcolor="#F5F9FC">

                                <td align="right"><span class="left_nav">搜索</span> 起始日期：
					<input type="text" class="input_calendar" name="date_start" id="date_start" maxlength="10" readonly="readonly" value="<%=date_start %>" />
                                    <script>          	    $("#date_start").datepicker({ numberOfMonths: 1 });</script>
                                    -
                                    <input type="text" class="input_calendar" name="date_end" id="date_end" maxlength="10" readonly="readonly" value="<%=date_end%>" />
                                    <script>          	    $("#date_end").datepicker({ numberOfMonths: 1 });</script>

                                    合同类型：
          	            <select id="contracttype" name="contracttype">
                              <option value="0" <%=Public.CheckedSelected(contracttype.ToString(),"0") %>>全部</option>
                              <option value="1" <%=Public.CheckedSelected(contracttype.ToString(),"1") %>>现货采购合同</option>
                              <option value="2" <%=Public.CheckedSelected(contracttype.ToString(),"2") %>>定制采购合同</option>
                              <option value="3" <%=Public.CheckedSelected(contracttype.ToString(),"3") %>>代理采购合同</option>

                          </select>
                                    确认状态：
          	            <select id="confirm" name="confirm">
                              <option value="0" <%=Public.CheckedSelected(confirm.ToString(),"0") %>>全部</option>
                              <option value="1" <%=Public.CheckedSelected(confirm.ToString(),"1") %>>双方未确认</option>
                              <option value="2" <%=Public.CheckedSelected(confirm.ToString(),"2") %>>用户已确认</option>
                              <option value="3" <%=Public.CheckedSelected(confirm.ToString(),"3") %>>平台已确认</option>

                          </select>
                                    发货状态：
          	            <select id="deliverystatus" name="deliverystatus">
                              <option value="0" <%=Public.CheckedSelected(deliverystatus.ToString(),"0") %>>全部</option>
                              <option value="1" <%=Public.CheckedSelected(deliverystatus.ToString(),"1") %>>未发货</option>
                              <option value="2" <%=Public.CheckedSelected(deliverystatus.ToString(),"2") %>>配货中</option>
                              <option value="3" <%=Public.CheckedSelected(deliverystatus.ToString(),"3") %>>部分发货</option>
                              <option value="4" <%=Public.CheckedSelected(deliverystatus.ToString(),"4") %>>全部发货</option>
                              <option value="5" <%=Public.CheckedSelected(deliverystatus.ToString(),"5") %>>全部签收</option>
                          </select>
                                    付款状态：
          	            <select id="Select1" name="paymentstatus">
                              <option value="0" <%=Public.CheckedSelected(paymentstatus.ToString(),"0") %>>全部</option>
                              <option value="1" <%=Public.CheckedSelected(paymentstatus.ToString(),"1") %>>未支付</option>
                              <option value="2" <%=Public.CheckedSelected(paymentstatus.ToString(),"2") %>>部分支付</option>
                              <option value="3" <%=Public.CheckedSelected(paymentstatus.ToString(),"3") %>>全部支付</option>
                              <option value="4" <%=Public.CheckedSelected(paymentstatus.ToString(),"4") %>>全部到账</option>

                          </select>
                                    <input type="text" name="keyword" id="keyword" size="40" onfocus="if(this.value=='输入合同编号、用户名称进行搜索'){this.value='';}" value="<% =keyword %>">
                                    <input type="submit" name="btn_sch" class="btn_01" id="btn_sch" value="搜索" /></td>
                            </tr>
                        </form>
                    </table>

                </td>
            </tr>
            <tr>
                <td class="content_content">
                    <table id="list"></table>
                    <div id="pager"></div>
                    <script type="text/javascript">
                        jQuery("#list").jqGrid({
                            url: '<%=pagerul %>',
                datatype: "json",
                colNames: ['ID', '合同编号', '合同类型', '甲方名称', '乙方名称', '合同金额', '合同状态','合同确认状态', "操作"],
                colModel: [
				{ width: 50, name: 'Contract.Contract_ID', index: 'Contract.Contract_ID', align: 'center' },
                { width: 150, name: 'ContractInfo.Contract_SN', index: 'ContractInfo.Contract_SN', align: 'center' },
                { width: 100, name: 'ContractInfo.Contract_Type', index: 'ContractInfo.Contract_Type', align: 'center' },

				{ width: 100, name: 'ContractInfo.Contract_SupplierID', index: 'ContractInfo.Contract_SupplierID', align: 'center' },
				{ width: 100, name: 'ContractInfo.Contract_BuyerID', index: 'ContractInfo.Contract_BuyerID', align: 'center' },
				{ width: 100, name: 'Contract.Contract_AllPrice', index: 'Contract.Contract_AllPrice', align: 'center' },

				{ width: 100, name: 'ContractInfo.Contract_Status', index: 'ContractInfo.Contract_Status', align: 'center' },
                { width: 100, name: 'ContractInfo.Contract_Confirm_Status', index: 'ContractInfo.Contract_Confirm_Status', align: 'center' },
				{ width: 100, name: 'Operate', index: 'Operate', align: 'center', sortable: false },
                ],
                sortname: 'ContractInfo.Contract_ID',
                sortorder: "desc",
                rowNum: 10,
                rowList: [10, 20, 40],
                pager: 'pager',
                multiselect: false,
                viewsortcols: [false, 'horizontal', true],
                width: getTotalWidth() - 35,
                height: "100%",
                shrinkToFit: true
            });
                    </script>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
