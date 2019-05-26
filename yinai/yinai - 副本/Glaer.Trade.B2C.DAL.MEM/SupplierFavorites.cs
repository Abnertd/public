using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using System.Data;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.SQLHelper;
using System.Data.SqlClient;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class SupplierFavorites : ISupplierFavorites
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SupplierFavorites()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSupplierFavorites(SupplierFavoritesInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM U_Supplier_Favorites";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Supplier_Favorites_ID"] = entity.Supplier_Favorites_ID;
            DrAdd["Supplier_Favorites_SupplierID"] = entity.Supplier_Favorites_SupplierID;
            DrAdd["Supplier_Favorites_Type"] = entity.Supplier_Favorites_Type;
            DrAdd["Supplier_Favorites_TargetID"] = entity.Supplier_Favorites_TargetID;
            DrAdd["Supplier_Favorites_Addtime"] = entity.Supplier_Favorites_Addtime;
            DrAdd["Supplier_Favorites_Site"] = entity.Supplier_Favorites_Site;

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

        public virtual bool EditSupplierFavorites(SupplierFavoritesInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM U_Supplier_Favorites WHERE Supplier_Favorites_ID = " + entity.Supplier_Favorites_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Supplier_Favorites_ID"] = entity.Supplier_Favorites_ID;
                    DrAdd["Supplier_Favorites_SupplierID"] = entity.Supplier_Favorites_SupplierID;
                    DrAdd["Supplier_Favorites_Type"] = entity.Supplier_Favorites_Type;
                    DrAdd["Supplier_Favorites_TargetID"] = entity.Supplier_Favorites_TargetID;
                    DrAdd["Supplier_Favorites_Addtime"] = entity.Supplier_Favorites_Addtime;
                    DrAdd["Supplier_Favorites_Site"] = entity.Supplier_Favorites_Site;

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

        public virtual int DelSupplierFavorites(int ID)
        {
            string SqlAdd = "DELETE FROM U_Supplier_Favorites WHERE Supplier_Favorites_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SupplierFavoritesInfo GetSupplierFavoritesByID(int ID)
        {
            SupplierFavoritesInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM U_Supplier_Favorites WHERE Supplier_Favorites_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierFavoritesInfo();

                    entity.Supplier_Favorites_ID = Tools.NullInt(RdrList["Supplier_Favorites_ID"]);
                    entity.Supplier_Favorites_SupplierID = Tools.NullInt(RdrList["Supplier_Favorites_SupplierID"]);
                    entity.Supplier_Favorites_Type = Tools.NullInt(RdrList["Supplier_Favorites_Type"]);
                    entity.Supplier_Favorites_TargetID = Tools.NullInt(RdrList["Supplier_Favorites_TargetID"]);
                    entity.Supplier_Favorites_Addtime = Tools.NullDate(RdrList["Supplier_Favorites_Addtime"]);
                    entity.Supplier_Favorites_Site = Tools.NullStr(RdrList["Supplier_Favorites_Site"]);

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

        public virtual SupplierFavoritesInfo GetSupplierFavoritesByProductID(int Supplier_ID, int type_id, int target_ID)
        {
            SupplierFavoritesInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM U_Supplier_Favorites WHERE Supplier_Favorites_SupplierID = " + Supplier_ID + " and Supplier_Favorites_Type=" + type_id + " and Supplier_Favorites_TargetID=" + target_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SupplierFavoritesInfo();

                    entity.Supplier_Favorites_ID = Tools.NullInt(RdrList["Supplier_Favorites_ID"]);
                    entity.Supplier_Favorites_SupplierID = Tools.NullInt(RdrList["Supplier_Favorites_SupplierID"]);
                    entity.Supplier_Favorites_Type = Tools.NullInt(RdrList["Supplier_Favorites_Type"]);
                    entity.Supplier_Favorites_TargetID = Tools.NullInt(RdrList["Supplier_Favorites_TargetID"]);
                    entity.Supplier_Favorites_Addtime = Tools.NullDate(RdrList["Supplier_Favorites_Addtime"]);
                    entity.Supplier_Favorites_Site = Tools.NullStr(RdrList["Supplier_Favorites_Site"]);

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

        public virtual IList<SupplierFavoritesInfo> GetSupplierFavoritess(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SupplierFavoritesInfo> entitys = null;
            SupplierFavoritesInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "U_Supplier_Favorites";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SupplierFavoritesInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SupplierFavoritesInfo();
                        entity.Supplier_Favorites_ID = Tools.NullInt(RdrList["Supplier_Favorites_ID"]);
                        entity.Supplier_Favorites_SupplierID = Tools.NullInt(RdrList["Supplier_Favorites_SupplierID"]);
                        entity.Supplier_Favorites_Type = Tools.NullInt(RdrList["Supplier_Favorites_Type"]);
                        entity.Supplier_Favorites_TargetID = Tools.NullInt(RdrList["Supplier_Favorites_TargetID"]);
                        entity.Supplier_Favorites_Addtime = Tools.NullDate(RdrList["Supplier_Favorites_Addtime"]);
                        entity.Supplier_Favorites_Site = Tools.NullStr(RdrList["Supplier_Favorites_Site"]);

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
                SqlTable = "U_Supplier_Favorites";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Supplier_Favorites_ID) FROM " + SqlTable + SqlParam;

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
