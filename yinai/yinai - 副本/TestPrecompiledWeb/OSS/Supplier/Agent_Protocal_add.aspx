<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    private Supplier supplier;
    private Contract myAppC;
    private Addr addr;
    string Purchase_Title, Purchase_SupplierName, Purchase_DeliveryTime, Purchase_ValidDate, Purchase_Addtime, Purchase_Address;
    string templateContent = "";
    int PurchaseID = 0;
    private SupplierPurchaseInfo purchaseinfo = null;


    SupplierAgentProtocalInfo agentprotocalinfo = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("e0920e95-65fa-4e3c-9dd6-2794ccc45782");
        Tools tools = new Tools();
        supplier = new Supplier();
        myAppC = new Contract();
        addr = new Addr();
        PurchaseID = tools.CheckInt(Request["Purchase_ID"]);

        SupplierAgentProtocalInfo agentprotocal = supplier.GetSupplierAgentProtocalByPurchaseID(PurchaseID);
        if (agentprotocal != null)
        {
            Public.Msg("error", "错误信息", "该采购申请已存在协议", false, "{back}");
            Response.End();
        }
        purchaseinfo = supplier.GetSupplierPurchaseByID(PurchaseID);

        if (purchaseinfo == null)
        {

            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {

            if (purchaseinfo.Purchase_Status != 2)
            {
                Public.Msg("error", "错误信息", "该信息未通过审核", false, "{back}");
                Response.End();
            }
            if (purchaseinfo.Purchase_IsActive != 1)
            {
                Public.Msg("error", "错误信息", "该信息已被挂起", false, "{back}");
                Response.End();
            }
            if (purchaseinfo.Purchase_SupplierID == 0)//#
            {
                Public.Msg("error", "错误信息", "不能对平台发布申请创建采购协议", false, "{back}");
                Response.End();
            }
            if (purchaseinfo.Purchase_Trash != 0)
            {
                Public.Msg("error", "错误信息", "该信息已被移至回收站", false, "{back}");
                Response.End();
            }
            if (purchaseinfo.Purchase_ValidDate < tools.NullDate(DateTime.Now.ToShortDateString()))
            {
                Public.Msg("error", "错误信息", "该信息已过期", false, "{back}");
                Response.End();
            }
            if (purchaseinfo.Purchase_TypeID != 1)
            {
                Public.Msg("error", "错误信息", "该信息类型不是代理采购", false, "{back}");
                Response.End();
            }

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
        ContractTemplateInfo contracttemplateinfo = myAppC.GetContractTemplateBySign("Agent_Protocal_Template");
        if (contracttemplateinfo != null)
        {
            templateContent = contracttemplateinfo.Contract_Template_Content;
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
                    添加代理采购协议
                </td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="Agent_Protocal_do.aspx">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
                        <%-- <tr>
          <td class="cell_title">协议编号</td>
          <td class="cell_content"><input name="Protocal_Code" type="text" id="Protocal_Code" size="50" maxlength="50" /> <span class="t14_red">*</span></td>
        </tr>--%>
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
                                <textarea cols="80" name="Protocal_Template" rows="16"><%=templateContent%></textarea>
                                <script type="text/javascript">
                                    CKEDITOR.replace('Protocal_Template');
                                </script>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" border="0" cellspacing="0" cellpadding="5">
                        <tr>
                            <td align="right">
                                <input type="hidden" id="action" name="action" value="new" />
                                <input type="hidden" id="PurchaseID" name="PurchaseID" value="<%=PurchaseID %>" />
                                <input name="save" type="submit" class="bt_orange" id="save" value="保存" onclick="this.form.action.value='new';" />
                                <input name="button" type="button" class="bt_grey" id="button" value="返回" onmouseover="this.className='bt_orange';"
                                    onmouseout="this.className='bt_grey';" onclick="location='Supplier_Purchase_list.aspx';" />
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
