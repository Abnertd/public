using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.Product
{
    public class Package : IPackage
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public Package()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddPackage(PackageInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Package";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();
            DrAdd["Package_ID"] = entity.Package_ID;
            DrAdd["Package_Name"] = entity.Package_Name;
            DrAdd["Package_IsInsale"] = entity.Package_IsInsale;
            DrAdd["Package_StockAmount"] = entity.Package_StockAmount;
            DrAdd["Package_Weight"] = entity.Package_Weight;
            DrAdd["Package_Price"] = entity.Package_Price;
            DrAdd["Package_Sort"] = entity.Package_Sort;
            DrAdd["Package_Addtime"] = entity.Package_Addtime;
            DrAdd["Package_Site"] = entity.Package_Site;
            DrAdd["Package_SupplierID"] = entity.Package_SupplierID;

            DtAdd.Rows.Add(DrAdd);
            try {
                DBHelper.SaveChanges(SqlAdd, DtAdd);
                entity.Package_ID = GetLastPackage(entity.Package_Name);

                AddPackageProduct(entity.PackageProductInfos, entity.Package_ID);

                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                DtAdd.Dispose();
            }
        }

        public virtual bool EditPackage(PackageInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Package WHERE Package_ID = " + entity.Package_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try {
                if (DtAdd.Rows.Count > 0) {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Package_ID"] = entity.Package_ID;
                    DrAdd["Package_Name"] = entity.Package_Name;
                    DrAdd["Package_IsInsale"] = entity.Package_IsInsale;
                    DrAdd["Package_StockAmount"] = entity.Package_StockAmount;
                    DrAdd["Package_Weight"] = entity.Package_Weight;
                    DrAdd["Package_Price"] = entity.Package_Price;
                    DrAdd["Package_Sort"] = entity.Package_Sort;
                    DrAdd["Package_Addtime"] = entity.Package_Addtime;
                    DrAdd["Package_Site"] = entity.Package_Site;
                    DrAdd["Package_SupplierID"] = entity.Package_SupplierID;

                    DBHelper.SaveChanges(SqlAdd, DtAdd);

                    AddPackageProduct(entity.PackageProductInfos, entity.Package_ID);
                }
                else {
                    return false;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                DtAdd.Dispose();
            }
            return true;
        }

        public virtual int DelPackage(int ID)
        {
            string SqlAdd = "DELETE FROM Package WHERE Package_ID = " + ID;
            try {
                DelPackageProduct(ID);
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public virtual PackageInfo GetPackageByID(int ID)
        {
            PackageInfo entity = null;
            SqlDataReader RdrList = null;
            try {
                string SqlList;
                SqlList = "SELECT * FROM Package WHERE Package_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read()) {
                    entity = new PackageInfo();
                    entity.Package_ID = Tools.NullInt(RdrList["Package_ID"]);
                    entity.Package_Name = Tools.NullStr(RdrList["Package_Name"]);
                    entity.Package_IsInsale = Tools.NullInt(RdrList["Package_IsInsale"]);
                    entity.Package_StockAmount = Tools.NullInt(RdrList["Package_StockAmount"]);
                    entity.Package_Weight = Tools.NullInt(RdrList["Package_Weight"]);
                    entity.Package_Price = Tools.NullDbl(RdrList["Package_Price"]);
                    entity.Package_Sort = Tools.NullInt(RdrList["Package_Sort"]);
                    entity.Package_Addtime = Tools.NullDate(RdrList["Package_Addtime"]);
                    entity.Package_Site = Tools.NullStr(RdrList["Package_Site"]);
                    entity.Package_SupplierID = Tools.NullInt(RdrList["Package_SupplierID"]);

                    entity.PackageProductInfos = null;
                }
                RdrList.Close();
                RdrList = null;

                if (entity != null) { entity.PackageProductInfos = GetProductListByPackage(entity.Package_ID); }

                return entity;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (RdrList != null) {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual IList<PackageInfo> GetPackages(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<PackageInfo> entitys = null;
            PackageInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Package";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows) {
                    entitys = new List<PackageInfo>();
                    while (RdrList.Read()) {
                        entity = new PackageInfo();
                        entity.Package_ID = Tools.NullInt(RdrList["Package_ID"]);
                        entity.Package_Name = Tools.NullStr(RdrList["Package_Name"]);
                        entity.Package_IsInsale = Tools.NullInt(RdrList["Package_IsInsale"]);
                        entity.Package_StockAmount = Tools.NullInt(RdrList["Package_StockAmount"]);
                        entity.Package_Weight = Tools.NullInt(RdrList["Package_Weight"]);
                        entity.Package_Price = Tools.NullDbl(RdrList["Package_Price"]);
                        entity.Package_Sort = Tools.NullInt(RdrList["Package_Sort"]);
                        entity.Package_Addtime = Tools.NullDate(RdrList["Package_Addtime"]);
                        entity.Package_Site = Tools.NullStr(RdrList["Package_Site"]);
                        entity.Package_SupplierID = Tools.NullInt(RdrList["Package_SupplierID"]);
                        entity.PackageProductInfos = null;
                        entitys.Add(entity);
                        entity = null;
                    }
                }
                if (entitys != null)
                {
                    foreach (PackageInfo obj in entitys)
                    {
                        obj.PackageProductInfos = GetProductListByPackage(obj.Package_ID);
                    }
                }
                return entitys;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (RdrList != null) {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            int RecordCount, PageCount, CurrentPage;
            string SqlCount, SqlParam, SqlTable;
            PageInfo Page;

            try {
                Page = new PageInfo();
                SqlTable = "Package";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Package_ID) FROM " + SqlTable + SqlParam;

                RecordCount = Tools.NullInt(DBHelper.ExecuteScalar(SqlCount));
                PageCount = Tools.CalculatePages(RecordCount, Query.PageSize);
                CurrentPage = Tools.DeterminePage(Query.CurrentPage, PageCount);

                Page.RecordCount = RecordCount;
                Page.PageCount = PageCount;
                Page.CurrentPage = CurrentPage;
                Page.PageSize = Query.PageSize;

                return Page;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public virtual bool AddPackageProduct(IList<PackageProductInfo> entitys, int package_id)
        {
            if (entitys == null || entitys.Count == 0) { return true; }

            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;

            SqlAdd = "SELECT TOP 0 * FROM Package_Product";
            DtAdd = DBHelper.Query(SqlAdd);

            foreach (PackageProductInfo entity in entitys) {
                DrAdd = DtAdd.NewRow();
                DrAdd["Package_Product_ID"] = entity.Package_Product_ID;
                DrAdd["Package_Product_PackageID"] = package_id;
                DrAdd["Package_Product_ProductID"] = entity.Package_Product_ProductID;
                DrAdd["Package_Product_Amount"] = entity.Package_Product_Amount;
                DtAdd.Rows.Add(DrAdd);            
            }

            try {
                DelPackageProduct(package_id);

                DBHelper.SaveChanges(SqlAdd, DtAdd);
                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                DtAdd.Dispose();
            }
        }

        public virtual int DelPackageProduct(int ID)
        {
            string SqlAdd = "DELETE FROM Package_Product WHERE Package_Product_PackageID = " + ID;
            try {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public virtual int GetLastPackage(string package_name)
        {
            int Package_ID = 0;
            string SqlList = "SELECT Package_ID FROM Package WHERE Package_Name = '" + package_name + "' ORDER BY Package_ID DESC";
            SqlDataReader RdrList = null;
            try {
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read()) {
                    Package_ID = Tools.NullInt(RdrList[0]);
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                RdrList.Close();
                RdrList = null;
            }
            return Package_ID;
        }

        public virtual IList<PackageProductInfo> GetProductListByPackage(int ID)
        {
            IList<PackageProductInfo> entityList = null;
            PackageProductInfo entity = null;
            string SqlList = "SELECT * FROM Package_Product WHERE Package_Product_PackageID = " + ID;
            SqlDataReader RdrList = null;
            try
            {
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                    entityList = new List<PackageProductInfo>();

                while (RdrList.Read())
                {
                    entity = new PackageProductInfo();
                    entity.Package_Product_ID = Tools.NullInt(RdrList["Package_Product_ID"]);
                    entity.Package_Product_PackageID = Tools.NullInt(RdrList["Package_Product_PackageID"]);
                    entity.Package_Product_ProductID = Tools.NullInt(RdrList["Package_Product_ProductID"]);
                    entity.Package_Product_Amount = Tools.NullInt(RdrList["Package_Product_Amount"]);
                    entityList.Add(entity);
                    entity = null;
                }

                return entityList;
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
        
        }

        public virtual string GetPackageIDByProductID(int ProductID)
        {
            string strPackageID = "0";
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT Package_Product_PackageID FROM Package_Product WHERE Package_Product_ProductID = " + ProductID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                while (RdrList.Read())
                    strPackageID += "," + Tools.NullInt(RdrList["Package_Product_PackageID"]);
                RdrList.Close();
                RdrList = null;

                return strPackageID;
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
    }

}
