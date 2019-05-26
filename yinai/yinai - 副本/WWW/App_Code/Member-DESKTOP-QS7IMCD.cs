using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.BLL.ORD;
using Glaer.Trade.B2C.BLL.Product;
using Glaer.Trade.B2C.BLL.SAL;
using Glaer.Trade.B2C.BLL.Sys;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Http;
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.Util.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text;
using System.Web;


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
    IMember MyMember;
    IMemberGrade Mygrade;
    IMemberLog MyMemLog;
    Public_Class pub = new Public_Class();
    SysMessage messageclass = new SysMessage();
    IEncrypt encrypt;
    IMemberConsumption MyConsumption;

    IMemberFavorites MyFavor;
    IProduct MyProduct;
    IProductTag MyTag;
    IPackage MyPackage;
    IProductReview MyReview;
    IFeedBack MyFeedback;
    IMemberAddress MyAddr;
    IOrdersGoodsTmp MyCart;
    IPromotionFavorCoupon MyCoupon;
    Addr addr = new Addr();
    Orders Myorder = new Orders();
    private IShoppingAsk MyAsk;
    private PageURL pageurl;
    //U_IEmailNotifyRequest MyEmail;
    ISupplierShopEvaluate MyShopEvaluate;
    IMemberAccountLog MyAccountLog;
    ISupplier MySupplier;
    ISupplierShop MyShop;
    IMemberInvoice MyInvoice;

    IOrders MyOrders;
    IOrdersLog MyOrdersLog;
    IOrdersDelivery MyDelivery;
    IOrdersInvoice MyOrdersInvoice;
    IOrdersContract MyOContract;
    IOrdersPayment MyPayment;
    IContractTemplate MyContract;
    IContract My_Contract;
    IHttpHelper HttpHelper;
    IJsonHelper JsonHelper;
    IMemberCertType MyCertType;
    IMemberToken MyToken;
    ISysMessage MySysMessage;
    IMemberPurchase MyPurchase;
    IMemberPurchaseReply MyPurchaseReply;
    IMemberCert MyCert;
    ISupplierMerchantsMessage MySupplierMessage;
    //private ISupplierGrade MySupplierGrade;
    ITender MyTender;

    ITextSharp textsharp;
    Supplier supplier = new Supplier();
    Credit credit;
    Payment payment;
    private ISQLHelper DBHelper;
    IMemberSubAccount MySubAccount;
    ISupplierSubAccount MySupplierSubAccount;
    SecurityUtil securityUtil = new SecurityUtil();

    string partner_id;
    string tradesignkey, signkey;
    int tokentime;
    string mgs, ntalker;
    string erp_url, erp_key, erp_pub_key;



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
        MyMemLog = MemberLogFactory.CreateMemberLog();
        encrypt = EncryptFactory.CreateEncrypt();
        MyConsumption = MemberConsumptionFactory.CreateMemberConsumption();
        MyFavor = MemberFavoritesFactory.CreateMemberFavorites();
        MyProduct = ProductFactory.CreateProduct();
        MyPackage = PackageFactory.CreatePackage();
        MyReview = ProductReviewFactory.CreateProductReview();
        MyFeedback = FeedBackFactory.CreateFeedBack();
        MyAddr = MemberAddressFactory.CreateMemberAddress();
        MyCart = OrdersGoodsTmpFactory.CreateOrdersGoodsTmp();
        MyCoupon = PromotionFavorCouponFactory.CreatePromotionFavorCoupon();

        //MySupplierGrade = SupplierGradeFactory.CreateSupplierGrade();
        // MyEmail = U_EmailNotifyRequestFactory.CreateU_EmailNotifyRequest();
        MyAccountLog = MemberAccountLogFactory.CreateMemberAccountLog();
        MyShopEvaluate = SupplierShopEvaluateFactory.CreateSupplierShopEvaluate();
        MySupplier = SupplierFactory.CreateSupplier();
        MyShop = SupplierShopFactory.CreateSupplierShop();
        MyAsk = ShoppingAskFactory.CreateShoppingAsk();
        MyTag = ProductTagFactory.CreateProductTag();
        MyInvoice = MemberInvoiceFactory.CreateMemberInvoice();
        MySubAccount = MemberSubAccountFactory.CreateMemberSubAccount();
        MySupplierSubAccount = SupplierSubAccountFactory.CreateSupplierSubAccount();
        MyOrders = OrdersFactory.CreateOrders();
        MyOrdersLog = OrdersLogFactory.CreateOrdersLog();
        MyDelivery = OrdersDeliveryFactory.CreateOrdersDelivery();
        MyOrdersInvoice = OrdersInvoiceFactory.CreateOrdersInvoice();
        MyPayment = OrdersPaymentFactory.CreateOrdersPayment();
        MyOContract = OrdersFactory.CreateOrdersContract();
        MyContract = ContractTemplateFactory.CreateContractTemplate();
        My_Contract = ContractFactory.CreateContract();
        MyPurchaseReply = MemberPurchaseReplyFactory.CreateMemberPurchaseReply();

        HttpHelper = HttpHelperFactory.CreateHttpHelper();
        JsonHelper = JsonHelperFactory.CreateJsonHelper();
        MyCertType = MemberCertTypeFactory.CreateMemberCertType();
        MyCert = MemberCertFactory.CreateMemberCert();
        MySysMessage = SysMessageFactory.CreateSysMessage();
        textsharp = new ITextSharp();
        credit = new Credit();
        payment = new Payment();
        DBHelper = SQLHelperFactory.CreateSQLHelper();

        MyPurchase = MemberPurchaseFactory.CreateMemberPurchase();
        MyToken = MemberTokenFactory.CreateMemberToken();

        MySupplierMessage = SupplierMerchantsMessageFactory.CreateSupplierMerchantsMessage();
        MyTender = TenderFactory.CreateTender();

        pageurl = new PageURL(int.Parse(Application["Static_IsEnable"].ToString()));

        tokentime = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["tokentime"]);
        tradesignkey = System.Web.Configuration.WebConfigurationManager.AppSettings["tradesignkey"].ToString();
        partner_id = tools.NullStr(Application["CreditPayment_Code"]);
        signkey = System.Web.Configuration.WebConfigurationManager.AppSettings["signkey"].ToString();
        erp_url = System.Web.Configuration.WebConfigurationManager.AppSettings["erp_url"].ToString();
        erp_key = System.Web.Configuration.WebConfigurationManager.AppSettings["ERP_Key"].ToString();
        erp_pub_key = System.Web.Configuration.WebConfigurationManager.AppSettings["ERP_Pub_Key"].ToString();
        ntalker = System.Web.Configuration.WebConfigurationManager.AppSettings["ntalker"].ToString();
        mgs = System.Web.Configuration.WebConfigurationManager.AppSettings["mgs"].ToString();
    }

    #region"辅助函数"

    //检查昵称是否使用
    public bool Check_Member_Nickname(string nick_name)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_NickName", "=", nick_name));
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Trash", "=", "0"));
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

    //检查注册ip有效性
    public bool Check_Member_IP(string IP)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_RegIP", "=", IP));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Trash", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Desc"));
        IList<MemberInfo> entitys = MyMember.GetMembers(Query, pub.CreateUserPrivilege("3a9a9cdf-ef00-407d-98ef-44e23be397e8"));
        if (entitys != null)
        {
            if (entitys[0].Member_Addtime.AddDays(1) > DateTime.Now)
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
        int member_id = tools.NullInt(Session["member_id"]);
        if (member_id > 0)
        {
            return MyMember.GetMemberByID(member_id, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        }
        else
        {
            return null;
        }
    }

    public MemberInfo GetMemberByID(int ID)
    {
        return MyMember.GetMemberByID(ID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
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
        int requiredcoid = 0;
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

    public string Get_Supplier_Name(int Supplier_ID)
    {
        string Supplier_Name = "--";
        SupplierInfo entity = MySupplier.GetSupplierByID(Supplier_ID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
        if (entity != null)
        {
            Supplier_Name = entity.Supplier_CompanyName;

        }
        return Supplier_Name;
    }

    //根据商品编号获取商品信息
    public ProductInfo GetProductByID(int ID)
    {
        return MyProduct.GetProductByID(ID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
    }

    public string GetProductName(int product_id)
    {
        string product_name = "";
        ProductInfo product = GetProductByID(product_id);

        if (product != null)
        {
            product_name = product.Product_Name;

        }
        return product_name;
    }

    public string GetMail_Site(string site_url)
    {
        switch (site_url)
        {
            case "qq.com":
                site_url = "mail.qq.com";
                break;
            case "126.com":
                site_url = "mail.126.com";
                break;
            case "163.com":
                site_url = "mail.163.com";
                break;
            case "189.cn":
                site_url = "mail.189.cn";
                break;
            case "139.com":
                site_url = "mail.139.com";
                break;
            case "wo.com.cn":
                site_url = "mail.wo.com.cn";
                break;
        }
        return site_url;
    }


    public bool CheckNickname(string strnickname)
    {
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9_\u4e00-\u9fa5]+$");
        return regex.IsMatch(strnickname);
    }

    public string CreateMobileVerifyCode()
    {
        string verifycode = pub.Createvkey(6);

        if (verifycode.Length > 0)
        {
            Session["Trade_MobileVerify"] = verifycode;
        }
        return verifycode;
    }


    public void BidUpRequireQuick()
    {

    }

    #endregion

    #region "AJAx函数"
    public void Check_MemberType()
    {
        int type = tools.CheckInt(Request["val"]);
        if (type == 0)
        {
            Response.Write("<font color=\"#cc0000\">请选择采购商类型！</font>");
            return;
        }
    }


    public void Check_Nickname()
    {
        string member_nickname = tools.CheckStr(Request["val"]).Trim();
        if (member_nickname == "" || member_nickname == "6-21位字符，可由中文、英文、数字及_、-组成")
        {
            Response.Write("<font color=\"#cc0000\">请输入用户名！</font>");
            return;
        }
        else
        {
            if (CheckNickname(member_nickname))
            {
                //if (member_nickname.Length > 15)
                //{
                //    Response.Write("<font color=\"#cc0000\">用户名不要超过15个字符！</font>");
                //    return;
                //}
                if (Check_Member_Nickname(member_nickname) == false)
                {
                    Response.Write("<font color=\"#00a226\">用户名输入正确！</font>");
                    return;
                }
                else
                {
                    Response.Write("<font color=\"#cc0000\">该用户名已被使用，请使用其他用户名注册，如果您是&quot;" + member_nickname + "&quot;,请<a href=\"/login.aspx\">登录</a></font>");
                    return;
                }
            }
            else
            {
                Response.Write("<font color=\"#cc0000\">用户名输入错误，请检查后重新输入！</font>");
                return;
            }
        }
    }


    public void Check_LoginNickname()
    {


        string member_name = tools.CheckStr(Request["val"]);
        if (member_name == "用户名/Email/手机" || member_name.Length == 0)
        {
            Response.Write("<font color=\"#cc0000\">请输入用户名！</font>");
            return;
        }
        else
        {
            if (!CheckNickname(member_name))
            {
                Response.Write("<font color=\"#cc0000\">用户名含有特殊字符！</font>");
                return;
            }
            else
            {
                Response.Write("");
                return;
            }
        }
    }


    public void Check_FeedBackName()
    {
        string FeedBackName = tools.CheckStr(Request["val"]).Trim();
        if (FeedBackName == "")
        {
            Response.Write("<font color=\"#cc0000\">请输入用户名！</font>");
            return;
        }
        else
        {
            if (CheckNickname(FeedBackName))
            {
                if (FeedBackName.Length > 15)
                {
                    Response.Write("<font color=\"#cc0000\">用户名不要超过15个字符！</font>");
                    return;
                }
                else
                {
                    Response.Write("<font color=\"#00a226\">用户名输入正确！</font>");
                    return;
                }

            }
            else
            {
                Response.Write("<font color=\"#cc0000\">用户名输入错误，请检查后重新输入！</font>");
                return;
            }
        }
    }

    public void Check_MemberEmail()
    {
        string member_email = tools.CheckStr(Request["val"]);
        if (member_email == "" || member_email == "请输入一个有效的邮箱")
        {
            Response.Write("<font color=\"#cc0000\">请输入E-mail！</font>");
            return;
        }
        else
        {
            if (tools.CheckEmail(member_email))
            {

                if (Check_Member_Email(member_email) || new Supplier().Check_Supplier_Email(member_email))
                {
                    //Response.Write("<font color=\"#cc0000\">该邮件地址已被使用。请使用另外一个邮件地址进行注册</font>");
                    Response.Write("<font color=\"#cc0000\">该邮件地址已被使用。</font>");
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
        if (member_password.Length < 6 || member_password.Length > 20 || member_password == "请输入6～20位密码(A-Z,a-z,0-9,不要输入空格)")
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

    //检验注册用户名是否符合要求
    public void Check_Real_Name()
    {
        string member_realname = tools.CheckStr(Request["val"]);
        if (member_realname.Length < 1)
        {
            Response.Write("<font color=\"#cc0000\">请输入用户名</font>");
            return;
        }
        else
        {
            if (Check_Nick_Member(member_realname))
            {
                Response.Write("<font color=\"#cc0000\">该用户名已被使用。</font>");
                return;
            }
            else
            {
                Response.Write("<font color=\"#00a226\">提交后不可更改！</font>");
                return;
            }

        }
    }



    //检验注册真实姓名是否符合要求
    public void Check_Member_Profile_Contact()
    {
        string member_realname = tools.CheckStr(Request["val"]);
        if (member_realname.Length < 1)
        {
            Response.Write("<font color=\"#cc0000\">请输入真实姓名</font>");
            return;
        }
        else
        {
            Response.Write("<font color=\"#00a226\">真实姓名输入正确！</font>");
            return;
        }
    }


    public bool Check_Nick_Member(string member_realname)
    {
        //QueryInfo Query = new QueryInfo();
        //Query.PageSize = 1;
        //Query.CurrentPage = 1;
        //Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_NickName", "=", member_realname));
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Trash", "=", "0"));
        //Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", "CN"));
        //Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Desc"));
        //IList<MemberInfo> entitys = MyMember.GetMembers(Query, pub.CreateUserPrivilege("3a9a9cdf-ef00-407d-98ef-44e23be397e8"));
        MemberInfo entitys = MyMember.GetMemberByNickName(member_realname, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        //PageInfo page = MyMember.GetPageInfo(Query, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        if (entitys != null)
        {
            if (entitys.Member_ID > 0)
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



    //检验注册公司名称是否符合要求
    public void Check_Real_CompanayName()
    {
        string company_name = tools.CheckStr(Request["val"]);
        if (company_name.Length < 1)
        {
            Response.Write("<font color=\"#cc0000\">请输入公司名称</font>");
            return;
        }
        else
        {
            if (CheckSupplierCompanyName(company_name))
            {
                Response.Write("<font color=\"#cc0000\">该公司名称已被使用。</font>");
                return;
            }
            else
            {
                Response.Write("<font color=\"#00a226\">公司名称输入正确！</font>");
                return;
            }

        }
    }


    public void Check_LoginMemberPasswprd()
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
                Response.Write("");
                return;
            }
            else
            {
                Response.Write("<font color=\"#cc0000\">密码包含特殊字符，只接受A-Z，a-z，0-9，不要输入空格</font>");
                return;
            }
        }
    }



    public void Check_MemberrePasswprd()
    {
        string member_repassword = tools.CheckStr(Request["val"]);
        string member_password = tools.CheckStr(Request["val1"]);
        if (member_repassword.Length < 6 || member_repassword.Length > 20 || member_repassword == "请再次输入密码")
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
        if (member_mobile == "" || member_mobile == "请输入有效的手机号码")
        {
            Response.Write("<font color=\"#cc0000\">请输入手机号码！</font>");
            return;
        }
        else
        {

            if (pub.Checkmobile(member_mobile))
            {

                if (Check_Member_LoginMobile(member_mobile))
                {
                    Response.Write("<font color=\"#cc0000\">该手机号码已被注册！</font>");
                    return;
                }
                else
                {
                    Response.Write("<font color=\"#00a226\">手机号码输入正确！</font>");
                    return;
                }

            }
            else
            {
                Response.Write("<font color=\"#cc0000\">无效的手机号码！</font>");
                return;
            }
        }
    }

    //检验司机手机号码是否正确
    public void Check_DriverMobile()
    {
        string member_mobile = tools.CheckStr(Request["val"]);
        if (member_mobile == "" || member_mobile == "请输入有效的手机号码")
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

    public void CheckCart_MemberMobile()
    {
        string member_mobile = tools.CheckStr(Request["val"]);
        if (member_mobile == "" || member_mobile == "请输入有效的手机号码")
        {
            Response.Write("<font color=\"#cc0000\">请输入手机号码！</font>");
            return;
        }
        else
        {
            //if (pub.Checkmobile_back(member_mobile))
            //{
            //}

            if (pub.Checkmobile(member_mobile))
            {

                //if (Check_Member_LoginMobile(member_mobile))
                //{
                //    Response.Write("<font color=\"#cc0000\">该手机号码已被注册！</font>");
                //    return;
                //}
                //else
                //{
                Response.Write("<font color=\"#00a226\">手机号码输入正确！</font>");
                return;
                //}

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
                Response.Write("<font color=\"#00a226\"></font>");
                return;
            }
            else
            {
                Response.Write("<font color=\"#cc0000\">电话格式错误，请重新输入！</font>");
                return;
            }
        }
    }

    public void Check_Member_Feedback()
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
                Response.Write("<font color=\"#00a226\">联系人电话号码正确</font>");
                return;
            }
            else
            {
                Response.Write("<font color=\"#cc0000\">电话格式错误，请重新输入！</font>");
                return;
            }
        }
    }
    public void Check_Member_FeedAmount()
    {
        double bid_bond = 0.00;
        string var = tools.CheckStr(Request["val"]);
        //var.i
        if ((var == "0") || (var == ""))
        {
            Response.Write("<font color=\"#cc0000\">请输入融资金额！</font>");
            return;
        }

        else
        {
            if (tools.CheckStr(var.ToString()).Length > 0)
            {
                bid_bond = tools.CheckFloat(var.ToString());
                if (bid_bond > 0)
                {
                    Response.Write("<font color=\"#009900\">融资金额正确！</font>");
                    return;
                }
                else
                {
                    Response.Write("<font color=\"#cc0000\">请输入正确的融资金额！</font>");
                    return;
                }
            }
            else
            {
                Response.Write("<font color=\"#009900\">融资金额正确！</font>");
                return;
            }
        }
    }
    //public void Check_Member_FeedAmount()
    //{
    //    string member_mobile = tools.CheckStr(Request["val"]);
    //    if (member_mobile == "")
    //    {
    //        Response.Write("<font color=\"#cc0000\">请输入联系人固定电话！</font>");
    //        return;
    //    }
    //    else
    //    {
    //        if (pub.Checkmobile(member_mobile))
    //        {
    //            Response.Write("<font color=\"#00a226\">联系人电话号码正确</font>");
    //            return;
    //        }
    //        else
    //        {
    //            Response.Write("<font color=\"#cc0000\">电话格式错误，请重新输入！</font>");
    //            return;
    //        }
    //    }
    //}

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

    public void Check_MobileVerifycode()
    {
        string verifycode = tools.CheckStr(Request["val"]);
        if (verifycode == "" || verifycode == "请输入您获取的短信验证码")
        {
            Response.Write("<font color=\"#cc0000\">请输入短信验证码！</font>");
            return;
        }
        else
        {
            if (verifycode == Session["Trade_MobileVerify"].ToString())
            {
                Response.Write("<font color=\"#00a226\">短信验证码输入正确！</font>");
                return;
            }
            else
            {
                Response.Write("<font color=\"#cc0000\">无效的短信验证码！</font>");
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

    public void Check_Company()
    {
        string company = tools.CheckStr(Request["val"]);
        if (company == "" || company == "请输入完整的公司名称")
        {
            Response.Write("<font color=\"#cc0000\">请输入公司名称！</font>");
            return;
        }
        else
        {
            Response.Write("<font color=\"#00a226\">公司名称输入正确！</font>");
            return;
        }
    }

    public void Check_ContactName()
    {
        string name = tools.CheckStr(Request["val"]);
        if (name == "")
        {
            Response.Write("<font color=\"#cc0000\">请输入联系人！</font>");
            return;
        }
        else
        {
            Response.Write("<font color=\"#00a226\">联系人输入正确！</font>");
            return;
        }
    }

    public void Check_Address()
    {
        string state = tools.CheckStr(Request["state"]);
        string city = tools.CheckStr(Request["city"]);
        string county = tools.CheckStr(Request["county"]);
        string address = tools.CheckStr(Request["val"]);

        if (state == "" || city == "" || county == "" || address == "")
        {
            Response.Write("<font color=\"#cc0000\">请完善注册地信息！</font>");
            return;
        }
        else
        {
            Response.Write("<font color=\"#00a226\">注册地信息填写正确！</font>");
            return;
        }
    }

    public void Check_ZipCode()
    {
        string member_zipcode = tools.CheckStr(Request["val"]);
        if (member_zipcode == "")
        {
            Response.Write("<font color=\"#ff0000\">请输入邮政编码！</font>");
            return;
        }
        else
        {
            if (Check_Zip(member_zipcode))
            {
                Response.Write("<font color=\"#00a226\">信息输入正确</font>");
                return;
            }
            else
            {
                Response.Write("<font color=\"#ff0000\">邮政编码错误，请重新输入！</font>");
                return;
            }
        }
    }

    #endregion

    //会员虚拟账户操作日志
    public void Member_Account_Log(int Member_ID, double Amount, string Log_note)
    {
        double Member_AccountRemain = 0;
        MemberInfo member = MyMember.GetMemberByID(Member_ID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
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
            accountLog.Account_Log_Site = "CN";

            MyAccountLog.AddMemberAccountLog(accountLog);

            if (Amount != 0)
            {
                member.Member_Account = Member_AccountRemain + Amount;
            }
            if (member.Member_Account >= 0)
            {
                MyMember.EditMember(member, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));
            }

        }
    }

    //会员积分消费
    public void Member_Coin_AddConsume(int coin_amount, string coin_reason, int member_id, bool is_return)
    {
        int Member_CoinRemain = 0;
        MemberInfo member = MyMember.GetMemberByID(member_id, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
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

            MyConsumption.AddMemberConsumption(consumption);

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

            MyMember.EditMember(member, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));
        }
    }

    #region 手机验证码

    public void Check_LoginMobile()
    {
        string member_mobile = tools.CheckStr(Request["val"]);
        if (member_mobile == "")
        {
            Response.Write("<font color=\"#cc0000\">请输入手机号码！</font>");
        }
        else
        {
            if (pub.Checkmobile_back(member_mobile))
            {
                if (Check_Member_LoginMobile(member_mobile))
                {
                    Response.Write("<font color=\"#cc0000\">该手机号码已被使用。请使用另外一个手机号码</font>");
                }
                else
                {
                    Response.Write("<font color=\"#00a226\">手机号码输入正确！</font>");
                }
            }
            else
            {
                Response.Write("<font color=\"#cc0000\">无效的手机号码！</font>");
            }
        }
    }

    /// <summary>
    /// 短信效验码验证
    /// </summary>
    public void Check_SMS_CheckCode()
    {
        string verifycode = tools.CheckStr(Request["val"]);
        string sign = tools.CheckStr(Request["sign"]);

        if (sign.Length == 0)
        {
            if ("True".Equals(Convert.ToString(Session["member_logined"])))
            {
                sign = Convert.ToString(Session["member_loginmobile"]);
            }
        }

        Dictionary<string, string> smscheckcode = Session["sms_check"] as Dictionary<string, string>;



        if (smscheckcode == null || verifycode.Length == 0)
        {
            Response.Write("<font color=\"#ffffff\">请输入短信效验码！</font>");
            return;
        }

        if (smscheckcode["sign"] != sign || verifycode != smscheckcode["code"])
        {
            Response.Write("<font color=\"#ffffff\">短信验证码输入错误！</font>");
            return;
        }

        if ((Convert.ToDateTime(smscheckcode["expiration"]) - DateTime.Now).TotalSeconds < 0)
        {
            Response.Write("<font color=\"#ffffff\">短信效验码过期！</font>");
            return;
        }

        Response.Write("<font color=\"#ffffff\">短信效验码正确！</font>");
    }

    /// <summary>
    /// 检查手机号是否使用
    /// </summary>
    /// <param name="LoginMobile"></param>
    /// <returns></returns>
    public bool Check_Member_LoginMobile(string LoginMobile)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_LoginMobile", "=", LoginMobile));
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



    /// <summary>
    /// 检查手机号是否使用
    /// </summary>
    /// <param name="LoginMobile"></param>
    /// <returns></returns>
    private bool CheckSupplierCompanyName(string company_name)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_CompanyName", "=", company_name));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", pub.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("SupplierInfo.Supplier_ID", "Desc"));
        PageInfo page = MySupplier.GetPageInfo(Query, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
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

    /// <summary>
    /// 获取登录名手机号
    /// </summary>
    /// <param name="loginname"></param>
    /// <returns></returns>
    public string GetLoginNameMobile(string loginname)
    {
        MemberInfo memberinfo = null;
        if (pub.Checkmobile_back(loginname))
        {
            #region 手机处理

            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_LoginMobile", "=", loginname));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_LoginMobileverify", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", "CN"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Trash", "=", "0"));
            IList<MemberInfo> entityList = MyMember.GetMembers(Query, pub.CreateUserPrivilege("3a9a9cdf-ef00-407d-98ef-44e23be397e8"));
            if (entityList != null)
            {
                memberinfo = entityList[0];
            }

            #endregion
        }
        else if (tools.CheckEmail(loginname))
        {
            #region 邮箱处理

            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Email", "=", loginname));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", "CN"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Trash", "=", "0"));
            IList<MemberInfo> entityList = MyMember.GetMembers(Query, pub.CreateUserPrivilege("3a9a9cdf-ef00-407d-98ef-44e23be397e8"));
            if (entityList != null)
            {
                memberinfo = entityList[0];
            }

            #endregion
        }
        else
        {
            memberinfo = MyMember.GetMemberByNickName(loginname, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        }

        if (memberinfo == null)
        {
            return string.Empty;
        }
        else
        {
            if (memberinfo.Member_LoginMobileverify == 1)
            {
                return memberinfo.Member_LoginMobile;
            }
            else
            {
                return string.Empty;
            }
        }

    }

    #endregion

    #region 注册/登录

    /// <summary>
    /// 验证采购商登录状态
    /// </summary>
    /// <param name="url_after_login"></param>
    public void Member_Login_Check(string url_after_login)
    {
        int Member_AuditStatus = -1;
        //商家子会员登录条件(商家登录 会员没登陆)
        //if (!((Session["member_logined"].ToString() != "True")&&(Session["supplier_logined"].ToString()== "True")))
        //{
        //     Response.Redirect("/supplier/index.aspx");
        //}
        if ((Session["member_logined"].ToString() != "True"))
        {
            //    if ((Session["supplier_sublogined"].ToString() == "True"))
            //    {
            //        Response.Redirect("supplier/index.aspx");
            //    }

            //}
            //else
            //{
            Response.Redirect("/supplier/index.aspx");
        }


        if (Session["member_logined"].ToString() != "True")
        {
            Session["url_after_login"] = url_after_login;

            //pub.Msg("error", "错误信息", "请先登录账号！", true, "/().aspx");
            Response.Redirect("/login.aspx");

        }
        MemberInfo memberinfo = MyMember.GetMemberByID(tools.CheckInt(Session["member_id"].ToString()), pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        if (memberinfo != null)
        {
            Member_AuditStatus = memberinfo.Member_AuditStatus;
            //Session["supplier_logined"] = "True";
        }
        if (Member_AuditStatus == 0)
        {
            Response.Redirect("/member/account_profile.aspx");
        }
        else if (tools.NullInt(Session["member_auditstatus"]) == -1)
        {
            Response.Redirect("/member/account_profile.aspx");
        }
    }


    public void Member_Login_Check1(string url_after_login)
    {
        int Member_AuditStatus = -1;

        //商家子会员登录条件(商家登录 会员没登陆)





        if (Session["member_logined"].ToString() != "True")
        {
            Session["url_after_login"] = url_after_login;

            //pub.Msg("error", "错误信息", "请先登录账号！", true, "/().aspx");
            Response.Redirect("/login.aspx");

        }
        else
        {
            string Member_Permissions = "";
            if ((tools.NullStr(Session["subPrivilege"]) == "") && (!(Session["IsSubPrivilege_Logined"] == "True")))
            {
                MemberInfo memberinfo = MyMember.GetMemberByID(tools.CheckInt(Session["member_id"].ToString()), pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                if (memberinfo != null)
                {
                    Member_AuditStatus = memberinfo.Member_AuditStatus;
                }
                if (Member_AuditStatus == 0)
                {
                    Response.Redirect("/member/account_profile.aspx");
                }
                else if (tools.NullInt(Session["member_auditstatus"]) == -1)
                {
                    Response.Redirect("/member/account_profile.aspx");
                }
                else
                {
                    Session["IsFirstTag"] = "False";
                    Response.Redirect(url_after_login);

                    //bool IsFirst = false;
                }
            }
            else
            {
                Member_Permissions = Session["subPrivilege"].ToString();
                List<string> oTempList = new List<string>(Member_Permissions.Split(','));
                if (!(oTempList.Contains("1") || oTempList.Contains("3") || oTempList.Contains("4")))
                {
                    Response.Redirect("/supplier/index.aspx");

                }
            }
        }

    }

    public void Member_Register()
    {
        //真实姓名
        string Member_Profile_Contact = tools.CheckStr(tools.NullStr(Request["Member_Profile_Contact"]).Trim());
        //用户名
        string member_nickname = tools.CheckStr(tools.NullStr(Request["member_realname"]).Trim());
        string member_email = tools.CheckStr(tools.NullStr(Request["member_email"]).Trim());
        string member_mobile = tools.CheckStr(tools.NullStr(Request["member_mobile"]).Trim());
        string member_password = tools.CheckStr(tools.NullStr(Request["member_password"]).Trim());
        string member_password_confirm = tools.CheckStr(tools.NullStr(Request["member_password_confirm"]).Trim());
        string member_company = tools.CheckStr(tools.NullStr(Request["member_company"]).Trim());
        string member_state = tools.CheckStr(tools.NullStr(Request["member_state"]));
        string member_city = tools.CheckStr(tools.NullStr(Request["member_city"]));
        string member_county = tools.CheckStr(tools.NullStr(Request["member_county"]));
        string member_address = tools.CheckStr(tools.NullStr(Request["member_address"]));
        //string member_realname = tools.CheckStr(tools.NullStr(Request["member_realname"]));
        string verifycode = tools.CheckStr(tools.NullStr(Request.Form["verifycode"])).ToLower();
        string mobile_verifycode = tools.CheckStr(tools.NullStr(Request.Form["mobile_verifycode"]));
        int Isagreement1 = tools.CheckInt(tools.NullStr(Request.Form["checkbox_agreement1"]));
        int Isagreement2 = tools.CheckInt(tools.NullStr(Request.Form["checkbox_agreement2"]));
        int Member_Type = tools.CheckInt(tools.NullStr(Request["member_type"]));
        //用户名
        string member_realname = tools.CheckStr(tools.NullStr(Request["member_realname"]));

        //注册用户真实姓名，即member_profile里面 联系人字段
        //string Member_Profile_Contact = tools.CheckStr(tools.NullStr(Request.Form["Member_Profile_Contact"]));
        Session["member_nicknames"] = member_nickname;

        int DefaultGrade = 1;
        string Member_Phone_Number = "";

        MemberGradeInfo member_grade = GetMemberDefaultGrade();
        if (member_grade != null)
        {
            DefaultGrade = member_grade.Member_Grade_ID;
        }
        #region  用户名有效性验证
        if (member_realname.Length > 0)
        {
            Encoding name_length = System.Text.Encoding.GetEncoding("gb2312");
            byte[] bytes = name_length.GetBytes(member_realname);
            //if (bytes.Length > 15)
            //{
            //    pub.Msg("error", "信息提示", "用户名不要超过15个字符。", false, "{back}");
            //}

            if (Check_Member_Nickname(member_realname))
            {
                pub.Msg("error", "该用户名已被使用", "该用户名已被使用。请使用另外一个用户名进行注册", false, "{back}");
            }
        }
        else
        {
            //pub.Msg("error", "用户名无效", "请输入有效的用户名", false, "{back}");
        }




        #endregion

        #region  公司名称有效性检验

        if (member_company.Length < 1)
        {
            pub.Msg("error", "请输入公司名称", "公司名称不能为空，请输入公司名称", false, "{back}");
        }
        else
        {
            if (CheckSupplierCompanyName(member_company))
            {
                pub.Msg("error", "该公司名称已被使用", "该公司名称已经存在。请使用另外一个公司名进行注册", false, "{back}");
            }
        }
        #endregion


        #region  检验真实姓名是否为空
        if (Member_Profile_Contact == "")
        {
            pub.Msg("error", "错误信息", "请输入真实姓名", false, "{back}");
        }
        #endregion

        #region  邮箱验证码验证
        //if (tools.CheckEmail(member_email) == false)
        //{
        //    pub.Msg("error", "邮件地址无效", "请输入有效的邮件地址", false, "{back}");
        //}
        //else
        //{
        //    if (Check_Member_Email(member_email))
        //    {
        //        pub.Msg("error", "该邮件地址已被使用", "该邮件地址已被使用。请使用另外一个邮件地址进行注册", false, "{back}");
        //    }
        //    if (supplier.Check_Supplier_Email(member_email))
        //    {
        //        pub.Msg("error", "该邮件地址已被使用", "该邮件地址已被使用。请使用另外一个邮件地址进行注册", false, "{back}");
        //    }
        //}
        #endregion

        #region 手机号验证

        if (member_mobile == "")
        {
            pub.Msg("error", "错误信息", "请输入手机号码", false, "{back}");
        }
        else
        {
            if (pub.Checkmobile_back(member_mobile))
            {
                if (Check_Member_LoginMobile(member_mobile))
                {
                    pub.Msg("error", "错误信息", "该手机号码已被使用。请使用另外一个手机号码进行注册", false, "{back}");
                }
            }
            else
            {
                pub.Msg("error", "错误信息", "无效的手机号码", false, "{back}");
            }
        }


        //if (true)
        //{
        //    Check_Nickname
        //}

        #endregion



        #region 短信效验码验证
        Dictionary<string, string> sms_check = Session["sms_check"] as Dictionary<string, string>;
        if (mobile_verifycode.Length == 0 || mobile_verifycode != sms_check["code"])
        {
            pub.Msg("error", "错误信息", "短信效验码错误", false, "{back}");
        }

        if ((Convert.ToDateTime(sms_check["expiration"]) - DateTime.Now).TotalSeconds < 0)
        {
            pub.Msg("error", "错误信息", "短信效验码过期", false, "{back}");
        }
        sms_check = null;
        Session.Remove("sms_check");

        #endregion

        #region 注册密码邮箱行验证
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
        #endregion

        //#region  验证码
        //if (verifycode != Session["Trade_Verify"].ToString() || verifycode.Length == 0)
        //{
        //    pub.Msg("error", "验证码输入错误", "验证码输入错误", false, "{back}");
        //}
        //#endregion






        //if (Check_Member_IP(Request.ServerVariables["remote_addr"]))
        //{
        //    pub.Msg("error", "信息提示", "同一ip不可同时注册！", false, "{back}");
        //}


        if (Isagreement1 != 1)
        {
            pub.Msg("error", "您需要接受用户注册协议", "要完成注册，您需要接受用户注册协议", false, "{back}");
        }
        if (Isagreement2 != 1)
        {
            pub.Msg("error", "您需要接受用户注册协议", "要完成注册，您需要接受易耐网中信附属账户开通协议", false, "{back}");
        }

        MemberInfo member = new MemberInfo();
        member.Member_ID = 0;
        member.Member_Email = member_email;
        member.Member_Emailverify = 0;
        member.Member_LoginMobile = member_mobile;
        member.Member_LoginMobileverify = 1;
        member.Member_NickName = member_realname;
        member.Member_Password = encrypt.MD5(member_password);
        member.Member_VerifyCode = pub.Createvkey();
        member.Member_LoginCount = 1;
        member.Member_LastLogin_IP = tools.NullStr(Request.ServerVariables["remote_addr"]);
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
        member.Member_Status = 0;
        member.Member_AuditStatus = 1;
        member.Member_Cert_Status = 0;
        member.Member_RegIP = tools.NullStr(Request.ServerVariables["remote_addr"]);
        member.Member_Source = Session["customer_source"].ToString();
        //member.Member_Profile_Contact = Member_Profile_Contact;
        //member.Member_SupplierID =0;

        #region  注册会员时,同时注册商家
        int Supplier_DefaultGrade = 0;
        SupplierGradeInfo supplier_grade = supplier.GetSupplierDefaultGrade();
        if (supplier_grade != null)
        {
            Supplier_DefaultGrade = supplier_grade.Supplier_Grade_ID;
        }


        SupplierInfo supplier_entity = new SupplierInfo();

        supplier_entity.Supplier_ID = 0;
        supplier_entity.Supplier_GradeID = Supplier_DefaultGrade;
        //supplier_entity.Supplier_Type = supplier_Type;
        string supplier_email = member_email;
        supplier_entity.Supplier_Email = supplier_email;
        string supplier_password = member_password;
        supplier_entity.Supplier_Password = encrypt.MD5(supplier_password);

        string Supplier_CompanyName = member_company;
        supplier_entity.Supplier_CompanyName = Supplier_CompanyName;

        string supplier_county = member_county;
        supplier_entity.Supplier_County = supplier_county;

        string supplier_city = member_city;
        supplier_entity.Supplier_City = supplier_city;

        string supplier_state = member_state;
        supplier_entity.Supplier_State = supplier_state;
        supplier_entity.Supplier_Country = "CN";

        string supplier_address = member_address;
        supplier_entity.Supplier_Address = supplier_address;
        supplier_entity.Supplier_Phone = "";
        supplier_entity.Supplier_Fax = "";
        supplier_entity.Supplier_Zip = "";

        string Supplier_Contactman = member_realname;
        supplier_entity.Supplier_Contactman = Supplier_Contactman;

        string Supplier_Mobile = member_mobile;
        supplier_entity.Supplier_Mobile = Supplier_Mobile;

        supplier_entity.Supplier_IsHaveShop = 0;
        supplier_entity.Supplier_IsApply = 0;
        supplier_entity.Supplier_ShopType = 0;
        supplier_entity.Supplier_Mode = 0;
        supplier_entity.Supplier_DeliveryMode = 1;
        supplier_entity.Supplier_Account = 0;
        supplier_entity.Supplier_Adv_Account = 0;
        supplier_entity.Supplier_Security_Account = 0;
        supplier_entity.Supplier_CreditLimit = 0;
        supplier_entity.Supplier_CreditLimitRemain = 0;
        supplier_entity.Supplier_CreditLimit_Expires = 0;
        supplier_entity.Supplier_TempCreditLimit = 0;
        supplier_entity.Supplier_TempCreditLimitRemain = 0;
        supplier_entity.Supplier_TempCreditLimit_ContractSN = "";
        supplier_entity.Supplier_TempCreditLimit_Expires = 0;
        supplier_entity.Supplier_Status = 1;
        //supplier_entity.Supplier_AuditStatus = -1;
        supplier_entity.Supplier_AuditStatus = 0;
        supplier_entity.Supplier_Cert_Status = 0;
        //supplier_entity.Supplier_CertType = supplier_Type;
        supplier_entity.Supplier_LoginCount = 1;
        supplier_entity.Supplier_LoginIP = tools.NullStr(Request.ServerVariables["remote_addr"]);
        supplier_entity.Supplier_Addtime = DateTime.Now;
        supplier_entity.Supplier_Lastlogintime = DateTime.Now;
        supplier_entity.Supplier_VerifyCode = pub.Createvkey();
        supplier_entity.Supplier_AllowOrderEmail = 1;
        supplier_entity.Supplier_Trash = 0;
        supplier_entity.Supplier_RegIP = tools.NullStr(Request.ServerVariables["remote_addr"]);

        supplier_entity.Supplier_Site = pub.GetCurrentSite();

        string supplier_nickname = member_realname;

        supplier_entity.Supplier_Nickname = supplier_nickname;
        supplier_entity.Supplier_SysEmail = supplier_email;
        supplier_entity.Supplier_SysMobile = Supplier_Mobile;
        supplier_entity.Supplier_AllowSysEmail = 0;
        supplier_entity.Supplier_AllowSysMessage = 0;
        supplier_entity.Supplier_AgentRate = 0;



        Session["member_phone_register"] = member_mobile;
        //添加商家基本信息
        if (MySupplier.AddSupplier(supplier_entity, pub.CreateUserPrivilege("6834de44-d231-42bc-a89f-3b0e4461fcc1")))
        {
            supplier_entity = null;

            //SupplierInfo supplierinfo = supplier.GetSupplierByEmail(supplier_email);

            ISQLHelper DBHelper = SQLHelperFactory.CreateSQLHelper();
            //int supplier_id = tools.NullInt(DBHelper.ExecuteScalar("select Supplier_ID from Supplier where Supplier_Nickname='"+member_nickname+'""));

            //MemberInfo entitys = MyMember.GetMemberByNickName(member_realname, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));

            SupplierInfo supplierinfo = null;

            QueryInfo Query = new QueryInfo();
            Query.PageSize = 1;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Nickname", "=", member_realname));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", "CN"));
            Query.OrderInfos.Add(new OrderInfo("SupplierInfo.Supplier_ID", "Desc"));

            IList<SupplierInfo> supplierinfo1 = MySupplier.GetSuppliers(Query, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
            if (supplierinfo1 != null)
            {
                int i = 0;
                foreach (var suppinfo in supplierinfo1)
                {
                    i++;
                    supplierinfo = supplier.GetSupplierByID(suppinfo.Supplier_ID);
                }

            }



            // SupplierInfo supplierinfo = supplier.GetSupplierByID(member.Member_SupplierID);
            if (supplierinfo != null)
            {
                member.Member_SupplierID = supplierinfo.Supplier_ID;
            }



            // SupplierInfo supplierinfo = supplier.GetSupplierByID(supplier_email);
            if (supplierinfo != null)
            {
                Session["supplier_id"] = supplierinfo.Supplier_ID;
                Session["supplier_email"] = supplierinfo.Supplier_Email;
                Session["supplier_companyname"] = supplierinfo.Supplier_CompanyName;
                Session["supplier_logined"] = "True";
                Session["supplier_logincount"] = supplierinfo.Supplier_LoginCount + 1;
                Session["supplier_lastlogin_time"] = supplierinfo.Supplier_Lastlogintime;
                Session["supplier_ishaveshop"] = supplierinfo.Supplier_IsHaveShop;
                Session["Supplier_Isapply"] = supplierinfo.Supplier_IsApply;
                Session["supplier_nickname"] = supplierinfo.Supplier_Nickname;
                Session["supplier_auditstatus"] = supplierinfo.Supplier_AuditStatus;
                //Session["supplier_logined"] = "True";
                #region 调用信贷系统创建会员接口创建会员
                //MemberJsonInfo JsonInfo =supplier. Create_Enterprise_Member(supplierinfo);
                //if (JsonInfo != null && JsonInfo.Is_success == "T")
                //{
                //    supplierinfo.Supplier_VfinanceID = JsonInfo.Member_id;
                //    MySupplier.EditSupplier(supplierinfo, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f"));
                //}
                #endregion

                //发送验证邮件
                supplier.supplier_register_sendemailverify(supplierinfo.Supplier_Email, supplierinfo.Supplier_VerifyCode);

                //Response.Redirect("/supplier/account_profile.aspx");

                //pub.Msg("positive", "注册成功", "注册完成！请完善您的信息！", true, "/supplier/account_profile.aspx");
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
        #endregion



        //添加会员基本信息
        if (MyMember.AddMember(member, pub.CreateUserPrivilege("5d071ec6-31d8-4960-a77d-f8209bbab496")))
        {
            //MemberInfo memberinfo = GetMemberInfoByEmail(member_email);

            MemberInfo memberinfo = MyMember.GetMemberByNickName(member_realname, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));



            if (memberinfo != null)
            {
                #region 添加详细信息
                //添加详细信息
                MemberProfileInfo profile = new MemberProfileInfo();

                profile.Member_Profile_MemberID = memberinfo.Member_ID;
                //profile.Member_Name = member_realname;
                profile.Member_Sex = -1;
                profile.Member_Birthday = DateTime.Now;
                profile.Member_StreetAddress = member_address;
                profile.Member_County = member_county;
                profile.Member_City = member_city;
                profile.Member_State = member_state;
                profile.Member_Country = "CN";
                profile.Member_Zip = "";
                profile.Member_Phone_Countrycode = "+86";
                profile.Member_Phone_Areacode = "";
                profile.Member_Phone_Number = "";
                profile.Member_Mobile = member_mobile;
                profile.Member_Phone_Number = Member_Phone_Number;
                profile.Member_Company = member_company;
                profile.Member_Fax = "";
                profile.Member_QQ = "";
                profile.Member_OrganizationCode = "";
                profile.Member_BusinessCode = "";
                //profile.Member_Name = Member_Profile_Contact;
                profile.Member_RealName = Member_Profile_Contact;

                MyMember.AddMemberProfile(profile, pub.CreateUserPrivilege("5d071ec6-31d8-4960-a77d-f8209bbab496"));


                SupplierInfo supplierinfo = null;

                QueryInfo Query = new QueryInfo();
                Query.PageSize = 1;
                Query.CurrentPage = 1;
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Nickname", "=", member_realname));
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", "CN"));
                Query.OrderInfos.Add(new OrderInfo("SupplierInfo.Supplier_ID", "Desc"));

                IList<SupplierInfo> supplierinfo11 = MySupplier.GetSuppliers(Query, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));

                int Supplier_id = 0; int i1 = 0;
                foreach (var supplierinfo1 in supplierinfo11)
                {

                    i1++;
                    if (i1 == 1)
                    {
                        if (supplierinfo11 != null)
                        {
                            Supplier_id = supplierinfo1.Supplier_ID;
                        };
                    }
                }


                Member_Log(memberinfo.Member_ID, "采购商注册");
                #endregion

                #region 调用信贷系统创建会员接口创建会员

                //MemberJsonInfo JsonInfo = Create_Enterprise_Member(memberinfo);
                //if (JsonInfo != null && JsonInfo.Is_success == "T")
                //{
                //    memberinfo.Member_VfinanceID = JsonInfo.Member_id;
                //    MyMember.EditMember(memberinfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));
                //}
                #endregion


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

                member_register_sendemailverify(memberinfo.Member_Email, memberinfo.Member_VerifyCode);

                //if (memberinfo.Member_Emailverify == 0 && memberinfo.Member_AuditStatus == 1)
                //{
                //    Session["member_emailverify"] = memberinfo.Member_Emailverify;
                //    //Response.Redirect("/member/emailverify.aspx");
                //}
                //else
                //{
                Response.Redirect("/member/account_profile.aspx");
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





    /// <summary>
    /// 自动登录
    /// </summary>
    public void MemberAutoLogin()
    {
        HttpCookie cookie = Request.Cookies["autologin"];
        if (cookie == null)
            return;

        try
        {
            string loginname = tools.NullStr(Server.UrlDecode(cookie.Values["loginname"]));
            string password = tools.NullStr(Server.UrlDecode(cookie.Values["password"]));

            if (loginname.Length == 0 || password.Length == 0)
            {
                return;
            }

            MemberInfo memberinfo = MyMember.GetMemberByLogin(tools.CheckStr(loginname), tools.CheckStr(password), pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
            if (memberinfo != null)
            {
                //设置登录
                SetMemberLogin(memberinfo);
            }
        }
        catch (Exception ex)
        {

        }
    }


    public void SetMemberLogin(MemberInfo memberinfo)
    {
        #region 登录赋值

        Session["member_id"] = memberinfo.Member_ID;
        Session["member_email"] = memberinfo.Member_Email;
        Session["member_nickname"] = memberinfo.Member_NickName;
        //if (MemberIsLogin==1)
        //{
        Session["member_logined"] = "True";
        //}
        //else
        //{
        //    Session["member_logined"] = "False";
        //}


        Session["supplier_logined"] = "True";
        Session["member_mobile"] = memberinfo.Member_LoginMobile;
        Session["member_logincount"] = memberinfo.Member_LoginCount + 1;
        Session["member_lastlogin_time"] = memberinfo.Member_LastLogin_Time;
        Session["member_lastlogin_ip"] = memberinfo.Member_LastLogin_IP;
        Session["member_coinremain"] = memberinfo.Member_CoinRemain;
        Session["member_coincount"] = memberinfo.Member_CoinCount;
        Session["member_grade"] = memberinfo.Member_Grade;
        Session["Member_AllowSysEmail"] = memberinfo.Member_AllowSysEmail;
        Session["Member_VfinanceID"] = memberinfo.Member_VfinanceID;
        Session["Member_ERP_StoreID"] = memberinfo.Member_ERP_StoreID;
        Session["Member_Company"] = memberinfo.MemberProfileInfo.Member_Company;
        //Session["member_auditstatus"] = memberinfo.Member_AuditStatus;
        Session["member_auditstatus"] = 1;

        if (MyToken.CheckMemberToken(memberinfo.Member_NickName, memberinfo.Member_Password, 1, tokentime))
        {
            Session["member_token"] = MyToken.GetMemberToken(memberinfo.Member_NickName, memberinfo.Member_Password, 1, tokentime);
        }
        else
        {
            string token = Guid.NewGuid().ToString();
            MemberTokenInfo tokenInfo = new MemberTokenInfo();
            tokenInfo.Token = token;
            tokenInfo.Type = 1;
            tokenInfo.MemberID = memberinfo.Member_ID;
            tokenInfo.IP = Request.ServerVariables["Remote_Addr"];
            tokenInfo.CreateTime = DateTime.Now;
            tokenInfo.UpdateTime = DateTime.Now;
            tokenInfo.ExpiredTime = DateTime.Now.AddMinutes(tokentime);
            MyToken.AddMemberToken(tokenInfo);

            Session["member_token"] = token;
        }


        //更新购物车价格
        Cart_Price_Update();

        //更新用户登录信息
        MyMember.UpdateMemberLogin(memberinfo.Member_ID, memberinfo.Member_LoginCount + 1, Request.ServerVariables["Remote_Addr"], pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));

        //更新会员等级
        Update_MemberGrade();

        Member_Log(memberinfo.Member_ID, "采购商登录");

        #endregion
    }



    //public void Member_Login()
    //{

    //    int autologin = tools.CheckInt(Request.Form["autologin"]);
    //    string member_name = tools.CheckStr(tools.NullStr(Request["Member_UserName"]).Trim());
    //    string member_password = tools.CheckStr(tools.NullStr(Request["Member_Password"]).Trim());
    //    member_password = encrypt.MD5(member_password);
    //    string verifycode = tools.CheckStr(tools.NullStr(Request["verifycode"]).Trim()).ToLower();

    //    bool AccountIsExist = false;

    public void Member_Login()
    {

        int autologin = tools.CheckInt(Request.Form["autologin"]);
        string member_name = tools.CheckStr(tools.NullStr(Request.Form["Member_UserName"]).Trim());
        string member_password = tools.CheckStr(tools.NullStr(Request.Form["Member_Password"]).Trim());
        member_password = encrypt.MD5(member_password);
        string verifycode = tools.CheckStr(tools.NullStr(Request.Form["verifycode"]).Trim()).ToLower();
        bool AccountIsExist = false;

        //Session["member_phone"] = member_name;
        Session["member_nickname"] = member_name;
        string Supplier_CompanyName = "";
        if (member_name == "")
        {
            Session["logintype"] = "False";
            Response.Redirect("/login.aspx?login=umsg_k&u_type=0");
            Response.End();
        }


        if (verifycode != Session["Trade_Verify"].ToString() || verifycode.Length == 0)
        {
            Session["logintype"] = "False";
            Response.Redirect("/login.aspx?login=vmsg&u_type=0");
            Response.End();
        }

        //查询 会员表 判断member_name信息,若存在说明存在该会员
        //主账号 账号是否存在
        bool IsExist = MyMember.GetMemberAccountByLogin(member_name, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));

        //子账号 账号是否存在
        bool SubIsExist = false;
        if ((tools.CheckInt(DBHelper.ExecuteScalar("select COUNT(*) from Supplier_SubAccount where Supplier_SubAccount_Name='" + member_name + "'").ToString())) > 0)
        {
            SubIsExist = true;
        }
        else
        {
            SubIsExist = false;
        }

        if ((IsExist == true) || (SubIsExist == true))
        {
            AccountIsExist = true;
        }
        else
        {
            AccountIsExist = false;
        }




        MemberInfo memberinfo1 = MyMember.GetMemberByLogin(member_name, member_password, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));


        SupplierInfo Supplierinfo = null;
        SupplierSubAccountInfo subaccountinfo = null;
        if (memberinfo1 == null)
        {
            //登录子账户
            subaccountinfo = MySupplierSubAccount.SubAccountLogin(member_name);
            if (subaccountinfo != null)
            {
                Supplierinfo = MySupplier.GetSupplierByID(subaccountinfo.Supplier_SubAccount_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                if (Supplierinfo != null)
                {
                    Supplier_CompanyName = Supplierinfo.Supplier_CompanyName;
                }
            }

        }
        else
        {
            Supplierinfo = MySupplier.GetSupplierByID(memberinfo1.Member_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
            if (Supplierinfo != null)
            {
                Supplier_CompanyName = Supplierinfo.Supplier_CompanyName;
            }
        }



        if ((Supplierinfo != null) || (memberinfo1 != null))
        {
            //登录子账户
            if (subaccountinfo != null)
            {
                if (subaccountinfo.Supplier_SubAccount_Password != member_password)
                {
                    Session["logintype"] = "False";
                    Response.Redirect("/login.aspx?u_type=1&login=pmsg_sub");
                    Response.End();
                }

                //子账户
                if (subaccountinfo.Supplier_SubAccount_IsActive != 1)
                {
                    Response.Redirect("/login.aspx?u_type=1&login=err_active_sub");
                }
                if (subaccountinfo.Supplier_SubAccount_ExpireTime.AddDays(1) < DateTime.Now)
                {
                    Response.Redirect("/login.aspx?u_type=1&login=err_time_sub");
                }
            }
            else
            {
                MemberInfo memberinfo = MyMember.GetMemberByLogin(member_name, member_password, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                if (memberinfo.Member_Password != member_password)
                {
                    Session["logintype"] = "False";
                    Response.Redirect("/login.aspx?u_type=1&login=pmsg_sub");
                    Response.End();
                }

                //主账户
                if (Supplierinfo.Supplier_Trash == 1)
                {
                    //Response.Redirect("/login.aspx?u_type=1&login=err_active_sub");
                    Response.Redirect("/login.aspx?u_type=1&login=err_check");
                }
                if (Supplierinfo.Supplier_Status != 1)
                {
                    Response.Redirect("/login.aspx?u_type=1&login=err_active");
                }
            }

            #region 30天免登陆

            if (autologin == 1)
            {
                HttpCookie cookie = new HttpCookie("autologin");
                cookie.Expires = DateTime.Today.AddDays(30);
                cookie.Values.Add("loginname", Server.UrlEncode(member_name));
                cookie.Values.Add("password", Server.UrlEncode(member_password));
                Response.Cookies.Add(cookie);
            }

            #endregion

            #region 主OR附属账号 登录

            if ((subaccountinfo != null) && (SubIsExist))
            {//登录子账号账户
                int member_id = tools.CheckInt(DBHelper.ExecuteScalar("select Member_ID from Member where Member_SupplierID =(select Supplier_SubAccount_SupplierID from Supplier_SubAccount where Supplier_SubAccount_Name='" + member_name + "')").ToString());
                MemberInfo memberinfo = MyMember.GetMemberByID(member_id, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                //SupplierInfo supplierinfo1 = null;
                if (memberinfo != null)
                {
                    //supplierinfo1 = MySupplier.GetSupplierByID(memberinfo.Member_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                    Session["supplier_id"] = Supplierinfo.Supplier_ID;
                }
                //设置登录
                if (memberinfo != null)
                {
                    SetMemberLogin(memberinfo);
                }
                else
                {
                    Response.Redirect("/login.aspx?u_type=1&login=err_active");
                }

                Session["subPrivilege"] = subaccountinfo.Supplier_SubAccount_Privilege;
                Session["IsSubPrivilege_Logined"] = "True";
                Session["Logistics_Logined"] = "False";
                //int subsupplier_nickname = tools.CheckInt(DBHelper.ExecuteScalar("select Member_ID from Member where Member_SupplierID =(select Supplier_SubAccount_SupplierID from Supplier_SubAccount where Supplier_SubAccount_Name='" + member_name + "')").ToString());
                Session["member_nickname"] = member_name;
                string Member_Permissions = Session["subPrivilege"].ToString();
                List<string> oTempList = new List<string>(Member_Permissions.Split(','));
                if (oTempList.Contains("1") || oTempList.Contains("6"))
                {
                    Session["member_logined"] = "True";
                    Session["member_buyenable"] = "True";

                }
                else if (oTempList.Contains("3") || oTempList.Contains("4"))
                {
                    Session["member_logined"] = "True";
                    if (oTempList.Contains("1"))
                    {
                        Session["member_buyenable"] = "True";
                    }
                    else
                    {
                        Session["member_buyenable"] = "False";
                    }


                }
                else
                {
                    Session["member_logined"] = "False";
                    Session["member_buyenable"] = "False";
                }


                Session["supplier_sublogined"] = "True";
                Session["member_nicknames"] = member_name;
                Session["member_nicknamess"] = Supplier_CompanyName;
                Session["supplier_ishaveshop"] = Supplierinfo.Supplier_IsHaveShop;

                //会员登录对应商家店铺状态
                Session["supplier_auditstatus"] = Supplierinfo.Supplier_AuditStatus;
                // 判断是否经过了邮件验证
                if (memberinfo.Member_Emailverify == 0)
                {
                    Session["supplier_auditstatus"] = tools.NullInt(Supplierinfo.Supplier_AuditStatus);
                    //if (tools.NullInt(Session["supplier_auditstatus"]) == 1)
                    if (tools.NullInt(Supplierinfo.Supplier_AuditStatus) == 1)
                    {
                        Session["member_emailverify"] = memberinfo.Member_Emailverify;

                        if (Session["url_after_login"] == null)
                        {
                            Session["url_after_login"] = "";
                        }
                        if (tools.NullStr(Session["url_after_login"].ToString()) != "")
                        {
                            if (tools.NullStr(Session["url_after_login"].ToString()).Contains("/index.aspx"))
                            {
                                Response.Redirect("/index.aspx");
                            }
                            else
                            {
                                Response.Redirect(Session["url_after_login"].ToString());
                            }


                        }
                        else
                        {
                            Session["m_error_count"] = 0;
                            Response.Redirect("/supplier/index.aspx");
                        }
                    }
                    else
                    {
                        //Response.Redirect("/supplier/account_profile.aspx");
                        Response.Redirect("/supplier/index.aspx");
                    }
                }
                else
                {
                    if (Session["url_after_login"] == null)
                    {
                        Session["url_after_login"] = "";
                    }
                    if (tools.NullStr(Session["url_after_login"].ToString()) != "")
                    {
                        //Response.Redirect(Session["url_after_login"].ToString());
                        if (tools.NullStr(Session["url_after_login"].ToString()).Contains("/index.aspx"))
                        {
                            Response.Redirect("/index.aspx");
                        }
                        else
                        {
                            Response.Redirect(Session["url_after_login"].ToString());
                        }
                    }
                    else
                    {
                        Session["m_error_count"] = 0;
                        //Response.Redirect("/member/index.aspx");
                        Response.Redirect("/supplier/index.aspx");
                    }
                }
                //更新商家登录信息
                //MySupplier.UpdateSupplierLogin(Supplierinfo.Supplier_ID, Supplierinfo.Supplier_LoginCount + 1, Request.ServerVariables["Remote_Addr"], pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f"));
                //更新购物车价格
                Cart_Price_Update();

            }
            else if ((subaccountinfo == null) && (IsExist))//主账号账户
            {
                //登录主账号账户
                MemberInfo memberinfo = MyMember.GetMemberByLogin(member_name, member_password, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                SupplierInfo supplierinfo = null;

                if (memberinfo != null)
                {
                    supplierinfo = MySupplier.GetSupplierByID(memberinfo.Member_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                    if (supplierinfo != null)
                    {
                        Supplier_CompanyName = supplierinfo.Supplier_CompanyName;
                        Session["supplier_id"] = supplierinfo.Supplier_ID;
                    }
                }

                //设置登录
                SetMemberLogin(memberinfo);

                Session["member_logined"] = "True";
                Session["Logistics_Logined"] = "False";

                Session["member_nicknames"] = Supplier_CompanyName;
                Session["member_nicknamess"] = Supplier_CompanyName;
                Session["member_buyenable"] = "True";

                Session["supplier_auditstatus"] = Supplierinfo.Supplier_AuditStatus;
                Session["supplier_logincount"] = Supplierinfo.Supplier_LoginCount + 1;
                Session["supplier_lastlogin_time"] = Supplierinfo.Supplier_Lastlogintime;
                //会员登录对应商家店铺状态
                Session["supplier_ishaveshop"] = Supplierinfo.Supplier_IsHaveShop;
                //更新商家登录信息
                MySupplier.UpdateSupplierLogin(Supplierinfo.Supplier_ID, Supplierinfo.Supplier_LoginCount + 1, Request.ServerVariables["Remote_Addr"], pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f"));
                //更新购物车价格
                Cart_Price_Update();
                // 判断是否经过了邮件验证
                if (memberinfo.Member_Emailverify == 0)
                {
                    //if (tools.NullInt(Session["supplier_auditstatus"]) == 1)
                    if (tools.NullInt(Supplierinfo.Supplier_AuditStatus) == 1)
                    {
                        Session["member_emailverify"] = memberinfo.Member_Emailverify;

                        if (Session["url_after_login"] == null)
                        {
                            Session["url_after_login"] = "";
                        }
                        if (tools.NullStr(Session["url_after_login"].ToString()) != "")
                        {
                            if (tools.NullStr(Session["url_after_login"].ToString()).Contains("/index.aspx"))
                            {
                                Response.Redirect("/index.aspx");
                            }
                            else
                            {
                                Response.Redirect(Session["url_after_login"].ToString());
                            }


                        }
                        else
                        {
                            Session["m_error_count"] = 0;
                            //Response.Redirect("/member/index.aspx");
                            Response.Redirect("/index.aspx");
                        }
                    }
                    else
                    {
                        Response.Redirect("/member/account_profile.aspx");
                    }


                    //更新会员等级
                    //Update_SupplierGrade();
                }
                else
                {
                    if (Session["url_after_login"] == null)
                    {
                        Session["url_after_login"] = "";
                    }
                    if (tools.NullStr(Session["url_after_login"].ToString()) != "")
                    {
                        //Response.Redirect(Session["url_after_login"].ToString());
                        if (tools.NullStr(Session["url_after_login"].ToString()).Contains("/index.aspx"))
                        {
                            Response.Redirect("/index.aspx");
                        }
                        else
                        {
                            Response.Redirect(Session["url_after_login"].ToString());
                        }
                    }
                    else
                    {
                        Session["m_error_count"] = 0;
                        //Response.Redirect("/member/index.aspx");
                        Response.Redirect("/index.aspx");
                    }
                }
                //}
            }
            else if (AccountIsExist == true)
            {
                Session["logintype"] = "False";
                Response.Redirect("/login.aspx?login=pmsg&u_type=0");
                Response.End();

            }
            else
            {
                Session["logintype"] = "False";
                Response.Redirect("/login.aspx?login=err_active1&u_type=0");
                Response.End();
            }
            #endregion
        }
        //else
        //{
        //    Response.Redirect("/login.aspx?login=err_active&u_type=0");
        //    Response.End();
        //}
        else
        {
            if (AccountIsExist)
            {
                Session["logintype"] = "False";
                Response.Redirect("/login.aspx?login=pmsg&u_type=0");
                Response.End();
            }
            else
            {
                Session["logintype"] = "False";
                Response.Redirect("/login.aspx?u_type=1&login=umsg_w");
                Response.End();
            }

        }
    }


    //首页登录
    public void Member_IndexLogin()
    {

        int autologin = tools.CheckInt(Request.Form["autologin"]);
        string member_name = tools.CheckStr(tools.NullStr(Request.Form["Member_UserName"]).Trim());
        string member_password = tools.CheckStr(tools.NullStr(Request.Form["Member_Password"]).Trim());
        member_password = encrypt.MD5(member_password);
        string verifycode = tools.CheckStr(tools.NullStr(Request.Form["verifycode"]).Trim()).ToLower();
        bool AccountIsExist = false;

        Session["member_phone"] = member_name;
        Session["member_nickname"] = member_name;
        if (member_name == "")
        {
            Session["logintype"] = "False";
            Response.Redirect("/login.aspx?login=umsg_k&u_type=0");
            Response.End();
        }


        if (verifycode != Session["Trade_Verify"].ToString() || verifycode.Length == 0)
        {
            Session["logintype"] = "False";
            Response.Redirect("/login.aspx?login=vmsg&u_type=0");
            Response.End();
        }

        //查询 会员表 判断member_name信息,若存在说明存在该会员
        bool IsExist = MyMember.GetMemberAccountByLogin(member_name, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        if (IsExist == true)
        {
            AccountIsExist = true;
        }
        else
        {
            AccountIsExist = false;
        }




        MemberInfo memberinfo = MyMember.GetMemberByLogin(member_name, member_password, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));




        if (memberinfo != null)
        {
            //if (memberinfo.Member_Password != member_password)
            //{
            //    Session["logintype"] = "False";
            //    Response.Redirect("/login.aspx?login=pmsg&u_type=0");
            //    Response.End();
            //}

            #region 30天免登陆

            if (autologin == 1)
            {
                HttpCookie cookie = new HttpCookie("autologin");
                cookie.Expires = DateTime.Today.AddDays(30);
                cookie.Values.Add("loginname", Server.UrlEncode(member_name));
                cookie.Values.Add("password", Server.UrlEncode(member_password));
                Response.Cookies.Add(cookie);
            }

            #endregion






            //设置登录
            SetMemberLogin(memberinfo);

            Session["Logistics_Logined"] = "False";

            SupplierInfo Supplierinfo = MySupplier.GetSupplierByID(memberinfo.Member_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
            if (Supplierinfo != null)
            {

                #region 登录会员同时登录登录商家,将商家信息存在session里
                Session["supplier_id"] = Supplierinfo.Supplier_ID;
                Session["supplier_type"] = Supplierinfo.Supplier_Type;
                Session["supplier_email"] = Supplierinfo.Supplier_Email;
                Session["supplier_mobile"] = Supplierinfo.Supplier_Mobile;
                Session["supplier_companyname"] = Supplierinfo.Supplier_CompanyName;
                Session["supplier_logined"] = "True";
                Session["supplier_logincount"] = Supplierinfo.Supplier_LoginCount + 1;
                Session["supplier_lastlogin_time"] = Supplierinfo.Supplier_Lastlogintime;
                Session["supplier_ishaveshop"] = Supplierinfo.Supplier_IsHaveShop;
                Session["Supplier_Isapply"] = Supplierinfo.Supplier_IsApply;
                Session["supplier_nickname"] = Supplierinfo.Supplier_Nickname;
                Session["supplier_auditstatus"] = Supplierinfo.Supplier_AuditStatus;

                if (MyToken.CheckMemberToken(Supplierinfo.Supplier_Nickname, Supplierinfo.Supplier_Password, 2, tokentime))
                {
                    Session["supplier_token"] = MyToken.GetMemberToken(Supplierinfo.Supplier_Nickname, Supplierinfo.Supplier_Password, 2, tokentime);
                }
                else
                {
                    string token = Guid.NewGuid().ToString();
                    MemberTokenInfo tokenInfo = new MemberTokenInfo();
                    tokenInfo.Token = token;
                    tokenInfo.Type = 2;
                    tokenInfo.MemberID = Supplierinfo.Supplier_ID;
                    tokenInfo.IP = Request.ServerVariables["Remote_Addr"];
                    tokenInfo.CreateTime = DateTime.Now;
                    tokenInfo.UpdateTime = DateTime.Now;
                    tokenInfo.ExpiredTime = DateTime.Now.AddMinutes(tokentime);
                    MyToken.AddMemberToken(tokenInfo);

                    Session["supplier_token"] = token;
                }

                if (Supplierinfo.Supplier_IsHaveShop == 1)
                {
                    SupplierShopInfo shopinfo = MyShop.GetSupplierShopBySupplierID(Supplierinfo.Supplier_ID);
                    if (shopinfo != null)
                    {
                        Session["supplier_shopid"] = shopinfo.Shop_ID;
                        Session["supplier_shoptype"] = shopinfo.Shop_Type;
                        if (shopinfo.Shop_Status == 0)
                        {

                            Session["supplier_ishaveshop"] = 0;
                        }
                    }
                }

                //更新商家登录信息
                MySupplier.UpdateSupplierLogin(Supplierinfo.Supplier_ID, Supplierinfo.Supplier_LoginCount + 1, Request.ServerVariables["Remote_Addr"], pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f"));
            }

            //更新会员等级
            supplier.Update_SupplierGrade();

            #endregion



            // 判断是否经过了邮件验证
            if (memberinfo.Member_Emailverify == 0)
            {
                if (tools.NullInt(Session["supplier_auditstatus"]) == 1)
                {
                    Session["member_emailverify"] = memberinfo.Member_Emailverify;

                    if (Session["url_after_login"].ToString().Length == 0)
                    {
                        Session["url_after_login"] = "";
                        Response.Redirect("/index.aspx");
                    }
                    if (tools.NullStr(Session["url_after_login"].ToString()) != "")
                    {
                        if (tools.NullStr(Session["url_after_login"].ToString()).Contains("/index.aspx"))
                        {
                            Response.Redirect("/index.aspx");
                        }
                        else
                        {
                            Response.Redirect(Session["url_after_login"].ToString());
                        }


                    }
                    else
                    {
                        Session["m_error_count"] = 0;
                        Response.Redirect("/member/index.aspx");
                    }
                }
                else
                {
                    Response.Redirect("/member/account_profile.aspx");
                }
            }
            else
            {
                if (Session["url_after_login"].ToString().Length == 0)
                {
                    Session["url_after_login"] = "";
                    Response.Redirect("/index.aspx");
                }
                if (tools.NullStr(Session["url_after_login"].ToString()) != "")
                {
                    //Response.Redirect(Session["url_after_login"].ToString());
                    if (tools.NullStr(Session["url_after_login"].ToString()).Contains("/index.aspx"))
                    {
                        Response.Redirect("/index.aspx");
                    }
                    else
                    {
                        Response.Redirect(Session["url_after_login"].ToString());
                    }
                }
                else
                {
                    Session["m_error_count"] = 0;
                    Response.Redirect("/member/index.aspx");
                }
            }
        }












        else if (AccountIsExist == true)
        {
            Session["logintype"] = "False";
            Response.Redirect("/login.aspx?login=err_active1&u_type=0");



            Response.End();
        }
        else
        {
            Session["logintype"] = "False";
            Response.Redirect("/login.aspx?login=err_active&u_type=0");
            Response.End();
        }
    }


    public void Fast_MemberLogin()
    {
        string member_name = tools.CheckStr(tools.NullStr(Request["Member_UserName"]).Trim());
        string member_password = tools.CheckStr(tools.NullStr(Request["Member_Password"]).Trim());
        member_password = encrypt.MD5(member_password);

        if (member_name == "")
        {
            Session["logintype"] = "False";
            Response.Redirect("/login.aspx?login=umsg_k");
            Response.End();
        }


        MemberInfo memberinfo = MyMember.GetMemberByLogin(member_name, member_password, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        if (memberinfo != null)
        {
            if (memberinfo.Member_Password != member_password)
            {
                Session["logintype"] = "False";

                Response.Redirect("/login.aspx?login=pmsg");
                Response.End();
            }

            //设置登录
            SetMemberLogin(memberinfo);

            if (MyToken.CheckMemberToken(memberinfo.Member_NickName, memberinfo.Member_Password, 1, tokentime))
            {
                Session["member_token"] = MyToken.GetMemberToken(memberinfo.Member_NickName, memberinfo.Member_Password, 1, tokentime);
            }
            else
            {
                string token = Guid.NewGuid().ToString();
                MemberTokenInfo tokenInfo = new MemberTokenInfo();
                tokenInfo.Token = token;
                tokenInfo.Type = 1;
                tokenInfo.MemberID = memberinfo.Member_ID;
                tokenInfo.IP = Request.ServerVariables["Remote_Addr"];
                tokenInfo.CreateTime = DateTime.Now;
                tokenInfo.UpdateTime = DateTime.Now;
                tokenInfo.ExpiredTime = DateTime.Now.AddMinutes(tokentime);
                MyToken.AddMemberToken(tokenInfo);

                Session["member_token"] = token;
            }

            //更新购物车价格
            Cart_Price_Update();

            //更新用户登录信息
            MyMember.UpdateMemberLogin(memberinfo.Member_ID, memberinfo.Member_LoginCount + 1, Request.ServerVariables["Remote_Addr"], pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));

            //更新会员等级
            Update_MemberGrade();

            Member_Log(memberinfo.Member_ID, "采购商登录");

            // 判断是否经过了邮件验证
            if (memberinfo.Member_Emailverify == 0)
            {
                Session["member_emailverify"] = memberinfo.Member_Emailverify;
                Response.Write("/member/emailverify.aspx");
                Response.End();
            }
            else
            {
                if (Session["url_after_login"] == null)
                {
                    Session["url_after_login"] = "";
                }
                if (tools.NullStr(Session["url_after_login"].ToString()) != "")
                {
                    Response.Write(Session["url_after_login"].ToString());
                }
                else
                {
                    Response.Write("/member/index.aspx");
                }
            }
        }
        else
        {
            Session["logintype"] = "False";
            Response.Write("Failure");
            Response.End();
        }
    }

    //public void Fast_MemberLogin()
    //{
    //    string member_name = tools.CheckStr(tools.NullStr(Request["Member_UserName"]).Trim());
    //    string member_password = tools.CheckStr(tools.NullStr(Request["Member_Password"]).Trim());
    //    member_password = encrypt.MD5(member_password);

    //    if (member_name == "")
    //    {
    //        Session["logintype"] = "False";
    //        Response.Redirect("/login.aspx?login=umsg_k");
    //        Response.End();
    //    }


    //    MemberInfo memberinfo = MyMember.GetMemberByLogin(member_name, member_password, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
    //    if (memberinfo != null)
    //    {
    //        if (memberinfo.Member_Password != member_password)
    //        {
    //            Session["logintype"] = "False";

    //            Response.Redirect("/login.aspx?login=pmsg");
    //            Response.End();
    //        }

    //        //设置登录
    //        SetMemberLogin(memberinfo);

    //        if (MyToken.CheckMemberToken(memberinfo.Member_NickName, memberinfo.Member_Password, 1, tokentime))
    //        {
    //            Session["member_token"] = MyToken.GetMemberToken(memberinfo.Member_NickName, memberinfo.Member_Password, 1, tokentime);
    //        }
    //        else
    //        {
    //            string token = Guid.NewGuid().ToString();
    //            MemberTokenInfo tokenInfo = new MemberTokenInfo();
    //            tokenInfo.Token = token;
    //            tokenInfo.Type = 1;
    //            tokenInfo.MemberID = memberinfo.Member_ID;
    //            tokenInfo.IP = Request.ServerVariables["Remote_Addr"];
    //            tokenInfo.CreateTime = DateTime.Now;
    //            tokenInfo.UpdateTime = DateTime.Now;
    //            tokenInfo.ExpiredTime = DateTime.Now.AddMinutes(tokentime);
    //            MyToken.AddMemberToken(tokenInfo);

    //            Session["member_token"] = token;
    //        }

    //        //更新购物车价格
    //        Cart_Price_Update();

    //        //更新用户登录信息
    //        MyMember.UpdateMemberLogin(memberinfo.Member_ID, memberinfo.Member_LoginCount + 1, Request.ServerVariables["Remote_Addr"], pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));

    //        //更新会员等级
    //        Update_MemberGrade();

    //        Member_Log(memberinfo.Member_ID, "采购商登录");

    //        // 判断是否经过了邮件验证
    //        if (memberinfo.Member_Emailverify == 0)
    //        {
    //            Session["member_emailverify"] = memberinfo.Member_Emailverify;
    //            Response.Write("/member/emailverify.aspx");
    //            Response.End();
    //        }
    //        else
    //        {
    //            if (Session["url_after_login"] == null)
    //            {
    //                Session["url_after_login"] = "";
    //            }
    //            if (tools.NullStr(Session["url_after_login"].ToString()) != "")
    //            {
    //                Response.Write(Session["url_after_login"].ToString());
    //            }
    //            else
    //            {
    //                Response.Write("/member/index.aspx");
    //            }
    //        }
    //    }
    //    else
    //    {
    //        Session["logintype"] = "False";
    //        Response.Write("Failure");
    //        Response.End();
    //    }
    //}

    //会员退出
    public void Member_LogOut()
    {
        Session.Abandon();
        Session["member_logined"] = false;
        Response.Cookies["autologin"].Expires = DateTime.Now.AddDays(-1);
        Response.Redirect("/login.aspx");
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

    //更新购物车信息
    public void Cart_Price_Update()
    {
        double Product_Price;
        int normal_amount, Product_Coin;

        ProductInfo productinfo = null;
        PackageInfo packageinfo = null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersGoodsTmpInfo.Orders_Goods_ParentID", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND((", "str", "OrdersGoodsTmpInfo.Orders_Goods_SessionID", "=", Session.SessionID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND)", "str", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("OR)", "str", "OrdersGoodsTmpInfo.Orders_Goods_BuyerID", "=", tools.NullInt(Session["member_id"]).ToString()));

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
                        Product_Price = pub.Get_Member_Price(productinfo.Product_ID, productinfo.Product_Price);
                        double wholesale_price = 0;
                        if (wholesale_price < Product_Price && wholesale_price != 0.00)
                        {
                            Product_Price = wholesale_price;
                        }

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
                if (entity.Orders_Goods_Type == 2 && entity.Orders_Goods_ParentID == 0)
                {
                    packageinfo = MyPackage.GetPackageByID(entity.Orders_Goods_Product_ID, pub.CreateUserPrivilege("0dd17a70-862d-4e57-9b45-897b98e8a858"));
                    if (packageinfo != null)
                    {
                        Product_Price = pub.Get_Member_Price(0, packageinfo.Package_Price);

                        double wholesale_price = 0;
                        if (wholesale_price < Product_Price && wholesale_price != 0.00)
                        {
                            Product_Price = wholesale_price;
                        }
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


    //发送验证邮件
    public int member_register_sendemailverify(string member_email, string member_verifycode)
    {
        //发送注册邮件
        string mailsubject, mailbodytitle, mailbody;
        mailsubject = "赶快验证，马上享受{sys_config_site_name}会员服务！";
        mailsubject = replace_sys_config(mailsubject);
        mailbodytitle = "赶快验证，马上享受{sys_config_site_name}会员服务！";
        mailbodytitle = replace_sys_config(mailbodytitle);
        mailbody = mail_template("emailverify", "", "", member_verifycode);
        return pub.Sendmail(member_email, mailsubject, mailbodytitle, mailbody);
    }

    //重新发送验证邮件
    public void member_register_resendemailverify()
    {
        if (Session["member_logined"].ToString() != "True")
        {
            Session["url_after_login"] = "/member/emailverify.aspx";
            Response.Redirect("/login.aspx");
        }
        else
        {
            MemberInfo memberinfo = MyMember.GetMemberByID(tools.CheckInt(Session["member_id"].ToString()), pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
            if (memberinfo != null)
            {
                member_register_sendemailverify(memberinfo.Member_Email, memberinfo.Member_VerifyCode);
                Response.Redirect("/member/emailverify.aspx");
            }
            else
            {
                Session["url_after_login"] = "/member/emailverify.aspx";
                Response.Redirect("/login.aspx");
            }
        }
    }

    //更改注册Email
    public void member_register_modifyemail()
    {
        string member_email = "";
        string member_verifycode = "";
        member_email = tools.CheckStr(Request["member_email"]);
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

        //更新用户邮件
        member_verifycode = pub.Createvkey();
        MemberInfo memberinfo = MyMember.GetMemberByID(tools.CheckInt(Session["member_id"].ToString()), pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        if (memberinfo != null && memberinfo.Member_Emailverify == 0)
        {
            memberinfo.Member_VerifyCode = member_verifycode;
            memberinfo.Member_Email = member_email;
            memberinfo.Member_Emailverify = 0;
            MyMember.EditMember(memberinfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));
        }


        //发送验证邮件
        member_register_sendemailverify(member_email, member_verifycode);

        //置Session和Cookies
        Session["member_email"] = member_email;
        Response.Cookies["member_email"].Expires = DateTime.Now.AddDays(365);
        Response.Cookies["member_email"].Value = member_email;

        //转到邮箱验证页面
        Response.Redirect("/member/emailverify.aspx");
        //Response.Write("success");
        //Response.End();
    }

    //验证邮件
    public void member_register_emailverify()
    {
        string member_verifycode = "";
        string member_email = "";
        member_verifycode = tools.CheckStr(Request["VerifyCode"]);
        string emailverify_result = "false";

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_VerifyCode", "=", member_verifycode));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Trash", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Desc"));
        IList<MemberInfo> memberinfo = MyMember.GetMembers(Query, pub.CreateUserPrivilege("3a9a9cdf-ef00-407d-98ef-44e23be397e8"));
        if (memberinfo != null)
        {
            foreach (MemberInfo entity in memberinfo)
            {
                member_email = entity.Member_Email;
                member_verifycode = pub.Createvkey();
                entity.Member_VerifyCode = member_verifycode;
                entity.Member_Emailverify = 1;
                if (MyMember.EditMember(entity, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe")))
                {
                    Session["member_emailverify"] = entity.Member_Emailverify;
                    emailverify_result = "true";
                    member_register_sendemailverifysuccess(member_email, member_verifycode);
                    Session["member_email"] = member_email;
                    Response.Cookies["member_email"].Expires = DateTime.Now.AddDays(365);
                    Response.Cookies["member_email"].Value = member_email;
                }
            }
        }
        Response.Redirect("/member/emailverify_result.aspx?result=" + emailverify_result);
    }

    //发送注册成功邮件
    public int member_register_sendemailverifysuccess(string member_email, string member_verifycode)
    {
        //发送注册邮件
        string mailsubject = "";
        string mailbodytitle = "";
        string mailbody = "";
        mailsubject = "验证成功，欢迎使用{sys_config_site_name}！";
        mailsubject = replace_sys_config(mailsubject);
        mailbodytitle = "验证成功，欢迎使用{sys_config_site_name}！";
        mailbodytitle = replace_sys_config(mailbodytitle);
        mailbody = mail_template("emailverify_success", "", "", member_verifycode);
        return pub.Sendmail(member_email, mailsubject, mailbodytitle, mailbody);
    }

    //找回密码发送邮件
    public void member_getpass_sendmail()
    {
        string member_email = "";
        string member_verifycode = "";
        member_email = tools.CheckStr(Request["member_email"]);
        //判断邮箱是否有效
        if (tools.CheckEmail(member_email))
        {
            MemberInfo memberinfo = MyMember.GetMemberByEmail(member_email, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
            if (memberinfo != null)
            {
                SupplierInfo supplierInfo = MySupplier.GetSupplierByID(memberinfo.Member_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                if (supplierInfo != null)
                {
                    if (supplierInfo.Supplier_AuditStatus == 1)
                    {
                        member_verifycode = pub.Createvkey();
                        memberinfo.Member_VerifyCode = member_verifycode;
                        MyMember.EditMember(memberinfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));

                        //发送找回密码邮件
                        string mailsubject = "";
                        string mailbodytitle = "";
                        string mailbody = "";
                        mailsubject = "重新设置密码";
                        mailbodytitle = "重新设置密码";
                        mailbody = mail_template("getpass", "", "", member_verifycode);
                        pub.Sendmail(member_email, mailsubject, mailbodytitle, mailbody);
                        Response.Redirect("/member/getpassword_mail_success.aspx");

                    }
                    else
                    {
                        pub.Msg("error", "错误信息", "您的资质尚未审核通过,请使用手机短信进行密码找回", false, "{back}");
                    }
                }
                else
                {
                    pub.Msg("error", "错误信息", "您输入的邮件地址不存在，请检查后重新输入", false, "{back}");
                }


            }
            else
            {
                pub.Msg("error", "错误信息", "请确认该账户对应的商家信息是否存在", false, "{back}");
            }
        }
        else if (pub.Checkmobile_back(member_email))
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_LoginMobile", "=", member_email));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_LoginMobileverify", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", "CN"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Trash", "=", "0"));
            IList<MemberInfo> entityList = MyMember.GetMembers(Query, pub.CreateUserPrivilege("3a9a9cdf-ef00-407d-98ef-44e23be397e8"));
            if (entityList != null)
            {
                Session["getpass_member_loginmobile"] = entityList[0].Member_LoginMobile;


                //Dictionary<string, string> smscheckcode = new Dictionary<string, string>();
                //smscheckcode.Add("sign", member_email);
                //string createkeys = new Public_Class().Createvkey(6);
                //smscheckcode.Add("code", createkeys);
                //smscheckcode.Add("expiration", DateTime.Now.AddSeconds(120).ToString());


                ////发送短信               
                //new SMS().Send(member_email, smscheckcode["code"].ToString());

                //Session["createkeys"] = createkeys;

                //Response.Write("{\"result\":\"true\", \"msg\":\"\"}");


                entityList = null;

                Response.Redirect("/member/getpassword_mobile.aspx?member_mobile=" + member_email + "");
            }
            else
            {
                pub.Msg("error", "错误信息", "您输入的手机号码不存在，请检查后重新输入", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "请输入有效的邮箱地址/手机号码", false, "{back}");
        }
    }

    //找回密码邮件验证
    public void member_getpass_verify()
    {
        string member_verifycode = "";
        member_verifycode = tools.CheckStr(Request["VerifyCode"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_VerifyCode", "=", member_verifycode));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Trash", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Desc"));
        IList<MemberInfo> memberinfo = MyMember.GetMembers(Query, pub.CreateUserPrivilege("3a9a9cdf-ef00-407d-98ef-44e23be397e8"));
        if (memberinfo != null)
        {
            foreach (MemberInfo entity in memberinfo)
            {
                Session["getpass_verify"] = "true";
                Session["getpass_member_id"] = entity.Member_ID;
                Session["getpass_member_mail"] = entity.Member_Email;
                Session["getpass_member_loginmobile"] = "";
            }
            Response.Redirect("/member/getpassword_reset.aspx");
        }
        else
        {
            Session["getpass_verify"] = "false";
            Session["getpass_member_id"] = 0;
            Session["getpass_member_mail"] = "";
            Session["getpass_member_loginmobile"] = "";
            Response.Redirect("/member/getpassword_verify_failed.aspx");
        }

    }

    //找回密码重新设置密码  type:0  邮箱验证   type:1手机验证
    public void member_getpass_resetpass(int type)
    {
        string member_id, member_password, member_password_confirm, verifycode, member_verifycode, member_mobile;
        string member_email = "";
        if (tools.NullStr(Session["getpass_verify"]) == "true")
        {
            member_id = Session["getpass_member_id"].ToString();
            if (type == 0)
            {
                member_email = Session["getpass_member_mail"].ToString();
            }
            else if (type == 1)
            {
                member_mobile = Session["member_mobile"].ToString();
            }
            else
            {
                pub.Msg("error", "验证信息有误", "请重新输入验证信息", false, "{back}");
            }


            member_password = tools.CheckStr(pub.FormatNullToStr(Request.Form["member_password"]).Trim());
            member_password_confirm = tools.CheckStr(pub.FormatNullToStr(Request.Form["member_password_confirm"]).Trim());

            verifycode = tools.CheckStr(Request["verifycode"]).ToLower();

            if (verifycode.ToLower() != Session["Trade_Verify"].ToString() || verifycode.Length == 0)
            {
                pub.Msg("error", "验证码输入错误", "验证码输入错误", false, "{back}");
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

            MemberInfo memberinfo = MyMember.GetMemberByID(tools.CheckInt(member_id), pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
            if (memberinfo != null)
            {
                member_verifycode = pub.Createvkey();
                memberinfo.Member_VerifyCode = member_verifycode;
                memberinfo.Member_Password = encrypt.MD5(member_password);
                MyMember.EditMember(memberinfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));

                //if (Session["getpass_member_loginmobile"] != null && Convert.ToString(Session["getpass_member_loginmobile"]).Length > 0)
                //{
                if (type == 1)
                {
                    new SMS().Send(memberinfo.Member_LoginMobile, "密码已重置！【易耐商城】");
                }


                //}
                else if (type == 0)
                {
                    //发送验证邮件
                    string mailsubject, mailbodytitle, mailbody;
                    mailsubject = "密码已重新设置";
                    mailbodytitle = "密码已重新设置";
                    mailbody = mail_template("getpass_success", "", "", member_verifycode);
                    pub.Sendmail(member_email, mailsubject, mailbodytitle, mailbody);
                }

                Response.Redirect("/member/getpassword_reset_success.aspx");
            }
            else
            {
                //跳转
                Response.Redirect("/member/getpassword.aspx");
            }
        }
        else
        {
            //跳转
            Response.Redirect("/member/getpassword.aspx");
        }
    }

    public string DisplayMemberType(int selectValeu)
    {
        StringBuilder strHTML = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberCertTypeInfo.Member_Cert_Type_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberCertTypeInfo.Member_Cert_Type_IsActive", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("MemberCertTypeInfo.Member_Cert_Type_Sort", "asc"));
        IList<MemberCertTypeInfo> entitys = MyCertType.GetMemberCertTypes(Query);

        strHTML.Append("<select name=\"member_type\" id=\"member_type\" style=\"width: 298px;\">");
        strHTML.Append("<option value=\"0\">请选择采购商类型</option>");
        if (entitys != null)
        {
            foreach (MemberCertTypeInfo entity in entitys)
            {
                if (entity.Member_Cert_Type_ID == selectValeu)
                {
                    strHTML.Append("<option value=\"" + entity.Member_Cert_Type_ID + "\" selected>" + entity.Member_Cert_Type_Name + "</option>");
                }
                else
                {
                    strHTML.Append("<option value=\"" + entity.Member_Cert_Type_ID + "\">" + entity.Member_Cert_Type_Name + "</option>");
                }
            }
        }
        strHTML.Append("</select>");

        return strHTML.ToString();
    }



    public void PasswordResetByMobile(string member_mobile)
    {
        int member_id = -1;
        SQLHelper DBHelper = new SQLHelper();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_LoginMobile", "=", member_mobile));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Desc"));
        IList<MemberInfo> entitys = MyMember.GetMembers(Query, pub.CreateUserPrivilege("3a9a9cdf-ef00-407d-98ef-44e23be397e8"));
        if (entitys != null)
        {
            member_id = entitys[0].Member_ID;
        }
        //int member_id= tools.CheckInt( DBHelper.ExecuteScalar("select Member_ID from Member where Member_LoginMobile="+member_mobile+"").ToString());
        string verifycode = Request["verifycode"];
        string mobile_verifycode = Request["mobile_verifycode"];
        #region  验证码

        #endregion
        #region 短信效验码验证
        Dictionary<string, string> sms_check = Session["sms_check"] as Dictionary<string, string>;
        string Sms_Code = "";
        if (sms_check != null)
        {
            Sms_Code = sms_check["code"];
        }
        else
        {
            pub.Msg("error", "错误信息", "请先获取短信验证码", false, "{back}");
        }

        if ((mobile_verifycode == null) || (mobile_verifycode == ""))
        {
            pub.Msg("error", "错误信息", "请输入短信效验码", false, "{back}");
        }
        if (mobile_verifycode.Length == 0 || mobile_verifycode != Sms_Code)
        {
            pub.Msg("error", "错误信息", "短信效验码错误", false, "{back}");
        }

        if ((Convert.ToDateTime(sms_check["expiration"]) - DateTime.Now).TotalSeconds < 0)
        {
            pub.Msg("error", "错误信息", "短信效验码过期", false, "{back}");
        }
        sms_check = null;
        Session.Remove("sms_check");
        Session["member_mobile"] = member_mobile;
        Session["getpass_verify"] = "true";
        Session["getpass_member_id"] = member_id;

        Response.Redirect("/member/getpasswordreset_bymoile.aspx?member_mobile=" + member_mobile + "");
        #endregion
    }
    #endregion

    #region 我是买家

    #region 页面左侧导航

    #region 采购管理

    /// <summary>
    /// 采购管理
    /// </summary>
    /// <param name="main"></param>
    /// <param name="sub"></param>
    /// <param name="sb"></param>
    private static void Procurement_Management(int main, int sub, StringBuilder sb)
    {
        sb.Append("                 <div class=\"b07_info\">");
        sb.Append("                       <h3><span onclick=\"openShutManager(this,'box1')\" ><a id=\"1\"  onClick=\"switchTag(1);\">采购管理</a></span></h3>");
        sb.Append("                       <div class=\"b07_info_main\" id=\"box1\">");
        sb.Append("                             <ul>");
        sb.Append("<li " + (main == 1 && sub == 1 ? " class=\"on\"" : "") + "><a href=\"/member/order_list.aspx\">我的采购订单</a></li>");
        sb.Append("<li " + (main == 1 && sub == 6 ? " class=\"on\"" : "") + "><a href=\"/member/order_delivery_list.aspx\">我的收货单</a></li>");
        sb.Append("<li " + (main == 1 && sub == 7 ? " class=\"on\"" : "") + "><a href=\"/member/member_favorites.aspx\">我的商品收藏</a></li>");
        sb.Append("<li " + (main == 1 && sub == 8 ? " class=\"on\"" : "") + "><a href=\"/valuate/member_shopevaluate.aspx\">我的供应商评价</a></li>");
        sb.Append("<li " + (main == 1 && sub == 9 ? " class=\"on\"" : "") + "><a href=\"/valuate/member_productvaluate.aspx\">我的商品评价</a></li>");
        sb.Append("                             </ul>                                                      ");
        sb.Append("                       </div>                                                           ");
        sb.Append("                 </div>                                                                 ");
    }

    #endregion

    #region 财务管理

    /// <summary>
    /// 财务管理
    /// </summary>
    /// <param name="main"></param>
    /// <param name="sub"></param>
    /// <param name="sb"></param>
    private static void Financial_Management(int main, int sub, StringBuilder sb)
    {
        sb.Append("                 <div class=\"b07_info\">                                                 ");
        sb.Append("                       <h3><span onclick=\"openShutManager(this,'box2')\" ><a id=\"2\" onClick=\"switchTag(2);\">财务统计</a></span></h3>");
        sb.Append("                       <div class=\"b07_info_main\" id=\"box2\">                                    ");
        sb.Append("                             <ul>                                                               ");
        sb.Append("<li " + (main == 2 && sub == 1 ? " class=\"on\"" : "") + "><a href=\"/member/Member_Statistics.aspx\">统计报表</a></li>");
        sb.Append("                                  <li " + (main == 2 && sub == 2 ? " class=\"on\"" : "") + "><a href=\"#\">交易收支查询</a></li>          ");
        //sb.Append("<li " + (main == 1 && sub == 1 ? " class=\"on\"" : "") + "><a href=\"/member/order_list.aspx\">我的采购订单</a></li>");
        sb.Append("                             </ul>                                                              ");
        sb.Append("                       </div>                                                                   ");
        sb.Append("                 </div>                                                                         ");
    }
    private static void Financial_Management(int main, int sub, StringBuilder sb, string item)
    {
        sb.Append("                 <div class=\"b07_info\">                                                 ");
        sb.Append("                       <h3><span onclick=\"openShutManager(this,'box2')\" ><a id=\"2\" onClick=\"switchTag(2);\">财务统计</a></span></h3>");
        sb.Append("                       <div class=\"b07_info_main\" id=\"box2\">                                    ");
        sb.Append("                             <ul>                                                               ");
        sb.Append("<li " + (main == 2 && sub == 1 ? " class=\"on\"" : "") + "><a href=\"/member/Member_Statistics.aspx\">统计报表</a></li>");
        sb.Append("                                  <li " + (main == 2 && sub == 2 ? " class=\"on\"" : "") + "><a href=\"#\">交易收支查询</a></li>          ");
        sb.Append("<li " + (main == 1 && sub == 1 ? " class=\"on\"" : "") + "><a href=\"/member/order_list.aspx\">我的采购订单</a></li>");
        sb.Append("                             </ul>                                                              ");
        sb.Append("                       </div>                                                                   ");
        sb.Append("                 </div>                                                                         ");
    }

    #endregion

    #region 招标管理

    /// <summary>
    /// 招标管理
    /// </summary>
    /// <param name="main"></param>
    /// <param name="sub"></param>
    /// <param name="sb"></param>
    private static void Tender_Management(int main, int sub, StringBuilder sb)
    {

        sb.Append("                 <div class=\"b07_info\">                                                         ");
        sb.Append("                       <h3><span onclick=\"openShutManager(this,'box3')\" ><a id=\"3\" onClick=\"switchTag(3);\">招标拍卖</a></span></h3>");
        sb.Append("                       <div class=\"b07_info_main\" id=\"box3\">                                       ");
        sb.Append("                             <ul>                                                                  ");
        sb.Append("                                  <li " + (main == 3 && sub == 2 ? " class=\"on\"" : "") + "><a href=\"/member/bid_add.aspx\">发布招标</a></li>          ");
        sb.Append("                                  <li " + (main == 3 && sub == 3 ? " class=\"on\"" : "") + "><a href=\"/member/bid_list.aspx\">招标列表</a></li>          ");
        sb.Append("<li " + (main == 1 && sub == 3 ? " class=\"on\"" : "") + "><a href=\"/member/bid_list.aspx\">管理招标</a></li>");
        sb.Append("<li " + (main == 9 && sub == 9 ? " class=\"on\"" : "") + "><a href=\"/supplier/auction_add.aspx\">发布拍卖</a></li>");
        sb.Append("<li " + (main == 5 && sub == 3 ? " class=\"on\"" : "") + "><a href=\"/supplier/auction_list.aspx\">拍卖列表</a></li>");
        sb.Append("                                  <li " + (main == 3 && sub == 5 ? " class=\"on\"" : "") + "><a href=\"/member/auction_list.aspx\">管理拍卖</a></li>            ");
        sb.Append("                       </div>                                                                   ");
        sb.Append("                 </div>                                                                         ");

    }

    #endregion

    #region 拍卖管理

    /// <summary>
    /// 拍卖管理
    /// </summary>
    /// <param name="main"></param>
    /// <param name="sub"></param>
    /// <param name="sb"></param>
    private static void Auction_Management(int main, int sub, StringBuilder sb, int Tender_ID)
    {

        sb.Append("                 <div class=\"b07_info\">                                                         ");
        sb.Append("                       <h3><span onclick=\"openShutManager(this,'box4')\" ><a id=\"4\" onClick=\"switchTag(4);\">拍卖管理</a></span></h3>");
        sb.Append("                       <div class=\"b07_info_main\" id=\"box4\">                                       ");
        sb.Append("                             <ul>                                                                  ");
        sb.Append("<li " + (main == 9 && sub == 9 ? " class=\"on\"" : "") + "><a href=\"/supplier/auction_add.aspx\">发布拍卖</a></li>");
        //sb.Append("<li " + (main == 5 && sub == 3 ? " class=\"on\"" : "") + "><a href=\"/supplier/auction_list.aspx\">拍卖列表</a></li>");
        sb.Append("                                  <li " + (main == 3 && sub == 5 ? " class=\"on\"" : "") + "><a href=\"/member/auction_list.aspx\">管理拍卖</a></li>            ");
        sb.Append("<li " + (main == 4 && sub == 4 ? " class=\"on\"" : "") + "><a href=\"/member/auction_tender_sign.aspx?TenderID=" + Tender_ID + "\">我的拍卖报价</a></li>");
        sb.Append("                       </div>                                                                      ");
        sb.Append("                 </div>                                                                            ");


    }

    #endregion

    #region 账户管理
    private static void Account_Management(int main, int sub, int MemberMessageNum, StringBuilder sb)
    {
        sb.Append("                 <div class=\"b07_info\">                                                         ");
        sb.Append("                       <h3><span onclick=\"openShutManager(this,'box5')\" ><a id=\"5\" onClick=\"switchTag(5);\">账户管理</a></span></h3>");
        sb.Append("                       <div class=\"b07_info_main\" id=\"box5\">                                       ");
        sb.Append("                             <ul>                                                                  ");


        //sb.Append("<li " + (main == 5 && sub == 1 ? " class=\"on\"" : "") + "><a href=\"/member/subAccount.aspx\">创建子账户</a></li>");
        //sb.Append("<li " + (main == 5 && sub == 2 ? " class=\"on\"" : "") + "><a href=\"/member/subAccount_list.aspx\">管理子账户</a></li>");


        sb.Append("<li " + (main == 5 && sub == 3 ? " class=\"on\"" : "") + "><a href=\"/member/feedback.aspx\">售后服务</a></li>");
        sb.Append("<li " + (main == 5 && sub == 4 ? " class=\"on\"" : "") + "><a href=\"/member/account_profile.aspx\" >资料管理</a></li>");
        //sb.Append("<li " + (main == 3 && sub == 2 ? " class=\"on\"" : "") + "><a href=\"/supplier/Supplier_Cert.aspx\">资质管理</a></li>");
        sb.Append("<li " + (main == 5 && sub == 5 ? " class=\"on\"" : "") + "><a href=\"/member/order_address.aspx\">收货地址薄</a></li>");
        sb.Append("<li " + (main == 5 && sub == 6 ? " class=\"on\"" : "") + "><a href=\"/member/message_list.aspx?action=list\">消息通知 ");
        sb.Append("<span style=\"color:#ff6600\">&nbsp(" + MemberMessageNum + ")</span> 条未读");
        sb.Append("   </a></li>");
        sb.Append("<li " + (main == 5 && sub == 7 ? " class=\"on\"" : "") + "><a href=\"/member/account_password.aspx\" >修改密码</a></li>");

        sb.Append("                             </ul>                                       ");
        sb.Append("                       </div>                                                                      ");
        sb.Append("                 </div>                                                                            ");
    }

    #endregion

    #region 页面左侧导航


    /// <summary>
    /// 页面左侧导航
    /// </summary>
    /// <param name="main"></param>
    /// <param name="sub"></param>
    /// <returns></returns>
    public string Member_Left_HTML(int main, int sub)
    {
        int MemberMessageNum = messageclass.GetMessageNum(1);

        StringBuilder sb = new StringBuilder();

        MemberInfo entity = GetMemberByID();

        Session["Position"] = "users";
        sb.Append("                 <div class=\"menu_1\">");
        //if (tools.NullInt(Session["member_auditstatus"]) == 1)
        //{

        sb.Append("                 <h2>我是买家</h2>");
        if (tools.NullStr(Session["subPrivilege"]) == "")
        {
            //主账号 会员中心页面
            int Tender_ID = Convert.ToInt32((new SQLHelper().ExecuteScalar("select max(Tender_ID) from Tender where   Tender_BidID in (select Bid_ID from Bid where Bid_Type=1) and Tender_SupplierID=" + tools.CheckInt(Session["supplier_id"].ToString()) + "")));
            //采购管理
            Procurement_Management(main, sub, sb);
            //招标管理
            Tender_Management(main, sub, sb);
            //拍卖管理
            //Auction_Management(main, sub, sb, Tender_ID);
            //账号管理
            Account_Management(main, sub, MemberMessageNum, sb);
            //财务管理
            Financial_Management(main, sub, sb);
        }
        else
        {
            //子账号 会员中心页面
            string Member_Permissions = Session["subPrivilege"].ToString();
            List<string> oTempList = new List<string>(Member_Permissions.Split(','));
            int Tender_ID = Convert.ToInt32((new SQLHelper().ExecuteScalar("select max(Tender_ID) from Tender where   Tender_BidID in (select Bid_ID from Bid where Bid_Type=1) and Tender_SupplierID=" + tools.CheckInt(Session["supplier_id"].ToString()) + "")));
            foreach (var item in oTempList)
            {
                switch (item)
                {
                    case "1":
                        //采购管理
                        Procurement_Management(main, sub, sb);
                        break;
                    case "2":
                        break;
                    case "3":
                        //招标管理
                        Tender_Management(main, sub, sb);
                        break;
                    case "4":
                        //拍卖管理
                        Auction_Management(main, sub, sb, Tender_ID);
                        break;
                    case "5":
                        break;
                    case "6":
                        //财务管理
                        Financial_Management(main, sub, sb, item);
                        break;
                    default:
                        //账号管理
                        //Account_Management(main, sub, MemberMessageNum, sb);
                        pub.Msg("error", "错误提示", "当前系统繁忙请稍后重试，若多次出现请联系网站管理员！", false, "{back}");
                        break;
                }
            }

        }
        sb.Append("                 </div>                                                                            ");
        return sb.ToString();
    }






    #endregion

    #endregion

    public void UpdateMemberProfile()
    {
        MemberProfileInfo profileInfo = null;
        SupplierInfo supplierinfo = null;
        int Supplier_AuditStatus = -1;
        int account_id = tools.NullInt(Session["member_accountid"]);
        if (account_id > 0)
        {
            Response.Redirect("/member/index.aspx");
        }

        string Member_Profile_OrganizationCode = null;
        string Member_Profile_BusinessCode = null;

        string Member_Corporate = null;
        string Member_CorporateMobile = null;
        double Member_RegisterFunds = 0.00;
        string Member_TaxationCode = null;
        string Member_BankAccountCode = null;

        string Member_Profile_SealImg = null;

        string Supplier_OrganizationCode = null;
        string Supplier_Corporate = null;
        string Supplier_CorporateMobile = null;
        double Supplier_RegisterFunds = 0.00;
        string Supplier_TaxationCode = null;
        string Supplier_BankAccountCode = null;
        string Member_NickName = "";
        string Member_RealName = "";


        string Member_Company = "";
        string Member_LoginMobile = "";


        int Member_ID = tools.CheckInt(Request.Form["Member_ID"]);
        MemberInfo memberinfo = GetMemberByID();
        if (memberinfo != null)
        {
            profileInfo = memberinfo.MemberProfileInfo;
            supplierinfo = supplier.GetSupplierByID(memberinfo.Member_SupplierID);
            if (supplierinfo != null)
            {
                Supplier_AuditStatus = supplierinfo.Supplier_AuditStatus;
                Member_Company = supplierinfo.Supplier_CompanyName;
            }
            if (profileInfo != null)
            {
                Member_RealName = profileInfo.Member_RealName;


            }
        }

        if (memberinfo != null)
        {
            Member_NickName = memberinfo.Member_NickName;


            Member_LoginMobile = memberinfo.Member_LoginMobile;
        }







        string Member_Email = tools.CheckStr(Request.Form["Member_Email"]);
        string Member_Profile_State = tools.CheckStr(Request.Form["Member_Profile_State"]);
        string Member_Profile_City = tools.CheckStr(Request.Form["Member_Profile_City"]);
        string Member_Profile_County = tools.CheckStr(Request.Form["Member_Profile_County"]);


        string Member_Profile_Country = tools.CheckStr(Request.Form["Member_Profile_Country"]);
        string Member_Profile_Address = tools.CheckStr(Request.Form["Member_Profile_Address"]);

        string Member_Profile_Phone = tools.CheckStr(Request.Form["Member_Profile_Phone"]);
        string Member_Profile_Fax = tools.CheckStr(Request.Form["Member_Profile_Fax"]);
        string Member_Profile_Zip = tools.CheckStr(Request.Form["Member_Profile_Zip"]);
        string Member_Profile_Contactman = tools.CheckStr(Request.Form["Member_Profile_Contactman"]);
        //string Member_Profile_Mobile = tools.CheckStr(Request.Form["Member_Profile_Mobile"]);
        string Member_Profile_QQ = tools.CheckStr(Request.Form["Member_Profile_QQ"]);



        Member_Profile_SealImg = tools.CheckStr(Request.Form["Member_Profile_SealImg"]);




        int Supplier_IsAuthorize = tools.CheckInt(Request.Form["Supplier_IsAuthorize"]);
        int Supplier_IsTrademark = tools.CheckInt(Request.Form["Supplier_IsTrademark"]);
        string Supplier_ServicesPhone = tools.CheckStr(Request.Form["Supplier_ServicesPhone"]);
        int Supplier_OperateYear = tools.CheckInt(Request.Form["Supplier_OperateYear"]);
        int Supplier_SaleType = tools.CheckInt(Request.Form["Supplier_SaleType"]);
        string Supplier_ContactEmail = tools.CheckStr(Request.Form["Supplier_ContactEmail"]);
        string Supplier_ContactQQ = tools.CheckStr(Request.Form["Supplier_ContactQQ"]);
        string Supplier_Category = tools.CheckStr(Request.Form["Supplier_Category"]);

        //新加两个字段 

        string Member_UniformSocial_Number = tools.CheckStr(Request.Form["Member_UniformSocial_Number"]);

        string Supplier_SysMobile = tools.CheckStr(Request.Form["Supplier_SysMobile"]);
        string Supplier_SysEmail = tools.CheckStr(Request.Form["Supplier_SysEmail"]);


        if (Member_Profile_QQ == "")
        {
            pub.Msg("info", "提示信息", "请将QQ号码填写完整", false, "{back}");
        }
        if (Member_Profile_Address == "")
        {
            pub.Msg("info", "提示信息", "请将公司地址填写完整", false, "{back}");
        }

        if (Member_Corporate == "")
        {
            pub.Msg("info", "提示信息", "请将法定代表人姓名填写完整", false, "{back}");
        }
        if (Supplier_ServicesPhone == "")
        {
            pub.Msg("info", "提示信息", "请将公司电话填写完整", false, "{back}");
        }
        if (Member_UniformSocial_Number == "")
        {
            pub.Msg("info", "提示信息", "统一社会代码证号", false, "{back}");
        }




        if (supplierinfo != null)
        {
            if (supplierinfo.Supplier_AuditStatus == 0)
            {
                Member_Profile_OrganizationCode = tools.CheckStr(Request.Form["Member_Profile_OrganizationCode"]);
                Member_Profile_BusinessCode = tools.CheckStr(Request.Form["Member_Profile_BusinessCode"]);

                Member_Corporate = tools.CheckStr(Request.Form["Member_Corporate"]);
                Member_CorporateMobile = tools.CheckStr(Request.Form["Member_CorporateMobile"]);
                Member_RegisterFunds = tools.CheckFloat(Request.Form["Member_RegisterFunds"]);
                Member_TaxationCode = tools.CheckStr(Request.Form["Member_TaxationCode"]);
                Member_BankAccountCode = tools.CheckStr(Request.Form["Member_BankAccountCode"]);

                Member_Profile_SealImg = tools.CheckStr(Request.Form["Member_Profile_SealImg"]);
            }
        }




        string Member_HeadImg = tools.CheckStr(Request.Form["Member_HeadImg"]);
        string hidden_type = tools.CheckStr(Request.Form["hidden_type"]);


        string Member_Company_Introduce = tools.CheckStr(Request.Form["Member_Company_Introduce"]);
        string Member_Company_Contact = tools.CheckStr(Request.Form["Member_Company_Contact"]);


        //if (Member_Profile_Contactman == "")
        //{
        //    pub.Msg("info", "提示信息", "请将联系人填写完整", false, "{back}");
        //}
        //if (Member_Profile_Phone == "")
        //{
        //    pub.Msg("info", "提示信息", "请将联系电话填写完整", false, "{back}");
        //}





        //if (Member_Profile_County == "0" || Member_Profile_County == "")
        //{
        //    pub.Msg("info", "提示信息", "请选择省市区信息", false, "{back}");
        //}




        if (memberinfo != null)
        {

            if (supplierinfo.Supplier_AuditStatus == 0)
            {
            }
            else
            {
                Member_CorporateMobile = profileInfo.Member_CorporateMobile;

            }
        }







        if (memberinfo != null && memberinfo.MemberProfileInfo != null)
        {
            memberinfo.Member_AuditStatus = 1;
            memberinfo.Member_NickName = Member_NickName;
            memberinfo.Member_Company_Introduce = Member_Company_Introduce;
            memberinfo.Member_Company_Contact = Member_Company_Contact;
            memberinfo.Member_Email = Member_Email;
            if (MyMember.EditMember(memberinfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe")))
            {
                memberinfo.MemberProfileInfo.Member_State = Member_Profile_State;
                memberinfo.MemberProfileInfo.Member_City = Member_Profile_City;
                memberinfo.MemberProfileInfo.Member_County = Member_Profile_County;
                memberinfo.MemberProfileInfo.Member_Country = Member_Profile_Country;
                memberinfo.MemberProfileInfo.Member_Zip = Member_Profile_Zip;
                memberinfo.MemberProfileInfo.Member_StreetAddress = Member_Profile_Address;
                memberinfo.MemberProfileInfo.Member_Name = Member_Profile_Contactman;
                memberinfo.MemberProfileInfo.Member_Fax = Member_Profile_Fax;
                memberinfo.MemberProfileInfo.Member_Phone_Number = Member_Profile_Phone;
                memberinfo.MemberProfileInfo.Member_QQ = Member_Profile_QQ;
                memberinfo.MemberProfileInfo.Member_HeadImg = Member_HeadImg;


                if (supplierinfo != null)
                {
                    if (supplierinfo.Supplier_AuditStatus == 0)
                    {

                        memberinfo.MemberProfileInfo.Member_OrganizationCode = Member_Profile_OrganizationCode;
                        memberinfo.MemberProfileInfo.Member_BusinessCode = Member_Profile_BusinessCode;
                        memberinfo.MemberProfileInfo.Member_Corporate = Member_Corporate;
                        memberinfo.MemberProfileInfo.Member_CorporateMobile = Member_CorporateMobile;
                        memberinfo.MemberProfileInfo.Member_RegisterFunds = Member_RegisterFunds;
                        memberinfo.MemberProfileInfo.Member_TaxationCode = Member_TaxationCode;
                        memberinfo.MemberProfileInfo.Member_BankAccountCode = Member_BankAccountCode;

                        memberinfo.MemberProfileInfo.Member_SealImg = Member_Profile_SealImg;
                        //entity.MemberProfileInfo.Member_RealName = Member_RealName;
                        memberinfo.MemberProfileInfo.Member_UniformSocial_Number = Member_UniformSocial_Number;


                    }
                }


                MyMember.EditMemberProfile(memberinfo.MemberProfileInfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));



                Session["member_nickname"] = Member_NickName;






                //int Supplier_ID = tools.CheckInt(Request.Form["Supplier_ID"]);
                string Supplier_Nickname = Member_NickName;
                string Supplier_Email = Member_Email;

                string Supplier_CompanyName = Member_Company;
                string Supplier_County = tools.CheckStr(Request.Form["Member_Profile_County"]);
                string Supplier_City = tools.CheckStr(Request.Form["Member_Profile_City"]);
                string Supplier_State = tools.CheckStr(Request.Form["Member_Profile_State"]);
                string Supplier_Country = tools.CheckStr(Request.Form["Member_Profile_Country"]);
                string Supplier_Address = tools.CheckStr(Request.Form["Member_Profile_Address"]);
                string Supplier_Phone = tools.CheckStr(Request.Form["Member_Profile_Phone"]);
                string Supplier_Fax = tools.CheckStr(Request.Form["Member_Profile_Fax"]);
                string Supplier_Zip = tools.CheckStr(Request.Form["Member_Profile_Zip"]);
                string Supplier_Contactman = tools.CheckStr(Request.Form["Member_Profile_Contactman"]);
                string Supplier_Mobile = tools.CheckStr(Request.Form["Member_Profile_Mobile"]);



                if (memberinfo != null)
                {
                    //if (memberinfo.Member_Cert_Status == 0)
                    //{
                    if (supplierinfo.Supplier_AuditStatus == 0)
                    {
                        Supplier_OrganizationCode = tools.CheckStr(Request.Form["Member_Profile_OrganizationCode"]);
                        Supplier_Corporate = tools.CheckStr(Request.Form["Member_Corporate"]);
                        Supplier_CorporateMobile = tools.CheckStr(Request.Form["Member_CorporateMobile"]);
                        Supplier_RegisterFunds = tools.CheckFloat(Request.Form["Member_RegisterFunds"]);
                        Supplier_TaxationCode = tools.CheckStr(Request.Form["Member_TaxationCode"]);
                        Supplier_BankAccountCode = tools.CheckStr(Request.Form["Member_BankAccountCode"]);
                        Supplier_Mobile = memberinfo.Member_LoginMobile;
                        Supplier_CompanyName = profileInfo.Member_Company;
                        Supplier_Nickname = memberinfo.Member_NickName;
                        Supplier_Email = memberinfo.Member_Email;
                    }
                }


                if (Supplier_Corporate == "")
                {
                    pub.Msg("info", "提示信息", "请将法定代表人姓名填写完整", false, "{back}");
                }


                if (Supplier_Address == "")
                {
                    Response.Write("请将详细地址填写完整");
                    Response.End();
                }


                SupplierInfo SupplierEntity = supplier.GetSupplierByID();

                SupplierEntity.Supplier_Nickname = Supplier_Nickname;
                SupplierEntity.Supplier_CompanyName = Supplier_CompanyName;
                SupplierEntity.Supplier_Email = Supplier_Email;
                SupplierEntity.Supplier_County = Supplier_County;
                SupplierEntity.Supplier_City = Supplier_City;
                SupplierEntity.Supplier_State = Supplier_State;
                SupplierEntity.Supplier_Country = Supplier_Country;
                SupplierEntity.Supplier_Address = Supplier_Address;
                SupplierEntity.Supplier_Phone = Supplier_Phone;
                SupplierEntity.Supplier_Fax = Supplier_Fax;
                SupplierEntity.Supplier_Zip = Supplier_Zip;
                SupplierEntity.Supplier_Contactman = Supplier_Contactman;
                SupplierEntity.Supplier_Mobile = Supplier_Mobile;
                SupplierEntity.Supplier_SysEmail = Supplier_SysEmail;
                SupplierEntity.Supplier_SysMobile = Supplier_SysMobile;
                if (memberinfo != null)
                {

                    if (supplierinfo.Supplier_AuditStatus == 0)
                    {
                        SupplierEntity.Supplier_Corporate = Supplier_Corporate;
                        SupplierEntity.Supplier_CorporateMobile = Supplier_CorporateMobile;
                        SupplierEntity.Supplier_RegisterFunds = Supplier_RegisterFunds;
                        SupplierEntity.Supplier_OrganizationCode = Supplier_OrganizationCode;
                        SupplierEntity.Supplier_TaxationCode = Supplier_TaxationCode;
                        SupplierEntity.Supplier_BankAccountCode = Supplier_BankAccountCode;
                    }
                }

                SupplierEntity.Supplier_IsAuthorize = Supplier_IsAuthorize;
                SupplierEntity.Supplier_IsTrademark = Supplier_IsTrademark;
                SupplierEntity.Supplier_ServicesPhone = Supplier_ServicesPhone;
                SupplierEntity.Supplier_OperateYear = Supplier_OperateYear;
                SupplierEntity.Supplier_SaleType = Supplier_SaleType;
                SupplierEntity.Supplier_ContactEmail = Supplier_ContactEmail;
                SupplierEntity.Supplier_ContactQQ = Supplier_ContactQQ;
                SupplierEntity.Supplier_Category = Supplier_Category;




                if (MySupplier.EditSupplier(SupplierEntity, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f")))
                {
                    string Supplier_Cert_1, Supplier_Cert_2, Supplier_Cert_3;
                    int Supplier_CertType;
                    Supplier_CertType = 0;
                    Supplier_Cert_1 = "";
                    Supplier_Cert_2 = "";
                    Supplier_Cert_3 = "";
                    Supplier_CertType = tools.CheckInt(Request["Supplier_CertType"]);
                    IList<SupplierCertInfo> certinfos = null;
                    SupplierRelateCertInfo ratecate = null;
                    //= GetSupplierByID();
                    SupplierInfo entity1 = supplier.GetSupplierByID(memberinfo.Member_SupplierID);
                    if (entity1 != null)
                    {

                        certinfos = supplier.GetSupplierCertByType(Supplier_CertType);
                        if (certinfos != null)
                        {
                            foreach (SupplierCertInfo certinfo in certinfos)
                            {
                                if (tools.CheckStr(Request["Supplier_Cert" + certinfo.Supplier_Cert_ID + "_tmp"]).Length == 0)
                                {
                                    //if (certinfo.Supplier_Cert_Name == "合同章(选填)")
                                    //{

                                    //}
                                    //else
                                    //{
                                    //    pub.Msg("info", "提示信息", "请将资质文件上传完整", false, "{back}");
                                    //}

                                }
                            }
                            //删除资质文件
                            MySupplier.DelSupplierRelateCertBySupplierID(entity1.Supplier_ID);
                            foreach (SupplierCertInfo certinfo in certinfos)
                            {
                                ratecate = new SupplierRelateCertInfo();
                                ratecate.Cert_SupplierID = entity1.Supplier_ID;
                                ratecate.Cert_CertID = certinfo.Supplier_Cert_ID;
                                if (tools.CheckStr(Request["Supplier_Cert" + certinfo.Supplier_Cert_ID]).Length == 0)
                                {
                                    ratecate.Cert_Img = tools.CheckStr(Request["Supplier_Cert" + certinfo.Supplier_Cert_ID + "_tmp"]);
                                    ratecate.Cert_Img1 = tools.CheckStr(Request["Supplier_Cert" + certinfo.Supplier_Cert_ID + "_tmp"]);
                                }
                                else
                                {
                                    ratecate.Cert_Img = tools.CheckStr(Request["Supplier_Cert" + certinfo.Supplier_Cert_ID]);
                                    ratecate.Cert_Img1 = tools.CheckStr(Request["Supplier_Cert" + certinfo.Supplier_Cert_ID + "_tmp"]);
                                }
                                MySupplier.AddSupplierRelateCert(ratecate);
                                ratecate = null;
                            }
                        }


                        entity1.Supplier_CertType = Supplier_CertType;
                        entity1.Supplier_Cert_Status = 1;
                        if (MySupplier.EditSupplier(entity1, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f")))
                        {
                            //pub.Msg("positive", "操作成功", "上传完成，请耐心等待审核通过！", true, "/supplier/Supplier_Cert.aspx");
                            //  Response.Write("success");
                            // Response.End();
                        }
                        else
                        {
                            pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
                            // Response.Write("failure");
                            // Response.End();
                        }
                    }
                    else
                    {
                        pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
                        //Response.Write("failure");
                        // Response.End();
                    }

                    pub.Msg("positive", "操作成功", "操作成功", true, "/member/account_profile.aspx");
                }
                else
                {
                    pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
                }
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
        }
    }




    //会员修改页面
    public void UpdateMemberProfileEdit()
    {
        MemberProfileInfo profileInfo = null;
        int Supplier_AuditStatus = -1;
        int account_id = tools.NullInt(Session["member_accountid"]);
        if (account_id > 0)
        {
            Response.Redirect("/member/index.aspx");
        }


        MemberInfo memberinfo = GetMemberByID();
        SupplierInfo Suppliernfo = null;
        int Member_ID = tools.CheckInt(Request.Form["Member_ID"]);
        MemberInfo memberInfo = GetMemberByID();
        if (memberInfo != null)
        {
            profileInfo = memberInfo.MemberProfileInfo;
            Suppliernfo = supplier.GetSupplierByID(memberInfo.Member_SupplierID);
            if (Suppliernfo != null)
            {
                Supplier_AuditStatus = Suppliernfo.Supplier_AuditStatus;
            }
        }

        //用户名
        string Member_NickName = "";
        //string Member_NickName = tools.CheckStr(Request.Form["Member_NickName"]);
        //string Member_Email = tools.CheckStr(Request.Form["Member_Email"]);
        //真实姓名  member表里member_login
        //string Member_RealName = tools.CheckStr(Request.Form["Member_RealName"]);
        //手机号码
        //string Member_LoginMobile = tools.CheckStr(Request.Form["Member_LoginMobile"]);
        //QQ号码
        string Member_Profile_QQ = tools.CheckStr(Request.Form["Member_Profile_QQ"]);
        //常用联系人
        string Member_Profile_Contactman = tools.CheckStr(Request.Form["Member_Profile_Contactman"]);
        //常用联系人电话
        string Member_Profile_Phone = tools.CheckStr(Request.Form["Member_Profile_Phone"]);
        //公司名称
        //string Member_Profile_CompanyName = tools.CheckStr(Request.Form["Member_Profile_CompanyName"]);
        //详细地址
        string Member_Profile_Address = tools.CheckStr(Request.Form["Member_Profile_Address"]);
        //法定代表人姓名 
        string Member_Corporate = tools.CheckStr(Request.Form["Member_Corporate"]);
        //统一社会代码证号  member_profile
        string Member_UniformSocial_Number = tools.CheckStr(Request.Form["Member_UniformSocial_Number"]);








        //if (Member_NickName == "")
        //{
        //    pub.Msg("info", "提示信息", "请填写用户名", false, "{back}");
        //}

        //if (Member_RealName=="")
        //{
        //    pub.Msg("info", "提示信息", "请填写真实姓名", false, "{back}");
        //}


        //if (Member_LoginMobile == "")
        //{
        //    pub.Msg("info", "提示信息", "请填写手机号码", false, "{back}");
        //}
        //else
        //{
        //    if (pub.Checkmobile(Member_LoginMobile) == false)
        //    {
        //        pub.Msg("info", "提示信息", "手机号号码错误", false, "{back}");
        //    }
        //}
        if (Member_Profile_Contactman == "")
        {
            pub.Msg("info", "提示信息", "请将联系人填写完整", false, "{back}");
        }
        if (Member_Profile_Phone == "")
        {
            pub.Msg("info", "提示信息", "请将联系电话填写完整", false, "{back}");
        }

        if (Member_Profile_QQ == "")
        {
            pub.Msg("info", "提示信息", "请将QQ填写完整", false, "{back}");
        }
        if (Member_Corporate == "")
        {
            pub.Msg("info", "提示信息", "请填写法定代表人姓名", false, "{back}");
        }

        //if (Member_Profile_CompanyName == "")
        //{
        //    pub.Msg("info", "提示信息", "请将公司名称填写完整", false, "{back}");
        //}


        if (Member_Profile_Address == "")
        {
            pub.Msg("info", "提示信息", "请将详细地址填写完整", false, "{back}");
        }















        MemberInfo entity = GetMemberByID();
        if (entity != null && entity.MemberProfileInfo != null)
        {


            //if (Member_Profile_CompanyName == "")
            //{
            //    pub.Msg("info", "提示信息", "请将公司名称填写完整", false, "{back}");
            //}
            //entity.MemberProfileInfo.Member_Company = Member_Profile_CompanyName;

            //entity.Member_LoginMobile = Member_LoginMobile;
            //entity.Member_Email = Member_Email;


            entity.Member_AuditStatus = 1;
            //entity.Member_NickName = Member_NickName;
            //entity.Member_Company_Introduce = Member_Company_Introduce;
            //entity.Member_Company_Contact = Member_Company_Contact;

            if (MyMember.EditMember(entity, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe")))
            {
                //entity.MemberProfileInfo.Member_State = Member_Profile_State;
                //entity.MemberProfileInfo.Member_City = Member_Profile_City;
                //entity.MemberProfileInfo.Member_County = Member_Profile_County;
                //entity.MemberProfileInfo.Member_Country = Member_Profile_Country;
                //entity.MemberProfileInfo.Member_Zip = Member_Profile_Zip;
                entity.MemberProfileInfo.Member_StreetAddress = Member_Profile_Address;
                entity.MemberProfileInfo.Member_Name = Member_Profile_Contactman;
                //entity.MemberProfileInfo.Member_Fax = Member_Profile_Fax;
                entity.MemberProfileInfo.Member_Phone_Number = Member_Profile_Phone;
                entity.MemberProfileInfo.Member_QQ = Member_Profile_QQ;
                //entity.MemberProfileInfo.Member_HeadImg = Member_HeadImg;


                if (memberinfo != null)
                {
                    //if (memberinfo.Member_Cert_Status == 0)
                    //{
                    //entity.MemberProfileInfo.Member_OrganizationCode = Member_Profile_OrganizationCode;
                    //entity.MemberProfileInfo.Member_BusinessCode = Member_Profile_BusinessCode;
                    entity.MemberProfileInfo.Member_Corporate = Member_Corporate;
                    //entity.MemberProfileInfo.Member_CorporateMobile = Member_CorporateMobile;
                    //entity.MemberProfileInfo.Member_RegisterFunds = Member_RegisterFunds;
                    //entity.MemberProfileInfo.Member_TaxationCode = Member_TaxationCode;
                    //entity.MemberProfileInfo.Member_BankAccountCode = Member_BankAccountCode;

                    //entity.MemberProfileInfo.Member_SealImg = Member_Profile_SealImg;


                    //entity.MemberProfileInfo.Member_RealName = Member_RealName;

                    //}
                    //entity.MemberProfileInfo.Member_RealName = Member_RealName;
                    entity.MemberProfileInfo.Member_UniformSocial_Number = Member_UniformSocial_Number;
                }


                MyMember.EditMemberProfile(entity.MemberProfileInfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));
                string Member_Company = "";
                int Member_Profile_MemberID = tools.CheckInt(DBHelper.ExecuteScalar("select Member_Profile_ID from Member_Profile where Member_Profile_MemberID=" + entity.Member_ID + "").ToString());
                MemberProfileInfo memberprofileinfo = MyMember.GetMemberProfileByID(Member_Profile_MemberID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                if (memberprofileinfo != null)
                {
                    Member_Company = memberprofileinfo.Member_Company;
                }
                //supplier.UpdateSupplierInfoByMember_Profile();
                //Modify_Enterprise_Info(entity);
                Session["member_nickname"] = entity.Member_NickName;





                string Supplier_Nickname = tools.CheckStr(Request.Form["Member_NickName"]);
                //string Supplier_Phone = tools.CheckStr(Request.Form["Member_Profile_Phone"]);
                //string Supplier_Profile_QQ = tools.CheckStr(Request.Form["Member_Profile_QQ"]);
                string Supplier_Contactman = tools.CheckStr(Request.Form["Member_Profile_Contactman"]);
                string Supplier_Profile_Phone = tools.CheckStr(Request.Form["Member_Profile_Phone"]);
                //string Supplier_Profile_CompanyName = tools.CheckStr(Request.Form["Member_Profile_CompanyName"]);
                string Supplier_Profile_CompanyName = Member_Company;
                string Supplier_Address = tools.CheckStr(Request.Form["Member_Profile_Address"]);

                string Supplier_Corporate = tools.CheckStr(Request.Form["Member_Corporate"]);
                string Supplier_ContactQQ = tools.CheckStr(Request.Form["Member_Profile_QQ"]);

                if (Supplier_Corporate == "")
                {
                    pub.Msg("info", "提示信息", "请填写法定代表人姓名", false, "{back}");
                }

                if (Member_Profile_Address == "")
                {
                    pub.Msg("info", "提示信息", "请将详细地址填写完整", false, "{back}");
                }





                SupplierInfo SupplierEntity = supplier.GetSupplierByID();
                if (SupplierEntity != null)
                {
                    SupplierEntity.Supplier_Nickname = Supplier_Nickname;
                    //SupplierEntity.Supplier_Phone = Supplier_Phone;

                    SupplierEntity.Supplier_Contactman = Supplier_Contactman;
                    SupplierEntity.Supplier_Phone = Supplier_Profile_Phone;
                    SupplierEntity.Supplier_CompanyName = Supplier_Profile_CompanyName;

                    SupplierEntity.Supplier_Address = Supplier_Address;
                    SupplierEntity.Supplier_Corporate = Supplier_Corporate;
                    SupplierEntity.Supplier_ContactQQ = Supplier_ContactQQ;
                    SupplierEntity.Supplier_AuditStatus = 0;
                    SupplierEntity.Supplier_Cert_Status = 0;
                }



                if (MySupplier.EditSupplier(SupplierEntity, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f")))
                {
                    string Supplier_Cert_1, Supplier_Cert_2, Supplier_Cert_3;
                    int Supplier_CertType;
                    Supplier_CertType = 0;
                    Supplier_Cert_1 = "";
                    Supplier_Cert_2 = "";
                    Supplier_Cert_3 = "";
                    Supplier_CertType = tools.CheckInt(Request["Supplier_CertType"]);
                    IList<SupplierCertInfo> certinfos = null;
                    SupplierRelateCertInfo ratecate = null;
                    //= GetSupplierByID();
                    SupplierInfo entity1 = supplier.GetSupplierByID(memberinfo.Member_SupplierID);
                    if (entity1 != null)
                    {

                        certinfos = supplier.GetSupplierCertByType(Supplier_CertType);
                        if (certinfos != null)
                        {
                            foreach (SupplierCertInfo certinfo in certinfos)
                            {
                                if (tools.CheckStr(Request["Supplier_Cert" + certinfo.Supplier_Cert_ID + "_tmp"]).Length == 0)
                                {
                                    //if (certinfo.Supplier_Cert_Name == "合同章(选填)")
                                    //{

                                    //}
                                    //else
                                    //{
                                    //    pub.Msg("info", "提示信息", "请将资质文件上传完整", false, "{back}");
                                    //}
                                }
                            }
                            //删除资质文件
                            MySupplier.DelSupplierRelateCertBySupplierID(entity1.Supplier_ID);
                            foreach (SupplierCertInfo certinfo in certinfos)
                            {
                                ratecate = new SupplierRelateCertInfo();
                                ratecate.Cert_SupplierID = entity1.Supplier_ID;
                                ratecate.Cert_CertID = certinfo.Supplier_Cert_ID;
                                if (tools.CheckStr(Request["Supplier_Cert" + certinfo.Supplier_Cert_ID]).Length == 0)
                                {
                                    ratecate.Cert_Img = tools.CheckStr(Request["Supplier_Cert" + certinfo.Supplier_Cert_ID + "_tmp"]);
                                    ratecate.Cert_Img1 = tools.CheckStr(Request["Supplier_Cert" + certinfo.Supplier_Cert_ID + "_tmp"]);
                                }
                                else
                                {
                                    ratecate.Cert_Img = tools.CheckStr(Request["Supplier_Cert" + certinfo.Supplier_Cert_ID]);
                                    ratecate.Cert_Img1 = tools.CheckStr(Request["Supplier_Cert" + certinfo.Supplier_Cert_ID + "_tmp"]);
                                }
                                MySupplier.AddSupplierRelateCert(ratecate);
                                ratecate = null;
                            }
                        }


                        entity1.Supplier_CertType = Supplier_CertType;
                        entity1.Supplier_Cert_Status = 1;
                        if (MySupplier.EditSupplier(entity1, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f")))
                        {
                            //pub.Msg("positive", "操作成功", "上传完成，请耐心等待审核通过！", true, "/supplier/Supplier_Cert.aspx");
                            //  Response.Write("success");
                            // Response.End();
                        }
                        else
                        {
                            pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
                            // Response.Write("failure");
                            // Response.End();
                        }
                    }
                    else
                    {
                        pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
                        //Response.Write("failure");
                        // Response.End();
                    }
                    pub.Msg("positive", "操作成功", "操作成功", true, "/member/account_profile_edit.aspx");
                }
                else
                {
                    pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
                }
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
        }
    }

    //公司 介绍信息
    public void UpdateMemberProfileSupplierIntro()
    {
        int account_id = tools.NullInt(Session["member_accountid"]);
        if (account_id > 0)
        {
            Response.Redirect("/member/index.aspx");
        }

        int Member_ID = tools.CheckInt(Request.Form["Member_ID"]);



        string Member_Company_Introduce = tools.CheckStr(Request.Form["Member_Company_Introduce"]);




        MemberInfo entity = GetMemberByID();
        if (entity != null)
        {


            entity.Member_Company_Introduce = Member_Company_Introduce;

            if (MyMember.EditMember(entity, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe")))
            {
                pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/account_profile_supplierIntro.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
            }
        }
    }


    //公司 联系信息
    public void UpdateMemberProfileSupplierContract()
    {
        int account_id = tools.NullInt(Session["member_accountid"]);
        if (account_id > 0)
        {
            Response.Redirect("/member/index.aspx");
        }

        int Member_ID = tools.CheckInt(Request.Form["Member_ID"]);



        string Member_Company_Contact = tools.CheckStr(Request.Form["Member_Company_Contact"]);




        MemberInfo entity = GetMemberByID();
        if (entity != null)
        {


            entity.Member_Company_Contact = Member_Company_Contact;

            if (MyMember.EditMember(entity, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe")))
            {
                pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/account_profile_Contract.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
            }
        }
    }

    //会员密码修改
    public void UpdateMemberPassword()
    {
        string old_pwd = tools.CheckStr(tools.NullStr(Request.Form["member_oldpassword"]));
        string member_password = tools.CheckStr(tools.NullStr(Request.Form["member_password"]));
        string member_password_confirm = tools.CheckStr(tools.NullStr(Request.Form["member_password_confirm"]));
        string verifycode = tools.CheckStr(tools.NullStr(Request.Form["verifycode"]));

        if (verifycode != Session["Trade_Verify"].ToString())
        {
            pub.Msg("info", "提示信息", "验证码输入错误", false, "{back}");
        }

        if (old_pwd == member_password)
        {
            pub.Msg("info", "提示信息", "新密码与原密码一样,请重新输入新密码", false, "{back}");
        }

        if (old_pwd == "")
        {
            pub.Msg("info", "提示信息", "请输入6～20位原密码", false, "{back}");
        }

        if (CheckSsn(member_password) == false)
        {
            pub.Msg("info", "提示信息", "密码包含特殊字符，只接受A-Z，a-z，0-9，不要输入空格", false, "{back}");
        }
        else
        {
            if (member_password.Length < 6 || member_password.Length > 20)
            {
                pub.Msg("info", "提示信息", "请输入6～20位新密码", false, "{back}");
            }
        }

        if (member_password != member_password_confirm)
        {
            pub.Msg("info", "提示信息", "两次密码输入不一致，请重新输入", false, "{back}");
        }

        old_pwd = encrypt.MD5(old_pwd);
        member_password = encrypt.MD5(member_password);


        MemberInfo memberinfo = new MemberInfo();
        memberinfo = MyMember.GetMemberByID(tools.CheckInt(Session["member_id"].ToString()), pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        if (memberinfo != null)
        {

            string Member_Password = memberinfo.Member_Password;

            memberinfo.Member_Password = member_password;

            if (old_pwd != Member_Password)
            {
                pub.Msg("info", "提示信息", "原密码输入错误，请重试！", false, "{back}");
            }
            if (MyMember.EditMember(memberinfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe")))
            {
                Response.Redirect("/member/account_password.aspx?tip=success");
            }
            else
            {
                pub.Msg("error", "错误信息", "密码修改失败，请稍后再试！", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "密码修改失败，请稍后再试！", false, "{back}");
        }
    }

    /// <summary>
    /// ERP用户绑定
    /// </summary>
    public void Erp_Binding()
    {
        string ERPuserid = tools.CheckStr(tools.NullStr(Request.Form["ERPuserid"]));
        string Pwd = tools.CheckStr(tools.NullStr(Request.Form["Pwd"]));
        string B2Buserid = "m" + tools.NullInt(Session["member_id"]);

        ERPBildingJsonInfo jsonInfo = null;

        if (ERPuserid == "")
        {
            Response.Write("请输入ERP用户ID");
            Response.End();
        }

        if (Pwd == "")
        {
            Response.Write("请输入ERP用户密码");
            Response.End();
        }

        string sign, sign_type;

        string gateway = erp_url + "/user/bindB2B";

        string[] parameters =
        {
            "B2Buserid="+B2Buserid,
            "ERPuserid="+ERPuserid,
            "Pwd="+Pwd
        };

        sign_type = "MD5";
        string[] return_parameters = pub.BubbleSort(parameters);

        StringBuilder per = new StringBuilder();
        int i = 0;
        for (i = 0; i <= return_parameters.Length - 1; i++)
        {
            if (i == return_parameters.Length - 1)
            {
                per.Append(return_parameters[i]);
            }
            else
            {
                per.Append(return_parameters[i] + "&");
            }
        }
        sign = securityUtil.signData(per.ToString(), sign_type);

        StringBuilder prestr = new StringBuilder();
        prestr.Append("{");
        prestr.Append("\"B2Buserid\":\"" + B2Buserid + "\",");
        prestr.Append("\"ERPuserid\":\"" + ERPuserid + "\",");
        prestr.Append("\"Pwd\":\"" + Pwd + "\",");
        prestr.Append("\"sign\":\"" + sign + "\",");
        prestr.Append("\"sign_type\":\"" + sign_type + "\"");
        prestr.Append("}");

        CookieCollection cookies = new CookieCollection();
        string strJson = HttpHelper.GetResponseString(HttpHelper.CreatePostJsonHttpResponse(gateway, prestr.ToString(), 0, "", cookies));

        jsonInfo = JsonHelper.JSONToObject<ERPBildingJsonInfo>(strJson);

        if (jsonInfo != null)
        {
            if (jsonInfo.result == true)
            {
                MemberInfo memberInfo = GetMemberByID();
                if (memberInfo != null)
                {
                    memberInfo.Member_ERP_StoreID = jsonInfo.storeid;
                    MyMember.EditMember(memberInfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));
                }

                Response.Write("success");
                Response.End();
            }
            else
            {
                if (jsonInfo.msg != null)
                {
                    Response.Write(jsonInfo.msg);
                    Response.End();
                }
                else
                {
                    if (jsonInfo.err != null)
                    {
                        Response.Write(jsonInfo.err.message);
                        Response.End();
                    }
                    else
                    {
                        Response.Write("用户绑定失败，请稍后再试......");
                        Response.End();
                    }
                }
            }
        }
        else
        {
            Response.Write("用户绑定失败，请稍后再试......");
            Response.End();
        }
    }

    public MemberAccountOrdersInfo GetMemberAccountOrdersByOrdersSN(string sn)
    {
        return MyAccountLog.GetMemberAccountOrdersByOrdersSN(sn);
    }

    public bool EditMemberAccountOrders(MemberAccountOrdersInfo entity)
    {
        return MyAccountLog.EditMemberAccountOrders(entity);
    }

    #endregion

    #region 采购商中心首页

    /// <summary>
    /// 采购商中心首页左侧账户信息
    /// </summary>
    /// <returns></returns>
    public string Member_Index_Left_Info()
    {
        StringBuilder strHTML = new StringBuilder();
        int Member_Emailverify = -1, Member_LoginMobileverify = -1;
        string MemCenter_Name = "";
        string Member_Email = "";
        string Member_LoginMobile = "";
        string company_name = "", member_img = "", member_nickname = "";
        MemberInfo entity = GetMemberByID();
        if (entity != null && entity.MemberProfileInfo != null)
        {
            company_name = entity.MemberProfileInfo.Member_Company;
            member_img = entity.MemberProfileInfo.Member_HeadImg;
            member_nickname = entity.Member_NickName;
            Member_Emailverify = entity.Member_Emailverify;
            Member_LoginMobileverify = entity.Member_LoginMobileverify;
            Member_LoginMobile = entity.Member_LoginMobile;
            Member_Email = entity.Member_Email;
            if (member_nickname.Length == 0)
            {
                MemCenter_Name = Member_Email;
            }
            else
            {
                MemCenter_Name = member_nickname;
            }
        }


        //strHTML.Append("<dt><a href=\"/member/account_profile.aspx\" target=\"_blank\">");

        //strHTML.Append("                     <img  src=\"" + pub.FormatImgURL(member_img, "fullpath") + "\" style=\"width:100px;height:100px;\" />");
        //strHTML.Append("                      我的资料</a></dt>");
        strHTML.Append("<dt><a href=\"/member/account_profile.aspx\" target=\"_blank\">");

        strHTML.Append("                     <img  src=\"" + pub.FormatImgURL(member_img, "fullpath") + "\" style=\"width:100px;height:100px;\" />");
        strHTML.Append("                      我的资料</a></dt>");
        strHTML.Append("                  <dd style=\"width: 600px;\">");
        strHTML.Append("                      <h2><strong>" + MemCenter_Name + "</strong>，欢迎回来！<a href=\"/member/order_address.aspx\">地址管理</a><a href=\"/member/account_password.aspx\">修改密码</a></h2>");



        strHTML.Append("                      <div class=\"clear\"></div>");
        if ((Member_LoginMobileverify == 1) && (Member_Emailverify == 1))
        {
            strHTML.Append("                      <div class=\"blk13_1_2\"><span>账户安全：</span><span><img src=\"/images/icon30_1.jpg\" width=\"130\" height=\"14\" /></span><span class=\"mal10\" style=\"color: #ff6600;\">高</span><span>");
        }
        else if ((Member_LoginMobileverify == 0) && (Member_Emailverify == 0))
        {
            strHTML.Append("                      <div class=\"blk13_1_2\"><span>账户安全：</span><span><img src=\"/images/icon30.jpg\" width=\"130\" height=\"14\" /></span><span class=\"mal10\" style=\"color: #ff6600;\">低</span><span>");
        }
        else
        {
            strHTML.Append("                      <div class=\"blk13_1_2\"><span>账户安全：</span><span><img src=\"/images/icon30.jpg\" width=\"130\" height=\"14\" /></span><span class=\"mal10\" style=\"color: #ff6600;\">中</span><span>");
        }




        if (Member_Emailverify == 0)
        {
            strHTML.Append(" <a href=\"emailverify.aspx\" class=\"email\">邮箱未绑定</a>");
        }
        else if (Member_Emailverify == 2)
        {
            strHTML.Append("    <span  class=\"email\">您的账户已被冻结！");
        }
        else
        {
            strHTML.Append("      <a href=\"/member/member_profile.aspx\" class=\"email\" target=\"_blank\">邮箱已绑定</a>");
        }




        if (Member_LoginMobileverify == 0)
        {
            strHTML.Append(" <a href=\"emailverify.aspx\" class=\"email\">手机未绑定</a>");
        }
        else if (Member_LoginMobileverify == 2)
        {
            strHTML.Append("    <span  class=\"email\">您的账户已被冻结！");
        }
        else
        {
            strHTML.Append("      <a href=\"/member/account_profile.aspx\" class=\"email\" target=\"_blank\">手机已绑定</a>");
        }

        strHTML.Append("         </span></div>");
        strHTML.Append("                      <div class=\"clear\"></div>");
        strHTML.Append("                      <div class=\"p_box\">");
        strHTML.Append("                          <ul>");
        strHTML.Append("                              <li><a href=\"/member/order_list.aspx?orderDate=1&orderStatus=payment\" target=\"_blank\">待支付(" + Member_Order_Count(tools.NullInt(Session["member_id"]), "payment") + ")</a></li>");
        strHTML.Append("                              <li><a href=\"/member/order_list.aspx?orderDate=1&orderStatus=accept\" target=\"_blank\">待签收(" + Member_Order_Count(tools.NullInt(Session["member_id"]), "accept") + ")</a></li>");
        strHTML.Append("                              <li style=\"border: none;\"><a href=\"/member/order_list.aspx?orderDate=1&orderStatus=unconfirm\" target=\"_blank\">待确认(" + Member_Order_Count(tools.NullInt(Session["member_id"]), "unconfirm") + ")</a></li>");
        strHTML.Append("                          </ul>");
        strHTML.Append("                          <div class=\"clear\"></div>");
        strHTML.Append("                      </div>");

        strHTML.Append("                  </dd>");
        strHTML.Append("                  <div class=\"clear\"></div>");

        return strHTML.ToString();
    }

    /// <summary>
    /// 采购商中心首页左侧常用功能
    /// </summary>
    /// <returns></returns>
    public string Member_Index_Left_Common()
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<div class=\"b10_main\">");
        strHTML.Append("<ul>");
        //strHTML.Append("<li><i>足</i><a href=\"#\" target=\"_blank\">足迹</a></li>");
        strHTML.Append("<li><i>已</i><a href=\"/member/order_list.aspx\">已买到的货品</a></li>");
        strHTML.Append("<li><i>优</i><a href=\"/member/member_coupon.aspx\">优惠券</a></li>");
        //strHTML.Append("<li><i>支</i><a href=\"#\">支出明细</a></li>");
        strHTML.Append("<li><i>售</i><a href=\"/member/feedback.aspx\">售后服务</a></li>");
        strHTML.Append("<li><i>信</i><a href=\"/member/loan_account.aspx\">信贷账户</a></li>");
        strHTML.Append("</ul>");
        strHTML.Append("</div>");

        return strHTML.ToString();
    }


    /// <summary>
    /// 采购商中心首页右侧订单状态信息
    /// </summary>
    /// <returns></returns>
    public string Member_Index_Right_OrdersInfo()
    {
        StringBuilder strHTML = new StringBuilder();
        int member_id = tools.NullInt(Session["member_id"]);

        strHTML.Append("<div class=\"blk11\">");
        strHTML.Append("<ul>");

        strHTML.Append("<li class=\"li03\">");
        strHTML.Append("<h2>交易前<span></span></h2>");
        strHTML.Append("<div class=\"li_main\">");
        strHTML.Append("<ul>");
        strHTML.Append("<li><img src=\"/images/icon09.jpg\"><a href=\"/member/order_list.aspx?orderStatus=unsupplierconfirm\"><span>" + Member_Order_Count(member_id, "unsupplierconfirm") + "</span>待卖家确认</a></li>");
        strHTML.Append("<li><img src=\"/images/icon10.jpg\"><a href=\"/member/order_list.aspx?orderStatus=loanapply\"><span>" + Member_Order_Count(member_id, "loanapply") + "</span>信贷申请中</a></li>");
        strHTML.Append("<li><img src=\"/images/icon11.jpg\"><a href=\"/member/order_list.aspx?orderStatus=unconfirm\"><span>" + Member_Order_Count(member_id, "unconfirm") + "</span>待确认</a></li>");
        strHTML.Append("</ul>");
        strHTML.Append("</div>");
        strHTML.Append("</li>");

        strHTML.Append("<li class=\"li03\">");
        strHTML.Append("<h2>交易中<span></span></h2>");
        strHTML.Append("<div class=\"li_main\">");
        strHTML.Append("<ul>");
        strHTML.Append("<li><img src=\"/images/icon12.jpg\"><a href=\"/member/order_list.aspx?orderStatus=payment\"><span>" + Member_Order_Count(member_id, "payment") + "</span>待支付</a></li>");
        strHTML.Append("<li><img src=\"/images/icon13.jpg\"><a href=\"/member/order_list.aspx?orderStatus=delivery\"><span>" + Member_Order_Count(member_id, "delivery") + "</span>待发货</a></li>");
        strHTML.Append("</ul>");
        strHTML.Append("</div>");
        strHTML.Append("</li>");

        strHTML.Append("<li class=\"li03\">");
        strHTML.Append("<h2>交易后<span></span></h2>");
        strHTML.Append("<div class=\"li_main\" style=\"border-right:none;\">");
        strHTML.Append("<ul>");
        strHTML.Append("<li><img src=\"/images/icon14.jpg\"><a href=\"/member/order_list.aspx?orderStatus=accept\"><span>" + Member_Order_Count(member_id, "accept") + "</span>待签收</a></li>");
        strHTML.Append("</ul>");
        strHTML.Append("</div>");
        strHTML.Append("</li>");

        strHTML.Append("</ul>");
        strHTML.Append("</div>");

        return strHTML.ToString();
    }


    /// <summary>
    /// 获取采购商等级
    /// </summary>
    /// <returns></returns>
    public string GetMemberGradeInfo()
    {
        string gradeName = "普通";

        MemberGradeInfo entity = Mygrade.GetMemberGradeByID(tools.NullInt(Session["member_grade"]), pub.CreateUserPrivilege("1c955ea6-881f-48d8-ba8d-c5aa7ce9cfea"));
        if (entity != null)
        {
            gradeName = entity.Member_Grade_Name;
        }

        return gradeName;
    }

    public string RecommendProduct(int Show_Num, string Tag_Name)
    {
        StringBuilder strHTML = new StringBuilder();

        ProductTagInfo Taginfo = MyTag.GetProductTagByValue(Tag_Name, pub.CreateUserPrivilege("ed87eb87-dade-4fbc-804c-c139c1cbe9c8"));
        if (Taginfo != null)
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = Show_Num;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", "SELECT Product_RelateTag_ProductID FROM Product_RelateTag WHERE Product_RelateTag_TagID = " + Taginfo.Product_Tag_ID + ""));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
            Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Sort", "asc"));
            Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "desc"));
            IList<ProductInfo> listProduct = MyProduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));

            if (listProduct != null)
            {
                foreach (ProductInfo entity in listProduct)
                {
                    strHTML.Append("<li><div class=\"img_box\">");
                    strHTML.Append("<a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Product_Img, "thumbnail") + "\"></a></div><p><a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString()) + "\" target=\"_blank\">" + tools.CutStr(entity.Product_Name, 18) + "</a></p>");
                    if (entity.Product_PriceType == 1)
                    {
                        strHTML.Append("<p>价格：" + pub.FormatCurrency(entity.Product_Price) + "</p>");
                    }
                    else
                    {
                        strHTML.Append("<p>价格：" + pub.FormatCurrency(pub.GetProductPrice(entity.Product_ManualFee, entity.Product_Weight)) + "</p>");
                    }
                    strHTML.Append("<p>成交：" + entity.Product_SaleAmount + "件</p>");
                    strHTML.Append("</li>");
                }
            }
        }

        return strHTML.ToString();
    }


    //用户留言添加
    public void AddFeedBack(int Flag)
    {
        int Feedback_ID = 0;
        int Feedback_Type = tools.CheckInt(Request.Form["Feedback_Type"]);
        int Feedback_MemberID = tools.CheckInt(Session["member_id"].ToString());
        string Feedback_Name = tools.CheckStr(Request.Form["Feedback_Name"]);
        string Feedback_Tel = tools.CheckStr(Request.Form["Feedback_Tel"]);
        string Feedback_Email = tools.CheckStr(Request.Form["Feedback_Email"]);
        string Feedback_Content = tools.CheckStr(Request.Form["Feedback_Content"]);
        DateTime Feedback_Addtime = DateTime.Now;
        int Feedback_IsRead = 0;
        int Feedback_Reply_IsRead = 0;
        string Feedback_Reply_Content = "";
        string Feedback_Site = "CN";
        string verifycode = tools.CheckStr(Request.Form["verifycode"]);

        if (verifycode != Session["Trade_Verify"].ToString() || verifycode.Length == 0)
        {
            pub.Msg("error", "错误提示", "验证码输入错误", false, "{back}");
        }
        if (Feedback_Name.Length < 1)
        {
            pub.Msg("info", "信息提示", "请填写姓名，以便于我们与您联系！", false, "{back}");
        }
        if (tools.CheckEmail(Feedback_Email) == false)
        {
            pub.Msg("info", "信息提示", "邮箱格式不正确！", false, "{back}");
        }
        if (pub.Checkmobile_phone(Feedback_Tel) == false)
        {
            pub.Msg("info", "信息提示", "联系电话格式不正确", false, "{back}");
        }
        if (Feedback_Content.Length < 1)
        {
            pub.Msg("info", "信息提示", "请输入留言内容！", false, "{back}");
        }

        FeedBackInfo entity = new FeedBackInfo();
        entity.Feedback_ID = Feedback_ID;
        entity.Feedback_Type = Feedback_Type;
        entity.Feedback_MemberID = Feedback_MemberID;
        entity.Feedback_Name = Feedback_Name;
        entity.Feedback_Tel = Feedback_Tel;
        entity.Feedback_Email = Feedback_Email;
        entity.Feedback_Content = Feedback_Content;
        entity.Feedback_Addtime = Feedback_Addtime;
        entity.Feedback_IsRead = Feedback_IsRead;
        entity.Feedback_Reply_IsRead = Feedback_Reply_IsRead;
        entity.Feedback_Reply_Content = Feedback_Reply_Content;
        entity.Feedback_Site = Feedback_Site;

        if (MyFeedback.AddFeedBack(entity, pub.CreateUserPrivilege("8ccafb10-8a4a-425f-8111-a1e4eb46a0b4")))
        {
            if (Flag == 1)
            {
                if (Feedback_Type == 2)
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "/Financial/financialservice.aspx");
                }
                else
                {
                    Response.Redirect("/member/feedback.aspx?tip=success");
                }

            }
            if (Flag == 0)
            {
                Response.Redirect("/help/feedback.aspx?tip=success");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }

    }




    //用户留言添加 金融中心
    public void AddFeedBack_Fin(int Flag)
    {
        int Feedback_ID = 0;
        int Feedback_Type = tools.CheckInt(Request.Form["Feedback_Type"]);
        int Feedback_MemberID = tools.CheckInt(Session["member_id"].ToString());
        string Feedback_Name = tools.CheckStr(Request.Form["Feedback_Name"]);
        string Feedback_Tel = tools.CheckStr(Request.Form["Feedback_Tel"]);
        string Feedback_Email = tools.CheckStr(Request.Form["Feedback_Email"]);
        string Feedback_Content = tools.CheckStr(Request.Form["Feedback_Content"]);
        string Feedback_CompanyName = "";
        if (Flag == 1)
        {
            Feedback_CompanyName = tools.CheckStr(Request.Form["Feedback_CompanyName"]);
        }
        string Feedback_Address = tools.CheckStr(Request.Form["Feedback_Address"]);
        double Feedback_Amount = tools.CheckFloat(Request.Form["Feedback_Amount"]);
        string Feedback_Attachment = tools.CheckStr(Request.Form["Feedback_Attachment"]);



        DateTime Feedback_Addtime = DateTime.Now;
        int Feedback_IsRead = 0;
        int Feedback_Reply_IsRead = 0;
        string Feedback_Reply_Content = "";
        string Feedback_Site = "CN";
        string verifycode = tools.CheckStr(Request.Form["verifycode"]);

        if (verifycode != Session["Trade_Verify"].ToString() || verifycode.Length == 0)
        {
            pub.Msg("error", "错误提示", "验证码输入错误", false, "{back}");
        }
        if (Feedback_Name.Length < 1)
        {
            pub.Msg("info", "信息提示", "请填写姓名，以便于我们与您联系！", false, "{back}");
        }
        //if (tools.CheckEmail(Feedback_Email) == false)
        //{
        //    pub.Msg("info", "信息提示", "邮箱格式不正确！", false, "{back}");
        //}
        //if (pub.Checkmobile_phone(Feedback_Tel) == false)
        //{
        //    pub.Msg("info", "信息提示", "联系电话格式不正确", false, "{back}");
        //}
        if (Feedback_Tel.Length < 1)
        {
            pub.Msg("info", "信息提示", "请输入联系方式！", false, "{back}");
        }
        if (Feedback_Content.Length < 1)
        {
            pub.Msg("info", "信息提示", "请输入留言内容！", false, "{back}");
        }

        FeedBackInfo entity = new FeedBackInfo();
        entity.Feedback_ID = Feedback_ID;
        entity.Feedback_Type = Feedback_Type;
        entity.Feedback_MemberID = Feedback_MemberID;
        entity.Feedback_Name = Feedback_Name;
        entity.Feedback_Tel = Feedback_Tel;
        entity.Feedback_Email = Feedback_Email;
        entity.Feedback_Content = Feedback_Content;
        entity.Feedback_Addtime = Feedback_Addtime;
        entity.Feedback_IsRead = Feedback_IsRead;
        entity.Feedback_Reply_IsRead = Feedback_Reply_IsRead;
        entity.Feedback_Reply_Content = Feedback_Reply_Content;
        entity.Feedback_Site = Feedback_Site;
        entity.Feedback_Address = Feedback_Address;
        entity.Feedback_Amount = Feedback_Amount;
        entity.Feedback_Attachment = Feedback_Attachment;
        if (Flag == 1)
        {
            entity.Feedback_CompanyName = Feedback_CompanyName;
        }





        if (MyFeedback.AddFeedBack(entity, pub.CreateUserPrivilege("8ccafb10-8a4a-425f-8111-a1e4eb46a0b4")))
        {
            if (Flag == 1)
            {
                if (Feedback_Type != 1)
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "/Financial/financialservice.aspx");
                }
                else
                {
                    Response.Redirect("/member/feedback.aspx?tip=success");
                }

            }
            if (Flag == 0)
            {
                Response.Redirect("/help/feedback.aspx?tip=success");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }

    }

    //用户留言列表
    public void Feedback_List()
    {
        StringBuilder strHTML = new StringBuilder();
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        Pageurl = "?action=list";
        string icon_alt = "";
        string icon = "";
        if (curpage < 1)
        {
            curpage = 1;
        }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "FeedBackInfo.Feedback_MemberID", "=", member_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("FeedBackInfo.Feedback_ID", "Desc"));
        IList<FeedBackInfo> entitys = MyFeedback.GetFeedBacks(Query, pub.CreateUserPrivilege("9877a09e-5dda-4b1e-bf6f-042504449eeb"));
        PageInfo page = MyFeedback.GetPageInfo(Query, pub.CreateUserPrivilege("9877a09e-5dda-4b1e-bf6f-042504449eeb"));



        strHTML.Append("<table width=\"973\" border=\"0\" cellspacing=\"2\" cellpadding=\"0\">");
        strHTML.Append("<tr>");
        strHTML.Append("<td class=\"name\">留言编号</td>");
        strHTML.Append("<td class=\"name\">类型</td>");
        strHTML.Append("<td width=\"500\" class=\"name\">内容</td>");
        strHTML.Append("<td class=\"name\">发布时间</td>");
        strHTML.Append("</tr>");

        if (entitys != null)
        {
            int j = 0;
            foreach (FeedBackInfo entity in entitys)
            {
                i = i + 1;
                j++;
                switch (entity.Feedback_Type)
                {
                    //case 1:
                    //    icon_alt = "简单的留言";
                    //    break;
                    //case 2:
                    //    icon_alt = "对网站的意见";
                    //    break;
                    //case 3:
                    //    icon_alt = "对公司的建议";
                    //    break;
                    //case 4:
                    //    icon_alt = "具有合作意向";
                    //    break;
                    //case 5:
                    //    icon_alt = "商品投诉";
                    //    break;
                    //case 6:
                    //    icon_alt = "服务投诉";
                    //    break;

                    case 1:
                        icon_alt = "网站留言";
                        break;
                    case 2:
                        icon_alt = "金融服务留言";
                        break;

                }

                i++;
                if (i % 2 == 0)
                {
                    strHTML.Append("<tr class=\"bg\">");
                }
                else
                {
                    strHTML.Append("<tr>");
                }
                strHTML.Append("<td><span><a href=\"/member/feedback_view.aspx?feedback_id=" + entity.Feedback_ID + "\">第" + j + "条</a></span></td>");
                strHTML.Append("<td>" + icon_alt + "</td>");
                strHTML.Append("<td>" + entity.Feedback_Content + "</td>");
                strHTML.Append("<td>" + entity.Feedback_Addtime.ToString("yyyy-MM-dd HH:mm:ss") + "</td>");
                strHTML.Append("</tr>");
            }
            strHTML.Append("</table>");
            Response.Write(strHTML.ToString());
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
        }
        else
        {

            strHTML.Append("<tr><td align=\"center\" class=\"t12_grey\" colspan=\"3\">暂无记录</td></tr>");
            strHTML.Append("</table>");
            Response.Write(strHTML.ToString());
        }
    }


    #endregion

    #region 订单管理

    #region 订单相关状态

    public string OrdersType(int typeid)
    {
        string resultstr = string.Empty;
        switch (typeid)
        {
            case 1:
                resultstr = "现货采购订单"; break;
            case 2:
                resultstr = "定制采购订单"; break;
            case 3:
                resultstr = "代理采购订单"; break;
        }
        return resultstr;
    }

    public string OrdersInvoiceType(int typeid)
    {
        string resultstr = string.Empty;
        switch (typeid)
        {
            case 0:
                resultstr = "不需要发票"; break;
            case 1:
                resultstr = "普通发票"; break;
            case 2:
                resultstr = "增值税发票"; break;
        }
        return resultstr;
    }

    public string OrdersStatus(int status, int Payment_Status, int Delivery_Status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "待确认";
                break;
            case 1:
                if (Delivery_Status == 0)
                {
                    resultstr = PaymentStatus(Payment_Status);
                }
                else
                {
                    resultstr = DeliveryStatus(Delivery_Status);
                }
                break;
            case 2:
                resultstr = "交易成功"; break;
            case 3:
                resultstr = "交易失败"; break;
            case 4:
                resultstr = "申请退换货"; break;
            case 5:
                resultstr = "申请退款"; break;
        }
        return resultstr;
    }

    public string PaymentStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "未支付"; break;
            case 1:
                resultstr = "已支付"; break;
            case 2:
                resultstr = "已退款"; break;
            case 3:
                resultstr = "退款处理中"; break;
            case 4:
                resultstr = "已到帐"; break;
        }
        return resultstr;
    }

    public string DeliveryStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "未发货"; break;
            case 1:
                resultstr = "已发货"; break;
            case 2:
                resultstr = "已签收"; break;
            case 3:
                resultstr = "已拒收"; break;
            case 4:
                resultstr = "退货处理中"; break;
            case 5:
                resultstr = "已退货"; break;
            case 6:
                resultstr = "配货中"; break;

        }
        return resultstr;
    }

    public string InvoiceStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "未开票"; break;
            case 1:
                resultstr = "已开票"; break;
            case 2:
                resultstr = "已退票"; break;
            case 3:
                resultstr = "不需要发票"; break;
        }
        return resultstr;
    }

    public string OrdersSupplierStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "待卖方确认"; break;
        }
        return resultstr;
    }

    public string OrdersMemberStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "待确认"; break;
        }
        return resultstr;
    }

    #endregion


    public string CreateContractName(string Orders_SN)
    {
        string fileName = "采购订单" + Orders_SN + "电子合同";
        return fileName;
    }

    private string CreateContractSN()
    {
        Random ran = new Random();
        int intstr = (int)ran.Next(9);
        return DateTime.Now.ToString("yyyyMMddHHmmss") + intstr.ToString();
    }

    public OrdersInfo GetOrdersInfoBySN(string SN)
    {
        return MyOrders.GetOrdersBySN(SN);
    }

    public int Member_Order_Count(int member_id, string count_type)
    {
        int Order_Count = 0;
        if (member_id > 0)
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 1;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_BuyerID", "=", member_id.ToString()));

            switch (count_type)
            {
                case "unsupplierconfirm":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_SupplierStatus", "=", "0"));
                    break;
                case "unconfirm":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "0"));
                    break;
                case "loanapply":
                    Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_Loan_proj_no", "<>", ""));
                    break;
                case "payment":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "1"));
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_PaymentStatus", "=", "0"));
                    break;
                case "delivery":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_PaymentStatus", "=", "1"));
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_DeliveryStatus", "=", "0"));
                    break;
                case "accept":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_DeliveryStatus", "=", "1"));
                    break;
                case "unprocessed":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "0"));
                    break;
                case "processing":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "1"));
                    break;
                case "success":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "2"));
                    break;
                case "faiture":
                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "3"));
                    break;
            }
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_IsShow", "=", "1"));
            PageInfo page = MyOrders.GetPageInfo(Query);
            if (page != null)
            {
                Order_Count = page.RecordCount;
            }
        }
        return Order_Count;
    }

    /// <summary>
    /// 订单页选项卡
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public string Orders_TabControl(string type)
    {
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        string orderstype = tools.CheckStr(Request["orders_type"]);
        string date_start = tools.CheckStr(Request["date_start"]);
        string date_end = tools.CheckStr(Request["date_end"]);
        string orders_sn;
        orders_sn = tools.CheckStr(Request["orders_sn"]);
        int PriceReport_ID = tools.CheckInt(Request["PriceReport_ID"]);
        Session["Tab_PriceReport_ID"] = PriceReport_ID;

        string queryurl = "orders_type=" + orderstype + "&orders_sn=" + orders_sn + "&date_start=" + date_start + "&date_end=" + date_end + "&PriceReport_ID=" + PriceReport_ID;

        StringBuilder strHTML = new StringBuilder();
        strHTML.Append("<ul class=\"zkw_lst31\">");
        strHTML.Append("	<li " + (type == "all" ? "class=\"on\"" : "") + " id=\"n01\"><a href=\"?" + queryurl + "\">全部订单(<span>" + Member_Order_Count(member_id, "all") + "</span>)</a></li>");
        strHTML.Append("	<li " + (type == "unprocessed" ? "class=\"on\"" : "") + " id=\"n02\"><a href=\"?" + queryurl + "&type=unprocessed\">未处理的订单(<span>" + Member_Order_Count(member_id, "unprocessed") + "</span>)</a></li>");
        strHTML.Append("	<li " + (type == "processing" ? "class=\"on\"" : "") + " id=\"n02\"><a href=\"?" + queryurl + "&type=processing\">处理中的订单(<span>" + Member_Order_Count(member_id, "processing") + "</span>)</a></li>");
        strHTML.Append("	<li " + (type == "success" ? "class=\"on\"" : "") + " id=\"n04\"><a href=\"?" + queryurl + "&type=success\">交易成功的订单(<span>" + Member_Order_Count(member_id, "success") + "</span>)</a></li>");
        strHTML.Append("	<li " + (type == "faiture" ? "class=\"on\"" : "") + " id=\"n05\"><a href=\"?" + queryurl + "&type=faiture\">交易失败的订单(<span>" + Member_Order_Count(member_id, "faiture") + "</span>)</a></li>");
        strHTML.Append("</ul><div class=\"clear\"></div>");
        Session["Tab_PriceReport_ID"] = "0";

        return strHTML.ToString();
    }

    /// <summary>
    /// 订单列表
    /// </summary>
    /// <param name="type"></param>
    public void Orders_List(string type)
    {
        int member_id = tools.NullInt(Session["member_id"]);

        string tmp_head, tmp_list, tmp_splitline, tmp_norecord, tmp_toolbar, tmp_toolbar_bottom, form_action;
        tmp_head = "";
        tmp_list = "";
        tmp_splitline = "";
        tmp_norecord = "";
        tmp_toolbar = "";
        tmp_toolbar_bottom = "";
        form_action = "";
        string date_start, date_end, orders_sn, info, page_url, Orders_Status, keyword, orderStatus, pay_operating, order_operating;
        int curpage = 0, orderDate = 0;
        keyword = tools.CheckStr(Request["keyword"]);
        date_start = tools.CheckStr(Request["date_start"]);
        date_end = tools.CheckStr(Request["date_end"]);
        orders_sn = tools.CheckStr(Request["orders_sn"]);
        orderDate = tools.CheckInt(Request["orderDate"]);
        orderStatus = tools.CheckStr(Request["orderStatus"]);
        curpage = tools.CheckInt(Request["page"]);
        int orders_type = tools.CheckInt(Request["orders_type"]);
        int PriceReport_ID = tools.CheckInt(Request["PriceReport_ID"]);
        if (curpage < 1)
        {
            curpage = 1;
        }
        page_url = "?keyword=" + keyword + "&date_start=" + date_start + "&date_end=" + date_end + "&orders_type=" + orders_type + "&orderDate=" + orderDate + "&orderStatus=" + orderStatus;

        if (orderDate == 0)
        {
            orderDate = 1;
        }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 5;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_BuyerID", "=", member_id.ToString()));

        if (PriceReport_ID > 0)
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_PriceReportID", "=", PriceReport_ID.ToString()));

        switch (orderStatus)
        {
            case "unsupplierconfirm":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_SupplierStatus", "=", "0"));
                break;
            case "unconfirm":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "0"));
                break;
            case "loanapply":
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersInfo.Orders_Loan_proj_no", "<>", ""));
                break;
            case "payment":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "1"));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_PaymentStatus", "=", "0"));
                break;
            case "delivery":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_PaymentStatus", "=", "1"));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_DeliveryStatus", "=", "0"));
                break;
            case "accept":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_DeliveryStatus", "=", "1"));
                break;
            case "unprocessed":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "0"));
                break;
            case "processing":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "1"));
                break;
            case "success":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "2"));
                break;
            case "faiture":
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Status", "=", "3"));
                break;
        }


        DateTime dt = DateTime.Now;
        switch (orderDate)
        {
            case 1:
                date_start = dt.AddMonths(-3).ToString();
                date_end = dt.ToString();
                break;
            case 2:
                date_start = new DateTime(dt.Year, 1, 1).ToString();
                date_end = dt.ToString();
                break;
            case 2016:
                date_start = new DateTime(orderDate, 1, 1).ToString();
                date_end = new DateTime(orderDate, 12, 31).ToString();
                break;
            case 2015:
                date_start = new DateTime(orderDate, 1, 1).ToString();
                date_end = new DateTime(orderDate, 12, 31).ToString();
                break;
            case 3:
                date_end = new DateTime(2014, 12, 31).ToString();
                break;
        }

        if (date_start != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{OrdersInfo.Orders_Addtime},'" + Convert.ToDateTime(date_start) + "')", "<=", "0"));
        }

        if (date_end != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{OrdersInfo.Orders_Addtime},'" + Convert.ToDateTime(date_end) + "')", ">=", "0"));
        }


        if (orders_type > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_Type", "=", orders_type.ToString()));
        }

        if (keyword != "" && keyword != "订单号/供应商/商品名称/商品编号")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "OrdersInfo.Orders_SN", "like", keyword));

            Query.ParamInfos.Add(new ParamInfo("OR", "str", "OrdersInfo.Orders_ID", "in", "select distinct Orders_Goods_OrdersID from Orders_Goods as OG where OG.Orders_Goods_Product_Name like '%" + keyword + "%' "));

            Query.ParamInfos.Add(new ParamInfo("OR", "str", "OrdersInfo.Orders_ID", "in", "select distinct Orders_Goods_OrdersID from Orders_Goods as OG where OG.Orders_Goods_Product_Code like '%" + keyword + "%' "));


            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "OrdersInfo.Orders_SupplierID", "in", "select Shop_SupplierID from Supplier_Shop where Shop_Name like '%" + keyword + "%'"));

        }
        //筛选出不允许出现且处理失败的订单
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersInfo.Orders_IsShow", "=", "1"));


        Query.OrderInfos.Add(new OrderInfo("OrdersInfo.Orders_ID", "Desc"));
        IList<OrdersInfo> orderslist = MyOrders.GetOrderss(Query);
        PageInfo page = MyOrders.GetPageInfo(Query);

        StringBuilder sb = new StringBuilder();

        sb.Append("<div class=\"blk04_sz\">");
        //sb.Append("<from name=\"\" id=\"\" method=\"\" action=\"\">");
        sb.Append("<select name=\"orderStatus\" id=\"orderStatus\"  onchange=\"OrderStatus()\">");
        sb.Append("<option value=\"normal\" " + (orderStatus == "normal" ? "selected" : "") + ">全部订单</option>");
        sb.Append("<option value=\"unconfirm\" " + (orderStatus == "unconfirm" ? "selected" : "") + ">待确认</option>");
        sb.Append("<option value=\"payment\" " + (orderStatus == "payment" ? "selected" : "") + ">待支付</option>");
        sb.Append("<option value=\"delivery\" " + (orderStatus == "delivery" ? "selected" : "") + ">待发货</option>");
        sb.Append("<option value=\"accept\" " + (orderStatus == "accept" ? "selected" : "") + ">待签收</option>");
        sb.Append("<option value=\"success\" " + (orderStatus == "success" ? "selected" : "") + ">交易成功</option>");
        sb.Append("<option value=\"faiture\" " + (orderStatus == "faiture" ? "selected" : "") + ">交易失败</option>");
        sb.Append("</select>");
        sb.Append("<select name=\"orderDate\" id=\"orderDate\" onchange=\"OrderDate()\">");
        sb.Append("<option value=\"1\" " + (orderDate == 1 ? "selected" : "") + ">近期订单</option>");
        sb.Append("<option value=\"2\" " + (orderDate == 2 ? "selected" : "") + ">今年内</option>");
        sb.Append("<option value=\"2016\" " + (orderDate == 2016 ? "selected" : "") + ">2016年</option>");
        sb.Append("<option value=\"2015\" " + (orderDate == 2015 ? "selected" : "") + ">2015年</option>");
        sb.Append("<option value=\"3\" " + (orderDate == 3 ? "selected" : "") + ">2014年以前</option>");
        sb.Append("</select>");
        sb.Append("<input name=\"orders_keyword\" id=\"orders_keyword\" type=\"text\"");
        if (keyword != "")
        {
            sb.Append(" value=\"" + keyword + "\" ");
        }
        else
        {
            sb.Append(" value=\"订单号/供应商/商品名称/商品编号\"");
        }

        sb.Append(" onfocus=\"if (this.value=='订单号/供应商/商品名称/商品编号') this.value=''\" /><a href=\"javascript:void();\" onclick=\"OrderSearch('orders_keyword');\">搜 索</a>");
        //sb.Append("");
        sb.Append("</div>");

        sb.Append("<div class=\"blk05_sz\">");
        sb.Append("<ul>");
        sb.Append("<li style=\"margin-left: 35px;\">订单商品</li>");
        sb.Append("<li style=\"margin-left: 225px;\">数量</li>");
        sb.Append("<li style=\"margin-left: 80px;\">订单状态</li>");
        sb.Append("<li style=\"margin-left: 100px;\">总金额</li>");
        //sb.Append("<li style=\"margin-left: 160px;\">支付状态</li>");
        sb.Append("<li style=\"margin-left: 230px;\">操作</li>");
        sb.Append("</ul>");
        sb.Append("<div class=\"clear\"></div>");
        sb.Append("</div>");

        if (orderslist != null)
        {
            IList<OrdersGoodsInfo> goodsList;
            SupplierInfo supplierInfo = null;
            SupplierShopInfo shopInfo = null;
            string supplierName = "--";
            string shopURL = string.Empty;
            bool IsBid = false;
            foreach (OrdersInfo entity in orderslist)
            {
                goodsList = MyOrders.GetGoodsListByOrderID(entity.Orders_ID);
                #region 商家信息

                supplierName = "--";
                shopURL = string.Empty;

                shopInfo = supplier.GetSupplierShopBySupplierID(entity.Orders_SupplierID);
                if (shopInfo != null)
                {
                    supplierName = shopInfo.Shop_Name;
                    shopURL = supplier.GetShopURL(shopInfo.Shop_Domain);
                }

                #endregion

                sb.Append("<div class=\"table_blk02\">");
                sb.Append("<table width=\"975\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\">");

                sb.Append("<tr>");
                sb.Append("<td colspan=\"6\" class=\"name\">");
                sb.Append("<input name=\"\" type=\"checkbox\" value=\"\" /><span><a href=\"/member/order_view.aspx?orders_sn=" + entity.Orders_SN + "\" target=\"_blank\">订单编号：" + entity.Orders_SN + "</a></span><span>提交时间：" + entity.Orders_Addtime.ToString("yyyy-MM-dd HH:mm:ss") + "</span>");
                if (shopInfo != null)
                {
                    sb.Append("      <a href=\"" + pub.GetShopDomain(shopInfo.Shop_Domain) + "\">卖方：" + supplierName + "</a>");
                }

                sb.Append("  <span onclick=\"update_erpparamsession(" + entity.Orders_ID + ");NTKF.im_openInPageChat('sz_" + (entity.Orders_SupplierID + 1000) + "_9999');\"></span></td>");
                sb.Append("</tr>");
                if (goodsList != null)
                {
                    sb.Append(Orders_List_Goods(entity, goodsList));
                }


                sb.Append("</table>");

                sb.Append("</div>");
            }
            Response.Write(sb.ToString());
            Response.Write("<div class=\"blk08\" style=\"border:0;\">");
            if (page != null)
            {
                pub.Page(page.PageCount, page.CurrentPage, page_url, page.PageSize, page.RecordCount);
            }

            Response.Write("</div>");
        }
        else
        {
            sb.Append("<div class=\"table_blk02\">");
            sb.Append("<table width=\"975\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\">");

            sb.Append("<tr>");
            sb.Append("<td colspan=\"6\" style=\"text-align:center;\">");
            sb.Append("暂无订单信息！");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");

            sb.Append("</div>");
            Response.Write(sb.ToString());
        }

    }

    /// <summary>
    /// 订单列表商品
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="goodslist"></param>
    /// <returns></returns>
    public string Orders_List_Goods(OrdersInfo entity, IList<OrdersGoodsInfo> goodslist)
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sub = new StringBuilder();
        int i = 0;


        QueryLoanProjectJsonInfo JsonInfo = null;

        if (entity.Orders_Loan_proj_no != "")
        {
            JsonInfo = credit.QueryLoanProject("M" + tools.NullStr(Session["member_id"]), entity.Orders_Loan_proj_no, "", 0, 0);
        }
        else
        {
            JsonInfo = new QueryLoanProjectJsonInfo();
        }

        sb.Append("<tr>");
        if (goodslist != null)
        {
            foreach (OrdersGoodsInfo goods in goodslist)
            {
                if (goods.Orders_Goods_Product_ID > 0)
                {
                    i++;
                    if (i == 1)
                    {
                        sb.Append("<td width=\"362\" class=\"td02\">");
                        sb.Append("<dl>");
                        sb.Append("<input name=\"\" type=\"checkbox\" value=\"\" />");
                        sb.Append("<dt><a href=\"/product/detail.aspx?product_id=" + goods.Orders_Goods_Product_ID + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(goods.Orders_Goods_Product_Img, "thumbnail") + "\"></a></dt>");
                        sb.Append("<dd>");
                        sb.Append("<a href=\"/product/detail.aspx?product_id=" + goods.Orders_Goods_Product_ID + "\" target=\"_blank\">" + goods.Orders_Goods_Product_Name + "</a>");
                        sb.Append("</dd>");
                        sb.Append("<span>" + goods.Orders_Goods_Amount + "</span>");
                        sb.Append("<div class=\"clear\"></div>");
                        sb.Append("</dl>");
                        sb.Append("</td>");
                    }
                    else
                    {
                        sub.Append("<tr>");
                        sub.Append("<td class=\"td02\">");
                        sub.Append("<dl>");
                        sub.Append("<input name=\"\" type=\"checkbox\" value=\"\" />");
                        sub.Append("<dt><a href=\"/product/detail.aspx?product_id=" + goods.Orders_Goods_Product_ID + "\" target=\"_blank\">");
                        sub.Append("<img src=\"" + pub.FormatImgURL(goods.Orders_Goods_Product_Img, "thumbnail") + "\"></a></dt>");
                        sub.Append("<dd>");
                        sub.Append("<a href=\"/product/detail.aspx?product_id=" + goods.Orders_Goods_Product_ID + "\" target=\"_blank\">" + goods.Orders_Goods_Product_Name + "</a>");
                        sub.Append("</dd>");
                        sub.Append("<span>" + goods.Orders_Goods_Amount + "</span>");
                        sub.Append("<div class=\"clear\"></div>");
                        sub.Append("</dl>");
                        sub.Append("</td>");
                        sub.Append("</tr>");
                    }
                }
            }
        }
        string Orders_Delivery_Num = "select COUNT(*) from Orders_Delivery  where Orders_Delivery_OrdersID=" + entity.Orders_ID + "";

        int OrdersDeliveryNum = tools.NullInt(DBHelper.ExecuteScalar(Orders_Delivery_Num));


        sb.Append("<td width=\"98\" rowspan=\"" + goodslist.Count + "\">");
        sb.Append("<p>" + OrdersStatus(entity.Orders_Status, entity.Orders_PaymentStatus, entity.Orders_DeliveryStatus) + "</p><p>" + entity.Orders_Payway_Name + "</p><a href=\"/member/order_delivery_list.aspx?order_id=" + entity.Orders_ID + "\"><p style=\"color:#ff6600\">收货单数(" + OrdersDeliveryNum + ")个</p></a>");
        sb.Append("</td>");
        sb.Append("<td width=\"219\" style=\"text-align:center;\" rowspan=\"" + goodslist.Count + "\">");
        sb.Append("<p><strong>" + pub.FormatCurrency(entity.Orders_Total_AllPrice) + "</strong></p>");
        //sb.Append("<p>(含运费：" + pub.FormatCurrency(entity.Orders_Total_Freight + entity.Orders_Total_FreightDiscount) + ")</p>");
        sb.Append("<p>原价:" + pub.FormatCurrency(entity.Orders_Total_Price) + "</p>");
        //sb.Append("<p>调价原因：" + entity.Orders_Total_PriceDiscount_Note + "</p>");
        sb.Append("</td>");

        //sb.Append("<td width=\"107\" rowspan=\"" + goodslist.Count + "\"><i>" + PaymentStatus(entity.Orders_PaymentStatus) + "</i></td>");

        sb.Append("<td width=\"180\" rowspan=\"" + goodslist.Count + "\">");
        if (entity.Orders_PaymentStatus == 0 && entity.Orders_Status == 1)
        {
            sb.Append("<a href=\"/member/order_payment.aspx?orders_sn=" + entity.Orders_SN + "\" target=\"_blank\" class=\"a07\" style=\"display:block;text-align:center;line-height: 24px;\">付款</a>");
        }




        if (entity.Orders_Status == 2 && entity.Orders_IsEvaluate == 0)
        {
            sb.Append("<a href=\"/member/order_evaluate.aspx?orders_sn=" + entity.Orders_SN + "\"  class=\"a07\" style=\"display:block;text-align:center;line-height: 24px;\">去评价</a>");
        }

        Glaer.Trade.B2C.BLL.ORD.IOrdersDelivery BllOrdersDelivery = Glaer.Trade.B2C.BLL.ORD.OrdersDeliveryFactory.CreateOrdersDelivery();
        IList<Glaer.Trade.B2C.Model.OrdersDeliveryInfo> listEntityOdersDelivery = new List<OrdersDeliveryInfo>();
        QueryInfo query = new QueryInfo();
        query.PageSize = 0;
        query.CurrentPage = 1;
        query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersDeliveryInfo.Orders_Delivery_OrdersID", "=", entity.Orders_ID.ToString()));
        listEntityOdersDelivery = BllOrdersDelivery.GetOrdersDeliverys(query, pub.CreateUserPrivilege("f606309a-2aa9-42e3-9d45-e0f306682a29"));
        // EntityOdersDelivery = BllOrdersDelivery.GetOrdersDeliveryByOrdersID(entity.Orders_ID, 1, pub.CreateUserPrivilege("f606309a-2aa9-42e3-9d45-e0f306682a29"));

        bool IsSuccess = false;
        if (listEntityOdersDelivery != null)
        {
            foreach (var item in listEntityOdersDelivery)
            {
                if (item != null)
                {
                    if (item.Orders_Delivery_ReceiveStatus == 9 && IsSuccess == false)
                    {
                        //Glaer.Trade.B2C.BLL.ORD.IOrdersDelivery BllOrdersDelivery = Glaer.Trade.B2C.BLL.ORD.OrdersDeliveryFactory.CreateOrdersDelivery();
                        //Glaer.Trade.B2C.Model.OrdersDeliveryInfo EntityOdersDelivery = new OrdersDeliveryInfo();
                        //EntityOdersDelivery = BllOrdersDelivery.GetOrdersDeliveryBySn(entity.Orders_SN, pub.CreateUserPrivilege("f606309a-2aa9-42e3-9d45-e0f306682a29"));
                        sb.Append("<a href=\"/member/order_delivery_view.aspx?Orders_Delivery_ID=" + item.Orders_Delivery_ID + "\"  class=\"a07\" style=\"display:block;text-align:center;line-height: 24px;\">确认签收</a>");
                        IsSuccess = true;
                    }
                }

            }
            //当卖家申请结算时，买家显示确认结算

        }
        sb.Append("<a href=\"/member/Order_Contract_View.aspx?orders_sn=" + entity.Orders_SN + "\" target=\"_blank\" class=\"a07\" style=\"display:block;text-align:center;line-height: 24px;\">合同预览</a>");
        sb.Append("<a href=\"/member/order_view.aspx?orders_sn=" + entity.Orders_SN + "\" target=\"_blank\" class=\"a07\" style=\"display:block;text-align:center;line-height: 24px;\">查看订单</a>");

        //只有失败的订单才能删除 (只是逻辑删除:即隐藏订单)
        if (entity.Orders_Status == 3)
        {
            ///member/orders_do.aspx?action=orderfirm&Orders_ID=" + entity.Orders_ID + "\"
            sb.Append("<a href=\"/member/orders_do.aspx?action=order_delete&Orders_ID=" + entity.Orders_ID + "\" target=\"_blank\" class=\"a07\" style=\"display:block;text-align:center;line-height: 24px;\">删除订单</a>");
        }


        if (tools.NullInt(Session["member_ID"]) == entity.Orders_BuyerID && entity.Orders_Status == 0)
        {
            sb.Append("<a href=\"/member/order_close.aspx?orders_sn=" + entity.Orders_SN + "\" class=\"a07\" style=\"display:block;text-align:center;line-height: 24px;\">取消订单</a>");
        }
        sb.Append("</td>");

        sb.Append("</tr>");

        sb.Append(sub);

        return sb.ToString();
    }


    /// <summary>
    /// 订单详情
    /// </summary>
    /// <param name="orders_sn"></param>
    public void Orders_Details(string orders_sn)
    {
        My_Contract = ContractFactory.CreateContract();
        ContractInfo ContractEntity = null;
        StringBuilder strHTML = new StringBuilder();
        OrdersInfo entity = Myorder.GetOrdersInfoBySN(orders_sn);
        if (entity != null)
        {
            ContractEntity = My_Contract.GetContractByID(entity.Orders_ContractID, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
        }



        SupplierInfo supplierInfo = null;
        string supplierName = "";
        string strOrdersResponsible = string.Empty;//初始化运输责任
        strOrdersResponsible = entity.Orders_Responsible == 1 ? "卖家责任" : "买家责任";
        if (entity != null)
        {
            QueryLoanProjectJsonInfo JsonInfo = null;

            if (entity.Orders_Loan_proj_no != "")
            {
                JsonInfo = credit.QueryLoanProject("M" + tools.NullStr(Session["member_id"]), entity.Orders_Loan_proj_no, "", 0, 0);
            }
            else
            {
                JsonInfo = new QueryLoanProjectJsonInfo();
            }

            supplierInfo = MySupplier.GetSupplierByID(entity.Orders_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
            if (supplierInfo != null)
            {
                supplierName = supplierInfo.Supplier_CompanyName;
            }
            else
            {
                supplierName = "--";
            }

            strHTML.Append("<div class=\"blk06\">");
            strHTML.Append("<h2><span>");
            if (entity.Orders_Status == 2 && entity.Orders_IsEvaluate == 0)
            // if (entity.Orders_Status == 2)
            {
                strHTML.Append("<a href=\"/member/order_evaluate.aspx?orders_sn=" + entity.Orders_SN + "\"  target=\"_blank\">去评价</a>");
            }
            if (entity.Orders_Status == 0)
            {
                strHTML.Append("<a href=\"/member/order_close.aspx?orders_sn=" + entity.Orders_SN + "\" target=\"_blank\">取消交易</a>");


                //strHTML.Append("<a href=\"/member/orders_do.aspx?action=orderfirm&Orders_ID=" + entity.Orders_ID + "\">确认订单</a>");

            }

            if (ContractEntity != null)
            {
                //订单未确定时，可以修改合同 未确定 ：0
                if (entity.Orders_Status == 0)
                {
                    if (entity.Orders_SupplierStatus == 0)
                    {
                        strHTML.Append("<a href=\"/member/Order_Contract.aspx?orders_sn=" + entity.Orders_SN + "\" target=\"_blank\">合同修改</a>");
                    }

                }




                strHTML.Append("<a href=\"/member/Order_Contract_View.aspx?orders_sn=" + entity.Orders_SN + "\" target=\"_blank\">合同预览</a>");


            }

            if (entity.Orders_PaymentStatus == 0 && entity.Orders_Status == 1)
            {


                strHTML.Append("<a href=\"/member/order_payment.aspx?orders_sn=" + entity.Orders_SN + "\" target=\"_blank\">付款</a>");
            }

            strHTML.Append("</span>订单信息</h2>");
            strHTML.Append("<div class=\"b06_main_sz\">");
            strHTML.Append("<p><span>订单编号：</span>" + entity.Orders_SN + "</p>");
            strHTML.Append("<p><span>卖方用户：</span>" + supplierName + "</p>");
            strHTML.Append("<p><span>订单状态：</span>" + OrdersStatus(entity.Orders_Status, entity.Orders_PaymentStatus, entity.Orders_DeliveryStatus) + "</p>");
            strHTML.Append("<p><span>订单总计：</span>" + pub.FormatCurrency(entity.Orders_Total_AllPrice) + "</p>");
            strHTML.Append("<p><span>收货地址：</span>" + addr.DisplayAddress(entity.Orders_Address_State, entity.Orders_Address_City, entity.Orders_Address_County) + entity.Orders_Address_StreetAddress + "    " + entity.Orders_Address_Name + "    " + entity.Orders_Address_Mobile + "</p>");
            strHTML.Append("<p><span>支付方式：</span>" + entity.Orders_Payway_Name + "");
            if (entity.Orders_Loan_proj_no != "" && entity.Orders_ApplyCreditAmount > 0)
            {
                if (JsonInfo != null && JsonInfo.Is_success == "T")
                {
                    IList<LoanlistInfo> loanlist = JsonInfo.Loan_list;
                    if (loanlist != null)
                    {
                        foreach (LoanlistInfo loaninfo in loanlist)
                        {
                            strHTML.Append("<strong style=\"color:red;\">（信贷：" + entity.Orders_ApplyCreditAmount + "元，期限：" + loaninfo.Term + pub.QueryLoanProductUnitChange(loaninfo.Term_unit) + "）</strong>");
                        }
                    }
                }
            }
            strHTML.Append("</p>");
            strHTML.Append("<p><span>运送责任：</span>" + strOrdersResponsible + "</p>");
            //strHTML.Append("<p><span>发票信息：</span>" + Orders_Detail_InvoiceInfo(entity.Orders_ID) + "</p>");
            strHTML.Append("<p><span>订单备注：</span>" + entity.Orders_Note + "</p>");
            strHTML.Append("</div>");
            strHTML.Append("</div>");







            //商品清单
            strHTML.Append(Order_Detail_Goods(entity));

            strHTML.Append("<div class=\"b06_info04_sz\">");
            strHTML.Append("<div class=\"b06_info04_left_sz\">订单费用调整原因：" + entity.Orders_Total_PriceDiscount_Note + "</div>");
            strHTML.Append("<div class=\"b06_info04_right_sz\">");
            strHTML.Append("<p><i>" + pub.FormatCurrency(entity.Orders_Total_Price) + "</i><span>商品总金额：</span></p>");



            strHTML.Append("<p><i><strong>" + pub.FormatCurrency(entity.Orders_Total_AllPrice) + "</strong></i><span>应付金额：</span></p>");
            strHTML.Append("</div>");
            strHTML.Append("</div>");

            //付款单
            IList<OrdersPaymentInfo> entitys = MyPayment.GetOrdersPaymentsByOrdersID(entity.Orders_ID);

            if (entitys != null)
            {
                strHTML.Append("<div class=\"blk14_1\">");
                strHTML.Append("<h2>付款单</h2>");
                strHTML.Append("<div class=\"b06_main02_sz\">");
                strHTML.Append(OrdersDetails_Payments(entity));
                strHTML.Append("</div>");
                strHTML.Append("</div>");
            }




            //收货单
            strHTML.Append("<div class=\"blk14_1\">");
            strHTML.Append("<h2>收货单</h2>");
            strHTML.Append("<div class=\"b06_main02_sz\">");
            strHTML.Append(OrdersDetail_Delivery_List(entity.Orders_ID, entity.Orders_Status));
            strHTML.Append("</div>");
            strHTML.Append("</div>");

            //订单日志
            strHTML.Append("<div class=\"blk14_1\">");
            strHTML.Append("<h2>订单日志</h2>");
            strHTML.Append("<div class=\"b06_main02_sz\">");
            strHTML.Append(Orders_Detail_Log(entity.Orders_ID));
            strHTML.Append("</div>");
            strHTML.Append("</div>");




            Response.Write(strHTML.ToString());
        }
        else
        {
            Response.Redirect("/member/orders_list.aspx?menu=1");
        }
    }







    //会员中心付款单
    public string OrdersDetails_Payments(OrdersInfo OrderInfo)
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<div class=\"b06_info03_sz\">");
        //strHTML.Append("<div class=\"title04\">商品清单</div>");
        //strHTML.Append("<h2>商品清单</h2>");
        strHTML.Append("<h3>");
        strHTML.Append("<ul>");
        strHTML.Append("<li style=\"width: 160px;\">付款单号</li>");
        strHTML.Append("<li style=\"width: 160px;\">支付金额</li>");
        strHTML.Append("<li style=\"width: 160px;\">支付方式</li>");
        strHTML.Append("<li style=\"width: 160px;\">支付备注</li>");
        strHTML.Append("<li style=\"width: 160px;\">支付时间</li>");
        strHTML.Append("<li style=\"width: 160px;\">支付状态</li>");
        strHTML.Append("</ul>");
        strHTML.Append("<div class=\"clear\"></div>");
        strHTML.Append("</h3>");
        strHTML.Append("<div class=\"b06_info03_main_sz\">");
        strHTML.Append("<table width=\"972\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
        IList<OrdersPaymentInfo> entitys = MyPayment.GetOrdersPaymentsByOrdersID(OrderInfo.Orders_ID);
        if (entitys != null)
        {
            //IList<OrdersGoodsInfo> GoodsList = Myorder.OrdersGoodsSearch(entitys, 0);
            //IList<OrdersGoodsInfo> GoodsListSub = null;
            foreach (OrdersPaymentInfo entity in entitys)
            {
                //GoodsListSub = Myorder.OrdersGoodsSearch(entitys, goodsinfo.Orders_Goods_ID);
                //strHTML.Append("<tr>");
                //if (goodsinfo.Orders_Goods_Product_ID > 0)
                //{
                //    strHTML.Append(" <td width=\"398\">");
                //    strHTML.Append("<dl>");
                //    strHTML.Append("<dt><a href=\"" + pageurl.FormatURL(pageurl.product_detail, goodsinfo.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(goodsinfo.Orders_Goods_Product_Img, "thumbnail") + "\"  onload=\"javascript:AutosizeImage(this,82,82);\" /></a></dt>");
                //    strHTML.Append("<dd>");
                //    strHTML.Append("<p><a href=\"" + pageurl.FormatURL(pageurl.product_detail, goodsinfo.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\"><strong>" + goodsinfo.Orders_Goods_Product_Name + "</strong></a></p>");
                //    //strHTML.Append("<p><span>编号：" + goodsinfo.Orders_Goods_Product_Code + "</span></p>");
                //    strHTML.Append("<p><span>" + new Product().Product_Extend_Content_New(goodsinfo.Orders_Goods_Product_ID) + "</span></p>");
                //    strHTML.Append("</dd>");
                //    strHTML.Append("<div class=\"clear\"></div>");
                //    strHTML.Append("</dl>");
                //    strHTML.Append("</td>");
                //    strHTML.Append("<td width=\"191\">" + pub.FormatCurrency(goodsinfo.Orders_Goods_Product_Price) + "</td>");
                //    strHTML.Append("<td width=\"194\">" + goodsinfo.Orders_Goods_Amount + "</td>");
                //    strHTML.Append("<td width=\"190\">" + pub.FormatCurrency((goodsinfo.Orders_Goods_Product_Price * goodsinfo.Orders_Goods_Amount)) + "</td>");
                //}
                //strHTML.Append("</tr>");

                strHTML.Append("<tr>");

                strHTML.Append("<td width=\"160px\">" + entity.Orders_Payment_DocNo + "</td>");
                strHTML.Append("<td width=\"160px\">" + pub.FormatCurrency(entity.Orders_Payment_Amount) + "</td>");
                strHTML.Append("<td width=\"160px\">" + entity.Orders_Payment_Name + "</td>");
                strHTML.Append("<td width=\"160px\">" + entity.Orders_Payment_Note + "</td>");
                strHTML.Append("<td width=\"160px\">" + entity.Orders_Payment_Addtime + "</td>");
                strHTML.Append("<td width=\"160px\">" + PaymentStatus(entity.Orders_Payment_PaymentStatus) + "</td>");





                strHTML.Append("</tr>");


            }
        }

        strHTML.Append("</table>");
        strHTML.Append("</div>");
        strHTML.Append("</div>");

        return strHTML.ToString();
    }







    /// <summary>
    /// 订单详情
    /// </summary>
    /// <param name="orders_sn"></param>
    public void Edit_Orders_Details(string orders_sn)
    {
        ContractInfo ContractEntity = null;
        StringBuilder strHTML = new StringBuilder();
        OrdersInfo entity = Myorder.GetOrdersInfoBySN(orders_sn);
        if (entity != null)
        {
            ContractEntity = My_Contract.GetContractByID(entity.Orders_ContractID, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
        }
        SupplierInfo supplierInfo = null;
        string supplierName = "";


        if (entity != null)
        {
            QueryLoanProjectJsonInfo JsonInfo = null;

            if (entity.Orders_Loan_proj_no != "")
            {
                JsonInfo = credit.QueryLoanProject("M" + tools.NullStr(Session["member_id"]), entity.Orders_Loan_proj_no, "", 0, 0);
            }
            else
            {
                JsonInfo = new QueryLoanProjectJsonInfo();
            }

            supplierInfo = MySupplier.GetSupplierByID(entity.Orders_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
            if (supplierInfo != null)
            {
                supplierName = supplierInfo.Supplier_CompanyName;
            }
            else
            {
                supplierName = "--";
            }


            //商品清单修改

            Member_Edit_Orders_Goods_Price(orders_sn);

            Response.Write(strHTML.ToString());
        }
        else
        {
            Response.Redirect("/member/orders_list.aspx?menu=1");
        }
    }


    /// <summary>
    /// 会员修改订单合同商品单价   即修改Order_Goods表
    /// </summary>
    /// <param name="orders_sn"></param>
    public void Member_Edit_Orders_Goods_Price(string orders_sn)
    {
        StringBuilder strHTML = new StringBuilder();
        OrdersInfo entity = Myorder.GetOrdersInfoBySN(orders_sn);
        SupplierInfo supplierInfo = null;
        string supplierName = "";


        if (entity != null)
        {
            QueryLoanProjectJsonInfo JsonInfo = null;

            if (entity.Orders_Loan_proj_no != "")
            {
                JsonInfo = credit.QueryLoanProject("M" + tools.NullStr(Session["member_id"]), entity.Orders_Loan_proj_no, "", 0, 0);
            }
            else
            {
                JsonInfo = new QueryLoanProjectJsonInfo();
            }

            supplierInfo = MySupplier.GetSupplierByID(entity.Orders_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
            if (supplierInfo != null)
            {
                supplierName = supplierInfo.Supplier_CompanyName;
            }
            else
            {
                supplierName = "--";
            }


            //商品清单
            strHTML.Append(Member_Edit_Order_Detail_Goods(entity, orders_sn));


            Response.Write(strHTML.ToString());
        }
        else
        {
            Response.Redirect("/member/orders_list.aspx?menu=1");
        }
    }


    /// <summary>
    /// 获取订单已付款金额
    /// </summary>
    /// <param name="orders_id"></param>
    /// <returns></returns>
    public double GetOrdersPayedAmount(int orders_id)
    {
        double TotalAmount = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersPaymentInfo.Orders_Payment_OrdersID", "=", orders_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersPaymentInfo.Orders_Payment_PaymentStatus", "=", "1"));
        IList<OrdersPaymentInfo> entitys = MyPayment.GetOrdersPayments(Query);
        if (entitys != null)
        {
            foreach (OrdersPaymentInfo entity in entitys)
            {
                TotalAmount += entity.Orders_Payment_Amount;
            }
        }
        return TotalAmount;
    }

    /// <summary>
    /// 订单详情页商品列表
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public string Order_Detail_Goods(OrdersInfo entity)
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<div class=\"b06_info03_sz\">");
        //strHTML.Append("<div class=\"title04\">商品清单</div>");
        strHTML.Append("<h2>商品清单</h2>");
        strHTML.Append("<h3>");
        strHTML.Append("<ul>");
        strHTML.Append("<li style=\"width: 394px;\">商品</li>");
        strHTML.Append("<li style=\"width: 190px;\">单价</li>");
        strHTML.Append("<li style=\"width: 190px;\">数量</li>");
        strHTML.Append("<li style=\"width: 190px; border-right: none;\">小计（元）</li>");
        strHTML.Append("</ul>");
        strHTML.Append("<div class=\"clear\"></div>");
        strHTML.Append("</h3>");
        strHTML.Append("<div class=\"b06_info03_main_sz\">");
        strHTML.Append("<table width=\"972\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
        IList<OrdersGoodsInfo> entitys = MyOrders.GetGoodsListByOrderID(entity.Orders_ID);
        if (entitys != null)
        {
            IList<OrdersGoodsInfo> GoodsList = Myorder.OrdersGoodsSearch(entitys, 0);
            IList<OrdersGoodsInfo> GoodsListSub = null;
            foreach (OrdersGoodsInfo goodsinfo in GoodsList)
            {
                GoodsListSub = Myorder.OrdersGoodsSearch(entitys, goodsinfo.Orders_Goods_ID);
                strHTML.Append("<tr>");
                if (goodsinfo.Orders_Goods_Product_ID > 0)
                {
                    strHTML.Append(" <td width=\"398\">");
                    strHTML.Append("<dl>");
                    strHTML.Append("<dt><a href=\"" + pageurl.FormatURL(pageurl.product_detail, goodsinfo.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(goodsinfo.Orders_Goods_Product_Img, "thumbnail") + "\"  onload=\"javascript:AutosizeImage(this,82,82);\" /></a></dt>");
                    strHTML.Append("<dd>");
                    strHTML.Append("<p><a href=\"" + pageurl.FormatURL(pageurl.product_detail, goodsinfo.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\"><strong>" + goodsinfo.Orders_Goods_Product_Name + "</strong></a></p>");
                    //strHTML.Append("<p><span>编号：" + goodsinfo.Orders_Goods_Product_Code + "</span></p>");
                    strHTML.Append("<p><span>" + new Product().Product_Extend_Content_New(goodsinfo.Orders_Goods_Product_ID) + "</span></p>");
                    strHTML.Append("</dd>");
                    strHTML.Append("<div class=\"clear\"></div>");
                    strHTML.Append("</dl>");
                    strHTML.Append("</td>");
                    strHTML.Append("<td width=\"191\">" + pub.FormatCurrency(goodsinfo.Orders_Goods_Product_Price) + "</td>");
                    strHTML.Append("<td width=\"194\">" + goodsinfo.Orders_Goods_Amount + "</td>");
                    strHTML.Append("<td width=\"190\">" + pub.FormatCurrency((goodsinfo.Orders_Goods_Product_Price * goodsinfo.Orders_Goods_Amount)) + "</td>");
                }
                strHTML.Append("</tr>");
            }
        }

        strHTML.Append("</table>");
        strHTML.Append("</div>");
        strHTML.Append("</div>");

        return strHTML.ToString();
    }






    /// <summary>
    /// 订单详情页商品列表
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public string Member_Edit_Order_Detail_Goods(OrdersInfo entity, string orders_sn)
    {
        StringBuilder strHTML = new StringBuilder();

        //strHTML.Append("<div class=\"b06_info03_sz\">");

        //strHTML.Append("<h2>商品订单合同<span><a href=\"\"></a></span></h2>");
        //strHTML.Append("<h3>");
        //strHTML.Append("<ul>");
        //strHTML.Append("<li style=\"width: 394px;\">商品</li>");
        //strHTML.Append("<li style=\"width: 190px;\">单价</li>");
        //strHTML.Append("<li style=\"width: 190px;\">数量</li>");
        //strHTML.Append("<li style=\"width: 190px; border-right: none;\">小计（元）</li>");
        //strHTML.Append("</ul>");
        //strHTML.Append("<div class=\"clear\"></div>");
        //strHTML.Append("</h3>");
        //strHTML.Append("<div class=\"b06_info03_main_sz\">");
        //  strHTML.Append(" <form name=\"frm_batch\" action=\"orders_do.aspx\" method=\"post\">");
        strHTML.Append("<table width=\"972\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
        IList<OrdersGoodsInfo> entitys = MyOrders.GetGoodsListByOrderID(entity.Orders_ID);
        if (entitys != null)
        {
            IList<OrdersGoodsInfo> GoodsList = Myorder.OrdersGoodsSearch(entitys, 0);
            IList<OrdersGoodsInfo> GoodsListSub = null;
            int i = 0;
            foreach (OrdersGoodsInfo goodsinfo in GoodsList)
            {
                i++;
                GoodsListSub = Myorder.OrdersGoodsSearch(entitys, goodsinfo.Orders_Goods_ID);
                strHTML.Append("<tr>");
                if (goodsinfo.Orders_Goods_Product_ID > 0)
                {
                    strHTML.Append(" <td width=\"398\" style=\"padding:0\">");
                    strHTML.Append("<dl>");
                    //strHTML.Append("<dt><a href=\"" + pageurl.FormatURL(pageurl.product_detail, goodsinfo.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(goodsinfo.Orders_Goods_Product_Img, "thumbnail") + "\"  onload=\"javascript:AutosizeImage(this,82,82);\" /></a></dt>");
                    strHTML.Append("<dd style=\" text-align: center;    width: 200px;\">");
                    strHTML.Append("<p>" + goodsinfo.Orders_Goods_Product_Name + "</p>");
                    strHTML.Append("<p><span>编号：" + goodsinfo.Orders_Goods_Product_Code + "</span></p>");
                    strHTML.Append("</dd>");
                    strHTML.Append("<div class=\"clear\"></div>");
                    strHTML.Append("</dl>");
                    strHTML.Append("</td>");

                    strHTML.Append("<td width=\"191\" id=\"Orders_Goods_Product_Price\" value=\"" + pub.FormatCurrency(goodsinfo.Orders_Goods_Product_Price) + "\"> <input onblur=\"sum('buy_amount_" + goodsinfo.Orders_Goods_ID + "',this.id,'Orders_Goods_EveryProduct_Sum" + i + "')\" name=\"buy_price_" + goodsinfo.Orders_Goods_ID + "\" id=\"buy_price_" + goodsinfo.Orders_Goods_ID + "\" type=\"text\" value=\"" + goodsinfo.Orders_Goods_Product_Price + "\" /></td>");
                    strHTML.Append("<td width=\"194\" id=\"Orders_Goods_Amount\"><input name=\"buy_amount_" + goodsinfo.Orders_Goods_ID + "\" onblur=\"sum(this.id,'buy_price_" + goodsinfo.Orders_Goods_ID + "','Orders_Goods_EveryProduct_Sum" + i + "')\" id=\"buy_amount_" + goodsinfo.Orders_Goods_ID + "\" type=\"text\" value=\"" + goodsinfo.Orders_Goods_Amount + "\"                       /></td>");
                    strHTML.Append("<td width=\"190\" id=\"Orders_Goods_EveryProduct_Sum" + i + "\">");

                    strHTML.Append(" " + goodsinfo.Orders_Goods_Product_Price * goodsinfo.Orders_Goods_Amount + "");

                    strHTML.Append("  </td>");

                }
                strHTML.Append("</tr>");
            }
        }

        strHTML.Append("</table>");
        //strHTML.Append("</form>");
        //strHTML.Append("</div>");
        //strHTML.Append("</div>");

        return strHTML.ToString();
    }

    /// <summary>
    /// 订单详情页订单日志
    /// </summary>
    /// <param name="Orders_ID"></param>
    /// <returns></returns>
    public string Orders_Detail_Log(int Orders_ID)
    {
        int i = 0;
        StringBuilder strHTML = new StringBuilder();
        IList<OrdersLogInfo> logList = MyOrdersLog.GetOrdersLogsByOrdersID(Orders_ID);
        if (logList != null)
        {
            strHTML.Append("<div class=\"b06_info02_sz\">");
            foreach (OrdersLogInfo entity in logList)
            {
                i++;
                strHTML.Append("<p><i>" + i + "</i>" + entity.Orders_Log_Addtime.ToString("yyyy-MM-dd HH:mm:ss") + " " + tools.CleanHTML(entity.Orders_Log_Remark) + "</p>");
            }
            strHTML.Append("</div>");
        }

        return strHTML.ToString();
    }

    /// <summary>
    /// 订单详情页发票信息
    /// </summary>
    /// <param name="Orders_ID"></param>
    /// <returns></returns>
    public string Orders_Detail_InvoiceInfo(int Orders_ID)
    {
        string invoiceStr = "";

        OrdersInvoiceInfo entity = MyOrdersInvoice.GetOrdersInvoiceByOrdersID(Orders_ID);
        if (entity != null)
        {
            invoiceStr = OrdersInvoiceType(entity.Invoice_Type) + "&nbsp;&nbsp;";

            if (OrdersInvoiceType(entity.Invoice_Type) != "增值税发票")
            {
                if (entity.Invoice_Title == "个人")
                {
                    invoiceStr += entity.Invoice_Content;
                }
                else
                {
                    invoiceStr += entity.Invoice_FirmName;
                }
            }
            else
            {
                invoiceStr += entity.Invoice_VAT_FirmName;
            }
        }
        return invoiceStr;
    }


    /// <summary>
    /// 订单付款单
    /// </summary>
    /// <param name="ordersInfo"></param>
    /// <returns></returns>
    public string Orders_Payments(OrdersInfo ordersInfo)
    {
        StringBuilder HTML_Str = new StringBuilder();

        IList<OrdersPaymentInfo> entitys = MyPayment.GetOrdersPaymentsByOrdersID(ordersInfo.Orders_ID);

        HTML_Str.Append("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\" bgcolor=\"#dddddd\">");
        if (entitys != null)
        {
            HTML_Str.Append("<tr bgcolor=\"#ffffff\">");
            HTML_Str.Append("<td align=\"center\" height=\"25\"><b>付款单号</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>支付金额</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>支付方式</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>支付备注</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>支付时间</b></td>");
            HTML_Str.Append("<td align=\"center\"><b>支付状态</b></td>");

            HTML_Str.Append("</tr>");
            foreach (OrdersPaymentInfo entity in entitys)
            {

                HTML_Str.Append("<tr bgcolor=\"#ffffff\">");

                HTML_Str.Append("<td align=\"center\" height=\"25\">" + entity.Orders_Payment_DocNo + "</td>");
                HTML_Str.Append("<td align=\"center\">" + pub.FormatCurrency(entity.Orders_Payment_Amount) + "</td>");
                HTML_Str.Append("<td align=\"center\">" + entity.Orders_Payment_Name + "</td>");
                HTML_Str.Append("<td align=\"center\">" + entity.Orders_Payment_Note + "</td>");
                HTML_Str.Append("<td align=\"center\">" + entity.Orders_Payment_Addtime + "</td>");
                HTML_Str.Append("<td align=\"center\">" + PaymentStatus(entity.Orders_Payment_PaymentStatus) + "</td>");

                HTML_Str.Append("</tr>");
            }
        }
        else
        {
            HTML_Str.Append("<tr bgcolor=\"#ffffff\"><td><img src=\"/images/icon_alert.gif\" align=\"absmiddle\"> 暂无付款信息</td></tr>");
        }
        HTML_Str.Append("</table>");

        return HTML_Str.ToString();
    }

    /// <summary>
    /// 订单关闭
    /// </summary>
    public void Orders_Close()
    {
        string orders_close_note = tools.CheckStr(Request["orders_close_note"]);
        string orders_sn = tools.CheckStr(Request["orders_sn"]);
        if (orders_sn == "")
        {
            pub.Msg("error", "错误信息", "订单不存在", false, "/member/order_list.aspx");
        }
        OrdersInfo ordersinfo = GetOrdersInfoBySN(orders_sn);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_BuyerID == tools.CheckInt(Session["member_id"].ToString()) && ordersinfo.Orders_Status == 0 && ordersinfo.Orders_PaymentStatus == 0 && ordersinfo.Orders_DeliveryStatus == 0)
            {
                ordersinfo.Orders_Fail_Addtime = DateTime.Now;
                ordersinfo.Orders_Fail_Note = orders_close_note;
                int total_usecoin = ordersinfo.Orders_Total_UseCoin;
                ordersinfo.Orders_Fail_SysUserID = 0;
                ordersinfo.Orders_Status = 3;
                MyOrders.EditOrders(ordersinfo);


                Myorder.Orders_Log(ordersinfo.Orders_ID, "", "取消", "成功", "订单取消,取消原因：" + orders_close_note + "");
            }
        }
        Response.Redirect("/member/order_list.aspx");
    }

    /// <summary>
    /// 订单备注管理
    /// </summary>
    public void Orders_NoteEdit()
    {
        string orders_note = tools.CheckStr(Request["orders_note"]);
        string orders_sn = tools.CheckStr(Request["orders_sn"]);
        if (orders_sn == "")
        {
            pub.Msg("error", "错误信息", "订单不存在", false, "/member/order_all.aspx");
        }
        OrdersInfo ordersinfo = GetOrdersInfoBySN(orders_sn);
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_BuyerID == tools.CheckInt(Session["member_id"].ToString()) && ordersinfo.Orders_Status == 0 && ordersinfo.Orders_PaymentStatus == 0 && ordersinfo.Orders_DeliveryStatus == 0)
            {
                ordersinfo.Orders_Note = orders_note;
                MyOrders.EditOrders(ordersinfo);
            }
        }
        Response.Redirect("/member/order_view.aspx?orders_sn=" + orders_sn);
    }

    //订单确认
    public void Orders_Confirm()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        string Template_Html = "";

        string contact_no = CreateContractSN();
        string orders_sn = "";
        OrdersInfo ordersinfo = MyOrders.GetMemberOrderInfoByID(Orders_ID, tools.NullInt(Session["member_id"]));
        if (ordersinfo != null)
        {
            SupplierInfo supplierInfo = supplier.GetSupplierByID(ordersinfo.Orders_SupplierID);
            if (supplier != null && supplierInfo.Supplier_ContractID > 0)
            {
                ContractTemplateInfo templateInfo = MyContract.GetContractTemplateByID(supplierInfo.Supplier_ContractID, pub.CreateUserPrivilege("d4d58107-0e58-485f-af9e-3b38c7ff9672"));
                if (templateInfo != null && templateInfo.Contract_Template_Content.Length > 0)
                {
                    if (ordersinfo.Orders_Status == 0 && ordersinfo.Orders_SupplierStatus == 1 && ordersinfo.Orders_BuyerID == tools.NullInt(Session["member_id"]))
                    {
                        orders_sn = ordersinfo.Orders_SN;
                        ordersinfo.Orders_MemberStatus = 1;
                        ordersinfo.Orders_Status = 1;
                        ordersinfo.Orders_MemberStatus_Time = DateTime.Now;

                        if (MyOrders.EditOrders(ordersinfo))
                        {
                            //获取并替换合同模板内容
                            Template_Html = ReplaceOrdersContract(ordersinfo, contact_no);

                            //添加订单合同信息
                            AddOrdersContract(ordersinfo.Orders_SN, ordersinfo.Orders_ID, contact_no);

                            //创建PDF电子合同
                            textsharp.ConvertHtmlTextToPDF(CreateContractName(orders_sn), Template_Html);

                            //PDF电子合同签名
                            textsharp.PDFSign(CreateContractName(orders_sn), encrypt.MD5(CreateContractName(orders_sn)));

                            //消息通知
                            messageclass.SendMessage(0, 2, ordersinfo.Orders_SupplierID, 0, "订单" + orders_sn + "采购商已确认");

                            //订单日志
                            Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["member_nickname"]), "确认", "成功", "订单确认");

                            #region 创建担保交易
                            TradeJsonInfo tradeJsonInfo = null;
                            string request_no = DateTime.Now.ToString("yyyyMMddHHmmss") + pub.Createvkey(10);
                            string notify_url = Application["Site_URL"] + "/pay/payment_notify.aspx";
                            string return_url = Application["Site_URL"] + "/member/order_view.aspx?orders_sn=" + ordersinfo.Orders_SN;
                            string trade_list = payment.BindingEnsureTradeList(Orders_ID, notify_url);
                            string buyer_id = "M" + tools.NullStr(Session["member_id"]);
                            string buyer_id_type = "UID";
                            string buyer_mobile = tools.NullStr(Session["member_mobile"]);

                            if (ordersinfo.Orders_Payway == 1)
                            {
                                tradeJsonInfo = payment.Create_Ensure_Trade_Get(request_no, trade_list, buyer_id, buyer_id_type, buyer_mobile, return_url);
                                if (tradeJsonInfo != null && tradeJsonInfo.Is_success == "T")
                                {
                                    Session["cashier_url"] = tradeJsonInfo.Cashier_url;
                                    ordersinfo.Orders_cashier_url = tradeJsonInfo.Cashier_url;
                                    MyOrders.EditOrders(ordersinfo);
                                }
                            }
                            else if (ordersinfo.Orders_Payway == 2 || ordersinfo.Orders_Payway == 3)
                            {
                                if (Check_Member_Loan(ordersinfo))
                                {
                                    if (ordersinfo.Orders_ApplyCreditAmount > 0)
                                    {
                                        tradeJsonInfo = payment.Create_Ensure_Trade_Get(request_no, trade_list, buyer_id, buyer_id_type, buyer_mobile, return_url);
                                        if (tradeJsonInfo != null && tradeJsonInfo.Is_success == "T")
                                        {
                                            Member_Loan_Apply(ordersinfo.Orders_SN);
                                        }
                                    }
                                    else
                                    {
                                        tradeJsonInfo = payment.Create_Ensure_Trade_Get(request_no, trade_list, buyer_id, buyer_id_type, buyer_mobile, return_url);
                                        if (tradeJsonInfo != null && tradeJsonInfo.Is_success == "T")
                                        {
                                            Session["cashier_url"] = tradeJsonInfo.Cashier_url;
                                            ordersinfo.Orders_cashier_url = tradeJsonInfo.Cashier_url;
                                            MyOrders.EditOrders(ordersinfo);
                                        }
                                    }

                                    //tradeJsonInfo = payment.Create_Ensure_Trade_Get(request_no, trade_list, buyer_id, buyer_id_type, buyer_mobile, return_url);
                                    //if (tradeJsonInfo != null && tradeJsonInfo.Is_success == "T")
                                    //{
                                    //    Member_Loan_Apply(ordersinfo.Orders_SN);
                                    //}
                                }
                                else
                                {
                                    ordersinfo.Orders_Status = 3;
                                    ordersinfo.Orders_Fail_Note = "订单不满足信贷申请条件";
                                    ordersinfo.Orders_Fail_Addtime = DateTime.Now;
                                    if (MyOrders.EditOrders(ordersinfo))
                                    {
                                        Myorder.Orders_Log(Orders_ID, "系统", "订单关闭", "成功", "订单不满足信贷申请条件，系统自动关闭");
                                    }
                                    pub.Msg("error", "提示信息", "您的订单不满足信贷申请条件，系统已自动关闭交易！", false, "/member/order_view.aspx?orders_sn=" + ordersinfo.Orders_SN);
                                }
                            }
                            #endregion

                            //短信发送
                            string[] content = { supplierInfo.Supplier_CompanyName, ordersinfo.Orders_SN };
                            ////new SMS().Send(tools.NullStr(Session["member_mobile"]), "member_confirm_orders_remind", content);
                            //  new SMS().Send(tools.NullStr(Session["member_mobile"]), content);
                            new SMS().Send(tools.NullStr(Session["member_mobile"].ToString()), content.ToString());
                        }
                        pub.Msg("positive", "操作成功", "操作成功", true, "/member/order_view.aspx?orders_sn=" + ordersinfo.Orders_SN);
                    }
                }
                else
                {
                    pub.Msg("error", "提示信息", "订单确认失败，请稍后再试......", false, "/member/order_view.aspx?orders_sn=" + ordersinfo.Orders_SN);
                }
            }
            else
            {
                pub.Msg("error", "提示信息", "订单确认失败，请稍后再试......", false, "/member/order_view.aspx?orders_sn=" + ordersinfo.Orders_SN);
            }
        }
        Response.Redirect("/member/order_view.aspx?orders_sn=" + orders_sn);
    }

    #region 确认签收

    /// <summary>
    /// 确认签收
    /// </summary>
    public void Orders_Delivery_Accept()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);

        int Product_ID, Goods_Amount, Goods_Type;

        int Orders_Delivery_ID = tools.CheckInt(Request["Delivery_ID"]);
        OrdersDeliveryInfo ODEntity = GetOrdersDeliveryByID(Orders_Delivery_ID);

        if (ODEntity == null)
        {
            pub.Msg("error", "错误信息", "发货单不存在", false, "index.aspx");
        }
        OrdersInfo ordersinfo = MyOrders.GetMemberOrderInfoByID(ODEntity.Orders_Delivery_OrdersID, tools.NullInt(Session["member_id"]));
        if (ordersinfo == null)
        {
            pub.Msg("error", "错误信息", "记录不存在", false, "/member/order_delivery_list.aspx?payment=1&menu=1");
        }
        if (ordersinfo.Orders_Status == 1 && ordersinfo.Orders_PaymentStatus == 4 && ODEntity.Orders_Delivery_ReceiveStatus == 0)
        {

        }
        else
        {
            pub.Msg("error", "错误信息", "记录不存在", false, "/member/order_delivery_list.aspx?payment=1&menu=1");
        }
        Orders_ID = ODEntity.Orders_Delivery_OrdersID;

        //检查是否包含签收数量商品
        bool isreceived = false;
        IList<OrdersDeliveryGoodsInfo> deliverygoods = MyDelivery.GetOrdersDeliveryGoods(ODEntity.Orders_Delivery_ID);
        if (deliverygoods != null)
        {
            foreach (OrdersDeliveryGoodsInfo deliverygood in deliverygoods)
            {
                int receiveamount = tools.CheckInt(Request[deliverygood.Orders_Delivery_Goods_ID + "__ReceivedAmount"]);
                if (receiveamount > 0)
                {
                    isreceived = true;
                }
            }
        }


        IList<OrdersGoodsInfo> entitys = null;
        //更新签收状态
        ODEntity.Orders_Delivery_ReceiveStatus = 1;
        if (MyDelivery.EditOrdersDelivery(ODEntity, pub.CreateUserPrivilege("")))
        {
            //更新签收数量
            if (deliverygoods != null)
            {
                foreach (OrdersDeliveryGoodsInfo deliverygood in deliverygoods)
                {
                    int receiveamount = tools.CheckInt(Request[deliverygood.Orders_Delivery_Goods_ID + "_ReceivedAmount"]);
                    if (receiveamount > 0)
                    {
                        deliverygood.Orders_Delivery_Goods_ReceivedAmount = receiveamount;
                        MyDelivery.EditOrdersDeliveryGoods(deliverygood);
                        //短信推送
                        SMS mySMS = new SMS();
                        Glaer.Trade.B2C.Model.OrdersInfo mysmsOrder = MyOrders.GetOrdersByID(Orders_ID);
                        SupplierInfo supplierEntity = MySupplier.GetSupplierByID(mysmsOrder.Orders_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                        mySMS.Send(supplierEntity.Supplier_Mobile, supplierEntity.Supplier_CompanyName + "," + ordersinfo.Orders_SN, "orders_accept");
                    }
                }
            }

            Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["member_nickname"]), "发货单签收", "成功", "发货单" + ODEntity.Orders_Delivery_DocNo + "签收！");
            //if (CheckAllReceive(ordersinfo.Orders_ID))
            //{
            //    ordersinfo.Orders_DeliveryStatus = 2;
            //    ordersinfo.Orders_DeliveryStatus_Time = DateTime.Now;

            //    ordersinfo.Orders_Status = 2;
            //    if (MyOrders.EditOrders(ordersinfo))
            //    {
            //        if (ordersinfo.Orders_DeliveryStatus == 2)
            //        {
            //            // messageclass.SendMessage(0, 2, ordersinfo.Orders_SupplierID, 0, "订单" + ordersinfo.Orders_SN + "已签收");


            //            SupplierInfo supplierInfo = MySupplier.GetSupplierByID(ordersinfo.Orders_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
            //            if (supplierInfo != null)
            //            {
            //                string[] content = { supplierInfo.Supplier_CompanyName, ordersinfo.Orders_SN };
            //                //推送短信到供货商
            //                //new SMS().Send(supplierInfo.Supplier_Mobile, "orders_accept", content);
            //            }
            //        }

            //        if (ordersinfo.Orders_Status == 2)
            //        {
            //            entitys = MyOrders.GetGoodsListByOrderID(ordersinfo.Orders_ID);
            //            if (entitys != null)
            //            {
            //                foreach (OrdersGoodsInfo entity in entitys)
            //                {
            //                    Product_ID = entity.Orders_Goods_Product_ID;
            //                    Goods_Amount = entity.Orders_Goods_Amount;
            //                    Goods_Type = entity.Orders_Goods_Type;

            //                    if (Goods_Type == 0 || Goods_Type == 3 || (Goods_Type == 2 && entity.Orders_Goods_ParentID > 0))
            //                    {
            //                        MyProduct.UpdateProductSaleAmount(Product_ID, Goods_Amount);
            //                    }
            //                }
            //            }
            //            entitys = null;

            //            messageclass.SendMessage(0, 1, ordersinfo.Orders_BuyerID, 0, "订单" + ordersinfo.Orders_SN + "已完成");

            //            Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["member_nickname"]), "订单完成", "成功", "订单交易成功！");

            //            //推送短信到采购商
            //            //new SMS().Send(tools.NullStr(Session["member_mobile"]), "orders_success", ordersinfo.Orders_SN);
            //        }
            //    }
            //}
        }
        pub.Msg("positive", "操作成功", "操作成功", true, "order_delivery_list.aspx");
    }

    #endregion

    public void Orders_Delivery_AcceptEdit()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);

        int Product_ID, Goods_Amount, Goods_Type;

        int Orders_Delivery_ID = tools.CheckInt(Request["Delivery_ID"]);
        OrdersDeliveryInfo ODEntity = GetOrdersDeliveryByID(Orders_Delivery_ID);

        if (ODEntity == null)
        {
            pub.Msg("error", "错误信息", "发货单不存在", false, "index.aspx");
        }
        OrdersInfo ordersinfo = MyOrders.GetMemberOrderInfoByID(ODEntity.Orders_Delivery_OrdersID, tools.NullInt(Session["member_id"]));
        if (ordersinfo == null)
        {
            pub.Msg("error", "错误信息", "记录不存在", false, "/member/order_delivery_list.aspx?payment=1&menu=1");
        }
        if (ordersinfo.Orders_Status == 1 && ordersinfo.Orders_PaymentStatus == 4 && ODEntity.Orders_Delivery_ReceiveStatus != 2)
        {

        }
        else
        {
            pub.Msg("error", "错误信息", "记录不存在", false, "/member/order_delivery_list.aspx?payment=1&menu=1");
        }
        Orders_ID = ODEntity.Orders_Delivery_OrdersID;

        //检查是否包含签收数量商品
        //bool isreceived = false;
        //IList<OrdersDeliveryGoodsInfo> deliverygoods = MyDelivery.GetOrdersDeliveryGoods(ODEntity.Orders_Delivery_ID);
        //if (deliverygoods != null)
        //{
        //    foreach (OrdersDeliveryGoodsInfo deliverygood in deliverygoods)
        //    {
        //        int receiveamount = tools.CheckInt(Request[deliverygood.Orders_Delivery_Goods_ID + "__ReceivedAmount"]);
        //        int receiveprice = tools.CheckInt(Request[deliverygood.Orders_Delivery_Goods_ID + "__ReceivedAmount"]);
        //        if (receiveamount > 0)
        //        {
        //            isreceived = true;
        //        }
        //    }
        //}


        IList<OrdersGoodsInfo> entitys = null;
        //更新签收状态
        //ODEntity.Orders_Delivery_ReceiveStatus = 1;
        IList<OrdersDeliveryGoodsInfo> deliverygoods = MyDelivery.GetOrdersDeliveryGoods(ODEntity.Orders_Delivery_ID);

        if (MyDelivery.EditOrdersDelivery(ODEntity, pub.CreateUserPrivilege("")))
        {
            //更新签收数量
            if (deliverygoods != null)
            {
                foreach (OrdersDeliveryGoodsInfo deliverygood in deliverygoods)
                {
                    double buy_price = tools.CheckFloat(Request["buy_price_" + deliverygood.Orders_Delivery_Goods_ID]);
                    int receiveamount = tools.CheckInt(Request["buy_amount_" + deliverygood.Orders_Delivery_Goods_ID]);
                    if (receiveamount >= 0)
                    {
                        deliverygood.Orders_Delivery_Goods_ProductPrice = buy_price;
                        deliverygood.Orders_Delivery_Goods_ReceivedAmount = receiveamount;
                        MyDelivery.EditOrdersDeliveryGoods(deliverygood);
                    }
                }
            }

            //Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["member_nickname"]), "发货单签收", "成功", "发货单" + ODEntity.Orders_Delivery_DocNo + "签收！");           
        }
        pub.Msg("positive", "操作成功", "操作成功", true, "/member/order_delivery_Edit.aspx?Orders_Delivery_ID=" + Orders_Delivery_ID + "");
    }

    public void Orders_Acceptbak()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);

        int Product_ID, Goods_Amount, Goods_Type;
        IList<OrdersGoodsInfo> entitys = null;

        OrdersInfo ordersinfo = MyOrders.GetMemberOrderInfoByID(Orders_ID, tools.NullInt(Session["member_id"]));
        if (ordersinfo != null)
        {
            if (ordersinfo.Orders_Status == 1 && ordersinfo.Orders_DeliveryStatus == 1 && (ordersinfo.Orders_PaymentStatus == 1 || ordersinfo.Orders_PaymentStatus == 4))
            {
                ordersinfo.Orders_DeliveryStatus = 2;
                ordersinfo.Orders_DeliveryStatus_Time = DateTime.Now;

                ordersinfo.Orders_Status = 2;
                if (MyOrders.EditOrders(ordersinfo))
                {
                    if (ordersinfo.Orders_DeliveryStatus == 2)
                    {
                        messageclass.SendMessage(0, 2, ordersinfo.Orders_SupplierID, 0, "订单" + ordersinfo.Orders_SN + "已签收");

                        Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["member_nickname"]), "订单签收", "成功", "订单签收！");

                        SupplierInfo supplierInfo = MySupplier.GetSupplierByID(ordersinfo.Orders_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                        if (supplierInfo != null)
                        {
                            string[] content = { supplierInfo.Supplier_CompanyName, ordersinfo.Orders_SN };
                            //推送短信到供货商
                            //new SMS().Send(supplierInfo.Supplier_Mobile, "orders_accept", content);
                            new SMS().Send(supplierInfo.Supplier_Mobile, content.ToString());
                        }
                    }

                    if (ordersinfo.Orders_Status == 2)
                    {
                        entitys = MyOrders.GetGoodsListByOrderID(ordersinfo.Orders_ID);
                        if (entitys != null)
                        {
                            foreach (OrdersGoodsInfo entity in entitys)
                            {
                                Product_ID = entity.Orders_Goods_Product_ID;
                                Goods_Amount = entity.Orders_Goods_Amount;
                                Goods_Type = entity.Orders_Goods_Type;

                                if (Goods_Type == 0 || Goods_Type == 3 || (Goods_Type == 2 && entity.Orders_Goods_ParentID > 0))
                                {
                                    MyProduct.UpdateProductSaleAmount(Product_ID, Goods_Amount);
                                }
                            }
                        }
                        entitys = null;

                        messageclass.SendMessage(0, 1, ordersinfo.Orders_BuyerID, 0, "订单" + ordersinfo.Orders_SN + "已完成");

                        Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["member_nickname"]), "订单完成", "成功", "订单交易成功！");

                        //推送短信到采购商
                        //new SMS().Send(tools.NullStr(Session["member_mobile"]), "orders_success", ordersinfo.Orders_SN);
                        new SMS().Send(tools.NullStr(Session["member_mobile"]), ordersinfo.Orders_SN.ToString());
                    }
                }
                Response.Write("success");
                Response.End();
            }
            else
            {
                Response.Write("操作失败，请稍后再试......");
                Response.End();
            }
        }
        else
        {
            Response.Write("操作失败，请稍后再试......");
            Response.End();
        }
    }

    /// <summary>
    /// 替换合同模板中的占位符信息
    /// </summary>
    /// <param name="Supplier_ID"></param>
    /// <returns></returns>
    public string ReplaceOrdersContract(OrdersInfo ordersInfo, string contact_no)
    {
        string Template_Html = "";
        ContractTemplateInfo templateInfo = null;
        MemberInfo memberInfo = GetMemberByID();

        if (ordersInfo != null)
        {
            SupplierInfo supplierInfo = supplier.GetSupplierByID(ordersInfo.Orders_SupplierID);
            if (supplier != null)
            {
                templateInfo = MyContract.GetContractTemplateByID(supplierInfo.Supplier_ContractID, pub.CreateUserPrivilege("d4d58107-0e58-485f-af9e-3b38c7ff9672"));
                if (templateInfo != null)
                {
                    Template_Html = templateInfo.Contract_Template_Content;

                    Template_Html = Template_Html.Replace("{contact_no}", contact_no);
                    Template_Html = Template_Html.Replace("{contact_addtime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                    Template_Html = Template_Html.Replace("{supplier_name}", supplierInfo.Supplier_CompanyName);
                    Template_Html = Template_Html.Replace("{supplier_sealimg}", pub.FormatImgURL(supplierInfo.Supplier_SealImg, "fullpath"));


                    Template_Html = Template_Html.Replace("{orders_paywayname}", ordersInfo.Orders_Payway_Name);
                    Template_Html = Template_Html.Replace("{orders_deliveryname}", ordersInfo.Orders_Delivery_Name);
                    Template_Html = Template_Html.Replace("{orders_address}", addr.DisplayAddress(ordersInfo.Orders_Address_State, ordersInfo.Orders_Address_City, ordersInfo.Orders_Address_County) + ordersInfo.Orders_Address_StreetAddress);

                    if (templateInfo.Contract_Template_Code == "Sell_Contract_Template")
                    {
                        Template_Html = Template_Html.Replace("{orders_goodslist}", GetOrdersGoods(ordersInfo));
                    }

                    if (memberInfo != null)
                    {
                        Template_Html = Template_Html.Replace("{member_name}", memberInfo.MemberProfileInfo.Member_Company);
                        Template_Html = Template_Html.Replace("{member_sealimg}", pub.FormatImgURL(memberInfo.MemberProfileInfo.Member_SealImg, "fullpath"));
                    }
                }
            }
            Template_Html = Template_Html + ordersInfo.Orders_ContractAdd;
        }
        return Template_Html;
    }




    /// <summary>
    /// 替换合同模板中的占位符信息
    /// </summary>
    /// <param name="Supplier_ID"></param>
    /// <returns></returns>
    public string ReplaceOrdersContract_New(OrdersInfo ordersInfo, int contact_no)
    {
        string Template_Html = "";
        string Contract_Template_TopContent = "";
        string ContractTemOnlyRead_Content = "";
        string Template_Product_Goods = "";
        string ContractTemOnlyEndContent = "";
        string ContractTemEndContent = "";
        ContractInfo ContractInfo = null;
        //  string Member_NickName = "";
        MemberInfo memberInfo = null;
        SupplierInfo Supplierinfo = null;
        string MemberSupplierCompanyName = "";
        if (ordersInfo != null)
        {
            memberInfo = new Member().GetMemberByID(ordersInfo.Orders_BuyerID);
            if (memberInfo != null)
            {
                Supplierinfo = new Supplier().GetSupplierByID(memberInfo.Member_SupplierID);

                if (memberInfo != null)
                {
                    //合同买方要使用商家公司名称
                    MemberSupplierCompanyName = Supplierinfo.Supplier_CompanyName;

                }
            }

        }
        SupplierInfo supplierInfo = supplier.GetSupplierByID(ordersInfo.Orders_SupplierID);
        ContractInfo = My_Contract.GetContractByID(contact_no, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
        if (ContractInfo != null)
        {
            Template_Product_Goods = new Contract().Contract_Orders_Goods_Print(ContractInfo.Contract_ID);
        }



        //合同模板 头文件部分
        ContractTemplateInfo ContractTemplateEntity = MyContract.GetContractTemplateBySign("Sell_Contract_Template_TopFuJian", pub.CreateUserPrivilege("d4d58107-0e58-485f-af9e-3b38c7ff9672"));
        if (ContractTemplateEntity != null)
        {
            Contract_Template_TopContent = ContractTemplateEntity.Contract_Template_Content;
            if (supplierInfo != null)
            {

                Contract_Template_TopContent = Contract_Template_TopContent.Replace("{supplier_name}", supplierInfo.Supplier_CompanyName);
                Contract_Template_TopContent = Contract_Template_TopContent.Replace("{member_name}", MemberSupplierCompanyName);
                Contract_Template_TopContent = Contract_Template_TopContent.Replace("{orders_goodslist}", GetOrdersGoods(ordersInfo));
                // Contract_Template_TopContent = Contract_Template_TopContent.Replace("{orders_goodslist}", GetOrdersGoods(ordersInfo));

            }
        }

        //合同模板 条款部分
        ContractTemplateInfo ContractTemOnlyReadEntity = MyContract.GetContractTemplateBySign("Sell_Contract_Template_OnlyRead", pub.CreateUserPrivilege("d4d58107-0e58-485f-af9e-3b38c7ff9672"));
        if (ContractTemOnlyReadEntity != null)
        {
            ContractTemOnlyRead_Content = ContractTemOnlyReadEntity.Contract_Template_Content;

        }


        //合同条款 尾文件部分
        ContractTemplateInfo ContractTemEndEntity = MyContract.GetContractTemplateBySign("Sell_Contract_Template_EndFuJian", pub.CreateUserPrivilege("d4d58107-0e58-485f-af9e-3b38c7ff9672"));
        if (ContractTemEndContent != null)
        {
            ContractTemOnlyEndContent = ContractTemEndEntity.Contract_Template_Content;
            if (supplierInfo != null)
            {
                ContractTemOnlyEndContent = ContractTemOnlyEndContent.Replace("{supplier_name}", supplierInfo.Supplier_CompanyName);
                ContractTemOnlyEndContent = ContractTemOnlyEndContent.Replace("{supplier_address}", supplierInfo.Supplier_Address);
                ContractTemOnlyEndContent = ContractTemOnlyEndContent.Replace("{supplier_man}", supplierInfo.Supplier_Corporate);
                ContractTemOnlyEndContent = ContractTemOnlyEndContent.Replace("{member_name}", Supplierinfo.Supplier_CompanyName);
                ContractTemOnlyEndContent = ContractTemOnlyEndContent.Replace("{member_address}", Supplierinfo.Supplier_Address);
                ContractTemOnlyEndContent = ContractTemOnlyEndContent.Replace("{member_man}", Supplierinfo.Supplier_Corporate);

            }
        }

        if (ordersInfo != null)
        {

            if (supplier != null)
            {

                if (ContractInfo != null)
                {

                    Template_Html = ContractInfo.Contract_Note;
                    if (supplierInfo != null)
                    {
                        Template_Html = Template_Html.Replace("{supplier_name}", supplierInfo.Supplier_CompanyName);
                    }
                    if (memberInfo != null)
                    {
                        Template_Html = Template_Html.Replace("{member_name}", MemberSupplierCompanyName);
                    }

                    Template_Html = Template_Html.Replace("{orders_address}", addr.DisplayAddress(ordersInfo.Orders_Address_State, ordersInfo.Orders_Address_City, ordersInfo.Orders_Address_County) + ordersInfo.Orders_Address_StreetAddress);
                    Template_Html = Template_Html.Replace("{supplier_sealimg}", pub.FormatImgURL(supplierInfo.Supplier_SealImg, "fullpath"));
                    Template_Html = Template_Html.Replace("{orders_paywayname}", ordersInfo.Orders_Payway_Name);
                    Template_Html = Template_Html.Replace("{orders_deliveryname}", ordersInfo.Orders_Delivery_Name);
                    Template_Html = Template_Html.Replace("{orders_goodslist}", GetOrdersGoods(ordersInfo));


                    if (memberInfo != null)
                    {
                        Template_Html = Template_Html.Replace("{member_name}", memberInfo.MemberProfileInfo.Member_Company);
                        Template_Html = Template_Html.Replace("{member_sealimg}", pub.FormatImgURL(memberInfo.MemberProfileInfo.Member_SealImg, "fullpath"));
                    }
                    Template_Html = Contract_Template_TopContent + Template_Html + ordersInfo.Orders_ContractAdd + ContractTemOnlyRead_Content + ContractTemOnlyEndContent;

                }

            }

        }
        return Template_Html;
    }



    /// <summary>
    /// 替换合同模板中的占位符信息
    /// </summary>
    /// <param name="Supplier_ID"></param>
    /// <returns></returns>
    public string ReplaceOrdersContract_SupplierNew(OrdersInfo ordersInfo, int contact_no)
    {
        string Template_Html = "";
        ContractInfo ContractInfo = null;
        MemberInfo memberInfo = GetMemberByID();
        SupplierInfo MemberSupplierinfo = null;
        string MemberSupplierCompanyName = "";
        if (memberInfo != null)
        {
            //Member_NickName = memberinfo.Member_NickName;
            MemberSupplierinfo = supplier.GetSupplierByID(memberInfo.Member_SupplierID);
            if (MemberSupplierinfo != null)
            {
                MemberSupplierCompanyName = MemberSupplierinfo.Supplier_CompanyName;
            }
        }

        if (ordersInfo != null)
        {
            SupplierInfo supplierInfo = supplier.GetSupplierByID(ordersInfo.Orders_SupplierID);
            if (supplier != null)
            {
                ContractInfo = My_Contract.GetContractByID(contact_no, pub.CreateUserPrivilege("a3465003-08b3-4a31-9103-28d16c57f2c8"));
                if (ContractInfo != null)
                {
                    Template_Html = ContractInfo.Contract_Template;

                    Template_Html = Template_Html.Replace("{supplier_name}", supplierInfo.Supplier_CompanyName);
                    Template_Html = Template_Html.Replace("{member_name}", MemberSupplierCompanyName);
                    Template_Html = Template_Html.Replace("{orders_address}", addr.DisplayAddress(ordersInfo.Orders_Address_State, ordersInfo.Orders_Address_City, ordersInfo.Orders_Address_County) + ordersInfo.Orders_Address_StreetAddress);
                    //   Template_Html = Template_Html.Replace("{contact_addtime}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));


                    Template_Html = Template_Html.Replace("{supplier_sealimg}", pub.FormatImgURL(supplierInfo.Supplier_SealImg, "fullpath"));


                    Template_Html = Template_Html.Replace("{orders_paywayname}", ordersInfo.Orders_Payway_Name);
                    Template_Html = Template_Html.Replace("{orders_deliveryname}", ordersInfo.Orders_Delivery_Name);


                    //if (ContractInfo.Contract_Template_Code == "Sell_Contract_Template")
                    //{
                    Template_Html = Template_Html.Replace("{orders_goodslist}", GetOrdersGoods(ordersInfo));
                    //}

                    if (memberInfo != null)
                    {
                        Template_Html = Template_Html.Replace("{member_name}", memberInfo.MemberProfileInfo.Member_Company);
                        Template_Html = Template_Html.Replace("{member_sealimg}", pub.FormatImgURL(memberInfo.MemberProfileInfo.Member_SealImg, "fullpath"));
                    }
                }
            }
            Template_Html = Template_Html + ordersInfo.Orders_ContractAdd;
        }
        return Template_Html;
    }

    /// <summary>
    /// 添加订单合同信息
    /// </summary>
    /// <param name="orders_sn"></param>
    /// <param name="Orders_ID"></param>
    public void AddOrdersContract(string orders_sn, int Orders_ID, string contact_no)
    {
        OrdersContractInfo orderscontractInfo = new OrdersContractInfo();
        orderscontractInfo.ID = 0;
        orderscontractInfo.SN = contact_no;
        orderscontractInfo.Name = CreateContractName(orders_sn) + ".pdf";
        orderscontractInfo.Orders_ID = Orders_ID;
        orderscontractInfo.Path = "/Download/" + encrypt.MD5(CreateContractName(orders_sn)) + ".pdf";
        orderscontractInfo.Addtime = DateTime.Now;
        orderscontractInfo.Site = pub.GetCurrentSite();
        MyOContract.AddOrdersContract(orderscontractInfo);
    }


    public string GetOrdersGoods(OrdersInfo ordersInfo)
    {
        StringBuilder strHTML = new StringBuilder();

        double total_price = 0;

        IList<OrdersGoodsInfo> listGoods = MyOrders.GetGoodsListByOrderID(ordersInfo.Orders_ID);
        if (listGoods != null)
        {
            strHTML.Append("<table border=\"1\" cellspacing=\"0\" cellpadding=\"1\"  style=\"width:100%;border-collapse:collapse;\">");
            strHTML.Append("<tr>");
            strHTML.Append("<td align=\"center\" width=\"200\" >商品名</td>");
            //strHTML.Append("<td align=\"center\" >编码</td>");
            strHTML.Append("<td align=\"center\" >规格</td>");
            strHTML.Append("<td align=\"center\" >单位</td>");
            strHTML.Append("<td align=\"center\" >数量</td>");
            strHTML.Append("<td align=\"center\" >单价</td>");
            strHTML.Append("<td align=\"center\" >金额</td>");
            strHTML.Append("</tr>");

            foreach (OrdersGoodsInfo entity in listGoods)
            {
                strHTML.Append("<tr>");
                strHTML.Append("<td align=\"center\">" + entity.Orders_Goods_Product_Name + "</td>");
                //strHTML.Append("<td align=\"center\">" + entity.Orders_Goods_Product_Code + "</td>");
                strHTML.Append("<td align=\"center\">" + entity.Orders_Goods_Product_Spec + "</td>");
                strHTML.Append("<td align=\"center\">件</td>");
                strHTML.Append("<td align=\"center\">" + entity.Orders_Goods_Amount + "</td>");
                strHTML.Append("<td align=\"center\">" + pub.FormatCurrency(entity.Orders_Goods_Product_Price) + "</td>");
                strHTML.Append("<td align=\"center\">" + pub.FormatCurrency(entity.Orders_Goods_Product_Price * entity.Orders_Goods_Amount) + "</td>");
                strHTML.Append("</tr>");

                total_price = total_price + (entity.Orders_Goods_Product_Price * entity.Orders_Goods_Amount);
            }

            strHTML.Append("<tr>");
            strHTML.Append("<td colspan=\"5\" align=\"right\" >合计:</td>");
            strHTML.Append("<td >" + pub.FormatCurrency(total_price) + "</td>");
            strHTML.Append("</tr>");

            strHTML.Append("<tr>");
            strHTML.Append("<td align=\"right\" >运费:</td>");
            strHTML.Append("<td colspan=\"5\">" + pub.FormatCurrency(ordersInfo.Orders_Total_Freight) + "</td>");
            strHTML.Append("</tr>");


            strHTML.Append("<tr>");
            strHTML.Append("<td colspan=\"5\" align=\"right\" >订单总金额:</td>");
            strHTML.Append("<td >" + pub.FormatCurrency(ordersInfo.Orders_Total_AllPrice) + "</td>");
            strHTML.Append("</tr>");


            strHTML.Append("</table>");

        }

        return strHTML.ToString();
    }


    public void Orders_Contract_Create()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        ContractTemplateInfo templateInfo = null;
        string Template_Html = "";
        OrdersInfo ordersinfo = MyOrders.GetMemberOrderInfoByID(Orders_ID, tools.NullInt(Session["member_id"]));
        if (ordersinfo != null)
        {
            //Template_Html = ReplaceOrdersContract(ordersinfo.Orders_SupplierID);

            //textsharp.CreatePDFContract(CreateContractName(ordersinfo.Orders_SN), Template_Html);

            //textsharp.PDFSign(CreateContractName(ordersinfo.Orders_SN), encrypt.MD5(CreateContractName(ordersinfo.Orders_SN)));
        }
    }


    public void Update_ERPParam_OrderID()
    {
        Session["erpparam_orderid"] = tools.CheckInt(Request["orders_id"]);
        Response.Write(tools.NullInt(Session["erpparam_orderid"]));
    }

    public void Get_ERPParam_OrderID()
    {
        int orders_id = tools.NullInt(Session["erpparam_orderid"]);
        Response.Write(tools.NullInt(Session["erpparam_orderid"]));
    }

    //订单发货单
    public void Orders_Delivery_List(int orders_id)
    {
        ISQLHelper DBHelper = SQLHelperFactory.CreateSQLHelper();
        StringBuilder strHTML = new StringBuilder();
        SupplierInfo supplierInfo = null;
        string supplier_name = "系统";
        PageInfo pageInfo = null;
        int member_id = tools.NullInt(Session["member_id"]);
        int current_page = tools.CheckInt(Request["page"]);
        if (current_page < 1)
        {
            current_page = 1;
        }
        int page_size = 20;
        int i = 0;
        string page_url = "?action=list";

        strHTML.Append("<table width=\"972\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
        strHTML.Append("<tbody>");
        strHTML.Append("<tr>");
        strHTML.Append("   <td height=\"30\" width=\"150\" align=\"center\">收货单号</td>");
        strHTML.Append("   <td height=\"30\" width=\"150\" align=\"center\">订单号</td>");
        strHTML.Append("   <td height=\"30\" width=\"150\" align=\"center\">签收状态</td>");
        strHTML.Append("   <td align=\"center\">司机电话</td>");
        //strHTML.Append("   <td width=\"100\" align=\"center\">物流单号</td>");
        //strHTML.Append("   <td width=\"100\" align=\"center\">收货数量</td>");
        strHTML.Append("   <td width=\"136\" align=\"center\">发货时间</td>");
        strHTML.Append("   <td width=\"100\" align=\"center\">操作</td>");
        strHTML.Append("</tr>");

        string SqlField = "OD.*, O.Orders_SN, O.Orders_Type, O.Orders_BuyerID";
        string SqlParam = "";
        if (orders_id > 0)
        {
            //SqlParam = "where  O.Orders_SupplierID = " + tools.NullInt(Session["supplier_id"]);
            SqlParam = "where  O.Orders_ID=" + orders_id + "  AND O.Orders_BuyerID = " + tools.NullInt(Session["member_id"]);
        }
        else
        {
            SqlParam = "where  O.Orders_BuyerID = " + tools.NullInt(Session["member_id"]);
        }
        string SqlTable = "Orders_Delivery AS OD INNER JOIN Orders AS O ON OD.Orders_Delivery_OrdersID = O.Orders_ID";




        string SqlOrder = "ORDER BY Orders_Delivery_ID DESC";
        string SqlCount = "SELECT COUNT(OD.Orders_Delivery_ID) FROM " + SqlTable + " " + SqlParam;
        DataTable DT = null;
        try
        {
            int PageSize = 20;
            int RecordCount = tools.NullInt(DBHelper.ExecuteScalar(SqlCount));
            int PageCount = tools.CalculatePages(RecordCount, PageSize);
            int CurrentPage = tools.DeterminePage(current_page, PageCount);

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("SELECT " + SqlField + " FROM " + SqlTable + " WHERE OD.Orders_Delivery_ID IN (");
            strSQL.Append("	SELECT Orders_Delivery_ID FROM (");
            strSQL.Append("		SELECT ROW_NUMBER() OVER(" + SqlOrder + ") AS RowNum, OD.Orders_Delivery_ID FROM " + SqlTable + " " + SqlParam);
            strSQL.Append("	) T");
            strSQL.Append("	WHERE RowNum > " + ((CurrentPage - 1) * PageSize) + " AND RowNum  <= " + (CurrentPage * PageSize));
            strSQL.Append(") " + SqlOrder);
            DT = DBHelper.Query(strSQL.ToString());

            if (DT != null)
            {
                foreach (DataRow RdrList in DT.Rows)
                {
                    i++;
                    int Orders_Delivery_Goods_ProductAmount = 0;
                    int Orders_Delivery_ID = tools.NullInt(RdrList["Orders_Delivery_ID"]);
                    //GetOrdersDeliveryByID
                    IList<OrdersDeliveryGoodsInfo> entitys = MyDelivery.GetOrdersDeliveryGoods(Orders_Delivery_ID);
                    if (entitys != null)
                    {
                        int i1 = 0;
                        foreach (var entity in entitys)
                        {
                            i1++;
                            if (i1 == 1)
                            {
                                Orders_Delivery_Goods_ProductAmount = entity.Orders_Delivery_Goods_ProductAmount;
                            }
                        }
                    }
                    if (i % 2 == 0)
                    {
                        strHTML.Append("<tr class=\"bg\">");
                    }
                    else
                    {
                        strHTML.Append("<tr>");
                    }
                    strHTML.Append("   <td height=\"35\" align=\"left\"><span><a href=\"order_delivery_view.aspx?Orders_Delivery_ID=" + Orders_Delivery_ID + "\" target=\"_blank\">" + tools.NullStr(RdrList["Orders_Delivery_DocNo"]) + "</a></span></td>");

                    strHTML.Append("   <td align=\"center\" ><span><a class=\"a_t12_blue\" href=\"order_view.aspx?orders_sn=" + tools.NullStr(RdrList["Orders_SN"]) + "\">" + tools.NullStr(RdrList["Orders_SN"]) + "</a></span></td>");
                    if (tools.NullInt(RdrList["Orders_Delivery_ReceiveStatus"]) == 0)
                    {
                        strHTML.Append("   <td align=\"center\">未签收</td>");
                    }
                    else if (tools.NullInt(RdrList["Orders_Delivery_ReceiveStatus"]) == 9)
                    {


                        strHTML.Append("   <td align=\"center\">签收未确认</td>");
                    }

                    else
                    {
                        strHTML.Append("   <td align=\"center\">已签收</td>");
                    }
                    strHTML.Append("   <td align=\"center\">" + tools.NullStr(RdrList["Orders_Delivery_DriverMobile"]) + "</td>");
                    //strHTML.Append("   <td align=\"center\">" + tools.NullStr(RdrList["Orders_Delivery_Code"]) + "</td>");
                    //strHTML.Append("   <td align=\"center\">" + Orders_Delivery_Goods_ProductAmount + "</td>");
                    //GetOrdersDeliveryByID();
                    //strHTML.Append("   <td align=\"center\">" + tools.NullStr(RdrList["Orders_Delivery_Goods_ProductAmount"]) + "</td>");
                    strHTML.Append("   <td align=\"center\">" + tools.NullDate(RdrList["Orders_Delivery_Addtime"]) + "</td>");
                    strHTML.Append("   <td height=\"35\" align=\"left\"><span><a href=\"order_delivery_view.aspx?Orders_Delivery_ID=" + Orders_Delivery_ID + "\" target=\"_blank\">查看</a></span></td>");
                    strHTML.Append(" </tr>");
                }
                Response.Write(strHTML.ToString());
                strHTML.Append("</tbody>");
                Response.Write("</table>");
                pub.Page(PageCount, current_page, page_url, PageSize, RecordCount);
            }
            else
            {
                strHTML.Append("<tr>");
                strHTML.Append("<td colspan=\"5\" style=\"text-align:center;\">暂无发货单信息！</td>");
                strHTML.Append("</tr>");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }



    }



    //订单详情页面 发货单   
    public string OrdersDetail_Delivery_List(int orders_id, int Order_status)
    {
        ISQLHelper DBHelper = SQLHelperFactory.CreateSQLHelper();
        StringBuilder strHTML = new StringBuilder();

        int member_id = tools.NullInt(Session["member_id"]);
        int current_page = tools.CheckInt(Request["page"]);
        if (current_page < 1)
        {
            current_page = 1;
        }

        int i = 0;
        string page_url = "?action=list";
        strHTML.Append("<div class=\"b06_info03_sz\">");

        strHTML.Append("<h3>");
        strHTML.Append("<ul>");
        strHTML.Append("<li style=\"width: 138px;\">收货单号</li>");
        strHTML.Append("<li style=\"width: 108px;\">运输方式</li>");
        strHTML.Append("<li style=\"width: 108px;\">签收状态</li>");
        strHTML.Append("<li style=\"width: 138px;\">发货数量</li>");
        strHTML.Append("<li style=\"width: 138px;\">货款金额</li>");
        strHTML.Append("<li style=\"width: 138px;\">付款时间</li>");
        strHTML.Append("<li style=\"width: 190px;\">操 作</li>");
        strHTML.Append("</ul>");
        strHTML.Append("<div class=\"clear\"></div>");
        strHTML.Append("</h3>");
        strHTML.Append("<div class=\"b06_info03_main_sz\">");
        strHTML.Append("<table width=\"972\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");



        //strHTML.Append("<table width=\"972\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
        //strHTML.Append("<tbody>");
        //strHTML.Append("<tr>");
        //strHTML.Append("   <td height=\"30\" width=\"150\" align=\"center\">收货单号</td>");
        //strHTML.Append("   <td height=\"30\" width=\"150\" align=\"center\">发货方式</td>");
        //strHTML.Append("   <td height=\"30\" width=\"150\" align=\"center\">状态</td>");
        //strHTML.Append("   <td align=\"center\">申请结算金额</td>");


        //strHTML.Append("   <td width=\"136\" align=\"center\">发货时间</td>");
        //strHTML.Append("   <td width=\"180\" align=\"center\">操作</td>");
        //strHTML.Append("</tr>");

        string SqlField = "OD.*, O.Orders_SN, O.Orders_Type, O.Orders_BuyerID";
        string SqlParam = "";
        if (orders_id > 0)
        {
            SqlParam = "where  O.Orders_ID=" + orders_id + "  AND O.Orders_BuyerID = " + tools.NullInt(Session["member_id"]);
        }
        else
        {
            SqlParam = "where  O.Orders_BuyerID = " + tools.NullInt(Session["member_id"]);
        }
        string SqlTable = "Orders_Delivery AS OD INNER JOIN Orders AS O ON OD.Orders_Delivery_OrdersID = O.Orders_ID";




        string SqlOrder = "ORDER BY Orders_Delivery_ID DESC";
        string SqlCount = "SELECT COUNT(OD.Orders_Delivery_ID) FROM " + SqlTable + " " + SqlParam;
        DataTable DT = null;
        try
        {
            int PageSize = 20;
            int RecordCount = tools.NullInt(DBHelper.ExecuteScalar(SqlCount));
            int PageCount = tools.CalculatePages(RecordCount, PageSize);
            int CurrentPage = tools.DeterminePage(current_page, PageCount);

            StringBuilder strSQL = new StringBuilder();
            strSQL.Append("SELECT " + SqlField + " FROM " + SqlTable + " WHERE OD.Orders_Delivery_ID IN (");
            strSQL.Append("	SELECT Orders_Delivery_ID FROM (");
            strSQL.Append("		SELECT ROW_NUMBER() OVER(" + SqlOrder + ") AS RowNum, OD.Orders_Delivery_ID FROM " + SqlTable + " " + SqlParam);
            strSQL.Append("	) T");
            strSQL.Append("	WHERE RowNum > " + ((CurrentPage - 1) * PageSize) + " AND RowNum  <= " + (CurrentPage * PageSize));
            strSQL.Append(") " + SqlOrder);
            DT = DBHelper.Query(strSQL.ToString());

            if (DT != null)
            {
                foreach (DataRow RdrList in DT.Rows)
                {
                    i++;
                    int Orders_Delivery_Goods_ProductAmount = 0;
                    int Orders_Delivery_ID = tools.NullInt(RdrList["Orders_Delivery_ID"]);
                    //GetOrdersDeliveryByID
                    IList<OrdersDeliveryGoodsInfo> entitys = MyDelivery.GetOrdersDeliveryGoods(Orders_Delivery_ID);
                    if (entitys != null)
                    {
                        int i1 = 0;
                        foreach (var entity in entitys)
                        {
                            i1++;
                            if (i1 == 1)
                            {
                                Orders_Delivery_Goods_ProductAmount = entity.Orders_Delivery_Goods_ProductAmount;
                            }
                        }
                    }
                    if (i % 2 == 0)
                    {
                        strHTML.Append("<tr class=\"bg\">");
                    }
                    else
                    {
                        strHTML.Append("<tr>");
                    }
                    strHTML.Append("   <td height=\"35\" align=\"left\" style=\"width: 138px;\"><span><a href=\"order_delivery_view.aspx?Orders_Delivery_ID=" + Orders_Delivery_ID + "\" target=\"_blank\" style=\"color:#ff6600;\">" + tools.NullStr(RdrList["Orders_Delivery_DocNo"]) + "</a></span></td>");



                    strHTML.Append("   <td align=\"center\" style=\"width: 108px;\" >" + tools.NullStr(RdrList["Orders_Delivery_TransportType"]) + "</td>");
                    // 收货状态0：未签收  1、9 结算中 2：已结算
                    if (tools.NullInt(RdrList["Orders_Delivery_ReceiveStatus"]) == 0)
                    {
                        strHTML.Append("   <td align=\"center\" style=\"width: 108px;\">未签收</td>");
                    }
                    else if (tools.NullInt(RdrList["Orders_Delivery_ReceiveStatus"]) == 2)
                    {
                        strHTML.Append("   <td align=\"center\" style=\"width: 108px;\">已签收</td>");
                    }
                    else
                    {
                        strHTML.Append("   <td align=\"center\" style=\"width: 108px;\">结算中</td>");
                    }


                    //计算当前收货单商品签收数量
                    //int OrdersDeliverySum = tools.CheckInt(DBHelper.ExecuteScalar("select sum(Orders_Delivery_Goods_ProductAmount)  from Orders_Delivery_Goods where Orders_Delivery_Goods_DeliveryID=(select Orders_Delivery_ID from Orders_Delivery where Orders_Delivery_ID=" + Orders_Delivery_ID + ")").ToString());




                    //商品单价
                    //double Orders_Delivery_Goods_ProductPrice = tools.CheckFloat(DBHelper.ExecuteScalar("select Orders_Delivery_Goods_ProductPrice from Orders_Delivery_Goods  where Orders_Delivery_Goods_DeliveryID=" + Orders_Delivery_ID + "").ToString());

                    //strHTML.Append("   <td align=\"center\" style=\"width: 138px;\">" + Orders_Delivery_Goods_ProductPrice + "</td>");


                    //计算当前收货单--发货数量
                    int OrdersDeliveryProductSum = tools.CheckInt(DBHelper.ExecuteScalar("select sum(Orders_Delivery_Goods_ProductAmount)  from Orders_Delivery_Goods where Orders_Delivery_Goods_DeliveryID=(select Orders_Delivery_ID from Orders_Delivery where Orders_Delivery_ID=" + Orders_Delivery_ID + ")").ToString());


                    //计算当前收货单--签收数量
                    int OrdersDeliveryReceiveSum = tools.CheckInt(DBHelper.ExecuteScalar("select sum(Orders_Delivery_Goods_ReceivedAmount)  from Orders_Delivery_Goods where Orders_Delivery_Goods_DeliveryID=(select Orders_Delivery_ID from Orders_Delivery where Orders_Delivery_ID=" + Orders_Delivery_ID + ")").ToString());


                    //签收数量
                    strHTML.Append("   <td align=\"center\" style=\"width: 138px;\">" + OrdersDeliveryProductSum + "</td>");


                    //发货单商品单价

                    //double Orders_Delivery_Goods_ProductPrice = tools.CheckFloat(DBHelper.ExecuteScalar("select Orders_Delivery_Goods_ProductPrice from Orders_Delivery_Goods  where Orders_Delivery_Goods_DeliveryID=" + Orders_Delivery_ID + "").ToString());

                    //贷款金额  
                    // 1:发货金额 
                    double ProductSum = tools.CheckFloat(DBHelper.ExecuteScalar("select SUM(Orders_Delivery_Goods_ProductAmount*Orders_Delivery_Goods_ProductPrice) from Orders_Delivery_Goods where  Orders_Delivery_Goods_DeliveryID=" + Orders_Delivery_ID + "").ToString());
                    // 2:签收金额
                    double ReceiveSum = tools.CheckFloat(DBHelper.ExecuteScalar("select SUM(Orders_Delivery_Goods_ReceivedAmount*Orders_Delivery_Goods_ProductPrice) from Orders_Delivery_Goods where  Orders_Delivery_Goods_DeliveryID=" + Orders_Delivery_ID + "").ToString());





                    //未签收显示,贷款金额(发货数量*商品单价)
                    if (tools.NullInt(RdrList["Orders_Delivery_ReceiveStatus"]) == 0)
                    {
                        //未签收显示 所有商品  发货金额*发货数量  累加
                        strHTML.Append("   <td align=\"center\" style=\"width: 138px;\">" + ProductSum + "</td>");
                    }
                    else
                    {
                        //签收显示 所有商品  收货金额*发货数量  累加
                        strHTML.Append("   <td align=\"center\" style=\"width: 138px;\">" + ReceiveSum + "</td>");
                    }

                    strHTML.Append("   <td align=\"center\" style=\"width: 138px;\">" + tools.NullDate(RdrList["Orders_Delivery_Addtime"]) + "</td>");
                    strHTML.Append("   <td align=\"center\" style=\"width: 190px;\"><a href=\"order_delivery_view.aspx?Orders_Delivery_ID=" + Orders_Delivery_ID + "\" style=\"color:#ff6600;\">查看</a>");

                    if (Order_status == 1 && tools.NullInt(RdrList["Orders_Delivery_DeliveryStatus"]) == 1 && tools.NullInt(RdrList["Orders_Delivery_ReceiveStatus"]) == 9)
                    {

                        strHTML.Append("   <a href=\"/member/order_delivery_view.aspx?Orders_Delivery_ID=" + Orders_Delivery_ID + "\" target=\"_blank\" style=\"color:#ff6600;\">确认结算</a>");
                    }





                    //strHTML.Append("   <a href=\"order_delivery_view.aspx?Orders_Delivery_ID=" + Orders_Delivery_ID + "\">申请结算</a>");

                    //strHTML.Append("      </td>");
                    strHTML.Append(" </tr>");
                }
                //Response.Write(strHTML.ToString());
                strHTML.Append("</div>");
                strHTML.Append("</tbody>");
                strHTML.Append("</table>");

                //Response.Write("</table>");
                //pub.Page(PageCount, current_page, page_url, PageSize, RecordCount);
            }
            else
            {
                strHTML.Append("<tr>");
                strHTML.Append("<td colspan=\"6\" style=\"text-align:center;\">暂无发货单信息！</td>");
                strHTML.Append("</tr>");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return strHTML.ToString();

    }

    public bool CheckAllReceive(int Orders_ID)
    {
        ISQLHelper DBHelper = SQLHelperFactory.CreateSQLHelper();
        int amount = tools.NullInt(DBHelper.ExecuteScalar("select count(0) from Orders_Delivery where Orders_Delivery_OrdersID=" + Orders_ID + " and Orders_Delivery_ReceiveStatus=0"));
        if (amount > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //查看销售发货单
    public void order_Delivery_Detail(OrdersDeliveryInfo deliveryinfo, OrdersInfo entity, bool IsEdit)
    {
        StringBuilder strHTML = new StringBuilder();
        //MemberInfo memberInfo = null;
        //string memberName = "";
        string strOrdersResponsible = string.Empty;//初始化运输责任
        strOrdersResponsible = entity.Orders_Responsible == 1 ? "卖家责任" : "买家责任";
        if (entity != null)
        {
            //商品清单
            if (IsEdit)
            {
                strHTML.Append(Order_Delivery_Detail_GoodsEdit(entity.Orders_Status, deliveryinfo));
            }
            else
            {
                strHTML.Append("<div class=\"blk06\">");
                strHTML.Append("<h2><span>");


                //if (entity.Orders_Status == 1 && entity.Orders_PaymentStatus == 4 && deliveryinfo.Orders_Delivery_ReceiveStatus == 1)
                //{
                //    strHTML.Append("<a href=\"/member/orders_do.aspx?action=orderacceptsettle&delivery_id=" + deliveryinfo.Orders_Delivery_ID + "\" class=\"a02\">确认签收</a>");
                //}

                strHTML.Append("</span>收货单信息</h2>");

                strHTML.Append("<div class=\"b06_main_sz\">");
                strHTML.Append("<p><span>订单编号：</span>" + entity.Orders_SN + "</p>");
                strHTML.Append("<p><span>收 货 人：</span>" + entity.Orders_Address_Name + "</p>");
                strHTML.Append("<p><span>收货地址：</span>" + addr.DisplayAddress(entity.Orders_Address_State, entity.Orders_Address_City, entity.Orders_Address_County) + entity.Orders_Address_StreetAddress + "    " + entity.Orders_Address_Name + "    " + entity.Orders_Address_Mobile + "</p>");
                strHTML.Append("<p><span>运送责任：</span>" + strOrdersResponsible + "</p>");
                if (deliveryinfo.Orders_Delivery_ReceiveStatus == 0)
                {
                    strHTML.Append("<p><span>签收状态：</span>未签收</p>");
                }
                else if (deliveryinfo.Orders_Delivery_ReceiveStatus == 1)
                {
                    strHTML.Append("<p><span>签收状态：</span>已签收</p>");
                }
                else if (deliveryinfo.Orders_Delivery_ReceiveStatus == 9)
                {
                    strHTML.Append("<p><span>签收状态：</span>签收未确认</p>");
                }
                else
                {
                    strHTML.Append("<p><span>签收状态：</span>已结算</p>");
                }
                strHTML.Append("<p><span>物流公司：</span>" + deliveryinfo.Orders_Delivery_companyName + "</p>");
                strHTML.Append("<p><span>物流单号：</span>" + deliveryinfo.Orders_Delivery_DocNo + "</p>");

                strHTML.Append("<p><span>司机电话：</span>" + deliveryinfo.Orders_Delivery_DriverMobile + "</p>");
                strHTML.Append("<p><span>车牌号码：</span>" + deliveryinfo.Orders_Delivery_PlateNum + "</p>");
                strHTML.Append("<p><span>运输方式：</span>" + deliveryinfo.Orders_Delivery_TransportType + "</p>");


                strHTML.Append("<p><span>物流备注：</span>" + deliveryinfo.Orders_Delivery_Note + "</p>");
                strHTML.Append("<p><span>发货时间：</span>" + deliveryinfo.Orders_Delivery_Addtime + "</p>");
                strHTML.Append("</div>");
                strHTML.Append("</div>");
                strHTML.Append(Order_Delivery_Detail_Goods(entity.Orders_Status, deliveryinfo));
            }



            Response.Write(strHTML.ToString());
        }
        else
        {
            Response.Redirect("/supplier/orders_list.aspx?menu=1");
        }
    }

    public string Order_Delivery_Detail_Goods(int Orders_Status, OrdersDeliveryInfo entity)
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<div class=\"b06_info03_sz\">");
        strHTML.Append("<h2>商品清单</h2>");
        strHTML.Append("<h3>");
        strHTML.Append("<ul>");

        strHTML.Append("<li style=\"width: 394px;\">商品</li>");
        strHTML.Append("<li style=\"width: 142px;\">单价</li>");
        strHTML.Append("<li style=\"width: 142px;\">发货数量</li>");
        strHTML.Append("<li style=\"width: 142px;\">签收数量</li>");
        strHTML.Append("<li style=\"width: 142px; border-right: none;\">小计（元）</li>");
        strHTML.Append("</ul>");
        strHTML.Append("<div class=\"clear\"></div>");
        strHTML.Append("</h3>");
        strHTML.Append("<div class=\"b06_info03_main_sz\">");

        strHTML.Append("<form name=\"formadd\" id=\"formadd\" method=\"post\" action=\"/member/orders_do.aspx\">");
        strHTML.Append("<table width=\"972\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
        IList<OrdersDeliveryGoodsInfo> entitys = MyDelivery.GetOrdersDeliveryGoods(entity.Orders_Delivery_ID);
        if (entitys != null)
        {
            foreach (OrdersDeliveryGoodsInfo goodsinfo in entitys)
            {
                string productURL = "";
                strHTML.Append("<tr>");
                if (goodsinfo.Orders_Delivery_Goods_ProductID > 0)
                {
                    productURL = pageurl.FormatURL(pageurl.product_detail, goodsinfo.Orders_Delivery_Goods_ProductID.ToString());

                    strHTML.Append(" <td width=\"398\">");
                    strHTML.Append("<dl>");
                    strHTML.Append("<dt><a href=\"" + productURL + "\"  target=\"_blank\"><img src=\"" + pub.FormatImgURL(goodsinfo.Orders_Delivery_Goods_ProductImg, "thumbnail") + "\"  onload=\"javascript:AutosizeImage(this,82,82);\" /></a></dt>");
                    strHTML.Append("<dd>");

                    //strHTML.Append("<a href=""><p>" + goodsinfo.Orders_Delivery_Goods_ProductName + "</p></a>");
                    strHTML.Append(" <a href=\"" + productURL + "\"  target=\"_blank\"><p><strong>" + goodsinfo.Orders_Delivery_Goods_ProductName + "</strong></p></a>");
                    //strHTML.Append("<p><span>编号：" + goodsinfo.Orders_Delivery_Goods_ProductCode + "</span></p>");
                    strHTML.Append("<p><span>" + new Product().Product_Extend_Content_New(goodsinfo.Orders_Delivery_Goods_ProductID) + "</span></p>");
                    strHTML.Append("</dd>");
                    strHTML.Append("<div class=\"clear\"></div>");
                    strHTML.Append("</dl>");
                    strHTML.Append("</td>");
                    strHTML.Append("<td width=\"192\">" + pub.FormatCurrency(goodsinfo.Orders_Delivery_Goods_ProductPrice) + "</td>");
                    strHTML.Append("<td width=\"142\">" + goodsinfo.Orders_Delivery_Goods_ProductAmount + "</td>");
                    //if (Orders_Status == 1 && entity.Orders_Delivery_DeliveryStatus == 1 && entity.Orders_Delivery_ReceiveStatus == 0)
                    //{
                    //    strHTML.Append("<td width=\"142\"><input class=\"received_input\" type=\"text\" name=\"" + goodsinfo.Orders_Delivery_Goods_ID + "_ReceivedAmount\" value=\"" + (goodsinfo.Orders_Delivery_Goods_ReceivedAmount) + "\" onblur=\"$('#price_" + goodsinfo.Orders_Delivery_Goods_ID + "').html($(this).val()*" + goodsinfo.Orders_Delivery_Goods_ProductPrice + ")\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" /></td>");


                    //    strHTML.Append("<td width=\"142\" >￥<span id=\"price_" + goodsinfo.Orders_Delivery_Goods_ID + "\">" + (goodsinfo.Orders_Delivery_Goods_ReceivedAmount * goodsinfo.Orders_Delivery_Goods_ProductPrice) + "</span></td>");

                    //}
                    //else
                    //{

                    if (Orders_Status == 1 && entity.Orders_Delivery_DeliveryStatus == 1 && entity.Orders_Delivery_ReceiveStatus == 0)
                    {//若未签收  商品签收数量等于发货数量
                        strHTML.Append("<td width=\"142\">" + goodsinfo.Orders_Delivery_Goods_ProductAmount + "</td>");
                    }
                    else
                    {
                        //若牵手  商品签收数量等于签收数量
                        strHTML.Append("<td width=\"142\">" + goodsinfo.Orders_Delivery_Goods_ReceivedAmount + "</td>");
                    }

                    strHTML.Append("<td width=\"142\" >￥" + (goodsinfo.Orders_Delivery_Goods_ReceivedAmount * goodsinfo.Orders_Delivery_Goods_ProductPrice) + "</td>");

                    //}
                }
                strHTML.Append("</tr>");
            }
        }

        strHTML.Append("</table>");
        //确认签收  确认签收 商品订单状态:1 已确定    发货状态:1 已发货   收货状态:1 已收货
        if (Orders_Status == 1 && entity.Orders_Delivery_DeliveryStatus == 1 && entity.Orders_Delivery_ReceiveStatus == 9)
        {
            //strHTML.Append("<div style=\"padding:10px;\">");
            strHTML.Append("<div style=\"display: inline; float: left; padding: 10px 170px 10px 10px;\">");
            strHTML.Append("<input type=\"hidden\" value=\"orderaccept\" name=\"action\">");
            strHTML.Append("<input type=\"hidden\" value=\"" + entity.Orders_Delivery_ID + "\" name=\"Delivery_ID\">");
            //strHTML.Append("<a href=\"javascript:void();\" onclick=\"$('#formadd').submit();\" class=\"a11\" style=\" width:179px; height:41px; background:#ff6600; color:#fff; text-align:center; font-size:16px; line-height:41px;\">确认签收</a>");
            //strHTML.Append("<a href=\"/member/orders_do.aspx?action=orderacceptsettle&delivery_id=" + entity.Orders_Delivery_ID + "\" class=\"a11\" style=\" width:179px; height:41px; background:#ff6600; color:#fff; text-align:center; font-size:16px; line-height:41px;\">确认签收</a>");
            strHTML.Append("<a href=\"javascript:void(0);\" onclick=\"confirmreceive(" + entity.Orders_Delivery_ID + ");\" class=\"a11\" style=\" width:179px; height:41px; background:#ff6600; color:#fff; text-align:center; font-size:16px; line-height:41px;\">确认签收</a>");



            strHTML.Append("</div>");
        }

        //会员修改发货单状态条件(申请扣款（改单价）)  订单确认  发货状态: 已发货 签收状态:只要非签收结算都能修改价格
        //if (Orders_Status == 1 && entity.Orders_Delivery_DeliveryStatus == 1 && entity.Orders_Delivery_ReceiveStatus != 2)
        //{
        //    strHTML.Append("<div style=\"display: inline;    float: left;    padding: 10px 140px;   \">");
        //    strHTML.Append("<a href=\"/member/order_delivery_Edit.aspx?Orders_Delivery_ID=" + entity.Orders_Delivery_ID + " \"  class=\"a11\"  style=\" width:179px; height:41px; background:#ff6600; color:#fff; text-align:center; font-size:16px; line-height:41px;\">修改发货单</a>");
        //    strHTML.Append("</div>");
        //}





        strHTML.Append("</form>");
        strHTML.Append("</div>");
        strHTML.Append("</div>");

        return strHTML.ToString();
    }



    public string Order_Delivery_Detail_GoodsEdit(int Orders_Status, OrdersDeliveryInfo entity)
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<div class=\"b06_info03_sz\">");
        strHTML.Append("<h2>收货单商品清单</h2>");
        strHTML.Append("<h3>");
        strHTML.Append("<ul>");

        strHTML.Append("<li style=\"width: 394px;\">商品</li>");
        strHTML.Append("<li style=\"width: 142px;\">发货数量</li>");
        strHTML.Append("<li style=\"width: 142px;\">单价</li>");

        strHTML.Append("<li style=\"width: 142px;\">签收数量</li>");
        strHTML.Append("<li style=\"width: 142px; border-right: none;\">小计（元）</li>");
        strHTML.Append("</ul>");
        strHTML.Append("<div class=\"clear\"></div>");
        strHTML.Append("</h3>");
        strHTML.Append("<div class=\"b06_info03_main_sz\">");

        strHTML.Append("<form name=\"formadd\" id=\"formadd\" method=\"post\" action=\"/member/orders_do.aspx\">");
        strHTML.Append("<table width=\"972\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
        IList<OrdersDeliveryGoodsInfo> entitys = MyDelivery.GetOrdersDeliveryGoods(entity.Orders_Delivery_ID);
        if (entitys != null)
        {
            int i = 0;
            foreach (OrdersDeliveryGoodsInfo goodsinfo in entitys)
            {
                i++;
                string productURL = "";
                strHTML.Append("<tr>");
                if (goodsinfo.Orders_Delivery_Goods_ProductID > 0)
                {

                    productURL = pageurl.FormatURL(pageurl.product_detail, goodsinfo.Orders_Delivery_Goods_ProductID.ToString());

                    strHTML.Append(" <td width=\"448\">");
                    strHTML.Append("<dl>");
                    strHTML.Append("<dt><a href=\"" + productURL + "\"  target=\"_blank\"><img src=\"" + pub.FormatImgURL(goodsinfo.Orders_Delivery_Goods_ProductImg, "thumbnail") + "\"  onload=\"javascript:AutosizeImage(this,82,82);\" /></a></dt>");
                    strHTML.Append("<dd>");

                    //strHTML.Append("<a href=""><p>" + goodsinfo.Orders_Delivery_Goods_ProductName + "</p></a>");
                    strHTML.Append(" <a href=\"" + productURL + "\"  target=\"_blank\"><p><strong>" + goodsinfo.Orders_Delivery_Goods_ProductName + "</strong></p></a>");
                    //strHTML.Append("<p><span>编号：" + goodsinfo.Orders_Delivery_Goods_ProductCode + "</span></p>");
                    strHTML.Append("<p><span>" + new Product().Product_Extend_Content_New(goodsinfo.Orders_Delivery_Goods_ProductID) + "</span></p>");
                    strHTML.Append("</dd>");
                    strHTML.Append("<div class=\"clear\"></div>");
                    strHTML.Append("</dl>");
                    strHTML.Append("</td>");
                    //strHTML.Append("<td width=\"192\">" + pub.FormatCurrency(goodsinfo.Orders_Delivery_Goods_ProductPrice) + "</td>");
                    strHTML.Append("<td width=\"242\">" + goodsinfo.Orders_Delivery_Goods_ProductAmount + "</td>");

                    if (Orders_Status == 1 && entity.Orders_Delivery_DeliveryStatus == 1 && entity.Orders_Delivery_ReceiveStatus != 2)
                    {


                        strHTML.Append("<td width=\"191\" id=\"Orders_Goods_Product_Price\" value=\"" + pub.FormatCurrency(goodsinfo.Orders_Delivery_Goods_ID) + "\"> <input onblur=\"sum('buy_amount_" + goodsinfo.Orders_Delivery_Goods_ID + "',this.id,'Orders_Goods_EveryProduct_Sum" + i + "'," + entitys.Count + ")\" name=\"buy_price_" + goodsinfo.Orders_Delivery_Goods_ID + "\" id=\"buy_price_" + goodsinfo.Orders_Delivery_Goods_ID + "\" type=\"text\" value=\"" + goodsinfo.Orders_Delivery_Goods_ProductPrice + "\" /></td>");

                        strHTML.Append("<td width=\"194\" id=\"Orders_Goods_Amount\"><input name=\"buy_amount_" + goodsinfo.Orders_Delivery_Goods_ID + "\" onblur=\"sum(this.id,'buy_price_" + goodsinfo.Orders_Delivery_Goods_ID + "','Orders_Goods_EveryProduct_Sum" + i + "'," + entitys.Count + ")\" id=\"buy_amount_" + goodsinfo.Orders_Delivery_Goods_ID + "\" type=\"text\" value=\"" + goodsinfo.Orders_Delivery_Goods_ReceivedAmount + "\"                       /></td>");



                        strHTML.Append("<td width=\"190\" id=\"Orders_Goods_EveryProduct_Sum" + i + "\">");
                        strHTML.Append(" " + goodsinfo.Orders_Delivery_Goods_ProductPrice * goodsinfo.Orders_Delivery_Goods_ReceivedAmount + "");
                        strHTML.Append("  </td>");
                    }
                    else
                    {
                        strHTML.Append("<td width=\"142\">" + goodsinfo.Orders_Delivery_Goods_ProductPrice + "</td>");
                        strHTML.Append("<td width=\"142\">" + goodsinfo.Orders_Delivery_Goods_ReceivedAmount + "</td>");
                        strHTML.Append("<td width=\"142\" >￥" + (goodsinfo.Orders_Delivery_Goods_ReceivedAmount * goodsinfo.Orders_Delivery_Goods_ProductPrice) + "</td>");

                    }
                }
                strHTML.Append("</tr>");
            }
        }

        strHTML.Append("</table>");
        if ((Orders_Status == 1 && entity.Orders_Delivery_DeliveryStatus == 1 && entity.Orders_Delivery_ReceiveStatus == 0) || (Orders_Status == 1 && entity.Orders_Delivery_DeliveryStatus == 1 && entity.Orders_Delivery_ReceiveStatus == 1))
        {
            strHTML.Append("<div style=\"padding:10px;\">");
            strHTML.Append("<div style=\"display: inline;    float: left;    padding: 10px;    width: 20px;\">");
            strHTML.Append("<input type=\"hidden\" value=\"orderacceptedit\" name=\"action\">");
            strHTML.Append("<input type=\"hidden\" value=\"" + entity.Orders_Delivery_OrdersID + "\" name=\"Orders_ID\">");
            strHTML.Append("<input type=\"hidden\" value=\"" + entity.Orders_Delivery_ID + "\" name=\"Delivery_ID\">");
            strHTML.Append("<a href=\"javascript:void();\" onclick=\"$('#formadd').submit();\" class=\"a11\"></a>");
            strHTML.Append("</div>");
        }



        strHTML.Append("</form>");
        strHTML.Append("</div>");
        strHTML.Append("</div>");

        return strHTML.ToString();
    }

    public OrdersDeliveryInfo GetOrdersDeliveryByID(int id)
    {

        return MyDelivery.GetOrdersDeliveryByID(id, pub.CreateUserPrivilege("f606309a-2aa9-42e3-9d45-e0f306682a29"));

    }

    #region 注释
    //发货单签收结算
    //public void Orders_Delivery_AcceptSettle()
    //{
    //    int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
    //    int Orders_Delivery_ID = tools.CheckInt(Request["Delivery_ID"]);
    //    int supplier_id = tools.NullInt(DBHelper.ExecuteScalar("select Orders_SupplierID from Orders where Orders_ID=(select Orders_Delivery_OrdersID from Orders_Delivery where Orders_Delivery_ID=" + Orders_Delivery_ID + ")"));


    //    OrdersDeliveryInfo ODEntity = GetOrdersDeliveryByID(Orders_Delivery_ID);
    //    if (ODEntity == null)
    //    {
    //        pub.Msg("error", "错误信息", "发货单不存在", false, "index.aspx");
    //    }
    //    Orders_ID = ODEntity.Orders_Delivery_OrdersID;



    //    OrdersInfo ordersinfo = MyOrders.GetSupplierOrderInfoByID(Orders_ID, supplier_id);
    //    // OrdersInfo ordersinfo =MyMember.getorder (Orders_ID, tools.NullInt(Session["supplier_id"]));

    //    if (ordersinfo == null)
    //    {
    //        pub.Msg("error", "错误信息", "记录不存在", false, "/member/order_delivery_list.aspx");
    //    }
    //    if (ordersinfo.Orders_Status == 1 && ordersinfo.Orders_PaymentStatus == 4 && ODEntity.Orders_Delivery_ReceiveStatus == 1)
    //    {

    //    }
    //    else
    //    {
    //        pub.Msg("error", "错误信息", "记录不存在", false, "/member/order_delivery_list.aspx");
    //    }
    //    //获取本单已结算签收金额
    //    double totalreceiveprice = tools.NullDbl(DBHelper.ExecuteScalar("select sum(Orders_Delivery_Goods_ReceivedAmount*Orders_Delivery_Goods_ProductPrice) as totalprice from Orders_Delivery inner join Orders_Delivery_Goods on Orders_Delivery_Goods_DeliveryID=Orders_Delivery_ID where Orders_Delivery_ReceiveStatus=2 and Orders_Delivery_OrdersID=" + ordersinfo.Orders_ID));
    //    //获取本发货单签收金额
    //    double curreceiveprice = tools.NullDbl(DBHelper.ExecuteScalar("select sum(Orders_Delivery_Goods_ReceivedAmount*Orders_Delivery_Goods_ProductPrice) as totalprice from Orders_Delivery inner join Orders_Delivery_Goods on Orders_Delivery_Goods_DeliveryID=Orders_Delivery_ID where Orders_Delivery_ID=" + ODEntity.Orders_Delivery_ID));
    //    //获取本发货单签收佣金金额
    //    double CommissionPrice = tools.NullDbl(DBHelper.ExecuteScalar("select sum(Orders_Delivery_Goods_ReceivedAmount*Orders_Delivery_Goods_brokerage*Orders_Delivery_Goods_ProductPrice) as totalprice from Orders_Delivery inner join Orders_Delivery_Goods on Orders_Delivery_Goods_DeliveryID=Orders_Delivery_ID where Orders_Delivery_ID=" + ODEntity.Orders_Delivery_ID));
    //    CommissionPrice = Math.Round(CommissionPrice, 2);
    //    //结算金额超过采购总金额
    //    if (totalreceiveprice + curreceiveprice > ordersinfo.Orders_Total_AllPrice)
    //    {
    //        int Supplier_ID1 = 0;
    //        string supplier_name = "";
    //        ZhongXin mycredit = new ZhongXin();
    //        //MemberInfo memberinfo1 = GetMemberByID(ordersinfo.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
    //        MemberInfo memberinfo1 = GetMemberByID(ordersinfo.Orders_BuyerID);
    //        if (memberinfo1 != null)
    //        {
    //            Supplier_ID1 = memberinfo1.Member_SupplierID;

    //        }
    //        if (Supplier_ID1 > 0)
    //        {
    //            SupplierInfo supplierinfo1 = MySupplier.GetSupplierByID(Supplier_ID1, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
    //            if (supplierinfo1 != null)
    //            {
    //                supplier_name = supplierinfo1.Supplier_CompanyName;
    //            }


    //            ZhongXinInfo accountinfo = mycredit.GetZhongXinBySuppleir(Supplier_ID1);
    //            if (accountinfo != null)
    //            {
    //                decimal accountremain = 0;
    //                accountremain = mycredit.GetAmount(accountinfo.SubAccount);
    //                if (accountremain < ((decimal)curreceiveprice - ((decimal)ordersinfo.Orders_Total_AllPrice - (decimal)totalreceiveprice)))
    //                {
    //                    pub.Msg("error", "错误信息", "您的账户余额不足，请联系买方充值入金！", false, "{back}");
    //                }

    //                double accountprice = curreceiveprice - (ordersinfo.Orders_Total_AllPrice - totalreceiveprice);

    //                string strResult = mycredit.ContractDeliveryAccept(Supplier_ID1, ordersinfo.Orders_SupplierID, curreceiveprice - accountprice, accountprice, CommissionPrice, "订单编号：" + ordersinfo.Orders_SN, ODEntity.Orders_Delivery_DocNo, ordersinfo.Orders_SN);

    //                if (strResult == "true")
    //                {

    //                    new Orders().Orders_Log(ordersinfo.Orders_ID, tools.NullStr(Session["supplier_email"]), "发货单签收结算", "成功", "发货单[" + ODEntity.Orders_Delivery_DocNo + "]签收结算:" + pub.FormatCurrency(curreceiveprice));

    //                    ////添加短信提醒2015-5-14
    //                    //MemberInfo memberinfo = MyMEM.GetMemberByID(orders.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
    //                    //if (memberinfo != null)
    //                    //{

    //                    //    pub.DuanXin(memberinfo.Member_LoginMobile, "您通过" + payway.Pay_Way_Name + "向供应商支付了" + pub.FormatCurrency(Orders_Total_AllPrice) + "元，订单号：" + orders.Orders_SN + "，请确认是您亲自操作，有问题请致电400-8108-802【易耐网】");//买方短信提醒(编号09)
    //                    //}
    //                    ODEntity.Orders_Delivery_ReceiveStatus = 2;

    //                    if (MyDelivery.EditOrdersDelivery(ODEntity, pub.CreateUserPrivilege("")))
    //                    {
    //                        Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["supplier_email"]), "发货单签收结算", "成功", "发货单" + ODEntity.Orders_Delivery_DocNo + "签收结算！");

    //                    }
    //                    pub.Msg("positive", "操作成功", "操作成功", true, "/member/order_delivery_list.aspx");
    //                    //
    //                }
    //                else
    //                {
    //                    //new ZhongXin().SaveZhongXinAccountLog(ordersinfo.Orders_BuyerID, curreceiveprice * -1, "[撤销][签收结算]订单编号：" + ordersinfo.Orders_SN);

    //                    pub.Msg("error", "错误信息", strResult, false, "{back}");
    //                }
    //            }
    //            else
    //            {
    //                pub.Msg("error", "错误信息", "商家未开通中信账户！", false, "{back}");
    //            }

    //        }
    //        else
    //        {
    //            pub.Msg("error", "错误信息", "支付失败，请稍后重试！", false, "{back}");
    //        }
    //    }
    //    else
    //    {
    //        int Supplier_ID1 = 0;
    //        string supplier_name = "";
    //        ZhongXin mycredit = new ZhongXin();
    //        MemberInfo memberinfo1 = GetMemberByID(ordersinfo.Orders_BuyerID);
    //        if (memberinfo1 != null)
    //        {
    //            Supplier_ID1 = memberinfo1.Member_SupplierID;

    //        }
    //        if (Supplier_ID1 > 0)
    //        {
    //            SupplierInfo supplierinfo1 = MySupplier.GetSupplierByID(Supplier_ID1, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
    //            if (supplierinfo1 != null)
    //            {
    //                supplier_name = supplierinfo1.Supplier_CompanyName;
    //            }

    //            ZhongXinInfo accountinfo = mycredit.GetZhongXinBySuppleir(Supplier_ID1);
    //            if (accountinfo != null)
    //            {
    //                string strResult = mycredit.ContractDeliveryAccept(Supplier_ID1, ordersinfo.Orders_SupplierID, curreceiveprice, 0, CommissionPrice, "订单编号：" + ordersinfo.Orders_SN, ODEntity.Orders_Delivery_DocNo, ordersinfo.Orders_SN);

    //                if (strResult == "true")
    //                {



    //                    new Orders().Orders_Log(ordersinfo.Orders_ID, tools.NullStr(Session["supplier_email"]), "发货单签收结算", "成功", "发货单[" + ODEntity.Orders_Delivery_DocNo + "]签收结算:" + pub.FormatCurrency(curreceiveprice));

    //                    ////添加短信提醒2015-5-14
    //                    //MemberInfo memberinfo = MyMEM.GetMemberByID(orders.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
    //                    //if (memberinfo != null)
    //                    //{

    //                    //    pub.DuanXin(memberinfo.Member_LoginMobile, "您通过" + payway.Pay_Way_Name + "向供应商支付了" + pub.FormatCurrency(Orders_Total_AllPrice) + "元，订单号：" + orders.Orders_SN + "，请确认是您亲自操作，有问题请致电400-8108-802【易耐网】");//买方短信提醒(编号09)
    //                    //}
    //                    ODEntity.Orders_Delivery_ReceiveStatus = 2;

    //                    if (MyDelivery.EditOrdersDelivery(ODEntity, pub.CreateUserPrivilege("")))
    //                    {
    //                        Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["supplier_email"]), "发货单签收结算", "成功", "发货单" + ODEntity.Orders_Delivery_DocNo + "签收结算！");

    //                    }
    //                    pub.Msg("positive", "操作成功", "操作成功", true, "/member/order_delivery_list.aspx");
    //                    //
    //                }
    //                else
    //                {
    //                    //new ZhongXin().SaveZhongXinAccountLog(ordersinfo.Orders_BuyerID, curreceiveprice * -1, "[撤销][签收结算]订单编号：" + ordersinfo.Orders_SN);

    //                    pub.Msg("error", "错误信息", strResult, false, "{back}");
    //                }
    //            }
    //            else
    //            {
    //                pub.Msg("error", "错误信息", "您尚未开通中信账户！", false, "{back}");
    //            }

    //        }
    //        else
    //        {
    //            pub.Msg("error", "错误信息", "支付失败，请稍后重试！", false, "{back}");
    //        }

    //    }



    //    Response.Redirect("/member/index.aspx");
    //}
    #endregion

    #region 确认签收

    /// <summary>
    /// 确认签收
    /// </summary>
    /// <returns></returns>
    public void Orders_Delivery_AcceptSettle()
    {

        //判断是否为主账户支付
        if (tools.NullStr(Session["subPrivilege"]) == "")
        {
            //实现确认签收
            Achieve_Confirm_Receipt();
        }
        else
        {
            //获取子帐号拥有权限
            string Member_Permissions = Session["subPrivilege"].ToString();
            List<string> oTempList = new List<string>(Member_Permissions.Split(','));
            //判断子账户是否拥有支付权限ps:支付权限 ： 6
            if (oTempList.Contains("6"))
            {
                //实现确认签收
                Achieve_Confirm_Receipt();
            }
            else//无权限
            {
                //提示消息权限不足
                pub.Msg("error", "错误信息", "权限不足", false, "{back}");

            }
        }
    }

    #endregion

    #region 实现确认签收功能

    /// <summary>
    /// 实现确认签收功能
    /// </summary>
    /// <returns></returns>
    private void Achieve_Confirm_Receipt()
    {
        int Orders_ID = tools.CheckInt(Request["Orders_ID"]);
        string strOrders_Delivery_ID = tools.CheckStr(Request["delivery_id"]);
        int Orders_Delivery_ID = tools.CheckInt(strOrders_Delivery_ID.Substring(0, strOrders_Delivery_ID.Length - 1));
        int supplier_id = tools.NullInt(DBHelper.ExecuteScalar("select Orders_SupplierID from Orders where Orders_ID=(select Orders_Delivery_OrdersID from Orders_Delivery where Orders_Delivery_ID=" + Orders_Delivery_ID + ")"));


        OrdersDeliveryInfo ODEntity = GetOrdersDeliveryByID(Orders_Delivery_ID);
        if (ODEntity == null)
        {
            pub.Msg("error", "错误信息", "发货单不存在 ", false, "index.aspx");
        }
        Orders_ID = ODEntity.Orders_Delivery_OrdersID;



        OrdersInfo ordersinfo = MyOrders.GetSupplierOrderInfoByID(Orders_ID, supplier_id);


        if (ordersinfo == null)
        {
            pub.Msg("error", "错误信息", "记录不存在", false, "/member/order_delivery_list.aspx");
        }
        //新加 Orders_Delivery_ReceiveStatus:9时  为签收未确认状态
        if (ordersinfo.Orders_Status == 1 && ordersinfo.Orders_PaymentStatus == 4 && ODEntity.Orders_Delivery_ReceiveStatus == 9)
        {

        }
        else
        {
            pub.Msg("error", "错误信息", "记录不存在", false, "/member/order_delivery_list.aspx");
        }

        ////获取该订单下所有商品ID
        //IList<OrdersGoodsInfo> OrderGoods = MyOrders.GetGoodsListByOrderID(Orders_ID);
        //ArrayList myList = new ArrayList();
        //if (OrderGoods != null)
        //{

        //    foreach (var OrderGood in OrderGoods)
        //    {
        //        myList.Add(OrderGood.Orders_Goods_Product_ID);
        //    }
        //}


        #region 支付
        //获取本单已结算签收金额
        double totalreceiveprice = tools.NullDbl(DBHelper.ExecuteScalar("select sum(Orders_Delivery_Goods_ReceivedAmount*Orders_Delivery_Goods_ProductPrice) as totalprice from Orders_Delivery inner join Orders_Delivery_Goods on Orders_Delivery_Goods_DeliveryID=Orders_Delivery_ID where Orders_Delivery_ReceiveStatus=2 and Orders_Delivery_OrdersID=" + ordersinfo.Orders_ID));
        //获取本发货单签收金额
        double curreceiveprice = tools.NullDbl(DBHelper.ExecuteScalar("select sum(Orders_Delivery_Goods_ReceivedAmount*Orders_Delivery_Goods_ProductPrice) as totalprice from Orders_Delivery inner join Orders_Delivery_Goods on Orders_Delivery_Goods_DeliveryID=Orders_Delivery_ID where Orders_Delivery_ID=" + ODEntity.Orders_Delivery_ID));
        //获取本发货单签收佣金金额
        double CommissionPrice = tools.NullDbl(DBHelper.ExecuteScalar("select sum(Orders_Delivery_Goods_ReceivedAmount*Orders_Delivery_Goods_brokerage*Orders_Delivery_Goods_ProductPrice) as totalprice from Orders_Delivery inner join Orders_Delivery_Goods on Orders_Delivery_Goods_DeliveryID=Orders_Delivery_ID where Orders_Delivery_ID=" + ODEntity.Orders_Delivery_ID));
        CommissionPrice = Math.Round(CommissionPrice, 2);
        //结算金额超过采购总金额
        if (totalreceiveprice + curreceiveprice > ordersinfo.Orders_Total_AllPrice)
        {
            int Supplier_ID1 = 0;
            string supplier_name = "";
            ZhongXin mycredit = new ZhongXin();
            //MemberInfo memberinfo1 = GetMemberByID(ordersinfo.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
            MemberInfo memberinfo1 = GetMemberByID(ordersinfo.Orders_BuyerID);
            if (memberinfo1 != null)
            {
                Supplier_ID1 = memberinfo1.Member_SupplierID;

            }
            if (Supplier_ID1 > 0)
            {
                SupplierInfo supplierinfo1 = MySupplier.GetSupplierByID(Supplier_ID1, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                if (supplierinfo1 != null)
                {
                    supplier_name = supplierinfo1.Supplier_CompanyName;
                }


                ZhongXinInfo accountinfo = mycredit.GetZhongXinBySuppleir(Supplier_ID1);
                if (accountinfo != null)
                {
                    decimal accountremain = 0;
                    //余额账户资金
                    accountremain = mycredit.GetAmount(accountinfo.SubAccount);

                    if (accountremain < ((decimal)curreceiveprice - ((decimal)ordersinfo.Orders_Total_AllPrice - (decimal)totalreceiveprice)))
                    {
                        pub.Msg("error", "错误信息", "您的账户余额不足，请联系买方充值入金！", false, "{back}");
                    }

                    double accountprice = curreceiveprice - (ordersinfo.Orders_Total_AllPrice - totalreceiveprice);

                    string strResult = mycredit.ContractDeliveryAccept(Supplier_ID1, ordersinfo.Orders_SupplierID, curreceiveprice - accountprice, accountprice, CommissionPrice, "订单编号：" + ordersinfo.Orders_SN, ODEntity.Orders_Delivery_DocNo, ordersinfo.Orders_SN);

                    if (strResult == "true")
                    {

                        new Orders().Orders_Log(ordersinfo.Orders_ID, tools.NullStr(Session["supplier_email"]), "发货单签收结算", "成功", "发货单[" + ODEntity.Orders_Delivery_DocNo + "]签收结算:" + pub.FormatCurrency(curreceiveprice));

                        #region 注释掉代码
                        ////添加短信提醒2015-5-14
                        //MemberInfo memberinfo = MyMEM.GetMemberByID(orders.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                        //if (memberinfo != null)
                        //{

                        //    pub.DuanXin(memberinfo.Member_LoginMobile, "您通过" + payway.Pay_Way_Name + "向供应商支付了" + pub.FormatCurrency(Orders_Total_AllPrice) + "元，订单号：" + orders.Orders_SN + "，请确认是您亲自操作，有问题请致电400-8108-802【易耐网】");//买方短信提醒(编号09)
                        //}
                        #endregion
                        ODEntity.Orders_Delivery_ReceiveStatus = 2;
                        ODEntity.Orders_Delivery_Amount = curreceiveprice;//将签收金额更改
                        if (MyDelivery.EditOrdersDelivery(ODEntity, pub.CreateUserPrivilege("")))
                        {
                            #region 注释掉代码


                            //foreach (var item in myList)
                            //{
                            //    QueryInfo Query = new QueryInfo();
                            //    Query.CurrentPage = 0;
                            //    Query.PageSize = 0;
                            //    Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersDeliveryGoodsInfo.Orders_Delivery_Goods_ProductID", "in", "((   select  odg.Orders_Delivery_Goods_ProductID from Orders o right join Orders_Delivery od on o.Orders_ID=od.Orders_Delivery_OrdersID join Orders_Delivery_Goods odg on od.Orders_Delivery_ID =odg.Orders_Delivery_Goods_DeliveryID  join Orders_Goods og on o.Orders_ID=og.Orders_Goods_OrdersID  where  Orders_Delivery_Goods_ProductID in  (select Orders_Goods_Product_ID from Orders_Goods where Orders_Goods_OrdersID=208 )  and Orders_ID=208 and Orders_Delivery_Goods_ProductID=809  ))"));
                            //    IList<OrdersDeliveryGoodsInfo> OrdersDeliveryGoodsInfos = MyDelivery.GetOrdersDeliveryGoods(ODEntity.get);
                            //}

                            #endregion
                            Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["supplier_email"]), "发货单签收结算", "成功", "发货单" + ODEntity.Orders_Delivery_DocNo + "签收结算！");

                        }
                        else
                        {
                            throw new Exception("用户权限不足");
                        }
                        pub.Msg("positive", "操作成功", "操作成功", true, "/member/order_delivery_list.aspx");

                    }
                    else
                    {
                        pub.Msg("error", "错误信息", strResult, false, "{back}");
                    }
                }
                else
                {
                    pub.Msg("error", "错误信息", "商家未开通中信账户！", false, "{back}");
                }

            }
            else
            {
                pub.Msg("error", "错误信息", "支付失败，请稍后重试！", false, "{back}");
            }
        }
        else //结算金额未超过总金额（正常流程）
        {
            int Supplier_ID1 = 0;
            string supplier_name = "";
            ZhongXin mycredit = new ZhongXin();
            MemberInfo memberinfo1 = GetMemberByID(ordersinfo.Orders_BuyerID);
            if (memberinfo1 != null)
            {
                Supplier_ID1 = memberinfo1.Member_SupplierID;

            }
            if (Supplier_ID1 > 0)
            {
                SupplierInfo supplierinfo1 = MySupplier.GetSupplierByID(Supplier_ID1, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                if (supplierinfo1 != null)
                {
                    supplier_name = supplierinfo1.Supplier_CompanyName;
                }

                ZhongXinInfo accountinfo = mycredit.GetZhongXinBySuppleir(Supplier_ID1);
                if (accountinfo != null)
                {
                    string strResult = mycredit.ContractDeliveryAccept(Supplier_ID1, ordersinfo.Orders_SupplierID, curreceiveprice, 0, CommissionPrice, "订单编号：" + ordersinfo.Orders_SN, ODEntity.Orders_Delivery_DocNo, ordersinfo.Orders_SN);

                    if (strResult == "true")
                    {



                        new Orders().Orders_Log(ordersinfo.Orders_ID, tools.NullStr(Session["supplier_email"]), "发货单签收结算", "成功", "发货单[" + ODEntity.Orders_Delivery_DocNo + "]签收结算:" + pub.FormatCurrency(curreceiveprice));
                        #region 注释掉代码
                        ////添加短信提醒2015-5-14
                        //MemberInfo memberinfo = MyMEM.GetMemberByID(orders.Orders_BuyerID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                        //if (memberinfo != null)
                        //{

                        //    pub.DuanXin(memberinfo.Member_LoginMobile, "您通过" + payway.Pay_Way_Name + "向供应商支付了" + pub.FormatCurrency(Orders_Total_AllPrice) + "元，订单号：" + orders.Orders_SN + "，请确认是您亲自操作，有问题请致电400-8108-802【易耐网】");//买方短信提醒(编号09)
                        //}
                        #endregion
                        ODEntity.Orders_Delivery_ReceiveStatus = 2;
                        ODEntity.Orders_Delivery_Amount = curreceiveprice;//将签收金额更改
                        if (MyDelivery.EditOrdersDelivery(ODEntity, pub.CreateUserPrivilege("")))
                        {
                            Myorder.Orders_Log(Orders_ID, tools.NullStr(Session["supplier_email"]), "发货单签收结算", "成功", "发货单" + ODEntity.Orders_Delivery_DocNo + "签收结算！");

                        }
                        pub.Msg("positive", "操作成功", "操作成功", true, "/member/order_delivery_list.aspx");
                        //Response.Write("success");
                        //return "success";

                    }
                    else
                    {
                        //new ZhongXin().SaveZhongXinAccountLog(ordersinfo.Orders_BuyerID, curreceiveprice * -1, "[撤销][签收结算]订单编号：" + ordersinfo.Orders_SN);

                        pub.Msg("error", "错误信息" + "curreceiveprice" + curreceiveprice, strResult, false, "{back}");
                    }
                }
                else
                {
                    pub.Msg("error", "错误信息", "您尚未开通中信账户！", false, "{back}");
                }

            }
            else
            {
                pub.Msg("error", "错误信息", "支付失败，请稍后重试！", false, "{back}");
            }

        }

        #endregion
        pub.Msg("positive", "操作成功", "操作成功", true, "/member/order_delivery_list.aspx");
        // Response.Write("success");                        
        //return "success";

        Response.Redirect("/member/index.aspx");
    }

    #endregion

    #endregion

    #region 信贷管理

    /// <summary>
    /// 根据订单号获取供应商名称
    /// </summary>
    /// <param name="orders_sn"></param>
    /// <returns></returns>
    public string GetSupplierByOrderSN(string orders_sn)
    {
        string supplierName = "";

        OrdersInfo entity = MyOrders.GetOrdersBySN(orders_sn);
        if (entity != null)
        {
            SupplierInfo supplierInfo = MySupplier.GetSupplierByID(entity.Orders_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
            if (supplierInfo != null)
            {
                if (supplierInfo.Supplier_Type == 1)
                {
                    supplierName = supplierInfo.Supplier_Contactman;
                }
                else
                {
                    supplierName = supplierInfo.Supplier_CompanyName;
                }
            }
            else
            {
                supplierName = "--";
            }
        }
        else
        {
            supplierName = "--";
        }
        return supplierName;
    }

    /// <summary>
    /// 信贷账户信息
    /// </summary>
    public void GetMemberLoanAccount()
    {
        string member_id = "M" + tools.NullStr(Session["member_id"]);

        StringBuilder returnValue = new StringBuilder();

        double balance_credit = 0;//可用余额

        returnValue.Append("[{name:'信贷账户',type:'pie',radius : '55%',center: ['50%', '60%'],data:[");

        QueryMemberLoanJsonInfo JsonInfo = credit.Query_Member_Loan(member_id);

        if (JsonInfo != null)
        {
            if (JsonInfo.Is_success == "T")
            {
                returnValue.Append("{value:" + JsonInfo.Used_credit + ", name:'已使用额度'},");
                returnValue.Append("{value:" + JsonInfo.Apply_credit + ", name:'申请中额度'},");
                returnValue.Append("{value:" + JsonInfo.Available_credit + ", name:'可用额度'}");
            }
        }
        returnValue.Append("]}]");

        var newObj = new
        {
            series = returnValue.ToString()
        };
        Response.Write(JsonHelper.ObjectToJSON(newObj));
        Response.End();
    }

    /// <summary>
    /// 信贷信息搜索
    /// </summary>
    /// <returns></returns>
    public string Member_Loan_Project_Search()
    {
        StringBuilder str = new StringBuilder();

        string loanStatus = tools.CheckStr(Request["loanStatus"]);
        string keyword = tools.CheckStr(Request["keyword"]);

        str.Append("<div class=\"blk04\">");

        str.Append("<select name=\"loanStatus\" id=\"loanStatus\" onchange=\"changeLoanStatus();\">");
        str.Append("<option value=\"NORMAL\" " + (loanStatus == "NORMAL" ? "selected" : "") + ">全部申请</option>");
        str.Append("<option value=\"CHECK_WAIT\" " + (loanStatus == "CHECK_WAIT" ? "selected" : "") + ">审核中</option>");
        str.Append("<option value=\"PUSH_WAIT\" " + (loanStatus == "PUSH_WAIT" ? "selected" : "") + ">已审核待推进</option>");
        str.Append("<option value=\"ISSUE_WAIT\" " + (loanStatus == "ISSUE_WAIT" ? "selected" : "") + ">放款中</option>");
        str.Append("<option value=\"REPAY_NORMAL\" " + (loanStatus == "REPAY_NORMAL" ? "selected" : "") + ">正常</option>");
        str.Append("<option value=\"OVERDUE\" " + (loanStatus == "OVERDUE" ? "selected" : "") + ">逾期</option>");
        str.Append("<option value=\"CHECK_FAIL\" " + (loanStatus == "CHECK_FAIL" ? "selected" : "") + ">已拒绝</option>");
        str.Append("<option value=\"FINISH_GIVEUP\" " + (loanStatus == "FINISH_GIVEUP" ? "selected" : "") + ">已撤销</option>");
        str.Append("<option value=\"FINISH_NORMAL\" " + (loanStatus == "FINISH_NORMAL" ? "selected" : "") + ">已完成</option>");
        str.Append("<option value=\"BAD\" " + (loanStatus == "BAD" ? "selected" : "") + ">坏账</option>");
        str.Append("</select>");

        //str.Append("<select name=\"\">");
        //str.Append("<option value=\"近期申请\">近期申请</option>");
        //str.Append("</select>");

        //str.Append("<input name=\"keyword\" id=\"keyword\" type=\"text\"");
        //if (keyword != "")
        //{
        //    str.Append(" value=\"" + keyword + "\" ");
        //}
        //else
        //{
        //    str.Append(" value=\"订单号/申请单号\" ");
        //}

        //str.Append(" onfocus=\"if(this.value==this.defaultValue) this.value=''\" onblur=\"if (this.value=='') this.value=this.defaultValue\" /><a href=\"javascript:void(0);\" onclick=\"LoanProjectSearch('keyword')\">搜 索</a>");
        str.Append("</div>");

        return str.ToString();
    }

    /// <summary>
    /// 信贷信息列表
    /// </summary>
    public void Member_Loan_Project(int showNum)
    {
        StringBuilder strHTML = new StringBuilder();
        PageInfo pageInfo = null;
        string loan_proj_no = "";
        string loanStatus = tools.CheckStr(Request["loanStatus"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        int current_page = tools.CheckInt(Request["page"]);
        if (current_page < 1)
        {
            current_page = 1;
        }
        int page_size = showNum;
        int i = 0;
        string page_url = "?loanStatus=" + loanStatus;

        QueryLoanProjectJsonInfo JsonInfo = credit.QueryLoanProject("M" + tools.NullStr(Session["member_id"]), loan_proj_no, loanStatus, current_page, page_size);

        strHTML.Append("<table width=\"973\" border=\"0\" cellspacing=\"2\" cellpadding=\"0\">");
        strHTML.Append("<tr>");
        strHTML.Append("<td class=\"name\">申请单号</td>");
        strHTML.Append("<td class=\"name\">订单号</td>");
        strHTML.Append("<td class=\"name\">供应商</td>");
        strHTML.Append("<td class=\"name\">申请时间</td>");
        strHTML.Append("<td class=\"name\">申请金额</td>");
        strHTML.Append("<td class=\"name\">保证金</td>");
        strHTML.Append("<td class=\"name\">申请状态</td>");
        strHTML.Append("<td class=\"name\">操作</td>");
        strHTML.Append("</tr>");
        if (JsonInfo != null && JsonInfo.Is_success == "T")
        {
            pageInfo = pub.GetPageInfo(page_size, current_page, JsonInfo.All_count);

            IList<LoanlistInfo> loanlist = JsonInfo.Loan_list;
            if (loanlist != null)
            {
                foreach (LoanlistInfo loaninfo in loanlist)
                {
                    i++;
                    if (i % 2 == 0)
                    {
                        strHTML.Append("<tr class=\"bg\">");
                    }
                    else
                    {
                        strHTML.Append("<tr>");
                    }
                    strHTML.Append("<td>" + loaninfo.Loan_proj_no + "</td>");
                    strHTML.Append("<td>" + loaninfo.Outer_order_no + "</td>");
                    strHTML.Append("<td>" + GetSupplierByOrderSN(loaninfo.Outer_order_no) + "</td>");
                    strHTML.Append("<td>" + DateTime.ParseExact(loaninfo.Submit_date, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd") + "</td>");
                    strHTML.Append("<td>" + pub.FormatCurrency(loaninfo.Principal_amount) + "</td>");
                    strHTML.Append("<td>" + pub.FormatCurrency(loaninfo.Margin_amount) + "</td>");
                    strHTML.Append("<td>" + Myorder.LoanProjectStatus(loaninfo.Loan_status) + "</td>");
                    strHTML.Append("<td style=\"text-align:left;padding-left:10px;\">");
                    if (loaninfo.Loan_status == "PUSH_WAIT" || loaninfo.Loan_status == "ISSUE_WAIT" || loaninfo.Loan_status == "REPAY_NORMAL" || loaninfo.Loan_status == "FINISH_NORMAL")
                    {
                        strHTML.Append("<a href=\"/member/loan_project_detail.aspx?loan_proj_no=" + loaninfo.Loan_proj_no + "\" class=\"a05\"  target=\"_blank\">查看</a>");
                    }

                    if (loaninfo.Loan_status == "CHECK_WAIT")
                    {
                        strHTML.Append("<a href=\"/member/loan_cancel.aspx?loan_proj_no=" + loaninfo.Loan_proj_no + "&orders_sn=" + loaninfo.Outer_order_no + "\">取消申请</a></td>");
                    }
                    else if (loaninfo.Loan_status == "PUSH_WAIT")
                    {
                        strHTML.Append("<a href=\"/member/credit_do.aspx?action=loan_push&Orders_SN=" + loaninfo.Outer_order_no + "\" target=\"_blank\">推进</a></td>");
                    }
                    else if (loaninfo.Loan_status == "REPAY_NORMAL" || loaninfo.Loan_status == "")
                    {
                        strHTML.Append("<a href=\"/member/credit_do.aspx?action=loan_repayment&loan_proj_no=" + loaninfo.Loan_proj_no + "&repay_amount=" + Math.Round(loaninfo.Repay_amount, 2) + "\" target=\"_blank\">还款</a></td>");
                    }
                    strHTML.Append("</tr>");
                }
            }
            else
            {
                strHTML.Append("<tr>");
                strHTML.Append("<td colspan=\"8\" style=\"text-align:center;\">暂无贷款申请！</td>");
                strHTML.Append("</tr>");
            }
            Response.Write(strHTML.ToString());
            Response.Write("</table>");
            pub.Page(pageInfo.PageCount, pageInfo.CurrentPage, page_url, pageInfo.PageSize, pageInfo.RecordCount);
        }
        else
        {
            strHTML.Append("<tr>");
            strHTML.Append("<td colspan=\"8\" style=\"text-align:center;\">暂无贷款申请！</td>");
            strHTML.Append("</tr>");
            Response.Write(strHTML.ToString());
            Response.Write("</table>");
        }
    }

    public void Member_Loan_Project_CreditDetail(int showNum)
    {
        StringBuilder strHTML = new StringBuilder();
        string keyword = tools.CheckStr(Request["keyword"]);
        string page_url = "?action=list&keword=" + keyword;
        QueryInfo Query = new QueryInfo();
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersPaymentInfo.Orders_Payment_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "OrdersPaymentInfo.Orders_Payment_MemberID", "=", tools.NullStr(Session["member_id"])));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersPaymentInfo.Orders_Payment_OrdersID", "in", "select Orders_ID from Orders where Orders_SN like '%" + keyword + "%'"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "OrdersPaymentInfo.Orders_Payment_Name", "=", "信贷支付"));
        Query.OrderInfos.Add(new OrderInfo("OrdersPaymentInfo.Orders_Payment_ID", "desc"));
        IList<OrdersPaymentInfo> entitys = MyPayment.GetOrdersPayments(Query);
        PageInfo pageInfo = MyPayment.GetPageInfo(Query);

        strHTML.Append("<table width=\"973\" border=\"0\" cellspacing=\"2\" cellpadding=\"0\">");
        strHTML.Append("<tr>");
        strHTML.Append("<td width=\"85\" class=\"name\">订单号</td>");
        strHTML.Append("<td width=\"165\" class=\"name\">供应商</td>");
        strHTML.Append("<td width=\"110\" class=\"name\">订单金额</td>");
        strHTML.Append("<td width=\"158\" class=\"name\">信贷付款金额</td>");
        strHTML.Append("<td width=\"148\" class=\"name\">信贷付款时间</td>");
        strHTML.Append("<td width=\"156\" class=\"name\">信贷还款日期</td>");
        strHTML.Append("<td width=\"135\" class=\"name\">操作</td>");
        strHTML.Append("</tr>");

        int i = 0;
        OrdersInfo ordersInfo = null;
        SupplierInfo supplierInfo = null;

        double Orders_AllTotalPrice = 0;
        string Orders_SN = "";
        string supplier_name = "";

        if (entitys != null)
        {
            foreach (OrdersPaymentInfo entity in entitys)
            {
                i++;

                ordersInfo = Myorder.GetOrdersByID(entity.Orders_Payment_OrdersID);
                if (ordersInfo != null)
                {
                    Orders_SN = ordersInfo.Orders_SN;
                    Orders_AllTotalPrice = ordersInfo.Orders_Total_AllPrice;
                    supplierInfo = MySupplier.GetSupplierByID(ordersInfo.Orders_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
                    if (supplierInfo != null)
                    {
                        supplier_name = supplierInfo.Supplier_CompanyName;
                    }
                }

                if (i % 2 == 0)
                {
                    strHTML.Append("<tr class=\"bg\">");
                }
                else
                {
                    strHTML.Append("<tr>");
                }
                strHTML.Append("<td>" + Orders_SN + "</td>");
                strHTML.Append("<td>" + supplier_name + "</td>");
                strHTML.Append("<td>" + pub.FormatCurrency(Orders_AllTotalPrice) + "</td>");
                strHTML.Append("<td>" + pub.FormatCurrency(entity.Orders_Payment_Amount) + "</td>");
                strHTML.Append("<td>" + entity.Orders_Payment_Addtime.ToString("yyyy-MM-dd") + "</td>");
                strHTML.Append("<td>" + DateTime.Now.ToShortDateString() + "</td>");
                strHTML.Append("<td style=\"text-align:left;padding-left:10px;\">");
                strHTML.Append("<a href=\"javascript:;\" class=\"a05\">查看</a>");

                strHTML.Append("<a href=\"javascript:;\" target=\"_blank\">还款</a></td>");

                strHTML.Append("</tr>");
            }
            Response.Write(strHTML.ToString());
            Response.Write("</table>");
            pub.Page(pageInfo.PageCount, pageInfo.CurrentPage, page_url, pageInfo.PageSize, pageInfo.RecordCount);
        }
        else
        {
            strHTML.Append("<tr>");
            strHTML.Append("<td colspan=\"7\" style=\"text-align:center;\">暂无信息！</td>");
            strHTML.Append("</tr>");
            strHTML.Append("</table>");
            Response.Write(strHTML.ToString());
        }
    }

    public string Member_Loan_Project_CreditDetail_Search()
    {
        StringBuilder str = new StringBuilder();
        string keyword = tools.CheckStr(Request["keyword"]);

        str.Append("<div class=\"blk04\">");

        str.Append("<input name=\"keyword\" id=\"keyword\" type=\"text\"");
        if (keyword != "")
        {
            str.Append(" value=\"" + keyword + "\" ");
        }
        else
        {
            str.Append(" value=\"订单号\" ");
        }

        str.Append(" onfocus=\"if(this.value==this.defaultValue) this.value=''\" onblur=\"if (this.value=='') this.value=this.defaultValue\" /><a href=\"javascript:void(0);\" onclick=\"LoanProjectSearch('keyword')\">搜 索</a>");
        str.Append("</div>");

        return str.ToString();
    }

    public QueryProjectDetailJsonInfo GetLoanProjectDetailByProjectNo(string loan_proj_no)
    {
        string member_id = "M" + tools.NullStr(Session["member_id"]);
        return credit.QueryProjectDetail(member_id, loan_proj_no);
    }

    /// <summary>
    /// 贷款分期详情列表（期数列表）
    /// </summary>
    /// <param name="JsonInfo"></param>
    public void Member_LoanProjectDetail_List(QueryProjectDetailJsonInfo JsonInfo)
    {
        StringBuilder strHTML = new StringBuilder();

        int i = 0;

        strHTML.Append("<table width=\"973\" border=\"0\" cellspacing=\"2\" cellpadding=\"0\">");
        strHTML.Append("<tr>");
        strHTML.Append("<td class=\"name\">期号</td>");
        strHTML.Append("<td class=\"name\">本金</td>");
        strHTML.Append("<td class=\"name\">利息</td>");
        strHTML.Append("<td class=\"name\">手续费</td>");
        strHTML.Append("<td class=\"name\">罚息</td>");
        strHTML.Append("<td class=\"name\">待还本金</td>");
        strHTML.Append("<td class=\"name\">待还利息</td>");
        strHTML.Append("<td class=\"name\">待还手续费</td>");
        strHTML.Append("<td class=\"name\">待还罚息</td>");
        strHTML.Append("<td class=\"name\">还款时间</td>");
        strHTML.Append("<td class=\"name\">分期状态</td>");
        //strHTML.Append("<td class=\"name\">操作</td>");
        strHTML.Append("</tr>");

        if (JsonInfo != null && JsonInfo.Is_success == "T")
        {
            if (JsonInfo.Installment_list != null)
            {
                foreach (QueryProjectDetailTremInfo tremInfo in JsonInfo.Installment_list)
                {
                    i++;
                    if (i % 2 == 0)
                    {
                        strHTML.Append("<tr class=\"bg\">");
                    }
                    else
                    {
                        strHTML.Append("<tr>");
                    }
                    strHTML.Append("<td>" + tremInfo.Term + "</td>");
                    strHTML.Append("<td>" + pub.FormatCurrency(tremInfo.Principal) + "</td>");
                    strHTML.Append("<td>" + pub.FormatCurrency(tremInfo.Interest) + "</td>");
                    strHTML.Append("<td>" + pub.FormatCurrency(tremInfo.Fee) + "</td>");
                    strHTML.Append("<td>" + pub.FormatCurrency(tremInfo.Penalty) + "</td>");
                    strHTML.Append("<td>" + pub.FormatCurrency(tremInfo.Unpaid_principal) + "</td>");
                    strHTML.Append("<td>" + pub.FormatCurrency(tremInfo.Unpaid_interest) + "</td>");
                    strHTML.Append("<td>" + pub.FormatCurrency(tremInfo.Unpaid_fee) + "</td>");
                    strHTML.Append("<td>" + pub.FormatCurrency(tremInfo.Unpaid_penalty) + "</td>");
                    strHTML.Append("<td>" + DateTime.ParseExact(tremInfo.Repay_date, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd") + "</td>");
                    strHTML.Append("<td>" + Myorder.LoanProjectDetailStatus(tremInfo.Status) + "</td>");
                    //strHTML.Append("<td style=\"text-align:left;padding-left:10px;\">");

                    //strHTML.Append("</td>");
                    strHTML.Append("</tr>");
                }
            }
            else
            {
                strHTML.Append("<tr>");
                strHTML.Append("<td colspan=\"12\" style=\"text-align:center;\">暂无贷款申请！</td>");
                strHTML.Append("</tr>");
            }
        }
        else
        {
            strHTML.Append("<tr>");
            strHTML.Append("<td colspan=\"12\" style=\"text-align:center;\">暂无贷款申请！</td>");
            strHTML.Append("</tr>");
        }
        strHTML.Append("</table>");
        Response.Write(strHTML.ToString());
    }

    /// <summary>
    /// 信贷申请
    /// </summary>
    public void Member_Loan_Apply(string orders_sn)
    {
        string member_id = "M" + tools.NullStr(Session["member_id"]);
        double apply_amount = 0;

        LoanApplyJsonInfo JsonInfo = new LoanApplyJsonInfo();

        OrdersInfo entity = MyOrders.GetOrdersBySN(orders_sn);

        if (entity != null)
        {
            JsonInfo = credit.Loan_Apply(member_id, entity);
            if (JsonInfo != null && JsonInfo.Is_success == "T")
            {
                apply_amount = entity.Orders_ApplyCreditAmount;

                entity.Orders_Loan_proj_no = JsonInfo.Loan_proj_no;
                MyOrders.EditOrders(entity);

                //订单日志
                Myorder.Orders_Log(entity.Orders_ID, "系统", "信贷申请", "成功", "贷款申请成功，申请贷款金额：" + pub.FormatCurrency(apply_amount));
            }
            else
            {
                //订单日志
                Myorder.Orders_Log(entity.Orders_ID, "系统", "信贷申请", "失败", "贷款申请失败：" + JsonInfo.Error_code + "：" + JsonInfo.Error_message + "(" + JsonInfo.Memo + ")");
            }
        }
        else
        {
            //订单日志
            Myorder.Orders_Log(entity.Orders_ID, "系统", "信贷申请", "失败", "系统错误");
        }
    }

    /// <summary>
    /// Ajax信贷申请
    /// </summary>
    /// <param name="orders_sn"></param>
    public void Member_Loan_Apply_Ajax(string orders_sn)
    {
        string member_id = "M" + tools.NullStr(Session["member_id"]);
        double apply_amount = 0;

        LoanApplyJsonInfo JsonInfo = new LoanApplyJsonInfo();

        OrdersInfo entity = MyOrders.GetOrdersBySN(orders_sn);

        if (entity != null)
        {
            JsonInfo = credit.Loan_Apply(member_id, entity);
            if (JsonInfo != null && JsonInfo.Is_success == "T")
            {
                apply_amount = entity.Orders_ApplyCreditAmount;

                entity.Orders_Loan_proj_no = JsonInfo.Loan_proj_no;
                MyOrders.EditOrders(entity);

                //订单日志
                Myorder.Orders_Log(entity.Orders_ID, "系统", "信贷申请", "成功", "贷款申请成功，申请贷款金额：" + pub.FormatCurrency(apply_amount));
                Response.Write(JsonInfo.Is_success);
            }
            else
            {
                //订单日志
                Myorder.Orders_Log(entity.Orders_ID, "系统", "信贷申请", "失败", "贷款申请失败：" + JsonInfo.Error_code + "：" + JsonInfo.Error_message + "(" + JsonInfo.Memo + ")");
                Response.Write(JsonInfo.Error_message);
            }
        }
        else
        {
            //订单日志
            Myorder.Orders_Log(entity.Orders_ID, "系统", "信贷申请", "失败", "系统错误");
            Response.Write("订单不存在");
        }
    }

    /// <summary>
    /// 贷款推进
    /// </summary>
    public void Member_Loan_Push()
    {
        string member_id = "M" + tools.NullStr(Session["member_id"]);
        string Orders_SN = tools.CheckStr(Request["Orders_SN"]);
        string token = tools.NullStr(Session["member_token"]);
        LoanPushJsonInfo JsonInfo = credit.Loan_Push(member_id, Orders_SN);
        if (JsonInfo != null && JsonInfo.Is_success == "T")
        {
            Response.Redirect(JsonInfo.Push_url + "&TPToken=" + tools.NullStr(Session["member_token"]) + "&BSType=1");
        }
        else
        {
            Response.Redirect("/member/loan_project.aspx");
        }
    }

    /// <summary>
    /// 贷款撤销
    /// </summary>
    public void Member_Loan_Cancel()
    {
        string member_id = "M" + tools.NullStr(Session["member_id"]);
        string loan_proj_no = tools.CheckStr(Request["loan_proj_no"]);
        string cause = tools.CheckStr(Request["cause"]);
        string channel = tools.CheckStr(Request["channel"]);
        string orders_sn = tools.CheckStr(Request["orders_sn"]);

        LoanCancelJsonInfo JsonInfo = credit.LoanCancel(member_id, loan_proj_no, cause, channel);
        if (JsonInfo != null)
        {
            if (JsonInfo.Is_success == "T")
            {
                OrdersInfo entity = Myorder.GetOrdersInfoBySN(orders_sn);
                if (entity != null)
                {
                    entity.Orders_Status = 3;
                    MyOrders.EditOrders(entity);
                }
                pub.Msg("positive", "提示信息", "贷款申请取消成功", true, "/member/loan_project.aspx");
            }
            else
            {
                pub.Msg("info", "提示信息", "操作失败，请稍后再试！", false, "{back}");
            }
        }
        else
        {
            pub.Msg("info", "提示信息", "操作失败，请稍后再试！", false, "{back}");
        }
    }

    /// <summary>
    /// 贷款还款
    /// </summary>
    public void Member_Loan_Repayment()
    {
        string req_no = DateTime.Now.ToString("yyyyMMddHHmmss") + pub.Createvkey(6);
        string member_id = "M" + tools.NullStr(Session["member_id"]);
        string loan_proj_no = tools.CheckStr(Request["loan_proj_no"]);
        double repay_amount = tools.CheckFloat(Request["repay_amount"]);

        LoanRepaymentJsonInfo JsonInfo = credit.LoanRepayment(req_no, member_id, loan_proj_no, repay_amount);
        if (JsonInfo != null && JsonInfo.Is_success == "T")
        {
            Response.Redirect(JsonInfo.Repay_url);
        }
        else
        {
            Response.Redirect("/member/loan_project.aspx");
        }
    }

    /// <summary>
    /// 检查贷款申请是否符合条件
    /// </summary>
    /// <returns></returns>
    public bool Check_Member_Loan(OrdersInfo ordersInfo)
    {
        bool is_true = true;
        string member_id = "M" + tools.NullStr(Session["member_id"]);

        QueryMemberLoanJsonInfo JsonInfo = credit.Query_Member_Loan(member_id);
        QueryLoanProductJsonInfo loanProduct = credit.Query_Loan_Product(member_id);

        if (JsonInfo != null && JsonInfo.Is_success == "T" && loanProduct != null && loanProduct.Is_success == "T")
        {
            if (ordersInfo.Orders_Payway == 2)
            {
                if (ordersInfo.Orders_Total_AllPrice <= JsonInfo.Available_credit && loanProduct.Agreement_no == ordersInfo.Orders_AgreementNo)
                {
                    is_true = true;
                }
                else
                {
                    is_true = false;
                }
            }
            else if (ordersInfo.Orders_Payway == 3)
            {
                if (ordersInfo.Orders_ApplyCreditAmount <= JsonInfo.Available_credit && loanProduct.Agreement_no == ordersInfo.Orders_AgreementNo)
                {
                    is_true = true;
                }
                else
                {
                    is_true = false;
                }
            }
        }
        else
        {
            is_true = false;
        }
        return is_true;
    }
    #endregion

    #region 操作员管理

    public void AddSubAccount()
    {
        int account_id = tools.NullInt(Session["member_accountid"]);
        if (account_id > 0)
        {
            Response.Redirect("/member/index.aspx");
        }

        int ID = tools.CheckInt(Request.Form["ID"]);
        int MemberID = tools.NullInt(Session["member_id"]);
        string AccountName = tools.CheckStr(Request.Form["Member_SubAccount_Name"]);
        string Password = tools.CheckStr(Request.Form["Member_SubAccount_Password"]);
        string Password_confirm = tools.CheckStr(Request.Form["Member_SubAccount_Password_confirm"]);
        string Name = tools.CheckStr(Request.Form["Member_SubAccount_TrueName"]);
        string Mobile = tools.CheckStr(Request.Form["Member_SubAccount_Mobile"]);
        string Email = tools.CheckStr(Request.Form["Member_SubAccount_Email"]);
        DateTime Addtime = DateTime.Now;
        DateTime LastLoginTime = DateTime.Now;
        int IsActive = tools.CheckInt(Request.Form["IsActive"]);
        string Privilege = tools.CheckStr(Request.Form["Privilege"]);

        if (AccountName == "")
        {
            pub.Msg("info", "信息提示", "请输入用户名", false, "{back}");
        }
        else
        {
            if (CheckNickname(AccountName))
            {
                Encoding name_length = System.Text.Encoding.GetEncoding("gb2312");
                byte[] bytes = name_length.GetBytes(AccountName);
                if (bytes.Length < 4 || bytes.Length > 20)
                {
                    pub.Msg("info", "信息提示", "用户名长度应在4-20位字符", false, "{back}");
                }
                if (Check_Member_Nickname(AccountName))
                {
                    pub.Msg("info", "信息提示", "该用户名已被使用，请使用其他用户名注册", false, "{back}");
                }
            }
            else
            {
                pub.Msg("info", "信息提示", "用户名含有特殊字符", false, "{back}");
            }
        }

        if (CheckSsn(Password) == false)
        {
            pub.Msg("info", "提示信息", "密码包含特殊字符，只接受A-Z，a-z，0-9，不要输入空格", false, "{back}");
        }
        else
        {
            if (Password.Length < 6 || Password.Length > 20)
            {
                pub.Msg("info", "提示信息", "请输入6～20位新密码", false, "{back}");
            }
        }

        if (Password != Password_confirm)
        {
            pub.Msg("info", "提示信息", "两次密码输入不一致，请重新输入", false, "{back}");
        }

        if (Name == "")
        {
            pub.Msg("info", "提示信息", "请填写子账户姓名", false, "{back}");
        }

        if (Mobile != "")
        {
            if (!pub.Checkmobile(Mobile))
            {
                pub.Msg("info", "提示信息", "手机格式不正确，请重新输入", false, "{back}");
            }
        }

        if (Email != "")
        {
            if (!tools.CheckEmail(Email))
            {
                pub.Msg("info", "提示信息", "邮箱格式不正确，请重新输入", false, "{back}");
            }
        }

        Password = encrypt.MD5(Password);

        MemberSubAccountInfo entity = new MemberSubAccountInfo();
        entity.ID = ID;
        entity.MemberID = MemberID;
        entity.AccountName = AccountName;
        entity.Password = Password;
        entity.Name = Name;
        entity.Mobile = Mobile;
        entity.Email = Email;
        entity.Addtime = Addtime;
        entity.LastLoginTime = LastLoginTime;
        entity.IsActive = 1;
        entity.Privilege = Privilege;

        if (MySubAccount.AddMemberSubAccount(entity))
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "subAccount_add.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditSubAccount()
    {
        int account_id = tools.NullInt(Session["member_accountid"]);
        if (account_id > 0)
        {
            Response.Redirect("/member/index.aspx");
        }

        int ID = tools.CheckInt(Request.Form["ID"]);
        int MemberID = tools.NullInt(Session["member_id"]);
        string AccountName = tools.CheckStr(Request.Form["Member_SubAccount_Name"]);
        string Password = tools.CheckStr(Request.Form["Member_SubAccount_Password"]);
        string Password_confirm = tools.CheckStr(Request.Form["Member_SubAccount_Password_confirm"]);
        string Name = tools.CheckStr(Request.Form["Member_SubAccount_TrueName"]);
        string Mobile = tools.CheckStr(Request.Form["Member_SubAccount_Mobile"]);
        string Email = tools.CheckStr(Request.Form["Member_SubAccount_Email"]);
        DateTime Addtime = DateTime.Now;
        DateTime LastLoginTime = DateTime.Now;
        int IsActive = tools.CheckInt(Request.Form["IsActive"]);
        string Privilege = tools.CheckStr(Request.Form["Privilege"]);

        int member_id = tools.NullInt(Session["member_id"]);

        bool editPwd = true;

        if (AccountName == "")
        {
            pub.Msg("info", "信息提示", "请输入用户名", false, "{back}");
        }
        else
        {
            if (CheckNickname(AccountName))
            {
                Encoding name_length = System.Text.Encoding.GetEncoding("gb2312");
                byte[] bytes = name_length.GetBytes(AccountName);
                if (bytes.Length < 4 || bytes.Length > 20)
                {
                    pub.Msg("info", "信息提示", "用户名长度应在4-20位字符", false, "{back}");
                }
                if (Check_Member_Nickname(AccountName))
                {
                    pub.Msg("info", "信息提示", "该用户名已被使用，请使用其他用户名注册", false, "{back}");
                }
            }
            else
            {
                pub.Msg("info", "信息提示", "用户名含有特殊字符", false, "{back}");
            }
        }

        if (Password == "" && Password_confirm == "")
        {
            editPwd = false;
        }
        else
        {
            if (CheckSsn(Password) == false)
            {
                pub.Msg("info", "提示信息", "密码包含特殊字符，只接受A-Z，a-z，0-9，不要输入空格", false, "{back}");
            }
            else
            {
                if (Password.Length < 6 || Password.Length > 20)
                {
                    pub.Msg("info", "提示信息", "请输入6～20位新密码", false, "{back}");
                }
            }

            if (Password != Password_confirm)
            {
                pub.Msg("info", "提示信息", "两次密码输入不一致，请重新输入", false, "{back}");
            }
        }


        if (Name == "")
        {
            pub.Msg("info", "提示信息", "请填写子账户姓名", false, "{back}");
        }

        if (Mobile != "")
        {
            if (!pub.Checkmobile(Mobile))
            {
                pub.Msg("info", "提示信息", "手机格式不正确，请重新输入", false, "{back}");
            }
        }

        if (Email != "")
        {
            if (!tools.CheckEmail(Email))
            {
                pub.Msg("info", "提示信息", "邮箱格式不正确，请重新输入", false, "{back}");
            }
        }

        Password = encrypt.MD5(Password);

        MemberSubAccountInfo entity = GetSubAccountByID(ID);
        if (entity != null)
        {
            if (entity.MemberID == member_id)
            {
                entity.MemberID = MemberID;
                entity.AccountName = AccountName;
                if (editPwd)
                {
                    entity.Password = Password;
                }
                entity.Name = Name;
                entity.Mobile = Mobile;
                entity.Email = Email;
                entity.LastLoginTime = LastLoginTime;
                entity.IsActive = 1;
                entity.Privilege = Privilege;

                if (MySubAccount.EditMemberSubAccount(entity))
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "subAccount_list.aspx");
                }
                else
                {
                    pub.Msg("error", "错误信息", "操作员修改失败，请稍后重试！", false, "{back}");
                }
            }
            else
            {
                pub.Msg("error", "错误信息", "操作员修改失败，请稍后重试！", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "操作员信息加载失败，请稍后再试！", false, "{back}");
        }
    }

    public void DelSubAccount()
    {
        int member_id = tools.NullInt(Session["member_id"]);
        int id = tools.CheckInt(Request["id"]);
        int account_id = tools.NullInt(Session["member_accountid"]);
        if (account_id > 0)
        {
            Response.Redirect("/member/index.aspx");
        }
        if (member_id > 0 && id > 0)
        {
            MemberSubAccountInfo subinfo = MySubAccount.GetMemberSubAccountByID(id);
            if (subinfo != null)
            {
                if (subinfo.MemberID == member_id)
                {
                    MySubAccount.DelMemberSubAccount(id);
                    Response.Redirect("/member/subAccount_list.aspx");
                }
                else
                {
                    pub.Msg("error", "错误信息", "操作员删除失败，请稍后再试！", false, "{back}");
                }
            }
            else
            {
                pub.Msg("error", "错误信息", "操作员删除失败，请稍后再试！", false, "{back}");
            }

        }
        else
        {
            pub.Msg("error", "错误信息", "操作员删除失败，请稍后再试！", false, "{back}");
        }
    }

    public void SubAccount_list(string keyword)
    {
        int member_id = tools.NullInt(Session["member_id"]);
        string Pageurl = "?action=list";
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        string BgColor = "";

        if (curpage < 1)
        {
            curpage = 1;
        }

        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" >");
        strHTML.Append("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        strHTML.Append("  <th width=\"20%\" align=\"center\" valign=\"middle\">用户名</th>");
        strHTML.Append("  <th width=\"20%\" align=\"center\" valign=\"middle\">姓名</th>");
        strHTML.Append("  <th width=\"20%\" align=\"center\" valign=\"middle\">邮箱</th>");
        strHTML.Append("  <th width=\"20%\" align=\"center\" valign=\"middle\">手机</th>");
        strHTML.Append("  <th width=\"10%\" align=\"center\" valign=\"middle\">状态</th>");
        strHTML.Append("  <th width=\"10%\" align=\"center\" valign=\"middle\">操作</th>");
        strHTML.Append("</tr>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberSubAccountInfo.MemberID", "=", member_id.ToString()));

        if (keyword != "")
        {
            Pageurl += "&subaccountkeyword=" + keyword;
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "MemberSubAccountInfo.AccountName", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "MemberSubAccountInfo.Name", "like", keyword));
        }

        Query.OrderInfos.Add(new OrderInfo("MemberSubAccountInfo.ID", "Desc"));
        IList<MemberSubAccountInfo> entitys = MySubAccount.GetMemberSubAccounts(Query);
        PageInfo page = MySubAccount.GetPageInfo(Query);
        if (entitys != null)
        {
            foreach (MemberSubAccountInfo entity in entitys)
            {
                if (BgColor == "#FFFFFF")
                {
                    BgColor = "#FFFFFF";
                }
                else
                {
                    BgColor = "#FFFFFF";
                }

                strHTML.Append("<tr bgcolor=\"" + BgColor + "\" height=\"35\">");
                strHTML.Append("<td align=\"center\" valign=\"middle\">" + entity.AccountName + "</td>");

                strHTML.Append("<td align=\"center\" valign=\"middle\">" + entity.Name + "</td>");
                strHTML.Append("<td align=\"center\" valign=\"middle\">" + entity.Email + "</td>");
                strHTML.Append("<td align=\"center\" valign=\"middle\">" + entity.Mobile + "</td>");
                strHTML.Append("<td align=\"center\" valign=\"middle\">" + (entity.IsActive == 1 ? "启用" : "禁用") + "</td>");

                strHTML.Append("<td height=\"35\" align=\"center\" valign=\"middle\"><a href=\"/member/subAccount_edit.aspx?id=" + entity.ID + "\">修改</a> <a href=\"account_do.aspx?action=subdel&id=" + entity.ID + "\">删除</a></td>");
                strHTML.Append("</tr>");
            }
            strHTML.Append("</table>");

            Response.Write(strHTML.ToString());

            Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            Response.Write("<tr><td height=\"8\"></td></tr>");
            Response.Write("<tr><td align=\"right\">");
            Response.Write("<div class=\"page\">");
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
            Response.Write("</div>");
            Response.Write("</td></tr>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#FFFFFF\">");
            Response.Write("<td colspan=\"8\" align=\"center\" height=\"35\">暂无记录</td>");
            Response.Write("</tr>");
        }
        Response.Write("</table>");

    }

    /// <summary>
    ///子帐号 修改资料
    /// </summary>
    public void UpdateSubAccount()
    {
        string Member_SubAccount_TrueName = tools.CheckStr(Request.Form["Member_SubAccount_TrueName"]);
        string Member_SubAccount_Mobile = tools.CheckStr(Request.Form["Member_SubAccount_Mobile"]);
        string Member_SubAccount_Email = tools.CheckStr(Request.Form["Member_SubAccount_Email"]);

        int member_id = tools.NullInt(Session["member_id"]);
        int account_id = tools.NullInt(Session["member_accountid"]);
        if (member_id < 1 || account_id < 1)
        {
            Response.Redirect("/supplier/subAccount_list.aspx");
        }

        if (Member_SubAccount_TrueName == "")
        {
            pub.Msg("info", "提示信息", "请填写真实姓名", false, "{back}");
        }

        if (Member_SubAccount_Mobile != "")
        {
            if (!pub.Checkmobile(Member_SubAccount_Mobile))
            {
                pub.Msg("info", "提示信息", "手机格式不正确，请重新输入", false, "{back}");
            }
        }

        if (Member_SubAccount_Email != "")
        {
            if (!tools.CheckEmail(Member_SubAccount_Email))
            {
                pub.Msg("info", "提示信息", "邮箱格式不正确，请重新输入", false, "{back}");
            }
        }

        MemberSubAccountInfo subAccountInfo = MySubAccount.GetMemberSubAccountByID(account_id);
        if (subAccountInfo != null)
        {
            if (subAccountInfo.MemberID == member_id)
            {
                subAccountInfo.Name = Member_SubAccount_TrueName;
                subAccountInfo.Mobile = Member_SubAccount_Mobile;
                subAccountInfo.Email = Member_SubAccount_Email;

                if (MySubAccount.EditMemberSubAccount(subAccountInfo))
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "/member/account_profile.aspx?tip=success");
                }
                else
                {
                    pub.Msg("error", "错误信息", "子账户修改失败，请稍后再试！", false, "{back}");
                }
            }
            else
            {
                pub.Msg("error", "错误信息", "子账户修改失败，请稍后再试！", false, "{back}");
            }
        }
    }

    public MemberSubAccountInfo GetSubAccountByID(int ID)
    {
        return MySubAccount.GetMemberSubAccountByID(ID);
    }

    public MemberSubAccountInfo GetSubAccountByID()
    {
        int account_id = tools.NullInt(Session["member_accountid"]);

        if (account_id > 0)
        {
            return MySubAccount.GetMemberSubAccountByID(account_id);
        }
        else
        {
            return null;
        }

    }

    //子账号 密码修改
    public void UpdateSubAccountPassword()
    {
        string old_pwd = tools.CheckStr(tools.NullStr(Request.Form["Member_oldpassword"]));
        string Member_password = tools.CheckStr(tools.NullStr(Request.Form["Member_password"]));
        string Member_password_confirm = tools.CheckStr(tools.NullStr(Request.Form["Member_password_confirm"]));
        string verifycode = tools.CheckStr(tools.NullStr(Request.Form["verifycode"]));

        if (verifycode != Session["Trade_Verify"].ToString())
        {
            pub.Msg("info", "提示信息", "验证码输入错误", false, "{back}");
        }

        if (old_pwd == "")
        {
            pub.Msg("info", "提示信息", "请输入6～20位原密码", false, "{back}");
        }

        if (CheckSsn(Member_password) == false)
        {
            pub.Msg("info", "提示信息", "密码包含特殊字符，只接受A-Z，a-z，0-9，不要输入空格", false, "{back}");
        }
        else
        {
            if (Member_password.Length < 6 || Member_password.Length > 20)
            {
                pub.Msg("info", "提示信息", "请输入6～20位新密码", false, "{back}");
            }
        }

        if (Member_password != Member_password_confirm)
        {
            pub.Msg("info", "提示信息", "两次密码输入不一致，请重新输入", false, "{back}");
        }

        old_pwd = encrypt.MD5(old_pwd);
        Member_password = encrypt.MD5(Member_password);


        int member_id = tools.NullInt(Session["member_id"]);
        int account_id = tools.NullInt(Session["member_accountid"]);
        if (member_id < 1 || account_id < 1)
        {
            Response.Redirect("/member/index.aspx");
        }

        MemberSubAccountInfo subAccountInfo = GetSubAccountByID();

        if (subAccountInfo != null)
        {
            if (subAccountInfo.MemberID == member_id)
            {
                string Member_SubAccount_Password = subAccountInfo.Password;

                subAccountInfo.Password = Member_password;

                if (old_pwd != Member_SubAccount_Password)
                {
                    pub.Msg("info", "提示信息", "原密码输入错误，请重试！", false, "{back}");
                }

                if (MySubAccount.EditMemberSubAccount(subAccountInfo))
                {
                    Response.Redirect("/member/account_password.aspx?tip=success");
                }
                else
                {
                    pub.Msg("error", "错误信息", "密码修改失败，请稍后再试！", false, "{back}");
                }
            }
            else
            {
                pub.Msg("error", "错误信息", "密码修改失败，请稍后再试！", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "密码修改失败，请稍后再试！", false, "{back}");
        }
    }

    #endregion

    #region 发票管理

    //检查身份证号
    public bool CheckPersonelCard(string card)
    {
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^(\\d{15}|\\d{17}[x0-9])$");
        return regex.IsMatch(card);
    }

    /// <summary>
    /// 根据发票ID获取发票信息
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    public MemberInvoiceInfo GetMemberInvoiceByID(int ID)
    {
        return MyInvoice.GetMemberInvoiceByID(ID);
    }

    /// <summary>
    /// 根据采购商ID获取发票信息
    /// </summary>
    /// <param name="Member_ID"></param>
    /// <returns></returns>
    public IList<MemberInvoiceInfo> GetMemberInvoicesByMemberID(int Member_ID)
    {
        return MyInvoice.GetMemberInvoicesByMemberID(Member_ID);
    }

    /// <summary>
    /// 添加采购商发票
    /// </summary>
    public void Member_Invoice_Add()
    {
        MemberInvoiceInfo entity = new MemberInvoiceInfo();

        int Invoice_Type = tools.CheckInt(Request.Form["Invoice_Type"]);
        entity.Invoice_Type = Invoice_Type;
        entity.Invoice_MemberID = tools.NullInt(Session["member_id"]);
        entity.Invoice_Address = tools.CheckStr(Request["Invoice_Address"]);
        entity.Invoice_Name = tools.CheckStr(Request["Invoice_Name"]);
        entity.Invoice_Tel = tools.CheckStr(Request["Invoice_Tel"]);
        entity.Invoice_ZipCode = tools.CheckStr(Request["Invoice_ZipCode"]);

        if (Invoice_Type == 0)
        {
            string Invoice_Title = tools.CheckStr(Request.Form["Invoice_Title"]);
            int Invoice_Details = tools.CheckInt(Request.Form["Invoice_Details"]);
            if (Invoice_Title == "个人")
            {
                entity.Invoice_Title = Invoice_Title;
                entity.Invoice_Details = Invoice_Details;
                entity.Invoice_PersonelCard = tools.CheckStr(Request["Invoice_PersonelCard"]);
                if (!CheckPersonelCard(entity.Invoice_PersonelCard))
                {
                    pub.Msg("info", "信息提示", "请选择填写正确格式的身份证号!", false, "{back}");
                }
                entity.Invoice_PersonelName = tools.CheckStr(Request["Invoice_PersonelName"]);
                if (entity.Invoice_PersonelName == "")
                {
                    pub.Msg("info", "信息提示", "请选择填写姓名!", false, "{back}");
                }
                if (Invoice_Details == 0)
                {
                    pub.Msg("info", "信息提示", "请选择发票内容!", false, "{back}");
                }
            }
            else if (Invoice_Title == "单位")
            {
                entity.Invoice_Title = Invoice_Title;
                entity.Invoice_Details = Invoice_Details;
                entity.Invoice_FirmName = tools.CheckStr(Request["Invoice_FirmName"]);
                entity.Invoice_VAT_Bank = tools.CheckStr(Request["Invoice_VAT_Bank"]);
                entity.Invoice_VAT_BankAccount = tools.CheckStr(Request["Invoice_VAT_BankAccount"]);
                entity.Invoice_VAT_RegAddr = tools.CheckStr(Request["Invoice_VAT_RegAddr"]);
                entity.Invoice_VAT_RegTel = tools.CheckStr(Request["Invoice_VAT_RegTel"]);
                entity.Invoice_VAT_Code = tools.CheckStr(Request["Invoice_VAT_Code"]);
                if (entity.Invoice_FirmName == "")
                {
                    pub.Msg("info", "信息提示", "请填写单位名称!", false, "{back}");
                }
            }
        }
        else
        {
            entity.Invoice_VAT_Bank = tools.CheckStr(Request["Invoice_VAT_Bank"]);
            if (entity.Invoice_VAT_Bank == "")
            {
                pub.Msg("info", "信息提示", "请填写开户银行!", false, "{back}");
            }
            entity.Invoice_VAT_BankAccount = tools.CheckStr(Request["Invoice_VAT_BankAccount"]);
            if (entity.Invoice_VAT_BankAccount.Length < 16)
            {
                pub.Msg("info", "提示信息", "请输入至少16位有效银行账号", false, "{back}");
            }
            entity.Invoice_VAT_Code = tools.CheckStr(Request["Invoice_VAT_Code"]);
            if (entity.Invoice_VAT_Code.Length != 15)
            {
                pub.Msg("info", "提示信息", "请输入15位有效纳税人识别号", false, "{back}");
            }
            entity.Invoice_VAT_Content = tools.CheckStr(Request["Invoice_VAT_Content"]);
            if (entity.Invoice_VAT_Content == "")
            {
                pub.Msg("info", "信息提示", "请填写发票内容!", false, "{back}");
            }
            entity.Invoice_VAT_FirmName = tools.CheckStr(Request["Invoice_VAT_FirmName"]);
            if (entity.Invoice_VAT_FirmName == "")
            {
                pub.Msg("info", "信息提示", "请填写单位名称!", false, "{back}");
            }
            entity.Invoice_VAT_RegAddr = tools.CheckStr(Request["Invoice_VAT_RegAddr"]);
            entity.Invoice_VAT_RegTel = tools.CheckStr(Request["Invoice_VAT_RegTel"]);
            entity.Invoice_VAT_Cert = tools.CheckStr(Request["Invoice_VAT_Cert"]);
            if (entity.Invoice_Address == "")
            {
                pub.Msg("info", "信息提示", "请填写邮寄地址!", false, "{back}");
            }

            if (entity.Invoice_Name == "")
            {
                pub.Msg("info", "信息提示", "请填写收票人姓名!", false, "{back}");
            }

            if (entity.Invoice_Tel == "")
            {
                pub.Msg("info", "信息提示", "请填写联系电话!", false, "{back}");
            }
            if (Check_Zip(entity.Invoice_ZipCode) == false)
            {
                pub.Msg("info", "信息提示", "邮政编码为6位数字!", false, "{back}");
            }
        }

        if (MyInvoice.AddMemberInvoice(entity))
        {
            pub.Msg("positive", "操作成功", "发票信息添加成功", true, "account_invoice.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
        }
    }

    /// <summary>
    /// 修改采购商发票
    /// </summary>
    public void Member_Invoice_Edit()
    {
        int invoice_id = tools.CheckInt(Request["invoice_id"]);
        MemberInvoiceInfo entity = MyInvoice.GetMemberInvoiceByID(invoice_id);

        if (entity != null)
        {
            if (entity.Invoice_MemberID != tools.NullInt(Session["member_id"]))
            {
                pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
            }
            entity.Invoice_Address = tools.CheckStr(Request["Invoice_Address"]);
            entity.Invoice_Name = tools.CheckStr(Request["Invoice_Name"]);
            entity.Invoice_Tel = tools.CheckStr(Request["Invoice_Tel"]);
            entity.Invoice_ZipCode = tools.CheckStr(Request["Invoice_ZipCode"]);

            int Invoice_Type = tools.CheckInt(Request["Invoice_Type"]);
            entity.Invoice_Type = Invoice_Type;
            if (Invoice_Type == 0)
            {
                string Invoice_Title = tools.CheckStr(Request["Invoice_Title"]);
                int Invoice_Details = tools.CheckInt(Request["Invoice_Details"]);
                if (Invoice_Title == "个人")
                {
                    entity.Invoice_Title = Invoice_Title;
                    entity.Invoice_Details = Invoice_Details;
                    entity.Invoice_PersonelCard = tools.CheckStr(Request["Invoice_PersonelCard"]);
                    if (!CheckPersonelCard(entity.Invoice_PersonelCard))
                    {
                        pub.Msg("info", "信息提示", "请选择填写正确格式的身份证号!", false, "{back}");
                    }
                    entity.Invoice_PersonelName = tools.CheckStr(Request["Invoice_PersonelName"]);
                    if (entity.Invoice_PersonelName == "")
                    {
                        pub.Msg("info", "信息提示", "请选择填写姓名!", false, "{back}");
                    }
                    if (Invoice_Details == 0)
                    {
                        pub.Msg("info", "信息提示", "请选择发票内容!", false, "{back}");
                    }
                }
                else if (Invoice_Title == "单位")
                {
                    entity.Invoice_Title = Invoice_Title;
                    entity.Invoice_Details = Invoice_Details;
                    entity.Invoice_FirmName = tools.CheckStr(Request["Invoice_FirmName"]);
                    entity.Invoice_VAT_Bank = tools.CheckStr(Request["Invoice_VAT_Bank"]);
                    entity.Invoice_VAT_BankAccount = tools.CheckStr(Request["Invoice_VAT_BankAccount"]);
                    entity.Invoice_VAT_RegAddr = tools.CheckStr(Request["Invoice_VAT_RegAddr"]);
                    entity.Invoice_VAT_RegTel = tools.CheckStr(Request["Invoice_VAT_RegTel"]);
                    entity.Invoice_VAT_Code = tools.CheckStr(Request["Invoice_VAT_Code"]);
                    if (entity.Invoice_FirmName == "")
                    {
                        pub.Msg("info", "信息提示", "请填写单位名称!", false, "{back}");
                    }
                }
            }
            else
            {
                entity.Invoice_VAT_Bank = tools.CheckStr(Request["Invoice_VAT_Bank"]);
                if (entity.Invoice_VAT_Bank == "")
                {
                    pub.Msg("info", "信息提示", "请填写开户银行!", false, "{back}");
                }
                entity.Invoice_VAT_BankAccount = tools.CheckStr(Request["Invoice_VAT_BankAccount"]);
                if (entity.Invoice_VAT_BankAccount.Length < 16)
                {
                    pub.Msg("info", "提示信息", "请输入至少16位有效银行账号", false, "{back}");
                }
                entity.Invoice_VAT_Code = tools.CheckStr(Request["Invoice_VAT_Code"]);
                if (entity.Invoice_VAT_Code.Length != 15)
                {
                    pub.Msg("info", "提示信息", "请输入15位有效纳税人识别号", false, "{back}");
                }
                entity.Invoice_VAT_Content = tools.CheckStr(Request["Invoice_VAT_Content"]);
                if (entity.Invoice_VAT_Content == "")
                {
                    pub.Msg("info", "信息提示", "请填写发票内容!", false, "{back}");
                }
                entity.Invoice_VAT_FirmName = tools.CheckStr(Request["Invoice_VAT_FirmName"]);
                if (entity.Invoice_VAT_FirmName == "")
                {
                    pub.Msg("info", "信息提示", "请填写单位名称!", false, "{back}");
                }
                entity.Invoice_VAT_RegAddr = tools.CheckStr(Request["Invoice_VAT_RegAddr"]);
                entity.Invoice_VAT_RegTel = tools.CheckStr(Request["Invoice_VAT_RegTel"]);
                entity.Invoice_VAT_Cert = tools.CheckStr(Request["Invoice_VAT_Cert"]);

                if (entity.Invoice_Address == "")
                {
                    pub.Msg("info", "信息提示", "请填写邮寄地址!", false, "{back}");
                }
                if (entity.Invoice_Name == "")
                {
                    pub.Msg("info", "信息提示", "请填写收票人姓名!", false, "{back}");
                }
                if (entity.Invoice_Tel == "")
                {
                    pub.Msg("info", "信息提示", "请填写联系电话!", false, "{back}");
                }
                if (Check_Zip(entity.Invoice_ZipCode) == false)
                {
                    pub.Msg("info", "信息提示", "邮政编码为6位数字!", false, "{back}");
                }
            }

            if (MyInvoice.EditMemberInvoice(entity))
            {
                pub.Msg("positive", "操作成功", "发票信息修改成功", true, "account_invoice.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
        }
    }

    /// <summary>
    /// 删除采购商发票
    /// </summary>
    public void Member_Invoice_Del()
    {
        int invoice_id = tools.CheckInt(Request["invoice_id"]);
        MemberInvoiceInfo invoiceinfo = GetMemberInvoiceByID(invoice_id);
        if (invoiceinfo != null)
        {
            if (invoiceinfo.Invoice_MemberID != tools.NullInt(Session["member_id"]))
            {
                pub.Msg("error", "错误信息", "操作失败", false, "{back}");
            }
            if (MyInvoice.DelMemberInvoice(invoice_id) > 0)
            {
                pub.Msg("positive", "操作成功", "操作成功", true, "account_invoice.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败", false, "{back}");
            }
        }
    }

    /// <summary>
    /// 采购商发票列表
    /// </summary>
    public void Member_Invoice()
    {
        Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"3\">");
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        int icount = 0;

        if (member_id > 0)
        {
            IList<MemberInvoiceInfo> listInvoice = GetMemberInvoicesByMemberID(member_id);
            if (listInvoice != null)
            {
                foreach (MemberInvoiceInfo entity in listInvoice)
                {
                    icount = icount + 1;
                    if (entity.Invoice_Type == 0)
                    {
                        if (entity.Invoice_Title == "单位")
                        {
                            Response.Write("  <tr>");
                            Response.Write("    <td width=\"10%\" rowspan=\"8\" align=\"center\" class=\"step_num_off\">" + icount + "</td>");
                            Response.Write("    <td width=\"35%\">发票类型：普通发票</td><td>发票抬头：" + entity.Invoice_Title + "</td>");
                            Response.Write("  </tr>");
                            Response.Write("  <tr>");
                            Response.Write("    <td width=\"35%\" colspan=\"2\">单位名称：" + entity.Invoice_FirmName + "</td>");
                            Response.Write("  </tr>");
                            Response.Write("<tr>");
                            Response.Write("    <td width=\"35%\">单位地址：" + entity.Invoice_VAT_RegAddr + "</td><td>单位电话：" + entity.Invoice_VAT_RegTel + "</td>");
                            Response.Write("  </tr>");
                            Response.Write("  <tr>");
                            Response.Write("    <td width=\"35%\">开户银行：" + entity.Invoice_VAT_Bank + "</td><td>银行账户：" + entity.Invoice_VAT_BankAccount + "</td>");
                            Response.Write("  </tr>");
                            Response.Write("  <tr>");
                            Response.Write("    <td width=\"35%\">税号：" + entity.Invoice_VAT_Code + "</td><td>发票内容：明细</td>");
                            Response.Write("  </tr>");
                        }
                        else
                        {

                            Response.Write("  <tr>");
                            Response.Write("    <td width=\"10%\" rowspan=\"6\" align=\"center\" class=\"step_num_off\">" + icount + "</td>");
                            Response.Write("    <td width=\"35%\">发票类型：普通发票</td><td></td>");
                            Response.Write("  </tr>");
                            Response.Write("  <tr>");
                            Response.Write("    <td width=\"35%\">发票抬头：" + entity.Invoice_Title + "</td><td>发票内容：明细</td>");
                            Response.Write("  </tr>");
                            Response.Write("  <tr>");
                            Response.Write("    <td width=\"35%\">姓名：" + entity.Invoice_PersonelName + "</td><td>身份证号：" + entity.Invoice_PersonelCard + "</td>");
                            Response.Write("  </tr>");
                        }
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"35%\">邮寄地址：" + entity.Invoice_Address + "</td><td>邮编：" + entity.Invoice_ZipCode + "</td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"35%\">收票人姓名：" + entity.Invoice_Name + "</td><td>联系电话：" + entity.Invoice_Tel + "</td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"90%\" colspan=\"2\"><input name=\"btn_delete\" type=\"button\" class=\"buttonupload\" id=\"btn_delete\" value=\"修改\" onclick=\"javascript:location.href='/member/account_invoice_edit.aspx?invoice_id=" + entity.Invoice_ID + "';\" /> <input name=\"btn_delete\" type=\"button\" class=\"buttonupload\" id=\"btn_delete\" value=\"删除\" onclick=\"javascript:if(confirm('您确定删除该发票信息吗？')==false){return false;}else{location.href='/member/account_invoice_do.aspx?action=moveinvoice&invoice_id=" + entity.Invoice_ID + "';}\" /></td>");
                        Response.Write("  </tr>");
                        if (icount < listInvoice.Count)
                        {
                            Response.Write("  <tr>");
                            Response.Write("    <td height=\"20\" colspan=\"3\" align=\"center\" class=\"dotline_h\"></td>");
                            Response.Write("  </tr>");
                        }
                    }
                    else
                    {
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"10%\" rowspan=\"7\" align=\"center\" class=\"step_num_off\">" + icount + "</td>");
                        if (entity.Invoice_VAT_Cert != "")
                        {
                            Response.Write("    <td width=\"35%\">发票类型：增值税发票</td><td>发票内容：" + entity.Invoice_VAT_Content + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"" + tools.NullStr(Application["upload_server_url"]) + entity.Invoice_VAT_Content + "\" class=\"a_t12_blue\" target=\"_blank\">查看纳税人资格证</a></td>");
                        }
                        else
                        {
                            Response.Write("    <td width=\"35%\">发票类型：增值税发票</td><td>发票内容：" + entity.Invoice_VAT_Content + "</td>");
                        }
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"35%\">单位名称：" + entity.Invoice_VAT_FirmName + "</td><td>纳税人识别号：" + entity.Invoice_VAT_Code + "</td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"35%\">注册地址：" + entity.Invoice_VAT_RegAddr + "</td><td>注册电话：" + entity.Invoice_VAT_RegAddr + "</td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"35%\">开户银行：" + entity.Invoice_VAT_Bank + "</td><td>银行账户：" + entity.Invoice_VAT_BankAccount + "</td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"35%\">邮寄地址：" + entity.Invoice_Address + "</td><td>邮编：" + entity.Invoice_ZipCode + "</td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"35%\">收票人姓名：" + entity.Invoice_Name + "</td><td>联系电话：" + entity.Invoice_Tel + "</td>");
                        Response.Write("  </tr>");
                        Response.Write("  <tr>");
                        Response.Write("    <td width=\"90%\" colspan=\"2\"><input name=\"btn_delete\" type=\"button\" class=\"buttonupload\" id=\"btn_delete\" value=\"修改\" onclick=\"javascript:location.href='/member/account_invoice_edit.aspx?invoice_id=" + entity.Invoice_ID + "';\" /> <input name=\"btn_delete\" type=\"button\" class=\"buttonupload\" id=\"btn_delete\" value=\"删除\" onclick=\"javascript:if(confirm('您确定删除该发票信息吗？')==false){return false;}else{location.href='/member/account_invoice_do.aspx?action=moveinvoice&invoice_id=" + entity.Invoice_ID + "';}\" /></td>");
                        Response.Write("  </tr>");
                        if (icount < listInvoice.Count)
                        {
                            Response.Write("  <tr>");
                            Response.Write("    <td height=\"20\" colspan=\"3\" align=\"center\" class=\"dotline_h\"></td>");
                            Response.Write("  </tr>");
                        }
                    }
                }
            }
            else
            {
                Response.Write("<tr>");
                Response.Write("<td height=\"50\" align=\"center\"><img src=\"/images/icon_alert.gif\" align=\"absmiddle\"> 您尚未维护发票信息</td></td>");
                Response.Write("</tr>");
            }
        }
        Response.Write("</table>");
    }

    #endregion

    #region 采购商资质

    public IList<MemberCertInfo> GetMemberCertByType(int member_type)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberCertInfo.Member_Cert_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberCertInfo.Member_Cert_Type", "=", member_type.ToString()));
        Query.OrderInfos.Add(new OrderInfo("MemberCertInfo.Member_Cert_Sort", "Desc"));
        return MyCert.GetMemberCerts(Query);
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

    public string Get_Member_Certtmp(int membercert_id, IList<MemberRelateCertInfo> RelateCates)
    {
        string Cert_Img = "";
        if (RelateCates != null)
        {
            foreach (MemberRelateCertInfo entity in RelateCates)
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

    public void Member_Cert_Save()
    {
        string Member_Cert_1, Member_Cert_2, Member_Cert_3;
        int Member_CertType;
        Member_CertType = 0;
        Member_Cert_1 = "";
        Member_Cert_2 = "";
        Member_Cert_3 = "";
        Member_CertType = tools.CheckInt(Request["Member_CertType"]);
        IList<MemberCertInfo> certinfos = null;
        MemberRelateCertInfo ratecate = null;
        MemberInfo entity = GetMemberByID();
        if (entity != null)
        {
            certinfos = GetMemberCertByType(Member_CertType);
            if (certinfos != null)
            {
                foreach (MemberCertInfo certinfo in certinfos)
                {
                    if (tools.CheckStr(Request["Member_Cert" + certinfo.Member_Cert_ID + "_tmp"]).Length == 0)
                    {
                        //pub.Msg("info", "提示信息", "请将资质文件上传完整", false, "{back}");
                        //if (certinfo.Supplier_Cert_Name == "公章")
                        //{

                        //}
                        //else
                        //{
                        pub.Msg("info", "提示信息", "请将资质文件上传完整", false, "{back}");
                        //}
                    }
                }
                //删除资质文件
                MyMember.DelMemberRelateCertByMemberID(entity.Member_ID);
                foreach (MemberCertInfo certinfo in certinfos)
                {
                    ratecate = new MemberRelateCertInfo();
                    ratecate.MemberID = entity.Member_ID;
                    ratecate.CertID = certinfo.Member_Cert_ID;
                    if (tools.CheckStr(Request["Member_Cert" + certinfo.Member_Cert_ID]).Length == 0)
                    {
                        ratecate.Img = tools.CheckStr(Request["Member_Cert" + certinfo.Member_Cert_ID + "_tmp"]);
                    }
                    else
                    {
                        ratecate.Img = tools.CheckStr(Request["Member_Cert" + certinfo.Member_Cert_ID]);
                    }
                    MyMember.AddMemberRelateCert(ratecate);
                    ratecate = null;
                }
            }

            entity.Member_Type = Member_CertType;
            entity.Member_Cert_Status = 1;
            if (MyMember.EditMember(entity, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe")))
            {
                //pub.Msg("positive", "操作成功", "上传完成，请耐心等待审核通过！", true, "/member/member_Cert.aspx");
                pub.Msg("positive", "操作成功", "资质上传成功！", true, "/member/Index.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
        }
    }


    #endregion

    #region "收货地址"

    //获取收货地址信息
    public MemberAddressInfo GetMemberAddressByID(int ID)
    {
        return MyAddr.GetMemberAddressByID(ID);
    }

    /// <summary>
    /// 收货地址表单检查
    /// </summary>
    public void Check_Member_Address_Form()
    {
        int Member_Address_ID = tools.CheckInt(Request.Form["Member_Address_ID"]);
        int Member_Address_MemberID = tools.CheckInt(Session["member_id"].ToString());
        string Member_Address_Country = tools.CheckStr(Request["Member_Address_Country"]);
        string Member_Address_State = tools.CheckStr(Request["Member_Address_State"]);
        string Member_Address_City = tools.CheckStr(Request["Member_Address_City"]);
        string Member_Address_County = tools.CheckStr(Request["Member_Address_County"]);
        string Member_Address_StreetAddress = tools.CheckStr(Request["Member_Address_StreetAddress"]);
        string Member_Address_Zip = tools.CheckStr(Request["Member_Address_Zip"]);
        string Member_Address_Name = tools.CheckStr(Request["Member_Address_Name"]);
        string Member_Address_Phone_Countrycode = tools.CheckStr(Request["Member_Address_Phone_Countrycode"]);
        string Member_Address_Phone_Areacode = "";
        string Member_Address_Phone_Number = tools.CheckStr(Request["Member_Address_Phone_Number"]);
        string Member_Address_Mobile = tools.CheckStr(Request["Member_Address_Mobile"]);
        string Member_Address_Site = "CN";

        if (Member_Address_County == "0" || Member_Address_County == "")
        {
            Response.Write("请选择区域信息");
            Response.End();
        }

        if (Member_Address_StreetAddress == "")
        {
            Response.Write("请将收货地址填写完整");
            Response.End();
        }

        if (Check_Zip(Member_Address_Zip) == false)
        {
            Response.Write("邮政编码应为6位数字");
            Response.End();
        }

        if (Member_Address_Name == "")
        {
            Response.Write("请将收货人姓名填写完整");
            Response.End();
        }

        if (Member_Address_Phone_Number == "" && Member_Address_Mobile == "")
        {
            Response.Write("联系电话与手机请至少填写一项");
            Response.End();
        }

        if (Member_Address_Mobile != "")
        {
            if (Member_Address_Mobile.Length > 11)
            {
                Response.Write("手机号码应为11位数字");
                Response.End();
            }
            else if (pub.Checkmobile(Member_Address_Mobile) == false)
            {
                Response.Write("手机号码错误");
                Response.End();
            }
        }

        Response.Write("success");
    }

    //收货地址添加
    public void Member_Address_Add_back(string action)
    {
        int Member_Address_ID = 0;
        int Member_Address_MemberID = tools.CheckInt(Session["member_id"].ToString());
        string Member_Address_Country = tools.CheckStr(Request.Form["Member_Address_Country"]);
        string Member_Address_State = tools.CheckStr(Request.Form["Member_Address_State"]);
        string Member_Address_City = tools.CheckStr(Request.Form["Member_Address_City"]);
        string Member_Address_County = tools.CheckStr(Request.Form["Member_Address_County"]);
        string Member_Address_StreetAddress = tools.CheckStr(Request.Form["Member_Address_StreetAddress"]);
        string Member_Address_Zip = tools.CheckStr(Request.Form["Member_Address_Zip"]);
        string Member_Address_Name = tools.CheckStr(Request.Form["Member_Address_Name"]);
        string Member_Address_Phone_Countrycode = tools.CheckStr(Request.Form["Member_Address_Phone_Countrycode"]);
        string Member_Address_Phone_Areacode = "";
        string Member_Address_Phone_Number = tools.CheckStr(Request.Form["Member_Address_Phone_Number"]);
        string Member_Address_Mobile = tools.CheckStr(Request.Form["Member_Address_Mobile"]);
        string Member_Address_Site = "CN";


        if (Member_Address_County == "0" || Member_Address_County == "")
        {
            pub.Msg("info", "提示信息", "请选择省市区信息", false, "{back}");
        }
        if (Member_Address_StreetAddress == "")
        {
            pub.Msg("info", "提示信息", "请将联系地址填写完整", false, "{back}");
        }
        if (Check_Zip(Member_Address_Zip) == false)
        {
            pub.Msg("info", "提示信息", "邮编信息应为6位数字", false, "{back}");
        }
        if (Member_Address_Name == "")
        {
            pub.Msg("info", "提示信息", "请将收货人姓名填写完整", false, "{back}");
        }
        if (Member_Address_Phone_Number == "" && Member_Address_Mobile == "")
        {
            pub.Msg("info", "提示信息", "联系电话与手机请至少填写一项", false, "{back}");
        }


        if (pub.Checkmobile(Member_Address_Mobile) == false && Member_Address_Mobile != "")
        {
            pub.Msg("info", "提示信息", "手机号码错误", false, "{back}");
        }



        MemberAddressInfo entity = new MemberAddressInfo();
        entity.Member_Address_ID = Member_Address_ID;
        entity.Member_Address_MemberID = Member_Address_MemberID;
        entity.Member_Address_Country = Member_Address_Country;
        entity.Member_Address_State = Member_Address_State;
        entity.Member_Address_City = Member_Address_City;
        entity.Member_Address_County = Member_Address_County;
        entity.Member_Address_StreetAddress = Member_Address_StreetAddress;
        entity.Member_Address_Zip = Member_Address_Zip;
        entity.Member_Address_Name = Member_Address_Name;
        entity.Member_Address_Phone_Countrycode = Member_Address_Phone_Countrycode;
        entity.Member_Address_Phone_Areacode = Member_Address_Phone_Areacode;
        entity.Member_Address_Phone_Number = Member_Address_Phone_Number;
        entity.Member_Address_Mobile = Member_Address_Mobile;
        entity.Member_Address_Site = Member_Address_Site;

        MyAddr.AddMemberAddress(entity);
        if (action == "address_add")
        {
            Response.Redirect("/member/order_address.aspx");
        }
        else
        {
            Response.Redirect("/cart/order_delivery.aspx");
        }
    }

    public void Member_Address_Add(string action)
    {
        int Member_Address_ID = 0;
        int type = tools.CheckInt(Request["type"]);
        int Member_Address_MemberID = tools.CheckInt(Session["member_id"].ToString());
        string Member_Address_Country = tools.CheckStr(Request["Member_Address_Country"]);
        string Member_Address_State = tools.CheckStr(Request["Member_Address_State"]);
        string Member_Address_City = tools.CheckStr(Request["Member_Address_City"]);
        string Member_Address_County = tools.CheckStr(Request["Member_Address_County"]);
        string Member_Address_StreetAddress = tools.CheckStr(Request["Orders_Address_StreetAddress"]);
        string Member_Address_Zip = tools.CheckStr(Request["Orders_Address_Zip"]);
        string Member_Address_Name = tools.CheckStr(Request["Orders_Address_Name"]);
        string Member_Address_Phone_Countrycode = tools.CheckStr(Request["Member_Address_Phone_Countrycode"]);
        string Member_Address_Phone_Areacode = "";
        string Member_Address_Phone_Number = tools.CheckStr(Request["Orders_Address_Phone_Number"]);
        string Member_Address_Mobile = tools.CheckStr(Request["Orders_Address_Mobile"]);
        string Member_Address_Site = "CN";

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberAddressInfo.Member_Address_Name", "=", Member_Address_Name));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberAddressInfo.Member_Address_MemberID", "=", Member_Address_MemberID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberAddressInfo.Member_Address_County", "=", Member_Address_County));
        IList<MemberAddressInfo> entitys = MyAddr.GetMemberAddresss(Query);
        MemberAddressInfo entity = null;
        if (entitys == null)
        {
            entity = new MemberAddressInfo();
            entity.Member_Address_ID = Member_Address_ID;
            entity.Member_Address_MemberID = Member_Address_MemberID;
            entity.Member_Address_Country = Member_Address_Country;
            entity.Member_Address_State = Member_Address_State;
            entity.Member_Address_City = Member_Address_City;
            entity.Member_Address_County = Member_Address_County;
            entity.Member_Address_StreetAddress = Member_Address_StreetAddress;
            entity.Member_Address_Zip = Member_Address_Zip;
            entity.Member_Address_Name = Member_Address_Name;
            entity.Member_Address_Phone_Countrycode = Member_Address_Phone_Countrycode;
            entity.Member_Address_Phone_Areacode = Member_Address_Phone_Areacode;
            entity.Member_Address_Phone_Number = Member_Address_Phone_Number;
            entity.Member_Address_Mobile = Member_Address_Mobile;
            entity.Member_Address_Site = Member_Address_Site;

            MyAddr.AddMemberAddress(entity);
            Response.Write("true");
            if (type == 1)
            {
                Query = new QueryInfo();
                Query.PageSize = 1;
                Query.CurrentPage = 1;
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberAddressInfo.Member_Address_MemberID", "=", Member_Address_MemberID.ToString()));
                Query.OrderInfos.Add(new OrderInfo("MemberAddressInfo.Member_Address_ID", "DESC"));
                entitys = MyAddr.GetMemberAddresss(Query);
                if (entitys != null)
                {
                    Session["Orders_Address_ID_cart"] = entitys[0].Member_Address_ID;
                    //Session["Orders_Address_ID"] = entitys[0].Member_Address_ID;
                }
            }
        }
        else if (type == 1)
        {
            entity = entitys[0];
            if (entity != null)
            {
                entity.Member_Address_Zip = Member_Address_Zip;
                entity.Member_Address_Phone_Number = Member_Address_Phone_Number;
                entity.Member_Address_Mobile = Member_Address_Mobile;
                entity.Member_Address_StreetAddress = Member_Address_StreetAddress;
                MyAddr.EditMemberAddress(entity);
            }
            else
            {
                entity = new MemberAddressInfo();
            }
            //Session["Orders_Address_ID"] = entity.Member_Address_ID;
            Session["Orders_Address_ID_cart"] = entitys[0].Member_Address_ID;
            Response.Write("");
        }

    }

    //收货地址修改
    public void Member_Address_Edit(string action)
    {
        int Member_Address_ID = tools.CheckInt(Request.Form["Member_Address_ID"]);
        int Member_Address_MemberID = tools.CheckInt(Session["member_id"].ToString());
        string Member_Address_Country = tools.CheckStr(Request.Form["Member_Address_Country"]);
        string Member_Address_State = tools.CheckStr(Request.Form["Member_Address_State"]);
        string Member_Address_City = tools.CheckStr(Request.Form["Member_Address_City"]);
        string Member_Address_County = tools.CheckStr(Request.Form["Member_Address_County"]);
        string Member_Address_StreetAddress = tools.CheckStr(Request.Form["Member_Address_StreetAddress"]);
        string Member_Address_Zip = tools.CheckStr(Request.Form["Member_Address_Zip"]);
        string Member_Address_Name = tools.CheckStr(Request.Form["Member_Address_Name"]);
        string Member_Address_Phone_Countrycode = tools.CheckStr(Request.Form["Member_Address_Phone_Countrycode"]);
        string Member_Address_Phone_Areacode = "";
        string Member_Address_Phone_Number = tools.CheckStr(Request.Form["Member_Address_Phone_Number"]);
        string Member_Address_Mobile = tools.CheckStr(Request.Form["Member_Address_Mobile"]);
        string Member_Address_Site = "CN";


        if (Member_Address_County == "0" || Member_Address_County == "")
        {
            pub.Msg("info", "提示信息", "请选择省市区信息", false, "{back}");
        }
        if (Member_Address_StreetAddress == "")
        {
            pub.Msg("info", "提示信息", "请将联系地址填写完整", false, "{back}");
        }
        if (Check_Zip(Member_Address_Zip) == false)
        {
            pub.Msg("info", "提示信息", "邮编信息应为6位数字", false, "{back}");
        }
        if (Member_Address_Name == "")
        {
            pub.Msg("info", "提示信息", "请将收货人姓名填写完整", false, "{back}");
        }
        if (Member_Address_Phone_Number == "" && Member_Address_Mobile == "")
        {
            pub.Msg("info", "提示信息", "联系电话与手机请至少填写一项", false, "{back}");
        }


        if (pub.Checkmobile(Member_Address_Mobile) == false && Member_Address_Mobile != "")
        {
            pub.Msg("info", "提示信息", "手机号码错误", false, "{back}");
        }



        MemberAddressInfo entity = MyAddr.GetMemberAddressByID(Member_Address_ID);
        if (entity != null)
        {
            if (entity.Member_Address_MemberID == Member_Address_MemberID)
            {
                entity.Member_Address_Country = Member_Address_Country;
                entity.Member_Address_State = Member_Address_State;
                entity.Member_Address_City = Member_Address_City;
                entity.Member_Address_County = Member_Address_County;
                entity.Member_Address_StreetAddress = Member_Address_StreetAddress;
                entity.Member_Address_Zip = Member_Address_Zip;
                entity.Member_Address_Name = Member_Address_Name;
                entity.Member_Address_Phone_Countrycode = Member_Address_Phone_Countrycode;
                entity.Member_Address_Phone_Areacode = Member_Address_Phone_Areacode;
                entity.Member_Address_Phone_Number = Member_Address_Phone_Number;
                entity.Member_Address_Mobile = Member_Address_Mobile;
                MyAddr.EditMemberAddress(entity);
            }
        }


        if (action == "address_edit")
        {
            Response.Redirect("/member/order_address.aspx");
        }
        else
        {
            Response.Redirect("/cart/order_delivery.aspx?member_address_id=" + Member_Address_ID);
        }
    }


    //收货地址列表
    public void Member_Address()
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<table width=\"974\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" >");
        strHTML.Append("<tbody>");
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        string Pageurl = "?action=list";
        PageInfo pageInfo = null;
        int icount = 0;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        if (curpage < 1)
        {
            curpage = 1;
        }

        strHTML.Append("<tr>");
        strHTML.Append("  <td width=\"85\" class=\"name\">收货人</td>");
        strHTML.Append("  <td width=\"250\" class=\"name\">所在地区</td>");
        strHTML.Append("  <td width=\"250\" class=\"name\">详细地址</td>");
        strHTML.Append("  <td width=\"85\" class=\"name\">邮编</td>");
        strHTML.Append("  <td width=\"100\" class=\"name\">电话/手机</td>");
        strHTML.Append("  <td width=\"100\" class=\"name\">操作</td>");
        strHTML.Append("  <td width=\"103\" class=\"name\"></td>");
        strHTML.Append("</tr>");

        if (member_id > 0)
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 10;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberAddressInfo.Member_Address_MemberID", "=", member_id.ToString()));
            Query.OrderInfos.Add(new OrderInfo("MemberAddressInfo.Member_Address_ID", "Desc"));
            IList<MemberAddressInfo> entitys = MyAddr.GetMemberAddresss(Query);
            pageInfo = MyAddr.GetPageInfo(Query);

            if (entitys != null)
            {
                foreach (MemberAddressInfo entity in entitys)
                {
                    icount = icount + 1;

                    if (icount % 2 == 0)
                    {
                        strHTML.Append("<tr class=\"bg\">");
                    }
                    else
                    {
                        strHTML.Append("<tr>");
                    }

                    strHTML.Append("<td>" + entity.Member_Address_Name + "</td>");
                    strHTML.Append("<td>" + addr.DisplayAddress(entity.Member_Address_State, entity.Member_Address_City, entity.Member_Address_County) + "</td>");
                    strHTML.Append("<td>" + entity.Member_Address_StreetAddress + "</td>");
                    strHTML.Append("<td>" + entity.Member_Address_Zip + "</td>");
                    strHTML.Append("<td>" + (entity.Member_Address_Mobile != "" ? "" + entity.Member_Address_Mobile + "" : "" + entity.Member_Address_Phone_Number + "") + "</td>");
                    strHTML.Append("<td>");
                    strHTML.Append("<span><a href=\"/member/order_address.aspx?member_address_id=" + entity.Member_Address_ID + "\" class=\"a12\" >修改</a></span>");
                    strHTML.Append("<span><a href=\"javascript:;\" onclick=\"javascript:if(confirm('您确定删除该收货地址信息吗？')==false){return false;}else{location.href='/member/order_address_do.aspx?action=address_move&member_address_id=" + entity.Member_Address_ID + "';}\">删除</a></span>");
                    strHTML.Append("</td>");
                    if (entity.Member_Address_IsDefault == 1)
                    {
                        strHTML.Append("<td>默认地址</td>");
                    }
                    else
                    {
                        strHTML.Append("<td><span><a href=\"javascript:void(0);\" onclick=\"setDefaultAddress(" + entity.Member_Address_ID + ");\">设为默认</a></span></td>");
                    }
                    strHTML.Append("</tr>");
                }
            }
            else
            {
                //Response.Write("<div height=\"50\"  style=\" height:50px; text-align:center;line-height:50px; color:#707070;\">暂无常用地址信息</div>");
            }
        }
        strHTML.Append("</tbody>");
        strHTML.Append("</table>");
        Response.Write(strHTML.ToString());
        pub.Page(pageInfo.PageCount, pageInfo.CurrentPage, Pageurl, pageInfo.PageSize, pageInfo.RecordCount);
    }

    //删除收货地址
    public void Member_Address_Del(string action)
    {
        int member_address_id = tools.CheckInt(Request.QueryString["member_address_id"]);
        if (member_address_id > 0)
        {
            MemberAddressInfo address = MyAddr.GetMemberAddressByID(member_address_id);
            if (address != null)
            {
                if (address.Member_Address_MemberID == tools.CheckInt(Session["member_id"].ToString()))
                {
                    MyAddr.DelMemberAddress(member_address_id);
                }
            }
        }
        if (action == "address_move")
        {
            Response.Redirect("/member/order_address.aspx");
        }
        else if (action == "cart_address_move")
        {
            Response.Write("true");
        }
    }

    #endregion

    #region "收藏管理"

    /// <summary>
    /// 获取收藏数量
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetMemberFavoritesCount(int type)
    {
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        int count = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberFavoritesInfo.Member_Favorites_MemberID", "=", member_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberFavoritesInfo.Member_Favorites_Type", "=", type.ToString()));
        Query.OrderInfos.Add(new OrderInfo("MemberFavoritesInfo.Member_Favorites_ID", "Desc"));
        IList<MemberFavoritesInfo> favoriates = MyFavor.GetMemberFavoritess(Query);
        if (favoriates != null)
        {
            count = favoriates.Count;
        }
        return count;
    }

    /// <summary>
    /// 商品收藏
    /// </summary>
    /// <param name="uses"></param>
    /// <param name="irowmax"></param>
    public void Member_Favorites(string uses, int irowmax)
    {
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        int icount = 0;
        int irow = 1;
        string productURL = string.Empty;

        QueryInfo Query = new QueryInfo();
        if (uses == "list")
        {
            Query.PageSize = 0;
        }
        else
        {
            Query.PageSize = irowmax;
        }
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberFavoritesInfo.Member_Favorites_MemberID", "=", member_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberFavoritesInfo.Member_Favorites_Type", "=", "0"));
        Query.OrderInfos.Add(new OrderInfo("MemberFavoritesInfo.Member_Favorites_ID", "Desc"));
        IList<MemberFavoritesInfo> favoriates = MyFavor.GetMemberFavoritess(Query);
        if (favoriates != null)
        {
            Response.Write("<table  border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width:973px;\">");
            Response.Write("  <tr>");
            foreach (MemberFavoritesInfo entity in favoriates)
            {
                ProductInfo product = MyProduct.GetProductByID(entity.Member_Favorites_TargetID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                if (product != null)
                {
                    if (product.Product_IsInsale == 0 || product.Product_IsAudit == 0)
                    {
                        continue;
                    }
                    productURL = pageurl.FormatURL(pageurl.product_detail, product.Product_ID.ToString());

                    icount = icount + 1;
                    Response.Write("    <td width=\"" + (100 / irowmax) + "%\" align=\"center\"><table width=\"120\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width:20%;\">");
                    Response.Write("      <tr>");
                    Response.Write("        <td height=\"120\" align=\"center\" valign=\"middle\"><a href=\"" + productURL + "\" alt=\"" + product.Product_Name + "\" title=\"" + product.Product_Name + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(product.Product_Img, "thumbnail") + "\" border=\"0\" width=\"120\" alt=\"" + product.Product_Name + "\"></a></td>");
                    Response.Write("      </tr>");
                    Response.Write("      <tr>");
                    Response.Write("        <td height=\"30\" align=\"center\" style=\"padding:5px 0 5px 5px\"><a href=\"" + productURL + "\" alt=\"" + product.Product_Name + "\" title=\"" + product.Product_Name + "\" target=\"_blank\">" + tools.CutStr(product.Product_Name, 30, "") + "</a>");
                    Response.Write("        </td>");
                    Response.Write("      </tr>");
                    Response.Write("      <tr>");
                    Response.Write("        <td height=\"30\" align=\"center\"  style=\"padding:5px 0 5px 5px\">");
                    Response.Write("<span class=\"list_price\">" + pub.FormatCurrency(pub.Get_Member_Price(product.Product_ID, product.Product_Price)) + "</span>");


                    Response.Write("        </td>");
                    Response.Write("      </tr>");

                    Response.Write("      <tr>");
                    Response.Write("        <td height=\"30\" valign=\"middle\" align=\"center\"  style=\"padding:5px 0 5px 5px\">");


                    if (product.Product_UsableAmount > 0 || product.Product_IsNoStock == 1)
                    {
                        Response.Write("<a href=\"" + pageurl.FormatURL(pageurl.product_detail, product.Product_ID.ToString()) + "\"><img src=\"/images/btn_buy.jpg\" align=\"absmiddle\" border=\"0\"  alt=\"添加到购物车\" style=\"display:inline-block;\" /></a> ");
                    }
                    else
                    {
                        Response.Write("<a><img src=\"/Images/btn_nostock.jpg\" align=\"absmiddle\" border=\"0\"  alt=\"暂无货\" style=\"display:inline-block;\" /></a> ");
                    }

                    Response.Write("<a href=\"/member/fav_do.aspx?action=goods_move&id=" + entity.Member_Favorites_ID + "\" onclick=\"if(confirm('确定将选择商品从收藏夹中删除吗？')==false){return false;}\"><img src=\"/Images/btn_del.jpg\" align=\"absmiddle\" border=\"0\" alt=\"从收藏中移除该商品\" style=\"display:inline-block;\" /></a>");


                    Response.Write("        </td>");
                    Response.Write("      </tr>");
                    Response.Write("    </table></td>");
                    irow = irow + 1;

                    if (irow > irowmax)
                    {
                        irow = 1;
                        Response.Write("</tr><tr><td colspan=\"" + irowmax + "\" height=\"15\"></td></tr><tr>");
                    }
                }
            }
            for (int i = irow; i <= irowmax; i++)
            {
                Response.Write("<td width=\"" + (100 / irowmax) + "%\" align=\"center\"></td>");
            }
            Response.Write("  </tr>");
            Response.Write("</table>");
        }
        else
        {
            Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            Response.Write("<tr><td align=\"center\" height=\"30\" class=\"t12_grey\" style=\"padding:5px 0 5px 5px ;color:#ff6600\">您还没有收藏商品</td></tr>");
            Response.Write("</table>");
        }
    }




    //商铺收藏
    public void Member_Shop_Favorites(string uses, int irowmax)
    {
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        int icount = 0;
        int irow = 1;
        string productURL = string.Empty;

        QueryInfo Query = new QueryInfo();
        if (uses == "list")
        {
            Query.PageSize = 0;
        }
        else
        {
            Query.PageSize = irowmax;
        }
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberFavoritesInfo.Member_Favorites_MemberID", "=", member_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberFavoritesInfo.Member_Favorites_Type", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("MemberFavoritesInfo.Member_Favorites_ID", "Desc"));
        IList<MemberFavoritesInfo> favoriates = MyFavor.GetMemberFavoritess(Query);
        if (favoriates != null)
        {
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
            Response.Write("  <tr>");
            foreach (MemberFavoritesInfo entity in favoriates)
            {
                SupplierShopInfo shopinfo = MyShop.GetSupplierShopByID(entity.Member_Favorites_TargetID);
                if (shopinfo != null)
                {

                    if (shopinfo.Shop_Status == 0)
                    {
                        continue;
                    }

                    icount = icount + 1;
                    Response.Write("    <td width=\"" + (100 / irowmax) + "%\" align=\"center\"><table width=\"120\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"width:20%;\">");
                    Response.Write("      <tr>");
                    Response.Write("        <td height=\"120\" align=\"center\" valign=\"middle\"><a href=\"" + supplier.GetShopURL(shopinfo.Shop_Domain) + "\" alt=\"" + shopinfo.Shop_Name + "\" title=\"" + shopinfo.Shop_Name + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(shopinfo.Shop_Img, "fullpath") + "\" border=\"0\" width=\"120\" alt=\"" + shopinfo.Shop_Name + "\"></a></td>");
                    Response.Write("      </tr>");
                    Response.Write("      <tr>");
                    Response.Write("        <td height=\"30\" align=\"center\" style=\"padding:5px 0 5px 5px\"><a href=\"http://" + shopinfo.Shop_Domain + supplier.GetShopDomain() + "\" alt=\"" + shopinfo.Shop_Name + "\" title=\"" + shopinfo.Shop_Name + "\" target=\"_blank\">" + tools.CutStr(shopinfo.Shop_Name, 30, "") + "</a>");
                    Response.Write("        </td>");
                    Response.Write("      </tr>");

                    Response.Write("      <tr>");
                    Response.Write("        <td height=\"30\" valign=\"middle\" align=\"center\" style=\"padding:5px 0 5px 5px\">");
                    Response.Write("<a href=\"/member/fav_do.aspx?action=shop_move&id=" + entity.Member_Favorites_ID + "\" onclick=\"if(confirm('确定将选择店铺从收藏夹中删除吗？')==false){return false;}\"><img src=\"/Images/btn_del.jpg\" align=\"absmiddle\" border=\"0\" alt=\"从收藏中移除该店铺\" style=\"display:inline-block;\" /></a>");
                    Response.Write("        </td>");
                    Response.Write("      </tr>");
                    Response.Write("    </table></td>");
                    irow = irow + 1;

                    if (irow > irowmax)
                    {
                        irow = 1;
                        Response.Write("</tr><tr><td colspan=\"" + irowmax + "\" height=\"15\"></td></tr><tr>");
                    }
                }
            }
            for (int i = irow; i <= irowmax; i++)
            {
                Response.Write("<td width=\"" + (100 / irowmax) + "%\" align=\"center\"></td>");
            }
            Response.Write("  </tr>");
            Response.Write("</table>");
        }
        else
        {
            Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            Response.Write("<tr><td align=\"center\" height=\"30\" class=\"t12_grey\" style=\"padding:5px 0 5px 5px\">您还没有收藏店铺</td></tr>");
            Response.Write("</table>");
        }
    }

    /// <summary>
    /// 添加收藏
    /// </summary>
    /// <param name="action"></param>
    /// <param name="targetid"></param>
    public void Member_Favorites_Add(string action, int targetid)
    {
        if (targetid == 0)
        {
            Response.Write("请选择要添加到收藏夹的内容");
            Response.End();
        }

        if (tools.NullInt(Session["member_id"]) == 0 && tools.NullInt(Session["supplier_id"]) > 0)
        {
            Response.Write("您的身份无法收藏商品！");
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

    //从收藏夹移除
    public void Member_Favorites_Del(int ID)
    {
        if (ID == 0)
        {
            pub.Msg("info", "信息提示", "请选择要删除的内容", false, "{back}");
        }
        int member_id = tools.NullInt(Session["member_id"]);
        MemberFavoritesInfo favor = MyFavor.GetMemberFavoritesByID(ID);
        if (favor != null)
        {
            if (favor.Member_Favorites_MemberID == member_id)
            {
                MyFavor.DelMemberFavorites(ID);
                if (favor.Member_Favorites_Type == 0)
                {
                    Response.Redirect("/member/member_favorites.aspx");
                }
                else
                {
                    Response.Redirect("/member/member_shop_favorites.aspx");
                }
            }
            else
            {
                pub.Msg("info", "信息提示", "收藏夹信息删除失败，请稍后再试！", false, "{back}");
            }
        }
        else
        {
            pub.Msg("info", "信息提示", "收藏夹信息删除失败，请稍后再试！", false, "{back}");
        }
    }

    public void Member_Top_Favoriets()
    {
        StringBuilder strHTML = new StringBuilder();

        int member_id = tools.CheckInt(Session["member_id"].ToString());
        string productURL = string.Empty;


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
                    if (product.Product_IsInsale == 0 || product.Product_IsAudit == 0)
                    {
                        continue;
                    }
                    productURL = pageurl.FormatURL(pageurl.product_detail, product.Product_ID.ToString());

                    strHTML.Append("<dl>");
                    strHTML.Append("<dt><a href=\"" + productURL + "\" target=\"_blank\">");
                    strHTML.Append("<img src=\"" + pub.FormatImgURL(product.Product_Img, "thumbnail") + "\" /></a></dt>");
                    strHTML.Append("<dd>");
                    strHTML.Append("<p><a href=\"" + productURL + "\" target=\"_blank\">" + product.Product_Name + "</a></p>");
                    //if (product.Product_PriceType == 1)
                    //{
                    strHTML.Append("<p>" + pub.FormatCurrency(pub.GetProductPrice(product.Product_ManualFee, product.Product_Weight)) + "</p>");
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

    #endregion

    #region "邮件处理"

    //替换系统变量
    public string replace_sys_config(string replacestr)
    {
        string result_value = "";
        result_value = replacestr;
        result_value = result_value.Replace("{sys_config_site_name}", Application["site_name"].ToString());
        result_value = result_value.Replace("{sys_config_site_url}", Application["site_url"].ToString());
        result_value = result_value.Replace("{sys_config_site_tel}", Application["site_tel"].ToString());
        return result_value;
    }

    //邮件模版
    public string mail_template(string template_name, string member_email, string member_password, string member_verifycode)
    {
        string mailbody = "";
        switch (template_name)
        {
            case "emailverify":
                mailbody = "<p>欢迎您注册{sys_config_site_name}！请点击下面的链接进行验证。</p>";
                mailbody = mailbody + "<p><a href=\"{sys_config_site_url}/member/register_do.aspx?action=emailverify&VerifyCode={member_verifycode}\" target=\"_blank\">{sys_config_site_url}/member/register_do.aspx?action=emailverify&VerifyCode={member_verifycode}</a></p>";
                mailbody = mailbody + "<p>如果链接无法点击，请将以上链接复制到浏览器地址栏中打开，即可完成验证！</p>";
                mailbody = mailbody + "<p>如果有任何疑问，欢迎<a href=\"{sys_config_site_url}/help/feedback.aspx\" target=\"_blank\">给我们留言</a>，我们将尽快给您回复！</p>";
                mailbody = mailbody + "<p><font color=red>为保证您正常接收邮件，建议您将此邮件地址加入到地址簿中。</font></p>";

                break;
            case "emailverify_success":
                mailbody = "<p>验证成功，欢迎使用{sys_config_site_name}！</p>";
                mailbody = mailbody + "<p><strong><a href=\"{sys_config_site_url}\" target=\"_blank\">现在就开始，体验网络的无穷乐趣</a></strong></p>";
                mailbody = mailbody + "<p>如果有任何疑问，欢迎<a href=\"{sys_config_site_url}/help/feedback.aspx\" target=\"_blank\">给我们留言</a>，我们将尽快给您回复！</p>";
                mailbody = mailbody + "<p><font color=red>为保证您正常接收邮件，建议您将此邮件地址加入到地址簿中。</font></p>";

                break;
            case "getpass":
                mailbody = "<p>收到此邮件是因为您申请了重新设置密码。如果您没有申请，请忽略这封邮件。</p>";
                mailbody = mailbody + "<p>请点击下面的链接来重新设置密码</p>";
                //mailbody = mailbody + "<p><a href=\"{sys_config_site_url}/member/login_do.aspx?action=verify&VerifyCode={member_verifycode}\" target=\"_blank\">{sys_config_site_url}/member/login_do.aspx?action=verify&VerifyCode={member_verifycode}</a></p>";
                mailbody = mailbody + "<p><a href=\"{sys_config_site_url}/member/login_do.aspx?action=verify&VerifyCode={member_verifycode}\" target=\"_blank\">{sys_config_site_url}/member/login_do.aspx?action=verify&VerifyCode={member_verifycode}</a></p>";
                mailbody = mailbody + "<p>如果链接无法点击，请将以上链接复制到浏览器地址栏中打开，即可重新设置密码！</p>";
                mailbody = mailbody + "<p>如果有任何疑问，欢迎<a href=\"{sys_config_site_url}/help/feedback.aspx\" target=\"_blank\">给我们留言</a>，我们将尽快给您回复！</p>";
                mailbody = mailbody + "<p><font color=red>为保证您正常接收邮件，建议您将此邮件地址加入到地址簿中。</font></p>";

                break;
            case "getpass_success":
                mailbody = "<p>您的密码已重新设置，请牢记新密码。</p>";
                mailbody = mailbody + "<p><strong><a href=\"{sys_config_site_url}/login.aspx\" target=\"_blank\">点击这里登录您的帐号</a></strong></p>";
                mailbody = mailbody + "<p>如果有任何疑问，欢迎<a href=\"{sys_config_site_url}/help/feedback.aspx\" target=\"_blank\">给我们留言</a>，我们将尽快给您回复！</p>";
                mailbody = mailbody + "<p><font color=red>为保证您正常接收邮件，建议您将此邮件地址加入到地址簿中。</font></p>";
                break;
            case "order_create":
                mailbody = "<p>感谢您通过{sys_config_site_name}购物，您的订单信息如下：</p>";
                mailbody = mailbody + "<p>" + Myorder.Order_Detail_Goods_Mail(member_verifycode) + "</p>";
                mailbody = mailbody + "<p>再次感谢您对{sys_config_site_name}的支持，并真诚欢迎您再次光临{sys_config_site_name}!</p>";
                mailbody = mailbody + "<p>如果有任何疑问，欢迎<a href=\"{sys_config_site_url}/help/feedback.aspx\" target=\"_blank\">给我们留言</a>，我们将尽快给您回复！</p>";
                mailbody = mailbody + "<p><font color=red>为保证您正常接收邮件，建议您将此邮件地址加入到地址簿中。</font></p>";
                break;
        }
        mailbody = mailbody.Replace("{member_verifycode}", member_verifycode);
        mailbody = mailbody.Replace("{member_password}", member_password);
        mailbody = mailbody.Replace("{member_email}", member_email);
        return mailbody;
    }

    #endregion

    #region 消息通知

    public void Member_MessageList(int num)
    {
        StringBuilder strHTML = new StringBuilder();
        SupplierInfo supplierInfo = null;
        string supplier_name = "系统";
        PageInfo pageInfo = null;
        int member_id = tools.NullInt(Session["member_id"]);
        int current_page = tools.CheckInt(Request["page"]);
        if (current_page < 1)
        {
            current_page = 1;
        }
        int page_size = num;
        int i = 0;
        string page_url = "?action=list";


        QueryInfo Query = new QueryInfo();
        Query.PageSize = page_size;
        Query.CurrentPage = current_page;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMessageInfo.Message_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_UserType", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_ReceiveID", "=", member_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_IsHidden", "=", "0"));
        Query.OrderInfos.Add(new OrderInfo("SysMessageInfo.Message_Addtime", "desc"));
        //Query.OrderInfos.Add(new OrderInfo("SysMessageInfo.Message_Status", "asc"));
        IList<SysMessageInfo> entitys = MySysMessage.GetSysMessages(Query);
        pageInfo = MySysMessage.GetPageInfo(Query);

        strHTML.Append("<table width=\"975\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
        strHTML.Append("<tbody>");
        strHTML.Append("<tr >");
        strHTML.Append("<td width=\"80\" class=\"name\"  style=\"color:#ff6600\"><input name=\"chk_all_messages\" id=\"chk_all_messages\"  onclick=\"check_Cart_All();\" type=\"checkbox\"  />全选</td>");
        strHTML.Append("<td width=\"70\" class=\"name\">消息ID</td>");
        strHTML.Append("<td width=\"500\" class=\"name\">消息内容</td>");
        strHTML.Append("<td width=\"150\" class=\"name\">消息状态</td>");
        strHTML.Append("<td width=\"172\" class=\"name\">发送时间</td>");
        strHTML.Append("</tr>");
        if (entitys != null)
        {
            foreach (SysMessageInfo entity in entitys)
            {
                i++;
                if (i % 2 == 0)
                {
                    strHTML.Append("<tr class=\"bg\">");
                }
                else
                {
                    strHTML.Append("<tr>");
                }

                strHTML.Append("<td><span style=\"float:left;padding-left:17.2px;padding-top:10px;\"><input type=\"checkbox\" id=\"chk_cart_goods_" + entity.Message_ID + "\" name=\"chk_messages\" value=\"" + entity.Message_ID + "\" onclick=\"setSelectSubAll(" + entity.Message_ID + "," + entity.Message_ID + ");\" class=\"select-sub" + entity.Message_ID + "\"   /></span></td>");
                strHTML.Append("<td>" + entity.Message_ID + "</td>");


                strHTML.Append("<td><span>" + entity.Message_Content + "</span></td>");

                if (entity.Message_Status == 1)
                {
                    strHTML.Append("   <td> <a href=\"javascript:void(0);\" onclick=\"chagemessageactive('0','" + entity.Message_ID + "');\">已读</a></td>");
                }


                else
                {
                    strHTML.Append("   <td> <span> <a href=\"javascript:void(0);\" onclick=\"chagemessageactive('1','" + entity.Message_ID + "');\">未读</a></span></td>");
                }

                strHTML.Append("<td>" + entity.Message_Addtime.ToString("yyyy-MM-dd HH:mm:ss") + "</td>");
                strHTML.Append("</tr>");
            }
            //strHTML.Append("<div><input name=\"chk_all_messages\" id=\"chk_all_messages\"  onclick=\"check_Cart_All();\" type=\"checkbox\"   checked=\"checked\" />全选 <a href=\"javascript:;\" onclick=\"MoveMessagesByID();\">删除</a> <a href=\"javascript:;\" onclick=\"AllMoveMessagesByID();\">全部删除</a>    <a href=\"javascript:;\" onclick=\"MessagesIsReadByID();\">标记为已读</a> <a href=\"javascript:;\" onclick=\"MessagesIsUnReadByID();\">标记为未读</a>   <a href=\"javascript:;\" onclick=\"AllMessagesIsReadByID();\">全部标记为已读</a>   </div>");
            //strHTML.Append("<div> <a href=\"javascript:;\" onclick=\"MoveMessagesByID();\">删除</a> <a href=\"javascript:;\" onclick=\"AllMoveMessagesByID();\">全部删除</a>    <a href=\"javascript:;\" onclick=\"MessagesIsReadByID();\">标记为已读</a> <a href=\"javascript:;\" onclick=\"MessagesIsUnReadByID();\">标记为未读</a>   <a href=\"javascript:;\" onclick=\"AllMessagesIsReadByID();\">全部标记为已读</a>   </div>");
            strHTML.Append("</tbody>");
            strHTML.Append("</table>");
            //strHTML.Append("<div> <span class=\"messageflag\"><a href=\"javascript:;\" onclick=\"MoveMessagesByID();\">删除</a> </span><span class=\"messageflag\"><a href=\"javascript:;\" onclick=\"AllMoveMessagesByID();\">全部删除</a>  </span><span class=\"messageflag\">  <a href=\"javascript:;\" onclick=\"MessagesIsReadByID();\">标记为已读</a></span><span class=\"messageflag\"> <a href=\"javascript:;\" onclick=\"MessagesIsUnReadByID();\">标记为未读</a> </span><span class=\"messageflag\">  <a href=\"javascript:;\" onclick=\"AllMessagesIsReadByID();\">全部标记为已读</a>  </span> </div>");
            strHTML.Append("<div class=\"messageflag\"> <a href=\"javascript:;\" onclick=\"MoveMessagesByID();\">删 除</a> <a href=\"javascript:;\" onclick=\"AllMoveMessagesByID();\">全部删除</a>    <a href=\"javascript:;\" onclick=\"MessagesIsReadByID();\">标记为已读</a> <a href=\"javascript:;\" onclick=\"MessagesIsUnReadByID();\">标记为未读</a>  <a href=\"javascript:;\" onclick=\"AllMessagesIsReadByID();\">全部标记为已读</a>  </span> </div>");
            Response.Write(strHTML.ToString());

            pub.Page(pageInfo.PageCount, pageInfo.CurrentPage, page_url, pageInfo.PageSize, pageInfo.RecordCount);
        }
        else
        {
            strHTML.Append("<tr>");
            strHTML.Append("<td colspan=\"5\" style=\"text-align:center;\">暂无消息通知！</td>");
            strHTML.Append("</tr>");
            strHTML.Append("</tbody>");
            strHTML.Append("</table>");
            Response.Write(strHTML.ToString());
        }

    }

    public void Member_SessionList(int num)
    {
        StringBuilder strHTML = new StringBuilder();

        int i = 0;
        string page_url = "?action=list";

        MessageChatmessageJsonInfo JsonInfo = GetChatMessages("userid", "chatmessage", num);

        strHTML.Append("<table width=\"973\" border=\"0\" cellspacing=\"2\" cellpadding=\"0\">");
        strHTML.Append("<tr>");
        strHTML.Append("<td width=\"150\" class=\"name\">开始时间</td>");
        strHTML.Append("<td width=\"500\" class=\"name\">会话发起页面</td>");
        strHTML.Append("<td width=\"173\" class=\"name\">消息总数</td>");
        strHTML.Append("<td width=\"150\" class=\"name\">查看</td>");
        strHTML.Append("</tr>");
        if (JsonInfo != null)
        {
            if (JsonInfo.datalist != null)
            {
                foreach (MessageChatmessageInfo messageInfo in JsonInfo.datalist)
                {
                    i++;

                    if (i % 2 == 0)
                    {
                        strHTML.Append("<tr class=\"bg\">");
                    }
                    else
                    {
                        strHTML.Append("<tr>");
                    }
                    strHTML.Append("<td>" + pub.ConvertIntDateTime(messageInfo.starttime) + "</td>");
                    strHTML.Append("<td><a href=\"" + messageInfo.url + "\" target=\"_blank\">" + messageInfo.url + "</td>");
                    strHTML.Append("<td>" + messageInfo.msgnum + "</td>");
                    strHTML.Append("<td><a href=\"message_datacogradient.aspx?sessionid=" + messageInfo.sessionid + "\" class=\"a05\" target=\"_blank\">查看消息</a></td>");
                    strHTML.Append("</tr>");
                }
            }
            strHTML.Append("</table>");
            Response.Write(strHTML.ToString());
            pub.Page(JsonInfo.pages.totalpage, JsonInfo.pages.page, page_url, JsonInfo.pages.pagenum, JsonInfo.pages.totalnum);
        }
        else
        {
            strHTML.Append("<tr>");
            strHTML.Append("<td colspan=\"4\" style=\"text-align:center;\">暂无会话记录！</td>");
            strHTML.Append("</tr>");
            strHTML.Append("</table>");
            Response.Write(strHTML.ToString());
        }
    }

    //删除选中的消息
    public void MemberMessges_Del(bool IsAllMessage)
    {
        IList<SysMessageInfo> entitys = null;
        MemberInfo memberinfo = GetMemberByID();
        if (IsAllMessage == false)
        {  //删除选中的消息
            string chk_messages = tools.CheckStr(Request["chk_messages"]);
            if (tools.Left(chk_messages, 1) == ",") { chk_messages = chk_messages.Remove(0, 1); }

            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMessageInfo.Message_Site", "=", pub.GetCurrentSite()));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_ID", "in", chk_messages));
            Query.OrderInfos.Add(new OrderInfo("SysMessageInfo.Message_ID", "Asc"));
            entitys = MySysMessage.GetSysMessages(Query);
        }
        else
        {
            //删除该会员下所有的消息
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;

            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMessageInfo.Message_Site", "=", pub.GetCurrentSite()));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_UserType", "=", "1"));
            if (memberinfo != null)
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_ReceiveID", "=", memberinfo.Member_ID.ToString()));
            }
            else
            {
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_ReceiveID", "=", "-1"));
            }

            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMessageInfo.Message_Site", "=", pub.GetCurrentSite()));

            Query.OrderInfos.Add(new OrderInfo("SysMessageInfo.Message_ID", "Asc"));
            entitys = MySysMessage.GetSysMessages(Query);
        }

        if (entitys != null)
        {
            int i = 0;
            foreach (SysMessageInfo entity in entitys)
            {//隐藏                
                entity.Message_IsHidden = 1;


                //if (MySysMessage.EditSysMessage(entity))
                //{                  
                //    Response.Write("success");
                //}
                //else
                //{
                //    //pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_Message.aspx?message_guide=product");
                //}
                if (MySysMessage.EditSysMessage(entity))
                {

                    i++;
                    if (i == 1)
                    {
                        Response.Write("success");
                    }

                }
                else
                {
                    Response.Write("false");
                    break;
                }
            }
        }

    }

    //批量操作消息读取状态 0:未读 1:已读
    public void MemberMessges_IsRead(int MessageStatus)
    {
        string chk_messages = tools.CheckStr(Request["chk_messages"]);
        if (tools.Left(chk_messages, 1) == ",") { chk_messages = chk_messages.Remove(0, 1); }

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMessageInfo.Message_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_ID", "in", chk_messages));
        Query.OrderInfos.Add(new OrderInfo("SysMessageInfo.Message_ID", "Asc"));
        IList<SysMessageInfo> entitys = MySysMessage.GetSysMessages(Query);
        if (entitys != null)
        {
            int i = 0;
            foreach (SysMessageInfo entity in entitys)
            {//隐藏   
                if (MessageStatus == 0)
                {
                    entity.Message_Status = 0;
                }
                else
                {
                    entity.Message_Status = 1;
                }

                if (MySysMessage.EditSysMessage(entity))
                {

                    i++;
                    if (i == 1)
                    {
                        Response.Write("success");
                    }

                }
                else
                {
                    Response.Write("false");
                    break;
                }

                //if (MySysMessage.EditSysMessage(entity))
                //{

                //    Response.Write("success");
                //}
                //else
                //{

                //}
            }
        }

    }
    /// <summary>
    /// 投诉
    /// </summary>
    public void FileComplaint()
    {

        string Order_Sn = tools.CheckStr(Request.Form["Order_Sn"]);
        int Feedback_Type = tools.CheckInt(Request.Form["Feedback_Type"]);
        string Complaint_Content = tools.CheckStr(Request.Form["Complaint_Content"]);
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        if (Order_Sn == "")
        {
            pub.Msg("error", "错误信息", "请输入订单号", false, "{back}");
        }
        if (Complaint_Content == "")
        {
            pub.Msg("error", "错误信息", "请输入投诉内容", false, "{back}");
        }
        if (member_id == 0)
        {
            pub.Msg("error", "错误信息", "请先登录", false, "login.aspx");
        }
        MemberInfo memberinfo = GetMemberByID(member_id);

        FeedBackInfo entity = new FeedBackInfo();
        entity.Feedback_ID = 0;
        entity.Feedback_Type = Feedback_Type;
        entity.Feedback_MemberID = member_id;
        entity.Feedback_Name = memberinfo.Member_NickName;
        entity.Feedback_Tel = memberinfo.Member_LoginMobile;
        entity.Feedback_Email = memberinfo.Member_Email;
        entity.Feedback_Content = "订单号" + Order_Sn + "投诉内容" + Complaint_Content;
        entity.Feedback_Addtime = DateTime.Now;
        entity.Feedback_IsRead = 0;
        entity.Feedback_Reply_IsRead = 0;
        entity.Feedback_Reply_Content = string.Empty;
        entity.Feedback_Site = "CN";
        entity.Feedback_Address = "";
        entity.Feedback_Amount = 0.0;
        entity.Feedback_Attachment = "";
        if (MyFeedback.AddFeedBack(entity, pub.CreateUserPrivilege("8ccafb10-8a4a-425f-8111-a1e4eb46a0b4")))
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "index");

        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }


    public void AllMemberMessges_IsRead(int MessageStatus)
    {
        MemberInfo memberinfo = GetMemberByID();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMessageInfo.Message_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_UserType", "=", "1"));
        if (memberinfo != null)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_ReceiveID", "=", memberinfo.Member_ID.ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_ReceiveID", "=", "-1"));
        }
        Query.OrderInfos.Add(new OrderInfo("SysMessageInfo.Message_ID", "Asc"));
        IList<SysMessageInfo> entitys = MySysMessage.GetSysMessages(Query);
        if (entitys != null)
        {
            int i = 0;
            foreach (SysMessageInfo entity in entitys)
            {//隐藏   
                if (MessageStatus == 0)
                {
                    entity.Message_Status = 0;
                }
                else
                {
                    entity.Message_Status = 1;
                }



                if (MySysMessage.EditSysMessage(entity))
                {

                    i++;
                    if (i == 1)
                    {
                        Response.Write("success");
                    }

                }
                else
                {
                    Response.Write("false");
                    break;
                }
            }
        }

    }
    #endregion

    #region  采购信息

    public virtual void AddMemberPurchase()
    {
        int Purchase_ID = tools.CheckInt(Request.Form["Purchase_ID"]);
        int Purchase_MemberID = tools.NullInt(Session["member_id"]);
        string Purchase_Title = tools.CheckStr(Request.Form["Purchase_Title"]);
        string Purchase_Img = tools.CheckStr(Request.Form["Purchase_Img"]);
        double Purchase_Amount = tools.CheckFloat(Request.Form["Purchase_Amount"]);
        string Purchase_Unit = tools.CheckStr(Request.Form["Purchase_Unit"]);
        DateTime Purchase_Validity = tools.NullDate(Request.Form["Purchase_Validity"]);
        string Purchase_Intro = tools.CheckStr(Request.Form["Purchase_Intro"]);
        int Purchase_Status = tools.CheckInt(Request.Form["Purchase_Status"]);
        DateTime Purchase_Addtime = DateTime.Now;
        string Purchase_Site = pub.GetCurrentSite();

        MemberPurchaseInfo entity = new MemberPurchaseInfo();
        entity.Purchase_ID = Purchase_ID;
        entity.Purchase_MemberID = Purchase_MemberID;
        entity.Purchase_Title = Purchase_Title;
        entity.Purchase_Img = Purchase_Img;
        entity.Purchase_Amount = Purchase_Amount;
        entity.Purchase_Unit = Purchase_Unit;
        entity.Purchase_Validity = Purchase_Validity;
        entity.Purchase_Intro = Purchase_Intro;
        entity.Purchase_Status = 1;
        entity.Purchase_Addtime = Purchase_Addtime;
        entity.Purchase_Site = Purchase_Site;

        if (MyPurchase.AddMemberPurchase(entity))
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "Purchase_add.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditMemberPurchase()
    {

        int Purchase_ID = tools.CheckInt(Request.Form["Purchase_ID"]);
        string Purchase_Title = tools.CheckStr(Request.Form["Purchase_Title"]);
        string Purchase_Img = tools.CheckStr(Request.Form["Purchase_Img"]);
        double Purchase_Amount = tools.CheckFloat(Request.Form["Purchase_Amount"]);
        string Purchase_Unit = tools.CheckStr(Request.Form["Purchase_Unit"]);
        DateTime Purchase_Validity = tools.NullDate(Request.Form["Purchase_Validity"]);
        string Purchase_Intro = tools.CheckStr(Request.Form["Purchase_Intro"]);

        MemberPurchaseInfo entity = GetMemberPurchaseByID(Purchase_ID);
        if (entity != null)
        {
            entity.Purchase_ID = Purchase_ID;
            entity.Purchase_Title = Purchase_Title;
            entity.Purchase_Img = Purchase_Img;
            entity.Purchase_Amount = Purchase_Amount;
            entity.Purchase_Unit = Purchase_Unit;
            entity.Purchase_Validity = Purchase_Validity;
            entity.Purchase_Intro = Purchase_Intro;

            if (MyPurchase.EditMemberPurchase(entity))
            {
                pub.Msg("positive", "操作成功", "操作成功", true, "Purchase_list.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void DelMemberPurchase()
    {
        int Purchase_ID = tools.CheckInt(Request.QueryString["Purchase_ID"]);
        if (MyPurchase.DelMemberPurchase(Purchase_ID) > 0)
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "Purchase_list.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual MemberPurchaseInfo GetMemberPurchaseByID(int cate_id)
    {
        return MyPurchase.GetMemberPurchaseByID(cate_id);
    }

    public void MemberPurchaseList()
    {
        int member_id = tools.NullInt(Session["member_id"]);
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        Pageurl = "?action=list";
        if (curpage < 1)
        {
            curpage = 1;
        }

        string strBG = "";

        Response.Write("<table width=\"973\" cellpadding=\"0\" cellspacing=\"2\" class=\"table02\">");
        Response.Write("<tr>");
        Response.Write("  <td width=\"242\" class=\"name\">采购标题</td>");
        Response.Write("  <td width=\"241\" class=\"name\">采购数量</td>");
        Response.Write("  <td width=\"238\" class=\"name\">有效期</td>");
        Response.Write("  <td width=\"238\" class=\"name\">添加时间</td>");
        Response.Write("  <td width=\"242\" class=\"name\">操作</td>");
        Response.Write("</tr>");
        string productURL = string.Empty;
        string parentname = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberPurchaseInfo.Purchase_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberPurchaseInfo.Purchase_MemberID", "=", member_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("MemberPurchaseInfo.Purchase_Addtime", "desc"));
        IList<MemberPurchaseInfo> entitys = MyPurchase.GetMemberPurchases(Query);
        PageInfo page = MyPurchase.GetPageInfo(Query);
        if (entitys != null)
        {
            foreach (MemberPurchaseInfo entity in entitys)
            {
                i++;

                if (i % 2 == 0)
                {
                    strBG = " class=\"bg\"";
                }
                else
                {
                    strBG = "";
                }

                Response.Write("<tr " + strBG + ">");
                Response.Write("<td>" + entity.Purchase_Title + "</a></td>");
                Response.Write("<td>" + entity.Purchase_Amount + entity.Purchase_Unit + "</td>");
                Response.Write("<td>" + entity.Purchase_Validity.ToString("yyyy-MM-dd") + "</td>");
                Response.Write("<td>" + entity.Purchase_Addtime.ToString("yyyy-MM-dd HH:mm:ss") + "</td>");
                Response.Write("<td>");
                Response.Write("<a href=\"/member/Purchase_Edit.aspx?Purchase_ID=" + entity.Purchase_ID + "\" class=\"a12\" style=\"color:#ff6600\">修改</a>");
                Response.Write(" <span><a href=\"/member/fav_do.aspx?action=PurchaseMove&Purchase_ID=" + entity.Purchase_ID + "\">删除</a></span>");
                Response.Write(" <a href=\"/member/PurchaseReply_List.aspx?Purchase_ID=" + entity.Purchase_ID + "\" class=\"a12\" style=\"color:#ff6600\">查看报价" + (GetNewPurchaseReplyCount(entity.Purchase_ID) > 0 ? "&nbsp(" + GetNewPurchaseReplyCount(entity.Purchase_ID) + ")" : "") + "</a>");
                Response.Write("</td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");

            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
        }
        else
        {
            Response.Write("<tr><td  colspan=\"5\">没有记录</td></tr>");
        }
        Response.Write("</table>");
    }

    public void MemberPurchaseReplyList()
    {
        int Purchase_ID = tools.CheckInt(Request["Purchase_ID"]);
        QueryInfo Query = new QueryInfo();
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberPurchaseReplyInfo.Reply_PurchaseID", "=", Purchase_ID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("MemberPurchaseReplyInfo.Reply_ID", "asc"));
        IList<MemberPurchaseReplyInfo> entitys = MyPurchaseReply.GetMemberPurchaseReplys(Query);
        PageInfo page = MyPurchaseReply.GetPageInfo(Query);

        string strBG = "";
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        Pageurl = "?action=list";
        if (curpage < 1)
        {
            curpage = 1;
        }

        Response.Write("<table width=\"973\" cellpadding=\"0\" cellspacing=\"2\" class=\"table02\">");
        Response.Write("<tr>");
        Response.Write("  <td width=\"142\" class=\"name\">报价标题</td>");
        Response.Write("  <td width=\"141\" class=\"name\">联系人</td>");
        Response.Write("  <td width=\"138\" class=\"name\">联系人手机</td>");
        Response.Write("  <td width=\"538\" class=\"name\">内容</td>");
        Response.Write("</tr>");

        if (entitys != null)
        {
            foreach (MemberPurchaseReplyInfo entity in entitys)
            {
                i++;

                if (i % 2 == 0)
                {
                    strBG = " class=\"bg\"";
                }
                else
                {
                    strBG = "";
                }

                Response.Write("<tr " + strBG + ">");
                Response.Write("<td>" + entity.Reply_Title + "</a></td>");
                Response.Write("<td>" + entity.Reply_Contactman + "</td>");
                Response.Write("<td>" + entity.Reply_Mobile + "</td>");
                Response.Write("<td>" + entity.Reply_Content + "</td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");

            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
        }
        else
        {
            Response.Write("<tr><td  colspan=\"4\">没有记录</td></tr>");
        }
        Response.Write("</table>");

    }

    public void Merchants_Message_Add()
    {
        int Message_ID = tools.CheckInt(Request.Form["Message_ID"]);
        int Message_MemberID = tools.NullInt(Session["member_id"]);
        int Message_MerchantsID = tools.CheckInt(Request.Form["Message_MerchantsID"]);
        string Message_Content = tools.CheckStr(Request.Form["Message_Content"]);
        string Message_Contactman = tools.CheckStr(Request.Form["Message_Contactman"]);
        string Message_ContactMobile = tools.CheckStr(Request.Form["Message_ContactMobile"]);
        string Message_ContactEmail = tools.CheckStr(Request.Form["Message_ContactEmail"]);
        DateTime Message_AddTime = DateTime.Now;

        if (Message_Content == "")
        {
            Response.Write("请填写留言内容");
            Response.End();
        }

        if (Message_Contactman == "")
        {
            Response.Write("请填写联系人姓名");
            Response.End();
        }

        if (Message_ContactMobile == "")
        {
            Response.Write("请填写手机号码");
            Response.End();
        }
        else
        {
            if (!pub.Checkmobile(Message_ContactMobile))
            {
                Response.Write("手机格式不正确");
                Response.End();
            }
        }

        if (tools.NullInt(Session["member_id"]) > 0)
        {
            SupplierMerchantsMessageInfo entity = new SupplierMerchantsMessageInfo();
            entity.Message_ID = Message_ID;
            entity.Message_MemberID = Message_MemberID;
            entity.Message_MerchantsID = Message_MerchantsID;
            entity.Message_Content = Message_Content;
            entity.Message_Contactman = Message_Contactman;
            entity.Message_ContactMobile = Message_ContactMobile;
            entity.Message_ContactEmail = Message_ContactEmail;
            entity.Message_AddTime = Message_AddTime;

            if (MySupplierMessage.AddSupplierMerchantsMessage(entity))
            {
                Response.Write("success");
                Response.End();
            }
            else
            {
                Response.Write("操作失败，请稍后重试......");
                Response.End();
            }
        }
        else
        {
            Response.Write("操作失败，请稍后重试......");
            Response.End();
        }
    }

    public void Show_MerchantsReply_Dialog()
    {
        StringBuilder str = new StringBuilder();

        int Message_MerchantsID = tools.CheckInt(Request["Message_MerchantsID"]);

        str.Append("<link href=\"/css/index.css\" rel=\"stylesheet\" type=\"text/css\" />");
        str.Append("<script type=\"text/javascript\" src=\"/scripts/jquery-1.8.3.js\"></script>");
        str.Append("<script type=\"text/javascript\" src=\"/scripts/layer/layer.js\"></script>");
        str.Append("<script type=\"text/javascript\" src=\"/scripts/member.js\"></script>");

        str.Append("<div class=\"content02\" style=\" width: 800px;\">");
        str.Append("<div class=\"content02_main\" style=\"width: 800px;\">");

        str.Append("<div class=\"pc_right\" style=\"width: 800px;\">");
        str.Append("<div class=\"blk17\"  style=\"border:0\">");
        str.Append("<form name=\"frm_reply\" id=\"frm_reply\" method=\"post\" action=\"/member/account_do.aspx\">");

        str.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" class=\"table_padding_5\">");
        str.Append("<tr>");
        str.Append("<td width=\"92\" class=\"name\">内容</td>");
        str.Append("<td width=\"801\">");
        str.Append("<textarea name=\"Message_Content\" id=\"Message_Content\" cols=\"80\" rows=\"5\"></textarea><i>*</i>");
        str.Append("</td>");
        str.Append("</tr>");
        str.Append("<tr>");
        str.Append("<td width=\"92\" class=\"name\">联系人</td>");
        str.Append("<td width=\"801\">");
        str.Append("<input name=\"Message_Contactman\" type=\"text\" id=\"Message_Contactman\" style=\"width: 300px;\" class=\"input01\" /><i>*</i>");
        str.Append("</td>");
        str.Append("</tr>");
        str.Append("<tr>");
        str.Append("<td width=\"92\" class=\"name\">联系电话</td>");
        str.Append("<td width=\"801\">");
        str.Append("<input name=\"Message_ContactMobile\" type=\"text\" id=\"Message_ContactMobile\" style=\"width: 300px;\" class=\"input01\" /><i>*</i>");
        str.Append("</td>");
        str.Append("</tr>");
        str.Append("<tr>");
        str.Append("<td width=\"92\" class=\"name\">Email</td>");
        str.Append("<td width=\"801\">");
        str.Append("<input name=\"Message_ContactEmail\" type=\"text\" id=\"Message_ContactEmail\" style=\"width: 300px;\" class=\"input01\" />");
        str.Append("</td>");
        str.Append("</tr>");
        str.Append("<tr>");
        str.Append("<td width=\"92\" class=\"name\">&nbsp;</td>");
        str.Append("<td width=\"801\">");
        str.Append("<span class=\"table_v_title\">");
        str.Append("<input name=\"action\" type=\"hidden\" id=\"action\" value=\"merchantsreply\" />");
        str.Append("<input name=\"Message_MerchantsID\" id=\"Message_MerchantsID\" type=\"hidden\" value=\"" + Message_MerchantsID + "\" />");
        str.Append("<a href=\"javascript:void(0);\" onclick=\"MerchantsReply_Add();\" class=\"a11\">保 存</a>");
        str.Append("</span>");
        str.Append("</td>");
        str.Append("</tr>");
        str.Append("</table>");
        str.Append("</form>");
        str.Append("</div>");
        str.Append("</div>");
        str.Append("<div class=\"clear\"></div>");
        str.Append(" </div>");
        str.Append(" </div>");
        str.Append("</div>");


        Response.Write(str.ToString());
    }

    public int GetNewPurchaseReplyCount(int Purchase_ID)
    {
        int count = 0;
        QueryInfo Query = new QueryInfo();

        if (Purchase_ID > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberPurchaseReplyInfo.Reply_PurchaseID", "=", Purchase_ID.ToString()));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberPurchaseReplyInfo.Reply_PurchaseID", "in", "select Purchase_ID from Member_Purchase where Purchase_MemberID = " + tools.NullInt(Session["member_id"]) + ""));
        }

        Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(mi, '" + DateTime.Now + "',{MemberPurchaseReplyInfo.Reply_Addtime})", "<=", "10"));

        Query.OrderInfos.Add(new OrderInfo("MemberPurchaseReplyInfo.Reply_ID", "asc"));
        PageInfo page = MyPurchaseReply.GetPageInfo(Query);

        if (page != null)
        {
            count = page.RecordCount;
        }
        else
        {
            count = 0;
        }

        return count;
    }

    #endregion

    #region 优惠券

    //会员优惠券列表
    public void Member_Coupon_List()
    {
        int member_id = tools.CheckInt(Session["member_id"].ToString());
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        int type = tools.CheckInt(tools.NullStr(Request["type"]));
        Pageurl = "?action=list&type=" + type;
        if (curpage < 1)
        {
            curpage = 1;
        }

        Response.Write("<table   width=\"973\" border=\"0\" cellspacing=\"2\" cellpadding=\"0\" class=\"table02\">");
        Response.Write("<tr>");
        Response.Write("  <td width=\"400\" style=\"font-size:12px;font-weight:bold;line-height: 54px;text-align:center;\" id=\"aaaaa\" class=\"name\">优惠标题</td>");
        Response.Write("  <td width=\"150\" style=\"font-size:12px;font-weight:bold;line-height: 54px;text-align:center;\" class=\"name\">卡号</td>");
        Response.Write("  <td width=\"150\" style=\"font-size:12px;font-weight:bold;line-height: 54px;text-align:center;\" class=\"name\">验证码</td>");
        Response.Write("  <td width=\"173\" style=\"font-size:12px;font-weight:bold;line-height: 54px;text-align:center;\" class=\"name\">有效期</td>");
        Response.Write("  <td width=\"100\" style=\"font-size:12px;font-weight:bold;line-height: 54px;text-align:center;\" class=\"name\">");
        Response.Write("  <select onchange=\"$.ajaxSetup({async: false});$('#div_couponlist').load('/member/account_do.aspx?action=couponlist&type='+this.options[this.selectedIndex].value);\">");
        Response.Write("    <option value=\"1\" " + pub.CheckSelect(type.ToString(), "1") + " >全部</option>");
        Response.Write("    <option value=\"0\" " + pub.CheckSelect(type.ToString(), "0") + " >正常</option>");
        Response.Write("    <option value=\"2\" " + pub.CheckSelect(type.ToString(), "2") + " >未开始</option>");
        Response.Write("    <option value=\"3\" " + pub.CheckSelect(type.ToString(), "3") + " >已使用</option>");
        Response.Write("    <option value=\"4\" " + pub.CheckSelect(type.ToString(), "4") + " >已过期</option>");
        Response.Write("  </td>");
        Response.Write("</tr>");
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionFavorCouponInfo.Promotion_Coupon_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND(", "int", "PromotionFavorCouponInfo.Promotion_Coupon_Member_ID", "=", member_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("OR)", "int", "PromotionFavorCouponInfo.Promotion_Coupon_Member_ID", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorCouponInfo.Promotion_Coupon_Display", "=", "1"));
        if (type == 3)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorCouponInfo.Promotion_Coupon_Isused", "=", "1"));
        }
        else if (type == 2)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now.ToShortDateString() + "',{PromotionFavorCouponInfo.Promotion_Coupon_Starttime})", ">", "0"));
        }
        else if (type == 4)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now.ToShortDateString() + "',{PromotionFavorCouponInfo.Promotion_Coupon_Endtime})", "<", "0"));
        }
        else if (type == 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionFavorCouponInfo.Promotion_Coupon_Isused", "=", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now.ToShortDateString() + "',{PromotionFavorCouponInfo.Promotion_Coupon_Starttime})", "<=", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d, '" + DateTime.Now.ToShortDateString() + "',{PromotionFavorCouponInfo.Promotion_Coupon_Endtime})", ">=", "0"));
        }
        Query.OrderInfos.Add(new OrderInfo("PromotionFavorCouponInfo.Promotion_Coupon_ID", "Desc"));
        IList<PromotionFavorCouponInfo> entitys = MyCoupon.GetPromotionFavorCoupons(Query, pub.CreateUserPrivilege("18cde8c2-8be5-4b15-b057-795726189795"));
        PageInfo page = MyCoupon.GetPageInfo(Query, pub.CreateUserPrivilege("18cde8c2-8be5-4b15-b057-795726189795"));
        if (entitys != null)
        {
            foreach (PromotionFavorCouponInfo entity in entitys)
            {
                i = i + 1;
                if (i % 2 == 0)
                {
                    Response.Write("<tr class=\"bg\">");
                }
                else
                {
                    Response.Write("<tr>");
                }
                Response.Write("<td ><a>" + entity.Promotion_Coupon_Title + "</a></td>");
                Response.Write("<td ><a>" + entity.Promotion_Coupon_Code + "</a></td>");
                Response.Write("<td ><a>" + entity.Promotion_Coupon_Verifycode + "</a></td>");
                Response.Write("<td ><a>" + entity.Promotion_Coupon_Starttime.ToShortDateString() + "至" + entity.Promotion_Coupon_Endtime.ToShortDateString() + "</a></td>");
                if (entity.Promotion_Coupon_Isused == 1)
                {
                    Response.Write("<td ><a>已使用</a></td>");
                }
                else if (tools.NullDate(entity.Promotion_Coupon_Starttime.ToShortDateString()) > tools.NullDate(DateTime.Now.ToShortDateString()))
                {
                    Response.Write("<td ><a>未开始</a></td>");
                }
                else if (tools.NullDate(entity.Promotion_Coupon_Endtime.ToShortDateString()) < tools.NullDate(DateTime.Now.ToShortDateString()))
                {
                    Response.Write("<td ><a>已过期</a></td>");
                }
                else
                {
                    Response.Write("<td ><a>正常</a></td>");
                }
                Response.Write("</tr>");

            }
            Response.Write("</table>");

            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);

        }
        else
        {
            Response.Write("<tr><td align=\"center\"colspan=\"5\"  height=\"50\" class=\"t12_grey\">没有记录</td></tr>");
            Response.Write("</table>");
        }
    }


    #endregion

    #region "会员网关接口"

    #region 个人会员
    /// <summary>
    /// 创建个人会员接口
    /// </summary>
    /// <param name="entity"></param>
    public MemberJsonInfo Create_Personal_Member(MemberInfo entity)
    {
        MemberJsonInfo jsonInfo = null;

        string sign, sign_type;

        string gateway = mgs + "?_input_charset=utf-8";

        string[] parameters =
        {
            "service=create_personal_member",
            "version=1.0",
            "partner_id="+partner_id,
            "_input_charset=utf-8",
            "mobile="+entity.Member_LoginMobile,
            "email="+entity.Member_Email,
            "uid="+"M"+entity.Member_ID,
            "real_name="+entity.MemberProfileInfo.Member_Name,
            "member_name="+entity.Member_NickName,
            "is_active=T"
        };

        sign_type = "MD5";
        sign = pub.ReturnSignStr(parameters, "utf-8", tradesignkey);

        StringBuilder prestr = new StringBuilder();
        prestr.Append("&service=create_personal_member");
        prestr.Append("&version=1.0");
        prestr.Append("&is_active=T");
        prestr.Append("&partner_id=" + partner_id);
        prestr.Append("&mobile=" + entity.Member_LoginMobile);
        prestr.Append("&email=" + entity.Member_Email);
        prestr.Append("&uid=" + "M" + entity.Member_ID);
        prestr.Append("&real_name=" + entity.MemberProfileInfo.Member_Name);
        prestr.Append("&member_name=" + entity.Member_NickName);
        prestr.Append("&sign=" + sign);
        prestr.Append("&sign_type=" + sign_type);

        string request_url = gateway + prestr.ToString();

        CookieCollection cookies = new CookieCollection();

        string strJson = HttpHelper.GetResponseString(HttpHelper.CreateGetHttpResponse(request_url, 0, "", cookies));

        jsonInfo = JsonHelper.JSONToObject<MemberJsonInfo>(strJson);

        if (jsonInfo != null && jsonInfo.Is_success == "T")
        {
            pub.AddSysInterfaceLog(1, "创建个人会员", "成功", prestr.ToString(), "个人会员创建成功，会员号：" + jsonInfo.Member_id);
        }
        else
        {
            pub.AddSysInterfaceLog(1, "创建个人会员", "失败", prestr.ToString(), jsonInfo.Error_code + ":" + jsonInfo.Error_message);
        }

        return jsonInfo;
    }

    /// <summary>
    /// 修改个人会员手机号接口
    /// </summary>
    /// <param name="identity_no">会员标识号（外部用户ID、手机号或钱包会员ID）</param>
    /// <param name="identity_type">标识类型（UID、MOBILE、MEMBER_ID）</param>
    /// <param name="mobile">新手机号</param>
    /// <returns></returns>
    public MemberJsonInfo Modify_Mobile(string identity_no, string identity_type, string mobile)
    {
        MemberJsonInfo jsonInfo = null;

        string sign, sign_type;

        string gateway = mgs + "?_input_charset=utf-8";

        string[] parameters =
        {
            "service=modify_mobile",
            "version=1.0",
            "partner_id="+partner_id,
            "_input_charset=utf-8",
            "identity_no="+identity_no,
            "identity_type="+identity_type,
            "mobile="+mobile
        };

        sign_type = "MD5";
        sign = pub.ReturnSignStr(parameters, "utf-8", tradesignkey);

        StringBuilder prestr = new StringBuilder();
        prestr.Append("&service=modify_mobile");
        prestr.Append("&version=1.0");
        prestr.Append("&partner_id=" + partner_id);
        prestr.Append("&identity_no=" + identity_no);
        prestr.Append("&identity_type=" + identity_type);
        prestr.Append("&mobile=" + mobile);
        prestr.Append("&sign=" + sign);
        prestr.Append("&sign_type=" + sign_type);

        string request_url = gateway + prestr.ToString();

        CookieCollection cookies = new CookieCollection();

        string strJson = HttpHelper.GetResponseString(HttpHelper.CreateGetHttpResponse(request_url, 0, "", cookies));

        jsonInfo = JsonHelper.JSONToObject<MemberJsonInfo>(strJson);

        if (jsonInfo != null && jsonInfo.Is_success == "T")
        {
            pub.AddSysInterfaceLog(1, "修改个人会员手机号", "成功", prestr.ToString(), "个人会员手机号修改成功");
        }
        else
        {
            pub.AddSysInterfaceLog(1, "修改个人会员手机号", "失败", prestr.ToString(), jsonInfo.Error_code + ":" + jsonInfo.Error_message);
        }

        return jsonInfo;
    }

    /// <summary>
    /// 修改个人会员信息接口
    /// </summary>
    /// <param name="identity_no">会员标识号（外部用户ID、手机号或钱包会员ID）</param>
    /// <param name="identity_type">标识类型（UID、MOBILE、MEMBER_ID）</param>
    /// <param name="member_name">会员名称</param>
    /// <returns></returns>
    public MemberJsonInfo Modify_Personal_Info(string identity_no, string identity_type, string member_name)
    {
        MemberJsonInfo jsonInfo = null;

        string sign, sign_type;

        string gateway = mgs + "?_input_charset=utf-8";

        string[] parameters =
        {
            "service=modify_personal_info",
            "version=1.0",
            "partner_id="+partner_id,
            "_input_charset=utf-8",
            "identity_no="+identity_no,
            "identity_type="+identity_type,
            "member_name="+member_name
        };

        sign_type = "MD5";
        sign = pub.ReturnSignStr(parameters, "utf-8", tradesignkey);

        StringBuilder prestr = new StringBuilder();
        prestr.Append("&service=modify_personal_info");
        prestr.Append("&version=1.0");
        prestr.Append("&partner_id=" + partner_id);
        prestr.Append("&identity_no=" + identity_no);
        prestr.Append("&identity_type=" + identity_type);
        prestr.Append("&member_name=" + member_name);
        prestr.Append("&sign=" + sign);
        prestr.Append("&sign_type=" + sign_type);

        string request_url = gateway + prestr.ToString();

        CookieCollection cookies = new CookieCollection();

        string strJson = HttpHelper.GetResponseString(HttpHelper.CreateGetHttpResponse(request_url, 0, "", cookies));

        jsonInfo = JsonHelper.JSONToObject<MemberJsonInfo>(strJson);

        jsonInfo = JsonHelper.JSONToObject<MemberJsonInfo>(strJson);

        if (jsonInfo != null && jsonInfo.Is_success == "T")
        {
            pub.AddSysInterfaceLog(1, "修改个人会员信息", "成功", prestr.ToString(), "个人会员信息修改成功");
        }
        else
        {
            pub.AddSysInterfaceLog(1, "修改个人会员信息", "失败", prestr.ToString(), jsonInfo.Error_code + ":" + jsonInfo.Error_message);
        }

        return jsonInfo;
    }
    #endregion

    #region 企业会员
    /// <summary>
    /// 创建企业会员接口
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public MemberJsonInfo Create_Enterprise_Member(MemberInfo entity)
    {
        MemberJsonInfo jsonInfo = null;

        string sign, sign_type;

        string gateway = mgs + "?_input_charset=utf-8";

        string[] parameters =
        {
            "service=create_enterprise_member",
            "version=1.0",
            "partner_id="+partner_id,
            "_input_charset=utf-8",
            "login_name="+entity.Member_NickName,
            //"enterprise_name="+entity.MemberProfileInfo.Member_Company,
            "member_name="+entity.Member_NickName,
            "uid="+"M"+entity.Member_ID
        };

        sign_type = "MD5";
        sign = pub.ReturnSignStr(parameters, "utf-8", tradesignkey);

        StringBuilder prestr = new StringBuilder();
        prestr.Append("&service=create_enterprise_member");
        prestr.Append("&version=1.0");
        prestr.Append("&partner_id=" + partner_id);
        prestr.Append("&login_name=" + entity.Member_NickName);
        //prestr.Append("enterprise_name" + entity.MemberProfileInfo.Member_Company);
        prestr.Append("&member_name=" + entity.Member_NickName);
        prestr.Append("&uid=" + "M" + entity.Member_ID);
        prestr.Append("&sign=" + sign);
        prestr.Append("&sign_type=" + sign_type);

        string request_url = gateway + prestr.ToString();

        CookieCollection cookies = new CookieCollection();

        string strJson = HttpHelper.GetResponseString(HttpHelper.CreateGetHttpResponse(request_url, 0, "", cookies));

        jsonInfo = JsonHelper.JSONToObject<MemberJsonInfo>(strJson);

        if (jsonInfo != null && jsonInfo.Is_success == "T")
        {
            pub.AddSysInterfaceLog(1, "创建企业会员", "成功", request_url, "企业会员创建成功，会员号：" + jsonInfo.Member_id);
        }
        else
        {
            pub.AddSysInterfaceLog(1, "创建企业会员", "失败", request_url, jsonInfo.Error_code + ":" + jsonInfo.Error_message);
        }

        return jsonInfo;
        //return request_url;
    }

    public MemberJsonInfo Modify_Enterprise_Info(MemberInfo entity)
    {
        MemberJsonInfo jsonInfo = null;

        string sign, sign_type;

        string gateway = mgs + "?_input_charset=utf-8";

        List<string> liststr = new List<string>();
        liststr.Add("service=modify_enterprise_info");
        liststr.Add("version=1.0");
        liststr.Add("partner_id=" + partner_id);
        liststr.Add("_input_charset=utf-8");
        liststr.Add("identity_no=M" + entity.Member_ID);
        liststr.Add("identity_type=UID");
        if (entity.MemberProfileInfo.Member_Company != "")
        {
            liststr.Add("enterprise_name=" + entity.MemberProfileInfo.Member_Company);
        }

        if (entity.Member_NickName != "")
        {
            liststr.Add("member_name=" + entity.Member_NickName);
        }

        if (entity.MemberProfileInfo.Member_Corporate != "")
        {
            liststr.Add("legal_person=" + entity.MemberProfileInfo.Member_Corporate);
        }

        if (entity.MemberProfileInfo.Member_CorporateMobile != "")
        {
            liststr.Add("legal_person_phone=" + entity.MemberProfileInfo.Member_CorporateMobile);
        }

        if (entity.MemberProfileInfo.Member_State != "" && entity.MemberProfileInfo.Member_City != "" && entity.MemberProfileInfo.Member_County != "")
        {
            liststr.Add("address=" + addr.DisplayAddress(entity.MemberProfileInfo.Member_State, entity.MemberProfileInfo.Member_City, entity.MemberProfileInfo.Member_County));
        }

        if (entity.MemberProfileInfo.Member_BusinessCode != "")
        {
            liststr.Add("license_no=" + entity.MemberProfileInfo.Member_BusinessCode);
        }

        if (entity.MemberProfileInfo.Member_Mobile != "")
        {
            liststr.Add("telephone=" + entity.MemberProfileInfo.Member_Mobile);
        }

        if (entity.MemberProfileInfo.Member_OrganizationCode != "")
        {
            liststr.Add("organization_no=" + entity.MemberProfileInfo.Member_OrganizationCode);
        }

        string[] parameters = liststr.ToArray();

        sign_type = "MD5";
        sign = pub.ReturnSignStr(parameters, "utf-8", tradesignkey);

        StringBuilder prestr = new StringBuilder();
        prestr.Append("&service=modify_enterprise_info");
        prestr.Append("&version=1.0");
        prestr.Append("&partner_id=" + partner_id);
        prestr.Append("&sign=" + sign);
        prestr.Append("&sign_type=" + sign_type);
        prestr.Append("&identity_no=M" + entity.Member_ID);
        prestr.Append("&identity_type=UID");

        if (entity.MemberProfileInfo.Member_Company != "")
        {
            prestr.Append("&enterprise_name=" + entity.MemberProfileInfo.Member_Company);
        }

        if (entity.Member_NickName != "")
        {
            prestr.Append("&member_name=" + entity.Member_NickName);
        }

        if (entity.MemberProfileInfo.Member_Corporate != "")
        {
            prestr.Append("&legal_person=" + entity.MemberProfileInfo.Member_Corporate);
        }

        if (entity.MemberProfileInfo.Member_CorporateMobile != "")
        {
            prestr.Append("&legal_person_phone=" + entity.MemberProfileInfo.Member_CorporateMobile);
        }

        if (entity.MemberProfileInfo.Member_State != "" && entity.MemberProfileInfo.Member_City != "" && entity.MemberProfileInfo.Member_County != "")
        {
            prestr.Append("&address=" + addr.DisplayAddress(entity.MemberProfileInfo.Member_State, entity.MemberProfileInfo.Member_City, entity.MemberProfileInfo.Member_County) + entity.MemberProfileInfo.Member_StreetAddress);
        }

        if (entity.MemberProfileInfo.Member_BusinessCode != "")
        {
            prestr.Append("&license_no=" + entity.MemberProfileInfo.Member_BusinessCode);
        }

        if (entity.MemberProfileInfo.Member_Mobile != "")
        {
            prestr.Append("&telephone=" + entity.MemberProfileInfo.Member_Mobile);
        }

        if (entity.MemberProfileInfo.Member_OrganizationCode != "")
        {
            prestr.Append("&organization_no=" + entity.MemberProfileInfo.Member_OrganizationCode);
        }

        string request_url = gateway + prestr.ToString();

        CookieCollection cookies = new CookieCollection();

        string strJson = HttpHelper.GetResponseString(HttpHelper.CreateGetHttpResponse(request_url, 0, "", cookies));

        jsonInfo = JsonHelper.JSONToObject<MemberJsonInfo>(strJson);

        if (jsonInfo != null && jsonInfo.Is_success == "T")
        {
            pub.AddSysInterfaceLog(1, "修改企业会员信息", "成功", request_url, "信息修改成功！");
        }
        else
        {
            pub.AddSysInterfaceLog(1, "修改企业会员信息", "失败", request_url, jsonInfo.Error_code + ":" + jsonInfo.Error_message + "(" + jsonInfo.Memo + ")");
        }

        return jsonInfo;
    }

    #endregion

    #endregion

    #region 会员消息记录接口

    public string GetMessageParam(string gettype, string messagetype, string starttime, string endtime)
    {
        string userid = "sz_1000_ISME9754_41", orderid = "";
        string searchtime = "";

        string siteid = "sz_1026";

        string timestamp = pub.GetTimeStamp(DateTime.Now);//时间戳
        string authorkey = pub.GetMD5(timestamp + signkey, "utf-8");

        int pagecount = 10;
        int curpage = tools.CheckInt(Request["page"]);
        if (curpage < 1)
        {
            curpage = 1;
        }

        string gateway = ntalker + "/api/message/getsessionlist?authorkey=" + authorkey + "&timestamp=" + timestamp;
        string param = gateway;
        StringBuilder prestr = new StringBuilder();

        prestr.Append("&gettype=" + gettype);
        prestr.Append("&siteid=" + siteid);
        prestr.Append("&messagetype=" + messagetype);

        param = gateway + "&siteid=" + siteid + "&messagetype=" + messagetype;

        if (gettype != "")
        {
            param = param + "&gettype=" + gettype;
        }

        if (gettype == "userid")
        {
            prestr.Append("&userid=" + userid);
            param = param + "&userid=" + userid;
        }
        else
        {

        }

        if (gettype == "orderid" || messagetype == "leavemessage")
        {
            prestr.Append("&orderid=" + orderid);
            prestr.Append("&timeslot=24");

            param = param + "&orderid=" + orderid + "&timeslot=24";
        }

        if ((pagecount > 0 && curpage > 0) && messagetype == "leavemessage")
        {
            prestr.Append("&page=" + curpage);
            prestr.Append("&pagenum=" + pagecount);

            param = param + "&page=" + curpage + "&pagenum=" + pagecount;
        }

        if (searchtime.Length > 0)
        {
            prestr.Append("&searchtime=" + endtime);
            param = param + "&searchtime=" + endtime;
        }

        prestr.Append("&starttime=" + starttime);
        prestr.Append("&endtime=" + endtime);
        param = param + "&starttime=" + starttime + "&endtime=" + endtime;

        return param;
    }

    public string datacogradient(string sessionid, string messagetype)
    {
        string timestamp = pub.GetTimeStamp(DateTime.Now);//时间戳
        string authorkey = pub.GetMD5(timestamp + signkey, "utf-8");
        string gateway = ntalker + "/api/message/datacogradient?authorkey=" + authorkey + "&timestamp=" + timestamp;

        gateway = gateway + "&sessionid=" + sessionid + "&messagetype=" + messagetype;

        return gateway;
    }


    public DatacogradientJsonInfo GetDatacogradien(string sessionid, string messagetype)
    {
        DatacogradientJsonInfo JsonInfo = new DatacogradientJsonInfo();

        string timestamp = pub.GetTimeStamp(DateTime.Now);//时间戳
        string authorkey = pub.GetMD5(timestamp + signkey, "utf-8");

        string gateway = ntalker + "/api/message/datacogradient?authorkey=" + authorkey + "&timestamp=" + timestamp;

        gateway = gateway + "&sessionid=" + sessionid + "&messagetype=" + messagetype;

        CookieCollection cookies = new CookieCollection();

        string strJson = HttpHelper.GetResponseString(HttpHelper.CreateGetHttpResponse(gateway, 0, "", cookies));

        JsonInfo = JsonHelper.JSONToObject<DatacogradientJsonInfo>(strJson);

        return JsonInfo;
    }

    public MessageChatmessageJsonInfo GetChatMessages(string gettype, string messagetype, int pagenum)
    {
        MessageChatmessageJsonInfo JsonInfo = new MessageChatmessageJsonInfo();

        string timestamp = pub.GetTimeStamp(DateTime.Now);//时间戳
        string authorkey = pub.GetMD5(timestamp + signkey, "utf-8");

        string userid = "sz_1000_ISME9754_" + tools.NullInt(Session["member_id"]);

        string siteid = tools.NullStr(Request["siteid"]);

        string orderid = tools.CheckStr(Request["orderid"]);


        if (messagetype == "")
        {
            messagetype = "chatmessage";
        }

        string starttime = pub.GetTimeStamp(tools.NullDate(Request["Start_Time"]));
        string endtime = pub.GetTimeStamp(tools.NullDate(Request["End_Time"]));


        int curpage = tools.CheckInt(Request["page"]);
        if (curpage < 1)
        {
            curpage = 1;
        }

        string searchtime = "";

        string gateway = ntalker + "/api/message/getsessionlist?authorkey=" + authorkey + "&timestamp=" + timestamp;

        StringBuilder prestr = new StringBuilder();

        prestr.Append("&gettype=" + gettype);
        prestr.Append("&messagetype=" + messagetype);

        if (siteid != "")
        {
            prestr.Append("&siteid=" + siteid);
        }

        if (starttime != "" && endtime != "")
        {
            prestr.Append("&starttime=" + starttime);
            prestr.Append("&endtime=" + endtime);
        }

        if (gettype == "userid")
        {
            prestr.Append("&userid=" + userid);
        }

        if (gettype == "orderid" || messagetype == "leavemessage")
        {
            prestr.Append("&orderid=" + orderid);
            prestr.Append("&timeslot");
        }

        if ((pagenum > 0 && curpage > 0) || messagetype == "leavemessage")
        {
            prestr.Append("&page=" + curpage);
            prestr.Append("&pagenum=" + pagenum);
        }

        if (searchtime.Length > 0)
        {
            prestr.Append("&searchtime=" + searchtime);
        }

        string request_url = gateway + prestr.ToString();

        CookieCollection cookies = new CookieCollection();

        string strJson = HttpHelper.GetResponseString(HttpHelper.CreateGetHttpResponse(request_url, 0, "", cookies));

        JsonInfo = JsonHelper.JSONToObject<MessageChatmessageJsonInfo>(strJson);

        return JsonInfo;
    }


    #endregion

    //private string Msg_Json(string err, string url)
    //{
    //    JObject jo = new JObject();
    //    jo.Add(new JProperty("err", err));
    //    jo.Add(new JProperty("url", url));
    //    return jo.ToString();
    //}    

    //public string SmscheckCode()
    //{
    //    string strmobile = tools.CheckStr(Request["phone"]);
    //    if (strmobile.Length == 0)
    //    {
    //        return Msg_Json("请输入手机号！", "");

    //    }

    //    //string verifycode = tools.CheckStr(Request["verifycode"]).ToLower();
    //    //if (verifycode == "")
    //    //{
    //    //    return Msg_Json("请输入验证码！", "");

    //    //}
    //    //else
    //    //{
    //    //    if (verifycode == Session["Trade_Verify"].ToString())
    //    //    {

    //    //    }
    //    //    else
    //    //    {
    //    //        return Msg_Json("无效的验证码！", "");

    //    //    }
    //    //}
    //    //Session["Trade_Verify"] = string.Empty;

    //    System.Collections.Generic.Dictionary<string, string> smscheckcode = new System.Collections.Generic.Dictionary<string, string>();
    //    smscheckcode.Add("sign", strmobile);
    //    smscheckcode.Add("code", new Public_Class().Createvkey(6));
    //    smscheckcode.Add("expiration", DateTime.Now.AddSeconds(3000).ToString());
    //    Session["sms_check"] = smscheckcode;
    //    string[] dataContent = { "" + smscheckcode["code"] + "" };
    //    string content = "【易耐网】尊敬的用户，您的验证码是：" + smscheckcode["code"] + "，验证码5分钟内有效，如有疑问请致电010-53380064";
    //    //new SMSNEW().Send(strmobile, dataContent, "54218");
    //    NSendMessage.SendSmsMessge(strmobile, content, "xxxxx");
    //    pub.DuanXin(strmobile, content);
    //    return Msg_Json("", "");
    //}


}
