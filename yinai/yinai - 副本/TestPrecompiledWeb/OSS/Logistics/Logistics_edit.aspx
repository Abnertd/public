<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    private Logistics myApp;
    private ITools tools;

    private string Logistics_NickName, Logistics_Password, Logistics_CompanyName, Logistics_Name, Logistics_Tel;
    private int Logistics_ID, Logistics_Status;
    private DateTime Logistics_Addtime;


    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("bd38ff8b-f627-44ec-9275-39c9df7425e1");
        myApp = new Logistics();
        tools = ToolsFactory.CreateTools();

        Logistics_ID = tools.CheckInt(Request.QueryString["Logistics_ID"]);
        LogisticsInfo entity = myApp.GetLogisticsByID(Logistics_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Logistics_ID = entity.Logistics_ID;
            Logistics_NickName = entity.Logistics_NickName;
            Logistics_Password = entity.Logistics_Password;
            Logistics_CompanyName = entity.Logistics_CompanyName;
            Logistics_Name = entity.Logistics_Name;
            Logistics_Tel = entity.Logistics_Tel;
            Logistics_Status = entity.Logistics_Status;
            Logistics_Addtime = entity.Logistics_Addtime;

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
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">修改物流商</td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="Logistics_do.aspx">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
                            <tr>
                                <td class="cell_title">物流商登录名</td>
                                <td class="cell_content">
                                    <input name="Logistics_NickName" type="text" id="Logistics_NickName" size="50" maxlength="100"  value="<%=Logistics_NickName %>" readonly="readonly"/></td>
                            </tr>

                            <tr>
                                <td class="cell_title">登录密码</td>
                                <td class="cell_content">
                                    <input name="Logistics_Password" type="password" id="Logistics_Password" size="50" maxlength="100" /></td>
                            </tr>

                            <tr>
                                <td class="cell_title">确认密码</td>
                                <td class="cell_content">
                                    <input name="password_confirm" type="password" id="password_confirm" size="50" maxlength="100" /></td>
                            </tr>

                            <tr>
                                <td class="cell_title">物流商公司名</td>
                                <td class="cell_content">
                                    <input name="Logistics_CompanyName" type="text" id="Logistics_CompanyName" size="50" maxlength="100"  value="<%=Logistics_CompanyName %>"/></td>
                            </tr>

                            <tr>
                                <td class="cell_title">联系人</td>
                                <td class="cell_content">
                                    <input name="Logistics_Name" type="text" id="Logistics_Name" size="50" maxlength="100"  value="<%=Logistics_Name %>"/></td>
                            </tr>
                            <tr>
                                <td class="cell_title">联系电话</td>
                                <td class="cell_content">
                                    <input name="Logistics_Tel" type="text" id="Logistics_Tel" size="50" maxlength="100" value="<%=Logistics_Tel %>"/></td>
                            </tr>
                            <tr>
                                <td class="cell_title">是否启用</td>
                                <td class="cell_content">
                                    <input name="Logistics_Status" type="radio" id="Logistics_Status1" value="1" <% =Public.CheckedRadio(Logistics_Status.ToString(), "1")%> />是
                                    <input type="radio" name="Logistics_Status" id="Logistics_Status2" value="0" <% =Public.CheckedRadio(Logistics_Status.ToString(), "0")%> />否</td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="right">
                                    <input type="hidden" id="action" name="action" value="renew" />
                                    <input type="hidden" id="Logistics_ID" name="Logistics_ID" value="<% =Logistics_ID%>" />
                                    <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                                    <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'Logistics_list.aspx';" /></td>
                            </tr>
                        </table>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
