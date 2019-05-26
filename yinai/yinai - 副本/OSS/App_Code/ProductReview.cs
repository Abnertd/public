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
using Glaer.Trade.B2C.BLL.Product;
using Glaer.Trade.B2C.BLL.MEM;

/// <summary>
///ProductReview 的摘要说明
/// </summary>
public class ProductReview
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IProductReviewConfig Myconfig;
    private IProductReview Myreview;
    private IMember MyMem;
    private IProduct Myproduct;
    Member member = new Member();
    Product ProApp = new Product();
    

    public ProductReview()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        Myconfig =ProductReviewConfigFactory.CreateProductReviewConfig();
        Myreview = ProductReviewFactory.CreateProductReview();
        MyMem = MemberFactory.CreateMember();
        Myproduct = ProductFactory.CreateProduct();

        
        
    }

    //编辑评论设置信息
    public void EditProductReviewConfig()
    {

        int Product_Review_Config_ID = tools.CheckInt(Request.Form["Product_Review_Config_ID"]);
        int Product_Review_Config_ProductCount = tools.CheckInt(Request.Form["Product_Review_Config_ProductCount"]);
        int Product_Review_Config_ListCount = tools.CheckInt(Request.Form["Product_Review_Config_ListCount"]);
        int Product_Review_Config_Power = tools.CheckInt(Request.Form["Product_Review_Config_Power"]);
        int Product_Review_giftcoin = tools.CheckInt(Request.Form["Product_Review_giftcoin"]);
        int Product_Review_Recommendcoin = tools.CheckInt(Request.Form["Product_Review_Recommendcoin"]);
        string Product_Review_Config_NoRecordTip = tools.CheckStr(Request.Form["Product_Review_Config_NoRecordTip"]);
        int Product_Review_Config_VerifyCode_IsOpen = tools.CheckInt(Request.Form["Product_Review_Config_VerifyCode_IsOpen"]);
        int Product_Review_Config_ManagerReply_Show = tools.CheckInt(Request.Form["Product_Review_Config_ManagerReply_Show"]);
        string Product_Review_Config_Show_SuccessTip = tools.CheckStr(Request.Form["Product_Review_Config_Show_SuccessTip"]);
        int Product_Review_Config_IsActive = tools.CheckInt(Request.Form["Product_Review_Config_IsActive"]);
        string Product_Review_Config_Site = tools.CheckStr(Request.Form["Product_Review_Config_Site"]);

        ProductReviewConfigInfo entity = new ProductReviewConfigInfo();
        entity.Product_Review_Config_ID = Product_Review_Config_ID;
        entity.Product_Review_Config_ProductCount = Product_Review_Config_ProductCount;
        entity.Product_Review_Config_ListCount = Product_Review_Config_ListCount;
        entity.Product_Review_Config_Power = Product_Review_Config_Power;
        entity.Product_Review_giftcoin = Product_Review_giftcoin;
        entity.Product_Review_Recommendcoin = Product_Review_Recommendcoin;
        entity.Product_Review_Config_NoRecordTip = Product_Review_Config_NoRecordTip;
        entity.Product_Review_Config_VerifyCode_IsOpen = Product_Review_Config_VerifyCode_IsOpen;
        entity.Product_Review_Config_ManagerReply_Show = Product_Review_Config_ManagerReply_Show;
        entity.Product_Review_Config_Show_SuccessTip = Product_Review_Config_Show_SuccessTip;
        entity.Product_Review_Config_IsActive = Product_Review_Config_IsActive;
        entity.Product_Review_Config_Site = Product_Review_Config_Site;


        if (Myconfig.EditProductReviewConfig(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "/shop/shop_evaluate_set.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //获取评论设置信息
    public ProductReviewConfigInfo GetProductReviewConfig()
    {
        return Myconfig.GetProductReviewConfig(Public.GetUserPrivilege());
    }

    //根据编号获取评论信息
    public ProductReviewInfo GetProductReviewByID(int review_id)
    {
        return Myreview.GetProductReviewByID(review_id, Public.GetUserPrivilege());
    }
    
    //获取产品评论列表
    public string GetProductReviews() {

        string keyword = tools.CheckStr(Request["keyword"]);
        int recommend_status = tools.CheckInt(Request["recommend_status"]);
        int audit_status = tools.CheckInt(Request["audit_status"]);
        int view_status = tools.CheckInt(Request["view_status"]);
        string productidstr, memberidstr;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductReviewInfo.Product_Review_Site", "=", Public.GetCurrentSite()));
        if (recommend_status == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductReviewInfo.Product_Review_IsRecommend", "=", "1"));
        }
        else if (recommend_status == 2)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductReviewInfo.Product_Review_IsRecommend", "=", "0"));
        }
        if (audit_status == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductReviewInfo.Product_Review_IsShow", "=", "1"));
        }
        else if (audit_status == 2)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductReviewInfo.Product_Review_IsShow", "=", "0"));
        }
        if (view_status == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductReviewInfo.Product_Review_IsView", "=", "1"));
        }
        else if (view_status == 2)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductReviewInfo.Product_Review_IsView", "=", "0"));
        }
        if (keyword.Length > 0)
        {
            if (keyword == "游客")
            {
                memberidstr = member.GetMemberIDByKeyword("");
            }
            else
            {
                memberidstr = member.GetMemberIDByKeyword(keyword);
            }
            productidstr = ProApp.GetProductIDByKeyword(keyword);
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductReviewInfo.Product_Review_ID", ">", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND(", "int", "ProductReviewInfo.Product_Review_MemberID", "in", memberidstr));
            Query.ParamInfos.Add(new ParamInfo("OR)", "int", "ProductReviewInfo.Product_Review_ProductID", "in", productidstr));
        }


        
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = Myreview.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<ProductReviewInfo> ProductReviews = Myreview.GetProductReviews(Query, Public.GetUserPrivilege());
        if (ProductReviews != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ProductReviewInfo entity in ProductReviews)
            {
                jsonBuilder.Append("{\"ProductReviewInfo.Product_Review_ID\":" + entity.Product_Review_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Product_Review_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Product_Review_MemberID == 0)
                {
                    jsonBuilder.Append("游客");
                }
                else
                {
                    MemberInfo member = MyMem.GetMemberByID(entity.Product_Review_MemberID, Public.GetUserPrivilege());
                    if (member != null)
                    {
                        jsonBuilder.Append(Public.JsonStr(member.Member_NickName));
                    }
                    else
                    {
                        jsonBuilder.Append("未知");
                    }
                }
                    
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Product_Review_Star);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Product_Review_ProductID > 0)
                {
                    ProductInfo productinfo = Myproduct.GetProductByID(entity.Product_Review_ProductID, Public.GetUserPrivilege());
                    if (productinfo != null)
                    {
                        jsonBuilder.Append(productinfo.Product_Name);
                    }
                    else
                    {
                        jsonBuilder.Append("未知");
                    }
                }
                else
                {
                    jsonBuilder.Append("未知");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Product_Review_IsRecommend == 0)
                {
                    jsonBuilder.Append("<a href=\\\"?action=recommend&review_id="+entity.Product_Review_ID+"\\\">未推荐</a>");
                }
                else
                {
                    jsonBuilder.Append("<a href=\\\"?action=recommendcancel&review_id=" + entity.Product_Review_ID + "\\\">已推荐</a>");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Product_Review_IsShow == 0)
                {
                    jsonBuilder.Append("<a href=\\\"?action=show&review_id=" + entity.Product_Review_ID + "\\\">未审核</a>");
                }
                else
                {
                    jsonBuilder.Append("<a href=\\\"?action=showcancel&review_id=" + entity.Product_Review_ID + "\\\">已审核</a>");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Product_Review_IsView == 0)
                {
                    jsonBuilder.Append("未查看");
                }
                else
                {
                    jsonBuilder.Append("已查看");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" align=\\\"absmiddle\\\"> <a href=\\\"product_review_view.aspx?review_id=" + entity.Product_Review_ID + "\\\" title=\\\"查看\\\">查看</a> <img src=\\\"/images/icon_del.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('product_review_do.aspx?action=move&review_id=" + entity.Product_Review_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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

    public void DelProductReview()
    {
        int review_id = tools.CheckInt(Request.QueryString["review_id"]);
        if (review_id > 0)
        {
            ProductReviewInfo entity = Myreview.GetProductReviewByID(review_id, Public.GetUserPrivilege());
            if (entity != null)
            {
                if (entity.Product_Review_IsShow == 0)
                {
                    if (Myreview.DelProductReview(review_id, Public.GetUserPrivilege()) > 0)
                    {
                        Public.Msg("positive", "操作成功", "操作成功", true, "Product_Review_list.aspx");
                    }
                    else
                    {
                        Public.Msg("error", "错误信息", "删除失败", true, "Product_Review_list.aspx");
                    }
                }
                else
                {
                    Public.Msg("error", "错误信息", "请先取消审核该评论！", true, "Product_Review_list.aspx");
                }
            }
            else
            {
                Public.Msg("error", "错误信息", "评论信息不存在", true, "Product_Review_list.aspx");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "请选择要删除的评论信息", true, "Product_Review_list.aspx");
        }
    }

    public void EditProductReview(string action)
    {
        int review_id = tools.CheckInt(Request.QueryString["review_id"]);
        int Review_count = 0;
        int Star_count = 0;
        int reviews_count = 0;
        double Review_Average = 0;
        if (review_id > 0)
        {
            ProductReviewInfo review = Myreview.GetProductReviewByID(review_id, Public.GetUserPrivilege());
            if (review != null)
            {
                int Product_Review_ID = review.Product_Review_ID;
                int Product_Review_MemberID = review.Product_Review_MemberID;
                int Product_Review_ProductID = review.Product_Review_ProductID;
                DateTime Product_Review_Addtime = review.Product_Review_Addtime;
                string Product_Review_Content = review.Product_Review_Content;
                int Product_Review_IsBuy = review.Product_Review_IsBuy;
                int Product_Review_IsRecommend = review.Product_Review_IsRecommend;
                int Product_Review_IsShow = review.Product_Review_IsShow;
                int Product_Review_Star = review.Product_Review_Star;
                string Product_Review_Subject = review.Product_Review_Subject;
                int Product_Review_Useful = review.Product_Review_Useful;
                int Product_Review_Useless = review.Product_Review_Useless;
                int Product_Review_IsGift=review.Product_Review_IsGift;
                int Product_Review_IsView = review.Product_Review_IsView;

                switch (action)
                {
                    case "recommend":
                        Product_Review_IsRecommend = 1;
                        break;
                    case "recommendcancel":
                        Product_Review_IsRecommend = 0;
                        break;
                    case "show":
                        Product_Review_IsShow = 1;

                        break;
                    case "showcancel":
                        Product_Review_IsShow = 0;
                        break;
                    case "reviewview":
                        Product_Review_IsView=1;
                        break;
                }

                ProductReviewInfo entity = new ProductReviewInfo();
                entity.Product_Review_ID = Product_Review_ID;
                entity.Product_Review_ProductID = Product_Review_ProductID;
                entity.Product_Review_MemberID = Product_Review_MemberID;
                entity.Product_Review_Star = Product_Review_Star;
                entity.Product_Review_Subject = Product_Review_Subject;
                entity.Product_Review_Content = Product_Review_Content;
                entity.Product_Review_Useful = Product_Review_Useful;
                entity.Product_Review_Useless = Product_Review_Useless;
                entity.Product_Review_Addtime = Product_Review_Addtime;
                entity.Product_Review_IsShow = Product_Review_IsShow;
                entity.Product_Review_IsBuy = Product_Review_IsBuy;
                entity.Product_Review_IsRecommend = Product_Review_IsRecommend;
                if (action == "show")
                {
                    entity.Product_Review_IsGift = 1;
                }
                else
                {
                    entity.Product_Review_IsGift = Product_Review_IsGift;
                }
                entity.Product_Review_Site = Public.GetCurrentSite();

                if (Myreview.EditProductReview(entity, Public.GetUserPrivilege()))
                {
                    ProductInfo productinfo = Myproduct.GetProductByID(Product_Review_ProductID, Public.GetUserPrivilege());
                    if (productinfo != null)
                    {
                        Review_Average = productinfo.Product_Review_Average;
                        reviews_count = productinfo.Product_Review_Count;
                    }
                    switch (action)
                    {
                        case "show":
                            Review_count = Myreview.GetProductReviewValidCount(Product_Review_ProductID);
                            if (Review_count > 0)
                            {
                                Review_Average = ((Review_Average * (Review_count - 1)) + Product_Review_Star) / Review_count;
                                Myreview.UpdateProductReviewINfo(Product_Review_ProductID, Review_Average, reviews_count, Review_count);
                            }
                            ProductReviewConfigInfo config = GetProductReviewConfig();
                            if (config != null)
                            {
                                if (config.Product_Review_giftcoin > 0 && Product_Review_MemberID > 0 && Product_Review_IsGift == 0)
                                {
                                    member.Member_Coin_AddConsume(config.Product_Review_giftcoin, "发表评论赠送积分", Product_Review_MemberID, true);
                                }
                            }
                            break;
                        case "showcancel":
                            Review_count = Myreview.GetProductReviewValidCount(Product_Review_ProductID);
                            Star_count = Myreview.GetProductStarCount(Product_Review_ProductID);
                            if (Review_count > 0)
                            {
                                Review_Average = Star_count / Review_count;
                                Myreview.UpdateProductReviewINfo(Product_Review_ProductID, Review_Average, reviews_count, Review_count);
                            }
                            else
                            {
                                Myreview.UpdateProductReviewINfo(Product_Review_ProductID, 0, reviews_count, 0);
                            }
                            
                            break;
                        case "recommend":
                            ProductReviewConfigInfo configreview = GetProductReviewConfig();
                            if (configreview != null)
                            {
                                if (configreview.Product_Review_Recommendcoin > 0 && Product_Review_MemberID > 0)
                                {
                                    member.Member_Coin_AddConsume(configreview.Product_Review_Recommendcoin, "评论推荐赠送积分", Product_Review_MemberID, true);
                                }
                            }
                            break;

                    }
                    Response.Redirect("Product_Review_list.aspx");
                }
                else
                {
                    Response.Redirect("Product_Review_list.aspx");
                }
            }
        }
    }

    

}
