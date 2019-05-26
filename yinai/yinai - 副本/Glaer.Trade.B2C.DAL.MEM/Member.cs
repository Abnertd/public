using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;
using System.Text;

namespace Glaer.Trade.B2C.DAL.MEM
{
    public class Member : IMember
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public Member()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddMember(MemberInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Member";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            //DrAdd["Member_ID"] = entity.Member_ID;
            DrAdd["Member_Type"] = entity.Member_Type;
            DrAdd["Member_Email"] = entity.Member_Email;
            DrAdd["Member_Emailverify"] = entity.Member_Emailverify;
            DrAdd["Member_LoginMobile"] = entity.Member_LoginMobile;
            DrAdd["Member_LoginMobileverify"] = entity.Member_LoginMobileverify;
            DrAdd["Member_NickName"] = entity.Member_NickName;
            DrAdd["Member_Password"] = entity.Member_Password;
            DrAdd["Member_VerifyCode"] = entity.Member_VerifyCode;
            DrAdd["Member_LoginCount"] = entity.Member_LoginCount;
            DrAdd["Member_LastLogin_IP"] = entity.Member_LastLogin_IP;
            DrAdd["Member_LastLogin_Time"] = entity.Member_LastLogin_Time;
            DrAdd["Member_CoinCount"] = entity.Member_CoinCount;
            DrAdd["Member_CoinRemain"] = entity.Member_CoinRemain;
            DrAdd["Member_Addtime"] = entity.Member_Addtime;
            DrAdd["Member_Trash"] = entity.Member_Trash;
            DrAdd["Member_Grade"] = entity.Member_Grade;
            DrAdd["Member_Account"] = entity.Member_Account;
            DrAdd["Member_Frozen"] = entity.Member_Frozen;
            DrAdd["Member_AllowSysEmail"] = entity.Member_AllowSysEmail;
            DrAdd["Member_AuditStatus"] = entity.Member_AuditStatus;
            DrAdd["Member_Cert_Status"] = entity.Member_Cert_Status;
            DrAdd["Member_Site"] = entity.Member_Site;
            DrAdd["Member_Source"] = entity.Member_Source;
            DrAdd["Member_RegIP"] = entity.Member_RegIP;
            DrAdd["Member_Status"] = entity.Member_Status;
            DrAdd["Member_VfinanceID"] = entity.Member_VfinanceID;
            DrAdd["Member_ERP_StoreID"] = entity.Member_ERP_StoreID;
            DrAdd["Member_SupplierID"] = entity.Member_SupplierID;
            DrAdd["Member_Company_Introduce"] = entity.Member_Company_Introduce;
            DrAdd["Member_Company_Contact"] = entity.Member_Company_Contact;
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

        public virtual bool EditMember(MemberInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Member WHERE Member_ID = " + entity.Member_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    //DrAdd["Member_ID"] = entity.Member_ID;
                    DrAdd["Member_Type"] = entity.Member_Type;
                    DrAdd["Member_Email"] = entity.Member_Email;
                    DrAdd["Member_Emailverify"] = entity.Member_Emailverify;
                    DrAdd["Member_LoginMobile"] = entity.Member_LoginMobile;
                    DrAdd["Member_LoginMobileverify"] = entity.Member_LoginMobileverify;
                    DrAdd["Member_NickName"] = entity.Member_NickName;
                    DrAdd["Member_Password"] = entity.Member_Password;
                    DrAdd["Member_VerifyCode"] = entity.Member_VerifyCode;
                    DrAdd["Member_LoginCount"] = entity.Member_LoginCount;
                    DrAdd["Member_LastLogin_IP"] = entity.Member_LastLogin_IP;
                    DrAdd["Member_LastLogin_Time"] = entity.Member_LastLogin_Time;
                    DrAdd["Member_CoinCount"] = entity.Member_CoinCount;
                    DrAdd["Member_CoinRemain"] = entity.Member_CoinRemain;
                    DrAdd["Member_Addtime"] = entity.Member_Addtime;
                    DrAdd["Member_Trash"] = entity.Member_Trash;
                    DrAdd["Member_Grade"] = entity.Member_Grade;
                    DrAdd["Member_Account"] = entity.Member_Account;
                    DrAdd["Member_Frozen"] = entity.Member_Frozen;
                    DrAdd["Member_AllowSysEmail"] = entity.Member_AllowSysEmail;
                    DrAdd["Member_AuditStatus"] = entity.Member_AuditStatus;
                    DrAdd["Member_Cert_Status"] = entity.Member_Cert_Status;
                    DrAdd["Member_Site"] = entity.Member_Site;
                    DrAdd["Member_Source"] = entity.Member_Source;
                    DrAdd["Member_RegIP"] = entity.Member_RegIP;
                    DrAdd["Member_Status"] = entity.Member_Status;
                    DrAdd["Member_VfinanceID"] = entity.Member_VfinanceID;
                    DrAdd["Member_ERP_StoreID"] = entity.Member_ERP_StoreID;
                    DrAdd["Member_Company_Introduce"] = entity.Member_Company_Introduce;
                    DrAdd["Member_Company_Contact"] = entity.Member_Company_Contact;
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

        public virtual bool UpdateMemberLogin(int Member_ID, int Count, string Remote_IP)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Member WHERE Member_ID = " + Member_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Member_LoginCount"] = Count;
                    DrAdd["Member_LastLogin_IP"] = Remote_IP;
                    DrAdd["Member_LastLogin_Time"] = DateTime.Now;

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

        public virtual int DelMember(int ID)
        {
            string SqlAdd = "DELETE FROM Member WHERE Member_ID = " + ID;
            try { return DBHelper.ExecuteNonQuery(SqlAdd); }
            catch (Exception ex) { throw ex; }
        }

        public virtual MemberInfo GetMemberByID(int ID)
        {
            MemberInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member WHERE Member_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new MemberInfo();
                    entity.Member_ID = Tools.NullInt(RdrList["Member_ID"]);
                    entity.Member_Type = Tools.NullInt(RdrList["Member_Type"]);
                    entity.Member_Email = Tools.NullStr(RdrList["Member_Email"]);
                    entity.Member_Emailverify = Tools.NullInt(RdrList["Member_Emailverify"]);
                    entity.Member_LoginMobile = Tools.NullStr(RdrList["Member_LoginMobile"]);
                    entity.Member_LoginMobileverify = Tools.NullInt(RdrList["Member_LoginMobileverify"]);
                    entity.Member_NickName = Tools.NullStr(RdrList["Member_NickName"]);
                    entity.Member_Password = Tools.NullStr(RdrList["Member_Password"]);
                    entity.Member_VerifyCode = Tools.NullStr(RdrList["Member_VerifyCode"]);
                    entity.Member_LoginCount = Tools.NullInt(RdrList["Member_LoginCount"]);
                    entity.Member_LastLogin_IP = Tools.NullStr(RdrList["Member_LastLogin_IP"]);
                    entity.Member_LastLogin_Time = Tools.NullDate(RdrList["Member_LastLogin_Time"]);
                    entity.Member_CoinCount = Tools.NullInt(RdrList["Member_CoinCount"]);
                    entity.Member_CoinRemain = Tools.NullInt(RdrList["Member_CoinRemain"]);
                    entity.Member_Addtime = Tools.NullDate(RdrList["Member_Addtime"]);
                    entity.Member_Trash = Tools.NullInt(RdrList["Member_Trash"]);
                    entity.Member_Grade = Tools.NullInt(RdrList["Member_Grade"]);
                    entity.Member_Account = Tools.NullDbl(RdrList["Member_Account"]);
                    entity.Member_Frozen = Tools.NullDbl(RdrList["Member_Frozen"]);
                    entity.Member_AllowSysEmail = Tools.NullInt(RdrList["Member_AllowSysEmail"]);
                    entity.Member_AuditStatus = Tools.NullInt(RdrList["Member_AuditStatus"]);
                    entity.Member_Cert_Status = Tools.NullInt(RdrList["Member_Cert_Status"]);
                    entity.Member_Site = Tools.NullStr(RdrList["Member_Site"]);
                    entity.Member_Source = Tools.NullStr(RdrList["Member_Source"]);
                    entity.Member_RegIP = Tools.NullStr(RdrList["Member_RegIP"]);
                    entity.Member_Status = Tools.NullInt(RdrList["Member_Status"]);
                    entity.Member_VfinanceID = Tools.NullStr(RdrList["Member_VfinanceID"]);
                    entity.Member_ERP_StoreID = Tools.NullStr(RdrList["Member_ERP_StoreID"]);
                    entity.Member_SupplierID = Tools.NullInt(RdrList["Member_SupplierID"]);
                    entity.Member_Company_Introduce = Tools.NullStr(RdrList["Member_Company_Introduce"]);
                    entity.Member_Company_Contact = Tools.NullStr(RdrList["Member_Company_Contact"]);
                    entity.MemberProfileInfo = null;
                    entity.MemberRelateCertInfos = null;

                }
                RdrList.Close();
                RdrList = null;
                if (entity != null)
                {
                    entity.MemberRelateCertInfos = GetMemberRelateCerts(entity.Member_ID);
                    entity.MemberProfileInfo = GetMemberProfile(entity.Member_ID);

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

        public virtual MemberInfo GetMemberByEmail(string email)
        {
            MemberInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member WHERE Member_Email = '" + email + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new MemberInfo();
                    entity.Member_ID = Tools.NullInt(RdrList["Member_ID"]);
                    entity.Member_Type = Tools.NullInt(RdrList["Member_Type"]);
                    entity.Member_Email = Tools.NullStr(RdrList["Member_Email"]);
                    entity.Member_Emailverify = Tools.NullInt(RdrList["Member_Emailverify"]);
                    entity.Member_LoginMobile = Tools.NullStr(RdrList["Member_LoginMobile"]);
                    entity.Member_LoginMobileverify = Tools.NullInt(RdrList["Member_LoginMobileverify"]);
                    entity.Member_NickName = Tools.NullStr(RdrList["Member_NickName"]);
                    entity.Member_Password = Tools.NullStr(RdrList["Member_Password"]);
                    entity.Member_VerifyCode = Tools.NullStr(RdrList["Member_VerifyCode"]);
                    entity.Member_LoginCount = Tools.NullInt(RdrList["Member_LoginCount"]);
                    entity.Member_LastLogin_IP = Tools.NullStr(RdrList["Member_LastLogin_IP"]);
                    entity.Member_LastLogin_Time = Tools.NullDate(RdrList["Member_LastLogin_Time"]);
                    entity.Member_CoinCount = Tools.NullInt(RdrList["Member_CoinCount"]);
                    entity.Member_CoinRemain = Tools.NullInt(RdrList["Member_CoinRemain"]);
                    entity.Member_Addtime = Tools.NullDate(RdrList["Member_Addtime"]);
                    entity.Member_Trash = Tools.NullInt(RdrList["Member_Trash"]);
                    entity.Member_Grade = Tools.NullInt(RdrList["Member_Grade"]);
                    entity.Member_Account = Tools.NullDbl(RdrList["Member_Account"]);
                    entity.Member_Frozen = Tools.NullDbl(RdrList["Member_Frozen"]);
                    entity.Member_AllowSysEmail = Tools.NullInt(RdrList["Member_AllowSysEmail"]);
                    entity.Member_AuditStatus = Tools.NullInt(RdrList["Member_AuditStatus"]);
                    entity.Member_Cert_Status = Tools.NullInt(RdrList["Member_Cert_Status"]);
                    entity.Member_Site = Tools.NullStr(RdrList["Member_Site"]);
                    entity.Member_Source = Tools.NullStr(RdrList["Member_Source"]);
                    entity.Member_RegIP = Tools.NullStr(RdrList["Member_RegIP"]);
                    entity.Member_Status = Tools.NullInt(RdrList["Member_Status"]);
                    entity.Member_VfinanceID = Tools.NullStr(RdrList["Member_VfinanceID"]);
                    entity.Member_ERP_StoreID = Tools.NullStr(RdrList["Member_ERP_StoreID"]);
                    entity.Member_SupplierID = Tools.NullInt(RdrList["Member_SupplierID"]);
                    entity.Member_Company_Introduce = Tools.NullStr(RdrList["Member_Company_Introduce"]);
                    entity.Member_Company_Contact = Tools.NullStr(RdrList["Member_Company_Contact"]);
                    entity.MemberProfileInfo = null;
                    entity.MemberRelateCertInfos = null;

                }

                if (entity != null)
                {
                    entity.MemberRelateCertInfos = GetMemberRelateCerts(entity.Member_ID);
                    entity.MemberProfileInfo = GetMemberProfile(entity.Member_ID);

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

        public virtual MemberInfo GetMemberByNickName(string NickName)
        {
            MemberInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member WHERE Member_NickName = '" + NickName + "'";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new MemberInfo();
                    entity.Member_ID = Tools.NullInt(RdrList["Member_ID"]);
                    entity.Member_Type = Tools.NullInt(RdrList["Member_Type"]);
                    entity.Member_Email = Tools.NullStr(RdrList["Member_Email"]);
                    entity.Member_Emailverify = Tools.NullInt(RdrList["Member_Emailverify"]);
                    entity.Member_LoginMobile = Tools.NullStr(RdrList["Member_LoginMobile"]);
                    entity.Member_LoginMobileverify = Tools.NullInt(RdrList["Member_LoginMobileverify"]);
                    entity.Member_NickName = Tools.NullStr(RdrList["Member_NickName"]);
                    entity.Member_Password = Tools.NullStr(RdrList["Member_Password"]);
                    entity.Member_VerifyCode = Tools.NullStr(RdrList["Member_VerifyCode"]);
                    entity.Member_LoginCount = Tools.NullInt(RdrList["Member_LoginCount"]);
                    entity.Member_LastLogin_IP = Tools.NullStr(RdrList["Member_LastLogin_IP"]);
                    entity.Member_LastLogin_Time = Tools.NullDate(RdrList["Member_LastLogin_Time"]);
                    entity.Member_CoinCount = Tools.NullInt(RdrList["Member_CoinCount"]);
                    entity.Member_CoinRemain = Tools.NullInt(RdrList["Member_CoinRemain"]);
                    entity.Member_Addtime = Tools.NullDate(RdrList["Member_Addtime"]);
                    entity.Member_Trash = Tools.NullInt(RdrList["Member_Trash"]);
                    entity.Member_Grade = Tools.NullInt(RdrList["Member_Grade"]);
                    entity.Member_Account = Tools.NullDbl(RdrList["Member_Account"]);
                    entity.Member_Frozen = Tools.NullDbl(RdrList["Member_Frozen"]);
                    entity.Member_AllowSysEmail = Tools.NullInt(RdrList["Member_AllowSysEmail"]);
                    entity.Member_AuditStatus = Tools.NullInt(RdrList["Member_AuditStatus"]);
                    entity.Member_Cert_Status = Tools.NullInt(RdrList["Member_Cert_Status"]);
                    entity.Member_Site = Tools.NullStr(RdrList["Member_Site"]);
                    entity.Member_Source = Tools.NullStr(RdrList["Member_Source"]);
                    entity.Member_RegIP = Tools.NullStr(RdrList["Member_RegIP"]);
                    entity.Member_Status = Tools.NullInt(RdrList["Member_Status"]);
                    entity.Member_VfinanceID = Tools.NullStr(RdrList["Member_VfinanceID"]);
                    entity.Member_ERP_StoreID = Tools.NullStr(RdrList["Member_ERP_StoreID"]);
                    entity.Member_SupplierID = Tools.NullInt(RdrList["Member_SupplierID"]);
                    entity.Member_Company_Introduce = Tools.NullStr(RdrList["Member_Company_Introduce"]);
                    entity.Member_Company_Contact = Tools.NullStr(RdrList["Member_Company_Contact"]);
                    entity.MemberProfileInfo = null;
                    entity.MemberRelateCertInfos = null;

                }

                if (entity != null)
                {
                    entity.MemberRelateCertInfos = GetMemberRelateCerts(entity.Member_ID);
                    entity.MemberProfileInfo = GetMemberProfile(entity.Member_ID);

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

        public virtual MemberInfo GetMemberByLogin(string nickname, string password)
        {
            MemberInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member WHERE (Member_NickName = '" + nickname + "' or Member_Email='" + nickname + "' or Member_LoginMobile='" + nickname + "') and Member_Password='" + password + "' and Member_Trash=0";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new MemberInfo();
                    entity.Member_ID = Tools.NullInt(RdrList["Member_ID"]);
                    entity.Member_Type = Tools.NullInt(RdrList["Member_Type"]);
                    entity.Member_Email = Tools.NullStr(RdrList["Member_Email"]);
                    entity.Member_Emailverify = Tools.NullInt(RdrList["Member_Emailverify"]);
                    entity.Member_LoginMobile = Tools.NullStr(RdrList["Member_LoginMobile"]);
                    entity.Member_LoginMobileverify = Tools.NullInt(RdrList["Member_LoginMobileverify"]);
                    entity.Member_NickName = Tools.NullStr(RdrList["Member_NickName"]);
                    entity.Member_Password = Tools.NullStr(RdrList["Member_Password"]);
                    entity.Member_VerifyCode = Tools.NullStr(RdrList["Member_VerifyCode"]);
                    entity.Member_LoginCount = Tools.NullInt(RdrList["Member_LoginCount"]);
                    entity.Member_LastLogin_IP = Tools.NullStr(RdrList["Member_LastLogin_IP"]);
                    entity.Member_LastLogin_Time = Tools.NullDate(RdrList["Member_LastLogin_Time"]);
                    entity.Member_CoinCount = Tools.NullInt(RdrList["Member_CoinCount"]);
                    entity.Member_CoinRemain = Tools.NullInt(RdrList["Member_CoinRemain"]);
                    entity.Member_Addtime = Tools.NullDate(RdrList["Member_Addtime"]);
                    entity.Member_Trash = Tools.NullInt(RdrList["Member_Trash"]);
                    entity.Member_Grade = Tools.NullInt(RdrList["Member_Grade"]);
                    entity.Member_Account = Tools.NullDbl(RdrList["Member_Account"]);
                    entity.Member_Frozen = Tools.NullDbl(RdrList["Member_Frozen"]);
                    entity.Member_AllowSysEmail = Tools.NullInt(RdrList["Member_AllowSysEmail"]);
                    entity.Member_AuditStatus = Tools.NullInt(RdrList["Member_AuditStatus"]);
                    entity.Member_Cert_Status = Tools.NullInt(RdrList["Member_Cert_Status"]);
                    entity.Member_Site = Tools.NullStr(RdrList["Member_Site"]);
                    entity.Member_Source = Tools.NullStr(RdrList["Member_Source"]);
                    entity.Member_RegIP = Tools.NullStr(RdrList["Member_RegIP"]);
                    entity.Member_Status = Tools.NullInt(RdrList["Member_Status"]);
                    entity.Member_VfinanceID = Tools.NullStr(RdrList["Member_VfinanceID"]);
                    entity.Member_ERP_StoreID = Tools.NullStr(RdrList["Member_ERP_StoreID"]);
                    entity.Member_SupplierID = Tools.NullInt(RdrList["Member_SupplierID"]);
                    entity.Member_Company_Introduce = Tools.NullStr(RdrList["Member_Company_Introduce"]);
                    entity.Member_Company_Contact = Tools.NullStr(RdrList["Member_Company_Contact"]);
                    entity.MemberProfileInfo = null;
                    entity.MemberRelateCertInfos = null;

                }
                RdrList.Close();
                RdrList = null;
                if (entity != null)
                {
                    entity.MemberRelateCertInfos = GetMemberRelateCerts(entity.Member_ID);
                    entity.MemberProfileInfo = GetMemberProfile(entity.Member_ID);

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


        public bool GetMemberAccountByLogin(string nickname)
        {
            bool MemberAccountIsExist = false;          
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member WHERE (Member_NickName = '" + nickname + "' or Member_Email='" + nickname + "' or Member_LoginMobile='" + nickname + "') ";
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    MemberAccountIsExist = true;

                }
                RdrList.Close();
                RdrList = null;
              

                return MemberAccountIsExist;
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

        public virtual IList<MemberInfo> GetMembers(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<MemberInfo> entitys = null;
            MemberInfo entity = null;
            MemberProfileInfo profile = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Member Inner Join Member_Profile On Member.Member_ID = Member_Profile.Member_Profile_MemberID";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<MemberInfo>();
                    while (RdrList.Read())
                    {
                        entity = new MemberInfo();
                        profile = new MemberProfileInfo();
                        entity.Member_ID = Tools.NullInt(RdrList["Member_ID"]);
                        entity.Member_Type = Tools.NullInt(RdrList["Member_Type"]);
                        entity.Member_Email = Tools.NullStr(RdrList["Member_Email"]);
                        entity.Member_Emailverify = Tools.NullInt(RdrList["Member_Emailverify"]);
                        entity.Member_LoginMobile = Tools.NullStr(RdrList["Member_LoginMobile"]);
                        entity.Member_LoginMobileverify = Tools.NullInt(RdrList["Member_LoginMobileverify"]);
                        entity.Member_NickName = Tools.NullStr(RdrList["Member_NickName"]);
                        entity.Member_Password = Tools.NullStr(RdrList["Member_Password"]);
                        entity.Member_VerifyCode = Tools.NullStr(RdrList["Member_VerifyCode"]);
                        entity.Member_LoginCount = Tools.NullInt(RdrList["Member_LoginCount"]);
                        entity.Member_LastLogin_IP = Tools.NullStr(RdrList["Member_LastLogin_IP"]);
                        entity.Member_LastLogin_Time = Tools.NullDate(RdrList["Member_LastLogin_Time"]);
                        entity.Member_CoinCount = Tools.NullInt(RdrList["Member_CoinCount"]);
                        entity.Member_CoinRemain = Tools.NullInt(RdrList["Member_CoinRemain"]);
                        entity.Member_Addtime = Tools.NullDate(RdrList["Member_Addtime"]);
                        entity.Member_Trash = Tools.NullInt(RdrList["Member_Trash"]);
                        entity.Member_Grade = Tools.NullInt(RdrList["Member_Grade"]);
                        entity.Member_Account = Tools.NullDbl(RdrList["Member_Account"]);
                        entity.Member_Frozen = Tools.NullDbl(RdrList["Member_Frozen"]);
                        entity.Member_AllowSysEmail = Tools.NullInt(RdrList["Member_AllowSysEmail"]);
                        entity.Member_AuditStatus = Tools.NullInt(RdrList["Member_AuditStatus"]);
                        entity.Member_Cert_Status = Tools.NullInt(RdrList["Member_Cert_Status"]);
                        entity.Member_Site = Tools.NullStr(RdrList["Member_Site"]);
                        entity.Member_Source = Tools.NullStr(RdrList["Member_Source"]);
                        entity.Member_RegIP = Tools.NullStr(RdrList["Member_RegIP"]);
                        entity.Member_Status = Tools.NullInt(RdrList["Member_Status"]);
                        entity.Member_VfinanceID = Tools.NullStr(RdrList["Member_VfinanceID"]);
                        entity.Member_ERP_StoreID = Tools.NullStr(RdrList["Member_ERP_StoreID"]);
                        entity.Member_SupplierID = Tools.NullInt(RdrList["Member_SupplierID"]);
                        entity.Member_Company_Introduce = Tools.NullStr(RdrList["Member_Company_Introduce"]);
                        entity.Member_Company_Contact = Tools.NullStr(RdrList["Member_Company_Contact"]);
                        profile.Member_Profile_ID = Tools.NullInt(RdrList["Member_Profile_ID"]);
                        profile.Member_Profile_MemberID = Tools.NullInt(RdrList["Member_Profile_MemberID"]);
                        profile.Member_Name = Tools.NullStr(RdrList["Member_Name"]);
                        profile.Member_Sex = Tools.NullInt(RdrList["Member_Sex"]);
                        profile.Member_StreetAddress = Tools.NullStr(RdrList["Member_StreetAddress"]);
                        profile.Member_County = Tools.NullStr(RdrList["Member_County"]);
                        profile.Member_City = Tools.NullStr(RdrList["Member_City"]);
                        profile.Member_State = Tools.NullStr(RdrList["Member_State"]);
                        profile.Member_Country = Tools.NullStr(RdrList["Member_Country"]);
                        profile.Member_Zip = Tools.NullStr(RdrList["Member_Zip"]);
                        profile.Member_Phone_Countrycode = Tools.NullStr(RdrList["Member_Phone_Countrycode"]);
                        profile.Member_Phone_Areacode = Tools.NullStr(RdrList["Member_Phone_Areacode"]);
                        profile.Member_Phone_Number = Tools.NullStr(RdrList["Member_Phone_Number"]);
                        profile.Member_Mobile = Tools.NullStr(RdrList["Member_Mobile"]);
                        profile.Member_Company = Tools.NullStr(RdrList["Member_Company"]);
                        profile.Member_Fax = Tools.NullStr(RdrList["Member_Fax"]);
                        profile.Member_QQ = Tools.NullStr(RdrList["Member_QQ"]);
                        profile.Member_OrganizationCode = Tools.NullStr(RdrList["Member_OrganizationCode"]);
                        profile.Member_BusinessCode = Tools.NullStr(RdrList["Member_BusinessCode"]);
                        
                        entity.MemberProfileInfo = profile;
                        entitys.Add(entity);
                        entity = null;
                        profile = null;
                    }
                }
                RdrList.Close();
                RdrList = null;


                //if (entitys != null) { 
                //    foreach (MemberInfo Info in entitys) { 
                //        Info.MemberProfileInfo = GetMemberProfile(Info.Member_ID);  
                //    } 
                //}
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
                SqlTable = "Member Inner Join Member_Profile On Member.Member_ID = Member_Profile.Member_Profile_MemberID";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Member_ID) FROM " + SqlTable + SqlParam;

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

        

        #region 采购商资料
        public virtual MemberProfileInfo GetMemberProfile(int member_id)
        {
            string SqlList = "SELECT Member_Profile_ID, Member_Profile_MemberID, Member_Name, Member_Sex";
            SqlList += ", Member_StreetAddress, Member_County, Member_City, Member_State, Member_Country";
            SqlList += ", Member_Zip, Member_Phone_Countrycode, Member_Phone_Areacode, Member_Phone_Number, Member_Mobile,Member_Company,Member_Fax,Member_QQ,Member_OrganizationCode,Member_BusinessCode,Member_SealImg,Member_Corporate,Member_CorporateMobile,Member_RegisterFunds,Member_TaxationCode,Member_BankAccountCode,Member_HeadImg,Member_RealName,Member_UniformSocial_Number FROM Member_Profile";
            SqlList += " WHERE Member_Profile_MemberID = " + member_id;
            DataTable DtList = null;
            DataRow DrList = null;
            MemberProfileInfo entity = null;

            try
            {
                DtList = DBHelper.Query(SqlList);
                if (DtList.Rows.Count > 0)
                {
                    entity = new MemberProfileInfo();
                    DrList = DtList.Rows[0];
                    entity.Member_Profile_ID = Tools.NullInt(DrList["Member_Profile_ID"]);
                    entity.Member_Profile_MemberID = Tools.NullInt(DrList["Member_Profile_MemberID"]);
                    entity.Member_Name = Tools.NullStr(DrList["Member_Name"]);
                    entity.Member_Sex = Tools.NullInt(DrList["Member_Sex"]);
                    entity.Member_StreetAddress = Tools.NullStr(DrList["Member_StreetAddress"]);
                    entity.Member_County = Tools.NullStr(DrList["Member_County"]);
                    entity.Member_City = Tools.NullStr(DrList["Member_City"]);
                    entity.Member_State = Tools.NullStr(DrList["Member_State"]);
                    entity.Member_Country = Tools.NullStr(DrList["Member_Country"]);
                    entity.Member_Zip = Tools.NullStr(DrList["Member_Zip"]);
                    entity.Member_Phone_Countrycode = Tools.NullStr(DrList["Member_Phone_Countrycode"]);
                    entity.Member_Phone_Areacode = Tools.NullStr(DrList["Member_Phone_Areacode"]);
                    entity.Member_Phone_Number = Tools.NullStr(DrList["Member_Phone_Number"]);
                    entity.Member_Mobile = Tools.NullStr(DrList["Member_Mobile"]);
                    entity.Member_Company = Tools.NullStr(DrList["Member_Company"]);
                    entity.Member_Fax = Tools.NullStr(DrList["Member_Fax"]);
                    entity.Member_QQ = Tools.NullStr(DrList["Member_QQ"]);
                    entity.Member_OrganizationCode = Tools.NullStr(DrList["Member_OrganizationCode"]);
                    entity.Member_BusinessCode = Tools.NullStr(DrList["Member_BusinessCode"]);
                    entity.Member_SealImg = Tools.NullStr(DrList["Member_SealImg"]);
                    entity.Member_Corporate = Tools.NullStr(DrList["Member_Corporate"]);
                    entity.Member_CorporateMobile = Tools.NullStr(DrList["Member_CorporateMobile"]);
                    entity.Member_RegisterFunds = Tools.NullDbl(DrList["Member_RegisterFunds"]);
                    entity.Member_TaxationCode = Tools.NullStr(DrList["Member_TaxationCode"]);
                    entity.Member_BankAccountCode = Tools.NullStr(DrList["Member_BankAccountCode"]);
                    entity.Member_HeadImg = Tools.NullStr(DrList["Member_HeadImg"]);
                    //新加 真实姓名  统一社会代码证号
                    entity.Member_RealName = Tools.NullStr(DrList["Member_RealName"]);
                    entity.Member_UniformSocial_Number = Tools.NullStr(DrList["Member_UniformSocial_Number"]);

                    DrList = null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return entity;
        }

        public virtual bool AddMemberProfile(MemberProfileInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Member_Profile";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Member_Profile_MemberID"] = entity.Member_Profile_MemberID;
            DrAdd["Member_Name"] = entity.Member_Name;
            DrAdd["Member_Sex"] = entity.Member_Sex;
            DrAdd["Member_StreetAddress"] = entity.Member_StreetAddress;
            DrAdd["Member_County"] = entity.Member_County;
            DrAdd["Member_City"] = entity.Member_City;
            DrAdd["Member_State"] = entity.Member_State;
            DrAdd["Member_Country"] = entity.Member_Country;
            DrAdd["Member_Zip"] = entity.Member_Zip;
            DrAdd["Member_Phone_Countrycode"] = entity.Member_Phone_Countrycode;
            DrAdd["Member_Phone_Areacode"] = entity.Member_Phone_Areacode;
            DrAdd["Member_Phone_Number"] = entity.Member_Phone_Number;
            DrAdd["Member_Mobile"] = entity.Member_Mobile;
            DrAdd["Member_Company"] = entity.Member_Company;
            DrAdd["Member_Fax"] = entity.Member_Fax;
            DrAdd["Member_QQ"] = entity.Member_QQ;
            DrAdd["Member_OrganizationCode"] = entity.Member_OrganizationCode;
            DrAdd["Member_BusinessCode"] = entity.Member_BusinessCode;
            DrAdd["Member_SealImg"] = entity.Member_SealImg;
            DrAdd["Member_Corporate"] = entity.Member_Corporate;
            DrAdd["Member_CorporateMobile"] = entity.Member_CorporateMobile;
            DrAdd["Member_RegisterFunds"] = entity.Member_RegisterFunds;
            DrAdd["Member_TaxationCode"] = entity.Member_TaxationCode;
            DrAdd["Member_BankAccountCode"] = entity.Member_BankAccountCode;
            DrAdd["Member_HeadImg"] = entity.Member_HeadImg;


            //新加 真实姓名  统一社会代码证号
            DrAdd["Member_RealName"] = entity.Member_RealName;
            DrAdd["Member_UniformSocial_Number"] = entity.Member_UniformSocial_Number;
           




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

        public virtual bool EditMemberProfile(MemberProfileInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Member_Profile WHERE Member_Profile_MemberID = " + entity.Member_Profile_MemberID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];

                    DrAdd["Member_Profile_MemberID"] = entity.Member_Profile_MemberID;
                    DrAdd["Member_Name"] = entity.Member_Name;
                    DrAdd["Member_Sex"] = entity.Member_Sex;
                    DrAdd["Member_StreetAddress"] = entity.Member_StreetAddress;
                    DrAdd["Member_County"] = entity.Member_County;
                    DrAdd["Member_City"] = entity.Member_City;
                    DrAdd["Member_State"] = entity.Member_State;
                    DrAdd["Member_Country"] = entity.Member_Country;
                    DrAdd["Member_Zip"] = entity.Member_Zip;
                    DrAdd["Member_Phone_Countrycode"] = entity.Member_Phone_Countrycode;
                    DrAdd["Member_Phone_Areacode"] = entity.Member_Phone_Areacode;
                    DrAdd["Member_Phone_Number"] = entity.Member_Phone_Number;
                    DrAdd["Member_Mobile"] = entity.Member_Mobile;
                    DrAdd["Member_Company"] = entity.Member_Company;
                    DrAdd["Member_Fax"] = entity.Member_Fax;
                    DrAdd["Member_QQ"] = entity.Member_QQ;
                    DrAdd["Member_OrganizationCode"] = entity.Member_OrganizationCode;
                    DrAdd["Member_BusinessCode"] = entity.Member_BusinessCode;
                    DrAdd["Member_SealImg"] = entity.Member_SealImg;
                    DrAdd["Member_Corporate"] = entity.Member_Corporate;
                    DrAdd["Member_CorporateMobile"] = entity.Member_CorporateMobile;
                    DrAdd["Member_RegisterFunds"] = entity.Member_RegisterFunds;
                    DrAdd["Member_TaxationCode"] = entity.Member_TaxationCode;
                    DrAdd["Member_BankAccountCode"] = entity.Member_BankAccountCode;
                    DrAdd["Member_HeadImg"] = entity.Member_HeadImg;



                    //新加 真实姓名  统一社会代码证号
                    DrAdd["Member_RealName"] = entity.Member_RealName;
                    DrAdd["Member_UniformSocial_Number"] = entity.Member_UniformSocial_Number;


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


        public virtual MemberProfileInfo GetMemberProfileByID(int member_id)
        {
            string SqlList = "SELECT Member_Profile_ID, Member_Profile_MemberID, Member_Name, Member_Sex";
            SqlList += ", Member_StreetAddress, Member_County, Member_City, Member_State, Member_Country";
            SqlList += ", Member_Zip, Member_Phone_Countrycode, Member_Phone_Areacode, Member_Phone_Number, Member_Mobile,Member_Company,Member_Fax,Member_QQ,Member_OrganizationCode,Member_BusinessCode,Member_SealImg,Member_Corporate,Member_CorporateMobile,Member_RegisterFunds,Member_TaxationCode,Member_BankAccountCode,Member_HeadImg,Member_RealName,Member_UniformSocial_Number FROM Member_Profile";
            SqlList += " WHERE Member_Profile_MemberID = " + member_id;
            DataTable DtList = null;
            DataRow DrList = null;
            MemberProfileInfo entity = null;

            try
            {
                DtList = DBHelper.Query(SqlList);
                if (DtList.Rows.Count > 0)
                {
                    entity = new MemberProfileInfo();
                    DrList = DtList.Rows[0];
                    entity.Member_Profile_ID = Tools.NullInt(DrList["Member_Profile_ID"]);
                    entity.Member_Profile_MemberID = Tools.NullInt(DrList["Member_Profile_MemberID"]);
                    entity.Member_Name = Tools.NullStr(DrList["Member_Name"]);
                    entity.Member_Sex = Tools.NullInt(DrList["Member_Sex"]);
                    entity.Member_StreetAddress = Tools.NullStr(DrList["Member_StreetAddress"]);
                    entity.Member_County = Tools.NullStr(DrList["Member_County"]);
                    entity.Member_City = Tools.NullStr(DrList["Member_City"]);
                    entity.Member_State = Tools.NullStr(DrList["Member_State"]);
                    entity.Member_Country = Tools.NullStr(DrList["Member_Country"]);
                    entity.Member_Zip = Tools.NullStr(DrList["Member_Zip"]);
                    entity.Member_Phone_Countrycode = Tools.NullStr(DrList["Member_Phone_Countrycode"]);
                    entity.Member_Phone_Areacode = Tools.NullStr(DrList["Member_Phone_Areacode"]);
                    entity.Member_Phone_Number = Tools.NullStr(DrList["Member_Phone_Number"]);
                    entity.Member_Mobile = Tools.NullStr(DrList["Member_Mobile"]);
                    entity.Member_Company = Tools.NullStr(DrList["Member_Company"]);
                    entity.Member_Fax = Tools.NullStr(DrList["Member_Fax"]);
                    entity.Member_QQ = Tools.NullStr(DrList["Member_QQ"]);
                    entity.Member_OrganizationCode = Tools.NullStr(DrList["Member_OrganizationCode"]);
                    entity.Member_BusinessCode = Tools.NullStr(DrList["Member_BusinessCode"]);
                    entity.Member_SealImg = Tools.NullStr(DrList["Member_SealImg"]);
                    entity.Member_Corporate = Tools.NullStr(DrList["Member_Corporate"]);
                    entity.Member_CorporateMobile = Tools.NullStr(DrList["Member_CorporateMobile"]);
                    entity.Member_RegisterFunds = Tools.NullDbl(DrList["Member_RegisterFunds"]);
                    entity.Member_TaxationCode = Tools.NullStr(DrList["Member_TaxationCode"]);
                    entity.Member_BankAccountCode = Tools.NullStr(DrList["Member_BankAccountCode"]);
                    entity.Member_HeadImg = Tools.NullStr(DrList["Member_HeadImg"]);



                    //新加 真实姓名  统一社会代码证号
                    entity.Member_RealName = Tools.NullStr(DrList["Member_RealName"]);
                    entity.Member_UniformSocial_Number = Tools.NullStr(DrList["Member_UniformSocial_Number"]);

                    DrList = null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return entity;
        }
                      
        #endregion

        #region 采购商资质关联
        public virtual bool AddMemberRelateCert(MemberRelateCertInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Member_Relate_Cert";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["ID"] = entity.ID;
            DrAdd["MemberID"] = entity.MemberID;
            DrAdd["CertID"] = entity.CertID;
            DrAdd["Img"] = entity.Img;

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

        public virtual bool EditMemberRelateCert(MemberRelateCertInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Member_Relate_Cert WHERE ID = " + entity.ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["ID"] = entity.ID;
                    DrAdd["MemberID"] = entity.MemberID;
                    DrAdd["CertID"] = entity.CertID;
                    DrAdd["Img"] = entity.Img;

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

        public virtual int DelMemberRelateCertByMemberID(int ID)
        {
            string SqlAdd = "DELETE FROM Member_Relate_Cert WHERE MemberID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IList<MemberRelateCertInfo> GetMemberRelateCerts(int Cert_MemberID)
        {
            IList<MemberRelateCertInfo> entitys = null;
            MemberRelateCertInfo entity = null;
            string SqlList;
            SqlDataReader RdrList = null;
            try
            {
                SqlList = "select * from Member_Relate_Cert where MemberID=" + Cert_MemberID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<MemberRelateCertInfo>();
                    while (RdrList.Read())
                    {
                        entity = new MemberRelateCertInfo();
                        entity.ID = Tools.NullInt(RdrList["ID"]);
                        entity.MemberID = Tools.NullInt(RdrList["MemberID"]);
                        entity.CertID = Tools.NullInt(RdrList["CertID"]);
                        entity.Img = Tools.NullStr(RdrList["Img"]);

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
        #endregion
    }

    public class MemberLog : IMemberLog
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public MemberLog()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddMemberLog(MemberLogInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Member_Log";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Log_ID"] = entity.Log_ID;
            DrAdd["Log_Member_ID"] = entity.Log_Member_ID;
            DrAdd["Log_Member_Action"] = entity.Log_Member_Action;
            DrAdd["Log_Addtime"] = entity.Log_Addtime;

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

        public virtual int DelMemberLog(int ID)
        {
            string SqlAdd = "DELETE FROM Member_Log WHERE Log_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IList<MemberLogInfo> GetMemberLogs(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<MemberLogInfo> entitys = null;
            MemberLogInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Member_Log";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<MemberLogInfo>();
                    while (RdrList.Read())
                    {
                        entity = new MemberLogInfo();
                        entity.Log_ID = Tools.NullInt(RdrList["Log_ID"]);
                        entity.Log_Member_ID = Tools.NullInt(RdrList["Log_Member_ID"]);
                        entity.Log_Member_Action = Tools.NullStr(RdrList["Log_Member_Action"]);
                        entity.Log_Addtime = Tools.NullDate(RdrList["Log_Addtime"]);

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

    public class MemberPurchase : IMemberPurchase
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public MemberPurchase()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddMemberPurchase(MemberPurchaseInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Member_Purchase";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Purchase_ID"] = entity.Purchase_ID;
            DrAdd["Purchase_MemberID"] = entity.Purchase_MemberID;
            DrAdd["Purchase_Title"] = entity.Purchase_Title;
            DrAdd["Purchase_Img"] = entity.Purchase_Img;
            DrAdd["Purchase_Amount"] = entity.Purchase_Amount;
            DrAdd["Purchase_Unit"] = entity.Purchase_Unit;
            DrAdd["Purchase_Validity"] = entity.Purchase_Validity;
            DrAdd["Purchase_Intro"] = entity.Purchase_Intro;
            DrAdd["Purchase_Status"] = entity.Purchase_Status;
            DrAdd["Purchase_Addtime"] = entity.Purchase_Addtime;
            DrAdd["Purchase_Site"] = entity.Purchase_Site;

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

        public virtual bool EditMemberPurchase(MemberPurchaseInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Member_Purchase WHERE Purchase_ID = " + entity.Purchase_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Purchase_ID"] = entity.Purchase_ID;
                    DrAdd["Purchase_MemberID"] = entity.Purchase_MemberID;
                    DrAdd["Purchase_Title"] = entity.Purchase_Title;
                    DrAdd["Purchase_Img"] = entity.Purchase_Img;
                    DrAdd["Purchase_Amount"] = entity.Purchase_Amount;
                    DrAdd["Purchase_Unit"] = entity.Purchase_Unit;
                    DrAdd["Purchase_Validity"] = entity.Purchase_Validity;
                    DrAdd["Purchase_Intro"] = entity.Purchase_Intro;
                    DrAdd["Purchase_Status"] = entity.Purchase_Status;
                    DrAdd["Purchase_Addtime"] = entity.Purchase_Addtime;
                    DrAdd["Purchase_Site"] = entity.Purchase_Site;

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

        public virtual int DelMemberPurchase(int ID)
        {
            string SqlAdd = "DELETE FROM Member_Purchase WHERE Purchase_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual MemberPurchaseInfo GetMemberPurchaseByID(int ID)
        {
            MemberPurchaseInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member_Purchase WHERE Purchase_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new MemberPurchaseInfo();

                    entity.Purchase_ID = Tools.NullInt(RdrList["Purchase_ID"]);
                    entity.Purchase_MemberID = Tools.NullInt(RdrList["Purchase_MemberID"]);
                    entity.Purchase_Title = Tools.NullStr(RdrList["Purchase_Title"]);
                    entity.Purchase_Img = Tools.NullStr(RdrList["Purchase_Img"]);
                    entity.Purchase_Amount = Tools.NullDbl(RdrList["Purchase_Amount"]);
                    entity.Purchase_Unit = Tools.NullStr(RdrList["Purchase_Unit"]);
                    entity.Purchase_Validity = Tools.NullDate(RdrList["Purchase_Validity"]);
                    entity.Purchase_Intro = Tools.NullStr(RdrList["Purchase_Intro"]);
                    entity.Purchase_Status = Tools.NullInt(RdrList["Purchase_Status"]);
                    entity.Purchase_Addtime = Tools.NullDate(RdrList["Purchase_Addtime"]);
                    entity.Purchase_Site = Tools.NullStr(RdrList["Purchase_Site"]);

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

        public virtual IList<MemberPurchaseInfo> GetMemberPurchases(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<MemberPurchaseInfo> entitys = null;
            MemberPurchaseInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Member_Purchase";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<MemberPurchaseInfo>();
                    while (RdrList.Read())
                    {
                        entity = new MemberPurchaseInfo();
                        entity.Purchase_ID = Tools.NullInt(RdrList["Purchase_ID"]);
                        entity.Purchase_MemberID = Tools.NullInt(RdrList["Purchase_MemberID"]);
                        entity.Purchase_Title = Tools.NullStr(RdrList["Purchase_Title"]);
                        entity.Purchase_Img = Tools.NullStr(RdrList["Purchase_Img"]);
                        entity.Purchase_Amount = Tools.NullDbl(RdrList["Purchase_Amount"]);
                        entity.Purchase_Unit = Tools.NullStr(RdrList["Purchase_Unit"]);
                        entity.Purchase_Validity = Tools.NullDate(RdrList["Purchase_Validity"]);
                        entity.Purchase_Intro = Tools.NullStr(RdrList["Purchase_Intro"]);
                        entity.Purchase_Status = Tools.NullInt(RdrList["Purchase_Status"]);
                        entity.Purchase_Addtime = Tools.NullDate(RdrList["Purchase_Addtime"]);
                        entity.Purchase_Site = Tools.NullStr(RdrList["Purchase_Site"]);

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
                SqlTable = "Member_Purchase";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Purchase_ID) FROM " + SqlTable + SqlParam;

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

    public class MemberPurchaseReply : IMemberPurchaseReply
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public MemberPurchaseReply()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddMemberPurchaseReply(MemberPurchaseReplyInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Member_Purchase_Reply";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Reply_ID"] = entity.Reply_ID;
            DrAdd["Reply_PurchaseID"] = entity.Reply_PurchaseID;
            DrAdd["Reply_SupplierID"] = entity.Reply_SupplierID;
            DrAdd["Reply_Title"] = entity.Reply_Title;
            DrAdd["Reply_Content"] = entity.Reply_Content;
            DrAdd["Reply_Contactman"] = entity.Reply_Contactman;
            DrAdd["Reply_Mobile"] = entity.Reply_Mobile;
            DrAdd["Reply_Email"] = entity.Reply_Email;
            DrAdd["Reply_Addtime"] = entity.Reply_Addtime;

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

        public virtual bool EditMemberPurchaseReply(MemberPurchaseReplyInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Member_Purchase_Reply WHERE Reply_ID = " + entity.Reply_ID;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Reply_ID"] = entity.Reply_ID;
                    DrAdd["Reply_PurchaseID"] = entity.Reply_PurchaseID;
                    DrAdd["Reply_SupplierID"] = entity.Reply_SupplierID;
                    DrAdd["Reply_Title"] = entity.Reply_Title;
                    DrAdd["Reply_Content"] = entity.Reply_Content;
                    DrAdd["Reply_Contactman"] = entity.Reply_Contactman;
                    DrAdd["Reply_Mobile"] = entity.Reply_Mobile;
                    DrAdd["Reply_Email"] = entity.Reply_Email;
                    DrAdd["Reply_Addtime"] = entity.Reply_Addtime;

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

        public virtual int DelMemberPurchaseReply(int ID)
        {
            string SqlAdd = "DELETE FROM Member_Purchase_Reply WHERE Reply_ID = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual MemberPurchaseReplyInfo GetMemberPurchaseReplyByID(int ID)
        {
            MemberPurchaseReplyInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member_Purchase_Reply WHERE Reply_ID = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new MemberPurchaseReplyInfo();

                    entity.Reply_ID = Tools.NullInt(RdrList["Reply_ID"]);
                    entity.Reply_PurchaseID = Tools.NullInt(RdrList["Reply_PurchaseID"]);
                    entity.Reply_SupplierID = Tools.NullInt(RdrList["Reply_SupplierID"]);
                    entity.Reply_Title = Tools.NullStr(RdrList["Reply_Title"]);
                    entity.Reply_Content = Tools.NullStr(RdrList["Reply_Content"]);
                    entity.Reply_Contactman = Tools.NullStr(RdrList["Reply_Contactman"]);
                    entity.Reply_Mobile = Tools.NullStr(RdrList["Reply_Mobile"]);
                    entity.Reply_Email = Tools.NullStr(RdrList["Reply_Email"]);
                    entity.Reply_Addtime = Tools.NullDate(RdrList["Reply_Addtime"]);

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

        public virtual IList<MemberPurchaseReplyInfo> GetMemberPurchaseReplys(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<MemberPurchaseReplyInfo> entitys = null;
            MemberPurchaseReplyInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Member_Purchase_Reply";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<MemberPurchaseReplyInfo>();
                    while (RdrList.Read())
                    {
                        entity = new MemberPurchaseReplyInfo();
                        entity.Reply_ID = Tools.NullInt(RdrList["Reply_ID"]);
                        entity.Reply_PurchaseID = Tools.NullInt(RdrList["Reply_PurchaseID"]);
                        entity.Reply_SupplierID = Tools.NullInt(RdrList["Reply_SupplierID"]);
                        entity.Reply_Title = Tools.NullStr(RdrList["Reply_Title"]);
                        entity.Reply_Content = Tools.NullStr(RdrList["Reply_Content"]);
                        entity.Reply_Contactman = Tools.NullStr(RdrList["Reply_Contactman"]);
                        entity.Reply_Mobile = Tools.NullStr(RdrList["Reply_Mobile"]);
                        entity.Reply_Email = Tools.NullStr(RdrList["Reply_Email"]);
                        entity.Reply_Addtime = Tools.NullDate(RdrList["Reply_Addtime"]);

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
                SqlTable = "Member_Purchase_Reply";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Reply_ID) FROM " + SqlTable + SqlParam;

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

    public class MemberToken : IMemberToken
    {
        ITools Tools;
        ISQLHelper DBHelper;
        public MemberToken()
        {
            Tools = ToolsFactory.CreateTools();
            DBHelper = SQLHelperFactory.CreateSQLHelper();
        }

        public virtual bool AddMemberToken(MemberTokenInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT TOP 0 * FROM Member_Token";
            DtAdd = DBHelper.Query(SqlAdd);
            DrAdd = DtAdd.NewRow();

            DrAdd["Token"] = entity.Token;
            DrAdd["Type"] = entity.Type;
            DrAdd["MemberID"] = entity.MemberID;
            DrAdd["IP"] = entity.IP;
            DrAdd["CreateTime"] = entity.CreateTime;
            DrAdd["UpdateTime"] = entity.UpdateTime;
            DrAdd["ExpiredTime"] = entity.ExpiredTime;

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

        public virtual bool EditMemberToken(MemberTokenInfo entity)
        {
            string SqlAdd = null;
            DataTable DtAdd = null;
            DataRow DrAdd = null;
            SqlAdd = "SELECT * FROM Member_Token WHERE Token = " + entity.Token;
            DtAdd = DBHelper.Query(SqlAdd);
            try
            {
                if (DtAdd.Rows.Count > 0)
                {
                    DrAdd = DtAdd.Rows[0];
                    DrAdd["Token"] = entity.Token;
                    DrAdd["Type"] = entity.Type;
                    DrAdd["MemberID"] = entity.MemberID;
                    DrAdd["IP"] = entity.IP;
                    DrAdd["CreateTime"] = entity.CreateTime;
                    DrAdd["UpdateTime"] = entity.UpdateTime;
                    DrAdd["ExpiredTime"] = entity.ExpiredTime;

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

        public virtual bool  CheckMemberToken(string user, string pwd, int type,int tokentime)
        { 
            StringBuilder strSql = new StringBuilder();
            DataTable dt = new DataTable();

            bool IsHave = false;

            if (type == 1)
            {
                strSql.Append("select M.Member_NickName,M.Member_ID,MT.Token from Member as M inner join Member_Token as MT on M.Member_ID=MT.MemberID");
                strSql.Append(" WHERE M.Member_NickName = '" + user + "' AND Member_Password = '" + pwd.ToLower() + "'  and DATEDIFF(n, UpdateTime, '" + DateTime.Now + "')<= " + tokentime + " and DATEDIFF(n, '" + DateTime.Now + "', ExpiredTime) >= 0 and MT.Type=" + type);

                dt = DBHelper.Query(strSql.ToString());

                if (dt.Rows.Count > 0)
                {
                    IsHave = true;
                }
                else
                {
                    IsHave = false;
                }
            }
            else
            {
                strSql.Append("select S.Supplier_Nickname,S.Supplier_ID,MT.Token from Supplier as S inner join Member_Token as MT on S.Supplier_ID=MT.MemberID");

                strSql.Append(" WHERE S.Supplier_Nickname = '" + user + "' AND S.Supplier_Password = '" + pwd.ToLower() + "'  and DATEDIFF(n, UpdateTime, '" + DateTime.Now + "')<= " + tokentime + " and DATEDIFF(n, '" + DateTime.Now + "', ExpiredTime) >= 0 and MT.Type=" + type);

                dt = DBHelper.Query(strSql.ToString());

                if (dt.Rows.Count > 0)
                {
                    IsHave = true;
                }
                else
                {
                    IsHave = false;
                }
            }

            return IsHave ;
        }

        public virtual int DelMemberToken(int ID)
        {
            string SqlAdd = "DELETE FROM Member_Token WHERE Token = " + ID;
            try
            {
                return DBHelper.ExecuteNonQuery(SqlAdd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetMemberToken(string user, string pwd, int type, int tokentime)
        {
            StringBuilder strSql = new StringBuilder();
            DataTable dt = new DataTable();

            string strToken = "";

            if (type == 1)
            {
                strSql.Append("select M.Member_NickName,M.Member_ID,MT.Token from Member as M inner join Member_Token as MT on M.Member_ID=MT.MemberID");
                strSql.Append(" WHERE M.Member_NickName = '" + user + "' AND Member_Password = '" + pwd.ToLower() + "'  and DATEDIFF(n, UpdateTime, '" + DateTime.Now + "')<= " + tokentime + " and DATEDIFF(n, '" + DateTime.Now + "', ExpiredTime) >= 0 and MT.Type=" + type);

                dt = DBHelper.Query(strSql.ToString());

                if (dt.Rows.Count > 0)
                {
                    strToken = Tools.NullStr(dt.Rows[0]["Token"]);
                }
            }
            else
            {
                strSql.Append("select S.Supplier_Nickname,S.Supplier_ID,MT.Token from Supplier as S inner join Member_Token as MT on S.Supplier_ID=MT.MemberID");

                strSql.Append(" WHERE S.Supplier_Nickname = '" + user + "' AND S.Supplier_Password = '" + pwd.ToLower() + "'  and DATEDIFF(n, UpdateTime, '" + DateTime.Now + "')<= " + tokentime + " and DATEDIFF(n, '" + DateTime.Now + "', ExpiredTime) >= 0 and MT.Type=" + type);

                dt = DBHelper.Query(strSql.ToString());

                if (dt.Rows.Count > 0)
                {
                    strToken = Tools.NullStr(dt.Rows[0]["Token"]);
                }
            }
            return strToken;
        }

        public virtual MemberTokenInfo GetMemberTokenByID(int ID)
        {
            MemberTokenInfo entity = null;
            SqlDataReader RdrList = null;
            try
            {
                string SqlList;
                SqlList = "SELECT * FROM Member_Token WHERE Token = " + ID;
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.Read())
                {
                    entity = new MemberTokenInfo();

                    entity.Token = Tools.NullStr(RdrList["Token"]);
                    entity.Type = Tools.NullInt(RdrList["Type"]);
                    entity.MemberID = Tools.NullInt(RdrList["MemberID"]);
                    entity.IP = Tools.NullStr(RdrList["IP"]);
                    entity.CreateTime = Tools.NullDate(RdrList["CreateTime"]);
                    entity.UpdateTime = Tools.NullDate(RdrList["UpdateTime"]);
                    entity.ExpiredTime = Tools.NullDate(RdrList["ExpiredTime"]);

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

        public virtual IList<MemberTokenInfo> GetMemberTokens(QueryInfo Query)
        {
            int PageSize;
            int CurrentPage;
            IList<MemberTokenInfo> entitys = null;
            MemberTokenInfo entity = null;
            string SqlList, SqlField, SqlOrder, SqlParam, SqlTable;
            SqlDataReader RdrList = null;
            try
            {
                CurrentPage = Query.CurrentPage;
                PageSize = Query.PageSize;
                SqlTable = "Member_Token";
                SqlField = "*";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlOrder = DBHelper.GetSqlOrder(Query.OrderInfos);
                SqlList = DBHelper.GetSqlPage(SqlTable, SqlField, SqlParam, SqlOrder, CurrentPage, PageSize);
                RdrList = DBHelper.ExecuteReader(SqlList);
                if (RdrList.HasRows)
                {
                    entitys = new List<MemberTokenInfo>();
                    while (RdrList.Read())
                    {
                        entity = new MemberTokenInfo();

                        entity.Token = Tools.NullStr(RdrList["Token"]);
                        entity.Type = Tools.NullInt(RdrList["Type"]);
                        entity.MemberID = Tools.NullInt(RdrList["MemberID"]);
                        entity.IP = Tools.NullStr(RdrList["IP"]);
                        entity.CreateTime = Tools.NullDate(RdrList["CreateTime"]);
                        entity.UpdateTime = Tools.NullDate(RdrList["UpdateTime"]);
                        entity.ExpiredTime = Tools.NullDate(RdrList["ExpiredTime"]);

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
                SqlTable = "Member_Token";
                SqlParam = DBHelper.GetSqlParam(Query.ParamInfos);
                SqlCount = "SELECT COUNT(Token) FROM " + SqlTable + SqlParam;

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
