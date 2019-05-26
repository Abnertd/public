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
    public class SysMenu : ISysMenu
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public SysMenu()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddSysMenu(SysMenuInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Sys_Menu";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Sys_Menu_ID"] = entity.Sys_Menu_ID;
            DrAdd["Sys_Menu_Channel"] = entity.Sys_Menu_Channel;
            DrAdd["Sys_Menu_Name"] = entity.Sys_Menu_Name;
            DrAdd["Sys_Menu_ParentID"] = entity.Sys_Menu_ParentID;
            DrAdd["Sys_Menu_Privilege"] = entity.Sys_Menu_Privilege;
            DrAdd["Sys_Menu_Icon"] = entity.Sys_Menu_Icon;
            DrAdd["Sys_Menu_Url"] = entity.Sys_Menu_Url;
            DrAdd["Sys_Menu_Target"] = entity.Sys_Menu_Target;
            DrAdd["Sys_Menu_IsSystem"] = entity.Sys_Menu_IsSystem;
            DrAdd["Sys_Menu_IsDefault"] = entity.Sys_Menu_IsDefault;
            DrAdd["Sys_Menu_IsCommon"] = entity.Sys_Menu_IsCommon;
            DrAdd["Sys_Menu_IsActive"] = entity.Sys_Menu_IsActive;
            DrAdd["Sys_Menu_Sort"] = entity.Sys_Menu_Sort;
            DrAdd["Sys_Menu_Site"] = entity.Sys_Menu_Site;

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

        public virtual bool EditSysMenu(SysMenuInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Sys_Menu WHERE Sys_Menu_ID = " + entity.Sys_Menu_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Sys_Menu_ID"] = entity.Sys_Menu_ID;
                    DrAdd["Sys_Menu_Channel"] = entity.Sys_Menu_Channel;
                    DrAdd["Sys_Menu_Name"] = entity.Sys_Menu_Name;
                    DrAdd["Sys_Menu_ParentID"] = entity.Sys_Menu_ParentID;
                    DrAdd["Sys_Menu_Privilege"] = entity.Sys_Menu_Privilege;
                    DrAdd["Sys_Menu_Icon"] = entity.Sys_Menu_Icon;
                    DrAdd["Sys_Menu_Url"] = entity.Sys_Menu_Url;
                    DrAdd["Sys_Menu_Target"] = entity.Sys_Menu_Target;
                    DrAdd["Sys_Menu_IsSystem"] = entity.Sys_Menu_IsSystem;
                    DrAdd["Sys_Menu_IsDefault"] = entity.Sys_Menu_IsDefault;
                    DrAdd["Sys_Menu_IsCommon"] = entity.Sys_Menu_IsCommon;
                    DrAdd["Sys_Menu_IsActive"] = entity.Sys_Menu_IsActive;
                    DrAdd["Sys_Menu_Sort"] = entity.Sys_Menu_Sort;
                    DrAdd["Sys_Menu_Site"] = entity.Sys_Menu_Site;

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

        public virtual int DelSysMenu(int ID)
        {
            string SqlAdd = "DELETE FROM Sys_Menu WHERE Sys_Menu_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual SysMenuInfo GetSysMenuByID(int ID)
        {
            SysMenuInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Sys_Menu WHERE Sys_Menu_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new SysMenuInfo();

                    entity.Sys_Menu_ID = Tools.NullInt(RdrList["Sys_Menu_ID"]);
                    entity.Sys_Menu_Channel = Tools.NullInt(RdrList["Sys_Menu_Channel"]);
                    entity.Sys_Menu_Name = Tools.NullStr(RdrList["Sys_Menu_Name"]);
                    entity.Sys_Menu_ParentID = Tools.NullInt(RdrList["Sys_Menu_ParentID"]);
                    entity.Sys_Menu_Privilege = Tools.NullStr(RdrList["Sys_Menu_Privilege"]);
                    entity.Sys_Menu_Icon = Tools.NullStr(RdrList["Sys_Menu_Icon"]);
                    entity.Sys_Menu_Url = Tools.NullStr(RdrList["Sys_Menu_Url"]);
                    entity.Sys_Menu_Target = Tools.NullInt(RdrList["Sys_Menu_Target"]);
                    entity.Sys_Menu_IsSystem = Tools.NullInt(RdrList["Sys_Menu_IsSystem"]);
                    entity.Sys_Menu_IsDefault = Tools.NullInt(RdrList["Sys_Menu_IsDefault"]);
                    entity.Sys_Menu_IsCommon = Tools.NullInt(RdrList["Sys_Menu_IsCommon"]);
                    entity.Sys_Menu_IsActive = Tools.NullInt(RdrList["Sys_Menu_IsActive"]);
                    entity.Sys_Menu_Sort = Tools.NullInt(RdrList["Sys_Menu_Sort"]);
                    entity.Sys_Menu_Site = Tools.NullStr(RdrList["Sys_Menu_Site"]);

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

        public virtual IList<SysMenuInfo> GetSysMenus(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<SysMenuInfo> entitys = null;
            SysMenuInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Sys_Menu";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SysMenuInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SysMenuInfo();
                        entity.Sys_Menu_ID = Tools.NullInt(RdrList["Sys_Menu_ID"]);
                        entity.Sys_Menu_Channel = Tools.NullInt(RdrList["Sys_Menu_Channel"]);
                        entity.Sys_Menu_Name = Tools.NullStr(RdrList["Sys_Menu_Name"]);
                        entity.Sys_Menu_ParentID = Tools.NullInt(RdrList["Sys_Menu_ParentID"]);
                        entity.Sys_Menu_Privilege = Tools.NullStr(RdrList["Sys_Menu_Privilege"]);
                        entity.Sys_Menu_Icon = Tools.NullStr(RdrList["Sys_Menu_Icon"]);
                        entity.Sys_Menu_Url = Tools.NullStr(RdrList["Sys_Menu_Url"]);
                        entity.Sys_Menu_Target = Tools.NullInt(RdrList["Sys_Menu_Target"]);
                        entity.Sys_Menu_IsSystem = Tools.NullInt(RdrList["Sys_Menu_IsSystem"]);
                        entity.Sys_Menu_IsDefault = Tools.NullInt(RdrList["Sys_Menu_IsDefault"]);
                        entity.Sys_Menu_IsCommon = Tools.NullInt(RdrList["Sys_Menu_IsCommon"]);
                        entity.Sys_Menu_IsActive = Tools.NullInt(RdrList["Sys_Menu_IsActive"]);
                        entity.Sys_Menu_Sort = Tools.NullInt(RdrList["Sys_Menu_Sort"]);
                        entity.Sys_Menu_Site = Tools.NullStr(RdrList["Sys_Menu_Site"]);

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

        public virtual IList<SysMenuInfo> GetSysMenusSub(int Menu_ParentID)
        {
            IList<SysMenuInfo> entitys = null;
            SysMenuInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "Select * From Sys_Menu Where Sys_Menu_ParentID=" + Menu_ParentID + " order by Sys_Menu_Sort";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<SysMenuInfo>();
                    while (RdrList.Read())
                    {
                        entity = new SysMenuInfo();
                        entity.Sys_Menu_ID = Tools.NullInt(RdrList["Sys_Menu_ID"]);
                        entity.Sys_Menu_Channel = Tools.NullInt(RdrList["Sys_Menu_Channel"]);
                        entity.Sys_Menu_Name = Tools.NullStr(RdrList["Sys_Menu_Name"]);
                        entity.Sys_Menu_ParentID = Tools.NullInt(RdrList["Sys_Menu_ParentID"]);
                        entity.Sys_Menu_Privilege = Tools.NullStr(RdrList["Sys_Menu_Privilege"]);
                        entity.Sys_Menu_Icon = Tools.NullStr(RdrList["Sys_Menu_Icon"]);
                        entity.Sys_Menu_Url = Tools.NullStr(RdrList["Sys_Menu_Url"]);
                        entity.Sys_Menu_Target = Tools.NullInt(RdrList["Sys_Menu_Target"]);
                        entity.Sys_Menu_IsSystem = Tools.NullInt(RdrList["Sys_Menu_IsSystem"]);
                        entity.Sys_Menu_IsDefault = Tools.NullInt(RdrList["Sys_Menu_IsDefault"]);
                        entity.Sys_Menu_IsCommon = Tools.NullInt(RdrList["Sys_Menu_IsCommon"]);
                        entity.Sys_Menu_IsActive = Tools.NullInt(RdrList["Sys_Menu_IsActive"]);
                        entity.Sys_Menu_Sort = Tools.NullInt(RdrList["Sys_Menu_Sort"]);
                        entity.Sys_Menu_Site = Tools.NullStr(RdrList["Sys_Menu_Site"]);

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
                SqlTable = "Sys_Menu";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Sys_Menu_ID) FROM " + SqlTable + SqlParam;

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
