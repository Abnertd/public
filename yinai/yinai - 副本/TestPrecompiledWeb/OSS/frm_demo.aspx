<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private Product myApp;
    private SysMenu sysmenu;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("a8dcfdfb-2227-40b3-a598-9643fd4c7e18");
        myApp = new Product();
        sysmenu = new SysMenu();
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/common.js" type="text/javascript"></script>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<link rel="stylesheet" type="text/css" href="/scripts/treeview/dhtmlxtree.css" />
<script  src="/scripts/treeview/dhtmlxcommon.js" type="text/javascript"></script>
<script  src="/scripts/treeview/dhtmlxtree.js" type="text/javascript"></script>
<script type="text/javascript" src="/public/fckeditor/fckeditor.js"></script>
<script src="/scripts/product.js" type="text/javascript"></script>
<!--颜色选择器-->
<link rel="stylesheet" href="/public/colorpicker/css/colorpicker.css" type="text/css" />
<script type="text/javascript" src="/public/colorpicker/js/colorpicker.js"></script>
<!--颜色选择器-->
<script type="text/javascript">
    function openUpload(openObj)
    {
        $("#td_upload").show();
        $("#iframe_upload")[0].src = '<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=product&formname=formadd&frmelement=' + openObj + '&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>';
    }
    
    function delImage(openObj)
    {
         $("#img_"+ openObj)[0].src="/images/detail_no_pic.gif";
         $("#"+ openObj)[0].value="/images/detail_no_pic.gif";
    }
    
    //扩展属性上传图片
    function uploadExample(objstr)
    {
        window.open('uploadwindow.aspx?elementName='+ objstr, '上传图片', 'height=100, width=400, top=0,left=0, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no');
    }
    
    $(document).ready(function(){
         $('#colorpicker_note').ColorPicker({
	        onSubmit: function(hsb, hex, rgb, el) {
	            $(el).css('backgroundColor', '#' + hex);
		        $(el).val("#" + hex);
		        $(el).ColorPickerHide();
	        },
	        onBeforeShow: function () {
		        $(this).ColorPickerSetColor(this.value);
	        }
        })
        .bind('keyup', function(){
	        $(this).ColorPickerSetColor(this.value);
        });
    });
</script>

<style type="text/css">
    .extendAttribute input { width:300px; vertical-align:middle;}
    .extendAttribute img { vertical-align:middle; width:26px; height:26px;}
</style>
<script>
change_inputcss();
btn_scroll_move();
</script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title"><%=sysmenu.Page_Menu_Title(199)%></td>
    </tr>
    <tr>
      <td class="content_content">
      <table width="100%" border="0" cellpadding="0" cellspacing="0">
      <tr><td valign="top" height="31" class="opt_foot">
      <table border="0" cellpadding="0" cellspacing="0">
      <tr>
      <td class="opt_gap">&nbsp;</td>
      <td class="opt_cur" id="frm_opt_1">
      <%=Public.Page_ScriptOption("choose_opt(1,4);", "基本信息")%></td>
      <td class="opt_gap">&nbsp;</td>
      <td class="opt_uncur" id="frm_opt_2">
      <%=Public.Page_ScriptOption("choose_opt(2,4);", "图片信息")%></td>
      <td class="opt_gap">&nbsp;</td>
      <td class="opt_uncur" id="frm_opt_3">
      <%=Public.Page_ScriptOption("choose_opt(3,4);", "SEO信息")%></td>
      <td class="opt_gap">&nbsp;</td>
      <td class="opt_uncur" id="frm_opt_4">
      <%=Public.Page_ScriptOption("choose_opt(4,4);", "介绍信息")%></td>
      </tr>
      </table>
      </td></tr>
    <tr>
      <td class="opt_content">
      <form id="formadd" name="formadd" method="post" action="/product/product_add_2.aspx" onsubmit="javascript:MM_findObj('Product_CateID').value = tree.getAllChecked();return checkform_step1();">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_1">
      <tr>
          <td class="cell_title" valign="top">主分类</td>
          <td class="item_single">
            
            <span id="main_cate"><%=myApp.Product_Category_Select(0, "main_cate")%></span>
            <span id="div_Product_Cate"></span>
          </td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">附加分类</td>
          <td class="cell_content">
            <table style="width:100%; background:#f5f5f5; border:1px solid Silver;">
                <tr>
	                <td valign="top" id="treeboxbox_tree">
		                
	                </td>
                </tr>
            </table>
            <script type="text/javascript">
                tree=new dhtmlXTreeObject("treeboxbox_tree","100%","100%",0);
                tree.setSkin('dhx_skyblue');
                tree.setImagePath("/scripts/treeview/imgs/csh_dhx_skyblue/");
                tree.enableCheckBoxes(1);
                tree.enableThreeStateCheckboxes(true);
                tree.loadXML("/product/treedata.aspx");
            </script>
            <span id="div_Product_CateID"></span>
          </td>
        </tr>
        <tr>
          <td class="cell_title">商品类型</td>
          <td class="item_single">
          <select name="Product_TypeID" id="Product_TypeID">
            <option value="0">选择商品类型</option>
            <% =myApp.ProductTypeOption(0)%>
          </select>
          <span id="div_Product_TypeID"></span></td>
        </tr>
        <tr>
          <td class="cell_title">商品编码</td>
          <td class="cell_content"><input name="Product_Code" type="text" id="Product_Code" size="50" maxlength="50" value="" onblur="check_product_code('Product_Code', 0);"/><span id="div_Product_Code"></span></td>
        </tr>
        <tr>
          <td class="cell_title">商品名称</td>
          <td class="item_single"><input name="Product_Name" type="text" id="Product_Name" size="50" maxlength="100" value="" onblur="check_product_name('Product_Name');" /><span id="div_Product_Name"></span></td>
        </tr>
        <tr>
          <td class="cell_title">通用名称</td>
          <td class="cell_content"><input name="Product_SubName" type="text" id="Product_SubName" size="50" maxlength="100" value=""/></td>
        </tr>
        <tr>
          <td class="cell_title">规格</td>
          <td class="item_single"><input name="Product_Spec" type="text" id="Product_Spec" size="50" maxlength="100"  /></td>
        </tr>
        <tr>
          <td class="cell_title">生产企业</td>
          <td class="cell_content"><input name="Product_Maker" type="text" id="Product_Maker" size="50" maxlength="200"  /></td>
        </tr>
        <tr>
          <td class="cell_title">单位</td>
          <td class="item_single"><input name="Product_Unit" type="text" id="Product_Unit" maxlength="10" /></td>
        </tr>
        <tr>
          <td class="cell_title">市场价格</td>
          <td class="cell_content"><input name="Product_MKTPrice" type="text" id="Product_MKTPrice" maxlength="10" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" /></td>
        </tr>
        <tr>
          <td class="cell_title">团购价格</td>
          <td class="item_single"><input name="Product_GroupPrice" type="text" id="Product_GroupPrice" maxlength="10" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" /></td>
        </tr>
        <tr>
          <td class="cell_title">本站价格</td>
          <td class="cell_content"><input name="Product_Price" type="text" id="Product_Price" maxlength="10" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" /> <a href="javascript:void(0);" onclick="$('#member_price').show();$('#price_info').load('/product/product_do.aspx?action=grade_price&product_id=0&product_price='+$('#Product_Price').attr('value')+'&fresh=' + Math.random()+'');">设置会员价</a></td>
        </tr>
        <tr id="member_price" style="display:none;"><td class="cell_title">会员价格</td>
        <td id="price_info">
        
        </td></tr>
        <tr>
          <td class="cell_title">价格单位</td>
          <td class="item_single"><input name="Product_PriceUnit" type="text" id="Product_PriceUnit" maxlength="10" /></td>
        </tr>
        <tr>
          <td class="cell_title">团购最少量</td>
          <td class="cell_content"><input name="Product_GroupNum" type="text" id="Product_GroupNum" maxlength="10" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" /></td>
        </tr>
        <tr>
          <td class="cell_title">商品备注</td>
          <td class="item_single"><input name="Product_Note" type="text" id="Product_Note" size="50" maxlength="100" /><input type="text" maxlength="7" size="6" name="Product_NoteColor" id="colorpicker_note"/></td>
        </tr>
        <tr>
          <td class="cell_title">商品重量</td>
          <td class="cell_content"><input name="Product_Weight" type="text" id="Product_Weight" maxlength="10" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" />g</td>
        </tr>
        <tr>
          <td class="cell_title">商品上架</td>
          <td class="item_single"><input name="Product_IsInsale" type="radio" id="Product_IsInsale1" value="1" checked="checked"/>是<input type="radio" name="Product_IsInsale" id="Product_IsInsale2" value="0"/>否</td>
        </tr>
        <tr>
          <td class="cell_title">是否团购</td>
          <td class="cell_content"><input name="Product_IsGroupBuy" type="radio" id="Product_IsGroupBuy1" value="1"/>是<input type="radio" name="Product_IsGroupBuy" id="Product_IsGroupBuy2" value="0" checked="checked"/>否</td>
        </tr>
        <tr>
          <td class="cell_title">是否积分购买</td>
          <td class="item_single"><input name="Product_IsCoinBuy" type="radio" id="Product_IsCoinBuy1" value="1"/>是<input type="radio" name="Product_IsCoinBuy" id="Product_IsCoinBuy2" value="0" checked="checked"/>否</td>
        </tr>
        <tr>
          <td class="cell_title">是否参与优惠</td>
          <td class="cell_content"><input name="Product_IsFavor" type="radio" id="Product_IsFavor1" value="1" checked="checked"/>是<input type="radio" name="Product_IsFavor" id="Product_IsFavor2" value="0"/>否</td>
        </tr>
        <tr>
          <td class="cell_title">是否赠品</td>
          <td class="item_single"><input name="Product_IsGift" type="radio" id="Product_IsGift1" value="1" />是<input type="radio" name="Product_IsGift" id="Product_IsGift2" value="0" checked="checked"/>否</td>
        </tr>
        <%--<tr>
          <td class="cell_title">审核状态</td>
          <td class="cell_content"><input name="Product_IsAudit" type="radio" id="Product_IsAudit1" value="1" />是<input type="radio" name="Product_IsAudit" id="Product_IsAudit2" value="0" checked="checked"/>否</td>
        </tr>--%>
        <tr>
          <td class="cell_title">是否赠送指定积分</td>
          <td class="cell_content"><input name="Product_IsGiftCoin" type="radio" id="Product_IsGiftCoin1" value="1" />是<input type="radio" name="Product_IsGiftCoin" id="Product_IsGiftCoin2" value="0" checked="checked"/>否</td>
        </tr>
        <tr>
          <td class="cell_title">指定积分比率</td>
          <td class="item_single"><input name="Product_Gift_Coin" type="text" id="Product_Gift_Coin" maxlength="10" /> </td>
        </tr>
        <tr>
          <td class="cell_title">商品预警数量</td>
          <td class="cell_content"><input name="Product_AlertAmount" type="text" id="Product_AlertAmount1" maxlength="10" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" /></td>
        </tr>
        <tr>
          <td class="cell_title">零库存商品</td>
          <td class="item_single"><input name="Product_IsNoStock" type="radio" id="Product_IsNoStock1" value="1"/>是<input type="radio" name="Product_IsNoStock" id="Product_IsNoStock0" value="0" checked="checked"/>否</td>
        </tr>
        <tr>
          <td class="cell_title">排序</td>
          <td class="cell_content"><input name="Product_Sort" type="text" id="Product_Sort" maxlength="10" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="9999" /></td>
        </tr>
        <tr>
          <td class="cell_title">限购数量</td>
          <td class="item_single"><input name="Product_QuotaAmount" type="text" id="Product_QuotaAmount" maxlength="10" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="0"/></td>
        </tr>
      </table>
      <table width="100%" border="0" style="display:none;" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_2">
      
        <tr>
          <td class="cell_title">图片</td>
          <td class="cell_content">
			<table width="630" border="0" cellspacing="0" cellpadding="5">
			  <tr>
				<td width="110" height="110" align="center"><img id="img_product_img" src="/images/detail_no_pic.gif" width="100" height="100" onload="javascript:AutosizeImage(this,100,100);"/></td>
				<td width="110" align="center"><img id="img_product_img_ext_1" src="/images/detail_no_pic.gif" width="100" height="100" onload="javascript:AutosizeImage(this,100,100);"/></td>
				<td width="110" align="center"><img id="img_product_img_ext_2" src="/images/detail_no_pic.gif" width="100" height="100" onload="javascript:AutosizeImage(this,100,100);"/></td>
				<td width="110" align="center"><img id="img_product_img_ext_3" src="/images/detail_no_pic.gif" width="100" height="100" onload="javascript:AutosizeImage(this,100,100);"/></td>
				<td width="110" align="center"><img id="img_product_img_ext_4" src="/images/detail_no_pic.gif" width="100" height="100" onload="javascript:AutosizeImage(this,100,100);"/></td>
			  </tr>
			  <tr>
				<td align="center"><input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript:openUpload('product_img');" /> <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript:delImage('product_img');"><input type="hidden" name="product_img" id="product_img" value="/images/detail_no_pic.gif" /></td>
				
				<td align="center"><input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript:openUpload('product_img_ext_1');" /> <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript:delImage('product_img_ext_1');"><input type="hidden" name="product_img_ext_1" id="product_img_ext_1" value="/images/detail_no_pic.gif" /></td>
				
				<td align="center"><input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript:openUpload('product_img_ext_2');" /> <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript:delImage('product_img_ext_2');"><input type="hidden" name="product_img_ext_2" id="product_img_ext_2" value="/images/detail_no_pic.gif" /></td>

				<td align="center"><input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript:openUpload('product_img_ext_3');" /> <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript:delImage('product_img_ext_3');"><input type="hidden" name="product_img_ext_3" id="product_img_ext_3" value="/images/detail_no_pic.gif" /></td>
				
				<td align="center"><input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript:openUpload('product_img_ext_4');" /> <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript:delImage('product_img_ext_4');"><input type="hidden" name="product_img_ext_4" id="product_img_ext_4" value="/images/detail_no_pic.gif" /></td>
			  </tr>
			</table>
          </td>
        </tr>
        <tr id="td_upload" style="display:none">
          <td class="cell_title">上传图片</td>
          <td class="cell_content"><iframe id="iframe_upload" src="" width="100%" height="100" frameborder="0" scrolling="no"></iframe></td>
        </tr>
      </table>
      <table width="100%" border="0" style="display:none;" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_3">
      <tr>
          <td class="cell_title">上传图片</td>
          <td class="cell_content"><iframe id="iframe1" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=productintro&formname=formadd&frmelement=Product_Intro&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe></td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">商品介绍</td>
          <td class="cell_content">
                <script type="text/javascript">
					var oFCKeditor = new FCKeditor("Product_Intro");
					oFCKeditor.BasePath = "/public/fckeditor/";
					oFCKeditor.Width = 660 ;
					oFCKeditor.Height = 500 ;
					oFCKeditor.ToolbarSet = "MyDefault" ;
					oFCKeditor.Value = "";
					oFCKeditor.Config[ "AutoDetectLanguage" ] = false ;
					oFCKeditor.Config[ "DefaultLanguage" ] = "zh-cn" ;
					oFCKeditor.Create();
				</script>
          </td>
        </tr>
        
      </table>
      <table width="100%" style="display:none;" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_4">
      <tr>
          <td class="cell_title">TITLE<br />
            (页面标题)</td>
          <td class="item_single"><input name="Product_SEO_Title" type="text" id="Product_SEO_Title" size="50" maxlength="200" /></td>
        </tr>
        <tr>
          <td class="cell_title">META_KEYWORDS<br />
            (页面关键词)</td>
          <td class="cell_content"><input name="Product_SEO_Keyword" type="text" id="Product_SEO_Keyword" size="50" maxlength="200" /></td>
        </tr>
        <tr>
          <td class="cell_title">META_DESCRIPTION<br />
            (页面描述)</td>
          <td class="item_single"><textarea name="Product_SEO_Description" cols="50" rows="5" id="Product_SEO_Description"></textarea></td>
        </tr>
      </table>
      <div class="foot_gapdiv"></div>
      <div class="float_option_div" id="float_option_div"><input name="Product_CateID" type="hidden" id="Product_CateID" value="" />
    <input name="save" type="submit" class="bt_orange" id="save" value="下一步" />
    <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='product.aspx';" />
    </div>
    </form>
      </td></tr>
  </table>
   
    
    </td></tr></table>
</div>
</body>
</html>
