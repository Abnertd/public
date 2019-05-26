using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.BLL.Sys;

/// <summary>
///Member 的摘要说明
/// </summary>
public class Member
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IMember MyBLL;
    private IMail mail;
    private IFeedBack MyFeedback;
    private IMemberConsumption MyCoinlog;
    private IMemberAccountLog MyAccountLog;
    private MemberGrade MyGrade;
    private IMemberGrade MyMGBLL;
    private ISources MySource;
    private IMemberInvoice MyInvoice;
    private IMemberCert MyCert;
    private Addr addr;


    public Member()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = MemberFactory.CreateMember();
        MyFeedback = FeedBackFactory.CreateFeedBack();
        MyCoinlog = MemberConsumptionFactory.CreateMemberConsumption();
        mail = MailFactory.CreateMail();
        MyAccountLog = MemberAccountLogFactory.CreateMemberAccountLog();
        MyGrade = new MemberGrade();
        MyMGBLL = MemberGradeFactory.CreateMemberGrade();
        MySource = SourcesFactory.CreateSources();
        MyInvoice = MemberInvoiceFactory.CreateMemberInvoice();
        MyCert = MemberCertFactory.CreateMemberCert();
        addr = new Addr();
    }

    public string Member_AuditStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        { 
            case -1:
                resultstr = "未提交审核";
                break;
            case 0:
                resultstr = "待审核";
                break;
            case 1:
                resultstr = "审核通过";
                break;
            case 2:
                resultstr = "审核未通过";
                break;
        }
        return resultstr;
    }

    public MemberInfo GetMemberByID(int ID)
    {
        return MyBLL.GetMemberByID(ID, Public.GetUserPrivilege());
    }


    public MemberInfo GetMemberByEmail(string Email)
    {
        return MyBLL.GetMemberByEmail(Email, Public.GetUserPrivilege());
    }


    public MemberProfileInfo GetMemberProfileByID(int ID)
    {
        return MyBLL.GetMemberProfileByID(ID, Public.GetUserPrivilege());
    }


    public bool EditMemberProfile(MemberProfileInfo entity)
    {
        return MyBLL.EditMemberProfile(entity, Public.GetUserPrivilege());
    }




    //通过会员表里对应的商家ID,获取会员ID
    public int GetMemberID_BySupplierID(int SupplierID)
    {
        int Member_id = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_SupplierID", "=", SupplierID.ToString()));
        IList<MemberInfo> entitys = MyBLL.GetMembers(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            if (entitys.Count==1)
            {
                foreach (MemberInfo entity in entitys)
                {
                    Member_id = entity.Member_ID;
                }
            }
            
        }
        
        return Member_id;
    }


    public string GetMembers()
    {
        int member_grade = tools.CheckInt(Request["member_grade"]);
        string member_source = tools.CheckStr(Request["member_source"]);
        string date_start = tools.CheckStr(Request.QueryString["date_start"]);
        string date_end = tools.CheckStr(Request.QueryString["date_end"]);
        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", Public.GetCurrentSite()));

        string listtype = tools.CheckStr(Request["listtype"]);
        switch (listtype)
        {
            case "uncommitted":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_AuditStatus", "=", "-1"));
                break;
            case "unaudit":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_AuditStatus", "=", "0"));
                break;
            case "audit":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_AuditStatus", "=", "1"));
                break;
            case "denyaudit":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_AuditStatus", "=", "2"));
                break;
            case "activate":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Emailverify", "=", "1"));
                break;
            case "inactive":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Emailverify", "=", "0"));
                break;
            case "emailsubscribe":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_AllowSysEmail", "=", "1"));
                break;
        }

        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "MemberInfo.Member_NickName", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberInfo.Member_Email", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberProfileInfo.Member_Name", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberProfileInfo.Member_Phone_Number", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "MemberProfileInfo.Member_Mobile", "like", keyword));
        }
        if (member_grade > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Grade", "=", member_grade.ToString()));
        }
        if (date_start != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_start + "',{MemberInfo.Member_Addtime})", ">=", "0"));
        }
        if (date_end != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_end + "',{MemberInfo.Member_Addtime})", "<=", "0"));
        }
        if (member_source != "")
        {
            if (member_source == "no-source")
            {
                member_source = "";
            }
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Source", "=", member_source));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<MemberInfo> entitys = MyBLL.GetMembers(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {
            MemberProfileInfo ProfileInfo;

            MemberGradeInfo membergrade;
            Sources sources = new Sources();

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (MemberInfo entity in entitys)
            {
                if (entity.MemberProfileInfo != null) { ProfileInfo = entity.MemberProfileInfo; }
                else { ProfileInfo = new MemberProfileInfo(); }

                membergrade = MyGrade.GetMemberGradeByID(entity.Member_Grade);

                jsonBuilder.Append("{\"id\":" + entity.Member_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Member_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Member_NickName));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Member_Email));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.MemberProfileInfo.Member_Company));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.MemberProfileInfo.Member_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.MemberProfileInfo.Member_Mobile));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                //jsonBuilder.Append(Public.JsonStr(entity.MemberProfileInfo.Member_Phone_Number));
                jsonBuilder.Append(Public.JsonStr(entity.Member_LoginMobile));
                jsonBuilder.Append("\",");

                //jsonBuilder.Append("\"");
                //jsonBuilder.Append(Member_AuditStatus(entity.Member_AuditStatus));
                //jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Member_Addtime.ToString("yyyy-MM-dd HH:mm:ss"));
                jsonBuilder.Append("\",");               

                jsonBuilder.Append("\"");
                //jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\" align=\\\"absmiddle\\\"> <a href=\\\"member_edit.aspx?member_id=" + entity.Member_ID + "\\\" title=\\\"修改\\\">修改</a>");

                jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"查看\\\" align=\\\"absmiddle\\\"> <a href=\\\"member_view.aspx?member_id=" + entity.Member_ID + "\\\" title=\\\"查看\\\">查看</a>");
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");

                ProfileInfo = null;
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    public void UpdateMemberProfile()
    {
        int Member_ID = tools.CheckInt(Request.Form["member_id"]);
        string Member_Profile_CompanyName = tools.CheckStr(Request.Form["Member_Profile_CompanyName"]);
        string Member_Profile_County = tools.CheckStr(Request.Form["Member_Profile_County"]);
        string Member_Profile_City = tools.CheckStr(Request.Form["Member_Profile_City"]);
        string Member_Profile_State = tools.CheckStr(Request.Form["Member_Profile_State"]);
        string Member_Profile_Country = tools.CheckStr(Request.Form["Member_Profile_Country"]);
        string Member_Profile_Address = tools.CheckStr(Request.Form["Member_Profile_Address"]);
        string Member_Profile_Phone = tools.CheckStr(Request.Form["Member_Profile_Phone"]);
        string Member_Profile_Fax = tools.CheckStr(Request.Form["Member_Profile_Fax"]);
        string Member_Profile_Zip = tools.CheckStr(Request.Form["Member_Profile_Zip"]);
        string Member_Profile_Contactman = tools.CheckStr(Request.Form["Member_Profile_Contactman"]);
        string Member_Profile_Mobile = tools.CheckStr(Request.Form["Member_Profile_Mobile"]);
        string Member_Profile_QQ = tools.CheckStr(Request.Form["Member_Profile_QQ"]);
        string Member_Profile_OrganizationCode = tools.CheckStr(Request.Form["Member_Profile_OrganizationCode"]);
        string Member_Profile_BusinessCode = tools.CheckStr(Request.Form["Member_Profile_BusinessCode"]);
        string Member_Profile_SealImg = tools.CheckStr(Request.Form["Member_Profile_SealImg"]);
        string Member_Corporate = tools.CheckStr(Request.Form["Member_Corporate"]);
        string Member_CorporateMobile = tools.CheckStr(Request.Form["Member_CorporateMobile"]);
        double Member_RegisterFunds = tools.CheckFloat(Request.Form["Member_RegisterFunds"]);
        string Member_TaxationCode = tools.CheckStr(Request.Form["Member_TaxationCode"]);
        string Member_BankAccountCode = tools.CheckStr(Request.Form["Member_BankAccountCode"]);
        string Member_HeadImg = tools.CheckStr(Request.Form["Member_HeadImg"]);

        string hidden_type = tools.CheckStr(Request.Form["hidden_type"]);


        if (Member_Profile_County == "0" || Member_Profile_County == "")
        {
            Response.Write("请选择省市区信息");
            Response.End();
        }
        if (Member_Profile_Address == "")
        {
            Response.Write("请将联系地址填写完整");
            Response.End();
        }

        if (Member_Profile_Mobile == "")
        {
            Response.Write("请填写手机号码");
            Response.End();
        }
        if (Member_Profile_Mobile != "")
        {
            if (Public.Checkmobile(Member_Profile_Mobile) == false)
            {
                Response.Write("手机号码错误");
                Response.End();
            }
        }
        if (Member_Profile_Contactman == "")
        {
            Response.Write("请将联系人填写完整");
            Response.End();
        }
        if (Member_Profile_BusinessCode == "")
        {
            Response.Write("请将营业执照代码填写完整");
            Response.End();
        }

        if (Member_Corporate == "")
        {
            Response.Write("请将法定代表人姓名填写完整");
            Response.End();
        }

        if (Member_CorporateMobile == "")
        {
            Response.Write("请将法定代表人电话填写完整");
            Response.End();
        }

        if (Member_Profile_OrganizationCode == "")
        {
            Response.Write("请将组织机构代码填写完整");
            Response.End();
        }

        if (Member_BankAccountCode == "")
        {
            Response.Write("请将银行开户许可证号填写完整");
            Response.End();
        }


        MemberInfo entity = GetMemberByID(Member_ID);
        if (entity != null && entity.MemberProfileInfo != null)
        {

            if (entity.Member_AuditStatus != 1)
            {
                if (Member_Profile_CompanyName == "")
                {
                    Response.Write("请将公司名称填写完整");
                    Response.End();
                }
                entity.MemberProfileInfo.Member_Company = Member_Profile_CompanyName;
            }

            entity.Member_LoginMobile = Member_Profile_Mobile;

            if (MyBLL.EditMember(entity, Public.GetUserPrivilege()))
            {
                entity.MemberProfileInfo.Member_Country = Member_Profile_Country;
                entity.MemberProfileInfo.Member_County = Member_Profile_County;               
                entity.MemberProfileInfo.Member_State = Member_Profile_State;
                entity.MemberProfileInfo.Member_City = Member_Profile_City;
                entity.MemberProfileInfo.Member_StreetAddress = Member_Profile_Address;
                entity.MemberProfileInfo.Member_Name = Member_Profile_Contactman;
                entity.MemberProfileInfo.Member_Fax = Member_Profile_Fax;
                entity.MemberProfileInfo.Member_Phone_Number = Member_Profile_Phone;
                entity.MemberProfileInfo.Member_QQ = Member_Profile_QQ;
                entity.MemberProfileInfo.Member_OrganizationCode = Member_Profile_OrganizationCode;
                entity.MemberProfileInfo.Member_BusinessCode = Member_Profile_BusinessCode;
                entity.MemberProfileInfo.Member_SealImg = Member_Profile_SealImg;
                entity.MemberProfileInfo.Member_Corporate = Member_Corporate;
                entity.MemberProfileInfo.Member_CorporateMobile = Member_CorporateMobile;
                entity.MemberProfileInfo.Member_RegisterFunds = Member_RegisterFunds;
                entity.MemberProfileInfo.Member_TaxationCode = Member_TaxationCode;
               
                entity.MemberProfileInfo.Member_BankAccountCode = Member_BankAccountCode;
                entity.MemberProfileInfo.Member_HeadImg = Member_HeadImg;

                MyBLL.EditMemberProfile(entity.MemberProfileInfo, Public.GetUserPrivilege());


                //Modify_Enterprise_Info(entity);


                Response.Write("success");
                Response.End();
            }
            else
            {
                Response.Write("信息保存失败，请稍后再试！");
                Response.End();
            }
        }
        else
        {
            Response.Write("信息保存失败，请稍后再试！");
            Response.End();
        }
    }

    //根据昵称关键词获取指定条件会员编号
    public string GetMemberIDByKeyword(string keyword)
    {
        string MemberID = "";
        if (keyword.Length == 0)
        {
            return "0";
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_NickName", "like", keyword));

        IList<MemberInfo> entitys = MyBLL.GetMembers(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (MemberInfo entity in entitys)
            {
                if (MemberID == "")
                {
                    MemberID = entity.Member_ID.ToString();
                }
                else
                {
                    MemberID = MemberID + "," + entity.Member_ID.ToString();
                }
            }
        }
        if (MemberID == "")
        {
            MemberID = "0";
        }
        return MemberID;

    }

    //会员积分消费
    public void Member_Coin_AddConsume(int coin_amount, string coin_reason, int member_id, bool is_return)
    {
        int Member_CoinRemain = 0;
        MemberInfo member = MyBLL.GetMemberByID(member_id, Public.GetUserPrivilege());
        if (member != null)
        {
            Member_CoinRemain = member.Member_CoinRemain;
            MemberConsumptionInfo consumption = new MemberConsumptionInfo();
            consumption.Consump_ID = 0;
            consumption.Consump_MemberID = member_id;
            consumption.Consump_Coin = coin_amount;
            consumption.Consump_CoinRemain = Member_CoinRemain + coin_amount;
            consumption.Consump_Reason = coin_reason;
            consumption.Consump_Addtime = DateTime.Now;

            MyCoinlog.AddMemberConsumption(consumption);

            if (coin_amount > 0)
            {
                if (is_return)
                {
                    member.Member_CoinRemain = Member_CoinRemain + coin_amount;
                }
                else
                {
                    member.Member_CoinRemain = Member_CoinRemain + coin_amount;
                    member.Member_CoinCount = member.Member_CoinCount + coin_amount;
                }
            }
            else
            {
                member.Member_CoinRemain = Member_CoinRemain + coin_amount;
            }

            MyBLL.EditMember(member, Public.GetUserPrivilege());
        }
    }

    //会员虚拟账号消费
    public void Member_Account_Log(int Member_ID, double Amount, string Log_note)
    {
        double Member_AccountRemain = 0;
        MemberInfo member = MyBLL.GetMemberByID(Member_ID, Public.GetUserPrivilege());
        if (member != null)
        {
            Member_AccountRemain = member.Member_Account;
            MemberAccountLogInfo accountLog = new MemberAccountLogInfo();
            accountLog.Account_Log_ID = 0;
            accountLog.Account_Log_MemberID = Member_ID;
            accountLog.Account_Log_Amount = Amount;
            accountLog.Account_Log_Remain = Member_AccountRemain + Amount;
            accountLog.Account_Log_Note = Log_note;
            accountLog.Account_Log_Addtime = DateTime.Now;
            accountLog.Account_Log_Site = Public.GetCurrentSite();

            MyAccountLog.AddMemberAccountLog(accountLog);

            if (Amount > 0)
            {
                member.Member_Account = Member_AccountRemain + Amount;
            }

            MyBLL.EditMember(member, Public.GetUserPrivilege());
        }
    }

    //会员导出
    public void Member_Export()
    {
        string MembersArry = tools.CheckStr(Request["member_id"]);
        if (MembersArry == "")
        {
            Public.Msg("error", "错误信息", "请选择要导出的信息", false, "{back}");
            return;
        }

        if (tools.Left(MembersArry, 1) == ",") { MembersArry = MembersArry.Remove(0, 1); }

        DataTable dt = new DataTable("excel");
        DataRow dr = null;
        DataColumn column = null;

        string[] dtcol = { "ID", "昵称", "注册邮箱", "姓名", "性别", "电话", "手机", "会员等级", "虚拟账户余额", "可用积分", "注册时间", "来源" };
        foreach (string col in dtcol)
        {
            try { dt.Columns.Add(col); }
            catch { dt.Columns.Add(col + ","); }
        }
        dtcol = null;

        int Orders_ID = 0;
        QueryInfo Query = new QueryInfo();
        MemberInfo memberinfo = null;
        MemberProfileInfo memberprofile = null;
        OrdersPaymentInfo orderspayment = null;
        OrdersDeliveryInfo ordersdelivery = null;
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_ID", "in", MembersArry));
        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "DESC"));

        IList<MemberInfo> entitys = MyBLL.GetMembers(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            MemberProfileInfo ProfileInfo;
            MemberGradeInfo membergrade;
            foreach (MemberInfo entity in entitys)
            {
                if (entity.MemberProfileInfo != null) { ProfileInfo = entity.MemberProfileInfo; }
                else { ProfileInfo = new MemberProfileInfo(); }
                membergrade = MyGrade.GetMemberGradeByID(entity.Member_Grade);


                dr = dt.NewRow();

                dr[0] = entity.Member_ID;
                dr[1] = entity.Member_NickName;
                dr[2] = entity.Member_Email;
                dr[3] = ProfileInfo.Member_Name;
                dr[4] = Public.DisplaySex(ProfileInfo.Member_Sex);
                dr[5] = ProfileInfo.Member_Phone_Number;
                dr[6] = ProfileInfo.Member_Mobile;
                if (membergrade != null)
                {
                    dr[7] = membergrade.Member_Grade_Name;
                }
                else
                {
                    dr[7] = "--";
                }
                dr[8] = entity.Member_Account;
                dr[9] = entity.Member_CoinRemain;
                dr[10] = entity.Member_Addtime;
                dr[11] = entity.Member_Source;
                membergrade = null;


                dt.Rows.Add(dr);

            }
        }




        Public.toExcel(dt);
    }

    //会员是否冻结
    public void Member_Audit(int Status)
    {
        string MembersArry = tools.CheckStr(Request["member_id"]);
        if (MembersArry == "")
        {
            Public.Msg("error", "错误信息", "请选择要操作的信息", false, "{back}");
            return;
        }

        if (tools.Left(MembersArry, 1) == ",") { MembersArry = MembersArry.Remove(0, 1); }
        
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_ID", "in", MembersArry));
        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "DESC"));

        IList<MemberInfo> entitys = MyBLL.GetMembers(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (MemberInfo entity in entitys)
            {
                if (Status == 1)
                {
                    entity.Member_Cert_Status = 2;
                }
                entity.Member_AuditStatus = Status;
                if (MyBLL.EditMember(entity, Public.GetUserPrivilege()))
                {
                    if (Status == 1)
                    {
                        //发送短信
                        SMS mySMS = new SMS();
                        mySMS.Send(entity.Member_LoginMobile, entity.Member_NickName, "meber_to_supplier");
                       // new SMS().Send(entity.Member_LoginMobile, "user_audit", null);
                    }
                }
            }
        }
        Response.Redirect("/member/member_list.aspx?listtype=" + tools.NullStr(Request["listtype"]));
    }

    public string GetMemberConsumptions()
    {

        string Member_IDstr = "";

        string keyword, date_start, date_end;
        //关键词
        keyword = tools.CheckStr(Request["keyword"]);
        if (keyword != "")
        {
            Member_IDstr = "0";
            QueryInfo Query1 = new QueryInfo();
            Query1.PageSize = 0;
            Query1.CurrentPage = 1;
            Query1.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_NickName", "like", keyword));
            IList<MemberInfo> members = MyBLL.GetMembers(Query1, Public.GetUserPrivilege());
            if (members != null)
            {
                foreach (MemberInfo ent in members)
                {
                    Member_IDstr = Member_IDstr + "," + ent.Member_ID;
                }
            }
            Query1 = null;
        }


        //开始时间
        date_start = tools.CheckStr(Request["date_start"]);

        //结束时间
        date_end = tools.CheckStr(Request["date_end"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (tools.CheckInt(Request["rows"]) < 1)
        {
            Query.PageSize = 1;
        }
        if (Member_IDstr != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberConsumptionInfo.Consump_MemberID", "in", Member_IDstr));
        }
        if (date_start != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_start + "',{MemberConsumptionInfo.Consump_Addtime})", ">=", "0"));
        }
        if (date_end != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_end + "',{MemberConsumptionInfo.Consump_Addtime})", "<=", "0"));
        }

        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyCoinlog.GetPageInfo(Query);
        IList<MemberConsumptionInfo> entitys = MyCoinlog.GetMemberConsumptions(Query);

        if (entitys != null)
        {
            MemberInfo memberinfo = null;

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (MemberConsumptionInfo entity in entitys)
            {



                jsonBuilder.Append("{\"MemberConsumptionInfo.Consump_ID\":" + entity.Consump_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Consump_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                memberinfo = MyBLL.GetMemberByID(entity.Consump_MemberID, Public.GetUserPrivilege());
                if (memberinfo != null)
                {
                    jsonBuilder.Append(Public.JsonStr(memberinfo.Member_NickName));
                }
                else
                {
                    jsonBuilder.Append("未知");
                }
                memberinfo = null;
                jsonBuilder.Append("\",");

                if (entity.Consump_Coin > 0)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(entity.Consump_Coin);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("");
                    jsonBuilder.Append("\",");
                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(entity.Consump_Coin);
                    jsonBuilder.Append("\",");
                }


                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Consump_CoinRemain);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Consump_Reason));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Consump_Addtime);
                jsonBuilder.Append("\",");



                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");


            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    //用户积分处理
    public void Member_Coin_Process()
    {
        string member_nickname = tools.CheckStr(Request["member_name"]);
        int coin_amount = tools.CheckInt(Request["coin_amount"]);
        string coin_reason = tools.CheckStr(Request["coin_reason"]);
        int member_id = 0;
        int coin_remain = 0;

        if (member_nickname == "" || coin_amount == 0 || coin_reason == "")
        {
            Public.Msg("error", "错误提示", "请将输入要操作用户名\\积分\\备注", false, "{back}");
            Response.End();
        }
        MemberInfo memberinfo = MyBLL.GetMemberByNickName(member_nickname, Public.GetUserPrivilege());
        if (memberinfo != null)
        {
            member_id = memberinfo.Member_ID;
            coin_remain = memberinfo.Member_CoinRemain;
        }
        memberinfo = null;
        if (member_id == 0)
        {
            Public.Msg("error", "错误提示", "用户不存在", false, "{back}");
            Response.End();
        }
        if (coin_amount < (0 - coin_remain))
        {
            Public.Msg("error", "错误提示", "扣除积分超过会员可用积分", false, "{back}");
            Response.End();
        }
        Member_Coin_AddConsume(coin_amount, coin_reason, member_id, false);
        Public.Msg("positive", "操作成功", "操作成功", true, "coin_detail.aspx");
    }

    //用户虚拟账户处理
    public void Member_Account_Process()
    {
        string member_nickname = tools.CheckStr(Request["member_name"]);
        double account_amount = tools.CheckFloat(Request["account_amount"]);
        string account_reason = tools.CheckStr(Request["account_reason"]);
        int member_id = 0;
        double account_remain = 0;

        if (member_nickname == "" || account_amount == 0 || account_reason == "")
        {
            Public.Msg("error", "错误提示", "请将输入要操作用户名\\金额\\备注", false, "{back}");
            Response.End();
        }
        MemberInfo memberinfo = MyBLL.GetMemberByNickName(member_nickname, Public.GetUserPrivilege());
        if (memberinfo != null)
        {
            member_id = memberinfo.Member_ID;
            account_remain = memberinfo.Member_Account;
        }
        memberinfo = null;
        if (member_id == 0)
        {
            Public.Msg("error", "错误提示", "用户不存在", false, "{back}");
            Response.End();
        }
        if (account_amount < (0 - account_remain))
        {
            Public.Msg("error", "错误提示", "扣除金额超过会员虚拟账户余额", false, "{back}");
            Response.End();
        }
        Member_Account_Log( member_id,account_amount, account_reason);
        Public.Msg("positive", "操作成功", "操作成功", true, "Account_detail.aspx");
    }

    //虚拟账号明细
    public string GetMemberAccountLogs()
    {

        string Member_IDstr = "";
        string keyword, date_start, date_end;
        //关键词
        keyword = tools.CheckStr(Request["keyword"]);
        if (keyword != "")
        {
            Member_IDstr = "0";
            QueryInfo Query1 = new QueryInfo();
            Query1.PageSize = 0;
            Query1.CurrentPage = 1;
            Query1.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_NickName", "like", keyword));
            IList<MemberInfo> members = MyBLL.GetMembers(Query1, Public.GetUserPrivilege());
            if (members != null)
            {
                foreach (MemberInfo ent in members)
                {
                    Member_IDstr = Member_IDstr + "," + ent.Member_ID;
                }
            }
            Query1 = null;
        }


        //开始时间
        date_start = tools.CheckStr(Request["date_start"]);

        //结束时间
        date_end = tools.CheckStr(Request["date_end"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (tools.CheckInt(Request["rows"]) < 1)
        {
            Query.PageSize = 1;
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberAccountLogInfo.Account_Log_Site", "=", Public.GetCurrentSite()));
        if (Member_IDstr != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberAccountLogInfo.Account_Log_MemberID", "in", Member_IDstr));
        }
        if (date_start != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_start + "',{MemberAccountLogInfo.Account_Log_Addtime})", ">=", "0"));
        }
        if (date_end != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + date_end + "',{MemberAccountLogInfo.Account_Log_Addtime})", "<=", "0"));
        }

        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyAccountLog.GetPageInfo(Query);
        IList<MemberAccountLogInfo> entitys = MyAccountLog.GetMemberAccountLogs(Query);
        if (entitys != null)
        {
            string membernickname = "";

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (MemberAccountLogInfo entity in entitys)
            {

                membernickname = Get_MemberNickname(entity.Account_Log_MemberID);


                jsonBuilder.Append("{\"MemberAccountLogInfo.Account_Log_ID\":" + entity.Account_Log_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Account_Log_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (membernickname != "")
                {
                    jsonBuilder.Append(Public.JsonStr(membernickname));
                }
                else
                {
                    jsonBuilder.Append("未知");
                }

                jsonBuilder.Append("\",");

                if (entity.Account_Log_Amount > 0)
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(entity.Account_Log_Amount);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("");
                    jsonBuilder.Append("\",");
                }
                else
                {
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(entity.Account_Log_Amount);
                    jsonBuilder.Append("\",");
                }


                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Account_Log_Remain);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Account_Log_Note));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Account_Log_Addtime);
                jsonBuilder.Append("\",");



                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");


            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    public string Get_MemberNickname(int member_id)
    {
        string member_nickname = "";
        MemberInfo entity = GetMemberByID( member_id);
        if (entity!=null)
        {
            member_nickname = entity.Member_NickName;
        }
        return member_nickname;
    }

    //发送邮件处理
    public void Send_Sysemail()
    {

        string member_id = "";
        member_id = tools.CheckStr(Request["member_id"]);
        if (member_id == "")
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", Public.GetCurrentSite()));


            Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_AllowSysEmail", "=", "1"));

            Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Asc"));

            IList<MemberInfo> entitys = MyBLL.GetMembers(Query, Public.GetUserPrivilege());
            if (entitys != null)
            {
                foreach (MemberInfo entity in entitys)
                {
                    if (member_id == "")
                    {
                        member_id = entity.Member_ID.ToString();
                    }
                    else
                    {
                        member_id = member_id + "," + entity.Member_ID.ToString();
                    }
                }
            }
        }

        string sysmail_title = tools.CheckStr(Request.Form["sysmail_title"]);
        string sysmail_content = tools.CheckHTML(Request.Form["sysmail_content"]);

        //FORM重复提交
        string tmp_str = "";
        tmp_str = tmp_str + "<html>";
        tmp_str = tmp_str + "<head>";
        tmp_str = tmp_str + "<title>管理平台</title>";
        tmp_str = tmp_str + "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">";
        tmp_str = tmp_str + "<link rel=\"stylesheet\" href=\"/public/style.css\" type=\"text/css\">";
        tmp_str = tmp_str + "</head>";
        tmp_str = tmp_str + "<body bgcolor=\"#FFFFFF\" text=\"#000000\" onload=\"document.form1.submit();\">";
        tmp_str = tmp_str + "<table width=\"98%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" align=\"center\">";
        tmp_str = tmp_str + "  <form name=\"form1\" method=\"post\" action=\"sysemail_do.aspx\" >";
        tmp_str = tmp_str + "\t<tr>";
        tmp_str = tmp_str + "\t  <td>";
        tmp_str = tmp_str + "\t <textarea name=\"sysmail_content\" id=\"sysmail_content\" style=\"display:none;\">" + sysmail_content + "</textarea>";
        tmp_str = tmp_str + "\t <input name=\"sysmail_title\" type=\"hidden\" id=\"sysmail_title\" value=\"" + sysmail_title + "\" >";
        tmp_str = tmp_str + "\t <input name=\"member_id\" type=\"hidden\" id=\"member_id\" value=\"" + member_id + "\" >";
        tmp_str = tmp_str + "\t <input name=\"page\" type=\"hidden\" id=\"page\" value=\"1\" >";
        tmp_str = tmp_str + "\t  </td>";
        tmp_str = tmp_str + "\t</tr>";
        tmp_str = tmp_str + "  </form>";
        tmp_str = tmp_str + "</table>";
        tmp_str = tmp_str + "</body>";
        tmp_str = tmp_str + "</html>";
        Response.Write(tmp_str);
        Response.End();

    }

    //发送订阅邮件
    public void Member_Sysemail_Send()
    {

        //取得上一页参数
        string sysmail_title, sysmail_content, member_id, member_arry, member_email;

        sysmail_title = Request.Form["sysmail_title"];
        sysmail_content = Request.Form["sysmail_content"];
        member_id = Request.Form["member_id"];
        member_email = "";
        member_arry = "";

        //处理参数
        int page = 0;

        int ii = 0;
        page = tools.CheckInt(Request["page"]);
        MemberInfo entity;


        //发送Email
        if (member_id.Length > 0)
        {
            foreach (string subid in member_id.Split(','))
            {
                if (tools.CheckInt(subid) > 0)
                {
                    entity = MyBLL.GetMemberByID(tools.CheckInt(subid), Public.GetUserPrivilege());
                    if (entity != null)
                    {
                        if (member_email != "")
                        {
                            if (member_arry == "")
                            {
                                member_arry = subid;
                            }
                            else
                            {
                                member_arry = member_arry + "," + subid;
                            }
                        }
                        if (member_arry == "")
                        {
                            member_email = entity.Member_Email;
                        }

                    }

                }
            }
        }

        if (member_email.Length > 0)
        {
            Sendmail(member_email, sysmail_title, sysmail_title, sysmail_content);
        }
        //FORM重复提交
        string tmp_str = "";
        tmp_str = tmp_str + "<html>";
        tmp_str = tmp_str + "<head>";
        tmp_str = tmp_str + "<title>管理平台</title>";
        tmp_str = tmp_str + "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">";
        tmp_str = tmp_str + "<link rel=\"stylesheet\" href=\"/css/style.css\" type=\"text/css\">";
        tmp_str = tmp_str + "</head>";

        if (member_id != "")
        {
            member_id = member_arry;

            tmp_str = tmp_str + "<body style=\"margin:10px;\" onload=\"document.form1.submit();\">";
            tmp_str = tmp_str + "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" align=\"center\" class=\"content_table\">";
            tmp_str = tmp_str + "  <tr> ";
            tmp_str = tmp_str + "    <td height=\"25\" class=\"content_title\">邮件发送中……</td>";
            tmp_str = tmp_str + "  </tr>";
            tmp_str = tmp_str + "  <tr> ";
            tmp_str = tmp_str + "    <td height=\"30\" class=\"t14red\">";
            tmp_str = tmp_str + "\t<table width=\"95%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" align=\"center\">";
            tmp_str = tmp_str + "\t  <tr> ";
            tmp_str = tmp_str + "        <td width=\"60\" height=\"60\"></td>";
            tmp_str = tmp_str + "        <td width=\"60\"><img src=\"/images/loading.gif\"></td>";
            tmp_str = tmp_str + "\t\t<td align=\"left\" class=\"t14_red\">邮件发送中，请不要关闭窗口……" + member_email + "</td>";
            tmp_str = tmp_str + "\t  </tr>";
            tmp_str = tmp_str + "\t</table>";
            tmp_str = tmp_str + "\t</td>";
            tmp_str = tmp_str + "  </tr>";
            tmp_str = tmp_str + "</table>";
            tmp_str = tmp_str + "<table width=\"98%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" align=\"center\">";
            tmp_str = tmp_str + "  <form name=\"form1\" method=\"post\" action=\"?\">";
            tmp_str = tmp_str + "\t<tr>";
            tmp_str = tmp_str + "\t  <td>";
            tmp_str = tmp_str + "\t <textarea name=\"sysmail_content\" id=\"sysmail_content\" style=\"display:none;\">" + sysmail_content + "</textarea>";
            tmp_str = tmp_str + "\t <input name=\"sysmail_title\" type=\"hidden\" id=\"sysmail_title\" value=\"" + sysmail_title + "\" >";
            tmp_str = tmp_str + "\t <input name=\"member_id\" type=\"hidden\" id=\"member_id\" value=\"" + member_id + "\" >";
            tmp_str = tmp_str + "\t <input name=\"page\" type=\"hidden\" id=\"page\" value=\"1\" >";
            tmp_str = tmp_str + "\t  </td>";
            tmp_str = tmp_str + "\t</tr>";
            tmp_str = tmp_str + "  </form>";
            tmp_str = tmp_str + "</table>";
        }
        else
        {
            tmp_str = tmp_str + "<body style=\"margin:10px;\">";
            tmp_str = tmp_str + "<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" align=\"center\" class=\"content_table\">";
            tmp_str = tmp_str + "  <tr> ";
            tmp_str = tmp_str + "    <td height=\"25\" class=\"content_title\">管理平台</td>";
            tmp_str = tmp_str + "  </tr>";
            tmp_str = tmp_str + "  <tr> ";
            tmp_str = tmp_str + "    <td height=\"30\" class=\"t14red\">";
            tmp_str = tmp_str + "\t<table width=\"95%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" align=\"center\">";
            tmp_str = tmp_str + "\t  <tr> ";
            tmp_str = tmp_str + "        <td width=\"60\" height=\"60\"></td>";
            tmp_str = tmp_str + "        <td width=\"60\"><img src=\"/images/icon_alert_b.gif\" width=\"50\" height=\"50\"></td>";
            tmp_str = tmp_str + "\t\t<td align=\"left\" class=\"t14_red\">邮件发送成功！</td>";
            tmp_str = tmp_str + "\t  </tr>";
            tmp_str = tmp_str + "\t</table>";
            tmp_str = tmp_str + "\t</td>";
            tmp_str = tmp_str + "  </tr>";
            tmp_str = tmp_str + "</table>";

        }
        tmp_str = tmp_str + "</body>";
        tmp_str = tmp_str + "</html>";
        Response.Write(tmp_str);

    }

    //会员选择
    public string SelectMember()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        if (keyword != "输入昵称、邮箱、姓名、电话、手机进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "";
        }

        IList<MemberInfo> entityList = (IList<MemberInfo>)Session["EmailMemberInfo"];
        string memberSelected = "0";
        if (entityList != null)
        {
            foreach (MemberInfo mminfo in entityList)
            {
                memberSelected += "," + mminfo.Member_ID.ToString();
            }
        }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_AllowSysEmail", "=", "1"));
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "MemberInfo.Member_NickName", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberInfo.Member_Email", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberProfileInfo.Member_Name", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberProfileInfo.Member_Phone_Number", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "MemberProfileInfo.Member_Mobile", "like", keyword));
        }
        int member_grade = tools.CheckInt(Request["member_grade"]);
        if (member_grade > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Grade", "=", member_grade.ToString()));
        }

        if (memberSelected.Length > 0)
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_ID", "not in", memberSelected));

        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "DESC"));

        IList<MemberInfo> entitys = MyBLL.GetMembers(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("<form method=\"post\"  id=\"frmadd\" name=\"frmadd\">");
            jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"list_table_bg\">");
            jsonBuilder.Append("    <tr class=\"list_head_bg\">");
            jsonBuilder.Append("        <td width=\"30\"></td>");
            jsonBuilder.Append("        <td width=\"80\">昵称</td>");
            jsonBuilder.Append("        <td>注册邮箱</td>");
            jsonBuilder.Append("        <td>姓名</td>");
            jsonBuilder.Append("        <td>会员等级</td>");
            jsonBuilder.Append("        <td>注册时间</td>");
            jsonBuilder.Append("    </tr>");
            MemberGradeInfo membergrade;
            foreach (MemberInfo entity in entitys)
            {
                jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                jsonBuilder.Append("        <td><input type=\"checkbox\" id=\"member_id\" name=\"member_id\" value=\"" + entity.Member_ID + "\" /></td>");
                jsonBuilder.Append("        <td>" + Public.JsonStr(entity.Member_NickName) + "</td>");
                jsonBuilder.Append("        <td>" + Public.JsonStr(entity.Member_Email) + "</td>");
                if (entity.MemberProfileInfo != null)
                {
                    jsonBuilder.Append("        <td>" + Public.JsonStr(entity.MemberProfileInfo.Member_Name) + "</td>"); 
                }
                else
                {
                    jsonBuilder.Append("        <td>--</td>");
                }
                membergrade = MyGrade.GetMemberGradeByID(entity.Member_Grade);
                if (membergrade != null)
                {
                    jsonBuilder.Append("        <td>" + Public.JsonStr(membergrade.Member_Grade_Name) + "</td>"); 
                }
                else
                {
                    jsonBuilder.Append("        <td>--</td>");
                }
                jsonBuilder.Append("        <td>" + entity.Member_Addtime + "</td>");
                jsonBuilder.Append("    </tr>");
            }

            jsonBuilder.Append("    <tr class=\"list_td_bg\">");
            jsonBuilder.Append("        <td><input type=\"checkbox\" id=\"checkbox\" name=\"chkall\" onclick=\"javascript:CheckAll(this.form)\" /></td>");
            jsonBuilder.Append("        <td colspan=\"6\" align=\"left\"><input type=\"button\" name=\"btn_ok\" value=\"确定\" class=\"bt_orange\" onclick=\"javascript:member_add('member_id');\" /></td>");
            jsonBuilder.Append("    </tr>");
            jsonBuilder.Append("</table>");
            jsonBuilder.Append("</form>");

            entitys = null;
            return jsonBuilder.ToString();
        }
        else { return null; }
    }

    //展示选择会员
    public string ShowMember()
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"1\" class=\"list_table_bg\">");
        jsonBuilder.Append("    <tr class=\"list_head_bg\">");
        jsonBuilder.Append("        <td width=\"60\"><input type=\"button\" value=\"添加\" onclick=\"SelectMember()\" class=\"bt_orange\"></td>");
        jsonBuilder.Append("        <td>ID</td>");
        jsonBuilder.Append("        <td>昵称</td>");
        jsonBuilder.Append("        <td>注册邮箱</td>");

        jsonBuilder.Append("    </tr>");

        IList<MemberInfo> entityList = (IList<MemberInfo>)Session["EmailMemberInfo"];

        MemberInfo memberEntity = null;
        if (entityList != null)
        {
            foreach (MemberInfo entity in entityList)
            {
                memberEntity = MyBLL.GetMemberByID(entity.Member_ID, Public.GetUserPrivilege());
                if (memberEntity != null)
                {
                    jsonBuilder.Append("    <tr class=\"list_td_bg\">");
                    jsonBuilder.Append("        <td><input type=\"hidden\" name=\"member_id\" value=\"" + entity.Member_ID + "\"><a href=\"javascript:member_del(" + entity.Member_ID + ");\"><img src=\"/images/btn_move.gif\" border=\"0\" alt=\"删除\"></a></td>");

                    jsonBuilder.Append("        <td align=\"left\">" + memberEntity.Member_ID + "</td>");
                    jsonBuilder.Append("        <td align=\"left\">" + Public.JsonStr(memberEntity.Member_NickName) + "</td>");
                    jsonBuilder.Append("        <td align=\"center\">" + Public.JsonStr(memberEntity.Member_Email) + "</td>");
                    jsonBuilder.Append("    </tr>");
                }
            }
        }
        jsonBuilder.Append("</table>");
        entityList = null;

        return jsonBuilder.ToString();
    }

    //会员等级选择
    public string GetMemberGradeHTML(int GradeId, string selectname)
    {
        string select_str = "";
        select_str += "<select name=\"" + selectname + "\">";
        select_str += "<option value=\"-1\">不限</option>";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberGradeInfo.Member_Grade_Site", "=", Public.GetCurrentSite()));
        IList<MemberGradeInfo> entitys = MyMGBLL.GetMemberGrades(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (MemberGradeInfo entity in entitys)
            {
                if (entity.Member_Grade_ID == GradeId)
                {
                    select_str += "<option value=\"" + entity.Member_Grade_ID + "\" selected=\"selected\">" + entity.Member_Grade_Name + "</option>";
                }
                else
                {
                    select_str += "<option value=\"" + entity.Member_Grade_ID + "\">" + entity.Member_Grade_Name + "</option>";
                }
            }
        }
        select_str += "</select>";
        return select_str;
    }

    //会员来源选择
    public string GetMemberSourceHTML(string Sources_Code, string selectname)
    {
        string select_str = "";
        select_str += "<select name=\"" + selectname + "\">";
        select_str += "<option value=\"\">不限</option>";
        if ("no-source" == Sources_Code)
        {
            select_str += "<option value=\"no-source\" selected>无来源</option>";
        }
        else
        {
            select_str += "<option value=\"no-source\">无来源</option>";
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SourcesInfo.Sources_Site", "=", Public.GetCurrentSite()));
        IList<SourcesInfo> entitys = MySource.GetSourcess(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (SourcesInfo entity in entitys)
            {
                if (entity.Sources_Code == Sources_Code)
                {
                    select_str += "<option value=\"" + entity.Sources_Code + "\" selected=\"selected\">" + entity.Sources_Name + "</option>";
                }
                else
                {
                    select_str += "<option value=\"" + entity.Sources_Code + "\">" + entity.Sources_Name + "</option>";
                }
            }
        }
        select_str += "</select>";
        return select_str;
    }


    #region  采购商资质

    public virtual void AddMemberCert()
    {
        int Member_Cert_ID = tools.CheckInt(Request.Form["Member_Cert_ID"]);
        int Member_Cert_Type = tools.CheckInt(Request.Form["Member_Cert_Type"]);
        string Member_Cert_Name = tools.CheckStr(Request.Form["Member_Cert_Name"]);
        string Member_Cert_Note = tools.CheckStr(Request.Form["Member_Cert_Note"]);
        int Member_Cert_Sort = tools.CheckInt(Request.Form["Member_Cert_Sort"]);
        DateTime Member_Cert_Addtime = DateTime.Now;
        string Member_Cert_Site = Public.GetCurrentSite();

        if (Member_Cert_Name == "")
        {
            Public.Msg("error","提示信息","请输入资质名称",false,"{back}");
        }

        MemberCertInfo entity = new MemberCertInfo();
        entity.Member_Cert_ID = Member_Cert_ID;
        entity.Member_Cert_Type = Member_Cert_Type;
        entity.Member_Cert_Name = Member_Cert_Name;
        entity.Member_Cert_Note = Member_Cert_Note;
        entity.Member_Cert_Sort = Member_Cert_Sort;
        entity.Member_Cert_Addtime = Member_Cert_Addtime;
        entity.Member_Cert_Site = Member_Cert_Site;

        if (MyCert.AddMemberCert(entity))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Member_Cert_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditMemberCert()
    {
        int Member_Cert_ID = tools.CheckInt(Request.Form["Member_Cert_ID"]);
        int Member_Cert_Type = tools.CheckInt(Request.Form["Member_Cert_Type"]);
        string Member_Cert_Name = tools.CheckStr(Request.Form["Member_Cert_Name"]);
        string Member_Cert_Note = tools.CheckStr(Request.Form["Member_Cert_Note"]);
        int Member_Cert_Sort = tools.CheckInt(Request.Form["Member_Cert_Sort"]);
        DateTime Member_Cert_Addtime = tools.NullDate(Request.Form["Member_Cert_Addtime"]);
        string Member_Cert_Site = tools.CheckStr(Request.Form["Member_Cert_Site"]);

        if (Member_Cert_Name == "")
        {
            Public.Msg("error", "提示信息", "请输入资质名称", false, "{back}");
        }

        MemberCertInfo entity = GetMemberCertByID(Member_Cert_ID);
        if (entity != null)
        {
            entity.Member_Cert_Type = Member_Cert_Type;
            entity.Member_Cert_Name = Member_Cert_Name;
            entity.Member_Cert_Note = Member_Cert_Note;
            entity.Member_Cert_Sort = Member_Cert_Sort;

            if (MyCert.EditMemberCert(entity))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "Member_Cert_list.aspx");
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void DelMemberCert()
    {
        int Member_Cert_ID = tools.CheckInt(Request.QueryString["Member_Cert_ID"]);
        if (MyCert.DelMemberCert(Member_Cert_ID) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Member_Cert_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual MemberCertInfo GetMemberCertByID(int ID)
    {
        return MyCert.GetMemberCertByID(ID);
    }

    public virtual string Get_Member_Type(int Supplier_Type) 
    {
        string Type_Name = "";
        switch (Supplier_Type)
        {
            case 0:
                Type_Name = "普通供应商";
                break;
            case 1:
                Type_Name = "下岗创业";
                break;
            case 2:
                Type_Name = "大学生创业";
                break;
            case 3:
                Type_Name = "自主创业";
                break;
        }
        return Type_Name;
    }

    /// <summary>
    /// 添加采购商资质信息
    /// </summary>
    public void AddMemberRelateCert()
    {
        int member_id = tools.CheckInt(Request["member_id"]);
        int member_certtype = tools.CheckInt(Request["member_certtype"]);
        IList<MemberCertInfo> certinfos = null;
        MemberRelateCertInfo ratecate = null;
        MemberInfo entity = GetMemberByID(member_id);
        if (entity != null)
        {
            certinfos = GetMemberCertByType(member_certtype);
            if (certinfos != null)
            {
                foreach (MemberCertInfo certinfo in certinfos)
                {
                    if (tools.CheckStr(Request["Member_Cert" + certinfo.Member_Cert_ID + "_tmp"]).Length == 0)
                    {
                        Response.Write("请将资质文件上传完整");
                        Response.End();
                    }
                }
                //删除资质文件
                MyBLL.DelMemberRelateCertByMemberID(entity.Member_ID);
                foreach (MemberCertInfo certinfo in certinfos)
                {
                    ratecate = new MemberRelateCertInfo();
                    ratecate.MemberID = entity.Member_ID;
                    ratecate.CertID = certinfo.Member_Cert_ID;
                    if (tools.CheckStr(Request["member_cert" + certinfo.Member_Cert_ID]).Length == 0)
                    {
                        ratecate.Img = tools.CheckStr(Request["member_cert" + certinfo.Member_Cert_ID + "_tmp"]);
                    }
                    else
                    {
                        ratecate.Img = tools.CheckStr(Request["member_cert" + certinfo.Member_Cert_ID]);
                    }
                    MyBLL.AddMemberRelateCert(ratecate);
                    ratecate = null;
                }
            }

            entity.Member_Type = member_certtype;
            entity.Member_Cert_Status = 1;
            if (MyBLL.EditMember(entity, Public.GetUserPrivilege()))
            {
                Response.Write("success");
                Response.End();
            }
            else
            {
                Response.Write("信息保存失败，请稍后再试！");
                Response.End();
            }
        }
        else
        {
            Response.Write("信息保存失败，请稍后再试！");
            Response.End();
        }
    }

    public string GetMemberCerts()
    {
        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberCertInfo.Member_Cert_Site", "=", Public.GetCurrentSite()));
        if (keyword.Length > 0)
        {

            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberCertInfo.Member_Cert_Name", "like", keyword));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyCert.GetPageInfo(Query);
        IList<MemberCertInfo> entitys = MyCert.GetMemberCerts(Query);
        if (entitys != null)
        {

            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (MemberCertInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.Member_Cert_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Member_Cert_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(Get_Member_Type(entity.Member_Cert_Type)));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Member_Cert_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Member_Cert_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                //if (Public.CheckPrivilege("6af8fb74-25d4-4884-b8b1-62e7f80068cc"))
                //{
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\" align=\\\"absmiddle\\\"> <a href=\\\"member_Cert_Edit.aspx?Member_Cert_ID=" + entity.Member_Cert_ID + "\\\" title=\\\"修改\\\">修改</a>");
                //}
                //if (Public.CheckPrivilege("d461e5c4-b803-47c2-8e25-35346e4132c5"))
                //{
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('member_Cert_Do.aspx?action=cert_del&Member_Cert_ID=" + entity.Member_Cert_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                //}
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    public string GetMemberCertList()
    {
        QueryInfo Query = new QueryInfo();
        string keyword = tools.CheckStr(Request["keyword"]);
        int Audit = tools.CheckInt(Request["Audit"]);
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", Public.GetCurrentSite()));

        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "MemberInfo.Member_Email", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "MemberInfo.Member_ID", "in", "select Member_Profile_MemberID from Member_Profile where Member_Company like '%" + keyword + "%'"));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Cert_Status", "=", (Audit + 1).ToString()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());
        IList<MemberInfo> entitys = MyBLL.GetMembers(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (MemberInfo entity in entitys)
            {

                jsonBuilder.Append("{\"MemberInfo.Member_ID\":" + entity.Member_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Member_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Member_Email));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.MemberProfileInfo.Member_Company));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.MemberProfileInfo.Member_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(addr.DisplayAddress(entity.MemberProfileInfo.Member_State, entity.MemberProfileInfo.Member_City, entity.MemberProfileInfo.Member_County)));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("\"" + Get_Cert_Status(entity.Member_Cert_Status));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("3ffb48b4-fa1a-45f9-a95d-177ff30ed00e"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_view.gif\\\" alt=\\\"查看\\\" align=\\\"absmiddle\\\"> <a href=\\\"member_Cert_View.aspx?member_id=" + entity.Member_ID + "\\\" title=\\\"查看\\\">查看</a>");
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");

            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }

    public string Get_Cert_Status(int Member_Cert_Status)
    {
        string status = "未上传";

        if (Member_Cert_Status == 0)
        {
            status = "未上传";
        }
        if (Member_Cert_Status == 1)
        {
            status = "待审核";
        }
        if (Member_Cert_Status == 2)
        {
            status = "审核通过";
        }
        if (Member_Cert_Status == 3)
        {
            status = "审核未通过";
        }
        return status;
    }

    public string Get_Member_Cert(int membercert_id, IList<MemberRelateCertInfo> relateinfo)
    {
        string Cert_Img = "";
        if (relateinfo != null)
        {
            foreach (MemberRelateCertInfo entity in relateinfo)
            {
                if (membercert_id == entity.CertID)
                {
                    Cert_Img = entity.Img;
                    break;
                }
            }
        }
        return Cert_Img;
    }

    public IList<MemberCertInfo> GetMemberCertByType(int member_type)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberCertInfo.Member_Cert_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberCertInfo.Member_Cert_Type", "=", member_type.ToString()));
        Query.OrderInfos.Add(new OrderInfo("MemberCertInfo.Member_Cert_Sort", "Desc"));
        return MyCert.GetMemberCerts(Query);
    }

    #endregion


    #region "邮件处理"


    //邮件发送处理过程
    public int Sendmail(string mailto, string mailsubject, string mailbodytitle, string mailbody)
    {

        //-------------------------------------定义邮件设置---------------------------------
        int mformat = 0;

        //-------------------------------------定义邮件模版---------------------------------
        string MailBody_Temp = null;
        MailBody_Temp = "";
        MailBody_Temp = MailBody_Temp + "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=GB2312\" /></head>";
        MailBody_Temp = MailBody_Temp + "<body>";
        MailBody_Temp = MailBody_Temp + "<DIV class=mailHeader><SPAN class=MailBody_title>{MailBody_title}</SPAN></DIV>";
        MailBody_Temp = MailBody_Temp + "<DIV class=mailContent>";
        MailBody_Temp = MailBody_Temp + "{MailBody_content}";
        MailBody_Temp = MailBody_Temp + "<p><br><B>{sys_config_site_name}</B><br>欲了解更多信息，请访问<a href='{sys_config_site_url}'>{sys_config_site_url}</a> 或致电{sys_config_site_tel}</P></DIV>";
        MailBody_Temp = MailBody_Temp + "<DIV class=mailFooter><P class=comments>&copy; {sys_config_site_name}</P></DIV>";
        MailBody_Temp = MailBody_Temp + "<style type=text/css>";
        MailBody_Temp = MailBody_Temp + "P {FONT-SIZE: 14px; MARGIN: 10px 0px 5px; LINE-HEIGHT: 130%; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif}";
        MailBody_Temp = MailBody_Temp + "td {FONT-SIZE: 12px; LINE-HEIGHT: 150%; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif}";
        MailBody_Temp = MailBody_Temp + "BODY {BORDER-RIGHT: 0px; PADDING-RIGHT: 0px; BORDER-TOP: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; BORDER-LEFT: 0px; PADDING-TOP: 0px; BORDER-BOTTOM: 0px; FONT-FAMILY: Arial, Verdana, Helvetica, sans-serif }";
        MailBody_Temp = MailBody_Temp + "UL {MARGIN-TOP: 0px; FONT-SIZE: 14px; LINE-HEIGHT: 130%; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif}";
        MailBody_Temp = MailBody_Temp + ".comments {FONT-SIZE: 12px; MARGIN: 0px; COLOR: gray; LINE-HEIGHT: 130%}";
        MailBody_Temp = MailBody_Temp + ".mailHeader {PADDING-RIGHT: 23px; PADDING-LEFT: 23px; PADDING-BOTTOM: 10px; COLOR: #003366; PADDING-TOP: 10px; BORDER-BOTTOM: #7a8995 1px solid; BACKGROUND-COLOR: #ebebeb}";
        MailBody_Temp = MailBody_Temp + ".mailContent {PADDING-RIGHT: 23px; PADDING-LEFT: 23px; PADDING-BOTTOM: 23px; PADDING-TOP: 11px}";
        MailBody_Temp = MailBody_Temp + ".mailFooter {PADDING-RIGHT: 23px; BORDER-TOP: #bbbbbb 1px solid; PADDING-LEFT: 23px; PADDING-BOTTOM: 11px; PADDING-TOP: 11px}";
        MailBody_Temp = MailBody_Temp + ".MailBody_title {  font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 20px; font-weight: bold; color: #009900}";
        MailBody_Temp = MailBody_Temp + "A:visited { COLOR: #105bac} A:hover { COLOR: orange} .img_border { border: 1px solid #E5E5E5}";
        MailBody_Temp = MailBody_Temp + ".highLight { BACKGROUND-COLOR: #FFFFCC; PADDING: 15px; FONT-FAMILY: Arial, Verdana, Helvetica, sans-serif}</style>";
        MailBody_Temp = MailBody_Temp + "</body><html>";

        //------------------------------------开始发送过程------------------------------------
        string body = "";
        switch (mformat)
        {
            case 0:
                //HTML格式
                body = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=GB2312\" />" + MailBody_Temp;
                body = body.Replace("{MailBody_title}", mailbodytitle);
                body = body.Replace("{MailBody_content}", mailbody);
                break;
            case 1:
                //纯文本格式
                body = mailbody;
                break;
        }

        body = replace_sys_config(body);

        // ERROR: Not supported in C#: OnErrorStatement
        try
        {
            mail.From = Application["Mail_From"].ToString();
            mail.Replyto = Application["Mail_Replyto"].ToString();
            mail.FromName = Application["Mail_FromName"].ToString();
            mail.Server = Application["Mail_Server"].ToString();
            //邮件格式 0=支持HTML,1=纯文本
            mail.ServerUsername = Application["Mail_ServerUserName"].ToString(); ;
            mail.ServerPassword = Application["Mail_ServerPassWord"].ToString();
            mail.ServerPort = tools.CheckInt(Application["Mail_ServerPort"].ToString());
            if (tools.CheckInt(Application["Mail_EnableSsl"].ToString()) == 0)
            {
                mail.EnableSsl = false;
            }
            else
            {
                mail.EnableSsl = true;
            }
            mail.Encode = Application["Mail_Encode"].ToString();

            if (mail.SendEmail(mailto, mailsubject, body))
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }
        catch (Exception ex)
        {
            return 0;
        }



    }

    //替换系统变量
    public string replace_sys_config(string replacestr)
    {
        string functionReturnValue;
        functionReturnValue = replacestr;
        functionReturnValue = functionReturnValue.Replace("{sys_config_site_name}", Application["site_name"].ToString());
        functionReturnValue = functionReturnValue.Replace("{sys_config_site_url}", Application["site_url"].ToString());
        functionReturnValue = functionReturnValue.Replace("{sys_config_site_tel}", Application["site_tel"].ToString());
        return functionReturnValue;
    }

    #endregion

}
