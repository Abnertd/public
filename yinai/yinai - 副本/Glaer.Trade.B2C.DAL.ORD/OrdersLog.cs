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
    public class OrdersLog : IOrdersLog
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public OrdersLog()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddOrdersLog(OrdersLogInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Orders_Log";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();
            DrAdd["Orders_Log_ID"] = entity.Orders_Log_ID;
            DrAdd["Orders_Log_OrdersID"] = entity.Orders_Log_OrdersID;
            DrAdd["Orders_Log_Addtime"] = entity.Orders_Log_Addtime;
            DrAdd["Orders_Log_Operator"] = entity.Orders_Log_Operator;
            DrAdd["Orders_Log_Remark"] = entity.Orders_Log_Remark;
            DrAdd["Orders_Log_Action"] = entity.Orders_Log_Action;
            DrAdd["Orders_Log_Result"] = entity.Orders_Log_Result;
            DtAdd.Rows.Add(DrAdd);
            try {
                DBHelper.SaveChanges(SqlAdd, DtAdd);
                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                DtAdd.Dispose();
            }
        }

        public virtual bool EditOrdersLog(OrdersLogInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Orders_Log WHERE Orders_Log_ID = " + entity.Orders_Log_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try {
                if (DtAdd.Rows.Count > 0) {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Orders_Log_ID"] = entity.Orders_Log_ID;
                    DrAdd["Orders_Log_OrdersID"] = entity.Orders_Log_OrdersID;
                    DrAdd["Orders_Log_Addtime"] = entity.Orders_Log_Addtime;
                    DrAdd["Orders_Log_Operator"] = entity.Orders_Log_Operator;
                    DrAdd["Orders_Log_Remark"] = entity.Orders_Log_Remark;
                    DrAdd["Orders_Log_Action"] = entity.Orders_Log_Action;
                    DrAdd["Orders_Log_Result"] = entity.Orders_Log_Result;
                    DBHelper.SaveChanges(SqlAdd, DtAdd);
                }
                else {
                    return false;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                DtAdd.Dispose();
            }
            return true;
        }

        public virtual int DelOrdersLog(int ID)
        {
            string SqlAdd = "DELETE FROM Orders_Log WHERE Orders_Log_ID = " + ID;
            try {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public virtual OrdersLogInfo GetOrdersLogByID(int ID)
        {
            OrdersLogInfo entity = null;
            SqlDataReader RdrList = null;
            try {
                string SqlList;
                SqlList = "SELECT * FROM Orders_Log WHERE Orders_Log_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read()) {
                    entity = new OrdersLogInfo();
                    entity.Orders_Log_ID = Tools.NullInt(RdrList["Orders_Log_ID"]);
                    entity.Orders_Log_OrdersID = Tools.NullInt(RdrList["Orders_Log_OrdersID"]);
                    entity.Orders_Log_Addtime = Tools.NullDate(RdrList["Orders_Log_Addtime"]);
                    entity.Orders_Log_Operator = Tools.NullStr(RdrList["Orders_Log_Operator"]);
                    entity.Orders_Log_Remark = Tools.NullStr(RdrList["Orders_Log_Remark"]);
                    entity.Orders_Log_Action = Tools.NullStr(RdrList["Orders_Log_Action"]);
                    entity.Orders_Log_Result = Tools.NullStr(RdrList["Orders_Log_Result"]);
                }
                return entity;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (RdrList != null) {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

        public virtual IList<OrdersLogInfo> GetOrdersLogsByOrdersID(int ID)
        {
            IList<OrdersLogInfo> entitys = null;
            OrdersLogInfo entity = null;
            string SqlList = "SELECT * FROM Orders_Log WHERE Orders_Log_OrdersID = " + ID;
            SqlDataReader RdrList = null;
            try {
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows) {
                    entitys = new List<OrdersLogInfo>();
                    while (RdrList.Read()) {
                        entity = new OrdersLogInfo();
                        entity.Orders_Log_ID = Tools.NullInt(RdrList["Orders_Log_ID"]);
                        entity.Orders_Log_OrdersID = Tools.NullInt(RdrList["Orders_Log_OrdersID"]);
                        entity.Orders_Log_Addtime = Tools.NullDate(RdrList["Orders_Log_Addtime"]);
                        entity.Orders_Log_Operator = Tools.NullStr(RdrList["Orders_Log_Operator"]);
                        entity.Orders_Log_Remark = Tools.NullStr(RdrList["Orders_Log_Remark"]);
                        entity.Orders_Log_Action = Tools.NullStr(RdrList["Orders_Log_Action"]);
                        entity.Orders_Log_Result = Tools.NullStr(RdrList["Orders_Log_Result"]);
                        entitys.Add(entity);
                        entity = null;
                    }
                }
                return entitys;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                if (RdrList != null) {
                    RdrList.Close();
                    RdrList = null;
                }
            }
        }

    }

}
