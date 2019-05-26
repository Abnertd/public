<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private ProductTag myApp;
    private Promotion mypromotion;
    private ITools tools;

    private string Product_Tag_Name, Product_Tag_Site;
    private int Product_Tag_ID, Product_Tag_IsActive, Product_Tag_IsSupplier;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("2fea26b6-bbb2-44d8-9b46-0b1aed1cc47f");
        myApp = new ProductTag();
        tools = ToolsFactory.CreateTools();
        mypromotion = new Promotion();

        Product_Tag_ID = tools.CheckInt(Request.QueryString["product_tag_id"]);
        
        ProductTagInfo entity = myApp.GetProductTagByID(Product_Tag_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Product_Tag_ID = entity.Product_Tag_ID;
            Product_Tag_Name = entity.Product_Tag_Name;
            Product_Tag_IsSupplier=entity.Product_Tag_IsSupplier;
            Product_Tag_IsActive = entity.Product_Tag_IsActive;
            Product_Tag_Site = entity.Product_Tag_Site;
        }
        Session["selected_productid"] = myApp.GetTagProductID(Product_Tag_ID);
        
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/common.js" type="text/javascript"></script>
<script src="/Scripts/jquery.zxxbox.3.0.js" type="text/javascript"></script>
<script language="javascript">
btn_scroll_move();
</script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">添加商品标签</td>
    </tr>
    <tr>
      <td class="content_content">
       <table width="100%" border="0" cellpadding="0" cellspacing="0">
      <tr><td valign="top" height="31" class="opt_foot">
      <table border="0" cellpadding="0" cellspacing="0">
      <tr>
      <td class="opt_gap">&nbsp;</td>
      <td class="opt_cur" id="frm_opt_1">
      <%=Public.Page_ScriptOption("choose_opt(1,2);", "基本信息")%></td>
      <td class="opt_gap">&nbsp;</td>
      <%if (Product_Tag_IsSupplier == 0)
        { %>
      <td class="opt_uncur" id="frm_opt_2">
      <%=Public.Page_ScriptOption("choose_opt(2,2);", "应用产品")%></td>
      <%} %>
      </tr>
      </table>
      </td></tr>
      <tr><td class="opt_content">
      <form id="formadd" name="formadd" method="post" action="/Product/tag_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_1">
        <tr>
          <td class="cell_title">标签名称</td>
          <td class="cell_content"><input name="Product_Tag_Name" type="text" id="Product_Tag_Name" size="50" maxlength="50" value="<% =Product_Tag_Name%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">启用该标签</td>
          <td class="cell_content"><input name="Product_Tag_IsActive" type="radio" id="radio" value="1" <% =Public.CheckedRadio(Product_Tag_IsActive.ToString(), "1")%> />是<input type="radio" name="Product_Tag_IsActive" id="radio2" value="0" <% =Public.CheckedRadio(Product_Tag_IsActive.ToString(), "0")%> />否 </td>
        </tr>
      </table>
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_2" style="display:none;">
       <tr>
          <td class="cell_title">应用产品</td>
          <td class="cell_content"><a href="" id="btn_opt"><input type="button" value="选择产品" class="bt_orange"/></a><span class="tip">&nbsp;&nbsp;点击选择使用该标签的产品</span><input type="hidden" name="favor_productid" id="favor_productid" />
          <div class="div_picker" id="product_picker"><%=mypromotion.ShowProduct(tools.NullStr(Session["selected_productid"]))%></div></td>
        </tr>
        </table>
        <div class="foot_gapdiv"></div>
      <div class="float_option_div" id="float_option_div">
            <input type="hidden" id="Product_Tag_ID" name="Product_Tag_ID" value="<% =Product_Tag_ID%>" />
            <input type="hidden" id="action" name="action" value="renew" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='Tag.aspx';"/>
    </div>
        
        </form>
        </td>
    </tr>
  </table>
        </td>
    </tr>
  </table>
</div>
<script type="text/javascript">

$("#btn_opt").click(function(){
    $("#btn_opt").attr("href","/promotion/Product_check.aspx?tag=tag&timer="+Math.random());
});

$("#btn_opt").zxxbox({height:460, width:600,title:'',bar:false,btnclose: false});
</script>
<script src="/Scripts/promotion.js" type="text/javascript"></script>
</body>
</html>
