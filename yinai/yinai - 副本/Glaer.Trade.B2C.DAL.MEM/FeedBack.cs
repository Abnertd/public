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
    public class FeedBack : IFeedBack
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public FeedBack()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddFeedBack(FeedBackInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Feedback";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Feedback_ID"] = entity.Feedback_ID;
            DrAdd["Feedback_Type"] = entity.Feedback_Type;
            DrAdd["Feedback_SupplierID"] = entity.Feedback_SupplierID;
            DrAdd["Feedback_MemberID"] = entity.Feedback_MemberID;
            DrAdd["Feedback_Name"] = entity.Feedback_Name;
            DrAdd["Feedback_Tel"] = entity.Feedback_Tel;
            DrAdd["Feedback_Email"] = entity.Feedback_Email;
            DrAdd["Feedback_CompanyName"] = entity.Feedback_CompanyName;
            DrAdd["Feedback_Content"] = entity.Feedback_Content;
            DrAdd["Feedback_Attachment"] = entity.Feedback_Attachment;
            DrAdd["Feedback_Addtime"] = entity.Feedback_Addtime;
            DrAdd["Feedback_IsRead"] = entity.Feedback_IsRead;
            DrAdd["Feedback_Reply_IsRead"] = entity.Feedback_Reply_IsRead;
            DrAdd["Feedback_Reply_Content"] = entity.Feedback_Reply_Content;
            DrAdd["Feedback_Site"] = entity.Feedback_Site;


            DrAdd["Feedback_Address"] = entity.Feedback_Address;

            DrAdd["Feedback_Amount"] = entity.Feedback_Amount;

            DrAdd["Feedback_Note"] = entity.Feedback_Note;

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

        public virtual bool EditFeedBack(FeedBackInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Feedback WHERE Feedback_ID = " + entity.Feedback_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Feedback_ID"] = entity.Feedback_ID;
                    DrAdd["Feedback_Type"] = entity.Feedback_Type;
                    DrAdd["Feedback_SupplierID"] = entity.Feedback_SupplierID;
                    DrAdd["Feedback_MemberID"] = entity.Feedback_MemberID;
                    DrAdd["Feedback_Name"] = entity.Feedback_Name;
                    DrAdd["Feedback_Tel"] = entity.Feedback_Tel;
                    DrAdd["Feedback_Email"] = entity.Feedback_Email;
                    DrAdd["Feedback_CompanyName"] = entity.Feedback_CompanyName;
                    DrAdd["Feedback_Content"] = entity.Feedback_Content;
                    DrAdd["Feedback_Attachment"] = entity.Feedback_Attachment;
                    DrAdd["Feedback_Addtime"] = entity.Feedback_Addtime;
                    DrAdd["Feedback_IsRead"] = entity.Feedback_IsRead;
                    DrAdd["Feedback_Reply_IsRead"] = entity.Feedback_Reply_IsRead;
                    DrAdd["Feedback_Reply_Content"] = entity.Feedback_Reply_Content;
                    DrAdd["Feedback_Reply_Addtime"] = entity.Feedback_Reply_Addtime;
                    DrAdd["Feedback_Site"] = entity.Feedback_Site;


                    DrAdd["Feedback_Address"] = entity.Feedback_Address;
                    DrAdd["Feedback_Amount"] = entity.Feedback_Amount;
                    DrAdd["Feedback_Note"] = entity.Feedback_Note;

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

        public virtual bool EditFeedBackReadStatus(int FeedBack_ID,int Read_Status, int Reply_Read_Status)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Feedback WHERE Feedback_ID = " + FeedBack_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Feedback_IsRead"] = Read_Status;
                    DrAdd["Feedback_Reply_IsRead"] = Reply_Read_Status;

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

        public virtual int DelFeedBack(int ID)
        {
            string SqlAdd = "DELETE FROM Feedback WHERE Feedback_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual FeedBackInfo GetFeedBackByID(int ID)
        {
            FeedBackInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Feedback WHERE Feedback_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new FeedBackInfo();

                    entity.Feedback_ID = Tools.NullInt(RdrList["Feedback_ID"]);
                    entity.Feedback_Type = Tools.NullInt(RdrList["Feedback_Type"]);
                    entity.Feedback_SupplierID = Tools.NullInt(RdrList["Feedback_SupplierID"]); 
                    entity.Feedback_MemberID = Tools.NullInt(RdrList["Feedback_MemberID"]);
                    entity.Feedback_Name = Tools.NullStr(RdrList["Feedback_Name"]);
                    entity.Feedback_Tel = Tools.NullStr(RdrList["Feedback_Tel"]);
                    entity.Feedback_Email = Tools.NullStr(RdrList["Feedback_Email"]);
                    entity.Feedback_CompanyName = Tools.NullStr(RdrList["Feedback_CompanyName"]);
                    entity.Feedback_Content = Tools.NullStr(RdrList["Feedback_Content"]);
                    entity.Feedback_Attachment = Tools.NullStr(RdrList["Feedback_Attachment"]);
                    entity.Feedback_Addtime = Tools.NullDate(RdrList["Feedback_Addtime"]);
                    entity.Feedback_IsRead = Tools.NullInt(RdrList["Feedback_IsRead"]);
                    entity.Feedback_Reply_IsRead = Tools.NullInt(RdrList["Feedback_Reply_IsRead"]);
                    entity.Feedback_Reply_Content = Tools.NullStr(RdrList["Feedback_Reply_Content"]);
                    entity.Feedback_Reply_Addtime = Tools.NullDate(RdrList["Feedback_Reply_Addtime"]);
                    entity.Feedback_Site = Tools.NullStr(RdrList["Feedback_Site"]);


                    entity.Feedback_Address = Tools.NullStr(RdrList["Feedback_Address"]);
                    entity.Feedback_Amount = Tools.NullDbl(RdrList["Feedback_Amount"]);
                    entity.Feedback_Note = Tools.NullStr(RdrList["Feedback_Note"]);

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

        public virtual IList<FeedBackInfo> GetFeedBacks(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<FeedBackInfo> entitys = null;
            FeedBackInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Feedback";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<FeedBackInfo>();
                    while (RdrList.Read())
                    {
                        entity = new FeedBackInfo();
                        entity.Feedback_ID = Tools.NullInt(RdrList["Feedback_ID"]);
                        entity.Feedback_Type = Tools.NullInt(RdrList["Feedback_Type"]);
                        entity.Feedback_SupplierID = Tools.NullInt(RdrList["Feedback_SupplierID"]); 
                        entity.Feedback_MemberID = Tools.NullInt(RdrList["Feedback_MemberID"]);
                        entity.Feedback_Name = Tools.NullStr(RdrList["Feedback_Name"]);
                        entity.Feedback_Tel = Tools.NullStr(RdrList["Feedback_Tel"]);
                        entity.Feedback_Email = Tools.NullStr(RdrList["Feedback_Email"]);
                        entity.Feedback_CompanyName = Tools.NullStr(RdrList["Feedback_CompanyName"]);
                        entity.Feedback_Content = Tools.NullStr(RdrList["Feedback_Content"]);
                        entity.Feedback_Attachment = Tools.NullStr(RdrList["Feedback_Attachment"]);
                        entity.Feedback_Addtime = Tools.NullDate(RdrList["Feedback_Addtime"]);
                        entity.Feedback_IsRead = Tools.NullInt(RdrList["Feedback_IsRead"]);
                        entity.Feedback_Reply_IsRead = Tools.NullInt(RdrList["Feedback_Reply_IsRead"]);
                        entity.Feedback_Reply_Content = Tools.NullStr(RdrList["Feedback_Reply_Content"]);
                        entity.Feedback_Reply_Addtime = Tools.NullDate(RdrList["Feedback_Reply_Addtime"]);
                        entity.Feedback_Site = Tools.NullStr(RdrList["Feedback_Site"]);

                        entity.Feedback_Address = Tools.NullStr(RdrList["Feedback_Address"]);
                        entity.Feedback_Amount = Tools.NullDbl(RdrList["Feedback_Amount"]);
                        entity.Feedback_Note = Tools.NullStr(RdrList["Feedback_Note"]);

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
                SqlTable = "Feedback";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Feedback_ID) FROM " + SqlTable + SqlParam;

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
