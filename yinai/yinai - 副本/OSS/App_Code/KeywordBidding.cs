using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Data.SqlClient;
using Glaer.Trade.Util.SQLHelper;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.MEM;

/// <summary>
///KeywordBidding 的摘要说明
/// </summary>
public class KeywordBidding
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    ISQLHelper DBHelper;
    private ITools tools;
    private IKeywordBidding MyBLL;
    private Supplier supplier;
    private ISupplierMessage MyMessage;
    private Product MyProduct = new Product();
    private Supplier mysupplier = new Supplier();
    public KeywordBidding()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;
        DBHelper = SQLHelperFactory.CreateSQLHelper();
        tools = ToolsFactory.CreateTools();
        MyMessage = SupplierMessageFactory.CreateSupplierMessage();
        MyBLL = KeywordBiddingFactory.CreateKeywordBidding();
    }

    //获取指定竞价关键词
    public virtual string GetKeywordBiddingKeywordByID(int Keyword_ID)
    {
        string Keyword_Name = "";
        KeywordBiddingKeywordInfo entity = MyBLL.GetKeywordBiddingKeywordByID(Keyword_ID,Public.GetUserPrivilege());
        if (entity != null)
        {
            Keyword_Name = entity.Keyword_Name;
        }
        return Keyword_Name;
    }

    //获取指定竞价关键词
    public virtual KeywordBiddingKeywordInfo GetKeywordBiddingKeywordID(int Keyword_ID)
    {
        return MyBLL.GetKeywordBiddingKeywordByID(Keyword_ID, Public.GetUserPrivilege());
    }

    public string KeywordBidding_List()
    {
        StringBuilder jsonBuilder = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "KeywordBiddingInfo.KeywordBidding_Site", "=", Public.GetCurrentSite()));
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "KeywordBiddingInfo.KeywordBidding_KeywordID", "in", "select distinct Keyword_ID from KeywordBidding_Keyword where Keyword_Name like '%" + keyword + "%'"));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "KeywordBiddingInfo.KeywordBidding_Audit", "=", tools.NullInt(Request["Audit"]).ToString()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<KeywordBiddingInfo> entitys = MyBLL.GetKeywordBiddings(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            string Product_Name="";
            string KeywordBidding_Name="";
            ProductInfo productinfo=null;
            SupplierInfo SupplierEntity = null;
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach(KeywordBiddingInfo entity in entitys)
            {
                productinfo=MyProduct.GetProductByID(entity.KeywordBidding_ProductID);
                if(productinfo!=null)
                {
                    Product_Name=productinfo.Product_Name;
                }
                else
                {
                    Product_Name="";
                }
                KeywordBidding_Name=GetKeywordBiddingKeywordByID(entity.KeywordBidding_KeywordID);

                jsonBuilder.Append("{\"id\":" + entity.KeywordBidding_ID + ",\"cell\":[");
                //各字段

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.KeywordBidding_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(Product_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                SupplierEntity = mysupplier.GetSupplierByID(entity.KeywordBidding_SupplierID);
                if (SupplierEntity != null)
                {
                    jsonBuilder.Append(SupplierEntity.Supplier_CompanyName);
                }
                else
                {
                jsonBuilder.Append("--");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(KeywordBidding_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.DisplayCurrency(entity.KeywordBidding_Price));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.KeywordBidding_ShowTimes);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.KeywordBidding_Hits);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.KeywordBidding_StartDate.ToShortDateString() + " - " + entity.KeywordBidding_EndDate.ToShortDateString());
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.KeywordBidding_Audit == 1)
                {
                    jsonBuilder.Append("已审核");
                }
                else if (entity.KeywordBidding_Audit == 2)
                {
                    jsonBuilder.Append("审核不通过");
                }
                else
                {
                    jsonBuilder.Append("未审核");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");

            }

            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            
        }
        return jsonBuilder.ToString();

    }

    /// <summary>
    /// 审核
    /// </summary>
    public void AuditKeywordBidding(int Status)
    {
        string KeywordBidding_ID = tools.CheckStr(Request["KeywordBidding_ID"]);
        if (KeywordBidding_ID == "")
        {
            Public.Msg("error", "错误信息", "请选择要操作的信息", false, "{back}");
            return;
        }

        if (tools.Left(KeywordBidding_ID, 1) == ",") { KeywordBidding_ID = KeywordBidding_ID.Remove(0, 1); }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        string keyword = tools.CheckStr(Request["keyword"]);

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "KeywordBiddingInfo.KeywordBidding_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "KeywordBiddingInfo.KeywordBidding_ID", "in", KeywordBidding_ID));

        IList<KeywordBiddingInfo> entitys = MyBLL.GetKeywordBiddings(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (KeywordBiddingInfo entity in entitys)
            {
                entity.KeywordBidding_Audit = Status;
                MyBLL.EditKeywordBidding(entity);
            }
        }

        Response.Redirect("/keywordbidding/keywordbidding_list.aspx");
    }

    /// <summary>
    /// 审核
    /// </summary>
    public void DelKeywordBidding()
    {
        string KeywordBidding_ID = tools.CheckStr(Request["KeywordBidding_ID"]);
        if (KeywordBidding_ID == "")
        {
            Public.Msg("error", "错误信息", "请选择要操作的信息", false, "{back}");
            return;
        }

        if (tools.Left(KeywordBidding_ID, 1) == ",") { KeywordBidding_ID = KeywordBidding_ID.Remove(0, 1); }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        string keyword = tools.CheckStr(Request["keyword"]);

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "KeywordBiddingInfo.KeywordBidding_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "KeywordBiddingInfo.KeywordBidding_ID", "in", KeywordBidding_ID));
        string KeywordBidding_Name="";
        IList<KeywordBiddingInfo> entitys = MyBLL.GetKeywordBiddings(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (KeywordBiddingInfo entity in entitys)
            {
                KeywordBidding_Name=GetKeywordBiddingKeywordByID(entity.KeywordBidding_KeywordID);
                MyBLL.DelKeywordBidding(0,entity.KeywordBidding_ID,Public.GetUserPrivilege());
                AddSupplierMessage(entity.KeywordBidding_SupplierID, "您的关键词竞价申请已被删除", "<p>您申请的出价为" + Public.DisplayCurrency(entity.KeywordBidding_Price) + "的关键词为“" + KeywordBidding_Name + "”的竞价申请信息已被删除！</p>");
            }
        }

        Response.Redirect("/keywordbidding/keywordbidding_list.aspx");
    }

    public int GetKeywordBiddingAmount(int Keyword_ID)
    {
        int Amount = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        string keyword = tools.CheckStr(Request["keyword"]);

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "KeywordBiddingInfo.KeywordBidding_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "KeywordBiddingInfo.KeywordBidding_KeywordID", "=", Keyword_ID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("KeywordBiddingInfo.KeywordBidding_ID", "Desc"));

        IList<KeywordBiddingInfo> entitys = MyBLL.GetKeywordBiddings(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            Amount = entitys.Count;

        }
        return Amount;

    }

    public string KeywordBidding_Keyword_List()
    {
        StringBuilder jsonBuilder = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        string keyword = tools.CheckStr(Request["keyword"]);

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "KeywordBiddingKeywordInfo.Keyword_ID", ">", "0"));
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "KeywordBiddingKeywordInfo.Keyword_Name", "like",  keyword));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetKeywordPageInfo(Query, Public.GetUserPrivilege());

        IList<KeywordBiddingKeywordInfo> entitys = MyBLL.GetKeywordBiddingKeywords(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            string Product_Name = "";
            string KeywordBidding_Name = "";
            ProductInfo productinfo = null;
            SupplierInfo SupplierEntity = null;
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (KeywordBiddingKeywordInfo entity in entitys)
            {
                

                jsonBuilder.Append("{\"id\":" + entity.Keyword_ID + ",\"cell\":[");
                //各字段

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Keyword_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Keyword_Name));
                jsonBuilder.Append("\",");

                
                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.DisplayCurrency(entity.Keyword_MinPrice));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(GetKeywordBiddingAmount(entity.Keyword_ID));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("0f39c533-9740-427f-ae56-649518a414c3"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"keyword_edit.aspx?Keyword_ID=" + entity.Keyword_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");

            }

            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");

        }
        return jsonBuilder.ToString();

    }

    public void EditKeywordBidding_Keyword()
    {
        int Keyword_ID = tools.CheckInt(Request.Form["Keyword_ID"]);
        double Keyword_MinPrice = tools.CheckFloat(Request.Form["Keyword_MinPrice"]);

        if (Keyword_MinPrice == 0) { Public.Msg("error", "错误信息", "请填写起步价", false, "{back}"); return; }

        KeywordBiddingKeywordInfo entity = GetKeywordBiddingKeywordID(Keyword_ID) ;
        if (entity != null)
        {
            entity.Keyword_MinPrice = Keyword_MinPrice;

            if (MyBLL.EditKeywordBiddingKeyword(entity, Public.GetUserPrivilege()))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "keyword_list.aspx");
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
    /// 删除
    /// </summary>
    public void DelKeywordBidding_Keyword()
    {
        string Keyword_ID = tools.CheckStr(Request["Keyword_ID"]);
        if (Keyword_ID == "")
        {
            Public.Msg("error", "错误信息", "请选择要操作的信息", false, "{back}");
            return;
        }

        if (tools.Left(Keyword_ID, 1) == ",") { Keyword_ID = Keyword_ID.Remove(0, 1); }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "KeywordBiddingKeywordInfo.Keyword_ID", "in", Keyword_ID));

        IList<KeywordBiddingKeywordInfo> entitys = MyBLL.GetKeywordBiddingKeywords(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (KeywordBiddingKeywordInfo entity in entitys)
            {

                MyBLL.DelKeywordBiddingKeyword(entity.Keyword_ID, Public.GetUserPrivilege());
            }
        }

        Response.Redirect("/keywordbidding/keyword_list.aspx");
    }

    public virtual void AddSupplierMessage(int SupplierID,string Message_Title, string Message_Content)
    {
        SupplierMessageInfo entity = null;
        entity = new SupplierMessageInfo();
        entity.Supplier_Message_ID = 0;
        entity.Supplier_Message_SupplierID = SupplierID;
        entity.Supplier_Message_Title = Message_Title;
        entity.Supplier_Message_Content = Message_Content;
        entity.Supplier_Message_Addtime = DateTime.Now;
        entity.Supplier_Message_IsRead = 0;
        entity.Supplier_Message_Site = Public.GetCurrentSite();
        MyMessage.AddSupplierMessage(entity, Public.GetUserPrivilege());
    }

}

