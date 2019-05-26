<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html>

<script runat="server">
    private Supplier myApp;
    private ITools tools;

    private string Supplier_Margin_Site;
    private int Supplier_Margin_ID, Supplier_Margin_Type;
    private double Supplier_Margin_Amount;

    protected void Page_Load(object sender, EventArgs e)
    {
        myApp = new Supplier();
        tools = ToolsFactory.CreateTools();

        Supplier_Margin_ID = tools.CheckInt(Request.QueryString["Supplier_Margin_ID"]);
        SupplierMarginInfo entity = myApp.GetSupplierMarginByID(Supplier_Margin_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Supplier_Margin_ID = entity.Supplier_Margin_ID;
            Supplier_Margin_Type = entity.Supplier_Margin_Type;
            Supplier_Margin_Amount = entity.Supplier_Margin_Amount;
            Supplier_Margin_Site = entity.Supplier_Margin_Site;
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>修改保证金标准</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">修改保证金标准
                </td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="/supplier/Supplier_Ｍargin_do.aspx">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">

                            <tr>
                                <td class="cell_title">类型
                                </td>
                                <td class="cell_content">
                                    <%=myApp.ShowSupplierCertName(Supplier_Margin_Type)%>
                                </td>
                            </tr>

                            <tr>
                                <td class="cell_title">保证金金额
                                </td>
                                <td class="cell_content">
                                    <input type="text" id="Supplier_Margin_Amount" name="Supplier_Margin_Amount" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="<%=Supplier_Margin_Amount %>" /> 元
                                </td>
                            </tr>

                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="right">
                                    <input type="hidden" id="action" name="action" value="renew" />
                                    <input type="hidden" id="Supplier_Margin_ID" name="Supplier_Margin_ID" value="<%=Supplier_Margin_ID %>" />
                                    <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                                    <input name="button" type="button" class="bt_grey" id="button" value="返回" onmouseover="this.className='bt_orange';"
                                        onmouseout="this.className='bt_grey';" onclick="location = 'Supplier_margin_list.aspx';" />
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
