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
    public class Bid : IBid
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public Bid()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddBid(BidInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Bid";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Bid_ID"] = entity.Bid_ID;
            DrAdd["Bid_MemberID"] = entity.Bid_MemberID;
            DrAdd["Bid_MemberCompany"] = entity.Bid_MemberCompany;
            DrAdd["Bid_SupplierID"] = entity.Bid_SupplierID;
            DrAdd["Bid_SupplierCompany"] = entity.Bid_SupplierCompany;
            DrAdd["Bid_Title"] = entity.Bid_Title;
            DrAdd["Bid_EnterStartTime"] = entity.Bid_EnterStartTime;
            DrAdd["Bid_EnterEndTime"] = entity.Bid_EnterEndTime;
            DrAdd["Bid_BidStartTime"] = entity.Bid_BidStartTime;
            DrAdd["Bid_BidEndTime"] = entity.Bid_BidEndTime;
            DrAdd["Bid_AddTime"] = entity.Bid_AddTime;
            DrAdd["Bid_Bond"] = entity.Bid_Bond;
            DrAdd["Bid_Number"] = entity.Bid_Number;
            DrAdd["Bid_Status"] = entity.Bid_Status;
            DrAdd["Bid_Content"] = entity.Bid_Content;
            DrAdd["Bid_ProductType"] = entity.Bid_ProductType;
            DrAdd["Bid_AllPrice"] = entity.Bid_AllPrice;
            DrAdd["Bid_Type"] = entity.Bid_Type;
            DrAdd["Bid_Contract"] = entity.Bid_Contract;
            DrAdd["Bid_IsAudit"] = entity.Bid_IsAudit;
            DrAdd["Bid_AuditTime"] = entity.Bid_AuditTime;
            DrAdd["Bid_AuditRemarks"] = entity.Bid_AuditRemarks;
            DrAdd["Bid_ExcludeSupplierID"] = entity.Bid_ExcludeSupplierID;
            DrAdd["Bid_SN"] = entity.Bid_SN;
            DrAdd["Bid_DeliveryTime"] = entity.Bid_DeliveryTime;
            DrAdd["Bid_IsOrders"] = entity.Bid_IsOrders;
            DrAdd["Bid_OrdersTime"] = entity.Bid_OrdersTime;
            DrAdd["Bid_OrdersSN"] = entity.Bid_OrdersSN;
            DrAdd["Bid_FinishTime"] = entity.Bid_FinishTime;
            DrAdd["Bid_IsShow"] = entity.Bid_IsShow;
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

        public virtual bool EditBid(BidInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Bid WHERE Bid_ID = " + entity.Bid_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Bid_ID"] = entity.Bid_ID;
                    DrAdd["Bid_MemberID"] = entity.Bid_MemberID;
                    DrAdd["Bid_MemberCompany"] = entity.Bid_MemberCompany;
                    DrAdd["Bid_SupplierID"] = entity.Bid_SupplierID;
                    DrAdd["Bid_SupplierCompany"] = entity.Bid_SupplierCompany;
                    DrAdd["Bid_Title"] = entity.Bid_Title;
                    DrAdd["Bid_EnterStartTime"] = entity.Bid_EnterStartTime;
                    DrAdd["Bid_EnterEndTime"] = entity.Bid_EnterEndTime;
                    DrAdd["Bid_BidStartTime"] = entity.Bid_BidStartTime;
                    DrAdd["Bid_BidEndTime"] = entity.Bid_BidEndTime;
                    DrAdd["Bid_AddTime"] = entity.Bid_AddTime;
                    DrAdd["Bid_Bond"] = entity.Bid_Bond;
                    DrAdd["Bid_Number"] = entity.Bid_Number;
                    DrAdd["Bid_Status"] = entity.Bid_Status;
                    DrAdd["Bid_Content"] = entity.Bid_Content;
                    DrAdd["Bid_ProductType"] = entity.Bid_ProductType;
                    DrAdd["Bid_AllPrice"] = entity.Bid_AllPrice;
                    DrAdd["Bid_Type"] = entity.Bid_Type;
                    DrAdd["Bid_Contract"] = entity.Bid_Contract;
                    DrAdd["Bid_IsAudit"] = entity.Bid_IsAudit;
                    DrAdd["Bid_AuditTime"] = entity.Bid_AuditTime;
                    DrAdd["Bid_AuditRemarks"] = entity.Bid_AuditRemarks;
                    DrAdd["Bid_ExcludeSupplierID"] = entity.Bid_ExcludeSupplierID;
                    DrAdd["Bid_SN"] = entity.Bid_SN;
                    DrAdd["Bid_DeliveryTime"] = entity.Bid_DeliveryTime;
                    DrAdd["Bid_IsOrders"] = entity.Bid_IsOrders;
                    DrAdd["Bid_OrdersTime"] = entity.Bid_OrdersTime;
                    DrAdd["Bid_OrdersSN"] = entity.Bid_OrdersSN;
                    DrAdd["Bid_FinishTime"] = entity.Bid_FinishTime;
                    DrAdd["Bid_IsShow"] = entity.Bid_IsShow;

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

        public virtual int DelBid(int ID)
        {
            string SqlAdd = "DELETE FROM Bid WHERE Bid_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual BidInfo GetBidByID(int ID)
        {
            BidInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Bid WHERE Bid_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new BidInfo();

                    entity.Bid_ID = Tools.NullInt(RdrList["Bid_ID"]);
                    entity.Bid_MemberID = Tools.NullInt(RdrList["Bid_MemberID"]);
                    entity.Bid_MemberCompany = Tools.NullStr(RdrList["Bid_MemberCompany"]);
                    entity.Bid_SupplierID = Tools.NullInt(RdrList["Bid_SupplierID"]);
                    entity.Bid_SupplierCompany = Tools.NullStr(RdrList["Bid_SupplierCompany"]);
                    entity.Bid_Title = Tools.NullStr(RdrList["Bid_Title"]);
                    entity.Bid_EnterStartTime = Tools.NullDate(RdrList["Bid_EnterStartTime"]);
                    entity.Bid_EnterEndTime = Tools.NullDate(RdrList["Bid_EnterEndTime"]);
                    entity.Bid_BidStartTime = Tools.NullDate(RdrList["Bid_BidStartTime"]);
                    entity.Bid_BidEndTime = Tools.NullDate(RdrList["Bid_BidEndTime"]);
                    entity.Bid_AddTime = Tools.NullDate(RdrList["Bid_AddTime"]);
                    entity.Bid_Bond = Tools.NullDbl(RdrList["Bid_Bond"]);
                    entity.Bid_Number = Tools.NullInt(RdrList["Bid_Number"]);
                    entity.Bid_Status = Tools.NullInt(RdrList["Bid_Status"]);
                    entity.Bid_Content = Tools.NullStr(RdrList["Bid_Content"]);
                    entity.Bid_ProductType = Tools.NullInt(RdrList["Bid_ProductType"]);
                    entity.Bid_AllPrice = Tools.NullDbl(RdrList["Bid_AllPrice"]);
                    entity.Bid_Type = Tools.NullInt(RdrList["Bid_Type"]);
                    entity.Bid_Contract = Tools.NullStr(RdrList["Bid_Contract"]);
                    entity.Bid_IsAudit = Tools.NullInt(RdrList["Bid_IsAudit"]);
                    entity.Bid_AuditTime = Tools.NullDate(RdrList["Bid_AuditTime"]);
                    entity.Bid_AuditRemarks = Tools.NullStr(RdrList["Bid_AuditRemarks"]);
                    entity.Bid_ExcludeSupplierID = Tools.NullInt(RdrList["Bid_ExcludeSupplierID"]);
                    entity.Bid_SN = Tools.NullStr(RdrList["Bid_SN"]);
                    entity.Bid_DeliveryTime = Tools.NullDate(RdrList["Bid_DeliveryTime"]);
                    entity.Bid_IsOrders = Tools.NullInt(RdrList["Bid_IsOrders"]);
                    entity.Bid_OrdersTime = Tools.NullDate(RdrList["Bid_OrdersTime"]);
                    entity.Bid_OrdersSN = Tools.NullStr(RdrList["Bid_OrdersSN"]);
                    entity.Bid_FinishTime = Tools.NullDate(RdrList["Bid_FinishTime"]);
                    entity.Bid_IsShow = Tools.NullInt(RdrList["Bid_IsShow"]);
                 
                    entity.BidProducts = null;
                    entity.BidAttachments = null;
                }

                if(entity!=null)
                {
                    entity.BidProducts = GetBidProductsByBidID(entity.Bid_ID);
                    entity.BidAttachments = GetBidAttachmentsByBidID(entity.Bid_ID);
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

        public virtual BidInfo GetBidBySN(string SN)
        {
            BidInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Bid WHERE Bid_SN = '" + SN+"'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new BidInfo();

                    entity.Bid_ID = Tools.NullInt(RdrList["Bid_ID"]);
                    entity.Bid_MemberID = Tools.NullInt(RdrList["Bid_MemberID"]);
                    entity.Bid_MemberCompany = Tools.NullStr(RdrList["Bid_MemberCompany"]);
                    entity.Bid_SupplierID = Tools.NullInt(RdrList["Bid_SupplierID"]);
                    entity.Bid_SupplierCompany = Tools.NullStr(RdrList["Bid_SupplierCompany"]);
                    entity.Bid_Title = Tools.NullStr(RdrList["Bid_Title"]);
                    entity.Bid_EnterStartTime = Tools.NullDate(RdrList["Bid_EnterStartTime"]);
                    entity.Bid_EnterEndTime = Tools.NullDate(RdrList["Bid_EnterEndTime"]);
                    entity.Bid_BidStartTime = Tools.NullDate(RdrList["Bid_BidStartTime"]);
                    entity.Bid_BidEndTime = Tools.NullDate(RdrList["Bid_BidEndTime"]);
                    entity.Bid_AddTime = Tools.NullDate(RdrList["Bid_AddTime"]);
                    entity.Bid_Bond = Tools.NullDbl(RdrList["Bid_Bond"]);
                    entity.Bid_Number = Tools.NullInt(RdrList["Bid_Number"]);
                    entity.Bid_Status = Tools.NullInt(RdrList["Bid_Status"]);
                    entity.Bid_Content = Tools.NullStr(RdrList["Bid_Content"]);
                    entity.Bid_ProductType = Tools.NullInt(RdrList["Bid_ProductType"]);
                    entity.Bid_AllPrice = Tools.NullDbl(RdrList["Bid_AllPrice"]);
                    entity.Bid_Type = Tools.NullInt(RdrList["Bid_Type"]);
                    entity.Bid_Contract = Tools.NullStr(RdrList["Bid_Contract"]);
                    entity.Bid_IsAudit = Tools.NullInt(RdrList["Bid_IsAudit"]);
                    entity.Bid_AuditTime = Tools.NullDate(RdrList["Bid_AuditTime"]);
                    entity.Bid_AuditRemarks = Tools.NullStr(RdrList["Bid_AuditRemarks"]);
                    entity.Bid_ExcludeSupplierID = Tools.NullInt(RdrList["Bid_ExcludeSupplierID"]);
                    entity.Bid_SN = Tools.NullStr(RdrList["Bid_SN"]);
                    entity.Bid_DeliveryTime = Tools.NullDate(RdrList["Bid_DeliveryTime"]);
                    entity.Bid_IsOrders = Tools.NullInt(RdrList["Bid_IsOrders"]);
                    entity.Bid_OrdersTime = Tools.NullDate(RdrList["Bid_OrdersTime"]);
                    entity.Bid_OrdersSN = Tools.NullStr(RdrList["Bid_OrdersSN"]);
                    entity.Bid_FinishTime = Tools.NullDate(RdrList["Bid_FinishTime"]);
                    entity.Bid_IsShow = Tools.NullInt(RdrList["Bid_IsShow"]);


                    entity.BidProducts = null;
                    entity.BidAttachments = null;
                }
                if (entity != null)
                {
                    entity.BidProducts = GetBidProductsByBidID(entity.Bid_ID);
                    entity.BidAttachments = GetBidAttachmentsByBidID(entity.Bid_ID);
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
        public virtual IList<BidInfo> GetBids(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<BidInfo> entitys = null;
            BidInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Bid";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<BidInfo>();
                    while (RdrList.Read())
                    {
                        entity = new BidInfo();
                        entity.Bid_ID = Tools.NullInt(RdrList["Bid_ID"]);
                        entity.Bid_MemberID = Tools.NullInt(RdrList["Bid_MemberID"]);
                        entity.Bid_MemberCompany = Tools.NullStr(RdrList["Bid_MemberCompany"]);
                        entity.Bid_SupplierID = Tools.NullInt(RdrList["Bid_SupplierID"]);
                        entity.Bid_SupplierCompany = Tools.NullStr(RdrList["Bid_SupplierCompany"]);
                        entity.Bid_Title = Tools.NullStr(RdrList["Bid_Title"]);
                        entity.Bid_EnterStartTime = Tools.NullDate(RdrList["Bid_EnterStartTime"]);
                        entity.Bid_EnterEndTime = Tools.NullDate(RdrList["Bid_EnterEndTime"]);
                        entity.Bid_BidStartTime = Tools.NullDate(RdrList["Bid_BidStartTime"]);
                        entity.Bid_BidEndTime = Tools.NullDate(RdrList["Bid_BidEndTime"]);
                        entity.Bid_AddTime = Tools.NullDate(RdrList["Bid_AddTime"]);
                        entity.Bid_Bond = Tools.NullDbl(RdrList["Bid_Bond"]);
                        entity.Bid_Number = Tools.NullInt(RdrList["Bid_Number"]);
                        entity.Bid_Status = Tools.NullInt(RdrList["Bid_Status"]);
                        entity.Bid_Content = Tools.NullStr(RdrList["Bid_Content"]);
                        entity.Bid_ProductType = Tools.NullInt(RdrList["Bid_ProductType"]);
                        entity.Bid_AllPrice = Tools.NullDbl(RdrList["Bid_AllPrice"]);
                        entity.Bid_Type = Tools.NullInt(RdrList["Bid_Type"]);
                        entity.Bid_Contract = Tools.NullStr(RdrList["Bid_Contract"]);
                        entity.Bid_IsAudit = Tools.NullInt(RdrList["Bid_IsAudit"]);
                        entity.Bid_AuditTime = Tools.NullDate(RdrList["Bid_AuditTime"]);
                        entity.Bid_AuditRemarks = Tools.NullStr(RdrList["Bid_AuditRemarks"]);
                        entity.Bid_ExcludeSupplierID = Tools.NullInt(RdrList["Bid_ExcludeSupplierID"]);
                        entity.Bid_SN = Tools.NullStr(RdrList["Bid_SN"]);
                        entity.Bid_DeliveryTime = Tools.NullDate(RdrList["Bid_DeliveryTime"]);
                        entity.Bid_IsOrders = Tools.NullInt(RdrList["Bid_IsOrders"]);
                        entity.Bid_OrdersTime = Tools.NullDate(RdrList["Bid_OrdersTime"]);
                        entity.Bid_OrdersSN = Tools.NullStr(RdrList["Bid_OrdersSN"]);
                        entity.Bid_FinishTime = Tools.NullDate(RdrList["Bid_FinishTime"]);
                        entity.Bid_IsShow = Tools.NullInt(RdrList["Bid_IsShow"]);

                        entity.BidProducts = null;
                        entity.BidAttachments = null;

                        if (entity != null)
                        {
                            entity.BidProducts = GetBidProductsByBidID(entity.Bid_ID);
                            entity.BidAttachments = GetBidAttachmentsByBidID(entity.Bid_ID);
                        }

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
                SqlTable = "Bid";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Bid_ID) FROM " + SqlTable + SqlParam;

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

        public virtual IList<BidProductInfo> GetBidProductsByBidID(int Bid_ID)
        {
            IList<BidProductInfo> entitys = null;
            BidProductInfo entity = null;
            SqlDataReader RdrList = null;

            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Bid_Product WHERE Bid_BidID = " + Bid_ID + " Order by Bid_Product_Sort ASC";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<BidProductInfo>();
                    while (RdrList.Read())
                    {
                        entity = new BidProductInfo();
                        entity.Bid_Product_ID = Tools.NullInt(RdrList["Bid_Product_ID"]);
                        entity.Bid_BidID = Tools.NullInt(RdrList["Bid_BidID"]);
                        entity.Bid_Product_Sort = Tools.NullInt(RdrList["Bid_Product_Sort"]);
                        entity.Bid_Product_Code = Tools.NullStr(RdrList["Bid_Product_Code"]);
                        entity.Bid_Product_Name = Tools.NullStr(RdrList["Bid_Product_Name"]);
                        entity.Bid_Product_Spec = Tools.NullStr(RdrList["Bid_Product_Spec"]);
                        entity.Bid_Product_Brand = Tools.NullStr(RdrList["Bid_Product_Brand"]);
                        entity.Bid_Product_Unit = Tools.NullStr(RdrList["Bid_Product_Unit"]);
                        entity.Bid_Product_Amount = Tools.NullInt(RdrList["Bid_Product_Amount"]);
                        entity.Bid_Product_Delivery = Tools.NullStr(RdrList["Bid_Product_Delivery"]);
                        entity.Bid_Product_Remark = Tools.NullStr(RdrList["Bid_Product_Remark"]);
                        entity.Bid_Product_StartPrice = Tools.NullDbl(RdrList["Bid_Product_StartPrice"]);
                        entity.Bid_Product_Img = Tools.NullStr(RdrList["Bid_Product_Img"]);
                        entity.Bid_Product_ProductID = Tools.NullInt(RdrList["Bid_Product_ProductID"]);

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

        public virtual IList<BidAttachmentsInfo> GetBidAttachmentsByBidID(int BidID)
        {
            IList<BidAttachmentsInfo> entitys = null;
            BidAttachmentsInfo entity = null;

            SqlDataReader RdrList = null;

            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Bid_Attachments WHERE Bid_Attachments_BidID = " + BidID + " Order by Bid_Attachments_Sort ASC";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<BidAttachmentsInfo>();
                    while (RdrList.Read())
                    {
                        entity = new BidAttachmentsInfo();
                        entity.Bid_Attachments_ID = Tools.NullInt(RdrList["Bid_Attachments_ID"]);
                        entity.Bid_Attachments_Sort = Tools.NullInt(RdrList["Bid_Attachments_Sort"]);
                        entity.Bid_Attachments_Name = Tools.NullStr(RdrList["Bid_Attachments_Name"]);
                        entity.Bid_Attachments_format = Tools.NullStr(RdrList["Bid_Attachments_format"]);
                        entity.Bid_Attachments_Size = Tools.NullStr(RdrList["Bid_Attachments_Size"]);
                        entity.Bid_Attachments_Remarks = Tools.NullStr(RdrList["Bid_Attachments_Remarks"]);
                        entity.Bid_Attachments_Path = Tools.NullStr(RdrList["Bid_Attachments_Path"]);
                        entity.Bid_Attachments_BidID = Tools.NullInt(RdrList["Bid_Attachments_BidID"]);

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

        public DataTable GetOrderProducts(int BidID)
        {
            DataTable DtAdd = null;
            string SqlList = "";

            try
            {
                SqlList = "select * from Bid_Product join Tender_Product on Bid_Product.Bid_Product_ID=Tender_Product.Tender_Product_BidProductID   where Bid_Product.Bid_BidID = " + BidID + " and Tender_Product.Tender_TenderID in ( select Tender_ID from Tender where Tender.Tender_BidID = " + BidID + " and Tender.Tender_IsWin = 1 ) order by Bid_Product.Bid_Product_Sort asc,Bid_Product_ID asc ";
                DtAdd = DBHelper.Query(SqlList);

                return DtAdd;
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
    }


    public class BidProduct : IBidProduct
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public BidProduct()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddBidProduct(BidProductInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Bid_Product";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Bid_Product_ID"] = entity.Bid_Product_ID;
            DrAdd["Bid_BidID"] = entity.Bid_BidID;
            DrAdd["Bid_Product_Sort"] = entity.Bid_Product_Sort;
            DrAdd["Bid_Product_Code"] = entity.Bid_Product_Code;
            DrAdd["Bid_Product_Name"] = entity.Bid_Product_Name;
            DrAdd["Bid_Product_Spec"] = entity.Bid_Product_Spec;
            DrAdd["Bid_Product_Brand"] = entity.Bid_Product_Brand;
            DrAdd["Bid_Product_Unit"] = entity.Bid_Product_Unit;
            DrAdd["Bid_Product_Amount"] = entity.Bid_Product_Amount;
            DrAdd["Bid_Product_Delivery"] = entity.Bid_Product_Delivery;
            DrAdd["Bid_Product_Remark"] = entity.Bid_Product_Remark;
            DrAdd["Bid_Product_StartPrice"] = entity.Bid_Product_StartPrice;
            DrAdd["Bid_Product_Img"] = entity.Bid_Product_Img;
            DrAdd["Bid_Product_ProductID"] = entity.Bid_Product_ProductID;

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

        public virtual bool EditBidProduct(BidProductInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Bid_Product WHERE Bid_Product_ID = " + entity.Bid_Product_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Bid_Product_ID"] = entity.Bid_Product_ID;
                    DrAdd["Bid_BidID"] = entity.Bid_BidID;
                    DrAdd["Bid_Product_Sort"] = entity.Bid_Product_Sort;
                    DrAdd["Bid_Product_Code"] = entity.Bid_Product_Code;
                    DrAdd["Bid_Product_Name"] = entity.Bid_Product_Name;
                    DrAdd["Bid_Product_Spec"] = entity.Bid_Product_Spec;
                    DrAdd["Bid_Product_Brand"] = entity.Bid_Product_Brand;
                    DrAdd["Bid_Product_Unit"] = entity.Bid_Product_Unit;
                    DrAdd["Bid_Product_Amount"] = entity.Bid_Product_Amount;
                    DrAdd["Bid_Product_Delivery"] = entity.Bid_Product_Delivery;
                    DrAdd["Bid_Product_Remark"] = entity.Bid_Product_Remark;
                    DrAdd["Bid_Product_StartPrice"] = entity.Bid_Product_StartPrice;
                    DrAdd["Bid_Product_Img"] = entity.Bid_Product_Img;
                    DrAdd["Bid_Product_ProductID"] = entity.Bid_Product_ProductID;

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

        public virtual int DelBidProduct(int ID)
        {
            string SqlAdd = "DELETE FROM Bid_Product WHERE Bid_Product_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual BidProductInfo GetBidProductByID(int ID)
        {
            BidProductInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Bid_Product WHERE Bid_Product_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new BidProductInfo();

                    entity.Bid_Product_ID = Tools.NullInt(RdrList["Bid_Product_ID"]);
                    entity.Bid_BidID = Tools.NullInt(RdrList["Bid_BidID"]);
                    entity.Bid_Product_Sort = Tools.NullInt(RdrList["Bid_Product_Sort"]);
                    entity.Bid_Product_Code = Tools.NullStr(RdrList["Bid_Product_Code"]);
                    entity.Bid_Product_Name = Tools.NullStr(RdrList["Bid_Product_Name"]);
                    entity.Bid_Product_Spec = Tools.NullStr(RdrList["Bid_Product_Spec"]);
                    entity.Bid_Product_Brand = Tools.NullStr(RdrList["Bid_Product_Brand"]);
                    entity.Bid_Product_Unit = Tools.NullStr(RdrList["Bid_Product_Unit"]);
                    entity.Bid_Product_Amount = Tools.NullInt(RdrList["Bid_Product_Amount"]);
                    entity.Bid_Product_Delivery = Tools.NullStr(RdrList["Bid_Product_Delivery"]);
                    entity.Bid_Product_Remark = Tools.NullStr(RdrList["Bid_Product_Remark"]);
                    entity.Bid_Product_StartPrice = Tools.NullDbl(RdrList["Bid_Product_StartPrice"]);
                    entity.Bid_Product_Img = Tools.NullStr(RdrList["Bid_Product_Img"]);
                    entity.Bid_Product_ProductID = Tools.NullInt(RdrList["Bid_Product_ProductID"]);

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

        public virtual IList<BidProductInfo> GetBidProducts(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<BidProductInfo> entitys = null;
            BidProductInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Bid_Product";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<BidProductInfo>();
                    while (RdrList.Read())
                    {
                        entity = new BidProductInfo();
                        entity.Bid_Product_ID = Tools.NullInt(RdrList["Bid_Product_ID"]);
                        entity.Bid_BidID = Tools.NullInt(RdrList["Bid_BidID"]);
                        entity.Bid_Product_Sort = Tools.NullInt(RdrList["Bid_Product_Sort"]);
                        entity.Bid_Product_Code = Tools.NullStr(RdrList["Bid_Product_Code"]);
                        entity.Bid_Product_Name = Tools.NullStr(RdrList["Bid_Product_Name"]);
                        entity.Bid_Product_Spec = Tools.NullStr(RdrList["Bid_Product_Spec"]);
                        entity.Bid_Product_Brand = Tools.NullStr(RdrList["Bid_Product_Brand"]);
                        entity.Bid_Product_Unit = Tools.NullStr(RdrList["Bid_Product_Unit"]);
                        entity.Bid_Product_Amount = Tools.NullInt(RdrList["Bid_Product_Amount"]);
                        entity.Bid_Product_Delivery = Tools.NullStr(RdrList["Bid_Product_Delivery"]);
                        entity.Bid_Product_Remark = Tools.NullStr(RdrList["Bid_Product_Remark"]);
                        entity.Bid_Product_StartPrice = Tools.NullDbl(RdrList["Bid_Product_StartPrice"]);
                        entity.Bid_Product_Img = Tools.NullStr(RdrList["Bid_Product_Img"]);
                        entity.Bid_Product_ProductID = Tools.NullInt(RdrList["Bid_Product_ProductID"]);

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
                SqlTable = "Bid_Product";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Bid_Product_ID) FROM " + SqlTable + SqlParam;

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


    public class BidAttachments : IBidAttachments
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public BidAttachments()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddBidAttachments(BidAttachmentsInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Bid_Attachments";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Bid_Attachments_ID"] = entity.Bid_Attachments_ID;
            DrAdd["Bid_Attachments_Sort"] = entity.Bid_Attachments_Sort;
            DrAdd["Bid_Attachments_Name"] = entity.Bid_Attachments_Name;
            DrAdd["Bid_Attachments_format"] = entity.Bid_Attachments_format;
            DrAdd["Bid_Attachments_Size"] = entity.Bid_Attachments_Size;
            DrAdd["Bid_Attachments_Remarks"] = entity.Bid_Attachments_Remarks;
            DrAdd["Bid_Attachments_Path"] = entity.Bid_Attachments_Path;
            DrAdd["Bid_Attachments_BidID"] = entity.Bid_Attachments_BidID;

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

        public virtual bool EditBidAttachments(BidAttachmentsInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Bid_Attachments WHERE Bid_Attachments_ID = " + entity.Bid_Attachments_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Bid_Attachments_ID"] = entity.Bid_Attachments_ID;
                    DrAdd["Bid_Attachments_Sort"] = entity.Bid_Attachments_Sort;
                    DrAdd["Bid_Attachments_Name"] = entity.Bid_Attachments_Name;
                    DrAdd["Bid_Attachments_format"] = entity.Bid_Attachments_format;
                    DrAdd["Bid_Attachments_Size"] = entity.Bid_Attachments_Size;
                    DrAdd["Bid_Attachments_Remarks"] = entity.Bid_Attachments_Remarks;
                    DrAdd["Bid_Attachments_Path"] = entity.Bid_Attachments_Path;
                    DrAdd["Bid_Attachments_BidID"] = entity.Bid_Attachments_BidID;

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

        public virtual int DelBidAttachments(int ID)
        {
            string SqlAdd = "DELETE FROM Bid_Attachments WHERE Bid_Attachments_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual BidAttachmentsInfo GetBidAttachmentsByID(int ID)
        {
            BidAttachmentsInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Bid_Attachments WHERE Bid_Attachments_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new BidAttachmentsInfo();

                    entity.Bid_Attachments_ID = Tools.NullInt(RdrList["Bid_Attachments_ID"]);
                    entity.Bid_Attachments_Sort = Tools.NullInt(RdrList["Bid_Attachments_Sort"]);
                    entity.Bid_Attachments_Name = Tools.NullStr(RdrList["Bid_Attachments_Name"]);
                    entity.Bid_Attachments_format = Tools.NullStr(RdrList["Bid_Attachments_format"]);
                    entity.Bid_Attachments_Size = Tools.NullStr(RdrList["Bid_Attachments_Size"]);
                    entity.Bid_Attachments_Remarks = Tools.NullStr(RdrList["Bid_Attachments_Remarks"]);
                    entity.Bid_Attachments_Path = Tools.NullStr(RdrList["Bid_Attachments_Path"]);
                    entity.Bid_Attachments_BidID = Tools.NullInt(RdrList["Bid_Attachments_BidID"]);

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

        public virtual IList<BidAttachmentsInfo> GetBidAttachmentss(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<BidAttachmentsInfo> entitys = null;
            BidAttachmentsInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Bid_Attachments";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<BidAttachmentsInfo>();
                    while (RdrList.Read())
                    {
                        entity = new BidAttachmentsInfo();
                        entity.Bid_Attachments_ID = Tools.NullInt(RdrList["Bid_Attachments_ID"]);
                        entity.Bid_Attachments_Sort = Tools.NullInt(RdrList["Bid_Attachments_Sort"]);
                        entity.Bid_Attachments_Name = Tools.NullStr(RdrList["Bid_Attachments_Name"]);
                        entity.Bid_Attachments_format = Tools.NullStr(RdrList["Bid_Attachments_format"]);
                        entity.Bid_Attachments_Size = Tools.NullStr(RdrList["Bid_Attachments_Size"]);
                        entity.Bid_Attachments_Remarks = Tools.NullStr(RdrList["Bid_Attachments_Remarks"]);
                        entity.Bid_Attachments_Path = Tools.NullStr(RdrList["Bid_Attachments_Path"]);
                        entity.Bid_Attachments_BidID = Tools.NullInt(RdrList["Bid_Attachments_BidID"]);

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
                SqlTable = "Bid_Attachments";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Bid_Attachments_ID) FROM " + SqlTable + SqlParam;

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


    public class BidEnter : IBidEnter
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public BidEnter()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddBidEnter(BidEnterInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Bid_Enter";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Bid_Enter_ID"] = entity.Bid_Enter_ID;
            DrAdd["Bid_Enter_BidID"] = entity.Bid_Enter_BidID;
            DrAdd["Bid_Enter_SupplierID"] = entity.Bid_Enter_SupplierID;
            DrAdd["Bid_Enter_Bond"] = entity.Bid_Enter_Bond;
            DrAdd["Bid_Enter_Type"] = entity.Bid_Enter_Type;
            DrAdd["Bid_Enter_IsShow"] = entity.Bid_Enter_IsShow;
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

        public virtual bool EditBidEnter(BidEnterInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Bid_Enter WHERE Bid_Enter_ID = " + entity.Bid_Enter_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Bid_Enter_ID"] = entity.Bid_Enter_ID;
                    DrAdd["Bid_Enter_BidID"] = entity.Bid_Enter_BidID;
                    DrAdd["Bid_Enter_SupplierID"] = entity.Bid_Enter_SupplierID;
                    DrAdd["Bid_Enter_Bond"] = entity.Bid_Enter_Bond;
                    DrAdd["Bid_Enter_Type"] = entity.Bid_Enter_Type;
                    DrAdd["Bid_Enter_IsShow"] = entity.Bid_Enter_IsShow;

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

        public virtual int DelBidEnter(int ID)
        {
            string SqlAdd = "DELETE FROM Bid_Enter WHERE Bid_Enter_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual BidEnterInfo GetBidEnterByID(int ID)
        {
            BidEnterInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Bid_Enter WHERE Bid_Enter_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new BidEnterInfo();

                    entity.Bid_Enter_ID = Tools.NullInt(RdrList["Bid_Enter_ID"]);
                    entity.Bid_Enter_BidID = Tools.NullInt(RdrList["Bid_Enter_BidID"]);
                    entity.Bid_Enter_SupplierID = Tools.NullInt(RdrList["Bid_Enter_SupplierID"]);
                    entity.Bid_Enter_Bond = Tools.NullDbl(RdrList["Bid_Enter_Bond"]);
                    entity.Bid_Enter_Type = Tools.NullInt(RdrList["Bid_Enter_Type"]);
                    entity.Bid_Enter_IsShow = Tools.NullInt(RdrList["Bid_Enter_IsShow"]);
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

        public virtual BidEnterInfo GetBidEnterBySupplierID(int BidID,int SupplierID)
        {
            BidEnterInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Bid_Enter WHERE Bid_Enter_BidID = " + BidID + " and Bid_Enter_SupplierID = " + SupplierID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new BidEnterInfo();

                    entity.Bid_Enter_ID = Tools.NullInt(RdrList["Bid_Enter_ID"]);
                    entity.Bid_Enter_BidID = Tools.NullInt(RdrList["Bid_Enter_BidID"]);
                    entity.Bid_Enter_SupplierID = Tools.NullInt(RdrList["Bid_Enter_SupplierID"]);
                    entity.Bid_Enter_Bond = Tools.NullDbl(RdrList["Bid_Enter_Bond"]);
                    entity.Bid_Enter_Type = Tools.NullInt(RdrList["Bid_Enter_Type"]);
                    entity.Bid_Enter_IsShow = Tools.NullInt(RdrList["Bid_Enter_IsShow"]);

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

        public DataTable GetBidEnterSupplierList(QueryInfo Query)
        {
            DataTable DtAdd = null;
            int PageSize;
            int CurrentPage;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Bid_Enter join Bid on Bid_Enter.Bid_Enter_BidID = Bid.Bid_ID";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                DtAdd = DBHelper.Query(SqlList);

                return DtAdd;
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
        public virtual IList<BidEnterInfo> GetBidEnters(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<BidEnterInfo> entitys = null;
            BidEnterInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Bid_Enter";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<BidEnterInfo>();
                    while (RdrList.Read())
                    {
                        entity = new BidEnterInfo();
                        entity.Bid_Enter_ID = Tools.NullInt(RdrList["Bid_Enter_ID"]);
                        entity.Bid_Enter_BidID = Tools.NullInt(RdrList["Bid_Enter_BidID"]);
                        entity.Bid_Enter_SupplierID = Tools.NullInt(RdrList["Bid_Enter_SupplierID"]);
                        entity.Bid_Enter_Bond = Tools.NullDbl(RdrList["Bid_Enter_Bond"]);

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
                SqlTable = "Bid_Enter";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Bid_Enter_ID) FROM " + SqlTable + SqlParam;

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

    public class Tender : ITender
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public Tender()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddTender(TenderInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Tender";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Tender_ID"] = entity.Tender_ID;
            DrAdd["Tender_SupplierID"] = entity.Tender_SupplierID;
            DrAdd["Tender_BidID"] = entity.Tender_BidID;
            DrAdd["Tender_Addtime"] = entity.Tender_Addtime;
            DrAdd["Tender_IsWin"] = entity.Tender_IsWin;
            DrAdd["Tender_Status"] = entity.Tender_Status;
            DrAdd["Tender_AllPrice"] = entity.Tender_AllPrice;
            DrAdd["Tender_IsRefund"] = entity.Tender_IsRefund;
            DrAdd["Tender_SN"] = entity.Tender_SN;
            DrAdd["Tender_IsProduct"] = entity.Tender_IsProduct;
            DrAdd["Tender_IsShow"] = entity.Tender_IsShow;



            DrAdd["Tender_ANote"] = entity.Tender_ANote;
            DrAdd["Tender_BNote"] = entity.Tender_BNote;


            DtAdd.Rows.Add(DrAdd);
            try
            {
                DBHelper.SaveChanges(SqlAdd, DtAdd);

                entity.Tender_ID = GetLastTender(entity.Tender_SN);

                if(entity.TenderProducts!=null)
                {
                    foreach(TenderProductInfo product in entity.TenderProducts)
                    {
                        AddTenderProduct(entity.Tender_ID, product);
                    }
                }
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

        public virtual bool EditTender(TenderInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Tender WHERE Tender_ID = " + entity.Tender_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Tender_ID"] = entity.Tender_ID;
                    DrAdd["Tender_SupplierID"] = entity.Tender_SupplierID;
                    DrAdd["Tender_BidID"] = entity.Tender_BidID;
                    DrAdd["Tender_Addtime"] = entity.Tender_Addtime;
                    DrAdd["Tender_IsWin"] = entity.Tender_IsWin;
                    DrAdd["Tender_Status"] = entity.Tender_Status;
                    DrAdd["Tender_AllPrice"] = entity.Tender_AllPrice;
                    DrAdd["Tender_IsRefund"] = entity.Tender_IsRefund;
                    DrAdd["Tender_SN"] = entity.Tender_SN;
                    DrAdd["Tender_IsProduct"] = entity.Tender_IsProduct;
                    DrAdd["Tender_IsShow"] = entity.Tender_IsShow;


                    DrAdd["Tender_ANote"] = entity.Tender_ANote;
                    DrAdd["Tender_BNote"] = entity.Tender_BNote;

                 

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

        public virtual int DelTender(int ID)
        {
            string SqlAdd = "DELETE FROM Tender WHERE Tender_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual TenderInfo GetTenderByID(int ID)
        {
            TenderInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Tender WHERE Tender_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new TenderInfo();

                    entity.Tender_ID = Tools.NullInt(RdrList["Tender_ID"]);
                    entity.Tender_SupplierID = Tools.NullInt(RdrList["Tender_SupplierID"]);
                    entity.Tender_BidID = Tools.NullInt(RdrList["Tender_BidID"]);
                    entity.Tender_Addtime = Tools.NullDate(RdrList["Tender_Addtime"]);
                    entity.Tender_IsWin = Tools.NullInt(RdrList["Tender_IsWin"]);
                    entity.Tender_Status = Tools.NullInt(RdrList["Tender_Status"]);
                    entity.Tender_AllPrice = Tools.NullDbl(RdrList["Tender_AllPrice"]);
                    entity.Tender_IsRefund = Tools.NullInt(RdrList["Tender_IsRefund"]);
                    entity.Tender_SN = Tools.NullStr(RdrList["Tender_SN"]);
                    entity.Tender_IsProduct = Tools.NullInt(RdrList["Tender_IsProduct"]);
                    entity.Tender_IsShow = Tools.NullInt(RdrList["Tender_IsShow"]);
                    entity.Tender_ANote = Tools.NullStr(RdrList["Tender_ANote"]);
                    entity.Tender_BNote = Tools.NullStr(RdrList["Tender_BNote"]);
                    entity.TenderProducts = null;
                }

                if(entity!=null)
                {
                    entity.TenderProducts = GetTenderProducts(entity.Tender_ID);
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

        public virtual TenderInfo GetTenderBySN(string SN)
        {
            TenderInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Tender WHERE Tender_SN = '" + SN+"'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new TenderInfo();

                    entity.Tender_ID = Tools.NullInt(RdrList["Tender_ID"]);
                    entity.Tender_SupplierID = Tools.NullInt(RdrList["Tender_SupplierID"]);
                    entity.Tender_BidID = Tools.NullInt(RdrList["Tender_BidID"]);
                    entity.Tender_Addtime = Tools.NullDate(RdrList["Tender_Addtime"]);
                    entity.Tender_IsWin = Tools.NullInt(RdrList["Tender_IsWin"]);
                    entity.Tender_Status = Tools.NullInt(RdrList["Tender_Status"]);
                    entity.Tender_AllPrice = Tools.NullDbl(RdrList["Tender_AllPrice"]);
                    entity.Tender_IsRefund = Tools.NullInt(RdrList["Tender_IsRefund"]);
                    entity.Tender_SN = Tools.NullStr(RdrList["Tender_SN"]);
                    entity.Tender_IsProduct = Tools.NullInt(RdrList["Tender_IsProduct"]);
                    entity.Tender_IsShow = Tools.NullInt(RdrList["Tender_IsShow"]);
                    entity.Tender_ANote = Tools.NullStr(RdrList["Tender_ANote"]);
                    entity.Tender_BNote = Tools.NullStr(RdrList["Tender_BNote"]);
                    entity.TenderProducts = null;
                }
                if (entity != null)
                {
                    entity.TenderProducts = GetTenderProducts(entity.Tender_ID);
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

        private int GetLastTender(string SN)
        {
            int Product_ID = 0;
            string SqlList = "SELECT Tender_ID FROM Tender WHERE Tender_SN = '" + SN + "' ORDER BY Tender_ID DESC";
            SqlDataReader RdrList = null;
            try
            {
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    Product_ID = Tools.NullInt(RdrList[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                RdrList.Close();
                RdrList = null;
            }
            return Product_ID;
        }

        public virtual IList<TenderInfo> GetTenders(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<TenderInfo> entitys = null;
            TenderInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Tender join Bid on Tender.Tender_BidID = Bid.Bid_ID and Bid.Bid_IsShow=1";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<TenderInfo>();
                    while (RdrList.Read())
                    {
                        entity = new TenderInfo();
                        entity.Tender_ID = Tools.NullInt(RdrList["Tender_ID"]);
                        entity.Tender_SupplierID = Tools.NullInt(RdrList["Tender_SupplierID"]);
                        entity.Tender_BidID = Tools.NullInt(RdrList["Tender_BidID"]);
                        entity.Tender_Addtime = Tools.NullDate(RdrList["Tender_Addtime"]);
                        entity.Tender_IsWin = Tools.NullInt(RdrList["Tender_IsWin"]);
                        entity.Tender_Status = Tools.NullInt(RdrList["Tender_Status"]);
                        entity.Tender_AllPrice = Tools.NullDbl(RdrList["Tender_AllPrice"]);
                        entity.Tender_IsRefund = Tools.NullInt(RdrList["Tender_IsRefund"]);
                        entity.Tender_SN = Tools.NullStr(RdrList["Tender_SN"]);
                        entity.Tender_IsProduct = Tools.NullInt(RdrList["Tender_IsProduct"]);
                        entity.Tender_IsShow = Tools.NullInt(RdrList["Tender_IsShow"]);
                        entity.Tender_ANote = Tools.NullStr(RdrList["Tender_ANote"]);
                        entity.Tender_BNote = Tools.NullStr(RdrList["Tender_BNote"]);
                        entity.BidMemberCompany = Tools.NullStr(RdrList["Bid_MemberCompany"]);
                        entity.Bid_Title = Tools.NullStr(RdrList["Bid_Title"]);
                        entity.TenderProducts = null;

                        entity.TenderProducts = GetTenderProducts(entity.Tender_ID);
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
                SqlTable = "Tender";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Tender_ID) FROM " + SqlTable + SqlParam;

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


        public virtual bool AddTenderProduct(TenderProductInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Tender_Product";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Tender_Product_ID"] = entity.Tender_Product_ID;
            DrAdd["Tender_Product_ProductID"] = entity.Tender_Product_ProductID;
            DrAdd["Tender_TenderID"] = entity.Tender_TenderID;
            DrAdd["Tender_Product_BidProductID"] = entity.Tender_Product_BidProductID;
            DrAdd["Tender_Product_Name"] = entity.Tender_Product_Name;
            DrAdd["Tender_Price"] = entity.Tender_Price;

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


        public virtual bool AddTenderProduct(int TenderID,TenderProductInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Tender_Product";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Tender_Product_ID"] = entity.Tender_Product_ID;
            DrAdd["Tender_Product_ProductID"] = entity.Tender_Product_ProductID;
            DrAdd["Tender_TenderID"] = TenderID;
            DrAdd["Tender_Product_BidProductID"] = entity.Tender_Product_BidProductID;
            DrAdd["Tender_Product_Name"] = entity.Tender_Product_Name;
            DrAdd["Tender_Price"] = entity.Tender_Price;

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
        public virtual bool EditTenderProduct(TenderProductInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Tender_Product WHERE Tender_Product_ID = " + entity.Tender_Product_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Tender_Product_ID"] = entity.Tender_Product_ID;
                    DrAdd["Tender_Product_ProductID"] = entity.Tender_Product_ProductID;
                    DrAdd["Tender_TenderID"] = entity.Tender_TenderID;
                    DrAdd["Tender_Product_BidProductID"] = entity.Tender_Product_BidProductID;
                    DrAdd["Tender_Product_Name"] = entity.Tender_Product_Name;
                    DrAdd["Tender_Price"] = entity.Tender_Price;

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

        public virtual int DelTenderProduct(int ID)
        {
            string SqlAdd = "DELETE FROM Tender_Product WHERE Tender_Product_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual TenderProductInfo GetTenderProductByID(int ID)
        {
            TenderProductInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Tender_Product WHERE Tender_Product_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new TenderProductInfo();

                    entity.Tender_Product_ID = Tools.NullInt(RdrList["Tender_Product_ID"]);
                    entity.Tender_Product_ProductID = Tools.NullInt(RdrList["Tender_Product_ProductID"]);
                    entity.Tender_TenderID = Tools.NullInt(RdrList["Tender_TenderID"]);
                    entity.Tender_Product_BidProductID = Tools.NullInt(RdrList["Tender_Product_BidProductID"]);
                    entity.Tender_Product_Name = Tools.NullStr(RdrList["Tender_Product_Name"]);
                    entity.Tender_Price = Tools.NullDbl(RdrList["Tender_Price"]);

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

        public virtual IList<TenderProductInfo> GetTenderProducts(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<TenderProductInfo> entitys = null;
            TenderProductInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Tender_Product";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<TenderProductInfo>();
                    while (RdrList.Read())
                    {
                        entity = new TenderProductInfo();
                        entity.Tender_Product_ID = Tools.NullInt(RdrList["Tender_Product_ID"]);
                        entity.Tender_Product_ProductID = Tools.NullInt(RdrList["Tender_Product_ProductID"]);
                        entity.Tender_TenderID = Tools.NullInt(RdrList["Tender_TenderID"]);
                        entity.Tender_Product_BidProductID = Tools.NullInt(RdrList["Tender_Product_BidProductID"]);
                        entity.Tender_Product_Name = Tools.NullStr(RdrList["Tender_Product_Name"]);
                        entity.Tender_Price = Tools.NullDbl(RdrList["Tender_Price"]);

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

        public virtual IList<TenderProductInfo> GetTenderProducts(int TenderID)
        {

            IList<TenderProductInfo> entitys = null;
            TenderProductInfo entity = null;

            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Tender_Product WHERE Tender_TenderID = " + TenderID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<TenderProductInfo>();
                    while (RdrList.Read())
                    {
                        entity = new TenderProductInfo();
                        entity.Tender_Product_ID = Tools.NullInt(RdrList["Tender_Product_ID"]);
                        entity.Tender_Product_ProductID = Tools.NullInt(RdrList["Tender_Product_ProductID"]);
                        entity.Tender_TenderID = Tools.NullInt(RdrList["Tender_TenderID"]);
                        entity.Tender_Product_BidProductID = Tools.NullInt(RdrList["Tender_Product_BidProductID"]);
                        entity.Tender_Product_Name = Tools.NullStr(RdrList["Tender_Product_Name"]);
                        entity.Tender_Price = Tools.NullDbl(RdrList["Tender_Price"]);

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
