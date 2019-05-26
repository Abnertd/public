using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.BLL.Product;

/// <summary>
/// 店铺搜索
/// </summary>
public class Shop
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private ISupplierShop myshop;
    private IProduct Myproduct;

    Public_Class pub;
    Addr addr;
    Supplier supplier;
    PageURL pageurl;

	public Shop()
	{
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        myshop = SupplierShopFactory.CreateSupplierShop();
        Myproduct = ProductFactory.CreateProduct();

        pub = new Public_Class();
        addr = new Addr();
        supplier = new Supplier();
        pageurl = new PageURL();
	}

    /// <summary>
    /// 搜索店铺
    /// </summary>
    /// <returns></returns>
    public void SearchShops()
    {
        string page_url = "action=list";

        StringBuilder strHTML = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 6;
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (Query.CurrentPage <= 0) Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopInfo.Shop_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopInfo.Shop_Status", "=", "1"));

        string keyword = tools.CheckStr(Request["keyword"]);
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopInfo.Shop_Name", "like", keyword));
            page_url += "&keyword=" + Server.UrlEncode(keyword);
        }

        Query.OrderInfos.Add(new OrderInfo("SupplierShopInfo.Shop_ID", "ASC"));
        PageInfo pageinfo = myshop.GetPageInfo(Query);
        IList<SupplierShopInfo> entitys;

        strHTML.Append("<h2 class=\"title10\">共找到<strong>" + pageinfo.RecordCount + "</strong>条信息</h2>");

        if (pageinfo.RecordCount >= 1)
        {
            string targetURL = string.Empty;
            SupplierInfo supplierInfo;
            entitys = myshop.GetSupplierShops(Query);

            if (entitys != null)
            {
                foreach (SupplierShopInfo entity in entitys)
                {
                    #region 循环体

                    supplierInfo = supplier.GetSupplierByID(entity.Shop_SupplierID);
                    if (supplierInfo == null)
                        supplierInfo = new SupplierInfo();

                    targetURL = supplier.GetShopURL(entity.Shop_Domain);

                    strHTML.Append("<div class=\"blk61\">");
                    strHTML.Append("	<div class=\"b61_left\">");
                    strHTML.Append("			<h2><a href=\"" + targetURL + "\" target=\"_blank\">" + entity.Shop_Name + "</a><span onclick=\"NTKF.im_openInPageChat('sz_" + (entity.Shop_SupplierID + 1000) + "_9999');\"><img style=\"display:inline-block; vertical-align:middle; margin-left:15px;width:74px;margin-right:10px;\" src=\"/images/icon04.png\"></span><span><a href=\"javascript:void(0);\">推广</a></span></h2>");
                    strHTML.Append("			<div class=\"b61_left_main\">");
                    strHTML.Append("				<ul>");
                    strHTML.Append("					<li><span>主营产品：</span>" + entity.Shop_MainProduct + "</li>");
                    strHTML.Append("					<li><span>经营年限：</span>" + supplierInfo.Supplier_OperateYear + "</li>");
                    strHTML.Append("					<li><span>所在地：</span>" + addr.DisplayAddress(supplierInfo.Supplier_State, supplierInfo.Supplier_City, supplierInfo.Supplier_County) + "</li>");
                    strHTML.Append("					<li><span>注册资金：</span>" + (supplierInfo.Supplier_RegisterFunds == 0 ? "无需验证" : supplierInfo.Supplier_RegisterFunds + "万") + "</li>");
                    strHTML.Append("				</ul>");
                    strHTML.Append("				<div class=\"clear\"></div>");

                    strHTML.Append("<p>");
                    IList<SupplierCertInfo> certs = supplier.GetSupplierCertByType(0);
                    if (certs != null)
                    {
                        int icert = 0;
                        foreach (SupplierCertInfo cert in certs)
                        {
                            if (supplier.Get_Supplier_Cert(cert.Supplier_Cert_ID, supplierInfo.SupplierRelateCertInfos).Length > 0)
                            {
                                icert++;

                                if (icert >= 2)
                                    strHTML.Append(" > ");

                                strHTML.Append(cert.Supplier_Cert_Name);
                            }
                        }
                    }
                    strHTML.Append("</p>");

                    strHTML.Append("			</div>");
                    strHTML.Append("	</div>");
                    strHTML.Append("	<div class=\"b61_right\">");
                    strHTML.Append("			<h2><a href=\"" + targetURL + "\" target=\"_blank\">进入本店铺 ></a></h2>");
                    strHTML.Append("			<div class=\"b61_right_main\">");
                    strHTML.Append("			<ul>");
                    strHTML.Append(ShopProducts(entity.Shop_SupplierID));
                    strHTML.Append("			</ul>");
                    strHTML.Append("			<div class=\"clear\"></div>");
                    strHTML.Append("			</div>");
                    strHTML.Append("	</div>");
                    strHTML.Append("	<div class=\"clear\"></div>");
                    strHTML.Append("	<em><img src=\"/images/div_bg.jpg\"></em>");
                    strHTML.Append("</div>");

                    #endregion
                }
            }
        }

        Response.Write(strHTML.ToString());

        page_url = Request.Path + "?" + page_url.TrimStart('&');
        pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, page_url, pageinfo.PageSize, pageinfo.RecordCount);
    }

    /// <summary>
    /// 搜索店铺下商品
    /// </summary>
    /// <param name="supplier_id"></param>
    /// <returns></returns>
    public string ShopProducts(int supplier_id)
    {
        StringBuilder strHTML = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 3;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_SupplierID", "=", supplier_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "DESC"));
        IList<ProductInfo> entitys = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));

        if (entitys != null)
        {
            string targetURL = string.Empty;
            foreach (ProductInfo entity in entitys)
            {
                targetURL = pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString());

                strHTML.Append("<li><div class=\"img_box\"><a href=\"" + targetURL + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\" alt=\"" + entity.Product_Name + "\" /></a><i><a href=\"" + targetURL + "\" target=\"_blank\">" + entity.Product_Name + "</a></i></div><p><span>成交 " + entity.Product_SaleAmount + "个</span><strong>" + (entity.Product_PriceType == 1 ? pub.FormatCurrency(entity.Product_Price) : pub.FormatCurrency(pub.GetProductPrice(entity.Product_ManualFee, entity.Product_Weight))) + "</strong></p></li>");
            }
        }
        entitys = null;

        return strHTML.ToString();
    }



}