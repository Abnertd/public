<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    private Supplier supplier;
    private Contract myAppC;
    private Addr addr;
    string Purchase_Title, Purchase_SupplierName, Purchase_DeliveryTime, Purchase_ValidDate, Purchase_Addtime, Purchase_Address;
    string Protocal_Template, Protocal_Code;
    int Protocal_PurchaseID = 0;
    int Protocal_ID = 0;
    int Protocal_Status = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("7abc095a-d322-4312-861c-aecb6088c3bb");
        Tools tools = new Tools();
        supplier = new Supplier();
        myAppC = new Contract();
        addr = new Addr();
        Protocal_ID = tools.CheckInt(Request["Protocal_ID"]);

        SupplierAgentProtocalInfo agentprotocalinfo = supplier.GetSupplierAgentProtocalByID(Protocal_ID);
        if (agentprotocalinfo == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
        }
        else
        {
            Protocal_PurchaseID = agentprotocalinfo.Protocal_PurchaseID;
            Protocal_Status = agentprotocalinfo.Protocal_Status;
            Protocal_Template = agentprotocalinfo.Protocal_Template;
            Protocal_Code = agentprotocalinfo.Protocal_Code;
        }

        SupplierPurchaseInfo purchaseinfo = supplier.GetSupplierPurchaseByID(Protocal_PurchaseID);
        if (purchaseinfo == null)
        {

            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Purchase_Title = purchaseinfo.Purchase_Title;
            SupplierInfo supplierinfo = supplier.GetSupplierByID(purchaseinfo.Purchase_SupplierID);
            if (supplierinfo != null)
            {
                Purchase_SupplierName = supplierinfo.Supplier_CompanyName;
            }
            Purchase_DeliveryTime = purchaseinfo.Purchase_DeliveryTime.ToShortDateString();
            Purchase_Addtime = purchaseinfo.Purchase_Addtime.ToShortDateString();
            Purchase_ValidDate = purchaseinfo.Purchase_ValidDate.ToShortDateString();
            Purchase_Address = addr.DisplayAddress(purchaseinfo.Purchase_State, purchaseinfo.Purchase_City, purchaseinfo.Purchase_County) + " " + purchaseinfo.Purchase_Address;

        }
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="/Public/ckeditor/ckeditor.js"></script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">
                    修改代理采购协议
                </td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="Agent_Protocal_do.aspx">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
                        <tr>
                            <td class="cell_title">
                                协议编号
                            </td>
                            <td class="cell_content">
                                <%=Protocal_Code %>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_title">
                                采购标题
                            </td>
                            <td class="cell_content">
                                <%=Purchase_Title%>
                            </td>
                        </tr>
                                     <tr>
                            <td class="cell_title">
                                采购会员
                            </td>
                            <td class="cell_content">
                                <%=Purchase_SupplierName%>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_title">
                                采购发布时间
                            </td>
                            <td class="cell_content">
                                <%=Purchase_Addtime%>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_title">
                                采购交货时间
                            </td>
                            <td class="cell_content">
                                <%=Purchase_DeliveryTime%>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_title">
                                采购交货地点
                            </td>
                            <td class="cell_content">
                                <%=Purchase_Address%>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_title">
                                采购有效时间
                            </td>
                            <td class="cell_content">
                                <%=Purchase_ValidDate%>
                            </td>
                        </tr>
                        <tr>
                            <td class="cell_title" valign="top">
                                模版内容
                            </td>
                            <td class="cell_content">
                                <%
                                    if (Protocal_Status == 0)
                                    {
                                %>
                                <textarea cols="80" name="Protocal_Template" rows="16"><%=Protocal_Template%></textarea>
                                <script type="text/javascript">
                                    CKEDITOR.replace('Protocal_Template');
                                </script>
                                <% }
                                    else
                                    {%>
                                <%=Protocal_Template%>
                                <%}
                                %>
                            </td>
                        </tr>
                         <tr>
                            <td class="cell_title">
                                协议状态
                            </td>
                            <td class="cell_content">
                                <%=supplier.ConvertAgentProtocalStatus(Protocal_Status)%>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="0" cellpadding="5">
                        <tr>
                            <td align="right">
                                <input type="hidden" id="action" name="action" value="renew" />
                                <input type="hidden" id="Protocal_ID" name="Protocal_ID" value="<%=Protocal_ID %>" />
                                <% if (Protocal_Status == 0 && Public.CheckPrivilege("7abc095a-d322-4312-861c-aecb6088c3bb"))
                                   {
                                %>
                                <input name="save" type="submit" class="bt_orange" id="Submit2" value="保存修改" onclick="this.form.action.value='renew';" />
                                <%--  <%}
                                   if (Protocal_Status == 1)
                                   {%>
                                <input name="save" type="submit" class="bt_orange" id="Submit1" value="平台确认" onclick="this.form.action.value='audit';" />--%>
                                <%} %>
                                <input name="button" type="button" class="bt_grey" id="button" value="返回" onmouseover="this.className='bt_orange';"
                                    onmouseout="this.className='bt_grey';" onclick="location='agent_protocal_list.aspx';" />
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
