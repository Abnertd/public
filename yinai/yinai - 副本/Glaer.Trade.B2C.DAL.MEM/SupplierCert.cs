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
    public class SupplierCert : ISupplierCert
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierCert()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierCert(SupplierCertInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Cert";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Supplier_Cert_ID"] = entity.Supplier_Cert_ID;
            DrAdd["Supplier_Cert_Type"] = entity.Supplier_Cert_Type;
            DrAdd["Supplier_Cert_Name"] = entity.Supplier_Cert_Name;
            DrAdd["Supplier_Cert_Note"] = entity.Supplier_Cert_Note;
            DrAdd["Supplier_Cert_Addtime"] = entity.Supplier_Cert_Addtime;
            DrAdd["Supplier_Cert_Sort"] = entity.Supplier_Cert_Sort;
            DrAdd["Supplier_Cert_Site"] = entity.Supplier_Cert_Site;

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

        public virtual bool EditSupplierCert(SupplierCertInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Cert WHERE Supplier_Cert_ID = " + entity.Supplier_Cert_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Supplier_Cert_ID"] = entity.Supplier_Cert_ID;
                    DrAdd["Supplier_Cert_Type"] = entity.Supplier_Cert_Type;
                    DrAdd["Supplier_Cert_Name"] = entity.Supplier_Cert_Name;
                    DrAdd["Supplier_Cert_Note"] = entity.Supplier_Cert_Note;
                    DrAdd["Supplier_Cert_Addtime"] = entity.Supplier_Cert_Addtime;
                    DrAdd["Supplier_Cert_Sort"] = entity.Supplier_Cert_Sort;
                    DrAdd["Supplier_Cert_Site"] = entity.Supplier_Cert_Site;

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

        public virtual int DelSupplierCert(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Cert WHERE Supplier_Cert_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierCertInfo GetSupplierCertByID(int ID)
        {
            SupplierCertInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Cert WHERE Supplier_Cert_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierCertInfo();

                    entity.Supplier_Cert_ID = Tools.NullInt(RdrList["Supplier_Cert_ID"]);
                    entity.Supplier_Cert_Type = Tools.NullInt(RdrList["Supplier_Cert_Type"]);
                    entity.Supplier_Cert_Name = Tools.NullStr(RdrList["Supplier_Cert_Name"]);
                    entity.Supplier_Cert_Note = Tools.NullStr(RdrList["Supplier_Cert_Note"]);
                    entity.Supplier_Cert_Addtime = Tools.NullDate(RdrList["Supplier_Cert_Addtime"]);
                    entity.Supplier_Cert_Sort = Tools.NullInt(RdrList["Supplier_Cert_Sort"]);
                    entity.Supplier_Cert_Site = Tools.NullStr(RdrList["Supplier_Cert_Site"]);

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

        public virtual IList<SupplierCertInfo> GetSupplierCerts(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierCertInfo> entitys = null;
            SupplierCertInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Cert";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierCertInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierCertInfo();
                        entity.Supplier_Cert_ID = Tools.NullInt(RdrList["Supplier_Cert_ID"]);
                        entity.Supplier_Cert_Type = Tools.NullInt(RdrList["Supplier_Cert_Type"]);
                        entity.Supplier_Cert_Name = Tools.NullStr(RdrList["Supplier_Cert_Name"]);
                        entity.Supplier_Cert_Note = Tools.NullStr(RdrList["Supplier_Cert_Note"]);
                        entity.Supplier_Cert_Addtime = Tools.NullDate(RdrList["Supplier_Cert_Addtime"]);
                        entity.Supplier_Cert_Sort = Tools.NullInt(RdrList["Supplier_Cert_Sort"]);
                        entity.Supplier_Cert_Site = Tools.NullStr(RdrList["Supplier_Cert_Site"]);

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
                SqlTable = "Supplier_Cert";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Supplier_Cert_ID) FROM " + SqlTable + SqlParam;

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


    public class SupplierCertType : ISupplierCertType
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierCertType()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierCertType(SupplierCertTypeInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Cert_Type";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Cert_Type_ID"] = entity.Cert_Type_ID;
            DrAdd["Cert_Type_Name"] = entity.Cert_Type_Name;
            DrAdd["Cert_Type_Sort"] = entity.Cert_Type_Sort;
            DrAdd["Cert_Type_IsActive"] = entity.Cert_Type_IsActive;
            DrAdd["Cert_Type_Site"] = entity.Cert_Type_Site;

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

        public virtual bool EditSupplierCertType(SupplierCertTypeInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Cert_Type WHERE Cert_Type_ID = " + entity.Cert_Type_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Cert_Type_ID"] = entity.Cert_Type_ID;
                    DrAdd["Cert_Type_Name"] = entity.Cert_Type_Name;
                    DrAdd["Cert_Type_Sort"] = entity.Cert_Type_Sort;
                    DrAdd["Cert_Type_IsActive"] = entity.Cert_Type_IsActive;
                    DrAdd["Cert_Type_Site"] = entity.Cert_Type_Site;

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

        public virtual int DelSupplierCertType(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Cert_Type WHERE Cert_Type_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierCertTypeInfo GetSupplierCertTypeByID(int ID)
        {
            SupplierCertTypeInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Cert_Type WHERE Cert_Type_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierCertTypeInfo();

                    entity.Cert_Type_ID = Tools.NullInt(RdrList["Cert_Type_ID"]);
                    entity.Cert_Type_Name = Tools.NullStr(RdrList["Cert_Type_Name"]);
                    entity.Cert_Type_Sort = Tools.NullInt(RdrList["Cert_Type_Sort"]);
                    entity.Cert_Type_IsActive = Tools.NullInt(RdrList["Cert_Type_IsActive"]);
                    entity.Cert_Type_Site = Tools.NullStr(RdrList["Cert_Type_Site"]);

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

        public virtual IList<SupplierCertTypeInfo> GetSupplierCertTypes(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierCertTypeInfo> entitys = null;
            SupplierCertTypeInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Cert_Type";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierCertTypeInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierCertTypeInfo();
                        entity.Cert_Type_ID = Tools.NullInt(RdrList["Cert_Type_ID"]);
                        entity.Cert_Type_Name = Tools.NullStr(RdrList["Cert_Type_Name"]);
                        entity.Cert_Type_Sort = Tools.NullInt(RdrList["Cert_Type_Sort"]);
                        entity.Cert_Type_IsActive = Tools.NullInt(RdrList["Cert_Type_IsActive"]);
                        entity.Cert_Type_Site = Tools.NullStr(RdrList["Cert_Type_Site"]);

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
                SqlTable = "Supplier_Cert_Type";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Cert_Type_ID) FROM " + SqlTable + SqlParam;

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
