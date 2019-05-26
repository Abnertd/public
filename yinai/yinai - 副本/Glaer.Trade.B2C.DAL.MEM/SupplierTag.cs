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
    public class SupplierTag : ISupplierTag
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierTag()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierTag(SupplierTagInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Tag";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Supplier_Tag_ID"] = entity.Supplier_Tag_ID;
            DrAdd["Supplier_Tag_Name"] = entity.Supplier_Tag_Name;
            DrAdd["Supplier_Tag_Img"] = entity.Supplier_Tag_Img;
            DrAdd["Supplier_Tag_Content"] = entity.Supplier_Tag_Content;
            DrAdd["Supplier_Tag_Site"] = entity.Supplier_Tag_Site;

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

        public virtual bool EditSupplierTag(SupplierTagInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Tag WHERE Supplier_Tag_ID = " + entity.Supplier_Tag_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Supplier_Tag_ID"] = entity.Supplier_Tag_ID;
                    DrAdd["Supplier_Tag_Name"] = entity.Supplier_Tag_Name;
                    DrAdd["Supplier_Tag_Img"] = entity.Supplier_Tag_Img;
                    DrAdd["Supplier_Tag_Content"] = entity.Supplier_Tag_Content;
                    DrAdd["Supplier_Tag_Site"] = entity.Supplier_Tag_Site;

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

        public virtual int DelSupplierTag(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Tag WHERE Supplier_Tag_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierTagInfo GetSupplierTagByID(int ID)
        {
            SupplierTagInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Tag WHERE Supplier_Tag_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierTagInfo();

                    entity.Supplier_Tag_ID = Tools.NullInt(RdrList["Supplier_Tag_ID"]);
                    entity.Supplier_Tag_Name = Tools.NullStr(RdrList["Supplier_Tag_Name"]);
                    entity.Supplier_Tag_Img = Tools.NullStr(RdrList["Supplier_Tag_Img"]);
                    entity.Supplier_Tag_Content = Tools.NullStr(RdrList["Supplier_Tag_Content"]);
                    entity.Supplier_Tag_Site = Tools.NullStr(RdrList["Supplier_Tag_Site"]);

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

        public virtual IList<SupplierTagInfo> GetSupplierTags(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierTagInfo> entitys = null;
            SupplierTagInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Tag";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierTagInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierTagInfo();
                        entity.Supplier_Tag_ID = Tools.NullInt(RdrList["Supplier_Tag_ID"]);
                        entity.Supplier_Tag_Name = Tools.NullStr(RdrList["Supplier_Tag_Name"]);
                        entity.Supplier_Tag_Img = Tools.NullStr(RdrList["Supplier_Tag_Img"]);
                        entity.Supplier_Tag_Content = Tools.NullStr(RdrList["Supplier_Tag_Content"]);
                        entity.Supplier_Tag_Site = Tools.NullStr(RdrList["Supplier_Tag_Site"]);

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
                SqlTable = "Supplier_Tag";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Supplier_Tag_ID) FROM " + SqlTable + SqlParam;

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

        public virtual bool AddSupplierRelateTag(SupplierRelateTagInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_RelateTag";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Supplier_RelateTag_ID"] = entity.Supplier_RelateTag_ID;
            DrAdd["Supplier_RelateTag_SupplierID"] = entity.Supplier_RelateTag_SupplierID;
            DrAdd["Supplier_RelateTag_TagID"] = entity.Supplier_RelateTag_TagID;

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

        public virtual int DelSupplierRelateTagBySupplierID(int Supplier_ID)
        {
            string SqlAdd = "DELETE FROM Supplier_RelateTag WHERE Supplier_RelateTag_SupplierID = " + Supplier_ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IList<SupplierRelateTagInfo> GetSupplierRelateTagsBySupplierID(int Supplier_ID)
        {
            IList<SupplierRelateTagInfo> entitys = null;
            SupplierRelateTagInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "Select * FROM Supplier_RelateTag WHERE Supplier_RelateTag_SupplierID = " + Supplier_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierRelateTagInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierRelateTagInfo();
                        entity.Supplier_RelateTag_ID = Tools.NullInt(RdrList["Supplier_RelateTag_ID"]);
                        entity.Supplier_RelateTag_SupplierID = Tools.NullInt(RdrList["Supplier_RelateTag_SupplierID"]);
                        entity.Supplier_RelateTag_TagID = Tools.NullInt(RdrList["Supplier_RelateTag_TagID"]);

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
