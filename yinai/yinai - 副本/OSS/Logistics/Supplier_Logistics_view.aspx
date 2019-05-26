<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    

    private SupplierLogistics myApp;
    private Logistics MyLogistics;
    private ITools tools;
    private Addr Addr;
    private string Supplier_Orders_Address_Country, Supplier_Orders_Address_State, Supplier_Orders_Address_City, Supplier_Orders_Address_County, Supplier_Orders_Address_StreetAddress, Supplier_Address_Country, Supplier_Address_State, Supplier_Address_City, Supplier_Address_County, Supplier_Address_StreetAddress, Supplier_Logistics_Name, Supplier_Logistics_Number, Supplier_Logistics_AuditRemarks;
    private int Supplier_Logistics_ID, Supplier_SupplierID, Supplier_OrdersID, Supplier_LogisticsID, Supplier_Status, Supplier_Logistics_IsAudit;
    private DateTime Supplier_Logistics_DeliveryTime, Supplier_Logistics_AuditTime, Supplier_Logistics_FinishTime;
    private double Supplier_Logistics_Price;
    private int Supplier_Logistics_TenderID;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("64bb04aa-9b78-4c41-ae9c-e94f57581e22");
        myApp = new SupplierLogistics();
        MyLogistics = new Logistics();
        tools = ToolsFactory.CreateTools();
        Addr = new Addr();
        Supplier_Logistics_ID = tools.CheckInt(Request.QueryString["Supplier_Logistics_ID"]);
        SupplierLogisticsInfo entity = myApp.GetSupplierLogisticsByID(Supplier_Logistics_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Supplier_Logistics_ID = entity.Supplier_Logistics_ID;
            Supplier_SupplierID = entity.Supplier_SupplierID;
            Supplier_OrdersID = entity.Supplier_OrdersID;
            Supplier_LogisticsID = entity.Supplier_LogisticsID;
            Supplier_Status = entity.Supplier_Status;
            Supplier_Orders_Address_Country = entity.Supplier_Orders_Address_Country;
            Supplier_Orders_Address_State = entity.Supplier_Orders_Address_State;
            Supplier_Orders_Address_City = entity.Supplier_Orders_Address_City;
            Supplier_Orders_Address_County = entity.Supplier_Orders_Address_County;
            Supplier_Orders_Address_StreetAddress = entity.Supplier_Orders_Address_StreetAddress;
            Supplier_Address_Country = entity.Supplier_Address_Country;
            Supplier_Address_State = entity.Supplier_Address_State;
            Supplier_Address_City = entity.Supplier_Address_City;
            Supplier_Address_County = entity.Supplier_Address_County;
            Supplier_Address_StreetAddress = entity.Supplier_Address_StreetAddress;
            Supplier_Logistics_Name = entity.Supplier_Logistics_Name;
            Supplier_Logistics_Number = entity.Supplier_Logistics_Number;
            Supplier_Logistics_DeliveryTime = entity.Supplier_Logistics_DeliveryTime;
            Supplier_Logistics_IsAudit = entity.Supplier_Logistics_IsAudit;
            Supplier_Logistics_AuditTime = entity.Supplier_Logistics_AuditTime;
            Supplier_Logistics_AuditRemarks = entity.Supplier_Logistics_AuditRemarks;
            Supplier_Logistics_FinishTime = entity.Supplier_Logistics_FinishTime;
            Supplier_Logistics_TenderID = entity.Supplier_Logistics_TenderID;
            Supplier_Logistics_Price = entity.Supplier_Logistics_Price;
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
    <script src="/Scripts/common.js" type="text/javascript"></script>
        <script>
            function Audit(obj) {
                $("#action").val(obj);
                $('#formadd').submit();
            }

    </script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">物流详情</td>
            </tr>
            <tr>
                <td valign="top" height="31" class="opt_foot">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="opt_gap">&nbsp;</td>
                            <td class="opt_cur" id="frm_opt_1"><%=Public.Page_ScriptOption("choose_opt(1,2);", "物流详情")%></td>

                            <td class="opt_gap">&nbsp;</td>
                            <td class="opt_uncur" id="frm_opt_2"><%=Public.Page_ScriptOption("choose_opt(2,2);", "物流商报价")%></td>



                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="Supplier_Logistics_do.aspx">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_1">
                            <tr>
                                <td class="cell_title">货物名称</td>
                                <td class="cell_content"><%=Supplier_Logistics_Name %></td>
                            </tr>

                            <tr>
                                <td class="cell_title">数量</td>
                                <td class="cell_content"><%=Supplier_Logistics_Number %></td>
                            </tr>

                            <tr>
                                <td class="cell_title">发货时间</td>
                                <td class="cell_content"><%=Supplier_Logistics_DeliveryTime.ToString("yyyy-MM-dd") %></td>
                            </tr>

                            <tr>
                                <td class="cell_title">发货地</td>
                                <td class="cell_content"><%=Addr.DisplayAddress(Supplier_Address_State, Supplier_Address_City, Supplier_Address_County) + " " + Supplier_Address_StreetAddress %></td>
                            </tr>

                            <tr>
                                <td class="cell_title">收货地</td>
                                <td class="cell_content"><%=Addr.DisplayAddress(Supplier_Orders_Address_State, Supplier_Orders_Address_City, Supplier_Orders_Address_County) + " " + Supplier_Orders_Address_StreetAddress %></td>
                            </tr>

                            <tr>
                                <td class="cell_title">货物清单</td>
                                <td class="cell_content"><a href="/orders/orders_view.aspx?orders_id=<%=Supplier_OrdersID %>" target="_blank">点击查看</a></td>
                            </tr>

                            <%if (Supplier_Logistics_TenderID > 0 && Supplier_LogisticsID>0) {%>
                            <tr>
                                <td class="cell_title">物流商</td>
                                <td class="cell_content"><%=MyLogistics.GetLogisticsNameByID(Supplier_LogisticsID)%></td>
                            </tr>

                            <tr>
                                <td class="cell_title">物流价格</td>
                                <td class="cell_content"><%=Public.DisplayCurrency(Supplier_Logistics_Price)%></td>
                            </tr>
                            <%} %>
                            <%if (Supplier_Logistics_IsAudit > 0)
                              {%>
                        <tr>
                            <td class="cell_title">审核时间</td>
                            <td class="cell_content"><% =Supplier_Logistics_AuditTime%></td>
                        </tr>
                        <tr>
                            <td class="cell_title">审核备注</td>
                            <td class="cell_content"><% =Supplier_Logistics_AuditRemarks%></td>
                        </tr>
                        <%}else{ %>

                        <tr>
                            <td class="cell_title">审核备注</td>
                            <td class="cell_content"><input name="Supplier_Logistics_AuditRemarks" type="text" id="Supplier_Logistics_AuditRemarks" size="80" maxlength="80" value="" /></td>
                        </tr>

                        <%} %>
                        </table>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_2" style="display: none;">
                        <tr>
                            <td class="cell_title" style="text-align:center;">物流商</td>
                            <td class="cell_title" style="text-align:center;">报价</td>
                            <td class="cell_title" style="text-align:center;">添加时间</td>
                            <td class="cell_title" style="text-align:center;">是否中标</td>
                        </tr>
                            <%=myApp.LogisticsTendersList(Supplier_Logistics_ID) %>
                    </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            
                            <tr>
                                <td align="left">
                                    <%if (Supplier_Logistics_IsAudit == 0 && Public.CheckPrivilege("65632742-f14a-4e44-8f7d-64e56c866da4")) {%>
                                    <input name="button" type="button" class="bt_orange" id="button1" value="审核通过" onclick="Audit('Audit');" />
                                    <input name="button" type="button" class="bt_orange" id="button2" value="审核不通过" onclick="Audit('NotAudit');" />
                               <input type="hidden" name="action" id="action" value="" />
                                    <input type="hidden" name="Supplier_Logistics_ID" id="Supplier_Logistics_ID" value="<%=Supplier_Logistics_ID %>"/>
                                <%} %>
                                     </td>
                                <td align="right">

                                    <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'Supplier_Logistics_List.aspx';" /></td>
                            </tr>
                        </table>



                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
