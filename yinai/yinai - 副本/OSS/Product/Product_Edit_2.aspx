<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private Product myApp;
    private ITools tools;
    private Promotion mypromotion;
    Addr addr = new Addr();

    private string Product_Code, Product_Name, U_Product_Parameters, Product_NameInitials, Product_SubName, Product_SubNameInitials, Product_PriceUnit, Product_Unit, Product_Note, Product_NoteColor, Product_Img, Product_Publisher, Product_Intro, Product_Site, Product_SEO_Title, Product_SEO_Keyword, Product_SEO_Description, Product_CateID, Product_Spec, Product_Maker, U_Product_DeliveryCycle, Product_Description, Product_Region_Name, Product_State_Name, Product_City_Name, Product_County_Name;
    private int Product_ID, Product_Cate, Product_BrandID, Product_GroupNum, Product_StockAmount, Product_SaleAmount, Product_Review_Count, Product_Review_ValidCount, Product_IsInsale, Product_IsGroupBuy, Product_IsCoinBuy, Product_IsFavor, Product_IsGift, Product_IsAudit, Product_CoinBuy_Coin, Product_AlertAmount, Product_IsNoStock, Product_TypeID, Product_Sort, Product_QuotaAmount, Product_IsGiftCoin, U_Product_MinBook, Product_PriceType, Product_SupplierID, Supplier_CompanyName;
    private DateTime Product_Addtime;
    private double Product_MKTPrice, Product_GroupPrice, Product_Price, Product_Weight, Product_Review_Average, Product_Gift_Coin;
    private bool grade_price = false;

    private string product_img, product_img_ext_1, product_img_ext_2, product_img_ext_3, product_img_ext_4;

    string U_Product_Innovation = "", U_Product_PackingList = "", U_Product_Service = "";
    int U_Product_SalesByProxy = 0, U_Product_Shipper = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d");
        myApp = new Product();
        mypromotion = new Promotion();

        tools = ToolsFactory.CreateTools();
        Product_ID = tools.CheckInt(Request.Form["Product_ID"]);
        Product_TypeID = tools.CheckInt(Request.Form["Product_TypeID"]);
        Product_Cate = tools.CheckInt(Request.Form["Product_Cate"]);
        Product_CateID = tools.CheckStr(Request.Form["Product_CateID"]);
        Product_Code = tools.CheckStr(Request.Form["Product_Code"]);
        Product_Name = tools.CheckStr(Request.Form["Product_Name"]);

        if (Product_Cate == 0)
        {
            Product_Cate = tools.CheckInt(Request.Form["Product_Cate_parent"]);
        }

        if (Product_CateID.IndexOf(Product_Cate.ToString()) < 0)
        {
            if (Product_CateID.Length == 0)
            {
                Product_CateID = Product_Cate.ToString();
            }
            else
            {
                Product_CateID = Product_CateID + "," + Product_Cate.ToString();
            }
        }
        if (Product_Cate == 0)
            Public.Msg("error", "错误信息", "请选择商品主分类！", false, "{back}");

        if (Product_TypeID == 0)
            Public.Msg("error", "错误信息", "请选择商品所属类型！", false, "{back}");

        if (Product_CateID.Length == 0)
            Public.Msg("error", "错误信息", "请选择商品所属类别！", false, "{back}");

        if (Product_Code.Length == 0)
            Public.Msg("error", "错误信息", "请填写商品编码！", false, "{back}");

        if (Product_Name.Length == 0)
            Public.Msg("error", "错误信息", "请填写商品名称！", false, "{back}");


        ProductInfo entity = myApp.GetProductByID(Product_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Product_ID = entity.Product_ID;
            //Product_Code = entity.Product_Code;
            //Product_CateID = entity.Product_CateID;
            Product_BrandID = entity.Product_BrandID;
            //Product_Name = entity.Product_Name;
            Product_SupplierID = entity.Product_SupplierID;

            Product_SubName = entity.Product_SubName;
            Product_PriceType = entity.Product_PriceType;
            Product_MKTPrice = entity.Product_MKTPrice;
            Product_GroupPrice = entity.Product_GroupPrice;
            Product_Price = entity.Product_Price;
            Product_PriceUnit = entity.Product_PriceUnit;
            Product_Unit = entity.Product_Unit;
            Product_GroupNum = entity.Product_GroupNum;
            Product_Note = entity.Product_Note;
            Product_NoteColor = entity.Product_NoteColor;
            Product_Weight = entity.Product_Weight;
            Product_Img = entity.Product_Img;
            Product_Publisher = entity.Product_Publisher;
            Product_StockAmount = entity.Product_StockAmount;
            Product_SaleAmount = entity.Product_SaleAmount;
            Product_Review_Count = entity.Product_Review_Count;
            Product_Review_ValidCount = entity.Product_Review_ValidCount;
            Product_Review_Average = entity.Product_Review_Average;
            Product_IsInsale = entity.Product_IsInsale;
            Product_IsGroupBuy = entity.Product_IsGroupBuy;
            Product_IsCoinBuy = entity.Product_IsCoinBuy;
            Product_IsFavor = entity.Product_IsFavor;
            Product_IsGift = entity.Product_IsGift;
            Product_IsAudit = entity.Product_IsAudit;
            Product_IsGiftCoin = entity.Product_IsGiftCoin;
            Product_Gift_Coin = entity.Product_Gift_Coin;
            Product_CoinBuy_Coin = entity.Product_CoinBuy_Coin;
            Product_Addtime = entity.Product_Addtime;
            Product_Intro = entity.Product_Intro;
            Product_AlertAmount = entity.Product_AlertAmount;
            Product_IsNoStock = entity.Product_IsNoStock;
            Product_Spec = entity.Product_Spec;
            Product_Maker = entity.Product_Maker;
            Product_Sort = entity.Product_Sort;
            Product_QuotaAmount = entity.Product_QuotaAmount;
            Product_Site = entity.Product_Site;
            Product_SEO_Title = entity.Product_SEO_Title;
            Product_SEO_Keyword = entity.Product_SEO_Keyword;
            Product_SEO_Description = entity.Product_SEO_Description;
            U_Product_Parameters = entity.U_Product_Parameters;

            U_Product_DeliveryCycle = entity.U_Product_DeliveryCycle;
            U_Product_MinBook = entity.U_Product_MinBook;
            Product_Description = entity.Product_Description;
            Product_State_Name = entity.Product_State_Name;
            Product_City_Name = entity.Product_City_Name;
            Product_County_Name = entity.Product_County_Name;



            string[] imgArr = myApp.GetProductImg(Product_ID);
            product_img = imgArr[0];
            product_img_ext_1 = imgArr[1];
            product_img_ext_2 = imgArr[2];
            product_img_ext_3 = imgArr[3];
            product_img_ext_4 = imgArr[4];

            IList<ProductPriceInfo> prices = myApp.GetProductPrices(Product_ID);
            if (prices != null)
            {
                grade_price = true;
            }

            if (entity.Product_GroupCode.Length > 0)
            {
                Session["selected_productid"] = myApp.GetGroupProductID(entity.Product_GroupCode);
            }
            else
            {
                Session["selected_productid"] = "0";
            }
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>修改商品</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="/public/fckeditor/fckeditor.js"></script>
    <script type="text/javascript" src="/Public/ckeditor/ckeditor.js"></script>
    <script src="/Scripts/product.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.zxxbox.3.0.js" type="text/javascript"></script>

    <!--颜色选择器-->
    <link rel="stylesheet" href="/public/colorpicker/css/colorpicker.css" type="text/css" />
    <script type="text/javascript" src="/public/colorpicker/js/colorpicker.js"></script>
    <!--颜色选择器-->
    <script type="text/javascript" >
        function RefillAddressNew(targetdiv, statename, cityname, countyname, statecode, citycode, countycode) {
            $.ajax({
                type: "get",
                global: false,
                async: false,
                dataType: "html",
                url: encodeURI("/public/ajax_address.aspx?action=newfill&targetdiv=" + targetdiv + "&statename=" + statename + "&cityname=" + cityname + "&countyname=" + countyname + "&statecode=" + statecode + "&citycode=" + citycode + "&countycode=" + countycode + "&timer=" + Math.random()),

                success: function (data) {
                    $("#" + targetdiv).html(data);
                    $("#" + statename).val(statecode);
                    $("#" + cityname).val(citycode);
                    $("#" + countyname).val(countycode);
                },
                error: function () {
                    alert("Error Script");
                }
            });
        }


        function ChangeUnitCase() {
            var a = document.getElementById("Product_Unit").value

            if (a == 0) {
                $("#Product_Unit_Name").html("");
                $("#Product_Unit_Name1").html("");
                $("#Product_Unit_Name2").html("");
                $("#Product_Unit_Name3").html("");
                $("#Product_Unit_Name4").html("");
                $("#Product_Unit_Name_weight").html("");
            }
            else {
                $("#Product_Unit_Name").html(a);
                $("#Product_Unit_Name1").html(a);
                $("#Product_Unit_Name2").html(a);
                $("#Product_Unit_Name3").html(a);
                $("#Product_Unit_Name4").html(a);
                $("#Product_Unit_Name_weight").html(a);
            }
        }


        function openUpload(openObj) {
            $("#td_upload").show();
            $("#iframe_upload")[0].src = '<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=product&formname=formadd&frmelement=' + openObj + '&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>';
        }

        function delImage(openObj) {
            $("#img_" + openObj)[0].src = "/images/detail_no_pic.gif";
            $("#" + openObj)[0].value = "/images/detail_no_pic.gif";
        }

        function VideoUpload(openObj) {
            $("#iframe_video").show();
            $("#iframe_video")[0].src = '<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=preview&formname=formadd&frmelement=product_video_' + openObj + '&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>';
        }

        function delVideo(openObj) {
            $("#img_product_video_" + openObj).attr("str", "/images/detail_no_pic.gif");
            $("#product_video_" + openObj).attr("str", "/images/detail_no_pic.gif");
        }

        //扩展属性上传图片
        function uploadExample(objstr) {
            window.open('uploadwindow.aspx?elementName=' + objstr, '上传图片', 'height=100, width=400, top=0,left=0, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no');
        }

        $(document).ready(function () {
            $('#colorpicker_note').ColorPicker({
                onSubmit: function (hsb, hex, rgb, el) {
                    $(el).css('backgroundColor', '#' + hex);
                    $(el).val("#" + hex);
                    $(el).ColorPickerHide();
                },
                onBeforeShow: function () {
                    $(this).ColorPickerSetColor(this.value);
                }
            })
           .bind('keyup', function () {
               $(this).ColorPickerSetColor(this.value);
           });
        });

        function SaveProduct() {
            if (confirm("是否审核并上架？")) {
                $("#Product_IsInsale").val(1);
                $("#Product_IsAudit").val(1);
            }
        }




    </script>
    <style type="text/css">
        .extendAttribute input {
            width: 300px;
            vertical-align: middle;
        }

        .extendAttribute img {
            vertical-align: middle;
            width: 26px;
            height: 26px;
        }

        .cell_title span {
            color: Red;
        }
    </style>
</head>
<body>

    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">修改商品</td>
            </tr>
            <tr>
                <td valign="top" height="31" class="opt_foot">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="opt_gap">&nbsp;
                            </td>
                            <td class="opt_cur" id="frm_opt_1">
                                <%=Public.Page_ScriptOption("choose_opt(1,6);", "基本信息")%>
                            </td>
                            <td class="opt_gap">&nbsp;
                            </td>
                            <td class="opt_uncur" id="frm_opt_2">
                                <%=Public.Page_ScriptOption("choose_opt(2,6);", "扩展属性")%>
                            </td>
                            <td class="opt_gap">&nbsp;
                            </td>
                            <td class="opt_uncur" id="frm_opt_3">
                                <%=Public.Page_ScriptOption("choose_opt(3,6);", "SEO信息")%>
                            </td>
                            <td class="opt_gap">&nbsp;
                            </td>
                            <td class="opt_uncur" id="frm_opt_4">
                                <%=Public.Page_ScriptOption("choose_opt(4,6);", "产品图片")%>
                            </td>
                            <td class="opt_gap">&nbsp;
                            </td>
                            <td class="opt_uncur" id="frm_opt_5">
                                <%=Public.Page_ScriptOption("choose_opt(5,6);", "产品介绍")%>
                            </td>
                            <td class="opt_gap">&nbsp;
                            </td>
                            <td class="opt_uncur" id="frm_opt_6">
                                <%=Public.Page_ScriptOption("choose_opt(6,6);", "聚合产品")%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="/product/product_do.aspx">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_1">
                          <%--  <tr>
                                <td class="cell_title">商品品牌</td>
                                <td class="cell_content">
                                    <select name="Product_BrandID" id="Product_BrandID">
                                        <option value="0">选择商品品牌</option>
                                        <% =myApp.ProductBrandOption(Product_TypeID, Product_BrandID)%>
                                    </select></td>
                            </tr>--%>
                            <tr>
                             <%--   <td class="cell_title">商品编码</td>--%>
                                <td class="cell_content" style="display:none">
                                    <input name="Product_Code" type="text" id="Product_Code" size="50" maxlength="50" readonly="readonly" value="<% =Product_Code%>" />
                                    <span class="t12_red">*</span></td>
                            </tr>
                            <tr>
                                <td class="cell_title">商品名称</td>
                                <td class="cell_content">
                                    <input name="Product_Name" type="text" id="Product_Name" size="50" maxlength="100" value="<% =Product_Name%>" /></td>
                            </tr>
                            <%--  <tr>
          <td class="cell_title">生产企业</td>
          <td class="cell_content"><input name="Product_Maker" type="text" id="Product_Maker" size="50" maxlength="200" value="<%=Product_Maker %>" /></td>
        </tr>--%> 
                            <tr>
                                <td class="cell_title">供应商</td>
                                <td class="cell_content">                                 
                                      <% =myApp.Product_Supplier_Select(Product_SupplierID, "Product_SupplierID")%> </td>
                            </tr>                           
                             <tr>
                                <td class="cell_title">商品产地</td>                             
                                    <td class="cell_content"  id="div_area">    
                                       <%=addr.SelectProudctAddress("div_area", "Product_State_Name", "Product_City_Name", "Product_County_Name", Product_State_Name, Product_City_Name, Product_County_Name)%> 
                                    </td>
                                 <td>
                                         <input type="hidden" id="Product_State_Name" name="Product_State_Name" value="<%=Product_State_Name%>" />
                                    <input type="hidden" id="Product_City_Name" name="Product_City_Name" value="<%=Product_City_Name%>" />
                                    <input type="hidden" id="Product_County_Name" name="Product_County_Name" value="<%=Product_County_Name%>" />
                                 </td>
                            </tr>
                               <%--<tr>
                                <td class="cell_title">商品产地</td>                             
                                  <td class="cell_content"  id="div_area">
                                <%=addr.SelectProudctAddress("div_area","Product_State_Name","","",Product_State_Name,"","") %>
                                 <input type="hidden" id="Product_State_Name" name="Product_State_Name"  value="<%=Product_State_Name%>"/></td>
                            </tr>--%>
                           
                            <tr>
                             <td class="cell_title">商品单位</td>
                                <td class="cell_content">                                
                                     <%--   <% =myApp.Product_Unit_Select(Product_ID, "Product_Unit")%>--%>
                                     <input name="Product_Unit" type="text" id="Product_Unit" style="width: 300px;" value="<%=Product_Unit %>" class="input01" onblur="ChangeUnitCase();"  onmousemove="ChangeUnitCase();" />
                      </td>  </tr>

                            <%-- <tr>
              <td class="cell_title">计价方式</td>
              <td class="cell_content">
                  <input type="radio" id="Product_PriceType1" name="Product_PriceType" value="1" <%=Public.CheckedRadio(Product_PriceType.ToString(),"1") %> />定价制
              <input type="radio" id="Product_PriceType2" name="Product_PriceType" value="2" <%=Public.CheckedRadio(Product_PriceType.ToString(),"2") %> />计价制
              </td>
          </tr>--%>
                            <tr>
                                <td class="cell_title">商品交货周期</td>
                                <td class="cell_content">
                                    <input name="U_Product_DeliveryCycle" type="text" id="U_Product_DeliveryCycle" style="width: 300px;" class="input01" value="<%=U_Product_DeliveryCycle %>"" />(单位:天)</td>  
                            </tr>

                            <tr>
                                <td class="cell_title">商品价格</td>
                                <td class="cell_content">
                                    <input name="Product_Price" type="text" id="Product_Price" maxlength="10" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="<% =Product_Price%>" />元(单位:<span id="Product_Unit_Name"><%=Product_Unit %></span>) <%--<a href="javascript:void(0);" onclick="$('#member_price').show();$('#price_info').load('/product/product_do.aspx?action=grade_price&product_id=<%=Product_ID %>&product_price='+$('#Product_Price').attr('value')+'&fresh=' + Math.random()+'');">设置会员价</a>--%></td>
                            </tr>
                             <tr>
                                <td class="cell_title">商品重量</td>
                                <td class="cell_content">
                                    <input name="Product_Weight" type="text" id="Product_Weight" maxlength="10" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="<% =Product_Weight%>" />(单位:<span id="Product_Unit_Name_weight"><%=Product_Unit %></span>)</td>
                            </tr>
                            <tr id="member_price"
                                <%if (grade_price == false)
                                  {
                                      Response.Write("style=\"display:none;\"");
                                  } %>>
                                <td class="cell_title">会员价格</td>
                                <td id="price_info">
                                    <%if (grade_price)
                                      {
                                          Response.Write(myApp.Product_Grade_Price_Form(Product_ID, Product_Price, 0));
                                      } %>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title">规格型号</td>                         
                                <td class="cell_content">                                 
                                       <input name="Product_Spec" type="text" id="Product_Spec" size="50" maxlength="100" value="<% =Product_Spec%>" style="width:300px;" /></td>
                            </tr>
                             <tr>
                <td class="cell_title">产品说明</td>
                <td class="cell_content">
                    <input name="Product_Note" type="text" id="Product_Note" size="50" maxlength="100" style="width: 300px;" /><%=Product_Note%></td>
            </tr>
                            <tr>
                                <td class="cell_title">产品描述</td>
                                <td class="cell_content">
                                    <textarea name="Product_Description" cols="50" rows="5" id="Product_Description"><%=Product_Description%></textarea></td>
                            </tr>
                            <tr>
                                <td class="cell_title">商品上架</td>
                                <td class="cell_content"><%if (Product_IsInsale == 0) { Response.Write("下架"); } else { Response.Write("上架"); } %><input type="hidden" id="Product_IsInsale" name="Product_IsInsale" value="1" /></td>
                            </tr>
                            <tr style="display: none;">
                                <td class="cell_title">是否参与优惠</td>
                                <td class="cell_content">
                                    <input name="Product_IsFavor" type="radio" id="Product_IsFavor1" value="1" <% =Public.CheckedRadio(Product_IsFavor.ToString(), "1")%> />是<input type="radio" name="Product_IsFavor" id="Product_IsFavor2" value="0" <% =Public.CheckedRadio(Product_IsFavor.ToString(), "0")%> />否</td>
                            </tr>
                            <tr>
                                <td class="cell_title">审核状态</td>
                                <td class="cell_content"><%if (Product_IsAudit == 0) { Response.Write("未审核"); } else { Response.Write("已审核"); } %><input type="hidden" id="Product_IsAudit" name="Product_IsAudit" value="0" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">商品预警数量</td>
                                <td class="cell_content">
                                    <input name="Product_AlertAmount" type="text" id="Product_AlertAmount" maxlength="10" onkeyup="this.value=this.value.replace(/\D/g,'')" onafterpaste="this.value=this.value.replace(/\D/g,'')" value="<% =Product_AlertAmount%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">零库存商品</td>
                                <td class="cell_content">
                                    <input name="Product_IsNoStock" type="radio" id="Product_IsNoStock1" value="1" <% =Public.CheckedRadio(Product_IsNoStock.ToString(), "1")%> />是<input type="radio" name="Product_IsNoStock" id="Product_IsNoStock0" value="0" <% =Public.CheckedRadio(Product_IsNoStock.ToString(), "0")%> />否</td>
                            </tr>
                            <tr>
                                <td class="cell_title">库存
                                </td>
                                <td class="cell_content">
                                    <input name="Product_StockAmount" type="text" id="Product_StockAmount" class="txt_border"
                                        size="10" maxlength="10" value="<%=Product_StockAmount %>" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" />

                                  (单位:<span id="Product_Unit_Name2"><%=Product_Unit %></span>)</td>
                            </tr>
                            <tr>
                                <td class="cell_title">排序</td>
                                <td class="cell_content">
                                    <input name="Product_Sort" type="text" id="Product_Sort" maxlength="10" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="<% =Product_Sort%>" /></td>
                            </tr>


                             <tr>
                                               <%-- <td width="138" class="name">起订量：</td>--%>
                                     <td class="cell_title">起订量</td>
                                                 <td class="cell_content">
                                                    <input name="U_Product_MinBook" type="text" id="U_Product_MinBook" style="width: 300px;" nkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="<%=U_Product_MinBook %>" class="input01" />(单位:<span id="Product_Unit_Name3"><%=Product_Unit %></span>)</td>
                                            </tr>

                        </table>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_2" style="display: none;">
                            <tr>
                                <td class="cell_title">扩展属性</td>
                                <td class="cell_content extendAttribute"><% =myApp.ProductExtendEditor(Product_TypeID, Product_ID)%></td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_5" style="display: none;">
                            <tr>
                                <td class="cell_title">上传图片</td>
                                <td class="cell_content">
                                    <iframe id="iframe1" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=productintro&formname=formadd&frmelement=Product_Intro&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe>
                                </td>
                            </tr>
                            <tr>
                                <td class="cell_title" valign="top">商品介绍</td>
                                <td class="cell_content">
                                    <textarea cols="80" id="Product_Intro" name="Product_Intro" rows="20"><% =Product_Intro%></textarea>
                                    <script type="text/javascript">
                                        CKEDITOR.replace('Product_Intro');
                                    </script>
                                </td>
                            </tr>
                            <tr>
                              <%--  <td class="cell_title" valign="top">规格参数</td>--%>
                                  <td class="cell_title" valign="top">物流售后</td>
                                <td class="cell_content">
                                    <textarea cols="80" id="U_Product_Parameters" name="U_Product_Parameters" rows="20"><% =U_Product_Parameters%></textarea>
                                    <script type="text/javascript">
                                        CKEDITOR.replace('U_Product_Parameters');
                                    </script>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_4" style="display: none;">
                            <tr>
                                <td class="cell_title">图片</td>
                                <td class="cell_content">
                                    <table width="630" border="0" cellspacing="0" cellpadding="5">
                                        <tr>
                                            <td width="110" height="110" align="center">
                                                <img id="img_product_img" src="<% =Public.FormatImgURL(product_img, "thumbnail") %>" width="100" height="100" onload="javascript:AutosizeImage(this,100,100);" /></td>
                                            <td width="110" align="center">
                                                <img id="img_product_img_ext_1" src="<% =Public.FormatImgURL(product_img_ext_1, "thumbnail") %>" width="100" height="100" onload="javascript:AutosizeImage(this,100,100);" /></td>
                                            <td width="110" align="center">
                                                <img id="img_product_img_ext_2" src="<% =Public.FormatImgURL(product_img_ext_2, "thumbnail") %>" width="100" height="100" onload="javascript:AutosizeImage(this,100,100);" /></td>
                                            <td width="110" align="center">
                                                <img id="img_product_img_ext_3" src="<% =Public.FormatImgURL(product_img_ext_3, "thumbnail") %>" width="100" height="100" onload="javascript:AutosizeImage(this,100,100);" /></td>
                                            <td width="110" align="center">
                                                <img id="img_product_img_ext_4" src="<% =Public.FormatImgURL(product_img_ext_4, "thumbnail") %>" width="100" height="100" onload="javascript:AutosizeImage(this,100,100);" /></td>
                                        </tr>
                                        <tr>
                                            <td style="text-align:center;color:red;">商品默认图片</td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('product_img');" />
                                                <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('product_img');"><input type="hidden" name="product_img" id="product_img" value="<% =product_img%>" /></td>

                                            <td align="center">
                                                <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('product_img_ext_1');" />
                                                <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('product_img_ext_1');"><input type="hidden" name="product_img_ext_1" id="product_img_ext_1" value="<% =product_img_ext_1%>" /></td>

                                            <td align="center">
                                                <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('product_img_ext_2');" />
                                                <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('product_img_ext_2');"><input type="hidden" name="product_img_ext_2" id="product_img_ext_2" value="<% =product_img_ext_2%>" /></td>

                                            <td align="center">
                                                <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('product_img_ext_3');" />
                                                <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('product_img_ext_3');"><input type="hidden" name="product_img_ext_3" id="product_img_ext_3" value="<% =product_img_ext_3%>" /></td>

                                            <td align="center">
                                                <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('product_img_ext_4');" />
                                                <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('product_img_ext_4');"><input type="hidden" name="product_img_ext_4" id="product_img_ext_4" value="<% =product_img_ext_4%>" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="td_upload" style="display: none">
                                <td class="cell_title">上传图片</td>
                                <td class="cell_content">
                                    <iframe id="iframe_upload" src="" width="100%" height="100" frameborder="0" scrolling="no"></iframe>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_3" style="display: none;">
                            <tr>
                                <td class="cell_title">TITLE<br />
                                    (页面标题)</td>
                                <td class="cell_content">
                                    <input name="Product_SEO_Title" type="text" id="Product_SEO_Title" size="50" maxlength="200" value="<%  =Product_SEO_Title%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">META_KEYWORDS<br />
                                    (页面关键词)</td>
                                <td class="cell_content">
                                    <input name="Product_SEO_Keyword" type="text" id="Product_SEO_Keyword" size="50" maxlength="200" value="<%  =Product_SEO_Keyword%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">META_DESCRIPTION<br />
                                    (页面描述)</td>
                                <td class="cell_content">
                                    <textarea name="Product_SEO_Description" cols="50" rows="5" id="Product_SEO_Description"><%  =Product_SEO_Description%></textarea></td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table" id="frm_optitem_6" style="display: none;">
                            <tr>
                                <td class="cell_title">聚合产品
                                </td>
                                <td class="cell_content">
                                    <a href="" id="btn_opt">
                                        <input type="button" value="选择产品" class="bt_orange" /></a><span class="tip">&nbsp;&nbsp;点击选择同款相关产品，复选框设置产品是否在产品列表显示</span><input
                                            type="hidden" name="favor_productid" id="favor_productid" />
                                    <div class="div_picker" id="product_picker">
                                        <%=mypromotion.ShowProduct(tools.NullStr(Session["selected_productid"]))%>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <div class="foot_gapdiv"></div>
                        <div class="float_option_div" id="float_option_div">
                            <input name="Product_Cate" type="hidden" id="Product_Cate" value="<% =Product_Cate%>" />
                            <input type="hidden" id="Product_ID" name="Product_ID" value="<% =Product_ID%>" />
                            <input name="Product_CateID" type="hidden" id="Product_CateID" value="<% =Product_CateID%>" />
                            <input name="Product_TypeID" type="hidden" id="Product_TypeID" value="<% =Product_TypeID%>" />
                            <input type="hidden" id="action" name="action" value="renew" />
                            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                            <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" />
                        </div>

                    </form>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">

        $("#btn_opt").click(function () {
            $("#btn_opt").attr("href", "/promotion/Product_check.aspx?target=group&timer=" + Math.random());
        });

        $("#btn_opt").zxxbox({ height: 450, width: 600, title: '', bar: false, btnclose: false });
    </script>
    <script src="/Scripts/promotion.js" type="text/javascript"></script>

</body>
</html>
