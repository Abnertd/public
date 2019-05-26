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
    private int list;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("db8de73b-9ac0-476e-866e-892dd35589c5");
        myApp = new Bid();
        tools = ToolsFactory.CreateTools();

        Bid_ID = tools.CheckInt(Request.QueryString["BID"]);
        list = tools.CheckInt(Request["list"]);
        list = list + 1;
        BidInfo entity = myApp.GetBidByID(Bid_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            if (entity.Bid_Type == 1)
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
    <link href="/scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/scripts/jquery-ui/jquery-ui.js"></script>
    <script src="/scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js"></script>
    <script src="/Scripts/My97DatePicker/WdatePicker.js"></script>
    <script>
        function choose_opt(curopt,totalopt)
        {
            for(var i=1;i<=totalopt;i++)
            {
                if(i==curopt)
                {
                    $("#frm_opt_"+i).attr("class","opt_cur");
                    $("#frm_optitem_"+i).show();
                }
                else
                {
                    $("#frm_opt_"+i).attr("class","opt_uncur");
                    $("#frm_optitem_"+i).hide();
                }
            }
        }
        window.onload = function () {
            choose_opt(<%=list%>,3);
                };
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
                            <td class="opt_cur" id="frm_opt_1"><%=Public.Page_ScriptOption("choose_opt(1,3);", "基本信息")%></td>

                            <td class="opt_gap">&nbsp;</td>
                            <td class="opt_uncur" id="frm_opt_2"><%=Public.Page_ScriptOption("choose_opt(2,3);", "商品清单")%></td>


                            <td class="opt_gap">&nbsp;</td>
                            <td class="opt_uncur" id="frm_opt_3"><%=Public.Page_ScriptOption("choose_opt(3,3);", "附件")%></td>


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
                                <td class="cell_content">
                                    <input name="Bid_Title" type="text" id="Bid_Title" size="50" maxlength="100" value="<% =Bid_Title%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">采购商</td>
                                <td class="cell_content">
                                    <input name="Bid_MemberCompany" type="text" id="Bid_MemberCompany" size="50" maxlength="100" value="<% =Bid_MemberCompany%>" /></td>
                            </tr>

                           <%-- <tr>
                                <td class="cell_title">报名时间</td>
                                <td class="cell_content">
                                    <input name="Bid_EnterStartTime" type="text" id="Bid_EnterStartTime" size="20" maxlength="50" readonly="readonly" value="<%=Bid_EnterStartTime.ToString("yyyy-MM-dd HH:mm:ss") %>" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });"/>
                                    至
                                    <input name="Bid_EnterEndTime" type="text" id="Bid_EnterEndTime" size="20" maxlength="50" readonly="readonly" value="<%=Bid_EnterEndTime.ToString("yyyy-MM-dd HH:mm:ss") %>" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });"/></td>

                            </tr>--%>
                            <tr>
                                <td class="cell_title">报价时间</td>
                                <td class="cell_content">
                                    <input name="Bid_BidStartTime" type="text" id="Bid_BidStartTime" size="20" maxlength="50" readonly="readonly" value="<%=Bid_BidStartTime.ToString("yyyy-MM-dd HH:mm:ss") %>" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });"/>
                                    至
                                    <input name="Bid_BidEndTime" type="text" id="Bid_BidEndTime" size="20" maxlength="50" readonly="readonly" value="<%=Bid_BidEndTime.ToString("yyyy-MM-dd HH:mm:ss") %>" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });"/></td>

                            </tr>
                            <tr>
                                <td class="cell_title">交货时间</td>
                                <td class="cell_content">
                                    <input name="Bid_DeliveryTime" type="text" id="Bid_DeliveryTime" size="20" maxlength="50" readonly="readonly" value="<%=Bid_DeliveryTime.ToString("yyyy-MM-dd") %>" /></td>
                                <script>$(function () { $("#Bid_DeliveryTime").datepicker({ inline: true }); })</script>
                            </tr>

                            <tr>
                                <td class="cell_title">报价轮次</td>
                                <td class="cell_content">
                                    <input name="Bid_Number" type="text" id="Bid_Number" size="50" maxlength="100" value="<%=Bid_Number %>" /></td>
                            </tr>

                            <tr>
                                <td class="cell_title">保证金</td>
                                <td class="cell_content">
                                    <input name="Bid_Bond" type="text" id="Bid_Bond" size="50" maxlength="100" value="<%=Bid_Bond %>" /></td>
                            </tr>

                            <tr>
                                <td class="cell_title">产品清单</td>
                                <td class="cell_content">
                                    <input name="Bid_ProductType" type="radio" id="Bid_ProductType1" value="1" <% =Public.CheckedRadio(Bid_ProductType.ToString(), "1")%> />展示
                                    <input type="radio" name="Bid_ProductType" id="Bid_ProductType2" value="0" <% =Public.CheckedRadio(Bid_ProductType.ToString(), "0")%> />不展示</td>
                            </tr>
                            <tr>
                                <td class="cell_title" valign="top">公告内容</td>
                                <td class="cell_content">
                                    <textarea cols="80" id="Bid_Content" name="Bid_Content" rows="16"><%=Bid_Content %></textarea>
                                    <script type="text/javascript">
                                        var About_ContentEditor;
                                        KindEditor.ready(function (K) {
                                            About_ContentEditor = K.create('#Bid_Content', {
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
                                <td class="cell_title"></td>
                                <td class="cell_content">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                        <tr>
                                            <td align="left">
                                                <input type="hidden" id="action" name="action" value="renew" />
                                                <input type="hidden" name="Bid_ID" id="Bid_ID" value="<%=Bid_ID %>" />

                                            </td>
                                            <td align="right">
                                                <input name="button" type="button" class="bt_grey" id="button1" value="保存并发布"  onclick="location = 'bid_do.aspx?action=Release&BID=<%=Bid_ID%>    ';" />&nbsp;&nbsp;<input name="save" type="submit" class="bt_orange" id="save" value="保存" />&nbsp;&nbsp;&nbsp;&nbsp;<input name="button" type="button" class="bt_grey" id="button" value="返回" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="    history.go(-1);" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_2" style="display: none;">
                            <tr>
                                <%--<td class="cell_title" style="text-align: center;">序号</td>--%>
                             <%--   <td class="cell_title" style="text-align: center;">物料编号</td>--%>
                                <td class="cell_title" style="text-align: center;">物料名称</td>
                                <td class="cell_title" style="text-align: center;">型号规格</td>
                              <%--  <td class="cell_title" style="text-align: center;">品牌</td>--%>
                                <td class="cell_title" style="text-align: center;">计量单位</td>
                                <td class="cell_title" style="text-align: center;">采购数量</td>
                                <td class="cell_title" style="text-align: center;">物流信息</td>
                                <td class="cell_title" style="text-align: center;">备注</td>
                                <td class="cell_title" style="text-align: center;">操作</td>
                            </tr>
                            <%=myApp.BidPoductList(Products,2) %>

                            <tr>
                                <td class="cell_content" colspan="10">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                        <tr>

                                            <td align="right">
                                                <input name="button" type="button" class="bt_grey" id="button" value="新增商品" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'bid_product_add.aspx?BID=<%=Bid_ID%>';" /></td></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_3" style="display: none;">
                            <tr>
                                <%--<td class="cell_title" style="text-align: center;">序号</td>--%>
                                <td class="cell_title" style="text-align: center;">附件名称</td>
                                <%--<td class="cell_title" style="text-align: center;">文件格式</td>
                                <td class="cell_title" style="text-align: center;">大小</td>--%>
                                <td class="cell_title" style="text-align: center;">说明</td>
                                <td class="cell_title" style="text-align: center;">查看</td>
                                <td class="cell_title" style="text-align: center;">操作</td>
                            </tr>
                            <%=myApp.BidAttachments(Atts,1) %>
                            <tr>
                                <td class="cell_content" colspan="7">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="5">
                                        <tr>

                                            <td align="right">
                                                <input name="button" type="button" class="bt_grey" id="button" value="新增附件" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'bid_Attachments_add.aspx?BID=<%=Bid_ID%>';" /></td></td>
                                        </tr>
                                    </table>
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
