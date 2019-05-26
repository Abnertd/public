using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierShopEvaluate : ISupplierShopEvaluate
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierShopEvaluate()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierShopEvaluate(SupplierShopEvaluateInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Shop_Evaluate";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Shop_Evaluate_ID"] = entity.Shop_Evaluate_ID;
            DrAdd["Shop_Evaluate_SupplierID"] = entity.Shop_Evaluate_SupplierID;
            DrAdd["Shop_Evaluate_ContractID"] = entity.Shop_Evaluate_ContractID;
            DrAdd["Shop_Evaluate_Productid"] = entity.Shop_Evaluate_Productid;
            DrAdd["Shop_Evaluate_MemberId"] = entity.Shop_Evaluate_MemberID;
            DrAdd["Shop_Evaluate_Product"] = entity.Shop_Evaluate_Product;
            DrAdd["Shop_Evaluate_Service"] = entity.Shop_Evaluate_Service;
            DrAdd["Shop_Evaluate_Delivery"] = entity.Shop_Evaluate_Delivery;
            DrAdd["Shop_Evaluate_Note"] = entity.Shop_Evaluate_Note;
            DrAdd["Shop_Evaluate_Ischeck"] = entity.Shop_Evaluate_Ischeck;
            DrAdd["Shop_Evaluate_Recommend"] = entity.Shop_Evaluate_Recommend;
            DrAdd["Shop_Evaluate_IsGift"] = entity.Shop_Evaluate_IsGift;
            DrAdd["Shop_Evaluate_Addtime"] = entity.Shop_Evaluate_Addtime;
            DrAdd["Shop_Evaluate_Site"] = entity.Shop_Evaluate_Site;

            DtAdd.Rows.Add(DrAdd);
            try
            {
                DBHelper.SaveChanges(SqlAdd, DtAdd);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DtAdd.Dispose();
            }
        }

        public virtual bool EditSupplierShopEvaluate(SupplierShopEvaluateInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Shop_Evaluate WHERE Shop_Evaluate_ID = " + entity.Shop_Evaluate_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Shop_Evaluate_ID"] = entity.Shop_Evaluate_ID;
                    DrAdd["Shop_Evaluate_SupplierID"] = entity.Shop_Evaluate_SupplierID;
                    DrAdd["Shop_Evaluate_ContractID"] = entity.Shop_Evaluate_ContractID;
                    DrAdd["Shop_Evaluate_Productid"] = entity.Shop_Evaluate_Productid;
                    DrAdd["Shop_Evaluate_MemberId"] = entity.Shop_Evaluate_MemberID;
                    DrAdd["Shop_Evaluate_Product"] = entity.Shop_Evaluate_Product;
                    DrAdd["Shop_Evaluate_Service"] = entity.Shop_Evaluate_Service;
                    DrAdd["Shop_Evaluate_Delivery"] = entity.Shop_Evaluate_Delivery;
                    DrAdd["Shop_Evaluate_Note"] = entity.Shop_Evaluate_Note;
                    DrAdd["Shop_Evaluate_Ischeck"] = entity.Shop_Evaluate_Ischeck;
                    DrAdd["Shop_Evaluate_Recommend"] = entity.Shop_Evaluate_Recommend;
                    DrAdd["Shop_Evaluate_IsGift"] = entity.Shop_Evaluate_IsGift;
                    DrAdd["Shop_Evaluate_Addtime"] = entity.Shop_Evaluate_Addtime;
                    DrAdd["Shop_Evaluate_Site"] = entity.Shop_Evaluate_Site;

                    DBHelper.SaveChanges(SqlAdd, DtAdd);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DtAdd.Dispose();
            }
            return true;

        }

        public virtual int DelSupplierShopEvaluate(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Shop_Evaluate WHERE Shop_Evaluate_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierShopEvaluateInfo GetSupplierShopEvaluateByID(int ID)
        {
            SupplierShopEvaluateInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Shop_Evaluate WHERE Shop_Evaluate_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierShopEvaluateInfo();

                    entity.Shop_Evaluate_ID = Tools.NullInt(RdrList["Shop_Evaluate_ID"]);
                    entity.Shop_Evaluate_SupplierID = Tools.NullInt(RdrList["Shop_Evaluate_SupplierID"]);
                    entity.Shop_Evaluate_ContractID = Tools.NullInt(RdrList["Shop_Evaluate_ContractID"]);
                    entity.Shop_Evaluate_Productid = Tools.NullInt(RdrList["Shop_Evaluate_Productid"]);
                    entity.Shop_Evaluate_MemberID = Tools.NullInt(RdrList["Shop_Evaluate_MemberId"]);
                    entity.Shop_Evaluate_Product = Tools.NullInt(RdrList["Shop_Evaluate_Product"]);
                    entity.Shop_Evaluate_Service = Tools.NullInt(RdrList["Shop_Evaluate_Service"]);
                    entity.Shop_Evaluate_Delivery = Tools.NullInt(RdrList["Shop_Evaluate_Delivery"]);
                    entity.Shop_Evaluate_Note = Tools.NullStr(RdrList["Shop_Evaluate_Note"]);
                    entity.Shop_Evaluate_Ischeck = Tools.NullInt(RdrList["Shop_Evaluate_Ischeck"]);
                    entity.Shop_Evaluate_Recommend = Tools.NullInt(RdrList["Shop_Evaluate_Recommend"]);
                    entity.Shop_Evaluate_IsGift = Tools.NullInt(RdrList["Shop_Evaluate_IsGift"]);
                    entity.Shop_Evaluate_Addtime = Tools.NullDate(RdrList["Shop_Evaluate_Addtime"]);
                    entity.Shop_Evaluate_Site = Tools.NullStr(RdrList["Shop_Evaluate_Site"]);

                }

                return entity;
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

        public virtual IList<SupplierShopEvaluateInfo> GetSupplierShopEvaluates(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierShopEvaluateInfo> entitys = null;
            SupplierShopEvaluateInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Shop_Evaluate";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierShopEvaluateInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierShopEvaluateInfo();
                        entity.Shop_Evaluate_ID = Tools.NullInt(RdrList["Shop_Evaluate_ID"]);
                        entity.Shop_Evaluate_SupplierID = Tools.NullInt(RdrList["Shop_Evaluate_SupplierID"]);
                        entity.Shop_Evaluate_ContractID = Tools.NullInt(RdrList["Shop_Evaluate_ContractID"]);
                        entity.Shop_Evaluate_Productid = Tools.NullInt(RdrList["Shop_Evaluate_Productid"]);
                        entity.Shop_Evaluate_MemberID = Tools.NullInt(RdrList["Shop_Evaluate_MemberId"]);
                        entity.Shop_Evaluate_Product = Tools.NullInt(RdrList["Shop_Evaluate_Product"]);
                        entity.Shop_Evaluate_Service = Tools.NullInt(RdrList["Shop_Evaluate_Service"]);
                        entity.Shop_Evaluate_Delivery = Tools.NullInt(RdrList["Shop_Evaluate_Delivery"]);
                        entity.Shop_Evaluate_Note = Tools.NullStr(RdrList["Shop_Evaluate_Note"]);
                        entity.Shop_Evaluate_Ischeck = Tools.NullInt(RdrList["Shop_Evaluate_Ischeck"]);
                        entity.Shop_Evaluate_Recommend = Tools.NullInt(RdrList["Shop_Evaluate_Recommend"]);
                        entity.Shop_Evaluate_IsGift = Tools.NullInt(RdrList["Shop_Evaluate_IsGift"]);
                        entity.Shop_Evaluate_Addtime = Tools.NullDate(RdrList["Shop_Evaluate_Addtime"]);
                        entity.Shop_Evaluate_Site = Tools.NullStr(RdrList["Shop_Evaluate_Site"]);

                        entitys.Add(entity);
                        entity = null;
                    }
                }
                return entitys;
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

        public virtual PageInfo GetPageInfo(QueryInfo Query)
        {
            int RecordCount, PageCount, CurrentPage;
            string SqlCount, SqlParam, SqlTable;
            PageInfo Page;

            try
            {
                Page = new PageInfo();
                SqlTable = "Supplier_Shop_Evaluate";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Shop_Evaluate_ID) FROM " + SqlTable + SqlParam;

                RecordCount = Tools.NullInt(DBHelper.ExecuteScalar(SqlCount));
                PageCount = Tools.CalculatePages(RecordCount, Query.PageSize);
                CurrentPage = Tools.DeterminePage(Query.CurrentPage, PageCount);

                Page.RecordCount = RecordCount;
                Page.PageCount = PageCount;
                Page.CurrentPage = CurrentPage;
                Page.PageSize = Query.PageSize;

                return Page;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public virtual int GetSupplierShopEvaluateReviewValidCount(int Product_ID)
        {
            string SqlAdd = "select count(Shop_Evaluate_ProductID) FROM Supplier_Shop_Evaluate WHERE Shop_Evaluate_IsCheck=1 and Shop_Evaluate_ProductID = " + Product_ID;

            try
            {
                return Tools.NullInt(DBHelper.ExecuteScalar(SqlAdd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }


}
