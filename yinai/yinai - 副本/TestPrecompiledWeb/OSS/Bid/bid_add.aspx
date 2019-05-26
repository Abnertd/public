<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    DateTime Today;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("e202397a-bb1e-4e67-b008-67701d37c5cb");
         Today = DateTime.Now;
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
        <link href="/scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/scripts/jquery-ui/jquery-ui.js"></script>
    <script src="/scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js"></script>
    <script src="/Scripts/My97DatePicker/WdatePicker.js"></script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">添加招标</td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="bid_do.aspx">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
                            <tr>
                                <td class="cell_title">公告标题</td>
                                <td class="cell_content">
                                    <input name="Bid_Title" type="text" id="Bid_Title" size="50" maxlength="100" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">采购商</td>
                                <td class="cell_content">
                                    <input name="Bid_MemberCompany" type="text" id="Bid_MemberCompany" size="50" maxlength="100" value="平台" /></td>
                            </tr>

                           <%-- <tr>
                                <td class="cell_title">报名时间</td>
                                <td class="cell_content">
                                    <input name="Bid_EnterStartTime" type="text" id="Bid_EnterStartTime" size="20" maxlength="50" readonly="readonly" value="<%=Today.ToString("yyyy-MM-dd HH:mm:ss") %>" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });"/> 至 <input name="Bid_EnterEndTime" type="text" id="Bid_EnterEndTime" size="20" maxlength="50" readonly="readonly" value="<%=Today.ToString("yyyy-MM-dd HH:mm:ss") %>" onclick="    WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });"/></td>

                            </tr>--%>
                            <tr>
                                <td class="cell_title">报价时间</td>
                                <td class="cell_content">
                                    <input name="Bid_BidStartTime" type="text" id="Bid_BidStartTime" size="20" maxlength="50" readonly="readonly" value="<%=Today.ToString("yyyy-MM-dd HH:mm:ss") %>" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });"/> 至 <input name="Bid_BidEndTime" type="text" id="Bid_BidEndTime" size="20" maxlength="50" readonly="readonly" value="<%=Today.ToString("yyyy-MM-dd HH:mm:ss") %>" onclick="    WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' });"/></td>

                            </tr>
                            <tr>
                                <td class="cell_title">交货时间</td>
                                <td class="cell_content">
                                    <input name="Bid_DeliveryTime" type="text" id="Bid_DeliveryTime" size="20" maxlength="50" readonly="readonly" value="<%=Today.ToString("yyyy-MM-dd") %>"/></td>
                                <script>$(function () { $("#Bid_DeliveryTime").datepicker({ inline: true }); })</script>

                            </tr>

                            <tr>
                                <td class="cell_title">报价轮次</td>
                                <td class="cell_content">
                                    <input name="Bid_Number" type="text" id="Bid_Number" size="50" maxlength="100" value="1" /></td>
                            </tr>

                            <tr>
                                <td class="cell_title">保证金</td>
                                <td class="cell_content">
                                    <input name="Bid_Bond" type="text" id="Bid_Bond" size="50" maxlength="100" value="" /></td>
                            </tr>

                            <tr>
                                <td class="cell_title">产品清单</td>
                                <td class="cell_content">
                                    <input name="Bid_ProductType" type="radio" id="Bid_ProductType1" value="1" checked="checked" />展示
                                    <input type="radio" name="Bid_ProductType" id="Bid_ProductType2" value="0" />不展示</td>
                            </tr>
                            <tr>
                                <td class="cell_title" valign="top">公告内容</td>
                                <td class="cell_content">
                                    <textarea cols="80" id="Bid_Content" name="Bid_Content" rows="16"></textarea>
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


                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="right">
                                    <input type="hidden" id="action" name="action" value="new" />
                                    <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                                    <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'bid_list.aspx';" /></td>
                            </tr>
                        </table>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
