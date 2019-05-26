using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.BLL.Product;
using Glaer.Trade.Util.Http;
using System.Text;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Statistics 的摘要说明
/// </summary>
public class SupplierStatistics
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
    private Public_Class pub;
    private Supplier supplier;
    private PageURL pageurl;
    private IJsonHelper JsonHelper;
    private ICategory category;

    public SupplierStatistics()
	{
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        DBHelper = SQLHelperFactory.CreateSQLHelper();
        JsonHelper = JsonHelperFactory.CreateJsonHelper();
        product = new Product();
        pub = new Public_Class();
        supplier = new Supplier();
        pageurl = new PageURL();
        category = CategoryFactory.CreateCategory();
	}

    #region

    /// <summary>
    /// 定义一个Series类 设置其每一组sereis的一些基本属性
    /// </summary>
    class Series
    {
        /// <summary>
        /// sereis序列组id
        /// </summary>
        public int id
        {
            get;
            set;
        }

        /// <summary>
        /// series序列组名称
        /// </summary>
        public string name
        {
            get;
            set;
        }

        /// <summary>
        /// series序列组呈现图表类型(line、column、bar等)
        /// </summary>
        public string type
        {
            get;
            set;
        }

        /// <summary>
        /// series序列组的数据为数据类型数组
        /// </summary>
        public List<object> data
        {
            get;
            set;
        }
    }

    #endregion 

    /// <summary>
    /// 销售额统计
    /// </summary>
    public void SalesPrice()
    {
        DateTime startDate;
        if (!DateTime.TryParse(tools.CheckStr(Request["startdate"]), out startDate))
        {
            startDate = DateTime.Today.AddDays(-15);
        }
        DateTime endDate;
        if (!DateTime.TryParse(tools.CheckStr(Request["endDate"]), out endDate))
        {
            endDate = DateTime.Today;
        }
        string orders_status = tools.CheckStr(Request["orders_status"]);


        //考虑到图表的category是字符串数组 这里定义一个string的List
        List<string> categoryList = new List<string>();
        //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类
        List<Series> seriesList = new List<Series>();
        //考虑到Echarts图表需要设置legend内的data数组为series的name集合这里需要定义一个legend数组
        List<string> legendList = new List<string>();

        StringBuilder SqlList = new StringBuilder();
        SqlDataReader RdrList = null;

        SqlList.Append("SELECT MIN(CONVERT(varchar(100), Orders_Payment_Addtime, 23)) AS orders_date, SUM(Orders_Payment_Amount) AS order_amount FROM Orders_Payment");

        SqlList.Append(" WHERE DATEDIFF(d, Orders_Payment_Addtime, '"+ startDate +"') <= 0");
        SqlList.Append(" AND DATEDIFF(d, Orders_Payment_Addtime, '" + endDate + "') >= 0");

        SqlList.Append(" AND Orders_Payment_OrdersID IN (SELECT Orders_ID FROM Orders WHERE Orders_SupplierID = " + tools.NullInt(Session["supplier_id"]) + ") GROUP BY DATEPART(DY, Orders_Payment_Addtime)");

        try
        {
            //定义一个Series对象
            Series seriesObj = new Series();
            seriesObj.id = 1;
            seriesObj.name = "销售额";
            seriesObj.type = "line"; //线性图呈现
            seriesObj.data = new List<object>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            RdrList = DBHelper.ExecuteReader(SqlList.ToString());
            if (RdrList.HasRows)
            {
                while (RdrList.Read())
                {
                    //加入category刻度数组
                    categoryList.Add(tools.NullStr(RdrList["orders_date"]));

                    //加入数据值series序列数组 这里提供为了效果只提供一组series数据好了                
                    seriesObj.data.Add(tools.NullInt(RdrList["order_amount"])); //数据依次递增
                }
                //将sereis对象压入sereis数组列表内
                seriesList.Add(seriesObj);
            }
            else
            {
                double upper = (endDate - startDate).TotalDays;
                for (int i = 0; i <= upper; i++)
                {
                    //加入category刻度数组
                    categoryList.Add(startDate.AddDays(i).ToString("yyyy-MM-dd"));
                    //加入数据值series序列数组 这里提供为了效果只提供一组series数据好了                
                    seriesObj.data.Add("0"); //数据依次递增
                }
                //将sereis对象压入sereis数组列表内
                seriesList.Add(seriesObj);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            RdrList.Close();
            RdrList = null;
        }

        //最后调用相关函数将List转换为Json
        //因为我们需要返回category和series、legend多个对象 这里我们自己在new一个新的对象来封装这两个对象
        var newObj = new
        {
            category = categoryList,
            series = seriesList
        };
        //Response返回新对象的json数据
        Response.Write(JsonHelper.ObjectToJSON(newObj));
        Response.End();
    }

    /// <summary>
    /// 销量统计
    /// </summary>
    public void SalesAmount()
    {
        DateTime startDate;
        if (!DateTime.TryParse(tools.CheckStr(Request["startdate"]), out startDate))
        {
            startDate = DateTime.Today.AddDays(-15);
        }
        DateTime endDate;
        if (!DateTime.TryParse(tools.CheckStr(Request["endDate"]), out endDate))
        {
            endDate = DateTime.Today;
        }
        string orders_status = tools.CheckStr(Request["orders_status"]);


        //考虑到图表的category是字符串数组 这里定义一个string的List
        List<string> categoryList = new List<string>();
        //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类
        List<Series> seriesList = new List<Series>();
        //考虑到Echarts图表需要设置legend内的data数组为series的name集合这里需要定义一个legend数组
        List<string> legendList = new List<string>();

        StringBuilder SqlList = new StringBuilder();
        SqlDataReader RdrList = null;

        SqlList.Append("SELECT MIN(CONVERT(varchar(100), Orders_Addtime, 23)) AS orders_date, COUNT(Orders_ID) AS order_count FROM Orders");
        SqlList.Append(" WHERE DATEDIFF(d, Orders_Addtime, '"+ startDate +"') <= 0");
        SqlList.Append(" AND DATEDIFF(d, Orders_Addtime, '" + endDate + "') >= 0");
        SqlList.Append(" AND Orders_SupplierID = " + tools.NullInt(Session["supplier_id"]) + " GROUP BY DATEPART(DY, Orders_Addtime)");

        try
        {
            //定义一个Series对象
            Series seriesObj = new Series();
            seriesObj.id = 1;
            seriesObj.name = "订单量统计";
            seriesObj.type = "line"; //线性图呈现
            seriesObj.data = new List<object>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            RdrList = DBHelper.ExecuteReader(SqlList.ToString());
            if (RdrList.HasRows)
            {
                while (RdrList.Read())
                {
                    //加入category刻度数组
                    categoryList.Add(tools.NullStr(RdrList["orders_date"]));

                    //加入数据值series序列数组 这里提供为了效果只提供一组series数据好了                
                    seriesObj.data.Add(tools.NullInt(RdrList["order_count"])); //数据依次递增
                }
                //将sereis对象压入sereis数组列表内
                seriesList.Add(seriesObj);
            }else
            {
                double upper = (endDate - startDate).TotalDays;
                for (int i = 0; i <= upper; i++)
                {
                    //加入category刻度数组
                    categoryList.Add(startDate.AddDays(i).ToString("yyyy-MM-dd"));
                    //加入数据值series序列数组 这里提供为了效果只提供一组series数据好了                
                    seriesObj.data.Add("0"); //数据依次递增
                }
                //将sereis对象压入sereis数组列表内
                seriesList.Add(seriesObj);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            RdrList.Close();
            RdrList = null;
        }

        //最后调用相关函数将List转换为Json
        //因为我们需要返回category和series、legend多个对象 这里我们自己在new一个新的对象来封装这两个对象
        var newObj = new
        {
            category = categoryList,
            series = seriesList
        };
        //Response返回新对象的json数据
        Response.Write(JsonHelper.ObjectToJSON(newObj));
        Response.End();
    }

    //json字符串转换
    public string JsonStr(string Input_Str)
    {
        string List_Str;
        List_Str = Input_Str;
        //List_Str = List_Str.Replace("!", "！");
        List_Str = List_Str.Replace("'", "&acute;");
        List_Str = List_Str.Replace("\"", "&quot;");
        List_Str = List_Str.Replace("\\", "＼");
        List_Str = List_Str.Replace(",", "&sbquo;");
        List_Str = List_Str.Replace("\r\n", " ");
        return List_Str;
    }

    /// <summary>
    /// 销售量(额)排名
    /// </summary>
    /// <returns></returns>
    public string SalesAmountRanked()
    {
        string orders_status = tools.CheckStr(Request["orders_status"]);

        DateTime startDate;
        if (!DateTime.TryParse(tools.CheckStr(Request["startdate"]), out startDate))
        {
            startDate = DateTime.Today.AddDays(-15);
        }
        DateTime endDate;
        if (!DateTime.TryParse(tools.CheckStr(Request["endDate"]), out endDate))
        {
            endDate = DateTime.Today;
        }

        int PageSize = tools.CheckInt(Request["rows"]);
        int CurrentPage = tools.CheckInt(Request["page"]);

        int RecordCount = 0;
        int PageCount = 0;

        string SqlField = "G.Orders_Goods_Product_ID, SUM(Orders_Goods_Amount) AS saleCount, SUM(Orders_Goods_Product_Price * Orders_Goods_Amount) AS saleSum";
        string SqlTable = "Orders_Goods AS G INNER JOIN Orders AS O ON G.Orders_Goods_OrdersID = O.Orders_ID";
        string SqlOrder = "ORDER BY " + tools.CheckStr(Request["sidx"]) + " " + tools.CheckStr(Request["sord"]);

        string SqlParam = " WHERE O.Orders_Site = 'CN' AND Orders_SupplierID = " + tools.NullInt(Session["supplier_id"]);
        SqlParam += " AND DATEDIFF(d, O.Orders_Addtime, '" + startDate + "') <= 0 AND DATEDIFF(d, O.Orders_Addtime, '" + endDate + "') >= 0";

        if (orders_status == "confirm") { SqlParam += " AND (Orders_Status > 0 AND Orders_Status < 3)"; }
        if (orders_status == "success") { SqlParam += " AND Orders_Status = 2"; }

        string SqlCount = "SELECT COUNT(DISTINCT Orders_Goods_Product_ID) FROM " + SqlTable + " " + SqlParam;

        DataTable DtList = null;
        try
        {
            RecordCount = (int)DBHelper.ExecuteScalar(SqlCount);
            PageCount = tools.CalculatePages(RecordCount, PageSize);
            CurrentPage = tools.DeterminePage(CurrentPage, PageCount);

            string SqlList = "SELECT * FROM (SELECT " + SqlField + " , ROW_NUMBER() OVER (" + SqlOrder + ") AS RowNumber FROM " + SqlTable + " " + SqlParam + " GROUP BY (G.Orders_Goods_Product_ID) ) AS T WHERE RowNumber >  " + (CurrentPage - 1) * PageSize + " AND RowNumber < " + ((CurrentPage * PageSize) + 1);

            int Product_ID, saleCount, Supplier_ID;
            string Product_Name, Product_Code, Product_Spec, Supplier_Name, Cate_Name;
            double saleSum;

            DtList = DBHelper.Query(SqlList);

            if (DtList.Rows.Count > 0)
            {
                ProductInfo pEntity;
                SupplierInfo sEntity;
                CategoryInfo cEntity;

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
                        //Product_Code = pEntity.Product_Code;
                        Product_Spec = pEntity.Product_Spec;
                        Supplier_ID = pEntity.Product_SupplierID;
                        sEntity = supplier.GetSupplierByID(Supplier_ID);
                        if (sEntity != null)
                        {
                            Supplier_Name = sEntity.Supplier_CompanyName;
                        }
                        else
                        {
                            Supplier_Name = Supplier_ID.ToString();
                        }
                        sEntity = null;

                        cEntity = category.GetCategoryByID(pEntity.Product_CateID, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
                        if (cEntity != null)
                        {
                            Cate_Name = cEntity.Cate_Name;
                        }
                        else
                        {
                            Cate_Name = pEntity.Product_CateID.ToString();
                        }
                    }
                    else
                    {
                        Product_Name = "";
                        //Product_Code = "";
                        Product_Spec = "";
                        Supplier_Name = string.Empty;
                        Cate_Name = string.Empty;
                    }
                    pEntity = null;

                    jsonBuilder.Append("{\"id\":" + Product_ID + ",\"cell\":[");

                    //各字段
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(Product_ID);
                    jsonBuilder.Append("\",");

                    //jsonBuilder.Append("\"");
                    //jsonBuilder.Append(JsonStr(Product_Code));
                    //jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(JsonStr(Product_Name));
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(JsonStr(Supplier_Name));
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(JsonStr(Cate_Name));
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(JsonStr(Product_Spec));
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(saleCount);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(pub.FormatCurrency(saleSum));
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

}