<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private Promotion myApp;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("9569bb39-3d52-49b3-8a70-139778cbecdd");
        myApp = new Promotion();

        Session["selected_productid"] = "";
        Session["selected_policyid"] = "";
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
      <td class="content_title">添加促销专题</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="/promotion/Promotion_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
			      <td class="cell_title">
                      促销标题</td>
			      <td class="cell_content"><input name="Promotion_Title" type="text" id="Promotion_Title" size="40" maxlength="50"></td>
			    </tr>
			    <tr>
          <td class="cell_title">上传图片</td>
          <td class="cell_content"><iframe id="iframe1" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=promotion&formname=formadd&frmelement=Promotion_TopHtml&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe></td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">页面头部内容</td>
          <td class="cell_content">
            <textarea cols="80" id="Promotion_TopHtml" name="Promotion_TopHtml" rows="20"></textarea>
            <script type="text/javascript">
                CKEDITOR.replace('Promotion_TopHtml');
            </script>
          </td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">选择类型</td>
          <td class="cell_content">
          <input type="radio" value="0" name="Promotion_Type" checked onclick="$('#product_opt').show();$('#promotion_opt').hide();$('#limitgroup_opt').hide();$('#wholesalegroup_opt').hide();"/> 指定产品 
          <input type="radio" value="1" name="Promotion_Type" onclick="$('#product_opt').hide();$('#promotion_opt').hide();$('#limitgroup_opt').hide();$('#wholesalegroup_opt').show();"/> 批发产品 
          <input type="radio" value="2" name="Promotion_Type" onclick="$('#product_opt').hide();$('#promotion_opt').hide();$('#limitgroup_opt').show();$('#wholesalegroup_opt').hide();"/> 限时产品 
          <input type="radio" value="3" name="Promotion_Type" onclick="$('#product_opt').hide();$('#promotion_opt').show();$('#limitgroup_opt').hide();$('#wholesalegroup_opt').hide();"/> 指定优惠政策产品 
          </td>
        </tr> 
			<tr id="product_opt">
          <td class="cell_title" valign="top">商品选择</td>
          <td class="cell_content"><a href="" id="btn_product"><input type="button" value="选择" class="bt_orange"/></a><input type="hidden" name="favor_productid" id="favor_productid" />
          <div class="div_picker" id="product_picker"><span class="pickertip">全部产品</span></div>
          </td>
        </tr>  
        <tr id="limitgroup_opt" style="display:none;">
          <td class="cell_title" valign="top">限时分组选择</td>
          <td class="cell_content"><%=myApp.ShowLimitGroup()%>
          </td>
        </tr> 
        <tr id="wholesalegroup_opt" style="display:none;">
          <td class="cell_title" valign="top">批发分组选择</td>
          <td class="cell_content"><%=myApp.ShowWholeSaleGroup()%>
          </td>
        </tr>  
        <tr id="promotion_opt" style="display:none;">
          <td class="cell_title" valign="top">优惠政策选择</td>
          <td class="cell_content"><%=myApp.ShowPromotionPolicy()%>
          </td>
        </tr>  
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="new" />
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
