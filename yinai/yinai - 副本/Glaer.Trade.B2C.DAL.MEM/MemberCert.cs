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
    public class MemberCertType : IMemberCertType
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public MemberCertType()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddMemberCertType(MemberCertTypeInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Member_Cert_Type";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Member_Cert_Type_ID"] = entity.Member_Cert_Type_ID;
            DrAdd["Member_Cert_Type_Name"] = entity.Member_Cert_Type_Name;
            DrAdd["Member_Cert_Type_Sort"] = entity.Member_Cert_Type_Sort;
            DrAdd["Member_Cert_Type_IsActive"] = entity.Member_Cert_Type_IsActive;
            DrAdd["Member_Cert_Type_Site"] = entity.Member_Cert_Type_Site;

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

        public virtual bool EditMemberCertType(MemberCertTypeInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Member_Cert_Type WHERE Member_Cert_Type_ID = " + entity.Member_Cert_Type_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Member_Cert_Type_ID"] = entity.Member_Cert_Type_ID;
                    DrAdd["Member_Cert_Type_Name"] = entity.Member_Cert_Type_Name;
                    DrAdd["Member_Cert_Type_Sort"] = entity.Member_Cert_Type_Sort;
                    DrAdd["Member_Cert_Type_IsActive"] = entity.Member_Cert_Type_IsActive;
                    DrAdd["Member_Cert_Type_Site"] = entity.Member_Cert_Type_Site;

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

        public virtual int DelMemberCertType(int ID)
        {
            string SqlAdd = "DELETE FROM Member_Cert_Type WHERE Member_Cert_Type_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual MemberCertTypeInfo GetMemberCertTypeByID(int ID)
        {
            MemberCertTypeInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member_Cert_Type WHERE Member_Cert_Type_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new MemberCertTypeInfo();

                    entity.Member_Cert_Type_ID = Tools.NullInt(RdrList["Member_Cert_Type_ID"]);
                    entity.Member_Cert_Type_Name = Tools.NullStr(RdrList["Member_Cert_Type_Name"]);
                    entity.Member_Cert_Type_Sort = Tools.NullInt(RdrList["Member_Cert_Type_Sort"]);
                    entity.Member_Cert_Type_IsActive = Tools.NullInt(RdrList["Member_Cert_Type_IsActive"]);
                    entity.Member_Cert_Type_Site = Tools.NullStr(RdrList["Member_Cert_Type_Site"]);

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

        public virtual IList<MemberCertTypeInfo> GetMemberCertTypes(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<MemberCertTypeInfo> entitys = null;
            MemberCertTypeInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Member_Cert_Type";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<MemberCertTypeInfo>();
                    while (RdrList.Read())
                    {
                        entity = new MemberCertTypeInfo();
                        entity.Member_Cert_Type_ID = Tools.NullInt(RdrList["Member_Cert_Type_ID"]);
                        entity.Member_Cert_Type_Name = Tools.NullStr(RdrList["Member_Cert_Type_Name"]);
                        entity.Member_Cert_Type_Sort = Tools.NullInt(RdrList["Member_Cert_Type_Sort"]);
                        entity.Member_Cert_Type_IsActive = Tools.NullInt(RdrList["Member_Cert_Type_IsActive"]);
                        entity.Member_Cert_Type_Site = Tools.NullStr(RdrList["Member_Cert_Type_Site"]);

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
                SqlTable = "Member_Cert_Type";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Member_Cert_Type_ID) FROM " + SqlTable + SqlParam;

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

    public class MemberCert : IMemberCert
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public MemberCert()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddMemberCert(MemberCertInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Member_Cert";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Member_Cert_ID"] = entity.Member_Cert_ID;
            DrAdd["Member_Cert_Type"] = entity.Member_Cert_Type;
            DrAdd["Member_Cert_Name"] = entity.Member_Cert_Name;
            DrAdd["Member_Cert_Note"] = entity.Member_Cert_Note;
            DrAdd["Member_Cert_Sort"] = entity.Member_Cert_Sort;
            DrAdd["Member_Cert_Addtime"] = entity.Member_Cert_Addtime;
            DrAdd["Member_Cert_Site"] = entity.Member_Cert_Site;

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

        public virtual bool EditMemberCert(MemberCertInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Member_Cert WHERE Member_Cert_ID = " + entity.Member_Cert_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Member_Cert_ID"] = entity.Member_Cert_ID;
                    DrAdd["Member_Cert_Type"] = entity.Member_Cert_Type;
                    DrAdd["Member_Cert_Name"] = entity.Member_Cert_Name;
                    DrAdd["Member_Cert_Note"] = entity.Member_Cert_Note;
                    DrAdd["Member_Cert_Sort"] = entity.Member_Cert_Sort;
                    DrAdd["Member_Cert_Addtime"] = entity.Member_Cert_Addtime;
                    DrAdd["Member_Cert_Site"] = entity.Member_Cert_Site;

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

        public virtual int DelMemberCert(int ID)
        {
            string SqlAdd = "DELETE FROM Member_Cert WHERE Member_Cert_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual MemberCertInfo GetMemberCertByID(int ID)
        {
            MemberCertInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member_Cert WHERE Member_Cert_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new MemberCertInfo();

                    entity.Member_Cert_ID = Tools.NullInt(RdrList["Member_Cert_ID"]);
                    entity.Member_Cert_Type = Tools.NullInt(RdrList["Member_Cert_Type"]);
                    entity.Member_Cert_Name = Tools.NullStr(RdrList["Member_Cert_Name"]);
                    entity.Member_Cert_Note = Tools.NullStr(RdrList["Member_Cert_Note"]);
                    entity.Member_Cert_Sort = Tools.NullInt(RdrList["Member_Cert_Sort"]);
                    entity.Member_Cert_Addtime = Tools.NullDate(RdrList["Member_Cert_Addtime"]);
                    entity.Member_Cert_Site = Tools.NullStr(RdrList["Member_Cert_Site"]);

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

        public virtual IList<MemberCertInfo> GetMemberCerts(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<MemberCertInfo> entitys = null;
            MemberCertInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Member_Cert";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<MemberCertInfo>();
                    while (RdrList.Read())
                    {
                        entity = new MemberCertInfo();
                        entity.Member_Cert_ID = Tools.NullInt(RdrList["Member_Cert_ID"]);
                        entity.Member_Cert_Type = Tools.NullInt(RdrList["Member_Cert_Type"]);
                        entity.Member_Cert_Name = Tools.NullStr(RdrList["Member_Cert_Name"]);
                        entity.Member_Cert_Note = Tools.NullStr(RdrList["Member_Cert_Note"]);
                        entity.Member_Cert_Sort = Tools.NullInt(RdrList["Member_Cert_Sort"]);
                        entity.Member_Cert_Addtime = Tools.NullDate(RdrList["Member_Cert_Addtime"]);
                        entity.Member_Cert_Site = Tools.NullStr(RdrList["Member_Cert_Site"]);

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
                SqlTable = "Member_Cert";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Member_Cert_ID) FROM " + SqlTable + SqlParam;

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
