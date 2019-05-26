using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;

namespace Glaer.Trade.B2C.DAL.ORD
{
    public class ContractPayment : IContractPayment
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public ContractPayment()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddContractPayment(ContractPaymentInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Contract_Payment";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Contract_Payment_ID"] = entity.Contract_Payment_ID;
            DrAdd["Contract_Payment_ContractID"] = entity.Contract_Payment_ContractID;
            DrAdd["Contract_Payment_PaymentStatus"] = entity.Contract_Payment_PaymentStatus;
            DrAdd["Contract_Payment_SysUserID"] = entity.Contract_Payment_SysUserID;
            DrAdd["Contract_Payment_DocNo"] = entity.Contract_Payment_DocNo;
            DrAdd["Contract_Payment_Name"] = entity.Contract_Payment_Name;
            DrAdd["Contract_Payment_Amount"] = entity.Contract_Payment_Amount;
            DrAdd["Contract_Payment_Note"] = entity.Contract_Payment_Note;
            DrAdd["Contract_Payment_Addtime"] = entity.Contract_Payment_Addtime;
            DrAdd["Contract_Payment_Site"] = entity.Contract_Payment_Site;
            DrAdd["Contract_Payment_Attachment"] = entity.Contract_Payment_Attachment;

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

        public virtual bool EditContractPayment(ContractPaymentInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Contract_Payment WHERE Contract_Payment_ID = " + entity.Contract_Payment_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Contract_Payment_ID"] = entity.Contract_Payment_ID;
                    DrAdd["Contract_Payment_ContractID"] = entity.Contract_Payment_ContractID;
                    DrAdd["Contract_Payment_PaymentStatus"] = entity.Contract_Payment_PaymentStatus;
                    DrAdd["Contract_Payment_SysUserID"] = entity.Contract_Payment_SysUserID;
                    DrAdd["Contract_Payment_DocNo"] = entity.Contract_Payment_DocNo;
                    DrAdd["Contract_Payment_Name"] = entity.Contract_Payment_Name;
                    DrAdd["Contract_Payment_Amount"] = entity.Contract_Payment_Amount;
                    DrAdd["Contract_Payment_Note"] = entity.Contract_Payment_Note;
                    DrAdd["Contract_Payment_Addtime"] = entity.Contract_Payment_Addtime;
                    DrAdd["Contract_Payment_Site"] = entity.Contract_Payment_Site;
                    DrAdd["Contract_Payment_Attachment"] = entity.Contract_Payment_Attachment;

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

        public virtual int DelContractPayment(int ID)
        {
            string SqlAdd = "DELETE FROM Contract_Payment WHERE Contract_Payment_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ContractPaymentInfo GetContractPaymentByID(int ID)
        {
            ContractPaymentInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Contract_Payment WHERE Contract_Payment_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ContractPaymentInfo();

                    entity.Contract_Payment_ID = Tools.NullInt(RdrList["Contract_Payment_ID"]);
                    entity.Contract_Payment_ContractID = Tools.NullInt(RdrList["Contract_Payment_ContractID"]);
                    entity.Contract_Payment_PaymentStatus = Tools.NullInt(RdrList["Contract_Payment_PaymentStatus"]);
                    entity.Contract_Payment_SysUserID = Tools.NullInt(RdrList["Contract_Payment_SysUserID"]);
                    entity.Contract_Payment_DocNo = Tools.NullStr(RdrList["Contract_Payment_DocNo"]);
                    entity.Contract_Payment_Name = Tools.NullStr(RdrList["Contract_Payment_Name"]);
                    entity.Contract_Payment_Amount = Tools.NullDbl(RdrList["Contract_Payment_Amount"]);
                    entity.Contract_Payment_Note = Tools.NullStr(RdrList["Contract_Payment_Note"]);
                    entity.Contract_Payment_Addtime = Tools.NullDate(RdrList["Contract_Payment_Addtime"]);
                    entity.Contract_Payment_Site = Tools.NullStr(RdrList["Contract_Payment_Site"]);
                    entity.Contract_Payment_Attachment = Tools.NullStr(RdrList["Contract_Payment_Attachment"]);
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

        public virtual ContractPaymentInfo GetContractPaymentBySN(string SN)
        {
            ContractPaymentInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Contract_Payment WHERE Contract_Payment_DocNo = '" + SN+"'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ContractPaymentInfo();

                    entity.Contract_Payment_ID = Tools.NullInt(RdrList["Contract_Payment_ID"]);
                    entity.Contract_Payment_ContractID = Tools.NullInt(RdrList["Contract_Payment_ContractID"]);
                    entity.Contract_Payment_PaymentStatus = Tools.NullInt(RdrList["Contract_Payment_PaymentStatus"]);
                    entity.Contract_Payment_SysUserID = Tools.NullInt(RdrList["Contract_Payment_SysUserID"]);
                    entity.Contract_Payment_DocNo = Tools.NullStr(RdrList["Contract_Payment_DocNo"]);
                    entity.Contract_Payment_Name = Tools.NullStr(RdrList["Contract_Payment_Name"]);
                    entity.Contract_Payment_Amount = Tools.NullDbl(RdrList["Contract_Payment_Amount"]);
                    entity.Contract_Payment_Note = Tools.NullStr(RdrList["Contract_Payment_Note"]);
                    entity.Contract_Payment_Addtime = Tools.NullDate(RdrList["Contract_Payment_Addtime"]);
                    entity.Contract_Payment_Site = Tools.NullStr(RdrList["Contract_Payment_Site"]);
                    entity.Contract_Payment_Attachment = Tools.NullStr(RdrList["Contract_Payment_Attachment"]);
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

        public virtual IList<ContractPaymentInfo> GetContractPayments(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ContractPaymentInfo> entitys = null;
            ContractPaymentInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Contract_Payment";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ContractPaymentInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ContractPaymentInfo();
                        entity.Contract_Payment_ID = Tools.NullInt(RdrList["Contract_Payment_ID"]);
                        entity.Contract_Payment_ContractID = Tools.NullInt(RdrList["Contract_Payment_ContractID"]);
                        entity.Contract_Payment_PaymentStatus = Tools.NullInt(RdrList["Contract_Payment_PaymentStatus"]);
                        entity.Contract_Payment_SysUserID = Tools.NullInt(RdrList["Contract_Payment_SysUserID"]);
                        entity.Contract_Payment_DocNo = Tools.NullStr(RdrList["Contract_Payment_DocNo"]);
                        entity.Contract_Payment_Name = Tools.NullStr(RdrList["Contract_Payment_Name"]);
                        entity.Contract_Payment_Amount = Tools.NullDbl(RdrList["Contract_Payment_Amount"]);
                        entity.Contract_Payment_Note = Tools.NullStr(RdrList["Contract_Payment_Note"]);
                        entity.Contract_Payment_Addtime = Tools.NullDate(RdrList["Contract_Payment_Addtime"]);
                        entity.Contract_Payment_Site = Tools.NullStr(RdrList["Contract_Payment_Site"]);
                        entity.Contract_Payment_Attachment = Tools.NullStr(RdrList["Contract_Payment_Attachment"]);

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

        public virtual IList<ContractPaymentInfo> GetContractPaymentsByContractID(int ID)
        {
            IList<ContractPaymentInfo> entitys = null;
            ContractPaymentInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "select * from Contract_Payment where Contract_Payment_ContractID=" + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ContractPaymentInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ContractPaymentInfo();
                        entity.Contract_Payment_ID = Tools.NullInt(RdrList["Contract_Payment_ID"]);
                        entity.Contract_Payment_ContractID = Tools.NullInt(RdrList["Contract_Payment_ContractID"]);
                        entity.Contract_Payment_PaymentStatus = Tools.NullInt(RdrList["Contract_Payment_PaymentStatus"]);
                        entity.Contract_Payment_SysUserID = Tools.NullInt(RdrList["Contract_Payment_SysUserID"]);
                        entity.Contract_Payment_DocNo = Tools.NullStr(RdrList["Contract_Payment_DocNo"]);
                        entity.Contract_Payment_Name = Tools.NullStr(RdrList["Contract_Payment_Name"]);
                        entity.Contract_Payment_Amount = Tools.NullDbl(RdrList["Contract_Payment_Amount"]);
                        entity.Contract_Payment_Note = Tools.NullStr(RdrList["Contract_Payment_Note"]);
                        entity.Contract_Payment_Addtime = Tools.NullDate(RdrList["Contract_Payment_Addtime"]);
                        entity.Contract_Payment_Site = Tools.NullStr(RdrList["Contract_Payment_Site"]);
                        entity.Contract_Payment_Attachment = Tools.NullStr(RdrList["Contract_Payment_Attachment"]);

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
                SqlTable = "Contract_Payment";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Contract_Payment_ID) FROM " + SqlTable + SqlParam;

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
