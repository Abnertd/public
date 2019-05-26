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
    public class ContractDelivery : IContractDelivery
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public ContractDelivery()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddContractDelivery(ContractDeliveryInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Contract_Delivery";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Contract_Delivery_ID"] = entity.Contract_Delivery_ID;
            DrAdd["Contract_Delivery_ContractID"] = entity.Contract_Delivery_ContractID;
            DrAdd["Contract_Delivery_DeliveryStatus"] = entity.Contract_Delivery_DeliveryStatus;
            DrAdd["Contract_Delivery_SysUserID"] = entity.Contract_Delivery_SysUserID;
            DrAdd["Contract_Delivery_DocNo"] = entity.Contract_Delivery_DocNo;
            DrAdd["Contract_Delivery_Name"] = entity.Contract_Delivery_Name;
            DrAdd["Contract_Delivery_CompanyName"] = entity.Contract_Delivery_CompanyName;
            DrAdd["Contract_Delivery_Code"] = entity.Contract_Delivery_Code;
            DrAdd["Contract_Delivery_Amount"] = entity.Contract_Delivery_Amount;
            DrAdd["Contract_Delivery_Note"] = entity.Contract_Delivery_Note;
            DrAdd["Contract_Delivery_AccpetNote"] = entity.Contract_Delivery_AccpetNote;
            DrAdd["Contract_Delivery_Addtime"] = entity.Contract_Delivery_Addtime;
            DrAdd["Contract_Delivery_Site"] = entity.Contract_Delivery_Site;

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

        public virtual bool EditContractDelivery(ContractDeliveryInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Contract_Delivery WHERE Contract_Delivery_ID = " + entity.Contract_Delivery_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Contract_Delivery_ID"] = entity.Contract_Delivery_ID;
                    DrAdd["Contract_Delivery_ContractID"] = entity.Contract_Delivery_ContractID;
                    DrAdd["Contract_Delivery_DeliveryStatus"] = entity.Contract_Delivery_DeliveryStatus;
                    DrAdd["Contract_Delivery_SysUserID"] = entity.Contract_Delivery_SysUserID;
                    DrAdd["Contract_Delivery_DocNo"] = entity.Contract_Delivery_DocNo;
                    DrAdd["Contract_Delivery_Name"] = entity.Contract_Delivery_Name;
                    DrAdd["Contract_Delivery_CompanyName"] = entity.Contract_Delivery_CompanyName;
                    DrAdd["Contract_Delivery_Code"] = entity.Contract_Delivery_Code;
                    DrAdd["Contract_Delivery_Amount"] = entity.Contract_Delivery_Amount;
                    DrAdd["Contract_Delivery_Note"] = entity.Contract_Delivery_Note;
                    DrAdd["Contract_Delivery_AccpetNote"] = entity.Contract_Delivery_AccpetNote;
                    DrAdd["Contract_Delivery_Addtime"] = entity.Contract_Delivery_Addtime;
                    DrAdd["Contract_Delivery_Site"] = entity.Contract_Delivery_Site;

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

        public virtual int DelContractDelivery(int ID)
        {
            string SqlAdd = "DELETE FROM Contract_Delivery WHERE Contract_Delivery_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual ContractDeliveryInfo GetContractDeliveryByID(int ID)
        {
            ContractDeliveryInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Contract_Delivery WHERE Contract_Delivery_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ContractDeliveryInfo();

                    entity.Contract_Delivery_ID = Tools.NullInt(RdrList["Contract_Delivery_ID"]);
                    entity.Contract_Delivery_ContractID = Tools.NullInt(RdrList["Contract_Delivery_ContractID"]);
                    entity.Contract_Delivery_DeliveryStatus = Tools.NullInt(RdrList["Contract_Delivery_DeliveryStatus"]);
                    entity.Contract_Delivery_SysUserID = Tools.NullInt(RdrList["Contract_Delivery_SysUserID"]);
                    entity.Contract_Delivery_DocNo = Tools.NullStr(RdrList["Contract_Delivery_DocNo"]);
                    entity.Contract_Delivery_Name = Tools.NullStr(RdrList["Contract_Delivery_Name"]);
                    entity.Contract_Delivery_CompanyName = Tools.NullStr(RdrList["Contract_Delivery_CompanyName"]);
                    entity.Contract_Delivery_Code = Tools.NullStr(RdrList["Contract_Delivery_Code"]);
                    entity.Contract_Delivery_Amount = Tools.NullDbl(RdrList["Contract_Delivery_Amount"]);
                    entity.Contract_Delivery_Note = Tools.NullStr(RdrList["Contract_Delivery_Note"]);
                    entity.Contract_Delivery_AccpetNote = Tools.NullStr(RdrList["Contract_Delivery_AccpetNote"]);
                    entity.Contract_Delivery_Addtime = Tools.NullDate(RdrList["Contract_Delivery_Addtime"]);
                    entity.Contract_Delivery_Site = Tools.NullStr(RdrList["Contract_Delivery_Site"]);

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

        public virtual ContractDeliveryInfo GetContractDeliveryBySN(string SN)
        {
            ContractDeliveryInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Contract_Delivery WHERE Contract_Delivery_DocNo = '" + SN+"'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ContractDeliveryInfo();

                    entity.Contract_Delivery_ID = Tools.NullInt(RdrList["Contract_Delivery_ID"]);
                    entity.Contract_Delivery_ContractID = Tools.NullInt(RdrList["Contract_Delivery_ContractID"]);
                    entity.Contract_Delivery_DeliveryStatus = Tools.NullInt(RdrList["Contract_Delivery_DeliveryStatus"]);
                    entity.Contract_Delivery_SysUserID = Tools.NullInt(RdrList["Contract_Delivery_SysUserID"]);
                    entity.Contract_Delivery_DocNo = Tools.NullStr(RdrList["Contract_Delivery_DocNo"]);
                    entity.Contract_Delivery_Name = Tools.NullStr(RdrList["Contract_Delivery_Name"]);
                    entity.Contract_Delivery_CompanyName = Tools.NullStr(RdrList["Contract_Delivery_CompanyName"]);
                    entity.Contract_Delivery_Code = Tools.NullStr(RdrList["Contract_Delivery_Code"]);
                    entity.Contract_Delivery_Amount = Tools.NullDbl(RdrList["Contract_Delivery_Amount"]);
                    entity.Contract_Delivery_Note = Tools.NullStr(RdrList["Contract_Delivery_Note"]);
                    entity.Contract_Delivery_AccpetNote = Tools.NullStr(RdrList["Contract_Delivery_AccpetNote"]);
                    entity.Contract_Delivery_Addtime = Tools.NullDate(RdrList["Contract_Delivery_Addtime"]);
                    entity.Contract_Delivery_Site = Tools.NullStr(RdrList["Contract_Delivery_Site"]);

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

        public virtual IList<ContractDeliveryInfo> GetContractDeliverys(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ContractDeliveryInfo> entitys = null;
            ContractDeliveryInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Contract_Delivery";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ContractDeliveryInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ContractDeliveryInfo();
                        entity.Contract_Delivery_ID = Tools.NullInt(RdrList["Contract_Delivery_ID"]);
                        entity.Contract_Delivery_ContractID = Tools.NullInt(RdrList["Contract_Delivery_ContractID"]);
                        entity.Contract_Delivery_DeliveryStatus = Tools.NullInt(RdrList["Contract_Delivery_DeliveryStatus"]);
                        entity.Contract_Delivery_SysUserID = Tools.NullInt(RdrList["Contract_Delivery_SysUserID"]);
                        entity.Contract_Delivery_DocNo = Tools.NullStr(RdrList["Contract_Delivery_DocNo"]);
                        entity.Contract_Delivery_Name = Tools.NullStr(RdrList["Contract_Delivery_Name"]);
                        entity.Contract_Delivery_CompanyName = Tools.NullStr(RdrList["Contract_Delivery_CompanyName"]);
                        entity.Contract_Delivery_Code = Tools.NullStr(RdrList["Contract_Delivery_Code"]);
                        entity.Contract_Delivery_Amount = Tools.NullDbl(RdrList["Contract_Delivery_Amount"]);
                        entity.Contract_Delivery_Note = Tools.NullStr(RdrList["Contract_Delivery_Note"]);
                        entity.Contract_Delivery_AccpetNote = Tools.NullStr(RdrList["Contract_Delivery_AccpetNote"]);
                        entity.Contract_Delivery_Addtime = Tools.NullDate(RdrList["Contract_Delivery_Addtime"]);
                        entity.Contract_Delivery_Site = Tools.NullStr(RdrList["Contract_Delivery_Site"]);

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

        public virtual IList<ContractDeliveryInfo> GetContractDeliverysByContractID(int ContractID)
        {
            IList<ContractDeliveryInfo> entitys = null;
            ContractDeliveryInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "select * from Contract_Delivery where Contract_Delivery_ContractID=" + ContractID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ContractDeliveryInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ContractDeliveryInfo();
                        entity.Contract_Delivery_ID = Tools.NullInt(RdrList["Contract_Delivery_ID"]);
                        entity.Contract_Delivery_ContractID = Tools.NullInt(RdrList["Contract_Delivery_ContractID"]);
                        entity.Contract_Delivery_DeliveryStatus = Tools.NullInt(RdrList["Contract_Delivery_DeliveryStatus"]);
                        entity.Contract_Delivery_SysUserID = Tools.NullInt(RdrList["Contract_Delivery_SysUserID"]);
                        entity.Contract_Delivery_DocNo = Tools.NullStr(RdrList["Contract_Delivery_DocNo"]);
                        entity.Contract_Delivery_Name = Tools.NullStr(RdrList["Contract_Delivery_Name"]);
                        entity.Contract_Delivery_CompanyName = Tools.NullStr(RdrList["Contract_Delivery_CompanyName"]);
                        entity.Contract_Delivery_Code = Tools.NullStr(RdrList["Contract_Delivery_Code"]);
                        entity.Contract_Delivery_Amount = Tools.NullDbl(RdrList["Contract_Delivery_Amount"]);
                        entity.Contract_Delivery_Note = Tools.NullStr(RdrList["Contract_Delivery_Note"]);
                        entity.Contract_Delivery_AccpetNote = Tools.NullStr(RdrList["Contract_Delivery_AccpetNote"]);
                        entity.Contract_Delivery_Addtime = Tools.NullDate(RdrList["Contract_Delivery_Addtime"]);
                        entity.Contract_Delivery_Site = Tools.NullStr(RdrList["Contract_Delivery_Site"]);

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
                SqlTable = "Contract_Delivery";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Contract_Delivery_ID) FROM " + SqlTable + SqlParam;

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

        public virtual int Get_Orders_Goods_DeliveryAmount(int Goods_ID)
        {
            string SqlAdd = "Select Sum(Delivery_Goods_Amount) as amount FROM Contract_Delivery_Goods WHERE Delivery_Goods_GoodsID = " + Goods_ID;
            try
            {
                return Tools.NullInt(DBHelper.ExecuteScalar(SqlAdd));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual bool AddContractDeliveryGoods(ContractDeliveryGoodsInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Contract_Delivery_Goods";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Delivery_Goods_ID"] = entity.Delivery_Goods_ID;
            DrAdd["Delivery_Goods_GoodsID"] = entity.Delivery_Goods_GoodsID;
            DrAdd["Delivery_Goods_DeliveryID"] = entity.Delivery_Goods_DeliveryID;
            DrAdd["Delivery_Goods_Amount"] = entity.Delivery_Goods_Amount;
            DrAdd["Delivery_Goods_Status"] = entity.Delivery_Goods_Status;
            DrAdd["Delivery_Goods_AcceptAmount"] = entity.Delivery_Goods_AcceptAmount;
            DrAdd["Delivery_Goods_Unit"] = entity.Delivery_Goods_Unit;

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

        public virtual bool EditContractDeliveryGoods(ContractDeliveryGoodsInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Contract_Delivery_Goods WHERE Delivery_Goods_ID = " + entity.Delivery_Goods_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Delivery_Goods_ID"] = entity.Delivery_Goods_ID;
                    DrAdd["Delivery_Goods_GoodsID"] = entity.Delivery_Goods_GoodsID;
                    DrAdd["Delivery_Goods_DeliveryID"] = entity.Delivery_Goods_DeliveryID;
                    DrAdd["Delivery_Goods_Amount"] = entity.Delivery_Goods_Amount;
                    DrAdd["Delivery_Goods_Status"] = entity.Delivery_Goods_Status;
                    DrAdd["Delivery_Goods_AcceptAmount"] = entity.Delivery_Goods_AcceptAmount;
                    DrAdd["Delivery_Goods_Unit"] = entity.Delivery_Goods_Unit;

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

        public virtual ContractDeliveryGoodsInfo GetContractDeliveryGoodsByID(int ID)
        {
            ContractDeliveryGoodsInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Contract_Delivery_Goods WHERE Delivery_Goods_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new ContractDeliveryGoodsInfo();

                    entity.Delivery_Goods_ID = Tools.NullInt(RdrList["Delivery_Goods_ID"]);
                    entity.Delivery_Goods_GoodsID = Tools.NullInt(RdrList["Delivery_Goods_GoodsID"]);
                    entity.Delivery_Goods_DeliveryID = Tools.NullInt(RdrList["Delivery_Goods_DeliveryID"]);
                    entity.Delivery_Goods_Amount = Tools.NullInt(RdrList["Delivery_Goods_Amount"]);
                    entity.Delivery_Goods_Status = Tools.NullInt(RdrList["Delivery_Goods_Status"]);
                    entity.Delivery_Goods_AcceptAmount = Tools.NullInt(RdrList["Delivery_Goods_AcceptAmount"]);
                    entity.Delivery_Goods_Unit = Tools.NullStr(RdrList["Delivery_Goods_Unit"]);

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

        public virtual IList<ContractDeliveryGoodsInfo> GetContractDeliveryGoodss(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<ContractDeliveryGoodsInfo> entitys = null;
            ContractDeliveryGoodsInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Contract_Delivery_Goods";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ContractDeliveryGoodsInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ContractDeliveryGoodsInfo();
                        entity.Delivery_Goods_ID = Tools.NullInt(RdrList["Delivery_Goods_ID"]);
                        entity.Delivery_Goods_GoodsID = Tools.NullInt(RdrList["Delivery_Goods_GoodsID"]);
                        entity.Delivery_Goods_DeliveryID = Tools.NullInt(RdrList["Delivery_Goods_DeliveryID"]);
                        entity.Delivery_Goods_Amount = Tools.NullInt(RdrList["Delivery_Goods_Amount"]);
                        entity.Delivery_Goods_Status = Tools.NullInt(RdrList["Delivery_Goods_Status"]);
                        entity.Delivery_Goods_AcceptAmount = Tools.NullInt(RdrList["Delivery_Goods_AcceptAmount"]);
                        entity.Delivery_Goods_Unit = Tools.NullStr(RdrList["Delivery_Goods_Unit"]);

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

        public virtual IList<ContractDeliveryGoodsInfo> GetContractDeliveryGoodssByDeliveryID(int ID)
        {
            IList<ContractDeliveryGoodsInfo> entitys = null;
            ContractDeliveryGoodsInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "select * from Contract_Delivery_Goods where Delivery_Goods_DeliveryID=" + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<ContractDeliveryGoodsInfo>();
                    while (RdrList.Read())
                    {
                        entity = new ContractDeliveryGoodsInfo();
                        entity.Delivery_Goods_ID = Tools.NullInt(RdrList["Delivery_Goods_ID"]);
                        entity.Delivery_Goods_GoodsID = Tools.NullInt(RdrList["Delivery_Goods_GoodsID"]);
                        entity.Delivery_Goods_DeliveryID = Tools.NullInt(RdrList["Delivery_Goods_DeliveryID"]);
                        entity.Delivery_Goods_Amount = Tools.NullInt(RdrList["Delivery_Goods_Amount"]);
                        entity.Delivery_Goods_Status = Tools.NullInt(RdrList["Delivery_Goods_Status"]);
                        entity.Delivery_Goods_AcceptAmount = Tools.NullInt(RdrList["Delivery_Goods_AcceptAmount"]);
                        entity.Delivery_Goods_Unit = Tools.NullStr(RdrList["Delivery_Goods_Unit"]);

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

    }

}
