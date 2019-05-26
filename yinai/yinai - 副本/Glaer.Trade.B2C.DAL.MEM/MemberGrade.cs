using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class MemberGrade : IMemberGrade
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public MemberGrade()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddMemberGrade(MemberGradeInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Member_Grade";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Member_Grade_ID"] = entity.Member_Grade_ID;
            DrAdd["Member_Grade_Name"] = entity.Member_Grade_Name;
            DrAdd["Member_Grade_Percent"] = entity.Member_Grade_Percent;
            DrAdd["Member_Grade_Default"] = entity.Member_Grade_Default;
            DrAdd["Member_Grade_RequiredCoin"] = entity.Member_Grade_RequiredCoin;
            DrAdd["Member_Grade_CoinRate"] = entity.Member_Grade_CoinRate;
            DrAdd["Member_Grade_Addtime"] = entity.Member_Grade_Addtime;
            DrAdd["Member_Grade_Site"] = entity.Member_Grade_Site;

            DtAdd.Rows.Add(DrAdd);
            try {
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

        public virtual bool EditMemberGrade(MemberGradeInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Member_Grade WHERE Member_Grade_ID = " + entity.Member_Grade_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try {
                if (DtAdd.Rows.Count > 0) {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Member_Grade_ID"] = entity.Member_Grade_ID;
                    DrAdd["Member_Grade_Name"] = entity.Member_Grade_Name;
                    DrAdd["Member_Grade_Percent"] = entity.Member_Grade_Percent;
                    DrAdd["Member_Grade_Default"] = entity.Member_Grade_Default;
                    DrAdd["Member_Grade_RequiredCoin"] = entity.Member_Grade_RequiredCoin;
                    DrAdd["Member_Grade_CoinRate"] = entity.Member_Grade_CoinRate;
                    DrAdd["Member_Grade_Addtime"] = entity.Member_Grade_Addtime;
                    DrAdd["Member_Grade_Site"] = entity.Member_Grade_Site;

                    DBHelper.SaveChanges(SqlAdd, DtAdd);
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

        public virtual int DelMemberGrade(int ID)
        {
            string SqlAdd = "DELETE FROM Member_Grade WHERE Member_Grade_ID = " + ID;
            try {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public virtual MemberGradeInfo GetMemberGradeByID(int ID)
        {
            MemberGradeInfo entity = null;
            SqlDataReader RdrList = null;
            try {
                string SqlList;
                SqlList = "SELECT * FROM Member_Grade WHERE Member_Grade_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read()) {
                    entity = new MemberGradeInfo();
                    entity.Member_Grade_ID = Tools.NullInt(RdrList["Member_Grade_ID"]);
                    entity.Member_Grade_Name = Tools.NullStr(RdrList["Member_Grade_Name"]);
                    entity.Member_Grade_Percent = Tools.NullInt(RdrList["Member_Grade_Percent"]);
                    entity.Member_Grade_Default = Tools.NullInt(RdrList["Member_Grade_Default"]);
                    entity.Member_Grade_RequiredCoin = Tools.NullInt(RdrList["Member_Grade_RequiredCoin"]);
                    entity.Member_Grade_CoinRate = Tools.NullDbl(RdrList["Member_Grade_CoinRate"]);
                    entity.Member_Grade_Addtime = Tools.NullDate(RdrList["Member_Grade_Addtime"]);
                    entity.Member_Grade_Site = Tools.NullStr(RdrList["Member_Grade_Site"]);
                }
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

        public virtual IList<MemberGradeInfo> GetMemberGrades(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<MemberGradeInfo> entitys = null;
            MemberGradeInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Member_Grade";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows) {
                    entitys = new List<MemberGradeInfo>();
                    while (RdrList.Read()) {
                        entity = new MemberGradeInfo();
                        entity.Member_Grade_ID = Tools.NullInt(RdrList["Member_Grade_ID"]);
                        entity.Member_Grade_Name = Tools.NullStr(RdrList["Member_Grade_Name"]);
                        entity.Member_Grade_Percent = Tools.NullInt(RdrList["Member_Grade_Percent"]);
                        entity.Member_Grade_Default = Tools.NullInt(RdrList["Member_Grade_Default"]);
                        entity.Member_Grade_RequiredCoin = Tools.NullInt(RdrList["Member_Grade_RequiredCoin"]);
                        entity.Member_Grade_CoinRate = Tools.NullDbl(RdrList["Member_Grade_CoinRate"]);
                        entity.Member_Grade_Addtime = Tools.NullDate(RdrList["Member_Grade_Addtime"]);
                        entity.Member_Grade_Site = Tools.NullStr(RdrList["Member_Grade_Site"]);
                        entitys.Add(entity);
                        entity = null;
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
                SqlTable = "Member_Grade";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Member_Grade_ID) FROM " + SqlTable + SqlParam;

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

        public virtual MemberGradeInfo GetMemberDefaultGrade()
        {
            MemberGradeInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member_Grade WHERE Member_Grade_Default = 1 ";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new MemberGradeInfo();
                    entity.Member_Grade_ID = Tools.NullInt(RdrList["Member_Grade_ID"]);
                    entity.Member_Grade_Name = Tools.NullStr(RdrList["Member_Grade_Name"]);
                    entity.Member_Grade_Percent = Tools.NullInt(RdrList["Member_Grade_Percent"]);
                    entity.Member_Grade_Default = Tools.NullInt(RdrList["Member_Grade_Default"]);
                    entity.Member_Grade_RequiredCoin = Tools.NullInt(RdrList["Member_Grade_RequiredCoin"]);
                    entity.Member_Grade_CoinRate = Tools.NullDbl(RdrList["Member_Grade_CoinRate"]);
                    entity.Member_Grade_Addtime = Tools.NullDate(RdrList["Member_Grade_Addtime"]);
                    entity.Member_Grade_Site = Tools.NullStr(RdrList["Member_Grade_Site"]);
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

    }

}
