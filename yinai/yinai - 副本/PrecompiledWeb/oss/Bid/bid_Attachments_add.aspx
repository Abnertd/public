<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private int BID;
    private ITools tools;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("e202397a-bb1e-4e67-b008-67701d37c5cb");
        tools = ToolsFactory.CreateTools();
        BID = tools.CheckInt(Request["BID"]);
        
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>
        <link href="/scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/scripts/jquery-ui/jquery-ui.js"></script>
    <script src="/scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js"></script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">添加附件</td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="bid_do.aspx">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
<%--                            <tr>
                                <td class="cell_title">序号</td>
                                <td class="cell_content">
                                    <input name="Bid_Attachments_Sort" type="text" id="Bid_Attachments_Sort" size="50" maxlength="100" value="1"/></td>
                            </tr>--%>

                            <tr>
                                <td class="cell_title">附件名称</td>
                                <td class="cell_content">
                                    <input name="Bid_Attachments_Name" type="text" id="Bid_Attachments_Name" size="50" maxlength="100" /></td>
                            </tr>
<%--                            <tr>
                                <td class="cell_title">文件格式</td>
                                <td class="cell_content">
                                    <input name="Bid_Attachments_format" type="text" id="Bid_Attachments_format" size="50" maxlength="100" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">大小</td>
                                <td class="cell_content">
                                    <input name="Bid_Attachments_Size" type="text" id="Bid_Attachments_Size" size="50" maxlength="100" /></td>
                            </tr>--%>
                            <tr>
                                <td class="cell_title">说明</td>
                                <td class="cell_content">
                                    <input name="Bid_Attachments_Remarks" type="text" id="Bid_Attachments_Remarks" size="50" maxlength="100" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">上传附件</td>
                                <td class="cell_content">
                                    <iframe id="iframe_upload" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=Bid&formname=formadd&frmelement=Bid_Attachments_Path&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe></td>
                            </tr>

                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="right">
                                    <input type="hidden" id="action" name="action" value="newArtt" />
                                    <input name="Bid_Attachments_Path" type="hidden" id="Bid_Attachments_Path" value="" />
                                    <input name="Bid_Attachments_BidID" type="hidden" id="Bid_Attachments_BidID" value="<%=BID %>" />
                                    <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                                    <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'bid_edit.aspx?list=2&BID=<%=BID%>';" /></td>
                            </tr>
                        </table>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
