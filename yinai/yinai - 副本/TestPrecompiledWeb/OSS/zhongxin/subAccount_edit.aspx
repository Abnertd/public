<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    
    private ZhongXin myApp;
    private ITools tools;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new ZhongXin();
        tools = ToolsFactory.CreateTools();
        
        Public.CheckLogin("all");
       
    }

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Public/ckeditor/ckeditor.js"></script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">
                    附属账户修改
                </td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="zhongxin_do.aspx">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
                        <tr>
                            <td class="cell_title">
                                附属账号
                            </td>
                            <td class="cell_content">
                                <input name="subAccNo" id="subAccNo" size="50" value="" />
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_title">
                                附属账户名称
                            </td>
                            <td class="cell_content">
                                <input name="subAccNm" id="subAccNm" size="50" value="" />
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="0" cellpadding="5">
                        <tr>
                            <td align="right">
                                <input type="hidden" id="action" name="action" value="subaccount" />
                                <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                            </td>
                        </tr>
                    </table>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
