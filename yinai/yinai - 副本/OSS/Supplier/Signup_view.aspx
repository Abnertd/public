<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private Feedback myApp;
    private ITools tools;

    private string Bid_Up_ContractMan, Bid_Up_ContractMobile, Bid_Up_ContractContent;
    private int signup_id;
    private DateTime Bid_Up_AddTime;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("9877a09e-5dda-4b1e-bf6f-042504449eeb");
        myApp = new Feedback();
        tools = ToolsFactory.CreateTools();

        signup_id = tools.CheckInt(Request.QueryString["signup_id"]);
        BidUpRequireQuickInfo entity = myApp.GetBidUpRequireByID(signup_id);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            signup_id = entity.Bid_Up_ID;
            //   Feedback_Type = entity.Feedback_Type;
            Bid_Up_ContractMan=entity.Bid_Up_ContractMan;
                Bid_Up_ContractMobile=entity.Bid_Up_ContractMobile;
                Bid_Up_ContractContent = entity.Bid_Up_ContractContent;
                Bid_Up_AddTime = entity.Bid_Up_AddTime;
        }

        
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">立即发布会员留言</td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="supplier_do.aspx">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">




                          

                            <tr>
                                <td class="cell_title">联系人</td>
                                <td class="cell_content"><%=Bid_Up_ContractMan %></td>
                            </tr>
                           
                            <tr>
                                <td class="cell_title">联系电话</td>
                                <td class="cell_content"><%=Bid_Up_ContractMobile %></td>
                            </tr>
                          
                            <tr>
                                <td class="cell_title">留言备注</td>
                                <td class="cell_content"><%=Bid_Up_ContractContent%></td>
                            </tr>

                            <tr>
                                <td class="cell_title">留言时间</td>
                                <td class="cell_content"><%=Bid_Up_AddTime%></td>
                            </tr>
                           
                        </table>

                     <%--   <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="right">
                                    <input type="hidden" id="action" name="action" value="feedbackreply" />
                                    <input type="hidden" id="feedback_id" name="feedback_id" value="<% =Feedback_ID%>" />
                                    <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                                    <%if (Feedback_Type!=1)
                                      { %>
                                            <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'feedback_list.aspx?listtype=idea';" />
                                     <% }
                                      else
                                      {
                                          
                                      %>
                                  

                                     <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location = 'feedback_list.aspx?listtype=message';" />
                                    <% } %>
                                </td>


                            </tr>
                        </table>--%>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
