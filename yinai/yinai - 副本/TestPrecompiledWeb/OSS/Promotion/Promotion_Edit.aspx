<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private Promotion myApp;
    private ITools tools;
    int promotion_id,promotion_type;
    string promotion_title, promotion_tophtml,Product_Str;
    PromotionInfo entity;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("c0330805-14e6-493e-8519-7ca89dddd157");
        myApp = new Promotion();
        tools=ToolsFactory.CreateTools();
        promotion_id = tools.CheckInt(Request["promotion_id"]);
        entity = myApp.GetPromotionByID(promotion_id);
        if (entity != null)
        {
            promotion_title = entity.Promotion_Title;
            promotion_tophtml = entity.Promotion_TopHtml;
            promotion_type = entity.Promotion_Type;
            Product_Str = myApp.Get_Promotion_Product_String(entity.PromotionProducts);
        }
        else
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/jquery.zxxbox.3.0.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script type="text/javascript" src="/Public/ckeditor/ckeditor.js"></script>
<script src="/Scripts/common.js" type="text/javascript"></script>
<script type="text/javascript">
change_inputcss();
</script>

</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">修改促销专题</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/promotion/Promotion_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
			      <td class="cell_title">
                      促销标题</td>
			      <td class="cell_content"><input name="Promotion_Title" type="text" id="Promotion_Title" value="<%=promotion_title %>" size="40" maxlength="50"></td>
			    </tr>
			    <tr>
          <td class="cell_title">上传图片</td>
          <td class="cell_content"><iframe id="iframe1" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=promotion&formname=formadd&frmelement=Promotion_TopHtml&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe></td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">页面头部内容</td>
          <td class="cell_content">
            <textarea cols="80" id="Promotion_TopHtml" name="Promotion_TopHtml" rows="20"><% =promotion_tophtml%></textarea>
            <script type="text/javascript">
                CKEDITOR.replace('Promotion_TopHtml');
            </script>
          </td>
        </tr>
			<tr>
          <td class="cell_title" valign="top">商品选择</td>
          <td class="cell_content">
          <a href="" id="btn_product"><input type="button" value="选择" class="bt_orange"/></a><input type="hidden" name="favor_productid" id="favor_productid" />
          <div class="div_picker" id="product_picker">
          <%
           if (Product_Str == "")
           { 
			   Response.Write("<span class=\"pickertip\">全部产品</span>");
			}
           else
           {
               Response.Write(myApp.ShowProduct(Product_Str));
           }
           %></div>
          </td>
        </tr>    
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="renew" />
            <input type="hidden" id="promotion_id" name="promotion_id" value="<%=promotion_id %>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='promotion_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
<script type="text/javascript">
$("#btn_product").click(function(){
    $("#btn_product").attr("href","product_check.aspx?timer=" + Math.random());						   
});
$("#btn_product").zxxbox({height:450, width:600,title:'',bar:false,btnclose: false});

</script>
<script src="/Scripts/promotion.js" type="text/javascript"></script>
</body>
</html>
