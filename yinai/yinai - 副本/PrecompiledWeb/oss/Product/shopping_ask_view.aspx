<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<script runat="server">
    private ShoppingAsk myApp;
    private ITools tools;
    private Member member = new Member();
    private Product product = new Product();

private string Ask_Content,Ask_Reply,member_name,product_name,Ask_Contact;
private int Ask_ID,Ask_Type,Ask_MemberID,Ask_ProductID,Ask_Pleasurenum,Ask_Displeasure,Ask_Isreply;
private DateTime Ask_Addtime;

protected void Page_Load(object sender, EventArgs e)
{
    Public.CheckLogin("b2a9d36e-9ba5-45b6-8481-9da1cd12ace0");
    myApp = new ShoppingAsk();
    tools = ToolsFactory.CreateTools();
    
    Ask_ID = tools.CheckInt(Request.QueryString["Ask_ID"]);
    ShoppingAskInfo entity = myApp.GetShoppingAskByID(Ask_ID);
    if (entity == null) {
        Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
        Response.End();
    } else {
        Ask_ID = entity.Ask_ID;
        Ask_Type = entity.Ask_Type;
        Ask_Content = entity.Ask_Content;
        Ask_Reply = entity.Ask_Reply;
        Ask_Addtime = entity.Ask_Addtime;
        Ask_MemberID = entity.Ask_MemberID;
        Ask_ProductID = entity.Ask_ProductID;
        Ask_Pleasurenum = entity.Ask_Pleasurenum;
        Ask_Displeasure = entity.Ask_Displeasure;
        Ask_Isreply = entity.Ask_Isreply;
        Ask_Contact = entity.Ask_Contact;

    }
    member_name = "";
    if (Ask_MemberID == 0)
    {
        member_name = "游客";
    }
    else
    {
        MemberInfo memberinfo = member.GetMemberByID(Ask_MemberID);
        if (memberinfo != null)
        {
            member_name = memberinfo.Member_NickName;
        }
        else
        {
            member_name = "未知";
        }
    }

    product_name = "";
        ProductInfo productinfo = product.GetProductByID(Ask_ProductID);
        if (productinfo != null)
        {
            product_name = productinfo.Product_Name;
        }
        else
        {
            product_name = "未知";
        }
}

</script>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">购物咨询回复</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/Product/shopping_ask_Do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">咨询人</td>
          <td class="cell_content"><%=member_name %></td>
        </tr>
        <tr>
          <td class="cell_title">联系方式</td>
          <td class="cell_content"><%=Ask_Contact%></td>
        </tr>
        <tr>
          <td class="cell_title">咨询产品</td>
          <td class="cell_content"><%=product_name %></td>
        </tr>
        <tr>
          <td class="cell_title">咨询时间</td>
          <td class="cell_content"><%=Ask_Addtime%></td>
        </tr>
        <tr>
          <td class="cell_title">咨询内容</td>
          <td class="cell_content"><%=Ask_Content%> </td>
        </tr>
        <tr>
          <td class="cell_title">咨询回复</td>
          <td class="cell_content"><%=Ask_Reply%></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">

             <input name="button" type="button" class="bt_grey" id="button" value="返回" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="history.go(-1);"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
