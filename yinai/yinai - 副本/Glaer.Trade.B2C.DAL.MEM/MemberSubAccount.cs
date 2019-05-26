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
    public class MemberSubAccount : IMemberSubAccount
    {
        ITools Tools; 
        ISQLHelper DBHelper;
        public MemberSubAccount()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddMemberSubAccount(MemberSubAccountInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Member_SubAccount";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["ID"] = entity.ID;
            DrAdd["MemberID"] = entity.MemberID;
            DrAdd["AccountName"] = entity.AccountName;
            DrAdd["Password"] = entity.Password;
            DrAdd["Name"] = entity.Name;
            DrAdd["Mobile"] = entity.Mobile;
            DrAdd["Email"] = entity.Email;
            DrAdd["Addtime"] = entity.Addtime;
            DrAdd["LastLoginTime"] = entity.LastLoginTime;
            DrAdd["IsActive"] = entity.IsActive;
            DrAdd["Privilege"] = entity.Privilege;

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

        public virtual bool EditMemberSubAccount(MemberSubAccountInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Member_SubAccount WHERE ID = " + entity.ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["ID"] = entity.ID;
                    DrAdd["MemberID"] = entity.MemberID;
                    DrAdd["AccountName"] = entity.AccountName;
                    DrAdd["Password"] = entity.Password;
                    DrAdd["Name"] = entity.Name;
                    DrAdd["Mobile"] = entity.Mobile;
                    DrAdd["Email"] = entity.Email;
                    DrAdd["Addtime"] = entity.Addtime;
                    DrAdd["LastLoginTime"] = entity.LastLoginTime;
                    DrAdd["IsActive"] = entity.IsActive;
                    DrAdd["Privilege"] = entity.Privilege;

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

        public virtual int DelMemberSubAccount(int ID)
        {
            string SqlAdd = "DELETE FROM Member_SubAccount WHERE ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual MemberSubAccountInfo GetMemberSubAccountByID(int ID)
        {
            MemberSubAccountInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member_SubAccount WHERE ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new MemberSubAccountInfo();

                    entity.ID = Tools.NullInt(RdrList["ID"]);
                    entity.MemberID = Tools.NullInt(RdrList["MemberID"]);
                    entity.AccountName = Tools.NullStr(RdrList["AccountName"]);
                    entity.Password = Tools.NullStr(RdrList["Password"]);
                    entity.Name = Tools.NullStr(RdrList["Name"]);
                    entity.Mobile = Tools.NullStr(RdrList["Mobile"]);
                    entity.Email = Tools.NullStr(RdrList["Email"]);
                    entity.Addtime = Tools.NullDate(RdrList["Addtime"]);
                    entity.LastLoginTime = Tools.NullDate(RdrList["LastLoginTime"]);
                    entity.IsActive = Tools.NullInt(RdrList["IsActive"]);
                    entity.Privilege = Tools.NullStr(RdrList["Privilege"]);

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

        public virtual IList<MemberSubAccountInfo> GetMemberSubAccounts(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<MemberSubAccountInfo> entitys = null;
            MemberSubAccountInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Member_SubAccount";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<MemberSubAccountInfo>();
                    while (RdrList.Read())
                    {
                        entity = new MemberSubAccountInfo();
                        entity.ID = Tools.NullInt(RdrList["ID"]);
                        entity.MemberID = Tools.NullInt(RdrList["MemberID"]);
                        entity.AccountName = Tools.NullStr(RdrList["AccountName"]);
                        entity.Password = Tools.NullStr(RdrList["Password"]);
                        entity.Name = Tools.NullStr(RdrList["Name"]);
                        entity.Mobile = Tools.NullStr(RdrList["Mobile"]);
                        entity.Email = Tools.NullStr(RdrList["Email"]);
                        entity.Addtime = Tools.NullDate(RdrList["Addtime"]);
                        entity.LastLoginTime = Tools.NullDate(RdrList["LastLoginTime"]);
                        entity.IsActive = Tools.NullInt(RdrList["IsActive"]);
                        entity.Privilege = Tools.NullStr(RdrList["Privilege"]);

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
                SqlTable = "Member_SubAccount";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(ID) FROM " + SqlTable + SqlParam;

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

    public class MemberSubAccountLog : IMemberSubAccountLog
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public MemberSubAccountLog()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddMemberSubAccountLog(MemberSubAccountLogInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Member_SubAccount_Log";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["ID"] = entity.ID;
            DrAdd["MemberID"] = entity.MemberID;
            DrAdd["AccountID"] = entity.AccountID;
            DrAdd["Action"] = entity.Action;
            DrAdd["Note"] = entity.Note;
            DrAdd["Addtime"] = entity.Addtime;

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

        public virtual bool EditMemberSubAccountLog(MemberSubAccountLogInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Member_SubAccount_Log WHERE ID = " + entity.ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["ID"] = entity.ID;
                    DrAdd["MemberID"] = entity.MemberID;
                    DrAdd["AccountID"] = entity.AccountID;
                    DrAdd["Action"] = entity.Action;
                    DrAdd["Note"] = entity.Note;
                    DrAdd["Addtime"] = entity.Addtime;

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

        public virtual int DelMemberSubAccountLog(int ID)
        {
            string SqlAdd = "DELETE FROM Member_SubAccount_Log WHERE ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual MemberSubAccountLogInfo GetMemberSubAccountLogByID(int ID)
        {
            MemberSubAccountLogInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member_SubAccount_Log WHERE ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new MemberSubAccountLogInfo();

                    entity.ID = Tools.NullInt(RdrList["ID"]);
                    entity.MemberID = Tools.NullInt(RdrList["MemberID"]);
                    entity.AccountID = Tools.NullInt(RdrList["AccountID"]);
                    entity.Action = Tools.NullStr(RdrList["Action"]);
                    entity.Note = Tools.NullStr(RdrList["Note"]);
                    entity.Addtime = Tools.NullDate(RdrList["Addtime"]);

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

        public virtual IList<MemberSubAccountLogInfo> GetMemberSubAccountLogs(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<MemberSubAccountLogInfo> entitys = null;
            MemberSubAccountLogInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Member_SubAccount_Log";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<MemberSubAccountLogInfo>();
                    while (RdrList.Read())
                    {
                        entity = new MemberSubAccountLogInfo();
                        entity.ID = Tools.NullInt(RdrList["ID"]);
                        entity.MemberID = Tools.NullInt(RdrList["MemberID"]);
                        entity.AccountID = Tools.NullInt(RdrList["AccountID"]);
                        entity.Action = Tools.NullStr(RdrList["Action"]);
                        entity.Note = Tools.NullStr(RdrList["Note"]);
                        entity.Addtime = Tools.NullDate(RdrList["Addtime"]);

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
                SqlTable = "Member_SubAccount_Log";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(ID) FROM " + SqlTable + SqlParam;

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
