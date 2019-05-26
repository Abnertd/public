using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.Sys
{
    public class SysMessage : ISysMessage
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SysMessage()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSysMessage(SysMessageInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Sys_Message";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Message_ID"] = entity.Message_ID;
            DrAdd["Message_Type"] = entity.Message_Type;
            DrAdd["Message_UserType"] = entity.Message_UserType;
            DrAdd["Message_ReceiveID"] = entity.Message_ReceiveID;
            DrAdd["Message_SendID"] = entity.Message_SendID;
            DrAdd["Message_Content"] = entity.Message_Content;
            DrAdd["Message_Addtime"] = entity.Message_Addtime;
            DrAdd["Message_Status"] = entity.Message_Status;
            DrAdd["Message_Site"] = entity.Message_Site;
            DrAdd["Message_IsHidden"] = entity.Message_IsHidden;
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

        public virtual bool EditSysMessage(SysMessageInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Sys_Message WHERE Message_ID = " + entity.Message_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Message_ID"] = entity.Message_ID;
                    DrAdd["Message_Type"] = entity.Message_Type;
                    DrAdd["Message_UserType"] = entity.Message_UserType;
                    DrAdd["Message_ReceiveID"] = entity.Message_ReceiveID;
                    DrAdd["Message_SendID"] = entity.Message_SendID;
                    DrAdd["Message_Content"] = entity.Message_Content;
                    DrAdd["Message_Addtime"] = entity.Message_Addtime;
                    DrAdd["Message_Status"] = entity.Message_Status;
                    DrAdd["Message_Site"] = entity.Message_Site;
                    DrAdd["Message_IsHidden"] = entity.Message_IsHidden;
               
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

        public virtual int DelSysMessage(int ID)
        {
            string SqlAdd = "DELETE FROM Sys_Message WHERE Message_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SysMessageInfo GetSysMessageByID(int ID)
        {
            SysMessageInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Sys_Message WHERE Message_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SysMessageInfo();

                    entity.Message_ID = Tools.NullInt(RdrList["Message_ID"]);
                    entity.Message_Type = Tools.NullInt(RdrList["Message_Type"]);
                    entity.Message_UserType = Tools.NullInt(RdrList["Message_UserType"]);
                    entity.Message_ReceiveID = Tools.NullInt(RdrList["Message_ReceiveID"]);
                    entity.Message_SendID = Tools.NullInt(RdrList["Message_SendID"]);
                    entity.Message_Content = Tools.NullStr(RdrList["Message_Content"]);
                    entity.Message_Addtime = Tools.NullDate(RdrList["Message_Addtime"]);
                    entity.Message_Status = Tools.NullInt(RdrList["Message_Status"]);
                    entity.Message_Site = Tools.NullStr(RdrList["Message_Site"]);
                    entity.Message_IsHidden = Tools.NullInt(RdrList["Message_IsHidden"]);
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

        public virtual IList<SysMessageInfo> GetSysMessages(int Message_Type,int Message_UserType, int Message_ReceiveID)
        {
            IList<SysMessageInfo> entitys = null;
            SysMessageInfo entity = null;
            string SqlList = "select * from Sys_Message where Message_UserType=" + Message_UserType + " and Message_ReceiveID=" + Message_ReceiveID;

            if (Message_Type > 0)
            {
                SqlList += " and Message_Type=" + Message_Type;
            }

            SqlDataReader RdrList = null;
            try
            {
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SysMessageInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SysMessageInfo();
                        entity.Message_ID = Tools.NullInt(RdrList["Message_ID"]);
                        entity.Message_Type = Tools.NullInt(RdrList["Message_Type"]);
                        entity.Message_UserType = Tools.NullInt(RdrList["Message_UserType"]);
                        entity.Message_ReceiveID = Tools.NullInt(RdrList["Message_ReceiveID"]);
                        entity.Message_SendID = Tools.NullInt(RdrList["Message_SendID"]);
                        entity.Message_Content = Tools.NullStr(RdrList["Message_Content"]);
                        entity.Message_Addtime = Tools.NullDate(RdrList["Message_Addtime"]);
                        entity.Message_Status = Tools.NullInt(RdrList["Message_Status"]);
                        entity.Message_Site = Tools.NullStr(RdrList["Message_Site"]);
                        entity.Message_IsHidden = Tools.NullInt(RdrList["Message_IsHidden"]);
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

        public virtual IList<SysMessageInfo> GetSysMessages(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SysMessageInfo> entitys = null;
            SysMessageInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Sys_Message";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SysMessageInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SysMessageInfo();
                        entity.Message_ID = Tools.NullInt(RdrList["Message_ID"]);
                        entity.Message_Type = Tools.NullInt(RdrList["Message_Type"]);
                        entity.Message_UserType = Tools.NullInt(RdrList["Message_UserType"]);
                        entity.Message_ReceiveID = Tools.NullInt(RdrList["Message_ReceiveID"]);
                        entity.Message_SendID = Tools.NullInt(RdrList["Message_SendID"]);
                        entity.Message_Content = Tools.NullStr(RdrList["Message_Content"]);
                        entity.Message_Addtime = Tools.NullDate(RdrList["Message_Addtime"]);
                        entity.Message_Status = Tools.NullInt(RdrList["Message_Status"]);
                        entity.Message_Site = Tools.NullStr(RdrList["Message_Site"]);
                        entity.Message_IsHidden = Tools.NullInt(RdrList["Message_IsHidden"]);
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
                SqlTable = "Sys_Message";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Message_ID) FROM " + SqlTable + SqlParam;

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
