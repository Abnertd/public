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
    public class ContractTemplate : IContractTemplate
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public ContractTemplate()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddContractTemplate(ContractTemplateInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Contract_Template";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Contract_Template_ID"] = entity.Contract_Template_ID;
            DrAdd["Contract_Template_Name"] = entity.Contract_Template_Name;
            DrAdd["Contract_Template_Code"] = entity.Contract_Template_Code;
            DrAdd["Contract_Template_Content"] = entity.Contract_Template_Content;
            DrAdd["Contract_Template_Addtime"] = entity.Contract_Template_Addtime;
            DrAdd["Contract_Template_Site"] = entity.Contract_Template_Site;

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

        public virtual bool EditContractTemplate(ContractTemplateInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Contract_Template WHERE Contract_Template_ID = " + entity.Contract_Template_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Contract_Template_ID"] = entity.Contract_Template_ID;
                    DrAdd["Contract_Template_Name"] = entity.Contract_Template_Name;
                    DrAdd["Contract_Template_Code"] = entity.Contract_Template_Code;
                    DrAdd["Contract_Template_Content"] = entity.Contract_Template_Content;
                    DrAdd["Contract_Template_Addtime"] = entity.Contract_Template_Addtime;
                    DrAdd["Contract_Template_Site"] = entity.Contract_Template_Site;

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

        public virtual int DelContractTemplate(int ID)
        {
            string SqlAdd = "DELETE FROM Contract_Template WHERE Contract_Template_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ContractTemplateInfo GetContractTemplateByID(int ID)
        {
            ContractTemplateInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Contract_Template WHERE Contract_Template_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ContractTemplateInfo();

                    entity.Contract_Template_ID = Tools.NullInt(RdrList["Contract_Template_ID"]);
                    entity.Contract_Template_Name = Tools.NullStr(RdrList["Contract_Template_Name"]);
                    entity.Contract_Template_Code = Tools.NullStr(RdrList["Contract_Template_Code"]);
                    entity.Contract_Template_Content = Tools.NullStr(RdrList["Contract_Template_Content"]);
                    entity.Contract_Template_Addtime = Tools.NullDate(RdrList["Contract_Template_Addtime"]);
                    entity.Contract_Template_Site = Tools.NullStr(RdrList["Contract_Template_Site"]);

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

        public virtual ContractTemplateInfo GetContractTemplateBySign(string Sign)
        {
            ContractTemplateInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Contract_Template WHERE Contract_Template_Code = '" + Sign + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ContractTemplateInfo();

                    entity.Contract_Template_ID = Tools.NullInt(RdrList["Contract_Template_ID"]);
                    entity.Contract_Template_Name = Tools.NullStr(RdrList["Contract_Template_Name"]);
                    entity.Contract_Template_Code = Tools.NullStr(RdrList["Contract_Template_Code"]);
                    entity.Contract_Template_Content = Tools.NullStr(RdrList["Contract_Template_Content"]);
                    entity.Contract_Template_Addtime = Tools.NullDate(RdrList["Contract_Template_Addtime"]);
                    entity.Contract_Template_Site = Tools.NullStr(RdrList["Contract_Template_Site"]);

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

        public virtual IList<ContractTemplateInfo> GetContractTemplates(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ContractTemplateInfo> entitys = null;
            ContractTemplateInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Contract_Template";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ContractTemplateInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ContractTemplateInfo();
                        entity.Contract_Template_ID = Tools.NullInt(RdrList["Contract_Template_ID"]);
                        entity.Contract_Template_Name = Tools.NullStr(RdrList["Contract_Template_Name"]);
                        entity.Contract_Template_Code = Tools.NullStr(RdrList["Contract_Template_Code"]);
                        entity.Contract_Template_Content = Tools.NullStr(RdrList["Contract_Template_Content"]);
                        entity.Contract_Template_Addtime = Tools.NullDate(RdrList["Contract_Template_Addtime"]);
                        entity.Contract_Template_Site = Tools.NullStr(RdrList["Contract_Template_Site"]);

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
                SqlTable = "Contract_Template";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Contract_Template_ID) FROM " + SqlTable + SqlParam;

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
