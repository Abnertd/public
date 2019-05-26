<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    
    private ZhongXin myApp;
    private ITools tools;

    ZhongXinInfo entity;

    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new ZhongXin();
        tools = ToolsFactory.CreateTools();

        Public.CheckLogin("all");

        entity = myApp.GetZhongXinByID(tools.CheckInt(Request.QueryString["id"]));
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
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
                <td class="content_title">修改会员中信出金账号信息
                </td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="zhongxin_do.aspx">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
                            <tr>
                                <td class="cell_title">单位名称
                                </td>
                                <td class="cell_content">
                                    <input name="CompanyName" id="CompanyName" size="50" maxlength="50" value="<% =entity.CompanyName%>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">出金开户名
                                </td>
                                <td class="cell_content">
                                    <input name="OpenAccountName" id="OpenAccountName" size="50" maxlength="100" value="<% =entity.OpenAccountName%>" />
                                    <span class="tip">出金开户名务必与商家单位名称保持一致！！！</span>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">出金收款账号
                                </td>
                                <td class="cell_content">
                                    <input name="ReceiptAccount" id="ReceiptAccount" size="50" maxlength="50" value="<% =entity.ReceiptAccount%>" />
                                </td>
                            </tr>


                            <tr>
                                <td class="cell_title">中信附属账号
                                </td>
                                <td class="cell_content">
                                    <input name="SubAccount" id="SubAccount" size="50" maxlength="50" value="<% =entity.SubAccount%>" />
                                </td>
                            </tr>



                            <tr>
                                <td class="cell_title">出金银行
                                </td>
                                <td class="cell_content">
                                    <input type="radio" name="ReceiptBank" id="ReceiptBank" style="margin: 0;" value="中信银行" <% =Public.CheckedRadio(entity.ReceiptBank, "中信银行")%> />中信银行
                                    <input type="radio" name="ReceiptBank" id="ReceiptBank1" style="margin: 0;" value="其他银行" <% =Public.CheckedRadio(entity.ReceiptBank, "其他银行")%> />其他银行
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">出金银行名称
                                </td>
                                <td class="cell_content">
                                    <input name="BankName" id="BankName" size="50" maxlength="100" value="<% =entity.BankName%>" />
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">出金银行行号
                                </td>
                                <td class="cell_content">
                                    <input name="BankCode" id="BankCode" size="50" maxlength="12" value="<% =entity.BankCode%>" />
                                </td>
                            </tr>


                            <tr>
                                <td class="cell_title">平台审核
                                </td>
                                <td class="cell_content">
                                    <input type="radio" name="Audit" value="1" <% =Public.CheckedRadio(entity.Audit.ToString(), "1")%> />审核通过
                                <input type="radio" name="Audit" value="0" <% =Public.CheckedRadio(entity.Audit.ToString(), "0")%> />审核不通过
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="right">
                                    <input type="hidden" id="action" name="action" value="renew" />
                                    <input type="hidden" id="ID" name="ID" value="<% =entity.ID%>" />
                                    <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                                    <%--<input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';"
                                        onmouseout="this.className='bt_grey';" onclick="location = 'signing_list.aspx';" />--%>
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
