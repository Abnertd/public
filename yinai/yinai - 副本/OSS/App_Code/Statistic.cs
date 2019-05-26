using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.BLL.Product;

/// <summary>
///Statistic 的摘要说明
/// </summary>
public class Statistic
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private ISQLHelper DBHelper;
    private Product product;
    private Member member;
    private ProductType producttype;

    public Statistic()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        DBHelper = SQLHelperFactory.CreateSQLHelper();
        product = new Product();
        member = new Member();
        producttype = new ProductType();

    }

    /// <summary>
    /// 销售量(额)排名
    /// </summary>
    /// <returns></returns>
    public string saleQuantity()
    {
        string startDate = tools.CheckStr(Request.QueryString["startDate"]);
        string endDate = tools.CheckStr(Request.QueryString["endDate"]);
        string orders_status = tools.CheckStr(Request.QueryString["orders_status"]);

        int PageSize = tools.CheckInt(Request["rows"]);
        int CurrentPage = tools.CheckInt(Request["page"]);

        int RecordCount = 0;
        int PageCount = 0;

        string SqlField = "G.Orders_Goods_Product_ID, SUM(Orders_Goods_Amount) AS saleCount, SUM(Orders_Goods_Product_Price * Orders_Goods_Amount) AS saleSum";
        string SqlTable = "Orders_Goods AS G INNER JOIN Orders AS O ON G.Orders_Goods_OrdersID = O.Orders_ID";
        string SqlOrder = "ORDER BY " + tools.CheckStr(Request["sidx"]) + " " + tools.CheckStr(Request["sord"]);

        string SqlParam = " WHERE O.Orders_Site = 'CN'";

        if (orders_status == "confirm") { SqlParam += " AND (Orders_Status > 0 AND Orders_Status < 3)"; }
        if (orders_status == "success") { SqlParam += " AND Orders_Status = 2"; }

        try { SqlParam += " AND DATEDIFF(d, O.Orders_Addtime, '" + DateTime.Parse(startDate).ToShortDateString() + "') < 1 AND DATEDIFF(d, O.Orders_Addtime, '" + DateTime.Parse(endDate).ToShortDateString() + "') > -1"; }
        catch (Exception ex) { }

        string SqlCount = "SELECT COUNT(DISTINCT Orders_Goods_Product_ID) FROM " + SqlTable + " " + SqlParam;

        DataTable DtList = null;
        try
        {
            RecordCount = (int)DBHelper.ExecuteScalar(SqlCount);
            PageCount = tools.CalculatePages(RecordCount, PageSize);
            CurrentPage = tools.DeterminePage(CurrentPage, PageCount);

            string SqlList = "SELECT * FROM (SELECT " + SqlField + " , ROW_NUMBER() OVER (" + SqlOrder + ") AS RowNumber FROM " + SqlTable + " " + SqlParam + " GROUP BY (G.Orders_Goods_Product_ID) ) AS T WHERE RowNumber >  " + (CurrentPage - 1) * PageSize + " AND RowNumber < " + ((CurrentPage * PageSize) + 1);

            int Product_ID, saleCount, Product_TypeID;
            string Product_Name, Product_Code, Product_SubName, Product_Spec;
            double saleSum;

            DtList = DBHelper.Query(SqlList);

            if (DtList.Rows.Count > 0)
            {
                ProductInfo pEntity;
                ProductTypeInfo ProductType;
                string targetURL = string.Empty;

                StringBuilder jsonBuilder = new StringBuilder();
                jsonBuilder.Append("{\"page\":" + CurrentPage + ",\"total\":" + PageCount + ",\"records\":" + RecordCount + ",\"rows\"");
                jsonBuilder.Append(":[");

                foreach (DataRow DrList in DtList.Rows)
                {
                    Product_ID = tools.NullInt(DrList["Orders_Goods_Product_ID"]);
                    saleCount = tools.NullInt(DrList["saleCount"]);
                    saleSum = tools.NullDbl(DrList["saleSum"]);

                    pEntity = product.GetProductByID(Product_ID);

                    if (pEntity != null)
                    {
                        Product_Name = pEntity.Product_Name;
                        Product_TypeID = pEntity.Product_TypeID;
                        Product_Code = pEntity.Product_Code;
                        Product_SubName = pEntity.Product_SubName;
                        Product_Spec = pEntity.Product_Spec;
                    }
                    else
                    {
                        Product_Name = "";
                        Product_TypeID = -1;
                        Product_Code = "";
                        Product_SubName = "";
                        Product_Spec = "";
                    }
                    pEntity = null;

                    //targetURL = Application["Site_URL"] + "product/detail.aspx?product_id=" + Product_ID;

                    jsonBuilder.Append("{\"Product_ID\":" + Product_ID + ",\"cell\":[");
                    //各字段
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Product_ID);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("<a href=\\\"" + targetURL + "\\\" target=\\\"_blank\\\">" + Public.JsonStr(Product_Code) + "</a>");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("<a href=\\\"" + targetURL + "\\\" target=\\\"_blank\\\">" + Public.JsonStr(Product_Name) + "</a>");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(Product_SubName));
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    ProductType = producttype.GetProductTypeByID(Product_TypeID);
                    if (ProductType != null)
                    {
                        jsonBuilder.Append(Public.JsonStr(ProductType.ProductType_Name));
                    }
                    else
                    {
                        jsonBuilder.Append("&nbsp;");
                    }
                    ProductType = null;
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(Product_Spec));
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(saleCount);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.DisplayCurrency(saleSum));
                    jsonBuilder.Append("\",");

                    jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                    jsonBuilder.Append("]},");
                    targetURL = null;
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]");
                jsonBuilder.Append("}");
                return jsonBuilder.ToString();
            }
            else
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            throw ex;
            return "";
        }
        finally
        {
            if (DtList != null)
            {
                DtList.Dispose();
                DtList = null;
            }

        }
    }

    /// <summary>
    /// 会员购物量(额)排名
    /// </summary>
    /// <returns></returns>
    public string memberBuyQuantity()
    {
        string startDate = tools.CheckStr(Request.QueryString["startDate"]);
        string endDate = tools.CheckStr(Request.QueryString["endDate"]);
        string orders_status = tools.CheckStr(Request.QueryString["orders_status"]);


        int PageSize = tools.CheckInt(Request["rows"]);
        int CurrentPage = tools.CheckInt(Request["page"]);

        int RecordCount = 0;
        int PageCount = 0;

        string SqlField = "Orders_BuyerID, SUM(Orders_Total_AllPrice) As buySum, COUNT(Orders_ID) As buyCount";
        string SqlTable = "Orders";
        string SqlOrder = "ORDER BY " + tools.CheckStr(Request["sidx"]) + " " + tools.CheckStr(Request["sord"]);

        string SqlParam = " WHERE Orders_Site = 'CN'";

        if (orders_status == "confirm") { SqlParam += " AND (Orders_Status > 0 AND Orders_Status < 3)"; }
        if (orders_status == "success") { SqlParam += " AND Orders_Status = 2"; }

        try { SqlParam += " AND DATEDIFF(d, Orders_Addtime, '" + DateTime.Parse(startDate).ToShortDateString() + "') < 1 AND DATEDIFF(d, Orders_Addtime, '" + DateTime.Parse(endDate).ToShortDateString() + "') > -1"; }
        catch (Exception ex) { }

        string SqlCount = "SELECT COUNT(DISTINCT Orders_BuyerID) FROM " + SqlTable + " " + SqlParam;

        DataTable DtList = null;
        try
        {
            RecordCount = (int)DBHelper.ExecuteScalar(SqlCount);
            PageCount = tools.CalculatePages(RecordCount, PageSize);
            CurrentPage = tools.DeterminePage(CurrentPage, PageCount);

            string SqlList = "SELECT * FROM (SELECT " + SqlField + " , ROW_NUMBER() OVER (" + SqlOrder + ") AS RowNumber FROM " + SqlTable + " " + SqlParam + " GROUP BY (Orders_BuyerID) ) AS T WHERE RowNumber >  " + (CurrentPage - 1) * PageSize + " AND RowNumber < " + ((CurrentPage * PageSize) + 1);

            int Member_ID, buyCount;
            string Member_Email, Member_Nickname;
            double buySum;

            DtList = DBHelper.Query(SqlList);

            if (DtList.Rows.Count > 0)
            {
                MemberInfo mEntity;
                MemberProfileInfo ProfileInfo;

                StringBuilder jsonBuilder = new StringBuilder();
                jsonBuilder.Append("{\"page\":" + CurrentPage + ",\"total\":" + PageCount + ",\"records\":" + RecordCount + ",\"rows\"");
                jsonBuilder.Append(":[");

                foreach (DataRow DrList in DtList.Rows)
                {
                    Member_ID = tools.NullInt(DrList["Orders_BuyerID"]);
                    buyCount = tools.NullInt(DrList["buyCount"]);
                    buySum = tools.NullDbl(DrList["buySum"]);

                    mEntity = member.GetMemberByID(Member_ID);

                    if (mEntity != null)
                    {
                        Member_Email = mEntity.Member_Email;
                        Member_Nickname = mEntity.Member_NickName;
                        if (mEntity.MemberProfileInfo != null)
                        {
                            ProfileInfo = mEntity.MemberProfileInfo;
                        }
                        else
                        {
                            ProfileInfo = new MemberProfileInfo();
                            ProfileInfo.Member_Name = "";
                        }
                    }
                    else
                    {
                        Member_Email = "";
                        Member_Nickname = "--";
                        ProfileInfo = new MemberProfileInfo();
                        ProfileInfo.Member_Name = "";
                    }

                    jsonBuilder.Append("{\"Member_ID\":" + Member_ID + ",\"cell\":[");
                    //各字段
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Member_ID);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    if (ProfileInfo.Member_Name.Length > 0)
                    {
                        jsonBuilder.Append("<a href=\\\"/member/member_view.aspx?member_id=" + Member_ID + "\\\">" + Public.JsonStr(ProfileInfo.Member_Name) + "</a>");

                    }
                    else
                    {
                        jsonBuilder.Append("<a href=\\\"/member/member_view.aspx?member_id=" + Member_ID + "\\\">--</a>");
                    }
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(Member_Nickname));
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.DisplaySex(ProfileInfo.Member_Sex));
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("<a href=\\\"/member/member_view.aspx?member_id=" + Member_ID + "\\\">" + Public.JsonStr(Member_Email) + "</a>");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(buyCount);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.DisplayCurrency(buySum));
                    jsonBuilder.Append("\",");

                    jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                    jsonBuilder.Append("]},");

                    ProfileInfo = null;
                }

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]");
                jsonBuilder.Append("}");
                return jsonBuilder.ToString();
            }
            else
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            throw ex;
            return "";
        }
        finally
        {
            if (DtList != null)
            {
                DtList.Dispose();
                DtList = null;
            }

        }
    }

    /// <summary>
    /// 供应商佣金统计
    /// </summary>
    /// <returns></returns>
    public string Supplier_Commission()
    {
        string startDate = tools.CheckStr(Request.QueryString["startDate"]);
        string endDate = tools.CheckStr(Request.QueryString["endDate"]);


        int PageSize = tools.CheckInt(Request["rows"]);
        int CurrentPage = tools.CheckInt(Request["page"]);

        int RecordCount = 0;
        int PageCount = 0;

        string SqlOrder = "ORDER BY  orders.Orders_PaymentStatus_Time desc";

        string SqlParam = " WHERE orders_goods.Orders_Goods_Product_brokerage>0 and orders.Orders_PaymentStatus>0 ";



        try { SqlParam += " AND DATEDIFF(d, orders.Orders_PaymentStatus_Time, '" + DateTime.Parse(startDate).ToShortDateString() + "') < 1 AND DATEDIFF(d, orders.Orders_PaymentStatus_Time, '" + DateTime.Parse(endDate).ToShortDateString() + "') > -1"; }
        catch (Exception ex) { }

        string SqlCount = "SELECT COUNT(Orders_Goods_ID) FROM orders_goods inner join supplier on orders_goods.Orders_Goods_Product_SupplierID=supplier.supplier_id inner join orders on orders.orders_id=orders_goods.Orders_Goods_OrdersID " + " " + SqlParam;

        DataTable DtList = null;
        try
        {
            RecordCount = (int)DBHelper.ExecuteScalar(SqlCount);
            PageCount = tools.CalculatePages(RecordCount, PageSize);
            CurrentPage = tools.DeterminePage(CurrentPage, PageCount);

            string SqlList = "SELECT Orders_Goods_OrdersID,Orders_Goods_Product_ID,Orders_Goods_Product_SupplierID,Orders_Goods_Amount,Orders_Goods_Product_Name,Orders_Goods_Product_Price,Orders_Goods_Product_brokerage,supplier.supplier_companyname,orders.Orders_Sn,orders.Orders_PaymentStatus_Time FROM orders_goods inner join supplier on orders_goods.Orders_Goods_Product_SupplierID=supplier.supplier_id join orders on orders.orders_id=orders_goods.Orders_Goods_OrdersID  " + " " + SqlParam + "  ";

            int Member_ID, buyCount, i;
            string Member_Email, Member_Nickname;
            double buySum;
            i = 0;
            double total_price, total_endprice, total_brokerage, product_price, product_brokerage; ;
            total_price = 0;
            total_endprice = 0;
            total_brokerage = 0;
            DtList = DBHelper.Query(SqlList);

            if (DtList.Rows.Count > 0)
            {

                StringBuilder jsonBuilder = new StringBuilder();
                jsonBuilder.Append("{\"page\":" + CurrentPage + ",\"total\":" + PageCount + ",\"records\":" + RecordCount + ",\"rows\"");
                jsonBuilder.Append(":[");

                foreach (DataRow DrList in DtList.Rows)
                {
                    i = i + 1;
                    product_price = (tools.NullDbl(DrList["Orders_Goods_Product_Price"]) * tools.NullDbl(DrList["Orders_Goods_Amount"]));
                    product_brokerage = (tools.NullDbl(DrList["Orders_Goods_Product_brokerage"]) * tools.NullDbl(DrList["Orders_Goods_Amount"]));
                    total_brokerage = total_brokerage + product_brokerage;
                    total_price = total_price + product_price;
                    total_endprice = total_endprice + (product_price - product_brokerage);

                    jsonBuilder.Append("{\"Member_ID\":" + i + ",\"cell\":[");
                    //各字段
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(i);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("<a href=\\\"/Supplier/Supplier_edit.aspx?Supplier_id=" + tools.NullInt(DrList["Orders_Goods_Product_SupplierID"]) + "\\\">" + Public.JsonStr(tools.NullStr(DrList["supplier_companyname"])) + "</a>");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("<a href=\\\"/orders/orders_view.aspx?orders_id=" + tools.NullInt(DrList["Orders_Goods_OrdersID"]) + "\\\">" + Public.JsonStr(tools.NullStr(DrList["Orders_Sn"])) + "</a>");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("" + Public.JsonStr(tools.NullStr(DrList["Orders_Goods_Product_Name"])) + "");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("" + Public.DisplayCurrency(product_price) + "");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("" + Public.DisplayCurrency(product_price - product_brokerage) + "");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("" + Public.DisplayCurrency(product_brokerage) + "");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("" + tools.NullDate(DrList["Orders_PaymentStatus_Time"]) + "");
                    jsonBuilder.Append("\",");



                    jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                    jsonBuilder.Append("]},");

                }

                jsonBuilder.Append("{\"Member_ID\":0,\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append("0");
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("合计");
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("--");
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("--");
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("" + Public.DisplayCurrency(total_price) + "");
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("" + Public.DisplayCurrency(total_endprice) + "");
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("" + Public.DisplayCurrency(total_brokerage) + "");
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("--");
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]");
                jsonBuilder.Append("}");
                return jsonBuilder.ToString();
            }
            else
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            throw ex;
            return "";
        }
        finally
        {
            if (DtList != null)
            {
                DtList.Dispose();
                DtList = null;
            }

        }
    }

    /// <summary>
    /// 商品访问购买次数
    /// </summary>
    /// <returns></returns>
    public string productBuyVisit()
    {
        string startDate = tools.CheckStr(Request.QueryString["startDate"]);
        string endDate = tools.CheckStr(Request.QueryString["endDate"]);
        string orders_status = tools.CheckStr(Request.QueryString["orders_status"]);

        int PageSize = tools.CheckInt(Request["rows"]);
        int CurrentPage = tools.CheckInt(Request["page"]);

        int RecordCount = 0;
        int PageCount = 0;

        string SqlField = "G.Orders_Goods_Product_ID, COUNT(DISTINCT Orders_ID) AS saleCount";
        string SqlTable = "Orders_Goods AS G INNER JOIN Orders AS O ON G.Orders_Goods_OrdersID = O.Orders_ID";
        string SqlOrder = "ORDER BY " + tools.CheckStr(Request["sidx"]) + " " + tools.CheckStr(Request["sord"]);

        string SqlParam = " WHERE O.Orders_Site = 'CN'";

        if (orders_status == "confirm") { SqlParam += " AND (Orders_Status > 0 AND Orders_Status < 3)"; }
        if (orders_status == "success") { SqlParam += " AND Orders_Status = 2"; }

        try { SqlParam += " AND DATEDIFF(d, O.Orders_Addtime, '" + DateTime.Parse(startDate).ToShortDateString() + "') < 1 AND DATEDIFF(d, O.Orders_Addtime, '" + DateTime.Parse(endDate).ToShortDateString() + "') > -1"; }
        catch (Exception ex) { }

        string SqlCount = "SELECT COUNT(DISTINCT Orders_Goods_Product_ID) FROM " + SqlTable + " " + SqlParam;
        DataTable DtList = null;

        try
        {
            RecordCount = (int)DBHelper.ExecuteScalar(SqlCount);
            PageCount = tools.CalculatePages(RecordCount, PageSize);
            CurrentPage = tools.DeterminePage(CurrentPage, PageCount);

            string SqlList = "SELECT * FROM (SELECT " + SqlField + " , ROW_NUMBER() OVER (" + SqlOrder + ") AS RowNumber FROM " + SqlTable + " " + SqlParam + " GROUP BY (G.Orders_Goods_Product_ID)) AS T WHERE RowNumber >  " + (CurrentPage - 1) * PageSize + " AND RowNumber < " + ((CurrentPage * PageSize) + 1);

            int Product_ID, saleCount, Product_Hits, Product_TypeID;
            string Product_Name, Product_Code, Product_SubName, Product_Spec;
            double saleSum, buyRate;

            DtList = DBHelper.Query(SqlList);

            if (DtList.Rows.Count > 0)
            {
                ProductInfo pEntity;
                ProductTypeInfo ProductType;
                string targetURL = string.Empty;

                StringBuilder jsonBuilder = new StringBuilder();
                jsonBuilder.Append("{\"page\":" + CurrentPage + ",\"total\":" + PageCount + ",\"records\":" + RecordCount + ",\"rows\"");
                jsonBuilder.Append(":[");

                foreach (DataRow DrList in DtList.Rows)
                {
                    Product_ID = tools.NullInt(DrList["Orders_Goods_Product_ID"]);
                    saleCount = tools.NullInt(DrList["saleCount"]);

                    pEntity = product.GetProductByID(Product_ID);

                    if (pEntity != null)
                    {
                        Product_Name = pEntity.Product_Name;
                        Product_TypeID = pEntity.Product_TypeID;
                        Product_Code = pEntity.Product_Code;
                        Product_SubName = pEntity.Product_SubName;
                        Product_Spec = pEntity.Product_Spec;
                        Product_Hits = pEntity.Product_Hits;
                    }
                    else
                    {
                        Product_Name = "";
                        Product_TypeID = -1;
                        Product_Code = "";
                        Product_SubName = "";
                        Product_Spec = "";
                        Product_Hits = 0;
                    }
                    pEntity = null;

                    if (Product_Hits == 0) { buyRate = 0; }
                    else { buyRate = (double)saleCount / Product_Hits * 100; }

                    targetURL = Application["Site_URL"] + "product/detail.aspx?product_id=" + Product_ID;

                    jsonBuilder.Append("{\"Product_ID\":" + Product_ID + ",\"cell\":[");
                    //各字段
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Product_ID);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("<a href=\\\"" + targetURL + "\\\" target=\\\"_blank\\\">" + Public.JsonStr(Product_Code) + "</a>");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("<a href=\\\"" + targetURL + "\\\" target=\\\"_blank\\\">" + Public.JsonStr(Product_Name) + "</a>");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(Product_SubName));
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    ProductType = producttype.GetProductTypeByID(Product_TypeID);
                    if (ProductType != null) { jsonBuilder.Append(Public.JsonStr(ProductType.ProductType_Name)); }
                    else { jsonBuilder.Append("&nbsp;"); }
                    ProductType = null;
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Public.JsonStr(Product_Spec));
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Product_Hits);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(saleCount);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(buyRate.ToString("0.00"));
                    jsonBuilder.Append("%\",");

                    jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                    jsonBuilder.Append("]},");

                    targetURL = null;
                }
                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]");
                jsonBuilder.Append("}");
                return jsonBuilder.ToString();
            }
            else
            {
                return "";
            }
        }
        catch (Exception ex)
        {
            throw ex;
            return "";
        }
        finally
        {
            if (DtList != null)
            {
                DtList.Dispose();
                DtList = null;
            }

        }
    }

    /// <summary>
    /// 获取某段时间内交易成功的订单数量
    /// </summary>
    /// <param name="startDate">起始时间</param>
    /// <param name="endDate">截止时间</param>
    /// <returns>交易成功的订单数量</returns>
    public int GetOrdersCountByDate(string startDate, string endDate, string orders_status)
    {
        string SqlList = "SELECT COUNT(Orders_ID) FROM Orders WHERE Orders_Site = 'CN'";

        switch (orders_status)
        {
            case "confirm":
                SqlList += " AND (Orders_Status > 0 AND Orders_Status < 3)";
                break;
            case "success":
                SqlList += " AND Orders_Status = 2";
                break;
            default:
                SqlList += " AND Orders_Status = " + orders_status;
                break;
        }

        try { SqlList += " AND DATEDIFF(d, Orders_Addtime, '" + DateTime.Parse(startDate).ToShortDateString() + "') < 1 AND DATEDIFF(d, Orders_Addtime, '" + DateTime.Parse(endDate).ToShortDateString() + "') > -1"; }
        catch (Exception ex) { }

        try
        {
            return (int)DBHelper.ExecuteScalar(SqlList);
        }
        catch (Exception ex) { return 0; }
    }

    /// <summary>
    /// 待审核供应商
    /// </summary>
    /// <returns></returns>
    public int GetUnAuditSupplierAmount()
    {
        string SqlList = "SELECT COUNT(Supplier_ID) FROM Supplier WHERE Supplier_Site = 'CN' and Supplier_AuditStatus=0";



        try { return (int)DBHelper.ExecuteScalar(SqlList); }
        catch (Exception ex) { return 0; }
    }
    /// <summary>
    /// 待审核物流发布
    /// </summary>
    /// <returns></returns>
    public int GetUnAuditLogisticsPost()
    {
        string SqlList = "  SELECT COUNT(Supplier_Logistics_ID) FROM Supplier_Logistics WHERE  Supplier_Logistics_IsAudit=0 and";



        try { return (int)DBHelper.ExecuteScalar(SqlList); }
        catch (Exception ex) { return 0; }
    }

    /// <summary>
    /// 待审核招标拍卖
    /// </summary>
    /// <returns></returns>
    public int GetUnAuditBidAmount()
    {
        string SqlList = "SELECT COUNT(Bid_ID) FROM Bid WHERE Bid_IsAudit=0 and Bid_IsShow = 1 and Bid_Status=1";



        try { return (int)DBHelper.ExecuteScalar(SqlList); }
        catch (Exception ex) { return 0; }
    }

    /// <summary>
    /// 待审核供应商资质申请
    /// </summary>
    /// <returns></returns>
    public int GetUnAuditSupplierCertAmount()
    {
        string SqlList = "SELECT COUNT(Supplier_ID) FROM Supplier WHERE Supplier_Site = 'CN' and Supplier_Cert_Status=1";



        try { return (int)DBHelper.ExecuteScalar(SqlList); }
        catch (Exception ex) { return 0; }
    }


    /// <summary>
    /// 待审核供应商资质申请
    /// </summary>
    /// <returns></returns>
    public int GetUnAuditSupplierCertAmount_new()
    {
        string SqlList = "SELECT COUNT(Supplier_ID) FROM ( SELECT *, ROW_NUMBER() OVER( ORDER BY Supplier.Supplier_ID DESC  ) AS RowNum FROM Supplier  WHERE Supplier.Supplier_Site = 'CN' AND Supplier.Supplier_Trash = 0 AND Supplier.Supplier_AuditStatus = 0 )  AS T WHERE RowNum > 0 AND RowNum < 21";


        try { return (int)DBHelper.ExecuteScalar(SqlList); }
        catch (Exception ex) { return 0; }
    }

    /// <summary>
    /// 待审核供应商店铺申请
    /// </summary>
    /// <returns></returns>
    public int GetUnAuditShopApplyAmount()
    {
        string SqlList = "SELECT COUNT(Shop_Apply_ID) FROM Supplier_apply WHERE  Shop_Apply_Status=0";



        try { return (int)DBHelper.ExecuteScalar(SqlList); }
        catch (Exception ex) { return 0; }
    }

    /// <summary>
    /// 待审核商品
    /// </summary>
    /// <returns></returns>
    public int GetUnAuditProductAmount()
    {
        string SqlList = "SELECT COUNT(product_id) FROM product_basic WHERE  product_isaudit=0";



        try { return (int)DBHelper.ExecuteScalar(SqlList); }
        catch (Exception ex) { return 0; }
    }


    /// <summary>
    /// 待回复留言
    /// </summary>
    /// <returns></returns>
    public int GetUnReplyMessageAmount()
    {
        string SqlList = "SELECT COUNT(feedback_id) FROM feedback WHERE  feedback_type=1 and feedback_reply_content=''";



        try { return (int)DBHelper.ExecuteScalar(SqlList); }
        catch (Exception ex) { return 0; }
    }


    /// <summary>
    /// 投诉留言留言
    /// </summary>
    /// <returns></returns>
    public int GetComplaintMail()
    {
        string SqlList = "SELECT COUNT(feedback_id) FROM feedback WHERE  feedback_type=8 and feedback_reply_content=''";



        try { return (int)DBHelper.ExecuteScalar(SqlList); }
        catch (Exception ex) { return 0; }
    }

    /// <summary>
    /// 未处理缺货登记
    /// </summary>
    /// <returns></returns>
    public int GetUnProcessStockoutAmount()
    {
        string SqlList = "SELECT COUNT(Stockout_ID) FROM Stockout_Booking WHERE  Stockout_IsRead=0";



        try { return (int)DBHelper.ExecuteScalar(SqlList); }
        catch (Exception ex) { return 0; }
    }

    /// <summary>
    /// 得到订单的商品
    /// </summary>
    /// <param name="strType"></param>
    /// <returns></returns>
    public int GetOrdersCount(string strType)
    {
        string SqlList = "SELECT COUNT(Orders_ID) FROM Orders WHERE Orders_Site = 'CN'";

        switch (strType)
        {
            case "confirm":
                SqlList += " AND (Orders_Status > 0 AND Orders_Status < 3)";
                break;
            case "success":
                SqlList += " AND Orders_Status = 2";
                break;
            case "unconfirm":
                SqlList += " AND Orders_Status = 0";
                break;
            case "todayunpay":
                SqlList += " AND Orders_PaymentStatus = 0 AND DATEDIFF(d, Orders_Addtime, getdate()) < 1 AND DATEDIFF(d, Orders_Addtime, getdate()) > -1 ";
                break;
            case "todaypayundelivery":
                SqlList += " AND Orders_PaymentStatus = 1 AND Orders_DeliveryStatus = 0 AND DATEDIFF(d, Orders_Addtime, getdate()) < 1 AND DATEDIFF(d, Orders_Addtime, getdate()) > -1";
                break;
        }

        try { return (int)DBHelper.ExecuteScalar(SqlList); }
        catch (Exception ex) { return 0; }
    }

    /// <summary>
    /// 得到未支付的应收账款
    /// </summary>
    /// <param name="strType"></param>
    /// <returns></returns>
    public int GetCreditLimitCount()
    {
        string SqlList = "SELECT COUNT(CreditLimit_Log_ID) FROM Supplier_CreditLimit_Log WHERE CreditLimit_Log_Amount<0 and CreditLimit_Log_PaymentStatus=0";


        try { return (int)DBHelper.ExecuteScalar(SqlList); }
        catch (Exception ex) { return 0; }
    }

    /// <summary>
    /// 获取某段时间内交易成功的订单总额
    /// </summary>
    /// <param name="startDate">起始时间</param>
    /// <param name="endDate">截止时间</param>
    /// <returns>交易成功的订单总额</returns>
    public double GetOrdersAllPriceByDate(string startDate, string endDate, string orders_status)
    {
        //Orders_Status = 2

        string SqlList = "SELECT SUM(Orders_Total_AllPrice) FROM Orders WHERE Orders_Site = 'CN'";

        if (orders_status == "confirm") { SqlList += " AND (Orders_Status > 0 AND Orders_Status < 3)"; }
        if (orders_status == "success") { SqlList += " AND Orders_Status = 2"; }

        try { SqlList += " AND DATEDIFF(d, Orders_Addtime, '" + DateTime.Parse(startDate).ToShortDateString() + "') < 1 AND DATEDIFF(d, Orders_Addtime, '" + DateTime.Parse(endDate).ToShortDateString() + "') > -1"; }
        catch (Exception ex) { }

        try
        {
            return tools.NullDbl(DBHelper.ExecuteScalar(SqlList));
        }
        catch (Exception ex) { return 0; }
    }

    /// <summary>
    /// 获取会员总数
    /// </summary>
    /// <returns></returns>
    public int GetMemberCount()
    {
        try { return (int)DBHelper.ExecuteScalar("SELECT COUNT(Member_ID) FROM Member"); }
        catch (Exception ex) { return 0; }
    }

    public int GetMemberCount(string strType)
    {
        string SqlCount = "SELECT COUNT(Member_ID) FROM Member WHERE Member_ID > 0";

        switch (strType)
        {
            case "today":
                SqlCount += " AND DATEDIFF(d, Member_Addtime, getdate()) < 1 AND DATEDIFF(d, Member_Addtime, getdate()) > -1";
                break;
            case "yesterday":
                SqlCount += " AND DATEDIFF(d, Member_Addtime, '" + DateTime.Today.AddDays(-1).ToShortDateString() + "') < 1 AND DATEDIFF(d, Member_Addtime, '" + DateTime.Today.AddDays(-1).ToShortDateString() + "') > -1";
                break;
        }

        try { return (int)DBHelper.ExecuteScalar(SqlCount); }
        catch (Exception ex) { return 0; }
    }

    public int GetSupplierCount(string strType)
    {
        string SqlCount = "SELECT COUNT(Supplier_ID) FROM Supplier WHERE Supplier_ID > 0";

        switch (strType)
        {
            case "today":
                SqlCount += " AND DATEDIFF(d, Supplier_Addtime, getdate()) < 1 AND DATEDIFF(d, Supplier_Addtime, getdate()) > -1";
                break;
            case "yesterday":
                SqlCount += " AND DATEDIFF(d, Supplier_Addtime, '" + DateTime.Today.AddDays(-1).ToShortDateString() + "') < 1 AND DATEDIFF(d, Supplier_Addtime, '" + DateTime.Today.AddDays(-1).ToShortDateString() + "') > -1";
                break;
        }

        try { return (int)DBHelper.ExecuteScalar(SqlCount); }
        catch (Exception ex) { return 0; }
    }

    /// <summary>
    /// 获取购买购买过的会员总数
    /// </summary>
    /// <returns>会员总数</returns>
    public int GetBuyMemberCount(string startDate, string endDate)
    {
        string SqlList = "SELECT COUNT(DISTINCT Orders_BuyerID) FROM Orders WHERE Orders_Site = 'CN'";
        try { SqlList += " AND DATEDIFF(d, Orders_Addtime, '" + DateTime.Parse(startDate).ToShortDateString() + "') < 1 AND DATEDIFF(d, Orders_Addtime, '" + DateTime.Parse(endDate).ToShortDateString() + "') > -1"; }
        catch (Exception ex) { }

        try
        {
            return (int)DBHelper.ExecuteScalar(SqlList);
        }
        catch (Exception ex) { return 0; }
    }

    /// <summary>
    /// 商品预警数量
    /// </summary>
    /// <returns></returns>
    public int ProductAlertCount()
    {
        string SqlCount = "SELECT COUNT(Product_ID) FROM Product_Basic WHERE Product_StockAmount <= Product_AlertAmount";
        try { return (int)DBHelper.ExecuteScalar(SqlCount); }
        catch (Exception ex) { throw ex; }
    }

    /// <summary>
    /// 待处理退换货申请数量
    /// </summary>
    /// <returns></returns>
    public int BackApplyCount()
    {
        string SqlCount = "SELECT COUNT(Orders_BackApply_ID) FROM Orders_BackApply WHERE Orders_BackApply_Status=0";
        try { return (int)DBHelper.ExecuteScalar(SqlCount); }
        catch (Exception ex) { throw ex; }
    }


    public void saleQuantity_Export()
    {
        string startDate = tools.CheckStr(Request.QueryString["startDate"]);
        string endDate = tools.CheckStr(Request.QueryString["endDate"]);
        string orders_status = tools.CheckStr(Request.QueryString["orders_status"]);


        int RecordCount = 0;
        int PageCount = 0;

        string SqlField = "G.Orders_Goods_Product_ID, SUM(Orders_Goods_Amount) AS saleCount, SUM(Orders_Goods_Product_Price * Orders_Goods_Amount) AS saleSum";
        string SqlTable = "Orders_Goods AS G INNER JOIN Orders AS O ON G.Orders_Goods_OrdersID = O.Orders_ID";
        string SqlOrder = "ORDER BY SUM(Orders_Goods_Amount) desc";

        string SqlParam = " WHERE O.Orders_Site = 'CN'";

        if (orders_status == "confirm") { SqlParam += " AND (Orders_Status > 0 AND Orders_Status < 3)"; }
        if (orders_status == "success") { SqlParam += " AND Orders_Status = 2"; }

        try { SqlParam += " AND DATEDIFF(d, O.Orders_Addtime, '" + DateTime.Parse(startDate).ToShortDateString() + "') < 1 AND DATEDIFF(d, O.Orders_Addtime, '" + DateTime.Parse(endDate).ToShortDateString() + "') > -1"; }
        catch (Exception ex) { }

        string SqlCount = "SELECT COUNT(DISTINCT Orders_Goods_Product_ID) FROM " + SqlTable + " " + SqlParam;

        DataTable DtList = null;
        try
        {

            string SqlList = "SELECT * FROM (SELECT " + SqlField + " , ROW_NUMBER() OVER (" + SqlOrder + ") AS RowNumber FROM " + SqlTable + " " + SqlParam + " GROUP BY (G.Orders_Goods_Product_ID) ) AS T";

            int Product_ID, saleCount, Product_TypeID;
            string Product_Name, Product_Code, Product_SubName, Product_Spec;
            double saleSum;

            DataTable dt = new DataTable("excel");
            DataRow dr = null;
            DataColumn column = null;

            string[] dtcol = { "编号", "商品编码", "商品名称", "商品通用名", "商品类型", "规格", "销售量", "销售额" };
            foreach (string col in dtcol)
            {
                try { dt.Columns.Add(col); }
                catch { dt.Columns.Add(col + ","); }
            }
            dtcol = null;

            DtList = DBHelper.Query(SqlList);

            if (DtList.Rows.Count > 0)
            {
                ProductInfo pEntity;
                ProductTypeInfo ProductType;
                string targetURL = string.Empty;


                foreach (DataRow DrList in DtList.Rows)
                {
                    Product_ID = tools.NullInt(DrList["Orders_Goods_Product_ID"]);
                    saleCount = tools.NullInt(DrList["saleCount"]);
                    saleSum = tools.NullDbl(DrList["saleSum"]);

                    pEntity = product.GetProductByID(Product_ID);

                    if (pEntity != null)
                    {
                        Product_Name = pEntity.Product_Name;
                        Product_TypeID = pEntity.Product_TypeID;
                        Product_Code = pEntity.Product_Code;
                        Product_SubName = pEntity.Product_SubName;
                        Product_Spec = pEntity.Product_Spec;
                    }
                    else
                    {
                        Product_Name = "";
                        Product_TypeID = -1;
                        Product_Code = "";
                        Product_SubName = "";
                        Product_Spec = "";
                    }
                    pEntity = null;
                    dr = dt.NewRow();
                    dr[0] = Product_ID;
                    dr[1] = Product_Code;
                    dr[2] = Product_Name;
                    dr[3] = Product_SubName;
                    ProductType = producttype.GetProductTypeByID(Product_TypeID);
                    if (ProductType != null)
                    {
                        dr[4] = ProductType.ProductType_Name;
                    }
                    else
                    {
                        dr[4] = "";
                    }
                    ProductType = null;
                    dr[5] = Product_Spec;
                    dr[6] = saleCount;
                    dr[7] = Public.DisplayCurrency(saleSum);

                    dt.Rows.Add(dr);
                    dr = null;

                }
            }
            Public.toExcel(dt);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (DtList != null)
            {
                DtList.Dispose();
                DtList = null;
            }

        }




    }

    public void memberBuyQuantity_Export()
    {
        string startDate = tools.CheckStr(Request.QueryString["startDate"]);
        string endDate = tools.CheckStr(Request.QueryString["endDate"]);
        string orders_status = tools.CheckStr(Request.QueryString["orders_status"]);


        int RecordCount = 0;
        int PageCount = 0;

        string SqlField = "Orders_BuyerID, SUM(Orders_Total_AllPrice) As buySum, COUNT(Orders_ID) As buyCount";
        string SqlTable = "Orders";
        string SqlOrder = "ORDER BY COUNT(Orders_ID) desc";

        string SqlParam = " WHERE Orders_Site = 'CN'";

        if (orders_status == "confirm") { SqlParam += " AND (Orders_Status > 0 AND Orders_Status < 3)"; }
        if (orders_status == "success") { SqlParam += " AND Orders_Status = 2"; }

        try { SqlParam += " AND DATEDIFF(d, Orders_Addtime, '" + DateTime.Parse(startDate).ToShortDateString() + "') < 1 AND DATEDIFF(d, Orders_Addtime, '" + DateTime.Parse(endDate).ToShortDateString() + "') > -1"; }
        catch (Exception ex) { }

        string SqlCount = "SELECT COUNT(DISTINCT Orders_BuyerID) FROM " + SqlTable + " " + SqlParam;

        DataTable DtList = null;
        try
        {

            string SqlList = "SELECT * FROM (SELECT " + SqlField + " , ROW_NUMBER() OVER (" + SqlOrder + ") AS RowNumber FROM " + SqlTable + " " + SqlParam + " GROUP BY (Orders_BuyerID) ) AS T";

            int Member_ID, buyCount;
            string Member_Email, Member_Nickname;
            double buySum;

            DataTable dt = new DataTable("excel");
            DataRow dr = null;
            DataColumn column = null;

            string[] dtcol = { "编号", "姓名", "昵称", "性别", "Email", "购物量", "购物额" };
            foreach (string col in dtcol)
            {
                try { dt.Columns.Add(col); }
                catch { dt.Columns.Add(col + ","); }
            }
            dtcol = null;

            DtList = DBHelper.Query(SqlList);

            if (DtList.Rows.Count > 0)
            {
                MemberInfo mEntity;
                MemberProfileInfo ProfileInfo;


                foreach (DataRow DrList in DtList.Rows)
                {
                    Member_ID = tools.NullInt(DrList["Orders_BuyerID"]);
                    buyCount = tools.NullInt(DrList["buyCount"]);
                    buySum = tools.NullDbl(DrList["buySum"]);

                    mEntity = member.GetMemberByID(Member_ID);

                    if (mEntity != null)
                    {
                        Member_Email = mEntity.Member_Email;
                        Member_Nickname = mEntity.Member_NickName;
                        if (mEntity.MemberProfileInfo != null)
                        {
                            ProfileInfo = mEntity.MemberProfileInfo;
                        }
                        else
                        {
                            ProfileInfo = new MemberProfileInfo();
                        }
                    }
                    else
                    {
                        Member_Email = "";
                        Member_Nickname = "--";
                        ProfileInfo = new MemberProfileInfo();
                    }

                    dr = dt.NewRow();
                    dr[0] = Member_ID;
                    if (ProfileInfo.Member_Name.Length > 0)
                    {
                        dr[1] = ProfileInfo.Member_Name;
                    }
                    else
                    {
                        dr[1] = "--";
                    }
                    dr[2] = Member_Nickname;
                    dr[3] = Public.DisplaySex(ProfileInfo.Member_Sex);

                    dr[4] = Member_Email;
                    dr[5] = buyCount;
                    dr[6] = Public.DisplayCurrency(buySum);

                    dt.Rows.Add(dr);
                    dr = null;

                }
            }
            Public.toExcel(dt);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (DtList != null)
            {
                DtList.Dispose();
                DtList = null;
            }

        }




    }

    public void productBuyVisit_Export()
    {
        string startDate = tools.CheckStr(Request.QueryString["startDate"]);
        string endDate = tools.CheckStr(Request.QueryString["endDate"]);
        string orders_status = tools.CheckStr(Request.QueryString["orders_status"]);

        int PageSize = tools.CheckInt(Request["rows"]);
        int CurrentPage = tools.CheckInt(Request["page"]);

        int RecordCount = 0;
        int PageCount = 0;

        string SqlField = "G.Orders_Goods_Product_ID, COUNT(DISTINCT Orders_ID) AS saleCount";
        string SqlTable = "Orders_Goods AS G INNER JOIN Orders AS O ON G.Orders_Goods_OrdersID = O.Orders_ID";
        string SqlOrder = "ORDER BY COUNT(DISTINCT Orders_ID) desc";

        string SqlParam = " WHERE O.Orders_Site = 'CN'";

        if (orders_status == "confirm") { SqlParam += " AND (Orders_Status > 0 AND Orders_Status < 3)"; }
        if (orders_status == "success") { SqlParam += " AND Orders_Status = 2"; }

        try { SqlParam += " AND DATEDIFF(d, O.Orders_Addtime, '" + DateTime.Parse(startDate).ToShortDateString() + "') < 1 AND DATEDIFF(d, O.Orders_Addtime, '" + DateTime.Parse(endDate).ToShortDateString() + "') > -1"; }
        catch (Exception ex) { }

        string SqlCount = "SELECT COUNT(DISTINCT Orders_Goods_Product_ID) FROM " + SqlTable + " " + SqlParam;
        DataTable DtList = null;

        try
        {

            string SqlList = "SELECT * FROM (SELECT " + SqlField + " , ROW_NUMBER() OVER (" + SqlOrder + ") AS RowNumber FROM " + SqlTable + " " + SqlParam + " GROUP BY (G.Orders_Goods_Product_ID)) AS T";

            int Product_ID, saleCount, Product_Hits, Product_TypeID;
            string Product_Name, Product_Code, Product_SubName, Product_Spec;
            double saleSum, buyRate;

            DataTable dt = new DataTable("excel");
            DataRow dr = null;
            DataColumn column = null;

            string[] dtcol = { "编号", "商品编码", "商品名称", "商品通用名", "商品类型", "规格", "访问次数", "购买次数", "访问购买率" };
            foreach (string col in dtcol)
            {
                try { dt.Columns.Add(col); }
                catch { dt.Columns.Add(col + ","); }
            }
            dtcol = null;

            DtList = DBHelper.Query(SqlList);

            if (DtList.Rows.Count > 0)
            {
                ProductInfo pEntity;
                ProductTypeInfo ProductType;
                string targetURL = string.Empty;

                StringBuilder jsonBuilder = new StringBuilder();
                jsonBuilder.Append("{\"page\":" + CurrentPage + ",\"total\":" + PageCount + ",\"records\":" + RecordCount + ",\"rows\"");
                jsonBuilder.Append(":[");

                foreach (DataRow DrList in DtList.Rows)
                {
                    Product_ID = tools.NullInt(DrList["Orders_Goods_Product_ID"]);
                    saleCount = tools.NullInt(DrList["saleCount"]);

                    pEntity = product.GetProductByID(Product_ID);

                    if (pEntity != null)
                    {
                        Product_Name = pEntity.Product_Name;
                        Product_TypeID = pEntity.Product_TypeID;
                        Product_Code = pEntity.Product_Code;
                        Product_SubName = pEntity.Product_SubName;
                        Product_Spec = pEntity.Product_Spec;
                        Product_Hits = pEntity.Product_Hits;
                    }
                    else
                    {
                        Product_Name = "";
                        Product_TypeID = -1;
                        Product_Code = "";
                        Product_SubName = "";
                        Product_Spec = "";
                        Product_Hits = 0;
                    }
                    pEntity = null;

                    if (Product_Hits == 0) { buyRate = 0; }
                    else { buyRate = (double)saleCount / Product_Hits * 100; }

                    dr = dt.NewRow();
                    dr[0] = Product_ID;
                    dr[1] = Product_Code;
                    dr[2] = Product_Name;
                    dr[3] = Product_SubName;

                    ProductType = producttype.GetProductTypeByID(Product_TypeID);
                    if (ProductType != null) { dr[4] = ProductType.ProductType_Name; }
                    else { dr[4] = ""; }
                    ProductType = null;
                    dr[5] = Product_Spec;
                    dr[6] = Product_Hits;
                    dr[7] = saleCount;
                    dr[8] = buyRate.ToString("0.00") + "%";

                    dt.Rows.Add(dr);
                    dr = null;

                }
            }
            Public.toExcel(dt);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (DtList != null)
            {
                DtList.Dispose();
                DtList = null;
            }

        }




    }



}
