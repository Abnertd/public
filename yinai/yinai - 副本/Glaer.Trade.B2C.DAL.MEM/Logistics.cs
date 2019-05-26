using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class Logistics : ILogistics
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public Logistics()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddLogistics(LogisticsInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Logistics";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Logistics_ID"] = entity.Logistics_ID;
            DrAdd["Logistics_NickName"] = entity.Logistics_NickName;
            DrAdd["Logistics_Password"] = entity.Logistics_Password;
            DrAdd["Logistics_CompanyName"] = entity.Logistics_CompanyName;
            DrAdd["Logistics_Name"] = entity.Logistics_Name;
            DrAdd["Logistics_Tel"] = entity.Logistics_Tel;
            DrAdd["Logistics_Status"] = entity.Logistics_Status;
            DrAdd["Logistics_Addtime"] = entity.Logistics_Addtime;
            DrAdd["Logistics_Lastlogin_Time"] = entity.Logistics_Lastlogin_Time;

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

        public virtual bool EditLogistics(LogisticsInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Logistics WHERE Logistics_ID = " + entity.Logistics_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Logistics_ID"] = entity.Logistics_ID;
                    DrAdd["Logistics_NickName"] = entity.Logistics_NickName;
                    DrAdd["Logistics_Password"] = entity.Logistics_Password;
                    DrAdd["Logistics_CompanyName"] = entity.Logistics_CompanyName;
                    DrAdd["Logistics_Name"] = entity.Logistics_Name;
                    DrAdd["Logistics_Tel"] = entity.Logistics_Tel;
                    DrAdd["Logistics_Status"] = entity.Logistics_Status;
                    DrAdd["Logistics_Addtime"] = entity.Logistics_Addtime;
                    DrAdd["Logistics_Lastlogin_Time"] = entity.Logistics_Lastlogin_Time;

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

        public virtual int DelLogistics(int ID)
        {
            string SqlAdd = "DELETE FROM Logistics WHERE Logistics_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual LogisticsInfo GetLogisticsByID(int ID)
        {
            LogisticsInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Logistics WHERE Logistics_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new LogisticsInfo();

                    entity.Logistics_ID = Tools.NullInt(RdrList["Logistics_ID"]);
                    entity.Logistics_NickName = Tools.NullStr(RdrList["Logistics_NickName"]);
                    entity.Logistics_Password = Tools.NullStr(RdrList["Logistics_Password"]);
                    entity.Logistics_CompanyName = Tools.NullStr(RdrList["Logistics_CompanyName"]);
                    entity.Logistics_Name = Tools.NullStr(RdrList["Logistics_Name"]);
                    entity.Logistics_Tel = Tools.NullStr(RdrList["Logistics_Tel"]);
                    entity.Logistics_Status = Tools.NullInt(RdrList["Logistics_Status"]);
                    entity.Logistics_Addtime = Tools.NullDate(RdrList["Logistics_Addtime"]);
                    entity.Logistics_Lastlogin_Time = Tools.NullDate(RdrList["Logistics_Lastlogin_Time"]);

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


        public virtual LogisticsInfo GetLogisticsByNickName(string NickName)
        {
            LogisticsInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Logistics WHERE Logistics_NickName = '" + NickName + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new LogisticsInfo();

                    entity.Logistics_ID = Tools.NullInt(RdrList["Logistics_ID"]);
                    entity.Logistics_NickName = Tools.NullStr(RdrList["Logistics_NickName"]);
                    entity.Logistics_Password = Tools.NullStr(RdrList["Logistics_Password"]);
                    entity.Logistics_CompanyName = Tools.NullStr(RdrList["Logistics_CompanyName"]);
                    entity.Logistics_Name = Tools.NullStr(RdrList["Logistics_Name"]);
                    entity.Logistics_Tel = Tools.NullStr(RdrList["Logistics_Tel"]);
                    entity.Logistics_Status = Tools.NullInt(RdrList["Logistics_Status"]);
                    entity.Logistics_Addtime = Tools.NullDate(RdrList["Logistics_Addtime"]);
                    entity.Logistics_Lastlogin_Time = Tools.NullDate(RdrList["Logistics_Lastlogin_Time"]);

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
        public virtual IList<LogisticsInfo> GetLogisticss(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<LogisticsInfo> entitys = null;
            LogisticsInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Logistics";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<LogisticsInfo>();
                    while (RdrList.Read())
                    {
                        entity = new LogisticsInfo();
                        entity.Logistics_ID = Tools.NullInt(RdrList["Logistics_ID"]);
                        entity.Logistics_NickName = Tools.NullStr(RdrList["Logistics_NickName"]);
                        entity.Logistics_Password = Tools.NullStr(RdrList["Logistics_Password"]);
                        entity.Logistics_CompanyName = Tools.NullStr(RdrList["Logistics_CompanyName"]);
                        entity.Logistics_Name = Tools.NullStr(RdrList["Logistics_Name"]);
                        entity.Logistics_Tel = Tools.NullStr(RdrList["Logistics_Tel"]);
                        entity.Logistics_Status = Tools.NullInt(RdrList["Logistics_Status"]);
                        entity.Logistics_Addtime = Tools.NullDate(RdrList["Logistics_Addtime"]);
                        entity.Logistics_Lastlogin_Time = Tools.NullDate(RdrList["Logistics_Lastlogin_Time"]);

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
                SqlTable = "Logistics";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Logistics_ID) FROM " + SqlTable + SqlParam;

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
