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
    public class ProductAuditReason : IProductAuditReason
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public ProductAuditReason()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddProductAuditReason(ProductAuditReasonInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Product_Audit_Reason";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Product_Audit_Reason_ID"] = entity.Product_Audit_Reason_ID;
            DrAdd["Product_Audit_Reason_Note"] = entity.Product_Audit_Reason_Note;

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

        public virtual bool EditProductAuditReason(ProductAuditReasonInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Product_Audit_Reason WHERE Product_Audit_Reason_ID = " + entity.Product_Audit_Reason_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Product_Audit_Reason_ID"] = entity.Product_Audit_Reason_ID;
                    DrAdd["Product_Audit_Reason_Note"] = entity.Product_Audit_Reason_Note;

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

        public virtual int DelProductAuditReason(int ID)
        {
            string SqlAdd = "DELETE FROM Product_Audit_Reason WHERE Product_Audit_Reason_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ProductAuditReasonInfo GetProductAuditReasonByID(int ID)
        {
            ProductAuditReasonInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Product_Audit_Reason WHERE Product_Audit_Reason_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ProductAuditReasonInfo();

                    entity.Product_Audit_Reason_ID = Tools.NullInt(RdrList["Product_Audit_Reason_ID"]);
                    entity.Product_Audit_Reason_Note = Tools.NullStr(RdrList["Product_Audit_Reason_Note"]);

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

        public virtual IList<ProductAuditReasonInfo> GetProductAuditReasons(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ProductAuditReasonInfo> entitys = null;
            ProductAuditReasonInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Product_Audit_Reason";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ProductAuditReasonInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ProductAuditReasonInfo();
                        entity.Product_Audit_Reason_ID = Tools.NullInt(RdrList["Product_Audit_Reason_ID"]);
                        entity.Product_Audit_Reason_Note = Tools.NullStr(RdrList["Product_Audit_Reason_Note"]);

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
                SqlTable = "Product_Audit_Reason";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Product_Audit_Reason_ID) FROM " + SqlTable + SqlParam;

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

        public virtual bool AddProductDenyReason(ProductDenyReasonInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Product_Deny_Reason";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Product_Deny_Reason_ID"] = entity.Product_Deny_Reason_ID;
            DrAdd["Product_Deny_Reason_ProductID"] = entity.Product_Deny_Reason_ProductID;
            DrAdd["Product_Deny_Reason_AuditReasonID"] = entity.Product_Deny_Reason_AuditReasonID;

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

        public virtual int DelProductDenyReason(int ID)
        {
            string SqlAdd = "DELETE FROM Product_Deny_Reason WHERE Product_Deny_Reason_ProductID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IList<ProductDenyReasonInfo> GetProductDenyReasons(int Product_ID)
        {
            int PageSize;
            int CurrentPage;
            IList<ProductDenyReasonInfo> entitys = null;
            ProductDenyReasonInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "Select * From Product_Deny_Reason Where Product_Deny_Reason_ProductID=" + Product_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ProductDenyReasonInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ProductDenyReasonInfo();
                        entity.Product_Deny_Reason_ID = Tools.NullInt(RdrList["Product_Deny_Reason_ID"]);
                        entity.Product_Deny_Reason_ProductID = Tools.NullInt(RdrList["Product_Deny_Reason_ProductID"]);
                        entity.Product_Deny_Reason_AuditReasonID = Tools.NullInt(RdrList["Product_Deny_Reason_AuditReasonID"]);

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

    }
}
