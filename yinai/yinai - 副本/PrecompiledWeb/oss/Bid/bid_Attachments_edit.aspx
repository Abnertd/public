<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private Bid myApp;
    private ITools tools;

    private string Bid_Attachments_Name, Bid_Attachments_format, Bid_Attachments_Size, Bid_Attachments_Remarks, Bid_Attachments_Path;
    private int Bid_Attachments_ID, Bid_Attachments_Sort, Bid_Attachments_BidID;


    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("3c1e3fb0-9ad4-4a25-afa3-5ebc7b706039");
        myApp = new Bid();
        tools = ToolsFactory.CreateTools();

        Bid_Attachments_ID = tools.CheckInt(Request.QueryString["AttID"]);
        BidAttachmentsInfo entity = myApp.GetBidAttachmentsByID(Bid_Attachments_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Bid_Attachments_ID = entity.Bid_Attachments_ID;
            Bid_Attachments_Sort = entity.Bid_Attachments_Sort;
            Bid_Attachments_Name = entity.Bid_Attachments_Name;
            Bid_Attachments_format = entity.Bid_Attachments_format;
            Bid_Attachments_Size = entity.Bid_Attachments_Size;
            Bid_Attachments_Remarks = entity.Bid_Attachments_Remarks;
            Bid_Attachments_Path = entity.Bid_Attachments_Path;
            Bid_Attachments_BidID = entity.Bid_Attachments_BidID;

        }
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
                                    <input name="Bid_Attachments_Sort" type="text" id="Bid_Attachments_Sort" size="50" maxlength="100" value="<%=Bid_Attachments_Sort %>" /></td>
                            </tr>--%>

                            <tr>
                                <td class="cell_title">附件名称</td>
                                <td class="cell_content">
                                    <input name="Bid_Attachments_Name" type="text" id="Bid_Attachments_Name" size="50" maxlength="100" value="<%=Bid_Attachments_Name %>"/></td>
                            </tr>
<%--                            <tr>
                                <td class="cell_title">文件格式</td>
                                <td class="cell_content">
                                    <input name="Bid_Attachments_format" type="text" id="Bid_Attachments_format" size="50" maxlength="100" value="<%=Bid_Attachments_format %>"/></td>
                            </tr>
                            <tr>
                                <td class="cell_title">大小</td>
                                <td class="cell_content">
                                    <input name="Bid_Attachments_Size" type="text" id="Bid_Attachments_Size" size="50" maxlength="100" value="<%=Bid_Attachments_Size %>"/></td>
                            </tr>--%>
                            <tr>
                                <td class="cell_title">说明</td>
                                <td class="cell_content">
                                    <input name="Bid_Attachments_Remarks" type="text" id="Bid_Attachments_Remarks" size="50" maxlength="100" value="<%=Bid_Attachments_Remarks %>" /></td>
                            </tr>

                            <tr>
                                <td class="cell_title">附件</td>
                                <td class="cell_content">
                                    <a href="<%=Application["Upload_Server_URL"]+Bid_Attachments_Path %>" target="_blank">点击查看</a>
                                    </td>
                            </tr>

                            <tr>
                                <td class="cell_title">上传附件</td>
                                <td class="cell_content">
                                    <iframe id="iframe_upload" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=Bid&formname=formadd&frmelement=Bid_Attachments_Path&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe>
                                </td>
                            </tr>

                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="right">
                                    <input type="hidden" id="action" name="action" value="renewArtt" />
                                    <input type="hidden" id="Bid_Attachments_ID" name="Bid_Attachments_ID" value="<%=Bid_Attachments_ID %>" />
                                    
                                    <input name="Bid_Attachments_Path" type="hidden" id="Bid_Attachments_Path" value="<%=Bid_Attachments_Path %>" />
                                    <input name="Bid_Attachments_BidID" type="hidden" id="Bid_Attachments_BidID" value="<%=Bid_Attachments_BidID %>" />
                                    <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                                    <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'bid_edit.aspx?list=2&BID=<%=Bid_Attachments_BidID%>';" /></td>
                            </tr>
                        </table>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
