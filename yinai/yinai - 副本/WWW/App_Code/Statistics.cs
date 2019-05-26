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
public class Statistics
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

    public Statistics()
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
    public class Series
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

        public int yAxisIndex
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

    public class axisLabel
    {
        public int interval { get; set; }
    }

    public class xAxis
    {
        public string type
        {
            get;
            set;
        }

        public axisLabel axisLabel { get; set; }

        /// <summary>
        /// xAxis序列组的数据为数据类型数组
        /// </summary>
        public List<object> data
        {
            get;
            set;
        }
    }

    #endregion

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

    #region 采购商统计

    public void Member_Account_Detail()
    {
        DateTime startDate;
        if (!DateTime.TryParse(tools.CheckStr(Request["startDate"]), out startDate))
        {
            startDate = DateTime.Today.AddDays(-15);
        }
        DateTime endDate;
        if (!DateTime.TryParse(tools.CheckStr(Request["endDate"]), out endDate))
        {
            endDate = DateTime.Today;
        }


        //考虑到图表的category是字符串数组 这里定义一个string的List
        List<string> categoryList = new List<string>();
        //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类
        List<Series> seriesList = new List<Series>();
        //考虑到Echarts图表需要设置legend内的data数组为series的name集合这里需要定义一个legend数组
        List<string> legendList = new List<string>();

        string SqlList = "";
        SqlDataReader RdrList = null;

        SqlList = "select min(CONVERT(varchar(100), Orders_Payment_Addtime, 23)) as orders_date,SUM(Orders_Payment_Amount) as order_amount from  Orders_Payment";

        SqlList += " WHERE DATEDIFF(d, Orders_Payment_Addtime, '" + startDate + "') <= 0";
        SqlList += " AND DATEDIFF(d, Orders_Payment_Addtime, '" + endDate + "') >= 0";


        SqlList += " AND Orders_Payment_OrdersID in (select Orders_ID from Orders where Orders_BuyerID=" + tools.NullInt(Session["member_id"]) + ")  group by datepart(DY,Orders_Payment_Addtime)";

        RdrList = DBHelper.ExecuteReader(SqlList);

        try
        {
            //定义一个Series对象
            Series seriesObj = new Series();
            seriesObj.id = 1;
            seriesObj.name = "销售额";
            seriesObj.type = "line"; //线性图呈现
            seriesObj.data = new List<object>(); //先初始化 不初始化后面直直接data.Add(x)会报错

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
    /// 采购商采购额统计
    /// </summary>
    public void Member_PurchaseAmount_Statistics()
    {
        DateTime startDate;
        if (!DateTime.TryParse(tools.CheckStr(Request["startDate"]), out startDate))
        {
            startDate = DateTime.Today.AddDays(-15);
        }
        DateTime endDate;
        if (!DateTime.TryParse(tools.CheckStr(Request["endDate"]), out endDate))
        {
            endDate = DateTime.Today;
        }


        //考虑到图表的category是字符串数组 这里定义一个string的List
        List<string> categoryList = new List<string>();
        //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类
        List<Series> seriesList = new List<Series>();
        //考虑到Echarts图表需要设置legend内的data数组为series的name集合这里需要定义一个legend数组
        List<string> legendList = new List<string>();

        SqlDataReader RdrList = null;

        int Cate_ID = tools.CheckInt(Request["product_cate"]);

        string SqlList = "select min(CONVERT(varchar(100), Orders_Addtime, 23)) as orders_date,SUM(Orders_Total_Price) as purchaseAmount from Orders";

        SqlList += " WHERE DATEDIFF(d, Orders_Addtime, '" + startDate + "') <= 0";
        SqlList += " AND DATEDIFF(d, Orders_Addtime, '" + endDate + "') >= 0";

        SqlList += "  AND Orders_BuyerID=" + tools.NullInt(Session["member_id"]) + " group by datepart(DY,Orders_Addtime)";



        RdrList = DBHelper.ExecuteReader(SqlList);

        try
        {
            //定义一个Series对象
            Series seriesObj = new Series();
            seriesObj.id = 1;
            seriesObj.name = "采购额统计";
            seriesObj.type = "line"; //线性图呈现
            seriesObj.data = new List<object>(); //先初始化 不初始化后面直直接data.Add(x)会报错

            if (RdrList.HasRows)
            {
                while (RdrList.Read())
                {
                    //加入category刻度数组
                    categoryList.Add(tools.NullStr(RdrList["orders_date"]));

                    //加入数据值series序列数组 这里提供为了效果只提供一组series数据好了                
                    seriesObj.data.Add(tools.NullInt(RdrList["purchaseAmount"])); //数据依次递增
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

    public void Member_OrdersCount_Statistics()
    {
        DateTime startDate;
        if (!DateTime.TryParse(tools.CheckStr(Request["startDate"]), out startDate))
        {
            startDate = DateTime.Today.AddDays(-15);
        }
        DateTime endDate;
        if (!DateTime.TryParse(tools.CheckStr(Request["endDate"]), out endDate))
        {
            endDate = DateTime.Today;
        }

        //考虑到图表的category是字符串数组 这里定义一个string的List
        List<string> categoryList = new List<string>();
        //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类
        List<Series> seriesList = new List<Series>();
        //考虑到Echarts图表需要设置legend内的data数组为series的name集合这里需要定义一个legend数组
        List<string> legendList = new List<string>();

        string strSql = "";
        SqlDataReader RdrList = null;

        strSql = "select min(CONVERT(varchar(100), Orders_Addtime, 23)) as orders_date,COUNT(Orders_ID) as order_count from Orders";

        strSql += " WHERE DATEDIFF(d, Orders_Addtime, '" + startDate + "') <= 0";
        strSql += " AND DATEDIFF(d, Orders_Addtime, '" + endDate + "') >= 0";

        strSql += "  AND Orders_BuyerID=" + tools.NullInt(Session["member_id"]) + " group by datepart(DY,Orders_Addtime)";

        RdrList = DBHelper.ExecuteReader(strSql);
        try
        {
            //定义一个Series对象
            Series seriesObj = new Series();
            seriesObj.id = 1;
            seriesObj.name = "订单量统计";
            seriesObj.type = "line"; //线性图呈现
            seriesObj.data = new List<object>(); //先初始化 不初始化后面直直接data.Add(x)会报错


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
    /// 采购量排名统计
    /// </summary>
    public string Member_Purchases_Statistics()
    {
        StringBuilder strHTML = new StringBuilder();

        string orders_status = tools.CheckStr(Request["purchases_orderStatus"]);

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

        int i = 0;
        int PageSize = 10;
        int CurrentPage = tools.CheckInt(Request["page"]);
        if (CurrentPage <= 0)
        {
            CurrentPage = 1;
        }


        int RecordCount = 0;
        int PageCount = 0;

        string SqlField = "G.Orders_Goods_Product_ID, SUM(Orders_Goods_Amount) AS purchasesCount, SUM(Orders_Goods_Product_Price * Orders_Goods_Amount) AS purchasesSum";

        string SqlTable = "Orders_Goods AS G INNER JOIN Orders AS O ON G.Orders_Goods_OrdersID = O.Orders_ID";

        string SqlOrder = "ORDER BY SUM(Orders_Goods_Amount) desc  ";

        string SqlParam = " WHERE O.Orders_Site = 'CN' and O.Orders_BuyerID=" + tools.NullInt(Session["member_id"]);

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

            int Product_ID, purchasesCount, Product_TypeID, Product_CateID, Supplier_ID;
            string Product_Name, Product_Code, Product_SubName, Product_Spec, Cate_Name = "", Supplier_Name;
            double purchasesSum;

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
                    purchasesCount = tools.NullInt(DrList["purchasesCount"]);
                    purchasesSum = tools.NullDbl(DrList["purchasesSum"]);

                    pEntity = product.GetProductByID(Product_ID);
                    if (pEntity != null)
                    {
                        Product_Name = pEntity.Product_Name;
                        Product_Code = pEntity.Product_Code;
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
                        Product_Code = "";
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

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(JsonStr(Product_Code));
                    jsonBuilder.Append("\",");

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
                    jsonBuilder.Append(purchasesCount);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(pub.FormatCurrency(purchasesSum));
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

    #endregion

    #region  首页今日热搜

    public void TradeIndex_Charts()
    { 
        DateTime startDate;
        if (!DateTime.TryParse(tools.CheckStr(Request["startDate"]), out startDate))
        {
            startDate = DateTime.Today.AddDays(-15);
        }
        DateTime endDate;
        if (!DateTime.TryParse(tools.CheckStr(Request["endDate"]), out endDate))
        {
            endDate = DateTime.Today;
        }


        //图标X轴
        List<xAxis> xAxisList = new List<xAxis>();

        //考虑到图表的series数据为一个对象数组 这里额外定义一个series的类
        List<Series> seriesList = new List<Series>();

        //考虑到Echarts图表需要设置legend内的data数组为series的name集合这里需要定义一个legend数组
        List<string> legendList = new List<string>();

        Random random = new Random();

        string[] categoryArry = new string[] { "定价黄金", "计价黄金", "钻石", "铂金", "K金", "宝石", "珍珠", "银饰", "翡翠玉石珠" };
        
        try
        {
            xAxis xAxis = new xAxis();
            xAxis.axisLabel = new axisLabel();
            xAxis.type = "category";
            xAxis.axisLabel.interval = 0;
            xAxis.data = new List<object>();

            if (categoryArry != null)
            {
                foreach (string itesms in categoryArry)
                {
                    xAxis.data.Add(itesms);
                }
            }
            xAxisList.Add(xAxis);
            xAxisList.Add(GetOrdersAmountxAxis());

            //将sereis对象压入sereis数组列表内
            seriesList.Add(GetKeywordSeries());
            seriesList.Add(GetOrdersAmountSeries());
            

        }
        catch (Exception ex)
        {
            throw ex;
        }

        //最后调用相关函数将List转换为Json
        //因为我们需要返回category和series、legend多个对象 这里我们自己在new一个新的对象来封装这两个对象
        var newObj = new
        {
            xAxis = xAxisList,
            series = seriesList
        };
        string jsonstr = JsonHelper.ObjectToJSON(newObj);
        //Response返回新对象的json数据
        Response.Write(JsonHelper.ObjectToJSON(newObj));
        Response.End();
    }


    public xAxis GetOrdersAmountxAxis()
    {
        xAxis xAxis = new xAxis();
        xAxis.axisLabel = new axisLabel();
        xAxis.type = "category";
        xAxis.data = new List<object>();

        Random random = new Random();
        DateTime startDate = DateTime.Today.AddDays(-7);
        DateTime endDate = DateTime.Today;


        int diffDate = tools.NullInt((endDate - startDate).TotalDays);

        string SqlList = "";
        SqlDataReader RdrList = null;

        SqlList = " select min(CONVERT(varchar(5), Orders_Addtime, 110)) as orders_date,SUM(Orders_Total_AllPrice) as order_amount from Orders ";

        SqlList += " WHERE DATEDIFF(d, Orders_Addtime, '" + startDate + "') <= 0";
        SqlList += " AND DATEDIFF(d, Orders_Addtime, '" + endDate + "') >= 0  and Orders_PaymentStatus=1 and Orders_Status=1";

        SqlList += " group by datepart(DY,Orders_Addtime)";

        try
        {
            RdrList = DBHelper.ExecuteReader(SqlList);

            if (RdrList.HasRows)
            {
                while (RdrList.Read())
                {
                    diffDate = diffDate - 1;
                    xAxis.data.Add(tools.NullStr(RdrList["orders_date"])); //数据依次递增
                }
            }
            else
            {
                double upper = (endDate - startDate).TotalDays;
                for (int i = 1; i <= upper; i++)
                {     
                    xAxis.data.Add(startDate.AddDays(i).ToString("MM-dd"));
                }
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

        return xAxis;
    }

    public Series GetOrdersAmountSeries()
    {
        Series seriesObj = new Series();
        seriesObj.id = 2;
        seriesObj.name = "销售额";
        seriesObj.type = "bar"; //线性图呈现
        seriesObj.data = new List<object>(); //先初始化 不初始化后面直直接data.Add(x)会报错

        Random random = new Random();
        DateTime startDate= DateTime.Today.AddDays(-7);
        DateTime endDate = DateTime.Today;        

        string SqlList = "";
        SqlDataReader RdrList = null;

        SqlList = " select min(CONVERT(varchar(100), Orders_Addtime, 23)) as orders_date,SUM(Orders_Total_AllPrice) as order_amount from Orders ";

        SqlList += " WHERE DATEDIFF(d, Orders_Addtime, '" + startDate + "') <= 0";
        SqlList += " AND DATEDIFF(d, Orders_Addtime, '" + endDate + "') >= 0  and Orders_PaymentStatus=1 and Orders_Status=1";

        SqlList += " group by datepart(DY,Orders_Addtime)";

        try
        {
            RdrList = DBHelper.ExecuteReader(SqlList);

            if (RdrList.HasRows)
            {
                while (RdrList.Read())
                {
                    seriesObj.data.Add((tools.NullDbl(RdrList["order_amount"])/10000)*5); //数据依次递增
                }
            }
            else
            {
                double upper = (endDate - startDate).TotalDays;
                for (int i = 1; i <= upper; i++)
                {
                    //加入数据值series序列数组 这里提供为了效果只提供一组series数据好了                
                    seriesObj.data.Add(random.Next(10)*5); //数据依次递增
                }
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

        return seriesObj;
    }

    public Series GetKeywordSeries()
    {
        Series seriesObj = new Series();
        seriesObj.id = 1;
        seriesObj.name = "热搜指数";
        seriesObj.type = "line"; //线性图呈现
        seriesObj.yAxisIndex = 1;
        seriesObj.data = new List<object>(); //先初始化 不初始化后面直直接data.Add(x)会报错

        int i = 9;

        Random random = new Random();
        DateTime startDate= DateTime.Today.AddDays(-7);
        DateTime endDate = DateTime.Today;

        string[] categoryArry = new string[] { "定价黄金", "计价黄金", "钻石", "铂金", "K金", "宝石", "珍珠", "银饰", "翡翠玉石珠" };

        string SqlList = "";
        SqlDataReader RdrList = null;

        SqlList = "select keyword,COUNT(ID) as keyword_counts from Keywords_Ranking";

        SqlList += " WHERE DATEDIFF(d, addtime, '" + startDate + "') <= 0";
        SqlList += " AND DATEDIFF(d, addtime, '" + endDate + "') >= 0  and Keyword in ('定价黄金','计价黄金','钻石','铂金','K金','宝石','珍珠','银饰','翡翠玉石珠') and Type=0";

        SqlList += " group by keyword";

        try
        {
            RdrList = DBHelper.ExecuteReader(SqlList);

            if (RdrList.HasRows)
            {
                while (RdrList.Read())
                {
                    i = i - 1;
                    seriesObj.data.Add(tools.NullDbl(RdrList["keyword_counts"]) * 50);
                }

                for (int j = 1; j <= i; j++)
                {
                    seriesObj.data.Add(random.Next(8) * 50);
                }
            }
            else
            {
                foreach (string items in categoryArry)
                {
                    seriesObj.data.Add(random.Next(50) * 50); //数据依次递增
                }
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

        return seriesObj;
    }

    #endregion

    #region 搜索排行统计

    public void TradeIndex_Keyword_Ranking(int type)
    {
        StringBuilder strHTML = new StringBuilder();

        DataTable DtList = null;
        int i = 0;
        string strSql = " select top 10 Keyword,count(Keyword) as allCount from Keywords_Ranking where Type=" + type + " and Site='CN' group by Keyword order by allCount desc ";

        try
        {
            DtList = DBHelper.Query(strSql);

            if (DtList.Rows.Count > 0)
            {
                string Keyword = string.Empty;
                int count = 0;

                strHTML.Append("<table width=\"220\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                strHTML.Append("<tr>");
                strHTML.Append("<td class=\"name\">排名</td>");
                strHTML.Append("<td class=\"name\">关键词</td>");
                strHTML.Append("<td class=\"name\">热度</td>");
                strHTML.Append("</tr>");

                foreach (DataRow DrList in DtList.Rows)
                {
                    i++;
                    Keyword = tools.NullStr(DrList["Keyword"]);
                    count = tools.NullInt(DrList["allCount"]);

                    strHTML.Append("<tr>");
                    strHTML.Append("<td><i class=\"bg\">" + i + "</i></td>");
                    if (type == 1)
                    {
                        strHTML.Append("<td><a href=\"/shop/search.htm?keyword="+Keyword+"\">" + tools.NullStr(tools.CutStr(Keyword, 9)) + "</a></td>");
                    }
                    else
                    {
                        strHTML.Append("<td><a href=\"/product/search.htm?keyword=" + Keyword + "\">" + tools.NullStr(tools.CutStr(Keyword, 9)) + "</a></td>");
                    }
                    strHTML.Append("<td>" + count + "</td>");
                    strHTML.Append("</tr>");
                }
                strHTML.Append("</table>");
            }
        }
        catch (Exception ex)
        {
            Response.Write(" ");
        }
        finally
        {
            if (DtList != null)
            {
                DtList.Dispose();
                DtList = null;
            }
        }
        Response.Write(strHTML.ToString());
    }

    #endregion

    public double GetTradeAmount()
    {
        double tradeAmount = 0;

        string SqlList = "SELECT SUM(Orders_Total_AllPrice) FROM Orders WHERE Orders_Site = 'CN'";

        SqlList += " and Orders_PaymentStatus=1"; 

        try
        {
            tradeAmount= tools.NullDbl(DBHelper.ExecuteScalar(SqlList));
        }
        catch (Exception ex)
        {
            tradeAmount = 0;
        }

        return tradeAmount;
    }


    public int GetSupplierOrdersCounts()
    {
        string SqlList = "SELECT count(Orders_ID) FROM Orders WHERE Orders_Site = 'CN'";
        try
        {
            return tools.NullInt(DBHelper.ExecuteScalar(SqlList));
        }
        catch (Exception ex)
        {
            return  0;
        }
    }

}