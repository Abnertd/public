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
    public class MemberFavorites : IMemberFavorites
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public MemberFavorites()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddMemberFavorites(MemberFavoritesInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Member_Favorites";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Member_Favorites_ID"] = entity.Member_Favorites_ID;
            DrAdd["Member_Favorites_MemberID"] = entity.Member_Favorites_MemberID;
            DrAdd["Member_Favorites_Type"] = entity.Member_Favorites_Type;
            DrAdd["Member_Favorites_TargetID"] = entity.Member_Favorites_TargetID;
            DrAdd["Member_Favorites_Addtime"] = entity.Member_Favorites_Addtime;
            DrAdd["Member_Favorites_Site"] = entity.Member_Favorites_Site;

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

        public virtual int DelMemberFavorites(int ID)
        {
            string SqlAdd = "DELETE FROM Member_Favorites WHERE Member_Favorites_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual MemberFavoritesInfo GetMemberFavoritesByID(int ID)
        {
            MemberFavoritesInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member_Favorites WHERE Member_Favorites_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new MemberFavoritesInfo();

                    entity.Member_Favorites_ID = Tools.NullInt(RdrList["Member_Favorites_ID"]);
                    entity.Member_Favorites_MemberID = Tools.NullInt(RdrList["Member_Favorites_MemberID"]);
                    entity.Member_Favorites_Type = Tools.NullInt(RdrList["Member_Favorites_Type"]);
                    entity.Member_Favorites_TargetID = Tools.NullInt(RdrList["Member_Favorites_TargetID"]);
                    entity.Member_Favorites_Addtime = Tools.NullDate(RdrList["Member_Favorites_Addtime"]);
                    entity.Member_Favorites_Site = Tools.NullStr(RdrList["Member_Favorites_Site"]);

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

        public virtual MemberFavoritesInfo GetMemberFavoritesByProductID(int Member_ID, int type_id, int Product_ID)
        {
            MemberFavoritesInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member_Favorites WHERE Member_Favorites_MemberID = " + Member_ID + " and Member_Favorites_Type=" + type_id + " and Member_Favorites_TargetID=" + Product_ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new MemberFavoritesInfo();

                    entity.Member_Favorites_ID = Tools.NullInt(RdrList["Member_Favorites_ID"]);
                    entity.Member_Favorites_MemberID = Tools.NullInt(RdrList["Member_Favorites_MemberID"]);
                    entity.Member_Favorites_Type = Tools.NullInt(RdrList["Member_Favorites_Type"]);
                    entity.Member_Favorites_TargetID = Tools.NullInt(RdrList["Member_Favorites_TargetID"]);
                    entity.Member_Favorites_Addtime = Tools.NullDate(RdrList["Member_Favorites_Addtime"]);
                    entity.Member_Favorites_Site = Tools.NullStr(RdrList["Member_Favorites_Site"]);

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

        public virtual IList<MemberFavoritesInfo> GetMemberFavoritess(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<MemberFavoritesInfo> entitys = null;
            MemberFavoritesInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Member_Favorites";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<MemberFavoritesInfo>();
                    while (RdrList.Read())
                    {
                        entity = new MemberFavoritesInfo();
                        entity.Member_Favorites_ID = Tools.NullInt(RdrList["Member_Favorites_ID"]);
                        entity.Member_Favorites_MemberID = Tools.NullInt(RdrList["Member_Favorites_MemberID"]);
                        entity.Member_Favorites_Type = Tools.NullInt(RdrList["Member_Favorites_Type"]);
                        entity.Member_Favorites_TargetID = Tools.NullInt(RdrList["Member_Favorites_TargetID"]);
                        entity.Member_Favorites_Addtime = Tools.NullDate(RdrList["Member_Favorites_Addtime"]);
                        entity.Member_Favorites_Site = Tools.NullStr(RdrList["Member_Favorites_Site"]);

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
                SqlTable = "Member_Favorites";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Member_Favorites_ID) FROM " + SqlTable + SqlParam;

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



        #region 易耐网新加
        //根据会员ID获取该会员收藏商品数量
        public virtual IList<MemberFavoritesInfo> GetMemberFavoritesByMemberID(int Member_ID, int type_id)
        {
            IList<MemberFavoritesInfo> entitys = null;
            MemberFavoritesInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member_Favorites WHERE Member_Favorites_MemberID = " + Member_ID + " and Member_Favorites_Type=" + type_id;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<MemberFavoritesInfo>();
                    while (RdrList.Read())
                    {
                        entity = new MemberFavoritesInfo();
                        entity.Member_Favorites_ID = Tools.NullInt(RdrList["Member_Favorites_ID"]);
                        entity.Member_Favorites_MemberID = Tools.NullInt(RdrList["Member_Favorites_MemberID"]);
                        entity.Member_Favorites_Type = Tools.NullInt(RdrList["Member_Favorites_Type"]);
                        entity.Member_Favorites_TargetID = Tools.NullInt(RdrList["Member_Favorites_TargetID"]);
                        entity.Member_Favorites_Addtime = Tools.NullDate(RdrList["Member_Favorites_Addtime"]);
                        entity.Member_Favorites_Site = Tools.NullStr(RdrList["Member_Favorites_Site"]);

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

        #endregion
    }
}
