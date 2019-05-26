<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier myApp = new Supplier();
    Product product = new Product();
    //CategoryInfo 
    Addr addr = new Addr();
    int Product_TypeID = 0;
    myApp.Supplier_Login_Check("/supplier/Product_list.aspx");
    string Product_Code, Product_Name, Product_NameInitials, Product_SubName, Product_SubNameInitials, Product_PriceUnit, Product_Unit, Product_Note, Product_NoteColor, Product_Img, Product_Publisher, Product_Intro, Product_Site, Product_SEO_Title, Product_SEO_Keyword, Product_SEO_Description, Product_CateID, Product_Spec, Product_Maker, Product_GroupID, U_Product_DeliveryCycle, Product_LibraryImg, Product_GroupCode;
    int Product_ID, Product_Cate, Product_BrandID, Product_GroupNum, Product_StockAmount, Product_SaleAmount, Product_Review_Count, Product_Review_ValidCount, Product_IsInsale, Product_IsGroupBuy, Product_IsCoinBuy, Product_IsFavor, Product_IsGift, Product_IsAudit, Product_CoinBuy_Coin, Product_AlertAmount, Product_IsNoStock, Product_Sort, Product_QuotaAmount, Product_IsGiftCoin, Product_SupplierID, U_Product_MinBook, Product_PriceType;
    DateTime Product_Addtime;
    int Product_Supplier_CommissionCateID = 0;
    double Product_MKTPrice, Product_GroupPrice, Product_Price, Product_Weight, Product_Review_Average, Product_Gift_Coin, Product_ManualFee;
    bool grade_price = false;
    string Product_Keyword = "";
    string Product_Keyword1 = "";
    string Product_Keyword2 = "";
    string Product_Keyword3 = "";
    string Product_Keyword4 = "";
    string Product_Keyword5 = "";
    string Product_Ids = "";
    string Product_State_Name = "";
    string Product_City_Name = "";
    string Product_County_Name = "";

    string product_img, product_img_ext_1, product_img_ext_2, product_img_ext_3, product_img_ext_4;


    Product_ID = tools.CheckInt(Request.Form["Product_ID"]);
    //Product_TypeID = tools.CheckInt(Request.Form["Product_TypeID"]);
    Product_Cate = tools.CheckInt(Request.Form["Product_Cate"]);

    CategoryInfo CategoryEntity = product.GetCategoryByID(Product_Cate);
    if (CategoryEntity != null)
    {
        Product_TypeID = CategoryEntity.Cate_TypeID;
    }


    Product_GroupID = tools.CheckStr(Request.Form["Product_GroupID"]);
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
        pub.Msg("error", "错误信息", "请选择商品主分类！", false, "{back}");

    //if (Product_TypeID == 0)
    //    pub.Msg("error", "错误信息", "请选择商品所属类型！", false, "{back}");

    //if (Product_Code.Length == 0)
    //{
    //    pub.Msg("error", "错误信息", "请填写商品编码！", false, "{back}");
    //}
    //else
    //{
    //    if (!myApp.CheckProductCode(Product_Code, Product_ID))
    //    {
    //        pub.Msg("error", "错误信息", "已存在该商品编码，请尝试更换其他编码！", false, "{back}");
    //    }
    //}

    if (Product_Name.Length == 0)
        pub.Msg("error", "错误信息", "请填写商品名称！", false, "{back}");

    Product_BrandID = 0;
    Product_SubName = "";
    Product_MKTPrice = 0;
    Product_GroupPrice = 0;
    Product_Price = 0;
    Product_PriceUnit = "";
    Product_Unit = "";
    Product_GroupNum = 0;
    Product_Note = "";
    Product_NoteColor = "";
    Product_Weight = 0;
    Product_Img = "";
    Product_Publisher = "";
    Product_StockAmount = 0;
    Product_SaleAmount = 0;
    Product_Review_Count = 0;
    Product_Review_ValidCount = 0;
    Product_Review_Average = 0;
    Product_IsInsale = 0;
    Product_IsGroupBuy = 0;
    Product_IsCoinBuy = 0;
    Product_IsFavor = 0;
    Product_IsGift = 0;
    Product_IsAudit = 0;
    Product_IsGiftCoin = 0;
    Product_Gift_Coin = 0;
    Product_CoinBuy_Coin = 0;
    Product_Intro = "";
    Product_AlertAmount = 0;
    Product_IsNoStock = 0;
    Product_Spec = "";
    Product_Maker = "";
    Product_Sort = 0;
    Product_QuotaAmount = 0;
    Product_SEO_Title = "";
    Product_SEO_Keyword = "";
    Product_SEO_Description = "";
    product_img = "";
    product_img_ext_1 = "";
    product_img_ext_2 = "";
    product_img_ext_3 = "";
    product_img_ext_4 = "";
    Product_GroupCode = "";
    U_Product_DeliveryCycle = "";
    U_Product_MinBook = 1;
    Product_PriceType = 1;
    Product_ManualFee = 0;
    Product_LibraryImg = "";


    string U_Product_Parameters = "";
    int U_Product_SalesByProxy = 0, U_Product_Shipper = 0;

    ProductInfo entity = myApp.GetProductByID(Product_ID);
    if (entity == null)
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "{back}");
        Response.End();
    }
    else
    {
        Product_ID = entity.Product_ID;
        Product_BrandID = entity.Product_BrandID;
        Product_SupplierID = entity.Product_SupplierID;
        Product_Supplier_CommissionCateID = entity.Product_Supplier_CommissionCateID;
        Product_SubName = entity.Product_SubName;
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
        Product_GroupCode = entity.Product_GroupCode;
        U_Product_Parameters = entity.U_Product_Parameters;
        U_Product_SalesByProxy = entity.U_Product_SalesByProxy;
        U_Product_Shipper = entity.U_Product_Shipper;
        U_Product_DeliveryCycle = entity.U_Product_DeliveryCycle;
        U_Product_MinBook = entity.U_Product_MinBook;
        Product_PriceType = entity.Product_PriceType;
        Product_ManualFee = entity.Product_ManualFee;
        Product_LibraryImg = entity.Product_LibraryImg;
        Product_State_Name = entity.Product_State_Name;
        Product_City_Name = entity.Product_City_Name;
        Product_County_Name = entity.Product_County_Name;

        if (Product_Keyword.Length > 0)
        {
            string[] Keyword = Product_Keyword.Split('|');
            if (Keyword.Length == 5)
            {
                Product_Keyword1 = Keyword[0];
                Product_Keyword2 = Keyword[1];
                Product_Keyword3 = Keyword[2];
                Product_Keyword4 = Keyword[3];
                Product_Keyword5 = Keyword[4];
            }
        }
        string[] imgArr = myApp.GetProductImg(Product_ID);
        product_img = imgArr[0];
        product_img_ext_1 = imgArr[1];
        product_img_ext_2 = imgArr[2];
        product_img_ext_3 = imgArr[3];
        product_img_ext_4 = imgArr[4];

        Product_Ids = myApp.GetGroupProductID(entity.Product_GroupCode);

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
    
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="商品管理 - 我是卖家 - " + pub.SEO_TITLE()%></title>
    <meta name="Keywords" content="<% = Application["Site_Keyword"]%>" />
    <meta name="Description" content="<%=Application["Site_Description"]%>" />

    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index_newadd.css" rel="stylesheet" type="text/css" />
  
   <!--layer弹出框.js .css引用 开始-->
 <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
       <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js" ></script>
  <!--layer弹出框.js .css引用 结束-->

    <%--<script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>--%>
    <script src="/scripts/common.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/supplier.js"></script>

    <script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
    <script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>

    <link rel="stylesheet" type="text/css" href="/scripts/treeview/dhtmlxtree.css" />

    <script src="/scripts/treeview/dhtmlxcommon.js" type="text/javascript"></script>
    <script src="/scripts/treeview/dhtmlxtree.js" type="text/javascript"></script>

    <script type="text/javascript">
        function openUpload(openObj) {

            $("#td_upload").show();
            $("#iframe_upload").attr("src", "<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=product&formname=formadd&frmelement=" + openObj + "&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>");
        }

        function delImage(openObj) {
            $("#img_" + openObj).attr("src", "/images/detail_no_pic.gif");
            $("#" + openObj).val("/images/detail_no_pic.gif");
        }
    </script>

    <script type="text/javascript">

        function SelectProduct() {
            window.open("selectproduct2.aspx", "选择商品", "height=560, width=800, toolbar=no, menubar=no, scrollbars=yes, resizable=no, location=no, status=no,top=100,left=300")
        }

        function product_del(product_id) {
            $.ajax({
                url: encodeURI("product_do.aspx?action=product_del&product_id=" + product_id + "&timer=" + Math.random()),
                type: "get",
                global: false,
                async: false,
                dataType: "html",
                success: function (data) {
                    $("#yhnr").html(data);
                },
                error: function () {
                    alert("Error Script");
                }
            });
        }
    </script>
    <!--滑动门 结束-->
    <script src="/js/1.js" type="text/javascript"></script>
    <!--弹出菜单 start-->
    <script type="text/javascript">
        $(document).ready(function () {
            var byt = $(".testbox li");
            var box = $(".boxshow")
            byt.hover(
                 function () {
                     $(this).find(".boxshow").show(); $(this).find(".a3").attr("class", "a3 a3h");
                 },
                function () {
                    $(this).find(".boxshow").hide(); $(this).find(".a3h").attr("class", "a3");
                }
            );
        });


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
    </script>
    <!--弹出菜单 end-->

     
   <!--示范一个公告层 开始-->
  <script type="text/javascript">
      function SignUpNow() {
          layer.open({
              type: 2
 , title: false //不显示标题栏
              //, closeBtn: false
 , area: ['480px;', '340px']

 , shade: 0.8
 , id: 'LAY_layuipro' //设定一个id，防止重复弹出
 , resize: false
 , btnAlign: 'c'
 , moveType: 1 //拖拽模式，0或者1              
              , content: ("/Bid/SignUpPopup.aspx")
          });
      }
    </script>
   <!--示范一个公告层 结束-->

    <style type="text/css">
        .txt_input_border {
            float: none;
            width: 150px;
            height: 20px;
            line-height: 20px;
            padding: 4px 20px 4px 3px;
            border: 1px solid #cccccc;
            font-size: 14px;
            font-family: "微软雅黑";
        }

        .txt_select_border {
            width: 150px;
            height: 25px;
            line-height: 25px;
            padding: 4px 5px 4px 3px;
        }

        .blk07_sz h2 .list02 {
            background-color: #fff;
            border-top: 1px solid #dddddd;
            border-right: 1px solid #dddddd;
        }

        .input01 {
            background-image: none;
        }

        #table_extend {
            margin: 0px 10px;
        }

            #table_extend td {
                padding: 5px;
            }
    </style>

</head>
<body>

    <uctop:top ID="top1" runat="server" />


    <div class="webwrap">
        <div class="content02" style="background-color: #FFF;">

            <!--位置说明 开始-->
            <div class="position">当前位置 > <a href="/supplier/index.aspx">我是卖家</a> > <strong>商品修改</strong></div>
            <!--位置说明 结束-->
            <div class="partd_1">
                <div class="pd_left">
                    <%myApp.Get_Supplier_Left_HTML(3, 2); %>
                </div>
                <div class="pd_right">
                    <div class="blk14_1" style="margin-top: 0px;">
                        <h2>商品管理</h2>
                        <div class="blk07_sz">
                            <h2>
                                <ul class="list02">
                                    <li id="apply_1" class="on" onclick="Set_Tab('apply',1,6,'on','');">基本信息</li>
                                    <%--      <li id="apply_2" onclick="Set_Tab('apply',2,6,'on','');">扩展属性</li>
                                    <li id="apply_3" onclick="Set_Tab('apply',3,6,'on','');">图片信息</li>--%>
                                    <li id="apply_4" onclick="Set_Tab('apply',4,6,'on','');">介绍信息</li>

                                </ul>
                                <div class="clear"></div>
                            </h2>

                            <form name="formadd" id="formadd" method="post" action="/supplier/product_do.aspx">

                                <div class="b07_main_sz" id="apply_1_content">

                                    <div class="b07_info04_sz" style="display: normal; margin-left: 50px">
                                        <span style="font-size: 18px; color: #ff6600">基础信息</span>
                                        <table width="793" border="0" cellspacing="0" cellpadding="0">

                                            <%--<tr>
                                            <td  class="name">扩展属性
                                            </td>
                                            <td>
                                                <% =myApp.ProductExtendEditor(Product_TypeID, Product_ID)%>
                                            </td>
                                        </tr>--%>
                                            <%-- <tr>
                                                <td width="138" class="name">产品品牌：</td>
                                                <td>
                                                    <select name="Product_BrandID" id="Product_BrandID" style="width: 160px;">
                                                        <option value="0">选择商品品牌</option>
                                                        <% =myApp.ProductBrandOption(Product_TypeID, Product_BrandID)%>
                                                    </select>
                                                    <i>*</i></td>
                                            </tr>--%>

                                            <tr>
                                                <td width="138" class="name">商品产地</td>
                                                <td class="cell_content" id="div_area">
                                                    <%=addr.SelectProductState("div_area","Product_State_Name","Product_City_Name","Product_County_Name",Product_State_Name,Product_City_Name,Product_County_Name) %></td>
                                                <td>
                                                    <input type="hidden" id="Product_State_Name" name="Product_State_Name" value="<%=Product_State_Name%>" /><input type="hidden" id="Product_City_Name" name="Product_City_Name" value="<%=Product_City_Name%>" /><input type="hidden" id="Product_County_Name" name="Product_County_Name" value="<%=Product_County_Name%>" /></td>
                                            </tr>

                                            <%--<tr>
                                            <td width="138" class="name">产品编号：</td>
                                            <td>
                                                <input name="Product_Code" type="text" id="Product_Code" style="width: 300px;" class="input01" readonly="readonly" value="<%=Product_Code %>" /><i>*</i></td>
                                        </tr>--%>
                                            <tr>
                                                <td width="138" class="name">产品名称：</td>
                                                <td>
                                                    <input name="Product_Name" type="text" id="Product_Name" style="width: 300px;" class="input01" value="<%=Product_Name %>" /><i>*</i></td>
                                            </tr>
                                            <%-- <tr>
                                                <td width="138" class="name">规格型号：</td>
                                                <td>
                                                    <input name="Product_Spec" type="text" id="Product_Spec" style="width: 300px;" class="input01" value="<%=Product_Spec %>" />
                                                    <input type="hidden" name="Product_PriceType" value="1" />
                                                </td>
                                            </tr>--%>

                                            <%--<tr>
                                            <td width="138" class="name">计价方式：</td>
                                            <td>
                                                <input type="radio" id="Product_PriceType1" name="Product_PriceType" value="1" <%=pub.CheckedRadio(Product_PriceType.ToString(),"1") %> onclick="$('#tr_price').show(); $('#tr_ManualFee').hide(); $('#tr_price1').hide();" class="input02" />一口价
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input type="radio" id="Product_PriceType2" name="Product_PriceType" <%=pub.CheckedRadio(Product_PriceType.ToString(),"2") %> value="2" onclick="$('#tr_price').hide(); $('#tr_ManualFee').show(); $('#tr_price1').show();" class="input02" />计价式</td>
                                        </tr>--%>
                                            <tr>
                                                <td width="138" class="name">商品单位</td>
                                                <td id="selected_Unit">
                                                    <%--  <% =new Product() .Product_Unit_Select(Product_ID, "Product_Unit")%>--%>
                                                    <input name="Product_Unit" type="text" id="Product_Unit" style="width: 300px;" value="<%=Product_Unit %>" class="input01" onmousemove="ChangeUnitCase();" onblur="ChangeUnitCase();" />
                                                </td>
                                            </tr>

                                            <tr id="tr_price" <% if (Product_PriceType == 2) { Response.Write("style=\"display:none;\""); } %>>
                                                <td width="138" class="name">商品价格：</td>
                                                <td>
                                                    <input name="Product_Price" type="text" id="Product_Price" style="width: 300px;" class="input01" value="<%=Product_Price %>" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" />
                                                    元(单位:<span id="Product_Unit_Name"><%=Product_Unit %></span>)</td>
                                            </tr>

                                            <tr>
                                                <td width="138" class="name">商品重量：</td>
                                                <td>
                                                    <input name="Product_Weight" type="text" id="Product_Weight" style="width: 300px;" value="<%=Product_Weight %>" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" class="input01" onblur="GetProductPrice();" />
                                                    (单位:<span id="Product_Unit_Name_weight"><%=Product_Unit %></span>)</td>
                                            </tr>





                                            <%if (myApp.ProductTagIsExist(0))
                                              {                                                 
                                            %>

                                            <tr>
                                                <td class="name">产品推荐：</td>
                                                <td align="left">
                                                    <%=myApp.ProductTagChoose(Product_ID)%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" align="left" style="padding-left: 150px; color: red; font-size: 12px;">说明: 通过选中所属标签,来控制商品在该标签产品展示区域是否显示 </td>
                                            </tr>
                                            <%} %>

                                            <tr>
                                                <td class="name">商品交货周期：</td>
                                                <td>
                                                    <input name="U_Product_DeliveryCycle" type="text" id="U_Product_DeliveryCycle" style="width: 300px;" class="input01" value="<%=U_Product_DeliveryCycle %>" />
                                                    (单位:天)</td>
                                            </tr>



                                            <%--<tr id="tr_price1" <%if (entity.Product_PriceType == 1) { Response.Write("style=\"display:none;\""); }%>>
                                                <td width="138" class="name">价格预览：</td>
                                                <td>
                                                    <span id="span_price"><%=pub.FormatCurrency(pub.GetProductPrice(entity.Product_ManualFee,entity.Product_Weight)) %></span>
                                                </td>
                                            </tr>--%>

                                            <tr>
                                                <%--  <td width="138" class="name">产品备注：</td>--%>
                                                <td width="138" class="name">产品说明：</td>
                                                <td>
                                                    <input name="Product_Note" type="text" id="Product_Note" style="width: 300px;" class="input01" value="<%=Product_Note %>" /></td>
                                            </tr>
                                            <tr>
                                                <td width="138" class="name">是否上架：</td>
                                                <td>
                                                    <input name="Product_IsInsale" type="radio" id="Product_IsInsale1" value="1" <%=pub.CheckedRadio(Product_IsInsale.ToString(),"1") %> class="input02" />是<input type="radio" name="Product_IsInsale" id="Product_IsInsale2" value="0" class="input02" <%=pub.CheckedRadio(Product_IsInsale.ToString(),"0") %> />否
                                            <i>*</i></td>
                                            </tr>

                                            <tr>
                                                <td class="name">产品库存：</td>
                                                <td>
                                                    <input name="Product_StockAmount" type="text" id="Product_StockAmount" class="txt_border"
                                                        size="10" maxlength="10" value="<%=Product_StockAmount %>" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" />
                                                    (单位:<span id="Product_Unit_Name2"><%=Product_Unit %></span>)<i>*</i><span id="stockamount_tip"></span>
                                                </td>
                                            </tr>
                                            <%-- <tr>
                                <td class="cell_title">库存
                                </td>
                                <td class="cell_content">
                                    <input name="Product_StockAmount" type="text" id="Product_StockAmount" class="txt_border"
                                        size="10" maxlength="10" value="<%=Product_StockAmount %>" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" />

                                </td>
                            </tr>--%>


                                            <tr>
                                                <td width="138" class="name">排序：</td>
                                                <td>
                                                    <input name="Product_Sort" type="text" id="Product_Sort" style="width: 300px;" value="<%=Product_Sort %>" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" class="input01" /></td>
                                            </tr>

                                            <tr>
                                                <td width="138" class="name">起订量 ≥</td>
                                                <td>
                                                    <input name="U_Product_MinBook" type="text" id="U_Product_MinBook" style="width: 300px;" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="<%=U_Product_MinBook %>" class="input01" />(单位:<span id="Product_Unit_Name3"><%=Product_Unit %></span>)<i>*</i></td>
                                            </tr>
                                        </table>
                                    </div>


                                    <%--   <div class="b07_main" id="apply_2_content" style="display: none;">--%>
                                    <div class="b07_main" style="margin: 50px 0; margin-left: 90px">
                                        <span style="font-size: 18px; color: #ff6600">扩展属性</span>
                                        <div class="b07_info04">

                                            <%=myApp.ProductExtendEditor(Product_TypeID,Product_ID) %>
                                        </div>


                                    </div>





                                    <%--   <div class="b07_main" id="apply_3_content" style="display: normal; margin: 50px 0; margin-left: 90px;">--%>
                                    <div class="b07_main" style="display: normal; margin: 50px 0; margin-left: 90px;">
                                        <span style="font-size: 18px; color: #ff6600">图片信息</span>
                                        <table width="793" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="20%">
                                                    <table border="0" width="100%" cellpadding="3" cellspacing="0">
                                                        <tr>
                                                            <td align="center" height="120">
                                                                <img id="img_product_img" src="<%=pub.FormatImgURL(product_img,"fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" height="30">
                                                                <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('product_img');" />
                                                                <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('product_img');"><input type="hidden" name="product_img" id="product_img" value="<%=product_img %>" />
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </td>
                                                <td width="20%">
                                                    <table border="0" width="100%" cellpadding="3" cellspacing="0">
                                                        <tr>
                                                            <td align="center" height="120">
                                                                <img id="img_product_img_ext_1" src="<%=pub.FormatImgURL(product_img_ext_1,"fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" height="30">
                                                                <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('product_img_ext_1');" />
                                                                <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('product_img_ext_1');"><input type="hidden" name="product_img_ext_1" id="product_img_ext_1" value="<%=product_img_ext_1 %>" />
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </td>
                                                <td width="20%">
                                                    <table border="0" width="100%" cellpadding="3" cellspacing="0">
                                                        <tr>
                                                            <td align="center" height="120">
                                                                <img id="img_product_img_ext_2" src="<%=pub.FormatImgURL(product_img_ext_2,"fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" height="30">
                                                                <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('product_img_ext_2');" />
                                                                <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('product_img_ext_2');"><input type="hidden" name="product_img_ext_2" id="product_img_ext_2" value="<%=product_img_ext_2 %>" />
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </td>
                                                <td width="20%">
                                                    <table border="0" width="100%" cellpadding="3" cellspacing="0">
                                                        <tr>
                                                            <td align="center" height="120">
                                                                <img id="img_product_img_ext_3" src="<%=pub.FormatImgURL(product_img_ext_3,"fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" height="30">
                                                                <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('product_img_ext_3');" />
                                                                <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('product_img_ext_3');"><input type="hidden" name="product_img_ext_3" id="product_img_ext_3" value="<%=product_img_ext_3 %>" />
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </td>
                                                <td width="20%">
                                                    <table border="0" width="100%" cellpadding="3" cellspacing="0">
                                                        <tr>
                                                            <td align="center" height="120">
                                                                <img id="img_product_img_ext_4" src="<%=pub.FormatImgURL(product_img_ext_4,"fullpath") %>" width="120" height="120" onload="javascript:AutosizeImage(this,120,120);">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" height="30">
                                                                <input type="button" name="btn_upload" value="上传" class="smb_btn" onclick="javascript: openUpload('product_img_ext_4');" />
                                                                <input type="button" name="btn_upload" value="删除" class="smb_btn" onclick="javascript: delImage('product_img_ext_4');"><input type="hidden" name="product_img_ext_4" id="product_img_ext_4" value="<%=product_img_ext_1 %>" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center; color: red; padding: 5px;">商品默认图片</td>
                                            </tr>
                                            <tr id="td_upload">
                                                <td colspan="5" style="padding-left: 20px;" align="left">图片上传：<iframe id="iframe_upload" src="" width="100%" height="90" frameborder="0" scrolling="no" align="absmiddle"></iframe>
                                                    <br />
                                                    <%--      <span class="t12_grey" style="height: 30px; line-height: 30px; color: red">图片上传：建议图片尺寸：800*800；支持格式：jpg、gif、png；大小不要超过3M；商品图片信息 </span>--%>      <span class="t12_grey" style="height: 30px; color: red">图片上传：建议图片尺寸：800*800；支持格式：jpg、gif、png；大小不要超过3M；商品图片信息 </span>
                                                </td>

                                            </tr>
                                        </table>
                                    </div>
                                    <%-- </div>--%>
                                </div>
                                <div class="b07_main" id="apply_4_content" style="display: normal; margin: 50px auto; margin-left: 90px;">
                                    <%--  <span style="font-size: 18px; color: #ff6600">介绍信息</span>--%>
                                    <div class="b07_info04">
                                        <table width="793" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td>
                                                    <iframe id="iframe2" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=shoppages&formname=formadd&frmelement=Product_Intro&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>" width="100%" height="90" frameborder="0" scrolling="no"></iframe>
                                                    <br />
                                                    <span class="t12_grey" style="height: 30px; line-height: 30px;">建议图片尺寸：宽度不要超过900,高度不限；支持格式：jpg、gif、png；大小不要超过3M</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <textarea cols="80" id="Product_Intro" name="Product_Intro" rows="16"><%=Product_Intro %></textarea>
                                                    <script type="text/javascript">
                                                        var Product_IntroEditor;
                                                        KindEditor.ready(function (K) {
                                                            Product_IntroEditor = K.create('#Product_Intro', {
                                                                width: '100%',
                                                                height: '500px',
                                                                filterMode: false,
                                                                afterBlur: function () { this.sync(); }
                                                            });
                                                        });
                                                    </script>
                                                </td>
                                            </tr>


                                            <%-- <tr>
                                            <td width="138" class="name">订购流程图片：
                                            </td>
                                            <td width="755">
                                                <iframe id="iframe1" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=shoppages&formname=formadd&frmelement=U_Product_Parameters&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>" width="100%" height="90" frameborder="0" scrolling="no"></iframe>
                                                <br />
                                                <span class="t12_grey" style="height: 30px; line-height: 30px;">建议图片尺寸：宽度不要超过900,高度不限；支持格式：jpg、gif、png；大小不要超过3M</span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="name">订购流程：
                                            </td>
                                            <td>
                                                <textarea cols="80" id="U_Product_Parameters" name="U_Product_Parameters" rows="16"><%=U_Product_Parameters %></textarea>
                                                <script type="text/javascript">
                                                    var U_Product_ParametersEditor;
                                                    KindEditor.ready(function (K) {
                                                        U_Product_ParametersEditor = K.create('#U_Product_Parameters', {
                                                            width: '100%',
                                                            height: '500px',
                                                            filterMode: false,
                                                            afterBlur: function () { this.sync(); }
                                                        });
                                                    });
                                                </script>
                                            </td>
                                        </tr>--%>
                                        </table>
                                    </div>
                                </div>

                                <div class="b07_main" id="apply_5_content" style="display: none;">
                                    <div class="b07_info04">
                                        <table width="793" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td class="name">精品库图片</td>
                                                <td>
                                                    <iframe id="iframe3" src="<% =Application["Upload_Server_URL"]%>/public/uploadify.aspx?App=productgifts&formname=formadd&frmelement=Product_LibraryImg&rtvalue=1&rturl=<% =Application["Upload_Server_Return_WWW"]%>" width="100%" height="90" frameborder="0" scrolling="no"></iframe>
                                                    <br />
                                                    <span class="t12_grey" style="height: 30px; line-height: 30px;">建议图片尺寸：1900*800；支持格式：jpg、gif、png；大小不要超过3M</span>
                                                </td>
                                            </tr>
                                            <tr id="tr_Product_LibraryImg" <%if (Product_LibraryImg.Length == 0) { Response.Write("style=\"display:none;\""); }%>>
                                                <%--     <td class="cell_title"></td>--%>
                                                <td class="cell_content">
                                                    <img src="<%=pub.FormatImgURL(Product_LibraryImg,"fullpath") %>" id="img_Product_LibraryImg" alt="" onload="javascript:AutosizeImage(this,475,200);" width="475" height="200" />
                                                    <input type="hidden" id="Product_LibraryImg" name="Product_LibraryImg" value="<%=Product_LibraryImg %>" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>

                                <div class="b07_main" id="apply_6_content" style="display: none;">
                                    <div class="b07_info04">
                                        <table width="793" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td align="right" style="line-height: 24px;" width="100" class="t12_53">批发价格设置</td>
                                                <td align="left">
                                                    <script>

                                                        function add_wholeprice() {
                                                            var maxnum = $("#maxnum_1").val();
                                                            maxnum = parseInt(String(maxnum));
                                                            $.ajaxSetup({ async: false });
                                                            var minamount = $("#max_product_wholeprice_amount_" + maxnum).val();
                                                            $("#gift_item").load("product_do.aspx?action=addwholeprice&minamount=" + minamount + "&maxnum=" + (maxnum + 1) + "&timer=" + Math.random());
                                                            $("#gift_more1").html($("#gift_more1").html() + $("#gift_item").html());
                                                            $("#maxnum_1").val((maxnum + 1));

                                                        }

                                                        function del_wholeprice(obj) {
                                                            $("#" + obj).html("");
                                                            $("#" + obj).hide();
                                                        }
                                                        function changeprice(obj) {

                                                            var a = document.getElementById("max_product_wholeprice_amount_" + obj).value;

                                                            a = parseInt(String(a));
                                                            obj = parseInt(String(obj)) + 1;
                                                            document.getElementById("min_product_wholeprice_amount_" + obj).value = a + 1;
                                                        }

                                                        function checkamount1() {
                                                            var min = $("#min_product_wholeprice_amount_1").val();
                                                            var max = $("#max_product_wholeprice_amount_1").val();
                                                            var price = $("#product_wholeprice_amount_1").val();
                                                            if (min > 0) {
                                                                if (max >= min || max == "0" || max == "") {
                                                                    if (price > 0) {
                                                                        return true;
                                                                    }
                                                                    else {
                                                                        return false;
                                                                    }
                                                                }
                                                                else {
                                                                    return false;
                                                                }
                                                            }
                                                            else {
                                                                return true;
                                                            }
                                                        }

                                                        function checkamount2() {
                                                            var min = $("#min_product_wholeprice_amount_2").val();
                                                            var max = $("#max_product_wholeprice_amount_2").val();
                                                            var price = $("#product_wholeprice_amount_2").val();

                                                            if (min > 0) {
                                                                if (max >= min || max == "0" || max == "") {
                                                                    if (price > 0) {
                                                                        return true;
                                                                    }
                                                                    else {
                                                                        return false;
                                                                    }
                                                                }
                                                                else {
                                                                    return false;
                                                                }
                                                            }
                                                            else {
                                                                return true;
                                                            }
                                                        }

                                                        function checkamount3() {
                                                            var min = $("#min_product_wholeprice_amount_3").val();
                                                            var max = $("#max_product_wholeprice_amount_3").val();
                                                            var price = $("#product_wholeprice_amount_3").val();
                                                            if (min > 0) {
                                                                if (max >= min || max == "0" || max == "") {
                                                                    if (price > 0) {
                                                                        return true;
                                                                    }
                                                                    else {
                                                                        return false;
                                                                    }
                                                                }
                                                                else {
                                                                    return false;
                                                                }
                                                            }
                                                            else {
                                                                return true;
                                                            }
                                                        }

                                                        function checkamount4() {
                                                            var min = $("#min_product_wholeprice_amount_4").val();
                                                            var max = $("#max_product_wholeprice_amount_4").val();
                                                            var price = $("#product_wholeprice_amount_4").val();
                                                            if (min > 0) {
                                                                if (max >= min || max == "0" || max == "") {
                                                                    if (price > 0) {
                                                                        return true;
                                                                    }
                                                                    else {
                                                                        return false;
                                                                    }
                                                                }
                                                                else {
                                                                    return false;
                                                                }
                                                            }
                                                            else {
                                                                return true;
                                                            }
                                                        }

                                                        function checkamount5() {
                                                            var min = $("#min_product_wholeprice_amount_5").val();
                                                            var max = $("#max_product_wholeprice_amount_5").val();
                                                            var price = $("#product_wholeprice_amount_5").val();
                                                            if (min > 0) {
                                                                if (max >= min || max == "0" || max == "") {
                                                                    if (price > 0) {
                                                                        return true;
                                                                    }
                                                                    else {
                                                                        return false;
                                                                    }
                                                                }
                                                                else {
                                                                    return false;
                                                                }
                                                            }
                                                            else {
                                                                return true;
                                                            }
                                                        }

                                                        function checkamount6() {
                                                            var min = $("#min_product_wholeprice_amount_6").val();
                                                            var max = $("#max_product_wholeprice_amount_6").val();
                                                            var price = $("#product_wholeprice_amount_6").val();
                                                            if (min > 0) {
                                                                if (max >= min || max == "0" || max == "") {
                                                                    if (price > 0) {
                                                                        return true;
                                                                    }
                                                                    else {
                                                                        return false;
                                                                    }
                                                                }
                                                                else {
                                                                    return false;
                                                                }
                                                            }
                                                            else {
                                                                return true;
                                                            }
                                                        }

                                                        function checkamount7() {
                                                            var min = $("#min_product_wholeprice_amount_7").val();
                                                            var max = $("#max_product_wholeprice_amount_7").val();
                                                            var price = $("#product_wholeprice_amount_7").val();
                                                            if (min > 0) {
                                                                if (max >= min || max == "0" || max == "") {
                                                                    if (price > 0) {
                                                                        return true;
                                                                    }
                                                                    else {
                                                                        return false;
                                                                    }
                                                                }
                                                                else {
                                                                    return false;
                                                                }
                                                            }
                                                            else {
                                                                return true;
                                                            }
                                                        }

                                                        function checkamount8() {
                                                            var min = $("#min_product_wholeprice_amount_8").val();
                                                            var max = $("#max_product_wholeprice_amount_8").val();
                                                            var price = $("#product_wholeprice_amount_8").val();
                                                            if (min > 0) {
                                                                if (max >= min || max == "0" || max == "") {
                                                                    if (price > 0) {
                                                                        return true;
                                                                    }
                                                                    else {
                                                                        return false;
                                                                    }
                                                                }
                                                                else {
                                                                    return false;
                                                                }
                                                            }
                                                            else {
                                                                return true;
                                                            }
                                                        }

                                                        function checkamount9() {
                                                            var min = $("#min_product_wholeprice_amount_9").val();
                                                            var max = $("#max_product_wholeprice_amount_9").val();
                                                            var price = $("#product_wholeprice_amount_9").val();
                                                            if (min > 0) {
                                                                if (max >= min || max == "0" || max == "") {
                                                                    if (price > 0) {
                                                                        return true;
                                                                    }
                                                                    else {
                                                                        return false;
                                                                    }
                                                                }
                                                                else {
                                                                    return false;
                                                                }
                                                            }
                                                            else {
                                                                return true;
                                                            }
                                                        }

                                                        function checkamount10() {
                                                            var min = $("#min_product_wholeprice_amount_10").val();
                                                            var max = $("#max_product_wholeprice_amount_10").val();
                                                            var price = $("#product_wholeprice_amount_10").val();
                                                            if (min > 0) {
                                                                if (max >= min || max == "0" || max == "") {
                                                                    if (price > 0) {
                                                                        return true;
                                                                    }
                                                                    else {
                                                                        return false;
                                                                    }
                                                                }
                                                                else {
                                                                    return false;
                                                                }
                                                            }
                                                            else {
                                                                return true;
                                                            }
                                                        }

                                                        function checkamount() {
                                                            var check1 = checkamount1();
                                                            var check2 = checkamount2();
                                                            var check3 = checkamount3();
                                                            var check4 = checkamount4();
                                                            var check5 = checkamount5();
                                                            var check6 = checkamount6();
                                                            var check7 = checkamount7();
                                                            var check8 = checkamount8();
                                                            var check9 = checkamount9();
                                                            var check10 = checkamount10();

                                                            if (check1 && check2 && check3 && check4 && check5 && check6 && check7 && check8 && check9 && check10) {
                                                                return true;
                                                            }
                                                            else {
                                                                alert("批发价格数量定义错误");
                                                                return false;
                                                            }
                                                        }
                                                    </script>
                                                    <%=myApp.GetProductWholeSales(entity) %>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>

                                <div class="b07_main">
                                    <div class="b07_info04">
                                        <table width="793" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td width="138">&nbsp;</td>
                                                <td width="755">
                                                    <a href="javascript:void();" onclick="product_save('renew')" class="a11"></a>
                                                    <input name="action" type="hidden" id="action" value="renew" />
                                                    <input name="Product_ID" type="hidden" id="Product_ID" value="<% =Product_ID%>" />
                                                    <input name="Product_Cate" type="hidden" id="Product_Cate" value="<% =Product_Cate%>" />
                                                    <input name="Product_CateID" type="hidden" id="Product_CateID" value="<% =Product_CateID%>" />
                                                    <input name="Product_GroupID" type="hidden" id="Product_GroupID" value="<% =Product_GroupID%>" />
                                                    <input name="Product_TypeID" type="hidden" id="Product_TypeID" value="<% =Product_TypeID%>" />
                                                    <input name="Product_Extends" type="hidden" id="Product_Extends" value="<%=tools.NullStr(Session["extend_value"]) %>" />
                                                    <input name="Product_GroupCode" type="hidden" id="Product_GroupCode" value="<%=Product_GroupCode %>" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <%--右侧浮动弹框 开始--%>
 <div id="leftsead">
        <ul>           
            <li>
                 <a href="javascript:void(0);" onclick="SignUpNow();">
                    <div class="hides" style="width: 130px;height:50px; display: none;" id="qq">
                        <div class="hides" id="p1">
                           <img src="/images/nav_1_1.png" />
                        </div>
                    </div>
                    <img src="/images/nav_1.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="tel">
                <a href="javascript:void(0)">
                    <div class="hides" style="width: 130px;height:50px; display: none;" id="tels">
                        <div class="hides" id="p2">
                            <img src="/images/nav_2_1.png">
                        </div>

                    </div>
                    <img src="/images/nav_2.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="btn">
              <a id="top_btn" href="http://wpa.qq.com/msgrd?v=3&uin=3558103267&site=qq&menu=yes" target="_blank">
                    <div class="hides" style="width: 130px;height:50px; display: none">
                        <div class="hides" id="p3">
                            <img src="/images/nav_3_1.png" width="130px;" height="50px" id="Img1" />
                        </div>
                    </div>
                    <img src="/images/nav_3.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
            <li id="Li1">
                <a href="#top">
                    <div class="hides" style="width: 130px; display: none" id="Div1">
                        <div class="hides" id="p4">
                            <img src="/images/nav_4_1.png" width="130px;" height="50px" />
                        </div>
                    </div>
                    <img src="/images/nav_4.png" width="57px" height="50px" class="shows" />
                </a>
            </li>
        </ul>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#leftsead a").hover(function () {

                $(this).children("div.hides").show();
                $(this).children("img.shows").hide();
                $(this).children("div.hides").animate({ marginRight: '0px' }, '0');

            }, function () {
                $(this).children("div.hides").animate({ marginRight: '-130px' }, 0, function () { $(this).hide(); $(this).next("img.shows").show(); });
            });
            $("#top_btn").click(function () { if (scroll == "off") return; $("html,body").animate({ scrollTop: 0 }, 600); });
        });
    </script>
    <%--右侧浮动弹框 结束--%>

    <ucbottom:bottom runat="server" ID="Bottom" />

</body>
</html>
