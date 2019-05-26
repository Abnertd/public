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
    public class SupplierOnline : ISupplierOnline
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierOnline()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierOnline(SupplierOnlineInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Supplier_Online";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Supplier_Online_ID"] = entity.Supplier_Online_ID;
            DrAdd["Supplier_Online_SupplierID"] = entity.Supplier_Online_SupplierID;
            DrAdd["Supplier_Online_Type"] = entity.Supplier_Online_Type;
            DrAdd["Supplier_Online_Name"] = entity.Supplier_Online_Name;
            DrAdd["Supplier_Online_Code"] = entity.Supplier_Online_Code;
            DrAdd["Supplier_Online_Sort"] = entity.Supplier_Online_Sort;
            DrAdd["Supplier_Online_IsActive"] = entity.Supplier_Online_IsActive;
            DrAdd["Supplier_Online_Addtime"] = entity.Supplier_Online_Addtime;
            DrAdd["Supplier_Online_Site"] = entity.Supplier_Online_Site;

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

        public virtual bool EditSupplierOnline(SupplierOnlineInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Supplier_Online WHERE Supplier_Online_ID = " + entity.Supplier_Online_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Supplier_Online_ID"] = entity.Supplier_Online_ID;
                    DrAdd["Supplier_Online_SupplierID"] = entity.Supplier_Online_SupplierID;
                    DrAdd["Supplier_Online_Type"] = entity.Supplier_Online_Type;
                    DrAdd["Supplier_Online_Name"] = entity.Supplier_Online_Name;
                    DrAdd["Supplier_Online_Code"] = entity.Supplier_Online_Code;
                    DrAdd["Supplier_Online_Sort"] = entity.Supplier_Online_Sort;
                    DrAdd["Supplier_Online_IsActive"] = entity.Supplier_Online_IsActive;
                    DrAdd["Supplier_Online_Addtime"] = entity.Supplier_Online_Addtime;
                    DrAdd["Supplier_Online_Site"] = entity.Supplier_Online_Site;

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

        public virtual int DelSupplierOnline(int ID)
        {
            string SqlAdd = "DELETE FROM Supplier_Online WHERE Supplier_Online_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierOnlineInfo GetSupplierOnlineByID(int ID)
        {
            SupplierOnlineInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Supplier_Online WHERE Supplier_Online_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierOnlineInfo();

                    entity.Supplier_Online_ID = Tools.NullInt(RdrList["Supplier_Online_ID"]);
                    entity.Supplier_Online_SupplierID = Tools.NullInt(RdrList["Supplier_Online_SupplierID"]);
                    entity.Supplier_Online_Type = Tools.NullStr(RdrList["Supplier_Online_Type"]);
                    entity.Supplier_Online_Name = Tools.NullStr(RdrList["Supplier_Online_Name"]);
                    entity.Supplier_Online_Code = Tools.NullStr(RdrList["Supplier_Online_Code"]);
                    entity.Supplier_Online_Sort = Tools.NullInt(RdrList["Supplier_Online_Sort"]);
                    entity.Supplier_Online_IsActive = Tools.NullInt(RdrList["Supplier_Online_IsActive"]);
                    entity.Supplier_Online_Addtime = Tools.NullDate(RdrList["Supplier_Online_Addtime"]);
                    entity.Supplier_Online_Site = Tools.NullStr(RdrList["Supplier_Online_Site"]);

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

        public virtual IList<SupplierOnlineInfo> GetSupplierOnlines(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierOnlineInfo> entitys = null;
            SupplierOnlineInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Supplier_Online";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierOnlineInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierOnlineInfo();
                        entity.Supplier_Online_ID = Tools.NullInt(RdrList["Supplier_Online_ID"]);
                        entity.Supplier_Online_SupplierID = Tools.NullInt(RdrList["Supplier_Online_SupplierID"]);
                        entity.Supplier_Online_Type = Tools.NullStr(RdrList["Supplier_Online_Type"]);
                        entity.Supplier_Online_Name = Tools.NullStr(RdrList["Supplier_Online_Name"]);
                        entity.Supplier_Online_Code = Tools.NullStr(RdrList["Supplier_Online_Code"]);
                        entity.Supplier_Online_Sort = Tools.NullInt(RdrList["Supplier_Online_Sort"]);
                        entity.Supplier_Online_IsActive = Tools.NullInt(RdrList["Supplier_Online_IsActive"]);
                        entity.Supplier_Online_Addtime = Tools.NullDate(RdrList["Supplier_Online_Addtime"]);
                        entity.Supplier_Online_Site = Tools.NullStr(RdrList["Supplier_Online_Site"]);

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
                SqlTable = "Supplier_Online";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Supplier_Online_ID) FROM " + SqlTable + SqlParam;

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
