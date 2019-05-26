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
using System.Collections.Generic;

using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.B2C.BLL.Product;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;

/// <summary>
///SCMPurchasing 的摘要说明
/// </summary>
public class SCMPurchasing
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private ISQLHelper DBHelper;
    private SCMDepot depot;
    private SCMSupplier supplier;
    private IProduct PROBLL;
    private IProductType PROTYPEBLL;
    private IBrand BRANDBLL;

    public SCMPurchasing()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        DBHelper = SQLHelperFactory.CreateSQLHelper();

        depot = new SCMDepot();
        supplier = new SCMSupplier();

        PROBLL = ProductFactory.CreateProduct();
        PROTYPEBLL = ProductTypeFactory.CreateProductType();
        BRANDBLL = BrandFactory.CreateBrand();
    }

    public void AddPurchasing()
    {
        int Purchasing_ID = tools.CheckInt(Request.Form["Purchasing_ID"]);
        int Purchasing_Type = tools.CheckInt(Request.Form["Purchasing_Type"]);
        int Purchasing_DepotID = tools.CheckInt(Request.Form["Purchasing_DepotID"]);
        int Purchasing_SupplierID = tools.CheckInt(Request.Form["Purchasing_SupplierID"]);
        string Purchasing_ProductCode = tools.CheckStr(Request.Form["Purchasing_ProductCode"]);
        double Purchasing_Price = tools.CheckFloat(Request.Form["Purchasing_Price"]);
        int Purchasing_Amount = tools.CheckInt(Request.Form["Purchasing_Amount"]);
        double Purchasing_TotalPrice = tools.CheckFloat(Request.Form["Purchasing_TotalPrice"]);
        string Purchasing_BatchNumber = tools.CheckStr(Request.Form["Purchasing_BatchNumber"]);
        string Purchasing_Operator = Session["User_Name"].ToString();
        int Purchasing_Checkout = tools.CheckInt(Request.Form["Purchasing_Checkout"]);
        int Purchasing_IsNoStock = 0;
        DateTime Purchasing_Tradetime = Public.ChangeToDate(tools.CheckStr(Request.Form["Purchasing_Tradetime"]));
        string Purchasing_Note = tools.CheckStr(Request.Form["Purchasing_Note"]);

        if (Purchasing_ProductCode.Length == 0) { Public.Msg("error", "错误信息", "请填写商品编码", false, "{back}"); return; }

        if (Purchasing_Type == 2 || Purchasing_Type == 4) {
            Purchasing_Amount = 0 - Purchasing_Amount;
        }

        ProductInfo productinfo = PROBLL.GetProductByCode(Purchasing_ProductCode, Public.GetCurrentSite() ,Public.GetUserPrivilege());
        if (productinfo != null)
        {
            
            Purchasing_IsNoStock = productinfo.Product_IsNoStock;
            if (productinfo.Product_SupplierID > 0)
            {
                productinfo = null;
            }
        }
        if (productinfo == null)
        {
            Public.Msg("error", "错误信息", "无效的商品编码", false, "{back}"); return; 
        }
        string SqlAdd = "INSERT INTO SCM_Purchasing (" +
            "Purchasing_Type, Purchasing_DepotID, Purchasing_SupplierID, Purchasing_ProductCode, Purchasing_Price, Purchasing_Amount" +
            ", Purchasing_TotalPrice, Purchasing_BatchNumber, Purchasing_Operator, Purchasing_Checkout,Purchasing_IsNoStock, Purchasing_Tradetime, Purchasing_Note) " +
            "VALUES (" + Purchasing_Type +
            ", " + Purchasing_DepotID +
            ", " + Purchasing_SupplierID +
            ", '" + Purchasing_ProductCode + "'" +
            ", " + Purchasing_Price +
            ", " + Purchasing_Amount +
            ", " + Purchasing_TotalPrice +
            ", '" + Purchasing_BatchNumber + "'" +
            ", '" + Purchasing_Operator + "'" +
            ", " + Purchasing_Checkout + "" +
            ", " + Purchasing_IsNoStock + "" +
            ", '" + Purchasing_Tradetime + "'" +
            ", '" + Purchasing_Note + "')";
        try { 
            DBHelper.ExecuteNonQuery(SqlAdd);

            //更新商品库存
            PROBLL.UpdateProductStock(Purchasing_ProductCode, Purchasing_Amount, Purchasing_Amount); 
        }
        catch (Exception ex) { throw ex; } 
        Public.Msg("positive", "操作成功", "操作成功", true, "purchasing_list.aspx?Purchasing_Type=" + Purchasing_Type);
    }

    public void EditPurchasing()
    {
        int Purchasing_ID = tools.CheckInt(Request.Form["Purchasing_ID"]);
        int Purchasing_Type = tools.CheckInt(Request.Form["Purchasing_Type"]);
        int Purchasing_DepotID = tools.CheckInt(Request.Form["Purchasing_DepotID"]);
        int Purchasing_SupplierID = tools.CheckInt(Request.Form["Purchasing_SupplierID"]);
        string Purchasing_ProductCode = tools.CheckStr(Request.Form["Purchasing_ProductCode"]);
        //double Purchasing_Price = tools.CheckFloat(Request.Form["Purchasing_Price"]);
        //int Purchasing_Amount = tools.CheckInt(Request.Form["Purchasing_Amount"]);
        //double Purchasing_TotalPrice = tools.CheckFloat(Request.Form["Purchasing_TotalPrice"]);
        //string Purchasing_BatchNumber = tools.CheckStr(Request.Form["Purchasing_BatchNumber"]);
        //string Purchasing_Operator = Session["User_Name"].ToString();
        //int Purchasing_Checkout = tools.CheckInt(Request.Form["Purchasing_Checkout"]);
        //DateTime Purchasing_Tradetime = Public.ChangeToDate(tools.CheckStr(Request.Form["Purchasing_Tradetime"]));
        string Purchasing_Note = tools.CheckStr(Request.Form["Purchasing_Note"]);

        //if (Purchasing_ProductCode.Length == 0) { Public.Msg("error", "错误信息", "请填写商品编码", false, "{back}"); return; }

        //if (Purchasing_Type == 2 || Purchasing_Type == 4) {
        //    Purchasing_Amount = 0 - Purchasing_Amount;
        //}

        string SqlAdd = "UPDATE SCM_Purchasing SET";
        //SqlAdd += " Purchasing_Type = " + Purchasing_Type;
        //SqlAdd += ", Purchasing_DepotID = " + Purchasing_DepotID;
        //SqlAdd += ", Purchasing_SupplierID = " + Purchasing_SupplierID;
        //SqlAdd += ", Purchasing_ProductCode = '" + Purchasing_ProductCode + "'";
        //SqlAdd += ", Purchasing_Price = " + Purchasing_Price;
        //SqlAdd += ", Purchasing_Amount = " + Purchasing_Amount;
        //SqlAdd += ", Purchasing_TotalPrice = " + Purchasing_TotalPrice;
        //SqlAdd += ", Purchasing_BatchNumber = '" + Purchasing_BatchNumber + "'";
        //SqlAdd += ", Purchasing_Operator = '" + Purchasing_Operator + "'";
        //SqlAdd += ", Purchasing_Checkout = " + Purchasing_Checkout;
        //SqlAdd += ", Purchasing_Tradetime ='" + Purchasing_Tradetime + "'";
        SqlAdd += " Purchasing_Note ='" + Purchasing_Note + "'";
        SqlAdd += " WHERE Purchasing_ID =" + Purchasing_ID;

        try {
            DBHelper.ExecuteNonQuery(SqlAdd);

            //更新商品库存
            //PROBLL.UpdateProductStock(Purchasing_ProductCode, Purchasing_Amount, Purchasing_Amount);
        }
        catch (Exception ex) { throw ex; }
        Public.Msg("positive", "操作成功", "操作成功", true, "purchasing_list.aspx?Purchasing_Type=" + Purchasing_Type);
    }

    public void DelPurchasing()
    {
        int Purchasing_ID = tools.CheckInt(Request.QueryString["Purchasing_ID"]);
        int Purchasing_Type = tools.CheckInt(Request.QueryString["Purchasing_Type"]);
        try {
            DBHelper.ExecuteNonQuery("DELETE FROM SCM_Purchasing WHERE Purchasing_ID = " + Purchasing_ID);
        }
        catch (Exception ex) {
            throw ex;
        }
        Public.Msg("positive", "操作成功", "操作成功", true, "purchasing_list.aspx?Purchasing_Type=" + Purchasing_Type);
    }

    public string GetPurchasings()
    {
        int Purchasing_ID, Purchasing_Type, Purchasing_DepotID, Purchasing_SupplierID, Purchasing_Amount, Purchasing_Checkout;
        string Purchasing_ProductCode, Purchasing_BatchNumber, Purchasing_Operator, Product_IDArry, Purchasing_Tradetime;
        double Purchasing_Price, Purchasing_TotalPrice;

        int PageSize = tools.CheckInt(Request["rows"]);
        int CurrentPage = tools.CheckInt(Request["page"]);
        int RecordCount = 0;
        int PageCount = 0;
        Product_IDArry = "'0'";
        Purchasing_Type = tools.CheckInt(Request.QueryString["Purchasing_Type"]);
        Purchasing_ProductCode = tools.CheckStr(Request.QueryString["Product_Code"]);

        string SqlField = "Purchasing_ID, Purchasing_DepotID, Purchasing_SupplierID, Purchasing_ProductCode, Purchasing_Price, Purchasing_Amount, Purchasing_TotalPrice, Purchasing_BatchNumber, Purchasing_Operator, Purchasing_Checkout, Purchasing_Tradetime, Product_Name, Product_Spec, Product_Maker";
        string SqlTable = "SCM_Purchasing LEFT JOIN Product_Basic ON SCM_Purchasing.Purchasing_ProductCode = Product_Basic.Product_Code";
        string SqlOrder = "ORDER BY " + tools.CheckStr(Request["sidx"]) + " " + tools.CheckStr(Request["sord"]);
        string SqlParam = " ";
        if (Purchasing_Type < 3)
        {
            SqlParam = SqlParam + "  WHERE SCM_Purchasing.Purchasing_IsNoStock = 0 and SCM_Purchasing.Purchasing_Type = " + Purchasing_Type;
        }
        else
        {
            SqlParam = SqlParam + "  WHERE SCM_Purchasing.Purchasing_Type = " + Purchasing_Type;
        }

        if (Purchasing_ProductCode.Length > 0)
        {
            SqlParam += " AND (Purchasing_ProductCode LIKE '%" + Purchasing_ProductCode + "%' OR Product_Basic.Product_Name LIKE '%" + Purchasing_ProductCode + "%' OR Product_Basic.Product_Maker LIKE '%" + Purchasing_ProductCode + "%')";
        }

        string SqlCount = "SELECT COUNT(SCM_Purchasing.Purchasing_ID) FROM " + SqlTable + " " + SqlParam;

        SqlDataReader RdrList = null;

        try
        {
            RecordCount = (int)DBHelper.ExecuteScalar(SqlCount);
            PageCount = tools.CalculatePages(RecordCount, PageSize);
            CurrentPage = tools.DeterminePage(CurrentPage, PageCount);

            string SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + CurrentPage + ",\"total\":" + PageCount + ",\"records\":" + RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            RdrList = DBHelper.ExecuteReader(SqlList);

            while (RdrList.Read())
            {
                Purchasing_ID = tools.NullInt(RdrList["Purchasing_ID"]);
                Purchasing_DepotID = tools.NullInt(RdrList["Purchasing_DepotID"]);
                Purchasing_SupplierID = tools.NullInt(RdrList["Purchasing_SupplierID"]);
                Purchasing_ProductCode = tools.NullStr(RdrList["Purchasing_ProductCode"]);
                Purchasing_Price = tools.NullDbl(RdrList["Purchasing_Price"]);
                Purchasing_Amount = tools.NullInt(RdrList["Purchasing_Amount"]);
                Purchasing_TotalPrice = tools.NullDbl(RdrList["Purchasing_TotalPrice"]);
                Purchasing_BatchNumber = tools.NullStr(RdrList["Purchasing_BatchNumber"]);
                Purchasing_Operator = tools.NullStr(RdrList["Purchasing_Operator"]);
                Purchasing_Checkout = tools.NullInt(RdrList["Purchasing_Checkout"]);
                Purchasing_Tradetime = tools.NullDate(RdrList["Purchasing_Tradetime"]).ToShortDateString();
  

                jsonBuilder.Append("{\"Purchasing_ID\":" + Purchasing_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(Purchasing_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(depot.GetNameByID(Purchasing_DepotID)));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(supplier.GetNameByID(Purchasing_SupplierID)));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(Purchasing_ProductCode));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(tools.NullStr(RdrList["Product_Name"])));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(tools.NullStr(RdrList["Product_Spec"])));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(tools.NullStr(RdrList["Product_Maker"])));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.DisplayCurrency(Purchasing_Price));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Purchasing_Amount);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.DisplayCurrency(Purchasing_TotalPrice));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(Purchasing_BatchNumber));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Purchasing_Tradetime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Purchasing_Operator);
                jsonBuilder.Append("\",");

                if (Purchasing_Checkout == 1) { jsonBuilder.Append("\"已结款\","); }
                else { jsonBuilder.Append("\"未结款\","); }

                jsonBuilder.Append("\"");
                
                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"查看\\\"> <a href=\\\"purchasing_view.aspx?purchasing_id=" + Purchasing_ID + "\\\" title=\\\"查看\\\">查看</a>");
                
                if (Public.CheckPrivilege("a56c96f7-fb31-4944-a248-45a8ad3c4398/a133f0cd-9a5e-4d02-ad94-9e0c0424d66d/87a8726b-5113-46ec-845c-6bd377935196/bfc31928-a7f1-45e0-bd3e-88a4268593ce"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"purchasing_edit.aspx?purchasing_id=" + Purchasing_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                //if (Public.CheckPrivilege("6b56546f-b64b-4365-85dd-e053f0a51630/598449ed-90f9-4211-bdd5-d471d8fda8a0/692dd604-e428-40cf-9202-43d333245b1d/7c806dd5-9a9a-4e83-bdec-10b328491c1a"))
                //{
                //    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('purchasing_do.aspx?action=move&purchasing_type=" + Purchasing_Type + "&purchasing_id=" + Purchasing_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                //}
                jsonBuilder.Append("\"");

                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
            return "";
        }
        finally
        {
            if (RdrList != null)
            {
                RdrList.Close();
                RdrList = null;
            }
        }
    }

    public void GetPurchasings_Export()
    {
        int Purchasing_ID, Purchasing_Type, Purchasing_DepotID, Purchasing_SupplierID, Purchasing_Amount, Purchasing_Checkout;
        string Purchasing_ProductCode, Purchasing_BatchNumber, Purchasing_Operator, Product_IDArry, Purchasing_Tradetime;
        double Purchasing_Price, Purchasing_TotalPrice;


        Product_IDArry = "'0'";
        Purchasing_Type = tools.CheckInt(Request.QueryString["Purchasing_Type"]);
        Purchasing_ProductCode = tools.CheckStr(Request.QueryString["Product_Code"]);
        int nostock = tools.CheckInt(Request.QueryString["nostock"]);
        ProductInfo productinfo = null;
        if (Purchasing_ProductCode.Length > 0)
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_Name", "like", Purchasing_ProductCode));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "ProductInfo.Product_Code", "like", Purchasing_ProductCode));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_Maker", "like", Purchasing_ProductCode));
            IList<ProductInfo> entitys = PROBLL.GetProducts(Query, Public.GetUserPrivilege());
            if (entitys != null)
            {
                foreach (ProductInfo entity in entitys)
                {
                    Product_IDArry = Product_IDArry + ",'" + entity.Product_Code + "'";
                }
            }
        }



        string SqlField = "Purchasing_ID, Purchasing_DepotID, Purchasing_SupplierID, Purchasing_ProductCode, Purchasing_Price, Purchasing_Amount, Purchasing_TotalPrice, Purchasing_BatchNumber, Purchasing_Operator, Purchasing_Checkout,Purchasing_Tradetime";
        string SqlTable = "SCM_Purchasing";
        string SqlOrder = "ORDER BY Purchasing_ID desc";
        string SqlParam = " WHERE  Purchasing_Type = " + Purchasing_Type;
        if (Purchasing_Type < 3)
        {
            if (nostock == 0)
            {
                SqlParam += " AND Purchasing_IsNoStock=0";
            }
            else
            {
                SqlParam += " AND Purchasing_IsNoStock=1";
            }
        }

        if (Purchasing_ProductCode.Length > 0)
            SqlParam += " AND Purchasing_ProductCode in (" + Product_IDArry + ")";

        string SqlCount = "SELECT COUNT(Purchasing_ID) FROM " + SqlTable + " " + SqlParam;

        SqlDataReader RdrList = null;

        try
        {


            DataTable dt = new DataTable("excel");
            DataRow dr = null;
            DataColumn column = null;

            string[] dtcol = { "编号", "所属仓库", "供应商", "商品编码", "商品名称", "规格", "产地", "价格", "数量","总价","批号","时间", "操作人", "结款状态" };
            foreach (string col in dtcol)
            {
                try { dt.Columns.Add(col); }
                catch { dt.Columns.Add(col + ","); }
            }
            dtcol = null;

            string SqlList = "Select " + SqlField + " From " + SqlTable + " " + SqlParam + " " + SqlOrder;


            RdrList = DBHelper.ExecuteReader(SqlList);

            while (RdrList.Read())
            {
                Purchasing_ID = tools.NullInt(RdrList["Purchasing_ID"]);
                Purchasing_DepotID = tools.NullInt(RdrList["Purchasing_DepotID"]);
                Purchasing_SupplierID = tools.NullInt(RdrList["Purchasing_SupplierID"]);
                Purchasing_ProductCode = tools.NullStr(RdrList["Purchasing_ProductCode"]);
                Purchasing_Price = tools.NullDbl(RdrList["Purchasing_Price"]);
                Purchasing_Amount = tools.NullInt(RdrList["Purchasing_Amount"]);
                Purchasing_TotalPrice = tools.NullDbl(RdrList["Purchasing_TotalPrice"]);
                Purchasing_BatchNumber = tools.NullStr(RdrList["Purchasing_BatchNumber"]);
                Purchasing_Operator = tools.NullStr(RdrList["Purchasing_Operator"]);
                Purchasing_Checkout = tools.NullInt(RdrList["Purchasing_Checkout"]);
                Purchasing_Tradetime = tools.NullDate(RdrList["Purchasing_Tradetime"]).ToShortDateString();
                productinfo = PROBLL.GetProductByCode(Purchasing_ProductCode, Public.GetCurrentSite(), Public.GetUserPrivilege());

                dr = dt.NewRow();
                dr[0]=Purchasing_ID;
                dr[1]=depot.GetNameByID(Purchasing_DepotID);
                dr[2] = supplier.GetNameByID(Purchasing_SupplierID);
                dr[3]=Purchasing_ProductCode;
                

                if (productinfo != null)
                {
                    dr[4]=productinfo.Product_Name;
                    dr[5]=productinfo.Product_Spec;
                    dr[6]=productinfo.Product_Maker;
                    
                }
                else
                {
                    dr[4] = "--";
                    dr[5] = "--";
                    dr[6] = "--";
                }

                dr[7] = Public.DisplayCurrency(Purchasing_Price);
                dr[8] = Purchasing_Amount;
                dr[9] = Public.DisplayCurrency(Purchasing_TotalPrice);
                dr[10] = Purchasing_BatchNumber;
                dr[11] = Purchasing_Tradetime;
                dr[12] = Purchasing_Operator;
                

                if (Purchasing_Checkout == 1) { dr[13] = "已结款"; }
                else { dr[13] = "未结款"; }

                dt.Rows.Add(dr);
                dr = null;
            }
            Public.toExcel(dt);
            
        }
        catch (Exception ex)
        {
            throw ex;

        }
        finally
        {
            if (RdrList != null)
            {
                RdrList.Close();
                RdrList = null;
            }
        }
    }

    public string GetNoStockPurchasings()
    {
        int Purchasing_ID, Purchasing_Type, Purchasing_DepotID, Purchasing_SupplierID, Purchasing_Amount, Purchasing_Checkout;
        string Purchasing_ProductCode, Purchasing_BatchNumber, Purchasing_Operator, Product_IDArry, Purchasing_Tradetime;
        double Purchasing_Price, Purchasing_TotalPrice;

        int PageSize = tools.CheckInt(Request["rows"]);
        int CurrentPage = tools.CheckInt(Request["page"]);
        int RecordCount = 0;
        int PageCount = 0;
        Product_IDArry = "'0'";
        Purchasing_Type = tools.CheckInt(Request.QueryString["Purchasing_Type"]);
        Purchasing_ProductCode = tools.CheckStr(Request.QueryString["Product_Code"]);

        string SqlField = "Purchasing_ID, Purchasing_DepotID, Purchasing_SupplierID, Purchasing_ProductCode, Purchasing_Price, Purchasing_Amount, Purchasing_TotalPrice, Purchasing_BatchNumber, Purchasing_Operator, Purchasing_Checkout, Purchasing_Tradetime, Product_Name, Product_Spec, Product_Maker";
        string SqlTable = "SCM_Purchasing LEFT JOIN Product_Basic ON SCM_Purchasing.Purchasing_ProductCode = Product_Basic.Product_Code";
        string SqlOrder = "ORDER BY " + tools.CheckStr(Request["sidx"]) + " " + tools.CheckStr(Request["sord"]);
        string SqlParam = " WHERE SCM_Purchasing.Purchasing_IsNoStock = 1 AND SCM_Purchasing.Purchasing_Type = " + Purchasing_Type;

        if (Purchasing_ProductCode.Length > 0)
        {
            SqlParam += " AND (Purchasing_ProductCode LIKE '%" + Purchasing_ProductCode + "%' OR Product_Basic.Product_Name LIKE '%" + Purchasing_ProductCode + "%' OR Product_Basic.Product_Maker LIKE '%" + Purchasing_ProductCode + "%')";
        }

        string SqlCount = "SELECT COUNT(SCM_Purchasing.Purchasing_ID) FROM " + SqlTable + " " + SqlParam;

        SqlDataReader RdrList = null;

        try
        {
            RecordCount = (int)DBHelper.ExecuteScalar(SqlCount);
            PageCount = tools.CalculatePages(RecordCount, PageSize);
            CurrentPage = tools.DeterminePage(CurrentPage, PageCount);

            string SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + CurrentPage + ",\"total\":" + PageCount + ",\"records\":" + RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            RdrList = DBHelper.ExecuteReader(SqlList);

            while (RdrList.Read())
            {
                Purchasing_ID = tools.NullInt(RdrList["Purchasing_ID"]);
                Purchasing_DepotID = tools.NullInt(RdrList["Purchasing_DepotID"]);
                Purchasing_SupplierID = tools.NullInt(RdrList["Purchasing_SupplierID"]);
                Purchasing_ProductCode = tools.NullStr(RdrList["Purchasing_ProductCode"]);
                Purchasing_Price = tools.NullDbl(RdrList["Purchasing_Price"]);
                Purchasing_Amount = tools.NullInt(RdrList["Purchasing_Amount"]);
                Purchasing_TotalPrice = tools.NullDbl(RdrList["Purchasing_TotalPrice"]);
                Purchasing_BatchNumber = tools.NullStr(RdrList["Purchasing_BatchNumber"]);
                Purchasing_Operator = tools.NullStr(RdrList["Purchasing_Operator"]);
                Purchasing_Checkout = tools.NullInt(RdrList["Purchasing_Checkout"]);
                Purchasing_Tradetime = tools.NullDate(RdrList["Purchasing_Tradetime"]).ToShortDateString();

                jsonBuilder.Append("{\"Purchasing_ID\":" + Purchasing_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(Purchasing_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(depot.GetNameByID(Purchasing_DepotID));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(supplier.GetNameByID(Purchasing_SupplierID));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Purchasing_ProductCode);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(tools.NullStr(RdrList["Product_Name"])));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(tools.NullStr(RdrList["Product_Spec"])));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(tools.NullStr(RdrList["Product_Maker"])));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.DisplayCurrency(Purchasing_Price));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Purchasing_Amount);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.DisplayCurrency(Purchasing_TotalPrice));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Purchasing_BatchNumber);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Purchasing_Tradetime);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Purchasing_Operator);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"--\",");

                jsonBuilder.Append("\"");

                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"查看\\\"> <a href=\\\"purchasing_view.aspx?purchasing_id=" + Purchasing_ID + "\\\" title=\\\"查看\\\">查看</a>");

                if (Public.CheckPrivilege("a56c96f7-fb31-4944-a248-45a8ad3c4398/a133f0cd-9a5e-4d02-ad94-9e0c0424d66d/87a8726b-5113-46ec-845c-6bd377935196/bfc31928-a7f1-45e0-bd3e-88a4268593ce"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"purchasing_edit.aspx?purchasing_id=" + Purchasing_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }
                //if (Public.CheckPrivilege("6b56546f-b64b-4365-85dd-e053f0a51630/598449ed-90f9-4211-bdd5-d471d8fda8a0/692dd604-e428-40cf-9202-43d333245b1d/7c806dd5-9a9a-4e83-bdec-10b328491c1a"))
                //{
                //    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('purchasing_do.aspx?action=move&purchasing_type=" + Purchasing_Type + "&purchasing_id=" + Purchasing_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                //}
                jsonBuilder.Append("\"");

                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
            return "";
        }
        finally
        {
            if (RdrList != null)
            {
                RdrList.Close();
                RdrList = null;
            }
        }
    }

    public string StockAlert()
    {
        string keyword = tools.CheckStr(Request.QueryString["keyword"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_SupplierID", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND((", "int", "ProductInfo.Product_AlertAmount", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND)", "int", "ProductInfo.Product_StockAmount", "<", Application["AlertAmount"].ToString()));

        Query.ParamInfos.Add(new ParamInfo("OR(", "int", "ProductInfo.Product_AlertAmount", ">", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND))", "feildint", "ProductInfo.Product_StockAmount", "<", "ProductInfo.Product_AlertAmount"));
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_Name", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_Code", "like", keyword));

        }

        PageInfo pageinfo = PROBLL.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<ProductInfo> entitys = PROBLL.GetProductList(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ProductInfo entity in entitys)
            {
                jsonBuilder.Append("{\"ProductInfo.Product_ID\":" + entity.Product_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Product_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Product_Code);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Product_Name);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Product_Spec);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Product_Maker);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Product_StockAmount);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<a href=\\\"/scm/purchasing/purchasing_add.aspx?Purchasing_Type=1&Product_Code=" + entity.Product_Code + "\\\" title=\\\"增加库存\\\"><img src=\\\"/images/btn_add.gif\\\" border=\\\"0\\\"></a>");
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
    /// 盘点是保存用
    /// </summary>
    public void ProductStockTake()
    {
        string Product_Code = tools.CheckStr(Request.Form["Product_Code"]);
        int Product_Amount = tools.CheckInt(Request.Form["Product_StockAmount"]);

        int StockAmout = 0, UseStockAmount = 0;

        ProductInfo pEntity = PROBLL.GetProductByCode(Product_Code, Public.GetCurrentSite(), Public.GetUserPrivilege());
        if (pEntity!=null)
        {

            StockAmout = Product_Amount - pEntity.Product_StockAmount;

            //UseStockAmount = pEntity.Product_StockAmount + StockAmout;
             
            //更新商品库存
            PROBLL.UpdateProductStock(Product_Code, StockAmout, StockAmout);
        }

        Public.Msg("positive", "操作成功", "操作成功", true, "stocktake.aspx");
    }

    public string StockTakeList()
    {
        string keyword = tools.CheckStr(Request.QueryString["keyword"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_SupplierID", "=", "0"));
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_Name", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_Code", "like", keyword));

        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = PROBLL.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<ProductInfo> entitys = PROBLL.GetProductList(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ProductInfo entity in entitys)
            {
                jsonBuilder.Append("{\"ProductInfo.Product_ID\":" + entity.Product_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Product_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Product_Code);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Product_Name);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Product_Spec);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Product_Maker);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"<div onmouseover=\\\"openupStock('" + entity.Product_Code + "', this);\\\" onmouseout=\\\"CloseStock();\\\">");
                jsonBuilder.Append(entity.Product_StockAmount);
                jsonBuilder.Append("</div>\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<a href=\\\"/scm/stocktake_edit.aspx?Product_Code=" + entity.Product_Code + "\\\" title=\\\"盘点\\\"><img src=\\\"/images/icon_edit.gif\\\" border=\\\"0\\\"></a>");
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
    /// 添加单据现在商品
    /// </summary>
    /// <returns></returns>
    public string SelectProduct()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        string target = tools.NullStr(Request["target"]);
        string product_id = "0";
        if (keyword != "输入商品编码、商品名称、通用名、生产企业、拼音首字母进行搜索" && keyword != null)
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

        int cate_id = tools.CheckInt(Request["product_cate"]);
        if (cate_id > 0)
        {
            Product product = new Product();
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

        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));
        PageInfo pageinfo = PROBLL.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<ProductInfo> entitys = PROBLL.GetProductList(Query, Public.GetUserPrivilege());

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

    /// <summary>
    /// 显示商品各仓库库存
    /// </summary>
    /// <param name="product_code">商品编码</param>
    /// <returns></returns>
    public string ShowBranchStock(string product_code)
    {
        product_code = tools.CheckStr(product_code);

        int ICount = 1;
        StringBuilder strJSON = new StringBuilder();
        string SqlList = "SELECT Depot_Name";
        SqlList += ", ISNULL((SELECT SUM(Purchasing_Amount) FROM SCM_Purchasing WHERE Purchasing_DepotID = Depot_ID AND Purchasing_ProductCode = '" + product_code + "'), 0) AS Depot_Amount";
        SqlList += " FROM SCM_Depot ORDER BY Depot_Sort";

        SqlDataReader RdrList = null;
        try
        {
            RdrList = DBHelper.ExecuteReader(SqlList);
            if (RdrList.HasRows)
            {
                strJSON.Append("[");
                while (RdrList.Read())
                {
                    if (ICount > 1) { strJSON.Append(","); }
                    strJSON.Append("{\"Depot_Name\":\"" + Public.JsonStr(tools.NullStr(RdrList["Depot_Name"])) + "\"");
                    strJSON.Append(", \"Depot_Amount\":\"" + Public.JsonStr(tools.NullStr(RdrList["Depot_Amount"])) + "\"}");
                    ICount++;
                }
                strJSON.Append("]");
            }

            RdrList.Close();
            RdrList = null;
        }
        catch 
        {
            RdrList.Close();
            RdrList = null;
        }

        return strJSON.ToString();
    }

}
