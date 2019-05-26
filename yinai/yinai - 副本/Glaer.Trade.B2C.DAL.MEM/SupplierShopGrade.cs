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
    public class SupplierShopGrade : ISupplierShopGrade
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierShopGrade()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierShopGrade(SupplierShopGradeInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Shop_Grade";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Shop_Grade_ID"] = entity.Shop_Grade_ID;
            DrAdd["Shop_Grade_Name"] = entity.Shop_Grade_Name;
            DrAdd["Shop_Grade_ProductLimit"] = entity.Shop_Grade_ProductLimit;
            DrAdd["Shop_Grade_DefaultCommission"] = entity.Shop_Grade_DefaultCommission;
            DrAdd["Shop_Grade_IsActive"] = entity.Shop_Grade_IsActive;
            DrAdd["Shop_Grade_Site"] = entity.Shop_Grade_Site;

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

        public virtual bool EditSupplierShopGrade(SupplierShopGradeInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Shop_Grade WHERE Shop_Grade_ID = " + entity.Shop_Grade_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Shop_Grade_ID"] = entity.Shop_Grade_ID;
                    DrAdd["Shop_Grade_Name"] = entity.Shop_Grade_Name;
                    DrAdd["Shop_Grade_ProductLimit"] = entity.Shop_Grade_ProductLimit;
                    DrAdd["Shop_Grade_DefaultCommission"] = entity.Shop_Grade_DefaultCommission;
                    DrAdd["Shop_Grade_IsActive"] = entity.Shop_Grade_IsActive;
                    DrAdd["Shop_Grade_Site"] = entity.Shop_Grade_Site;

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

        public virtual int DelSupplierShopGrade(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Shop_Grade WHERE Shop_Grade_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierShopGradeInfo GetSupplierShopGradeByID(int ID)
        {
            SupplierShopGradeInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Shop_Grade WHERE Shop_Grade_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierShopGradeInfo();

                    entity.Shop_Grade_ID = Tools.NullInt(RdrList["Shop_Grade_ID"]);
                    entity.Shop_Grade_Name = Tools.NullStr(RdrList["Shop_Grade_Name"]);
                    entity.Shop_Grade_ProductLimit = Tools.NullInt(RdrList["Shop_Grade_ProductLimit"]);
                    entity.Shop_Grade_DefaultCommission = Tools.NullDbl(RdrList["Shop_Grade_DefaultCommission"]);
                    entity.Shop_Grade_IsActive = Tools.NullInt(RdrList["Shop_Grade_IsActive"]);
                    entity.Shop_Grade_Site = Tools.NullStr(RdrList["Shop_Grade_Site"]);

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

        public virtual IList<SupplierShopGradeInfo> GetSupplierShopGrades(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierShopGradeInfo> entitys = null;
            SupplierShopGradeInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Shop_Grade";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierShopGradeInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierShopGradeInfo();
                        entity.Shop_Grade_ID = Tools.NullInt(RdrList["Shop_Grade_ID"]);
                        entity.Shop_Grade_Name = Tools.NullStr(RdrList["Shop_Grade_Name"]);
                        entity.Shop_Grade_ProductLimit = Tools.NullInt(RdrList["Shop_Grade_ProductLimit"]);
                        entity.Shop_Grade_DefaultCommission = Tools.NullDbl(RdrList["Shop_Grade_DefaultCommission"]);
                        entity.Shop_Grade_IsActive = Tools.NullInt(RdrList["Shop_Grade_IsActive"]);
                        entity.Shop_Grade_Site = Tools.NullStr(RdrList["Shop_Grade_Site"]);

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
                SqlTable = "Supplier_Shop_Grade";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Shop_Grade_ID) FROM " + SqlTable + SqlParam;

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

        

    }



}
