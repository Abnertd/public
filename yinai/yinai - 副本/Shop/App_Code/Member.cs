using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.BLL.Product;
using Glaer.Trade.B2C.BLL.ORD;
using Glaer.Trade.B2C.BLL.SAL;

/// <summary>
///Member 的摘要说明
/// </summary>
public class Member
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    ITools tools;
    ISupplierShop MyShop;
    IMember MyMember;
    IMemberGrade Mygrade;
    IMemberLog MyMemLog;
    Public_Class pub = new Public_Class();
    IEncrypt encrypt;
    IMemberConsumption MyConsumption;
    IMemberFavorites MyFavor;
    IProduct MyProduct;
    IPackage MyPackage;
    IProductReview MyReview;
    IFeedBack MyFeedback;
    IMemberAddress MyAddr;
    IOrdersGoodsTmp MyCart;
    IPromotionFavorCoupon MyCoupon;
    Addr addr = new Addr();
    Orders Myorder = new Orders();
    private PageURL pageurl;
    //U_IEmailNotifyRequest MyEmail;
    IMemberAccountLog MyAccountLog;

    public Member()
    {
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session; 
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyMember = MemberFactory.CreateMember();
        Mygrade = MemberGradeFactory.CreateMemberGrade();
        MyMemLog=MemberLogFactory.CreateMemberLog();
        encrypt = EncryptFactory.CreateEncrypt();
        MyConsumption = MemberConsumptionFactory.CreateMemberConsumption();
        MyFavor = MemberFavoritesFactory.CreateMemberFavorites();
        MyProduct = ProductFactory.CreateProduct();
        MyPackage = PackageFactory.CreatePackage();
        MyReview = ProductReviewFactory.CreateProductReview();
        MyFeedback = FeedBackFactory.CreateFeedBack();
        MyAddr=MemberAddressFactory.CreateMemberAddress();
        MyCart = OrdersGoodsTmpFactory.CreateOrdersGoodsTmp();
        MyCoupon = PromotionFavorCouponFactory.CreatePromotionFavorCoupon();
       // MyEmail = U_EmailNotifyRequestFactory.CreateU_EmailNotifyRequest();
        MyAccountLog = MemberAccountLogFactory.CreateMemberAccountLog();
        MyShop = SupplierShopFactory.CreateSupplierShop();

        pageurl = new PageURL(int.Parse(Application["Static_IsEnable"].ToString()));
    }

    #region"辅助函数"

    //检查昵称是否使用
    public bool Check_Member_Nickname(string nick_name)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_NickName", "=", nick_name));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Trash", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Desc"));
        PageInfo page = MyMember.GetPageInfo(Query, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        if (page != null)
        {
            if (page.RecordCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    //检查注册邮箱是否使用
    public bool Check_Member_Email(string Member_Email)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Email", "=", Member_Email));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Trash", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Desc"));
        PageInfo page = MyMember.GetPageInfo(Query, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        if (page != null)
        {
            if (page.RecordCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    //检查密码
    public bool CheckSsn(string strSsn)
    {
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9]*$");
        return regex.IsMatch(strSsn);
    }

    //根据用户邮箱获取信息
    public MemberInfo GetMemberInfoByEmail(string Member_Email)
    {
        return MyMember.GetMemberByEmail(Member_Email, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
    }

    //获取默认会员等级
    public MemberGradeInfo GetMemberDefaultGrade()
    {
        return Mygrade.GetMemberDefaultGrade();
    }

    //会员日志
    public void Member_Log(int member_id, string description)
    {
        MemberLogInfo memberlog = new MemberLogInfo();
        memberlog.Log_ID = 0;
        memberlog.Log_Member_ID = member_id;
        memberlog.Log_Member_Action = description;
        memberlog.Log_Addtime = DateTime.Now;

        MyMemLog.AddMemberLog(memberlog);

    }

    //根据编号获取会员信息
    public MemberInfo GetMemberByID()
    {
        int member_id=tools.CheckInt(Session["member_id"].ToString());
        if (member_id > 0)
        {
            return MyMember.GetMemberByID(member_id, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        }
        else
        {
            return null;
        }
    }

    //根据编号获取会员等级信息
    public MemberGradeInfo GetMemberGradeByMemberID()
    {
        int member_id = tools.CheckInt(Session["member_id"].ToString());            
        MemberInfo MEntity = MyMember.GetMemberByID(member_id, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        if (MEntity != null)
        {
            return Mygrade.GetMemberGradeByID(MEntity.Member_Grade, pub.CreateUserPrivilege("1c955ea6-881f-48d8-ba8d-c5aa7ce9cfea"));
        }
        else
        {
            return null;
        }
    }

    //根据会员等级获取下一等级 文字提示
    public string GetLastMemberGrade()
    {
        MemberInfo memberinfo = GetMemberByID();
        string str = "";
        int requiredcoid=0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.OrderInfos.Add(new OrderInfo("MemberGradeInfo.Member_Grade_RequiredCoin", "ASC"));
        IList<MemberGradeInfo> entitys = Mygrade.GetMemberGrades(Query, pub.CreateUserPrivilege("1c955ea6-881f-48d8-ba8d-c5aa7ce9cfea"));
        if (entitys != null)
        {
            bool bz = false;
            foreach (MemberGradeInfo entity in entitys)
            {
                if (bz)
                {
                    str = "（还差" + (entity.Member_Grade_RequiredCoin - memberinfo.Member_CoinCount) + "积分升级为" + entity.Member_Grade_Name + "）";
                    break;
                }
                if (entity.Member_Grade_ID == memberinfo.Member_Grade)
                {
                    bz = true;
                }
            }
        }
        return str;
    }

    public bool Check_Zip(string zip)
    {
        if (zip.Length != 6)
        {
            return false;
        }
        else
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[0-9]{6}");
            return regex.IsMatch(zip);
        }
    }

    //公司部门
    public string Get_Member_Department(int id)
    {
        switch (id)
        {
            case 1:
                return "市场部";
            case 2:
                return "采购部";
            case 3:
                return "其他";
            default:
                return "--";
        }
    }

    //购买类型
    public string Get_Buy_Type(int id)
    {
        switch (id)
        {
            case 1:
                return "自用采购";
            case 2:
                return "合作采购";
            case 3:
                return "其他";
            default:
                return "--";
        }
    }

    //企业人数
    public string Get_Men_Amount(int id)
    {
        switch (id)
        {
            case 1:
                return "1--49";
            case 2:
                return "50-99";
            case 3:
                return "100-499";
            case 4:
                return "500-999";
            case 5:
                return "1000以上";
            default:
                return "--";
        }
    }

    //公司行业
    public string Get_Company_Vocation(int id)
    {
        switch (id)
        {
            case 1:
                return "医药/生物工程";
            case 2:
                return "医疗/护理/美容/保健";
            case 3:
                return "医疗设备/器械";
            case 4:
                return "快速消费品（食品/饮料/烟酒/化妆品）";
            case 5:
                return "耐用消费品（服装服饰/纺织/皮革/家具/家电）";
            case 6:
                return "其他";
            default:
                return "--";
        }
    }

    //公司性质
    public string Get_Company_Kind(int id)
    {
        switch (id)
        {
            case 1:
                return "政府机关/事业单位";
            case 2:
                return "国营";
            case 3:
                return "私营";
            case 4:
                return "中外合资";
            case 5:
                return "外资";
            case 6:
                return "其他";
            default:
                return "--";
        }
    }

    //导航栏所用函数
    public string GetBHTD()
    {
        StringBuilder html = new StringBuilder();
        html.Append("");
        //html.Append("<div>");        
        html.Append("<table style=\"position:absolute;z-index:99999;overflow:hidden;display:none;width:77px;!margin-top:-8px; !margin-left:-9px;padding-left:0px; border:1px solid #C6C6C6; background:#ffffff;\" id=\"div_bhtd\" border=\"0\" width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"z-index:9999; font-size:11px;margin-left:0px;padding-left:0px;margin-top:0px; padding-top:0px;\">");
        html.Append("<tr><td height=\"20\" style=\"border-bottom:1px solid #F7F7F7;_height:30px;\"><a class=\"a_11_60\" style=\"font-size:12px;\" href=\"/member/index.aspx\">我的新兴 <img src=\"/images/arrow_up.jpg\" align=\"absmiddle\"/></a></td></tr>");
        html.Append("<tr><td height=\"15\" style=\"height:25px;line-height:25px;border-bottom:1px solid #F7F7F7;\"><a class=\"a_11_60\" style=\"font-size:12px;\" href=\"/member/member_favorites.aspx\">我的收藏</a></td></tr>");
        html.Append("<tr><td style=\"height:25px;line-height:25px;\"><a class=\"a_11_60\" style=\"font-size:12px;\" href=\"/member/account_coin_list.aspx\">我的积分</a></td></tr>");
        html.Append("</table>");
        return html.ToString();
    }
    public void GetWebDH()
    {
        //Response.Write("<div id=\"div_web\" style=\"position:absolute; display:none; margin-top:22px; margin-left:0px; text-align:center; width:70px; border:1px solid #C6C6C6; background:#FFFFFF;\">");
        //Response.Write("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\" font-size:11px;\">");
        //Response.Write("<tr><td height=\"20\" style=\"border-bottom:1px solid #F7F7F7;\"><a class=\"a_11_60\" href=\"#\">官方微博</a></td></tr>");
        //Response.Write("<tr><td height=\"20\" style=\"border-bottom:1px solid #F7F7F7;\"><a class=\"a_11_60\" href=\"/about/poplink.aspx?cate_id=2\">合作专区</a></td></tr>");
        //Response.Write("</table></div>");
    }
    public void GetHelp()
    {
        //Response.Write("<div id=\"div_help\" style=\"position:absolute; display:none; margin-top:22px; margin-left:0px; text-align:center; width:70px; border:1px solid #C6C6C6; background:#FFFFFF;\">");
        //Response.Write("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\" font-size:11px;\">");
        //Response.Write("<tr><td width=\"100%\" style=\"border-bottom:1px solid #F7F7F7;\" height=\"20\" id=\"zc\">");
        //GetZC();
        //Response.Write("<a class=\"a_11_60\">政策中心</a></td></tr>");
        //Response.Write("<tr><td height=\"20\"><a class=\"a_11_60\" href=\"/member/feedback.aspx\">投诉建议</a></td></tr>");
        //Response.Write("</table></div>");
    }
    public void GetZC()
    {
        //Response.Write("<div id=\"div_zc\" style=\"position:absolute; display:none; margin-top:0px; margin-left:48px; text-align:center; width:95px; border:1px solid #C6C6C6; background:#FFFFFF;\">");
        //Response.Write("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\" font-size:11px;\">");
        //Response.Write("<tr><td height=\"20\" style=\"border-bottom:1px solid #F7F7F7;\"><a class=\"a_11_60\" href=\"/help/index.aspx?help_id=4\">退换货</a></td></tr>");
        //Response.Write("<tr><td height=\"20\" style=\"border-bottom:1px solid #F7F7F7;\"><a class=\"a_11_60\" href=\"/help/index.aspx?help_id=5\">运费</a></td></tr>");
        //Response.Write("<tr><td height=\"20\"><a class=\"a_11_60\" href=\"/help/index.aspx?help_id=6\">优惠券使用规则</a></td></tr>");
        //Response.Write("</table></div>");
    }
    #endregion

    #region "AJAx函数"

    public void Check_Nickname()
    {
        string member_nickname = tools.CheckStr(Request["val"]).Trim();
        if (member_nickname == "")
        {
            Response.Write("<font color=\"#cc0000\">请输入用户名！</font>");
            return;
        }
        else
        {
            if (member_nickname.Length > 15)
            {
                Response.Write("<font color=\"#cc0000\">用户名不要超过15个字符！</font>");
                return;
            }
            if (Check_Member_Nickname(member_nickname) == false)
            {
                Response.Write("<font color=\"#00a226\">用户名输入正确！</font>");
                return;
            }
            else
            {
                Response.Write("<font color=\"#cc0000\">该用户名已被使用，请使用其他用户名注册，如果您是&quot;" + member_nickname + "&quot;,请<a href=\"/member/login.aspx\">登录</a></font>");
                return;
            }
        }
    }

    public void Check_MemberEmail()
    {
        string member_email = tools.CheckStr(Request["val"]);
        if (member_email == "")
        {
            Response.Write("<font color=\"#cc0000\">请输入E-mail！</font>");
            return;
        }
        else
        {
            if (tools.CheckEmail(member_email))
            {

                if (Check_Member_Email(member_email))
                {
                    Response.Write("<font color=\"#cc0000\">该邮件地址已被使用。请使用另外一个邮件地址进行注册</font>");
                    return;
                }
                else
                {
                    Response.Write("<font color=\"#00a226\">E-mail输入正确！</font>");
                    return;
                }

            }
            else
            {
                Response.Write("<font color=\"#cc0000\">无效的E-mail！</font>");
                return;
            }
        }
    }

    public void Check_MemberPasswprd()
    {
        string member_password = tools.CheckStr(Request["val"]);
        if (member_password.Length < 6 || member_password.Length > 20)
        {
            Response.Write("<font color=\"#cc0000\">请输入6～20位密码</font>");
            return;
        }
        else
        {
            if (CheckSsn(member_password))
            {
                Response.Write("<font color=\"#00a226\">密码输入正确！</font>");
                return;
            }
            else
            {
                Response.Write("<font color=\"#cc0000\">密码包含特殊字符！</font>");
                return;
            }
        }
    }

    public void Check_MemberrePasswprd()
    {
        string member_repassword = tools.CheckStr(Request["val"]);
        string member_password = tools.CheckStr(Request["val1"]);
        if (member_repassword.Length < 6 || member_repassword.Length > 20)
        {
            Response.Write("<font color=\"#cc0000\">请输入6～20位密码</font>");
            return;
        }
        if (member_repassword != member_password)
        {
            Response.Write("<font color=\"#cc0000\">两次密码不一致</font>");
            return;
        }
        else
        {
            Response.Write("<font color=\"#00a226\">确认密码输入正确！</font>");
            return;
        }
    }

    public void Check_MemberMobile()
    {
        string member_mobile = tools.CheckStr(Request["val"]);
        if (member_mobile == "")
        {
            Response.Write("<font color=\"#cc0000\">请输入手机号码！</font>");
            return;
        }
        else
        {
            if (pub.Checkmobile(member_mobile))
            {
                Response.Write("<font color=\"#00a226\">手机号码输入正确！</font>");
                return;
            }
            else
            {
                Response.Write("<font color=\"#cc0000\">无效的手机号码！</font>");
                return;
            }
        }
    }

    public void Check_MemberPhone()
    {
        string member_mobile = tools.CheckStr(Request["val"]);
        if (member_mobile == "")
        {
            Response.Write("<font color=\"#cc0000\">请输入联系人固定电话！</font>");
            return;
        }
        else
        {
            if (pub.Checkmobile(member_mobile))
            {
                Response.Write("<font color=\"#00a226\"> </font>");
                return;
            }
            else
            {
                Response.Write("<font color=\"#cc0000\">电话格式错误，请重新输入！</font>");
                return;
            }
        }
    }

    public void Check_Verifycode()
    {
        string verifycode = tools.CheckStr(Request["val"]);
        if (verifycode == "")
        {
            Response.Write("<font color=\"#cc0000\">请输入验证码！</font>");
            return;
        }
        else
        {
            if (verifycode == Session["Trade_Verify"].ToString())
            {
                Response.Write("<font color=\"#00a226\">验证码输入正确！</font>");
                return;
            }
            else
            {
                Response.Write("<font color=\"#cc0000\">无效的验证码！</font>");
                return;
            }
        }
    }

    public void Check_Checkprotocal()
    {
        string protocal = tools.CheckStr(Request["val"]);
        if (protocal != "1")
        {
            Response.Write("<font color=\"#cc0000\">请阅读并接受网站使用协议！</font>");
            return;
        }
        else
        {
            Response.Write("<font color=\"#00a226\">您已接受网站使用协议！</font>");
            return;
        }
    }

    public void Check_IsBlank()
    {
        string content = tools.CheckStr(Request["val"]);
        string success = tools.CheckStr(Server.UrlDecode(Request["success"]));
        if (success == "") { success = "信息输入正确！"; }
        string error = tools.CheckStr(Server.UrlDecode(Request["error"]));
        if (error == "") { error = "信息不可为空！"; }
        if (content == "")
        {
            Response.Write("<font color=\"#cc0000\">" + error + "</font>");
            return;
        }
        else
        {
            Response.Write("<font color=\"#00a226\">" + success + "</font>");
            return;
        }
    }

    #endregion

    #region"注册登录"

    //会员注册
    public void Member_Register()
    { 
        string member_nickname = tools.CheckStr(tools.NullStr(Request.Form["member_nickname"]).Trim());
        string member_email = tools.CheckStr(tools.NullStr(Request.Form["member_email"]).Trim());
        string member_password = tools.CheckStr(tools.NullStr(Request.Form["member_password"]).Trim());
        string member_password_confirm = tools.CheckStr(tools.NullStr(Request.Form["member_password_confirm"]).Trim());
        string verifycode = tools.CheckStr(tools.NullStr(Request.Form["verifycode"]));
        int Isagreement = tools.CheckInt(tools.NullStr(Request.Form["checkbox_agreement"]));
        int Member_Type = tools.CheckInt(tools.NullStr(Request["Member_Type"]));
        //string member_mobile = tools.CheckStr(tools.NullStr(Request.Form["member_mobile"]));
        int DefaultGrade = 1;
        string Member_State = "", Member_City = "", Member_County = "", Member_Name = "", Member_StreetAddress = "", Member_Phone_Number = "", member_mobile = "", U_Member_CompanyName = "", U_Member_CompanyUrl = "";
        int U_Member_Department = 0, U_Member_CompanyKind = 0, U_Member_CompanyVocation = 0, U_Member_MenAmount = 0, U_Member_BuyType = 0;
        
        MemberGradeInfo member_grade = GetMemberDefaultGrade();
        if (member_grade != null)
        {
            DefaultGrade = member_grade.Member_Grade_ID;
        }

        if(verifycode != Session["Trade_Verify"].ToString()&&verifycode.Length==0)
        {
            pub.Msg("error", "验证码输入错误", "验证码输入错误", false,  "{back}");
        }

        if (member_nickname.Length > 0)
        {
            if (Check_Member_Nickname(member_nickname))
            {
                pub.Msg("error", "该昵称已被使用", "该昵称已被使用。请使用另外一个昵称进行注册", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "昵称无效", "请输入有效的昵称", false, "{back}");
        }

        if (tools.CheckEmail(member_email) == false)
        {
            pub.Msg("error", "邮件地址无效", "请输入有效的邮件地址", false, "{back}");
        }
        else
        {
            if (Check_Member_Email(member_email))
            {
                pub.Msg("error", "该邮件地址已被使用", "该邮件地址已被使用。请使用另外一个邮件地址进行注册", false, "{back}");
            }
        }

        if (CheckSsn(member_password) == false)
        {
            pub.Msg("error", "密码包含特殊字符", "密码包含特殊字符，只接受A-Z，a-z，0-9，不要输入空格", false, "{back}");
        }
        else
        {
            if (member_password.Length < 6 || member_password.Length > 20)
            {
                pub.Msg("error", "请输入6～20位密码", "请输入6～20位密码", false, "{back}");
            }
        }

        if (member_password_confirm != member_password)
        {
            pub.Msg("error", "两次密码输入不一致", "两次密码输入不一致，请重新输入", false, "{back}");
        }

        if (Member_Type == 1)
        {
            Member_Name = tools.CheckStr(tools.NullStr(Request["Member_Name"]));
            if (Member_Name == "")
            {
                pub.Msg("error", "信息不能为空", "联系人姓名不能为空", false, "{back}");
            }

            U_Member_Department = tools.CheckInt(tools.NullStr(Request["Member_Department"]));
            if (U_Member_Department == 0)
            {
                pub.Msg("error", "信息提示", "请选择联系人所在部门", false, "{back}");
            }

            Member_Phone_Number = tools.CheckStr(tools.NullStr(Request["Member_Phone_Number"]));
            if (Member_Phone_Number == "")
            {
                pub.Msg("error", "信息提示", "请输入联系人的固定电话", false, "{back}");
            }
            else if (pub.Checkmobile(Member_Phone_Number) == false)
            {
                pub.Msg("error", "信息提示", "电话格式错误，请重新输入！", false, "{back}");
            }

            member_mobile = tools.CheckStr(tools.NullStr(Request["member_mobile"]));

            U_Member_CompanyName = tools.CheckStr(tools.NullStr(Request["U_Member_CompanyName"]));
            if (U_Member_CompanyName == "")
            {
                pub.Msg("error", "信息提示", "请输入公司名称！", false, "{back}");
            }

            Member_State = tools.CheckStr(tools.NullStr(Request["Member_State"]));
            Member_City = tools.CheckStr(tools.NullStr(Request["Member_City"]));
            Member_County = tools.CheckStr(tools.NullStr(Request["Member_County"]));
            if (Member_County == "0")
            {
                pub.Msg("error", "信息提示", "请选择公司所在地！", false, "{back}");
            }

            Member_StreetAddress = tools.CheckStr(tools.NullStr(Request["Member_StreetAddress"]));

            U_Member_BuyType = tools.CheckInt(tools.NullStr(Request["Buy_Type"]));
            if (U_Member_BuyType == 0)
            {
                pub.Msg("error", "信息提示", "请选择购买类型/用途！", false, "{back}");
            }

            U_Member_CompanyUrl = tools.CheckStr(tools.NullStr(Request["U_Member_CompanyUrl"]));
            U_Member_MenAmount = tools.CheckInt(tools.NullStr(Request["Men_Amount"]));
            U_Member_CompanyVocation = tools.CheckInt(tools.NullStr(Request["Company_Vocation"]));
            U_Member_CompanyKind = tools.CheckInt(tools.NullStr(Request["Company_Kind"]));
        }

        if (Isagreement != 1)
        {
            pub.Msg("error", "您需要接受用户注册协议", "要完成注册，您需要接受用户注册协议", false, "{back}");
        }

        MemberInfo member = new MemberInfo();
        member.Member_ID = 0;
        member.Member_Email = member_email;
        member.Member_Emailverify = 0;
        member.Member_LoginMobile = "";
        member.Member_LoginMobileverify = 1;
        member.Member_NickName = member_nickname;
        member.Member_Password = encrypt.MD5(member_password);
        member.Member_VerifyCode = pub.Createvkey();
        member.Member_LoginCount = 1;
        member.Member_LastLogin_IP = Request.ServerVariables["remote_addr"];
        member.Member_LastLogin_Time = DateTime.Now;
        member.Member_CoinCount = 0;
        member.Member_CoinRemain = 0;
        member.Member_Addtime = DateTime.Now;
        member.Member_Trash = 0;
        member.Member_Grade = DefaultGrade;
        member.Member_Account = 0;
        member.Member_Frozen = 0;
        member.Member_AllowSysEmail = 1;
        member.Member_Site = "CN";
        member.Member_Source = Session["customer_source"].ToString();
        //member.U_Member_Type = Member_Type;

        //添加会员基本信息
        if (MyMember.AddMember(member, pub.CreateUserPrivilege("5d071ec6-31d8-4960-a77d-f8209bbab496")))
        {
            MemberInfo memberinfo = GetMemberInfoByEmail(member_email);
            if (memberinfo != null)
            {
                //添加详细信息
                MemberProfileInfo profile = new MemberProfileInfo();

                profile.Member_Profile_MemberID = memberinfo.Member_ID;
                profile.Member_Name = "";
                profile.Member_Sex = -1;
                profile.Member_StreetAddress = "";
                profile.Member_County = "0";
                profile.Member_City = "0";
                profile.Member_State = "0";
                profile.Member_Country = "CN";
                profile.Member_Zip = "";
                profile.Member_Phone_Countrycode = "+86";
                profile.Member_Phone_Areacode = "";
                profile.Member_Phone_Number = "";
                profile.Member_Mobile = "";
                if (Member_Type == 1)
                {
                    profile.Member_County = Member_County;
                    profile.Member_City = Member_City;
                    profile.Member_State = Member_State;
                    profile.Member_Name = Member_Name;
                    profile.Member_Phone_Number = Member_Phone_Number;
                    profile.Member_Mobile = member_mobile;
                    //profile.U_Member_BuyType = U_Member_BuyType;
                    //profile.U_Member_CompanyName = U_Member_CompanyName;
                    //profile.U_Member_Department = U_Member_Department;
                    //profile.Member_StreetAddress = Member_StreetAddress;
                    //profile.U_Member_CompanyKind = U_Member_CompanyKind;
                    //profile.U_Member_CompanyUrl = U_Member_CompanyUrl;
                    //profile.U_Member_CompanyVocation = U_Member_CompanyVocation;
                    //profile.U_Member_MenAmount = U_Member_MenAmount;
                }

                MyMember.AddMemberProfile(profile, pub.CreateUserPrivilege("5d071ec6-31d8-4960-a77d-f8209bbab496"));

                Member_Log(memberinfo.Member_ID, "会员注册");

                //MyEmail.DelU_EmailNotifyRequestByEmail(memberinfo.Member_Email, pub.CreateUserPrivilege("83b084ee-1f49-4da0-b28c-8cdaaec1c193"));

                Session["member_id"] = memberinfo.Member_ID;
                Session["member_email"] = memberinfo.Member_Email;
                Session["member_nickname"] = memberinfo.Member_NickName;
                Session["member_logined"] = true;
                Session["member_emailverify"] = true;
                Session["member_logincount"] = memberinfo.Member_LoginCount + 1;
                Session["member_lastlogin_time"] = memberinfo.Member_LastLogin_Time;
                Session["member_lastlogin_ip"] = memberinfo.Member_LastLogin_IP;
                Session["member_coinremain"] = memberinfo.Member_CoinRemain;
                Session["member_coincount"] = memberinfo.Member_CoinCount;
                Session["member_grade"] = memberinfo.Member_Grade;
                Session["Member_AllowSysEmail"] = memberinfo.Member_AllowSysEmail;
               // Session["Member_Type"] = memberinfo.U_Member_Type;
                Response.Cookies["member_email"].Expires = DateTime.Now.AddDays(365);
                Response.Cookies["member_email"].Value = memberinfo.Member_Email;

                //member_register_sendemailverify(memberinfo.Member_Email, memberinfo.Member_VerifyCode);

                //if (memberinfo.Member_Emailverify == 0)
                //{
                Session["member_emailverify"] = memberinfo.Member_Emailverify;
                    //Response.Redirect("/member/emailverify.aspx");
                //}
                //else
                //{
                    //Response.Redirect("/member/index.aspx");
                //}
                if (Session["url_after_login"] == null)
                {
                    Session["url_after_login"] = "";
                }
                if (tools.NullStr(Session["url_after_login"].ToString()) != "")
                {
                    Response.Redirect(Session["url_after_login"].ToString());
                }
                else
                {
                    Response.Redirect("/member/index.aspx");
                }
            }
            else
            {
                pub.Msg("error", "错误信息", "用户注册失败，请稍后再试！", true, "/index.aspx");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "用户注册失败，请稍后再试！", true, "/index.aspx");
        }
    }

    //会员登录
    public void Member_Login()
    {
        int chk_UserName = tools.CheckInt(Request["chk_UserName"]);
        Response.Cookies["BoHaiUserName"].Expires = DateTime.Now.AddYears(1);
        string Member_Username = tools.CheckStr(tools.NullStr(Request["member_name"]).Trim());
        string Member_Password = Request["member_password"];
        Member_Password = encrypt.MD5(Member_Password);
        if (tools.NullStr(Session["logintype"]) == "False")
        {
            string Trade_Verify = tools.CheckStr(Request["Trade_Verify"]);
            if (Trade_Verify != tools.NullStr(Session["Trade_Verify"]))
            {
                Session["logintype"] = "False";

                if (chk_UserName == 1) { Response.Cookies["BoHaiUserName"].Value = Server.UrlEncode(Member_Username); }
                else { Response.Cookies["BoHaiUserName"].Value = ""; }
                //Response.Redirect("/member/login.aspx?login=vmsg"); 
                Response.Write("验证码错误");
                Response.End();
            }
        }
        if (Member_Username == "")
        {
            Session["logintype"] = "False";
            //Response.Redirect("/member/login.aspx?login=umsg_k");
            Response.Write("请输入您的用户名");
            Response.End();
        }
        MemberInfo memberinfo = MyMember.GetMemberByNickName(Member_Username, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        if (memberinfo != null)
        {
            if (memberinfo.Member_Password != Member_Password)
            {
                Session["logintype"] = "False";

                if (chk_UserName == 1) { Response.Cookies["BoHaiUserName"].Value = Server.UrlEncode(Member_Username); }
                else { Response.Cookies["BoHaiUserName"].Value = ""; }
                //Response.Redirect("/member/login.aspx?login=pmsg");
                Response.Write("密码错误");
                Response.End();
            }
            Response.Write("成功");
            Session["member_id"] = memberinfo.Member_ID;
            Session["member_email"] = memberinfo.Member_Email;
            Session["member_nickname"] = memberinfo.Member_NickName;
            Session["member_logined"] = "True";
            Session["member_logincount"] = memberinfo.Member_LoginCount + 1;
            Session["member_lastlogin_time"] = memberinfo.Member_LastLogin_Time;
            Session["member_lastlogin_ip"] = memberinfo.Member_LastLogin_IP;
            Session["member_coinremain"] = memberinfo.Member_CoinRemain;
            Session["member_coincount"] = memberinfo.Member_CoinCount;
            Session["member_grade"] = memberinfo.Member_Grade;
            Session["Member_AllowSysEmail"] = memberinfo.Member_AllowSysEmail;
            //Session["Member_Type"] = memberinfo.U_Member_Type;

            //if (chk_UserName == 1) { Response.Cookies["BoHaiUserName"].Value = Server.UrlEncode(memberinfo.Member_NickName); }
            //else { Response.Cookies["BoHaiUserName"].Value = ""; }

            //更新购物车价格
            Cart_Price_Update();

            //更新用户登录信息
            MyMember.UpdateMemberLogin(memberinfo.Member_ID, memberinfo.Member_LoginCount + 1, Request.ServerVariables["Remote_Addr"], pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));

            //更新会员等级
            Update_MemberGrade();

            Member_Log(memberinfo.Member_ID, "会员登录");
            
            //判断是否经过了邮件验证
            //if(memberinfo.Member_Emailverify==0)
            //{
            Session["member_emailverify"] = memberinfo.Member_Emailverify;
            //    //Response.Redirect("/member/emailverify.aspx");
            //    Response.Write("/member/emailverify.aspx");
            //}
            //else
            //{
                if (Session["url_after_login"] == null)
                {
                    Session["url_after_login"] = "";
                }
                if(tools.NullStr(Session["url_after_login"].ToString()) !="")
                {
                    //Response.Redirect(Session["url_after_login"].ToString());
                    Response.Write(Session["url_after_login"].ToString());
                }
                else
                {
                    //Response.Redirect("/member/index.aspx");
                    Response.Write("/member/index.aspx");
                }
            //}
        }
        else
        {
            Session["logintype"] = "False";
            if (chk_UserName == 1) { Response.Cookies["BoHaiUserName"].Value = Server.UrlEncode(Member_Username); }
            else { Response.Cookies["BoHaiUserName"].Value = ""; }
            //Response.Redirect("/member/login.aspx?login=umsg_w");
            Response.Write("账号未注册");
            Response.End();
        }
    }

    public void Update_MemberGrade()
    {
        
        MemberInfo memberinfo = GetMemberByID();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberGradeInfo.Member_Grade_Site", "=", "CN"));
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberGradeInfo.Member_Grade_ID", "<>", "2"));
        Query.OrderInfos.Add(new OrderInfo("MemberGradeInfo.Member_Grade_RequiredCoin", "desc"));
        IList<MemberGradeInfo> grades = Mygrade.GetMemberGrades(Query, pub.CreateUserPrivilege("1c955ea6-881f-48d8-ba8d-c5aa7ce9cfea"));
        if (grades != null)
        {
            foreach (MemberGradeInfo grade in grades)
            {
                if (memberinfo.Member_CoinCount >= grade.Member_Grade_RequiredCoin)
                {
                    memberinfo.Member_Grade = grade.Member_Grade_ID;
                    Session["member_grade"] = memberinfo.Member_Grade;
                    MyMember.EditMember(memberinfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));
                    break;
                }
            }
        }
    }

    //会员登录检查
    public void Member_Login_Check(string url_after_login)
    {
        if (Session["member_logined"].ToString() != "True")
        {
            Session["url_after_login"] = url_after_login;
            //if (url_after_login.IndexOf("order_confirm.aspx") > 0 && tools.NullInt(Application["RepidBuy_IsEnable"]) == 1)
            //{
            //    Response.Redirect("/member/login.aspx?action=nologin");
            //}
            //else
            //{
                Response.Redirect("/member/login.aspx");
            //}
        }        
    }

    //Ajax会员登录检查
    public void Member_Login_Check_Ajax()
    {
        if (Session["member_logined"].ToString() != "True")
        {
            Session["url_after_login"] = tools.NullStr(Request["url_login"]);
            Response.Write(tools.NullStr(Request["url_login"]));
        }
        else
        {
            Response.Write("True");
        }
    }

    //会员退出
    public void Member_LogOut()
    { 
        Session.Abandon();
        Session["member_logined"] = false;
        Response.Redirect("/member/login_out.aspx");
    }

    //更新购物车信息
    public void Cart_Price_Update()
    {
        double Product_Price;
        int normal_amount,Product_Coin;


        ProductInfo productinfo=null;
        PackageInfo packageinfo = null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_ParentID", "=", "0"));
        IList<OrdersGoodsTmpInfo> goodstmps = MyCart.GetOrdersGoodsTmps(Query);
        if (goodstmps != null)
        {
            foreach (OrdersGoodsTmpInfo entity in goodstmps)
            {
                //正常购买商品
                if (entity.Orders_Goods_Type == 0)
                {
                    productinfo = MyProduct.GetProductByID(entity.Orders_Goods_Product_ID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                    if (productinfo != null)
                    {
                        Product_Price = pub.Get_Member_Price(productinfo.Product_ID,productinfo.Product_Price);
                        normal_amount = MyCart.Get_Orders_Goods_TypeAmount(Session.SessionID, productinfo.Product_ID, 0);

                        //检查是否团购
                        if (productinfo.Product_IsGroupBuy == 1)
                        {
                            if (normal_amount >= productinfo.Product_GroupNum)
                            {
                                Product_Price = productinfo.Product_GroupPrice;
                            }
                        }

                        Product_Coin = pub.Get_Member_Coin(Product_Price);
                        //检查是否赠送指定积分
                        if (productinfo.Product_IsGiftCoin == 1)
                        {
                            Product_Coin = (int)(Product_Price * productinfo.Product_Gift_Coin);
                        }

                        entity.Orders_Goods_BuyerID = tools.CheckInt(Session["member_id"].ToString());
                        entity.Orders_Goods_Product_Coin = Product_Coin;
                        entity.Orders_Goods_Product_Price = Product_Price;
                        MyCart.EditOrdersGoodsTmp(entity);
                        productinfo = null;
                    }
                }
                if (entity.Orders_Goods_Type == 2&&entity.Orders_Goods_ParentID==0)
                {
                    packageinfo = MyPackage.GetPackageByID(entity.Orders_Goods_Product_ID, pub.CreateUserPrivilege("0dd17a70-862d-4e57-9b45-897b98e8a858"));
                    if (packageinfo != null)
                    {
                        Product_Price = pub.Get_Member_Price(0,packageinfo.Package_Price);
                        Product_Coin = pub.Get_Member_Coin(Product_Price);

                        entity.Orders_Goods_BuyerID = tools.CheckInt(Session["member_id"].ToString());
                        entity.Orders_Goods_Product_Coin = Product_Coin;
                        entity.Orders_Goods_Product_Price = Product_Price;
                        MyCart.EditOrdersGoodsTmp(entity);
                        productinfo = null;
                    }
                }
            }
        }
    }

    

    #endregion


    public void Member_Favorites_Add(string action, int targetid)
    {
        if (targetid == 0)
        {
            Response.Write("请选择要添加到收藏夹的内容");
            Response.End();
        }

        if (tools.NullInt(Session["member_id"]) == 0)
        {
            Response.Write("收藏失败，请稍后再试！");
            Response.End();
        }

        int supplier_id = tools.NullInt(Session["member_id"]);
        if (action == "product")
        {
            ProductInfo product = MyProduct.GetProductByID(targetid, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            if (product != null)
            {
                if (product.Product_IsInsale == 1 && product.Product_IsAudit == 1)
                {
                    MemberFavoritesInfo favorcheck = MyFavor.GetMemberFavoritesByProductID(supplier_id, 0, targetid);
                    if (favorcheck != null)
                    {
                        //pub.Msg("info", "信息提示", "该商品已在您的收藏夹中！", true, "/member/member_favorites.aspx");

                        Response.Write("该商品已在您的收藏夹中");
                        Response.End();
                    }
                    MemberFavoritesInfo favor = new MemberFavoritesInfo();
                    favor.Member_Favorites_ID = 0;
                    favor.Member_Favorites_MemberID = supplier_id;
                    favor.Member_Favorites_Type = 0;
                    favor.Member_Favorites_TargetID = targetid;
                    favor.Member_Favorites_Addtime = DateTime.Now;
                    favor.Member_Favorites_Site = pub.GetCurrentSite();

                    if (MyFavor.AddMemberFavorites(favor))
                    {
                        //pub.Msg("positive", "信息提示", "信息收藏成功！", true, "/member/member_favorites.aspx");

                        Response.Write("success");
                        Response.End();
                    }
                    else
                    {
                        //pub.Msg("info", "信息提示", "收藏失败，请稍后再试！", false, "{back}");
                        Response.Write("收藏失败，请稍后再试！");
                        Response.End();
                    }
                }
                else
                {
                    //pub.Msg("info", "信息提示", "收藏失败，请稍后再试！", false, "{back}");
                    Response.Write("收藏失败，请稍后再试！");
                    Response.End();
                }
            }
            else
            {
                //pub.Msg("info", "信息提示", "收藏失败，请稍后再试！", false, "{back}");
                Response.Write("收藏失败，请稍后再试！");
                Response.End();
            }
        }
        else
        {
            SupplierShopInfo shopinfo = MyShop.GetSupplierShopByID(targetid);
            if (shopinfo != null)
            {
                if (shopinfo.Shop_Status == 1)
                {
                    MemberFavoritesInfo favorcheck = MyFavor.GetMemberFavoritesByProductID(supplier_id, 1, targetid);
                    if (favorcheck != null)
                    {
                        //pub.Msg("info", "信息提示", "该店铺已在您的收藏夹中！", true, "/member/member_shop_favorites.aspx");

                        Response.Write("该店铺已在您的收藏夹中！");
                        Response.End();
                    }
                    MemberFavoritesInfo favor = new MemberFavoritesInfo();
                    favor.Member_Favorites_ID = 0;
                    favor.Member_Favorites_MemberID = supplier_id;
                    favor.Member_Favorites_Type = 1;
                    favor.Member_Favorites_TargetID = targetid;
                    favor.Member_Favorites_Addtime = DateTime.Now;
                    favor.Member_Favorites_Site = pub.GetCurrentSite();

                    if (MyFavor.AddMemberFavorites(favor))
                    {
                        //pub.Msg("positive", "信息提示", "信息收藏成功！", true, "/member/member_shop_favorites.aspx");
                        Response.Write("success");
                        Response.End();
                    }
                    else
                    {
                        Response.Write("收藏失败，请稍后再试！");
                        Response.End();
                    }
                }
                else
                {
                    Response.Write("收藏失败，请稍后再试！");
                    Response.End();
                }
            }
            else
            {
                Response.Write("收藏失败，请稍后再试！");
                Response.End();
            }
        }
    }

    public void Member_Top_Favoriets()
    {
        StringBuilder strHTML = new StringBuilder();

        int member_id = tools.CheckInt(Session["member_id"].ToString());
        string productURL = string.Empty;
        //string targetURL = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 5;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberFavoritesInfo.Member_Favorites_MemberID", "=", member_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberFavoritesInfo.Member_Favorites_Type", "=", "0"));
        Query.OrderInfos.Add(new OrderInfo("MemberFavoritesInfo.Member_Favorites_ID", "Desc"));
        IList<MemberFavoritesInfo> favoriates = MyFavor.GetMemberFavoritess(Query);
        strHTML.Append("<div class=\"li01_box\">");

        if (favoriates != null)
        {
            foreach (MemberFavoritesInfo entity in favoriates)
            {
                ProductInfo product = MyProduct.GetProductByID(entity.Member_Favorites_TargetID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                if (product != null)
                {
                    productURL = tools.NullStr(Application["Site_URL"]).TrimEnd('/') + pageurl.FormatURL(pageurl.product_detail, product.Product_ID.ToString());
                    if (product.Product_IsInsale == 0 || product.Product_IsAudit == 0)
                    {
                        continue;
                    }
                   // productURL = pageurl.FormatURL(pageurl.product_detail, product.Product_ID.ToString());

                    strHTML.Append("<dl>");
                    strHTML.Append("<dt><a href=\"" + productURL + "\" target=\"_blank\">");
                    strHTML.Append("<img src=\"" + pub.FormatImgURL(product.Product_Img, "thumbnail") + "\" /></a></dt>");
                    strHTML.Append("<dd>");
                    strHTML.Append("<p><a href=\"" + productURL + "\" target=\"_blank\">" + product.Product_Name + "</a></p>");
                    //if (product.Product_PriceType == 1)
                    //{
                        //strHTML.Append("<p>" + pub.FormatCurrency(product.Product_Price) + "</p>");
                    strHTML.Append("<p>" + pub.FormatCurrency(pub.Get_Member_Price(product.Product_ID, product.Product_Price)) + "</p>");
                    //}
                    //else
                    //{
                    //    strHTML.Append("<p>" + pub.FormatCurrency(pub.GetProductPrice(product.Product_ManualFee, product.Product_Weight)) + "</p>");
                    //}

                    strHTML.Append("</dd>");
                    strHTML.Append("<div class=\"clear\"></div>");
                    strHTML.Append("</dl>");
                }
            }
        }
        else
        {
            strHTML.Append("<span class=\"tip\">暂无收藏的商品</span>");
        }
        strHTML.Append("</div>");
        Response.Write(strHTML.ToString());
    }
}
