using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;
using System.Text;

namespace Glaer.Trade.B2C.DAL.Product
{
    public class HomeLeftCate : IHomeLeftCate
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public HomeLeftCate()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddHomeLeftCate(HomeLeftCateInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Home_Left_Cate";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Home_Left_Cate_ID"] = entity.Home_Left_Cate_ID;
            DrAdd["Home_Left_Cate_ParentID"] = entity.Home_Left_Cate_ParentID;
            DrAdd["Home_Left_Cate_CateID"] = entity.Home_Left_Cate_CateID;
            DrAdd["Home_Left_Cate_Name"] = entity.Home_Left_Cate_Name;
            DrAdd["Home_Left_Cate_URL"] = entity.Home_Left_Cate_URL;
            DrAdd["Home_Left_Cate_Img"] = entity.Home_Left_Cate_Img;
            DrAdd["Home_Left_Cate_Sort"] = entity.Home_Left_Cate_Sort;
            DrAdd["Home_Left_Cate_Active"] = entity.Home_Left_Cate_Active;
            DrAdd["Home_Left_Cate_Site"] = entity.Home_Left_Cate_Site;

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

        public virtual bool EditHomeLeftCate(HomeLeftCateInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Home_Left_Cate WHERE Home_Left_Cate_ID = " + entity.Home_Left_Cate_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Home_Left_Cate_ID"] = entity.Home_Left_Cate_ID;
                    DrAdd["Home_Left_Cate_ParentID"] = entity.Home_Left_Cate_ParentID;
                    DrAdd["Home_Left_Cate_CateID"] = entity.Home_Left_Cate_CateID;
                    DrAdd["Home_Left_Cate_Name"] = entity.Home_Left_Cate_Name;
                    DrAdd["Home_Left_Cate_URL"] = entity.Home_Left_Cate_URL;
                    DrAdd["Home_Left_Cate_Img"] = entity.Home_Left_Cate_Img;
                    DrAdd["Home_Left_Cate_Sort"] = entity.Home_Left_Cate_Sort;
                    DrAdd["Home_Left_Cate_Active"] = entity.Home_Left_Cate_Active;
                    DrAdd["Home_Left_Cate_Site"] = entity.Home_Left_Cate_Site;

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

        public virtual int DelHomeLeftCate(int ID)
        {
            string SqlAdd = "DELETE FROM Home_Left_Cate WHERE Home_Left_Cate_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual int DelHomeLeftCateAll()
        {
            string SqlAdd = "DELETE FROM Home_Left_Cate";
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual HomeLeftCateInfo GetHomeLeftCateByID(int ID)
        {
            HomeLeftCateInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Home_Left_Cate WHERE Home_Left_Cate_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new HomeLeftCateInfo();

                    entity.Home_Left_Cate_ID = Tools.NullInt(RdrList["Home_Left_Cate_ID"]);
                    entity.Home_Left_Cate_ParentID = Tools.NullInt(RdrList["Home_Left_Cate_ParentID"]);
                    entity.Home_Left_Cate_CateID = Tools.NullInt(RdrList["Home_Left_Cate_CateID"]);
                    entity.Home_Left_Cate_Name = Tools.NullStr(RdrList["Home_Left_Cate_Name"]);
                    entity.Home_Left_Cate_URL = Tools.NullStr(RdrList["Home_Left_Cate_URL"]);
                    entity.Home_Left_Cate_Img = Tools.NullStr(RdrList["Home_Left_Cate_Img"]);
                    entity.Home_Left_Cate_Sort = Tools.NullInt(RdrList["Home_Left_Cate_Sort"]);
                    entity.Home_Left_Cate_Active = Tools.NullInt(RdrList["Home_Left_Cate_Active"]);
                    entity.Home_Left_Cate_Site = Tools.NullStr(RdrList["Home_Left_Cate_Site"]);

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

        public virtual HomeLeftCateInfo GetHomeLeftCateByLastID()
        {
            HomeLeftCateInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT top 1 * FROM Home_Left_Cate Order By Home_Left_Cate_ID Desc";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new HomeLeftCateInfo();

                    entity.Home_Left_Cate_ID = Tools.NullInt(RdrList["Home_Left_Cate_ID"]);
                    entity.Home_Left_Cate_ParentID = Tools.NullInt(RdrList["Home_Left_Cate_ParentID"]);
                    entity.Home_Left_Cate_CateID = Tools.NullInt(RdrList["Home_Left_Cate_CateID"]);
                    entity.Home_Left_Cate_Name = Tools.NullStr(RdrList["Home_Left_Cate_Name"]);
                    entity.Home_Left_Cate_URL = Tools.NullStr(RdrList["Home_Left_Cate_URL"]);
                    entity.Home_Left_Cate_Img = Tools.NullStr(RdrList["Home_Left_Cate_Img"]);
                    entity.Home_Left_Cate_Sort = Tools.NullInt(RdrList["Home_Left_Cate_Sort"]);
                    entity.Home_Left_Cate_Active = Tools.NullInt(RdrList["Home_Left_Cate_Active"]);
                    entity.Home_Left_Cate_Site = Tools.NullStr(RdrList["Home_Left_Cate_Site"]);

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

        public virtual IList<HomeLeftCateInfo> GetHomeLeftCates(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<HomeLeftCateInfo> entitys = null;
            HomeLeftCateInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Home_Left_Cate";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<HomeLeftCateInfo>();
                    while (RdrList.Read())
                    {
                        entity = new HomeLeftCateInfo();
                        entity.Home_Left_Cate_ID = Tools.NullInt(RdrList["Home_Left_Cate_ID"]);
                        entity.Home_Left_Cate_ParentID = Tools.NullInt(RdrList["Home_Left_Cate_ParentID"]);
                        entity.Home_Left_Cate_CateID = Tools.NullInt(RdrList["Home_Left_Cate_CateID"]);
                        entity.Home_Left_Cate_Name = Tools.NullStr(RdrList["Home_Left_Cate_Name"]);
                        entity.Home_Left_Cate_URL = Tools.NullStr(RdrList["Home_Left_Cate_URL"]);
                        entity.Home_Left_Cate_Img = Tools.NullStr(RdrList["Home_Left_Cate_Img"]);
                        entity.Home_Left_Cate_Sort = Tools.NullInt(RdrList["Home_Left_Cate_Sort"]);
                        entity.Home_Left_Cate_Active = Tools.NullInt(RdrList["Home_Left_Cate_Active"]);
                        entity.Home_Left_Cate_Site = Tools.NullStr(RdrList["Home_Left_Cate_Site"]);

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
                SqlTable = "Home_Left_Cate";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Home_Left_Cate_ID) FROM " + SqlTable + SqlParam;

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
