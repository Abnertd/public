using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.SAL;
using Glaer.Trade.B2C.BLL.Product;
using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.BLL.ORD;

/// <summary>
///Promotion 的摘要说明
/// </summary>
public class Promotion
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IProduct MyBLLPRO;
    private IBrand MyBrand;
    private IPromotionFavorFee MyBLL;
    private IPromotion Mypromotion;
    private IPromotionLimit MyLimit;
    private IMember Mymember;
    private IMemberGrade MyGrade;
    private IPromotionFavorCoupon MyCoupon;
    private Addr addr;
    private Product product;
    private ICategory MyCate;
    private IPromotionFavorPolicy MyPolicy;
    private IPromotionFavorGift MyGift;
    private IDeliveryWay MyDelivery;
    private IPayWay MyPayway;
    private IProduct MyProduct;
    private IPromotionFavor MyFavor;
    private IPromotionWholeSale MyWholeSale;
    private IPromotionCouponRule MyCouponRule;
    private Supplier supplier;
    public Promotion()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = PromotionFavorFeeFactory.CreatePromotionFavorFee();
        MyBLLPRO = ProductFactory.CreateProduct();
        Mypromotion = PromotionFactory.CreatePromotion();
        MyLimit = PromotionLimitFactory.CreatePromotionLimit();
        MyBrand = BrandFactory.CreateBrand();
        Mymember = MemberFactory.CreateMember();
        MyGrade = MemberGradeFactory.CreateMemberGrade();
        MyCoupon = PromotionFavorCouponFactory.CreatePromotionFavorCoupon();
        MyCate = CategoryFactory.CreateCategory();
        MyPolicy = PromotionFavorPolicyFactory.CreatePromotionFavorPolicy();
        MyGift = PromotionFavorGiftFactory.CreatePromotionFavorGift();
        MyDelivery = DeliveryWayFactory.CreateDeliveryWay();
        MyPayway = PayWayFactory.CreatePayWay();
        MyProduct = ProductFactory.CreateProduct();
        MyFavor = PromotionFavorFactory.CreatePromotionFavor();
        MyWholeSale = PromotionWholeSaleFactory.CreatePromotionWholeSale();
        MyCouponRule = PromotionCouponRuleFactory.CreatePromotionFavorCoupon();
        addr = new Addr();
        product = new Product();
        supplier = new Supplier();

    }

    #region "辅助函数"

        public string State_Select(string select_name, string select_value)
        {
            string return_str = "";
            return_str = return_str + "<select name=\"" + select_name + "\" id=\"" + select_name + "\" multiple=\"multiple\" style=\"height:150px;\">";
            return_str = return_str + "<option value=\"0\">----选择省----</option>";
            IList<StateInfo> entitys = addr.Get_States();
            if (entitys != null)
            {
                foreach (StateInfo entity in entitys)
                {
                    if (select_value == entity.State_Code)
                    {
                        return_str = return_str + "<option value=\"" + entity.State_Code + "\" selected>" + entity.State_CN + "</option>";
                    }
                    else
                    {
                        return_str = return_str + "<option value=\"" + entity.State_Code + "\">" + entity.State_CN + "</option>";
                    }

                }
            }
            return_str = return_str + "</select>";
            return return_str;

        }

        public string State_Name_String(string State_ID)
        {
            string return_value = "";
            if (State_ID == "0")
            {
                return_value = "所有省份";
            }
            else
            {
                StateInfo state;
                foreach (string substr in State_ID.Split(','))
                {
                    state = addr.GetStateInfoByID(substr);
                    if (state != null)
                    {
                        return_value = return_value + state.State_CN + ",";
                    }
                }
            }
            return return_value;

        }

        public string Delivery_Name_String(string Delivery_ID)
        {
            string return_value = "";
            if (Delivery_ID == "0")
            {
                return_value = "所有配送方式";
            }
            else
            {
                DeliveryWayInfo delivery;
                foreach (string substr in Delivery_ID.Split(','))
                {
                    delivery = MyDelivery.GetDeliveryWayByID(tools.CheckInt(substr), Public.GetUserPrivilege());
                    if (delivery != null)
                    {
                        return_value = return_value + delivery.Delivery_Way_Name + ",";
                    }
                }
            }
            return return_value;

        }

        public string Payway_Name_String(string Payway_ID)
        {
            string return_value = "";
            if (Payway_ID == "0")
            {
                return_value = "所有支付方式";
            }
            else
            {
                PayWayInfo payway;
                foreach (string substr in Payway_ID.Split(','))
                {
                    payway = MyPayway.GetPayWayByID(tools.CheckInt(substr), Public.GetUserPrivilege());
                    if (payway != null)
                    {
                        return_value = return_value + payway.Pay_Way_Name + ",";
                    }
                }
            }
            return return_value;


        }

        public string Cate_Name_String(string Cate_ID)
        {
            if (Cate_ID == "0")
            {
                return "所有分类";
            }
            else
            {
                return product.CategoryDisplay(Cate_ID);
            }

        }

        public string Product_Name_String(string Product_ID)
        {
            if (Product_ID == "0")
            {
                return "所有产品";
            }
            else
            {
                return product.ProductDisplay(Product_ID);
            }

        }

        public string GetBrand_Name(string brandSelected)
        {
            string brand_name_arry = "";
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_Site", "=", Public.GetCurrentSite()));


            Query.ParamInfos.Add(new ParamInfo("AND", "int", "BrandInfo.Brand_IsActive", "=", "1"));

            if (brandSelected.Length > 0)
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_ID", "in", brandSelected));

            Query.OrderInfos.Add(new OrderInfo("BrandInfo.Brand_ID", "DESC"));

            IList<BrandInfo> entitys = MyBrand.GetBrands(Query, Public.GetUserPrivilege());

            if (entitys != null)
            {

                foreach (BrandInfo entity in entitys)
                {
                    brand_name_arry = brand_name_arry + entity.Brand_Name + " ";
                }


            }
            return brand_name_arry;
        }

        public string GetCateName(string cateSelected)
        {
            string cate_name_arry = "";
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", Public.GetCurrentSite()));


            Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));

            if (cateSelected.Length > 0)
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_ID", "in", cateSelected));

            Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_ID", "DESC"));

            IList<CategoryInfo> entitys = MyCate.GetCategorys(Query, Public.GetUserPrivilege());

            if (entitys != null)
            {

                foreach (CategoryInfo entity in entitys)
                {
                    cate_name_arry = cate_name_arry + entity.Cate_Name + " ";
                }


            }
            return cate_name_arry;
        }

        public string GetProductName(string productSelected)
        {
            string product_name_arry = "";
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));


            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));

            if (productSelected.Length > 0)
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", productSelected));

            Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "DESC"));

            IList<ProductInfo> entitys = MyBLLPRO.GetProductList(Query, Public.GetUserPrivilege());

            if (entitys != null)
            {

                foreach (ProductInfo entity in entitys)
                {
                    product_name_arry = product_name_arry + entity.Product_Name + ",";
                }
            }
            return product_name_arry;
        }

        public string GetMemberGrade_Name(string gradeSelected)
        {
            string grade_name_arry = "";
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberGradeInfo.Member_Grade_Site", "=", Public.GetCurrentSite()));

            if (gradeSelected.Length > 0)
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberGradeInfo.Member_Grade_ID", "in", gradeSelected));

            Query.OrderInfos.Add(new OrderInfo("MemberGradeInfo.Member_Grade_ID", "Asc"));

            IList<MemberGradeInfo> entitys = MyGrade.GetMemberGrades(Query, Public.GetUserPrivilege());

            if (entitys != null)
            {

                foreach (MemberGradeInfo entity in entitys)
                {
                    grade_name_arry = grade_name_arry + entity.Member_Grade_Name + " ";
                }


            }
            return grade_name_arry;
        }

        //展示已选择类别
        public string ShowCategory(string Cate_ID)
        {
            int del_cate = tools.CheckInt(Request["cid"]);
            string parentid = "";
            StringBuilder jsonBuilder = new StringBuilder();
            string cate_id = "";
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ID", "in", Cate_ID));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", Public.GetCurrentSite()));
            Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_ID", "Asc"));
            IList<CategoryInfo> entitys = MyCate.GetCategorys(Query,Public.GetUserPrivilege());
            if (entitys != null)
            {
                jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" width=\"600\" bgcolor=\"#B0CADA\">");
                jsonBuilder.Append("    <tr class=\"list_head_bg\">");
                jsonBuilder.Append("        <td colspan=\"2\" align=\"left\">已选择类别 <span id=\"cate_unfold\">[<a href=\"javascript:void(0);\" onclick=\"$('#cate_unfold').hide();$('#cate_fold').show();$('#cate_picker').attr('class','div_picker_unfold');\">展开</a>]</span><span id=\"cate_fold\" style=\"display:none;\">[<a href=\"javascript:void(0);\" onclick=\"$('#cate_unfold').show();$('#cate_fold').hide();$('#cate_picker').attr('class','div_picker');\">还原</a>]</span></td>");
                jsonBuilder.Append("    </tr>");
                foreach (CategoryInfo entity in entitys)
                {
                    if (entity != null)
                    {

                        if (del_cate != entity.Cate_ID)
                        {
                            parentid = product.Get_All_SubCate(entity.Cate_ID);

                            if (("," + Cate_ID + ",").IndexOf("," + parentid + ",") >= 0)
                            {
                                if (cate_id == "")
                                {
                                    cate_id = entity.Cate_ID.ToString();
                                }
                                else
                                {
                                    cate_id += "," + entity.Cate_ID.ToString();
                                }
                                jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                                jsonBuilder.Append("        <td width=\"550\" align=\"left\">" + MyCate.DisplayCategoryRecursion(entity.Cate_ID, "", Public.GetUserPrivilege()) + "&nbsp;&gt;&nbsp;全部商品</td>");
                                jsonBuilder.Append("        <td><a href=\"javascript:picker_cate_del('" + entity.Cate_ID + "');\"><img src=\"/images/btn_move.gif\" border=\"0\" alt=\"删除\"></a></td>");
                                jsonBuilder.Append("    </tr>");
                            }
                        }
                    }
                    
                }
                jsonBuilder.Append("</table>");
                if (cate_id == "")
                {
                    jsonBuilder = null;
                    jsonBuilder = new StringBuilder();
                    jsonBuilder.Append("<span class=\"pickertip\">已选择类别</span>");
                }
                else
                {
                    jsonBuilder.Append("<script>if($('#cate_picker').attr('class')=='div_picker_unfold'){$('#cate_unfold').hide();$('#cate_fold').show();}else{$('#cate_unfold').show();$('#cate_fold').hide();}</script>");
                }
            }
            else
            {
                jsonBuilder.Append("<span class=\"pickertip\">已选择类别</span>");
            }
            Session["selected_cateid"] = cate_id;
            jsonBuilder.Append("<script>$('#favor_cateid').val('" + cate_id + "');</script>");
            entitys = null;

            return jsonBuilder.ToString();
        }
      
        //品牌选择器
        public string SelectBrand()
        {
            string keyword = tools.CheckStr(Request["keyword"]);
            string brand_id = "0";
            if (keyword != "输入品牌名称进行搜索" && keyword != null)
            {
                keyword = keyword;
            }
            else
            {
                keyword = "";
            }

            QueryInfo Query = new QueryInfo();
            Query.PageSize = tools.CheckInt(Request["rows"]); 
            if (tools.CheckInt(Request["page"]) == 0)
            {
                Query.CurrentPage = 1;
            }
            else
            {
                Query.CurrentPage = tools.CheckInt(Request["page"]);
            }
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_Site", "=", Public.GetCurrentSite()));

            string cate_id = tools.NullStr(Request["cateid"]);
            if (cate_id !="-1")
            {
                if (cate_id.Length>0)
                {
                    brand_id = MyBrand.Get_Cate_Brand(cate_id);
                }
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_ID", "in", brand_id));
            }

            if (keyword.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_Name", "like", keyword));
            }
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "BrandInfo.Brand_IsActive", "=", "1"));

            Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
            PageInfo pageinfo = MyBrand.GetPageInfo(Query, Public.GetUserPrivilege());
            IList<BrandInfo> entitys = MyBrand.GetBrands(Query, Public.GetUserPrivilege());

            if (entitys != null)
            {
                StringBuilder jsonBuilder = new StringBuilder();
                jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
                jsonBuilder.Append(":[");
                foreach (BrandInfo entity in entitys)
                {
                    jsonBuilder.Append("{\"id\":" + entity.Brand_ID + ",\"cell\":[");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(entity.Brand_ID);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(entity.Brand_Name));
                    jsonBuilder.Append("\",");



                    jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                    jsonBuilder.Append("]},");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]");
                jsonBuilder.Append("}");

                entitys = null;
                return jsonBuilder.ToString();
            }
            else { return null; }
        }

        //获取全部品牌编号
        public string Get_BrandList_IDs()
        {
            string brand_arry="";
            string keyword = tools.CheckStr(Request["keyword"]);
            string brand_id = "0";
            if (keyword != "输入品牌名称进行搜索" && keyword != null)
            {
                keyword = keyword;
            }
            else
            {
                keyword = "";
            }
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0 ;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_Site", "=", Public.GetCurrentSite()));
            string cate_id = tools.NullStr(Request["cateid"]);
            if (cate_id !="-1")
            {
                if (cate_id.Length>0)
                {
                    brand_id = MyBrand.Get_Cate_Brand(cate_id);
                }
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_ID", "in", brand_id));
            }
            if (keyword.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_Name", "like", keyword));
            }
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "BrandInfo.Brand_IsActive", "=", "1"));
            Query.OrderInfos.Add(new OrderInfo("BrandInfo.Brand_ID", "Desc"));
            IList<BrandInfo> entitys = MyBrand.GetBrands(Query, Public.GetUserPrivilege());

            if (entitys != null)
            {
                foreach (BrandInfo entity in entitys)
                {
                    if (brand_arry.Length > 0)
                    {
                        brand_arry = brand_arry + "," + entity.Brand_ID.ToString();
                    }
                    else
                    {
                        brand_arry = entity.Brand_ID.ToString();
                    }
                }
            }
            return brand_arry;
        }

        //展示已选择品牌
        public string ShowBrand(string Brand_ID)
        {
            int del_brand = tools.CheckInt(Request["bid"]);
            StringBuilder jsonBuilder = new StringBuilder();
            string brand_id = "";
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            //Brand_ID = tools.NullStr(Request["bid"]);
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "BrandInfo.Brand_ID", "in", Brand_ID));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "BrandInfo.Brand_IsActive", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "BrandInfo.Brand_Site", "=", Public.GetCurrentSite()));
            Query.OrderInfos.Add(new OrderInfo("BrandInfo.Brand_ID", "Desc"));
            IList<BrandInfo> entitys = MyBrand.GetBrands(Query, Public.GetUserPrivilege());
            if (entitys != null)
            {
                jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" width=\"600\" bgcolor=\"#B0CADA\">");
                jsonBuilder.Append("    <tr class=\"list_head_bg\">");
                jsonBuilder.Append("        <td colspan=\"2\" align=\"left\">已选择品牌 <span id=\"brand_unfold\">[<a href=\"javascript:void(0);\" onclick=\"$('#brand_unfold').hide();$('#brand_fold').show();$('#brand_picker').attr('class','div_picker_unfold');\">展开</a>]</span><span id=\"brand_fold\" style=\"display:none;\">[<a href=\"javascript:void(0);\" onclick=\"$('#brand_unfold').show();$('#brand_fold').hide();$('#brand_picker').attr('class','div_picker');\">还原</a>]</span></td>");
                jsonBuilder.Append("    </tr>");
                foreach (BrandInfo entity in entitys)
                {
                    if (entity != null)
                    {

                        if (del_brand != entity.Brand_ID)
                        {

                            if (brand_id == "")
                            {
                                brand_id = entity.Brand_ID.ToString();
                            }
                            else
                            {
                                brand_id += "," + entity.Brand_ID.ToString();
                            }
                            jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                            jsonBuilder.Append("        <td width=\"550\" align=\"left\">" + tools.NullStr(entity.Brand_Name) + "</td>");
                            jsonBuilder.Append("        <td><a href=\"javascript:picker_brand_del('" + entity.Brand_ID + "');\"><img src=\"/images/btn_move.gif\" border=\"0\" alt=\"删除\"></a></td>");
                            jsonBuilder.Append("    </tr>");
                        }
                    }

                }
                jsonBuilder.Append("</table>");
                if (brand_id == "")
                {
                    jsonBuilder = null;
                    jsonBuilder = new StringBuilder();
                    jsonBuilder.Append("<span class=\"pickertip\">已选择品牌</span>");
                }
                else
                {
                    jsonBuilder.Append("<script>if($('#brand_picker').attr('class')=='div_picker_unfold'){$('#brand_unfold').hide();$('#brand_fold').show();}else{$('#brand_unfold').show();$('#brand_fold').hide();}</script>");
                }
            }
            else
            {
                jsonBuilder.Append("<span class=\"pickertip\">已选择品牌</span>");
            }
            Session["selected_brandid"] = brand_id;
            jsonBuilder.Append("<script>$('#favor_brandid').val('" + brand_id + "');</script>");
            entitys = null;

            return jsonBuilder.ToString();
        }

        //产品选择
        public string SelectProduct()
        {
            string tag = tools.CheckStr(Request["tag"]);
            string keyword = tools.CheckStr(Request["keyword"]);
            string target = tools.NullStr(Request["target"]);
            string product_id = "0";
            if (keyword != "输入商品编码、商品名称进行搜索" && keyword != null)
            {
                keyword = keyword;
            }
            else
            {
                keyword = "";
            }
            QueryInfo Query = new QueryInfo();
            Query.PageSize = tools.CheckInt(Request["rows"]);
            if (tools.CheckInt(Request["page"]) == 0)
            {
                Query.CurrentPage = 1;
            }
            else
            {
                Query.CurrentPage = tools.CheckInt(Request["page"]);
            }
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
            //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsListShow", "=", "1"));

            //if (tag == "")
            //{
            //    Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_SupplierID", "=", "0"));
            //}

            int cate_id = tools.CheckInt(Request["product_cate"]);
            if (cate_id > 0)
            {
                string subCates = product.Get_All_SubCate(cate_id);
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", subCates));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + subCates + ")"));
                //product_id = product.Get_All_CateProductID(subCates);
                //Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", product_id));
            }

            if (keyword.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_Name", "like", keyword));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_Code", "like", keyword));
            }
            if (target == "group")
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_GroupCode", "=", ""));
            }
            else
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_IsAudit", "=", "1"));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
            }

            Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
            PageInfo pageinfo = MyProduct.GetPageInfo(Query, Public.GetUserPrivilege());
            IList<ProductInfo> entitys = MyProduct.GetProductList(Query, Public.GetUserPrivilege());

            if (entitys != null)
            {
                StringBuilder jsonBuilder = new StringBuilder();
                jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
                jsonBuilder.Append(":[");
                foreach (ProductInfo entity in entitys)
                {
                    jsonBuilder.Append("{\"id\":" + entity.Product_ID + ",\"cell\":[");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(entity.Product_ID);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(entity.Product_Code);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(entity.Product_Name));
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(entity.Product_Maker));
                    jsonBuilder.Append("\",");

                    jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                    jsonBuilder.Append("]},");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]");
                jsonBuilder.Append("}");

                entitys = null;
                return jsonBuilder.ToString();
            }
            else { return null; }
        }

        //获取全部产品编号
        public string Get_ProductList_IDs()
        {
            string product_arry = "";
            string keyword = tools.CheckStr(Request["keyword"]);
            string target = tools.NullStr(Request["target"]);
            string product_id = "0";
            if (keyword != "输入商品编码、商品名称、通用名、生产企业、店铺名称、拼音首字母进行搜索" && keyword != null)
            {
                keyword = keyword;
            }
            else
            {
                keyword = "";
            }
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_SupplierID", "=", "0"));

            int cate_id = tools.CheckInt(Request["product_cate"]);
            if (cate_id > 0)
            {
                string subCates = product.Get_All_SubCate(cate_id);
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", subCates));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + subCates + ")"));
                //product_id = product.Get_All_CateProductID(subCates);
                //Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", product_id));
            }

            if (keyword.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_Name", "like", keyword));
                Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_Code", "like", keyword));
                Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_SubName", "like", keyword));
                Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_Maker", "like", keyword));
                Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_NameInitials", "like", keyword));
                Query.ParamInfos.Add(new ParamInfo("OR", "int", "ProductInfo.Product_SupplierID", "in", "select Shop_SupplierID from supplier_shop where Shop_Name like '%" + keyword + "%'"));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_SubNameInitials", "like", keyword));
            }
            if (target == "group")
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_GroupCode", "=", ""));
            }
            else
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_IsAudit", "=", "1"));
            }

            Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "Desc"));
            IList<ProductInfo> entitys = MyProduct.GetProductList(Query, Public.GetUserPrivilege());

            if (entitys != null)
            {
                foreach (ProductInfo entity in entitys)
                {
                    if (product_arry.Length > 0)
                    {
                        product_arry = product_arry + "," + entity.Product_ID.ToString();
                    }
                    else
                    {
                        product_arry = entity.Product_ID.ToString();
                    }
                }
            }
            return product_arry;
        }

        //展示已选择产品
        public string ShowProduct(string Product_ID)
        {
            int del_product = tools.CheckInt(Request["pid"]);
            string product_id = "";
            StringBuilder jsonBuilder = new StringBuilder();
            string target = tools.NullStr(Request["target"]);

            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
            //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsListShow", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", Product_ID));
            if (target != "group")
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_IsAudit", "=", "1"));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
            }
            Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "Desc"));
            IList<ProductInfo> entitys = MyProduct.GetProductList(Query, Public.GetUserPrivilege());
            if (entitys != null)
            {
                jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" width=\"700\" bgcolor=\"#B0CADA\">");
                jsonBuilder.Append("    <tr class=\"list_head_bg\">");
                jsonBuilder.Append("        <td colspan=\"5\" align=\"left\">已选择产品 <span id=\"product_unfold\">[<a href=\"javascript:void(0);\" onclick=\"$('#product_unfold').hide();$('#product_fold').show();$('#product_picker').attr('class','div_picker_unfold');\">展开</a>]</span><span id=\"product_fold\" style=\"display:none;\">[<a href=\"javascript:void(0);\" onclick=\"$('#product_unfold').show();$('#product_fold').hide();$('#product_picker').attr('class','div_picker');\">还原</a>]</span></td>");
                jsonBuilder.Append("    </tr>");
                foreach (ProductInfo entity in entitys)
                {
                    if (del_product != entity.Product_ID)
                    {

                        if (product_id == "")
                        {
                            product_id = entity.Product_ID.ToString();
                        }
                        else
                        {
                            product_id += "," + entity.Product_ID.ToString();
                        }
                        jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                        jsonBuilder.Append("        <td width=\"100\" align=\"left\">" + entity.Product_Code + "</td>");
                        jsonBuilder.Append("        <td width=\"350\" align=\"left\">" + entity.Product_Name + "</td>");
                        jsonBuilder.Append("        <td width=\"200\" align=\"left\">" + entity.Product_Maker + "</td>");
                        if (target == "group")
                        {
                            if (entity.Product_IsListShow == 0)
                            {
                                jsonBuilder.Append("        <td width=\"50\" align=\"center\"><input type=\"checkbox\" name=\"islistshow_" + entity.Product_ID + "\" value=\"1\"></td>");
                            }
                            else
                            {
                                jsonBuilder.Append("        <td width=\"50\" align=\"center\"><input type=\"checkbox\" name=\"islistshow_" + entity.Product_ID + "\" value=\"1\" checked></td>");
                            }
                        }
                        if (target == "group")
                        {
                            jsonBuilder.Append("        <td><a href=\"javascript:picker_product_delgroup('" + entity.Product_ID + "');\"><img src=\"/images/btn_move.gif\" border=\"0\" alt=\"删除\"></a></td>");
                        }
                        else
                        {
                            jsonBuilder.Append("        <td><a href=\"javascript:picker_product_del('" + entity.Product_ID + "');\"><img src=\"/images/btn_move.gif\" border=\"0\" alt=\"删除\"></a></td>");
                        }
                        jsonBuilder.Append("    </tr>");
                    }
                }
                jsonBuilder.Append("</table>");
                if (product_id == "")
                {
                    jsonBuilder = null;
                    jsonBuilder = new StringBuilder();
                    jsonBuilder.Append("<span class=\"pickertip\">已选择产品</span>");
                }
                else
                {
                    jsonBuilder.Append("<script>if($('#product_picker').attr('class')=='div_picker_unfold'){$('#product_unfold').hide();$('#product_fold').show();}else{$('#product_unfold').show();$('#product_fold').hide();}</script>");
                }
            }
            else
            {
                jsonBuilder.Append("<span class=\"pickertip\">已选择产品</span>");
            }
            Session["selected_productid"] = product_id;
            jsonBuilder.Append("<script>$('#favor_productid').val('" + product_id + "');</script>");
            entitys = null;

            return jsonBuilder.ToString();
        }

        //配送方式选择器
        public string SelectDelivery()
        {
            string keyword = tools.CheckStr(Request["keyword"]);
            string brand_id = "0";
            if (keyword != "输入配送方式名称进行搜索" && keyword != null)
            {
                keyword = keyword;
            }
            else
            {
                keyword = "";
            }
            QueryInfo Query = new QueryInfo();
            Query.PageSize = tools.CheckInt(Request["rows"]);
            if (tools.CheckInt(Request["page"]) == 0)
            {
                Query.CurrentPage = 1;
            }
            else
            {
                Query.CurrentPage = tools.CheckInt(Request["page"]);
            }
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "DeliveryWayInfo.Delivery_Way_Site", "=", Public.GetCurrentSite()));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "DeliveryWayInfo.Delivery_Way_Status", "=", "1"));
            if (keyword.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "DeliveryWayInfo.Delivery_Way_Name", "like", keyword));
            }
            Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
            PageInfo pageinfo =MyDelivery.GetPageInfo(Query, Public.GetUserPrivilege());
            IList<DeliveryWayInfo> entitys = MyDelivery.GetDeliveryWays(Query, Public.GetUserPrivilege());

            if (entitys != null)
            {
                StringBuilder jsonBuilder = new StringBuilder();
                jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
                jsonBuilder.Append(":[");
                foreach (DeliveryWayInfo entity in entitys)
                {
                    jsonBuilder.Append("{\"id\":" + entity.Delivery_Way_ID + ",\"cell\":[");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(entity.Delivery_Way_ID);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(entity.Delivery_Way_Name));
                    jsonBuilder.Append("\",");



                    jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                    jsonBuilder.Append("]},");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]");
                jsonBuilder.Append("}");

                entitys = null;
                return jsonBuilder.ToString();
            }
            else { return null; }
        }

        //获取全部配送方式编号
        public string Get_Delivery_IDs()
        {
            string keyword = tools.CheckStr(Request["keyword"]);
            string delivery_arry = "";
            if (keyword != "输入配送方式名称进行搜索" && keyword != null)
            {
                keyword = keyword;
            }
            else
            {
                keyword = "";
            }
            QueryInfo Query = new QueryInfo();
            Query.PageSize = tools.CheckInt(Request["rows"]);
            if (tools.CheckInt(Request["page"]) == 0)
            {
                Query.CurrentPage = 1;
            }
            else
            {
                Query.CurrentPage = tools.CheckInt(Request["page"]);
            }
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "DeliveryWayInfo.Delivery_Way_Site", "=", Public.GetCurrentSite()));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "DeliveryWayInfo.Delivery_Way_Status", "=", "1"));

            if (keyword.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "DeliveryWayInfo.Delivery_Way_Name", "like", keyword));
            }
            Query.OrderInfos.Add(new OrderInfo("DeliveryWayInfo.Delivery_Way_ID", "Desc"));
            IList<DeliveryWayInfo> entitys = MyDelivery.GetDeliveryWays(Query, Public.GetUserPrivilege());

            if (entitys != null)
            {
                foreach (DeliveryWayInfo entity in entitys)
                {
                    if (delivery_arry.Length > 0)
                    {
                        delivery_arry = delivery_arry + "," + entity.Delivery_Way_ID.ToString();
                    }
                    else
                    {
                        delivery_arry = entity.Delivery_Way_ID.ToString();
                    }
                }
            }
            return delivery_arry;
        }

        //展示已选择配送方式
        public string ShowDelivery(string Delivery_ID)
        {
            int del_delivery = tools.CheckInt(Request["did"]);
            string delivery_id = "";
            StringBuilder jsonBuilder = new StringBuilder();


            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "DeliveryWayInfo.Delivery_Way_Site", "=", Public.GetCurrentSite()));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "DeliveryWayInfo.Delivery_Way_Status", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "DeliveryWayInfo.Delivery_Way_ID", "in", Delivery_ID));
            Query.OrderInfos.Add(new OrderInfo("DeliveryWayInfo.Delivery_Way_ID", "Desc"));
            IList<DeliveryWayInfo> entitys = MyDelivery.GetDeliveryWays(Query, Public.GetUserPrivilege());
            if (entitys != null)
            {
                jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" width=\"600\" bgcolor=\"#B0CADA\">");
                jsonBuilder.Append("    <tr class=\"list_head_bg\">");
                jsonBuilder.Append("        <td colspan=\"4\" align=\"left\">已选择配送方式 <span id=\"delivery_unfold\">[<a href=\"javascript:void(0);\" onclick=\"$('#delivery_unfold').hide();$('#delivery_fold').show();$('#delivery_picker').attr('class','div_picker_unfold');\">展开</a>]</span><span id=\"delivery_fold\" style=\"display:none;\">[<a href=\"javascript:void(0);\" onclick=\"$('#delivery_unfold').show();$('#delivery_fold').hide();$('#delivery_picker').attr('class','div_picker');\">还原</a>]</span></td>");
                jsonBuilder.Append("    </tr>");
                foreach (DeliveryWayInfo entity in entitys)
                {
                    if (del_delivery != entity.Delivery_Way_ID)
                    {

                        if (delivery_id == "")
                        {
                            delivery_id = entity.Delivery_Way_ID.ToString();
                        }
                        else
                        {
                            delivery_id += "," + entity.Delivery_Way_ID.ToString();
                        }
                        jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                        jsonBuilder.Append("        <td width=\"550\" align=\"left\">" + tools.NullStr(entity.Delivery_Way_Name) + "</td>");
                        jsonBuilder.Append("        <td><a href=\"javascript:picker_delivery_del('" + entity.Delivery_Way_ID + "');\"><img src=\"/images/btn_move.gif\" border=\"0\" alt=\"删除\"></a></td>");
                        jsonBuilder.Append("    </tr>");
                    }
                }
                jsonBuilder.Append("</table>");
                if (delivery_id == "")
                {
                    jsonBuilder = null;
                    jsonBuilder = new StringBuilder();
                    jsonBuilder.Append("<span class=\"pickertip\">已选择配送方式</span>");
                }
                else
                {
                    jsonBuilder.Append("<script>if($('#delivery_picker').attr('class')=='div_picker_unfold'){$('#delivery_unfold').hide();$('#delivery_fold').show();}else{$('#delivery_unfold').show();$('#delivery_fold').hide();}</script>");
                }
            }
            else
            {
                jsonBuilder.Append("<span class=\"pickertip\">已选择配送方式</span>");
            }
            Session["selected_deliveryid"] = delivery_id;
            jsonBuilder.Append("<script>$('#favor_deliveryid').val('" + delivery_id + "');</script>");
            entitys = null;

            return jsonBuilder.ToString();
        }

        //支付方式选择器
        public string SelectPayway()
        {
            string keyword = tools.CheckStr(Request["keyword"]);
            string brand_id = "0";
            if (keyword != "输入支付方式名称进行搜索" && keyword != null)
            {
                keyword = keyword;
            }
            else
            {
                keyword = "";
            }
            QueryInfo Query = new QueryInfo();
            Query.PageSize = tools.CheckInt(Request["rows"]);
            if (tools.CheckInt(Request["page"]) == 0)
            {
                Query.CurrentPage = 1;
            }
            else
            {
                Query.CurrentPage = tools.CheckInt(Request["page"]);
            }
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_Site", "=", Public.GetCurrentSite()));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "PayWayInfo.Pay_Way_Status", "=", "1"));

            if (keyword.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_Name", "like", keyword));
            }
            Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
            PageInfo pageinfo = MyPayway.GetPageInfo(Query, Public.GetUserPrivilege());
            IList<PayWayInfo> entitys = MyPayway.GetPayWays(Query, Public.GetUserPrivilege());

            if (entitys != null)
            {
                StringBuilder jsonBuilder = new StringBuilder();
                jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
                jsonBuilder.Append(":[");
                foreach (PayWayInfo entity in entitys)
                {
                    jsonBuilder.Append("{\"id\":" + entity.Pay_Way_ID + ",\"cell\":[");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(entity.Pay_Way_ID);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(entity.Pay_Way_Name));
                    jsonBuilder.Append("\",");



                    jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                    jsonBuilder.Append("]},");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]");
                jsonBuilder.Append("}");

                entitys = null;
                return jsonBuilder.ToString();
            }
            else { return null; }
        }

        //获取全部支付方式编号
        public string Get_Payway_IDs()
        {
            string keyword = tools.CheckStr(Request["keyword"]);
            string payway_arry = "";
            if (keyword != "输入支付方式名称进行搜索" && keyword != null)
            {
                keyword = keyword;
            }
            else
            {
                keyword = "";
            }
            QueryInfo Query = new QueryInfo();
            Query.PageSize = tools.CheckInt(Request["rows"]);
            if (tools.CheckInt(Request["page"]) == 0)
            {
                Query.CurrentPage = 1;
            }
            else
            {
                Query.CurrentPage = tools.CheckInt(Request["page"]);
            }
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_Site", "=", Public.GetCurrentSite()));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "PayWayInfo.Pay_Way_Status", "=", "1"));

            if (keyword.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_Name", "like", keyword));
            }
            Query.OrderInfos.Add(new OrderInfo("PayWayInfo.Pay_Way_ID", "Desc"));
            IList<PayWayInfo> entitys = MyPayway.GetPayWays(Query, Public.GetUserPrivilege());

            if (entitys != null)
            {
                foreach (PayWayInfo entity in entitys)
                {
                    if (payway_arry.Length > 0)
                    {
                        payway_arry = payway_arry + "," + entity.Pay_Way_ID.ToString();
                    }
                    else
                    {
                        payway_arry = entity.Pay_Way_ID.ToString();
                    }
                }
            }
            return payway_arry;
        }

        //展示已选择支付方式
        public string ShowPayway(string Payway_ID)
        {
            int del_payway = tools.CheckInt(Request["payid"]);
            string payway_id = "";
            StringBuilder jsonBuilder = new StringBuilder();


            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_Site", "=", Public.GetCurrentSite()));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "PayWayInfo.Pay_Way_Status", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PayWayInfo.Pay_Way_ID", "in", Payway_ID));
            Query.OrderInfos.Add(new OrderInfo("PayWayInfo.Pay_Way_ID", "Desc"));
            IList<PayWayInfo> entitys = MyPayway.GetPayWays(Query, Public.GetUserPrivilege());
            if (entitys != null)
            {
                jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" width=\"600\" bgcolor=\"#B0CADA\">");
                jsonBuilder.Append("    <tr class=\"list_head_bg\">");
                jsonBuilder.Append("        <td colspan=\"4\" align=\"left\">已选择支付方式 <span id=\"payway_unfold\">[<a href=\"javascript:void(0);\" onclick=\"$('#payway_unfold').hide();$('#payway_fold').show();$('#payway_picker').attr('class','div_picker_unfold');\">展开</a>]</span><span id=\"payway_fold\" style=\"display:none;\">[<a href=\"javascript:void(0);\" onclick=\"$('#payway_unfold').show();$('#payway_fold').hide();$('#payway_picker').attr('class','div_picker');\">还原</a>]</span></td>");
                jsonBuilder.Append("    </tr>");
                foreach (PayWayInfo entity in entitys)
                {
                    if (del_payway != entity.Pay_Way_ID)
                    {

                        if (payway_id == "")
                        {
                            payway_id = entity.Pay_Way_ID.ToString();
                        }
                        else
                        {
                            payway_id += "," + entity.Pay_Way_ID.ToString();
                        }
                        jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                        jsonBuilder.Append("        <td width=\"550\" align=\"left\">" + tools.NullStr(entity.Pay_Way_Name) + "</td>");
                        jsonBuilder.Append("        <td><a href=\"javascript:picker_payway_del('" + entity.Pay_Way_ID + "');\"><img src=\"/images/btn_move.gif\" border=\"0\" alt=\"删除\"></a></td>");
                        jsonBuilder.Append("    </tr>");
                    }
                }
                jsonBuilder.Append("</table>");
                if (payway_id == "")
                {
                    jsonBuilder = null;
                    jsonBuilder = new StringBuilder();
                    jsonBuilder.Append("<span class=\"pickertip\">已选择支付方式</span>");
                }
                else
                {
                    jsonBuilder.Append("<script>if($('#payway_picker').attr('class')=='div_picker_unfold'){$('#payway_unfold').hide();$('#payway_fold').show();}else{$('#payway_unfold').show();$('#payway_fold').hide();}</script>");
                }
            }
            else
            {
                jsonBuilder.Append("<span class=\"pickertip\">已选择支付方式</span>");
            }
            Session["selected_paywayid"] = payway_id;
            jsonBuilder.Append("<script>$('#favor_paywayid').val('" + payway_id + "');</script>");
            entitys = null;

            return jsonBuilder.ToString();
        }

        //会员选择器
        public string SelectMember()
        {
            string keyword = tools.CheckStr(Request["keyword"]);
            string member_id = "0";
            if (keyword != "输入昵称、邮箱、姓名、电话、手机进行搜索" && keyword != null)
            {
                keyword = keyword;
            }
            else
            {
                keyword = "";
            }
            MemberGradeInfo membergrade = null;
            QueryInfo Query = new QueryInfo();
            Query.PageSize = tools.CheckInt(Request["rows"]);
            if (tools.CheckInt(Request["page"]) == 0)
            {
                Query.CurrentPage = 1;
            }
            else
            {
                Query.CurrentPage = tools.CheckInt(Request["page"]);
            }
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", Public.GetCurrentSite()));

            if (keyword.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "MemberInfo.Member_NickName", "like", keyword));
                Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberInfo.Member_Email", "like", keyword));
                Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberProfileInfo.Member_Name", "like", keyword));
                Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberProfileInfo.Member_Phone_Number", "like", keyword));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "MemberProfileInfo.Member_Mobile", "like", keyword));
            }
            Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
            PageInfo pageinfo = Mymember.GetPageInfo(Query, Public.GetUserPrivilege());
            IList<MemberInfo> entitys = Mymember.GetMembers(Query, Public.GetUserPrivilege());

            if (entitys != null)
            {
                StringBuilder jsonBuilder = new StringBuilder();
                jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
                jsonBuilder.Append(":[");
                foreach (MemberInfo entity in entitys)
                {
                    membergrade = MyGrade.GetMemberGradeByID(entity.Member_Grade,Public.GetUserPrivilege());
                    jsonBuilder.Append("{\"id\":" + entity.Member_ID + ",\"cell\":[");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(entity.Member_ID);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(entity.Member_NickName));
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(entity.Member_Email));
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(membergrade.Member_Grade_Name));
                    jsonBuilder.Append("\",");

                    jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                    jsonBuilder.Append("]},");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]");
                jsonBuilder.Append("}");

                entitys = null;
                return jsonBuilder.ToString();
            }
            else { return null; }
        }

        //获取全部会员编号
        public string Get_MemberList_IDs()
        {
            string member_arry = "";
            string keyword = tools.CheckStr(Request["keyword"]);
            string member_id = "0";
            if (keyword != "输入昵称、邮箱、姓名、电话、手机进行搜索" && keyword != null)
            {
                keyword = keyword;
            }
            else
            {
                keyword = "";
            }
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", Public.GetCurrentSite()));
            if (keyword.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "MemberInfo.Member_NickName", "like", keyword));
                Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberInfo.Member_Email", "like", keyword));
                Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberProfileInfo.Member_Name", "like", keyword));
                Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberProfileInfo.Member_Phone_Number", "like", keyword));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "MemberProfileInfo.Member_Mobile", "like", keyword));
            }
            Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Desc"));
            IList<MemberInfo> entitys = Mymember.GetMembers(Query, Public.GetUserPrivilege());

            if (entitys != null)
            {
                foreach (MemberInfo entity in entitys)
                {
                    if (member_arry.Length > 0)
                    {
                        member_arry = member_arry + "," + entity.Member_ID.ToString();
                    }
                    else
                    {
                        member_arry = entity.Member_ID.ToString();
                    }
                }
            }
            return member_arry;
        }

        //展示选择会员
        public string ShowMember(string Member_ID)
        {
            int del_member = tools.CheckInt(Request["mid"]);
            StringBuilder jsonBuilder = new StringBuilder();
            string member_id = "";
            MemberGradeInfo membergrade = null;
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            //member_ID = tools.NullStr(Request["bid"]);
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_ID", "in", Member_ID));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", Public.GetCurrentSite()));
            Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Desc"));
            IList<MemberInfo> entitys = Mymember.GetMembers(Query, Public.GetUserPrivilege());
            if (entitys != null)
            {
                jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" width=\"600\" bgcolor=\"#B0CADA\">");
                jsonBuilder.Append("    <tr class=\"list_head_bg\">");
                jsonBuilder.Append("        <td colspan=\"4\" align=\"left\">已选择会员 <span id=\"member_unfold\">[<a href=\"javascript:void(0);\" onclick=\"$('#member_unfold').hide();$('#member_fold').show();$('#member_picker').attr('class','div_picker_unfold');\">展开</a>]</span><span id=\"member_fold\" style=\"display:none;\">[<a href=\"javascript:void(0);\" onclick=\"$('#member_unfold').show();$('#member_fold').hide();$('#member_picker').attr('class','div_picker');\">还原</a>]</span></td>");
                jsonBuilder.Append("    </tr>");
                foreach (MemberInfo entity in entitys)
                {
                    if (entity != null)
                    {

                        if (del_member != entity.Member_ID)
                        {

                            if (member_id == "")
                            {
                                member_id = entity.Member_ID.ToString();
                            }
                            else
                            {
                                member_id += "," + entity.Member_ID.ToString();
                            }
                            membergrade = MyGrade.GetMemberGradeByID(entity.Member_Grade, Public.GetUserPrivilege());
                            jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                            jsonBuilder.Append("        <td align=\"left\">" + entity.Member_NickName + "</td>");
                            jsonBuilder.Append("        <td align=\"center\">" + entity.Member_Email + "</td>");
                            jsonBuilder.Append("        <td align=\"center\">" + membergrade.Member_Grade_Name + "</td>");
                            jsonBuilder.Append("        <td><a href=\"javascript:picker_member_del('" + entity.Member_ID + "');\"><img src=\"/images/btn_move.gif\" border=\"0\" alt=\"删除\"></a></td>");
                            jsonBuilder.Append("    </tr>");
                        }
                    }

                }
                jsonBuilder.Append("</table>");
                if (member_id == "")
                {
                    jsonBuilder = null;
                    jsonBuilder = new StringBuilder();
                    jsonBuilder.Append("<span class=\"pickertip\">已选择会员</span>");
                }
                else
                {
                    jsonBuilder.Append("<script>if($('#member_picker').attr('class')=='div_picker_unfold'){$('#member_unfold').hide();$('#member_fold').show();}else{$('#member_unfold').show();$('#member_fold').hide();}</script>");
                }
            }
            else
            {
                jsonBuilder.Append("<span class=\"pickertip\">已选择会员</span>");
            }
            Session["selected_memberid"] = member_id;
            jsonBuilder.Append("<script>$('#favor_memberid').val('" + member_id + "');</script>");
            entitys = null;

            return jsonBuilder.ToString();
        }

        //促销优惠政策选择
        public string SelectPromotionPolicy()
        {
            string keyword = tools.CheckStr(Request["keyword"]);
            string product_id = "0";
            if (keyword != "输入优惠标题搜索" && keyword != null)
            {
                keyword = keyword;
            }
            else
            {
                keyword = "";
            }
            QueryInfo Query = new QueryInfo();
            Query.PageSize = tools.CheckInt(Request["rows"]);
            if (tools.CheckInt(Request["page"]) == 0)
            {
                Query.CurrentPage = 1;
            }
            else
            {
                Query.CurrentPage = tools.CheckInt(Request["page"]);
            }
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorPolicyInfo.Promotion_Policy_Site", "=", Public.GetCurrentSite()));

            if (keyword.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorPolicyInfo.Promotion_Policy_Title", "like", keyword));
            }
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorPolicyInfo.Promotion_Policy_IsActive", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorPolicyInfo.Promotion_Policy_IsChecked", "=", "1"));
            Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
            PageInfo pageinfo = MyPolicy.GetPageInfo(Query, Public.GetUserPrivilege());
            IList<PromotionFavorPolicyInfo> entitys = MyPolicy.GetPromotionFavorPolicys(Query, Public.GetUserPrivilege());

            if (entitys != null)
            {
                StringBuilder jsonBuilder = new StringBuilder();
                jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
                jsonBuilder.Append(":[");
                foreach (PromotionFavorPolicyInfo entity in entitys)
                {
                    jsonBuilder.Append("{\"id\":" + entity.Promotion_Policy_ID + ",\"cell\":[");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(entity.Promotion_Policy_ID);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("<a href=\\\"Promotion_Favor_Policy_View.aspx?promotion_policy_id=" + entity.Promotion_Policy_ID + "\\\" target=\\\"_blank\\\">查看详情</a>");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                    jsonBuilder.Append("]},");
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]");
                jsonBuilder.Append("}");

                entitys = null;
                return jsonBuilder.ToString();
            }
            else { return null; }
        }

        //获取全部促销优惠政策编号
        public string Get_PromotionPolicyList_IDs()
        {
            string policy_arry = "";
            string keyword = tools.CheckStr(Request["keyword"]);
            string product_id = "0";
            if (keyword != "输入优惠标题搜索" && keyword != null)
            {
                keyword = keyword;
            }
            else
            {
                keyword = "";
            }
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorPolicyInfo.Promotion_Policy_Site", "=", Public.GetCurrentSite()));


            if (keyword.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorPolicyInfo.Promotion_Policy_Title", "like", keyword));
            }
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorPolicyInfo.Promotion_Policy_IsActive", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorPolicyInfo.Promotion_Policy_IsChecked", "=", "1"));
            Query.OrderInfos.Add(new OrderInfo("PromotionFavorPolicyInfo.Promotion_Policy_ID", "Desc"));
            IList<PromotionFavorPolicyInfo> entitys = MyPolicy.GetPromotionFavorPolicys(Query, Public.GetUserPrivilege());

            if (entitys != null)
            {
                foreach (PromotionFavorPolicyInfo entity in entitys)
                {
                    if (policy_arry.Length > 0)
                    {
                        policy_arry = policy_arry + "," + entity.Promotion_Policy_ID.ToString();
                    }
                    else
                    {
                        policy_arry = entity.Promotion_Policy_ID.ToString();
                    }
                }
            }
            return policy_arry;
        }

        //展示已选择优惠政策
        public string ShowPromotionPolicy_bak(string Policy_ID)
        {
            int del_policy = tools.CheckInt(Request["pid"]);
            string policy_id = "";
            StringBuilder jsonBuilder = new StringBuilder();


            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorPolicyInfo.Promotion_Policy_Site", "=", Public.GetCurrentSite()));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorPolicyInfo.Promotion_Policy_ID", "in", Policy_ID));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorPolicyInfo.Promotion_Policy_IsActive", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorPolicyInfo.Promotion_Policy_IsChecked", "=", "1"));
            Query.OrderInfos.Add(new OrderInfo("PromotionFavorPolicyInfo.Promotion_Policy_ID", "Desc"));
            IList<PromotionFavorPolicyInfo> entitys = MyPolicy.GetPromotionFavorPolicys(Query, Public.GetUserPrivilege());
            if (entitys != null)
            {
                jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" width=\"700\" bgcolor=\"#B0CADA\">");
                jsonBuilder.Append("    <tr class=\"list_head_bg\">");
                jsonBuilder.Append("        <td colspan=\"4\" align=\"left\">已选择优惠政策 <span id=\"promotion_unfold\">[<a href=\"javascript:void(0);\" onclick=\"$('#promotion_unfold').hide();$('#promotion_fold').show();$('#promotion_picker').attr('class','div_picker_unfold');\">展开</a>]</span><span id=\"promotion_fold\" style=\"display:none;\">[<a href=\"javascript:void(0);\" onclick=\"$('#promotion_unfold').show();$('#promotion_fold').hide();$('#promotion_picker').attr('class','div_picker');\">还原</a>]</span></td>");
                jsonBuilder.Append("    </tr>");
                foreach (PromotionFavorPolicyInfo entity in entitys)
                {
                    if (del_policy != entity.Promotion_Policy_ID)
                    {

                        if (policy_id == "")
                        {
                            policy_id = entity.Promotion_Policy_ID.ToString();
                        }
                        else
                        {
                            policy_id += "," + entity.Promotion_Policy_ID.ToString();
                        }
                        jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                        jsonBuilder.Append("        <td width=\"100\" align=\"left\">" + tools.NullStr(entity.Promotion_Policy_ID) + "</td>");
                        jsonBuilder.Append("        <td width=\"350\" align=\"left\">" + tools.NullStr(entity.Promotion_Policy_Title) + "</td>");
                        jsonBuilder.Append("        <td width=\"350\" align=\"left\"><a href=\"Promotion_Favor_Policy_View.aspx?promotion_policy_id="+entity.Promotion_Policy_ID+"\" target=\"_blank\">查看详情</a></td>");
                        jsonBuilder.Append("        <td><a href=\"javascript:picker_promotion_del('" + entity.Promotion_Policy_ID + "');\"><img src=\"/images/btn_move.gif\" border=\"0\" alt=\"删除\"></a></td>");
                        jsonBuilder.Append("    </tr>");
                    }
                }
                jsonBuilder.Append("</table>");
                if (policy_id == "")
                {
                    jsonBuilder = null;
                    jsonBuilder = new StringBuilder();
                    jsonBuilder.Append("<span class=\"pickertip\">已选择优惠政策</span>");
                }
                else
                {
                    jsonBuilder.Append("<script>if($('#product_picker').attr('class')=='div_picker_unfold'){$('#product_unfold').hide();$('#product_fold').show();}else{$('#product_unfold').show();$('#product_fold').hide();}</script>");
                }
            }
            else
            {
                jsonBuilder.Append("<span class=\"pickertip\">已选择优惠政策</span>");
            }
            Session["selected_policyid"] = policy_id;
            jsonBuilder.Append("<script>$('#favor_policyid').val('" + policy_id + "');</script>");
            entitys = null;

            return jsonBuilder.ToString();
        }

        //展示已选择优惠政策
        public string ShowPromotionPolicy()
        {
            string policy_id = "";
            StringBuilder jsonBuilder = new StringBuilder();


            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorPolicyInfo.Promotion_Policy_Site", "=", Public.GetCurrentSite()));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorPolicyInfo.Promotion_Policy_IsActive", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorPolicyInfo.Promotion_Policy_IsChecked", "=", "1"));
            Query.OrderInfos.Add(new OrderInfo("PromotionFavorPolicyInfo.Promotion_Policy_ID", "Desc"));
            IList<PromotionFavorPolicyInfo> entitys = MyPolicy.GetPromotionFavorPolicys(Query, Public.GetUserPrivilege());
            if (entitys != null)
            {
                jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" width=\"700\" bgcolor=\"#B0CADA\">");
                foreach (PromotionFavorPolicyInfo entity in entitys)
                {
                    jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                    jsonBuilder.Append("        <td><input type=\"checkbox\" name=\"favor_policy\" value=\"" + entity.Promotion_Policy_ID + "\"></td>");
                    jsonBuilder.Append("        <td width=\"100\" align=\"left\">" + entity.Promotion_Policy_ID + "</td>");
                    jsonBuilder.Append("        <td width=\"350\" align=\"left\">" + entity.Promotion_Policy_Title + "</td>");
                    jsonBuilder.Append("        <td width=\"350\" align=\"left\">" + entity.Promotion_Policy_Starttime.ToShortDateString() +"至"+entity.Promotion_Policy_Endtime.ToShortDateString() + "</td>");
                    jsonBuilder.Append("        <td width=\"350\" align=\"left\"><a href=\"Promotion_Favor_Policy_View.aspx?promotion_policy_id=" + entity.Promotion_Policy_ID + "\" target=\"_blank\">查看详情</a></td>");
                    
                    jsonBuilder.Append("    </tr>");
                }
                jsonBuilder.Append("</table>");
            }
            else
            {
                jsonBuilder.Append("<span class=\"pickertip\">暂无可选优惠政策</span>");
            }
            entitys = null;

            return jsonBuilder.ToString();
        }

        //展示已选择限时分组
        public string ShowLimitGroup()
        {
            string policy_id = "";
            StringBuilder jsonBuilder = new StringBuilder();


            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionLimitGroupInfo.Promotion_Limit_Group_Site", "=", Public.GetCurrentSite()));
            Query.OrderInfos.Add(new OrderInfo("PromotionLimitGroupInfo.Promotion_Limit_Group_ID", "Desc"));
            IList<PromotionLimitGroupInfo> entitys = MyLimit.GetPromotionLimitGroups(Query);
            if (entitys != null)
            {
                jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" width=\"700\" bgcolor=\"#B0CADA\">");
                foreach (PromotionLimitGroupInfo entity in entitys)
                {
                    jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                    jsonBuilder.Append("        <td><input type=\"checkbox\" name=\"group_id\" value=\"" + entity.Promotion_Limit_Group_ID + "\"></td>");
                    jsonBuilder.Append("        <td width=\"100\" align=\"left\">" + entity.Promotion_Limit_Group_ID + "</td>");
                    jsonBuilder.Append("        <td width=\"350\" align=\"left\">" + entity.Promotion_Limit_Group_Name + "</td>");
                    jsonBuilder.Append("        <td width=\"350\" align=\"left\"><a href=\"Promotion_Limit_List.aspx?group_id=" + entity.Promotion_Limit_Group_ID + "\" target=\"_blank\">查看相关产品</a></td>");

                    jsonBuilder.Append("    </tr>");
                }
                jsonBuilder.Append("</table>");
            }
            else
            {
                jsonBuilder.Append("<span class=\"pickertip\">暂无可选限时分组</span>");
            }
            entitys = null;

            return jsonBuilder.ToString();
        }

        //展示已选择批发分组
        public string ShowWholeSaleGroup()
        {
            string policy_id = "";
            StringBuilder jsonBuilder = new StringBuilder();


            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionWholeSaleGroupInfo.Promotion_WholeSale_Group_Site", "=", Public.GetCurrentSite()));
            Query.OrderInfos.Add(new OrderInfo("PromotionWholeSaleGroupInfo.Promotion_WholeSale_Group_ID", "Desc"));
            IList<PromotionWholeSaleGroupInfo> entitys = MyWholeSale.GetPromotionWholeSaleGroups(Query);
            if (entitys != null)
            {
                jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" width=\"700\" bgcolor=\"#B0CADA\">");
                foreach (PromotionWholeSaleGroupInfo entity in entitys)
                {
                    jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                    jsonBuilder.Append("        <td><input type=\"checkbox\" name=\"group_id\" value=\"" + entity.Promotion_WholeSale_Group_ID + "\"></td>");
                    jsonBuilder.Append("        <td width=\"100\" align=\"left\">" + entity.Promotion_WholeSale_Group_ID + "</td>");
                    jsonBuilder.Append("        <td width=\"350\" align=\"left\">" + entity.Promotion_WholeSale_Group_Name + "</td>");
                    jsonBuilder.Append("        <td width=\"350\" align=\"left\"><a href=\"Promotion_WholeSale_List.aspx?group_id=" + entity.Promotion_WholeSale_Group_ID + "\" target=\"_blank\">查看相关产品</a></td>");

                    jsonBuilder.Append("    </tr>");
                }
                jsonBuilder.Append("</table>");
            }
            else
            {
                jsonBuilder.Append("<span class=\"pickertip\">暂无可选限时分组</span>");
            }
            entitys = null;

            return jsonBuilder.ToString();
        }

        

        //产品选择
        public string Select_Product()
        {
            string keyword = tools.CheckStr(Request["keyword"]);
            if (keyword != "输入商品编码、商品名称、通用名、生产企业、拼音首字母进行搜索" && keyword != null)
            {
                keyword = keyword;
            }
            else
            {
                keyword = "";
            }
            int product_cate = tools.CheckInt(Request["product_cate"]);
            if (product_cate == 0) { product_cate = tools.CheckInt(Request["product_cate_parent"]); }

            string productSelected = tools.CheckStr(Request["product_id"]);


            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
            if (product_cate > 0)
            {
                string subCates = product.Get_All_SubCate(product_cate);
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", subCates));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + subCates + ")"));
                //string product_idstr = product.Get_All_CateProductID(subCates);
                //Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "IN", product_idstr));
            }
            if (keyword.Length > 0)
            {
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_Name", "like", keyword));
                Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_Code", "like", keyword));
                Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_SubName", "like", keyword));
                Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_Maker", "like", keyword));
                Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_NameInitials", "like", keyword));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_SubNameInitials", "like", keyword));
            }
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
            if (productSelected.Length > 0)
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "not in", productSelected));

            Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "DESC"));

            IList<ProductInfo> entitys = MyBLLPRO.GetProductList(Query, Public.GetUserPrivilege());

            if (entitys != null)
            {
                StringBuilder jsonBuilder = new StringBuilder();
                jsonBuilder.Append("<form method=\"post\"  id=\"frmadd\" name=\"frmadd\">");
                jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"list_table_bg\">");
                jsonBuilder.Append("    <tr class=\"list_head_bg\">");
                jsonBuilder.Append("        <td width=\"30\"></td>");
                jsonBuilder.Append("        <td width=\"80\">库存</td>");
                jsonBuilder.Append("        <td>商品编码</td>");
                jsonBuilder.Append("        <td>商品名称</td>");
                jsonBuilder.Append("        <td>规格</td>");
                jsonBuilder.Append("        <td>生产企业</td>");
                jsonBuilder.Append("        <td>本站价格</td>");
                jsonBuilder.Append("    </tr>");
                foreach (ProductInfo entity in entitys)
                {
                    jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                    jsonBuilder.Append("        <td><input type=\"checkbox\" id=\"product_id\" name=\"product_id\" value=\"" + entity.Product_ID + "\" /></td>");
                    jsonBuilder.Append("        <td>" + entity.Product_SaleAmount + "</td>");
                    jsonBuilder.Append("        <td>" + entity.Product_Code + "</td>");
                    jsonBuilder.Append("        <td align=\"left\">" + tools.NullStr(entity.Product_Name) + "</td>");
                    jsonBuilder.Append("        <td align=\"center\">" + tools.NullStr(entity.Product_Spec) + "</td>");
                    jsonBuilder.Append("        <td align=\"center\">" + tools.NullStr(entity.Product_Maker) + "</td>");
                    jsonBuilder.Append("        <td align=\"center\">" + tools.NullDbl(entity.Product_Price) + "</td>");
                    jsonBuilder.Append("    </tr>");
                }

                jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                jsonBuilder.Append("        <td><input type=\"checkbox\" id=\"checkbox\" name=\"chkall\" onclick=\"javascript:CheckAll(this.form)\" /></td>");
                jsonBuilder.Append("        <td colspan=\"6\" align=\"left\"><input type=\"button\" name=\"btn_ok\" value=\"确定\" class=\"bt_orange\" onclick=\"javascript:MM_ReturnID();\" /><input type=\"hidden\" id=\"s_product_id\" value=\"" + productSelected + "\"></td>");
                jsonBuilder.Append("    </tr>");
                jsonBuilder.Append("</table>");
                jsonBuilder.Append("</form>");

                entitys = null;
                return jsonBuilder.ToString();
            }
            else { return null; }
        }

    #endregion

    #region 运费优惠  

    public virtual void AddPromotionFavorFee()
    {
        string Promotion_Fee_Title = tools.CheckStr(Request.Form["Promotion_Fee_Title"]);
        double Promotion_Fee_Payline = tools.CheckFloat(Request.Form["Promotion_Fee_Payline"]);
        int Promotion_Fee_Manner = tools.CheckInt(Request.Form["Promotion_Fee_Manner"]);
        double Promotion_Fee_Price = tools.CheckFloat(Request.Form["Promotion_Fee_Price"]);
        int Promotion_Fee_Target = tools.CheckInt(Request.Form["favor_target"]);
        DateTime Promotion_Fee_Starttime = tools.NullDate(Request.Form["Promotion_Fee_Starttime"]);
        DateTime Promotion_Fee_Endtime = tools.NullDate(Request.Form["Promotion_Fee_Endtime"]);
        string Promotion_Fee_Note = tools.CheckStr(Request.Form["Promotion_Fee_Note"]);
        DateTime Promotion_Fee_Addtime = DateTime.Now;
        string Promotion_Fee_Site = Public.GetCurrentSite();
        int Cate_All = tools.CheckInt(Request.Form["favor_cateall"]);
        int Brand_All = tools.CheckInt(Request.Form["favor_brandall"]);
        int Product_All = tools.CheckInt(Request.Form["favor_productall"]);
        int Delivery_All = tools.CheckInt(Request.Form["favor_deliveryall"]);
        int Payway_All = tools.CheckInt(Request.Form["favor_paywayall"]);
        int State_All = tools.CheckInt(Request.Form["favor_provinceall"]);
        string Favor_Cateid=tools.CheckStr(Request.Form["favor_cateid"]);
        string Favor_Brandid=tools.CheckStr(Request.Form["favor_brandid"]);
        string Favor_Productid = tools.CheckStr(Request.Form["favor_productid"]);
        string favor_deliveryid = tools.CheckStr(Request.Form["favor_deliveryid"]);
        string favor_paywayid = tools.CheckStr(Request.Form["favor_paywayid"]);
        string favor_gradeid = tools.CheckStr(Request.Form["Member_Grade"]);
        string Favor_State = tools.CheckStr(Request.Form["favor_province"]);
        int Promotion_Fee_Sort = tools.CheckInt(Request.Form["Favor_Sort"]);
        int Promotion_Fee_IsActive = 1;
        int Promotion_Fee_IsChecked = 0;
        if (Promotion_Fee_Title == "")
        {
            Public.Msg("error", "错误提示", "请输入优惠名称", false, "{back}");
        }
        if (Promotion_Fee_Manner == 1 && Promotion_Fee_Price == 0)
        {
            Public.Msg("error", "错误提示", "请设置优惠运费金额", false, "{back}");
        }
        if (Cate_All == 0 && Favor_Cateid == "" && Promotion_Fee_Target==0)
        {
            Public.Msg("error", "错误提示", "请选择适用产品类别", false, "{back}");
        }
        if (Brand_All == 0 && Favor_Brandid == "" && Promotion_Fee_Target == 0)
        {
            Public.Msg("error", "错误提示", "请选择适用产品品牌", false, "{back}");
        }
        if (Product_All == 0 && Favor_Productid == "" && Promotion_Fee_Target == 1)
        {
            Public.Msg("error", "错误提示", "请选择适用产品", false, "{back}");
        }
        if (Delivery_All == 0 && favor_deliveryid == "")
        {
            Public.Msg("error", "错误提示", "请选择适用配送方式", false, "{back}");
        }
        if (Payway_All == 0 && favor_paywayid == "")
        {
            Public.Msg("error", "错误提示", "请选择适用支付方式", false, "{back}");
        }
        if (State_All == 0 && (Favor_State == "0" || Favor_State == ""))
        {
            Public.Msg("error", "错误提示", "请指定适用省份", false, "{back}");
        }
        if (favor_gradeid.Length == 0)
        {
            Public.Msg("error", "错误信息", "请选择针对会员！", false, "{back}");
        }
        PromotionFavorFeeInfo entity = new PromotionFavorFeeInfo();
        entity.Promotion_Fee_ID = 0;
        entity.Promotion_Fee_Title = Promotion_Fee_Title;
        entity.Promotion_Fee_Target = Promotion_Fee_Target;
        entity.Promotion_Fee_Payline = Promotion_Fee_Payline;
        entity.Promotion_Fee_Manner = Promotion_Fee_Manner;
        entity.Promotion_Fee_Price = Promotion_Fee_Price;
        entity.Promotion_Fee_Starttime = Promotion_Fee_Starttime;
        entity.Promotion_Fee_Endtime = Promotion_Fee_Endtime;
        entity.Promotion_Fee_Sort = Promotion_Fee_Sort;
        entity.Promotion_Fee_IsActive = Promotion_Fee_IsActive;
        entity.Promotion_Fee_IsChecked = Promotion_Fee_IsChecked;
        entity.Promotion_Fee_Note = Promotion_Fee_Note;
        entity.Promotion_Fee_Addtime = Promotion_Fee_Addtime;
        entity.Promotion_Fee_Site = Promotion_Fee_Site;

        if (MyBLL.AddPromotionFavorFee(entity,Public.GetUserPrivilege()))
        {
            entity = null;
            entity = new PromotionFavorFeeInfo();
            entity = MyBLL.GetLastPromotionFavorFee(Public.GetUserPrivilege());
            if (entity != null)
            {
                if (entity.Promotion_Fee_Title == Promotion_Fee_Title)
                {
                    //添加适用会员等级
                    PromotionFavorFeeMemberGradeInfo feegrade;
                    if (favor_gradeid.Length==0)
                    {
                        feegrade = new PromotionFavorFeeMemberGradeInfo();
                        feegrade.Favor_Id = 0;
                        feegrade.Favor_GradeID = 0;
                        feegrade.Promotion_Fee_ID = entity.Promotion_Fee_ID;
                        MyBLL.AddPromotionFavorFeeMemberGrade(feegrade);
                        feegrade = null;
                    }
                    else
                    {
                        foreach (string substr in favor_gradeid.Split(','))
                        {
                            if (tools.CheckInt(substr) > 0)
                            {
                                feegrade = new PromotionFavorFeeMemberGradeInfo();
                                feegrade.Favor_Id = 0;
                                feegrade.Favor_GradeID = tools.CheckInt(substr);
                                feegrade.Promotion_Fee_ID = entity.Promotion_Fee_ID;
                                MyBLL.AddPromotionFavorFeeMemberGrade(feegrade);
                                feegrade = null;
                            }
                        }
                    }

                    if (Promotion_Fee_Target == 0)
                    {
                        //添加适用分类
                        PromotionFavorFeeCateInfo feecate;
                        if (Cate_All == 1)
                        {
                            feecate = new PromotionFavorFeeCateInfo();
                            feecate.Favor_Id = 0;
                            feecate.Favor_CateId = 0;
                            feecate.Promotion_Fee_ID = entity.Promotion_Fee_ID;
                            MyBLL.AddPromotionFavorFeeCate(feecate);
                            feecate = null;
                        }
                        else
                        {
                            foreach (string substr in Favor_Cateid.Split(','))
                            {
                                if (tools.CheckInt(substr) > 0)
                                {
                                    feecate = new PromotionFavorFeeCateInfo();
                                    feecate.Favor_Id = 0;
                                    feecate.Favor_CateId = tools.CheckInt(substr);
                                    feecate.Promotion_Fee_ID = entity.Promotion_Fee_ID;
                                    MyBLL.AddPromotionFavorFeeCate(feecate);
                                    feecate = null;
                                }
                            }
                        }

                        //添加适用品牌
                        PromotionFavorFeeBrandInfo feebrand;
                        if (Brand_All == 1)
                        {
                            feebrand = new PromotionFavorFeeBrandInfo();
                            feebrand.Favor_Id = 0;
                            feebrand.Favor_BrandID = 0;
                            feebrand.Promotion_Fee_ID = entity.Promotion_Fee_ID;
                            MyBLL.AddPromotionFavorFeeBrand(feebrand);
                            feebrand = null;
                        }
                        else
                        {
                            foreach (string substr in Favor_Brandid.Split(','))
                            {
                                if (tools.CheckInt(substr) > 0)
                                {
                                    feebrand = new PromotionFavorFeeBrandInfo();
                                    feebrand.Favor_Id = 0;
                                    feebrand.Favor_BrandID = tools.CheckInt(substr);
                                    feebrand.Promotion_Fee_ID = entity.Promotion_Fee_ID;
                                    MyBLL.AddPromotionFavorFeeBrand(feebrand);
                                    feebrand = null;
                                }
                            }
                        }
                    }
                    else
                    {
                        //添加适用产品
                        PromotionFavorFeeProductInfo feeproduct;
                        if (Product_All == 1)
                        {
                            feeproduct = new PromotionFavorFeeProductInfo();
                            feeproduct.Favor_Id = 0;
                            feeproduct.Favor_ProductId = 0;
                            feeproduct.Promotion_Fee_ID = entity.Promotion_Fee_ID;
                            MyBLL.AddPromotionFavorFeeProduct(feeproduct);
                            feeproduct = null;
                        }
                        else
                        {
                            foreach (string substr in Favor_Productid.Split(','))
                            {
                                if (tools.CheckInt(substr) > 0)
                                {
                                    feeproduct = new PromotionFavorFeeProductInfo();
                                    feeproduct.Favor_Id = 0;
                                    feeproduct.Favor_ProductId = tools.CheckInt(substr);
                                    feeproduct.Promotion_Fee_ID = entity.Promotion_Fee_ID;
                                    MyBLL.AddPromotionFavorFeeProduct(feeproduct);
                                    feeproduct = null;
                                }
                            }
                        }
                    }
                    //添加适用区域
                    PromotionFavorFeeDistrictInfo feedistrict;
                    if (State_All == 1)
                    {
                        feedistrict = new PromotionFavorFeeDistrictInfo();
                        feedistrict.Favor_ID = 0;
                        feedistrict.Favor_State_ID = "0";
                        feedistrict.Promotion_Fee_ID = entity.Promotion_Fee_ID;
                        MyBLL.AddPromotionFavorFeeDistrict(feedistrict);
                        feedistrict = null;
                    }
                    else
                    {
                        foreach (string substr in Favor_State.Split(','))
                        {
                            if (tools.CheckInt(substr) > 0)
                            {
                                feedistrict = new PromotionFavorFeeDistrictInfo();
                                feedistrict.Favor_ID = 0;
                                feedistrict.Favor_State_ID = substr;
                                feedistrict.Promotion_Fee_ID = entity.Promotion_Fee_ID;
                                MyBLL.AddPromotionFavorFeeDistrict(feedistrict);
                                feedistrict = null;
                            }
                        }
                    }

                    //添加适用配送方式
                    PromotionFavorFeeDeliveryInfo feedelivery;
                    if (Delivery_All == 1)
                    {
                        feedelivery = new PromotionFavorFeeDeliveryInfo();
                        feedelivery.Favor_Id = 0;
                        feedelivery.Favor_DeliveryId = 0;
                        feedelivery.Promotion_Fee_ID = entity.Promotion_Fee_ID;
                        MyBLL.AddPromotionFavorFeeDelivery(feedelivery);
                        feedelivery = null;
                    }
                    else
                    {
                        foreach (string substr in favor_deliveryid.Split(','))
                        {
                            if (tools.CheckInt(substr) > 0)
                            {
                                feedelivery = new PromotionFavorFeeDeliveryInfo();
                                feedelivery.Favor_Id = 0;
                                feedelivery.Favor_DeliveryId = tools.CheckInt(substr); ;
                                feedelivery.Promotion_Fee_ID = entity.Promotion_Fee_ID;
                                MyBLL.AddPromotionFavorFeeDelivery(feedelivery);
                                feedelivery = null;
                            }
                        }
                    }

                    //添加适用支付方式
                    PromotionFavorFeePaywayInfo feepayway;
                    if (Payway_All == 1)
                    {
                        feepayway = new PromotionFavorFeePaywayInfo();
                        feepayway.Favor_Id = 0;
                        feepayway.Favor_PaywayId = 0;
                        feepayway.Promotion_Fee_ID = entity.Promotion_Fee_ID;
                        MyBLL.AddPromotionFavorFeePayway(feepayway);
                        feepayway = null;
                    }
                    else
                    {
                        foreach (string substr in favor_paywayid.Split(','))
                        {
                            if (tools.CheckInt(substr) > 0)
                            {
                                feepayway = new PromotionFavorFeePaywayInfo();
                                feepayway.Favor_Id = 0;
                                feepayway.Favor_PaywayId = tools.CheckInt(substr); ;
                                feepayway.Promotion_Fee_ID = entity.Promotion_Fee_ID;
                                MyBLL.AddPromotionFavorFeePayway(feepayway);
                                feepayway = null;
                            }
                        }
                    }

                }
            }
            entity = null;
            Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_Favor_Fee_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditPromotionFavorFeeStatus(int Action_Code)
    {
        string Promotion_Fee_ID = tools.CheckStr(Request["favor_id"]);
        if (Promotion_Fee_ID == "")
        {
            Public.Msg("error", "错误信息", "请选择要操作的优惠信息", false, "{back}");
            return;
        }
        if (tools.Left(Promotion_Fee_ID, 1) == ",") { Promotion_Fee_ID = Promotion_Fee_ID.Remove(0, 1); }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorFeeInfo.Promotion_Fee_ID", "in", Promotion_Fee_ID));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorFeeInfo.Promotion_Fee_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("PromotionFavorFeeInfo.Promotion_Fee_ID", "DESC"));
        IList<PromotionFavorFeeInfo> entitys = MyBLL.GetPromotionFavorFees(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (PromotionFavorFeeInfo entity in entitys)
            {
                switch (Action_Code)
                {
                    case 1:
                        entity.Promotion_Fee_IsActive = 1;
                        break;
                    case 2:
                        entity.Promotion_Fee_IsActive = 0;
                        break;
                    case 3:
                        entity.Promotion_Fee_IsChecked = 1;
                        break;
                    case 4:
                        entity.Promotion_Fee_IsChecked = 2;
                        break;
                }
                MyBLL.EditPromotionFavorFee(entity);
            }
            Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_Favor_Fee_List.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
            
            
    }

    public virtual void DelPromotionFavorFee()
    {
        int Promotion_Fee_ID = tools.CheckInt(Request.QueryString["favor_id"]);
        if (MyBLL.DelPromotionFavorFee(Promotion_Fee_ID, Public.GetUserPrivilege()) > 0)
        {
            MyBLL.DelPromotionFavorFeeCateByFeedID(Promotion_Fee_ID);
            MyBLL.DelPromotionFavorFeeDistrictByFeedID(Promotion_Fee_ID);
            MyBLL.DelPromotionFavorFeeDeliveryByFeedID(Promotion_Fee_ID);
            MyBLL.DelPromotionFavorFeePaywayByFeedID(Promotion_Fee_ID);
            MyBLL.DelPromotionFavorFeeProductByFeedID(Promotion_Fee_ID);
            MyBLL.DelPromotionFavorFeeMemberGradeByFeedID(Promotion_Fee_ID);
            MyBLL.DelPromotionFavorFeeExceptByFeedID(Promotion_Fee_ID);

            Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_Favor_Fee_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual PromotionFavorFeeInfo GetPromotionFavorFeeByID(int ID)
    {
        return MyBLL.GetPromotionFavorFeeByID(ID, Public.GetUserPrivilege());
    }

    public virtual string Get_Fee_State_String(IList<PromotionFavorFeeDistrictInfo> entitys)
    {
        string return_value="";
        int i = 0;
        if (entitys != null)
        {
            foreach (PromotionFavorFeeDistrictInfo entity in entitys)
            {
                i = i + 1;
                if (i < entitys.Count)
                {
                    return_value = return_value + entity.Favor_State_ID + ",";
                }
                else
                {
                    return_value = return_value + entity.Favor_State_ID;
                }
            }

        }
        else
        {
            return_value = "0";
        }
        return return_value;
    }

    public virtual string Get_Fee_Delivery_String(IList<PromotionFavorFeeDeliveryInfo> entitys)
    {
        string return_value = "";
        int i = 0;
        if (entitys != null)
        {
            foreach (PromotionFavorFeeDeliveryInfo entity in entitys)
            {
                i = i + 1;
                if (i < entitys.Count)
                {
                    return_value = return_value + entity.Favor_DeliveryId + ",";
                }
                else
                {
                    return_value = return_value + entity.Favor_DeliveryId;
                }
            }

        }
        else
        {
            return_value = "0";
        }
        return return_value;
    }

    public virtual string Get_Fee_Payway_String(IList<PromotionFavorFeePaywayInfo> entitys)
    {
        string return_value = "";
        int i = 0;
        if (entitys != null)
        {
            foreach (PromotionFavorFeePaywayInfo entity in entitys)
            {
                i = i + 1;
                if (i < entitys.Count)
                {
                    return_value = return_value + entity.Favor_PaywayId + ",";
                }
                else
                {
                    return_value = return_value + entity.Favor_PaywayId;
                }
            }

        }
        else
        {
            return_value = "0";
        }
        return return_value;
    }

    public virtual string Get_Fee_MemberGrade_String(IList<PromotionFavorFeeMemberGradeInfo> entitys)
    {
        string return_value = "";
        int i = 0;
        if (entitys != null)
        {
            foreach (PromotionFavorFeeMemberGradeInfo entity in entitys)
            {
                i = i + 1;
                if (i < entitys.Count)
                {
                    return_value = return_value + entity.Favor_GradeID + ",";
                }
                else
                {
                    return_value = return_value + entity.Favor_GradeID;
                }
            }

        }
        else
        {
            return_value = "0";
        }
        return return_value;
    }

    public virtual string Get_Fee_Cate_String(IList<PromotionFavorFeeCateInfo> entitys)
    {
        string return_value = "";
        int i = 0;
        if (entitys != null)
        {
            foreach (PromotionFavorFeeCateInfo entity in entitys)
            {
                i = i + 1;
                if (i < entitys.Count)
                {
                    return_value = return_value + entity.Favor_CateId + ",";
                }
                else
                {
                    return_value = return_value + entity.Favor_CateId;
                }
            }

        }
        else
        {
            return_value = "0";
        }
        return return_value;
    }

    public virtual string Get_Fee_Brand_String(IList<PromotionFavorFeeBrandInfo> entitys)
    {
        string return_value = "";
        int i = 0;
        if (entitys != null)
        {
            foreach (PromotionFavorFeeBrandInfo entity in entitys)
            {
                i = i + 1;
                if (i < entitys.Count)
                {
                    return_value = return_value + entity.Favor_BrandID + ",";
                }
                else
                {
                    return_value = return_value + entity.Favor_BrandID;
                }
            }

        }
        else
        {
            return_value = "0";
        }
        return return_value;
    }

    public virtual string Get_Fee_Product_String(IList<PromotionFavorFeeProductInfo> entitys)
    {
        string return_value = "";
        int i = 0;
        if (entitys != null)
        {
            foreach (PromotionFavorFeeProductInfo entity in entitys)
            {
                i = i + 1;
                if (i < entitys.Count)
                {
                    return_value = return_value + entity.Favor_ProductId + ",";
                }
                else
                {
                    return_value = return_value + entity.Favor_ProductId;
                }
            }

        }
        else
        {
            return_value = "0";
        }
        return return_value;
    }

    //展示适用的产品
    public string ShowFee_FavorProduct(string valid_type,int Fee_ID)
    {
        string productid = "0";
        string promotionids = "0";
        string[] temp_arry;
        string product_id = "";
        int totalcount = 0;
        string exceptid = "0";
        IList<string> productids = new List<string>();
        if (valid_type == "valid")
        {
            productids = GetFeeProducts(Fee_ID);
            if (productids != null)
            {
                foreach (string subproduct in productids)
                {
                    if (subproduct.Split('|')[1] == "0")
                    {
                        totalcount = totalcount + 1;
                        promotionids = promotionids + "," + subproduct.Split('|')[0];
                    }
                    else
                    {
                        productid = productid + "," + subproduct.Split('|')[1];
                        temp_arry = subproduct.Split('|')[1].Split(',');
                        if (temp_arry.Contains(product_id.ToString()))
                        {
                            promotionids = promotionids + "," + subproduct.Split('|')[0];
                        }
                    }
                }
            }
            PromotionFavorFeeInfo feeinfo = GetPromotionFavorFeeByID(Fee_ID);
            if (feeinfo != null)
            {
                if (feeinfo.PromotionFavorFeeExcepts != null)
                {
                    foreach (PromotionFavorFeeExceptInfo subexcept in feeinfo.PromotionFavorFeeExcepts)
                    {
                        exceptid = exceptid + "," + subexcept.Favor_ProductId;
                    }
                }
            }
        }
        else
        {
            PromotionFavorFeeInfo feeinfo = GetPromotionFavorFeeByID(Fee_ID);
            if (feeinfo != null)
            {
                if (feeinfo.PromotionFavorFeeExcepts != null)
                {
                    foreach (PromotionFavorFeeExceptInfo subexcept in feeinfo.PromotionFavorFeeExcepts)
                    {
                        productid = productid + "," + subexcept.Favor_ProductId;
                    }
                }
            }
        }
        StringBuilder jsonBuilder = new StringBuilder();


        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
        if (totalcount == 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", productid));
        }
        if (exceptid != "0")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "not in", exceptid));
        }
        IList<ProductInfo> entitys = MyProduct.GetProductList(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" width=\"600\" bgcolor=\"#B0CADA\">");
            jsonBuilder.Append("    <tr class=\"list_td_bg\">");
            jsonBuilder.Append("        <td width=\"55\" align=\"center\">ID</td>");
            jsonBuilder.Append("        <td width=\"100\" align=\"center\">产品编号</td>");
            jsonBuilder.Append("        <td align=\"center\">产品名称</td>");
            jsonBuilder.Append("        <td align=\"center\">规格</td>");
            jsonBuilder.Append("        <td align=\"center\">生产厂家</td>");
            jsonBuilder.Append("        <td align=\"center\" width=\"80\">操作</td>");
            jsonBuilder.Append("    </tr>");
            foreach (ProductInfo entity in entitys)
            {
                if (product_id == "")
                {
                    product_id = entity.Product_ID.ToString();
                }
                else
                {
                    product_id += "," + entity.Product_ID.ToString();
                }
                jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                jsonBuilder.Append("        <td width=\"55\" align=\"left\">" + entity.Product_ID + "</td>");
                jsonBuilder.Append("        <td width=\"100\" align=\"left\">" + entity.Product_Code + "</td>");
                jsonBuilder.Append("        <td align=\"left\">" + entity.Product_Name + "</td>");
                jsonBuilder.Append("        <td align=\"left\">" + entity.Product_Spec + "</td>");
                jsonBuilder.Append("        <td align=\"left\">" + entity.Product_Maker + "</td>");
                if (valid_type == "valid")
                {
                    jsonBuilder.Append("        <td><img src=\"/images/icon_del.gif\"> <a href=\"javascript:void(0);\" onclick=\"promotionexcept('fee'," + Fee_ID + "," + entity.Product_ID + ")\" title=\"排除\">排除</a></td>");

                }
                else
                {
                    jsonBuilder.Append("        <td><img src=\"/images/icon_ok.gif\"> <a href=\"javascript:void(0);\" onclick=\"promotionvalid('fee'," + Fee_ID + "," + entity.Product_ID + ")\" title=\"恢复\">恢复</a></td>");
                }
                jsonBuilder.Append("    </tr>");
            }
            jsonBuilder.Append("</table>");
            if (product_id == "")
            {
                jsonBuilder = null;
                jsonBuilder = new StringBuilder();
                jsonBuilder.Append("<span class=\"pickertip\">无产品信息</span>");
            }
        }
        else
        {
            jsonBuilder.Append("<span class=\"pickertip\">无产品信息</span>");
        }
        entitys = null;

        return jsonBuilder.ToString();
    }

    //获取运费优惠列表
    public string GetPromotionFavorFees()
    {
        QueryInfo Query = new QueryInfo();
        DateTime date1,date2,date3;
        date1 = DateTime.Now.Date;
        Query.PageSize = tools.CheckInt(Request["rows"]);
        if (tools.CheckInt(Request["page"]) == 0)
        {
            Query.CurrentPage = 1;
        }
        else
        {
            Query.CurrentPage = tools.CheckInt(Request["page"]);
        }
        string keyword;
        int Fee_Status, Fee_IsActive, Fee_IsChecked;
        keyword = tools.CheckStr(Request["keyword"]).Trim();
        if (keyword != "输入优惠标题搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "";
        }
        Fee_Status = tools.CheckInt(Request["Fee_Status"]);
        Fee_IsChecked = tools.CheckInt(Request["Fee_IsChecked"]);
        Fee_IsActive = tools.CheckInt(Request["Fee_IsActive"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorFeeInfo.Promotion_Fee_Site", "=", Public.GetCurrentSite()));
        if (keyword.Length>0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorFeeInfo.Promotion_Fee_Title", "%like%", keyword));
        }
        if (Fee_Status == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "Datediff(d,{PromotionFavorFeeInfo.Promotion_Fee_Starttime},GetDATE())", ">=", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "Datediff(d,{PromotionFavorFeeInfo.Promotion_Fee_Endtime},GetDATE())", "<=", "0"));
        }
        if (Fee_Status == 2)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "Datediff(d,{PromotionFavorFeeInfo.Promotion_Fee_Starttime},GetDATE())", "<", "0"));
        }
        if (Fee_Status == 3)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "Datediff(d,{PromotionFavorFeeInfo.Promotion_Fee_Endtime},GetDATE())", ">", "0"));
        }
        if (Fee_IsActive > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorFeeInfo.Promotion_Fee_IsActive", "=", (Fee_IsActive - 1).ToString()));
        }
        if (Fee_IsChecked > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorFeeInfo.Promotion_Fee_IsChecked", "=", (Fee_IsChecked - 1).ToString()));
        }
        
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<PromotionFavorFeeInfo> entitys = MyBLL.GetPromotionFavorFees(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (PromotionFavorFeeInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.Promotion_Fee_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Fee_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Fee_Title);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("≥" + entity.Promotion_Fee_Payline.ToString());
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Fee_Starttime.ToShortDateString() + "至" + entity.Promotion_Fee_Endtime.ToShortDateString());
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Promotion_Fee_IsChecked == 0)
                {
                    jsonBuilder.Append("待审核");
                }
                else if (entity.Promotion_Fee_IsChecked == 2)
                {
                    jsonBuilder.Append("审核未通过");
                }
                else if (entity.Promotion_Fee_IsActive == 0)
                {
                    jsonBuilder.Append("暂停中");
                }
                else
                {
                    date2 = entity.Promotion_Fee_Starttime.Date;
                    date3 = entity.Promotion_Fee_Endtime.Date;
                    if (date1 > date3)
                    {
                        jsonBuilder.Append("<font color=\\\"#ff0000\\\">已结束</font>");
                    }
                    else if (date1 < date2)
                    {
                        jsonBuilder.Append("<font color=\\\"#ff0000\\\">未开始</font>");
                    }
                    else
                    {
                        jsonBuilder.Append("应用中");
                    }
                }
                jsonBuilder.Append("\",");

                

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Fee_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("db71e6f9-f858-4469-b45e-b6ab55412853"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" align=\\\"absmiddle\\\"> <a href=\\\"Promotion_Favor_Fee_View.aspx?favor_id=" + entity.Promotion_Fee_ID + "\\\" title=\\\"查看\\\">查看</a>");
                }
                if (Public.CheckPrivilege("7cd745a6-dfce-4f33-8453-a64cc07c44c9"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_copy.gif\\\" align=\\\"absmiddle\\\"> <a href=\\\"Promotion_Favor_Fee_Copy.aspx?favor_id=" + entity.Promotion_Fee_ID + "\\\" title=\\\"复制\\\">复制</a>");
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }


    #endregion

    #region 促销专题

    public virtual void AddPromotionGroup()
    {
        int Promotion_Group_ID = tools.CheckInt(Request.Form["Promotion_Group_ID"]);
        string Promotion_Group_Name = tools.CheckStr(Request.Form["Promotion_Group_Name"]);
        string Promotion_Group_PromotionID = tools.CheckStr(Request.Form["promtion_id"]);
        string Promotion_Group_Site = Public.GetCurrentSite();
        if (Promotion_Group_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写组合名称", false, "{back}");
        }
        if (Promotion_Group_PromotionID.Length == 0)
        {
            Public.Msg("error", "错误信息", "请选择组合专题", false, "{back}");
        }
        PromotionGroupInfo entity = new PromotionGroupInfo();
        PromotionGroupPromotionInfo grouppromotion;
        entity.Promotion_Group_ID = Promotion_Group_ID;
        entity.Promotion_Group_Title = Promotion_Group_Name;
        entity.Promotion_Group_Addtime = DateTime.Now;
        entity.Promotion_Group_Site = Promotion_Group_Site;

        if (Mypromotion.AddPromotionGroup(entity))
        {
            entity = Mypromotion.GetLastPromotionGroupByID();
            if (entity != null)
            {
                if (entity.Promotion_Group_Title == Promotion_Group_Name)
                {
                    foreach (string subid in Promotion_Group_PromotionID.Split(','))
                    {
                        if (tools.CheckInt(subid) > 0)
                        {
                            grouppromotion = new PromotionGroupPromotionInfo();
                            grouppromotion.Promotion_Group_Promotion_GroupID = entity.Promotion_Group_ID;
                            grouppromotion.Promotion_Group_Promotion_PromotionID = tools.CheckInt(subid);
                            grouppromotion.Promotion_Group_Promotion_Sort = tools.NullInt(Request["promotion_sort" + subid]);
                            Mypromotion.AddPromotionGroupPromotion(grouppromotion);
                            grouppromotion = null;
                        }
                    }
                }
            }
            Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_Group_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditPromotionGroup()
    {

        int Promotion_Group_ID = tools.CheckInt(Request.Form["Promotion_Group_ID"]);
        string Promotion_Group_Name = tools.CheckStr(Request.Form["Promotion_Group_Name"]);
        string Promotion_Group_PromotionID = tools.CheckStr(Request.Form["promtion_id"]);
        if (Promotion_Group_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写组合名称", false, "{back}");
        }
        if (Promotion_Group_PromotionID.Length == 0)
        {
            Public.Msg("error", "错误信息", "请选择组合专题", false, "{back}");
        }
        PromotionGroupPromotionInfo grouppromotion;
        PromotionGroupInfo entity = GetPromotionGroupByID(Promotion_Group_ID);
        if (entity != null)
        {
            entity.Promotion_Group_ID = Promotion_Group_ID;
            entity.Promotion_Group_Title = Promotion_Group_Name;


            if (Mypromotion.EditPromotionGroup(entity))
            {
                Mypromotion.DelPromotionGroupPromotionByID(Promotion_Group_ID);

                foreach (string subid in Promotion_Group_PromotionID.Split(','))
                {
                    if (tools.CheckInt(subid) > 0)
                    {
                        grouppromotion = new PromotionGroupPromotionInfo();
                        grouppromotion.Promotion_Group_Promotion_GroupID = Promotion_Group_ID;
                        grouppromotion.Promotion_Group_Promotion_PromotionID = tools.CheckInt(subid);
                        grouppromotion.Promotion_Group_Promotion_Sort = tools.NullInt(Request["promotion_sort" + subid]);
                        Mypromotion.AddPromotionGroupPromotion(grouppromotion);
                        grouppromotion = null;
                    }
                }
                Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_Group_list.aspx");
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }

    }

    public virtual void DelPromotionGroup()
    {
        int Promotion_Group_ID = tools.CheckInt(Request["Promotion_Group_ID"]);
        if (Mypromotion.DelPromotionGroup(Promotion_Group_ID) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_Group_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual PromotionGroupInfo GetPromotionGroupByID(int cate_id)
    {
        return Mypromotion.GetPromotionGroupByID(cate_id);
    }

    //获取批发促销列表
    public virtual string GetPromotionGroups()
    {
        string product_id = "0";
        string keyword = tools.CheckStr(Request["keyword"]);
        QueryInfo Query = new QueryInfo();
        ProductInfo product = null;
        Query.PageSize = tools.CheckInt(Request["rows"]);
        if (tools.CheckInt(Request["page"]) == 0)
        {
            Query.CurrentPage = 1;
        }
        else
        {
            Query.CurrentPage = tools.CheckInt(Request["page"]);

        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionGroupInfo.Promotion_Group_Site", "=", Public.GetCurrentSite()));
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionGroupInfo.Promotion_Group_Title", "like", keyword));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = Mypromotion.GetGroupPageInfo(Query);
        IList<PromotionGroupInfo> entitys = Mypromotion.GetPromotionGroups(Query);
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (PromotionGroupInfo entity in entitys)
            {

                jsonBuilder.Append("{\"PromotionGroupInfo.Promotion_Group_ID\":" + entity.Promotion_Group_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Group_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Group_Title);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Application["site_url"] + "promotion/index.aspx?group=" + entity.Promotion_Group_ID.ToString());
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Group_Addtime.ToShortDateString());
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

     
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"Promotion_Group_Edit.aspx?Promotion_Group_ID=" + entity.Promotion_Group_ID + "\\\" title=\\\"修改\\\">修改</a>");
                
                
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Promotion_Group_do.aspx?action=move&Promotion_Group_ID=" + entity.Promotion_Group_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    public virtual void GetPromotionGroups(int Seled_ID)
    {
        QueryInfo Query = new QueryInfo();
        ProductInfo product = null;
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Response.Write("<select name=\"Promotion_Group\">");
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionGroupInfo.Promotion_Group_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("PromotionGroupInfo.Promotion_Group_ID", "Desc"));
        IList<PromotionGroupInfo> entitys = Mypromotion.GetPromotionGroups(Query);
        if (entitys != null)
        {
            foreach (PromotionGroupInfo entity in entitys)
            {
                if (Seled_ID == entity.Promotion_Group_ID)
                {
                    Response.Write("<option value=\"" + entity.Promotion_Group_ID + "\" selected>" + entity.Promotion_Group_Title + "</option>");
                }
                else
                {
                    Response.Write("<option value=\"" + entity.Promotion_Group_ID + "\">" + entity.Promotion_Group_Title + "</option>");
                }
            }
        }
        else
        {
            Response.Write("<option value=\"0\">选择组合</option>");
        }
        Response.Write("</select>");
    }

    public virtual void AddPromotion()
    {
        string Promotion_Title = tools.CheckStr(Request.Form["Promotion_Title"]);
        string Promotion_TopHtml = Request.Form["Promotion_TopHtml"];
        int Promotion_Type = tools.CheckInt(Request.Form["Promotion_Type"]);
        string Promotion_Site = Public.GetCurrentSite();

        
        if (Promotion_Title == "")
        {
            Public.Msg("error", "错误提示", "请输入促销专题名称", false, "{back}");
        }


        PromotionInfo promotion = new PromotionInfo();
        promotion.Promotion_ID = 0;
        promotion.Promotion_Title = Promotion_Title;
        promotion.Promotion_TopHtml = Promotion_TopHtml;
        promotion.Promotion_Type = Promotion_Type;
        promotion.Promotion_Addtime = DateTime.Now;
        if (Promotion_Type == 0)
        {
            promotion.PromotionProducts = ReadPromotionProduct();
        }
        if (Promotion_Type == 3)
        {
            promotion.PromotionProducts = ReadPromotionPolicyProduct();
        }
        if (Promotion_Type == 1)
        {
            promotion.PromotionProducts = ReadWholeSaleGroupProduct();
        }
        if (Promotion_Type == 2)
        {
            promotion.PromotionProducts = ReadLimitGroupProduct();
        }
        promotion.Promotion_Site = Promotion_Site;

        if (Mypromotion.AddPromotion(promotion, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //修改促销专题
    public virtual void EditPromotion()
    {
        int Promotion_ID = tools.CheckInt(Request.Form["Promotion_ID"]);
        string Promotion_Title = tools.CheckStr(Request.Form["Promotion_Title"]);
        string Promotion_TopHtml =Request.Form["Promotion_TopHtml"];
        int Promotion_Type = 0;
        string Promotion_Site = Public.GetCurrentSite();


        if (Promotion_Title == "")
        {
            Public.Msg("error", "错误提示", "请输入促销专题名称", false, "{back}");
        }


        PromotionInfo promotion = GetPromotionByID(Promotion_ID);
        if (promotion != null)
        {
            promotion.Promotion_Title = Promotion_Title;
            promotion.Promotion_TopHtml = Promotion_TopHtml;
            promotion.Promotion_Type = Promotion_Type;
            promotion.PromotionProducts = ReadPromotionProduct();
            promotion.Promotion_Site = Promotion_Site;

            if (Mypromotion.EditPromotion(promotion, Public.GetUserPrivilege()))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_list.aspx");
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            } 
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        } 
    }

    //读取产品编号
    public IList<PromotionProductInfo> ReadPromotionProduct()
    {
        string product_id = tools.CheckStr(Request.Form["favor_productid"]);
        string[] ProductIDArr = product_id.Split(',');

        int icount = 0;

        IList<PromotionProductInfo> entityList = new List<PromotionProductInfo>();
        PromotionProductInfo product = null;
        foreach (string pid in ProductIDArr)
        {
            if (tools.CheckInt(pid) > 0)
            {
                product = new PromotionProductInfo();
                product.Promotion_Product_Product_ID = tools.CheckInt(pid);
                entityList.Add(product);
            }
            icount++;
        }

        if (entityList.Count == 0) { entityList = null; }

        return entityList;
    }

    //读取产品编号
    public IList<PromotionProductInfo> ReadPromotionPolicyProduct()
    {
        string policy_id = tools.CheckStr(Request.Form["favor_policy"]);
        string[] PolicyIDArr = policy_id.Split(',');
        IList<string> productids = null;
        string product_id = "0";
        string newproduct_id="";

        int icount = 0;

        IList<PromotionProductInfo> entityList = new List<PromotionProductInfo>();
        PromotionProductInfo product = null;
        foreach (string pid in PolicyIDArr)
        {
            if (tools.CheckInt(pid) > 0)
            {
                productids = MyFavor.GetPolicyProducts(tools.CheckInt(pid), Public.GetCurrentSite());

                if (productids != null)
                {
                    foreach (string subproduct in productids)
                    {
                        product_id = product_id + subproduct.Split('|')[1];
                    }
                }
            }
            icount++;
        }

        newproduct_id= MyFavor.Get_DistinctProductID(product_id);
        string[] ProductIDArr = newproduct_id.Split(',');
        foreach (string pid in ProductIDArr)
        {
            if (tools.CheckInt(pid) > 0)
            {
                product = new PromotionProductInfo();
                product.Promotion_Product_Product_ID = tools.CheckInt(pid);
                entityList.Add(product);
            }
            icount++;
        }

        if (entityList.Count == 0) { entityList = null; }

        return entityList;
    }

    //读取批发分组编号
    public IList<PromotionProductInfo> ReadWholeSaleGroupProduct()
    {
        string group_id = tools.CheckStr(Request.Form["group_id"]);
        string[] PolicyIDArr = group_id.Split(',');
        IList<string> productids = null;
        string product_id = "0";
        string newproduct_id = "";

        int icount = 0;
        if (group_id.Length == 0)
        {
            group_id = "-1";
        }
        IList<PromotionProductInfo> entityList = new List<PromotionProductInfo>();
        PromotionProductInfo product;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionWholeSaleInfo.Promotion_WholeSale_Site", "=", Public.GetCurrentSite()));

        Query.ParamInfos.Add(new ParamInfo("And", "str", "PromotionWholeSaleInfo.Promotion_WholeSale_GroupID", "in", group_id));
        IList<PromotionWholeSaleInfo> entitys = MyWholeSale.GetPromotionWholeSales(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (PromotionWholeSaleInfo entity in entitys)
            {
                if (entity.Promotion_WholeSale_ProductID > 0)
                {
                    product = new PromotionProductInfo();
                    product.Promotion_Product_Product_ID = entity.Promotion_WholeSale_ProductID;
                    entityList.Add(product);
                }
            }
        }

        if (entityList.Count == 0) { entityList = null; }

        return entityList;
    }

    //读取限时分组编号
    public IList<PromotionProductInfo> ReadLimitGroupProduct()
    {
        string group_id = tools.CheckStr(Request.Form["group_id"]);
        string[] PolicyIDArr = group_id.Split(',');
        IList<string> productids = null;
        string product_id = "0";
        string newproduct_id = "";

        int icount = 0;
        if (group_id.Length == 0)
        {
            group_id = "-1";
        }
        IList<PromotionProductInfo> entityList = new List<PromotionProductInfo>();
        PromotionProductInfo product = null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionLimitInfo.Promotion_Limit_Site", "=", Public.GetCurrentSite()));

        Query.ParamInfos.Add(new ParamInfo("And", "str", "PromotionLimitInfo.Promotion_Limit_GroupID", "in", group_id));
        IList<PromotionLimitInfo> entitys = MyLimit.GetPromotionLimits (Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (PromotionLimitInfo entity in entitys)
            {
                if (entity.Promotion_Limit_ProductID > 0)
                {
                    product = new PromotionProductInfo();
                    product.Promotion_Product_Product_ID = entity.Promotion_Limit_ProductID;
                    entityList.Add(product);
                }
            }
        }

        if (entityList.Count == 0) { entityList = null; }

        return entityList;
    }

    //获取促销专题列表
    public virtual string GetPromotions()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        if (tools.CheckInt(Request["page"]) == 0)
        {
            Query.CurrentPage = 1;
        }
        else
        {
            Query.CurrentPage = tools.CheckInt(Request["page"]);
        
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionInfo.Promotion_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = Mypromotion.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<PromotionInfo> entitys = Mypromotion.GetPromotions(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (PromotionInfo entity in entitys)
            {
                jsonBuilder.Append("{\"Promotion.Promotion_ID\":" + entity.Promotion_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Title);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Application["site_url"] + "promotion/index.aspx?id=" + entity.Promotion_ID.ToString());
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Addtime.ToString());
                jsonBuilder.Append("\",");

                

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("c0330805-14e6-493e-8519-7ca89dddd157"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"Promotion_Edit.aspx?promotion_id=" + entity.Promotion_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("8638fc66-772f-4981-af7a-94c128e15ed2"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Promotion_do.aspx?action=move&promotion_id=" + entity.Promotion_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    //删除促销专题
    public virtual void DelPromotion()
    {
        int promotion_id = tools.CheckInt(Request["promotion_id"]);
        if (promotion_id > 0)
        {
            Mypromotion.DelPromotion(promotion_id, Public.GetUserPrivilege());
            Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //展示已选择专题
    public string ShowPromotion(IList<PromotionGroupPromotionInfo> promotion_ids)
    {
        string policy_id = "";
        StringBuilder jsonBuilder = new StringBuilder();

        int sort;
        string seled;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionInfo.Promotion_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("PromotionInfo.Promotion_ID", "Desc"));
        IList<PromotionInfo> entitys = Mypromotion.GetPromotions(Query,Public.GetUserPrivilege());
        if (entitys != null)
        {
            jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" width=\"700\" bgcolor=\"#B0CADA\">");
            foreach (PromotionInfo entity in entitys)
            {
                seled = "";
                sort = 0;
                if (promotion_ids != null)
                {
                    foreach (PromotionGroupPromotionInfo ids in promotion_ids)
                    {
                        if (ids.Promotion_Group_Promotion_PromotionID == entity.Promotion_ID)
                        {
                            seled = " checked";
                            sort = ids.Promotion_Group_Promotion_Sort;
                        }
                    }
                }
                jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                jsonBuilder.Append("        <td><input type=\"checkbox\" name=\"promtion_id\" value=\"" + entity.Promotion_ID + "\"" + seled + "></td>");

                jsonBuilder.Append("        <td width=\"100\" align=\"left\">" + entity.Promotion_ID + "</td>");
                jsonBuilder.Append("        <td width=\"300\" align=\"left\">" + entity.Promotion_Title + "</td>");
                jsonBuilder.Append("        <td align=\"left\">" + Application["site_url"] + "promotion/index.aspx?id=" + entity.Promotion_ID.ToString() + "</td>");
                jsonBuilder.Append("        <td width=\"50\" align=\"left\"><input type=\"text\" size=\"5\" name=\"promotion_sort" + entity.Promotion_ID + "\" value=\"" + sort + "\"></td>");

                jsonBuilder.Append("    </tr>");
            }
            jsonBuilder.Append("</table>");
        }
        else
        {
            jsonBuilder.Append("<span class=\"pickertip\">暂无可选限时分组</span>");
        }
        entitys = null;

        return jsonBuilder.ToString();
    }

    public virtual string Get_Promotion_Product_String(IList<PromotionProductInfo> entitys)
    {
        string return_value = "";
        int i = 0;
        if (entitys != null)
        {
            foreach (PromotionProductInfo entity in entitys)
            {
                i = i + 1;
                if (i < entitys.Count)
                {
                    return_value = return_value + entity.Promotion_Product_Product_ID + ",";
                }
                else
                {
                    return_value = return_value + entity.Promotion_Product_Product_ID;
                }
            }

        }
        else
        {
            return_value = "0";
        }
        return return_value;
    }

    //根据编号获取促销专题
    public virtual PromotionInfo GetPromotionByID(int ID)
    {
        return Mypromotion.GetPromotionByID(ID, Public.GetUserPrivilege());
    }

    #endregion

    #region 限时促销

    public virtual void AddPromotionLimitGroup()
    {
        int Promotion_Limit_Group_ID = tools.CheckInt(Request.Form["Promotion_Limit_Group_ID"]);
        string Promotion_Limit_Group_Name = tools.CheckStr(Request.Form["Promotion_Limit_Group_Name"]);
        string Promotion_Limit_Group_Site = Public.GetCurrentSite();
        if (Promotion_Limit_Group_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写分组名称", false, "{back}");
        }
        PromotionLimitGroupInfo entity = new PromotionLimitGroupInfo();
        entity.Promotion_Limit_Group_ID = Promotion_Limit_Group_ID;
        entity.Promotion_Limit_Group_Name = Promotion_Limit_Group_Name;
        entity.Promotion_Limit_Group_Site = Promotion_Limit_Group_Site;

        if (MyLimit.AddPromotionLimitGroup(entity))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_Limit_Group_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditPromotionLimitGroup()
    {

        int Promotion_Limit_Group_ID = tools.CheckInt(Request.Form["Promotion_Limit_Group_ID"]);
        string Promotion_Limit_Group_Name = tools.CheckStr(Request.Form["Promotion_Limit_Group_Name"]);
        string Promotion_Limit_Group_Site = Public.GetCurrentSite();
        if (Promotion_Limit_Group_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写分组名称", false, "{back}");
        }

        PromotionLimitGroupInfo entity = GetPromotionLimitGroupByID(Promotion_Limit_Group_ID);
        if (entity != null)
        {
            entity.Promotion_Limit_Group_ID = Promotion_Limit_Group_ID;
            entity.Promotion_Limit_Group_Name = Promotion_Limit_Group_Name;
            entity.Promotion_Limit_Group_Site = Promotion_Limit_Group_Site;


            if (MyLimit.EditPromotionLimitGroup(entity))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_Limit_Group_list.aspx");
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
        
    }

    public virtual void DelPromotionLimitGroup()
    {
        int Promotion_Limit_Group_ID = tools.CheckInt(Request["Promotion_Limit_Group_ID"]);
        if (MyLimit.DelPromotionLimitGroup(Promotion_Limit_Group_ID) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_Limit_Group_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual PromotionLimitGroupInfo GetPromotionLimitGroupByID(int cate_id)
    {
        return MyLimit.GetPromotionLimitGroupByID(cate_id);
    }

    //获取限时促销列表
    public virtual string GetPromotionLimitGroups()
    {
        string product_id = "0";
        string keyword = tools.CheckStr(Request["keyword"]);
        QueryInfo Query = new QueryInfo();
        ProductInfo product = null;
        Query.PageSize = tools.CheckInt(Request["rows"]);
        if (tools.CheckInt(Request["page"]) == 0)
        {
            Query.CurrentPage = 1;
        }
        else
        {
            Query.CurrentPage = tools.CheckInt(Request["page"]);

        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionLimitGroupInfo.Promotion_Limit_Group_Site", "=", Public.GetCurrentSite()));
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionLimitGroupInfo.Promotion_Limit_Group_Name", "like", keyword));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyLimit.GetGroupPageInfo(Query);
        IList<PromotionLimitGroupInfo> entitys = MyLimit.GetPromotionLimitGroups(Query);
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (PromotionLimitGroupInfo entity in entitys)
            {

                jsonBuilder.Append("{\"PromotionLimitGroupInfo.Promotion_Limit_Group_ID\":" + entity.Promotion_Limit_Group_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Limit_Group_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Limit_Group_Name);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("34b7b99f-451c-4c0b-8da1-e3ba000891a8"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"Promotion_limit_group_Edit.aspx?Promotion_Limit_Group_ID=" + entity.Promotion_Limit_Group_ID  + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("470c7741-f942-42df-9973-c36555c8d2e6"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Promotion_Limit_group_do.aspx?action=move&Promotion_Limit_Group_ID=" + entity.Promotion_Limit_Group_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    public virtual void GetPromotionLimitGroups(int Seled_ID)
    {
        QueryInfo Query = new QueryInfo();
        ProductInfo product = null;
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Response.Write("<select name=\"Promotion_Limit_Group\">");
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionLimitGroupInfo.Promotion_Limit_Group_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("PromotionLimitGroupInfo.Promotion_Limit_Group_ID", "Desc"));
        IList<PromotionLimitGroupInfo> entitys = MyLimit.GetPromotionLimitGroups(Query);
        if (entitys != null)
        {
            foreach (PromotionLimitGroupInfo entity in entitys)
            {
                if (Seled_ID == entity.Promotion_Limit_Group_ID)
                {
                    Response.Write("<option value=\"" + entity.Promotion_Limit_Group_ID + "\" selected>" + entity.Promotion_Limit_Group_Name + "</option>");
                }
                else
                {
                    Response.Write("<option value=\"" + entity.Promotion_Limit_Group_ID + "\">" + entity.Promotion_Limit_Group_Name + "</option>");
                }
            }
        }
        else
        {
            Response.Write("<option value=\"0\">选择分组</option>");
        }
        Response.Write("</select>");
    }

    //展示选择产品
    public string Limit_ShowProduct()
    {
        int del_product = tools.CheckInt(Request["pid"]);
        string product_id="0";
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"list_table_bg\">");
        jsonBuilder.Append("    <tr class=\"list_head_bg\">");
        jsonBuilder.Append("        <td width=\"60\"></td>");
        jsonBuilder.Append("        <td>商品编码</td>");
        jsonBuilder.Append("        <td>商品名称</td>");
        jsonBuilder.Append("        <td>规格</td>");
        jsonBuilder.Append("        <td>生产企业</td>");
        jsonBuilder.Append("        <td>本站价格</td>");
        jsonBuilder.Append("        <td>限时价格</td>");
        //jsonBuilder.Append("        <td>促销数量</td>");
        jsonBuilder.Append("        <td>每单限购数量</td>");
        jsonBuilder.Append("    </tr>");

        string Product_ID = tools.NullStr(Session["selected_productid"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        if (Product_ID == "")
        {
            Product_ID = "0";
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", Product_ID));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "Desc"));
        IList<ProductInfo> entityList = MyProduct.GetProductList(Query, Public.GetUserPrivilege());
        if (entityList != null)
        {
            foreach (ProductInfo entity in entityList)
            {
                if (del_product != entity.Product_ID)
                {
                    product_id = product_id + "," + entity.Product_ID;
                    jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                    jsonBuilder.Append("        <td><input type=\"hidden\" name=\"product_id\" value=\"" + entity.Product_ID + "\"><a href=\"javascript:picker_limitproduct_del(" + entity.Product_ID + ");\"><img src=\"/images/btn_move.gif\" border=\"0\" alt=\"删除\"></a></td>");

                    jsonBuilder.Append("        <td align=\"left\">" + tools.NullStr(entity.Product_Code) + "</td>");
                    jsonBuilder.Append("        <td align=\"left\">" + tools.NullStr(entity.Product_Name) + "</td>");
                    jsonBuilder.Append("        <td align=\"center\">" + tools.NullStr(entity.Product_Spec) + "</td>");
                    jsonBuilder.Append("        <td align=\"center\">" + tools.NullStr(entity.Product_Maker) + "</td>");
                    jsonBuilder.Append("        <td align=\"center\">" + tools.NullDbl(entity.Product_Price) + "</td>");
                    jsonBuilder.Append("        <td align=\"center\"><input type=\"text\" name=\"limit_price" + entity.Product_ID + "\"></td>");
                    //jsonBuilder.Append("        <td align=\"center\"><input type=\"text\" name=\"limit_amount" + entity.Product_ID + "\"></td>");
                    jsonBuilder.Append("        <td align=\"center\"><input type=\"text\" name=\"limit_limit" + entity.Product_ID + "\"></td>");
                    jsonBuilder.Append("    </tr>");
                }
            }
            Session["selected_productid"] = product_id;
            entityList = null;
        }
        jsonBuilder.Append("</table>");
        return jsonBuilder.ToString();
    }

    public virtual void AddPromotionLimit()
    {
        int Promotion_Limit_ID = 0;
        string product_id = tools.CheckStr(Request.Form["favor_productid"]);
        string[] ProductIDArr = product_id.Split(',');
        int Promotion_Limit_GroupID = tools.CheckInt(Request["Promotion_Limit_Group"]);
        DateTime Promotion_Limit_Starttime = tools.NullDate(Request.Form["Promotion_Limit_Starttime"]);
        DateTime Promotion_Limit_Endtime = tools.NullDate(Request.Form["Promotion_Limit_Endtime"]);
        string favor_gradeid = tools.CheckStr(Request.Form["Member_Grade"]);
        if (favor_gradeid.Length == 0)
        {
            Public.Msg("error", "错误信息", "请选择针对会员！", false, "{back}");
        }
        if (tools.NullDate(Promotion_Limit_Starttime).Year < 1900 || tools.NullDate(Promotion_Limit_Endtime).Year < 1900)
        {
            Public.Msg("error", "错误信息", "时间设置错误", false, "{back}");
        }
        if (product_id.Length==0)
        {
            Public.Msg("error", "错误信息", "请选择产品信息", false, "{back}");
        }

        PromotionLimitInfo entity = null;
        foreach (string subid in ProductIDArr)
        {
            entity = new PromotionLimitInfo();
            entity.Promotion_Limit_ID = Promotion_Limit_ID;
            entity.Promotion_Limit_GroupID = Promotion_Limit_GroupID;
            entity.Promotion_Limit_ProductID = int.Parse(subid);
            entity.Promotion_Limit_Price = tools.CheckFloat(Request.Form["limit_price" + subid]);
            entity.Promotion_Limit_Amount = tools.CheckInt(Request.Form["limit_amount" + subid]);
            entity.Promotion_Limit_Limit = tools.CheckInt(Request.Form["limit_limit" + subid]);
            entity.Promotion_Limit_Starttime = Promotion_Limit_Starttime;
            entity.Promotion_Limit_Endtime = Promotion_Limit_Endtime;
            entity.Promotion_Limit_Site = Public.GetCurrentSite();
            if(MyLimit.AddPromotionLimit(entity, Public.GetUserPrivilege()))
            {
                entity = null;
                entity = MyLimit.GetLastPromotionLimit(Public.GetUserPrivilege());
                if (entity.Promotion_Limit_ProductID == int.Parse(subid))
                {
                    //添加适用会员等级
                    PromotionLimitMemberGradeInfo limitgrade;
                    if (favor_gradeid.Length == 0)
                    {
                        limitgrade = new PromotionLimitMemberGradeInfo();
                        limitgrade.Promotion_Limit_MemberGrade_ID = 0;
                        limitgrade.Promotion_Limit_MemberGrade_Grade = 0;
                        limitgrade.Promotion_Limit_MemberGrade_LimitID = entity.Promotion_Limit_ID;
                        MyLimit.AddPromotionLimitMemberGrade(limitgrade);
                        limitgrade = null;
                    }
                    else
                    {
                        foreach (string substr in favor_gradeid.Split(','))
                        {
                            if (tools.CheckInt(substr) > 0)
                            {
                                limitgrade = new PromotionLimitMemberGradeInfo();
                                limitgrade.Promotion_Limit_MemberGrade_ID = 0;
                                limitgrade.Promotion_Limit_MemberGrade_Grade = tools.CheckInt(substr);
                                limitgrade.Promotion_Limit_MemberGrade_LimitID = entity.Promotion_Limit_ID;
                                MyLimit.AddPromotionLimitMemberGrade(limitgrade);
                                limitgrade = null;
                            }
                        }
                    }
                }
            }
        }
        Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_Limit_add.aspx");
    }

    public virtual void EditPromotionLimit()
    {

        int Promotion_Limit_ID = tools.CheckInt(Request.Form["Promotion_Limit_ID"]);
        int Promotion_Limit_GroupID = tools.CheckInt(Request["Promotion_Limit_Group"]);
        int Promotion_Limit_ProductID = tools.CheckInt(Request.Form["Promotion_Limit_ProductID"]);
        double Promotion_Limit_Price = tools.CheckFloat(Request.Form["Promotion_Limit_Price"]);
        int Promotion_Limit_Amount = tools.CheckInt(Request.Form["Promotion_Limit_Amount"]);
        int Promotion_Limit_Limit = tools.CheckInt(Request.Form["Promotion_Limit_Limit"]);
        DateTime Promotion_Limit_Starttime = tools.NullDate(Request.Form["Promotion_Limit_Starttime"]);
        DateTime Promotion_Limit_Endtime = tools.NullDate(Request.Form["Promotion_Limit_Endtime"]);
        string favor_gradeid = tools.CheckStr(Request.Form["Member_Grade"]);
        if (favor_gradeid.Length == 0)
        {
            Public.Msg("error", "错误信息", "请选择针对会员！", false, "{back}");
        }
        if (tools.NullDate(Promotion_Limit_Starttime).Year < 1900 || tools.NullDate(Promotion_Limit_Endtime).Year < 1900)
        {
            Public.Msg("error", "错误信息", "时间设置错误", false, "{back}");
        }
        PromotionLimitInfo entity = MyLimit.GetPromotionLimitByID(Promotion_Limit_ID,Public.GetUserPrivilege());
        if (entity != null)
        {
            entity.Promotion_Limit_ID = Promotion_Limit_ID;
            entity.Promotion_Limit_GroupID = Promotion_Limit_GroupID;
            entity.Promotion_Limit_ProductID = Promotion_Limit_ProductID;
            entity.Promotion_Limit_Price = Promotion_Limit_Price;
            entity.Promotion_Limit_Amount = Promotion_Limit_Amount;
            entity.Promotion_Limit_Limit = Promotion_Limit_Limit;
            entity.Promotion_Limit_Starttime = Promotion_Limit_Starttime;
            entity.Promotion_Limit_Endtime = Promotion_Limit_Endtime;
            entity.Promotion_Limit_Site = Public.GetCurrentSite();

            if (MyLimit.EditPromotionLimit(entity, Public.GetUserPrivilege()))
            {
                MyLimit.DelPromotionLimitMemberGrade(entity.Promotion_Limit_ID);
                //添加适用会员等级
                PromotionLimitMemberGradeInfo limitgrade;
                if (favor_gradeid.Length == 0)
                {
                    limitgrade = new PromotionLimitMemberGradeInfo();
                    limitgrade.Promotion_Limit_MemberGrade_ID = 0;
                    limitgrade.Promotion_Limit_MemberGrade_Grade = 0;
                    limitgrade.Promotion_Limit_MemberGrade_LimitID = entity.Promotion_Limit_ID;
                    MyLimit.AddPromotionLimitMemberGrade(limitgrade);
                    limitgrade = null;
                }
                else
                {
                    foreach (string substr in favor_gradeid.Split(','))
                    {
                        if (tools.CheckInt(substr) > 0)
                        {
                            limitgrade = new PromotionLimitMemberGradeInfo();
                            limitgrade.Promotion_Limit_MemberGrade_ID = 0;
                            limitgrade.Promotion_Limit_MemberGrade_Grade = tools.CheckInt(substr);
                            limitgrade.Promotion_Limit_MemberGrade_LimitID = entity.Promotion_Limit_ID;
                            MyLimit.AddPromotionLimitMemberGrade(limitgrade);
                            limitgrade = null;
                        }
                    }
                }
                Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_Limit_list.aspx");
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void DelPromotionLimit()
    {
        int Promotion_Limit_ID = tools.CheckInt(Request.QueryString["Promotion_Limit_ID"]);
        if (MyLimit.DelPromotionLimit(Promotion_Limit_ID, Public.GetUserPrivilege()) > 0)
        {
            MyLimit.DelPromotionLimitMemberGrade(Promotion_Limit_ID);
            Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_Limit_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual PromotionLimitInfo GetPromotionLimitByID(int cate_id)
    {
        return MyLimit.GetPromotionLimitByID(cate_id, Public.GetUserPrivilege());
    }

    public virtual string Get_Limit_MemberGrade_String(IList<PromotionLimitMemberGradeInfo> entitys)
    {
        string return_value = "";
        int i = 0;
        if (entitys != null)
        {
            foreach (PromotionLimitMemberGradeInfo entity in entitys)
            {
                i = i + 1;
                if (i < entitys.Count)
                {
                    return_value = return_value + entity.Promotion_Limit_MemberGrade_Grade + ",";
                }
                else
                {
                    return_value = return_value + entity.Promotion_Limit_MemberGrade_Grade;
                }
            }
        }
        else
        {
            return_value = "0";
        }
        return return_value;
    }

    public virtual void Member_Grade_Check(string Check_Name,string Checked_Value)
    {
        Checked_Value = "," + Checked_Value + ",";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberGradeInfo.Member_Grade_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("MemberGradeInfo.Member_Grade_ID", "Asc"));
        IList<MemberGradeInfo> entitys = MyGrade.GetMemberGrades(Query,Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (MemberGradeInfo entity in entitys)
            {
                if (Checked_Value.IndexOf("," + entity.Member_Grade_ID.ToString() + ",") >= 0 || Checked_Value==",0,")
                {
                    Response.Write("<input type=\"checkbox\" name=\"" + Check_Name + "\" value=\"" + entity.Member_Grade_ID + "\" checked onclick=\"check_membergrade('" + Check_Name + "')\"> " + entity.Member_Grade_Name + " &nbsp; ");
                }
                else
                {
                    Response.Write("<input type=\"checkbox\" name=\"" + Check_Name + "\" value=\"" + entity.Member_Grade_ID + "\" onclick=\"check_membergrade('" + Check_Name + "')\"> " + entity.Member_Grade_Name + " &nbsp; ");
                }
            }
        }
    }

    //获取限时促销列表
    public virtual string GetPromotionLimits()
    {
        string product_id = "0";
        string group_id = "0";
        int cur_group;
        cur_group = tools.CheckInt(Request["group_id"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        int Status = tools.CheckInt(Request["Status"]);
        if (keyword.Length > 0)
        {
            QueryInfo Query1 = new QueryInfo();
            Query1.PageSize = 0;
            Query1.CurrentPage = 1;
            Query1.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
            Query1.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_Name", "like", keyword));
            Query1.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_Code", "like", keyword));

            IList<ProductInfo> pentitys = MyProduct.GetProducts(Query1, Public.GetUserPrivilege());
            if (pentitys != null)
            {
                foreach (ProductInfo pentity in pentitys)
                {
                    product_id = product_id + "," + pentity.Product_ID;
                }
            }
            Query1 = null;
            Query1 = new QueryInfo();
            Query1.PageSize = 0;
            Query1.CurrentPage = 1;
            Query1.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionLimitGroupInfo.Promotion_Limit_Group_Site", "=", Public.GetCurrentSite()));
            Query1.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionLimitGroupInfo.Promotion_Limit_Group_Name", "like", keyword));

            IList<PromotionLimitGroupInfo> groupinfo = MyLimit.GetPromotionLimitGroups(Query1);
            if (groupinfo != null)
            {
                foreach (PromotionLimitGroupInfo group in groupinfo)
                {
                    group_id = group_id + "," + group.Promotion_Limit_Group_ID;
                }
            }
        }




        QueryInfo Query = new QueryInfo();
        ProductInfo product = null;
        Query.PageSize = tools.CheckInt(Request["rows"]);
        if (tools.CheckInt(Request["page"]) == 0)
        {
            Query.CurrentPage = 1;
        }
        else
        {
            Query.CurrentPage = tools.CheckInt(Request["page"]);

        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionLimitInfo.Promotion_Limit_Site", "=", Public.GetCurrentSite()));

        if (group_id != "0")
        {
            if (product_id != "0")
            {
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "PromotionLimitInfo.Promotion_Limit_ProductID", "in", product_id));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "PromotionLimitInfo.Promotion_Limit_GroupID", "in", group_id));
            }
            else
            {
                Query.ParamInfos.Add(new ParamInfo("And", "str", "PromotionLimitInfo.Promotion_Limit_GroupID", "in", group_id));
            }

        }
        else
        {
            if (product_id != "0")
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionLimitInfo.Promotion_Limit_ProductID", "in", product_id));
            }
        }
        if (cur_group > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("And", "str", "PromotionLimitInfo.Promotion_Limit_GroupID", "=", cur_group.ToString()));
        }
        if (Status == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "Datediff(d,{PromotionLimitInfo.Promotion_Limit_Starttime},GetDATE())", "<", "0"));
        }
        if (Status == 2)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "Datediff(d,{PromotionLimitInfo.Promotion_Limit_Starttime},GetDATE())", ">=", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "Datediff(d,{PromotionLimitInfo.Promotion_Limit_Endtime},GetDATE())", "<=", "0"));
        }
        if (Status == 3)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "Datediff(d,{PromotionLimitInfo.Promotion_Limit_Endtime},GetDATE())", ">", "0"));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyLimit.GetPageInfo(Query, Public.GetUserPrivilege());
        SupplierInfo supplierinfo;
        IList<PromotionLimitInfo> entitys = MyLimit.GetPromotionLimits(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (PromotionLimitInfo entity in entitys)
            {

                jsonBuilder.Append("{\"id\":" + entity.Promotion_Limit_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Limit_ID);
                jsonBuilder.Append("\",");

                supplierinfo = supplier.GetSupplierByID(entity.Promotion_Limit_GroupID);
                if (supplierinfo != null)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(supplierinfo.Supplier_CompanyName);
                    jsonBuilder.Append("\",");
                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");
                }

                product = MyBLLPRO.GetProductByID(entity.Promotion_Limit_ProductID, Public.GetUserPrivilege());
                if (product != null)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(product.Product_Code);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(product.Product_Name);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(product.Product_Spec);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(product.Product_Maker);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(product.Product_Price);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(product.Product_Sort);
                    jsonBuilder.Append("\",");


                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");
                }

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Limit_Price);
                jsonBuilder.Append("\",");

                //jsonBuilder.Append("\"");
                //jsonBuilder.Append(entity.Promotion_Limit_Amount);
                //jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Limit_Starttime.ToString());
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Limit_Endtime.ToString());
                jsonBuilder.Append("\",");



                //jsonBuilder.Append("\"");

                //if (Public.CheckPrivilege("34b7b99f-451c-4c0b-8da1-e3ba000891a8"))
                //{
                //    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"Promotion_limit_Edit.aspx?Promotion_Limit_ID=" + entity.Promotion_Limit_ID + "\\\" title=\\\"修改\\\">修改</a>");
                //}
                //if (Public.CheckPrivilege("470c7741-f942-42df-9973-c36555c8d2e6"))
                //{
                //    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Promotion_Limit_do.aspx?action=move&Promotion_Limit_ID=" + entity.Promotion_Limit_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                //}

                //jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    public void LimitProduct_EditSortList()
    {
        int Cate_ID = tools.CheckInt(Request["id"]);
        int Cate_Sort = tools.CheckInt(Request["PromotionLimitInfo.Promotion_Limit_Sort"]);
        PromotionLimitInfo limitinfo = GetPromotionLimitByID(Cate_ID);
        if (limitinfo != null)
        {
            ProductInfo entity = product.GetProductByID(limitinfo.Promotion_Limit_ProductID);
            if (entity != null)
            {
                if (entity.Product_Sort != Cate_Sort)
                {
                    entity.Product_Sort = Cate_Sort;
                    MyProduct.EditProductInfo(entity, Public.GetUserPrivilege());
                }
            }
        }
    }

    //获取限时促销列表
    public virtual string GetPromotionLimits_bak()
    {
        string product_id = "0";
        string group_id = "0";
        int cur_group;
        cur_group = tools.CheckInt(Request["group_id"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        int Status = tools.CheckInt(Request["Status"]);
        if (keyword.Length > 0)
        {
            QueryInfo Query1 = new QueryInfo();
            Query1.PageSize = 0;
            Query1.CurrentPage = 1;
            Query1.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
            Query1.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_Name", "like", keyword));
            Query1.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_Code", "like", keyword));
            
            IList<ProductInfo> pentitys = MyProduct.GetProducts(Query1, Public.GetUserPrivilege());
            if (pentitys != null)
            {
                foreach (ProductInfo pentity in pentitys)
                {
                    product_id = product_id + "," + pentity.Product_ID;
                }
            }
            Query1 = null;
            Query1 = new QueryInfo();
            Query1.PageSize = 0;
            Query1.CurrentPage = 1;
            Query1.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionLimitGroupInfo.Promotion_Limit_Group_Site", "=", Public.GetCurrentSite()));
            Query1.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionLimitGroupInfo.Promotion_Limit_Group_Name", "like", keyword));

            IList<PromotionLimitGroupInfo> groupinfo = MyLimit.GetPromotionLimitGroups(Query1);
            if (groupinfo != null)
            {
                foreach (PromotionLimitGroupInfo group in groupinfo)
                {
                    group_id = group_id + "," + group.Promotion_Limit_Group_ID;
                }
            }
        }
        



        QueryInfo Query = new QueryInfo();
        ProductInfo product = null;
        Query.PageSize = tools.CheckInt(Request["rows"]);
        if (tools.CheckInt(Request["page"]) == 0)
        {
            Query.CurrentPage = 1;
        }
        else
        {
            Query.CurrentPage = tools.CheckInt(Request["page"]);

        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionLimitInfo.Promotion_Limit_Site", "=", Public.GetCurrentSite()));

        if (group_id != "0")
        {
            if (product_id != "0")
            {
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "PromotionLimitInfo.Promotion_Limit_ProductID", "in", product_id));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "PromotionLimitInfo.Promotion_Limit_GroupID", "in", group_id));
            }
            else
            {
                Query.ParamInfos.Add(new ParamInfo("And", "str", "PromotionLimitInfo.Promotion_Limit_GroupID", "in", group_id));
            }
            
        }
        else
        {
            if (product_id != "0")
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionLimitInfo.Promotion_Limit_ProductID", "in", product_id));
            }
        }
        if (cur_group > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("And", "str", "PromotionLimitInfo.Promotion_Limit_GroupID", "=", cur_group.ToString()));
        }
        if (Status == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "Datediff(d,{PromotionLimitInfo.Promotion_Limit_Starttime},GetDATE())", "<", "0"));
        }
        if (Status == 2)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "Datediff(d,{PromotionLimitInfo.Promotion_Limit_Starttime},GetDATE())", ">=", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "Datediff(d,{PromotionLimitInfo.Promotion_Limit_Endtime},GetDATE())", "<=", "0"));
        }
        if (Status == 3)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "Datediff(d,{PromotionLimitInfo.Promotion_Limit_Endtime},GetDATE())", ">", "0"));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyLimit.GetPageInfo(Query, Public.GetUserPrivilege());
        PromotionLimitGroupInfo limitgroup;
        IList<PromotionLimitInfo> entitys = MyLimit.GetPromotionLimits(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (PromotionLimitInfo entity in entitys)
            {
                
                jsonBuilder.Append("{\"PromotionLimitInfo.Promotion_Limit_ID\":" + entity.Promotion_Limit_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Limit_ID);
                jsonBuilder.Append("\",");

                limitgroup = GetPromotionLimitGroupByID(entity.Promotion_Limit_GroupID);
                if (limitgroup != null)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(limitgroup.Promotion_Limit_Group_Name);
                    jsonBuilder.Append("\",");
                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");
                }

                product = MyBLLPRO.GetProductByID(entity.Promotion_Limit_ProductID, Public.GetUserPrivilege());
                if (product != null)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(product.Product_Code);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(product.Product_Name);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(product.Product_Spec);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(product.Product_Maker);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(product.Product_Price);
                    jsonBuilder.Append("\",");

                    
                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");
                }

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Limit_Price);
                jsonBuilder.Append("\",");

                //jsonBuilder.Append("\"");
                //jsonBuilder.Append(entity.Promotion_Limit_Amount);
                //jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Limit_Starttime.ToString());
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Limit_Endtime.ToString());
                jsonBuilder.Append("\",");



                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("34b7b99f-451c-4c0b-8da1-e3ba000891a8"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"Promotion_limit_Edit.aspx?Promotion_Limit_ID=" + entity.Promotion_Limit_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("470c7741-f942-42df-9973-c36555c8d2e6"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Promotion_Limit_do.aspx?action=move&Promotion_Limit_ID=" + entity.Promotion_Limit_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    #endregion

    #region 批发促销

    public virtual void AddPromotionWholeSaleGroup()
    {
        int Promotion_WholeSale_Group_ID = tools.CheckInt(Request.Form["Promotion_WholeSale_Group_ID"]);
        string Promotion_WholeSale_Group_Name = tools.CheckStr(Request.Form["Promotion_WholeSale_Group_Name"]);
        string Promotion_WholeSale_Group_Site = Public.GetCurrentSite();
        if (Promotion_WholeSale_Group_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写分组名称", false, "{back}");
        }
        PromotionWholeSaleGroupInfo entity = new PromotionWholeSaleGroupInfo();
        entity.Promotion_WholeSale_Group_ID = Promotion_WholeSale_Group_ID;
        entity.Promotion_WholeSale_Group_Name = Promotion_WholeSale_Group_Name;
        entity.Promotion_WholeSale_Group_Site = Promotion_WholeSale_Group_Site;

        if (MyWholeSale.AddPromotionWholeSaleGroup(entity))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_WholeSale_Group_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditPromotionWholeSaleGroup()
    {

        int Promotion_WholeSale_Group_ID = tools.CheckInt(Request.Form["Promotion_WholeSale_Group_ID"]);
        string Promotion_WholeSale_Group_Name = tools.CheckStr(Request.Form["Promotion_WholeSale_Group_Name"]);
        string Promotion_WholeSale_Group_Site = Public.GetCurrentSite();
        if (Promotion_WholeSale_Group_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写分组名称", false, "{back}");
        }

        PromotionWholeSaleGroupInfo entity = GetPromotionWholeSaleGroupByID(Promotion_WholeSale_Group_ID);
        if (entity != null)
        {
            entity.Promotion_WholeSale_Group_ID = Promotion_WholeSale_Group_ID;
            entity.Promotion_WholeSale_Group_Name = Promotion_WholeSale_Group_Name;
            entity.Promotion_WholeSale_Group_Site = Promotion_WholeSale_Group_Site;


            if (MyWholeSale.EditPromotionWholeSaleGroup(entity))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_WholeSale_Group_list.aspx");
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }

    }

    public virtual void DelPromotionWholeSaleGroup()
    {
        int Promotion_WholeSale_Group_ID = tools.CheckInt(Request["Promotion_WholeSale_Group_ID"]);
        if (MyWholeSale.DelPromotionWholeSaleGroup(Promotion_WholeSale_Group_ID) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_WholeSale_Group_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual PromotionWholeSaleGroupInfo GetPromotionWholeSaleGroupByID(int cate_id)
    {
        return MyWholeSale.GetPromotionWholeSaleGroupByID(cate_id);
    }

    //获取批发促销列表
    public virtual string GetPromotionWholeSaleGroups()
    {
        string product_id = "0";
        string keyword = tools.CheckStr(Request["keyword"]);
        QueryInfo Query = new QueryInfo();
        ProductInfo product = null;
        Query.PageSize = tools.CheckInt(Request["rows"]);
        if (tools.CheckInt(Request["page"]) == 0)
        {
            Query.CurrentPage = 1;
        }
        else
        {
            Query.CurrentPage = tools.CheckInt(Request["page"]);

        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionWholeSaleGroupInfo.Promotion_WholeSale_Group_Site", "=", Public.GetCurrentSite()));
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionWholeSaleGroupInfo.Promotion_WholeSale_Group_Name", "like", keyword));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyWholeSale.GetGroupPageInfo(Query);
        IList<PromotionWholeSaleGroupInfo> entitys = MyWholeSale.GetPromotionWholeSaleGroups(Query);
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (PromotionWholeSaleGroupInfo entity in entitys)
            {

                jsonBuilder.Append("{\"PromotionWholeSaleGroupInfo.Promotion_WholeSale_Group_ID\":" + entity.Promotion_WholeSale_Group_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_WholeSale_Group_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_WholeSale_Group_Name);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("34b7b99f-451c-4c0b-8da1-e3ba000891a8"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"Promotion_WholeSale_group_Edit.aspx?Promotion_WholeSale_Group_ID=" + entity.Promotion_WholeSale_Group_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("470c7741-f942-42df-9973-c36555c8d2e6"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Promotion_WholeSale_group_do.aspx?action=move&Promotion_WholeSale_Group_ID=" + entity.Promotion_WholeSale_Group_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    public virtual void GetPromotionWholeSaleGroups(int Seled_ID)
    {
        QueryInfo Query = new QueryInfo();
        ProductInfo product = null;
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Response.Write("<select name=\"Promotion_WholeSale_Group\">");
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionWholeSaleGroupInfo.Promotion_WholeSale_Group_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("PromotionWholeSaleGroupInfo.Promotion_WholeSale_Group_ID", "Desc"));
        IList<PromotionWholeSaleGroupInfo> entitys = MyWholeSale.GetPromotionWholeSaleGroups(Query);
        if (entitys != null)
        {
            foreach (PromotionWholeSaleGroupInfo entity in entitys)
            {
                if (Seled_ID == entity.Promotion_WholeSale_Group_ID)
                {
                    Response.Write("<option value=\"" + entity.Promotion_WholeSale_Group_ID + "\" selected>" + entity.Promotion_WholeSale_Group_Name + "</option>");
                }
                else
                {
                    Response.Write("<option value=\"" + entity.Promotion_WholeSale_Group_ID + "\">" + entity.Promotion_WholeSale_Group_Name + "</option>");
                }
            }
        }
        else
        {
            Response.Write("<option value=\"0\">选择分组</option>");
        }
        Response.Write("</select>");
    }

    //展示选择产品
    public string WholeSale_ShowProduct()
    {
        int del_product = tools.CheckInt(Request["pid"]);
        string product_id = "0";
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"list_table_bg\">");
        jsonBuilder.Append("    <tr class=\"list_head_bg\">");
        jsonBuilder.Append("        <td width=\"60\"></td>");
        jsonBuilder.Append("        <td>商品编码</td>");
        jsonBuilder.Append("        <td>商品名称</td>");
        jsonBuilder.Append("        <td>规格</td>");
        jsonBuilder.Append("        <td>生产企业</td>");
        jsonBuilder.Append("        <td>本站价格</td>");
        jsonBuilder.Append("        <td>批发价格</td>");
        jsonBuilder.Append("        <td>最小购买数量</td>");
        jsonBuilder.Append("    </tr>");

        string Product_ID = tools.NullStr(Session["selected_productid"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        if (Product_ID == "")
        {
            Product_ID = "0";
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", Product_ID));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "Desc"));
        IList<ProductInfo> entityList = MyProduct.GetProductList(Query, Public.GetUserPrivilege());
        if (entityList != null)
        {
            foreach (ProductInfo entity in entityList)
            {
                if (del_product != entity.Product_ID)
                {
                    product_id = product_id + "," + entity.Product_ID;
                    jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                    jsonBuilder.Append("        <td><input type=\"hidden\" name=\"product_id\" value=\"" + entity.Product_ID + "\"><a href=\"javascript:picker_groupproduct_del(" + entity.Product_ID + ");\"><img src=\"/images/btn_move.gif\" border=\"0\" alt=\"删除\"></a></td>");

                    jsonBuilder.Append("        <td align=\"left\">" + tools.NullStr(entity.Product_Code) + "</td>");
                    jsonBuilder.Append("        <td align=\"left\">" + tools.NullStr(entity.Product_Name) + "</td>");
                    jsonBuilder.Append("        <td align=\"center\">" + tools.NullStr(entity.Product_Spec) + "</td>");
                    jsonBuilder.Append("        <td align=\"center\">" + tools.NullStr(entity.Product_Maker) + "</td>");
                    jsonBuilder.Append("        <td align=\"center\">" + tools.NullDbl(entity.Product_Price) + "</td>");
                    jsonBuilder.Append("        <td align=\"center\"><input type=\"text\" name=\"wholesale_price" + entity.Product_ID + "\"></td>");
                    jsonBuilder.Append("        <td align=\"center\"><input type=\"text\" name=\"wholesale_minamount" + entity.Product_ID + "\"></td>");
                    jsonBuilder.Append("    </tr>");
                }
            }
            Session["selected_productid"] = product_id;
            entityList = null;
        }
        jsonBuilder.Append("</table>");
        return jsonBuilder.ToString();
    }

    public virtual void AddPromotionWholeSale()
    {
        int Promotion_WholeSale_ID = 0;
        string product_id = tools.CheckStr(Request.Form["product_id"]);
        string[] ProductIDArr = product_id.Split(',');
        if (product_id.Length == 0)
        {
            Public.Msg("error", "错误信息", "请选择产品信息", false, "{back}");
        }

        PromotionWholeSaleInfo entity = null;
        foreach (string subid in ProductIDArr)
        {
            entity = new PromotionWholeSaleInfo();
            entity.Promotion_WholeSale_ID = Promotion_WholeSale_ID;
            entity.Promotion_WholeSale_GroupID = tools.CheckInt(Request.Form["Promotion_WholeSale_Group"]);
            entity.Promotion_WholeSale_ProductID = int.Parse(subid);
            entity.Promotion_WholeSale_Price = tools.CheckFloat(Request.Form["wholesale_price" + subid]);
            entity.Promotion_WholeSale_MinAmount = tools.CheckInt(Request.Form["wholesale_minamount" + subid]);
            entity.Promotion_WholeSale_Site = Public.GetCurrentSite();
            MyWholeSale.AddPromotionWholeSale(entity, Public.GetUserPrivilege());
        }
        Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_WholeSale_add.aspx");
    }

    public virtual void EditPromotionWholeSale()
    {

        int Promotion_WholeSale_ID = tools.CheckInt(Request.Form["Promotion_WholeSale_ID"]);
        int Promotion_WholeSale_GroupID = tools.CheckInt(Request.Form["Promotion_WholeSale_Group"]);
        int Promotion_WholeSale_ProductID = tools.CheckInt(Request.Form["Promotion_WholeSale_ProductID"]);
        double Promotion_WholeSale_Price = tools.CheckFloat(Request.Form["Promotion_WholeSale_Price"]);
        int Promotion_WholeSale_MinAmount = tools.CheckInt(Request.Form["Promotion_WholeSale_MinAmount"]);
        PromotionWholeSaleInfo entity = MyWholeSale.GetPromotionWholeSaleByID(Promotion_WholeSale_ID, Public.GetUserPrivilege());
        if (entity != null)
        {
            entity.Promotion_WholeSale_ProductID = Promotion_WholeSale_ProductID;
            entity.Promotion_WholeSale_GroupID = Promotion_WholeSale_GroupID;
            entity.Promotion_WholeSale_Price = Promotion_WholeSale_Price;
            entity.Promotion_WholeSale_MinAmount = Promotion_WholeSale_MinAmount;
            if (MyWholeSale.EditPromotionWholeSale(entity, Public.GetUserPrivilege()))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_WholeSale_list.aspx");
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }

        
    }

    public virtual void DelPromotionWholeSale()
    {
        int Promotion_WholeSale_ID = tools.CheckInt(Request.QueryString["Promotion_WholeSale_ID"]);
        if (MyWholeSale.DelPromotionWholeSale(Promotion_WholeSale_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_WholeSale_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual PromotionWholeSaleInfo GetPromotionWholeSaleByID(int ID)
    {
        return MyWholeSale.GetPromotionWholeSaleByID(ID, Public.GetUserPrivilege());
    }

    //获取批发促销列表
    public virtual string GetPromotionWholeSales()
    {
        string product_id = "0";
        string group_id = "0";
        int cur_group;
        cur_group = tools.CheckInt(Request["group_id"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        if (keyword.Length > 0)
        {
            QueryInfo Query1 = new QueryInfo();
            Query1.PageSize = 0;
            Query1.CurrentPage = 1;
            Query1.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
            Query1.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_Name", "like", keyword));
            Query1.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_Code", "like", keyword));

            IList<ProductInfo> pentitys = MyProduct.GetProducts(Query1, Public.GetUserPrivilege());
            if (pentitys != null)
            {
                foreach (ProductInfo pentity in pentitys)
                {
                    product_id = product_id + "," + pentity.Product_ID;
                }
            }
            Query1 = null;
            Query1 = new QueryInfo();
            Query1.PageSize = 0;
            Query1.CurrentPage = 1;
            Query1.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionWholeSaleGroupInfo.Promotion_WholeSale_Group_Site", "=", Public.GetCurrentSite()));
            Query1.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionWholeSaleGroupInfo.Promotion_WholeSale_Group_Name", "like", keyword));

            IList<PromotionWholeSaleGroupInfo> groupinfo = MyWholeSale.GetPromotionWholeSaleGroups(Query1);
            if (groupinfo != null)
            {
                foreach (PromotionWholeSaleGroupInfo group in groupinfo)
                {
                    group_id = group_id + "," + group.Promotion_WholeSale_Group_ID;
                }
            }
        }
        




        QueryInfo Query = new QueryInfo();
        ProductInfo product = null;
        Query.PageSize = tools.CheckInt(Request["rows"]);
        if (tools.CheckInt(Request["page"]) == 0)
        {
            Query.CurrentPage = 1;
        }
        else
        {
            Query.CurrentPage = tools.CheckInt(Request["page"]);

        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionWholeSaleInfo.Promotion_WholeSale_Site", "=", Public.GetCurrentSite()));

        if (group_id != "0")
        {
            if (product_id != "0")
            {
                Query.ParamInfos.Add(new ParamInfo("AND(", "str", "PromotionWholeSaleInfo.Promotion_WholeSale_ProductID", "in", product_id));
                Query.ParamInfos.Add(new ParamInfo("OR)", "str", "PromotionWholeSaleInfo.Promotion_WholeSale_GroupID", "in", group_id));
            }
            else
            {
                Query.ParamInfos.Add(new ParamInfo("And", "str", "PromotionWholeSaleInfo.Promotion_WholeSale_GroupID", "in", group_id));
            }

        }
        else
        {
            if (product_id != "0")
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionWholeSaleInfo.Promotion_WholeSale_ProductID", "in", product_id));
            }
        }
        if (cur_group > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("And", "str", "PromotionWholeSaleInfo.Promotion_WholeSale_GroupID", "=", cur_group.ToString()));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyWholeSale.GetPageInfo(Query, Public.GetUserPrivilege());
        PromotionWholeSaleGroupInfo wholesalegroup;
        IList<PromotionWholeSaleInfo> entitys = MyWholeSale.GetPromotionWholeSales(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (PromotionWholeSaleInfo entity in entitys)
            {

                jsonBuilder.Append("{\"PromotionWholeSaleInfo.Promotion_WholeSale_ID\":" + entity.Promotion_WholeSale_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_WholeSale_ID);
                jsonBuilder.Append("\",");

                wholesalegroup = GetPromotionWholeSaleGroupByID(entity.Promotion_WholeSale_GroupID);
                if (wholesalegroup != null)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(wholesalegroup.Promotion_WholeSale_Group_Name);
                    jsonBuilder.Append("\",");
                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");
                }

                product = MyBLLPRO.GetProductByID(entity.Promotion_WholeSale_ProductID, Public.GetUserPrivilege());
                if (product != null)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(product.Product_Code);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(product.Product_Name);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(product.Product_Spec);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(product.Product_Maker);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(product.Product_Price);
                    jsonBuilder.Append("\",");


                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");
                }

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_WholeSale_Price);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_WholeSale_MinAmount);
                jsonBuilder.Append("\",");




                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("6f2255a8-caae-4e2e-9228-e9b61fd3ce99"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"Promotion_WholeSale_Edit.aspx?Promotion_WholeSale_ID=" + entity.Promotion_WholeSale_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                if (Public.CheckPrivilege("03163e1d-0324-414c-8ec7-a6e1d285aacc"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Promotion_WholeSale_do.aspx?action=move&Promotion_WholeSale_ID=" + entity.Promotion_WholeSale_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    #endregion

    #region "优惠券/代金券"


    public virtual void AddPromotionFavorCoupon()
    {
        int Promotion_Coupon_ID = 0;
        string Promotion_Coupon_Title = tools.CheckStr(Request.Form["Promotion_Coupon_Title"]);
        int Promotion_Coupon_Target = tools.CheckInt(Request.Form["favor_target"]);
        double Promotion_Coupon_Payline = tools.CheckFloat(Request.Form["Promotion_Coupon_Payline"]);
        int Promotion_Coupon_Manner = tools.CheckInt(Request.Form["Promotion_Coupon_Manner"]);
        double Promotion_Coupon_Price = tools.CheckFloat(Request.Form["Promotion_Coupon_Price"]);
        double Promotion_Coupon_Percent = tools.CheckFloat(Request.Form["Promotion_Coupon_Percent"]);
        int Promotion_Coupon_Amount = tools.CheckInt(Request.Form["favor_amount"]);
        DateTime Promotion_Coupon_Starttime = tools.NullDate(Request.Form["Promotion_Coupon_Starttime"]);
        DateTime Promotion_Coupon_Endtime = tools.NullDate(Request.Form["Promotion_Coupon_Endtime"]);
        string Promotion_Coupon_Note = tools.NullStr(Request.Form["Promotion_Coupon_Note"]);
        string Member_ID = tools.NullStr(Request.Form["favor_memberid"]);
        string favor_cateid = tools.CheckStr(Request.Form["favor_cateid"]);
        string favor_brandid = tools.CheckStr(Request.Form["favor_brandid"]);
        string favor_productid = tools.CheckStr(Request.Form["favor_productid"]);
        int member_all = tools.CheckInt(Request.Form["favor_memberall"]);
        int favor_cateall = tools.CheckInt(Request.Form["favor_cateall"]);
        int favor_productall = tools.CheckInt(Request.Form["favor_productall"]);
        int favor_brandall = tools.CheckInt(Request.Form["favor_brandall"]);
        int Promotion_Coupon_num = tools.CheckInt(Request.Form["Promotion_Coupon_num"]);
        int Promotion_Coupon_Isused = 0;
        int Promotion_Coupon_UseAmount = 0;
        int Promotion_Coupon_Display = 1;
        DateTime Promotion_Coupon_Addtime = DateTime.Now;
        string VerifyCode = Public.Createvkey(8).ToUpper();
        string Promotion_Coupon_Site = Public.GetCurrentSite();

        if (member_all == 1)
        {
            Member_ID = "";
        }

        if (Promotion_Coupon_Title.Length == 0)
        {
            Public.Msg("error", "错误提示", "请输入优惠标题", false, "{back}");
        }

        if (favor_cateall == 0 && favor_cateid == "" && Promotion_Coupon_Target == 0)
        {
            Public.Msg("error", "错误提示", "请选择适用产品分类", false, "{back}");
        }

        if (favor_brandall == 0 && favor_brandid == "" && Promotion_Coupon_Target == 0)
        {
            Public.Msg("error", "错误提示", "请选择适用产品品牌", false, "{back}");
        }

        if (favor_productall == 0 && favor_productid == "" && Promotion_Coupon_Target == 1)
        {
            Public.Msg("error", "错误提示", "请选择适用产品", false, "{back}");
        }

        if (member_all == 0 && Member_ID == "")
        {
            Public.Msg("error", "错误提示", "请选择派发目标会员", false, "{back}");
        }

        if (Promotion_Coupon_Manner == 1 && Promotion_Coupon_Price == 0)
        {
            Public.Msg("error", "错误提示", "请输入优惠金额", false, "{back}");
        }

        if (Promotion_Coupon_Manner == 2 && Promotion_Coupon_Percent == 0)
        {
            Public.Msg("error", "错误提示", "请输入优惠折扣", false, "{back}");
        }

        if (Promotion_Coupon_num < 1)
        {
            Promotion_Coupon_num = 1;
        }

        PromotionFavorCouponInfo lastinfo = MyCoupon.GetLastPromotionFavorCoupon(Public.GetUserPrivilege());
        PromotionFavorCouponCateInfo couponcate = null;
        PromotionFavorCouponBrandInfo couponbrand = null;
        PromotionFavorCouponProductInfo couponproduct = null;
        string Coupon_Code = "";
        int code_start = 1;
        if (lastinfo != null)
        {
            if (lastinfo.Promotion_Coupon_Code.Substring(0, 6) == DateTime.Now.ToString("yyMMdd"))
            {
                code_start = tools.CheckInt(lastinfo.Promotion_Coupon_Code.Substring(7));
                code_start = code_start + 1;
            }
            else
            {
                code_start = 1;
            }
        }
        else
        {
            code_start = 1;
            //Coupon_Code = DateTime.Now.ToString("yyMMdd") + "000001";
        }
        lastinfo = null;

        for (int i = 1; i <= Promotion_Coupon_num; i++)
        {
            if (Member_ID != "")
            {
                Member_ID = Member_ID + ",";

                foreach (string Promotion_Coupon_MemberID in Member_ID.Split(','))
                {
                    while (Verify_Isrepeat(VerifyCode) == true)
                    {
                        VerifyCode = Public.Createvkey(8).ToUpper();
                    }

                    if (tools.CheckInt(Promotion_Coupon_MemberID) > 0)
                    {
                        Coupon_Code = "000000" + code_start.ToString();
                        Coupon_Code = Coupon_Code.Substring(Coupon_Code.Length - 6);
                        Coupon_Code = DateTime.Now.ToString("yyMMdd") + Coupon_Code;
                        PromotionFavorCouponInfo entity = new PromotionFavorCouponInfo();
                        entity.Promotion_Coupon_ID = Promotion_Coupon_ID;
                        entity.Promotion_Coupon_Title = Promotion_Coupon_Title;
                        entity.Promotion_Coupon_Target = Promotion_Coupon_Target;
                        entity.Promotion_Coupon_Payline = Promotion_Coupon_Payline;
                        entity.Promotion_Coupon_Manner = Promotion_Coupon_Manner;
                        entity.Promotion_Coupon_Price = Promotion_Coupon_Price;
                        entity.Promotion_Coupon_Percent = Promotion_Coupon_Percent;
                        entity.Promotion_Coupon_Amount = Promotion_Coupon_Amount;
                        entity.Promotion_Coupon_Starttime = Promotion_Coupon_Starttime;
                        entity.Promotion_Coupon_Endtime = Promotion_Coupon_Endtime;
                        entity.Promotion_Coupon_Member_ID = tools.CheckInt(Promotion_Coupon_MemberID);
                        entity.Promotion_Coupon_Code = Coupon_Code;
                        entity.Promotion_Coupon_Verifycode = VerifyCode;
                        entity.Promotion_Coupon_Isused = Promotion_Coupon_Isused;
                        entity.Promotion_Coupon_UseAmount = Promotion_Coupon_UseAmount;
                        entity.Promotion_Coupon_Display = Promotion_Coupon_Display;
                        entity.Promotion_Coupon_OrdersID = 0;
                        entity.Promotion_Coupon_Note = Promotion_Coupon_Note;
                        entity.Promotion_Coupon_Addtime = Promotion_Coupon_Addtime;
                        entity.Promotion_Coupon_Site = Promotion_Coupon_Site;
                        if (MyCoupon.AddPromotionFavorCoupon(entity, Public.GetUserPrivilege()))
                        {
                            lastinfo = MyCoupon.GetLastPromotionFavorCoupon(Public.GetUserPrivilege());
                            if (lastinfo != null)
                            {
                                if (lastinfo.Promotion_Coupon_Code == Coupon_Code)
                                {
                                    if (Promotion_Coupon_Target == 0)
                                    {
                                        if (favor_cateall == 1)
                                        {
                                            couponcate = new PromotionFavorCouponCateInfo();
                                            couponcate.Favor_CateID = 0;
                                            couponcate.Promotion_Coupon_ID = lastinfo.Promotion_Coupon_ID;
                                            MyCoupon.AddPromotion_FavorCouponCate(couponcate);
                                            couponcate = null;
                                        }
                                        else
                                        {
                                            foreach (string subcate in favor_cateid.Split(','))
                                            {
                                                if (tools.CheckInt(subcate) > 0)
                                                {
                                                    couponcate = new PromotionFavorCouponCateInfo();
                                                    couponcate.Favor_CateID = tools.CheckInt(subcate);
                                                    couponcate.Promotion_Coupon_ID = lastinfo.Promotion_Coupon_ID;
                                                    MyCoupon.AddPromotion_FavorCouponCate(couponcate);
                                                    couponcate = null;
                                                }
                                            }
                                        }
                                        if (favor_brandall == 1)
                                        {
                                            couponbrand = new PromotionFavorCouponBrandInfo();
                                            couponbrand.Favor_BrandID = 0;
                                            couponbrand.Promotion_Coupon_ID = lastinfo.Promotion_Coupon_ID;
                                            MyCoupon.AddPromotion_FavorCouponBrand(couponbrand);
                                            couponbrand = null;
                                        }
                                        else
                                        {
                                            foreach (string subbrand in favor_brandid.Split(','))
                                            {
                                                if (tools.CheckInt(subbrand) > 0)
                                                {
                                                    couponbrand = new PromotionFavorCouponBrandInfo();
                                                    couponbrand.Favor_BrandID = tools.CheckInt(subbrand);
                                                    couponbrand.Promotion_Coupon_ID = lastinfo.Promotion_Coupon_ID;
                                                    MyCoupon.AddPromotion_FavorCouponBrand(couponbrand);
                                                    couponbrand = null;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (favor_productall == 1)
                                        {
                                            couponproduct = new PromotionFavorCouponProductInfo();
                                            couponproduct.Favor_ProductID = 0;
                                            couponproduct.Promotion_Coupon_ID = lastinfo.Promotion_Coupon_ID;
                                            MyCoupon.AddPromotion_FavorCouponProduct(couponproduct);
                                            couponproduct = null;
                                        }
                                        else
                                        {
                                            foreach (string subproduct in favor_productid.Split(','))
                                            {
                                                if (tools.CheckInt(subproduct) > 0)
                                                {
                                                    couponproduct = new PromotionFavorCouponProductInfo();
                                                    couponproduct.Favor_ProductID = tools.CheckInt(subproduct); ;
                                                    couponproduct.Promotion_Coupon_ID = lastinfo.Promotion_Coupon_ID;
                                                    MyCoupon.AddPromotion_FavorCouponProduct(couponproduct);
                                                    couponproduct = null;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        entity = null;
                        code_start = code_start + 1;
                    }
                }
            }
            else
            {
                while (Verify_Isrepeat(VerifyCode) == true)
                {
                    VerifyCode = Public.Createvkey(8).ToUpper();
                }
                Coupon_Code = "000000" + code_start.ToString();
                Coupon_Code = Coupon_Code.Substring(Coupon_Code.Length - 6);
                Coupon_Code = DateTime.Now.ToString("yyMMdd") + Coupon_Code;
                PromotionFavorCouponInfo entity = new PromotionFavorCouponInfo();
                entity.Promotion_Coupon_ID = Promotion_Coupon_ID;
                entity.Promotion_Coupon_Title = Promotion_Coupon_Title;
                entity.Promotion_Coupon_Target = Promotion_Coupon_Target;
                entity.Promotion_Coupon_Payline = Promotion_Coupon_Payline;
                entity.Promotion_Coupon_Manner = Promotion_Coupon_Manner;
                entity.Promotion_Coupon_Price = Promotion_Coupon_Price;
                entity.Promotion_Coupon_Percent = Promotion_Coupon_Percent;
                entity.Promotion_Coupon_Amount = Promotion_Coupon_Amount;
                entity.Promotion_Coupon_Starttime = Promotion_Coupon_Starttime;
                entity.Promotion_Coupon_Endtime = Promotion_Coupon_Endtime;
                entity.Promotion_Coupon_Member_ID = 0;
                entity.Promotion_Coupon_Code = Coupon_Code;
                entity.Promotion_Coupon_Verifycode = VerifyCode;
                entity.Promotion_Coupon_Isused = Promotion_Coupon_Isused;
                entity.Promotion_Coupon_UseAmount = Promotion_Coupon_UseAmount;
                entity.Promotion_Coupon_Display = Promotion_Coupon_Display;
                entity.Promotion_Coupon_OrdersID = 0;
                entity.Promotion_Coupon_Note = Promotion_Coupon_Note;
                entity.Promotion_Coupon_Addtime = Promotion_Coupon_Addtime;
                entity.Promotion_Coupon_Site = Promotion_Coupon_Site;
                if (MyCoupon.AddPromotionFavorCoupon(entity, Public.GetUserPrivilege()))
                {
                    lastinfo = MyCoupon.GetLastPromotionFavorCoupon(Public.GetUserPrivilege());
                    if (lastinfo != null)
                    {
                        if (lastinfo.Promotion_Coupon_Code == Coupon_Code)
                        {
                            if (favor_cateall == 1)
                            {
                                couponcate = new PromotionFavorCouponCateInfo();
                                couponcate.Favor_CateID = 0;
                                couponcate.Promotion_Coupon_ID = lastinfo.Promotion_Coupon_ID;
                                MyCoupon.AddPromotion_FavorCouponCate(couponcate);
                                couponcate = null;
                            }
                            else
                            {
                                foreach (string subcate in favor_cateid.Split(','))
                                {
                                    if (tools.CheckInt(subcate) > 0)
                                    {
                                        couponcate = new PromotionFavorCouponCateInfo();
                                        couponcate.Favor_CateID = tools.CheckInt(subcate);
                                        couponcate.Promotion_Coupon_ID = lastinfo.Promotion_Coupon_ID;
                                        MyCoupon.AddPromotion_FavorCouponCate(couponcate);
                                        couponcate = null;
                                    }
                                }
                            }
                            if (favor_brandall == 1)
                            {
                                couponbrand = new PromotionFavorCouponBrandInfo();
                                couponbrand.Favor_BrandID = 0;
                                couponbrand.Promotion_Coupon_ID = lastinfo.Promotion_Coupon_ID;
                                MyCoupon.AddPromotion_FavorCouponBrand(couponbrand);
                                couponbrand = null;
                            }
                            else
                            {
                                foreach (string subbrand in favor_brandid.Split(','))
                                {
                                    if (tools.CheckInt(subbrand) > 0)
                                    {
                                        couponbrand = new PromotionFavorCouponBrandInfo();
                                        couponbrand.Favor_BrandID = tools.CheckInt(subbrand);
                                        couponbrand.Promotion_Coupon_ID = lastinfo.Promotion_Coupon_ID;
                                        MyCoupon.AddPromotion_FavorCouponBrand(couponbrand);
                                        couponbrand = null;
                                    }
                                }
                            }
                            if (favor_productall == 1)
                            {
                                couponproduct = new PromotionFavorCouponProductInfo();
                                couponproduct.Favor_ProductID = 0;
                                couponproduct.Promotion_Coupon_ID = lastinfo.Promotion_Coupon_ID;
                                MyCoupon.AddPromotion_FavorCouponProduct(couponproduct);
                                couponproduct = null;
                            }
                            else
                            {
                                foreach (string subproduct in favor_productid.Split(','))
                                {
                                    if (tools.CheckInt(subproduct) > 0)
                                    {
                                        couponproduct = new PromotionFavorCouponProductInfo();
                                        couponproduct.Favor_ProductID = tools.CheckInt(subproduct); ;
                                        couponproduct.Promotion_Coupon_ID = lastinfo.Promotion_Coupon_ID;
                                        MyCoupon.AddPromotion_FavorCouponProduct(couponproduct);
                                        couponproduct = null;
                                    }
                                }
                            }
                        }
                    }
                }
                entity = null;
                code_start = code_start + 1;
            }
        }
        Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_Favor_Coupon_add.aspx");
    }

    public virtual bool Verify_Isrepeat(string verify_code)
    {
        QueryInfo Query = new QueryInfo();
        ProductInfo product = null;
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorCouponInfo.Promotion_Coupon_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorCouponInfo.Promotion_Coupon_Verifycode", "=", verify_code));
        IList<PromotionFavorCouponInfo> entitys = MyCoupon.GetPromotionFavorCoupons(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //获取优惠券/代金券列表
    public virtual string GetPromotionFavorCoupons()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        if (keyword != "输入卡号进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "";
        }
        int status = tools.CheckInt(Request["status"]);
        QueryInfo Query = new QueryInfo();
        ProductInfo product = null;
        Query.PageSize = tools.CheckInt(Request["rows"]);
        if (tools.CheckInt(Request["page"]) == 0)
        {
            Query.CurrentPage = 1;
        }
        else
        {
            Query.CurrentPage = tools.CheckInt(Request["page"]);

        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorCouponInfo.Promotion_Coupon_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorCouponInfo.Promotion_Coupon_Display", "=", "1"));
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorCouponInfo.Promotion_Coupon_Code", "like", keyword));
        }
        if (status == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{PromotionFavorCouponInfo.Promotion_Coupon_Starttime}, GETDATE())", ">=", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{PromotionFavorCouponInfo.Promotion_Coupon_Endtime}, GETDATE())", "<=", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorCouponInfo.Promotion_Coupon_Isused", "=", "0"));
        }
        if (status == 2)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "funint", "DATEDIFF(d,{PromotionFavorCouponInfo.Promotion_Coupon_Starttime}, GETDATE())", "<", "0"));
            Query.ParamInfos.Add(new ParamInfo("OR", "funint", "DATEDIFF(d,{PromotionFavorCouponInfo.Promotion_Coupon_Endtime}, GETDATE())", ">", "0"));
            Query.ParamInfos.Add(new ParamInfo("OR)", "int", "PromotionFavorCouponInfo.Promotion_Coupon_Isused", "<>", "0"));
        }

        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyCoupon.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<PromotionFavorCouponInfo> entitys = MyCoupon.GetPromotionFavorCoupons(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (PromotionFavorCouponInfo entity in entitys)
            {

                jsonBuilder.Append("{\"PromotionFavorCouponInfo.Promotion_Coupon_ID\":" + entity.Promotion_Coupon_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Coupon_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Coupon_Title);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(">=" + entity.Promotion_Coupon_Payline);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Promotion_Coupon_Manner == 1)
                {
                    jsonBuilder.Append(entity.Promotion_Coupon_Price + "元");
                }
                else
                {
                    jsonBuilder.Append(entity.Promotion_Coupon_Percent + "%");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Coupon_Code);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Coupon_Verifycode);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Coupon_Starttime.ToShortDateString() + "至" + entity.Promotion_Coupon_Endtime.ToShortDateString());
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Promotion_Coupon_Isused == 1)
                {
                    jsonBuilder.Append("已使用");   
                }
                else if (tools.NullDate(entity.Promotion_Coupon_Starttime.ToShortDateString()) > tools.NullDate(DateTime.Now.ToShortDateString()))
                {
                    jsonBuilder.Append("未开始");   
                }
                else if (tools.NullDate(entity.Promotion_Coupon_Endtime.ToShortDateString()) < tools.NullDate(DateTime.Now.ToShortDateString()))
                {
                    jsonBuilder.Append("已过期");
                }
                else
                {
                    jsonBuilder.Append("正常");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Coupon_UseAmount);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\"> <a href=\\\"Promotion_favor_coupon_view.aspx?Promotion_coupon_ID=" + entity.Promotion_Coupon_ID + "\\\" title=\\\"查看\\\">查看</a>");
                
                if (Public.CheckPrivilege("d394b6b8-560a-49b9-9d20-1a356d3bf984"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Promotion_favor_coupon_do.aspx?action=move&Promotion_coupon_ID=" + entity.Promotion_Coupon_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    public virtual PromotionFavorCouponInfo GetPromotionFavorCouponByID(int ID)
    {
        return MyCoupon.GetPromotionFavorCouponByID(ID, Public.GetUserPrivilege());
    }

    public virtual void DelPromotionFavorCoupon()
    {
        int Promotion_coupon_ID = tools.CheckInt(Request.QueryString["Promotion_coupon_ID"]);
        if (MyCoupon.DelPromotionFavorCoupon(Promotion_coupon_ID, Public.GetUserPrivilege()) > 0)
        {
            MyCoupon.DelPromotion_FavorCouponBrand(Promotion_coupon_ID);
            MyCoupon.DelPromotion_FavorCouponCate(Promotion_coupon_ID);
            MyCoupon.DelPromotion_FavorCouponProduct(Promotion_coupon_ID);

            Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_Favor_Coupon_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void PromotionFavorCoupons_Export()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        if (keyword != "输入卡号进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "";
        }
        int status = tools.CheckInt(Request["status"]);
        QueryInfo Query = new QueryInfo();
        ProductInfo product = null;
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorCouponInfo.Promotion_Coupon_Code", "like", keyword));
        }
        if (status == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{PromotionFavorCouponInfo.Promotion_Coupon_Starttime}, GETDATE())", ">=", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{PromotionFavorCouponInfo.Promotion_Coupon_Endtime}, GETDATE())", "<=", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorCouponInfo.Promotion_Coupon_Isused", "=", "0"));
        }
        if (status == 2)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "funint", "DATEDIFF(d,{PromotionFavorCouponInfo.Promotion_Coupon_Starttime}, GETDATE())", "<", "0"));
            Query.ParamInfos.Add(new ParamInfo("OR", "funint", "DATEDIFF(d,{PromotionFavorCouponInfo.Promotion_Coupon_Endtime}, GETDATE())", ">", "0"));
            Query.ParamInfos.Add(new ParamInfo("OR)", "int", "PromotionFavorCouponInfo.Promotion_Coupon_Isused", "<>", "0"));
        }

        DataTable dt = new DataTable("excel");
        DataRow dr = null;
        DataColumn column = null;

        string[] dtcol = { "编号", "优惠标题", "卡号", "验证码", "优惠条件", "优惠金额", "可用次数", "适用会员", "有效期", "状态" };
        foreach (string col in dtcol)
        {
            try { dt.Columns.Add(col); }
            catch { dt.Columns.Add(col + ","); }
        }
        dtcol = null;
        MemberInfo memberinfo = null;

        Query.OrderInfos.Add(new OrderInfo("PromotionFavorCouponInfo.Promotion_Coupon_ID", "Desc"));
        IList<PromotionFavorCouponInfo> entitys = MyCoupon.GetPromotionFavorCoupons(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            
            foreach (PromotionFavorCouponInfo entity in entitys)
            {

                dr = dt.NewRow();

                dr[0] = entity.Promotion_Coupon_ID;
                dr[1] = entity.Promotion_Coupon_Title;
                dr[2] = entity.Promotion_Coupon_Code;
                dr[3] = entity.Promotion_Coupon_Verifycode;
                dr[4] = ">=" + entity.Promotion_Coupon_Payline;
                if (entity.Promotion_Coupon_Manner == 1)
                {
                    dr[5] = Public.DisplayCurrency(entity.Promotion_Coupon_Price);
                }
                else
                {
                    dr[5] = entity.Promotion_Coupon_Percent + "折";
                }
                if (entity.Promotion_Coupon_Amount == 1)
                {
                    dr[6] = "1次";
                }
                else
                {
                    dr[6] = "不限次数";
                }
                if (entity.Promotion_Coupon_Member_ID == 0)
                {
                    dr[7] = "所有会员";
                }
                else
                {
                    memberinfo = Mymember.GetMemberByID(entity.Promotion_Coupon_Member_ID, Public.GetUserPrivilege());
                    if (memberinfo != null)
                    {
                        dr[7] = memberinfo.Member_NickName;
                    }
                    else
                    {
                        dr[7] = "未知";
                    }
                    memberinfo = null;
                }
                dr[8] = entity.Promotion_Coupon_Starttime.ToShortDateString() + "-" + entity.Promotion_Coupon_Endtime.ToShortDateString();
                if (entity.Promotion_Coupon_Isused == 1)
                {
                    dr[9]="已使用";
                }
                else if (tools.NullDate(entity.Promotion_Coupon_Starttime.ToShortDateString()) > tools.NullDate(DateTime.Now.ToShortDateString()))
                {
                    dr[9] = "未开始";
                }
                else if (tools.NullDate(entity.Promotion_Coupon_Endtime.ToShortDateString()) < tools.NullDate(DateTime.Now.ToShortDateString()))
                {
                    dr[9] = "已过期";
                }
                else
                {
                    dr[9] = "正常";
                }
                

                dt.Rows.Add(dr);
                dr = null;

            }
           
        }
        Public.toExcel(dt);
    }

    public virtual string Get_Coupon_Cate_String(IList<PromotionFavorCouponCateInfo> entitys)
    {
        string return_value = "";
        int i = 0;
        if (entitys != null)
        {
            foreach (PromotionFavorCouponCateInfo entity in entitys)
            {
                i = i + 1;
                if (i < entitys.Count)
                {
                    return_value = return_value + entity.Favor_CateID + ",";
                }
                else
                {
                    return_value = return_value + entity.Favor_CateID;
                }
            }
        }
        else
        {
            return_value = "0";
        }
        return return_value;
    }

    public virtual string Get_Coupon_Brand_String(IList<PromotionFavorCouponBrandInfo> entitys)
    {
        string return_value = "";
        int i = 0;
        if (entitys != null)
        {
            foreach (PromotionFavorCouponBrandInfo entity in entitys)
            {
                i = i + 1;
                if (i < entitys.Count)
                {
                    return_value = return_value + entity.Favor_BrandID + ",";
                }
                else
                {
                    return_value = return_value + entity.Favor_BrandID;
                }
            }
        }
        else
        {
            return_value = "0";
        }
        return return_value;
    }

    public virtual string Get_Coupon_Product_String(IList<PromotionFavorCouponProductInfo> entitys)
    {
        string return_value = "";
        int i = 0;
        if (entitys != null)
        {
            foreach (PromotionFavorCouponProductInfo entity in entitys)
            {
                i = i + 1;
                if (i < entitys.Count)
                {
                    return_value = return_value + entity.Favor_ProductID + ",";
                }
                else
                {
                    return_value = return_value + entity.Favor_ProductID;
                }
            }
        }
        else
        {
            return_value = "0";
        }
        return return_value;
    }

    #endregion

    #region "优惠券/代金券派发规则"


    public virtual void AddPromotionCouponRule()
    {
        int Coupon_Rule_ID = 0;
        string Coupon_Rule_Title = tools.CheckStr(Request.Form["Promotion_Coupon_Title"]);
        int Coupon_Rule_Target = tools.CheckInt(Request.Form["favor_target"]);
        double Coupon_Rule_Payline = tools.CheckFloat(Request.Form["Promotion_Coupon_Payline"]);
        int Coupon_Rule_Manner = tools.CheckInt(Request.Form["Promotion_Coupon_Manner"]);
        double Coupon_Rule_Price = tools.CheckFloat(Request.Form["Promotion_Coupon_Price"]);
        double Coupon_Rule_Percent = tools.CheckFloat(Request.Form["Promotion_Coupon_Percent"]);
        string Promotion_Coupon_Note = tools.CheckStr(Request.Form["Promotion_Coupon_Note"]);
        int Promotion_Coupon_Amount = tools.CheckInt(Request.Form["favor_amount"]);
        int Coupon_Rule_Valid = tools.CheckInt(Request.Form["favor_valid"]);
        string favor_cateid = tools.CheckStr(Request.Form["favor_cateid"]);
        string favor_brandid = tools.CheckStr(Request.Form["favor_brandid"]);
        string favor_productid = tools.CheckStr(Request.Form["favor_productid"]);
        int favor_cateall = tools.CheckInt(Request.Form["favor_cateall"]);
        int favor_productall = tools.CheckInt(Request.Form["favor_productall"]);
        int favor_brandall = tools.CheckInt(Request.Form["favor_brandall"]);
        string Coupon_Rule_Site = Public.GetCurrentSite();



        if (Coupon_Rule_Title.Length == 0)
        {
            Public.Msg("error", "错误提示", "请输入优惠标题", false, "{back}");
        }

        if (favor_cateall == 0 && favor_cateid == "" && Coupon_Rule_Target == 0)
        {
            Public.Msg("error", "错误提示", "请选择适用产品分类", false, "{back}");
        }

        if (favor_brandall == 0 && favor_brandid == "" && Coupon_Rule_Target == 0)
        {
            Public.Msg("error", "错误提示", "请选择适用产品品牌", false, "{back}");
        }

        if (favor_productall == 0 && favor_productid == "" && Coupon_Rule_Target == 1)
        {
            Public.Msg("error", "错误提示", "请选择适用产品", false, "{back}");
        }

        if (Coupon_Rule_Manner == 1 && Coupon_Rule_Price == 0)
        {
            Public.Msg("error", "错误提示", "请输入优惠金额", false, "{back}");
        }

        if (Coupon_Rule_Manner == 2 && Coupon_Rule_Percent == 0)
        {
            Public.Msg("error", "错误提示", "请输入优惠折扣", false, "{back}");
        }
        if (Coupon_Rule_Valid < 1)
        {
            Coupon_Rule_Valid = 1;
        }
        PromotionCouponRuleInfo lastinfo;
        PromotionCouponRuleCateInfo couponcate = null;
        PromotionCouponRuleBrandInfo couponbrand = null;
        PromotionCouponRuleProductInfo couponproduct = null;
        PromotionCouponRuleInfo entity = new PromotionCouponRuleInfo();
        entity.Coupon_Rule_ID = Coupon_Rule_ID;
        entity.Coupon_Rule_Title = Coupon_Rule_Title;
        entity.Coupon_Rule_Target = Coupon_Rule_Target;
        entity.Coupon_Rule_Payline = Coupon_Rule_Payline;
        entity.Coupon_Rule_Manner = Coupon_Rule_Manner;
        entity.Coupon_Rule_Price = Coupon_Rule_Price;
        entity.Coupon_Rule_Percent = Coupon_Rule_Percent;
        entity.Coupon_Rule_Amount = 1;
        entity.Coupon_Rule_Valid = Coupon_Rule_Valid;
        entity.Coupon_Rule_Note = Promotion_Coupon_Note;
        entity.Coupon_Rule_Site = Coupon_Rule_Site;
        if (MyCouponRule.AddPromotionCouponRule(entity, Public.GetUserPrivilege()))
        {
            lastinfo = MyCouponRule.GetLastPromotionCouponRule(Public.GetUserPrivilege());
            if (lastinfo != null)
            {
                if (lastinfo.Coupon_Rule_Title == Coupon_Rule_Title)
                {
                    if (Coupon_Rule_Target == 0)
                    {
                        if (favor_cateall == 1)
                        {
                            couponcate = new PromotionCouponRuleCateInfo();
                            couponcate.Coupon_Rule_Cate_CateID = 0;
                            couponcate.Coupon_Rule_Cate_RuleID = lastinfo.Coupon_Rule_ID;
                            MyCouponRule.AddPromotionCouponRuleCate(couponcate);
                            couponcate = null;
                        }
                        else
                        {
                            foreach (string subcate in favor_cateid.Split(','))
                            {
                                if (tools.CheckInt(subcate) > 0)
                                {
                                    couponcate = new PromotionCouponRuleCateInfo();
                                    couponcate.Coupon_Rule_Cate_CateID = tools.CheckInt(subcate);
                                    couponcate.Coupon_Rule_Cate_RuleID = lastinfo.Coupon_Rule_ID;
                                    MyCouponRule.AddPromotionCouponRuleCate(couponcate);
                                    couponcate = null;
                                }
                            }
                        }
                        if (favor_brandall == 1)
                        {
                            couponbrand = new PromotionCouponRuleBrandInfo();
                            couponbrand.Coupon_Rule_Brand_BrandID = 0;
                            couponbrand.Coupon_Rule_Brand_RuleID = lastinfo.Coupon_Rule_ID;
                            MyCouponRule.AddPromotionCouponRuleBrand(couponbrand);
                            couponbrand = null;
                        }
                        else
                        {
                            foreach (string subbrand in favor_brandid.Split(','))
                            {
                                if (tools.CheckInt(subbrand) > 0)
                                {
                                    couponbrand = new PromotionCouponRuleBrandInfo();
                                    couponbrand.Coupon_Rule_Brand_BrandID = tools.CheckInt(subbrand);
                                    couponbrand.Coupon_Rule_Brand_RuleID = lastinfo.Coupon_Rule_ID;
                                    MyCouponRule.AddPromotionCouponRuleBrand(couponbrand);
                                    couponbrand = null;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (favor_productall == 1)
                        {
                            couponproduct = new PromotionCouponRuleProductInfo();
                            couponproduct.Coupon_Rule_Product_ProductID = 0;
                            couponproduct.Coupon_Rule_Product_RuleID = lastinfo.Coupon_Rule_ID;
                            MyCouponRule.AddPromotionCouponRuleProduct(couponproduct);
                            couponproduct = null;
                        }
                        else
                        {
                            foreach (string subproduct in favor_productid.Split(','))
                            {
                                if (tools.CheckInt(subproduct) > 0)
                                {
                                    couponproduct = new PromotionCouponRuleProductInfo();
                                    couponproduct.Coupon_Rule_Product_ProductID = tools.CheckInt(subproduct); ;
                                    couponproduct.Coupon_Rule_Product_RuleID = lastinfo.Coupon_Rule_ID;
                                    MyCouponRule.AddPromotionCouponRuleProduct(couponproduct);
                                    couponproduct = null;
                                }
                            }
                        }
                    }
                }
            }
        }
        entity = null;
        Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_Coupon_Rule_add.aspx");
    }


    //获取优惠券/代金券列表派发规则
    public virtual string GetPromotionCouponRules()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        if (keyword != "输入优惠标题进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "";
        }
        QueryInfo Query = new QueryInfo();
        ProductInfo product = null;
        Query.PageSize = tools.CheckInt(Request["rows"]);
        if (tools.CheckInt(Request["page"]) == 0)
        {
            Query.CurrentPage = 1;
        }
        else
        {
            Query.CurrentPage = tools.CheckInt(Request["page"]);

        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionCouponRuleInfo.Coupon_Rule_Site", "=", Public.GetCurrentSite()));
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionCouponRuleInfo.Coupon_Rule_Title", "like", keyword));
        }

        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyCouponRule.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<PromotionCouponRuleInfo> entitys = MyCouponRule.GetPromotionCouponRules(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (PromotionCouponRuleInfo entity in entitys)
            {

                jsonBuilder.Append("{\"PromotionCouponRuleInfo.Coupon_Rule_ID\":" + entity.Coupon_Rule_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Coupon_Rule_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Coupon_Rule_Title);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(">=" + entity.Coupon_Rule_Payline);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Coupon_Rule_Manner == 1)
                {
                    jsonBuilder.Append(entity.Coupon_Rule_Price + "元");
                }
                else
                {
                    jsonBuilder.Append(entity.Coupon_Rule_Percent + "%");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Coupon_Rule_Valid);
                jsonBuilder.Append("天\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("ac7af23d-9a18-4c6e-b8d5-cdbbc0bc018c"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\"> <a href=\\\"Promotion_coupon_rule_view.aspx?Coupon_Rule_ID=" + entity.Coupon_Rule_ID + "\\\" title=\\\"查看\\\">查看</a>");
                }

                if (Public.CheckPrivilege("ac7af23d-9a18-4c6e-b8d5-cdbbc0bc018c"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Promotion_coupon_rule_do.aspx?action=move&Coupon_Rule_ID=" + entity.Coupon_Rule_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    public virtual void PromotionCouponRule_Select(int selected_id)
    {
        Response.Write("<select name=\"Coupon_Rule_ID\">");
        Response.Write("<option value=\"0\">选择派发规则</option>");
        QueryInfo Query = new QueryInfo();
        ProductInfo product = null;
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionCouponRuleInfo.Coupon_Rule_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("PromotionCouponRuleInfo.Coupon_Rule_ID", "Desc"));
        IList<PromotionCouponRuleInfo> entitys = MyCouponRule.GetPromotionCouponRules(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach(PromotionCouponRuleInfo entity in entitys)
            {
                if (selected_id == entity.Coupon_Rule_ID)
                {
                    Response.Write("<option value=\"" + entity.Coupon_Rule_ID + "\" selected>" + entity.Coupon_Rule_Title + "</option>");
                }
                else
                {
                    Response.Write("<option value=\"" + entity.Coupon_Rule_ID + "\">" + entity.Coupon_Rule_Title + "</option>");
                }
            }
        }
        Response.Write("</select>");
    }

    public virtual PromotionCouponRuleInfo GetPromotionCouponRuleByID(int ID)
    {
        return MyCouponRule.GetPromotionCouponRuleByID(ID, Public.GetUserPrivilege());
    }

    public virtual void DelPromotionCouponRule()
    {
        int Coupon_Rule_ID = tools.CheckInt(Request.QueryString["Coupon_Rule_ID"]);
        if (MyCouponRule.DelPromotionCouponRule(Coupon_Rule_ID, Public.GetUserPrivilege()) > 0)
        {
            MyCouponRule.DelPromotionCouponRuleBrand(Coupon_Rule_ID);
            MyCouponRule.DelPromotionCouponRuleCate(Coupon_Rule_ID);
            MyCouponRule.DelPromotionCouponRuleProduct(Coupon_Rule_ID);

            Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_Coupon_Rule_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual string Get_CouponRule_Cate_String(IList<PromotionCouponRuleCateInfo> entitys)
    {
        string return_value = "";
        int i = 0;
        if (entitys != null)
        {
            foreach (PromotionCouponRuleCateInfo entity in entitys)
            {
                i = i + 1;
                if (i < entitys.Count)
                {
                    return_value = return_value + entity.Coupon_Rule_Cate_CateID + ",";
                }
                else
                {
                    return_value = return_value + entity.Coupon_Rule_Cate_CateID;
                }
            }
        }
        else
        {
            return_value = "0";
        }
        return return_value;
    }

    public virtual string Get_CouponRule_Brand_String(IList<PromotionCouponRuleBrandInfo> entitys)
    {
        string return_value = "";
        int i = 0;
        if (entitys != null)
        {
            foreach (PromotionCouponRuleBrandInfo entity in entitys)
            {
                i = i + 1;
                if (i < entitys.Count)
                {
                    return_value = return_value + entity.Coupon_Rule_Brand_BrandID + ",";
                }
                else
                {
                    return_value = return_value + entity.Coupon_Rule_Brand_BrandID;
                }
            }
        }
        else
        {
            return_value = "0";
        }
        return return_value;
    }

    public virtual string Get_CouponRule_Product_String(IList<PromotionCouponRuleProductInfo> entitys)
    {
        string return_value = "";
        int i = 0;
        if (entitys != null)
        {
            foreach (PromotionCouponRuleProductInfo entity in entitys)
            {
                i = i + 1;
                if (i < entitys.Count)
                {
                    return_value = return_value + entity.Coupon_Rule_Product_ProductID + ",";
                }
                else
                {
                    return_value = return_value + entity.Coupon_Rule_Product_ProductID;
                }
            }
        }
        else
        {
            return_value = "0";
        }
        return return_value;
    }

    #endregion

    #region 优惠政策

    public virtual void AddPromotionFavorPolicy()
    {
        int Promotion_Policy_ID = 0;
        string Promotion_Policy_Title = tools.CheckStr(Request.Form["Promotion_Policy_Title"]);
        int Promotion_Policy_Target = tools.CheckInt(Request.Form["favor_target"]);
        double Promotion_Policy_Payline = tools.CheckFloat(Request.Form["Promotion_Policy_Payline"]);
        int Promotion_Policy_Manner = tools.CheckInt(Request.Form["Promotion_Policy_Manner"]);
        int Promotion_Policy_CouponRuleID = tools.CheckInt(Request.Form["Coupon_Rule_ID"]);
        double Promotion_Policy_Price = tools.CheckFloat(Request.Form["Promotion_Policy_Price"]);
        double Promotion_Policy_Percent = tools.CheckFloat(Request.Form["Promotion_Policy_Percent"]);
        int Promotion_Policy_Group = tools.CheckInt(Request.Form["Promotion_Policy_Group"]);
        int Promotion_Policy_Limit = tools.CheckInt(Request.Form["Promotion_Policy_Limit"]);
        string Promotion_Policy_Note = tools.CheckStr(Request.Form["Promotion_Policy_Note"]);
        int Promotion_Policy_IsRepeat = tools.CheckInt(Request.Form["Promotion_Policy_IsRepeat"]);
        DateTime Promotion_Policy_Starttime = tools.NullDate(Request.Form["Promotion_Policy_Starttime"]);
        DateTime Promotion_Policy_Endtime = tools.NullDate(Request.Form["Promotion_Policy_Endtime"]); 
        string Promotion_Policy_Site = Public.GetCurrentSite();
        string favor_cateid = tools.CheckStr(Request.Form["favor_cateid"]);
        string favor_brandid = tools.CheckStr(Request.Form["favor_brandid"]);
        string Favor_Productid = tools.CheckStr(Request.Form["favor_productid"]);
        int favor_cateall = tools.CheckInt(Request.Form["favor_cateall"]);
        int favor_brandall = tools.CheckInt(Request.Form["favor_brandall"]);
        int Product_All = tools.CheckInt(Request.Form["favor_productall"]);
        string favor_gradeid = tools.CheckStr(Request.Form["Member_Grade"]);
        double favor_couponprice = tools.CheckFloat(Request.Form["favor_couponprice"]);
        int Favor_Sort = tools.CheckInt(Request.Form["Favor_Sort"]);

        if (Promotion_Policy_Title.Length == 0)
        {
            Public.Msg("error", "错误提示", "请输入优惠标题", false, "{back}");
        }

        if (favor_cateall == 0 && favor_cateid == "" && Promotion_Policy_Target == 0)
        {
            Public.Msg("error", "错误提示", "请选择适用产品分类", false, "{back}");
        }

        if (favor_brandall == 0 && favor_brandid == "" && Promotion_Policy_Target == 0)
        {
            Public.Msg("error", "错误提示", "请选择适用产品品牌", false, "{back}");
        }

        if (Product_All == 0 && Favor_Productid == "" && Promotion_Policy_Target == 1)
        {
            Public.Msg("error", "错误提示", "请选择适用产品", false, "{back}");
        }

        if (Promotion_Policy_Manner == 1 && Promotion_Policy_Price == 0)
        {
            Public.Msg("error", "错误提示", "请输入优惠金额", false, "{back}");
        }

        if (Promotion_Policy_Manner == 2 && Promotion_Policy_Percent == 0)
        {
            Public.Msg("error", "错误提示", "请输入优惠折扣", false, "{back}");
        }

        if (Promotion_Policy_Manner == 3 && Promotion_Policy_CouponRuleID==0)
        {
            Public.Msg("error", "错误提示", "请选择优惠券派发规则！", false, "{back}");
        }
        if (favor_gradeid.Length == 0)
        {
            Public.Msg("error", "错误信息", "请选择针对会员！", false, "{back}");
        }
        if (Promotion_Policy_Manner == 1)
        {
            Promotion_Policy_CouponRuleID = 0;
            Promotion_Policy_Percent = 0;
        }
        if (Promotion_Policy_Manner == 2)
        {
            Promotion_Policy_CouponRuleID = 0;
            Promotion_Policy_Price = 0;
        }
        if (Promotion_Policy_Manner == 3)
        {
            Promotion_Policy_Price = 0;
            Promotion_Policy_Percent = 0;
        }

        PromotionFavorPolicyInfo lastinfo;
        PromotionFavorPolicyCateInfo policycate;
        PromotionFavorPolicyBrandInfo policybrand;
        PromotionFavorPolicyProductInfo policyproduct;
        PromotionFavorPolicyMemberGradeInfo policygrade;

        PromotionFavorPolicyInfo entity = new PromotionFavorPolicyInfo();
        entity.Promotion_Policy_ID = Promotion_Policy_ID;
        entity.Promotion_Policy_Title = Promotion_Policy_Title;
        entity.Promotion_Policy_Target = Promotion_Policy_Target;
        entity.Promotion_Policy_Payline = Promotion_Policy_Payline;
        entity.Promotion_Policy_Manner = Promotion_Policy_Manner;
        
        entity.Promotion_Policy_CouponRuleID = Promotion_Policy_CouponRuleID;
        entity.Promotion_Policy_Price = Promotion_Policy_Price;
        entity.Promotion_Policy_Percent = Promotion_Policy_Percent;
        
        entity.Promotion_Policy_Group = Promotion_Policy_Group;
        entity.Promotion_Policy_Limit = Promotion_Policy_Limit;
        entity.Promotion_Policy_IsRepeat = Promotion_Policy_IsRepeat;
        entity.Promotion_Policy_Note = Promotion_Policy_Note;
        entity.Promotion_Policy_Starttime = Promotion_Policy_Starttime;
        entity.Promotion_Policy_Endtime = Promotion_Policy_Endtime;
        entity.Promotion_Policy_Sort = Favor_Sort;
        entity.Promotion_Policy_IsActive = 1;
        entity.Promotion_Policy_IsChecked = 0;
        entity.Promotion_Policy_Site = Promotion_Policy_Site;

        if (MyPolicy.AddPromotionFavorPolicy(entity, Public.GetUserPrivilege()))
        {
            lastinfo = MyPolicy.GetLastPromotionFavorPolicy(Public.GetUserPrivilege());
            if (lastinfo != null)
            {
                if (lastinfo.Promotion_Policy_Title == Promotion_Policy_Title)
                {
                    //添加适用会员等级
                    
                    if (favor_gradeid.Length == 0)
                    {
                        policygrade = new PromotionFavorPolicyMemberGradeInfo();
                        policygrade.Favor_ID = 0;
                        policygrade.Favor_GradeID = 0;
                        policygrade.Promotion_Policy_ID = lastinfo.Promotion_Policy_ID;
                        MyPolicy.AddPromotionFavorPolicyMemberGrade(policygrade);
                        policygrade = null;
                    }
                    else
                    {
                        foreach (string substr in favor_gradeid.Split(','))
                        {
                            if (tools.CheckInt(substr) > 0)
                            {
                                policygrade = new PromotionFavorPolicyMemberGradeInfo();
                                policygrade.Favor_ID = 0;
                                policygrade.Favor_GradeID = tools.CheckInt(substr);
                                policygrade.Promotion_Policy_ID = lastinfo.Promotion_Policy_ID;
                                MyPolicy.AddPromotionFavorPolicyMemberGrade(policygrade);
                                policygrade = null;
                            }
                        }
                    }

                    if (Promotion_Policy_Target == 0)
                    {
                        if (favor_cateall == 1)
                        {
                            policycate = new PromotionFavorPolicyCateInfo();
                            policycate.Favor_CateID = 0;
                            policycate.Promotion_Policy_ID = lastinfo.Promotion_Policy_ID;
                            MyPolicy.AddPromotionFavorPolicyCate(policycate);
                            policycate = null;
                        }
                        else
                        {
                            foreach (string substr in favor_cateid.Split(','))
                            {
                                if (tools.CheckInt(substr) > 0)
                                {
                                    policycate = new PromotionFavorPolicyCateInfo();
                                    policycate.Favor_CateID = tools.CheckInt(substr);
                                    policycate.Promotion_Policy_ID = lastinfo.Promotion_Policy_ID;
                                    MyPolicy.AddPromotionFavorPolicyCate(policycate);
                                    policycate = null;
                                }
                            }
                        }

                        if (favor_brandall == 1)
                        {
                            policybrand = new PromotionFavorPolicyBrandInfo();
                            policybrand.Favor_BrandID = 0;
                            policybrand.Promotion_Policy_ID = lastinfo.Promotion_Policy_ID;
                            MyPolicy.AddPromotionFavorPolicyBrand(policybrand);
                            policybrand = null;
                        }
                        else
                        {
                            foreach (string subbrand in favor_brandid.Split(','))
                            {
                                if (tools.CheckInt(subbrand) > 0)
                                {
                                    policybrand = new PromotionFavorPolicyBrandInfo();
                                    policybrand.Favor_BrandID = tools.CheckInt(subbrand);
                                    policybrand.Promotion_Policy_ID = lastinfo.Promotion_Policy_ID;
                                    MyPolicy.AddPromotionFavorPolicyBrand(policybrand);
                                    policybrand = null;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Product_All == 1)
                        {
                            policyproduct = new PromotionFavorPolicyProductInfo();
                            policyproduct.Favor_ProductID = 0;
                            policyproduct.Promotion_Policy_ID = lastinfo.Promotion_Policy_ID;
                            MyPolicy.AddPromotionFavorPolicyProduct(policyproduct);
                            policyproduct = null;
                        }
                        else
                        {
                            foreach (string subproduct in Favor_Productid.Split(','))
                            {
                                if (tools.CheckInt(subproduct) > 0)
                                {
                                    policyproduct = new PromotionFavorPolicyProductInfo();
                                    policyproduct.Favor_ProductID = tools.CheckInt(subproduct);
                                    policyproduct.Promotion_Policy_ID = lastinfo.Promotion_Policy_ID;
                                    MyPolicy.AddPromotionFavorPolicyProduct(policyproduct);
                                    policyproduct = null;
                                }
                            }
                        }
                    }
                }
            }
            Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_Favor_Policy_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditPromotionFavorPolicyStatus(int Action_Code)
    {
        string Promotion_Policy_ID = tools.CheckStr(Request["favor_id"]);
        if (Promotion_Policy_ID == "")
        {
            Public.Msg("error", "错误信息", "请选择要操作的优惠信息", false, "{back}");
            return;
        }
        if (tools.Left(Promotion_Policy_ID, 1) == ",") { Promotion_Policy_ID = Promotion_Policy_ID.Remove(0, 1); }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorPolicyInfo.Promotion_Policy_ID", "in", Promotion_Policy_ID));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorPolicyInfo.Promotion_Policy_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("PromotionFavorPolicyInfo.Promotion_Policy_ID", "DESC"));
        IList<PromotionFavorPolicyInfo> entitys = MyPolicy.GetPromotionFavorPolicys(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (PromotionFavorPolicyInfo entity in entitys)
            {
                switch (Action_Code)
                {
                    case 1:
                        entity.Promotion_Policy_IsActive = 1;
                        break;
                    case 2:
                        entity.Promotion_Policy_IsActive = 0;
                        break;
                    case 3:
                        entity.Promotion_Policy_IsChecked = 1;
                        break;
                    case 4:
                        entity.Promotion_Policy_IsChecked = 2;
                        break;
                }
                MyPolicy.EditPromotionFavorPolicy(entity);
            }
            Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_Favor_Policy_List.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }


    }

    public virtual string Get_Policy_Cate_String(IList<PromotionFavorPolicyCateInfo> entitys)
    {
        string return_value = "";
        int i = 0;
        if (entitys != null)
        {
            foreach (PromotionFavorPolicyCateInfo entity in entitys)
            {
                i = i + 1;
                if (i < entitys.Count)
                {
                    return_value = return_value + entity.Favor_CateID + ",";
                }
                else
                {
                    return_value = return_value + entity.Favor_CateID;
                }
            }
        }
        else
        {
            return_value = "0";
        }
        return return_value;
    }

    public virtual string Get_Policy_Brand_String(IList<PromotionFavorPolicyBrandInfo> entitys)
    {
        string return_value = "";
        int i = 0;
        if (entitys != null)
        {
            foreach (PromotionFavorPolicyBrandInfo entity in entitys)
            {
                i = i + 1;
                if (i < entitys.Count)
                {
                    return_value = return_value + entity.Favor_BrandID + ",";
                }
                else
                {
                    return_value = return_value + entity.Favor_BrandID;
                }
            }
        }
        else
        {
            return_value = "0";
        }
        return return_value;
    }

    public virtual string Get_Policy_Product_String(IList<PromotionFavorPolicyProductInfo> entitys)
    {
        string return_value = "";
        int i = 0;
        if (entitys != null)
        {
            foreach (PromotionFavorPolicyProductInfo entity in entitys)
            {
                i = i + 1;
                if (i < entitys.Count)
                {
                    return_value = return_value + entity.Favor_ProductID + ",";
                }
                else
                {
                    return_value = return_value + entity.Favor_ProductID;
                }
            }
        }
        else
        {
            return_value = "0";
        }
        return return_value;
    }

    public virtual string Get_Policy_MemberGrade_String(IList<PromotionFavorPolicyMemberGradeInfo> entitys)
    {
        string return_value = "";
        int i = 0;
        if (entitys != null)
        {
            foreach (PromotionFavorPolicyMemberGradeInfo entity in entitys)
            {
                i = i + 1;
                if (i < entitys.Count)
                {
                    return_value = return_value + entity.Favor_GradeID + ",";
                }
                else
                {
                    return_value = return_value + entity.Favor_GradeID;
                }
            }
        }
        else
        {
            return_value = "0";
        }
        return return_value;
    }

    public virtual string GetPromotionFavorPolicys()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        if (keyword != "输入优惠标题搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "";
        }
        int status = tools.CheckInt(Request["status"]);
        QueryInfo Query = new QueryInfo();
        ProductInfo product = null;
        Query.PageSize = tools.CheckInt(Request["rows"]);
        if (tools.CheckInt(Request["page"]) == 0)
        {
            Query.CurrentPage = 1;
        }
        else
        {
            Query.CurrentPage = tools.CheckInt(Request["page"]);

        }

        int Policy_Status, Policy_IsActive, Policy_IsChecked;
        Policy_Status = tools.CheckInt(Request["Policy_Status"]);
        Policy_IsChecked = tools.CheckInt(Request["Policy_IsChecked"]);
        Policy_IsActive = tools.CheckInt(Request["Policy_IsActive"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorPolicyInfo.Promotion_Policy_Site", "=", Public.GetCurrentSite()));
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorPolicyInfo.Promotion_Policy_Title", "%like%", keyword));
        }
        if (Policy_Status == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "Datediff(d,{PromotionFavorPolicyInfo.Promotion_Policy_Starttime},GetDATE())", ">=", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "Datediff(d,{PromotionFavorPolicyInfo.Promotion_Policy_Endtime},GetDATE())", "<=", "0"));
        }
        if (Policy_Status == 2)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "Datediff(d,{PromotionFavorPolicyInfo.Promotion_Policy_Starttime},GetDATE())", "<", "0"));
        }
        if (Policy_Status == 3)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "Datediff(d,{PromotionFavorPolicyInfo.Promotion_Policy_Endtime},GetDATE())", ">", "0"));
        }
        if (Policy_IsActive > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorPolicyInfo.Promotion_Policy_IsActive", "=", (Policy_IsActive - 1).ToString()));
        }
        if (Policy_IsChecked > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorPolicyInfo.Promotion_Policy_IsChecked", "=", (Policy_IsChecked - 1).ToString()));
        }

        

        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyPolicy.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<PromotionFavorPolicyInfo> entitys = MyPolicy.GetPromotionFavorPolicys(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (PromotionFavorPolicyInfo entity in entitys)
            {

                jsonBuilder.Append("{\"id\":" + entity.Promotion_Policy_ID  + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Policy_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Policy_Title);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(">=" + entity.Promotion_Policy_Payline);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Promotion_Policy_Manner == 1)
                {
                    jsonBuilder.Append(entity.Promotion_Policy_Price + "元");
                }
                else if (entity.Promotion_Policy_Manner == 2)
                {
                    jsonBuilder.Append(entity.Promotion_Policy_Percent + "%");
                }
                else
                {
                    jsonBuilder.Append("派发优惠券");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Policy_Starttime.ToShortDateString() + "至-" + entity.Promotion_Policy_Endtime.ToShortDateString());
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (tools.NullDate(entity.Promotion_Policy_Starttime.ToShortDateString()) > tools.NullDate(DateTime.Now.ToShortDateString()))
                {
                    jsonBuilder.Append("<font color=\\\"#ff0000\\\">未开始</font>");
                }
                else if (tools.NullDate(entity.Promotion_Policy_Endtime.ToShortDateString()) < tools.NullDate(DateTime.Now.ToShortDateString()))
                {
                    jsonBuilder.Append("<font color=\\\"#ff0000\\\">已过期</font>");
                }
                else
                {
                    jsonBuilder.Append("正常");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Promotion_Policy_IsActive == 1)
                {
                    jsonBuilder.Append("已启用");
                }
                else
                {
                    jsonBuilder.Append("未启用");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Promotion_Policy_IsChecked == 1)
                {
                    jsonBuilder.Append("已审核");
                }
                else if (entity.Promotion_Policy_IsChecked == 2)
                {
                    jsonBuilder.Append("审核未通过");
                }
                else if (entity.Promotion_Policy_IsChecked == 0)
                {
                    jsonBuilder.Append("未审核");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Policy_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" align=\\\"absmiddle\\\"> <a href=\\\"Promotion_favor_policy_view.aspx?Promotion_policy_ID=" + entity.Promotion_Policy_ID + "\\\" title=\\\"查看\\\">查看</a>");

                if (Public.CheckPrivilege("6ee2adbb-590f-4c59-b7b3-da7266977106"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_copy.gif\\\" align=\\\"absmiddle\\\"> <a href=\\\"Promotion_favor_policy_copy.aspx?Promotion_policy_ID=" + entity.Promotion_Policy_ID + "\\\" title=\\\"复制\\\">复制</a>");
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    public virtual PromotionFavorPolicyInfo GetPromotionFavorPolicyByID(int ID)
    {
        return MyPolicy.GetPromotionFavorPolicyByID(ID, Public.GetUserPrivilege());
    }

    //展示适用的产品
    public string ShowPolicy_FavorProduct(string valid_type, int Policy_ID)
    {
        string productid = "0";
        string promotionids = "0";
        string[] temp_arry;
        string product_id = "";
        int totalcount = 0;
        IList<string> productids = new List<string>();
        if (valid_type == "valid")
        {
            productids = GetPolicyProducts(Policy_ID);
            if (productids != null)
            {
                foreach (string subproduct in productids)
                {
                    if (subproduct.Split('|')[1] == "0")
                    {
                        totalcount = totalcount + 1;
                        promotionids = promotionids + "," + subproduct.Split('|')[0];
                    }
                    else
                    {
                        productid = productid + "," + subproduct.Split('|')[1];
                        temp_arry = subproduct.Split('|')[1].Split(',');
                        if (temp_arry.Contains(product_id.ToString()))
                        {
                            promotionids = promotionids + "," + subproduct.Split('|')[0];
                        }
                    }
                }
            }
        }
        else
        {
            PromotionFavorPolicyInfo policyinfo = GetPromotionFavorPolicyByID(Policy_ID);
            if (policyinfo != null)
            {
                if (policyinfo.PromotionFavorPolicyExcepts != null)
                {
                    foreach (PromotionFavorPolicyExceptInfo subexcept in policyinfo.PromotionFavorPolicyExcepts)
                    {
                        productid = productid + "," + subexcept.Favor_ProductID;
                    }
                }
            }
        }
        StringBuilder jsonBuilder = new StringBuilder();


        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
        if (totalcount == 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", productid));
        }
        IList<ProductInfo> entitys = MyProduct.GetProductList(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" width=\"600\" bgcolor=\"#B0CADA\">");
            jsonBuilder.Append("    <tr class=\"list_td_bg\">");
            jsonBuilder.Append("        <td width=\"55\" align=\"center\">ID</td>");
            jsonBuilder.Append("        <td width=\"100\" align=\"center\">产品编号</td>");
            jsonBuilder.Append("        <td align=\"center\">产品名称</td>");
            jsonBuilder.Append("        <td align=\"center\">规格</td>");
            jsonBuilder.Append("        <td align=\"center\">生产厂家</td>");
            jsonBuilder.Append("        <td align=\"center\" width=\"80\">操作</td>");
            jsonBuilder.Append("    </tr>");
            foreach (ProductInfo entity in entitys)
            {
                if (product_id == "")
                {
                    product_id = entity.Product_ID.ToString();
                }
                else
                {
                    product_id += "," + entity.Product_ID.ToString();
                }
                jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                jsonBuilder.Append("        <td width=\"55\" align=\"left\">" + entity.Product_ID + "</td>");
                jsonBuilder.Append("        <td width=\"100\" align=\"left\">" + entity.Product_Code + "</td>");
                jsonBuilder.Append("        <td align=\"left\">" + entity.Product_Name + "</td>");
                jsonBuilder.Append("        <td align=\"left\">" + entity.Product_Spec + "</td>");
                jsonBuilder.Append("        <td align=\"left\">" + entity.Product_Maker + "</td>");
                if (valid_type == "valid")
                {
                    jsonBuilder.Append("        <td><img src=\"/images/icon_del.gif\"> <a href=\"javascript:void(0);\" onclick=\"promotionexcept('policy'," + Policy_ID + "," + entity.Product_ID + ")\" title=\"排除\">排除</a></td>");

                }
                else
                {
                    jsonBuilder.Append("        <td><img src=\"/images/icon_ok.gif\"> <a href=\"javascript:void(0);\" onclick=\"promotionvalid('policy'," + Policy_ID + "," + entity.Product_ID + ")\" title=\"恢复\">恢复</a></td>");
                }
                jsonBuilder.Append("    </tr>");
            }
            jsonBuilder.Append("</table>");
            if (product_id == "")
            {
                jsonBuilder = null;
                jsonBuilder = new StringBuilder();
                jsonBuilder.Append("<span class=\"pickertip\">无产品信息</span>");
            }
        }
        else
        {
            jsonBuilder.Append("<span class=\"pickertip\">无产品信息</span>");
        }
        entitys = null;

        return jsonBuilder.ToString();
    }

    public virtual void DelPromotionFavorPolicy()
    {
        int Promotion_policy_ID = tools.CheckInt(Request["Promotion_policy_ID"]);
        if (MyPolicy.DelPromotionFavorPolicy(Promotion_policy_ID, Public.GetUserPrivilege()) > 0)
        {
            MyPolicy.DelPromotionFavorPolicyBrand(Promotion_policy_ID);
            MyPolicy.DelPromotionFavorPolicyCate(Promotion_policy_ID);
            MyPolicy.DelPromotionFavorPolicyProduct(Promotion_policy_ID);
            MyPolicy.DelPromotionFavorPolicyMemberGrade(Promotion_policy_ID);
            MyPolicy.DelPromotionFavorPolicyExcept(Promotion_policy_ID);

            Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_Favor_Policy_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    #endregion

    #region 赠品优惠

    public virtual void Promotion_Gift(string select_name,int Gift_ID)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsGift", "=", "1"));
        
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "DESC"));

        IList<ProductInfo> entitys = MyBLLPRO.GetProductList(Query, Public.GetUserPrivilege());
        Response.Write("<select name=\"" + select_name + "\">");
        Response.Write("<option value=\"0\">选择赠品</option>");
        if (entitys != null)
        {
            
            foreach (ProductInfo entity in entitys)
            {
                if (entity.Product_ID == Gift_ID)
                {
                    Response.Write("<option value=\"" + entity.Product_ID + "\" selected>" + entity.Product_Name + "</option>");
                }
                else
                {
                    Response.Write("<option value=\"" + entity.Product_ID + "\">" + entity.Product_Name + "</option>");
                }
            }
        }
        Response.Write("</select>");
    }

    public virtual void AddPromotionFavorGift()
    {
        bool firstgift = true;
        int Promotion_Gift_ID = 0;
        string Promotion_Gift_Title = tools.CheckStr(Request.Form["Promotion_Gift_Title"]);
        int Promotion_Gift_Target = tools.CheckInt(Request.Form["favor_target"]);
        int Promotion_Gift_Group = tools.CheckInt(Request.Form["Promotion_Gift_Group"]);
        int Promotion_Gift_Limit = tools.CheckInt(Request.Form["Promotion_Gift_Limit"]);
        int Promotion_Gift_IsRepeat = tools.CheckInt(Request.Form["Promotion_Gift_IsRepeat"]);

        int Promotion_Gift_Amount ;
        double Promotion_Gift_Buyamount ;
        int Promotion_Gift_Gift ;
        int Promotion_Gift_Gift_amount ;
        int promottion_gift_maxnum,first;

        DateTime Promotion_Gift_Starttime = tools.NullDate(Request.Form["Promotion_Gift_Starttime"]);
        DateTime Promotion_Gift_Endtime = tools.NullDate(Request.Form["Promotion_Gift_Endtime"]); 
        DateTime Promotion_Gift_Addtime = DateTime.Now;
        string favor_cateid = tools.CheckStr(Request.Form["favor_cateid"]);
        string favor_brandid = tools.CheckStr(Request.Form["favor_brandid"]);
        int favor_cateall = tools.CheckInt(Request.Form["favor_cateall"]);
        int favor_brandall = tools.CheckInt(Request.Form["favor_brandall"]);
        int favor_productall = tools.CheckInt(Request.Form["favor_productall"]);
        string favor_productid = tools.CheckStr(Request.Form["favor_productid"]);
        string favor_gradeid = tools.CheckStr(Request.Form["Member_Grade"]);
        int promotion_gift_group = tools.CheckInt(Request.Form["group"]);
        int Favor_Sort = tools.CheckInt(Request.Form["Favor_Sort"]);

        if (Promotion_Gift_Title.Length == 0)
        {
            Public.Msg("error", "错误提示", "请输入优惠标题", false, "{back}");
        }

        if (favor_cateall == 0 && favor_cateid == "" && Promotion_Gift_Target==0)
        {
            Public.Msg("error", "错误提示", "请选择适用产品类别", false, "{back}");
        }

        if (favor_brandall == 0 && favor_brandid == "" && Promotion_Gift_Target == 0)
        {
            Public.Msg("error", "错误提示", "请选择适用产品品牌", false, "{back}");
        }

        if (favor_productall == 0 && favor_productid == "" && Promotion_Gift_Target == 1)
        {
            Public.Msg("error", "错误提示", "请选择适用产品", false, "{back}");
        }
        if (favor_gradeid.Length == 0)
        {
            Public.Msg("error", "错误信息", "请选择针对会员！", false, "{back}");
        }
        PromotionFavorGiftInfo entity=null;
        PromotionFavorGiftInfo Lastgift = null;
        PromotionFavorGiftBrandInfo giftbrand=null;
        PromotionFavorGiftCateInfo giftcate = null;
        PromotionFavorGiftAmountInfo giftamount = null;
        PromotionFavorGiftAmountInfo giftlastamount = null;
        PromotionFavorGiftGiftInfo giftgift = null;
        PromotionFavorGiftProductInfo giftproduct = null;
        PromotionFavorGiftMemberGradeInfo giftgrade = null;

        first=0;
        for (int i = 1; i <= promotion_gift_group; i++)
        {

            Promotion_Gift_Amount = tools.CheckInt(Request.Form["favor_buy_amount" + i]);
            Promotion_Gift_Buyamount = tools.CheckFloat(Request.Form["favor_buy_money" + i]);
            Promotion_Gift_Gift = tools.CheckInt(Request.Form["promotion_gift1_" + i]);
            Promotion_Gift_Gift_amount = tools.CheckInt(Request.Form["favor_buy_gift1_amount_" + i]);
            promottion_gift_maxnum=tools.CheckInt(Request.Form["maxnum_" + i]);


            if ((Promotion_Gift_Amount > 0 || Promotion_Gift_Buyamount > 0) && Promotion_Gift_Gift > 0 && Promotion_Gift_Gift_amount > 0)
            {
                first = first + 1;
                if (firstgift == true)
                {
                    
                    entity = new PromotionFavorGiftInfo();
                    entity.Promotion_Gift_ID = Promotion_Gift_ID;
                    entity.Promotion_Gift_Title = Promotion_Gift_Title;
                    entity.Promotion_Gift_Target = Promotion_Gift_Target;
                    entity.Promotion_Gift_Group = Promotion_Gift_Group;
                    entity.Promotion_Gift_Limit = Promotion_Gift_Limit;
                    entity.Promotion_Gift_IsRepeat = Promotion_Gift_IsRepeat;
                    entity.Promotion_Gift_Starttime = Promotion_Gift_Starttime;
                    entity.Promotion_Gift_Endtime = Promotion_Gift_Endtime;
                    entity.Promotion_Gift_Addtime = Promotion_Gift_Addtime;
                    entity.Promotion_Gift_IsActive = 1;
                    entity.Promotion_Gift_IsChecked = 0;
                    entity.Promotion_Gift_Sort = Favor_Sort;
                    entity.Promotion_Gift_Site = Public.GetCurrentSite();
                    MyGift.AddPromotionFavorGift(entity, Public.GetUserPrivilege());
                    entity = null;
                    firstgift = false;
                }

                Lastgift = MyGift.GetLastPromotionFavorGift(Public.GetUserPrivilege());

                if (Lastgift != null)
                {
                    if (Lastgift.Promotion_Gift_Title == Promotion_Gift_Title)
                    {
                        if (first == 1)
                        {
                            //添加适用会员等级
                            
                            if (favor_gradeid.Length == 0)
                            {
                                giftgrade = new PromotionFavorGiftMemberGradeInfo();
                                giftgrade.Favor_Id = 0;
                                giftgrade.Favor_GradeID = 0;
                                giftgrade.Promotion_Gift_ID = Lastgift.Promotion_Gift_ID;
                                MyGift.AddPromotionFavorGiftMemberGrade(giftgrade);
                                giftgrade = null;
                            }
                            else
                            {
                                foreach (string substr in favor_gradeid.Split(','))
                                {
                                    if (tools.CheckInt(substr) > 0)
                                    {
                                        giftgrade = new PromotionFavorGiftMemberGradeInfo();
                                        giftgrade.Favor_Id = 0;
                                        giftgrade.Favor_GradeID = tools.CheckInt(substr);
                                        giftgrade.Promotion_Gift_ID = Lastgift.Promotion_Gift_ID;
                                        MyGift.AddPromotionFavorGiftMemberGrade(giftgrade);
                                        giftgrade = null;
                                    }
                                }
                            }

                            if (Promotion_Gift_Target == 0)
                            {
                                //添加适用类别
                                if (favor_cateall == 1)
                                {
                                    giftcate = new PromotionFavorGiftCateInfo();
                                    giftcate.Favor_CateId = 0;
                                    giftcate.Promotion_Gift_ID = Lastgift.Promotion_Gift_ID;
                                    MyGift.AddPromotionFavorGiftCate(giftcate);
                                    giftcate = null;
                                }
                                else
                                {
                                    foreach (string subcate in favor_cateid.Split(','))
                                    {
                                        if (tools.CheckInt(subcate) > 0)
                                        {
                                            giftcate = new PromotionFavorGiftCateInfo();
                                            giftcate.Favor_CateId = tools.CheckInt(subcate);
                                            giftcate.Promotion_Gift_ID = Lastgift.Promotion_Gift_ID;
                                            MyGift.AddPromotionFavorGiftCate(giftcate);
                                            giftcate = null;
                                        }
                                    }
                                }

                                //添加适用品牌
                                if (favor_brandall == 1)
                                {
                                    giftbrand = new PromotionFavorGiftBrandInfo();
                                    giftbrand.Favor_BrandID = 0;
                                    giftbrand.Promotion_Gift_ID = Lastgift.Promotion_Gift_ID;
                                    MyGift.AddPromotionFavorGiftBrand(giftbrand);
                                    giftbrand = null;
                                }
                                else
                                {
                                    foreach (string subbrand in favor_brandid.Split(','))
                                    {
                                        if (tools.CheckInt(subbrand) > 0)
                                        {
                                            giftbrand = new PromotionFavorGiftBrandInfo();
                                            giftbrand.Favor_BrandID = tools.CheckInt(subbrand);
                                            giftbrand.Promotion_Gift_ID = Lastgift.Promotion_Gift_ID;
                                            MyGift.AddPromotionFavorGiftBrand(giftbrand);
                                            giftbrand = null;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //添加适用产品
                                if (favor_productall == 1 && i == 1)
                                {
                                    giftproduct = new PromotionFavorGiftProductInfo();
                                    giftproduct.Favor_ProductID = 0;
                                    giftproduct.Promotion_Gift_ID = Lastgift.Promotion_Gift_ID;
                                    MyGift.AddPromotionFavorGiftProduct(giftproduct);
                                    giftproduct = null;
                                }
                                else
                                {
                                    foreach (string subproduct in favor_productid.Split(','))
                                    {
                                        if (tools.CheckInt(subproduct) > 0)
                                        {
                                            giftproduct = new PromotionFavorGiftProductInfo();
                                            giftproduct.Favor_ProductID = tools.CheckInt(subproduct);
                                            giftproduct.Promotion_Gift_ID = Lastgift.Promotion_Gift_ID;
                                            MyGift.AddPromotionFavorGiftProduct(giftproduct);
                                            giftproduct = null;
                                        }
                                    }
                                }
                            }
                        }
                        

                        //添加优惠额度
                        if (Promotion_Gift_Amount > 0)
                        {
                            Promotion_Gift_Buyamount = 0;
                        }
                        giftamount = new PromotionFavorGiftAmountInfo();
                        giftamount.Gift_Amount_Amount = Promotion_Gift_Amount;
                        giftamount.Gift_Amount_BuyAmount = Promotion_Gift_Buyamount;
                        giftamount.Gift_Amount_GiftID = Lastgift.Promotion_Gift_ID;


                        if (MyGift.AddPromotionFavorGiftAmount(giftamount))
                        {
                            giftlastamount = MyGift.GetLastPromotionFavorGiftAmount();
                            if (giftlastamount != null)
                            {
                                if (giftlastamount.Gift_Amount_GiftID == Lastgift.Promotion_Gift_ID)
                                {
                                    //添加赠品信息
                                    for (int n = 1; n <= promottion_gift_maxnum; n++)
                                    {
                                        Promotion_Gift_Gift = tools.CheckInt(Request.Form["promotion_gift" + n + "_" + i]);
                                        Promotion_Gift_Gift_amount = tools.CheckInt(Request.Form["favor_buy_gift" + n + "_amount_" + i]);

                                        if (Promotion_Gift_Gift > 0 && Promotion_Gift_Gift_amount > 0)
                                        {
                                            giftgift = new PromotionFavorGiftGiftInfo();
                                            giftgift.Gift_Amount = Promotion_Gift_Gift_amount;
                                            giftgift.Gift_ProductID = Promotion_Gift_Gift;
                                            giftgift.Gift_AmountID = giftlastamount.Gift_Amount_ID;
                                            MyGift.AddmotionFavorGiftGift(giftgift);
                                            giftgift = null;
                                        }

                                    }
                                }
                            }
                        }
                        giftamount = null;

                    }
                }
            }
        }
        Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_Favor_Gift_add.aspx");
    }

    public virtual void EditPromotionFavorGiftStatus(int Action_Code)
    {
        string Promotion_Gift_ID = tools.CheckStr(Request["favor_id"]);
        if (Promotion_Gift_ID == "")
        {
            Public.Msg("error", "错误信息", "请选择要操作的优惠信息", false, "{back}");
            return;
        }
        if (tools.Left(Promotion_Gift_ID, 1) == ",") { Promotion_Gift_ID = Promotion_Gift_ID.Remove(0, 1); }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorGiftInfo.Promotion_Gift_ID", "in", Promotion_Gift_ID));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorGiftInfo.Promotion_Gift_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("PromotionFavorGiftInfo.Promotion_Gift_ID", "DESC"));
        IList<PromotionFavorGiftInfo> entitys = MyGift.GetPromotionFavorGifts(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (PromotionFavorGiftInfo entity in entitys)
            {
                switch (Action_Code)
                {
                    case 1:
                        entity.Promotion_Gift_IsActive = 1;
                        break;
                    case 2:
                        entity.Promotion_Gift_IsActive = 0;
                        break;
                    case 3:
                        entity.Promotion_Gift_IsChecked = 1;
                        break;
                    case 4:
                        entity.Promotion_Gift_IsChecked = 2;
                        break;
                }
                MyGift.EditPromotionFavorGift(entity);
            }
            Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_Favor_Gift_List.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }


    }
    
    public virtual void DelPromotionFavorGift()
    {
        int Promotion_Gift_ID = tools.CheckInt(Request.QueryString["Promotion_gift_ID"]);
        if (MyGift.DelPromotionFavorGift(Promotion_Gift_ID,Public.GetUserPrivilege()) > 0)
        {
            MyGift.DelPromotionFavorGiftCate(Promotion_Gift_ID);
            MyGift.DelPromotionFavorGiftBrand(Promotion_Gift_ID);
            MyGift.DelPromotionFavorGiftProduct(Promotion_Gift_ID);
            MyGift.DelPromotionFavorGiftAmount(Promotion_Gift_ID);
            MyGift.DelPromotionFavorGiftMemberGrade(Promotion_Gift_ID);
            MyGift.DelPromotionFavorGiftExcept(Promotion_Gift_ID);
            Public.Msg("positive", "操作成功", "操作成功", true, "Promotion_Favor_Gift_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual string GetPromotionFavorGifts()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        if (keyword != "输入优惠标题搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "";
        }
        int status = tools.CheckInt(Request["status"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        if (tools.CheckInt(Request["page"]) == 0)
        {
            Query.CurrentPage = 1;
        }
        else
        {
            Query.CurrentPage = tools.CheckInt(Request["page"]);
        }

        int Gift_Status, Gift_IsActive, Gift_IsChecked;
        Gift_Status = tools.CheckInt(Request["Gift_Status"]);
        Gift_IsChecked = tools.CheckInt(Request["Gift_IsChecked"]);
        Gift_IsActive = tools.CheckInt(Request["Gift_IsActive"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorGiftInfo.Promotion_Gift_Site", "=", Public.GetCurrentSite()));
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorGiftInfo.Promotion_Gift_Title", "%like%", keyword));
        }
        if (Gift_Status == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "Datediff(d,{PromotionFavorGiftInfo.Promotion_Gift_Starttime},GetDATE())", ">=", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "Datediff(d,{PromotionFavorGiftInfo.Promotion_Gift_Endtime},GetDATE())", "<=", "0"));
        }
        if (Gift_Status == 2)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "Datediff(d,{PromotionFavorGiftInfo.Promotion_Gift_Starttime},GetDATE())", "<", "0"));
        }
        if (Gift_Status == 3)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "Datediff(d,{PromotionFavorGiftInfo.Promotion_Gift_Endtime},GetDATE())", ">", "0"));
        }
        if (Gift_IsActive > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorGiftInfo.Promotion_Gift_IsActive", "=", (Gift_IsActive - 1).ToString()));
        }
        if (Gift_IsChecked > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorGiftInfo.Promotion_Gift_IsChecked", "=", (Gift_IsChecked - 1).ToString()));
        }


        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyGift.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<PromotionFavorGiftInfo> entitys = MyGift.GetPromotionFavorGifts(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (PromotionFavorGiftInfo entity in entitys)
            {

                jsonBuilder.Append("{\"id\":" + entity.Promotion_Gift_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Gift_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Gift_Title);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Gift_Starttime.ToShortDateString() + "至-" + entity.Promotion_Gift_Endtime.ToShortDateString());
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (tools.NullDate(entity.Promotion_Gift_Starttime.ToShortDateString()) > tools.NullDate(DateTime.Now.ToShortDateString()))
                {
                    jsonBuilder.Append("<font color=\\\"#ff0000\\\">未开始</font>");
                }
                else if (tools.NullDate(entity.Promotion_Gift_Endtime.ToShortDateString()) < tools.NullDate(DateTime.Now.ToShortDateString()))
                {
                    jsonBuilder.Append("<font color=\\\"#ff0000\\\">已过期</font>");
                }
                else
                {
                    jsonBuilder.Append("正常");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Promotion_Gift_IsActive == 1)
                {
                    jsonBuilder.Append("已启用");
                }
                else
                {
                    jsonBuilder.Append("未启用");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Promotion_Gift_IsChecked == 1)
                {
                    jsonBuilder.Append("已审核");
                }
                else if (entity.Promotion_Gift_IsChecked == 2)
                {
                    jsonBuilder.Append("审核未通过");
                }
                else if (entity.Promotion_Gift_IsChecked == 0)
                {
                    jsonBuilder.Append("未审核");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Promotion_Gift_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" align=\\\"absmiddle\\\"> <a href=\\\"Promotion_favor_gift_view.aspx?Promotion_gift_ID=" + entity.Promotion_Gift_ID + "\\\" title=\\\"查看\\\">查看</a>");

                if (Public.CheckPrivilege("b2170460-e90d-4b4f-89b6-c88b75c2989b"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_copy.gif\\\" align=\\\"absmiddle\\\"> <a href=\\\"Promotion_favor_gift_copy.aspx?Promotion_gift_ID=" + entity.Promotion_Gift_ID + "\\\" title=\\\"复制\\\">复制</a>");
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    public virtual string Get_Gift_Cate_String(IList<PromotionFavorGiftCateInfo> entitys)
    {
        string return_value = "";
        int i = 0;
        if (entitys != null)
        {
            foreach (PromotionFavorGiftCateInfo entity in entitys)
            {
                i = i + 1;
                if (i < entitys.Count)
                {
                    return_value = return_value + entity.Favor_CateId + ",";
                }
                else
                {
                    return_value = return_value + entity.Favor_CateId;
                }
            }
        }
        else
        {
            return_value = "0";
        }
        return return_value;
    }

    public virtual string Get_Gift_Brand_String(IList<PromotionFavorGiftBrandInfo> entitys)
    {
        string return_value = "";
        int i = 0;
        if (entitys != null)
        {
            foreach (PromotionFavorGiftBrandInfo entity in entitys)
            {
                i = i + 1;
                if (i < entitys.Count)
                {
                    return_value = return_value + entity.Favor_BrandID + ",";
                }
                else
                {
                    return_value = return_value + entity.Favor_BrandID;
                }
            }
        }
        else
        {
            return_value = "0";
        }
        return return_value;
    }

    public virtual string Get_Gift_Product_String(IList<PromotionFavorGiftProductInfo> entitys)
    {
        string return_value = "";
        int i = 0;
        if (entitys != null)
        {
            foreach (PromotionFavorGiftProductInfo entity in entitys)
            {
                i = i + 1;
                if (i < entitys.Count)
                {
                    return_value = return_value + entity.Favor_ProductID + ",";
                }
                else
                {
                    return_value = return_value + entity.Favor_ProductID;
                }
            }
        }
        else
        {
            return_value = "0";
        }
        return return_value;
    }

    public virtual string Get_Gift_MemberGrade_String(IList<PromotionFavorGiftMemberGradeInfo> entitys)
    {
        string return_value = "";
        int i = 0;
        if (entitys != null)
        {
            foreach (PromotionFavorGiftMemberGradeInfo entity in entitys)
            {
                i = i + 1;
                if (i < entitys.Count)
                {
                    return_value = return_value + entity.Favor_GradeID + ",";
                }
                else
                {
                    return_value = return_value + entity.Favor_GradeID;
                }
            }
        }
        else
        {
            return_value = "0";
        }
        return return_value;
    }

    public virtual PromotionFavorGiftInfo GetPromotionFavorGiftByID(int ID)
    {
        return MyGift.GetPromotionFavorGiftByID(ID, Public.GetUserPrivilege());
    }

    //展示适用的产品
    public string ShowGift_FavorProduct(string valid_type, int Gift_ID)
    {
        string productid = "0";
        string promotionids = "0";
        string[] temp_arry;
        string product_id = "";
        int totalcount = 0;
        IList<string> productids = new List<string>();
        if (valid_type == "valid")
        {
            productids = GetGiftProducts(Gift_ID);
            if (productids != null)
            {
                foreach (string subproduct in productids)
                {
                    if (subproduct.Split('|')[1] == "0")
                    {
                        totalcount = totalcount + 1;
                        promotionids = promotionids + "," + subproduct.Split('|')[0];
                    }
                    else
                    {
                        productid = productid + "," + subproduct.Split('|')[1];
                        temp_arry = subproduct.Split('|')[1].Split(',');
                        if (temp_arry.Contains(product_id.ToString()))
                        {
                            promotionids = promotionids + "," + subproduct.Split('|')[0];
                        }
                    }
                }
            }
        }
        else
        {
            PromotionFavorGiftInfo giftinfo = GetPromotionFavorGiftByID(Gift_ID);
            if (giftinfo != null)
            {
                if (giftinfo.PromotionFavorGiftExcepts != null)
                {
                    foreach (PromotionFavorGiftExceptInfo subexcept in giftinfo.PromotionFavorGiftExcepts)
                    {
                        productid = productid + "," + subexcept.Favor_ProductID;
                    }
                }
            }
        }
        StringBuilder jsonBuilder = new StringBuilder();


        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
        if (totalcount == 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", productid));
        }
        IList<ProductInfo> entitys = MyProduct.GetProductList(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" width=\"600\" bgcolor=\"#B0CADA\">");
            jsonBuilder.Append("    <tr class=\"list_td_bg\">");
            jsonBuilder.Append("        <td width=\"55\" align=\"center\">ID</td>");
            jsonBuilder.Append("        <td width=\"100\" align=\"center\">产品编号</td>");
            jsonBuilder.Append("        <td align=\"center\">产品名称</td>");
            jsonBuilder.Append("        <td align=\"center\">规格</td>");
            jsonBuilder.Append("        <td align=\"center\">生产厂家</td>");
            jsonBuilder.Append("        <td align=\"center\" width=\"80\">操作</td>");
            jsonBuilder.Append("    </tr>");
            foreach (ProductInfo entity in entitys)
            {
                if (product_id == "")
                {
                    product_id = entity.Product_ID.ToString();
                }
                else
                {
                    product_id += "," + entity.Product_ID.ToString();
                }
                jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                jsonBuilder.Append("        <td width=\"55\" align=\"left\">" + entity.Product_ID + "</td>");
                jsonBuilder.Append("        <td width=\"100\" align=\"left\">" + entity.Product_Code + "</td>");
                jsonBuilder.Append("        <td align=\"left\">" + entity.Product_Name + "</td>");
                jsonBuilder.Append("        <td align=\"left\">" + entity.Product_Spec + "</td>");
                jsonBuilder.Append("        <td align=\"left\">" + entity.Product_Maker + "</td>");
                if (valid_type == "valid")
                {
                    jsonBuilder.Append("        <td><img src=\"/images/icon_del.gif\"> <a href=\"javascript:void(0);\" onclick=\"promotionexcept('gift'," + Gift_ID + "," + entity.Product_ID + ")\" title=\"排除\">排除</a></td>");

                }
                else
                {
                    jsonBuilder.Append("        <td><img src=\"/images/icon_ok.gif\"> <a href=\"javascript:void(0);\" onclick=\"promotionvalid('gift'," + Gift_ID + "," + entity.Product_ID + ")\" title=\"恢复\">恢复</a></td>");
                }
                jsonBuilder.Append("    </tr>");
            }
            jsonBuilder.Append("</table>");
            if (product_id == "")
            {
                jsonBuilder = null;
                jsonBuilder = new StringBuilder();
                jsonBuilder.Append("<span class=\"pickertip\">无产品信息</span>");
            }
        }
        else
        {
            jsonBuilder.Append("<span class=\"pickertip\">无产品信息</span>");
        }
        entitys = null;

        return jsonBuilder.ToString();
    }

    #endregion

    #region 优惠排除

    public virtual string GetPromotionFavorExcepts()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        if (keyword != "输入商品编码、商品名称、通用名、生产企业、拼音首字母进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "";
        }
        QueryInfo Query = new QueryInfo();
        ProductInfo product = null;
        Query.PageSize = tools.CheckInt(Request["rows"]);
        if (tools.CheckInt(Request["page"]) == 0)
        {
            Query.CurrentPage = 1;
        }
        else
        {
            Query.CurrentPage = tools.CheckInt(Request["page"]);

        }

        int Fee_multi, policy_multi, gift_multi;
        Fee_multi = tools.CheckInt(Request["Fee_multi"]);
        policy_multi = tools.CheckInt(Request["policy_multi"]);
        gift_multi = tools.CheckInt(Request["gift_multi"]);
        IList<string> productids = new List<string>();
        int totalcount=0;
        string productid="0";
        string repeatarry="0";
        string norepeatarry = "0";
        IList<string> list = new List<string>();
        IList<string> list1 = new List<string>();
        //运费优惠
        if (Fee_multi > 0)
        {
            productids = GetFeeProducts(0);
            if (productids != null)
            {
                foreach (string subproduct in productids)
                {
                    if (subproduct.Split('|')[1] == "0")
                    {
                        totalcount = totalcount + 1;
                    }
                    else
                    {
                        productid = productid + "," + subproduct.Split('|')[1];
                        
                    }
                }

                foreach (string substr in productid.Split(','))
                {
                    if (list1.Contains(substr))
                    {
                        list.Add(substr);
                    }
                    else
                    {
                        list1.Add(substr);
                    }
                }
                foreach (string substr in list.Distinct())
                {
                    if (repeatarry.Length > 0)
                    {
                        repeatarry += "," + substr;
                    }
                    else
                    {
                        repeatarry = substr;
                    }
                }

                if (totalcount == 0)
                {
                    if (Fee_multi == 2)
                    {
                        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", repeatarry));
                    }
                    else
                    {
                        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", productid));
                        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "not in", repeatarry));
                    }
                }
                else if (totalcount == 1)
                {
                    if (Fee_multi == 1)
                    {
                        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "not in", productid));
                    }
                    else
                    {
                        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", productid));
                    }
                }
            }
            else
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "=", "0"));
            }
        }
        //政策优惠
        if (policy_multi > 0)
        {
            productids = GetPolicyProducts(0);
            if (productids != null)
            {
                foreach (string subproduct in productids)
                {
                    if (subproduct.Split('|')[1] == "0")
                    {
                        totalcount = totalcount + 1;
                    }
                    else
                    {
                        productid = productid + "," + subproduct.Split('|')[1];

                    }
                }
                foreach (string substr in productid.Split(','))
                {
                    if (list1.Contains(substr))
                    {
                        list.Add(substr);
                    }
                    else
                    {
                        list1.Add(substr);
                    }
                }
                foreach (string substr in list.Distinct())
                {
                    if (repeatarry.Length > 0)
                    {
                        repeatarry += "," + substr;
                    }
                    else
                    {
                        repeatarry = substr;
                    }
                }

                if (totalcount == 0)
                {
                    if (policy_multi == 2)
                    {
                        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", repeatarry));
                    }
                    else
                    {
                        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", productid));
                        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "not in", repeatarry));
                    }
                }
                else if (totalcount == 1)
                {
                    if (policy_multi == 1)
                    {
                        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "not in", productid));
                    }
                    else
                    {
                        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", productid));
                    }
                }
            }
            else
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "=", "0"));
            }
        }
        //赠品优惠
        if (gift_multi > 0)
        {
            productids = GetGiftProducts(0);
            if (productids != null)
            {
                foreach (string subproduct in productids)
                {
                    if (subproduct.Split('|')[1] == "0")
                    {
                        totalcount = totalcount + 1;
                    }
                    else
                    {
                        productid = productid + "," + subproduct.Split('|')[1];

                    }
                }
                foreach (string substr in productid.Split(','))
                {
                    if (list1.Contains(substr))
                    {
                        list.Add(substr);
                    }
                    else
                    {
                        list1.Add(substr);
                    }
                }
                foreach (string substr in list.Distinct())
                {
                    if (repeatarry.Length > 0)
                    {
                        repeatarry += "," + substr;
                    }
                    else
                    {
                        repeatarry = substr;
                    }
                }

                if (totalcount == 0)
                {
                    if (gift_multi == 2)
                    {
                        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", repeatarry));
                    }
                    else
                    {
                        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", productid));
                        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "not in", repeatarry));
                    }
                }
                else if (totalcount == 1)
                {
                    if (gift_multi == 1)
                    {
                        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "not in", productid));
                    }
                    else
                    {
                        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", productid));
                    }
                }
            }
            else
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "=", "0"));
            }
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", Public.GetCurrentSite()));
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_Name", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_Code", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_SubName", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_Maker", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_NameInitials", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_SubNameInitials", "like", keyword));
        }



        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyProduct.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<ProductInfo> entitys = MyProduct.GetProductList(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ProductInfo entity in entitys)
            {

                jsonBuilder.Append("{\"id\":" + entity.Product_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Product_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Product_Code);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("" + entity.Product_Name);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("" + entity.Product_Spec);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("" + entity.Product_Maker);
                jsonBuilder.Append("\",");

                

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    public virtual string GetSubPromotions()
    {


        int Fee_multi, policy_multi, gift_multi,product_id,i;
        string promotion_type;
        promotion_type = Request["promotion_type"];
        Fee_multi = tools.CheckInt(Request["Fee_multi"]);
        policy_multi = tools.CheckInt(Request["policy_multi"]);
        gift_multi = tools.CheckInt(Request["gift_multi"]);
        product_id=tools.CheckInt(Request["id"]);
        switch (promotion_type)
        { 
            case "fee":
                Fee_multi = 1;
                policy_multi = 0;
                gift_multi = 0;
                break;
            case "policy":
                Fee_multi = 0;
                policy_multi = 1;
                gift_multi = 0;
                break;
            case "gift":
                Fee_multi = 0;
                policy_multi = 0;
                gift_multi = 1;
                break;
        }
        IList<string> productids = new List<string>();
        int totalcount = 0;
        string productid = "0";
        string promotionids = "0";
        string[] temp_arry;
        if (Fee_multi > 0)
        {
            promotion_type = "fee";
            productids = GetFeeProducts(0);
            if (productids != null)
            {
                foreach (string subproduct in productids)
                {
                    if (subproduct.Split('|')[1] == "0")
                    {
                        totalcount = totalcount + 1;
                        promotionids = promotionids + "," + subproduct.Split('|')[0];
                    }
                    else
                    {
                        productid = productid + "," + subproduct.Split('|')[1];
                        temp_arry = subproduct.Split('|')[1].Split(',');
                        if (temp_arry.Contains(product_id.ToString()))
                        {
                            promotionids = promotionids + "," + subproduct.Split('|')[0];
                        }
                    }
                }
            }
        }
        if (policy_multi > 0)
        {
            promotion_type = "policy";
            productids = GetPolicyProducts(0);
            if (productids != null)
            {
                foreach (string subproduct in productids)
                {
                    if (subproduct.Split('|')[1] == "0")
                    {
                        totalcount = totalcount + 1;
                        promotionids = promotionids + "," + subproduct.Split('|')[0];
                    }
                    else
                    {
                        productid = productid + "," + subproduct.Split('|')[1];
                        temp_arry = subproduct.Split('|')[1].Split(',');
                        if (temp_arry.Contains(product_id.ToString()))
                        {
                            promotionids = promotionids + "," + subproduct.Split('|')[0];
                        }
                    }
                }
            }
        }
        if (gift_multi > 0)
        {
            promotion_type = "gift";
            productids = GetGiftProducts(0);
            if (productids != null)
            {
                foreach (string subproduct in productids)
                {
                    if (subproduct.Split('|')[1] == "0")
                    {
                        totalcount = totalcount + 1;
                        promotionids = promotionids + "," + subproduct.Split('|')[0];
                    }
                    else
                    {
                        productid = productid + "," + subproduct.Split('|')[1];
                        temp_arry = subproduct.Split('|')[1].Split(',');
                        if (temp_arry.Contains(product_id.ToString()))
                        {
                            promotionids = promotionids + "," + subproduct.Split('|')[0];
                        }
                    }
                }
            }
        }
        if (promotionids != "0")
        {
            promotionids = promotionids.Substring(2);
        }
        else
        {
            return "";
        }
        temp_arry = promotionids.Split(',');
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":1,\"total\":1,\"records\":1,\"rows\"");
            jsonBuilder.Append(":[");

            for (i = 0; i < temp_arry.Length; i++)
            {
                jsonBuilder.Append("{\"id\":"+(i+1)+",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append((i + 1));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Fee_multi > 0)
                {
                    PromotionFavorFeeInfo entity = MyBLL.GetPromotionFavorFeeByID(tools.CheckInt(temp_arry[i]), Public.GetUserPrivilege());
                    if (entity != null)
                    {
                        jsonBuilder.Append(entity.Promotion_Fee_Title);
                    }
                    else
                    {
                        jsonBuilder.Append("未知");
                    }
                }
                if (policy_multi > 0)
                {
                    PromotionFavorPolicyInfo entity = MyPolicy.GetPromotionFavorPolicyByID(tools.CheckInt(temp_arry[i]), Public.GetUserPrivilege());
                    if (entity != null)
                    {
                        jsonBuilder.Append(entity.Promotion_Policy_Title);
                    }
                    else
                    {
                        jsonBuilder.Append("未知");
                    }
                }
                if (gift_multi > 0)
                {
                    PromotionFavorGiftInfo entity = MyGift.GetPromotionFavorGiftByID(tools.CheckInt(temp_arry[i]), Public.GetUserPrivilege());
                    if (entity != null)
                    {
                        jsonBuilder.Append(entity.Promotion_Gift_Title);
                    }
                    else
                    {
                        jsonBuilder.Append("未知");
                    }
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\" <img src=\\\"/images/icon_del.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"promotionexcept('" + promotion_type + "'," + temp_arry[i] + "," + product_id + ")\\\" title=\\\"排除\\\">排除</a>");
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");

            }

            

            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
    }

    public IList<string> GetFeeProducts(int Fee_ID)
    {
        return MyFavor.GetFeeProducts(Fee_ID, Public.GetCurrentSite());
    }

    public IList<string> GetPolicyProducts(int Policy_ID)
    {
        return MyFavor.GetPolicyProducts(Policy_ID, Public.GetCurrentSite());
    }

    public IList<string> GetGiftProducts(int Gift_ID)
    {
        return MyFavor.GetGiftProducts(Gift_ID, Public.GetCurrentSite());
        
    }

    public void AddPromotionExcept()
    {
        string promotion_type;
        int promotion_id, product_id;
        promotion_type = Request["promotion_type"];
        promotion_id = tools.CheckInt(Request["promotion_id"]);
        product_id = tools.CheckInt(Request["product_id"]);
        switch (promotion_type)
        { 
            case "fee":
                PromotionFavorFeeExceptInfo entity = MyBLL.GetPromotionFavorFeeExceptByID(promotion_id,product_id);
                if (entity == null)
                {
                    entity = new PromotionFavorFeeExceptInfo();
                    entity.Favor_ProductId = product_id;
                    entity.Promotion_Fee_ID = promotion_id;
                    MyBLL.AddPromotionFavorFeeExcept(entity);
                }
                break;
            case "policy":
                PromotionFavorPolicyExceptInfo entity1 = MyPolicy.GetPromotionFavorPolicyExceptByID(promotion_id,product_id);
                if (entity1 == null)
                {
                    entity1=new PromotionFavorPolicyExceptInfo();
                    entity1.Favor_ProductID = product_id;
                    entity1.Promotion_Policy_ID = promotion_id;
                    MyPolicy.AddPromotionFavorPolicyExcept(entity1);
                }
                break;
            case "gift":
                PromotionFavorGiftExceptInfo entity2 = MyGift.GetPromotionFavorGiftExceptByID(promotion_id, product_id);
                if (entity2 == null)
                {
                    entity2 = new PromotionFavorGiftExceptInfo();
                    entity2.Favor_ProductID = product_id;
                    entity2.Promotion_Gift_ID = promotion_id;
                    MyGift.AddPromotionFavorGiftExcept(entity2);
                }
                break;
        }
    }

    public void DelPromotionExcept()
    {
        string promotion_type;
        int promotion_id, product_id;
        promotion_type = Request["promotion_type"];
        promotion_id = tools.CheckInt(Request["promotion_id"]);
        product_id = tools.CheckInt(Request["product_id"]);
        switch (promotion_type)
        {
            case "fee":
                MyBLL.DelPromotionFavorFeeExceptByID(promotion_id, product_id);
                break;
            case "policy":
                MyPolicy.DelPromotionFavorPolicyExceptByID(promotion_id, product_id);
                break;
            case "gift":
                MyGift.DelPromotionFavorGiftExceptByID(promotion_id, product_id);
                break;
        }
    }

    #endregion


}
