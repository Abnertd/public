using System;
using System.Text;
using System.Data;
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
using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.BLL.Product;

/// <summary>
///供应商店铺类
/// </summary>
public class Shop
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IEncrypt encrypt;
    private Addr addr;
    private ISupplierShop MyBLL;
    private ISupplierShopBanner MyBanner;
    private ISupplierShopCss MyCss;
    private ISupplier MySupplier;
    private ISupplierShopPages MyPages;
    private ISupplierShopArticle MyArticle;
    private ISupplierShopEvaluate MyEvaluate;
    private IProductReview Myreview;
    private IProductReviewConfig Myconfig;
    private Member member;
    private Product ProApp;
    private Supplier supplier;
    private Orders order;
    private ISupplierShopDomain MyDomain;
    private ISupplierShopGrade MyShopGrade;
    private Contract contract;


    public Shop()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = SupplierShopFactory.CreateSupplierShop();
        MyBanner = SupplierShopBannerFactory.CreateSupplierShopBanner();
        MyCss = SupplierShopCssFactory.CreateSupplierShopCss();
        MySupplier = SupplierFactory.CreateSupplier();
        MyPages = SupplierShopPagesFactory.CreateSupplierShopPages();
        MyArticle = SupplierShopArticleFactory.CreateSupplierShopArticle();
        MyEvaluate = SupplierShopEvaluateFactory.CreateSupplierShopEvaluate();
        Myreview = ProductReviewFactory.CreateProductReview();
        Myconfig = ProductReviewConfigFactory.CreateProductReviewConfig();
        member = new Member();
        ProApp = new Product();
        supplier = new Supplier();
        order = new Orders();
        contract = new Contract();
        MyDomain = SupplierShopDomainFactory.CreateSupplierShopDomain();
        MyShopGrade = SupplierShopGradeFactory.CreateSupplierShopGrade();
    }


    public SupplierInfo GetSupplierByID(int ID)
    {
        return MySupplier.GetSupplierByID(ID, Public.GetUserPrivilege());
    }

    //获取店铺类型
    public virtual string Get_Shop_Type(int Shop_Grade)
    {
        string Grade_Name = "--";
        SupplierShopGradeInfo shopgradeinfo = GetSupplierShopGradeByID(Shop_Grade);
        if (shopgradeinfo != null)
        {
            Grade_Name = shopgradeinfo.Shop_Grade_Name;
        }
        return Grade_Name;
    }



    #region 店铺样式管理

    public virtual void AddSupplierShopCss()
    {
        int Shop_Css_ID = tools.CheckInt(Request.Form["Shop_Css_ID"]);
        string Shop_Css_Title = tools.CheckStr(Request.Form["Shop_Css_Title"]);
        string Shop_Css_Target = tools.CheckStr(Request.Form["Shop_Css_Target"]);
        string Shop_Css_GapColor = tools.CheckStr(Request.Form["Shop_Css_GapColor"]);
        int Shop_Css_IsActive = tools.CheckInt(Request.Form["Shop_Css_IsActive"]);
        string Shop_Css_Img = tools.CheckStr(Request.Form["Shop_Css_Img"]);
        string Shop_Css_Site = Public.GetCurrentSite();

        if (Shop_Css_Title.Length == 0)
        {
            Public.Msg("error", "错误信息", "请将样式名称填写完整！", false, "{back}");
        }
        if (Shop_Css_Img.Length == 0)
        {
            Public.Msg("error", "错误信息", "请上传样式预览图片！", false, "{back}");
        }
        SupplierShopCssInfo entity = new SupplierShopCssInfo();
        entity.Shop_Css_ID = Shop_Css_ID;
        entity.Shop_Css_Title = Shop_Css_Title;
        entity.Shop_Css_Target = Shop_Css_Target;
        entity.Shop_Css_GapColor = Shop_Css_GapColor;
        entity.Shop_Css_Img = Shop_Css_Img;
        entity.Shop_Css_IsActive = Shop_Css_IsActive;
        entity.Shop_Css_Site = Shop_Css_Site;

        if (MyCss.AddSupplierShopCss(entity, Public.GetUserPrivilege()))
        {
            SaveRelateSupplier(GetLastShopCssID());
            Public.Msg("positive", "操作成功", "操作成功", true, "Shop_Css_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void SaveRelateSupplier(int Css_ID)
    {
        string Supplier_Message_SupplierID = tools.CheckStr(Request.Form["Supplier_Message_SupplierID"]);
        MyCss.DelSupplierShopCssRelateSupplierByCssID(Css_ID);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", Public.GetCurrentSite()));

        if (Supplier_Message_SupplierID.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_ID", "in", Supplier_Message_SupplierID));
        }
        SupplierShopCssRelateSupplierInfo entity = null;
        IList<SupplierInfo> entitys = MySupplier.GetSuppliers(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (SupplierInfo supplierinfo in entitys)
            {
                entity = new SupplierShopCssRelateSupplierInfo();
                entity.Relate_ID = 0;
                entity.Relate_SupplierID = supplierinfo.Supplier_ID;
                entity.Relate_ShopCssID = Css_ID;

                MyCss.AddSupplierShopCssRelateSupplier(entity);
                entity = null;
            }
        }
    }

    public virtual void EditSupplierShopCss()
    {
        int Shop_Css_ID = tools.CheckInt(Request.Form["Shop_Css_ID"]);
        string Shop_Css_Title = tools.CheckStr(Request.Form["Shop_Css_Title"]);
        string Shop_Css_Target = tools.CheckStr(Request.Form["Shop_Css_Target"]);
        string Shop_Css_GapColor = tools.CheckStr(Request.Form["Shop_Css_GapColor"]);
        string Shop_Css_Img = tools.CheckStr(Request.Form["Shop_Css_Img"]);
        int Shop_Css_IsActive = tools.CheckInt(Request.Form["Shop_Css_IsActive"]);
        if (Shop_Css_Title.Length == 0)
        {
            Public.Msg("error", "错误信息", "请将样式名称填写完整！", false, "{back}");
        }
        if (Shop_Css_Img.Length == 0)
        {
            Public.Msg("error", "错误信息", "请上传样式预览图片！", false, "{back}");
        }
        SupplierShopCssInfo entity = GetSupplierShopCssByID(Shop_Css_ID);
        if (entity != null)
        {
            entity.Shop_Css_ID = Shop_Css_ID;
            entity.Shop_Css_Title = Shop_Css_Title;
            entity.Shop_Css_Target = Shop_Css_Target;
            entity.Shop_Css_GapColor = Shop_Css_GapColor;
            entity.Shop_Css_IsActive = Shop_Css_IsActive;
            entity.Shop_Css_Img = Shop_Css_Img;
            if (MyCss.EditSupplierShopCss(entity, Public.GetUserPrivilege()))
            {
                SaveRelateSupplier(Shop_Css_ID);
                Public.Msg("positive", "操作成功", "操作成功", true, "Shop_Css_list.aspx");
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

    public virtual void DelSupplierShopCss()
    {
        int Shop_Css_ID = tools.CheckInt(Request.QueryString["Shop_Css_ID"]);
        if (MyCss.DelSupplierShopCss(Shop_Css_ID, Public.GetUserPrivilege()) > 0)
        {
            MyCss.DelSupplierShopCssRelateSupplierByCssID(Shop_Css_ID);
            Public.Msg("positive", "操作成功", "操作成功", true, "Shop_Css_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public IList<SupplierInfo> GetSupplierShopCssRelateSuppler(int Css_ID)
    {
        string supplier_ids = "0";
        IList<SupplierShopCssRelateSupplierInfo> suppliers = MyCss.GetSupplierShopCssRelateSuppliersByCss(Css_ID);
        if (suppliers != null)
        {
            foreach (SupplierShopCssRelateSupplierInfo entity in suppliers)
            {
                supplier_ids += "," + entity.Relate_SupplierID;
            }
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", Public.GetCurrentSite()));

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_ID", "in", supplier_ids));

        IList<SupplierInfo> entitys = MySupplier.GetSuppliers(Query, Public.GetUserPrivilege());
        return entitys;
    }

    public string GetSupplierShopCsses()
    {
        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopCssInfo.Shop_Css_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyCss.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<SupplierShopCssInfo> entitys = MyCss.GetSupplierShopCsss(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierShopCssInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.Shop_Css_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Shop_Css_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Shop_Css_Title));
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                if (entity.Shop_Css_IsActive == 1)
                {
                    jsonBuilder.Append("启用");
                }
                else
                {
                    jsonBuilder.Append("未启用");
                }
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("227ca224-42de-48c6-9e4b-d09d019f7b36"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\" align=\\\"absmiddle\\\"> <a href=\\\"Shop_Css_edit.aspx?Shop_Css_ID=" + entity.Shop_Css_ID + "\\\" title=\\\"修改\\\">修改</a>");

                }
                if (Public.CheckPrivilege("8407715f-18d7-445b-92a1-0c7ce9cc027a"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Shop_Css_do.aspx?action=move&Shop_Css_ID=" + entity.Shop_Css_ID + "')\\\" title=\\\"删除\\\">删除</a>");

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

    public int GetLastShopCssID()
    {
        int Css_ID = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopCssInfo.Shop_Css_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopCssInfo.Shop_Css_ID", "desc"));

        IList<SupplierShopCssInfo> entitys = MyCss.GetSupplierShopCsss(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            Css_ID = entitys[0].Shop_Css_ID;
        }
        return Css_ID;
    }

    public virtual SupplierShopCssInfo GetSupplierShopCssByID(int cate_id)
    {
        return MyCss.GetSupplierShopCssByID(cate_id, Public.GetUserPrivilege());
    }


    #endregion

    #region 店铺Banner管理
    public virtual void AddSupplierShopBanner()
    {
        int Shop_Banner_ID = tools.CheckInt(Request.Form["Shop_Banner_ID"]);
        int Shop_Banner_Type = tools.CheckInt(Request.Form["Shop_Banner_Type"]);
        string Shop_Banner_Name = tools.CheckStr(Request.Form["Shop_Banner_Name"]);
        string Shop_Banner_Url = tools.CheckStr(Request.Form["Shop_Banner_Img"]);
        int Shop_Banner_IsActive = tools.CheckInt(Request.Form["Shop_Banner_IsActive"]);
        string Shop_Banner_Site = Public.GetCurrentSite();
        if (Shop_Banner_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请将Banner名称填写完整！", false, "{back}");
        }
        if (Shop_Banner_Url.Length == 0)
        {
            Public.Msg("error", "错误信息", "请上传Banner图片！", false, "{back}");
        }
        SupplierShopBannerInfo entity = new SupplierShopBannerInfo();
        entity.Shop_Banner_ID = Shop_Banner_ID;
        entity.Shop_Banner_Type = Shop_Banner_Type;
        entity.Shop_Banner_Name = Shop_Banner_Name;
        entity.Shop_Banner_Url = Shop_Banner_Url;
        entity.Shop_Banner_IsActive = Shop_Banner_IsActive;
        entity.Shop_Banner_Site = Shop_Banner_Site;

        if (MyBanner.AddSupplierShopBanner(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Shop_Banner_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditSupplierShopBanner()
    {

        int Shop_Banner_ID = tools.CheckInt(Request.Form["Shop_Banner_ID"]);
        int Shop_Banner_Type = tools.CheckInt(Request.Form["Shop_Banner_Type"]);
        string Shop_Banner_Name = tools.CheckStr(Request.Form["Shop_Banner_Name"]);
        string Shop_Banner_Url = tools.CheckStr(Request.Form["Shop_Banner_Img"]);
        int Shop_Banner_IsActive = tools.CheckInt(Request.Form["Shop_Banner_IsActive"]);
        if (Shop_Banner_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请将Banner名称填写完整！", false, "{back}");
        }
        if (Shop_Banner_Url.Length == 0)
        {
            Public.Msg("error", "错误信息", "请上传Banner图片！", false, "{back}");
        }
        SupplierShopBannerInfo entity = GetSupplierShopBannerByID(Shop_Banner_ID);
        if (entity != null)
        {
            entity.Shop_Banner_ID = Shop_Banner_ID;
            entity.Shop_Banner_Type = Shop_Banner_Type;
            entity.Shop_Banner_Name = Shop_Banner_Name;
            entity.Shop_Banner_Url = Shop_Banner_Url;
            entity.Shop_Banner_IsActive = Shop_Banner_IsActive;
            if (MyBanner.EditSupplierShopBanner(entity, Public.GetUserPrivilege()))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "Shop_Banner_list.aspx");
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

    public virtual void DelSupplierShopBanner()
    {
        int Shop_Banner_ID = tools.CheckInt(Request.QueryString["Shop_Banner_ID"]);
        if (MyBanner.DelSupplierShopBanner(Shop_Banner_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Shop_Banner_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public string GetSupplierShopBanners()
    {
        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopBannerInfo.Shop_Banner_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBanner.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<SupplierShopBannerInfo> entitys = MyBanner.GetSupplierShopBanners(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierShopBannerInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.Shop_Banner_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Shop_Banner_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Shop_Banner_Name));
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                if (entity.Shop_Banner_IsActive == 1)
                {
                    jsonBuilder.Append("启用");
                }
                else
                {
                    jsonBuilder.Append("未启用");
                }
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("9f2a1a11-c019-4443-b6eb-18ab1483e0b9"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\" align=\\\"absmiddle\\\"> <a href=\\\"Shop_Banner_edit.aspx?Shop_Banner_ID=" + entity.Shop_Banner_ID + "\\\" title=\\\"修改\\\">修改</a>");

                }
                if (Public.CheckPrivilege("a574ef1a-b5ce-43ba-ab38-3470a9896237"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Shop_Banner_do.aspx?action=move&Shop_Banner_ID=" + entity.Shop_Banner_ID + "')\\\" title=\\\"删除\\\">删除</a>");

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

    public virtual SupplierShopBannerInfo GetSupplierShopBannerByID(int cate_id)
    {
        return MyBanner.GetSupplierShopBannerByID(cate_id, Public.GetUserPrivilege());
    }

    #endregion

    #region 店铺管理
    /// <summary>
    /// 获取指定供应商店铺信息
    /// </summary>
    /// <param name="Supplier_ID">供应商ID</param>
    /// <returns>店铺信息</returns>
    public SupplierShopInfo GetSupplierShopBySupplierID(int Supplier_ID)
    {
        SupplierShopInfo entity = null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopInfo.Shop_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopInfo.Shop_SupplierID", "=", Supplier_ID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopInfo.Shop_ID", "Desc"));
        SupplierShopCssInfo cssinfo;
        IList<SupplierShopInfo> entitys = MyBLL.GetSupplierShops(Query);
        if (entitys != null)
        {
            entity = entitys[0];
        }
        return entity;
    }

    /// <summary>
    /// 店铺升级
    /// </summary>
    /// <param name="Shop_Type">等级</param>
    public void SupplierShopUpGradeBatch(int Shop_Type)
    {
        string shop_id = tools.CheckStr(Request.QueryString["shop_id"]);
        if (shop_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要操作的信息", false, "{back}");
            return;
        }

        if (tools.Left(shop_id, 1) == ",") { shop_id = shop_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopInfo.Shop_ID", "in", shop_id));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopInfo.Shop_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopInfo.Shop_ID", "DESC"));
        IList<SupplierShopInfo> entitys = MyBLL.GetSupplierShops(Query);
        if (entitys != null)
        {
            foreach (SupplierShopInfo entity in entitys)
            {
                entity.Shop_Type = Shop_Type;
                MyBLL.EditSupplierShop(entity);
            }
        }
        Public.Msg("positive", "操作成功", "操作成功", true, "Shop_List.aspx");
    }

    //店铺升级
    public void Supplier_Shop_UpGrade()
    {
        int Shop_Type = tools.CheckInt(Request["Shop_Type"]);
        int Supplier_ID = tools.CheckInt(Request["Supplier_ID"]);
        int Shop_ID = 0;
        bool upgrade_status = false;
        SupplierInfo entity = MySupplier.GetSupplierByID(Supplier_ID, Public.GetUserPrivilege());
        if (entity != null)
        {
            //if (entity.Supplier_Margin_ID > 0)
            //{
            //    SupplierMarginInfo margininfo = MyMargin.GetSupplierMarginByID(entity.Supplier_Margin_ID,Public.GetUserPrivilege());
            //    if (margininfo != null)
            //    {
            //        if (Shop_Type == 3)
            //        {
            //            if (entity.Supplier_Security_Account >= margininfo.Supplier_Margin_SecurityMoney1 && entity.Supplier_Account >= margininfo.Supplier_Margin_Account1)
            //            {
            //                upgrade_status = true;
            //            }
            //        }
            //        if (Shop_Type == 2)
            //        {
            //            if (entity.Supplier_Security_Account >= margininfo.Supplier_Margin_SecurityMoney2 && entity.Supplier_Account >= margininfo.Supplier_Margin_Account2)
            //            {
            //                upgrade_status = true;
            //            }
            //        }
            //        if (Shop_Type == 1)
            //        {
            //            if (entity.Supplier_Security_Account >= margininfo.Supplier_Margin_SecurityMoney3 && entity.Supplier_Account >= margininfo.Supplier_Margin_Account3)
            //            {
            //                upgrade_status = true;
            //            }
            //        }
            //    }
            //}

            SupplierShopInfo shopinfo = GetSupplierShopBySupplierID(entity.Supplier_ID);
            if (shopinfo != null)
            {
                upgrade_status = true;
                if (upgrade_status)
                {
                    Shop_ID = shopinfo.Shop_ID;
                    shopinfo.Shop_Type = Shop_Type;
                    MyBLL.EditSupplierShop(shopinfo);
                }
            }
        }
        if (upgrade_status)
        {
            Public.Msg("positive", "操作成功", "店铺升级成功！", true, "Shop_View.aspx?shop_id=" + Shop_ID);
        }
        else
        {
            Public.Msg("error", "错误信息", "店铺升级失败，请稍后重试！", false, "{back}");
        }
    }

    /// <summary>
    /// 展示店铺列表
    /// </summary>
    /// <returns>店铺列表</returns>
    public string GetSupplierShops()
    {

        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        int shop_type = tools.CheckInt(Request["shop_type"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopInfo.Shop_Site", "=", Public.GetCurrentSite()));
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "SupplierShopInfo.Shop_Name", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "SupplierShopInfo.Shop_Code", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "SupplierShopInfo.Shop_Domain", "like", keyword));
        }
        if (shop_type > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopInfo.Shop_Type", "=", shop_type.ToString()));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        SupplierShopCssInfo cssinfo;
        PageInfo pageinfo = MyBLL.GetPageInfo(Query);
        IList<SupplierShopInfo> entitys = MyBLL.GetSupplierShops(Query);
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierShopInfo entity in entitys)
            {

                jsonBuilder.Append("{\"id\":" + entity.Shop_ID + ",\"cell\":[");
                //各字段

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Shop_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                SupplierInfo supplierinfo = MySupplier.GetSupplierByID(entity.Shop_SupplierID, Public.GetUserPrivilege());
                if (supplierinfo != null)
                {
                    jsonBuilder.Append(Public.JsonStr(supplierinfo.Supplier_CompanyName));
                }

                else
                {
                    jsonBuilder.Append(Public.JsonStr("--"));
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Shop_Domain));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Shop_Code));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(Get_Shop_Type(entity.Shop_Type)));
                jsonBuilder.Append("\",");

                cssinfo = GetSupplierShopCssByID(entity.Shop_Css);
                if (cssinfo != null)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(cssinfo.Shop_Css_Title));
                    jsonBuilder.Append("\",");
                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("--");
                    jsonBuilder.Append("\",");
                }

                jsonBuilder.Append("\"");
                if (entity.Shop_Status == 0)
                {
                    jsonBuilder.Append("未启用");
                }
                else
                {
                    jsonBuilder.Append("使用中");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"查看\\\" align=\\\"absmiddle\\\"> <a href=\\\"Shop_View.aspx?Shop_ID=" + entity.Shop_ID + "\\\" title=\\\"查看\\\">查看</a>");
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

    /// <summary>
    /// 根据店铺ID获取店铺信息
    /// </summary>
    /// <param name="ID">店铺ID</param>
    /// <returns>店铺实体</returns>
    public SupplierShopInfo GetSupplierShopByID(int ID)
    {
        return MyBLL.GetSupplierShopByID(ID);
    }

    /// <summary>
    /// 店铺修改
    /// </summary>
    public virtual void EditSupplierShop()
    {

        int Shop_ID = tools.CheckInt(Request.Form["Shop_ID"]);
        string Shop_Name = tools.CheckStr(Request.Form["Shop_Name"]);
        string Shop_Domain = tools.CheckStr(Request.Form["Shop_Domain"]);
        string Shop_Code = tools.CheckStr(Request.Form["Shop_Code"]);
        int Shop_Recommend = tools.CheckInt(Request.Form["Shop_Recommend"]);
        int Shop_Status = tools.CheckInt(Request.Form["Shop_Status"]);
        SupplierShopInfo entity = GetSupplierShopByID(Shop_ID);
        if (entity != null)
        {
            entity.Shop_Name = Shop_Name;
            entity.Shop_Domain = Shop_Domain;
            entity.Shop_Code = Shop_Code;
            entity.Shop_Recommend = Shop_Recommend;
            entity.Shop_Status = Shop_Status;
            if (MyBLL.EditSupplierShop(entity))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "Shop_list.aspx");
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

    /// <summary>
    /// 展示店铺版面文章
    /// </summary>
    /// <returns>店铺版面文章列表</returns>
    public string GetShopPages()
    {
        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopPagesInfo.Shop_Pages_Site", "=", Public.GetCurrentSite()));
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "SupplierShopPagesInfo.Shop_Pages_Title", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "int", "SupplierShopPagesInfo.Shop_Pages_SupplierID", "in", "(select Supplier_ID from supplier where Supplier_CompanyName like '%" + keyword + "%')"));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "SupplierShopPagesInfo.Shop_Pages_Sign", "like", keyword));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyPages.GetPageInfo(Query);
        IList<SupplierShopPagesInfo> entitys = MyPages.GetSupplierShopPagess(Query);
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierShopPagesInfo entity in entitys)
            {

                jsonBuilder.Append("{\"id\":" + entity.Shop_Pages_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Shop_Pages_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Shop_Pages_Title));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                SupplierInfo supplierinfo = MySupplier.GetSupplierByID(entity.Shop_Pages_SupplierID, Public.GetUserPrivilege());
                if (supplierinfo != null)
                {
                    jsonBuilder.Append(Public.JsonStr(supplierinfo.Supplier_CompanyName));
                }

                else
                {
                    jsonBuilder.Append(Public.JsonStr("--"));
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Shop_Pages_Sign));
                jsonBuilder.Append("\",");



                jsonBuilder.Append("\"");
                if (entity.Shop_Pages_Ischeck == 0)
                {
                    jsonBuilder.Append("未审核");
                }
                else if (entity.Shop_Pages_Ischeck == 1)
                {
                    jsonBuilder.Append("已审核");
                }
                else
                {
                    jsonBuilder.Append("审核失败");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Shop_Pages_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"查看\\\" align=\\\"absmiddle\\\"> <a href=\\\"Shop_Pages_View.aspx?Shop_Pages_ID=" + entity.Shop_Pages_ID + "\\\" title=\\\"查看\\\">查看</a>");
                jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Shop_do.aspx?action=pagemove&Shop_Pages_ID=" + entity.Shop_Pages_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    /// <summary>
    /// 根据版面ID获取店铺版面信息
    /// </summary>
    /// <param name="ID">店铺版面ID</param>
    /// <returns>版面实体</returns>
    public SupplierShopPagesInfo GetSupplierShopPagesByID(int ID)
    {
        return MyPages.GetSupplierShopPagesByID(ID);
    }

    public virtual void DelSupplierShopPages()
    {
        int Shop_Pages_ID = tools.CheckInt(Request.QueryString["Shop_Pages_ID"]);
        if (MyPages.DelSupplierShopPages(Shop_Pages_ID) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Shop_Pages_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    /// <summary>
    /// 店铺版面审核
    /// </summary>
    /// <param name="Status">审核状态</param>
    public void SupplierShopPagesCheck(int Status)
    {
        string pages_id = tools.CheckStr(Request.QueryString["pages_id"]);
        if (pages_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要审核的信息", false, "{back}");
            return;
        }

        if (tools.Left(pages_id, 1) == ",") { pages_id = pages_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopPagesInfo.Shop_Pages_ID", "in", pages_id));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopPagesInfo.Shop_Pages_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopPagesInfo.Shop_Pages_ID", "DESC"));
        IList<SupplierShopPagesInfo> entitys = MyPages.GetSupplierShopPagess(Query);
        if (entitys != null)
        {
            foreach (SupplierShopPagesInfo entity in entitys)
            {
                entity.Shop_Pages_Ischeck = Status;
                MyPages.EditSupplierShopPages(entity);
            }
        }
        Public.Msg("positive", "操作成功", "操作成功", true, "Shop_Pages_List.aspx");
    }

    /// <summary>
    /// 店铺域名申请
    /// </summary>
    /// <returns>店铺域名申请列表</returns>
    public string GetShopDomains()
    {
        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopDomainInfo.Shop_Domain_Site", "=", Public.GetCurrentSite()));
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "SupplierShopDomainInfo.Shop_Domain_Name", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "int", "SupplierShopDomainInfo.Shop_Domain_SupplierID", "in", "(select Supplier_ID from supplier where Supplier_CompanyName like '%" + keyword + "%')"));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyDomain.GetPageInfo(Query);
        IList<SupplierShopDomainInfo> entitys = MyDomain.GetSupplierShopDomains(Query);
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierShopDomainInfo entity in entitys)
            {

                jsonBuilder.Append("{\"id\":" + entity.Shop_Domain_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Shop_Domain_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Shop_Domain_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Shop_Domain_Type == 1)
                {
                    jsonBuilder.Append(Public.JsonStr("顶级域名"));
                }
                else
                {
                    jsonBuilder.Append(Public.JsonStr("默认域名"));
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                SupplierInfo supplierinfo = MySupplier.GetSupplierByID(entity.Shop_Domain_SupplierID, Public.GetUserPrivilege());
                if (supplierinfo != null)
                {
                    jsonBuilder.Append(Public.JsonStr(supplierinfo.Supplier_CompanyName));
                }

                else
                {
                    jsonBuilder.Append(Public.JsonStr("--"));
                }
                jsonBuilder.Append("\",");




                jsonBuilder.Append("\"");
                if (entity.Shop_Domain_Status == 0)
                {
                    jsonBuilder.Append("未审核");
                }
                else if (entity.Shop_Domain_Status == 1)
                {
                    jsonBuilder.Append("已审核");
                }
                else
                {
                    jsonBuilder.Append("审核失败");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Shop_Domain_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("d43c156b-194d-4a29-a7b2-b55a199ded70"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Shop_do.aspx?action=doaminmove&domain_ID=" + entity.Shop_Domain_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    public void SupplierShopDomainsCheck(int Status)
    {
        string domain_id = tools.CheckStr(Request.QueryString["domain_id"]);
        if (domain_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要审核的信息", false, "{back}");
            return;
        }

        if (tools.Left(domain_id, 1) == ",") { domain_id = domain_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopDomainInfo.Shop_Domain_ID", "in", domain_id));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopDomainInfo.Shop_Domain_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopDomainInfo.Shop_Domain_ID", "DESC"));
        IList<SupplierShopDomainInfo> entitys = MyDomain.GetSupplierShopDomains(Query);
        if (entitys != null)
        {
            foreach (SupplierShopDomainInfo entity in entitys)
            {
                entity.Shop_Domain_Status = Status;
                MyDomain.EditSupplierShopDomain(entity);
            }
        }
        Public.Msg("positive", "操作成功", "操作成功", true, "Shop_domain_List.aspx");
    }

    public virtual void DelSupplierShopDomains()
    {
        int domain_id = tools.CheckInt(Request.QueryString["domain_id"]);
        if (MyDomain.DelSupplierShopDomain(domain_id) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Shop_domain_List.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    /// <summary>
    /// 展示店铺活动文章
    /// </summary>
    /// <returns>店铺活动文章列表</returns>
    public string GetShopArticles()
    {
        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopArticleInfo.Shop_Article_Site", "=", Public.GetCurrentSite()));
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "SupplierShopArticleInfo.Shop_Article_Title", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "int", "SupplierShopArticleInfo.Shop_Article_SupplierID", "in", "(select Supplier_ID from supplier where Supplier_CompanyName like '%" + keyword + "%')"));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyArticle.GetPageInfo(Query);
        IList<SupplierShopArticleInfo> entitys = MyArticle.GetSupplierShopArticles(Query);
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierShopArticleInfo entity in entitys)
            {

                jsonBuilder.Append("{\"id\":" + entity.Shop_Article_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Shop_Article_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Shop_Article_Title));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                SupplierInfo supplierinfo = MySupplier.GetSupplierByID(entity.Shop_Article_SupplierID, Public.GetUserPrivilege());
                if (supplierinfo != null)
                {
                    jsonBuilder.Append(Public.JsonStr(supplierinfo.Supplier_CompanyName));
                }

                else
                {
                    jsonBuilder.Append(Public.JsonStr("--"));
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Shop_Article_IsActive == 0)
                {
                    jsonBuilder.Append("未审核");
                }
                else if (entity.Shop_Article_IsActive == 1)
                {
                    jsonBuilder.Append("已审核");
                }
                else
                {
                    jsonBuilder.Append("审核失败");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Shop_Article_Addtime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"查看\\\" align=\\\"absmiddle\\\"> <a href=\\\"Shop_Article_View.aspx?Shop_Article_ID=" + entity.Shop_Article_ID + "\\\" title=\\\"查看\\\">查看</a>");

                jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Shop_do.aspx?action=articlemove&Shop_Article_ID=" + entity.Shop_Article_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    public virtual void DelSupplierShopArticle()
    {
        int Shop_Article_ID = tools.CheckInt(Request.QueryString["Shop_Article_ID"]);
        if (MyArticle.DelSupplierShopArticle(Shop_Article_ID) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Shop_Article_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    /// <summary>
    /// 根据活动ID获取店铺活动信息
    /// </summary>
    /// <param name="ID">店铺活动ID</param>
    /// <returns>活动实体</returns>
    public SupplierShopArticleInfo GetSupplierShopArticleByID(int ID)
    {
        return MyArticle.GetSupplierShopArticleByID(ID);
    }

    /// <summary>
    /// 店铺活动审核
    /// </summary>
    /// <param name="Status">审核状态</param>
    public void SupplierShopArticleCheck(int Status)
    {
        string article_id = tools.CheckStr(Request.QueryString["article_id"]);
        if (article_id == "")
        {
            Public.Msg("error", "错误信息", "请选择要删除的信息", false, "{back}");
            return;
        }

        if (tools.Left(article_id, 1) == ",") { article_id = article_id.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopArticleInfo.Shop_Article_ID", "in", article_id));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopArticleInfo.Shop_Article_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopArticleInfo.Shop_Article_ID", "DESC"));
        IList<SupplierShopArticleInfo> entitys = MyArticle.GetSupplierShopArticles(Query);
        if (entitys != null)
        {
            foreach (SupplierShopArticleInfo entity in entitys)
            {
                entity.Shop_Article_IsActive = Status;
                MyArticle.EditSupplierShopArticle(entity);
            }
        }
        Public.Msg("positive", "操作成功", "操作成功", true, "Shop_Article_List.aspx");
    }

    #endregion

    #region 店铺等级
    public virtual void AddSupplierShopGrade()
    {
        string Shop_Grade_Name = tools.CheckStr(Request.Form["Shop_Grade_Name"]);
        int Shop_Grade_ProductLimit = tools.CheckInt(Request.Form["Shop_Grade_ProductLimit"]);
        double Shop_Grade_DefaultCommission = tools.CheckFloat(Request.Form["Shop_Grade_DefaultCommission"]);
        int Shop_Grade_IsActive = tools.CheckInt(Request.Form["Shop_Grade_IsActive"]);
        string Shop_Grade_Site = Public.GetCurrentSite();
        if (Shop_Grade_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请将店铺等级名称填写完整！", false, "{back}");
        }

        SupplierShopGradeInfo entity = new SupplierShopGradeInfo();
        entity.Shop_Grade_Name = Shop_Grade_Name;
        entity.Shop_Grade_ProductLimit = Shop_Grade_ProductLimit;
        entity.Shop_Grade_DefaultCommission = Shop_Grade_DefaultCommission;
        entity.Shop_Grade_IsActive = Shop_Grade_IsActive;
        entity.Shop_Grade_Site = Shop_Grade_Site;

        if (MyShopGrade.AddSupplierShopGrade(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Shop_Grade_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditSupplierShopGrade()
    {

        int Shop_Grade_ID = tools.CheckInt(Request.Form["Shop_Grade_ID"]);
        string Shop_Grade_Name = tools.CheckStr(Request.Form["Shop_Grade_Name"]);
        int Shop_Grade_ProductLimit = tools.CheckInt(Request.Form["Shop_Grade_ProductLimit"]);
        double Shop_Grade_DefaultCommission = tools.CheckFloat(Request.Form["Shop_Grade_DefaultCommission"]);
        int Shop_Grade_IsActive = tools.CheckInt(Request.Form["Shop_Grade_IsActive"]);
        if (Shop_Grade_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请将店铺等级名称填写完整！", false, "{back}");
        }
        SupplierShopGradeInfo entity = GetSupplierShopGradeByID(Shop_Grade_ID);
        if (entity != null)
        {
            entity.Shop_Grade_Name = Shop_Grade_Name;
            entity.Shop_Grade_ProductLimit = Shop_Grade_ProductLimit;
            entity.Shop_Grade_DefaultCommission = Shop_Grade_DefaultCommission;
            entity.Shop_Grade_IsActive = Shop_Grade_IsActive;
            if (MyShopGrade.EditSupplierShopGrade(entity, Public.GetUserPrivilege()))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "Shop_Grade_list.aspx");
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

    public virtual void DelSupplierShopGrade()
    {
        int Shop_Grade_ID = tools.CheckInt(Request.QueryString["Shop_Grade_ID"]);
        if (MyShopGrade.DelSupplierShopGrade(Shop_Grade_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Shop_Grade_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public string GetSupplierShopGrades()
    {
        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopGradeInfo.Shop_Grade_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyShopGrade.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<SupplierShopGradeInfo> entitys = MyShopGrade.GetSupplierShopGrades(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (SupplierShopGradeInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.Shop_Grade_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Shop_Grade_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Shop_Grade_Name));
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                if (entity.Shop_Grade_IsActive == 1)
                {
                    jsonBuilder.Append("启用");
                }
                else
                {
                    jsonBuilder.Append("未启用");
                }
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");

                if (Public.CheckPrivilege("adde3836-fe74-4976-9297-61fe4b3db991"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\" align=\\\"absmiddle\\\"> <a href=\\\"Shop_Grade_edit.aspx?Shop_Grade_ID=" + entity.Shop_Grade_ID + "\\\" title=\\\"修改\\\">修改</a>");

                }
                if (Public.CheckPrivilege("3ba627a6-0a91-48d3-a4bc-9e2a84fc8dba"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('Shop_Grade_do.aspx?action=move&Shop_Grade_ID=" + entity.Shop_Grade_ID + "')\\\" title=\\\"删除\\\">删除</a>");

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

    public virtual SupplierShopGradeInfo GetSupplierShopGradeByID(int Grade_id)
    {
        return MyShopGrade.GetSupplierShopGradeByID(Grade_id, Public.GetUserPrivilege());
    }
    #endregion

    #region 店铺评论

    //根据编号获取评论信息
    public SupplierShopEvaluateInfo GetSupplierShopEvaluateByID(int review_id)
    {
        return MyEvaluate.GetSupplierShopEvaluateByID(review_id);
    }

    //获取产品评论列表
    public string GetSupplierShopEvaluates()
    {

        string keyword = tools.CheckStr(Request["keyword"]);
        int audit_status = tools.CheckInt(Request["audit_status"]);
        string productidstr, memberidstr, supplieridstr;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopEvaluateInfo.Shop_Evaluate_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", ">", "0"));
        if (audit_status == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));
        }
        else if (audit_status == 2)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "0"));
        }

        if (keyword.Length > 0)
        {
            memberidstr = member.GetMemberIDByKeyword(keyword);
            productidstr = ProApp.GetProductIDByKeyword(keyword);
            supplieridstr = supplier.GetSuppliersByKeyword(keyword);
            Query.ParamInfos.Add(new ParamInfo("AND(", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_MemberId", "in", memberidstr));
            Query.ParamInfos.Add(new ParamInfo("OR", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_SupplierID", "in", supplieridstr));
            Query.ParamInfos.Add(new ParamInfo("OR", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_ContractID", "in", "(select orders_id from Orders where orders_sn like '%" + keyword + "%')"));
            Query.ParamInfos.Add(new ParamInfo("OR)", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", "in", productidstr));
        }



        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyEvaluate.GetPageInfo(Query);
        IList<SupplierShopEvaluateInfo> entitys = MyEvaluate.GetSupplierShopEvaluates(Query);
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            OrdersInfo ordersInfo;
            SupplierInfo supplierinfo;
            MemberInfo memberinfo;
            ProductInfo productinfo;
            foreach (SupplierShopEvaluateInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.Shop_Evaluate_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Shop_Evaluate_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                ordersInfo = order.GetOrdersByID(entity.Shop_Evaluate_ContractID);
                if (ordersInfo != null)
                {
                    jsonBuilder.Append(ordersInfo.Orders_SN);
                }
                else
                {
                    jsonBuilder.Append("--");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Shop_Evaluate_SupplierID > 0)
                {
                    supplierinfo = supplier.GetSupplierByID(entity.Shop_Evaluate_SupplierID);
                    if (supplierinfo != null)
                    {
                        jsonBuilder.Append(supplierinfo.Supplier_CompanyName);
                    }
                    else
                    {
                        jsonBuilder.Append("--");
                    }
                }
                else
                {
                    jsonBuilder.Append("易耐产业金服");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                supplierinfo = supplier.GetSupplierByID(entity.Shop_Evaluate_MemberID);
                if (supplierinfo != null)
                {
                    jsonBuilder.Append(supplierinfo.Supplier_CompanyName);
                }
                else
                {
                    jsonBuilder.Append("--");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                productinfo = ProApp.GetProductByID(entity.Shop_Evaluate_Productid);
                if (productinfo != null)
                {
                    jsonBuilder.Append(productinfo.Product_Name);
                }
                else
                {
                    jsonBuilder.Append("--");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Shop_Evaluate_Service);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Shop_Evaluate_Delivery);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Shop_Evaluate_Product);
                jsonBuilder.Append("\",");



                jsonBuilder.Append("\"");
                if (entity.Shop_Evaluate_Ischeck == 0)
                {
                    jsonBuilder.Append("未审核");
                }
                else
                {
                    jsonBuilder.Append("已审核");
                }
                jsonBuilder.Append("\",");



                jsonBuilder.Append("\"");
                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" align=\\\"absmiddle\\\"> <a href=\\\"shop_evaluate_view.aspx?Shop_Evaluate_ID=" + entity.Shop_Evaluate_ID + "\\\" title=\\\"查看\\\">查看</a><img src=\\\"/images/icon_edit.gif\\\" align=\\\"absmiddle\\\"> <a href=\\\"shop_evaluate_edit.aspx?Shop_Evaluate_ID=" + entity.Shop_Evaluate_ID + "\\\" title=\\\"修改\\\">修改</a>");
                jsonBuilder.Append("\",");


                //jsonBuilder.Append("\"");
                //jsonBuilder.Append("");
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



    //获取商家评论列表
    public string GetShopSupplierEvaluates()
    {

        string keyword = tools.CheckStr(Request["keyword"]);
        int audit_status = tools.CheckInt(Request["audit_status"]);
        string productidstr, memberidstr, supplieridstr;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopEvaluateInfo.Shop_Evaluate_Site", "=", Public.GetCurrentSite()));
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", ">", "0"));
        if (audit_status == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));
        }
        else if (audit_status == 2)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "0"));
        }

        if (keyword.Length > 0)
        {
            memberidstr = member.GetMemberIDByKeyword(keyword);
            productidstr = ProApp.GetProductIDByKeyword(keyword);
            supplieridstr = supplier.GetSuppliersByKeyword(keyword);
            Query.ParamInfos.Add(new ParamInfo("AND(", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_MemberId", "in", memberidstr));
            Query.ParamInfos.Add(new ParamInfo("OR", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_SupplierID", "in", supplieridstr));
            Query.ParamInfos.Add(new ParamInfo("OR", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_ContractID", "in", "(select orders_id from Orders where orders_sn like '%" + keyword + "%')"));
            Query.ParamInfos.Add(new ParamInfo("OR)", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", "=", productidstr));
        }


        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", "=", "0"));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = MyEvaluate.GetPageInfo(Query);
        IList<SupplierShopEvaluateInfo> entitys = MyEvaluate.GetSupplierShopEvaluates(Query);
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            OrdersInfo ordersInfo;
            SupplierInfo supplierinfo;
            MemberInfo memberinfo;
            ProductInfo productinfo;
            foreach (SupplierShopEvaluateInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.Shop_Evaluate_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Shop_Evaluate_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                ordersInfo = order.GetOrdersByID(entity.Shop_Evaluate_ContractID);
                if (ordersInfo != null)
                {
                    jsonBuilder.Append(ordersInfo.Orders_SN);
                }
                else
                {
                    jsonBuilder.Append("--");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Shop_Evaluate_SupplierID > 0)
                {
                    supplierinfo = supplier.GetSupplierByID(entity.Shop_Evaluate_SupplierID);
                    if (supplierinfo != null)
                    {
                        jsonBuilder.Append(supplierinfo.Supplier_CompanyName);
                    }
                    else
                    {
                        jsonBuilder.Append("--");
                    }
                }
                else
                {
                    jsonBuilder.Append("易耐产业金服");
                }
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                supplierinfo = supplier.GetSupplierByID(entity.Shop_Evaluate_MemberID);
                if (supplierinfo != null)
                {
                    jsonBuilder.Append(supplierinfo.Supplier_CompanyName);
                }
                else
                {
                    jsonBuilder.Append("--");
                }
                jsonBuilder.Append("\",");





                //jsonBuilder.Append("\"");
                //productinfo = ProApp.GetProductByID(entity.Shop_Evaluate_Productid);
                //if (productinfo != null)
                //{
                //    jsonBuilder.Append(productinfo.Product_Name);
                //}
                //else
                //{
                //    jsonBuilder.Append("--");
                //}
                //jsonBuilder.Append("\",");






                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Shop_Evaluate_Service);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Shop_Evaluate_Delivery);
                jsonBuilder.Append("\",");

                //jsonBuilder.Append("\"");
                //jsonBuilder.Append(entity.Shop_Evaluate_Product);
                //jsonBuilder.Append("\",");






                jsonBuilder.Append("\"");
                if (entity.Shop_Evaluate_Ischeck == 0)
                {
                    jsonBuilder.Append("未审核");
                }
                else
                {
                    jsonBuilder.Append("已审核");
                }
                jsonBuilder.Append("\",");



                jsonBuilder.Append("\"");

                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" align=\\\"absmiddle\\\"> <a href=\\\"shop_evaluate_view.aspx?Shop_Evaluate_ID=" + entity.Shop_Evaluate_ID + "\\\" title=\\\"查看\\\">查看</a><img src=\\\"/images/icon_edit.gif\\\" align=\\\"absmiddle\\\"> <a href=\\\"shop_supplierevaluate_edit.aspx?Shop_Evaluate_ID=" + entity.Shop_Evaluate_ID + "\\\" title=\\\"修改\\\">修改</a>");

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

    public void EditSupplierShopEvaluate(string action)
    {
        string Shop_Evaluate_ID = tools.CheckStr(Request["Shop_Evaluate_ID"]);
        double Review_Average = 0;
        int reviews_count = 0;
        int Review_count = 0;
        int Product_Review_Star = 0;
        int Star_count = 0;
        int Shop_Evaluate_IsGift = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopEvaluateInfo.Shop_Evaluate_Site", "=", Public.GetCurrentSite()));
        if (action == "shop_evaluate_audit")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", "=", "0"));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", ">", "0"));
        }

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_ID", "in", Shop_Evaluate_ID));
        IList<SupplierShopEvaluateInfo> entitys = MyEvaluate.GetSupplierShopEvaluates(Query);
        if (entitys != null)
        {
            foreach (SupplierShopEvaluateInfo entity in entitys)
            {
                Product_Review_Star = entity.Shop_Evaluate_Product;
                Shop_Evaluate_IsGift = entity.Shop_Evaluate_IsGift;
                if (action == "audit" || action == "recommend" || action == "shop_evaluate_audit" || action == "shop_evaluate_deny")
                {
                    entity.Shop_Evaluate_Ischeck = 1;
                    entity.Shop_Evaluate_IsGift = 1;
                }
                else
                {
                    entity.Shop_Evaluate_Ischeck = 0;
                }

                if (MyEvaluate.EditSupplierShopEvaluate(entity))
                {
                    ProductInfo productinfo = ProApp.GetProductByID(entity.Shop_Evaluate_Productid);
                    if (productinfo != null)
                    {
                        Review_Average = productinfo.Product_Review_Average;
                        reviews_count = productinfo.Product_Review_Count;
                    }
                    switch (action)
                    {
                        case "audit":
                            Review_count = GetProductEvaluateCount(entity.Shop_Evaluate_Productid);
                            if (Review_count > 0)
                            {
                                Review_Average = ((Review_Average * (Review_count - 1)) + Product_Review_Star) / Review_count;
                                Myreview.UpdateProductReviewINfo(entity.Shop_Evaluate_Productid, Review_Average, reviews_count, Review_count);
                            }
                            ProductReviewConfigInfo config = Myconfig.GetProductReviewConfig(Public.GetUserPrivilege());
                            if (config != null)
                            {
                                if (config.Product_Review_giftcoin > 0 && entity.Shop_Evaluate_MemberID > 0 && Shop_Evaluate_IsGift == 0)
                                {
                                    member.Member_Coin_AddConsume(config.Product_Review_giftcoin, "发表评论赠送积分", entity.Shop_Evaluate_MemberID, true);
                                }
                            }
                            break;
                        case "deny":
                            Review_count = GetProductEvaluateCount(entity.Shop_Evaluate_Productid);
                            Star_count = GetProductEvaluateStarCount(entity.Shop_Evaluate_Productid);
                            if (Review_count > 0)
                            {
                                Review_Average = Star_count / Review_count;
                                Myreview.UpdateProductReviewINfo(entity.Shop_Evaluate_Productid, Review_Average, reviews_count, Review_count);
                            }
                            else
                            {
                                Myreview.UpdateProductReviewINfo(entity.Shop_Evaluate_Productid, 0, reviews_count, 0);
                            }

                            break;
                        case "recommend":
                            ProductReviewConfigInfo configreview = Myconfig.GetProductReviewConfig(Public.GetUserPrivilege());
                            if (configreview != null)
                            {
                                if (configreview.Product_Review_Recommendcoin > 0 && entity.Shop_Evaluate_MemberID > 0)                 
                                {
                                    member.Member_Coin_AddConsume(configreview.Product_Review_Recommendcoin, "评论推荐赠送积分", entity.Shop_Evaluate_MemberID, true);
                                }
                            }
                            break;

                    }

                }
            }
        }
        Response.Redirect("shop_evaluate_list.aspx");
    }



    public void Edit_SupplierShopEvaluate()
    {
        int Shop_Evaluate_ID = tools.CheckInt(Request["Shop_Evaluate_ID"]);
        int Shop_Evaluate_Product = tools.CheckInt(Request["Shop_Evaluate_Product"]);
        int Shop_Evaluate_Service = tools.CheckInt(Request["Shop_Evaluate_Service"]);
        int Shop_Evaluate_Delivery = tools.CheckInt(Request["Shop_Evaluate_Delivery"]);
        string Shop_Evaluate_Note = tools.CheckStr(Request["Shop_Evaluate_Note"]);


        SupplierShopEvaluateInfo entity = MyEvaluate.GetSupplierShopEvaluateByID(Shop_Evaluate_ID);
        if (entity != null)
        {
            entity.Shop_Evaluate_Product = Shop_Evaluate_Product;
            entity.Shop_Evaluate_Service = Shop_Evaluate_Service;
            entity.Shop_Evaluate_Delivery = Shop_Evaluate_Delivery;
            entity.Shop_Evaluate_Note = Shop_Evaluate_Note;
            entity.Shop_Evaluate_Addtime = entity.Shop_Evaluate_Addtime;
            if (MyEvaluate.EditSupplierShopEvaluate(entity))
            {
                if (Shop_Evaluate_Product>0)
                {
                    Public.Msg("positive", "操作成功", "操作成功", true, "/shop/shop_evaluate_edit.aspx?Shop_Evaluate_ID=" + Shop_Evaluate_ID + "");
                }
                else
                {
                    Public.Msg("positive", "操作成功", "操作成功", true, "/shop/shop_supplierevaluate_edit.aspx?Shop_Evaluate_ID=" + Shop_Evaluate_ID + "");
                }
               
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

    //获取产品评论数量
    public int GetProductEvaluateCount(int Product_ID)
    {
        int Evaluate_Count = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopEvaluateInfo.Shop_Evaluate_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));
        if (Product_ID > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", "=", Product_ID.ToString()));
        }
        IList<SupplierShopEvaluateInfo> entitys = MyEvaluate.GetSupplierShopEvaluates(Query);
        if (entitys != null)
        {
            Evaluate_Count = entitys.Count;
        }
        return Evaluate_Count;

    }

    //获取产品评论评星总和
    public int GetProductEvaluateStarCount(int Product_ID)
    {
        int Evaluate_Star_Count = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopEvaluateInfo.Shop_Evaluate_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));
        if (Product_ID > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Productid", "=", Product_ID.ToString()));
        }
        IList<SupplierShopEvaluateInfo> entitys = MyEvaluate.GetSupplierShopEvaluates(Query);
        if (entitys != null)
        {
            foreach (SupplierShopEvaluateInfo entity in entitys)
            {
                Evaluate_Star_Count += entity.Shop_Evaluate_Product;
            }
        }
        return Evaluate_Star_Count;

    }
    #endregion


}