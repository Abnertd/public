<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    private About myApp;
    private ITools tools;

    private string About_Title, About_Sign, About_Content, About_Site;
    private int About_ID, About_IsActive, About_Sort;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("b15dd1c4-d9c5-4b09-b7c2-3ef4d24af7ef");
        myApp = new About();
        tools = ToolsFactory.CreateTools();

        About_ID = tools.CheckInt(Request.QueryString["About_ID"]);
        AboutInfo entity = myApp.GetAboutByID(About_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            About_ID = entity.About_ID;
            About_IsActive = entity.About_IsActive;
            About_Title = entity.About_Title;
            About_Sign = entity.About_Sign;
            About_Content = entity.About_Content;
            About_Sort = entity.About_Sort;
            About_Site = entity.About_Site;
        }
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">添加页面</td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="about_do.aspx">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
                            <tr>
                                <td class="cell_title">页面标题</td>
                                <td class="cell_content">
                                    <input name="About_Title" type="text" id="About_Title" size="50" maxlength="100" value="<% =About_Title%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">页面标识</td>
                                <td class="cell_content">
                                    <input name="About_Sign" type="text" id="About_Sign" size="50" maxlength="100" value="<% =About_Sign%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">上传图片</td>
                                <td class="cell_content">
                                    <iframe id="iframe1" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=content&formname=formadd&frmelement=About_Content&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title" valign="top">页面内容</td>
                                <td class="cell_content">
                                    <textarea cols="80" id="About_Content" name="About_Content" rows="16"><% =About_Content%></textarea>
                                    <script type="text/javascript">
                                        var About_ContentEditor;
                                        KindEditor.ready(function (K) {
                                            About_ContentEditor = K.create('#About_Content', {
                                                width: '100%',
                                                height: '500px',
                                                filterMode: false,
                                                afterBlur: function () { this.sync(); }
                                            });
                                        });
                                    </script>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">页面次序</td>
                                <td class="cell_content">
                                    <input name="About_Sort" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" type="text" id="About_Sort" size="10" maxlength="10" value="<% =About_Sort%>" />
                                    <span class="tip">数字越小越靠前</span></td>
                            </tr>
                            <tr>
                                <td class="cell_title">页面显示</td>
                                <td class="cell_content">
                                    <input name="About_IsActive" type="radio" id="About_IsActive1" value="1" <% =Public.CheckedRadio(About_IsActive.ToString(), "1")%> />是
                                    <input type="radio" name="About_IsActive" id="About_IsActive2" value="0" <% =Public.CheckedRadio(About_IsActive.ToString(), "0")%> />否</td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="right">
                                    <input type="hidden" id="action" name="action" value="renew" />
                                    <input type="hidden" id="About_ID" name="About_ID" value="<% =About_ID%>" />
                                    <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                                    <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'about_list.aspx';" /></td>
                            </tr>
                        </table>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
