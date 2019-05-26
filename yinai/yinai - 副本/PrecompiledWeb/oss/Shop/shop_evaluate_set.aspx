<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<script runat="server">
    private ProductReview reviewconfig;
    private ITools tools;

    private int Product_Review_Config_ID, Product_Review_Config_ProductCount, Product_Review_Config_ListCount, Product_Review_Config_Power, Product_Review_giftcoin, Product_Review_Recommendcoin;
    private int Product_Review_Config_VerifyCode_IsOpen, Product_Review_Config_ManagerReply_Show, Product_Review_Config_IsActive;
    private string Product_Review_Config_NoRecordTip, Product_Review_Config_Show_SuccessTip, Product_Review_Config_Site;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("b948d76d-944c-4a97-82dc-a3917ce6dcd9");

        reviewconfig = new ProductReview(); 
        tools = ToolsFactory.CreateTools();

        ProductReviewConfigInfo entity = reviewconfig.GetProductReviewConfig();
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        } else {
            Product_Review_Config_ID = entity.Product_Review_Config_ID;
            Product_Review_Config_ProductCount = entity.Product_Review_Config_ProductCount;
            Product_Review_Config_ListCount = entity.Product_Review_Config_ListCount;
            Product_Review_Config_Power = entity.Product_Review_Config_Power;
            Product_Review_giftcoin = entity.Product_Review_giftcoin;
            Product_Review_Recommendcoin = entity.Product_Review_Recommendcoin;
            Product_Review_Config_NoRecordTip = entity.Product_Review_Config_NoRecordTip;
            Product_Review_Config_VerifyCode_IsOpen = entity.Product_Review_Config_VerifyCode_IsOpen;
            Product_Review_Config_ManagerReply_Show = entity.Product_Review_Config_ManagerReply_Show;
            Product_Review_Config_Show_SuccessTip = entity.Product_Review_Config_Show_SuccessTip;
            Product_Review_Config_IsActive = entity.Product_Review_Config_IsActive;
            Product_Review_Config_Site = entity.Product_Review_Config_Site;

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
      <td class="content_title">产品评论设置</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/Product/product_review_Do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">商品详细页显示</td>
          <td class="cell_content"><input name="Product_Review_Config_ProductCount" type="text" id="Product_Review_Config_ProductCount" value="<%=Product_Review_Config_ProductCount %>" size="50" maxlength="50" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" /></td>
        </tr>
        <tr>
          <td class="cell_title">评论/咨询页显示</td>
          <td class="cell_content"><input name="Product_Review_Config_ListCount" type="text" id="Product_Review_Config_ListCount" value="<%=Product_Review_Config_ListCount %>" size="50" maxlength="50" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" /></td>
        </tr>
       <%-- <tr>
          <td class="cell_title">是否开启评论功能</td>
          <td class="cell_content">
          <input name="Product_Review_Config_IsActive" type="radio" id="Product_Review_Config_IsActive1" value="1" <% =Public.CheckedRadio(Product_Review_Config_IsActive.ToString(), "1")%>/>是
          <input type="radio" name="Product_Review_Config_IsActive" value="0" <% =Public.CheckedRadio(Product_Review_Config_IsActive.ToString(), "0")%>/>否</td>
        </tr>
        <tr>
          <td class="cell_title">发表评论权限</td>
          <td class="cell_content"><input name="Product_Review_Config_Power" type="radio" id="Product_Review_Config_Power1" value="0" <% =Public.CheckedRadio(Product_Review_Config_Power.ToString(), "0")%>/>所有人均可发表评论
          <input type="radio" name="Product_Review_Config_Power" value="1" <% =Public.CheckedRadio(Product_Review_Config_Power.ToString(), "1")%>/>注册会员可发表评论
          <input type="radio" name="Product_Review_Config_Power" value="2" <% =Public.CheckedRadio(Product_Review_Config_Power.ToString(), "2")%>/>只有购买过此商品的会员  </td>
        </tr>--%>
        <tr>
          <td class="cell_title">发表时需输入验证码</td>
          <td class="cell_content"><input name="Product_Review_Config_VerifyCode_IsOpen" type="radio" id="Product_Review_Config_VerifyCode_IsOpen1" value="1" <% =Public.CheckedRadio(Product_Review_Config_VerifyCode_IsOpen.ToString(), "1")%>/>是 <input type="radio" name="Product_Review_Config_VerifyCode_IsOpen" value="0" <% =Public.CheckedRadio(Product_Review_Config_VerifyCode_IsOpen.ToString(), "0")%>/>否 </td>
        </tr>
        <tr>
          <td class="cell_title">显示条件</td>
          <td class="cell_content"><input name="Product_Review_Config_ManagerReply_Show" type="radio" id="Product_Review_Config_ManagerReply_Show" value="1" <% =Public.CheckedRadio(Product_Review_Config_ManagerReply_Show.ToString(), "1")%>/>通过审核 <input type="radio" name="Product_Review_Config_ManagerReply_Show" value="0" <% =Public.CheckedRadio(Product_Review_Config_ManagerReply_Show.ToString(), "0")%>/>立即显示 </td>
        </tr>
        <tr>
          <td class="cell_title">无评论时缺省文字</td>
          <td class="cell_content"><input name="Product_Review_Config_NoRecordTip" type="text" id="Product_Review_Config_NoRecordTip" size="50" value="<%=Product_Review_Config_NoRecordTip %>" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">发表成功提示文字</td>
          <td class="cell_content"><input name="Product_Review_Config_Show_SuccessTip" type="text" id="Product_Review_Config_Show_SuccessTip" size="50" value="<%=Product_Review_Config_Show_SuccessTip %>" maxlength="100" /></td>
        </tr>
        <tr>
          <td class="cell_title">发表评论获取积分</td>
          <td class="cell_content"><input name="Product_Review_giftcoin" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" id="Product_Review_giftcoin" size="20" value="<%=Product_Review_giftcoin %>" maxlength="50" /></td>
        </tr>
        <%--<tr>
          <td class="cell_title">评论推荐获取积分</td>
          <td class="cell_content"><input name="Product_Review_Recommendcoin" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" id="Product_Review_Recommendcoin" size="20" value="<%=Product_Review_Recommendcoin %>" maxlength="50" /></td>
        </tr>--%>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="Product_Review_Config_ID" name="Product_Review_Config_ID" value="<%=Product_Review_Config_ID %>" />
            <input type="hidden" id="action" name="action" value="update_config" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存设置" />
             <input name="button" type="button" class="bt_grey" id="button" value="更新系统缓存" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="window.open('<%=Application["site_url"] %>/tools/updateapplication.aspx?timer='+ Math.random());"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
