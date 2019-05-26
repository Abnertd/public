<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="System.Collections.Generic" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    
    private Bid myApp;
    private ITools tools;

    private string Bid_MemberCompany, Bid_SupplierCompany, Bid_Title, Bid_Content, Bid_Contract, Bid_AuditRemarks, Bid_SN, Bid_OrdersSN;
    private int Bid_ID, Bid_MemberID, Bid_SupplierID, Bid_Number, Bid_Status, Bid_ProductType, Bid_Type, Bid_IsAudit, Bid_ExcludeSupplierID, Bid_IsOrders;
    private DateTime Bid_EnterStartTime, Bid_EnterEndTime, Bid_BidStartTime, Bid_BidEndTime, Bid_AddTime, Bid_AuditTime, Bid_DeliveryTime, Bid_OrdersTime, Bid_FinishTime;
    private double Bid_Bond, Bid_AllPrice;
    private IList<BidProductInfo> Products = null;
    private IList<BidAttachmentsInfo> Atts = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("db8de73b-9ac0-476e-866e-892dd35589c5");
        myApp = new Bid();
        tools = ToolsFactory.CreateTools();

        Bid_ID = tools.CheckInt(Request.QueryString["BID"]);
        BidInfo entity = myApp.GetBidByID(Bid_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            if(entity.Bid_Type==1)
            {
                Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
                Response.End();
            }
            Bid_ID = entity.Bid_ID;
            Bid_MemberID = entity.Bid_MemberID;
            Bid_MemberCompany = entity.Bid_MemberCompany;
            Bid_SupplierID = entity.Bid_SupplierID;
            Bid_SupplierCompany = entity.Bid_SupplierCompany;
            Bid_Title = entity.Bid_Title;
            Bid_EnterStartTime = entity.Bid_EnterStartTime;
            Bid_EnterEndTime = entity.Bid_EnterEndTime;
            Bid_BidStartTime = entity.Bid_BidStartTime;
            Bid_BidEndTime = entity.Bid_BidEndTime;
            Bid_AddTime = entity.Bid_AddTime;
            Bid_Bond = entity.Bid_Bond;
            Bid_Number = entity.Bid_Number;
            Bid_Status = entity.Bid_Status;
            Bid_Content = entity.Bid_Content;
            Bid_ProductType = entity.Bid_ProductType;
            Bid_AllPrice = entity.Bid_AllPrice;
            Bid_Type = entity.Bid_Type;
            Bid_Contract = entity.Bid_Contract;
            Bid_IsAudit = entity.Bid_IsAudit;
            Bid_AuditTime = entity.Bid_AuditTime;
            Bid_AuditRemarks = entity.Bid_AuditRemarks;
            Bid_ExcludeSupplierID = entity.Bid_ExcludeSupplierID;
            Bid_SN = entity.Bid_SN;
            Bid_DeliveryTime = entity.Bid_DeliveryTime;
            Bid_IsOrders = entity.Bid_IsOrders;
            Bid_OrdersTime = entity.Bid_OrdersTime;
            Bid_OrdersSN = entity.Bid_OrdersSN;
            Bid_FinishTime = entity.Bid_FinishTime;
            Products = entity.BidProducts;
            Atts = entity.BidAttachments;
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
        function Audit(obj)
        {          
            $("#action").val(obj);
            $('#formadd').submit();
        }

    </script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">招标详情</td>
            </tr>
            <tr>
                <td valign="top" height="31" class="opt_foot">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="opt_gap">&nbsp;</td>
                            <td class="opt_cur" id="frm_opt_1"><%=Public.Page_ScriptOption("choose_opt(1,4);", "基本信息")%></td>

                            <td class="opt_gap">&nbsp;</td>
                            <td class="opt_uncur" id="frm_opt_2"><%=Public.Page_ScriptOption("choose_opt(2,4);", "内容公告")%></td>

                            <td class="opt_gap">&nbsp;</td>
                            <td class="opt_uncur" id="frm_opt_3"><%=Public.Page_ScriptOption("choose_opt(3,4);", "商品清单")%></td>


                            <td class="opt_gap">&nbsp;</td>
                            <td class="opt_uncur" id="frm_opt_4"><%=Public.Page_ScriptOption("choose_opt(4,4);", "附件")%></td>


                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="/bid/bid_do.aspx">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_1">
                        <tr>
                            <td class="cell_title">公告标题</td>
                            <td class="cell_content"><% =Bid_Title%></td>
                        </tr>
                        <tr>
                            <td class="cell_title">采购商</td>
                            <td class="cell_content"><% =Bid_MemberCompany%></td>
                        </tr>

                      <%--  <tr>
                            <td class="cell_title">报名时间</td>
                            <td class="cell_content"><% =Bid_EnterStartTime.ToString("yyyy-MM-dd HH:mm:ss")%>至<%=Bid_EnterEndTime.ToString("yyyy-MM-dd HH:mm:ss") %></td>
                        </tr>--%>

                         <tr>
                            <td class="cell_title">报价时间</td>
                            <td class="cell_content"><% =Bid_BidStartTime.ToString("yyyy-MM-dd HH:mm:ss")%>至<%=Bid_BidEndTime.ToString("yyyy-MM-dd HH:mm:ss") %></td>
                        </tr>

                        <tr>
                            <td class="cell_title">交货时间</td>
                            <td class="cell_content"><% =Bid_DeliveryTime.ToString("yyyy-MM-dd")%></td>
                        </tr>

                        <tr>
                            <td class="cell_title">报价轮次</td>
                            <td class="cell_content"><% =Bid_Number%></td>
                        </tr>

                        <tr>
                            <td class="cell_title">保证金</td>
                            <td class="cell_content"><% =Public.DisplayCurrency(Bid_Bond)%></td>
                        </tr>
                        <tr>
                            <td class="cell_title">产品清单</td>
                            <td class="cell_content"><% if (Bid_ProductType == 0) { Response.Write("不展示"); } else { Response.Write("展示"); }%></td>
                        </tr>
                        
                        <tr>
                            <td class="cell_title">添加时间</td>
                            <td class="cell_content"><%=Bid_AddTime%></td>
                        </tr>

                        <tr>
                            <td class="cell_title">审核状态</td>
                            <td class="cell_content"><% =myApp.IsAudit(Bid_IsAudit)%></td>
                        </tr>
                        <%if (Bid_IsAudit>0) {%>
                        <tr>
                            <td class="cell_title">审核时间</td>
                            <td class="cell_content"><% =Bid_AuditTime%></td>
                        </tr>
                        <tr>
                            <td class="cell_title">审核备注</td>
                            <td class="cell_content"><% =Bid_AuditRemarks%></td>
                        </tr>
                        <%}else{ %>

                        <tr>
                            <td class="cell_title">审核备注</td>
                            <td class="cell_content"><input name="Bid_AuditRemarks" type="text" id="Bid_AuditRemarks" size="80" maxlength="80" value="" /></td>
                        </tr>

                        <%} %>
                        <%if(Bid_SupplierID>0) {%>
                        <tr>
                            <td class="cell_title">中标单位</td>
                            <td class="cell_content"><%=Bid_SupplierCompany%></td>
                        </tr>
                        <%} %>
                    </table>
                        <div id="frm_optitem_2" style="display: none; width:100%;">
                            <%=Bid_Content %>
                        </div>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_3" style="display: none;">
                        <tr>
                            <%--<td class="cell_title" style="text-align:center;">序号</td>--%>
                          <%--  <td class="cell_title" style="text-align:center;">物料编号</td>--%>
                            <td class="cell_title" style="text-align:center;">物料名称</td>
                            <td class="cell_title" style="text-align:center;">型号规格</td>
                        <%--    <td class="cell_title" style="text-align:center;">品牌</td>--%>
                            <td class="cell_title" style="text-align:center;">计量单位</td>
                            <td class="cell_title" style="text-align:center;">采购数量</td>
                            <td class="cell_title" style="text-align:center;">物流信息</td>
                            <td class="cell_title" style="text-align:center;">备注</td>
                        </tr>
                        <%=myApp.BidPoductList(Products,0) %>
                    </table>
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_4" style="display: none;">
                        <tr>
                            <%--<td class="cell_title" style="text-align:center;">序号</td>--%>
                            <td class="cell_title" style="text-align:center;">附件名称</td>
<%--                            <td class="cell_title" style="text-align:center;">文件格式</td>
                            <td class="cell_title" style="text-align:center;">大小</td>--%>
                            <td class="cell_title" style="text-align:center;">说明</td>
                            <td class="cell_title" style="text-align:center;">操作</td>
                        </tr>
                        <%=myApp.BidAttachments(Atts,0) %>
                    </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="left">
                                    <%if(Bid_IsAudit==0) {%>


                                  
                                    <input name="button" type="button" class="bt_orange" id="button1" value="审核通过" onclick="Audit('Audit');" />
                                    <input name="button" type="button" class="bt_orange" id="button2" value="审核不通过" onclick="Audit('NotAudit');" />
                              
                                <%} %>
                                   <%--  <%if(Bid_IsAudit==1) {%>--%>
                                  
                           
                            <%--    <%} %>--%>
         <%if(Bid_IsAudit!=2) { %>
                                      <input name="button" type="button" class="bt_orange" id="button3" value="冻结" onclick="Audit('Frozen');" />
                                      <input name="button" type="button" class="bt_orange" id="button4" value="取消冻结" onclick="Audit('CancelFrozen');" />
                                   
                                     <input type="hidden" name="action" id="action" value="" />
                                    <input type="hidden" name="BID" id="BID" value="<%=Bid_ID %>"/>
                                    <%} %>
                                    
                                     </td>
                                <td align="right"><input name="button" type="button" class="bt_grey" id="button" value="返回" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="history.go(-1);"/></td>
                            </tr>
                        </table>
                        </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
