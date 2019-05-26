<%@ Control Language="C#" ClassName="Left" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%
    ITools tools = ToolsFactory.CreateTools();
    Shop shop = new Shop();
    Public_Class pub = new Public_Class();
    Product product = new Product();
    shop.Shop_Initial();
    string keyword = tools.CheckStr(Request["keyword"]);
    double minprice = tools.CheckFloat(Request["minprice"]);
    double maxprice = tools.CheckFloat(Request["maxprice"]);
    int cate_id = tools.CheckInt(Request["cate_id"]);
    string index_content = "";
    string index_top = "";
    string index_left = "";
    string index_right = "";
    string Shop_Addtime="";
    int SettledYear=0;
    string Shop_Name = "";
    string Supplier_Address = "";
   Glaer.Trade.B2C.BLL.MEM.ISupplier supplier=Glaer.Trade.B2C.BLL.MEM.SupplierFactory.CreateSupplier();
    SupplierShopPagesInfo pageinfo = shop.GetSupplierShopPagesByIDSign("Index", tools.NullInt(Session["shop_supplier_id"]));
    if (pageinfo != null)
    {
        index_content = pageinfo.Shop_Pages_Content;
    }
    pageinfo = shop.GetSupplierShopPagesByIDSign("IndexTOP", tools.NullInt(Session["shop_supplier_id"]));
    if (pageinfo != null)
    {
        index_top = pageinfo.Shop_Pages_Content;
    }

    pageinfo = shop.GetSupplierShopPagesByIDSign("INDEXLEFT", tools.NullInt(Session["shop_supplier_id"]));
    if (pageinfo != null)
    {
        index_left = pageinfo.Shop_Pages_Content;
        if (index_left.Length > 0)
        {

        }
        else
        {


            index_left = "<img src=\"http://img.easynai.com/shop/banner/default/img19.jpg\" />";

        }
    }
  
   
    
    keyword = keyword.Replace("\"", "&quot;");

    SupplierShopInfo ShopEntity = Glaer.Trade.B2C.BLL.MEM.SupplierShopFactory.CreateSupplierShop().GetSupplierShopBySupplierID(tools.NullInt(Session["shop_supplier_id"]));
    if (ShopEntity!=null)
	{
		Shop_Addtime= ShopEntity.Shop_Addtime.ToString("yyyy年MM月dd日");
        SettledYear = new Public_Class().DateDiffYear(ShopEntity.Shop_Addtime, DateTime.Now);
     Shop_Name = shop.Shop_Name;
     SupplierInfo supplierinfo = supplier.GetSupplierByID(ShopEntity.Shop_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));

     if (supplierinfo!=null)
	{
    Supplier_Address=    supplierinfo.Supplier_Address;
	}
	}
%>

<%
    
    if (shop.Shop_Info_IsActive == 1)
    {
        Response.Write("<!--本类推荐 开始-->");
        Response.Write("<div class=\"blk28\" >");
        Response.Write("	<h2 style=\"font-size:14px;\">商家信息</h2>");
        Response.Write("	<div class=\"b28_main03\">");

        Response.Write("<dl>");
        if (shop.shop_img.Length>0)
        {
            Response.Write("	<dt><a href=\"/\"><div class=\"images-wrapper_new\"> <img src=\"" + pub.FormatImgURL(shop.shop_img, "fullpath") + "\"  /></div></a></dt>"); 
        }
        else
        {
            //Response.Write("	<dt><a href=\"/\"><div class=\"images-wrapper_new\"> <img src=\"" + pub.FormatImgURL(shop.shop_img, "fullpath") + "\" width=\"170\" /><div class=\"images-content_new\"> " + Shop_Name + "</div></div></a></dt>"); 
            Response.Write("	<dt><a href=\"/\"><div class=\"images-wrapper_new\"><img src=\"http://img.easynai.com/shop/banner/default/暂无图片.png\" width=\"170\"/><div class=\"images-content_new\"> " + Shop_Name + "</div></div></a></dt>"); 
        }    
               
        Response.Write("	<dd>");
        Response.Write("		<p>" + Shop_Name + "<span onclick=\"NTKF.im_openInPageChat('sz_" + (tools.NullInt(Session["shop_supplier_id"]) + 1000) + "_9999');\"></span></p>");
        Response.Write("		<p>入驻时间：" + Shop_Addtime + "</p>");
        Response.Write("		<p>公司地址：" + Supplier_Address + "</p>");
        Response.Write("		<p>入驻第<strong>" +SettledYear + "</strong>年</p>");       
        Response.Write("		<p>信用评价：");       
        shop.shop_evaluate_info_AVG();
      Response.Write("      </p>"); 
        Response.Write("	</dd>"); 
        Response.Write("</dl>");
        
        

        Response.Write("	</div></div><!--本类推荐 结束-->");
    } 
    
%>




<% 
    if (shop.Shop_LeftSearch_IsActive == 1)
    {
        Response.Write("<!--店内搜索 开始-->");
        Response.Write("<div class=\"blk28\" style=\" margin-top:15px;\">");
        Response.Write("	<h2 style=\"font-size:14px;\">店内搜索</h2>");
        Response.Write("    <form action=\"/category.aspx\" method=\"post\" id=\"form_product\">");
        Response.Write("	<div class=\"b28_main04\">");
        Response.Write("		<ul>");
        Response.Write("			<li><span>关键字：</span><label><input name=\"keyword\" value=\"" + keyword + "\" type=\"text\" style=\"width:126px; padding:0px 3px;\"/></label></li>");
        Response.Write("			<li><span>价&nbsp;&nbsp;&nbsp;格：</span><label><input value=\"" + minprice + "\" name=\"minprice\" type=\"text\" style=\"width:50px;padding:0px 3px;\"/> 到 <input value=\"" + maxprice + "\" name=\"maxprice\" type=\"text\" style=\"width:50px;padding:0px 3px;\"/></label></li>");
        Response.Write("			<li><a href=\"javascript:void(0);\" onclick=\"$('#form_product').submit();\">搜 索</a></li>");
        Response.Write("		</ul>");
        Response.Write("	</div>");
        Response.Write("    </form>");
        Response.Write("</div>");
        Response.Write("<!--店内搜索 结束-->");
    }

    if (shop.Shop_LeftCate_IsActive == 1)
    {
        Response.Write("<!--商品分类 开始-->");
        Response.Write("<div class=\"blk28\" style=\"margin-top: 15px;\">");
        Response.Write("    <h2 style=\"font-size: 14px;\">商品分类</h2>");
        Response.Write("    <div class=\"b28_main05\">");
        Response.Write("        <div class=\"blk28_info\">");
        shop.Shop_Left_Menu(cate_id);
        Response.Write("        </div>");
        Response.Write("    </div>");
        Response.Write("</div>");
        Response.Write("<!--商品分类 结束-->");
    }
    
%>

<!--店铺销量排行 开始-->
<%if (shop.Shop_LeftSale_IsActive == 1)
  {%>
<div class="blk28" style="margin-top: 15px;">
 
     <h2 style="font-size: 14px;">热销排行</h2>
                    <div class="b07_main_yn">
                        <ul>
                           <%=product.Shop_LeftSale_Product(5)%>
                        </ul>
                    </div>
                </div>
<%} %>
<!--店铺销量排行 结束-->


<%if (shop.Shop_Left_IsActive == 1)
  {
      Response.Write("<div class=\"index_left_img\" style=\"margin-top:15px;\">");
      Response.Write(index_left);
      Response.Write("</div>");
  }
%>
<%if (shop.Shop_RightProduct_IsActive == 1)
  {
      Response.Write(index_right);
  }
%>
