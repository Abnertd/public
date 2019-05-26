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
    public class ZhongXin : IZhongXin
    {
        ITools Tools;
        ISQLHelper DBHelper;

        public ZhongXin()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddZhongXin(ZhongXinInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM ZhongXin";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["ID"] = entity.ID;
            DrAdd["SupplierID"] = entity.SupplierID;
            DrAdd["CompanyName"] = entity.CompanyName;
            DrAdd["ReceiptAccount"] = entity.ReceiptAccount;
            DrAdd["ReceiptBank"] = entity.ReceiptBank;
            DrAdd["BankCode"] = entity.BankCode;
            DrAdd["BankName"] = entity.BankName;
            DrAdd["OpenAccountName"] = entity.OpenAccountName;
            DrAdd["SubAccount"] = entity.SubAccount;
            DrAdd["Audit"] = entity.Audit;
            DrAdd["Register"] = entity.Register;
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

        public virtual bool EditZhongXin(ZhongXinInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM ZhongXin WHERE ID = " + entity.ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["ID"] = entity.ID;
                    DrAdd["SupplierID"] = entity.SupplierID;
                    DrAdd["CompanyName"] = entity.CompanyName;
                    DrAdd["ReceiptAccount"] = entity.ReceiptAccount;
                    DrAdd["ReceiptBank"] = entity.ReceiptBank;
                    DrAdd["BankCode"] = entity.BankCode;
                    DrAdd["BankName"] = entity.BankName;
                    DrAdd["OpenAccountName"] = entity.OpenAccountName;
                    DrAdd["SubAccount"] = entity.SubAccount;
                    DrAdd["Audit"] = entity.Audit;
                    DrAdd["Register"] = entity.Register;
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

        public virtual int DelZhongXin(int ID)
        {
            string SqlAdd = "DELETE FROM ZhongXin WHERE ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ZhongXinInfo GetZhongXinByID(int ID)
        {
            ZhongXinInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM ZhongXin WHERE ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ZhongXinInfo();

                    entity.ID = Tools.NullInt(RdrList["ID"]);
                    entity.SupplierID = Tools.NullInt(RdrList["SupplierID"]);
                    entity.CompanyName = Tools.NullStr(RdrList["CompanyName"]);
                    entity.ReceiptAccount = Tools.NullStr(RdrList["ReceiptAccount"]);
                    entity.ReceiptBank = Tools.NullStr(RdrList["ReceiptBank"]);
                    entity.BankCode = Tools.NullStr(RdrList["BankCode"]);
                    entity.BankName = Tools.NullStr(RdrList["BankName"]);
                    entity.OpenAccountName = Tools.NullStr(RdrList["OpenAccountName"]);
                    entity.SubAccount = Tools.NullStr(RdrList["SubAccount"]);
                    entity.Audit = Tools.NullInt(RdrList["Audit"]);
                    entity.Register = Tools.NullInt(RdrList["Register"]);
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

        public virtual ZhongXinInfo GetZhongXinBySuppleir(int ID)
        {
            ZhongXinInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM ZhongXin WHERE SupplierID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ZhongXinInfo();

                    entity.ID = Tools.NullInt(RdrList["ID"]);
                    entity.SupplierID = Tools.NullInt(RdrList["SupplierID"]);
                    entity.CompanyName = Tools.NullStr(RdrList["CompanyName"]);
                    entity.ReceiptAccount = Tools.NullStr(RdrList["ReceiptAccount"]);
                    entity.ReceiptBank = Tools.NullStr(RdrList["ReceiptBank"]);
                    entity.BankCode = Tools.NullStr(RdrList["BankCode"]);
                    entity.BankName = Tools.NullStr(RdrList["BankName"]);
                    entity.OpenAccountName = Tools.NullStr(RdrList["OpenAccountName"]);
                    entity.SubAccount = Tools.NullStr(RdrList["SubAccount"]);
                    entity.Audit = Tools.NullInt(RdrList["Audit"]);
                    entity.Register = Tools.NullInt(RdrList["Register"]);
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

        public virtual IList<ZhongXinInfo> GetZhongXins(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ZhongXinInfo> entitys = null;
            ZhongXinInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "ZhongXin";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ZhongXinInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ZhongXinInfo();
                        entity.ID = Tools.NullInt(RdrList["ID"]);
                        entity.SupplierID = Tools.NullInt(RdrList["SupplierID"]);
                        entity.CompanyName = Tools.NullStr(RdrList["CompanyName"]);
                        entity.ReceiptAccount = Tools.NullStr(RdrList["ReceiptAccount"]);
                        entity.ReceiptBank = Tools.NullStr(RdrList["ReceiptBank"]);
                        entity.BankCode = Tools.NullStr(RdrList["BankCode"]);
                        entity.BankName = Tools.NullStr(RdrList["BankName"]);
                        entity.OpenAccountName = Tools.NullStr(RdrList["OpenAccountName"]);
                        entity.SubAccount = Tools.NullStr(RdrList["SubAccount"]);
                        entity.Audit = Tools.NullInt(RdrList["Audit"]);
                        entity.Register = Tools.NullInt(RdrList["Register"]);
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
                SqlTable = "ZhongXin";
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

        #region "中信担保账户明细"

        public virtual bool SaveZhongXinAccountLog(ZhongXinAccountLogInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM ZhongXin_Account_Log";
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

        public virtual double GetZhongXinAccountRemainByMemberID(int MemberID)
        {
            string SqlList = "select top 1 Account_Log_Remain from ZhongXin_Account_Log where Account_Log_MemberID=" + MemberID + " order by Account_Log_ID desc";
            try
            {
                return Tools.NullDbl(DBHelper.ExecuteScalar(SqlList));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion 
    }
}
