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
    public class MemberAccountLog : IMemberAccountLog
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public MemberAccountLog()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddMemberAccountOrders(MemberAccountOrdersInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Member_Account_Orders";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Account_Orders_ID"] = entity.Account_Orders_ID;
            DrAdd["Account_Orders_MemberID"] = entity.Account_Orders_MemberID;
            DrAdd["Account_Orders_SupplierID"] = entity.Account_Orders_SupplierID;
            DrAdd["Account_Orders_AccountType"] = entity.Account_Orders_AccountType;
            DrAdd["Account_Orders_SN"] = entity.Account_Orders_SN;
            DrAdd["Account_Orders_Amount"] = entity.Account_Orders_Amount;
            DrAdd["Account_Orders_Status"] = entity.Account_Orders_Status;
            DrAdd["Account_Orders_Addtime"] = entity.Account_Orders_Addtime;

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

        public virtual bool EditMemberAccountOrders(MemberAccountOrdersInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Member_Account_Orders WHERE Account_Orders_ID = " + entity.Account_Orders_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Account_Orders_ID"] = entity.Account_Orders_ID;
                    DrAdd["Account_Orders_MemberID"] = entity.Account_Orders_MemberID;
                    DrAdd["Account_Orders_SupplierID"] = entity.Account_Orders_SupplierID;
                    DrAdd["Account_Orders_AccountType"] = entity.Account_Orders_AccountType;
                    DrAdd["Account_Orders_SN"] = entity.Account_Orders_SN;
                    DrAdd["Account_Orders_Amount"] = entity.Account_Orders_Amount;
                    DrAdd["Account_Orders_Status"] = entity.Account_Orders_Status;
                    DrAdd["Account_Orders_Addtime"] = entity.Account_Orders_Addtime;

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

        public virtual int DelMemberAccountOrders(int ID)
        {
            string SqlAdd = "DELETE FROM Member_Account_Orders WHERE Account_Orders_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual MemberAccountOrdersInfo GetMemberAccountOrdersByOrdersSN(string OrdersSN)
        {
            MemberAccountOrdersInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member_Account_Orders WHERE Account_Orders_SN = '" + OrdersSN + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new MemberAccountOrdersInfo();

                    entity.Account_Orders_ID = Tools.NullInt(RdrList["Account_Orders_ID"]);
                    entity.Account_Orders_MemberID = Tools.NullInt(RdrList["Account_Orders_MemberID"]);
                    entity.Account_Orders_SupplierID = Tools.NullInt(RdrList["Account_Orders_SupplierID"]);
                    entity.Account_Orders_AccountType = Tools.NullInt(RdrList["Account_Orders_AccountType"]);
                    entity.Account_Orders_SN = Tools.NullStr(RdrList["Account_Orders_SN"]);
                    entity.Account_Orders_Amount = Tools.NullDbl(RdrList["Account_Orders_Amount"]);
                    entity.Account_Orders_Status = Tools.NullInt(RdrList["Account_Orders_Status"]);
                    entity.Account_Orders_Addtime = Tools.NullDate(RdrList["Account_Orders_Addtime"]);

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


        public virtual bool AddMemberAccountLog(MemberAccountLogInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Member_Account_Log";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Account_Log_ID"] = entity.Account_Log_ID;
            DrAdd["Account_Log_MemberID"] = entity.Account_Log_MemberID;
            DrAdd["Account_Log_Amount"] = entity.Account_Log_Amount;
            DrAdd["Account_Log_Remain"] = entity.Account_Log_Remain;
            DrAdd["Account_Log_Note"] = entity.Account_Log_Note;
            DrAdd["Account_Log_Addtime"] = entity.Account_Log_Addtime;
            DrAdd["Account_Log_Site"] = entity.Account_Log_Site;

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

        public virtual bool EditMemberAccountLog(MemberAccountLogInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Member_Account_Log WHERE Account_Log_ID = " + entity.Account_Log_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Account_Log_ID"] = entity.Account_Log_ID;
                    DrAdd["Account_Log_MemberID"] = entity.Account_Log_MemberID;
                    DrAdd["Account_Log_Amount"] = entity.Account_Log_Amount;
                    DrAdd["Account_Log_Remain"] = entity.Account_Log_Remain;
                    DrAdd["Account_Log_Note"] = entity.Account_Log_Note;
                    DrAdd["Account_Log_Addtime"] = entity.Account_Log_Addtime;
                    DrAdd["Account_Log_Site"] = entity.Account_Log_Site;

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

        public virtual int DelMemberAccountLog(int ID)
        {
            string SqlAdd = "DELETE FROM Member_Account_Log WHERE Account_Log_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual MemberAccountLogInfo GetMemberAccountLogByID(int ID)
        {
            MemberAccountLogInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member_Account_Log WHERE Account_Log_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new MemberAccountLogInfo();

                    entity.Account_Log_ID = Tools.NullInt(RdrList["Account_Log_ID"]);
                    entity.Account_Log_MemberID = Tools.NullInt(RdrList["Account_Log_MemberID"]);
                    entity.Account_Log_Amount = Tools.NullDbl(RdrList["Account_Log_Amount"]);
                    entity.Account_Log_Remain = Tools.NullDbl(RdrList["Account_Log_Remain"]);
                    entity.Account_Log_Note = Tools.NullStr(RdrList["Account_Log_Note"]);
                    entity.Account_Log_Addtime = Tools.NullDate(RdrList["Account_Log_Addtime"]);
                    entity.Account_Log_Site = Tools.NullStr(RdrList["Account_Log_Site"]);

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

        public virtual IList<MemberAccountLogInfo> GetMemberAccountLogs(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<MemberAccountLogInfo> entitys = null;
            MemberAccountLogInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Member_Account_Log";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<MemberAccountLogInfo>();
                    while (RdrList.Read())
                    {
                        entity = new MemberAccountLogInfo();
                        entity.Account_Log_ID = Tools.NullInt(RdrList["Account_Log_ID"]);
                        entity.Account_Log_MemberID = Tools.NullInt(RdrList["Account_Log_MemberID"]);
                        entity.Account_Log_Amount = Tools.NullDbl(RdrList["Account_Log_Amount"]);
                        entity.Account_Log_Remain = Tools.NullDbl(RdrList["Account_Log_Remain"]);
                        entity.Account_Log_Note = Tools.NullStr(RdrList["Account_Log_Note"]);
                        entity.Account_Log_Addtime = Tools.NullDate(RdrList["Account_Log_Addtime"]);
                        entity.Account_Log_Site = Tools.NullStr(RdrList["Account_Log_Site"]);

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
                SqlTable = "Member_Account_Log";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Account_Log_ID) FROM " + SqlTable + SqlParam;

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
