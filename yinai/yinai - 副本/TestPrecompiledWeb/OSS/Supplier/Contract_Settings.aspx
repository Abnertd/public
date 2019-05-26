<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%
    string Supplier_Nickname = "--";
    string Member_Company = "--";
    Public.CheckLogin("40f51178-030c-402a-bee4-57ed6d1ca03f");
    ITools tools = ToolsFactory.CreateTools();
    Supplier Supplier = new Supplier();
    Member Member = new Member();
    Contract contract = new Contract();
    int Supplier_id = tools.CheckInt(Request["Supplier_id"]);
    SupplierInfo supplierinfo = Supplier.GetSupplierByID(Supplier_id);



    if (supplierinfo == null)
    {
        Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
        Response.End();
    }
    else
    {
        MemberInfo memberinfo = Member.GetMemberByEmail(supplierinfo.Supplier_Email);
        if (memberinfo != null)
        {
            Supplier_Nickname = memberinfo.Member_NickName;
            MemberProfileInfo memberprofile = Member.GetMemberProfileByID(memberinfo.Member_ID);
            if (memberprofile != null)
            {
                Member_Company = memberprofile.Member_Company;
            }

        }

    }
%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">设置合同模板</td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="/supplier/supplier_do.aspx">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
                            <tr>
                                <td class="cell_title">供应商名称</td>
                                <td class="cell_content"><%=Member_Company%> 【<%=Supplier_Nickname%>】</td>
                            </tr>
                            <tr>
                                <td class="cell_title">选择合同模板</td>
                                <td class="cell_content">
                                    <select name="Contract_TemplateID" id="Contract_TemplateID">
                                        <option value="0">请选择合同模板</option>
                                        <%=contract.DisplayContractTemplate(supplierinfo.Supplier_ContractID) %>
                                    </select></td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="right">
                                    <input type="hidden" id="action" name="action" value="contract_settings" />
                                    <input type="hidden" id="supplier_id" name="supplier_id" value="<%=supplierinfo.Supplier_ID %>" />
                                    <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                                    <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'Supplier_list.aspx';" /></td>
                            </tr>
                        </table>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
