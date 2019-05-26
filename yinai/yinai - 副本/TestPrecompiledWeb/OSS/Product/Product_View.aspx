<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private Product myApp;
    private ITools tools;
    private ProductAuditReason myreason;
    private Supplier mysupplier;

    private string Product_Code, U_Product_Parameters, Product_Name, Product_NameInitials, Product_Audit_Note, commission_info, Product_SubName, Product_SubNameInitials, Product_PriceUnit, Product_Unit, Product_Note, Product_Img, Product_Publisher, Product_Intro, Product_Site, Product_SEO_Title, Product_SEO_Keyword, Product_SEO_Description, Product_Spec, Product_Maker, Product_Cate, Supplier_Name, U_Product_DeliveryCycle, Product_LibraryImg;
    private int Product_ID, Product_SupplierID, Product_CateID, Product_BrandID, Product_Supplier_CommissionCateID, Product_GroupNum, Product_StockAmount, Product_SaleAmount, Product_Review_Count, Product_Review_ValidCount, Product_IsInsale, Product_IsGroupBuy, Product_IsCoinBuy, Product_IsFavor, Product_IsGift, Product_IsAudit, Product_CoinBuy_Coin, Product_AlertAmount, Product_IsNoStock, Product_TypeID, Product_Sort, Product_QuotaAmount, Product_Region, Product_IsGiftCoin, U_Product_MinBook, Product_PriceType;
    private DateTime Product_Addtime;
    private double Product_MKTPrice, Product_GroupPrice, Product_PurchasingPrice, Product_Price, Product_Weight, Product_Review_Average, Product_Gift_Coin, Product_ManualFee;
    private bool grade_price = false;

    private string product_img, product_img_ext_1, product_img_ext_2, product_img_ext_3, product_img_ext_4;
    string Product_Keyword = "";
    string Product_Keyword1 = "";
    string Product_Keyword2 = "";
    string Product_Keyword3 = "";
    string Product_Keyword4 = "";
    string Product_Keyword5 = "";
    string Brand_Name, Type_Name, Cate_Name;
    string Product_State_Name = "";
    string Product_City_Name = "";
    string Product_County_Name = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        tools = ToolsFactory.CreateTools();
        // Response.Redirect(tools.NullStr(Application["Site_URL"]) + "product/preview.aspx?Product_ID=" + tools.CheckInt(Request.QueryString["Product_ID"]));

        Public.CheckLogin("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8");
        myApp = new Product();
        myreason = new ProductAuditReason();
        mysupplier = new Supplier();
        Supplier_Name = "平台";
        Product_ID = tools.CheckInt(Request.QueryString["Product_ID"]);

        ProductInfo entity = myApp.GetProductByID(Product_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Product_ID = entity.Product_ID;
            Product_Code = entity.Product_Code;
            Product_TypeID = entity.Product_TypeID;
            Product_CateID = entity.Product_CateID;
            Product_Cate = myApp.GetProductCategory(Product_ID);
            Product_SupplierID = entity.Product_SupplierID;
            Product_Supplier_CommissionCateID = entity.Product_Supplier_CommissionCateID;
            Product_BrandID = entity.Product_BrandID;
            Product_Name = entity.Product_Name;
            Product_Audit_Note = entity.Product_Audit_Note;
            Product_SubName = entity.Product_SubName;
            Product_PriceType = entity.Product_PriceType;
            Product_MKTPrice = entity.Product_MKTPrice;
            Product_GroupPrice = entity.Product_GroupPrice;
            Product_PurchasingPrice = entity.Product_PurchasingPrice;
            Product_Price = entity.Product_Price;
            Product_PriceUnit = entity.Product_PriceUnit;
            Product_Unit = entity.Product_Unit;
            Product_GroupNum = entity.Product_GroupNum;
            Product_Note = entity.Product_Note;
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
            U_Product_Parameters = entity.U_Product_Parameters;
            Product_SEO_Title = entity.Product_SEO_Title;
            Product_SEO_Keyword = entity.Product_SEO_Keyword;
            Product_SEO_Description = entity.Product_SEO_Description;

            U_Product_DeliveryCycle = entity.U_Product_DeliveryCycle;
            U_Product_MinBook = entity.U_Product_MinBook;
            Product_LibraryImg = entity.Product_LibraryImg;
            Product_ManualFee = entity.Product_ManualFee;
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

            IList<ProductPriceInfo> prices = myApp.GetProductPrices(Product_ID);
            if (prices != null)
            {
                grade_price = true;
            }

            BrandInfo entityBrand = new Brand().GetBrandByID(Product_BrandID);
            if (entityBrand != null) { Brand_Name = entityBrand.Brand_Name; }
            else { Brand_Name = "--"; }
            entityBrand = null;

            ProductTypeInfo entityType = new ProductType().GetProductTypeByID(Product_TypeID);
            if (entityType != null) { Type_Name = entityType.ProductType_Name; }
            else { Type_Name = "--"; }

            CategoryInfo entityCate = new Category().GetCategoryByID(Product_CateID);
            if (entityCate != null) { Cate_Name = entityCate.Cate_Name; }
            else { Cate_Name = "--"; }

            SupplierCommissionCategoryInfo commissioncate = mysupplier.GetSupplierCommissionCategoryByID(Product_Supplier_CommissionCateID);
            if (commissioncate != null)
            {
                commission_info = commissioncate.Supplier_Commission_Cate_Name + "(" + commissioncate.Supplier_Commission_Cate_Amount + "%)";
            }
            else
            {
                commission_info = "--";
            }

            if (Product_SupplierID > 0)
            {
                SupplierInfo supplier = mysupplier.GetSupplierByID(Product_SupplierID);
                if (supplier != null)
                {
                    Supplier_Name = supplier.Supplier_CompanyName;
                }
            }

        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>商品查看</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <script src="/Scripts/product.js" type="text/javascript"></script>

    <script type="text/javascript">
        function openUpload(openObj) {
            $("#td_upload").show();
            $("#iframe_upload")[0].src = '<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=product&formname=formadd&frmelement=' + openObj + '&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>';
    }

    function delImage(openObj) {
        $("#img_" + openObj)[0].src = "/images/detail_no_pic.gif";
        $("#" + openObj)[0].value = "/images/detail_no_pic.gif";
    }
    </script>
    <style type="text/css">
        .extendAttribute input {
            width: 300px;
        }
    </style>
</head>
<body>

    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">查看商品</td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="/product/product_do.aspx">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
                            <tr>
                                <td class="cell_title">商品类型</td>
                                <td class="cell_content"><% =Type_Name%></td>
                            </tr>
                            <tr>
                                <td class="cell_title">商品主类</td>
                                <td class="cell_content"><% =Cate_Name%></td>
                            </tr>
                            <tr>
                                <td class="cell_title">供应商</td>
                                <td class="cell_content"><% =Supplier_Name%></td>
                            </tr>
                              <tr>
                                <td class="cell_title">产品产地</td>
                                <td class="cell_content"><%=new Addr().DisplayProductAddress(Product_State_Name.ToString(),Product_City_Name.ToString(),Product_County_Name.ToString())%></td>
                            </tr>
                            <tr>
                                <td class="cell_title">附加分类</td>
                                <td class="cell_content"><% =myApp.CategoryDisplay(Product_Cate)%></td>
                            </tr>
                            <tr>
                                <td class="cell_title">扩展属性</td>
                                <td class="cell_content extendAttribute"><% =myApp.ProductExtendDisplay(Product_TypeID, Product_ID)%></td>
                            </tr>
                            <%--<tr>
                                <td class="cell_title">商品品牌</td>
                                <td class="cell_content"><% = Brand_Name%></td>
                            </tr>--%>
                            <tr style="display:none;">
                                <td class="cell_title">商品编码</td>
                                <td class="cell_content"><% =Product_Code%></td>
                            </tr>
                            <tr>
                                <td class="cell_title">商品名称</td>
                                <td class="cell_content"><% =Product_Name%></td>
                            </tr>
                            <tr>
                                <td class="cell_title">材质型号</td>
                                <td class="cell_content"><% =Product_Spec%></td>
                            </tr>
                          <%--  <tr>
                                <td class="cell_title">计价方式</td>
                                <td class="cell_content"><% =Product_PriceType == 1 ? "定价制" : "计价制"%></td>
                            </tr>--%>

                            <tr>
                                <td class="cell_title">商品价格</td>
                                <%if (Product_PriceType == 1)
                                  { %>
                                    <td class="cell_content"><% =Public.DisplayCurrency(Product_Price)%></td>
                                <%}
                                  else
                                  { %>
                                    <td class="cell_content"><% =Public.GetProductPrice(Product_ManualFee,Product_Weight)%></td>
                                <%} %>
                            </tr>
                             <tr>
                                <td class="cell_title">规格型号</td>
                                <td class="cell_content"><% =Product_Spec%></td>
                            </tr>
                            <tr>
                                <td class="cell_title">商品备注</td>
                                <td class="cell_content"><% =Product_Note%></td>
                            </tr>
                             <tr>
                                <td class="cell_title">商品交货周期</td>
                                <td class="cell_content"><% =U_Product_DeliveryCycle%>  (单位:天)</td>
                            </tr>
                            <tr>
                                <td class="cell_title">商品重量</td>
                                <td class="cell_content"><% =Product_Weight%></td>
                            </tr>
                            <tr>
                                <td class="cell_title">商品单位</td>
                                <td class="cell_content"><% =Product_Unit%></td>
                            </tr>
                            <tr>
                                <td class="cell_title">商品上架</td>
                                <td class="cell_content"><%if (Product_IsInsale == 0) { Response.Write("下架"); } else { Response.Write("上架"); } %></td>
                            </tr>

                            <tr>
                                <td class="cell_title">审核状态</td>
                                <td class="cell_content"><%if (Product_IsAudit == 0) { Response.Write("未审核"); } else if (Product_IsAudit == 2) { Response.Write("未通过审核"); } else { Response.Write("已审核"); } %></td>
                            </tr>
                            <%if (Product_IsAudit == 2)
                              { %>
                            <tr>
                                <td class="cell_title">原因</td>
                                <td class="cell_content"><% =myreason.GetProductAuditReasonsByProductID(Product_ID)%></td>
                            </tr>
                            <tr>
                                <td class="cell_title">审核备注</td>
                                <td class="cell_content"><% =Product_Audit_Note%></td>
                            </tr>
                            <%} %>
                            <tr>
                                <td class="cell_title">库存</td>
                                <td class="cell_content"><% =Product_StockAmount%></td>
                            </tr>
                            <tr>
                                <td class="cell_title">最小起订量</td>
                                <td class="cell_content"><% =U_Product_MinBook%></td>
                            </tr>
                            <tr>
                                <td class="cell_title">排序</td>
                                <td class="cell_content"><% =Product_Sort%></td>
                            </tr>
                            <tr>
                                <td class="cell_title" valign="top">商品介绍</td>
                                <td class="cell_content"><% =Product_Intro%></td>
                            </tr>
                            <tr>
                                <td class="cell_title" valign="top">规格参数</td>
                                <td class="cell_content"><% =U_Product_Parameters%></td>
                            </tr>
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
                                    </table>
                                </td>
                            </tr>

                        <%--    <tr>
                                <td class="cell_title">精品库图片</td>
                                <td class="cell_content"><img src="<%=Public.FormatImgURL(Product_LibraryImg,"fullpath") %>" /></td>
                            </tr>--%>

                            <tr>
                                <td class="cell_title">TITLE<br />
                                    (页面标题)</td>
                                <td class="cell_content"><%  =Product_SEO_Title%></td>
                            </tr>
                            <tr>
                                <td class="cell_title">META_KEYWORDS<br />
                                    (页面关键词)</td>
                                <td class="cell_content"><%  =Product_SEO_Keyword%></td>
                            </tr>
                            <tr>
                                <td class="cell_title">META_DESCRIPTION<br />
                                    (页面描述)</td>
                                <td class="cell_content"><%  =Product_SEO_Description%></td>
                            </tr>
                            <tr>
                                <td class="cell_title">关键词</td>
                                <td class="cell_content"><%  =Product_Keyword1%>&nbsp;&nbsp;<%  =Product_Keyword2%>&nbsp;&nbsp;<%  =Product_Keyword3%>&nbsp;&nbsp;<%  =Product_Keyword4%>&nbsp;&nbsp;<%  =Product_Keyword5%></td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="right">
                                    <input name="button" type="button" class="bt_orange" id="button" value="返回" onclick="history.go(-1)" /></td>
                            </tr>
                        </table>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
