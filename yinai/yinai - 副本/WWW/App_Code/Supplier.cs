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

using System.Data.SqlClient;
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
using Glaer.Trade.Util.SQLHelper;
using Glaer.Trade.B2C.BLL.Sys;
using Glaer.Trade.B2C.BLL.CMS;
using Glaer.Trade.B2C.BLL.AD;
using Glaer.Trade.Util.Http;
using System.Net;
using System.Linq;

/// <summary>
///供应商应用类
/// </summary>
public partial class Supplier
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    ITools tools;
    Public_Class pub = new Public_Class();
    IEncrypt encrypt;
    Addr addr = new Addr();
    Orders Myorder = new Orders();
    Contract My_Contract = new Contract();
    private PageURL pageurl;

    private ISupplier MyBLL;
    private IBid MyBid;
    private IMember MyMEM;
    private IOrders MyOrders;
    private IDeliveryWay MyDeliveryway;
    private ISupplierMessage MyMessage;
    private ISupplierAccountLog MyAccountLog;
    private ISupplierShopApply MyShopApply;
    private ISupplierPayBackApply MyPayBackApply;
    private ISupplierCloseShopApply MyCloseShopApply;
    private ISupplierCert MyCert;
    private ISupplierCertType MyCertType;
    private ISupplierShop MyShop;
    private ISupplierShopBanner MyShopBanner;
    private ISupplierShopCss MyShopCss;
    private ISupplierShopPages MyShopPages;
    private ISupplierShopArticle MyShopArticle;
    private ISupplierShopEvaluate MyShopEvaluate;
    private ISupplierCommissionCategory MyCommissionCate;
    private ISupplierCategory MyProductCate;
    private IKeywordBidding MyKeywordBidding;
    private ICategory MyCBLL;
    private IProductType MyTBLL;
    private IProduct MyProduct;
    private IProductTag MyProductTag;
    private IMemberGrade MyGrade;
    private ISupplierGrade MySGrade;
    private IProductPrice MyPrice;
    private IMember MyMember;
    private IPackage MyPackage;
    private IDeliveryWay deliveryway;
    private IOrdersDelivery Mydelivery;
    private ISQLHelper DBHelper;
    private IShoppingAsk MyAsk;
    private IMail mail;
    private IOrdersPayment Mypayment;
    private IAddr addrBLL;
    private ISupplierGrade MySupplierGrade;
    private ISupplierBank MyBank;
    private IOrdersBackApply MyBack;
    private IProduct_Article_Label MyLabel;
    private IProduct_Label MyPro_Label;
    private IOrdersLog MyOrdersLog;
    private ISupplierShopDomain MyDomain;
    private ISupplierOnline MyOnline;
    private IADPosition AdPositionBLL;
    private IPromotionLimit MyLimit;
    private ISysMessage MySysMessage;
    private ITender MyTender;


    IMemberFavorites MyFavor;
    private IAD AdBLL;

    private IOrdersBackApplyProduct MyBackProduct;

    private IMemberAccountLog MyMEMAccountLog;
    private ISupplierShopGrade MyShopGrade;
    private ISupplierFavorites MySFav;
    private ISupplierSubAccount MySubAccount;
    private ISupplierSubAccountLog MySubAccountLog;
    private IFeedBack MyFeedback;
    private ISupplierConsumption MyConsumption;
    private ISupplierMargin MyMargin;
    private ISupplierPurchase MyPurchase;
    private ISupplierPurchaseDetail MyPurchaseDetail;
    private ISupplierPriceReport MyPriceReport;
    private ISupplierPriceReportDetail MyPriceReportDetail;
    private ISupplierPriceAsk MyPriceAsk;
    private ISupplierAddress MyAddr;
    private IOrdersGoodsTmp MyCart;
    private IContract MyContract;
    private IContractTemplate MyTemplate;
    private IContractLog MyContractLog;
    private ISupplierContractTemplate MySupplierContractTemplate;
    private ToUcase MyUcase = new ToUcase();
    private ISupplierInvoice MyInvoice;
    private IContractDividedPayment MyDividedpay;
    private IContractDelivery MyFreight;
    private IContractPayment MyPayment;
    private IPayWay MyPayway;
    private ISupplierPurchaseCategory MyPurchaseCategory;
    private ISupplierAgentProtocal MyAgentProtocal;
    private ISupplierCreditLimitLog myCreditLimitLog;
    private IOrdersAccompanying MyAccompanying;
    private IProductWholeSalePrice MySalePrice;
    private IOrdersInvoice MyOrdersInvoice;
    private ISupplierMerchants MyMerchants;
    private ISupplierMerchantsMessage MyMerchantsMessage;
    private IMemberPurchaseReply MyPurchaseReply;
    private IShoppingAsk MyShopingAsk;
    IMemberToken MyToken;
    IHttpHelper HttpHelper;
    IJsonHelper JsonHelper;
    SysMessage messageclass = new SysMessage();
    string partner_id;
    string tradesignkey;
    int tokentime;
    SecurityUtil securityUtil = new SecurityUtil();
    Credit credit = new Credit();

    ZhongXinUtil.SendMessages sendmessages;


    string erp_url, erp_key, erp_pub_key;
    string mgs;
    string merchantguaranteeaccno;
    string merchantguaranteeaccnm;

    string username;
    string mainaccno;

    public Supplier()
    {
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        encrypt = EncryptFactory.CreateEncrypt();

        HttpHelper = HttpHelperFactory.CreateHttpHelper();
        JsonHelper = JsonHelperFactory.CreateJsonHelper();
        MyMember = MemberFactory.CreateMember();
        MyBid = BidFactory.CreateBid();
        MyBack = OrdersBackApplyFactory.CreateOrdersBackApply();
        MyMerchants = SupplierMerchantsFactory.CreateSupplierMerchants();
        MyMerchantsMessage = SupplierMerchantsMessageFactory.CreateSupplierMerchantsMessage();
        AdPositionBLL = ADPositionFactory.CreateADPosition();
        AdBLL = ADFactory.CreateAD();
        MySGrade = SupplierGradeFactory.CreateSupplierGrade();
        MyBLL = SupplierFactory.CreateSupplier();
        MyMEM = MemberFactory.CreateMember();
        MyOrders = OrdersFactory.CreateOrders();
        MyPayBackApply = SupplierPayBackApplyFactory.CreateSupplierPayBackApply();
        MyCloseShopApply = SupplierCloseShopApplyFactory.CreateSupplierCloseShopApply();
        MyCert = SupplierCertFactory.CreateSupplierCert();
        MyCertType = SupplierCertFactory.CreateSupplierCertType();
        MyDeliveryway = DeliveryWayFactory.CreateDeliveryWay();
        MyMessage = SupplierMessageFactory.CreateSupplierMessage();
        MyAccountLog = SupplierAccountLogFactory.CreateSupplierAccountLog();
        MyShopApply = SupplierShopApplyFactory.CreateSupplierShopApply();
        MyShop = SupplierShopFactory.CreateSupplierShop();
        MyShopBanner = SupplierShopBannerFactory.CreateSupplierShopBanner();
        MyShopCss = SupplierShopCssFactory.CreateSupplierShopCss();
        MyShopPages = SupplierShopPagesFactory.CreateSupplierShopPages();
        MyShopArticle = SupplierShopArticleFactory.CreateSupplierShopArticle();
        MyShopEvaluate = SupplierShopEvaluateFactory.CreateSupplierShopEvaluate();
        MyProductCate = SupplierCategoryFactory.CreateSupplierCategory();
        MyCommissionCate = SupplierCommissionCategoryFactory.CreateSupplierCommissionCategory();
        MyKeywordBidding = KeywordBiddingFactory.CreateKeywordBidding();
        MyCart = OrdersGoodsTmpFactory.CreateOrdersGoodsTmp();
        MyCBLL = CategoryFactory.CreateCategory();
        MyTBLL = ProductTypeFactory.CreateProductType();
        MyProduct = ProductFactory.CreateProduct();
        MyProductTag = ProductTagFactory.CreateProductTag();
        MyGrade = MemberGradeFactory.CreateMemberGrade();
        MyPrice = ProductPriceFactory.CreateProductPrice();
        MyPackage = PackageFactory.CreatePackage();
        deliveryway = DeliveryWayFactory.CreateDeliveryWay();
        Mydelivery = OrdersDeliveryFactory.CreateOrdersDelivery();
        DBHelper = SQLHelperFactory.CreateSQLHelper();
        MyAsk = ShoppingAskFactory.CreateShoppingAsk();
        mail = MailFactory.CreateMail();
        Mypayment = OrdersPaymentFactory.CreateOrdersPayment();
        addrBLL = AddrFactory.CreateAddr();
        MySupplierGrade = SupplierGradeFactory.CreateSupplierGrade();
        MyBank = SupplierBankFactory.CreateSupplierBank();
        MyConsumption = SupplierConsumptionFactory.CreateSupplierConsumption();
        MySysMessage = SysMessageFactory.CreateSysMessage();
        MyLabel = Product_Article_LabelFactory.CreateProduct_Article_Label();
        MyPro_Label = Product_LabelFactory.CreateProduct_Label();
        MyMargin = SupplierMarginFactory.CreateSupplierMargin();
        MyDomain = SupplierShopDomainFactory.CreateSupplierShopDomain();
        MyOnline = SupplierOnlineFactory.CreateSupplierOnline();

        MyFavor = MemberFavoritesFactory.CreateMemberFavorites();
        MyShopingAsk = ShoppingAskFactory.CreateShoppingAsk();
        MyLimit = PromotionLimitFactory.CreatePromotionLimit();
        pageurl = new PageURL(int.Parse(Application["Static_IsEnable"].ToString()));

        MyBackProduct = OrdersBackApplyProductFactory.CreateOrdersBackApplyProduct();
        MyMEMAccountLog = MemberAccountLogFactory.CreateMemberAccountLog();
        MyShopGrade = SupplierShopGradeFactory.CreateSupplierShopGrade();
        MySFav = SupplierFavoritesFactory.CreateSupplierFavorites();
        MySubAccount = SupplierSubAccountFactory.CreateSupplierSubAccount();
        MySubAccountLog = SupplierSubAccountLogFactory.CreateSupplierSubAccountLog();

        MyFeedback = FeedBackFactory.CreateFeedBack();

        MyPurchase = SupplierPurchaseFactory.CreateSupplierPurchase();
        MyPurchaseDetail = SupplierPurchaseDetailFactory.CreateSupplierPurchaseDetail();
        MyPriceReport = SupplierPriceReportFactory.CreateSupplierPriceReport();
        MyPriceReportDetail = SupplierPriceReportDetailFactory.CreateSupplierPriceReportDetail();
        MyPriceAsk = SupplierPriceAskFactory.CreateSupplierPriceAsk();
        MyAddr = SupplierAddressFactory.CreateSupplierAddress();

        MyContract = ContractFactory.CreateContract();
        MyTemplate = ContractTemplateFactory.CreateContractTemplate();
        MyContractLog = ContractLogFactory.CreateContractLog();
        MySupplierContractTemplate = SupplierContractTemplateFactory.CreateSupplierContractTemplate();
        MyInvoice = SupplierInvoiceFactory.CreateSupplierInvoice();
        MyDividedpay = ContractDividedPaymentFactory.CreateContractDividedPayment();
        MyFreight = ContractDeliveryFactory.CreateContractDelivery();
        MyPayment = ContractPaymentFactory.CreateContractPayment();
        MyPayway = PayWayFactory.CreatePayWay();
        MyPurchaseCategory = SupplierPurchaseCategoryFactory.CreateSupplierPurchaseCategory();
        MyAgentProtocal = SupplierAgentProtocalFactory.CreateSupplierAgentProtocal();
        myCreditLimitLog = SupplierCreditLimitLogFactory.CreateSupplierCreditLimitLog();
        MyAccompanying = OrdersAccompanyingFactory.CreateOrdersAccompanying();
        MySalePrice = ProductWholeSalePriceFactory.CreateProductWholeSalePrice();
        MyOrdersInvoice = OrdersInvoiceFactory.CreateOrdersInvoice();
        MyOrdersLog = OrdersLogFactory.CreateOrdersLog();
        MyPurchaseReply = MemberPurchaseReplyFactory.CreateMemberPurchaseReply();
        MyToken = MemberTokenFactory.CreateMemberToken();
        MyTender = TenderFactory.CreateTender();


        sendmessages = new ZhongXinUtil.SendMessages();


        tokentime = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["tokentime"]);
        tradesignkey = System.Web.Configuration.WebConfigurationManager.AppSettings["tradesignkey"].ToString();
        partner_id = tools.NullStr(Application["CreditPayment_Code"]);

        mgs = System.Web.Configuration.WebConfigurationManager.AppSettings["mgs"].ToString();
        erp_url = System.Web.Configuration.WebConfigurationManager.AppSettings["erp_url"].ToString();
        erp_key = System.Web.Configuration.WebConfigurationManager.AppSettings["ERP_Key"].ToString();
        erp_pub_key = System.Web.Configuration.WebConfigurationManager.AppSettings["ERP_Pub_Key"].ToString();



        //商家保证金账户
        merchantguaranteeaccno = System.Configuration.ConfigurationManager.AppSettings["zhongxin_merchantguaranteeaccno"];
        merchantguaranteeaccnm = System.Configuration.ConfigurationManager.AppSettings["zhongxin_merchantguaranteeaccnm"];

        //平台主账号
        username = System.Configuration.ConfigurationManager.AppSettings["zhongxin_username"];
        mainaccno = System.Configuration.ConfigurationManager.AppSettings["zhongxin_mainaccno"];
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


    #region"辅助函数"

    //检查注册邮箱是否使用
    public bool Check_Supplier_Email(string Supplier_Email)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Email", "=", Supplier_Email));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", pub.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("SupplierInfo.Supplier_ID", "Desc"));
        PageInfo page = MyBLL.GetPageInfo(Query, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
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

    //获取用户名昵称
    public string GetMemberNickName(int Member_ID)
    {
        string nickname = "--";
        MemberInfo entity = MyMEM.GetMemberByID(Member_ID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
        if (entity != null)
        {
            nickname = entity.Member_NickName;
        }
        return nickname;
    }

    //根据商家邮箱获取信息
    //public SupplierInfo GetSupplierByEmail(string Supplier_Email)
    //{
    //return MyBLL.GetSupplierByEmail(Supplier_Email, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
    //  }

    //检查昵称是否使用
    public bool Check_Supplier_Nickname(string nick_name)
    {
        bool bl1 = CheckSupplierNickName(nick_name);

        bool bl2 = CheckSubAccountName(nick_name);

        if (bl1 || bl2)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private bool CheckSubAccountName(string nick_name)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierSubAccountInfo.Supplier_SubAccount_Name", "=", nick_name));
        Query.OrderInfos.Add(new OrderInfo("SupplierSubAccountInfo.Supplier_SubAccount_ID", "Desc"));
        PageInfo page = MySubAccount.GetPageInfo(Query);
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

    private bool CheckSupplierNickName(string nick_name)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        //Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Nickname", "=", nick_name));
        //Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", pub.GetCurrentSite()));
        //Query.OrderInfos.Add(new OrderInfo("SupplierInfo.Supplier_ID", "Desc"));
        //Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", subCates));
        //Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + subCates + ")"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_NickName", "=", nick_name));
        Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberInfo.Member_Email", "=", nick_name));
        Query.ParamInfos.Add(new ParamInfo("OR", "str", "MemberInfo.Member_LoginMobile", "=", nick_name));


        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", pub.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("MemberInfo.Member_ID", "Desc"));

        PageInfo page = MyMEM.GetPageInfo(Query, pub.CreateUserPrivilege("3a9a9cdf-ef00-407d-98ef-44e23be397e8"));
        //PageInfo page = MyBLL.GetPageInfo(Query, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
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

    private bool CheckSupplierCompanyName(string company_name)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_CompanyName", "=", company_name));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", pub.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("SupplierInfo.Supplier_ID", "Desc"));
        PageInfo page = MyBLL.GetPageInfo(Query, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
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

    //根据编号获取商家信息
    public SupplierInfo GetSupplierByID()
    {
        //int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        //if (supplier_id > 0)
        //{
        //    return MyBLL.GetSupplierByID(supplier_id, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
        //}
        //else
        //{
        //    return null;
        //}


        int member_id = tools.CheckInt(Session["member_id"].ToString());
        if (member_id > 0)
        {
            MemberInfo memberinfo = MyMEM.GetMemberByID(member_id, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
            if (memberinfo != null)
            {
                return MyBLL.GetSupplierByID(memberinfo.Member_SupplierID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }





    }

    //根据编号获取商家信息
    public SupplierInfo GetSupplierByID(int supplier_id)
    {
        if (supplier_id > 0)
        {
            return MyBLL.GetSupplierByID(supplier_id, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
        }
        else
        {
            return null;
        }
    }

    //获取评价产品信息
    public string Get_Evaluate_Product_Price(int Product_ID)
    {
        string product_info = "";
        OrdersGoodsInfo entity = MyOrders.GetOrdersGoodsByID(Product_ID);
        if (entity != null)
        {
            product_info = "<a href=\"" + pageurl.FormatURL(pageurl.product_detail, entity.Orders_Goods_Product_ID.ToString()) + "\" target=\"_blank\">" + entity.Orders_Goods_Product_Name + "</a> <span class=\"t12_orange\">" + pub.FormatCurrency(entity.Orders_Goods_Product_Price) + "</span> 元";
        }
        return product_info;
    }

    //获取指定店铺收藏数
    public int GetShopFavorAmount(int Shop_ID)
    {
        int amount = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberFavoritesInfo.Member_Favorites_TargetID", "=", Shop_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberFavoritesInfo.Member_Favorites_Type", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("MemberFavoritesInfo.Member_Favorites_ID", "Desc"));
        IList<MemberFavoritesInfo> favoriates = MyFavor.GetMemberFavoritess(Query);
        if (favoriates != null)
        {
            amount = favoriates.Count;
        }
        return amount;
    }

    //获取商家类型
    public int GetSupplierGrade(int supplier_ID)
    {
        SupplierInfo entity = MyBLL.GetSupplierByID(supplier_ID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
        if (entity != null)
        {
            return entity.Supplier_Type;
        }
        else
        {
            return 3;
        }
    }

    //获取默认商家等级
    public SupplierGradeInfo GetSupplierDefaultGrade()
    {
        return MySupplierGrade.GetSupplierDefaultGrade();
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

    public bool Check_Supplier_Margin()
    {
        bool is_true = false;

        SupplierInfo entity = GetSupplierByID();
        if (entity != null && entity.Supplier_AuditStatus == 1)
        {
            SupplierMarginInfo marginInfo = MyMargin.GetSupplierMarginByTypeID(entity.Supplier_Type);
            if (marginInfo != null)
            {
                if (entity.Supplier_Security_Account == marginInfo.Supplier_Margin_Amount)
                {
                    is_true = true;
                }
                else
                {
                    is_true = false;
                }
            }
        }

        return is_true;
    }

    public SupplierCreditLimitLogInfo GetSupplierCreditLimitLogByID(int ID)
    {
        return myCreditLimitLog.GetSupplierCreditLimitLogByID(ID);
    }

    #endregion

    #region "AJAx函数"

    public void Check_SupplierType()
    {
        int type = tools.CheckInt(Request["val"]);
        if (type == 0)
        {
            Response.Write("<font color=\"#ff0000\">请选择供货商类型！</font>");
            return;
        }
    }

    public void Check_Nickname()
    {
        string member_nickname = tools.CheckStr(Request["val"]);
        if (member_nickname == "")
        {
            Response.Write("<font color=\"#ff0000\">请输入用户名！</font>");
            return;
        }
        else
        {
            if (CheckNickname(member_nickname))
            {
                Encoding name_length = System.Text.Encoding.GetEncoding("gb2312");
                byte[] bytes = name_length.GetBytes(member_nickname);
                if (bytes.Length < 4 || bytes.Length > 20)
                {
                    Response.Write("<font color=\"#ff0000\">用户名长度应在4-20位字符！</font>");
                    return;
                }
                //if (Check_Supplier_Nickname(member_nickname) == false)
                //{
                //    Response.Write("<font color=\"#00a226\">用户名输入正确！</font>");
                //    return;
                //}
                //else
                //{
                //    Response.Write("<font color=\"#ff0000\">该用户名已被使用，请使用其他用户名注册</font>");
                //    return;
                //}
                if (Check_Supplier_Nickname(member_nickname) == false)
                {
                    Response.Write("<font color=\"#00a226\">用户名输入正确！</font>");
                    return;
                }
                else
                {
                    Response.Write("<font color=\"#ff0000\">该用户名已被使用，请使用其他用户名注册</font>");
                    return;
                }
            }
            else
            {
                Response.Write("<font color=\"#ff0000\">用户名含有特殊字符！</font>");
                return;
            }
        }
    }

    public void Check_Companyname()
    {
        string companyname = tools.CheckStr(Request["val"]);
        if (companyname == "")
        {
            Response.Write("<font color=\"#ff0000\">请输入单位名称！</font>");
            return;
        }
        else
        {
            if (CheckSupplierCompanyName(companyname) == false)
            {
                Response.Write("<font color=\"#00a226\">单位名称输入正确！</font>");
                return;
            }
            else
            {
                Response.Write("<font color=\"#ff0000\">该单位名称已经注册，不能重复注册</font>");
                return;
            }
        }
    }

    public void Check_SupplierEmail()
    {
        string supplier_email = tools.CheckStr(Request["val"]);
        if (supplier_email == "")
        {
            Response.Write("<font color=\"#ff0000\">请输入E-mail！</font>");
            return;
        }
        else
        {
            if (tools.CheckEmail(supplier_email))
            {

                if (Check_Supplier_Email(supplier_email))
                {
                    Response.Write("<font color=\"#ff0000\">该邮件地址已被使用。请使用另外一个邮件地址进行注册</font>");
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
                Response.Write("<font color=\"#ff0000\">无效的E-mail！</font>");
                return;
            }
        }
    }

    public void Check_Passwprd()
    {
        string supplier_password = tools.CheckStr(Request["val"]);
        if (supplier_password.Length < 6 || supplier_password.Length > 20)
        {
            Response.Write("<font color=\"#ff0000\">请输入6～20位密码</font>");
            return;
        }
        else
        {
            if (CheckSsn(supplier_password))
            {
                Response.Write("<font color=\"#00a226\">密码输入正确！</font>");
                return;
            }
            else
            {
                Response.Write("<font color=\"#ff0000\">密码包含特殊字符！</font>");
                return;
            }
        }
    }

    public void Check_rePasswprd()
    {
        string supplier_repassword = tools.CheckStr(Request["val"]);
        string supplier_password = tools.CheckStr(Request["val1"]);
        if (supplier_repassword.Length < 6 || supplier_repassword.Length > 20)
        {
            Response.Write("<font color=\"#ff0000\">请输入6～20位密码</font>");
            return;
        }
        if (supplier_repassword != supplier_password)
        {
            Response.Write("<font color=\"#ff0000\">两次密码不一致</font>");
            return;
        }
        else
        {
            Response.Write("<font color=\"#00a226\">确认密码输入正确！</font>");
            return;
        }
    }

    public void Check_Verifycode()
    {
        string verifycode = tools.CheckStr(Request["val"]);
        if (verifycode == "")
        {
            Response.Write("<font color=\"#ff0000\">请输入验证码！</font>");
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
                Response.Write("<font color=\"#ff0000\">无效的验证码！</font>");
                return;
            }
        }
    }

    public void Check_MobileVerify()
    {
        string verifycode = tools.CheckStr(Request["val"]);
        if (verifycode == "")
        {
            Response.Write("<font color=\"#ff0000\">请输入短信验证码！</font>");
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
                Response.Write("<font color=\"#ff0000\">无效的短信验证码！</font>");
                return;
            }
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
            Response.Write("<font color=\"#ff0000\">" + error + "</font>");
            return;
        }
        else
        {
            Response.Write("<font color=\"#00a226\">" + success + "</font>");
            return;
        }
    }

    public void Check_Mobile()
    {
        string supplier_mobile = tools.CheckStr(Request["val"]);
        if (supplier_mobile == "")
        {
            Response.Write("<font color=\"#ff0000\">请输入手机号码！</font>");
            return;
        }
        else
        {
            if (pub.Checkmobile(supplier_mobile))
            {
                Response.Write("<font color=\"#00a226\">手机号码输入正确！</font>");
                return;
            }
            else
            {
                Response.Write("<font color=\"#ff0000\">无效的手机号码！</font>");
                return;
            }
        }
    }

    public bool CheckNickname(string strnickname)
    {
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9_\u4e00-\u9fa5]+$");
        return regex.IsMatch(strnickname);
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
                Response.Write("<font color=\"#ff0000\">电话格式错误，请重新输入！</font>");
                return;
            }
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

    #region"注册登录"


    /// <summary>
    /// 检查是否所属账号权限 
    /// </summary>
    /// <param name="BelongsType"></param>
    public void CheckBelongsType(BelongsTypeEnum[] BelongsType)
    {
        if (CheckBelongsTypeBool(BelongsType))
        {
        }
        else
        {
            pub.Msg("info", "信息提示", "您的账号不具有该权限", false, "/supplier/index.aspx");
        }
    }


    /// <summary>
    /// 检查是否所属账号权限 
    /// </summary>
    /// <param name="BelongsType"></param>
    public bool CheckBelongsTypeBool(BelongsTypeEnum[] BelongsType)
    {
        if (BelongsType.Contains((BelongsTypeEnum)tools.NullInt(Session["BelongsType"])))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 自动登录
    /// </summary>
    public void SupplierAutoLogin()
    {
        HttpCookie cookie = Request.Cookies["autologin"];
        //HttpCookie cookie = Request.Cookies["supplier_autologin"];
        if (cookie == null)
            return;

        try
        {
            //string logintype = Server.UrlDecode(cookie.Values["logintype"]);
            string loginname = Server.UrlDecode(cookie.Values["loginname"]);
            string password = Server.UrlDecode(cookie.Values["password"]);

            //if (logintype == null || logintype.Length == 0 || loginname == null || loginname.Length == 0 || password == null || password.Length == 0)
            //{
            //    return;
            //}
            if (loginname == null || loginname.Length == 0 || password == null || password.Length == 0)
            {
                return;
            }

            string Supplier_Email = tools.CheckStr(loginname);
            string Supplier_Password = tools.CheckStr(password);

            #region 登录过程

            SupplierInfo Supplierinfo = MyBLL.SupplierLogin(Supplier_Email, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
            SupplierSubAccountInfo subaccountinfo = null;
            if (Supplierinfo == null)
            {
                //登录子账户
                subaccountinfo = MySubAccount.SubAccountLogin(Supplier_Email);
                if (subaccountinfo != null)
                {
                    Supplierinfo = GetSupplierByID(subaccountinfo.Supplier_SubAccount_SupplierID);
                }
            }
            if (Supplierinfo != null)
            {
                //登录子账户
                if (subaccountinfo != null)
                {
                    if (subaccountinfo.Supplier_SubAccount_Password != Supplier_Password)
                    {
                        return;
                    }

                    //子账户
                    if (subaccountinfo.Supplier_SubAccount_IsActive != 1)
                    {
                        return;
                    }

                    if (subaccountinfo.Supplier_SubAccount_ExpireTime.AddDays(1) < DateTime.Now)
                    {
                        return;
                    }
                }
                else//登录主账户
                {
                    if (Supplierinfo.Supplier_Password != Supplier_Password)
                    {
                        return;
                    }

                    if (Supplierinfo.Supplier_Trash == 1)
                    {
                        return;
                    }
                    if (Supplierinfo.Supplier_Status != 1)
                    {
                        return;
                    }
                }

                if (subaccountinfo != null)
                {
                    Session["supplier_id"] = Supplierinfo.Supplier_ID;
                    Session["supplier_type"] = Supplierinfo.Supplier_Type;
                    Session["supplier_email"] = Supplierinfo.Supplier_Email;
                    Session["supplier_auditstatus"] = Supplierinfo.Supplier_AuditStatus;
                    Session["supplier_companyname"] = Supplierinfo.Supplier_CompanyName;
                    Session["supplier_logined"] = "True";
                    Session["supplier_ishaveshop"] = Supplierinfo.Supplier_IsHaveShop;
                    Session["Supplier_Isapply"] = Supplierinfo.Supplier_IsApply;
                    Session["supplier_lastlogin_time"] = subaccountinfo.Supplier_SubAccount_lastLoginTime;
                    Session["supplier_nickname"] = subaccountinfo.Supplier_SubAccount_Name;
                    Session["account_id"] = subaccountinfo.Supplier_SubAccount_ID;

                    subaccountinfo.Supplier_SubAccount_lastLoginTime = DateTime.Now;
                    MySubAccount.EditSupplierSubAccount(subaccountinfo);
                    AddSubAccountLog("登录", "【" + Supplierinfo.Supplier_Nickname + "】子账户" + subaccountinfo.Supplier_SubAccount_Name + "登录");

                }
                else
                {
                    Session["supplier_id"] = Supplierinfo.Supplier_ID;
                    Session["supplier_type"] = Supplierinfo.Supplier_Type;
                    Session["supplier_email"] = Supplierinfo.Supplier_Email;
                    Session["supplier_companyname"] = Supplierinfo.Supplier_CompanyName;
                    Session["supplier_logined"] = "True";
                    Session["supplier_logincount"] = Supplierinfo.Supplier_LoginCount + 1;
                    Session["supplier_lastlogin_time"] = Supplierinfo.Supplier_Lastlogintime;
                    Session["supplier_ishaveshop"] = Supplierinfo.Supplier_IsHaveShop;
                    Session["Supplier_Isapply"] = Supplierinfo.Supplier_IsApply;
                    Session["supplier_nickname"] = Supplierinfo.Supplier_Nickname;
                    Session["supplier_auditstatus"] = Supplierinfo.Supplier_AuditStatus;

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
                    MyBLL.UpdateSupplierLogin(Supplierinfo.Supplier_ID, Supplierinfo.Supplier_LoginCount + 1, Request.ServerVariables["Remote_Addr"], pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f"));
                }

            }

            #endregion

        }
        catch (Exception ex)
        {

        }
    }

    //商家登录
    public void Supplier_Login()
    {
        int autologin = tools.CheckInt(Request.Form["autologin"]);
        string Supplier_Email = tools.CheckStr(tools.NullStr(Request["Supplier_Email"]).Trim());
        string Supplier_Password = Request["Supplier_Password"];
        Supplier_Password = encrypt.MD5(Supplier_Password);
        string verifycode = tools.CheckStr(tools.NullStr(Request.Form["supplier_verifycode"]).Trim()).ToLower();

        if (Supplier_Email == "")
        {
            Session["logintype"] = "False";
            Response.Redirect("/login.aspx?u_type=1&login=umsg_k");
            Response.End();
        }

        if (verifycode != Session["Trade_Verify"].ToString() || verifycode.Length == 0)
        {
            Session["logintype"] = "False";
            Response.Redirect("/login.aspx?login=vmsg&u_type=1");
            Response.End();
        }


        SupplierInfo Supplierinfo = MyBLL.SupplierLogin(Supplier_Email, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
        SupplierSubAccountInfo subaccountinfo = null;
        if (Supplierinfo == null)
        {
            //登录子账户
            subaccountinfo = MySubAccount.SubAccountLogin(Supplier_Email);
            if (subaccountinfo != null)
            {
                Supplierinfo = GetSupplierByID(subaccountinfo.Supplier_SubAccount_SupplierID);
            }
        }
        if (Supplierinfo != null)
        {
            //登录子账户
            if (subaccountinfo != null)
            {
                if (subaccountinfo.Supplier_SubAccount_Password != Supplier_Password)
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
            else//登录主账户
            {
                if (Supplierinfo.Supplier_Password != Supplier_Password)
                {
                    Session["logintype"] = "False";

                    Response.Redirect("/login.aspx?u_type=1&login=pmsg");
                    Response.End();
                }

                if (Supplierinfo.Supplier_Trash == 1)
                {
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
                HttpCookie cookie = new HttpCookie("supplier_autologin");
                cookie.Expires = DateTime.Today.AddDays(30);
                if (subaccountinfo != null)
                {
                    cookie.Values.Add("logintype", "1");
                }
                else
                {
                    cookie.Values.Add("logintype", "0");
                }
                cookie.Values.Add("loginname", Server.UrlEncode(Supplier_Email));
                cookie.Values.Add("password", Server.UrlEncode(Supplier_Password));
                Response.Cookies.Add(cookie);
            }

            #endregion

            if (subaccountinfo != null)
            {
                Session["supplier_id"] = Supplierinfo.Supplier_ID;
                Session["supplier_type"] = Supplierinfo.Supplier_Type;
                Session["supplier_email"] = Supplierinfo.Supplier_Email;
                Session["supplier_mobile"] = Supplierinfo.Supplier_Mobile;
                Session["supplier_auditstatus"] = Supplierinfo.Supplier_AuditStatus;
                Session["supplier_companyname"] = Supplierinfo.Supplier_CompanyName;
                Session["supplier_sublogined"] = "True";
                Session["supplier_logined"] = "True";
                Session["supplier_ishaveshop"] = Supplierinfo.Supplier_IsHaveShop;
                Session["Supplier_Isapply"] = Supplierinfo.Supplier_IsApply;
                Session["supplier_lastlogin_time"] = subaccountinfo.Supplier_SubAccount_lastLoginTime;
                Session["supplier_nickname"] = subaccountinfo.Supplier_SubAccount_Name;
                Session["account_id"] = subaccountinfo.Supplier_SubAccount_ID;
                Session["subPrivilege"] = subaccountinfo.Supplier_SubAccount_Privilege;

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

                subaccountinfo.Supplier_SubAccount_lastLoginTime = DateTime.Now;
                MySubAccount.EditSupplierSubAccount(subaccountinfo);
                AddSubAccountLog("登录", "【" + Supplierinfo.Supplier_Nickname + "】子账户" + subaccountinfo.Supplier_SubAccount_Name + "登录");

            }
            else
            {
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
                MyBLL.UpdateSupplierLogin(Supplierinfo.Supplier_ID, Supplierinfo.Supplier_LoginCount + 1, Request.ServerVariables["Remote_Addr"], pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f"));
            }

            //更新购物车价格
            Cart_Price_Update();

            //更新会员等级
            Update_SupplierGrade();

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
                Session["error_count"] = 0;
                Response.Redirect("/supplier/index.aspx");
            }
        }
        else
        {
            Session["logintype"] = "False";
            Response.Redirect("/login.aspx?u_type=1&login=err_active");
            Response.End();
        }
    }

    public void Supplier_FastLogin()
    {
        string Supplier_Email = tools.CheckStr(tools.NullStr(Request["Supplier_Email"]).Trim());
        string Supplier_Password = Request["Supplier_Password"];
        Supplier_Password = encrypt.MD5(Supplier_Password);

        SupplierInfo Supplierinfo = MyBLL.SupplierLogin(Supplier_Email, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
        SupplierSubAccountInfo subaccountinfo = null;
        if (Supplierinfo == null)
        {
            //登录子账户
            subaccountinfo = MySubAccount.SubAccountLogin(Supplier_Email);
            if (subaccountinfo != null)
            {
                Supplierinfo = GetSupplierByID(subaccountinfo.Supplier_SubAccount_SupplierID);
            }
        }
        if (Supplierinfo != null)
        {
            //登录子账户
            if (subaccountinfo != null)
            {
                if (subaccountinfo.Supplier_SubAccount_Password != Supplier_Password)
                {
                    Session["logintype"] = "False";
                    Response.Write("Failure");
                    Response.End();
                }

                //子账户
                if (subaccountinfo.Supplier_SubAccount_IsActive != 1)
                {
                    //Response.Redirect("/login.aspx?u_type=1&login=err_active_sub");
                    Response.Write("Failure");
                    Response.End();
                }
                if (subaccountinfo.Supplier_SubAccount_ExpireTime.AddDays(1) < DateTime.Now)
                {
                    //Response.Redirect("/login.aspx?u_type=1&login=err_time_sub");
                    Response.Write("Failure");
                    Response.End();
                }
            }
            else//登录主账户
            {
                if (Supplierinfo.Supplier_Password != Supplier_Password)
                {
                    Session["logintype"] = "False";
                    //Response.Redirect("/login.aspx?u_type=1&login=pmsg");
                    Response.Write("Failure");
                    Response.End();
                }

                if (Supplierinfo.Supplier_Trash == 1)
                {
                    //Response.Redirect("/login.aspx?u_type=1&login=err_check");
                    Response.Write("Failure");
                    Response.End();
                }
                if (Supplierinfo.Supplier_Status != 1)
                {
                    //Response.Redirect("/login.aspx?u_type=1&login=err_active");
                    Response.Write("Failure");
                    Response.End();
                }
            }

            if (subaccountinfo != null)
            {
                Session["supplier_id"] = Supplierinfo.Supplier_ID;
                Session["supplier_type"] = Supplierinfo.Supplier_Type;
                Session["supplier_email"] = Supplierinfo.Supplier_Email;
                Session["supplier_auditstatus"] = Supplierinfo.Supplier_AuditStatus;
                Session["supplier_companyname"] = Supplierinfo.Supplier_CompanyName;
                Session["supplier_logined"] = "True";
                Session["supplier_ishaveshop"] = Supplierinfo.Supplier_IsHaveShop;
                Session["Supplier_Isapply"] = Supplierinfo.Supplier_IsApply;
                Session["supplier_lastlogin_time"] = subaccountinfo.Supplier_SubAccount_lastLoginTime;
                Session["supplier_nickname"] = subaccountinfo.Supplier_SubAccount_Name;
                Session["account_id"] = subaccountinfo.Supplier_SubAccount_ID;
                Session["subPrivilege"] = subaccountinfo.Supplier_SubAccount_Privilege;

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

                subaccountinfo.Supplier_SubAccount_lastLoginTime = DateTime.Now;
                MySubAccount.EditSupplierSubAccount(subaccountinfo);
                AddSubAccountLog("登录", "【" + Supplierinfo.Supplier_Nickname + "】子账户" + subaccountinfo.Supplier_SubAccount_Name + "登录");

            }
            else
            {
                Session["supplier_id"] = Supplierinfo.Supplier_ID;
                Session["supplier_type"] = Supplierinfo.Supplier_Type;
                Session["supplier_email"] = Supplierinfo.Supplier_Email;
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
                MyBLL.UpdateSupplierLogin(Supplierinfo.Supplier_ID, Supplierinfo.Supplier_LoginCount + 1, Request.ServerVariables["Remote_Addr"], pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f"));
            }


            //更新会员等级
            Update_SupplierGrade();

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
                Session["error_count"] = 0;
                Response.Write("/supplier/index.aspx");
            }
        }
        else
        {
            Session["logintype"] = "False";
            //Response.Redirect("/login.aspx?u_type=1&login=err_active");
            Response.Write("Failure");
            Response.End();
        }
    }

    public void Update_SupplierGrade()
    {

        SupplierInfo supplierinfo = GetSupplierByID();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierGradeInfo.Supplier_Grade_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("SupplierGradeInfo.Supplier_Grade_RequiredCoin", "desc"));
        IList<SupplierGradeInfo> grades = MySupplierGrade.GetSupplierGrades(Query, pub.CreateUserPrivilege("1d3f7ace-2191-4c5e-9403-840ddaf191c0"));
        if (grades != null)
        {
            foreach (SupplierGradeInfo grade in grades)
            {

                if (supplierinfo != null)
                {
                    if (supplierinfo.Supplier_CoinCount >= grade.Supplier_Grade_RequiredCoin)
                    {
                        supplierinfo.Supplier_GradeID = grade.Supplier_Grade_ID;
                        Session["member_grade"] = supplierinfo.Supplier_GradeID;
                        MyBLL.EditSupplier(supplierinfo, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f"));
                        break;
                    }
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
                        Product_Price = pub.Get_Member_Price(productinfo.Product_ID, productinfo.Product_Price);
                        normal_amount = MyCart.Get_Orders_Goods_TypeAmount(Session.SessionID, productinfo.Product_ID, 0);


                        Product_Coin = pub.Get_Member_Coin(Product_Price);
                        //检查是否赠送指定积分
                        if (productinfo.Product_IsGiftCoin == 1)
                        {
                            Product_Coin = (int)(Product_Price * productinfo.Product_Gift_Coin);
                        }

                        entity.Orders_Goods_BuyerID = tools.CheckInt(Session["supplier_id"].ToString());
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
                        Product_Coin = pub.Get_Member_Coin(Product_Price);

                        entity.Orders_Goods_BuyerID = tools.CheckInt(Session["supplier_id"].ToString());
                        entity.Orders_Goods_Product_Coin = Product_Coin;
                        entity.Orders_Goods_Product_Price = Product_Price;
                        MyCart.EditOrdersGoodsTmp(entity);
                        productinfo = null;
                    }
                }
            }
        }
    }

    private bool AddSubAccountLog(string subAccount_action, string subAccount_note)
    {
        SupplierSubAccountLogInfo loginfo = new SupplierSubAccountLogInfo();

        loginfo.Log_SubAccount_ID = tools.NullInt(Session["account_id"]);
        loginfo.Log_Supplier_ID = tools.NullInt(Session["supplier_id"]);
        loginfo.Log_SubAccount_Note = subAccount_note;
        loginfo.Log_SubAccount_Action = subAccount_action;
        loginfo.Log_Addtime = DateTime.Now;


        return MySubAccountLog.AddSupplierSubAccountLog(loginfo);
    }

    //商家登录检查
    public void Supplier_Login_Check(string url_after_login)
    {
        if (Session["supplier_logined"].ToString() != "True")
        {
            Session["url_after_login"] = url_after_login;
            Response.Redirect("/login.aspx?u_type=1");
        }

        if (tools.NullInt(Session["supplier_auditstatus"]) == 0)
        {
            pub.Msg("info", "信息提示", "您的资料尚未通过平台审核，暂时无法使用此功能！", false, " /supplier/Supplier_Cert.aspx");
        }
        else if (tools.NullInt(Session["supplier_auditstatus"]) == -1)
        {
            pub.Msg("info", "信息提示", "您的资料尚未通过平台审核，暂时无法使用此功能！", false, "/supplier/Supplier_Cert.aspx");
        }
    }


    //商家登录检查
    public void Supplier_AuditLogin_Check(string url_after_login)
    {
        if (Session["supplier_logined"].ToString() != "True")
        {
            Session["url_after_login"] = url_after_login;
            Response.Redirect("/login.aspx?u_type=1");
        }

        if (tools.NullInt(Session["supplier_auditstatus"]) == 0)
        {
            //pub.Msg("info", "信息提示", "您的资料尚未通过平台审核，暂时无法使用此功能！", false, "/supplier/account_profile.aspx");

            Response.Redirect("/supplier/account_profile.aspx");
        }
        else if (tools.NullInt(Session["supplier_auditstatus"]) == -1)
        {
            //pub.Msg("info", "信息提示", "您的资料尚未通过平台审核，暂时无法使用此功能！", false, "/supplier/account_profile.aspx");

            Response.Redirect("/supplier/account_profile.aspx");
        }
    }
    ////商家登录检查
    //public void Supplier_AuditLogin_Check(string url_after_login)
    //{
    //    if (Session["supplier_logined"].ToString() != "True")
    //    {
    //        Session["url_after_login"] = url_after_login;
    //        Response.Write("/login.aspx");
    //    }

    //    if (tools.NullInt(Session["supplier_auditstatus"]) == 0)
    //    {
    //        //pub.Msg("info", "信息提示", "您的资料尚未通过平台审核，暂时无法使用此功能！", false, "/supplier/account_profile.aspx");

    //        //Response.Redirect("/supplier/account_profile.aspx");
    //        Response.Write("成功");
    //    }
    //    else if (tools.NullInt(Session["supplier_auditstatus"]) == -1)
    //    {
    //        //pub.Msg("info", "信息提示", "您的资料尚未通过平台审核，暂时无法使用此功能！", false, "/supplier/account_profile.aspx");

    //        //Response.Redirect("/supplier/account_profile.aspx");
    //        Response.Write("成功");
    //    }
    //    else
    //    {
    //        //Response.Redirect("/member/index.aspx");
    //        Response.Write("成功");
    //    }
    //}

    //商家退出
    public void Supplier_LogOut()
    {
        Session.Abandon();
        Session["supplier_logined"] = "False";
        Response.Cookies["supplier_autologin"].Expires = DateTime.Now.AddDays(-1);
        Response.Redirect("/login.aspx");
    }



    //找回密码邮件验证
    public void supplier_getpass_verify()
    {
        string supplier_verifycode = "";
        supplier_verifycode = tools.CheckStr(Request["VerifyCode"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_VerifyCode", "=", supplier_verifycode));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("SupplierInfo.Supplier_ID", "Desc"));
        IList<SupplierInfo> supplierinfo = MyBLL.GetSuppliers(Query, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
        if (supplierinfo != null)
        {
            foreach (SupplierInfo entity in supplierinfo)
            {
                Session["getpass_verify"] = "true";
                Session["getpass_supplier_id"] = entity.Supplier_ID;
                Session["getpass_supplier_mail"] = entity.Supplier_Email;
            }
            Response.Redirect("/supplier/getpassword_reset.aspx");
        }
        else
        {
            Session["getpass_verify"] = "false";
            Session["getpass_supplier_id"] = 0;
            Session["getpass_supplier_mail"] = "";
            Response.Redirect("/supplier/getpassword_verify_failed.aspx");
        }

    }

    //找回密码重新设置密码
    public void supplier_getpass_resetpass()
    {
        string supplier_id, supplier_email, supplier_password, supplier_password_confirm, verifycode, supplier_verifycode;
        if (Session["getpass_verify"] == "true")
        {
            supplier_id = Session["getpass_supplier_id"].ToString();
            supplier_email = Session["getpass_supplier_mail"].ToString();
            supplier_password = tools.CheckStr(tools.NullStr(Request.Form["supplier_password"]).Trim());
            supplier_password_confirm = tools.CheckStr(tools.NullStr(Request.Form["supplier_password_confirm"]).Trim());

            verifycode = tools.CheckStr(Request["verifycode"]);

            if (verifycode != Session["Trade_Verify"].ToString() && verifycode.Length == 0)
            {
                pub.Msg("error", "验证码输入错误", "验证码输入错误", false, "{back}");
            }

            if (CheckSsn(supplier_password) == false)
            {
                pub.Msg("error", "密码包含特殊字符", "密码包含特殊字符，只接受A-Z，a-z，0-9，不要输入空格", false, "{back}");
            }
            else
            {
                if (supplier_password.Length < 6 || supplier_password.Length > 20)
                {
                    pub.Msg("error", "请输入6～20位密码", "请输入6～20位密码", false, "{back}");
                }
            }

            if (supplier_password_confirm != supplier_password)
            {
                pub.Msg("error", "两次密码输入不一致", "两次密码输入不一致，请重新输入", false, "{back}");
            }

            SupplierInfo supplierinfo = MyBLL.GetSupplierByID(tools.CheckInt(supplier_id), pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
            if (supplierinfo != null)
            {
                supplier_verifycode = pub.Createvkey();
                supplierinfo.Supplier_VerifyCode = supplier_verifycode;
                supplierinfo.Supplier_Password = encrypt.MD5(supplier_password);
                MyBLL.EditSupplier(supplierinfo, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f"));
                //发送验证邮件
                string mailsubject, mailbodytitle, mailbody;
                mailsubject = "密码已重新设置";
                mailbodytitle = "密码已重新设置";
                mailbody = mail_template("getpass_success", "", "", supplier_verifycode);
                pub.Sendmail(supplier_email, mailsubject, mailbodytitle, mailbody);
                Response.Redirect("/supplier/getpassword_reset_success.aspx");

            }
            else
            {
                //跳转
                Response.Redirect("/supplier/getpassword.aspx");
            }
        }
        else
        {
            //跳转
            Response.Redirect("/supplier/getpassword.aspx");
        }
    }

    //发送验证邮件
    public int supplier_register_sendemailverify(string supplier_email, string supplier_verifycode)
    {
        //发送注册邮件
        string mailsubject, mailbodytitle, mailbody;
        mailsubject = "赶快验证，马上享受{sys_config_site_name}会员服务！";
        mailsubject = replace_sys_config(mailsubject);
        mailbodytitle = "赶快验证，马上享受{sys_config_site_name}会员服务！";
        mailbodytitle = replace_sys_config(mailbodytitle);
        mailbody = mail_template("emailverify", "", "", supplier_verifycode);
        return pub.Sendmail(supplier_email, mailsubject, mailbodytitle, mailbody);
    }

    //重新发送验证邮件
    public void supplier_register_resendemailverify()
    {
        if (Session["supplier_logined"].ToString() != "True")
        {
            Session["url_after_login"] = "/supplier/emailverify.aspx";
            Response.Redirect("/login.aspx?u_type=1");
        }
        else
        {
            SupplierInfo supplierinfo = MyBLL.GetSupplierByID(tools.CheckInt(Session["supplier_id"].ToString()), pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
            if (supplierinfo != null)
            {
                supplier_register_sendemailverify(supplierinfo.Supplier_Email, supplierinfo.Supplier_VerifyCode);
                Response.Redirect("/supplier/emailverify.aspx");
            }
            else
            {
                Session["url_after_login"] = "/supplier/emailverify.aspx";
                Response.Redirect("/login.aspx?u_type=1");
            }
        }
    }

    //更改注册Email
    public void supplier_register_modifyemail()
    {
        string supplier_email = "";
        string supplier_verifycode = "";
        supplier_email = tools.CheckStr(Request["supplier_email"]);
        if (tools.CheckEmail(supplier_email) == false)
        {
            pub.Msg("error", "邮件地址无效", "请输入有效的邮件地址", false, "{back}");

        }
        else
        {
            if (Check_Supplier_Email(supplier_email))
            {
                pub.Msg("error", "该邮件地址已被使用", "该邮件地址已被使用。请使用另外一个邮件地址进行注册", false, "{back}");

            }
        }

        //更新用户邮件
        supplier_verifycode = pub.Createvkey();
        SupplierInfo supplierinfo = MyBLL.GetSupplierByID(tools.CheckInt(Session["supplier_id"].ToString()), pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
        if (supplierinfo != null && supplierinfo.Supplier_Emailverify == 0)
        {
            supplierinfo.Supplier_VerifyCode = supplier_verifycode;
            supplierinfo.Supplier_Email = supplier_email;
            supplierinfo.Supplier_Emailverify = 0;
            MyBLL.EditSupplier(supplierinfo, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f"));
        }


        //发送验证邮件
        supplier_register_sendemailverify(supplier_email, supplier_verifycode);

        //置Session和Cookies
        Session["supplier_email"] = supplier_email;
        Response.Cookies["supplier_email"].Expires = DateTime.Now.AddDays(365);
        Response.Cookies["supplier_email"].Value = supplier_email;

        //转到邮箱验证页面
        Response.Redirect("/supplier/emailverify.aspx");
    }

    //验证邮件
    public void supplier_register_emailverify()
    {
        string supplier_verifycode = "";
        string supplier_email = "";
        supplier_verifycode = tools.CheckStr(Request["VerifyCode"]);
        string emailverify_result = "false";

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_VerifyCode", "=", supplier_verifycode));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_Trash", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("SupplierInfo.Supplier_ID", "Desc"));
        IList<SupplierInfo> supplierinfo = MyBLL.GetSuppliers(Query, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
        if (supplierinfo != null)
        {
            foreach (SupplierInfo entity in supplierinfo)
            {


                supplier_email = entity.Supplier_Email;
                supplier_verifycode = pub.Createvkey();
                entity.Supplier_VerifyCode = supplier_verifycode;
                entity.Supplier_Emailverify = 1;
                if (MyBLL.EditSupplier(entity, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f")))
                {
                    Session["supplier_emailverify"] = entity.Supplier_Emailverify;
                    emailverify_result = "true";
                    supplier_register_sendemailverifysuccess(supplier_email, supplier_verifycode);
                    Session["supplier_email"] = supplier_email;
                    Response.Cookies["supplier_email"].Expires = DateTime.Now.AddDays(365);
                    Response.Cookies["supplier_email"].Value = supplier_email;
                }
            }
        }
        Response.Redirect("/supplier/emailverify_result.aspx?result=" + emailverify_result);

    }

    //发送注册成功邮件
    public int supplier_register_sendemailverifysuccess(string supplier_email, string supplier_verifycode)
    {
        //发送注册邮件
        string mailsubject = "";
        string mailbodytitle = "";
        string mailbody = "";
        mailsubject = "验证成功，欢迎使用{sys_config_site_name}！";
        mailsubject = replace_sys_config(mailsubject);
        mailbodytitle = "验证成功，欢迎使用{sys_config_site_name}！";
        mailbodytitle = replace_sys_config(mailbodytitle);
        mailbody = mail_template("emailverify_success", "", "", supplier_verifycode);
        return pub.Sendmail(supplier_email, mailsubject, mailbodytitle, mailbody);
    }

    #endregion

    #region "商家信息"

    #region 页面左侧导航

    #region 交易管理

    /// <summary>
    /// 交易管理DIV
    /// </summary>
    /// <param name="main"></param>
    /// <param name="sub"></param>
    /// <returns></returns>
    private string Transaction_Management(int main, int sub, StringBuilder sb)
    {
        sb.Append("                 <div class=\"b07_info\">");
        sb.Append("                       <h3><span onclick=\"openShutManager(this,'box1')\" ><a id=\"1\"  onClick=\"switchTag(1);\">交易管理</a></span></h3>");
        sb.Append("                       <div class=\"b07_info_main\" id=\"box1\">");
        sb.Append("<ul>");
        sb.Append("<li " + (main == 1 && sub == 1 ? " class=\"on\"" : "") + "><a href=\"/supplier/order_list.aspx\">我的订单</a></li>");
        sb.Append("<li " + (main == 1 && sub == 2 ? " class=\"on\"" : "") + "><a href=\"/supplier/order_delivery_list.aspx\">我的发货单</a></li>");
        sb.Append("<li " + (main == 1 && sub == 3 ? " class=\"on\"" : "") + "><a href=\"/supplier/customer_list.aspx\">客户数据</a></li>");
        sb.Append("<li " + (main == 1 && sub == 4 ? " class=\"on\"" : "") + "><a href=\"datastatistics.aspx\">数据统计</a></li>");
        sb.Append("<li " + (main == 1 && sub == 5 ? " class=\"on\"" : "") + "><a href=\"/valuate/supplier_shopevaluate.aspx\">我的供应商评价</a></li>");
        sb.Append("<li " + (main == 1 && sub == 6 ? " class=\"on\"" : "") + "><a href=\"/valuate/supplier_productvaluate.aspx\">我的商品评价</a></li>");
        sb.Append("</ul>");
        sb.Append("</div>");
        sb.Append("                 </div>                                                                         ");
        return tools.NullStr(sb.ToString());
    }

    #endregion

    #region  财务管理

    /// <summary>
    /// 财务管理DIV
    /// </summary>
    /// <returns></returns>
    private string Financial_Management(int main, int sub, StringBuilder sb)
    {
        sb.Append("                 <div class=\"b07_info\">");
        sb.Append("                       <h3><span onclick=\"openShutManager(this,'box2')\" ><a id=\"2\"  onClick=\"switchTag(2);\">财务管理</a></span></h3>");
        sb.Append("                       <div class=\"b07_info_main\" id=\"box2\">");
        sb.Append("<ul style=\"border-bottom: none;\" id=\"box2\">");
        sb.Append("<li " + (main == 7 && sub == 1 ? " class=\"on\"" : "") + "><a href=\"/supplier/ZhongXin.aspx\">中信支付管理</a></li>");
        sb.Append("<li " + (main == 7 && sub == 2 ? " class=\"on\"" : "") + "><a href=\"/supplier/zhongxin_withdraw.aspx\">中信申请出金</a></li>");
        sb.Append("<li " + (main == 7 && sub == 3 ? " class=\"on\"" : "") + "><a href=\"/supplier/zhongxin_detail.aspx\">中信交易流水查询</a></li>");
        sb.Append("</ul>");
        sb.Append("</div>");
        sb.Append("</div>");
        return tools.NullStr(sb.ToString());
    }

    #endregion

    #region 店铺管理

    /// <summary>
    /// 店铺管理
    /// </summary>
    /// <param name="main"></param>
    /// <param name="sub"></param>
    /// <returns></returns>
    private string Shop_Management(int main, int sub, string Shop_Domain, int SupplierPurchasereplyNum, int SupplierShoppingAskNum, StringBuilder sb)
    {
        sb.Append("                 <div class=\"b07_info\">");
        sb.Append("                       <h3><span onclick=\"openShutManager(this,'box3')\" ><a id=\"3\"  onClick=\"switchTag(3);\">店铺管理</a></span></h3>");
        sb.Append("                       <div class=\"b07_info_main\" id=\"box3\">");
        sb.Append("<ul id=\"box3\">");
        if (Session["supplier_ishaveshop"].ToString() == "0")
        {
            sb.Append("<li " + (main == 2 && sub == 1 ? " class=\"on\"" : "") + "><a href=\"/supplier/Supplier_Shop_Apply.aspx\">开通店铺</a></li>");
        }
        else
        {
            sb.Append("<li " + (main == 2 && sub == 22 ? " class=\"on\"" : "") + "><a href=\"" + Shop_Domain + "\" target=\"blank\">进入店铺首页</a></li>");
            sb.Append("<li " + (main == 2 && sub == 2 ? " class=\"on\"" : "") + "><a href=\"/supplier/Supplier_Shop_Model.aspx\">模版管理</a></li>");
            sb.Append("<li " + (main == 2 && sub == 3 ? " class=\"on\"" : "") + " ><a href=\"/supplier/Supplier_Shop_Config.aspx\">店铺基础设置</a>");
            sb.Append("</li>");
            //sb.Append("<ul>");
            //
            sb.Append("<li" + (main == 2 && sub == 14 ? " class=\"on\"" : "") + "><a href=\"/supplier/account_profile_supplierIntro.aspx \" >公司介绍</a></li>");
            sb.Append("<li " + (main == 2 && sub == 15 ? " class=\"on\"" : "") + "><a href=\"/supplier/account_profile_Contract.aspx \" >联系我们</a></li>");

            //sb.Append("</ul>");


            sb.Append("<li " + (main == 2 && sub == 10 ? " class=\"on\"" : "") + "><a href=\"/supplier/Supplier_CloseShop_Apply_Add.aspx\">店铺关闭申请</a></li>");
            sb.Append("<li " + (main == 2 && sub == 4 ? " class=\"on\"" : "") + "><a href=\"/supplier/Supplier_Shop_Pages_List.aspx\">栏目管理</a></li>");
            sb.Append("<li " + (main == 2 && sub == 5 ? " class=\"on\"" : "") + "><a href=\"/supplier/Supplier_Shop_CustomModule.aspx\">自定义模块</a></li>");
            sb.Append("<li " + (main == 2 && sub == 6 ? " class=\"on\"" : "") + "><a href=\"/supplier/tag.aspx\">商品标签管理</a></li>");
            sb.Append("<li " + (main == 2 && sub == 7 ? " class=\"on\"" : "") + "><a href=\"/supplier/Supplier_Shop_Article_List.aspx\">活动公告管理</a></li>");
            //sb.Append("<li " + (main == 2 && sub == 8 ? " class=\"on\"" : "") + "><a href=\"/supplier/Supplier_MerchantsList.aspx\">招商加盟</a></li>");
            sb.Append("<li " + (main == 2 && sub == 9 ? " class=\"on\"" : "") + "><a href=\"/supplier/PurchaseReply_list.aspx\">报价留言");
            sb.Append(" <span style=\"color:#ff6600\">(" + SupplierPurchasereplyNum + ")  </span>条");
            sb.Append("    </a></li>");



            sb.Append("<li " + (main == 2 && sub == 11 ? " class=\"on\"" : "") + "><a href=\"/supplier/supplier_shop_shoppingask.aspx\">咨询管理");
            sb.Append(" <span style=\"color:#ff6600\">(" + SupplierShoppingAskNum + ")  </span>条");
            sb.Append("    </a></li>");

            sb.Append("<li " + (main == 2 && sub == 12 ? " class=\"on\"" : "") + "><a href=\"/supplier/Supplier_Shop_Online_Add.aspx\">在线客服</a></li>");
        }

        sb.Append("</ul>");
        sb.Append("</div>");
        sb.Append("</div>");
        return tools.NullStr(sb.ToString());
    }

    #endregion

    #region 商品管理

    /// <summary>
    /// 商品管理
    /// </summary>
    /// <param name="main"></param>
    /// <param name="sub"></param>
    /// <returns></returns>
    private string Commodity_Management(int main, int sub, StringBuilder sb)
    {

        if (Session["supplier_ishaveshop"].ToString() == "1")
        {
            sb.Append("                 <div class=\"b07_info\">");
            sb.Append("                       <h3><span onclick=\"openShutManager(this,'box4')\" ><a id=\"4\"  onClick=\"switchTag(4);\">商品管理</a></span></h3>");
            sb.Append("                       <div class=\"b07_info_main\" id=\"box4\">");
            sb.Append("<ul id=\"box4\">");
            sb.Append("<li " + (main == 3 && sub == 1 ? " class=\"on\"" : "") + "><a href=\"/supplier/product_add.aspx\">发布商品</a></li>");
            sb.Append("<li " + (main == 3 && sub == 2 ? " class=\"on\"" : "") + "><a href=\"/supplier/product_list.aspx\">管理商品</a></li>");
            sb.Append("<li " + (main == 3 && sub == 3 ? " class=\"on\"" : "") + "><a href=\"/supplier/product_seo_list.aspx\">商品SEO管理</a></li>");
            sb.Append("<li " + (main == 3 && sub == 4 ? " class=\"on\"" : "") + "><a href=\"/supplier/product_category_list.aspx\">店内分类管理</a></li>");
            sb.Append("</ul>");
            sb.Append("</div>");
            sb.Append("</div>");
        }
        return tools.NullStr(sb.ToString());
    }

    #endregion

    #region 招标管理

    /// <summary>
    /// 招标管理
    /// </summary>
    /// <param name="main"></param>
    /// <param name="sub"></param>
    /// <returns></returns>
    private string Tender_Management(int main, int sub, StringBuilder sb)
    {
        sb.Append("                 <div class=\"b07_info\">");
        sb.Append("                       <h3><span onclick=\"openShutManager(this,'box5')\" ><a id=\"5\"  onClick=\"switchTag(5);\">招标拍卖</a></span></h3>");
        sb.Append("                       <div class=\"b07_info_main\" id=\"box5\">");
        sb.Append("<ul style=\"border-bottom: none;\" id=\"box5\">");
        sb.Append("<li " + (main == 5 && sub == 4 ? " class=\"on\"" : "") + "><a href=\"/supplier/auction_add.aspx\">发布拍卖</a></li>");
        sb.Append("                                  <li " + (main == 3 && sub == 2 ? " class=\"on\"" : "") + "><a href=\"/member/bid_add.aspx\">发布招标</a></li>          ");
        sb.Append("<li " + (main == 5 && sub == 3 ? " class=\"on\"" : "") + "><a href=\"/supplier/auction_list.aspx\">拍卖列表</a></li>");
        sb.Append("                                  <li " + (main == 0 && sub == 0 ? " class=\"on\"" : "") + "><a href=\"/member/bid_list.aspx\">招标列表</a></li>          ");
        sb.Append("<li " + (main == 5 && sub == 2 ? " class=\"on\"" : "") + "><a href=\"/supplier/tender_sign.aspx\">招标管理</a></li>");
        sb.Append("                                  <li " + (main == 3 && sub == 5 ? " class=\"on\"" : "") + "><a href=\"/member/auction_tender_sign.aspx?TenderID=0\">拍卖管理</a></li>            ");

        sb.Append("</ul>");
        sb.Append("</div>");
        sb.Append("</div>");

        return tools.NullStr(sb.ToString());
    }

    #endregion

    #region 拍卖管理
    /// <summary>
    /// 拍卖管理
    /// </summary>
    /// <param name="main"></param>
    /// <param name="sub"></param>
    /// <returns></returns>
    private string Auction_Management(int main, int sub, StringBuilder sb)
    {
        sb.Append("                 <div class=\"b07_info\">");
        sb.Append("                       <h3><span onclick=\"openShutManager(this,'box6')\" ><a id=\"6\"  onClick=\"switchTag(6);\">拍卖管理</a></span></h3>");
        sb.Append("                       <div class=\"b07_info_main\" id=\"box6\">");
        sb.Append("<ul style=\"border-bottom: none;\" id=\"box6\">");
        sb.Append("<li " + (main == 5 && sub == 4 ? " class=\"on\"" : "") + "><a href=\"/supplier/auction_add.aspx\">发布拍卖</a></li>");
        sb.Append("<li " + (main == 5 && sub == 3 ? " class=\"on\"" : "") + "><a href=\"/supplier/auction_list.aspx\">拍卖列表</a></li>");
        sb.Append("                                  <li " + (main == 3 && sub == 5 ? " class=\"on\"" : "") + "><a href=\"/member/auction_list.aspx\">拍卖管理</a></li>            ");
        sb.Append("</ul>");
        sb.Append("</div>");
        sb.Append("</div>");

        return tools.NullStr(sb.ToString());
    }
    #endregion

    #region 账户管理

    /// <summary>
    /// 账户管理
    /// </summary>
    /// <param name="main"></param>
    /// <param name="sub"></param>
    /// <param name="supplierinfo"></param>
    /// <param name="SupplierMessageNum"></param>
    /// <returns></returns>
    private string Account_Management(int main, int sub, SupplierInfo supplierinfo, int SupplierMessageNum, StringBuilder sb)
    {


        sb.Append("                 <div class=\"b07_info\">");
        sb.Append("                       <h3><span onclick=\"openShutManager(this,'box7')\" ><a id=\"7\"  onClick=\"switchTag(7);\">账户管理</a></span></h3>");
        sb.Append("                       <div class=\"b07_info_main\" id=\"box7\">");
        sb.Append("<ul style=\"border-bottom: none;\" id=\"box7\">");


        if (Session["supplier_ishaveshop"].ToString() == "1")
        {
            sb.Append("<li " + (main == 4 && sub == 1 ? " class=\"on\"" : "") + "><a href=\"/supplier/subAccount.aspx\">创建子账户</a></li>");
            sb.Append("<li " + (main == 4 && sub == 2 ? " class=\"on\"" : "") + "><a href=\"/supplier/subAccount_list.aspx\">管理子账户</a></li>");
        }



        sb.Append("<li " + (main == 4 && sub == 3 ? " class=\"on\"" : "") + "><a href=\"/supplier/Supplier_Security_Account_list.aspx\">保证金管理</a></li>");
        if (supplierinfo.Supplier_MerchantMar_Status == 0)
        {
            sb.Append("<li " + (main == 4 && sub == 7 ? " class=\"on\"" : "") + "><a href=\"/supplier/supplier_margin_account_Recharge.aspx\">充值保证金</a></li>");
        }



        sb.Append("<li " + (main == 4 && sub == 4 ? " class=\"on\"" : "") + "><a href=\"/supplier/supplier_sysmessage.aspx\">消息通知");


        sb.Append(" <span style=\"color:#ff6600\">&nbsp(" + SupplierMessageNum + ")  </span>条未读");
        sb.Append("    </a></li>");
        sb.Append("<li " + (main == 4 && sub == 6 ? " class=\"on\"" : "") + "><a href=\"/supplier/account_profile.aspx\">资料管理</a></li>");
        sb.Append("</ul>");
        sb.Append("</div>");
        sb.Append("</div>");
        return tools.NullStr(sb.ToString());
    }

    #endregion

    #region 物流管理

    /// <summary>
    /// 物流管理
    /// </summary>
    /// <param name="main"></param>
    /// <param name="sub"></param>
    /// <param name="supplierinfo"></param>
    /// <param name="SupplierMessageNum"></param>
    /// <returns></returns>
    private string Logistics_Management(int main, int sub, StringBuilder sb)
    {

        sb.Append("                 <div class=\"b07_info\">");

        sb.Append("                       <h3><span onclick=\"openShutManager(this,'box8')\" ><a id=\"8\"  onClick=\"switchTag(8);\">物流管理</a></span></h3>");
        sb.Append("                       <div class=\"b07_info_main\" id=\"box8\">");
        sb.Append("<ul style=\"border-bottom: none;\" id=\"box6\">");

        sb.Append("<li " + (main == 8 && sub == 1 ? " class=\"on\"" : "") + "><a href=\"/supplier/Logistics_list.aspx\">物流列表</a></li>");

        sb.Append("</ul>");
        sb.Append("</div>");
        sb.Append("</div>");
        return tools.NullStr(sb.ToString());
    }

    #endregion

    #region 页面左侧导航

    /// <summary>
    /// 页面左侧导航
    /// </summary>
    /// <param name="main"></param>
    /// <param name="sub"></param>
    public void Get_Supplier_Left_HTML(int main, int sub)
    {
        int Supplier_IsHaveShop = 0;
        StringBuilder sb = new StringBuilder();
        string s = tools.NullStr(Session["subPrivilege"]);
        int SupplierMessageNum = messageclass.GetMessageNum(2);
        int SupplierPurchasereplyNum = Supplier_PurchaseReply_Num();
        int SupplierShoppingAskNum = GetShoppingAskNum(15);
        SupplierInfo supplierinfo = GetSupplierByID();
        string Shop_Domain = "";


        if (supplierinfo != null)
        {
            Supplier_IsHaveShop = supplierinfo.Supplier_IsHaveShop;
            SupplierShopInfo shopinfos = MyShop.GetSupplierShopBySupplierID(supplierinfo.Supplier_ID);
            if (shopinfos != null)
            {

                Shop_Domain = pub.GetShopDomain(shopinfos.Shop_Domain);
            }
        }

        if (s == "" && (!(Session["IsSubPrivilege_Logined"] == "True")))
        {
            if (tools.NullInt(Session["supplier_auditstatus"]) == 1)
            {
                sb.Append("<div class=\"menu_1\">");
                sb.Append("                 <h2>我是卖家</h2>");
                sb.Append("                 <div class=\"b07_info\">");

                //添加交易管理DIV
                Transaction_Management(main, sub, sb);

                //添加财务管理DIV
                Financial_Management(main, sub, sb);

                //添加店铺管理DIV
                Shop_Management(main, sub, Shop_Domain, SupplierPurchasereplyNum, SupplierShoppingAskNum, sb);

                //添加商品管理DIV
                Commodity_Management(main, sub, sb);

                //添加招标管理DIV
                Tender_Management(main, sub, sb);

                //添加拍卖管理DIV
               // Auction_Management(main, sub, sb);

                //添加账户管理DIV
                Account_Management(main, sub, supplierinfo, SupplierMessageNum, sb);

                //添加物流管理DIV
                Logistics_Management(main, sub, sb);

                sb.Append("</div>");
            }
            else
            {
                sb.Append("<div class=\"menu_1\">");
                sb.Append("                 <h2>我是卖家</h2>");
                sb.Append("                 <div class=\"b07_info\">");
                sb.Append("                       <h3><span onclick=\"openShutManager(this,'box4')\" ><a id=\"4\"  onClick=\"switchTag(4);\">账户管理</a></span></h3>");
                sb.Append("                       <div class=\"b07_info_main\" id=\"box4\">");
                sb.Append("<ul style=\"border-bottom: none;\" id=\"box4\">");
                sb.Append("<li " + (main == 4 && sub == 6 ? " class=\"on\"" : "") + "><a href=\"/supplier/account_profile.aspx\">资料管理</a></li>");
                sb.Append("</ul>");
                sb.Append("</div>");
                sb.Append("</div>");
            }
        }
        else
        {
            string Member_Permissions = Session["subPrivilege"].ToString();
            List<string> oTempList = new List<string>(Member_Permissions.Split(','));
            sb.Append("<div class=\"menu_1\">");
            sb.Append("                 <h2>我是卖家</h2>");

            //循环权限
            foreach (string item in oTempList)
            {
                //通过权限选取DIV
                switch (item)
                {
                    case "1":
                        break;
                    case "2":
                        //添加交易管理DIV
                        Transaction_Management(main, sub, sb);
                        //添加商品管理DIV
                        Commodity_Management(main, sub, sb);
                        //添加账号管理
                        Account_Management(main, sub, supplierinfo, SupplierMessageNum, sb);
                        //添加物流管理DIV
                        Logistics_Management(main, sub, sb);
                        break;
                    case "3":
                        //添加招标管理DIV
                        Tender_Management(main, sub, sb);
                        break;
                    case "4":
                        //添加拍卖管理DIV
                        Auction_Management(main, sub, sb);
                        break;
                    case "5":
                        //添加店铺管理DIV
                        Shop_Management(main, sub, Shop_Domain, SupplierPurchasereplyNum, SupplierShoppingAskNum, sb);
                        break;
                    case "6":
                        //添加财务管理DIV
                        Financial_Management(main, sub, sb);
                        break;
                    default:
                        pub.Msg("error", "错误提示", "当前系统繁忙请稍后重试，若多次出现请联系网站管理员！", false, "{back}");
                        break;
                }
            }


        }

        sb.Append("</div>");

        Response.Write(sb.ToString());
    }

    #endregion

    #endregion

    //店铺评分
    public void Shop_evalueinfo()
    {
        int Shop_product_Count = 0;
        int Shop_product_Sum = 0;
        int Shop_service_Count = 0;
        int Shop_service_Sum = 0;
        int Shop_delivery_Count = 0;
        int Shop_delivery_Sum = 0;
        double Shop_product_Avg = 0;
        double Shop_service_Avg = 0;
        double Shop_delivery_Avg = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopEvaluateInfo.Shop_Evaluate_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_SupplierID", "=", tools.NullInt(Session["supplier_id"]).ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopEvaluateInfo.Shop_Evaluate_ID", "Desc"));
        IList<SupplierShopEvaluateInfo> entitys = MyShopEvaluate.GetSupplierShopEvaluates(Query);
        if (entitys != null)
        {
            foreach (SupplierShopEvaluateInfo entity in entitys)
            {
                Shop_product_Count = Shop_product_Count + 1;
                Shop_product_Sum = Shop_product_Sum + entity.Shop_Evaluate_Product;
                Shop_service_Count = Shop_service_Count + 1;
                Shop_service_Sum = Shop_service_Sum + entity.Shop_Evaluate_Service;
                Shop_delivery_Count = Shop_delivery_Count + 1;
                Shop_delivery_Sum = Shop_delivery_Sum + entity.Shop_Evaluate_Delivery;
            }
        }
        if (Shop_product_Count > 0 && Shop_product_Sum > 0)
        {
            Shop_product_Avg = Math.Round(tools.CheckFloat((Shop_product_Sum / Shop_product_Count).ToString()), 1);
        }
        else
        {
            Shop_product_Avg = 0;
        }
        if (Shop_service_Count > 0 && Shop_service_Sum > 0)
        {
            Shop_service_Avg = Math.Round(tools.CheckFloat((Shop_service_Sum / Shop_service_Count).ToString()), 1);
        }
        else
        {
            Shop_service_Avg = 0;
        }
        if (Shop_delivery_Count > 0 && Shop_delivery_Sum > 0)
        {
            Shop_delivery_Avg = Math.Round(tools.CheckFloat((Shop_delivery_Sum / Shop_delivery_Count).ToString()), 1);
        }
        else
        {
            Shop_delivery_Avg = 0;
        }
        int i, j;
        Response.Write("<tr>");
        Response.Write("  <td>");
        Response.Write("         &nbsp; 卖家商品满意度：");
        for (i = 1; i <= Shop_product_Avg; i++)
        {
            Response.Write("<img src=\"/images/icon_star_on_small.gif\" align=\"absmiddle\">");
        }
        for (j = 0; j <= 5 - i; j++)
        {
            Response.Write("<img src=\"/images/icon_star_off_small.gif\" align=\"absmiddle\">");
        }
        Response.Write("  <strong>" + Shop_product_Avg + "</striong></td>");
        Response.Write("</tr>");
        Response.Write(" <tr>");
        Response.Write("  <td>");
        Response.Write("       &nbsp; 卖家服务满意度：");
        for (i = 1; i <= Shop_service_Avg; i++)
        {
            Response.Write("<img src=\"/images/icon_star_on_small.gif\" align=\"absmiddle\">");
        }
        for (j = 0; j <= 5 - i; j++)
        {
            Response.Write("<img src=\"/images/icon_star_off_small.gif\" align=\"absmiddle\">");
        }
        Response.Write("  <strong>" + Shop_service_Avg + "</striong></td>");
        Response.Write(" </tr>");
        Response.Write(" <tr>");
        Response.Write("   <td>");
        Response.Write("         &nbsp; 发货速度满意度：");
        for (i = 1; i <= Shop_delivery_Avg; i++)
        {
            Response.Write("<img src=\"/images/icon_star_on_small.gif\" align=\"absmiddle\">");
        }
        for (j = 0; j <= 5 - i; j++)
        {
            Response.Write("<img src=\"/images/icon_star_off_small.gif\" align=\"absmiddle\">");
        }
        Response.Write("   <strong>" + Shop_delivery_Avg + "</striong></td>");
        Response.Write(" </tr>");
    }

    public double Shop_evalues(int Supplier_ID)
    {
        int Shop_product_Count = 0;
        int Shop_product_Sum = 0;
        int Shop_service_Count = 0;
        int Shop_service_Sum = 0;
        int Shop_delivery_Count = 0;
        int Shop_delivery_Sum = 0;
        double Shop_product_Avg = 0;
        double Shop_service_Avg = 0;
        double Shop_delivery_Avg = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopEvaluateInfo.Shop_Evaluate_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_SupplierID", "=", Supplier_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopEvaluateInfo.Shop_Evaluate_ID", "Desc"));
        IList<SupplierShopEvaluateInfo> entitys = MyShopEvaluate.GetSupplierShopEvaluates(Query);
        if (entitys != null)
        {
            foreach (SupplierShopEvaluateInfo entity in entitys)
            {
                Shop_product_Count = Shop_product_Count + 1;
                Shop_product_Sum = Shop_product_Sum + entity.Shop_Evaluate_Product;
                Shop_service_Count = Shop_service_Count + 1;
                Shop_service_Sum = Shop_service_Sum + entity.Shop_Evaluate_Service;
                Shop_delivery_Count = Shop_delivery_Count + 1;
                Shop_delivery_Sum = Shop_delivery_Sum + entity.Shop_Evaluate_Delivery;
            }
        }

        return Shop_service_Avg;
    }

    //消息提示信息
    public void Shop_Tip()
    {

        int Product_Ask, Shop_Ask, Message_Count;
        Product_Ask = 0;
        Shop_Ask = 0;
        Message_Count = 0;
        QueryInfo Query = new QueryInfo();
        if (tools.NullInt(Session["supplier_ishaveshop"]) == 1)
        {
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ShoppingAskInfo.Ask_SupplierID", "=", tools.NullInt(Session["supplier_id"]).ToString()));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ShoppingAskInfo.Ask_ProductID", ">", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ShoppingAskInfo.Ask_Isreply", "=", "0"));
            IList<ShoppingAskInfo> entitys = MyAsk.GetShoppingAsks(Query, pub.CreateUserPrivilege("fe2e0dd7-2773-4748-915a-103065ed0334"));
            if (entitys != null)
            {
                Product_Ask = entitys.Count;
            }
            Query = null;
            Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ShoppingAskInfo.Ask_SupplierID", "=", tools.NullInt(Session["supplier_id"]).ToString()));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ShoppingAskInfo.Ask_ProductID", "=", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ShoppingAskInfo.Ask_Isreply", "=", "0"));
            entitys = MyAsk.GetShoppingAsks(Query, pub.CreateUserPrivilege("fe2e0dd7-2773-4748-915a-103065ed0334"));
            if (entitys != null)
            {
                Shop_Ask = entitys.Count;
            }
            Query = null;
        }
        Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierMessageInfo.Supplier_Message_SupplierID", "=", tools.NullInt(Session["supplier_id"]).ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierMessageInfo.Supplier_Message_IsRead", "=", "0"));
        Query.OrderInfos.Add(new OrderInfo("SupplierMessageInfo.Supplier_Message_ID", "Desc"));
        IList<SupplierMessageInfo> messages = MyMessage.GetSupplierMessages(Query, pub.CreateUserPrivilege("d8b3c47b-26c4-435f-884e-c9951464b633"));
        if (messages != null)
        {
            Message_Count = messages.Count;
        }
        Response.Write("有 <a href=\"/supplier/Sys_Notice.aspx\" class=\"a_t12_blue\"><span class=\"t12_red\"><strong>" + (Message_Count) + "</strong></span> 条未读通知</a>");
        if (tools.NullInt(Session["supplier_ishaveshop"]) == 1)
        {
            Response.Write("，有 <span class=\"t12_red\"><strong>" + (Shop_Ask + Product_Ask) + "</strong></span> 条未回复信息， 其中<a href=\"/supplier/Supplier_Shop_Message.aspx\" class=\"a_t12_blue\">店铺咨询 <span class=\"t12_red\"><strong>" + Shop_Ask + "</strong></span> 条</a>， <a href=\"/supplier/Supplier_Shop_Message.aspx?action=product\" class=\"a_t12_blue\">商品咨询 <span class=\"t12_red\"><strong>" + Product_Ask + "</strong></span> 条</a>");
        }

    }

    //检查信息是否完整
    public bool Account_Iscompleteprofile()
    {
        bool Result = false;
        SupplierInfo supplier = MyBLL.GetSupplierByID(tools.CheckInt(Session["supplier_id"].ToString()), pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
        if (supplier != null)
        {
            if (supplier.Supplier_CompanyName != "")
            {
                Result = true;
            }
        }
        return Result;
    }

    private bool CheckSupplierSysMobileEmail(string Mobile, string Email, int Supplier_ID)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        if (Mobile.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_SysMobile", "=", Mobile));
        }
        if (Email.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_SysEmail", "=", Email));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierInfo.Supplier_ID", "<>", Supplier_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierInfo.Supplier_Site", "=", pub.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("SupplierInfo.Supplier_ID", "Desc"));
        PageInfo page = MyBLL.GetPageInfo(Query, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
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

    //商家资料修改  
    public void UpdateSupplierInfo()
    {
        int account_id = tools.NullInt(Session["account_id"]);
        if (account_id > 0)
        {
            Response.Redirect("/supplier/index.aspx");
        }
        int Supplier_ID = tools.CheckInt(Request.Form["Supplier_ID"]);
        string Supplier_CompanyName = tools.CheckStr(Request.Form["Supplier_CompanyName"]);
        string Supplier_County = tools.CheckStr(Request.Form["Supplier_County"]);
        string Supplier_City = tools.CheckStr(Request.Form["Supplier_City"]);
        string Supplier_State = tools.CheckStr(Request.Form["Supplier_State"]);
        string Supplier_Country = tools.CheckStr(Request.Form["Supplier_Country"]);
        string Supplier_Address = tools.CheckStr(Request.Form["Supplier_Address"]);
        string Supplier_Phone = tools.CheckStr(Request.Form["Supplier_Phone"]);
        string Supplier_Fax = tools.CheckStr(Request.Form["Supplier_Fax"]);
        string Supplier_Zip = tools.CheckStr(Request.Form["Supplier_Zip"]);
        string Supplier_Contactman = tools.CheckStr(Request.Form["Supplier_Contactman"]);
        string Supplier_Mobile = tools.CheckStr(Request.Form["Supplier_Mobile"]);
        int Supplier_IsApply = tools.CheckInt(Request.Form["Supplier_IsApply"]);
        string Supplier_SysMobile = tools.CheckStr(Request.Form["Supplier_SysMobile"]);
        string Supplier_SysEmail = tools.CheckStr(Request.Form["Supplier_SysEmail"]);
        string Supplier_SealImg = tools.CheckStr(Request.Form["Supplier_SealImg"]);
        string Supplier_Corporate = tools.CheckStr(Request.Form["Supplier_Corporate"]);
        string Supplier_CorporateMobile = tools.CheckStr(Request.Form["Supplier_CorporateMobile"]);
        double Supplier_RegisterFunds = tools.CheckFloat(Request.Form["Supplier_RegisterFunds"]);
        string Supplier_BusinessCode = tools.CheckStr(Request.Form["Supplier_BusinessCode"]);
        string Supplier_OrganizationCode = tools.CheckStr(Request.Form["Supplier_OrganizationCode"]);
        string Supplier_TaxationCode = tools.CheckStr(Request.Form["Supplier_TaxationCode"]);
        string Supplier_BankAccountCode = tools.CheckStr(Request.Form["Supplier_BankAccountCode"]);
        int Supplier_IsAuthorize = tools.CheckInt(Request.Form["Supplier_IsAuthorize"]);
        int Supplier_IsTrademark = tools.CheckInt(Request.Form["Supplier_IsTrademark"]);
        string Supplier_ServicesPhone = tools.CheckStr(Request.Form["Supplier_ServicesPhone"]);
        int Supplier_OperateYear = tools.CheckInt(Request.Form["Supplier_OperateYear"]);
        string Supplier_ContactEmail = tools.CheckStr(Request.Form["Supplier_ContactEmail"]);
        string Supplier_ContactQQ = tools.CheckStr(Request.Form["Supplier_ContactQQ"]);
        string Supplier_Category = tools.CheckStr(Request.Form["Supplier_Category"]);
        int Supplier_SaleType = tools.CheckInt(Request.Form["Supplier_SaleType"]);

        if (Supplier_Corporate == "")
        {
            Response.Write("请填写法定代表人姓名");
            Response.End();
        }

        if (Supplier_CorporateMobile == "")
        {
            Response.Write("请填写法定代表人手机号");
            Response.End();
        }

        if (Supplier_BusinessCode == "")
        {
            Response.Write("请填写营业执照副本号");
            Response.End();
        }

        if (Supplier_BankAccountCode == "")
        {
            Response.Write("请填写法定代表人姓名");
            Response.End();
        }

        if (Supplier_TaxationCode == "")
        {
            Response.Write("税务登记证副本号");
            Response.End();
        }

        if (Supplier_OrganizationCode == "")
        {
            Response.Write("组织机构代码证副本号");
            Response.End();
        }

        if (Supplier_County == "0" || Supplier_County == "")
        {
            //pub.Msg("info", "提示信息", "请选择所在地区", false, "{back}");
            Response.Write("请选择所在地区");
            Response.End();
        }

        if (Supplier_Address == "")
        {
            //pub.Msg("info", "提示信息", "请将详细地址填写完整", false, "{back}");
            Response.Write("请将详细地址填写完整");
            Response.End();
        }

        if (Supplier_Mobile == "")
        {
            //pub.Msg("info", "提示信息", "请填写手机号码", false, "{back}");
        }

        if (Supplier_Mobile != "")
        {
            if (pub.Checkmobile(Supplier_Mobile) == false)
            {
                //pub.Msg("info", "提示信息", "手机号码错误", false, "{back}");
                Response.Write("请填写手机号码");
                Response.End();
            }
        }

        if (Supplier_Contactman == "")
        {
            //pub.Msg("info", "提示信息", "请将联系人填写完整", false, "{back}");
            Response.Write("请将联系人填写完整");
            Response.End();
        }

        if (Supplier_SysMobile != "")
        {
            if (pub.Checkmobile(Supplier_SysMobile) == false)
            {
                //pub.Msg("info", "提示信息", "订阅接收手机号码错误", false, "{back}");
                Response.Write("订阅接收手机号码错误");
                Response.End();
            }
        }

        if (Supplier_SysEmail != "")
        {
            if (tools.CheckEmail(Supplier_SysEmail) == false)
            {
                //pub.Msg("info", "提示信息", "订阅接收邮箱格式错误", false, "{back}");
                Response.Write("订阅接收邮箱格式错误");
                Response.End();
            }
        }

        SupplierInfo entity = GetSupplierByID();
        if (entity != null)
        {
            if (Supplier_SysMobile.Length > 0)
            {
                if (CheckSupplierSysMobileEmail(Supplier_SysMobile, "", entity.Supplier_ID))
                {
                    //pub.Msg("info", "提示信息", "订阅接收手机号码已被他人使用", false, "{back}");
                    Response.Write("订阅接收手机号码已被他人使用");
                    Response.End();
                }
            }
            if (Supplier_SysEmail.Length > 0)
            {
                if (CheckSupplierSysMobileEmail("", Supplier_SysEmail, entity.Supplier_ID))
                {
                    //pub.Msg("info", "提示信息", "订阅接收邮箱已被他人使用", false, "{back}");
                    Response.Write("订阅接收邮箱已被他人使用");
                    Response.End();
                }
            }
            if (entity.Supplier_AuditStatus != 1)
            {
                if (Supplier_CompanyName == "")
                {
                    //pub.Msg("info", "提示信息", "请将公司名称填写完整", false, "{back}");
                    Response.Write("请将公司名称填写完整");
                    Response.End();
                }
                entity.Supplier_CompanyName = Supplier_CompanyName;
            }

            entity.Supplier_County = Supplier_County;
            entity.Supplier_City = Supplier_City;
            entity.Supplier_State = Supplier_State;
            entity.Supplier_Country = Supplier_Country;
            entity.Supplier_Address = Supplier_Address;
            entity.Supplier_Phone = Supplier_Phone;
            entity.Supplier_Fax = Supplier_Fax;
            entity.Supplier_Zip = Supplier_Zip;
            entity.Supplier_Contactman = Supplier_Contactman;
            entity.Supplier_Mobile = Supplier_Mobile;
            entity.Supplier_IsApply = Supplier_IsApply;
            entity.Supplier_SysMobile = Supplier_SysMobile;
            entity.Supplier_SysEmail = Supplier_SysEmail;
            entity.Supplier_SealImg = Supplier_SealImg;
            entity.Supplier_Corporate = Supplier_Corporate;
            entity.Supplier_CorporateMobile = Supplier_CorporateMobile;
            entity.Supplier_RegisterFunds = Supplier_RegisterFunds;
            entity.Supplier_BusinessCode = Supplier_BusinessCode;
            entity.Supplier_OrganizationCode = Supplier_OrganizationCode;
            entity.Supplier_TaxationCode = Supplier_TaxationCode;
            entity.Supplier_BankAccountCode = Supplier_BankAccountCode;
            entity.Supplier_IsAuthorize = Supplier_IsAuthorize;
            entity.Supplier_IsTrademark = Supplier_IsTrademark;
            entity.Supplier_ServicesPhone = Supplier_ServicesPhone;
            entity.Supplier_OperateYear = Supplier_OperateYear;
            entity.Supplier_ContactEmail = Supplier_ContactEmail;
            entity.Supplier_ContactQQ = Supplier_ContactQQ;
            entity.Supplier_Category = Supplier_Category;
            entity.Supplier_SaleType = Supplier_SaleType;
            entity.Supplier_AuditStatus = 0;

            if (MyBLL.EditSupplier(entity, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f")))
            {
                Modify_Enterprise_Info(entity);

                //Supplier_Cert_Save();
                //pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/account_profile.aspx?tip=success");
                Response.Write("success");
                Response.End();
            }
            else
            {
                //pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
                Response.Write("failure");
                Response.End();
            }
        }
        else
        {
            //pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
            Response.Write("failure");
            Response.End();
        }
    }



    public void UpdateSupplierInfoByMember_Profile()
    {
        int account_id = tools.NullInt(Session["account_id"]);
        if (account_id > 0)
        {
            Response.Redirect("/supplier/index.aspx");
        }
        int Supplier_ID = tools.CheckInt(Request.Form["Supplier_ID"]);
        string Supplier_Nickname = tools.CheckStr(Request.Form["Supplier_Nickname"]);
        string Supplier_Email = tools.CheckStr(Request.Form["Supplier_Email"]);
        string Supplier_CompanyName = tools.CheckStr(Request.Form["Supplier_CompanyName"]);
        string Supplier_County = tools.CheckStr(Request.Form["Supplier_County"]);
        string Supplier_City = tools.CheckStr(Request.Form["Supplier_City"]);
        string Supplier_State = tools.CheckStr(Request.Form["Supplier_State"]);
        string Supplier_Country = tools.CheckStr(Request.Form["Supplier_Country"]);
        string Supplier_Address = tools.CheckStr(Request.Form["Supplier_Address"]);
        string Supplier_Phone = tools.CheckStr(Request.Form["Supplier_Phone"]);
        string Supplier_Fax = tools.CheckStr(Request.Form["Supplier_Fax"]);
        string Supplier_Zip = tools.CheckStr(Request.Form["Member_Profile_Zip"]);
        string Supplier_Contactman = tools.CheckStr(Request.Form["Supplier_Contactman"]);
        string Supplier_Mobile = tools.CheckStr(Request.Form["Supplier_Mobile"]);
        int Supplier_IsApply = tools.CheckInt(Request.Form["Supplier_IsApply"]);
        string Supplier_SysMobile = tools.CheckStr(Request.Form["Supplier_SysMobile"]);
        string Supplier_SysEmail = tools.CheckStr(Request.Form["Supplier_SysEmail"]);
        string Supplier_SealImg = tools.CheckStr(Request.Form["Supplier_SealImg"]);
        string Supplier_Corporate = tools.CheckStr(Request.Form["Supplier_Corporate"]);
        string Supplier_CorporateMobile = tools.CheckStr(Request.Form["Supplier_CorporateMobile"]);
        double Supplier_RegisterFunds = tools.CheckFloat(Request.Form["Supplier_RegisterFunds"]);
        string Supplier_BusinessCode = tools.CheckStr(Request.Form["Supplier_BusinessCode"]);
        string Supplier_OrganizationCode = tools.CheckStr(Request.Form["Supplier_OrganizationCode"]);
        string Supplier_TaxationCode = tools.CheckStr(Request.Form["Supplier_TaxationCode"]);
        string Supplier_BankAccountCode = tools.CheckStr(Request.Form["Supplier_BankAccountCode"]);



        int Supplier_IsAuthorize = tools.CheckInt(Request.Form["Supplier_IsAuthorize"]);
        int Supplier_IsTrademark = tools.CheckInt(Request.Form["Supplier_IsTrademark"]);
        string Supplier_ServicesPhone = tools.CheckStr(Request.Form["Supplier_ServicesPhone"]);
        int Supplier_OperateYear = tools.CheckInt(Request.Form["Supplier_OperateYear"]);
        int Supplier_SaleType = tools.CheckInt(Request.Form["Supplier_SaleType"]);

        string Supplier_ContactEmail = tools.CheckStr(Request.Form["Supplier_ContactEmail"]);
        string Supplier_ContactQQ = tools.CheckStr(Request.Form["Supplier_ContactQQ"]);
        string Supplier_Category = tools.CheckStr(Request.Form["Supplier_Category"]);


        if (Supplier_Corporate == "")
        {
            Response.Write("请填写法定代表人姓名");
            Response.End();
        }

        if (Supplier_CorporateMobile == "")
        {
            Response.Write("请填写法定代表人手机号");
            Response.End();
        }

        if (Supplier_BusinessCode == "")
        {
            Response.Write("请填写营业执照副本号");
            Response.End();
        }

        //if (Supplier_BankAccountCode == "")
        //{
        //    Response.Write("银行开户许可证号");
        //    Response.End();
        //}

        if (Supplier_TaxationCode == "")
        {
            Response.Write("税务登记证副本号");
            Response.End();
        }

        if (Supplier_OrganizationCode == "")
        {
            Response.Write("组织机构代码证副本号");
            Response.End();
        }

        //if (Supplier_County == "0" || Supplier_County == "")
        //{
        //    //pub.Msg("info", "提示信息", "请选择所在地区", false, "{back}");
        //    Response.Write("请选择所在地区");
        //    Response.End();
        //}

        if (Supplier_Address == "")
        {
            //pub.Msg("info", "提示信息", "请将详细地址填写完整", false, "{back}");
            Response.Write("请将详细地址填写完整");
            Response.End();
        }

        if (Supplier_Mobile == "")
        {
            pub.Msg("info", "提示信息", "请填写手机号码", false, "{back}");
        }

        if (Supplier_Mobile != "")
        {
            if (pub.Checkmobile(Supplier_Mobile) == false)
            {
                //pub.Msg("info", "提示信息", "手机号码错误", false, "{back}");
                Response.Write("请填写手机号码");
                Response.End();
            }
        }

        if (Supplier_Contactman == "")
        {
            //pub.Msg("info", "提示信息", "请将联系人填写完整", false, "{back}");
            Response.Write("请将联系人填写完整");
            Response.End();
        }

        if (Supplier_SysMobile != "")
        {
            if (pub.Checkmobile(Supplier_SysMobile) == false)
            {
                //pub.Msg("info", "提示信息", "订阅接收手机号码错误", false, "{back}");
                Response.Write("订阅接收手机号码错误");
                Response.End();
            }
        }

        if (Supplier_SysEmail != "")
        {
            if (tools.CheckEmail(Supplier_SysEmail) == false)
            {
                //pub.Msg("info", "提示信息", "订阅接收邮箱格式错误", false, "{back}");
                Response.Write("订阅接收邮箱格式错误");
                Response.End();
            }
        }

        SupplierInfo entity = GetSupplierByID();
        if (entity != null)
        {
            if (Supplier_SysMobile.Length > 0)
            {
                if (CheckSupplierSysMobileEmail(Supplier_SysMobile, "", entity.Supplier_ID))
                {
                    //pub.Msg("info", "提示信息", "订阅接收手机号码已被他人使用", false, "{back}");
                    Response.Write("订阅接收手机号码已被他人使用");
                    Response.End();
                }
            }
            if (Supplier_SysEmail.Length > 0)
            {
                if (CheckSupplierSysMobileEmail("", Supplier_SysEmail, entity.Supplier_ID))
                {
                    //pub.Msg("info", "提示信息", "订阅接收邮箱已被他人使用", false, "{back}");
                    Response.Write("订阅接收邮箱已被他人使用");
                    Response.End();
                }
            }
            if (entity.Supplier_AuditStatus != 1)
            {
                if (Supplier_CompanyName == "")
                {
                    //pub.Msg("info", "提示信息", "请将公司名称填写完整", false, "{back}");
                    Response.Write("请将公司名称填写完整");
                    Response.End();
                }
                entity.Supplier_CompanyName = Supplier_CompanyName;
            }
            entity.Supplier_Nickname = Supplier_Nickname;
            entity.Supplier_Email = Supplier_Email;
            entity.Supplier_County = Supplier_County;
            entity.Supplier_City = Supplier_City;
            entity.Supplier_State = Supplier_State;
            entity.Supplier_Country = Supplier_Country;
            entity.Supplier_Address = Supplier_Address;
            entity.Supplier_Phone = Supplier_Phone;
            entity.Supplier_Fax = Supplier_Fax;
            entity.Supplier_Zip = Supplier_Zip;
            entity.Supplier_Contactman = Supplier_Contactman;
            entity.Supplier_Mobile = Supplier_Mobile;
            entity.Supplier_IsApply = Supplier_IsApply;
            entity.Supplier_SysMobile = Supplier_SysMobile;
            entity.Supplier_SysEmail = Supplier_SysEmail;
            entity.Supplier_SealImg = Supplier_SealImg;
            entity.Supplier_Corporate = Supplier_Corporate;
            entity.Supplier_CorporateMobile = Supplier_CorporateMobile;
            entity.Supplier_RegisterFunds = Supplier_RegisterFunds;
            entity.Supplier_BusinessCode = Supplier_BusinessCode;
            entity.Supplier_OrganizationCode = Supplier_OrganizationCode;
            entity.Supplier_TaxationCode = Supplier_TaxationCode;
            entity.Supplier_BankAccountCode = Supplier_BankAccountCode;
            entity.Supplier_IsAuthorize = Supplier_IsAuthorize;
            entity.Supplier_IsTrademark = Supplier_IsTrademark;
            entity.Supplier_ServicesPhone = Supplier_ServicesPhone;
            entity.Supplier_OperateYear = Supplier_OperateYear;
            entity.Supplier_ContactEmail = Supplier_ContactEmail;
            entity.Supplier_ContactQQ = Supplier_ContactQQ;
            entity.Supplier_Category = Supplier_Category;
            entity.Supplier_SaleType = Supplier_SaleType;
            entity.Supplier_AuditStatus = 0;

            if (MyBLL.EditSupplier(entity, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f")))
            {
                //Modify_Enterprise_Info(entity);

                //Supplier_Cert_Save();
                //pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/account_profile.aspx?tip=success");
                Response.Write("success");
                Response.End();
            }
            else
            {
                //pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
                Response.Write("failure");
                Response.End();
            }
        }
        else
        {
            //pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
            Response.Write("failure");
            Response.End();
        }
    }

    //商家密码修改
    public void UpdateSupplierPassword()
    {
        int account_id = tools.NullInt(Session["account_id"]);
        if (account_id > 0)
        {
            Response.Redirect("/supplier/index.aspx");
        }
        string old_pwd = tools.CheckStr(tools.NullStr(Request.Form["Supplier_oldpassword"]));
        string Supplier_password = tools.CheckStr(tools.NullStr(Request.Form["Supplier_password"]));
        string Supplier_password_confirm = tools.CheckStr(tools.NullStr(Request.Form["Supplier_password_confirm"]));
        string verifycode = tools.CheckStr(tools.NullStr(Request.Form["verifycode"]));

        if (verifycode != Session["Trade_Verify"].ToString())
        {
            pub.Msg("info", "提示信息", "验证码输入错误", false, "{back}");
        }

        if (old_pwd == "")
        {
            pub.Msg("info", "提示信息", "请输入6～20位原密码", false, "{back}");
        }

        if (CheckSsn(Supplier_password) == false)
        {
            pub.Msg("info", "提示信息", "密码包含特殊字符，只接受A-Z，a-z，0-9，不要输入空格", false, "{back}");
        }
        else
        {
            if (Supplier_password.Length < 6 || Supplier_password.Length > 20)
            {
                pub.Msg("info", "提示信息", "请输入6～20位新密码", false, "{back}");
            }
        }

        if (Supplier_password != Supplier_password_confirm)
        {
            pub.Msg("info", "提示信息", "两次密码输入不一致，请重新输入", false, "{back}");
        }

        old_pwd = encrypt.MD5(old_pwd);
        Supplier_password = encrypt.MD5(Supplier_password);


        SupplierInfo Supplierinfo = new SupplierInfo();
        Supplierinfo = GetSupplierByID();
        if (Supplierinfo != null)
        {

            string Supplier_Password = Supplierinfo.Supplier_Password;

            Supplierinfo.Supplier_Password = Supplier_password;

            if (old_pwd != Supplier_Password)
            {
                pub.Msg("info", "提示信息", "原密码输入错误，请重试！", false, "{back}");
            }
            if (MyBLL.EditSupplier(Supplierinfo, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f")))
            {
                Response.Redirect("/Supplier/account_password.aspx?tip=success");
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


    //政策通知列表
    public void Sys_Notice_List()
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        Pageurl = "?action=list";
        if (curpage < 1)
        {
            curpage = 1;
        }

        Response.Write("<table width=\"904\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <th align=\"center\" valign=\"middle\">通知标题</th>");
        Response.Write("  <th width=\"130\" align=\"center\" valign=\"middle\">通知时间</th>");
        Response.Write("  <th width=\"70\" align=\"center\" valign=\"middle\">查看</th>");
        Response.Write("</tr>");
        string productURL = string.Empty;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierMessageInfo.Supplier_Message_SupplierID", "=", supplier_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierMessageInfo.Supplier_Message_ID", "Desc"));
        IList<SupplierMessageInfo> entitys = MyMessage.GetSupplierMessages(Query, pub.CreateUserPrivilege("d8b3c47b-26c4-435f-884e-c9951464b633"));
        PageInfo page = MyMessage.GetPageInfo(Query, pub.CreateUserPrivilege("d8b3c47b-26c4-435f-884e-c9951464b633"));
        if (entitys != null)
        {
            foreach (SupplierMessageInfo entity in entitys)
            {
                i = i + 1;
                Response.Write("<tr bgcolor=\"#ffffff\">");
                if (entity.Supplier_Message_IsRead == 0)
                {
                    Response.Write("<td width=\"200\" height=\"35\" align=\"left\" class=\"comment_td_bg\" style=\"padding-left:10px;\" valign=\"middle\"><span class=\" t12_red\">" + tools.CutStr(entity.Supplier_Message_Title, 50) + "</span></td>");
                }
                else
                {
                    Response.Write("<td width=\"200\" height=\"35\" align=\"left\" class=\"comment_td_bg\" style=\"padding-left:10px;\" valign=\"middle\">" + tools.CutStr(entity.Supplier_Message_Title, 50) + "</a></td>");
                }
                Response.Write("<td width=\"130\" height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\"><a>" + entity.Supplier_Message_Addtime + "</a></td>");
                Response.Write("<td width=\"70\" height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\"><a href=\"/supplier/Sys_Notice_View.aspx?notice_id=" + entity.Supplier_Message_ID + "\" style=\"color:#3366cc;\" target=\"_blank\">查看</a>");
                if (entity.Supplier_Message_IsRead == 1)
                {
                    Response.Write(" <a href=\"/supplier/Sys_Notice_do.aspx?action=remove&notice_id=" + entity.Supplier_Message_ID + "\" style=\"color:#3366cc;\">删除</a>");
                }
                Response.Write("</td>");
                Response.Write("</tr>");


            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" align=\"center\" cellpadding=\"2\"><tr><td align=\"right\" height=\"10\"></td></tr><tr><td align=\"right\"><div class=\"page\" style=\"float:right;\">");
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
            Response.Write("</div></td></tr></table>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#ffffff\"><td width=\"70\" height=\"35\" colspan=\"4\" align=\"center\" valign=\"middle\">没有记录</td></tr>");
        }
        Response.Write("</table>");
    }

    //获取指定记录政策通知
    public SupplierMessageInfo GetSupplierMessageByID(int ID)
    {
        return MyMessage.GetSupplierMessageByID(ID, pub.CreateUserPrivilege("d8b3c47b-26c4-435f-884e-c9951464b633"));
    }

    //删除指定政策通知
    public void Supplier_Message_Del()
    {
        int Notice_ID = tools.CheckInt(Request["notice_id"]);
        if (Notice_ID > 0)
        {
            SupplierMessageInfo entity = GetSupplierMessageByID(Notice_ID);
            if (entity != null)
            {
                if (entity.Supplier_Message_SupplierID == tools.NullInt(Session["supplier_id"]) && entity.Supplier_Message_IsRead == 1)
                {
                    MyMessage.DelSupplierMessage(Notice_ID, pub.CreateUserPrivilege("ba7a4b2e-b6d1-473d-b0ba-2d3041c30aa7"));
                    pub.Msg("positive", "操作成功", "操作成功", true, "Sys_Notice.aspx");
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
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //更新政策通知
    public void Supplier_Message_Edit(SupplierMessageInfo entity)
    {
        MyMessage.EditSupplierMessage(entity, pub.CreateUserPrivilege("b7d38ac5-000c-4d07-9ca3-46df47367554"));
    }

    public void Supplier_Cert_Save()
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
        SupplierInfo entity = GetSupplierByID();
        if (entity != null)
        {

            certinfos = GetSupplierCertByType(Supplier_CertType);
            if (certinfos != null)
            {
                foreach (SupplierCertInfo certinfo in certinfos)
                {
                    if (tools.CheckStr(Request["Supplier_Cert" + certinfo.Supplier_Cert_ID + "_tmp"]).Length == 0)
                    {
                        //pub.Msg("info", "提示信息", "请将资质文件上传完整", false, "{back}");
                        //Response.Write("请将资质上传完整");
                        //Response.End();
                        if (certinfo.Supplier_Cert_Name == "合同章(选填)")
                        {

                        }
                        else
                        {
                            pub.Msg("info", "提示信息", "请将资质文件上传完整", false, "{back}");
                            Response.End();
                        }
                    }
                }
                //删除资质文件
                MyBLL.DelSupplierRelateCertBySupplierID(entity.Supplier_ID);
                foreach (SupplierCertInfo certinfo in certinfos)
                {
                    ratecate = new SupplierRelateCertInfo();
                    ratecate.Cert_SupplierID = entity.Supplier_ID;
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
                    MyBLL.AddSupplierRelateCert(ratecate);
                    ratecate = null;
                }
            }


            entity.Supplier_CertType = Supplier_CertType;
            entity.Supplier_Cert_Status = 1;
            if (MyBLL.EditSupplier(entity, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f")))
            {
                //pub.Msg("positive", "操作成功", "上传完成，请耐心等待审核通过！", true, "/supplier/Supplier_Cert.aspx");
                Response.Write("success");
                Response.End();
            }
            else
            {
                //pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
                Response.Write("failure");
                Response.End();
            }
        }
        else
        {
            //pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
            Response.Write("failure");
            Response.End();
        }
    }

    public string Get_Supplier_Cert(int Supplier_CertID, IList<SupplierRelateCertInfo> RelateCates)
    {
        string Cert_Img = "";
        if (RelateCates != null)
        {
            foreach (SupplierRelateCertInfo entity in RelateCates)
            {
                if (Supplier_CertID == entity.Cert_CertID)
                {
                    Cert_Img = entity.Cert_Img;
                    break;
                }
            }
        }
        return Cert_Img;
    }

    public string Get_Supplier_Certtmp(int Supplier_CertID, IList<SupplierRelateCertInfo> RelateCates)
    {
        string Cert_Img = "";
        if (RelateCates != null)
        {
            foreach (SupplierRelateCertInfo entity in RelateCates)
            {
                if (Supplier_CertID == entity.Cert_CertID)
                {
                    Cert_Img = entity.Cert_Img1;
                    break;
                }
            }
        }
        return Cert_Img;
    }

    public string Get_Cert_Status()
    {
        string status = "未上传";
        SupplierInfo entity = GetSupplierByID();
        if (entity != null)
        {
            if (entity.Supplier_Cert_Status == 0)
            {
                status = "<b><span class=\"t12_red\">未上传</span></b>";
            }
            if (entity.Supplier_Cert_Status == 1)
            {
                status = "<b><span class=\"t12_red\">资质审核中</span></b>";
            }
            if (entity.Supplier_Cert_Status == 2)
            {
                status = "<b><span class=\"t12_green\">审核通过</span></b>";
            }
            if (entity.Supplier_Cert_Status == 3)
            {
                status = "<b><span class=\"t12_red\">审核未通过</span></b>";
            }
        }
        return status;
    }

    public string Get_Cert_Type(int CertType)
    {
        string Cert_Type = "";
        switch (CertType)
        {
            case 0:
                Cert_Type = "普通商家";
                break;
            case 1:
                Cert_Type = "下岗创业";
                break;
            case 2:
                Cert_Type = "大学生创业";
                break;
            case 3:
                Cert_Type = "自主创业";
                break;
        }
        return Cert_Type;
    }



    public void UpdateSupplierProfile()
    {
        #region 注释太长 折叠
        //MemberProfileInfo profileInfo = null;
        //SupplierInfo supplierinfo = null;
        //int Supplier_AuditStatus = -1;
        //int account_id = tools.NullInt(Session["member_accountid"]);
        //if (account_id > 0)
        //{
        //    Response.Redirect("/supplier/index.aspx");
        //}

        //string Member_Profile_OrganizationCode = null;
        //string Member_Profile_BusinessCode = null;

        //string Member_Corporate = null;
        //string Member_CorporateMobile = null;
        //double Member_RegisterFunds = 0.00;
        //string Member_TaxationCode = null;
        //string Member_BankAccountCode = null;

        //string Member_Profile_SealImg = null;

        //string Supplier_OrganizationCode = null;
        //string Supplier_Corporate = null;
        //string Supplier_CorporateMobile = null;
        //double Supplier_RegisterFunds = 0.00;
        //string Supplier_TaxationCode = null;
        //string Supplier_BankAccountCode = null;
        //string Member_NickName = "";
        //string Member_RealName = "";

        //string Member_Email = "";
        //string Member_Company = "";
        //string Member_LoginMobile = "";


        //int Member_ID = tools.CheckInt(Request.Form["Member_ID"]);
        //MemberInfo memberinfo = new Member().GetMemberByID();
        //if (memberinfo != null)
        //{
        //    profileInfo = memberinfo.MemberProfileInfo;
        //    supplierinfo = GetSupplierByID(memberinfo.Member_SupplierID);
        //    if (supplierinfo != null)
        //    {
        //        Supplier_AuditStatus = supplierinfo.Supplier_AuditStatus;
        //    }
        //    if (profileInfo != null)
        //    {
        //        Member_RealName = profileInfo.Member_RealName;
        //        Member_Company = profileInfo.Member_Company;

        //    }
        //}

        //if (memberinfo != null)
        //{
        //    Member_NickName = memberinfo.Member_NickName;
        //    Member_Email = memberinfo.Member_Email;

        //    Member_LoginMobile = memberinfo.Member_LoginMobile;
        //}








        //string Member_Profile_State = tools.CheckStr(Request.Form["Member_Profile_State"]);
        //string Member_Profile_City = tools.CheckStr(Request.Form["Member_Profile_City"]);
        //string Member_Profile_County = tools.CheckStr(Request.Form["Member_Profile_County"]);


        //string Member_Profile_Country = tools.CheckStr(Request.Form["Member_Profile_Country"]);
        //string Member_Profile_Address = tools.CheckStr(Request.Form["Member_Profile_Address"]);

        //string Member_Profile_Phone = tools.CheckStr(Request.Form["Member_Profile_Phone"]);
        //string Member_Profile_Fax = tools.CheckStr(Request.Form["Member_Profile_Fax"]);
        //string Member_Profile_Zip = tools.CheckStr(Request.Form["Member_Profile_Zip"]);
        //string Member_Profile_Contactman = tools.CheckStr(Request.Form["Member_Profile_Contactman"]);
        ////string Member_Profile_Mobile = tools.CheckStr(Request.Form["Member_Profile_Mobile"]);
        //string Member_Profile_QQ = tools.CheckStr(Request.Form["Member_Profile_QQ"]);



        //Member_Profile_SealImg = tools.CheckStr(Request.Form["Member_Profile_SealImg"]);




        //int Supplier_IsAuthorize = tools.CheckInt(Request.Form["Supplier_IsAuthorize"]);
        //int Supplier_IsTrademark = tools.CheckInt(Request.Form["Supplier_IsTrademark"]);
        //string Supplier_ServicesPhone = tools.CheckStr(Request.Form["Supplier_ServicesPhone"]);
        //int Supplier_OperateYear = tools.CheckInt(Request.Form["Supplier_OperateYear"]);
        //int Supplier_SaleType = tools.CheckInt(Request.Form["Supplier_SaleType"]);
        //string Supplier_ContactEmail = tools.CheckStr(Request.Form["Supplier_ContactEmail"]);
        //string Supplier_ContactQQ = tools.CheckStr(Request.Form["Supplier_ContactQQ"]);
        //string Supplier_Category = tools.CheckStr(Request.Form["Supplier_Category"]);

        ////新加两个字段 

        //string Member_UniformSocial_Number = tools.CheckStr(Request.Form["Member_UniformSocial_Number"]);

        //string Supplier_SysMobile = tools.CheckStr(Request.Form["Supplier_SysMobile"]);
        //string Supplier_SysEmail = tools.CheckStr(Request.Form["Supplier_SysEmail"]);

        //if (supplierinfo != null)
        //{
        //    if (supplierinfo.Supplier_AuditStatus == 0)
        //    {
        //        Member_Profile_OrganizationCode = tools.CheckStr(Request.Form["Member_Profile_OrganizationCode"]);
        //        Member_Profile_BusinessCode = tools.CheckStr(Request.Form["Member_Profile_BusinessCode"]);

        //        Member_Corporate = tools.CheckStr(Request.Form["Member_Corporate"]);
        //        Member_CorporateMobile = tools.CheckStr(Request.Form["Member_CorporateMobile"]);
        //        Member_RegisterFunds = tools.CheckFloat(Request.Form["Member_RegisterFunds"]);
        //        Member_TaxationCode = tools.CheckStr(Request.Form["Member_TaxationCode"]);
        //        Member_BankAccountCode = tools.CheckStr(Request.Form["Member_BankAccountCode"]);

        //        Member_Profile_SealImg = tools.CheckStr(Request.Form["Member_Profile_SealImg"]);
        //    }
        //}




        //string Member_HeadImg = tools.CheckStr(Request.Form["Member_HeadImg"]);
        //string hidden_type = tools.CheckStr(Request.Form["hidden_type"]);


        //string Member_Company_Introduce = tools.CheckStr(Request.Form["Member_Company_Introduce"]);
        //string Member_Company_Contact = tools.CheckStr(Request.Form["Member_Company_Contact"]);


        ////if (Member_Profile_Contactman == "")
        ////{
        ////    pub.Msg("info", "提示信息", "请将联系人填写完整", false, "{back}");
        ////}
        ////if (Member_Profile_Phone == "")
        ////{
        ////    pub.Msg("info", "提示信息", "请将联系电话填写完整", false, "{back}");
        ////}





        ////if (Member_Profile_County == "0" || Member_Profile_County == "")
        ////{
        ////    pub.Msg("info", "提示信息", "请选择省市区信息", false, "{back}");
        ////}
        //if (Member_Profile_Address == "")
        //{
        //    pub.Msg("info", "提示信息", "请将详细地址填写完整", false, "{back}");
        //}




        //if (Member_Corporate == "")
        //{
        //    pub.Msg("info", "提示信息", "请将法定代表人姓名填写完整", false, "{back}");
        //}




        //if (memberinfo != null)
        //{
        //    //if (memberinfo.Member_Cert_Status == 0)
        //    //{
        //    if (supplierinfo.Supplier_AuditStatus == 0)
        //    {
        //        if (Member_CorporateMobile == "")
        //        {
        //            pub.Msg("info", "提示信息", "请将法定代表人电话填写完整", false, "{back}");
        //        }

        //        if (Member_RegisterFunds == 0)
        //        {
        //            pub.Msg("info", "提示信息", "请将注册资金填写完整", false, "{back}");
        //        }
        //        if (Member_RegisterFunds < 0)
        //        {
        //            pub.Msg("info", "提示信息", "请将注册资金填写完整", false, "{back}");
        //        }




        //        if (Member_Profile_SealImg == "")
        //        {
        //            pub.Msg("info", "提示信息", "请将上传公章图片", false, "{back}");
        //        }
        //    }
        //    else
        //    {
        //        Member_CorporateMobile = profileInfo.Member_CorporateMobile;

        //    }
        //}






        ////MemberInfo entity = GetMemberByID();
        //if (memberinfo != null && memberinfo.MemberProfileInfo != null)
        //{



        //    memberinfo.Member_AuditStatus = 1;
        //    memberinfo.Member_NickName = Member_NickName;
        //    memberinfo.Member_Company_Introduce = Member_Company_Introduce;
        //    memberinfo.Member_Company_Contact = Member_Company_Contact;

        //    if (MyMEM.EditMember(memberinfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe")))
        //    {
        //        memberinfo.MemberProfileInfo.Member_State = Member_Profile_State;
        //        memberinfo.MemberProfileInfo.Member_City = Member_Profile_City;
        //        memberinfo.MemberProfileInfo.Member_County = Member_Profile_County;
        //        memberinfo.MemberProfileInfo.Member_Country = Member_Profile_Country;
        //        memberinfo.MemberProfileInfo.Member_Zip = Member_Profile_Zip;
        //        memberinfo.MemberProfileInfo.Member_StreetAddress = Member_Profile_Address;
        //        memberinfo.MemberProfileInfo.Member_Name = Member_Profile_Contactman;
        //        memberinfo.MemberProfileInfo.Member_Fax = Member_Profile_Fax;
        //        memberinfo.MemberProfileInfo.Member_Phone_Number = Member_Profile_Phone;
        //        memberinfo.MemberProfileInfo.Member_QQ = Member_Profile_QQ;
        //        memberinfo.MemberProfileInfo.Member_HeadImg = Member_HeadImg;


        //        if (supplierinfo != null)
        //        {
        //            if (supplierinfo.Supplier_AuditStatus == 0)
        //            {
        //                memberinfo.MemberProfileInfo.Member_OrganizationCode = Member_Profile_OrganizationCode;
        //                memberinfo.MemberProfileInfo.Member_BusinessCode = Member_Profile_BusinessCode;
        //                memberinfo.MemberProfileInfo.Member_Corporate = Member_Corporate;
        //                memberinfo.MemberProfileInfo.Member_CorporateMobile = Member_CorporateMobile;
        //                memberinfo.MemberProfileInfo.Member_RegisterFunds = Member_RegisterFunds;
        //                memberinfo.MemberProfileInfo.Member_TaxationCode = Member_TaxationCode;
        //                memberinfo.MemberProfileInfo.Member_BankAccountCode = Member_BankAccountCode;

        //                memberinfo.MemberProfileInfo.Member_SealImg = Member_Profile_SealImg;
        //                //entity.MemberProfileInfo.Member_RealName = Member_RealName;
        //                memberinfo.MemberProfileInfo.Member_UniformSocial_Number = Member_UniformSocial_Number;

        //            }
        //        }


        //        MyMEM.EditMemberProfile(memberinfo.MemberProfileInfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));



        //        Session["member_nickname"] = Member_NickName;






        //        //int Supplier_ID = tools.CheckInt(Request.Form["Supplier_ID"]);
        //        string Supplier_Nickname = Member_NickName;
        //        string Supplier_Email = Member_Email;

        //        string Supplier_CompanyName = Member_Company;
        //        string Supplier_County = tools.CheckStr(Request.Form["Member_Profile_County"]);
        //        string Supplier_City = tools.CheckStr(Request.Form["Member_Profile_City"]);
        //        string Supplier_State = tools.CheckStr(Request.Form["Member_Profile_State"]);
        //        string Supplier_Country = tools.CheckStr(Request.Form["Member_Profile_Country"]);
        //        string Supplier_Address = tools.CheckStr(Request.Form["Member_Profile_Address"]);
        //        string Supplier_Phone = tools.CheckStr(Request.Form["Member_Profile_Phone"]);
        //        string Supplier_Fax = tools.CheckStr(Request.Form["Member_Profile_Fax"]);
        //        string Supplier_Zip = tools.CheckStr(Request.Form["Member_Profile_Zip"]);
        //        string Supplier_Contactman = tools.CheckStr(Request.Form["Member_Profile_Contactman"]);
        //        string Supplier_Mobile = tools.CheckStr(Request.Form["Member_Profile_Mobile"]);



        //        if (memberinfo != null)
        //        {
        //            //if (memberinfo.Member_Cert_Status == 0)
        //            //{
        //            if (supplierinfo.Supplier_AuditStatus == 0)
        //            {
        //                Supplier_OrganizationCode = tools.CheckStr(Request.Form["Member_Profile_OrganizationCode"]);
        //                Supplier_Corporate = tools.CheckStr(Request.Form["Member_Corporate"]);
        //                Supplier_CorporateMobile = tools.CheckStr(Request.Form["Member_CorporateMobile"]);
        //                Supplier_RegisterFunds = tools.CheckFloat(Request.Form["Member_RegisterFunds"]);
        //                Supplier_TaxationCode = tools.CheckStr(Request.Form["Member_TaxationCode"]);
        //                Supplier_BankAccountCode = tools.CheckStr(Request.Form["Member_BankAccountCode"]);
        //                Supplier_Mobile = memberinfo.Member_LoginMobile;
        //                Supplier_CompanyName = profileInfo.Member_Company;
        //                Supplier_Nickname = memberinfo.Member_NickName;
        //                Supplier_Email = memberinfo.Member_Email;
        //            }
        //        }


        //        if (Supplier_Corporate == "")
        //        {
        //            Response.Write("请填写法定代表人姓名");
        //            Response.End();
        //        }

        //        if (Supplier_CorporateMobile == "")
        //        {
        //            Response.Write("请填写法定代表人手机号");
        //            Response.End();
        //        }



        //        if (Supplier_Address == "")
        //        {
        //            //pub.Msg("info", "提示信息", "请将详细地址填写完整", false, "{back}");
        //            Response.Write("请将详细地址填写完整");
        //            Response.End();
        //        }


        //        SupplierInfo SupplierEntity = GetSupplierByID();

        //        SupplierEntity.Supplier_Nickname = Supplier_Nickname;
        //        SupplierEntity.Supplier_CompanyName = Supplier_CompanyName;
        //        SupplierEntity.Supplier_Email = Supplier_Email;
        //        SupplierEntity.Supplier_County = Supplier_County;
        //        SupplierEntity.Supplier_City = Supplier_City;
        //        SupplierEntity.Supplier_State = Supplier_State;
        //        SupplierEntity.Supplier_Country = Supplier_Country;
        //        SupplierEntity.Supplier_Address = Supplier_Address;
        //        SupplierEntity.Supplier_Phone = Supplier_Phone;
        //        SupplierEntity.Supplier_Fax = Supplier_Fax;
        //        SupplierEntity.Supplier_Zip = Supplier_Zip;
        //        SupplierEntity.Supplier_Contactman = Supplier_Contactman;
        //        SupplierEntity.Supplier_Mobile = Supplier_Mobile;
        //        SupplierEntity.Supplier_SysEmail = Supplier_SysEmail;
        //        SupplierEntity.Supplier_SysMobile = Supplier_SysMobile;
        //        if (memberinfo != null)
        //        {

        //            if (supplierinfo.Supplier_AuditStatus == 0)
        //            {
        //                SupplierEntity.Supplier_Corporate = Supplier_Corporate;
        //                SupplierEntity.Supplier_CorporateMobile = Supplier_CorporateMobile;
        //                SupplierEntity.Supplier_RegisterFunds = Supplier_RegisterFunds;
        //                SupplierEntity.Supplier_OrganizationCode = Supplier_OrganizationCode;
        //                SupplierEntity.Supplier_TaxationCode = Supplier_TaxationCode;
        //                SupplierEntity.Supplier_BankAccountCode = Supplier_BankAccountCode;
        //            }
        //        }

        //        SupplierEntity.Supplier_IsAuthorize = Supplier_IsAuthorize;
        //        SupplierEntity.Supplier_IsTrademark = Supplier_IsTrademark;
        //        SupplierEntity.Supplier_ServicesPhone = Supplier_ServicesPhone;
        //        SupplierEntity.Supplier_OperateYear = Supplier_OperateYear;
        //        SupplierEntity.Supplier_SaleType = Supplier_SaleType;
        //        SupplierEntity.Supplier_ContactEmail = Supplier_ContactEmail;
        //        SupplierEntity.Supplier_ContactQQ = Supplier_ContactQQ;
        //        SupplierEntity.Supplier_Category = Supplier_Category;




        //        if (MyBLL.EditSupplier(SupplierEntity, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f")))
        //        {
        //            string Supplier_Cert_1, Supplier_Cert_2, Supplier_Cert_3;
        //            int Supplier_CertType;
        //            Supplier_CertType = 0;
        //            Supplier_Cert_1 = "";
        //            Supplier_Cert_2 = "";
        //            Supplier_Cert_3 = "";
        //            Supplier_CertType = tools.CheckInt(Request["Supplier_CertType"]);
        //            IList<SupplierCertInfo> certinfos = null;
        //            SupplierRelateCertInfo ratecate = null;
        //            //= GetSupplierByID();
        //            SupplierInfo entity1 = GetSupplierByID(memberinfo.Member_SupplierID);
        //            if (entity1 != null)
        //            {

        //                certinfos = GetSupplierCertByType(Supplier_CertType);
        //                if (certinfos != null)
        //                {
        //                    foreach (SupplierCertInfo certinfo in certinfos)
        //                    {
        //                        if (tools.CheckStr(Request["Supplier_Cert" + certinfo.Supplier_Cert_ID + "_tmp"]).Length == 0)
        //                        {
        //                            //pub.Msg("info", "提示信息", "请将资质文件上传完整", false, "{back}");
        //                            if (certinfo.Supplier_Cert_Name == "公章")
        //                            {

        //                            }
        //                            else
        //                            {
        //                                pub.Msg("info", "提示信息", "请将资质文件上传完整", false, "{back}");
        //                            }
        //                            // Response.Write("请将资质上传完整");
        //                            //   Response.End();
        //                        }
        //                    }
        //                    //删除资质文件
        //                   MyBLL .DelSupplierRelateCertBySupplierID(entity1.Supplier_ID);
        //                    foreach (SupplierCertInfo certinfo in certinfos)
        //                    {
        //                        ratecate = new SupplierRelateCertInfo();
        //                        ratecate.Cert_SupplierID = entity1.Supplier_ID;
        //                        ratecate.Cert_CertID = certinfo.Supplier_Cert_ID;
        //                        if (tools.CheckStr(Request["Supplier_Cert" + certinfo.Supplier_Cert_ID]).Length == 0)
        //                        {
        //                            ratecate.Cert_Img = tools.CheckStr(Request["Supplier_Cert" + certinfo.Supplier_Cert_ID + "_tmp"]);
        //                            ratecate.Cert_Img1 = tools.CheckStr(Request["Supplier_Cert" + certinfo.Supplier_Cert_ID + "_tmp"]);
        //                        }
        //                        else
        //                        {
        //                            ratecate.Cert_Img = tools.CheckStr(Request["Supplier_Cert" + certinfo.Supplier_Cert_ID]);
        //                            ratecate.Cert_Img1 = tools.CheckStr(Request["Supplier_Cert" + certinfo.Supplier_Cert_ID + "_tmp"]);
        //                        }
        //                        MyBLL.AddSupplierRelateCert(ratecate);
        //                        ratecate = null;
        //                    }
        //                }


        //                entity1.Supplier_CertType = Supplier_CertType;
        //                entity1.Supplier_Cert_Status = 1;
        //                if (MyBLL.EditSupplier(entity1, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f")))
        //                {
        //                    //pub.Msg("positive", "操作成功", "上传完成，请耐心等待审核通过！", true, "/supplier/Supplier_Cert.aspx");
        //                    //  Response.Write("success");
        //                    // Response.End();
        //                }
        //                else
        //                {
        //                    pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
        //                    // Response.Write("failure");
        //                    // Response.End();
        //                }
        //            }
        //            else
        //            {
        //                pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
        //                //Response.Write("failure");
        //                // Response.End();
        //            }

        //            pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/account_profile.aspx");
        //        }
        //        else
        //        {
        //            pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
        //        }
        //    }
        //}
        //else
        //{
        //    pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
        //}

        #endregion
        MemberProfileInfo profileInfo = null;
        SupplierInfo supplierinfo = null;
        int Supplier_AuditStatus = -1;
        int account_id = tools.NullInt(Session["member_accountid"]);
        if (account_id > 0)
        {
            Response.Redirect("/supplier/index.aspx");
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
        MemberInfo memberinfo = new Member().GetMemberByID();
        if (memberinfo != null)
        {
            profileInfo = memberinfo.MemberProfileInfo;
            supplierinfo = GetSupplierByID(memberinfo.Member_SupplierID);
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
            //Member_Email = memberinfo.Member_Email;

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
        if (Member_Profile_Address == "")
        {
            pub.Msg("info", "提示信息", "请将详细地址填写完整", false, "{back}");
        }




        if (Member_Corporate == "")
        {
            pub.Msg("info", "提示信息", "请将法定代表人姓名填写完整", false, "{back}");
        }




        if (memberinfo != null)
        {
            //if (memberinfo.Member_Cert_Status == 0)
            //{
            if (supplierinfo.Supplier_AuditStatus == 0)
            {
                //if (Member_CorporateMobile == "")
                //{
                //    pub.Msg("info", "提示信息", "请将法定代表人电话填写完整", false, "{back}");
                //}

                //if (Member_RegisterFunds == 0)
                //{
                //    pub.Msg("info", "提示信息", "请将注册资金填写完整", false, "{back}");
                //}
                //if (Member_RegisterFunds < 0)
                //{
                //    pub.Msg("info", "提示信息", "请将注册资金填写完整", false, "{back}");
                //}




                //if (Member_Profile_SealImg == "")
                //{
                //    pub.Msg("info", "提示信息", "请将上传公章图片", false, "{back}");
                //}
            }
            else
            {
                Member_CorporateMobile = profileInfo.Member_CorporateMobile;

            }
        }






        //MemberInfo entity = GetMemberByID();
        if (memberinfo != null && memberinfo.MemberProfileInfo != null)
        {



            memberinfo.Member_AuditStatus = 1;
            memberinfo.Member_NickName = Member_NickName;
            memberinfo.Member_Company_Introduce = Member_Company_Introduce;
            memberinfo.Member_Company_Contact = Member_Company_Contact;
            memberinfo.Member_Email = Member_Email;
            if (MyMEM.EditMember(memberinfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe")))
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


                MyMEM.EditMemberProfile(memberinfo.MemberProfileInfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));



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
                    //Response.Write("请填写法定代表人姓名");
                    //Response.End();
                    pub.Msg("info", "提示信息", "请将法定代表人姓名填写完整", false, "{back}");
                }

                //if (Supplier_CorporateMobile == "")
                //{
                //    Response.Write("请填写法定代表人手机号");
                //    Response.End();
                //}



                if (Supplier_Address == "")
                {
                    //pub.Msg("info", "提示信息", "请将详细地址填写完整", false, "{back}");
                    Response.Write("请将详细地址填写完整");
                    Response.End();
                }


                SupplierInfo SupplierEntity = GetSupplierByID();

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




                if (MyBLL.EditSupplier(SupplierEntity, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f")))
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
                    SupplierInfo entity1 = GetSupplierByID(memberinfo.Member_SupplierID);
                    if (entity1 != null)
                    {

                        certinfos = GetSupplierCertByType(Supplier_CertType);
                        if (certinfos != null)
                        {
                            foreach (SupplierCertInfo certinfo in certinfos)
                            {
                                if (tools.CheckStr(Request["Supplier_Cert" + certinfo.Supplier_Cert_ID + "_tmp"]).Length == 0)
                                {
                                    if (certinfo.Supplier_Cert_Name == "合同章(选填)")
                                    {

                                    }
                                    else
                                    {
                                        pub.Msg("info", "提示信息", "请将资质文件上传完整", false, "{back}");
                                    }

                                }
                            }
                            //删除资质文件
                            MyBLL.DelSupplierRelateCertBySupplierID(entity1.Supplier_ID);
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
                                MyBLL.AddSupplierRelateCert(ratecate);
                                ratecate = null;
                            }
                        }


                        entity1.Supplier_CertType = Supplier_CertType;
                        entity1.Supplier_Cert_Status = 1;
                        if (MyBLL.EditSupplier(entity1, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f")))
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

                    pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/account_profile.aspx");
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
    public void UpdateSupplierEdit()
    {
        MemberProfileInfo profileInfo = null;
        int Supplier_AuditStatus = -1;
        int account_id = tools.NullInt(Session["member_accountid"]);
        if (account_id > 0)
        {
            Response.Redirect("/supplier/index.aspx");
        }


        MemberInfo memberinfo = new Member().GetMemberByID();
        SupplierInfo Suppliernfo = null;
        int Member_ID = tools.CheckInt(Request.Form["Member_ID"]);
        // MemberInfo memberInfo = GetMemberByID();
        if (memberinfo != null)
        {
            profileInfo = memberinfo.MemberProfileInfo;
            Suppliernfo = GetSupplierByID(memberinfo.Member_SupplierID);
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

        //if (Member_Profile_CompanyName == "")
        //{
        //    pub.Msg("info", "提示信息", "请将公司名称填写完整", false, "{back}");
        //}


















        MemberInfo entity = new Member().GetMemberByID();
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

            if (MyMEM.EditMember(entity, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe")))
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

                string Member_Company = "";
                MyMEM.EditMemberProfile(entity.MemberProfileInfo, pub.CreateUserPrivilege("079ec5fc-33fe-4d58-a17f-14b5877b4ffe"));
                int Member_Profile_MemberID = tools.CheckInt(DBHelper.ExecuteScalar("select Member_Profile_ID from Member_Profile where Member_Profile_MemberID=" + entity.Member_ID + "").ToString());
                MemberProfileInfo memberprofileinfo = MyMEM.GetMemberProfileByID(Member_Profile_MemberID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                if (memberprofileinfo != null)
                {
                    Member_Company = memberprofileinfo.Member_Company;
                }
                //new Member().GetMemberProfileByID();
                //get


                //supplier.UpdateSupplierInfoByMember_Profile();
                //Modify_Enterprise_Info(entity);
                Session["member_nickname"] = entity.Member_NickName;





                string Supplier_Nickname = tools.CheckStr(Request.Form["Member_NickName"]);
                //string Supplier_Phone = tools.CheckStr(Request.Form["Member_Profile_Phone"]);
                //string Supplier_Profile_QQ = tools.CheckStr(Request.Form["Member_Profile_QQ"]);
                string Supplier_Contactman = tools.CheckStr(Request.Form["Member_Profile_Contactman"]);
                string Supplier_Profile_Phone = tools.CheckStr(Request.Form["Member_Profile_Phone"]);
                //string Supplier_Profile_CompanyName = tools.CheckStr(Request.Form["Member_Profile_CompanyName"]);
                string Supplier_Address = tools.CheckStr(Request.Form["Member_Profile_Address"]);

                string Supplier_Corporate = tools.CheckStr(Request.Form["Member_Corporate"]);
                string Supplier_ContactQQ = tools.CheckStr(Request.Form["Member_Profile_QQ"]);








                if (Supplier_Corporate == "")
                {
                    Response.Write("请填写法定代表人姓名");
                    Response.End();
                }

                if (Supplier_Address == "")
                {
                    //pub.Msg("info", "提示信息", "请将详细地址填写完整", false, "{back}");
                    Response.Write("请将详细地址填写完整");
                    Response.End();
                }




                SupplierInfo SupplierEntity = GetSupplierByID();
                if (SupplierEntity != null)
                {
                    SupplierEntity.Supplier_Nickname = Supplier_Nickname;
                    //SupplierEntity.Supplier_Phone = Supplier_Phone;

                    SupplierEntity.Supplier_Contactman = Supplier_Contactman;
                    SupplierEntity.Supplier_Phone = Supplier_Profile_Phone;
                    SupplierEntity.Supplier_CompanyName = Member_Company;

                    SupplierEntity.Supplier_Address = Supplier_Address;
                    SupplierEntity.Supplier_Corporate = Supplier_Corporate;
                    SupplierEntity.Supplier_ContactQQ = Supplier_ContactQQ;
                    SupplierEntity.Supplier_AuditStatus = 0;
                    SupplierEntity.Supplier_Cert_Status = 0;
                }



                if (MyBLL.EditSupplier(SupplierEntity, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f")))
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
                    SupplierInfo entity1 = GetSupplierByID(memberinfo.Member_SupplierID);
                    if (entity1 != null)
                    {

                        certinfos = GetSupplierCertByType(Supplier_CertType);
                        if (certinfos != null)
                        {
                            foreach (SupplierCertInfo certinfo in certinfos)
                            {
                                if (tools.CheckStr(Request["Supplier_Cert" + certinfo.Supplier_Cert_ID + "_tmp"]).Length == 0)
                                {
                                    //pub.Msg("info", "提示信息", "请将资质文件上传完整", false, "{back}");
                                    if (certinfo.Supplier_Cert_Name == "公章")
                                    {

                                    }
                                    else
                                    {
                                        pub.Msg("info", "提示信息", "请将资质文件上传完整", false, "{back}");
                                    }
                                    // Response.Write("请将资质上传完整");
                                    //   Response.End();
                                }
                            }
                            //删除资质文件
                            MyBLL.DelSupplierRelateCertBySupplierID(entity1.Supplier_ID);
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
                                MyBLL.AddSupplierRelateCert(ratecate);
                                ratecate = null;
                            }
                        }


                        entity1.Supplier_CertType = Supplier_CertType;
                        entity1.Supplier_Cert_Status = 1;
                        if (MyBLL.EditSupplier(entity1, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f")))
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
                    pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/account_profile_edit.aspx");
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

    /// <summary>
    /// 资质类型选择
    /// </summary>
    /// <param name="selectVlaue"></param>
    /// <returns></returns>
    public string DisplaySupplierCertType(int selectVlaue)
    {
        StringBuilder strHTML = new StringBuilder();

        QueryInfo Query = new QueryInfo();

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCertTypeInfo.Cert_Type_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCertTypeInfo.Cert_Type_IsActive", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("SupplierCertTypeInfo.Cert_Type_Sort", "asc"));
        IList<SupplierCertTypeInfo> entitys = MyCertType.GetSupplierCertTypes(Query);

        strHTML.Append("<select name=\"supplier_type\" id=\"supplier_type\" style=\"width: 298px;\">");
        strHTML.Append("<option value='0'>请选择供货商类型</option>");
        if (entitys != null)
        {
            foreach (SupplierCertTypeInfo entity in entitys)
            {
                if (entity.Cert_Type_ID == selectVlaue)
                {
                    strHTML.Append("<option value='" + entity.Cert_Type_ID + "' selected>" + entity.Cert_Type_Name + "</option>");
                }
                else
                {
                    strHTML.Append("<option value='" + entity.Cert_Type_ID + "'>" + entity.Cert_Type_Name + "</option>");
                }
            }
        }
        strHTML.Append("</select>");
        return strHTML.ToString();
    }

    /// <summary>
    /// 添加银行账户
    /// </summary>
    public virtual void AddSupplierBank()
    {

        int Supplier_Bank_SupplierID = tools.NullInt(Session["supplier_id"]);
        string Supplier_Bank_Name = tools.CheckStr(Request.Form["Supplier_Bank_Name"]);
        string Supplier_Bank_NetWork = tools.CheckStr(Request.Form["Supplier_Bank_NetWork"]);
        string Supplier_Bank_SName = tools.CheckStr(Request.Form["Supplier_Bank_SName"]);
        string Supplier_Bank_Account = tools.CheckStr(Request.Form["Supplier_Bank_Account"]);
        string Supplier_Bank_Attachment = tools.CheckStr(Request.Form["Supplier_Bank_Attachment"]);
        if (Supplier_Bank_Name == "" || Supplier_Bank_NetWork == "" || Supplier_Bank_SName == "" || Supplier_Bank_Account == "")
        {
            pub.Msg("info", "提示信息", "请将信息填写完整", false, "{back}");
        }
        SupplierBankInfo entity = MyBank.GetSupplierBankBySupplierID(Supplier_Bank_SupplierID);
        if (entity != null)
        {
            entity.Supplier_Bank_SupplierID = Supplier_Bank_SupplierID;
            entity.Supplier_Bank_Name = Supplier_Bank_Name;
            entity.Supplier_Bank_NetWork = Supplier_Bank_NetWork;
            entity.Supplier_Bank_SName = Supplier_Bank_SName;
            entity.Supplier_Bank_Account = Supplier_Bank_Account;
            entity.Supplier_Bank_Attachment = Supplier_Bank_Attachment;

            if (MyBank.EditSupplierBank(entity))
            {
                pub.Msg("positive", "操作成功", "操作成功", true, "my_account.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            entity = new SupplierBankInfo();
            entity.Supplier_Bank_SupplierID = Supplier_Bank_SupplierID;
            entity.Supplier_Bank_Name = Supplier_Bank_Name;
            entity.Supplier_Bank_NetWork = Supplier_Bank_NetWork;
            entity.Supplier_Bank_SName = Supplier_Bank_SName;
            entity.Supplier_Bank_Account = Supplier_Bank_Account;
            entity.Supplier_Bank_Attachment = Supplier_Bank_Attachment;

            if (MyBank.AddSupplierBank(entity))
            {
                pub.Msg("positive", "操作成功", "操作成功", true, "my_account.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
    }

    /// <summary>
    /// 获取指定供应商银行账户信息
    /// </summary>
    /// <param name="ID">供应商ID</param>
    /// <returns>银行账户信息</returns>
    public SupplierBankInfo GetSupplierBankInfoBySupplierID(int ID)
    {
        return MyBank.GetSupplierBankBySupplierID(ID);
    }

    /// <summary>
    /// 账户退款申请列表
    /// </summary>
    public void Shop_PayBack_Apply_List()
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        Pageurl = "?action=list";
        if (curpage < 1)
        {
            curpage = 1;
        }

        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <th align=\"center\" valign=\"middle\">退款类型</th>");
        Response.Write("  <th align=\"center\" valign=\"middle\">申请退款金额</th>");
        Response.Write("  <th width=\"70\" align=\"center\" valign=\"middle\">审核状态</th>");
        Response.Write("  <th width=\"130\" align=\"center\" valign=\"middle\">申请时间</th>");
        Response.Write("  <th width=\"70\" align=\"center\" valign=\"middle\">操作</th>");
        Response.Write("</tr>");
        string productURL = string.Empty;
        string checkstatus = "";
        string payback_type = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPayBackApplyInfo.Supplier_PayBack_Apply_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPayBackApplyInfo.Supplier_PayBack_Apply_SupplierID", "=", supplier_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierPayBackApplyInfo.Supplier_PayBack_Apply_ID", "Desc"));
        IList<SupplierPayBackApplyInfo> entitys = MyPayBackApply.GetSupplierPayBackApplys(Query, pub.CreateUserPrivilege("b90823db-e737-4ae9-b428-1494717b85c7"));
        PageInfo page = MyPayBackApply.GetPageInfo(Query, pub.CreateUserPrivilege("b90823db-e737-4ae9-b428-1494717b85c7"));
        if (entitys != null)
        {
            foreach (SupplierPayBackApplyInfo entity in entitys)
            {
                i = i + 1;
                if (entity.Supplier_PayBack_Apply_Status == 1)
                {
                    checkstatus = "已审核";
                }
                else if (entity.Supplier_PayBack_Apply_Status == 2)
                {
                    checkstatus = "审核不通过";
                }
                else
                {
                    checkstatus = "未审核";
                }
                if (entity.Supplier_PayBack_Apply_Type == 1)
                {
                    payback_type = "会员费";
                }
                else if (entity.Supplier_PayBack_Apply_Type == 2)
                {
                    payback_type = "保证金";
                }
                else
                {
                    payback_type = "推广费";
                }
                Response.Write("<tr bgcolor=\"#ffffff\">");
                Response.Write("<td height=\"35\" align=\"left\" class=\"comment_td_bg\" style=\"padding-left:10px;\" valign=\"middle\">" + payback_type + "</a></td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + pub.FormatCurrency(entity.Supplier_PayBack_Apply_Amount) + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + checkstatus + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + entity.Supplier_PayBack_Apply_Addtime + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">");
                Response.Write(" <a href=\"/supplier/Supplier_PayBack_Apply_View.aspx?apply_id=" + entity.Supplier_PayBack_Apply_ID + "\" style=\"color:#3366cc;\"><img src=\"/images/icon_vieworder.gif\" border=\"0\" alt=\"查看\"></a>");
                Response.Write("</td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" align=\"center\" cellpadding=\"2\"><tr><td align=\"right\" height=\"10\"></td></tr><tr><td align=\"right\" style=\"padding-right:10px;\"><div class=\"page\" style=\"float:right;\">");
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
            Response.Write("</div></td></tr></table>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#ffffff\"><td width=\"70\" height=\"35\" colspan=\"6\" align=\"center\" valign=\"middle\">没有记录</td></tr>");
        }
        Response.Write("</table>");
    }

    /// <summary>
    /// 申请账户退款
    /// </summary>
    public virtual void AddSupplierPayBackApply()
    {
        int Supplier_PayBack_Apply_Type = tools.CheckInt(Request.Form["Supplier_PayBack_Apply_Type"]);
        double Supplier_PayBack_Apply_Amount = tools.CheckFloat(Request.Form["Supplier_PayBack_Apply_Amount"]);
        string Supplier_PayBack_Apply_Note = tools.CheckStr(Request.Form["Supplier_PayBack_Apply_Note"]);
        int Supplier_PayBack_Apply_SupplierID = 0;
        if (Supplier_PayBack_Apply_Amount == 0)
        {
            pub.Msg("info", "提示信息", "请填写申请退款金额！", false, "{back}");
        }
        if (Supplier_PayBack_Apply_Note.Length == 0)
        {
            pub.Msg("info", "提示信息", "请填写申请退款备注！", false, "{back}");
        }
        SupplierInfo supplierinfo = GetSupplierByID();
        if (supplierinfo != null)
        {
            Supplier_PayBack_Apply_SupplierID = supplierinfo.Supplier_ID;
            if (Supplier_PayBack_Apply_Type == 1 && Supplier_PayBack_Apply_Amount > supplierinfo.Supplier_Account)
            {
                pub.Msg("info", "提示信息", "申请退款金额超过现有会员费余额！", false, "{back}");
            }
            if (Supplier_PayBack_Apply_Type == 2 && Supplier_PayBack_Apply_Amount > supplierinfo.Supplier_Security_Account)
            {
                pub.Msg("info", "提示信息", "申请退款金额超过现有保证金余额！", false, "{back}");
            }
            if (Supplier_PayBack_Apply_Type == 3 && Supplier_PayBack_Apply_Amount > supplierinfo.Supplier_Adv_Account)
            {
                pub.Msg("info", "提示信息", "申请退款金额超过现有推广费余额！", false, "{back}");
            }
        }


        SupplierPayBackApplyInfo entity = new SupplierPayBackApplyInfo();
        entity.Supplier_PayBack_Apply_SupplierID = Supplier_PayBack_Apply_SupplierID;
        entity.Supplier_PayBack_Apply_Type = Supplier_PayBack_Apply_Type;
        entity.Supplier_PayBack_Apply_Amount = Supplier_PayBack_Apply_Amount;
        entity.Supplier_PayBack_Apply_Note = Supplier_PayBack_Apply_Note;
        entity.Supplier_PayBack_Apply_Addtime = DateTime.Now;
        entity.Supplier_PayBack_Apply_Status = 0;
        entity.Supplier_PayBack_Apply_AdminAmount = 0;
        entity.Supplier_PayBack_Apply_AdminNote = "";
        entity.Supplier_PayBack_Apply_AdminTime = DateTime.Now;
        entity.Supplier_PayBack_Apply_Site = pub.GetCurrentSite();

        if (MyPayBackApply.AddSupplierPayBackApply(entity))
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_PayBack_Apply.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    /// <summary>
    /// 获取指定账户退款信息
    /// </summary>
    /// <param name="Apply_ID">账户退款申请ID</param>
    /// <returns>账户退款信息</returns>
    public virtual SupplierPayBackApplyInfo GetSupplierPayBackApplyByID(int Apply_ID)
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);

        SupplierPayBackApplyInfo entity = MyPayBackApply.GetSupplierPayBackApplyByID(Apply_ID, pub.CreateUserPrivilege("b90823db-e737-4ae9-b428-1494717b85c7"));
        if (entity != null)
        {
            if (supplier_id != entity.Supplier_PayBack_Apply_SupplierID)
            {
                entity = null;
            }
        }
        return entity;
    }

    /// <summary>
    /// 添加店铺关闭申请
    /// </summary>
    public virtual void AddSupplierCloseShopApply()
    {
        int CloseShop_Apply_SupplierID = 0;
        string CloseShop_Apply_Note = tools.CheckStr(Request.Form["CloseShop_Apply_Note"]);
        int CloseShop_Apply_Status = 0;
        SupplierInfo supplierinfo = GetSupplierByID();
        if (supplierinfo != null)
        {
            CloseShop_Apply_SupplierID = supplierinfo.Supplier_ID;
        }
        SupplierCloseShopApplyInfo entity = new SupplierCloseShopApplyInfo();
        entity.CloseShop_Apply_SupplierID = CloseShop_Apply_SupplierID;
        entity.CloseShop_Apply_Note = CloseShop_Apply_Note;
        entity.CloseShop_Apply_Status = CloseShop_Apply_Status;
        entity.CloseShop_Apply_AdminNote = "";
        entity.CloseShop_Apply_Addtime = DateTime.Now;
        entity.CloseShop_Apply_AdminTime = DateTime.Now;
        entity.CloseShop_Apply_Site = pub.GetCurrentSite();

        if (MyCloseShopApply.AddSupplierCloseShopApply(entity))
        {
            pub.Msg("positive", "温馨提示", "您的申请已提交！（我们的工作人员会尽快与您取得联系，请保持手机畅通！）", true, "Supplier_CloseShop_Apply_add.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    /// <summary>
    /// 获取指定店铺关闭申请信息
    /// </summary>
    /// <returns>店铺关闭申请</returns>
    public virtual SupplierCloseShopApplyInfo GetSupplierCloseShopApplyByID()
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        SupplierCloseShopApplyInfo entity = null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCloseShopApplyInfo.CloseShop_Apply_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCloseShopApplyInfo.CloseShop_Apply_SupplierID", "=", supplier_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierCloseShopApplyInfo.CloseShop_Apply_ID", "Desc"));
        IList<SupplierCloseShopApplyInfo> entitys = MyCloseShopApply.GetSupplierCloseShopApplys(Query, pub.CreateUserPrivilege("81e0af57-348d-4565-9e73-7146b3116b8c"));
        if (entitys != null)
        {
            entity = entitys[0];
        }
        return entity;
    }

    /// <summary>
    /// 获取指定商家类型的资质文件
    /// </summary>
    /// <param name="Supplier_Type">商家类型</param>
    /// <returns>资质文件信息</returns>
    public virtual IList<SupplierCertInfo> GetSupplierCertByType(int Supplier_Type)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCertInfo.Supplier_Cert_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCertInfo.Supplier_Cert_Type", "=", Supplier_Type.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierCertInfo.Supplier_Cert_Sort", "Desc"));
        return MyCert.GetSupplierCerts(Query, pub.CreateUserPrivilege("29f32a17-8d3f-4ca5-9628-524316760713"));

    }

    public string GetSupplierShopImg()
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        string shopImg = "";

        SupplierShopInfo shopInfo = MyShop.GetSupplierShopBySupplierID(supplier_id);
        if (shopInfo != null)
        {
            shopImg = shopInfo.Shop_Img;
        }

        return pub.FormatImgURL(shopImg, "fullpath");
    }

    public string GetSuppleirGrade()
    {
        int supplier_id = tools.NullInt(Request["supplier_id"]);
        SupplierInfo supplierInfo = GetSupplierByID();
        string gradeName = "普通";
        SupplierGradeInfo entity = null;
        if (supplierInfo != null)
        {
            entity = MySGrade.GetSupplierGradeByID(supplierInfo.Supplier_GradeID, pub.CreateUserPrivilege("1d3f7ace-2191-4c5e-9403-840ddaf191c0"));
        }
        return gradeName;
    }

    public string GetSuppleirGrade(int supplier_id)
    {
        SupplierInfo supplierInfo = GetSupplierByID(supplier_id);
        string gradeName = "普通";
        SupplierGradeInfo entity = null;
        if (supplierInfo != null)
        {
            entity = MySGrade.GetSupplierGradeByID(supplierInfo.Supplier_GradeID, pub.CreateUserPrivilege("1d3f7ace-2191-4c5e-9403-840ddaf191c0"));
        }
        return gradeName;
    }

    public string GetSupplierName(int supplier_id)
    {
        SupplierShopInfo shopInfo = MyShop.GetSupplierShopBySupplierID(supplier_id);
        string supplierName = "";

        if (shopInfo != null)
        {
            supplierName = shopInfo.Shop_Name;
        }
        return supplierName;
    }

    public string GetSupplierShopInfo(string Merchant_ID)
    {
        StringBuilder strHTML = new StringBuilder();

        int supplier_id = tools.CheckInt(Merchant_ID.Replace("sz_", "")) - 1000;

        SupplierInfo entity = GetSupplierByID(supplier_id);
        strHTML.Append("<div class=\"introduction\">");
        if (entity != null)
        {
            SupplierShopInfo shopInfo = MyShop.GetSupplierShopBySupplierID(entity.Supplier_ID);

            if (shopInfo != null)
            {
                strHTML.Append("<p class=\"introduction_img\"><img src=\"" + pub.FormatImgURL(shopInfo.Shop_Img, "fullpath") + "\" width=\"168\" height=\"168\" /></p>");
                strHTML.Append("<p><a href=\"http://" + shopInfo.Shop_Domain + Application["Shop_Second_Domain"] + "\">" + entity.Supplier_CompanyName + "</a></p>");
                strHTML.Append("<p><a href=\"http://" + shopInfo.Shop_Domain + Application["Shop_Second_Domain"] + "\">" + entity.Supplier_Contactman + "</a></p>");
                strHTML.Append("<p>入住时间：" + entity.Supplier_Addtime.ToString("yyyy年MM月dd日") + "</p>");
                strHTML.Append("<p>公司地址：" + addr.DisplayAddress(entity.Supplier_State, "", "") + "</p>");
                strHTML.Append("<p>入驻第<b style=\"color:red;\">" + pub.DateDiffYear(shopInfo.Shop_Addtime, DateTime.Now) + "</b>年</p>");
                strHTML.Append("<p>交易等级：<img src=\"/images/icon05.png\"><img src=\"/images/icon05.png\"><img src=\"/images/icon05.png\"><img src=\"/images/icon05.png\"><img src=\"/images/icon05.png\"></p>");
                strHTML.Append("<p>满意度：" + Shop_evalues(entity.Supplier_ID) + "</p>");
            }
        }
        strHTML.Append("</div>");

        return strHTML.ToString();
    }

    public string GetSupplierShopProduct(string Merchant_ID)
    {
        StringBuilder strHTML = new StringBuilder();

        int supplier_id = tools.CheckInt(Merchant_ID.Replace("sz_", "")) - 1000;


        int i = 0;
        SupplierInfo entity = GetSupplierByID(supplier_id);
        strHTML.Append("<div class=\"selling_01\">");

        if (entity != null)
        {
            SupplierShopInfo shopInfo = MyShop.GetSupplierShopBySupplierID(entity.Supplier_ID);

            if (shopInfo != null)
            {
                IList<ProductInfo> entitys = GetSupplierProduct(entity.Supplier_ID);
                if (entitys != null)
                {
                    foreach (ProductInfo productInfo in entitys)
                    {
                        i++;

                        if (i % 2 == 0)
                        {
                            strHTML.Append("<div class=\"selling_info\"  style=\"margin-left:5px;\">");
                        }
                        else
                        {
                            strHTML.Append("<div class=\"selling_info\">");
                        }

                        strHTML.Append("<div class=\"selling_info_img\"><img src=\"" + pub.FormatImgURL(productInfo.Product_Img, "thumbnail") + "\" width=\"122\" height=\"122\" /></div>");
                        strHTML.Append("<div class=\"selling_info_text\">");
                        strHTML.Append("<p>批发价：");

                        if (productInfo.Product_PriceType == 1)
                        {
                            strHTML.Append("<font>" + pub.FormatCurrency(productInfo.Product_Price) + "</font>");
                        }
                        else
                        {
                            strHTML.Append("<font>" + pub.FormatCurrency(pub.GetProductPrice(productInfo.Product_ManualFee, productInfo.Product_Weight)) + "</font>");
                        }
                        strHTML.Append("</p>");
                        strHTML.Append("<p style=\"height:32px;line-height:32px;overflow:hidden;width:112px;\" title=\"" + productInfo.Product_Name + "\">" + productInfo.Product_Name + "</p>");
                        //strHTML.Append("<p>" + shopInfo.Shop_Name + "</p>");
                        strHTML.Append("</div>");
                        strHTML.Append("</div>");

                        if (i % 2 == 0)
                        {
                            strHTML.Append("</div>");
                            strHTML.Append("<div class=\"selling_01\">");
                        }
                    }
                }
            }
        }
        strHTML.Append("</div>");
        return strHTML.ToString();
    }

    public IList<ProductInfo> GetSupplierProduct(int Supplier_ID)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 4;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));
        //聚合是否列表显示 暂时屏蔽掉
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsListShow", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_SupplierID", "=", Supplier_ID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "desc"));
        IList<ProductInfo> entitys = MyProduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));

        return entitys;
    }

    #endregion

    #region "商家店铺"

    public void Goto_Shop()
    {
        string go_url = "/";
        SupplierShopInfo entity = GetSupplierShopBySupplierID(tools.NullInt(Session["supplier_id"]));
        if (entity != null)
        {
            go_url = "http://" + entity.Shop_Domain + Application["Shop_Second_Domain"] + "/";
        }
        Response.Redirect(go_url);
    }

    /// <summary>
    /// 获得商铺地址
    /// </summary>
    /// <param name="ShopDomain">商铺二级域名</param>
    /// <returns></returns>
    public string GetShopURL(string ShopDomain)
    {
        return "http://" + ShopDomain + Application["Shop_Second_Domain"] + "/";
    }

    /// <summary>
    /// 获得商铺域名
    /// </summary>
    /// <returns></returns>
    public string GetShopDomain()
    {
        return tools.NullStr(Application["Shop_Second_Domain"]);
    }

    public string Get_Shop_Type(int Type)
    {
        string Shop_Type = "";
        switch (Type)
        {
            case 1:
                Shop_Type = "体验店铺";
                break;
            case 2:
                Shop_Type = "展示店铺";
                break;
            case 3:
                Shop_Type = "销售店铺";
                break;
        }
        return Shop_Type;
    }

    /// <summary>
    /// 获取供应商店铺开通申请
    /// </summary>
    /// <param name="ID">供应商编号</param>
    public SupplierShopApplyInfo GetSupplierShopApplyBySupplierID(int ID)
    {
        return MyShopApply.GetSupplierShopApplyBySupplierID(ID);
    }

    /// <summary>
    /// 店铺开通申请提交
    /// </summary>
    public void Supplier_Shop_Apply_Add()
    {
        int Shop_Apply_ID = tools.CheckInt(Request.Form["Shop_Apply_ID"]);
        int Shop_Apply_SupplierID = tools.NullInt(Session["supplier_id"]);
        int Shop_Apply_ShopType = tools.CheckInt(Request.Form["Shop_Apply_ShopType"]);
        string Shop_Apply_Name = tools.CheckStr(Request.Form["Shop_Apply_Name"]);
        string Shop_Apply_PINCode = tools.CheckStr(Request.Form["Shop_Apply_PINCode"]);
        string Shop_Apply_Mobile = tools.CheckStr(Request.Form["Shop_Apply_Mobile"]);
        string Shop_Apply_ShopName = tools.CheckStr(Request.Form["Shop_Apply_ShopName"]);
        string Shop_Apply_CompanyType = tools.CheckStr(Request.Form["Shop_Apply_CompanyType"]);
        string Shop_Apply_Lawman = tools.CheckStr(Request.Form["Shop_Apply_Lawman"]);
        string Shop_Apply_CertCode = tools.CheckStr(Request.Form["Shop_Apply_CertCode"]);
        string Shop_Apply_CertAddress = tools.CheckStr(Request.Form["Shop_Apply_CertAddress"]);
        string Shop_Apply_CompanyAddress = tools.CheckStr(Request.Form["Shop_Apply_CompanyAddress"]);
        string Shop_Apply_CompanyPhone = tools.CheckStr(Request.Form["Shop_Apply_CompanyPhone"]);
        string Shop_Apply_Certification1 = tools.CheckStr(Request.Form["supplier_cert1"]);
        string Shop_Apply_Certification2 = tools.CheckStr(Request.Form["supplier_cert2"]);
        string Shop_Apply_Certification3 = tools.CheckStr(Request.Form["supplier_cert3"]);
        string Shop_Apply_Certification4 = tools.CheckStr(Request.Form["supplier_cert4"]);
        string Shop_Apply_Certification5 = tools.CheckStr(Request.Form["supplier_cert5"]);
        string Shop_Apply_MainBrand = tools.CheckStr(Request.Form["Shop_Apply_MainBrand"]);
        int Shop_Apply_Status = 0;

        if (Shop_Apply_Name.Length == 0 || Shop_Apply_PINCode.Length == 0 || Shop_Apply_Mobile.Length == 0 || Shop_Apply_ShopName.Length == 0 || Shop_Apply_CompanyType.Length == 0 || Shop_Apply_Lawman.Length == 0 || Shop_Apply_CertCode.Length == 0 || Shop_Apply_CertAddress.Length == 0 || Shop_Apply_CompanyAddress.Length == 0 || Shop_Apply_CompanyPhone.Length == 0)
        {
            pub.Msg("error", "错误信息", "请将申请信息填写完整！", false, "{back}");
        }

        if (pub.CheckIDCard(Shop_Apply_PINCode) == false)
        {
            pub.Msg("error", "错误信息", "无效的身份证号码！", false, "{back}");
        }

        if (pub.Checkmobile(Shop_Apply_Mobile) == false)
        {
            pub.Msg("error", "错误信息", "手机号码格式错误！", false, "{back}");
        }

        //if (Shop_Apply_Certification1.Length == 0 || Shop_Apply_Certification2.Length == 0 || Shop_Apply_Certification3.Length == 0)
        //{
        //    pub.Msg("error", "错误信息", "请将资质证明上传完整！", false, "{back}");
        //}

        SupplierShopApplyInfo entity = GetSupplierShopApplyBySupplierID(Shop_Apply_SupplierID);
        if (entity == null)
        {
            entity = new SupplierShopApplyInfo();
            entity.Shop_Apply_ID = Shop_Apply_ID;
            entity.Shop_Apply_SupplierID = Shop_Apply_SupplierID;
            entity.Shop_Apply_ShopType = Shop_Apply_ShopType;
            entity.Shop_Apply_Name = Shop_Apply_Name;
            entity.Shop_Apply_PINCode = Shop_Apply_PINCode;
            entity.Shop_Apply_Mobile = Shop_Apply_Mobile;
            entity.Shop_Apply_ShopName = Shop_Apply_ShopName;
            entity.Shop_Apply_CompanyType = Shop_Apply_CompanyType;
            entity.Shop_Apply_Lawman = Shop_Apply_Lawman;
            entity.Shop_Apply_CertCode = Shop_Apply_CertCode;
            entity.Shop_Apply_CertAddress = Shop_Apply_CertAddress;
            entity.Shop_Apply_CompanyAddress = Shop_Apply_CompanyAddress;
            entity.Shop_Apply_CompanyPhone = Shop_Apply_CompanyPhone;
            entity.Shop_Apply_Certification1 = Shop_Apply_Certification1;
            entity.Shop_Apply_Certification2 = Shop_Apply_Certification2;
            entity.Shop_Apply_Certification3 = Shop_Apply_Certification3;
            entity.Shop_Apply_Certification4 = Shop_Apply_Certification4;
            entity.Shop_Apply_Certification5 = Shop_Apply_Certification5;
            entity.Shop_Apply_MainBrand = Shop_Apply_MainBrand;
            entity.Shop_Apply_Status = Shop_Apply_Status;
            entity.Shop_Apply_Addtime = DateTime.Now;

            if (MyShopApply.AddSupplierShopApply(entity))
            {
                SupplierInfo supplierinfo = GetSupplierByID();
                if (supplierinfo != null)
                {
                    supplierinfo.Supplier_IsApply = 1;
                }
                MyBLL.EditSupplier(supplierinfo, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f"));
                pub.Msg("positive", "操作成功", "店铺开通申请提交成功，请等待平台审核！审核通过后，请重新登录后查看！", true, "index.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            if (entity.Shop_Apply_Status == 1)
            {
                pub.Msg("error", "错误信息", "您暂时无法使用该功能", false, "/supplier/index.aspx");
            }
            entity.Shop_Apply_Name = Shop_Apply_Name;
            entity.Shop_Apply_PINCode = Shop_Apply_PINCode;
            entity.Shop_Apply_ShopType = Shop_Apply_ShopType;
            entity.Shop_Apply_Mobile = Shop_Apply_Mobile;
            entity.Shop_Apply_ShopName = Shop_Apply_ShopName;
            entity.Shop_Apply_CompanyType = Shop_Apply_CompanyType;
            entity.Shop_Apply_Lawman = Shop_Apply_Lawman;
            entity.Shop_Apply_CertCode = Shop_Apply_CertCode;
            entity.Shop_Apply_CertAddress = Shop_Apply_CertAddress;
            entity.Shop_Apply_CompanyAddress = Shop_Apply_CompanyAddress;
            entity.Shop_Apply_CompanyPhone = Shop_Apply_CompanyPhone;
            entity.Shop_Apply_Certification1 = Shop_Apply_Certification1;
            entity.Shop_Apply_Certification2 = Shop_Apply_Certification2;
            entity.Shop_Apply_Certification3 = Shop_Apply_Certification3;
            entity.Shop_Apply_Certification4 = Shop_Apply_Certification4;
            entity.Shop_Apply_Certification5 = Shop_Apply_Certification5;
            entity.Shop_Apply_MainBrand = Shop_Apply_MainBrand;
            entity.Shop_Apply_Status = Shop_Apply_Status;

            if (MyShopApply.EditSupplierShopApply(entity))
            {
                SupplierInfo supplierinfo = GetSupplierByID();
                if (supplierinfo != null)
                {
                    supplierinfo.Supplier_IsApply = 1;
                }
                MyBLL.EditSupplier(supplierinfo, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f"));
                pub.Msg("positive", "操作成功", "店铺开通申请提交成功，请等待平台审核！", true, "index.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
    }

    /// <summary>
    /// 店铺资质信息
    /// </summary>
    public void Supplier_Shop_Cert()
    {
        int Shop_Apply_ID = tools.CheckInt(Request.Form["Shop_Apply_ID"]);
        int Shop_Apply_SupplierID = tools.NullInt(Session["supplier_id"]);
        string Shop_Apply_Certification1 = tools.CheckStr(Request.Form["supplier_cert1"]);
        string Shop_Apply_Certification2 = tools.CheckStr(Request.Form["supplier_cert2"]);
        string Shop_Apply_Certification3 = tools.CheckStr(Request.Form["supplier_cert3"]);
        string Shop_Apply_Certification4 = tools.CheckStr(Request.Form["supplier_cert4"]);
        string Shop_Apply_Certification5 = tools.CheckStr(Request.Form["supplier_cert5"]);
        int Shop_Apply_Status = 0;

        //if (Shop_Apply_Name.Length == 0 || Shop_Apply_PINCode.Length == 0 || Shop_Apply_Mobile.Length == 0 || Shop_Apply_ShopName.Length == 0 || Shop_Apply_CompanyType.Length == 0 || Shop_Apply_Lawman.Length == 0 || Shop_Apply_CertCode.Length == 0 || Shop_Apply_CertAddress.Length == 0 || Shop_Apply_CompanyAddress.Length == 0 || Shop_Apply_CompanyPhone.Length == 0)
        //{
        //    pub.Msg("error", "错误信息", "请将申请信息填写完整！", false, "{back}");
        //}
        if (Shop_Apply_Certification1.Length == 0 || Shop_Apply_Certification2.Length == 0 || Shop_Apply_Certification3.Length == 0)
        {
            pub.Msg("error", "错误信息", "请将资质证明上传完整！", false, "{back}");
        }

        SupplierShopApplyInfo entity = GetSupplierShopApplyBySupplierID(Shop_Apply_SupplierID);
        if (entity == null)
        {
            entity = new SupplierShopApplyInfo();
            entity.Shop_Apply_ID = Shop_Apply_ID;
            entity.Shop_Apply_SupplierID = Shop_Apply_SupplierID;
            entity.Shop_Apply_Certification1 = Shop_Apply_Certification1;
            entity.Shop_Apply_Certification2 = Shop_Apply_Certification2;
            entity.Shop_Apply_Certification3 = Shop_Apply_Certification3;
            entity.Shop_Apply_Certification4 = Shop_Apply_Certification4;
            entity.Shop_Apply_Certification5 = Shop_Apply_Certification5;
            entity.Shop_Apply_Addtime = DateTime.Now;

            if (MyShopApply.AddSupplierShopApply(entity))
            {
                pub.Msg("positive", "操作成功", "店铺资质提交成功！", true, "index.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {

            entity.Shop_Apply_Certification1 = Shop_Apply_Certification1;
            entity.Shop_Apply_Certification2 = Shop_Apply_Certification2;
            entity.Shop_Apply_Certification3 = Shop_Apply_Certification3;
            entity.Shop_Apply_Certification4 = Shop_Apply_Certification4;
            entity.Shop_Apply_Certification5 = Shop_Apply_Certification5;

            if (MyShopApply.EditSupplierShopApply(entity))
            {

                pub.Msg("positive", "操作成功", "店铺资质提交成功！", true, "index.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
    }

    /// <summary>
    /// 根据商家编号获取店铺开通申请信息
    /// </summary>
    /// <param name="ID">商家编号</param>
    /// <returns></returns>
    public SupplierShopInfo GetSupplierShopBySupplierID(int ID)
    {
        return MyShop.GetSupplierShopBySupplierID(ID);
    }

    /// <summary>
    /// 店铺banner选择
    /// </summary>
    /// <returns></returns>
    public string Display_Shop_Banner(int Shop_Banner)
    {
        string result = "";
        int num = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopBannerInfo.Shop_Banner_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopBannerInfo.Shop_Banner_IsActive", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopBannerInfo.Shop_Banner_ID", "Desc"));
        IList<SupplierShopBannerInfo> entitys = MyShopBanner.GetSupplierShopBanners(Query, pub.CreateUserPrivilege("daff677a-1be4-4438-b1e8-32b453275341"));

        if (entitys != null)
        {
            result = "<tr>";
            foreach (SupplierShopBannerInfo entity in entitys)
            {
                num = num + 1;

                result += "<td width=\"33%\">";
                result += "<div>";
                result += "<img src=\"" + pub.FormatImgURL(entity.Shop_Banner_Url, "fullpath") + "\" width=\"200\" height=\"31\">";
                result += "</div>";
                result += "<div style=\"padding-top:5px;\">";
                result += "<input type=\"radio\" name=\"shop_banner\" align=\"absmiddle\" value=\"" + entity.Shop_Banner_ID + "\" " + pub.CheckRadio(Shop_Banner.ToString(), entity.Shop_Banner_ID.ToString()) + "> " + entity.Shop_Banner_Name;
                result += "</div>";
                result += "</td>";
                if (num % 3 == 0)
                {
                    result += "</tr><tr>";
                }
            }
            result += "</tr>";
        }
        return result;
    }

    /// <summary>
    /// 店铺样式选择
    /// </summary>
    /// <returns></returns>
    public string Display_Shop_Css(int Shop_Css, int Shop_Type)
    {
        string result = "";
        int num = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopCssInfo.Shop_Css_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopCssInfo.Shop_Css_IsActive", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopCssInfo.Shop_Css_ID", "ASC"));
        IList<SupplierShopCssInfo> entitys = MyShopCss.GetSupplierShopCsss(Query, pub.CreateUserPrivilege("3396b3c6-8116-4c3b-9682-6d29c937947e"));

        if (entitys != null)
        {
            result = "<tr>";
            foreach (SupplierShopCssInfo entity in entitys)
            {
                if ((",," + entity.Shop_Css_Target + ",").IndexOf("," + Shop_Type + ",") > 0 || CheckCssRelateSupplier(entity.Shop_Css_ID, tools.NullInt(Session["supplier_id"])))
                {
                    num = num + 1;

                    result += "<td width=\"184\">";
                    result += "<div>";
                    result += "<img src=\"" + pub.FormatImgURL(entity.Shop_Css_Img, "fullpath") + "\" width=\"170\" height=\"291\">";
                    result += "</div>";
                    result += "<div style=\"padding-top:5px;\">";
                    result += "<input type=\"radio\" name=\"shop_css\" align=\"absmiddle\" value=\"" + entity.Shop_Css_ID + "\" " + pub.CheckRadio(Shop_Css.ToString(), entity.Shop_Css_ID.ToString()) + " style=\"display:inline-block; vertical-align:middle; padding:0; border:none;-webkit-box-shadow:none; -moz-box-shadow:none;\"> " + entity.Shop_Css_Title;
                    result += "</div>";
                    result += "</td>";
                    if (num % 4 == 0)
                    {
                        result += "</tr><tr>";
                    }
                }
            }
            result += "</tr>";
        }
        return result;
    }

    public bool CheckCssRelateSupplier(int Css_ID, int Supplier_ID)
    {
        bool result = false;
        IList<SupplierShopCssRelateSupplierInfo> entitys = MyShopCss.GetSupplierShopCssRelateSuppliersByCss(Css_ID);
        if (entitys != null)
        {
            foreach (SupplierShopCssRelateSupplierInfo entity in entitys)
            {
                if (Supplier_ID == entity.Relate_SupplierID)
                {
                    result = true;
                }
            }
        }
        return result;
    }

    //店铺域名申请列表
    public void Shop_Domain_List()
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        Pageurl = "?action=list";
        if (curpage < 1)
        {
            curpage = 1;
        }

        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" class=\"zkw_table\" style=\"background:#dadada;\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <th align=\"center\" valign=\"middle\">域名</th>");
        Response.Write("  <th width=\"80\" align=\"center\" valign=\"middle\">域名类型</th>");
        Response.Write("  <th width=\"130\" align=\"center\" valign=\"middle\">审核状态</th>");
        Response.Write("  <th width=\"130\" align=\"center\" valign=\"middle\">申请时间</th>");
        Response.Write("  <th width=\"70\" align=\"center\" valign=\"middle\">操作</th>");
        Response.Write("</tr>");
        string productURL = string.Empty;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopDomainInfo.Shop_Domain_SupplierID", "=", supplier_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopDomainInfo.Shop_Domain_ID", "Desc"));
        IList<SupplierShopDomainInfo> entitys = MyDomain.GetSupplierShopDomains(Query);
        PageInfo page = MyDomain.GetPageInfo(Query);
        if (entitys != null)
        {
            foreach (SupplierShopDomainInfo entity in entitys)
            {
                i = i + 1;
                Response.Write("<tr bgcolor=\"#ffffff\">");

                if (entity.Shop_Domain_Type == 1)
                {
                    Response.Write("<td width=\"200\" height=\"35\" align=\"left\" class=\"comment_td_bg\" style=\"padding-left:10px;\" valign=\"middle\">http://<span class=\"t12_red\">" + entity.Shop_Domain_Name + "</span></td>");
                    Response.Write("<td width=\"130\" height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">顶级域名</td>");
                }
                else
                {
                    Response.Write("<td width=\"200\" height=\"35\" align=\"left\" class=\"comment_td_bg\" style=\"padding-left:10px;\" valign=\"middle\">http://<span class=\"t12_red\">" + entity.Shop_Domain_Name + "</span>" + GetShopDomain() + "</td>");
                    Response.Write("<td width=\"130\" height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">默认域名</td>");
                }
                if (entity.Shop_Domain_Status == 0)
                {
                    Response.Write("<td width=\"130\" height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">未审核</td>");
                }
                else
                {
                    Response.Write("<td width=\"130\" height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">已审核</td>");
                }
                Response.Write("<td width=\"130\" height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\"><a>" + entity.Shop_Domain_Addtime + "</a></td>");
                Response.Write("<td width=\"130\" height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\"><a href=\"/supplier/supplier_shop_Domain_do.aspx?action=remove&domain_id=" + entity.Shop_Domain_ID + "\" style=\"color:#3366cc;\">删除</a>");
                Response.Write("</td>");
                Response.Write("</tr>");


            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" align=\"center\" cellpadding=\"2\"><tr><td align=\"right\" height=\"10\"></td></tr><tr><td align=\"right\"><div class=\"page\" style=\"float:right;\">");
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
            Response.Write("</div></td></tr></table>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#ffffff\"><td width=\"70\" height=\"35\" colspan=\"5\" align=\"center\" valign=\"middle\">没有记录</td></tr>");
        }
        Response.Write("</table>");
    }

    //删除指定店铺域名申请
    public void Shop_Domain_Del()
    {
        int domain_id = tools.CheckInt(Request["domain_id"]);
        if (domain_id > 0)
        {
            SupplierShopDomainInfo entity = MyDomain.GetSupplierShopDomainByID(domain_id);
            if (entity != null)
            {
                if (entity.Shop_Domain_SupplierID == tools.NullInt(Session["supplier_id"]) && entity.Shop_Domain_Status == 0)
                {
                    MyDomain.DelSupplierShopDomain(domain_id);
                    pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_Domain.aspx");
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
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void AddSupplierShopDomain()
    {

        string Shop_Domain_Name = tools.CheckStr(Request.Form["Shop_Domain_Name"]);
        int Shop_Domain_Type = tools.CheckInt(Request["Shop_Domain_Type"]);
        if (Shop_Domain_Name == "")
        {
            pub.Msg("info", "提示信息", "请填写申请的店铺域名！", false, "{back}");
        }
        SupplierShopInfo shopinfo = MyShop.GetSupplierShopBySupplierID(tools.NullInt(Session["supplier_id"]));
        if (shopinfo == null)
        {
            if (shopinfo.Shop_Type > 1 && Shop_Domain_Type == 1)
            {
                Shop_Domain_Type = 0;
            }
        }
        SupplierShopDomainInfo entity = new SupplierShopDomainInfo();
        entity.Shop_Domain_SupplierID = tools.NullInt(Session["supplier_id"]);
        entity.Shop_Domain_ShopID = tools.NullInt(Session["supplier_shopid"]);
        entity.Shop_Domain_Name = Shop_Domain_Name;
        entity.Shop_Domain_Type = Shop_Domain_Type;
        entity.Shop_Domain_Status = 0;
        entity.Shop_Domain_Addtime = DateTime.Now;
        entity.Shop_Domain_Site = pub.GetCurrentSite();



        if (MyDomain.AddSupplierShopDomain(entity))
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_Domain.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }

    }

    //根据版面标识与供应商获取版面信息
    public SupplierShopPagesInfo GetSupplierShopPagesByIDSign(string Sign, int Supplier_ID)
    {
        return MyShopPages.GetSupplierShopPagesByIDSign(Sign, Supplier_ID);
    }

    //店铺配置
    public void Supplier_Shop_Config()
    {
        //int Shop_Css = tools.CheckInt(Request.Form["Shop_Css"]);
        //int Shop_Banner = tools.CheckInt(Request.Form["Shop_Banner"]);
        string Shop_Img = tools.CheckStr(Request.Form["Shop_Img"]);
        string Shop_Banner_Title = tools.CheckStr(Request.Form["Shop_Banner_Title"]);
        string Shop_Banner_Title_Family = tools.CheckStr(Request.Form["Shop_Banner_Title_Family"]);
        int Shop_Banner_Title_Size = tools.CheckInt(Request.Form["Shop_Banner_Title_Size"]);
        int Shop_Banner_Title_LeftPadding = tools.CheckInt(Request.Form["Shop_Banner_Title_LeftPadding"]);
        string Shop_banner_Title_color = tools.CheckStr(Request.Form["Shop_banner_Title_color"]);
        string Shop_Banner_Img = tools.CheckStr(Request.Form["Shop_Banner_Img"]);
        string Shop_MainProduct = tools.CheckStr(Request.Form["Shop_MainProduct"]);
        string Shop_Domain = tools.CheckStr(Request.Form["Shop_Domain"]);
        string Shop_CateID = tools.CheckStr(Request.Form["Shop_CateID"]);
        string Shop_SEO_Title = tools.CheckStr(Request.Form["Shop_SEO_Title"]);
        string Shop_SEO_Keyword = tools.CheckStr(Request.Form["Shop_SEO_Keyword"]);
        string Shop_SEO_Description = tools.CheckStr(Request.Form["Shop_SEO_Description"]);
        string[] cateArray = Shop_CateID.Split(',');

        SupplierShopInfo entity = GetSupplierShopBySupplierID(tools.NullInt(Session["supplier_id"]));
        if (entity != null)
        {
            //entity.Shop_Css = Shop_Css;
            //entity.Shop_Banner = Shop_Banner;
            entity.Shop_Img = Shop_Img;
            entity.Shop_Banner_Title = Shop_Banner_Title;
            entity.Shop_Banner_Title_Family = Shop_Banner_Title_Family;
            entity.Shop_Banner_Title_Size = Shop_Banner_Title_Size;
            entity.Shop_Banner_Title_LeftPadding = Shop_Banner_Title_LeftPadding;
            entity.Shop_banner_Title_color = Shop_banner_Title_color;
            entity.Shop_Banner_Img = Shop_Banner_Img;
            entity.Shop_MainProduct = Shop_MainProduct;
            entity.Shop_SEO_Title = Shop_SEO_Title;
            entity.Shop_SEO_Keyword = Shop_SEO_Keyword;
            entity.Shop_SEO_Description = Shop_SEO_Description;

            if (MyShop.EditSupplierShop(entity))
            {
                MyShop.SaveShopCategory(entity.Shop_ID, cateArray);
                pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_Config.aspx");
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

    //店铺面板配置
    public void Supplier_Shop_Model()
    {
        int Shop_Css = tools.CheckInt(Request.Form["Shop_Css"]);

        SupplierShopInfo entity = GetSupplierShopBySupplierID(tools.NullInt(Session["supplier_id"]));
        if (entity != null)
        {
            entity.Shop_Css = Shop_Css;


            if (MyShop.EditSupplierShop(entity))
            {

                pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_Model.aspx");
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

    //店铺Banner配置
    public void Supplier_Shop_Banner()
    {
        int Shop_Banner = tools.CheckInt(Request.Form["Shop_Banner"]);
        string Shop_Banner_Img = tools.CheckStr(Request.Form["Shop_Banner_Img"]);
        SupplierShopInfo entity = GetSupplierShopBySupplierID(tools.NullInt(Session["supplier_id"]));
        if (entity != null)
        {
            entity.Shop_Banner = Shop_Banner;
            entity.Shop_Banner_Img = Shop_Banner_Img;

            if (MyShop.EditSupplierShop(entity))
            {

                pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_CustomModule.aspx");
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

    //店铺升级
    public void Supplier_Shop_UpGrade()
    {
        int Shop_Type = tools.CheckInt(Request["Shop_Type"]);
        bool upgrade_status = false;
        SupplierInfo entity = GetSupplierByID();
        if (entity != null)
        {
            SupplierShopInfo shopinfo = GetSupplierShopBySupplierID(entity.Supplier_ID);
            if (shopinfo != null)
            {
                if (upgrade_status)
                {
                    shopinfo.Shop_Type = Shop_Type;
                    MyShop.EditSupplierShop(shopinfo);
                }
            }
        }
        if (upgrade_status)
        {
            pub.Msg("positive", "操作成功", "店铺升级成功！", true, "Supplier_Shop_Config.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "店铺升级失败，请稍后重试！", false, "{back}");
        }
    }

    //获取店铺分类
    public string GetShopCategory(int Shop_ID)
    {
        return MyShop.GetShopCategory(Shop_ID);
    }

    public string GetShopCategoryName(int Shop_ID)
    {
        string[] strCate = GetShopCategory(Shop_ID).Split(',');
        CategoryInfo cateInfo = null;
        string cateName = "";

        foreach (string item in strCate)
        {
            cateInfo = MyCBLL.GetCategoryByID(tools.CheckInt(item), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
            if (cateInfo != null)
            {
                cateName = cateName + cateInfo.Cate_Name;
            }
        }

        return cateName;
    }

    //店铺版面保存
    public virtual void SupplierShopPagesSave()
    {
        int Shop_Pages_ID = 0;
        string Shop_Pages_Title = tools.CheckStr(Request.Form["Shop_Pages_Title"]);
        int Shop_Pages_SupplierID = tools.NullInt(Session["supplier_id"]);
        string Shop_Pages_Sign = tools.CheckStr(Request.Form["Shop_Pages_Sign"]);
        string Shop_Pages_Content = Request.Form["Shop_Pages_Content"];
        int Shop_Pages_IsActive = tools.CheckInt(Request.Form["Shop_Pages_IsActive"]);
        int Shop_Pages_Sort = tools.NullInt(Request.Form["Shop_Pages_Sort"]);
        string Shop_Pages_Site = pub.GetCurrentSite();
        Shop_Pages_Sign = Shop_Pages_Sign.ToUpper();
        switch (Shop_Pages_Sign)
        {
            case "INDEX":
                Shop_Pages_Title = "店铺首页右侧自定义";
                break;
            case "INDEXLEFT":
                Shop_Pages_Title = "店铺首页左侧自定义";
                break;
            case "INDEXTOP":
                Shop_Pages_Title = "店铺首页通栏";
                break;
            case "INTRO":
                Shop_Pages_Title = "店铺介绍";
                break;
            case "SERVICE":
                Shop_Pages_Title = "店铺服务条款";
                break;
            case "PAYDELIVERY":
                Shop_Pages_Title = "支付配送说明";
                break;
        }
        if (Shop_Pages_Title.Length == 0)
        {
            pub.Msg("error", "错误信息", "请填写版面标题！", false, "{back}");
        }
        if (Shop_Pages_Sign.Length == 0)
        {
            Shop_Pages_Sign = pub.CreatevkeySign(6);
        }

        if (Shop_Pages_Sign != "INDEX" && Shop_Pages_Sign != "INDEXLEFT" && Shop_Pages_Sign != "INDEXTOP" && Shop_Pages_Sign != "INTRO" && Shop_Pages_Sign != "SERVICE" && Shop_Pages_Sign != "PAYDELIVERY")
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 10;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopPagesInfo.Shop_Pages_Site", "=", pub.GetCurrentSite()));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopPagesInfo.Shop_Pages_Sign", "<>", "INDEX"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopPagesInfo.Shop_Pages_Sign", "<>", "INDEXLEFT"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopPagesInfo.Shop_Pages_Sign", "<>", "INDEXTOP"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopPagesInfo.Shop_Pages_Sign", "<>", "INTRO"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopPagesInfo.Shop_Pages_Sign", "<>", "SERVICE"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopPagesInfo.Shop_Pages_Sign", "<>", "PAYDELIVERY"));
            Query.OrderInfos.Add(new OrderInfo("SupplierShopPagesInfo.Shop_Pages_ID", "Desc"));
            IList<SupplierShopPagesInfo> entitys = MyShopPages.GetSupplierShopPagess(Query);
            if (entitys != null)
            {
                if (entitys.Count >= 4)
                {
                    pub.Msg("error", "错误信息", "您最多只能添加四个自定义版面！", false, "{back}");
                }
            }
        }

        SupplierShopPagesInfo entity = MyShopPages.GetSupplierShopPagesByIDSign(Shop_Pages_Sign, Shop_Pages_SupplierID);
        if (entity != null)
        {
            entity.Shop_Pages_Title = Shop_Pages_Title;
            entity.Shop_Pages_SupplierID = Shop_Pages_SupplierID;
            entity.Shop_Pages_Sign = Shop_Pages_Sign;
            entity.Shop_Pages_Ischeck = 1;
            entity.Shop_Pages_IsActive = Shop_Pages_IsActive;
            entity.Shop_Pages_Content = Shop_Pages_Content;
            entity.Shop_Pages_Sort = Shop_Pages_Sort;

            if (MyShopPages.EditSupplierShopPages(entity))
            {
                if (Shop_Pages_Sign == "INDEX" || Shop_Pages_Sign == "INDEXLEFT" || Shop_Pages_Sign == "INDEXTOP" || Shop_Pages_Sign == "INTRO" || Shop_Pages_Sign == "SERVICE" || Shop_Pages_Sign == "PAYDELIVERY")
                {
                    if (entity.Shop_Pages_Sign == "INDEX" || entity.Shop_Pages_Sign == "INDEXLEFT" || entity.Shop_Pages_Sign == "INDEXTOP")
                    {
                        pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_CustomModule.aspx");
                    }
                    else
                    {
                        pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_Pages_List.aspx");
                    }
                }
                else
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_Pages_List.aspx");
                }
            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            entity = new SupplierShopPagesInfo();
            entity.Shop_Pages_ID = Shop_Pages_ID;
            entity.Shop_Pages_Title = Shop_Pages_Title;
            entity.Shop_Pages_SupplierID = Shop_Pages_SupplierID;
            entity.Shop_Pages_Sign = Shop_Pages_Sign;
            entity.Shop_Pages_Content = Shop_Pages_Content;
            entity.Shop_Pages_Ischeck = 1;
            entity.Shop_Pages_IsActive = Shop_Pages_IsActive;
            entity.Shop_Pages_Addtime = DateTime.Now;
            entity.Shop_Pages_Site = Shop_Pages_Site;

            if (MyShopPages.AddSupplierShopPages(entity))
            {
                pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_Pages_List.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
    }

    //店铺版面保存
    public virtual void EditSupplierShopPages()
    {
        int Shop_Pages_ID = tools.NullInt(Request.Form["Shop_Pages_ID"]);
        string Shop_Pages_Title = tools.CheckStr(Request.Form["Shop_Pages_Title"]);
        int Shop_Pages_SupplierID = tools.NullInt(Session["supplier_id"]);
        string Shop_Pages_Sign = tools.CheckStr(Request.Form["Shop_Pages_Sign"]);
        string Shop_Pages_Content = Request.Form["Shop_Pages_Content"];
        int Shop_Pages_Sort = tools.NullInt(Request.Form["Shop_Pages_Sort"]);
        string Shop_Pages_Site = pub.GetCurrentSite();
        Shop_Pages_Sign = Shop_Pages_Sign.ToUpper();

        if (Shop_Pages_Title.Length == 0)
        {
            pub.Msg("error", "错误信息", "请填写版面标题！", false, "{back}");
        }
        //if (Shop_Pages_Sign.Length == 0)
        //{
        //    Shop_Pages_Sign.Length = pub.CreatevkeySign(6);
        //}

        if (Shop_Pages_Sign != "INDEX" && Shop_Pages_Sign != "INDEXLEFT" && Shop_Pages_Sign != "INDEXTOP" && Shop_Pages_Sign != "INTRO" || Shop_Pages_Sign != "SERVICE" || Shop_Pages_Sign != "PAYDELIVERY")
        {
            SupplierShopPagesInfo entity = MyShopPages.GetSupplierShopPagesByID(Shop_Pages_ID);
            if (entity != null)
            {
                if (Shop_Pages_SupplierID == entity.Shop_Pages_SupplierID)
                {
                    entity.Shop_Pages_ID = Shop_Pages_ID;
                    entity.Shop_Pages_Title = Shop_Pages_Title;
                    entity.Shop_Pages_SupplierID = Shop_Pages_SupplierID;
                    //entity.Shop_Pages_Sign = Shop_Pages_Sign;
                    entity.Shop_Pages_Content = Shop_Pages_Content;
                    entity.Shop_Pages_Sort = Shop_Pages_Sort;

                    if (MyShopPages.EditSupplierShopPages(entity))
                    {
                        if (entity.Shop_Pages_Sign == "INDEX" || entity.Shop_Pages_Sign == "INDEXLEFT" || entity.Shop_Pages_Sign == "INDEXTOP")
                        {
                            pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_CustomModule.aspx");
                        }
                        else
                        {
                            pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_Pages_List.aspx");
                        }
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

    //店铺版面删除
    public void SupplierShopPages_Del()
    {
        string Shop_Pages_Sign = tools.CheckStr(Request["sign"]);
        if (Shop_Pages_Sign == "INDEX" || Shop_Pages_Sign == "INDEXLEFT" || Shop_Pages_Sign == "INDEXTOP" || Shop_Pages_Sign == "INTRO" || Shop_Pages_Sign == "SERVICE" || Shop_Pages_Sign == "PAYDELIVERY")
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
        else
        {
            SupplierShopPagesInfo entity = GetSupplierShopPagesByIDSign(Shop_Pages_Sign, tools.NullInt(Session["supplier_id"]));
            if (entity != null)
            {
                if (MyShopPages.DelSupplierShopPages(entity.Shop_Pages_ID) > 0)
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_Pages_List.aspx");
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
    }

    //店铺版面列表
    public void Shop_Pages_List()
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        Pageurl = "?action=list";
        if (curpage < 1)
        {
            curpage = 1;
        }

        Response.Write("<table   width=\"973\" border=\"0\" cellspacing=\"2\" cellpadding=\"0\" class=\"table02\">");
        Response.Write("<tr>");
        Response.Write("  <td width=\"220\" class=\"name\">版面标题</td>");
        Response.Write("  <td width=\"220\" class=\"name\">版面标识</td>");
        Response.Write("  <td width=\"167\" class=\"name\">审核状态</td>");
        Response.Write("  <td width=\"76\" class=\"name\">排序</td>");
        Response.Write("  <td width=\"130\" class=\"name\">添加时间</td>");
        Response.Write("  <td width=\"160\" class=\"name\">操作</td>");
        Response.Write("</tr>");
        string productURL = string.Empty;
        string checkstatus = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopPagesInfo.Shop_Pages_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopPagesInfo.Shop_Pages_Sign", "<>", "INDEX"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopPagesInfo.Shop_Pages_Sign", "<>", "INDEXTOP"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopPagesInfo.Shop_Pages_Sign", "<>", "INDEXLEFT"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopPagesInfo.Shop_Pages_SupplierID", "=", supplier_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopPagesInfo.Shop_Pages_ID", "Desc"));
        IList<SupplierShopPagesInfo> entitys = MyShopPages.GetSupplierShopPagess(Query);
        PageInfo page = MyShopPages.GetPageInfo(Query);
        if (entitys != null)
        {
            foreach (SupplierShopPagesInfo entity in entitys)
            {
                i = i + 1;
                if (entity.Shop_Pages_Ischeck == 1)
                {
                    checkstatus = "已审核";
                }
                else
                {
                    checkstatus = "未审核";
                }

                if (i % 2 == 0)
                {
                    Response.Write("<tr class=\"bg\">");
                }
                else
                {
                    Response.Write("<tr>");
                }
                Response.Write("<td height=\"35\" align=\"left\" class=\"comment_td_bg\" style=\"padding-left:10px;\" valign=\"middle\">" + tools.CutStr(entity.Shop_Pages_Title, 50) + "</a></td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + entity.Shop_Pages_Sign + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + checkstatus + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + entity.Shop_Pages_Sort + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + entity.Shop_Pages_Addtime + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">");
                if (entity.Shop_Pages_Sign == "INDEX")
                {
                    Response.Write("<span><a href=\"/supplier/Supplier_Shop_Index.aspx\" class=\"a12\">修改</a></span>");
                }
                else if (entity.Shop_Pages_Sign == "INDEXLEFT")
                {
                    Response.Write("<span><a href=\"/supplier/Supplier_Shop_Left.aspx\" class=\"a12\">修改</a></span>");
                }
                else if (entity.Shop_Pages_Sign == "INDEXTOP")
                {
                    Response.Write("<span><a href=\"/supplier/Supplier_Shop_Top.aspx\" class=\"a12\">修改</a></span>");
                }
                else if (entity.Shop_Pages_Sign == "INTRO")
                {
                    Response.Write("<span><a href=\"/supplier/Supplier_Shop_Intro.aspx\" class=\"a12\">修改</a></span>");
                }
                else if (entity.Shop_Pages_Sign == "SERVICE")
                {
                    Response.Write("<span><a href=\"/supplier/Supplier_Shop_Service.aspx\" class=\"a12\">修改</a></span>");
                }
                else if (entity.Shop_Pages_Sign == "PAYDELIVERY")
                {
                    Response.Write("<span><a href=\"/supplier/Supplier_Shop_Delivery.aspx\" class=\"a12\">修改</a></span>");
                }
                else
                {
                    Response.Write("<span><a href=\"/supplier/Supplier_Shop_Pages_Edit.aspx?sign=" + entity.Shop_Pages_Sign + "\" class=\"a12\">修改</a></span>");
                }
                if (entity.Shop_Pages_Sign == "INDEX" || entity.Shop_Pages_Sign == "INTRO" || entity.Shop_Pages_Sign == "SERVICE" || entity.Shop_Pages_Sign == "PAYDELIVERY")
                {
                    Response.Write("");
                }
                else
                {
                    Response.Write("<span> <a href=\"/supplier/Supplier_Shop_Pages_do.aspx?action=pagesdel&sign=" + entity.Shop_Pages_Sign + "\">删除</a></span>");
                }
                Response.Write("</td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");

            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
        }
        else
        {
            Response.Write("<tr><td colspan=\"6\">没有记录</td></tr>");
            Response.Write("</table>");
        }
    }

    //店铺活动文章列表
    public void Shop_Article_List()
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        Pageurl = "?action=list";
        if (curpage < 1)
        {
            curpage = 1;
        }

        Response.Write("<table width=\"973\" cellpadding=\"0\" align=\"center\" cellspacing=\"2\" class=\"table02\" >");
        Response.Write("<tr>");
        Response.Write("  <td width=\"220\" class=\"name\">活动标题</td>");
        Response.Write("  <td width=\"164\" class=\"name\">审核状态</td>");
        Response.Write("  <td width=\"216\" class=\"name\">浏览数</td>");
        Response.Write("  <td width=\"141\" class=\"name\">添加时间</td>");
        Response.Write("  <td width=\"220\" class=\"name\" style=\"border-right:none;\">操作</td>");
        Response.Write("</tr>");
        string productURL = string.Empty;
        string checkstatus = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopArticleInfo.Shop_Article_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopArticleInfo.Shop_Article_SupplierID", "=", supplier_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopArticleInfo.Shop_Article_ID", "Desc"));
        IList<SupplierShopArticleInfo> entitys = MyShopArticle.GetSupplierShopArticles(Query);
        PageInfo page = MyShopArticle.GetPageInfo(Query);
        if (entitys != null)
        {
            foreach (SupplierShopArticleInfo entity in entitys)
            {
                i = i + 1;

                if (entity.Shop_Article_IsActive == 1)
                {
                    checkstatus = "已审核";
                }
                else
                {
                    checkstatus = "未审核";
                }

                if (i % 2 == 0)
                {
                    Response.Write("<tr class=\"bg\">");
                }
                else
                {
                    Response.Write("<tr>");
                }
                Response.Write("<td>" + tools.CutStr(entity.Shop_Article_Title, 50) + "</a></td>");
                Response.Write("<td>" + checkstatus + "</td>");
                Response.Write("<td>" + entity.Shop_Article_Hits + "</td>");
                Response.Write("<td>" + entity.Shop_Article_Addtime + "</td>");
                Response.Write("<td>");
                Response.Write("<span><a href=\"/supplier/Supplier_Shop_Article_Edit.aspx?Article_ID=" + entity.Shop_Article_ID + "\" class=\"a12\">修改</a></span>");
                Response.Write("<span> <a href=\"/supplier/Supplier_Shop_Pages_do.aspx?action=articledel&Article_ID=" + entity.Shop_Article_ID + "\">删除</a></span>");
                Response.Write("</td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");

            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
        }
        else
        {
            Response.Write("<tr><td  colspan=\"5\" align=\"center\" valign=\"middle\">没有记录</td></tr>");
        }
        Response.Write("</table>");
    }

    //店铺活动公告保存
    public virtual void SupplierShopArticleSave()
    {
        int Shop_Article_ID = tools.NullInt(Request["Shop_Article_ID"]); ;
        string Shop_Article_Title = tools.CheckStr(Request["Shop_Article_Title"]);
        int Shop_Article_SupplierID = tools.NullInt(Session["supplier_id"]);
        string Shop_Article_Content = Request["Shop_Article_Content"];
        string Shop_Article_Site = pub.GetCurrentSite();
        if (Shop_Article_Title.Length == 0)
        {
            pub.Msg("error", "错误信息", "请填写公告标题！", false, "{back}");
        }

        SupplierShopArticleInfo entity = MyShopArticle.GetSupplierShopArticleByIDSupplier(Shop_Article_ID, Shop_Article_SupplierID);
        if (entity != null)
        {
            entity.Shop_Article_ID = Shop_Article_ID;
            entity.Shop_Article_Title = Shop_Article_Title;
            entity.Shop_Article_SupplierID = Shop_Article_SupplierID;
            entity.Shop_Article_IsActive = 0;
            entity.Shop_Article_Content = Shop_Article_Content;

            if (MyShopArticle.EditSupplierShopArticle(entity))
            {
                pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_Article_List.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            entity = new SupplierShopArticleInfo();
            entity.Shop_Article_ID = Shop_Article_ID;
            entity.Shop_Article_Title = Shop_Article_Title;
            entity.Shop_Article_SupplierID = Shop_Article_SupplierID;
            entity.Shop_Article_Hits = 0;
            entity.Shop_Article_Content = Shop_Article_Content;
            entity.Shop_Article_IsActive = 0;
            entity.Shop_Article_Addtime = DateTime.Now;
            entity.Shop_Article_Site = Shop_Article_Site;

            if (MyShopArticle.AddSupplierShopArticle(entity))
            {
                pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_Article_List.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
    }

    //获取指定店铺活动信息
    public SupplierShopArticleInfo GetSupplierShopArticleByID(int ID)
    {
        return MyShopArticle.GetSupplierShopArticleByID(ID);
    }

    //获取指定供应商、指定店铺活动信息
    public SupplierShopArticleInfo GetSupplierShopArticleByIDSupplier(int ID, int Supplier_ID)
    {
        return MyShopArticle.GetSupplierShopArticleByIDSupplier(ID, Supplier_ID);
    }

    //店铺活动公告删除
    public void SupplierShopArticle_Del()
    {
        int Article_ID = tools.CheckInt(Request["Article_ID"]);
        if (Article_ID == 0)
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
        else
        {
            SupplierShopArticleInfo entity = GetSupplierShopArticleByIDSupplier(Article_ID, tools.NullInt(Session["supplier_id"]));
            if (entity != null)
            {
                if (MyShopArticle.DelSupplierShopArticle(Article_ID) > 0)
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_Article_List.aspx");
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
    }

    //店铺评价列表
    public void Shop_Evaluate_List()
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        Pageurl = "?action=list";
        if (curpage < 1)
        {
            curpage = 1;
        }

        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <th width=\"295\" align=\"center\" valign=\"middle\">评价内容</th>");
        Response.Write("  <th width=\"150\" align=\"center\" valign=\"middle\">评价人</th>");
        Response.Write("  <th align=\"center\" valign=\"middle\">商品信息</th>");
        Response.Write("</tr>");
        string productURL = string.Empty;
        string checkstatus = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierShopEvaluateInfo.Shop_Evaluate_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_SupplierID", "=", supplier_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("SupplierShopEvaluateInfo.Shop_Evaluate_ID", "Desc"));
        IList<SupplierShopEvaluateInfo> entitys = MyShopEvaluate.GetSupplierShopEvaluates(Query);
        PageInfo page = MyShopEvaluate.GetPageInfo(Query);
        if (entitys != null)
        {
            foreach (SupplierShopEvaluateInfo entity in entitys)
            {
                i = i + 1;
                Response.Write("<tr bgcolor=\"#ffffff\">");
                Response.Write("<td height=\"35\" align=\"left\" class=\"comment_td_bg\" style=\"padding-left:10px;\" valign=\"middle\">" + entity.Shop_Evaluate_Note + "<bR>[" + entity.Shop_Evaluate_Addtime + "]</a></td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + GetMemberNickName(entity.Shop_Evaluate_MemberID) + "</td>");
                Response.Write("<td height=\"35\" align=\"left\" class=\"comment_td_bg\" valign=\"middle\">" + Get_Evaluate_Product_Price(entity.Shop_Evaluate_Productid) + "</td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" align=\"center\" cellpadding=\"2\"><tr><td align=\"right\" height=\"10\"></td></tr><tr><td align=\"right\"><div class=\"page\" style=\"float:right;\">");
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
            Response.Write("</div></td></tr></table>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#ffffff\"><td width=\"70\" height=\"35\" colspan=\"3\" align=\"center\" valign=\"middle\">没有记录</td></tr>");
        }
        Response.Write("</table>");
    }

    //配送方式列表
    public void Delivery_Way_List()
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        Pageurl = "?action=list";
        if (curpage < 1)
        {
            curpage = 1;
        }

        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <th align=\"center\" valign=\"middle\">配送方式名称</th>");
        Response.Write("  <th width=\"250\" align=\"center\" valign=\"middle\">运费</th>");
        Response.Write("  <th width=\"50\" align=\"center\" valign=\"middle\">状态</th>");
        Response.Write("  <th width=\"50\" align=\"center\" valign=\"middle\">排序</th>");
        Response.Write("  <th width=\"80\" align=\"center\" valign=\"middle\">设置区域</th>");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\">操作</th>");
        Response.Write("</tr>");
        string productURL = string.Empty;
        string checkstatus = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "DeliveryWayInfo.Delivery_Way_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "DeliveryWayInfo.Delivery_Way_SupplierID", "=", supplier_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("DeliveryWayInfo.Delivery_Way_ID", "Desc"));
        IList<DeliveryWayInfo> entitys = MyDeliveryway.GetDeliveryWays(Query, pub.CreateUserPrivilege("837c9372-3b25-494f-b141-767e195e3c88"));
        PageInfo page = MyDeliveryway.GetPageInfo(Query, pub.CreateUserPrivilege("837c9372-3b25-494f-b141-767e195e3c88"));
        if (entitys != null)
        {
            foreach (DeliveryWayInfo entity in entitys)
            {
                i = i + 1;
                Response.Write("<tr bgcolor=\"#ffffff\">");
                Response.Write("<td height=\"35\" align=\"left\" class=\"comment_td_bg\" style=\"padding-left:10px;\" valign=\"middle\">" + entity.Delivery_Way_Name + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">");
                if (entity.Delivery_Way_FeeType == 0)
                {
                    Response.Write(pub.FormatCurrency(entity.Delivery_Way_Fee));
                }
                else
                {
                    Response.Write("首重费用：" + pub.FormatCurrency(entity.Delivery_Way_InitialFee) + " 续重费用：" + pub.FormatCurrency(entity.Delivery_Way_UpFee));
                }
                Response.Write("</td>");

                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">");
                if (entity.Delivery_Way_Status == 0)
                {
                    Response.Write("关闭");
                }
                else
                {
                    Response.Write("启用");
                }
                Response.Write("</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + entity.Delivery_Way_Sort + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\"><a href=\"/supplier/Delivery_Way_Area_List.aspx?delivery_way_id=" + entity.Delivery_Way_ID + "\">设置区域</a.</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">");
                Response.Write("<a href=\"/supplier/Delivery_Way_Edit.aspx?delivery_way_id=" + entity.Delivery_Way_ID + "\" style=\"color:#3366cc;\">修改</a>");
                Response.Write(" <a href=\"/supplier/Delivery_Way_do.aspx?action=del&delivery_way_id=" + entity.Delivery_Way_ID + "\" style=\"color:#3366cc;\">删除</a>");
                Response.Write("</td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" align=\"center\" cellpadding=\"6\"><tr><td align=\"right\" height=\"10\"></td></tr><tr><td align=\"right\">");
            Response.Write("<div class=\"page\" style=\"float:right;\">");
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
            Response.Write("</div></td></tr></table>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#ffffff\"><td width=\"70\" height=\"35\" colspan=\"6\" align=\"center\" valign=\"middle\">没有记录</td></tr>");
            Response.Write("</table>");
        }

    }

    //添加配送方式
    public void AddDeliveryWay()
    {
        int Delivery_Way_ID = tools.CheckInt(Request.Form["Delivery_Way_ID"]);
        string Delivery_Way_Name = tools.CheckStr(Request.Form["Delivery_Way_Name"]);
        int Delivery_Way_Sort = tools.CheckInt(Request.Form["Delivery_Way_Sort"]);
        int Delivery_Way_InitialWeight = tools.CheckInt(Request.Form["Delivery_Way_InitialWeight"]);
        int Delivery_Way_UpWeight = tools.CheckInt(Request.Form["Delivery_Way_UpWeight"]);
        int Delivery_Way_FeeType = tools.CheckInt(Request.Form["Delivery_Way_FeeType"]);
        double Delivery_Way_Fee = tools.CheckFloat(Request.Form["Delivery_Way_Fee"]);
        double Delivery_Way_InitialFee = tools.CheckFloat(Request.Form["Delivery_Way_InitialFee"]);
        double Delivery_Way_UpFee = tools.CheckFloat(Request.Form["Delivery_Way_UpFee"]);
        int Delivery_Way_Status = tools.CheckInt(Request.Form["Delivery_Way_Status"]);
        int Delivery_Way_Cod = tools.CheckInt(Request.Form["Delivery_Way_Cod"]);
        string Delivery_Way_Img = "";
        string Delivery_Way_Url = "";
        string Delivery_Way_Intro = Request.Form["Delivery_Way_Intro"];

        if (Delivery_Way_Name.Length == 0) { pub.Msg("error", "错误信息", "请填写配送方式名称", false, "{back}"); return; }

        DeliveryWayInfo entity = new DeliveryWayInfo();
        entity.Delivery_Way_ID = Delivery_Way_ID;
        entity.Delivery_Way_SupplierID = tools.NullInt(Session["supplier_id"]);
        entity.Delivery_Way_Name = Delivery_Way_Name;
        entity.Delivery_Way_Sort = Delivery_Way_Sort;
        entity.Delivery_Way_InitialWeight = Delivery_Way_InitialWeight;
        entity.Delivery_Way_UpWeight = Delivery_Way_UpWeight;
        entity.Delivery_Way_FeeType = Delivery_Way_FeeType;
        entity.Delivery_Way_Fee = Delivery_Way_Fee;
        entity.Delivery_Way_InitialFee = Delivery_Way_InitialFee;
        entity.Delivery_Way_UpFee = Delivery_Way_UpFee;
        entity.Delivery_Way_Status = Delivery_Way_Status;
        entity.Delivery_Way_Cod = Delivery_Way_Cod;
        entity.Delivery_Way_Img = Delivery_Way_Img;
        entity.Delivery_Way_Url = Delivery_Way_Url;
        entity.Delivery_Way_Intro = Delivery_Way_Intro;
        entity.Delivery_Way_Site = pub.GetCurrentSite();

        if (MyDeliveryway.AddDeliveryWay(entity, pub.CreateUserPrivilege("8632585c-0447-4003-a97d-48cade998a05")))
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "delivery_way_list.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //修改配送方式
    public void EditDeliveryWay()
    {
        int Delivery_Way_ID = tools.CheckInt(Request.Form["Delivery_Way_ID"]);
        string Delivery_Way_Name = tools.CheckStr(Request.Form["Delivery_Way_Name"]);
        int Delivery_Way_Sort = tools.CheckInt(Request.Form["Delivery_Way_Sort"]);
        int Delivery_Way_InitialWeight = tools.CheckInt(Request.Form["Delivery_Way_InitialWeight"]);
        int Delivery_Way_UpWeight = tools.CheckInt(Request.Form["Delivery_Way_UpWeight"]);
        int Delivery_Way_FeeType = tools.CheckInt(Request.Form["Delivery_Way_FeeType"]);
        double Delivery_Way_Fee = tools.CheckFloat(Request.Form["Delivery_Way_Fee"]);
        double Delivery_Way_InitialFee = tools.CheckFloat(Request.Form["Delivery_Way_InitialFee"]);
        double Delivery_Way_UpFee = tools.CheckFloat(Request.Form["Delivery_Way_UpFee"]);
        int Delivery_Way_Status = tools.CheckInt(Request.Form["Delivery_Way_Status"]);
        int Delivery_Way_Cod = tools.CheckInt(Request.Form["Delivery_Way_Cod"]);
        string Delivery_Way_Img = tools.CheckStr(Request.Form["Delivery_Way_Img"]);
        string Delivery_Way_Url = tools.CheckStr(Request.Form["Delivery_Way_Url"]);
        string Delivery_Way_Intro = Request.Form["Delivery_Way_Intro"];

        if (Delivery_Way_Name.Length == 0) { pub.Msg("error", "错误信息", "请填写配送方式名称", false, "{back}"); return; }

        DeliveryWayInfo entity = new DeliveryWayInfo();
        entity.Delivery_Way_ID = Delivery_Way_ID;
        entity.Delivery_Way_SupplierID = tools.NullInt(Session["supplier_id"]);
        entity.Delivery_Way_Name = Delivery_Way_Name;
        entity.Delivery_Way_Sort = Delivery_Way_Sort;
        entity.Delivery_Way_InitialWeight = Delivery_Way_InitialWeight;
        entity.Delivery_Way_UpWeight = Delivery_Way_UpWeight;
        entity.Delivery_Way_FeeType = Delivery_Way_FeeType;
        entity.Delivery_Way_Fee = Delivery_Way_Fee;
        entity.Delivery_Way_InitialFee = Delivery_Way_InitialFee;
        entity.Delivery_Way_UpFee = Delivery_Way_UpFee;
        entity.Delivery_Way_Status = Delivery_Way_Status;
        entity.Delivery_Way_Cod = Delivery_Way_Cod;
        entity.Delivery_Way_Img = Delivery_Way_Img;
        entity.Delivery_Way_Url = Delivery_Way_Url;
        entity.Delivery_Way_Intro = Delivery_Way_Intro;
        entity.Delivery_Way_Site = pub.GetCurrentSite();


        if (MyDeliveryway.EditDeliveryWay(entity, pub.CreateUserPrivilege("58d92d67-4e0b-4a4c-bd5c-6062c432554d")))
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "delivery_way_list.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //删除配送方式
    public void DelDeliveryWay()
    {
        int Delivery_Way_ID = tools.CheckInt(Request.QueryString["Delivery_Way_ID"]);
        DeliveryWayInfo entity = GetDeliveryWayByID(Delivery_Way_ID);
        if (entity != null)
        {
            if (entity.Delivery_Way_SupplierID == tools.NullInt(Session["supplier_id"]))
            {
                if (MyDeliveryway.DelDeliveryWay(Delivery_Way_ID, pub.CreateUserPrivilege("9909b492-b55c-49bf-b726-0f2d36e7ff4b")) > 0)
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "delivery_way_list.aspx");
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
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //获取指定配送方式信息
    public DeliveryWayInfo GetDeliveryWayByID(int cate_id)
    {
        return MyDeliveryway.GetDeliveryWayByID(cate_id, pub.CreateUserPrivilege("837c9372-3b25-494f-b141-767e195e3c88"));
    }

    //获取指定配送方式可以配送的区域信息
    public IList<DeliveryWayInfo> GetDeliveryWaysByDistrict(string state, string city, string county)
    {
        return MyDeliveryway.GetDeliveryWaysByDistrict(state, city, county, pub.CreateUserPrivilege("8632585c-0447-4003-a97d-48cade998a05"));
    }

    //添加配送方式区域
    public void AddDeliveryWayDistrict()
    {
        int District_ID = tools.CheckInt(Request.Form["District_ID"]);
        int District_DeliveryWayID = tools.CheckInt(Request.Form["District_DeliveryWayID"]);
        string District_Country = tools.CheckStr(Request.Form["District_Country"]);
        string District_State = tools.CheckStr(Request.Form["District_State"]);
        string District_City = tools.CheckStr(Request.Form["District_City"]);
        string District_County = tools.CheckStr(Request.Form["District_County"]);

        DeliveryWayDistrictInfo entity = new DeliveryWayDistrictInfo();
        entity.District_ID = District_ID;
        entity.District_DeliveryWayID = District_DeliveryWayID;
        entity.District_Country = District_Country;
        entity.District_State = District_State;
        entity.District_City = District_City;
        entity.District_County = District_County;

        if (MyDeliveryway.AddDeliveryWayDistrict(entity, pub.CreateUserPrivilege("8632585c-0447-4003-a97d-48cade998a05")))
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "Delivery_Way_Area_List.aspx?delivery_way_id=" + District_DeliveryWayID);
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //删除配送方式区域
    public void DelDeliveryWayDistrict()
    {
        int District_ID = tools.CheckInt(Request.QueryString["District_ID"]);
        if (MyDeliveryway.DelDeliveryWayDistrict(District_ID, pub.CreateUserPrivilege("8632585c-0447-4003-a97d-48cade998a05")) > 0)
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "Delivery_Way_Area_List.aspx?delivery_way_id=" + Request.QueryString["delivery_way_id"]);
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void GetDeliveryWayDistricts()
    {
        int Delivery_Way_ID = tools.CheckInt(Request.QueryString["Delivery_Way_ID"]);
        IList<DeliveryWayDistrictInfo> entitys = MyDeliveryway.GetDeliveryWayDistrictsByDWID(Delivery_Way_ID, pub.CreateUserPrivilege("8632585c-0447-4003-a97d-48cade998a05"));
        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <td width=\"200\" align=\"center\" valign=\"middle\">省份</td>");
        Response.Write("  <td width=\"200\" align=\"center\" valign=\"middle\">城市</td>");
        Response.Write("  <td align=\"center\" valign=\"middle\">区域</td>");
        Response.Write("  <td width=\"100\" align=\"center\" valign=\"middle\">操作</td>");
        Response.Write("</tr>");
        if (entitys != null)
        {
            DeliveryWayInfo wayinfo = GetDeliveryWayByID(Delivery_Way_ID);

            StateInfo stateEntity = null;
            CityInfo cityEntity = null;
            CountyInfo countyEntity = null;

            foreach (DeliveryWayDistrictInfo entity in entitys)
            {

                stateEntity = addrBLL.GetStateInfoByCode(entity.District_State);
                cityEntity = addrBLL.GetCityInfoByCode(entity.District_City);
                countyEntity = addrBLL.GetCountyInfoByCode(entity.District_County);

                Response.Write("<tr bgcolor=\"#ffffff\">");
                if (stateEntity != null)
                {
                    Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + stateEntity.State_CN + "</td>");
                }
                else
                {
                    if (entity.District_State.Length == 0)
                    {
                        Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">全部省</td>");
                    }
                    else
                    {
                        Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">--</td>");
                    }
                }

                if (cityEntity != null)
                {
                    Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + cityEntity.City_CN + "</td>");
                }
                else
                {
                    if (entity.District_City.Length == 0)
                    {
                        Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">全部市/地区</td>");
                    }
                    else
                    {
                        Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">--</td>");
                    }
                }


                if (countyEntity != null)
                {
                    Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + countyEntity.County_CN + "</td>");
                }
                else
                {
                    if (entity.District_County.Length == 0)
                    {
                        Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">全部区/县</td>");
                    }
                    else
                    {
                        Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">--</td>");
                    }
                }
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\"><a href=\"/supplier/Delivery_Way_do.aspx?action=districtdel&District_ID=" + entity.District_ID + "&delivery_way_id=" + Delivery_Way_ID + "\">删除</a></td>");
                Response.Write("</tr>");
            }
        }
        else
        {
            Response.Write("<tr bgcolor=\"#ffffff\"><td width=\"70\" height=\"35\" colspan=\"4\" align=\"center\" valign=\"middle\">没有记录</td></tr>");
        }
        Response.Write("</table>");
    }

    public void ChangeShopActive(SupplierShopInfo entity)
    {
        MyShop.EditSupplierShop(entity);
    }

    //咨询留言
    //会员费明细
    public void Shop_Ask_List(string action)
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        string BgColor = "";
        Pageurl = "?action=" + action;


        if (curpage < 1)
        {
            curpage = 1;
        }


        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <th align=\"center\" valign=\"middle\">咨询内容</th>");
        if (action == "product")
        {
            Response.Write("  <th width=\"220\" align=\"center\" valign=\"middle\" >咨询产品</th>");
        }
        Response.Write("  <th width=\"80\" align=\"center\" valign=\"middle\">状态</th>");
        Response.Write("  <th width=\"130\" align=\"center\" valign=\"middle\">咨询时间</th>");
        Response.Write("  <th width=\"80\" align=\"center\" valign=\"middle\">操作</th>");
        Response.Write("</tr>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ShoppingAskInfo.Ask_SupplierID", "=", supplier_id.ToString()));
        if (action == "product")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ShoppingAskInfo.Ask_ProductID", ">", "0"));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ShoppingAskInfo.Ask_ProductID", "=", "0"));
        }
        Query.OrderInfos.Add(new OrderInfo("ShoppingAskInfo.Ask_ID", "Desc"));
        IList<ShoppingAskInfo> entitys = MyAsk.GetShoppingAsks(Query, pub.CreateUserPrivilege("fe2e0dd7-2773-4748-915a-103065ed0334"));
        PageInfo page = MyAsk.GetPageInfo(Query, pub.CreateUserPrivilege("fe2e0dd7-2773-4748-915a-103065ed0334"));
        if (entitys != null)
        {
            ProductInfo productinfo = null;
            foreach (ShoppingAskInfo entity in entitys)
            {
                if (BgColor == "#FFFFFF")
                {
                    BgColor = "#FFFFFF";
                }
                else
                {
                    BgColor = "#FFFFFF";
                }

                Response.Write("<tr bgcolor=\"" + BgColor + "\">");
                Response.Write("  <td height=\"35\" align=\"left\" valign=\"middle\">&nbsp;" + entity.Ask_Content + "</td>");
                if (action == "product")
                {
                    productinfo = GetProductByID(entity.Ask_ProductID);
                    if (productinfo != null)
                    {
                        Response.Write("  <td height=\"35\" align=\"left\" valign=\"middle\">" + productinfo.Product_Name + "</td>");
                    }
                    else
                    {
                        Response.Write("  <td height=\"35\" align=\"center\" valign=\"middle\">--</td>");
                    }
                }
                if (entity.Ask_Isreply == 0)
                {
                    Response.Write("  <td  height=\"35\" align=\"center\" valign=\"middle\">未回复</td>");
                }
                else
                {
                    Response.Write("  <td  height=\"35\" align=\"center\" valign=\"middle\">已回复 </td>");
                }

                Response.Write("  <td height=\"35\" align=\"center\" valign=\"middle\">" + entity.Ask_Addtime + "</td>");
                Response.Write("  <td height=\"35\" align=\"center\" valign=\"middle\">");
                Response.Write("<a href=\"/supplier/Supplier_Shop_Message_Reply.aspx?Ask_ID=" + entity.Ask_ID + "\" style=\"color:#3366cc;\"><img src=\"/images/btn_edit.gif\" border=\"0\" alt=\"回复\"></a>");
                Response.Write(" <a href=\"/supplier/Supplier_Shop_Message_do.aspx?action=del&Ask_ID=" + entity.Ask_ID + "\" style=\"color:#3366cc;\"><img src=\"/images/btn_del.gif\" border=\"0\" alt=\"删除\"></a>");
                Response.Write("</td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            Response.Write("<tr><td height=\"8\"></td></tr>");
            Response.Write("<tr><td align=\"right\"><div class=\"page\" style=\"float:right;\">");
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
            Response.Write("</div></td></tr>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#FFFFFF\">");
            Response.Write("<td colspan=\"5\" align=\"center\" height=\"35\">暂无记录</td>");
            Response.Write("</tr>");
        }
        Response.Write("</table>");

    }

    public ShoppingAskInfo GetShoppingAskByID(int Ask_ID)
    {
        ShoppingAskInfo entity = MyAsk.GetShoppingAskByID(Ask_ID, pub.CreateUserPrivilege("fe2e0dd7-2773-4748-915a-103065ed0334"));
        if (entity != null)
        {
            if (entity.Ask_SupplierID != tools.NullInt(Session["supplier_id"]))
            {
                entity = null;
            }
        }
        return entity;
    }

    //店铺版面删除
    public void SupplierShopAsk_Del()
    {
        int Ask_ID = tools.CheckInt(Request["Ask_ID"]);
        ShoppingAskInfo entity = GetShoppingAskByID(Ask_ID);
        if (entity != null)
        {
            if (MyAsk.DelShoppingAsk(Ask_ID, pub.CreateUserPrivilege("9cf98bf5-6f7c-4fbc-973e-a92c9a37c732")) > 0)
            {
                if (entity.Ask_ProductID > 0)
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_Message.aspx?message_guide=product");
                }
                else
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_Message.aspx");
                }
            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
    }

    //店铺版面回复
    public void SupplierShopAsk_Reply()
    {
        int Ask_ID = tools.CheckInt(Request["Ask_ID"]);
        string Ask_Reply = tools.CheckStr(Request["Ask_Reply"]);
        ShoppingAskInfo entity = GetShoppingAskByID(Ask_ID);
        if (entity != null)
        {
            entity.Ask_Reply = Ask_Reply;
            if (Ask_Reply.Length > 0)
            {
                entity.Ask_Isreply = 1;
            }
            else
            {
                entity.Ask_Isreply = 0;
            }
            entity.Ask_IsCheck = 1;
            if (MyAsk.EditShoppingAsk(entity, pub.CreateUserPrivilege("b2a9d36e-9ba5-45b6-8481-9da1cd12ace0")))
            {
                if (entity.Ask_ProductID > 0)
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_Message.aspx?message_guide=product");
                }
                else
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_Message.aspx");
                }
            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
    }

    //在线客服列表
    public void Supplier_Online_List()
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        Pageurl = "?action=list";
        if (curpage < 1)
        {
            curpage = 1;
        }

        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <th align=\"center\" valign=\"middle\">客服名称</th>");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\">类型</th>");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\">状态</th>");
        Response.Write("  <th width=\"150\" align=\"center\" valign=\"middle\">添加时间</th>");
        Response.Write("  <th width=\"70\" align=\"center\" valign=\"middle\">操作</th>");
        Response.Write("</tr>");
        string productURL = string.Empty;
        string status_name = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierOnlineInfo.Supplier_Online_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierOnlineInfo.Supplier_Online_SupplierID", "=", supplier_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierOnlineInfo.Supplier_Online_Sort", "Asc"));
        IList<SupplierOnlineInfo> entitys = MyOnline.GetSupplierOnlines(Query);
        PageInfo page = MyOnline.GetPageInfo(Query);
        if (entitys != null)
        {
            foreach (SupplierOnlineInfo entity in entitys)
            {
                i = i + 1;
                if (entity.Supplier_Online_IsActive == 0)
                {
                    status_name = "未启用";
                }
                else
                {
                    status_name = "启用";
                }
                Response.Write("<tr bgcolor=\"#ffffff\">");
                Response.Write("<td height=\"35\" align=\"left\" class=\"comment_td_bg\" style=\"padding-left:10px;\" valign=\"middle\">" + entity.Supplier_Online_Name + "</a></td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + entity.Supplier_Online_Type + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + status_name + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + entity.Supplier_Online_Addtime + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">");
                Response.Write("<a href=\"/supplier/Supplier_Shop_Online_Edit.aspx?Online_ID=" + entity.Supplier_Online_ID + "\" style=\"color:#3366cc;\"><img src=\"/images/btn_edit.gif\" border=\"0\" alt=\"修改\"></a>");
                Response.Write(" <a href=\"/supplier/Supplier_Shop_Online_do.aspx?action=remove&Online_ID=" + entity.Supplier_Online_ID + "\" style=\"color:#3366cc;\"><img src=\"/images/btn_del.gif\" border=\"0\" alt=\"删除\"></a>");
                Response.Write("</td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" align=\"center\" cellpadding=\"2\"><tr><td align=\"right\" height=\"10\"></td></tr><tr><td align=\"right\"><div class=\"page\" style=\"float:right;\">");
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
            Response.Write("</div></td></tr></table>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#ffffff\"><td width=\"70\" height=\"35\" colspan=\"5\" align=\"center\" valign=\"middle\">没有记录</td></tr>");
        }
        Response.Write("</table>");
    }

    //在线客服添加
    public virtual void AddSupplierOnline()
    {
        int Supplier_Online_ID = tools.CheckInt(Request.Form["Supplier_Online_ID"]);
        int Supplier_Online_SupplierID = tools.NullInt(Session["supplier_id"]);
        string Supplier_Online_Type = tools.CheckStr(Request.Form["Online_Type"]);
        string Supplier_Online_Name = tools.CheckStr(Request.Form["Online_Name"]);
        string Supplier_Online_QQ = Request.Form["Online_QQ"];
        string Supplier_Online_MSN = tools.CheckStr(Request.Form["Online_MSN"]);
        int Supplier_Online_Sort = tools.CheckInt(Request.Form["Online_Sort"]);
        int Supplier_Online_IsActive = tools.CheckInt(Request.Form["Online_IsActive"]);
        string Supplier_Online_Site = pub.GetCurrentSite();

        if (Supplier_Online_Name == "")
        {
            pub.Msg("error", "错误信息", "请填写客服名称", false, "{back}");
        }

        if (Supplier_Online_Type == "QQ" && Supplier_Online_QQ.Length == 0)
        {
            pub.Msg("error", "错误信息", "请填写QQ客服代码", false, "{back}");
        }

        if (Supplier_Online_Type == "MSN" && Supplier_Online_MSN.Length == 0)
        {
            pub.Msg("error", "错误信息", "请填写MSN客服账号", false, "{back}");
        }

        SupplierOnlineInfo entity = new SupplierOnlineInfo();
        entity.Supplier_Online_ID = Supplier_Online_ID;
        entity.Supplier_Online_SupplierID = Supplier_Online_SupplierID;
        entity.Supplier_Online_Type = Supplier_Online_Type;
        entity.Supplier_Online_Name = Supplier_Online_Name;
        if (Supplier_Online_Type == "QQ")
        {
            entity.Supplier_Online_Code = Supplier_Online_QQ;
        }
        else
        {
            entity.Supplier_Online_Code = Supplier_Online_MSN;
        }
        entity.Supplier_Online_Sort = Supplier_Online_Sort;
        entity.Supplier_Online_IsActive = Supplier_Online_IsActive;
        entity.Supplier_Online_Addtime = DateTime.Now;
        entity.Supplier_Online_Site = Supplier_Online_Site;

        if (MyOnline.AddSupplierOnline(entity))
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_Online_Add.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //在线客服修改
    public virtual void EditSupplierOnline()
    {

        int Supplier_Online_ID = tools.CheckInt(Request.Form["Supplier_Online_ID"]);
        int Supplier_Online_SupplierID = tools.NullInt(Session["supplier_id"]);
        string Supplier_Online_Type = tools.CheckStr(Request.Form["Online_Type"]);
        string Supplier_Online_Name = tools.CheckStr(Request.Form["Online_Name"]);
        string Supplier_Online_QQ = Request.Form["Online_QQ"];
        string Supplier_Online_MSN = tools.CheckStr(Request.Form["Online_MSN"]);
        int Supplier_Online_Sort = tools.CheckInt(Request.Form["Online_Sort"]);
        int Supplier_Online_IsActive = tools.CheckInt(Request.Form["Online_IsActive"]);

        if (Supplier_Online_Name == "")
        {
            pub.Msg("error", "错误信息", "请填写客服名称", false, "{back}");
        }

        if (Supplier_Online_Type == "QQ" && Supplier_Online_QQ.Length == 0)
        {
            pub.Msg("error", "错误信息", "请填写QQ客服代码", false, "{back}");
        }

        if (Supplier_Online_Type == "MSN" && Supplier_Online_MSN.Length == 0)
        {
            pub.Msg("error", "错误信息", "请填写MSN客服账号", false, "{back}");
        }

        SupplierOnlineInfo entity = GetSupplierOnlineByID(Supplier_Online_ID, Supplier_Online_SupplierID);
        if (entity != null)
        {

            entity.Supplier_Online_SupplierID = Supplier_Online_SupplierID;
            entity.Supplier_Online_Type = Supplier_Online_Type;
            entity.Supplier_Online_Name = Supplier_Online_Name;
            if (Supplier_Online_Type == "QQ")
            {
                entity.Supplier_Online_Code = Supplier_Online_QQ;
            }
            else
            {
                entity.Supplier_Online_Code = Supplier_Online_MSN;
            }
            entity.Supplier_Online_Sort = Supplier_Online_Sort;
            entity.Supplier_Online_IsActive = Supplier_Online_IsActive;


            if (MyOnline.EditSupplierOnline(entity))
            {
                pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_Online.aspx");
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

    //在线客服删除
    public virtual void DelSupplierOnline()
    {
        int Supplier_Online_ID = tools.CheckInt(Request.QueryString["Online_ID"]);
        if (MyOnline.DelSupplierOnline(Supplier_Online_ID) > 0)
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Shop_Online.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual SupplierOnlineInfo GetSupplierOnlineByID(int cate_id, int supplier_id)
    {
        SupplierOnlineInfo entity = MyOnline.GetSupplierOnlineByID(cate_id);
        if (entity != null)
        {
            if (entity.Supplier_Online_SupplierID != supplier_id)
            {
                entity = null;
            }
        }
        return entity;
    }

    #region 咨询管理

    public ShoppingAskInfo GetShoppingAskInfoByID(int ID)
    {
        return MyShopingAsk.GetShoppingAskByID(ID, pub.CreateUserPrivilege("fe2e0dd7-2773-4748-915a-103065ed0334"));
    }

    public void GetShoppingAsk(int showNum)
    {
        string CurrentURL = Request.Path + "?listtype=normal";

        QueryInfo Query = new QueryInfo();
        Query.PageSize = showNum;
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (Query.CurrentPage <= 0)
            Query.CurrentPage = 1;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ShoppingAskInfo.Ask_SupplierID", "=", tools.NullStr(Session["supplier_id"])));
        Query.OrderInfos.Add(new OrderInfo("ShoppingAskInfo.Ask_Addtime", "desc"));
        PageInfo pageinfo = MyShopingAsk.GetPageInfo(Query, pub.CreateUserPrivilege("fe2e0dd7-2773-4748-915a-103065ed0334"));
        IList<ShoppingAskInfo> entitys = MyShopingAsk.GetShoppingAsks(Query, pub.CreateUserPrivilege("fe2e0dd7-2773-4748-915a-103065ed0334"));

        StringBuilder strHTML = new StringBuilder();
        strHTML.Append("<table  width=\"100%\" border=\"0\" cellspacing=\"2\" cellpadding=\"0\" class=\"table02\">");
        strHTML.Append("  <tr>");
        strHTML.Append("    <td width=\"35%\" class=\"name\">咨询内容</td>");
        strHTML.Append("    <td width=\"15%\" class=\"name\">咨询人</td>");
        strHTML.Append("    <td width=\"30%\" class=\"name\">产品</td>");
        strHTML.Append("    <td width=\"10%\" class=\"name\">状态</td>");
        strHTML.Append("    <td width=\"10%\" class=\"name\">操作</td>");
        strHTML.Append("  </tr>");

        if (entitys != null)
        {
            foreach (ShoppingAskInfo entity in entitys)
            {
                strHTML.Append("  <tr>");
                strHTML.Append("    <td style=\"text-align:left;padding-left:10px;\">" + entity.Ask_Content + "</td>");
                if (entity.Ask_MemberID > 0)
                {
                    strHTML.Append("<td>" + GetMemberNickName(entity.Ask_MemberID) + "</td>");
                }
                else
                {
                    strHTML.Append("<td>游客</td>");
                }

                ProductInfo productInfo = MyProduct.GetProductByID(entity.Ask_ProductID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                if (productInfo != null)
                {
                    strHTML.Append("    <td>" + productInfo.Product_Name + "</td>");
                }
                else
                {
                    strHTML.Append("    <td>--</td>");
                }

                if (entity.Ask_Isreply == 0)
                {
                    strHTML.Append("    <td>未回复</td>");
                }
                else
                {
                    strHTML.Append("<td>已回复</td>");
                }
                strHTML.Append("  <td><span><a href=\"/supplier/supplier_shoppingask_reply.aspx?shoppingask_id=" + entity.Ask_ID + "\"  class=\"a12\">回复</a></span></td>");
                strHTML.Append("  </tr>");
                strHTML.Append("</table>");
                Response.Write(strHTML.ToString());
            }
            pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, CurrentURL, pageinfo.PageSize, pageinfo.RecordCount);

        }
        else
        {
            Response.Write(strHTML.ToString());
            Response.Write("  <tr>");
            Response.Write("    <td colspan=\"5\">记录不存在</td>");
            Response.Write("  </tr>");
            Response.Write("</table>");
        }
    }


    public int GetShoppingAskNum(int showNum)
    {
        int supplier_id = tools.CheckInt(tools.NullStr(Session["supplier_id"]).ToString());
        int GetShoppingAskNum = tools.CheckInt(DBHelper.ExecuteScalar("select count(*) from Shopping_Ask where Ask_SupplierID=" + supplier_id + "").ToString());
        return GetShoppingAskNum;
        //int i = 0;
        //string CurrentURL = Request.Path + "?listtype=normal";

        //QueryInfo Query = new QueryInfo();
        //Query.PageSize = showNum;
        //Query.CurrentPage = tools.CheckInt(Request["page"]);
        //if (Query.CurrentPage <= 0)
        //    Query.CurrentPage = 1;

        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ShoppingAskInfo.Ask_SupplierID", "=", tools.NullStr(Session["supplier_id"])));
        //Query.OrderInfos.Add(new OrderInfo("ShoppingAskInfo.Ask_Addtime", "desc"));
        //PageInfo pageinfo = MyShopingAsk.GetPageInfo(Query, pub.CreateUserPrivilege("fe2e0dd7-2773-4748-915a-103065ed0334"));
        //IList<ShoppingAskInfo> entitys = MyShopingAsk.GetShoppingAsks(Query, pub.CreateUserPrivilege("fe2e0dd7-2773-4748-915a-103065ed0334"));

        //StringBuilder strHTML = new StringBuilder();

        //if (entitys != null)
        //{

        //    foreach (ShoppingAskInfo entity in entitys)
        //    {

        //        i++;
        //    }
        //}
        //return i;
    }

    public void EditShoppingAsk()
    {
        int shoppingask_id = tools.CheckInt(Request["shoppingask_id"]);
        string Ask_Reply = tools.CheckStr(Request["Ask_Reply"]);

        ShoppingAskInfo entity = GetShoppingAskInfoByID(shoppingask_id);
        if (entity != null)
        {
            entity.Ask_Reply = Ask_Reply;
            entity.Ask_Isreply = 1;
            if (MyShopingAsk.EditShoppingAsk(entity, pub.CreateUserPrivilege("b2a9d36e-9ba5-45b6-8481-9da1cd12ace0")))
            {
                pub.Msg("positive", "温馨提示", "操作成功", false, "/supplier/supplier_shop_shoppingask.aspx");
            }
            else
            {
                pub.Msg("info", "温馨提示", "操作失败，请稍后再试......", false, "{back}");
            }
        }
        else
        {
            pub.Msg("info", "温馨提示", "操作失败，请稍后再试......", false, "{back}");
        }
    }

    #endregion



    #endregion

    #region 店铺商品

    /// <summary>
    /// 招商加盟管理
    /// </summary>
    public void Supplier_Merchants_List()
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
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
        Response.Write("  <td width=\"242\" class=\"name\">名称</td>");
        Response.Write("  <td width=\"241\" class=\"name\">有效期</td>");
        Response.Write("  <td width=\"238\" class=\"name\">添加时间</td>");
        Response.Write("  <td width=\"242\" class=\"name\">操作</td>");
        Response.Write("</tr>");
        string productURL = string.Empty;
        string parentname = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierMerchantsInfo.Merchants_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierMerchantsInfo.Merchants_SupplierID", "=", supplier_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierMerchantsInfo.Merchants_AddTime", "desc"));
        IList<SupplierMerchantsInfo> entitys = MyMerchants.GetSupplierMerchantss(Query);
        PageInfo page = MyMerchants.GetPageInfo(Query);
        if (entitys != null)
        {
            foreach (SupplierMerchantsInfo entity in entitys)
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
                Response.Write("<td>" + entity.Merchants_Name + "</a></td>");
                Response.Write("<td>" + entity.Merchants_AddTime.AddMonths(entity.Merchants_Validity) + "</td>");
                Response.Write("<td>" + entity.Merchants_AddTime + "</td>");
                Response.Write("<td>");
                Response.Write("<span><a href=\"/supplier/Supplier_Merchants_Edit.aspx?Merchants_ID=" + entity.Merchants_ID + "\" class=\"a12\">修改</a></span>");
                Response.Write("<span><a href=\"javascript:;\" onclick=\"ajax_merchants_del(" + entity.Merchants_ID + ");\">删除</a></span>");
                Response.Write("<span><a href=\"/supplier/Supplier_Merchants_Reply.aspx?Merchants_ID=" + entity.Merchants_ID + "\">查看加盟</a></span>");
                Response.Write("</td>");
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

    public void Supplier_Merchants_Reply()
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        int Message_MerchantsID = tools.CheckInt(Request["Merchants_ID"]);
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
        Response.Write("  <td width=\"242\" class=\"name\">采购商</td>");
        Response.Write("  <td width=\"241\" class=\"name\">联系人</td>");
        Response.Write("  <td width=\"241\" class=\"name\">联系人电话</td>");
        Response.Write("  <td width=\"241\" class=\"name\">联系人Email</td>");
        Response.Write("  <td width=\"238\" class=\"name\">添加时间</td>");
        Response.Write("  <td width=\"242\" class=\"name\">操作1</td>");
        Response.Write("</tr>");
        string productURL = string.Empty;
        string parentname = "";
        string memberName = "";
        MemberInfo memberInfo = null;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierMerchantsMessageInfo.Message_MerchantsID", "=", Message_MerchantsID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierMerchantsMessageInfo.Message_AddTime", "desc"));
        IList<SupplierMerchantsMessageInfo> entitys = MyMerchantsMessage.GetSupplierMerchantsMessages(Query);
        PageInfo page = MyMerchantsMessage.GetPageInfo(Query);
        if (entitys != null)
        {
            foreach (SupplierMerchantsMessageInfo entity in entitys)
            {
                memberInfo = MyMEM.GetMemberByID(entity.Message_MemberID, pub.CreateUserPrivilege("833b9bdd-a344-407b-b23a-671348d57f76"));
                if (memberInfo != null)
                {
                    memberName = memberInfo.Member_NickName;
                }

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
                Response.Write("<td>" + memberName + "</a></td>");
                Response.Write("<td>" + entity.Message_Contactman + "</a></td>");
                Response.Write("<td>" + entity.Message_ContactMobile + "</a></td>");
                Response.Write("<td>" + entity.Message_ContactEmail + "</a></td>");
                Response.Write("<td>" + entity.Message_AddTime.ToString("yyyy-MM-dd") + "</td>");
                Response.Write("<td>");
                Response.Write("<a href=\"/supplier/Supplier_Merchants_Reply_view.aspx?Message_ID=" + entity.Message_ID + "\" class=\"a12\">查看</a>");
                Response.Write("</td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");

            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
        }
        else
        {
            Response.Write("<tr><td  colspan=\"4\" style=\"text-align:center;\">没有记录</td></tr>");
        }
        Response.Write("</table>");
    }

    public void Supplier_Merchants_Validity(int selectValue)
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<option value=\"1\" " + (selectValue == 1 ? "selected" : "") + ">1个月</option>");
        strHTML.Append("<option value=\"3\" " + (selectValue == 3 ? "selected" : "") + ">3个月</option>");
        strHTML.Append("<option value=\"6\" " + (selectValue == 6 ? "selected" : "") + ">6个月</option>");
        strHTML.Append("<option value=\"10\" " + (selectValue == 10 ? "selected" : "") + ">10个月</option>");

        Response.Write(strHTML.ToString());
    }

    public void Supplier_Merchants_Add()
    {
        int Merchants_ID = tools.CheckInt(Request.Form["Merchants_ID"]);
        int Merchants_SupplierID = tools.NullInt(Session["supplier_id"]);
        string Merchants_Name = tools.CheckStr(Request.Form["Merchants_Name"]);
        int Merchants_Validity = tools.CheckInt(Request.Form["Merchants_Validity"]);
        string Merchants_Channel = tools.CheckStr(Request.Form["Merchants_Channel"]);
        string Merchants_Advantage = tools.CheckStr(Request.Form["Merchants_Advantage"]);
        string Merchants_Trem = tools.CheckStr(Request.Form["Merchants_Trem"]);
        string Merchants_Intro = tools.CheckStr(Request.Form["Merchants_Intro"]);
        string Merchants_Img = tools.CheckStr(Request.Form["merchants_Img"]);
        DateTime Merchants_AddTime = DateTime.Now;
        string Merchants_Site = pub.GetCurrentSite();

        SupplierMerchantsInfo entity = new SupplierMerchantsInfo();
        entity.Merchants_ID = Merchants_ID;
        entity.Merchants_SupplierID = Merchants_SupplierID;
        entity.Merchants_Name = Merchants_Name;
        entity.Merchants_Validity = Merchants_Validity;
        entity.Merchants_Channel = Merchants_Channel;
        entity.Merchants_Advantage = Merchants_Advantage;
        entity.Merchants_Trem = Merchants_Trem;
        entity.Merchants_Intro = Merchants_Intro;
        entity.Merchants_Img = Merchants_Img;
        entity.Merchants_AddTime = Merchants_AddTime;
        entity.Merchants_Site = Merchants_Site;

        if (Merchants_Name.Length < 1)
        {
            pub.Msg("error", "错误信息", "请输入招商加盟名称", false, "{back}");

        }


        if (MyMerchants.AddSupplierMerchants(entity))
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Merchants_add.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void Supplier_Merchants_Edit()
    {
        int Merchants_ID = tools.CheckInt(Request.Form["Merchants_ID"]);
        string Merchants_Name = tools.CheckStr(Request.Form["Merchants_Name"]);
        int Merchants_Validity = tools.CheckInt(Request.Form["Merchants_Validity"]);
        string Merchants_Channel = tools.CheckStr(Request.Form["Merchants_Channel"]);
        string Merchants_Advantage = tools.CheckStr(Request.Form["Merchants_Advantage"]);
        string Merchants_Trem = tools.CheckStr(Request.Form["Merchants_Trem"]);
        string Merchants_Intro = tools.CheckStr(Request.Form["Merchants_Intro"]);
        string Merchants_Img = tools.CheckStr(Request.Form["merchants_Img"]);

        SupplierMerchantsInfo entity = GetSupplierMerchantsByID(Merchants_ID);
        if (entity != null)
        {
            entity.Merchants_ID = Merchants_ID;
            entity.Merchants_SupplierID = tools.NullInt(Session["supplier_id"]);
            entity.Merchants_Name = Merchants_Name;
            entity.Merchants_Validity = Merchants_Validity;
            entity.Merchants_Channel = Merchants_Channel;
            entity.Merchants_Advantage = Merchants_Advantage;
            entity.Merchants_Trem = Merchants_Trem;
            entity.Merchants_Intro = Merchants_Intro;
            entity.Merchants_Img = Merchants_Img;

            if (MyMerchants.EditSupplierMerchants(entity))
            {
                pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Merchantslist.aspx");
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

    public virtual void DelSupplierMerchants()
    {
        int Merchants_ID = tools.CheckInt(Request.QueryString["Merchants_ID"]);
        if (MyMerchants.DelSupplierMerchants(Merchants_ID) > 0)
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "Supplier_Merchantslist.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public SupplierMerchantsInfo GetSupplierMerchantsByID(int ID)
    {
        return MyMerchants.GetSupplierMerchantsByID(ID);
    }

    public SupplierMerchantsMessageInfo GetSupplierMerchantsMessageInfo(int ID)
    {
        return MyMerchantsMessage.GetSupplierMerchantsMessageByID(ID);
    }


    //本店分类列表
    public void Supplier_Cate_List()
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
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
        Response.Write("  <td width=\"242\" class=\"name\">分类名称</td>");
        Response.Write("  <td width=\"241\" class=\"name\">所属分类</td>");
        Response.Write("  <td width=\"238\" class=\"name\">排序</td>");
        Response.Write("  <td width=\"242\" class=\"name\">操作</td>");
        Response.Write("</tr>");
        string productURL = string.Empty;
        string parentname = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCategoryInfo.Supplier_Cate_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCategoryInfo.Supplier_Cate_SupplierID", "=", supplier_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierCategoryInfo.Supplier_Cate_Sort", "Asc"));
        IList<SupplierCategoryInfo> entitys = MyProductCate.GetSupplierCategorys(Query);
        PageInfo page = MyProductCate.GetPageInfo(Query);
        if (entitys != null)
        {
            foreach (SupplierCategoryInfo entity in entitys)
            {
                i++;
                parentname = "";
                SupplierCategoryInfo parent = GetSupplierCategoryByID(entity.Supplier_Cate_Parentid);
                if (parent != null)
                {
                    parentname = parent.Supplier_Cate_Name;
                }

                if (i % 2 == 0)
                {
                    strBG = " class=\"bg\"";
                }
                else
                {
                    strBG = "";
                }

                Response.Write("<tr " + strBG + ">");
                Response.Write("<td>" + entity.Supplier_Cate_Name + "</a></td>");
                Response.Write("<td>" + parentname + "</td>");
                Response.Write("<td>" + entity.Supplier_Cate_Sort + "</td>");
                Response.Write("<td>");
                Response.Write("<span><a href=\"/supplier/Product_category_Edit.aspx?Cate_ID=" + entity.Supplier_Cate_ID + "\" class=\"a12\">修改</a></span>");
                Response.Write(" <span><a href=\"/supplier/Product_do.aspx?action=catedel&Cate_ID=" + entity.Supplier_Cate_ID + "\">删除</a></span>");
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


    public string GetSupplierShopProductCate(int supplier_id)
    {
        StringBuilder strHTML = new StringBuilder();

        int i = 0;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCategoryInfo.Supplier_Cate_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCategoryInfo.Supplier_Cate_SupplierID", "=", supplier_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierCategoryInfo.Supplier_Cate_Sort", "Asc"));
        IList<SupplierCategoryInfo> entitys = MyProductCate.GetSupplierCategorys(Query);



        if (entitys != null)
        {
            List<SupplierCategoryInfo> FirstList = entitys.Where(P => P.Supplier_Cate_Parentid == 0).ToList();
            List<SupplierCategoryInfo> subCate = null;
            foreach (SupplierCategoryInfo entity in FirstList)
            {
                i++;
                strHTML.Append("<div class=\"blk28_info\">");
                if (i == 1)
                {
                    strHTML.Append("<div class=\"title07\"><span onclick=\"openShutManager(this,'box')\"><a id=\"1\" class=\"hidecontent\" onclick=\"switchTag(1);\">" + entity.Supplier_Cate_Name + "</a></span></div>");
                    strHTML.Append("<ul id=\"box\">");
                }
                else
                {
                    strHTML.Append("<div class=\"title07\"><span onclick=\"openShutManager(this,'box" + i + "')\"><a id=\"" + i + "\" class=\"hidecontent\" onclick=\"switchTag(1);\">" + entity.Supplier_Cate_Name + "</a></span></div>");
                    strHTML.Append("<ul id=\"box" + i + "\">");
                }
                subCate = entitys.Where(P => P.Supplier_Cate_Parentid == entity.Supplier_Cate_ID).ToList();
                if (subCate != null)
                {
                    foreach (SupplierCategoryInfo sub in subCate)
                    {
                        strHTML.Append("<li><a href=\"#\" >" + sub.Supplier_Cate_Name + "</a></li>");
                    }
                }
                strHTML.Append("</ul>");
                strHTML.Append("</div>");
            }
        }

        return strHTML.ToString();
    }


    public SupplierCategoryInfo GetSupplierCategoryByID(int ID)
    {
        return MyProductCate.GetSupplierCategoryByID(ID);
    }

    public SupplierCategoryInfo GetSupplierCategoryByIDSupplier(int ID, int Supplier_ID)
    {
        return MyProductCate.GetSupplierCategoryByIDSupplier(ID, Supplier_ID);
    }

    //店铺产品分类保存
    public virtual void SupplierProductCategorySave()
    {
        int Supplier_Cate_ID = tools.NullInt(Request["Cate_ID"]); ;
        string Supplier_Cate_Name = tools.CheckStr(Request.Form["Cate_Name"]);
        int Supplier_Cate_SupplierID = tools.NullInt(Session["supplier_id"]);
        int Supplier_Cate_Sort = tools.NullInt(Request["Cate_Sort"]);
        int Supplier_Cate_Parentid = tools.NullInt(Request["Cate_Parentid"]);
        string Supplier_Cate_Site = pub.GetCurrentSite();
        if (Supplier_Cate_Name.Length == 0)
        {
            pub.Msg("error", "错误信息", "请填写分类名称！", false, "{back}");
        }

        SupplierCategoryInfo entity = GetSupplierCategoryByIDSupplier(Supplier_Cate_ID, Supplier_Cate_SupplierID);
        if (entity != null)
        {
            entity.Supplier_Cate_ID = Supplier_Cate_ID;
            entity.Supplier_Cate_Name = Supplier_Cate_Name;
            entity.Supplier_Cate_SupplierID = Supplier_Cate_SupplierID;
            entity.Supplier_Cate_Sort = Supplier_Cate_Sort;
            entity.Supplier_Cate_Parentid = Supplier_Cate_Parentid;

            if (MyProductCate.EditSupplierCategory(entity))
            {
                pub.Msg("positive", "操作成功", "操作成功", true, "Product_Category_List.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            entity = new SupplierCategoryInfo();
            entity.Supplier_Cate_ID = Supplier_Cate_ID;
            entity.Supplier_Cate_Name = Supplier_Cate_Name;
            entity.Supplier_Cate_SupplierID = Supplier_Cate_SupplierID;
            entity.Supplier_Cate_Sort = Supplier_Cate_Sort;
            entity.Supplier_Cate_Parentid = Supplier_Cate_Parentid;
            entity.Supplier_Cate_Site = Supplier_Cate_Site;

            if (MyProductCate.AddSupplierCategory(entity))
            {
                pub.Msg("positive", "操作成功", "操作成功", true, "Product_Category_List.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
    }

    //店铺产品分类删除
    public void SupplierProductCategory_Del()
    {
        int Cate_ID = tools.CheckInt(Request["Cate_ID"]);
        if (Cate_ID == 0)
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
        else
        {
            SupplierCategoryInfo entity = GetSupplierCategoryByIDSupplier(Cate_ID, tools.NullInt(Session["supplier_id"]));
            if (entity != null)
            {
                if (MyProductCate.DelSupplierCategory(Cate_ID) > 0)
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "Product_Category_List.aspx");
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
    }

    //产品标签选择
    public string ProductTagChoose(int Product_ID)
    {
        string strHTML = "";
        string strTag = ",0";

        if (Product_ID > 0)
            strTag = "," + MyProduct.GetProductTag(Product_ID) + ",";

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductTagInfo.Product_Tag_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductTagInfo.Product_Tag_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductTagInfo.Product_Tag_IsSupplier", "=", "1"));

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductTagInfo.Product_Tag_SupplierID", "=", tools.NullInt(Session["supplier_id"]).ToString()));

        Query.OrderInfos.Add(new OrderInfo("ProductTagInfo.Product_Tag_ID", "DESC"));
        IList<ProductTagInfo> entitys = MyProductTag.GetProductTags(Query, pub.CreateUserPrivilege("ed87eb87-dade-4fbc-804c-c139c1cbe9c8"));
        if (entitys != null)
        {
            foreach (ProductTagInfo entity in entitys)
            {
                if (strTag.IndexOf("," + entity.Product_Tag_ID.ToString() + ",") > 0)
                {
                    strHTML += " <input style=\"height:auto;\" type=\"checkbox\" name=\"ProductTag_ID\" value=\"" + entity.Product_Tag_ID + "\" checked=\"checked\">" + entity.Product_Tag_Name;
                }
                else
                {
                    strHTML += " <input style=\"height:auto;\" type=\"checkbox\" name=\"ProductTag_ID\" value=\"" + entity.Product_Tag_ID + "\">" + entity.Product_Tag_Name;
                }
            }
        }

        return strHTML;
    }


    //产品标签选择
    public bool ProductTagIsExist(int Product_ID)
    {
        bool ProductTagIsExist = false;
        string strTag = ",0";

        if (Product_ID > 0)
            strTag = "," + MyProduct.GetProductTag(Product_ID) + ",";

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductTagInfo.Product_Tag_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductTagInfo.Product_Tag_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductTagInfo.Product_Tag_IsSupplier", "=", "1"));

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductTagInfo.Product_Tag_SupplierID", "=", tools.NullInt(Session["supplier_id"]).ToString()));

        Query.OrderInfos.Add(new OrderInfo("ProductTagInfo.Product_Tag_ID", "DESC"));
        IList<ProductTagInfo> entitys = MyProductTag.GetProductTags(Query, pub.CreateUserPrivilege("ed87eb87-dade-4fbc-804c-c139c1cbe9c8"));
        if (entitys != null)
        {
            ProductTagIsExist = true;
        }

        return ProductTagIsExist;
    }

    //佣金分类选择
    public string Product_CommissionCate_Select(int cate_id)
    {
        string select_list = "";
        select_list = "<select id=\"Product_Supplier_CommissionCateID\" name=\"Product_Supplier_CommissionCateID\">";

        QueryInfo Query = new QueryInfo();
        Query.CurrentPage = 1;
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCommissionCategoryInfo.Supplier_Commission_Cate_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCommissionCategoryInfo.Supplier_Commission_Cate_SupplierID", "=", tools.NullInt(Session["supplier_id"]).ToString()));
        IList<SupplierCommissionCategoryInfo> entitys = MyCommissionCate.GetSupplierCommissionCategorys(Query, pub.CreateUserPrivilege("ed55dd89-e07e-438d-9529-a46de2cdda7b"));
        if (entitys != null)
        {
            select_list = select_list + "<option value=\"0\">选择佣金分类</option>";
            foreach (SupplierCommissionCategoryInfo entity in entitys)
            {
                if (cate_id == entity.Supplier_Commission_Cate_ID)
                {
                    select_list = select_list + "<option value=\"" + entity.Supplier_Commission_Cate_ID + "\" selected>" + entity.Supplier_Commission_Cate_Name + "</option>";
                }
                else
                {
                    select_list = select_list + "<option value=\"" + entity.Supplier_Commission_Cate_ID + "\">" + entity.Supplier_Commission_Cate_Name + "</option>";
                }
            }
        }
        else
        {
            select_list = select_list + "<option value=\"0\">默认佣金</option>";
        }
        select_list = select_list + "</select>";
        return select_list;
    }

    //佣金分类数量
    public int GetCommissionCateAmount()
    {
        int amount = 0;

        QueryInfo Query = new QueryInfo();
        Query.CurrentPage = 1;
        Query.PageSize = 0;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCommissionCategoryInfo.Supplier_Commission_Cate_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCommissionCategoryInfo.Supplier_Commission_Cate_SupplierID", "=", tools.NullInt(Session["supplier_id"]).ToString()));
        IList<SupplierCommissionCategoryInfo> entitys = MyCommissionCate.GetSupplierCommissionCategorys(Query, pub.CreateUserPrivilege("ed55dd89-e07e-438d-9529-a46de2cdda7b"));
        if (entitys != null)
        {
            amount = entitys.Count;
        }

        return amount;
    }

    //选择分组
    public string Select_Supplier_Cate(int cate_id, int current_cate, int select_id, int parentcount)
    {
        string result = "";
        int i = 0;
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCategoryInfo.Supplier_Cate_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCategoryInfo.Supplier_Cate_SupplierID", "=", supplier_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCategoryInfo.Supplier_Cate_Parentid", "=", cate_id.ToString()));
        if (current_cate > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCategoryInfo.Supplier_Cate_ID", "<>", current_cate.ToString()));
        }
        Query.OrderInfos.Add(new OrderInfo("SupplierCategoryInfo.Supplier_Cate_Sort", "Asc"));
        IList<SupplierCategoryInfo> entitys = MyProductCate.GetSupplierCategorys(Query);
        if (entitys != null)
        {
            foreach (SupplierCategoryInfo entity in entitys)
            {
                if (select_id == entity.Supplier_Cate_ID)
                {
                    result += "<option value=\"" + entity.Supplier_Cate_ID + "\" selected>";
                }
                else
                {
                    result += "<option value=\"" + entity.Supplier_Cate_ID + "\">";
                }
                for (i = 1; i <= parentcount; i++)
                {
                    result += "&nbsp; ";
                }
                result += entity.Supplier_Cate_Name + "</option>";
                result += Select_Supplier_Cate(entity.Supplier_Cate_ID, current_cate, select_id, parentcount + 1);
            }
        }
        return result;
    }

    public string SupplierCategoryTree(int parentid, string cateid)
    {
        string cateidbak = "," + cateid + ",";
        string strHTML = "";
        IList<SupplierCategoryInfo> entitys = GetSubCategorys(parentid);
        if (entitys != null)
        {
            foreach (SupplierCategoryInfo entity in entitys)
            {
                if (GetSubCategorysAmount(entity.Supplier_Cate_ID) > 0)
                {
                    if (cateidbak.IndexOf("," + entity.Supplier_Cate_ID + ",") >= 0)
                    {
                        strHTML += "<item text=\"" + entity.Supplier_Cate_Name + "\" name=\"checkbox\" id=\"" + entity.Supplier_Cate_ID + "\" open=\"yes\" checked=\"yes\">\n";
                    }
                    else
                    {
                        strHTML += "<item text=\"" + entity.Supplier_Cate_Name + "\" name=\"checkbox\" id=\"" + entity.Supplier_Cate_ID + "\">\n";
                    }
                    strHTML += SupplierCategoryTree(entity.Supplier_Cate_ID, cateid);
                    strHTML += "</item>\n";
                }
                else
                {
                    if (cateidbak.IndexOf("," + entity.Supplier_Cate_ID + ",") >= 0)
                    {
                        strHTML += "<item text=\"" + entity.Supplier_Cate_Name + "\" name=\"checkbox\" id=\"" + entity.Supplier_Cate_ID + "\" checked=\"yes\" />\n";
                    }
                    else
                    {
                        strHTML += "<item text=\"" + entity.Supplier_Cate_Name + "\" name=\"checkbox\" id=\"" + entity.Supplier_Cate_ID + "\" />\n";
                    }
                }
            }
        }
        return strHTML;
    }

    public IList<SupplierCategoryInfo> GetSubCategorys(int parentid)
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCategoryInfo.Supplier_Cate_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCategoryInfo.Supplier_Cate_SupplierID", "=", supplier_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCategoryInfo.Supplier_Cate_Parentid", "=", parentid.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierCategoryInfo.Supplier_Cate_Sort", "Asc"));
        IList<SupplierCategoryInfo> entitys = MyProductCate.GetSupplierCategorys(Query);
        return entitys;
    }

    public int GetSubCategorysAmount(int parentid)
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierCategoryInfo.Supplier_Cate_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCategoryInfo.Supplier_Cate_SupplierID", "=", supplier_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCategoryInfo.Supplier_Cate_Parentid", "=", parentid.ToString()));
        Query.OrderInfos.Add(new OrderInfo("SupplierCategoryInfo.Supplier_Cate_Sort", "Asc"));
        IList<SupplierCategoryInfo> entitys = MyProductCate.GetSupplierCategorys(Query);
        if (entitys != null)
        {
            return entitys.Count;
        }
        else
        {
            return 0;
        }
    }

    public string CategoryTree(int parentid, string cateid)
    {
        string cateidbak = "," + cateid + ",";
        string strHTML = "";
        IList<CategoryInfo> entitys = MyCBLL.GetSubCategorys(parentid, pub.GetCurrentSite(), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        if (entitys != null)
        {
            foreach (CategoryInfo entity in entitys)
            {
                if (entity.Cate_IsActive == 1)
                {
                    if (MyCBLL.GetSubCateCount(entity.Cate_ID, pub.GetCurrentSite(), pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb")) > 0)
                    {
                        if (cateidbak.IndexOf("," + entity.Cate_ID + ",") >= 0)
                        {
                            strHTML += "<item text=\"" + entity.Cate_Name + "\" name=\"checkbox\" id=\"" + entity.Cate_ID + "\" open=\"yes\" checked=\"yes\">\n";
                        }
                        else
                        {
                            strHTML += "<item text=\"" + entity.Cate_Name + "\" name=\"checkbox\" id=\"" + entity.Cate_ID + "\">\n";
                        }
                        strHTML += CategoryTree(entity.Cate_ID, cateid);
                        strHTML += "</item>\n";
                    }
                    else
                    {
                        if (cateidbak.IndexOf("," + entity.Cate_ID + ",") >= 0)
                        {
                            strHTML += "<item text=\"" + entity.Cate_Name + "\" name=\"checkbox\" id=\"" + entity.Cate_ID + "\" checked=\"yes\" />\n";
                        }
                        else
                        {
                            strHTML += "<item text=\"" + entity.Cate_Name + "\" name=\"checkbox\" id=\"" + entity.Cate_ID + "\" />\n";
                        }
                    }
                }
            }
        }
        return strHTML;
    }

    public string Get_Category_Relate(int cate_id, string cate_str)
    {

        string cate_relate = cate_id.ToString();
        if (cate_id > 0)
        {
            CategoryInfo category = MyCBLL.GetCategoryByID(cate_id, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
            if (category != null)
            {
                cate_relate = cate_relate + ",";
                cate_relate = cate_str + Get_Category_Relate(category.Cate_ParentID, cate_relate);
            }
            else
            {
                cate_relate = "0";
            }
        }
        else
        {
            if (cate_str != "")
            {
                cate_relate = cate_str + cate_relate;
            }
        }
        return cate_relate;

    }

    public string Product_Category_Select(int cate_id, string div_name)
    {
        string select_list = "";
        string select_tmp = "";
        int grade = 0;
        int i;
        int parentid = 0;
        string select_name = "";
        string cate_relate = Get_Category_Relate(cate_id, "");
        cate_relate = cate_relate + ",";
        foreach (string cate in cate_relate.Split(','))
        {
            if (cate.Length > 0)
            {

                QueryInfo Query = new QueryInfo();
                Query.CurrentPage = 1;
                Query.PageSize = 0;
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_ParentID", "=", cate));
                IList<CategoryInfo> categorys = MyCBLL.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
                if (categorys != null)
                {

                    grade = grade + 1;
                    if (grade == 1)
                    {
                        select_tmp = "<select id=\"Product_cate\" name=\"Product_cate\" onchange=\"change_maincate('" + div_name + "','Product_cate'); \">";
                        select_tmp = select_tmp + "<option value=\"0\">选择类别</option>";
                    }
                    else
                    {
                        select_name = "Product_cate";
                        for (i = 1; i < grade; i++)
                        {
                            select_name = select_name + "_parent";
                        }
                        select_tmp = "<select id=\"" + select_name + "\" name=\"" + select_name + "\" onchange=\"change_maincate('" + div_name + "','" + select_name + "');\">";
                        select_tmp = select_tmp + "<option value=\"0\">选择类别</option>";
                    }

                    foreach (CategoryInfo entity in categorys)
                    {
                        if (entity.Cate_IsActive == 1)
                        {
                            if (parentid == entity.Cate_ID || cate_id == entity.Cate_ID)
                            {
                                select_tmp = select_tmp + "<option value=\"" + entity.Cate_ID + "\" selected>" + entity.Cate_Name + "</option>";
                            }
                            else
                            {
                                select_tmp = select_tmp + "<option value=\"" + entity.Cate_ID + "\">" + entity.Cate_Name + "</option>";
                            }
                        }
                    }
                    select_tmp = select_tmp + "</select> ";
                    parentid = tools.CheckInt(cate);
                }

                Query = null;
                categorys = null;
                select_list = select_tmp + select_list;
            }
        }
        return select_list;
    }

    public string ProductTypeOption(int selectValue)
    {
        string strHTML = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductType.ProductType_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductType.ProductType_IsActive", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("ProductType.ProductType_Sort", "DESC"));
        IList<ProductTypeInfo> entitys = MyTBLL.GetProductTypes(Query, pub.CreateUserPrivilege(""));
        if (entitys != null)
        {
            foreach (ProductTypeInfo entity in entitys)
            {
                if (entity.ProductType_ID == selectValue)
                {
                    strHTML += "<option value=\"" + entity.ProductType_ID + "\" selected=\"selected\">" + entity.ProductType_Name + "</option>";
                }
                else
                {
                    strHTML += "<option value=\"" + entity.ProductType_ID + "\">" + entity.ProductType_Name + "</option>";
                }
            }
        }
        return strHTML;
    }

    public string ProductExtendEditor(int Product_TypeID, int Product_ID)
    {
        StringBuilder strHTML = new StringBuilder();
        string[] extDefault, valExts;
        string valExt = string.Empty, valImg = string.Empty;

        IList<ProductTypeExtendInfo> entitys = MyProduct.ProductExtendEditor(Product_TypeID);
        IList<ProductExtendInfo> extendList = null;

        if (entitys != null)
        {
            extendList = MyProduct.ProductExtendValue(Product_ID);

            strHTML.Append("<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"width:893px;\">");
            foreach (ProductTypeExtendInfo entity in entitys)
            {
                if (entity.ProductType_Extend_Gather == 0)
                {
                    valExts = GetProductExtendValue(entity.ProductType_Extend_ID, extendList).Split('|');

                    valExt = valExts[0];
                    valImg = valExts[1];

                    strHTML.Append("<tr>");
                    if (entity.ProductType_Extend_Name.Length > 8)
                    {
                        strHTML.Append("<td style=\"text-align:right;width:120px;cursor: pointer;\" title=\"" + entity.ProductType_Extend_Name + "\">" + entity.ProductType_Extend_Name.Substring(0, 8) + "<td>");
                    }
                    else
                    {
                        strHTML.Append("<td style=\"text-align:right;width:120px;cursor: pointer;\"  title=\"" + entity.ProductType_Extend_Name + "\">" + entity.ProductType_Extend_Name + "<td>");
                    }


                    strHTML.Append("<td style=\"text-align:left;\">");
                    switch (entity.ProductType_Extend_Display.ToLower())
                    {
                        case "text":
                            strHTML.Append("<input type=\"text\" name=\"product_extend" + entity.ProductType_Extend_ID + "\" value=\"" + valExt + "\" />");
                            break;
                        case "select":
                            extDefault = entity.ProductType_Extend_Default.Split('|');
                            strHTML.Append("<select name=\"product_extend" + entity.ProductType_Extend_ID + "\">");
                            foreach (string extText in extDefault)
                            {
                                if (extText == valExt)
                                {
                                    strHTML.Append("<option value=\"" + extText + "\" selected=\"selected\">" + extText + "</option>\n");
                                }
                                else
                                {
                                    strHTML.Append("<option value=\"" + extText + "\">" + extText + "</option>\n");
                                }
                            }
                            strHTML.Append("</select>");
                            break;
                        case "html":
                            strHTML.Append("<textarea style=\"display:none;\" cols=\"0\" rows=\"0\" id=\"tmp_content_" + entity.ProductType_Extend_ID + "\">" + valExt + "</textarea>");
                            strHTML.Append("<script type=\"text/javascript\">");
                            strHTML.Append("var oFCKeditor = new FCKeditor('p_extend" + entity.ProductType_Extend_ID + "');");
                            strHTML.Append("oFCKeditor.BasePath = \"/public/fckeditor/\";");
                            strHTML.Append("oFCKeditor.Width = 600;");
                            strHTML.Append("oFCKeditor.Height = 100;");
                            strHTML.Append("oFCKeditor.ToolbarSet = 'Basic';");
                            strHTML.Append("oFCKeditor.Value = MM_findObj('tmp_content_" + entity.ProductType_Extend_ID + "').value;");
                            strHTML.Append("oFCKeditor.Config['AutoDetectLanguage'] = false;");
                            strHTML.Append("oFCKeditor.Config['DefaultLanguage'] = \"zh-cn\";");
                            strHTML.Append("oFCKeditor.Create();");
                            strHTML.Append("</script>\n");
                            break;
                    }
                    strHTML.Append("<input type=\"hidden\" name=\"PExtend" + entity.ProductType_Extend_ID + "_img\" id=\"PExtend" + entity.ProductType_Extend_ID + "_img\" value=\"" + valImg + "\" />");
                    strHTML.Append("<td>");
                    strHTML.Append("</tr>");
                }
            }
            strHTML.Append("</table>");
        }
        return strHTML.ToString();
    }

    public string ProductExtendDisplay(int Product_TypeID, int Product_ID)
    {
        StringBuilder strHTML = new StringBuilder();
        string[] extDefault;
        string valExt = "";

        IList<ProductTypeExtendInfo> entitys = MyProduct.ProductExtendEditor(Product_TypeID);
        IList<ProductExtendInfo> extendList = null;

        if (entitys != null)
        {
            extendList = MyProduct.ProductExtendValue(Product_ID);

            strHTML.Append("<table cellspacing=\"3\" cellpadding=\"0\" border=\"0\">\n");
            foreach (ProductTypeExtendInfo entity in entitys)
            {
                valExt = GetProductExtendValue(entity.ProductType_Extend_ID, extendList);

                strHTML.Append("\t<tr>\n");
                strHTML.Append("\t\t<td style=\"text-align:right;\">" + entity.ProductType_Extend_Name + "<td>\n");
                strHTML.Append("\t\t<td>" + valExt + "<td>\n");
                strHTML.Append("\t</tr>\n");
            }
            strHTML.Append("</table>");
        }
        return strHTML.ToString();
    }

    public string GetProductExtendValue(int Extend_ID, IList<ProductExtendInfo> extendList)
    {
        string valExt = "|";
        try
        {
            if (extendList != null)
            {
                foreach (ProductExtendInfo entity in extendList)
                {
                    if (entity.Extent_ID == Extend_ID)
                    {
                        valExt = entity.Extend_Value + "|" + entity.Extend_Img;
                        break;
                    }
                }
            }
        }
        catch (Exception ex) { }

        return valExt;
    }

    #region 商品聚合

    public string ProductExtendEditorDisplay()
    {
        string extendValue, extendName;
        string Extend_Value;
        string Product_Price;
        int Product_ID;

        string product_ids = tools.NullStr(Session["selected_productid"]);
        if (product_ids == "")
        {
            product_ids = "0";
        }
        extendName = tools.CheckStr(Request["extendName"]);
        extendValue = tools.CheckStr(Request["extendValue"]);

        string[] valueArray = null;
        if (extendValue != "")
        {
            valueArray = extendValue.Split(',');
        }

        if (extendName == "")
        {
            extendName = tools.NullStr(Session["extend_name"]);
        }

        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("<table cellspacing=\"2\" cellpadding=\"0\" border=\"0\" style=\"width:100%\" id=\"table_extend\">");

        strHTML.Append("<tr>");
        strHTML.Append("<td class=\"name\" wight=\"200\">" + extendName + "</td>");
        strHTML.Append("<td class=\"name\" wight=\"200\">商品编码</td>");
        strHTML.Append("<td class=\"name\" wight=\"200\">库存</td>");
        strHTML.Append("</tr>");

        if (valueArray != null && product_ids == "0")
        {
            foreach (string items in valueArray)
            {
                if (items != "" && items != "0")
                {
                    strHTML.Append("<tr>");
                    strHTML.Append("<td>" + items + "</td>");
                    strHTML.Append("<td><input name=\"Product_Code" + items + "\" type=\"text\" id=\"Product_Code" + items + "\"  class=\"txt_input_border\"  value=\"\" onblur=\"check_product_code($(this).val(),0)\" /><i style=\"color:red;\">*</i></td>");
                    strHTML.Append("<td><input name=\"Product_StockAmount" + items + "\" type=\"text\" id=\"Product_StockAmount" + items + "\" value=\"0\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" class=\"txt_input_border\" /><i style=\"color:red;\">*</i></td>");
                    strHTML.Append("</tr>");
                }
            }
        }
        else
        {
            ProductInfo entity = null;
            SqlDataReader RdrList = null;
            string SqlList = null;

            try
            {
                if (valueArray != null)
                {
                    foreach (string strExtend in valueArray)
                    {
                        if (strExtend != "" && strExtend != "0")
                        {
                            SqlList = "select PE.Product_Extend_Value,PB.Product_ID from Product_Basic as PB left join Product_Extend as PE on PB.Product_ID=PE.Product_Extend_ProductID where PB.Product_ID in (" + product_ids + ") and PE.Product_Extend_Value='" + strExtend + "'";
                            RdrList = DBHelper.ExecuteReader(SqlList);
                            if (RdrList.HasRows)
                            {
                                while (RdrList.Read())
                                {
                                    Extend_Value = tools.NullStr(RdrList["Product_Extend_Value"]);
                                    Product_ID = tools.NullInt(RdrList["Product_ID"]);

                                    entity = GetProductByID(Product_ID);

                                    if (entity != null)
                                    {
                                        if (entity.Product_PriceType == 1)
                                        {
                                            Product_Price = pub.FormatCurrency(entity.Product_Price);
                                        }
                                        else
                                        {
                                            Product_Price = pub.FormatCurrency(pub.GetProductPrice(entity.Product_ManualFee, entity.Product_Weight));
                                        }

                                        strHTML.Append("<tr id=\"tr_extend_" + Extend_Value + "\">");

                                        strHTML.Append("<td>" + Extend_Value + "");
                                        strHTML.Append("<input type=\"hidden\" id=\"hidden_extend_product_" + Extend_Value + "\" name=\"hidden_extend_product_" + Extend_Value + "\" value=\"" + Product_ID + "\" />");
                                        strHTML.Append("<input type=\"hidden\" id=\"hidden_extend_status_" + Extend_Value + "\" name=\"hidden_extend_status_" + Extend_Value + "\" />");
                                        strHTML.Append("</td>");

                                        strHTML.Append("<td><input name=\"Product_Code" + Extend_Value + "\" type=\"text\" id=\"Product_Code" + Extend_Value + "\"  class=\"txt_input_border\"  value=\"" + entity.Product_Code + "\" readonly=\"readonly\" onblur=\"check_product_code($(this).val()," + Product_ID + ")\" /><i style=\"color:red;\">*</i></td>");
                                        strHTML.Append("<td><input name=\"Product_StockAmount" + Extend_Value + "\" type=\"text\" id=\"Product_StockAmount" + Extend_Value + "\" value=\"" + entity.Product_StockAmount + "\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" class=\"txt_input_border\" /><i style=\"color:red;\">*</i></td>");
                                        strHTML.Append("</tr>");
                                    }
                                }
                            }
                            else
                            {
                                strHTML.Append("<tr id=\"tr_extend_" + strExtend + "\">");

                                strHTML.Append("<td>" + strExtend + "");
                                strHTML.Append("<input type=\"hidden\" id=\"hidden_extend_product_" + strExtend + "\" name=\"hidden_extend_product_" + strExtend + "\" value=\"0\" />");
                                strHTML.Append("<input type=\"hidden\" id=\"hidden_extend_status_" + strExtend + "\" name=\"hidden_extend_status_" + strExtend + "\" />");
                                strHTML.Append("</td>");


                                strHTML.Append("<td><input name=\"Product_Code" + strExtend + "\" type=\"text\" id=\"Product_Code" + strExtend + "\"  class=\"txt_input_border\"  value=\"\" onblur=\"check_product_code($(this).val(),0)\" /><i style=\"color:red;\">*</i></td>");
                                strHTML.Append("<td><input name=\"Product_StockAmount" + strExtend + "\" type=\"text\" id=\"Product_StockAmount" + strExtend + "\" value=\"0\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" class=\"txt_input_border\" /><i style=\"color:red;\">*</i></td>");
                                strHTML.Append("</tr>");
                            }
                            RdrList.Close();
                        }
                    }
                }
                else
                {
                    SqlList = "select PE.Product_Extend_Value,PB.Product_ID from Product_Basic as PB left join Product_Extend as PE on PB.Product_ID=PE.Product_Extend_ProductID where PB.Product_ID in (" + product_ids + ") and PE.Product_Extend_ExtendID=" + tools.NullInt(Session["extend_id"]);
                    RdrList = DBHelper.ExecuteReader(SqlList);
                    if (RdrList.HasRows)
                    {
                        while (RdrList.Read())
                        {
                            Extend_Value = tools.NullStr(RdrList["Product_Extend_Value"]);
                            Product_ID = tools.NullInt(RdrList["Product_ID"]);

                            entity = GetProductByID(Product_ID);

                            if (entity != null)
                            {
                                if (entity.Product_PriceType == 1)
                                {
                                    Product_Price = pub.FormatCurrency(entity.Product_Price);
                                }
                                else
                                {
                                    Product_Price = pub.FormatCurrency(pub.GetProductPrice(entity.Product_ManualFee, entity.Product_Weight));
                                }

                                strHTML.Append("<tr id=\"tr_extend_" + Extend_Value + "\">");

                                strHTML.Append("<td>" + Extend_Value + "");
                                strHTML.Append("<input type=\"hidden\" id=\"hidden_extend_product_" + Extend_Value + "\" name=\"hidden_extend_product_" + Extend_Value + "\" value=\"" + Product_ID + "\" />");
                                strHTML.Append("<input type=\"hidden\" id=\"hidden_extend_status_" + Extend_Value + "\" name=\"hidden_extend_status_" + Extend_Value + "\" />");
                                strHTML.Append("</td>");


                                strHTML.Append("<td><input name=\"Product_Code" + Extend_Value + "\" type=\"text\" id=\"Product_Code" + Extend_Value + "\"  class=\"txt_input_border\"  value=\"" + entity.Product_Code + "\" onblur=\"check_product_code($(this).val()," + Product_ID + ")\" readonly=\"readonly\" /><i style=\"color:red;\">*</i></td>");
                                strHTML.Append("<td><input name=\"Product_StockAmount" + Extend_Value + "\" type=\"text\" id=\"Product_StockAmount" + Extend_Value + "\" value=\"" + entity.Product_StockAmount + "\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" class=\"txt_input_border\" /><i style=\"color:red;\">*</i></td>");
                                strHTML.Append("</tr>");
                            }
                        }
                    }
                }
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

        strHTML.Append("</table>");
        return strHTML.ToString();
    }


    public string ProductExtendCheckboxDisplay(int Product_TypeID, string Product_ID, string action)
    {
        StringBuilder strHTML = new StringBuilder();

        int i = 0;

        string valExt = string.Empty, valImg = string.Empty;

        IList<ProductTypeExtendInfo> entitys = MyProduct.ProductExtendEditor(Product_TypeID);
        IList<ProductExtendInfo> extendList = null;

        if (entitys != null)
        {
            //extendList = MyProduct.ProductExtendValue(Product_ID);

            foreach (ProductTypeExtendInfo entity in entitys)
            {
                if (entity.ProductType_Extend_Gather == 1)
                {
                    i++;
                    strHTML.Append("<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"margin: 15px 10px 15px 10px;width:893px;\">");
                    strHTML.Append("<tr>");
                    strHTML.Append("    <td style=\"font-size:14px;font-weight:bold;border-bottom:1px solid #ccc;text-align:left;padding-left:15px;\">" + entity.ProductType_Extend_Name + "&nbsp;&nbsp;<i style=\"color:red;font-style:normal;\">*</i></td>");
                    strHTML.Append("</tr>");

                    strHTML.Append("<tr>");
                    strHTML.Append("    <td>" + ProductExtendValueCheckboxDisplay(entity, Product_ID, action) + "</td>");
                    strHTML.Append("</tr>");
                    strHTML.Append("</table>");
                    if (i == 1)
                    {
                        break;
                    }
                }
            }
        }
        return strHTML.ToString();
    }

    /// <summary>
    /// 商品属性选择
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="Product_ID"></param>
    /// <returns></returns>
    public string ProductExtendValueCheckboxDisplay(ProductTypeExtendInfo entity, string Product_ID, string action)
    {
        StringBuilder strHTML = new StringBuilder();
        string[] extDefault, valExts;
        string valExt = string.Empty;

        if (entity != null)
        {
            strHTML.Append("<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\" style=\"margin: 15px 10px 15px 10px;width:700px;\">");
            strHTML.Append("<tr>");
            strHTML.Append("    <td style=\"font-size:12px;text-align:left;padding-left:15px;\">");
            if (entity.ProductType_Extend_Gather == 1)
            {
                valExt = MyProduct.GetProductExtendValue(entity.ProductType_Extend_ID, Product_ID);

                Session["extend_id"] = entity.ProductType_Extend_ID;
                Session["extend_name"] = entity.ProductType_Extend_Name;
                Session["extend_value"] = valExt;


                switch (entity.ProductType_Extend_Display.ToLower())
                {
                    case "select":
                        extDefault = entity.ProductType_Extend_Default.Split('|');

                        int i = 0;
                        foreach (string extText in extDefault)
                        {
                            i++;
                            if (valExt.Contains(extText))
                            {
                                strHTML.Append("<input type=\"checkbox\" name=\"product_extend_" + entity.ProductType_Extend_ID + "\" checked=\"checked\" value=\"" + extText + "\" onclick=\"LoadProductExtends(" + entity.ProductType_Extend_ID + ",'" + entity.ProductType_Extend_Name + "','" + extText + "');\" />" + extText + "&nbsp;&nbsp;");
                            }
                            //else if (i == 1)
                            //{
                            //    strHTML.Append("<input type=\"checkbox\" name=\"product_extend_" + entity.ProductType_Extend_ID + "\" checked=\"checked\" value=\"" + extText + "\" onclick=\"LoadProductExtends(" + entity.ProductType_Extend_ID + ",'" + entity.ProductType_Extend_Name + "','" + extText + "');\" />" + extText + "&nbsp;&nbsp;");
                            //}
                            else
                            {
                                strHTML.Append("<input type=\"checkbox\" name=\"product_extend_" + entity.ProductType_Extend_ID + "\" value=\"" + extText + "\" onclick=\"LoadProductExtends(" + entity.ProductType_Extend_ID + ",'" + entity.ProductType_Extend_Name + "','" + extText + "');\"  />" + extText + "&nbsp;&nbsp;");
                            }
                        }
                        break;
                }
            }

            strHTML.Append("<input id=\"chk_display_extend\" name=\"chk_display_extend\" type=\"hidden\" value=\"" + entity.ProductType_Extend_ID + "\" />");
            strHTML.Append("</td>");
            strHTML.Append("</tr>");
            strHTML.Append("</table>");
        }
        return strHTML.ToString();
    }

    public string ProductExtendAppend()
    {
        StringBuilder strHTML = new StringBuilder();

        string Extend_ID = tools.CheckStr(Request["Extend_ID"]);

        strHTML.Append("<tr>");
        strHTML.Append("<td>" + Extend_ID + "</td>");
        strHTML.Append("<td><input name=\"Product_Code" + Extend_ID + "\" type=\"text\" id=\"Product_Code" + Extend_ID + "\"  class=\"txt_input_border\"  value=\"\" onblur=\"check_product_code($(this).val())\" /><i style=\"color:red;\">*</i></td>");
        strHTML.Append("<td><input name=\"Product_StockAmount" + Extend_ID + "\" type=\"text\" id=\"Product_StockAmount" + Extend_ID + "\" value=\"0\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" class=\"txt_input_border\" /><i style=\"color:red;\">*</i></td>");
        strHTML.Append("</tr>");

        return strHTML.ToString();
    }

    #endregion

    public string ProductBrandOption(int Product_TypeID, int selectValue)
    {
        string strHTML = "";
        IList<BrandInfo> entitys = MyTBLL.GetProductBrands(Product_TypeID, pub.CreateUserPrivilege(""));
        if (entitys != null)
        {
            foreach (BrandInfo entity in entitys)
            {
                if (entity.Brand_ID == selectValue)
                {
                    strHTML += "<option value=\"" + entity.Brand_ID + "\" selected=\"selected\">" + entity.Brand_Name + "</option>";
                }
                else
                {
                    strHTML += "<option value=\"" + entity.Brand_ID + "\">" + entity.Brand_Name + "</option>";
                }
            }
        }

        return strHTML;

    }

    public bool CheckProductCode(string code, int id)
    {
        if (code == null || code.Length == 0) { return false; }

        ProductInfo Entity = MyProduct.GetProductByCode(code, pub.GetCurrentSite(), pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (Entity != null)
        {
            if (id == 0) { return false; }

            if (Entity.Product_ID == id) { return true; }
            else { return false; }
        }
        else
        {
            return true;
        }
    }

    public void Check_Product_Code()
    {
        string code = tools.CheckStr(Request["product_code"]);
        int id = tools.CheckInt(Request["id"]);

        if (code != "")
        {
            ProductInfo Entity = MyProduct.GetProductByCode(code, pub.GetCurrentSite(), pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            if (Entity != null)
            {
                if (Entity.Product_ID == id)
                {
                    Response.Write("success");
                    Response.End();
                }
                else
                {
                    Response.Write("商品编号已存在");
                    Response.End();
                }
            }
            else
            {
                Response.Write("success");
                Response.End();
            }
        }
        else
        {
            Response.Write("商品编号不能为空");
            Response.End();
        }
    }


    public void AddProduct_bak()
    {
        int pro_id = 0;
        int Product_ID = tools.CheckInt(Request.Form["Product_ID"]);
        int Product_Supplier_CommissionCateID = tools.CheckInt(Request.Form["Product_Supplier_CommissionCateID"]);
        string Product_Code = tools.CheckStr(Request.Form["Product_Code"]);
        string Product_CateID = tools.CheckStr(Request.Form["Product_CateID"]);
        string Product_GroupID = tools.CheckStr(Request.Form["Product_GroupID"]);
        int Product_Cate = tools.CheckInt(Request.Form["Product_Cate"]);
        int Product_BrandID = tools.CheckInt(Request.Form["Product_BrandID"]);
        int Product_TypeID = tools.CheckInt(Request.Form["Product_TypeID"]);
        string Product_Name = tools.CheckStr(Request.Form["Product_Name"]);
        string Product_NameInitials = pub.GetFirstLetter(Product_Name);
        string Product_SubName = tools.CheckStr(Request.Form["Product_SubName"]);
        string Product_SubNameInitials = pub.GetFirstLetter(Product_SubName);
        double Product_MKTPrice = tools.CheckFloat(Request.Form["Product_MKTPrice"]);
        double Product_GroupPrice = tools.CheckFloat(Request.Form["Product_GroupPrice"]);
        double Product_Price = tools.CheckFloat(Request.Form["Product_Price"]);
        string Product_PriceUnit = tools.CheckStr(Request.Form["Product_PriceUnit"]);
        string Product_Unit = tools.CheckStr(Request.Form["Product_Unit"]);
        int Product_GroupNum = tools.CheckInt(Request.Form["Product_GroupNum"]);
        string Product_Note = tools.CheckStr(Request.Form["Product_Note"]);
        string Product_NoteColor = tools.CheckStr(Request.Form["Product_NoteColor"]);
        double Product_Weight = tools.CheckFloat(Request.Form["Product_Weight"]);
        string Product_Img = tools.CheckStr(Request.Form["Product_Img"]);
        int Product_StockAmount = tools.CheckInt(Request.Form["Product_StockAmount"]);
        int Product_IsInsale = tools.CheckInt(Request.Form["Product_IsInsale"]);
        int Product_IsGroupBuy = tools.CheckInt(Request.Form["Product_IsGroupBuy"]);
        int Product_IsCoinBuy = tools.CheckInt(Request.Form["Product_IsCoinBuy"]);
        int Product_IsFavor = tools.CheckInt(Request.Form["Product_IsFavor"]);
        int Product_IsGift = tools.CheckInt(Request.Form["Product_IsGift"]);
        int Product_IsAudit = tools.CheckInt(Request.Form["Product_IsAudit"]);
        int Product_IsGiftCoin = tools.CheckInt(Request.Form["Product_IsGiftCoin"]);
        double Product_Gift_Coin = tools.CheckFloat(Request.Form["Product_Gift_Coin"]);
        int Product_CoinBuy_Coin = tools.CheckInt(Request.Form["Product_CoinBuy_Coin"]);
        string Product_Intro = tools.CheckHTML(Request.Form["Product_Intro"]);
        int Product_AlertAmount = tools.CheckInt(Request.Form["Product_AlertAmount"]);
        int Product_IsNoStock = tools.CheckInt(Request.Form["Product_IsNoStock"]);
        string Product_Spec = tools.CheckStr(Request.Form["Product_Spec"]);
        string Product_Maker = tools.CheckStr(Request.Form["Product_Maker"]);
        int Product_Sort = tools.CheckInt(Request.Form["Product_Sort"]);
        int Product_QuotaAmount = tools.CheckInt(Request.Form["Product_QuotaAmount"]);
        int U_Product_SalesByProxy = tools.CheckInt(Request.Form["U_Product_SalesByProxy"]);
        int U_Product_Shipper = tools.CheckInt(Request.Form["U_Product_Shipper"]);
        string U_Product_Parameters = tools.CheckHTML(Request.Form["U_Product_Parameters"]);
        int Product_SupplierID = tools.NullInt(Session["supplier_id"]);
        Product_IsFavor = 1;
        Product_IsAudit = 1;
        string product_img = tools.CheckStr(Request.Form["product_img"]);
        string product_img_ext_1 = tools.CheckStr(Request.Form["product_img_ext_1"]);
        string product_img_ext_2 = tools.CheckStr(Request.Form["product_img_ext_2"]);
        string product_img_ext_3 = tools.CheckStr(Request.Form["product_img_ext_3"]);
        string product_img_ext_4 = tools.CheckStr(Request.Form["product_img_ext_4"]);

        string Product_Keyword1 = tools.CheckStr(Request.Form["Product_Keyword1"]);
        string Product_Keyword2 = tools.CheckStr(Request.Form["Product_Keyword2"]);
        string Product_Keyword3 = tools.CheckStr(Request.Form["Product_Keyword3"]);
        string Product_Keyword4 = tools.CheckStr(Request.Form["Product_Keyword4"]);
        string Product_Keyword5 = tools.CheckStr(Request.Form["Product_Keyword5"]);
        string Product_Keyword = Product_Keyword1 + "|" + Product_Keyword2 + "|" + Product_Keyword3 + "|" + Product_Keyword4 + "|" + Product_Keyword5;


        double Product_Grade_Price = 0;

        string ProductTag_ID = tools.CheckStr(Request.Form["ProductTag_ID"]);

        string ImgPath = product_img + "|" + product_img_ext_1 + "|" + product_img_ext_2 + "|" + product_img_ext_3 + "|" + product_img_ext_4;
        string[] ProductImages = ImgPath.Split('|');

        string[] cateArray = Product_CateID.Split(',');
        string[] tagArray = ProductTag_ID.Split(',');

        string U_Product_DeliveryCycle = tools.CheckStr(Request.Form["U_Product_DeliveryCycle"]);
        int U_Product_MinBook = tools.CheckInt(Request.Form["U_Product_MinBook"]);

        int Product_PriceType = tools.CheckInt(Request["Product_PriceType"]);
        double Product_ManualFee = tools.CheckFloat(Request["Product_ManualFee"]);
        string Product_LibraryImg = tools.CheckStr(Request["Product_LibraryImg"]);

        if (!CheckProductCode(Product_Code, 0))
        {
            pub.Msg("error", "错误信息", "已存在该商品编码，请尝试更换其他编码！", false, "{back}");
            return;
        }

        if (Product_Name.Length == 0)
        {
            pub.Msg("error", "错误信息", "请填写产品名称", false, "{back}");
            return;
        }

        if (Product_Supplier_CommissionCateID == 0 && GetCommissionCateAmount() > 0)
        {
            pub.Msg("error", "错误信息", "请选择佣金分类", false, "{back}");
            return;
        }

        //if (Product_BrandID <= 0)
        //{
        //    pub.Msg("error", "错误信息", "请选择产品品牌", false, "{back}");
        //    return;
        //}

        if (Product_PriceType == 1)
        {
            if (Product_Price <= 0)
            {
                pub.Msg("error", "错误信息", "请填写商品价格", false, "{back}");
                return;
            }
        }
        else
        {
            //if (Product_ManualFee <= 0)
            //{
            //    pub.Msg("error", "错误信息", "请填写手工费", false, "{back}");
            //    return;
            //}
        }

        if (Product_StockAmount <= 0)
        {
            pub.Msg("error", "错误信息", "请填写产品库存", false, "{back}");
            return;
        }

        if (U_Product_MinBook < 1)
        {
            pub.Msg("error", "错误信息", "最小起订量必须大于0", false, "{back}");
            return;
        }

        int min_wholeprice = 0;
        int max_wholeprice = 0;
        double product_wholeprice_amount = 0.00;
        int check_num = 0;
        for (int i = 1; i <= 10; i++)
        {
            min_wholeprice = tools.CheckInt(Request["min_product_wholeprice_amount_" + i]);
            max_wholeprice = tools.CheckInt(Request["max_product_wholeprice_amount_" + i]);
            if (max_wholeprice == 0)
            {
                if (i == 1)
                { }
                else
                {
                    if (min_wholeprice == 0)
                    { }
                    else
                    {
                        if (min_wholeprice != check_num + 1)
                        {
                            pub.Msg("error", "错误信息", "批发价格设置不符合规范", false, "{back}");
                            return;
                        }
                    }
                }
                break;
            }
            else
            {
                if (i == 1)
                {

                }
                else
                {
                    if (min_wholeprice != check_num + 1)
                    {
                        pub.Msg("error", "错误信息", "批发价格设置不符合规范", false, "{back}");
                        return;
                    }

                    if (min_wholeprice > max_wholeprice)
                    {
                        pub.Msg("error", "错误信息", "批发价格设置不符合规范", false, "{back}");
                        return;
                    }
                }
            }
            check_num = max_wholeprice;
        }


        ProductInfo entity = new ProductInfo();
        entity.Product_ID = Product_ID;
        entity.Product_Code = Product_Code;
        entity.Product_CateID = Product_Cate;
        entity.Product_BrandID = Product_BrandID;
        entity.Product_TypeID = Product_TypeID;
        entity.Product_SupplierID = Product_SupplierID;
        entity.Product_Supplier_CommissionCateID = Product_Supplier_CommissionCateID;
        entity.Product_Name = Product_Name;
        entity.Product_NameInitials = Product_NameInitials;
        entity.Product_SubName = Product_SubName;
        entity.Product_SubNameInitials = Product_SubNameInitials;
        entity.Product_MKTPrice = Product_MKTPrice;
        entity.Product_GroupPrice = Product_GroupPrice;
        entity.Product_PurchasingPrice = 0;
        entity.Product_Price = Product_Price;
        entity.Product_PriceUnit = Product_PriceUnit;
        entity.Product_Unit = Product_Unit;
        entity.Product_GroupNum = Product_GroupNum;
        entity.Product_Note = Product_Note;
        entity.Product_NoteColor = Product_NoteColor;
        entity.Product_Audit_Note = "";
        entity.Product_Weight = Product_Weight;
        entity.Product_Img = Product_Img;
        entity.Product_Publisher = "";
        entity.Product_StockAmount = Product_StockAmount;
        entity.Product_SaleAmount = 0;
        entity.Product_Review_Count = 0;
        entity.Product_Review_ValidCount = 0;
        entity.Product_Review_Average = 0;
        entity.Product_IsInsale = Product_IsInsale;
        entity.Product_IsGroupBuy = Product_IsGroupBuy;
        entity.Product_IsCoinBuy = Product_IsCoinBuy;
        entity.Product_IsFavor = Product_IsFavor;
        entity.Product_IsGift = Product_IsGift;
        entity.Product_IsAudit = Product_IsAudit;
        entity.Product_IsGiftCoin = Product_IsGiftCoin;
        entity.Product_Gift_Coin = Product_Gift_Coin;
        entity.Product_CoinBuy_Coin = Product_CoinBuy_Coin;
        entity.Product_Addtime = DateTime.Now;
        entity.Product_Intro = Product_Intro;
        entity.Product_AlertAmount = Product_AlertAmount;
        entity.Product_UsableAmount = Product_StockAmount;
        entity.Product_IsNoStock = 0;
        entity.Product_Spec = Product_Spec;
        entity.Product_Maker = Product_Maker;
        entity.Product_Sort = Product_Sort;
        entity.Product_Hits = 0;
        entity.Product_QuotaAmount = Product_QuotaAmount;
        entity.Product_Site = pub.GetCurrentSite();
        entity.U_Product_SalesByProxy = U_Product_SalesByProxy;
        entity.U_Product_Shipper = U_Product_Shipper;
        entity.Product_IsListShow = 1;
        entity.Product_GroupCode = "";
        entity.U_Product_MinBook = U_Product_MinBook;
        entity.U_Product_DeliveryCycle = U_Product_DeliveryCycle;
        entity.U_Product_Parameters = U_Product_Parameters;
        entity.Product_PriceType = Product_PriceType;
        entity.Product_ManualFee = Product_ManualFee;
        entity.Product_LibraryImg = Product_LibraryImg;

        IList<ProductExtendInfo> extends = ReadProductExtend(0);

        if (MyProduct.AddProduct(entity, cateArray, tagArray, ProductImages, extends, pub.CreateUserPrivilege("a8dcfdfb-2227-40b3-a598-9643fd4c7e18")))
        {
            ProductWholeSalePriceInfo saleinfo = new ProductWholeSalePriceInfo(); ;
            for (int i = 1; i <= 10; i++)
            {
                min_wholeprice = tools.CheckInt(Request["min_product_wholeprice_amount_" + i]);
                max_wholeprice = tools.CheckInt(Request["max_product_wholeprice_amount_" + i]);
                product_wholeprice_amount = tools.CheckFloat(Request["product_wholeprice_amount_" + i]);
                if (max_wholeprice != 0)
                {
                    saleinfo.Product_WholeSalePrice_MinAmount = min_wholeprice;
                    saleinfo.Product_WholeSalePrice_MaxAmount = max_wholeprice;
                    saleinfo.Product_WholeSalePrice_Price = product_wholeprice_amount;
                    saleinfo.Product_WholeSalePrice_ProductID = entity.Product_ID;
                    saleinfo.Product_WholeSalePrice_Site = pub.GetCurrentSite();
                    MySalePrice.AddProductWholeSalePrice(saleinfo);
                }
                else
                {
                    if (min_wholeprice > 0 && product_wholeprice_amount > 0)
                    {
                        saleinfo.Product_WholeSalePrice_MinAmount = min_wholeprice;
                        saleinfo.Product_WholeSalePrice_MaxAmount = max_wholeprice;
                        saleinfo.Product_WholeSalePrice_Price = product_wholeprice_amount;
                        saleinfo.Product_WholeSalePrice_ProductID = entity.Product_ID;
                        saleinfo.Product_WholeSalePrice_Site = pub.GetCurrentSite();
                        MySalePrice.AddProductWholeSalePrice(saleinfo);
                    }
                    break;
                }
            }

            pro_id = entity.Product_ID;
            ProductInfo productinfo = new ProductInfo();
            productinfo = MyProduct.GetProductByCode(Product_Code, pub.GetCurrentSite(), pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            if (productinfo != null)
            {
                MyProduct.SaveProductTag(productinfo.Product_ID, tagArray);
                SupplierProductCategoryInfo productgroup;
                foreach (string subcate in Product_GroupID.Split(','))
                {
                    if (tools.CheckInt(subcate) > 0)
                    {
                        productgroup = new SupplierProductCategoryInfo();
                        productgroup.Supplier_Product_Cate_CateID = tools.CheckInt(subcate);
                        productgroup.Supplier_Product_Cate_ProductID = productinfo.Product_ID;
                        MyProductCate.AddSupplierProductCategory(productgroup);
                        productgroup = null;
                    }
                }

                #region 更新聚合信息

                string group_productid = "";
                string Group_Code = "";

                group_productid = tools.NullStr(Request["sltproductid"]);
                if (group_productid.Length > 0)
                {
                    Group_Code = tools.NullStr(Guid.NewGuid());
                    foreach (string subproductid in group_productid.Split(','))
                    {
                        if (tools.CheckInt(subproductid) > 0)
                        {
                            MyProduct.UpdateProductGroupInfo(Group_Code, tools.NullInt(Request["islistshow_" + tools.CheckInt(subproductid)]), tools.CheckInt(subproductid));
                        }
                    }
                    MyProduct.UpdateProductGroupInfo(Group_Code, tools.NullInt(Request["islistshow_" + productinfo.Product_ID]), productinfo.Product_ID);
                }

                #endregion

                #region 更新会员价格
                QueryInfo Query = new QueryInfo();
                Query.PageSize = 0;
                Query.CurrentPage = 1;
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberGradeInfo.Member_Grade_Site", "=", pub.GetCurrentSite()));
                Query.OrderInfos.Add(new OrderInfo("MemberGradeInfo.Member_Grade_ID", "asc"));
                IList<MemberGradeInfo> grades = MyGrade.GetMemberGrades(Query, pub.CreateUserPrivilege("1c955ea6-881f-48d8-ba8d-c5aa7ce9cfea"));
                if (grades != null)
                {
                    foreach (MemberGradeInfo grade in grades)
                    {
                        Product_Grade_Price = tools.CheckFloat(Request.Form["Product_Grade_Price_" + grade.Member_Grade_ID]);
                        if (Product_Grade_Price > 0)
                        {
                            ProductPriceInfo priceinfo = new ProductPriceInfo();
                            priceinfo.Product_Price_ID = 0;
                            priceinfo.Product_Price_MemberGradeID = grade.Member_Grade_ID;
                            priceinfo.Product_Price_Price = Product_Grade_Price;
                            priceinfo.Product_Price_ProcutID = productinfo.Product_ID;
                            MyPrice.AddProductPrice(priceinfo);
                        }
                    }
                }
                #endregion

            }
            pub.Msg("positive", "操作成功", "操作成功", true, "product_add.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void Check_Product_Step()
    {
        string Product_CateID, Product_Name, Product_Code;
        int Product_TypeID, Product_Cate;

        string Result = "success";

        Product_CateID = tools.CheckStr(Request["Product_CateID"]);
        Product_TypeID = tools.CheckInt(Request["Product_TypeID"]);
        Product_Cate = tools.CheckInt(Request["Product_Cate"]);
        Product_Name = tools.CheckStr(Request["Product_Name"]);
        Product_Code = tools.CheckStr(Request["Product_Code"]);
        if (Product_Cate == 0)
        {
            Product_Cate = tools.CheckInt(Request["Product_Cate_parent"]);
        }

        if (Product_Cate == 0)
        {
            Result = "请选择商品主分类！";
        }

        //if (Product_TypeID == 0)
        //{
        //    Result = "请选择商品所属类型！";
        //}

        if (Product_Name.Length == 0)
        {
            Result = "请填写商品名称！";
        }

        Response.Write(Result);
        Response.End();
    }
    public void AddProduct()
    {
        int pro_id = 0;
        int Product_ID = tools.CheckInt(Request.Form["Product_ID"]);
        int Product_Supplier_CommissionCateID = tools.CheckInt(Request.Form["Product_Supplier_CommissionCateID"]);



        string Product_Code = "";

        int jj = 1;
        for (int i = 0; i < jj; i++)
        {
            if ((!new Product().CheckProductCode(Product_Code, 0)) || (Product_Code.Length < 1))
            {
                Product_Code = pub.GenerateRandomNumber(20);
                jj++;
            }
        }
        string Product_CateID = tools.CheckStr(Request.Form["Product_Cate"]);
        Myorder.Orders_Log(Product_ID, "添加商品", "添加商品", "Product_CateID是否有值", "" + Product_CateID + "---是否存在");
        //string Product_CateID = tools.CheckStr(Request.Form["Product_CateID"]);
        string Product_GroupID = tools.CheckStr(Request.Form["Product_GroupID"]);
        int Product_Cate = tools.CheckInt(Request.Form["Product_Cate"]);
        int Product_BrandID = tools.CheckInt(Request.Form["Product_BrandID"]);
        int Product_TypeID = tools.CheckInt(Request.Form["Product_TypeID"]);
        string Product_Name = tools.CheckStr(Request.Form["Product_Name"]);
        string Product_NameInitials = pub.GetFirstLetter(Product_Name);
        string Product_SubName = tools.CheckStr(Request.Form["Product_SubName"]);
        string Product_SubNameInitials = pub.GetFirstLetter(Product_SubName);
        double Product_MKTPrice = tools.CheckFloat(Request.Form["Product_MKTPrice"]);
        double Product_GroupPrice = tools.CheckFloat(Request.Form["Product_GroupPrice"]);
        double Product_Price = tools.CheckFloat(Request.Form["Product_Price"]);
        string Product_PriceUnit = tools.CheckStr(Request.Form["Product_PriceUnit"]);
        string Product_Unit = tools.CheckStr(Request.Form["Product_Unit"]);
        int Product_GroupNum = tools.CheckInt(Request.Form["Product_GroupNum"]);
        string Product_Note = tools.CheckStr(Request.Form["Product_Note"]);
        string Product_NoteColor = tools.CheckStr(Request.Form["Product_NoteColor"]);
        double Product_Weight = tools.CheckFloat(Request.Form["Product_Weight"]);
        string Product_Img = tools.CheckStr(Request.Form["Product_Img"]);
        string U_Product_DeliveryCycle = tools.CheckStr(Request.Form["U_Product_DeliveryCycle"]);
        //int Product_StockAmount = tools.CheckInt(Request.Form["Product_StockAmount"]);
        int Product_IsInsale = tools.CheckInt(Request.Form["Product_IsInsale"]);
        int Product_IsGroupBuy = tools.CheckInt(Request.Form["Product_IsGroupBuy"]);
        int Product_IsCoinBuy = tools.CheckInt(Request.Form["Product_IsCoinBuy"]);
        int Product_IsFavor = tools.CheckInt(Request.Form["Product_IsFavor"]);
        int Product_IsGift = tools.CheckInt(Request.Form["Product_IsGift"]);
        int Product_IsAudit = tools.CheckInt(Request.Form["Product_IsAudit"]);
        int Product_IsGiftCoin = tools.CheckInt(Request.Form["Product_IsGiftCoin"]);
        double Product_Gift_Coin = tools.CheckFloat(Request.Form["Product_Gift_Coin"]);
        int Product_CoinBuy_Coin = tools.CheckInt(Request.Form["Product_CoinBuy_Coin"]);
        string Product_Intro = tools.CheckHTML(Request.Form["Product_Intro"]);
        int Product_AlertAmount = tools.CheckInt(Request.Form["Product_AlertAmount"]);
        int Product_IsNoStock = tools.CheckInt(Request.Form["Product_IsNoStock"]);
        string Product_Spec = tools.CheckStr(Request.Form["Product_Spec"]);
        string Product_Maker = tools.CheckStr(Request.Form["Product_Maker"]);
        int Product_Sort = tools.CheckInt(Request.Form["Product_Sort"]);
        int Product_QuotaAmount = tools.CheckInt(Request.Form["Product_QuotaAmount"]);
        int U_Product_SalesByProxy = tools.CheckInt(Request.Form["U_Product_SalesByProxy"]);
        int U_Product_Shipper = tools.CheckInt(Request.Form["U_Product_Shipper"]);
        string U_Product_Parameters = tools.CheckHTML(tools.NullStr(Request.Form["U_Product_Parameters"]));
        int Product_SupplierID = tools.NullInt(Session["supplier_id"]);
        Product_IsFavor = 1;
        //Product_IsAudit = 1;
        string product_img = tools.CheckStr(Request.Form["product_img"]);
        string product_img_ext_1 = tools.CheckStr(Request.Form["product_img_ext_1"]);
        string product_img_ext_2 = tools.CheckStr(Request.Form["product_img_ext_2"]);
        string product_img_ext_3 = tools.CheckStr(Request.Form["product_img_ext_3"]);
        string product_img_ext_4 = tools.CheckStr(Request.Form["product_img_ext_4"]);

        string Product_Keyword1 = tools.CheckStr(Request.Form["Product_Keyword1"]);
        string Product_Keyword2 = tools.CheckStr(Request.Form["Product_Keyword2"]);
        string Product_Keyword3 = tools.CheckStr(Request.Form["Product_Keyword3"]);
        string Product_Keyword4 = tools.CheckStr(Request.Form["Product_Keyword4"]);
        string Product_Keyword5 = tools.CheckStr(Request.Form["Product_Keyword5"]);
        string Product_Keyword = Product_Keyword1 + "|" + Product_Keyword2 + "|" + Product_Keyword3 + "|" + Product_Keyword4 + "|" + Product_Keyword5;
        string Product_State_Name = tools.CheckStr(Request.Form["Product_State_Name"]);
        string Product_City_Name = tools.CheckStr(Request.Form["Product_City_Name"]);
        string Product_County_Name = tools.CheckStr(Request.Form["Product_County_Name"]);

        double Product_Grade_Price = 0;

        string ProductTag_ID = tools.CheckStr(Request.Form["ProductTag_ID"]);

        string ImgPath = product_img + "|" + product_img_ext_1 + "|" + product_img_ext_2 + "|" + product_img_ext_3 + "|" + product_img_ext_4;
        string[] ProductImages = ImgPath.Split('|');

        string[] cateArray = Product_CateID.Split(',');
        string[] tagArray = ProductTag_ID.Split(',');


        int U_Product_MinBook = tools.CheckInt(Request.Form["U_Product_MinBook"]);

        int Product_PriceType = tools.CheckInt(Request["Product_PriceType"]);
        double Product_ManualFee = tools.CheckFloat(Request["Product_ManualFee"]);
        string Product_LibraryImg = tools.CheckStr(Request["Product_LibraryImg"]);

        string Product_Extends = tools.CheckStr(Request["Product_Extends"]);
        string[] extendArr = Product_Extends.Split(',');

        if (!CheckProductCode(Product_Code, 0))
        {
            pub.Msg("error", "错误信息", "已存在该商品编码，请尝试更换其他编码！", false, "{back}");
            return;
        }

        if (Product_Name.Length == 0)
        {
            Response.Write("请填写产品名称");
            Response.End();
        }

        if (Product_Supplier_CommissionCateID == 0 && GetCommissionCateAmount() > 0)
        {
            Response.Write("请选择佣金分类");
            Response.End();
        }

        if (Product_Supplier_CommissionCateID == 0 && GetCommissionCateAmount() > 0)
        {
            Response.Write("请选择佣金分类");
            Response.End();
        }

        //if (Product_BrandID <= 0)
        //{
        //    Response.Write("请选择产品品牌");
        //    Response.End();
        //}

        if (Product_PriceType == 0)
        {
            if (Product_Price <= 0)
            {
                Response.Write("请填写商品价格");
                Response.End();
            }
        }
        else
        {
            //if (Product_ManualFee <= 0)
            //{
            //    Response.Write("请填写手工费");
            //    Response.End();
            //}
        }


        if (U_Product_MinBook < 1)
        {
            Response.Write("最小起订量必须大于0");
            Response.End();
        }

        int min_wholeprice = 0;
        int max_wholeprice = 0;
        double product_wholeprice_amount = 0.00;
        int check_num = 0;
        for (int i = 1; i <= 10; i++)
        {
            min_wholeprice = tools.CheckInt(Request["min_product_wholeprice_amount_" + i]);
            max_wholeprice = tools.CheckInt(Request["max_product_wholeprice_amount_" + i]);
            if (max_wholeprice == 0)
            {
                if (i == 1)
                { }
                else
                {
                    if (min_wholeprice == 0)
                    { }
                    else
                    {
                        if (min_wholeprice != check_num + 1)
                        {
                            Response.Write("批发价格设置不符合规范");
                            Response.End();
                        }
                    }
                }
                break;
            }
            else
            {
                if (i == 1)
                {

                }
                else
                {
                    if (min_wholeprice != check_num + 1)
                    {
                        Response.Write("批发价格设置不符合规范");
                        Response.End();
                    }

                    if (min_wholeprice > max_wholeprice)
                    {
                        Response.Write("批发价格设置不符合规范");
                        Response.End();
                    }
                }
            }
            check_num = max_wholeprice;
        }

        string Group_Code = tools.NullStr(Guid.NewGuid());
        int j = 0;


        ProductExtendInfo extendInfo = null;

        if (1 == 1)
        {
            foreach (string extend in extendArr)
            {
                //if (extend != "" && extend != "0")
                if (1 == 1)
                {
                    j++;
                    //string Product_Code = tools.CheckStr(Request.Form["Product_Code" + extend]);
                    //int Product_StockAmount = tools.CheckInt(Request.Form["Product_StockAmount" + extend]);
                    //string Product_Code = tools.CheckStr(Request.Form["Product_Code"]);
                    int Product_StockAmount = tools.CheckInt(Request.Form["Product_StockAmount"]);

                    if (Product_Code == "")
                    {
                        Response.Write("请输入商品编号");
                        Response.End();
                    }

                    if (Product_StockAmount == 0)
                    {
                        Response.Write("请输入商品库存");
                        Response.End();
                    }

                    ProductInfo entity = new ProductInfo();
                    entity.Product_ID = Product_ID;
                    entity.Product_Code = Product_Code;
                    entity.Product_CateID = Product_Cate;
                    entity.Product_BrandID = Product_BrandID;
                    entity.Product_TypeID = Product_TypeID;
                    entity.Product_SupplierID = Product_SupplierID;
                    entity.Product_Supplier_CommissionCateID = Product_Supplier_CommissionCateID;
                    entity.Product_Name = Product_Name;
                    entity.Product_NameInitials = Product_NameInitials;
                    entity.Product_SubName = Product_SubName;
                    entity.Product_SubNameInitials = Product_SubNameInitials;
                    entity.Product_MKTPrice = Product_MKTPrice;
                    entity.Product_GroupPrice = Product_GroupPrice;
                    entity.Product_PurchasingPrice = 0;
                    entity.Product_Price = Product_Price;
                    entity.Product_PriceUnit = Product_PriceUnit;
                    entity.Product_Unit = Product_Unit;
                    entity.Product_GroupNum = Product_GroupNum;
                    entity.Product_Note = Product_Note;
                    entity.Product_NoteColor = Product_NoteColor;
                    entity.Product_Audit_Note = "";
                    entity.Product_Weight = Product_Weight;
                    entity.Product_Img = Product_Img;
                    entity.Product_Publisher = "";
                    entity.Product_StockAmount = Product_StockAmount;
                    entity.Product_SaleAmount = 0;
                    entity.Product_Review_Count = 0;
                    entity.Product_Review_ValidCount = 0;
                    entity.Product_Review_Average = 0;
                    entity.Product_IsInsale = Product_IsInsale;
                    entity.Product_IsGroupBuy = Product_IsGroupBuy;
                    entity.Product_IsCoinBuy = Product_IsCoinBuy;
                    entity.Product_IsFavor = Product_IsFavor;
                    entity.Product_IsGift = Product_IsGift;
                    entity.Product_IsAudit = Product_IsAudit;
                    entity.Product_IsGiftCoin = Product_IsGiftCoin;
                    entity.Product_Gift_Coin = Product_Gift_Coin;
                    entity.Product_CoinBuy_Coin = Product_CoinBuy_Coin;
                    entity.Product_Addtime = DateTime.Now;
                    entity.Product_Intro = Product_Intro;
                    entity.Product_AlertAmount = Product_AlertAmount;
                    entity.Product_UsableAmount = Product_StockAmount;
                    entity.Product_IsNoStock = 0;
                    entity.Product_Spec = Product_Spec;
                    entity.Product_Maker = Product_Maker;
                    entity.Product_Sort = Product_Sort;
                    entity.Product_Hits = 0;
                    entity.Product_QuotaAmount = Product_QuotaAmount;
                    entity.Product_Site = pub.GetCurrentSite();
                    entity.U_Product_SalesByProxy = U_Product_SalesByProxy;
                    entity.U_Product_Shipper = U_Product_Shipper;
                    if (j == 1)
                    {
                        entity.Product_IsListShow = 1;
                    }
                    else
                    {
                        entity.Product_IsListShow = 0;
                    }
                    entity.Product_GroupCode = Group_Code;
                    entity.U_Product_MinBook = U_Product_MinBook;
                    entity.U_Product_DeliveryCycle = U_Product_DeliveryCycle;
                    entity.U_Product_Parameters = U_Product_Parameters;
                    entity.Product_PriceType = Product_PriceType;
                    entity.Product_ManualFee = Product_ManualFee;
                    entity.Product_LibraryImg = Product_LibraryImg;
                    entity.Product_State_Name = Product_State_Name;
                    entity.Product_City_Name = Product_City_Name;
                    entity.Product_County_Name = Product_County_Name;


                    IList<ProductExtendInfo> extends = ReadProductExtend(0);
                    extendInfo = new ProductExtendInfo();
                    extendInfo.Product_ID = Product_ID;
                    extendInfo.Extent_ID = tools.CheckInt(Request["chk_display_extend"]);
                    extendInfo.Extend_Value = tools.CheckStr(extend);
                    extends.Add(extendInfo);


                    if (MyProduct.AddProduct(entity, cateArray, tagArray, ProductImages, extends, pub.CreateUserPrivilege("a8dcfdfb-2227-40b3-a598-9643fd4c7e18")))
                    {
                        Myorder.Orders_Log(entity.Product_ID, "添加商品", "添加商品", "是否存在", "" + cateArray + "---是否存在");
                        ProductWholeSalePriceInfo saleinfo = new ProductWholeSalePriceInfo(); ;
                        for (int i = 1; i <= 10; i++)
                        {
                            min_wholeprice = tools.CheckInt(Request["min_product_wholeprice_amount_" + i]);
                            max_wholeprice = tools.CheckInt(Request["max_product_wholeprice_amount_" + i]);
                            product_wholeprice_amount = tools.CheckFloat(Request["product_wholeprice_amount_" + i]);
                            if (max_wholeprice != 0)
                            {
                                saleinfo.Product_WholeSalePrice_MinAmount = min_wholeprice;
                                saleinfo.Product_WholeSalePrice_MaxAmount = max_wholeprice;
                                saleinfo.Product_WholeSalePrice_Price = product_wholeprice_amount;
                                saleinfo.Product_WholeSalePrice_ProductID = entity.Product_ID;
                                saleinfo.Product_WholeSalePrice_Site = pub.GetCurrentSite();
                                MySalePrice.AddProductWholeSalePrice(saleinfo);
                            }
                            else
                            {
                                if (min_wholeprice > 0 && product_wholeprice_amount > 0)
                                {
                                    saleinfo.Product_WholeSalePrice_MinAmount = min_wholeprice;
                                    saleinfo.Product_WholeSalePrice_MaxAmount = max_wholeprice;
                                    saleinfo.Product_WholeSalePrice_Price = product_wholeprice_amount;
                                    saleinfo.Product_WholeSalePrice_ProductID = entity.Product_ID;
                                    saleinfo.Product_WholeSalePrice_Site = pub.GetCurrentSite();
                                    MySalePrice.AddProductWholeSalePrice(saleinfo);
                                }
                                break;
                            }
                        }

                        pro_id = entity.Product_ID;
                        ProductInfo productinfo = new ProductInfo();
                        productinfo = MyProduct.GetProductByCode(Product_Code, pub.GetCurrentSite(), pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                        if (productinfo != null)
                        {
                            MyProduct.SaveProductTag(productinfo.Product_ID, tagArray);
                            SupplierProductCategoryInfo productgroup;
                            foreach (string subcate in Product_GroupID.Split(','))
                            {
                                if (tools.CheckInt(subcate) > 0)
                                {
                                    productgroup = new SupplierProductCategoryInfo();
                                    productgroup.Supplier_Product_Cate_CateID = tools.CheckInt(subcate);
                                    productgroup.Supplier_Product_Cate_ProductID = productinfo.Product_ID;
                                    MyProductCate.AddSupplierProductCategory(productgroup);
                                    productgroup = null;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (extend == "")
                    {
                        Response.Write("操作失败，请维护扩展属性......");
                        Response.End();
                    }

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

    //public void AddProduct()
    //{
    //    int pro_id = 0;
    //    int Product_ID = tools.CheckInt(Request.Form["Product_ID"]);
    //    int Product_Supplier_CommissionCateID = tools.CheckInt(Request.Form["Product_Supplier_CommissionCateID"]);
    //    //string Product_Code = tools.CheckStr(Request.Form["Product_Code"]);
    //    string Product_CateID = tools.CheckStr(Request.Form["Product_CateID"]);
    //    string Product_GroupID = tools.CheckStr(Request.Form["Product_GroupID"]);
    //    int Product_Cate = tools.CheckInt(Request.Form["Product_Cate"]);
    //    int Product_BrandID = tools.CheckInt(Request.Form["Product_BrandID"]);
    //    int Product_TypeID = tools.CheckInt(Request.Form["Product_TypeID"]);
    //    string Product_Name = tools.CheckStr(Request.Form["Product_Name"]);
    //    string Product_NameInitials = pub.GetFirstLetter(Product_Name);
    //    string Product_SubName = tools.CheckStr(Request.Form["Product_SubName"]);
    //    string Product_SubNameInitials = pub.GetFirstLetter(Product_SubName);
    //    double Product_MKTPrice = tools.CheckFloat(Request.Form["Product_MKTPrice"]);
    //    double Product_GroupPrice = tools.CheckFloat(Request.Form["Product_GroupPrice"]);
    //    double Product_Price = tools.CheckFloat(Request.Form["Product_Price"]);
    //    string Product_PriceUnit = tools.CheckStr(Request.Form["Product_PriceUnit"]);
    //    string Product_Unit = tools.CheckStr(Request.Form["Product_Unit"]);
    //    int Product_GroupNum = tools.CheckInt(Request.Form["Product_GroupNum"]);
    //    string Product_Note = tools.CheckStr(Request.Form["Product_Note"]);
    //    string Product_NoteColor = tools.CheckStr(Request.Form["Product_NoteColor"]);
    //    double Product_Weight = tools.CheckFloat(Request.Form["Product_Weight"]);
    //    string Product_Img = tools.CheckStr(Request.Form["Product_Img"]);
    //    string U_Product_DeliveryCycle = tools.CheckStr(Request.Form["U_Product_DeliveryCycle"]);
    //    //int Product_StockAmount = tools.CheckInt(Request.Form["Product_StockAmount"]);
    //    int Product_IsInsale = tools.CheckInt(Request.Form["Product_IsInsale"]);
    //    int Product_IsGroupBuy = tools.CheckInt(Request.Form["Product_IsGroupBuy"]);
    //    int Product_IsCoinBuy = tools.CheckInt(Request.Form["Product_IsCoinBuy"]);
    //    int Product_IsFavor = tools.CheckInt(Request.Form["Product_IsFavor"]);
    //    int Product_IsGift = tools.CheckInt(Request.Form["Product_IsGift"]);
    //    int Product_IsAudit = tools.CheckInt(Request.Form["Product_IsAudit"]);
    //    int Product_IsGiftCoin = tools.CheckInt(Request.Form["Product_IsGiftCoin"]);
    //    double Product_Gift_Coin = tools.CheckFloat(Request.Form["Product_Gift_Coin"]);
    //    int Product_CoinBuy_Coin = tools.CheckInt(Request.Form["Product_CoinBuy_Coin"]);
    //    string Product_Intro = tools.CheckHTML(Request.Form["Product_Intro"]);
    //    int Product_AlertAmount = tools.CheckInt(Request.Form["Product_AlertAmount"]);
    //    int Product_IsNoStock = tools.CheckInt(Request.Form["Product_IsNoStock"]);
    //    string Product_Spec = tools.CheckStr(Request.Form["Product_Spec"]);
    //    string Product_Maker = tools.CheckStr(Request.Form["Product_Maker"]);
    //    int Product_Sort = tools.CheckInt(Request.Form["Product_Sort"]);
    //    int Product_QuotaAmount = tools.CheckInt(Request.Form["Product_QuotaAmount"]);
    //    int U_Product_SalesByProxy = tools.CheckInt(Request.Form["U_Product_SalesByProxy"]);
    //    int U_Product_Shipper = tools.CheckInt(Request.Form["U_Product_Shipper"]);
    //    string U_Product_Parameters = tools.CheckHTML(tools.NullStr(Request.Form["U_Product_Parameters"]));
    //    int Product_SupplierID = tools.NullInt(Session["supplier_id"]);
    //    Product_IsFavor = 1;
    //    Product_IsAudit = 1;
    //    string product_img = tools.CheckStr(Request.Form["product_img"]);
    //    string product_img_ext_1 = tools.CheckStr(Request.Form["product_img_ext_1"]);
    //    string product_img_ext_2 = tools.CheckStr(Request.Form["product_img_ext_2"]);
    //    string product_img_ext_3 = tools.CheckStr(Request.Form["product_img_ext_3"]);
    //    string product_img_ext_4 = tools.CheckStr(Request.Form["product_img_ext_4"]);

    //    string Product_Keyword1 = tools.CheckStr(Request.Form["Product_Keyword1"]);
    //    string Product_Keyword2 = tools.CheckStr(Request.Form["Product_Keyword2"]);
    //    string Product_Keyword3 = tools.CheckStr(Request.Form["Product_Keyword3"]);
    //    string Product_Keyword4 = tools.CheckStr(Request.Form["Product_Keyword4"]);
    //    string Product_Keyword5 = tools.CheckStr(Request.Form["Product_Keyword5"]);
    //    string Product_Keyword = Product_Keyword1 + "|" + Product_Keyword2 + "|" + Product_Keyword3 + "|" + Product_Keyword4 + "|" + Product_Keyword5;


    //    double Product_Grade_Price = 0;

    //    string ProductTag_ID = tools.CheckStr(Request.Form["ProductTag_ID"]);

    //    string ImgPath = product_img + "|" + product_img_ext_1 + "|" + product_img_ext_2 + "|" + product_img_ext_3 + "|" + product_img_ext_4;
    //    string[] ProductImages = ImgPath.Split('|');

    //    string[] cateArray = Product_CateID.Split(',');
    //    string[] tagArray = ProductTag_ID.Split(',');


    //    int U_Product_MinBook = tools.CheckInt(Request.Form["U_Product_MinBook"]);

    //    int Product_PriceType = tools.CheckInt(Request["Product_PriceType"]);
    //    double Product_ManualFee = tools.CheckFloat(Request["Product_ManualFee"]);
    //    string Product_LibraryImg = tools.CheckStr(Request["Product_LibraryImg"]);

    //    string Product_Extends = tools.CheckStr(Request["Product_Extends"]);
    //    string[] extendArr = Product_Extends.Split(',');

    //    //if (!CheckProductCode(Product_Code, 0))
    //    //{
    //    //    pub.Msg("error", "错误信息", "已存在该商品编码，请尝试更换其他编码！", false, "{back}");
    //    //    return;
    //    //}

    //    if (Product_Name.Length == 0)
    //    {
    //        Response.Write("请填写产品名称");
    //        Response.End();
    //    }

    //    if (Product_Supplier_CommissionCateID == 0 && GetCommissionCateAmount() > 0)
    //    {
    //        Response.Write("请选择佣金分类");
    //        Response.End();
    //    }

    //    if (Product_BrandID <= 0)
    //    {
    //        Response.Write("请选择产品品牌");
    //        Response.End();
    //    }

    //    if (Product_PriceType == 1)
    //    {
    //        if (Product_Price <= 0)
    //        {
    //            Response.Write("请填写商品价格");
    //            Response.End();
    //        }
    //    }
    //    else
    //    {
    //        if (Product_ManualFee <= 0)
    //        {
    //            Response.Write("请填写手工费");
    //            Response.End();
    //        }
    //    }


    //    if (U_Product_MinBook < 1)
    //    {
    //        Response.Write("最小起订量必须大于0");
    //        Response.End();
    //    }

    //    int min_wholeprice = 0;
    //    int max_wholeprice = 0;
    //    double product_wholeprice_amount = 0.00;
    //    int check_num = 0;
    //    for (int i = 1; i <= 10; i++)
    //    {
    //        min_wholeprice = tools.CheckInt(Request["min_product_wholeprice_amount_" + i]);
    //        max_wholeprice = tools.CheckInt(Request["max_product_wholeprice_amount_" + i]);
    //        if (max_wholeprice == 0)
    //        {
    //            if (i == 1)
    //            { }
    //            else
    //            {
    //                if (min_wholeprice == 0)
    //                { }
    //                else
    //                {
    //                    if (min_wholeprice != check_num + 1)
    //                    {
    //                        Response.Write("批发价格设置不符合规范");
    //                        Response.End();
    //                    }
    //                }
    //            }
    //            break;
    //        }
    //        else
    //        {
    //            if (i == 1)
    //            {

    //            }
    //            else
    //            {
    //                if (min_wholeprice != check_num + 1)
    //                {
    //                    Response.Write("批发价格设置不符合规范");
    //                    Response.End();
    //                }

    //                if (min_wholeprice > max_wholeprice)
    //                {
    //                    Response.Write("批发价格设置不符合规范");
    //                    Response.End();
    //                }
    //            }
    //        }
    //        check_num = max_wholeprice;
    //    }

    //    string Group_Code = tools.NullStr(Guid.NewGuid());
    //    int j = 0;


    //    ProductExtendInfo extendInfo = null;

    //    if (extendArr != null)
    //    {
    //        foreach (string extend in extendArr)
    //        {
    //            if (extend != "" && extend != "0")
    //            {
    //                j++;
    //                string Product_Code = tools.CheckStr(Request.Form["Product_Code" + extend]);
    //                int Product_StockAmount = tools.CheckInt(Request.Form["Product_StockAmount" + extend]);

    //                if (Product_Code == "")
    //                {
    //                    Response.Write("请输入商品编号");
    //                    Response.End();
    //                }

    //                if (Product_StockAmount == 0)
    //                {
    //                    Response.Write("请输入商品库存");
    //                    Response.End();
    //                }

    //                ProductInfo entity = new ProductInfo();
    //                entity.Product_ID = Product_ID;
    //                entity.Product_Code = Product_Code;
    //                entity.Product_CateID = Product_Cate;
    //                entity.Product_BrandID = Product_BrandID;
    //                entity.Product_TypeID = Product_TypeID;
    //                entity.Product_SupplierID = Product_SupplierID;
    //                entity.Product_Supplier_CommissionCateID = Product_Supplier_CommissionCateID;
    //                entity.Product_Name = Product_Name;
    //                entity.Product_NameInitials = Product_NameInitials;
    //                entity.Product_SubName = Product_SubName;
    //                entity.Product_SubNameInitials = Product_SubNameInitials;
    //                entity.Product_MKTPrice = Product_MKTPrice;
    //                entity.Product_GroupPrice = Product_GroupPrice;
    //                entity.Product_PurchasingPrice = 0;
    //                entity.Product_Price = Product_Price;
    //                entity.Product_PriceUnit = Product_PriceUnit;
    //                entity.Product_Unit = Product_Unit;
    //                entity.Product_GroupNum = Product_GroupNum;
    //                entity.Product_Note = Product_Note;
    //                entity.Product_NoteColor = Product_NoteColor;
    //                entity.Product_Audit_Note = "";
    //                entity.Product_Weight = Product_Weight;
    //                entity.Product_Img = Product_Img;
    //                entity.Product_Publisher = "";
    //                entity.Product_StockAmount = Product_StockAmount;
    //                entity.Product_SaleAmount = 0;
    //                entity.Product_Review_Count = 0;
    //                entity.Product_Review_ValidCount = 0;
    //                entity.Product_Review_Average = 0;
    //                entity.Product_IsInsale = Product_IsInsale;
    //                entity.Product_IsGroupBuy = Product_IsGroupBuy;
    //                entity.Product_IsCoinBuy = Product_IsCoinBuy;
    //                entity.Product_IsFavor = Product_IsFavor;
    //                entity.Product_IsGift = Product_IsGift;
    //                entity.Product_IsAudit = Product_IsAudit;
    //                entity.Product_IsGiftCoin = Product_IsGiftCoin;
    //                entity.Product_Gift_Coin = Product_Gift_Coin;
    //                entity.Product_CoinBuy_Coin = Product_CoinBuy_Coin;
    //                entity.Product_Addtime = DateTime.Now;
    //                entity.Product_Intro = Product_Intro;
    //                entity.Product_AlertAmount = Product_AlertAmount;
    //                entity.Product_UsableAmount = Product_StockAmount;
    //                entity.Product_IsNoStock = 0;
    //                entity.Product_Spec = Product_Spec;
    //                entity.Product_Maker = Product_Maker;
    //                entity.Product_Sort = Product_Sort;
    //                entity.Product_Hits = 0;
    //                entity.Product_QuotaAmount = Product_QuotaAmount;
    //                entity.Product_Site = pub.GetCurrentSite();
    //                entity.U_Product_SalesByProxy = U_Product_SalesByProxy;
    //                entity.U_Product_Shipper = U_Product_Shipper;
    //                if (j == 1)
    //                {
    //                    entity.Product_IsListShow = 1;
    //                }
    //                else
    //                {
    //                    entity.Product_IsListShow = 0;
    //                }
    //                entity.Product_GroupCode = Group_Code;
    //                entity.U_Product_MinBook = U_Product_MinBook;
    //                entity.U_Product_DeliveryCycle = U_Product_DeliveryCycle;
    //                entity.U_Product_Parameters = U_Product_Parameters;
    //                entity.Product_PriceType = Product_PriceType;
    //                entity.Product_ManualFee = Product_ManualFee;
    //                entity.Product_LibraryImg = Product_LibraryImg;


    //                IList<ProductExtendInfo> extends = ReadProductExtend(0);
    //                extendInfo = new ProductExtendInfo();
    //                extendInfo.Product_ID = Product_ID;
    //                extendInfo.Extent_ID = tools.CheckInt(Request["chk_display_extend"]);
    //                extendInfo.Extend_Value = tools.CheckStr(extend);
    //                extends.Add(extendInfo);


    //                if (MyProduct.AddProduct(entity, cateArray, tagArray, ProductImages, extends, pub.CreateUserPrivilege("a8dcfdfb-2227-40b3-a598-9643fd4c7e18")))
    //                {
    //                    ProductWholeSalePriceInfo saleinfo = new ProductWholeSalePriceInfo(); ;
    //                    for (int i = 1; i <= 10; i++)
    //                    {
    //                        min_wholeprice = tools.CheckInt(Request["min_product_wholeprice_amount_" + i]);
    //                        max_wholeprice = tools.CheckInt(Request["max_product_wholeprice_amount_" + i]);
    //                        product_wholeprice_amount = tools.CheckFloat(Request["product_wholeprice_amount_" + i]);
    //                        if (max_wholeprice != 0)
    //                        {
    //                            saleinfo.Product_WholeSalePrice_MinAmount = min_wholeprice;
    //                            saleinfo.Product_WholeSalePrice_MaxAmount = max_wholeprice;
    //                            saleinfo.Product_WholeSalePrice_Price = product_wholeprice_amount;
    //                            saleinfo.Product_WholeSalePrice_ProductID = entity.Product_ID;
    //                            saleinfo.Product_WholeSalePrice_Site = pub.GetCurrentSite();
    //                            MySalePrice.AddProductWholeSalePrice(saleinfo);
    //                        }
    //                        else
    //                        {
    //                            if (min_wholeprice > 0 && product_wholeprice_amount > 0)
    //                            {
    //                                saleinfo.Product_WholeSalePrice_MinAmount = min_wholeprice;
    //                                saleinfo.Product_WholeSalePrice_MaxAmount = max_wholeprice;
    //                                saleinfo.Product_WholeSalePrice_Price = product_wholeprice_amount;
    //                                saleinfo.Product_WholeSalePrice_ProductID = entity.Product_ID;
    //                                saleinfo.Product_WholeSalePrice_Site = pub.GetCurrentSite();
    //                                MySalePrice.AddProductWholeSalePrice(saleinfo);
    //                            }
    //                            break;
    //                        }
    //                    }

    //                    pro_id = entity.Product_ID;
    //                    ProductInfo productinfo = new ProductInfo();
    //                    productinfo = MyProduct.GetProductByCode(Product_Code, pub.GetCurrentSite(), pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
    //                    if (productinfo != null)
    //                    {
    //                        MyProduct.SaveProductTag(productinfo.Product_ID, tagArray);
    //                        SupplierProductCategoryInfo productgroup;
    //                        foreach (string subcate in Product_GroupID.Split(','))
    //                        {
    //                            if (tools.CheckInt(subcate) > 0)
    //                            {
    //                                productgroup = new SupplierProductCategoryInfo();
    //                                productgroup.Supplier_Product_Cate_CateID = tools.CheckInt(subcate);
    //                                productgroup.Supplier_Product_Cate_ProductID = productinfo.Product_ID;
    //                                MyProductCate.AddSupplierProductCategory(productgroup);
    //                                productgroup = null;
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                if (extend == "")
    //                {
    //                    Response.Write("操作失败，请维护扩展属性......");
    //                    Response.End();
    //                }

    //            }
    //        }

    //        Response.Write("success");
    //        Response.End();
    //    }
    //    else
    //    {
    //        Response.Write("操作失败，请稍后再试......");
    //        Response.End();
    //    }
    //}

    private IList<ProductExtendInfo> ReadProductExtend(int Product_ID)
    {
        IList<ProductExtendInfo> extends = new List<ProductExtendInfo>();
        ProductExtendInfo extend = null;
        foreach (string frmelement in Request.Form)
        {
            if (frmelement.Length > 14 && frmelement.Substring(0, 14) == "product_extend")
            {
                extend = new ProductExtendInfo();
                extend.Product_ID = Product_ID;
                extend.Extent_ID = tools.CheckInt(frmelement.Substring(14));
                extend.Extend_Value = tools.CheckStr(Request.Form[frmelement]);
                extend.Extend_Img = tools.CheckStr(Request.Form["PExtend" + extend.Extent_ID + "_img"]);
                extends.Add(extend);
                extend = null;
            }
            else
            {
                if (frmelement.Length > 8 && frmelement.Substring(0, 8) == "p_extend")
                {
                    extend = new ProductExtendInfo();
                    extend.Product_ID = Product_ID;
                    extend.Extent_ID = tools.CheckInt(frmelement.Substring(8));
                    extend.Extend_Value = Request.Form[frmelement];
                    extends.Add(extend);
                    extend = null;
                }
            }
        }

        return extends;
    }

    public void EditProduct_bak()
    {
        int Product_ID = tools.CheckInt(Request.Form["Product_ID"]);
        int Product_Supplier_CommissionCateID = tools.CheckInt(Request.Form["Product_Supplier_CommissionCateID"]);
        string Product_Code = tools.CheckStr(Request.Form["Product_Code"]);
        string Product_CateID = tools.CheckStr(Request.Form["Product_CateID"]);
        int Product_Cate = tools.CheckInt(Request.Form["Product_Cate"]);
        string Product_GroupID = tools.CheckStr(Request.Form["Product_GroupID"]);
        int Product_BrandID = tools.CheckInt(Request.Form["Product_BrandID"]);
        int Product_TypeID = tools.CheckInt(Request.Form["Product_TypeID"]);
        string Product_Name = tools.CheckStr(Request.Form["Product_Name"]);
        string Product_NameInitials = pub.GetFirstLetter(Product_Name);
        string Product_SubName = tools.CheckStr(Request.Form["Product_SubName"]);
        string Product_SubNameInitials = pub.GetFirstLetter(Product_SubName);
        double Product_MKTPrice = tools.CheckFloat(Request.Form["Product_MKTPrice"]);
        double Product_GroupPrice = tools.CheckFloat(Request.Form["Product_GroupPrice"]);
        double Product_Price = tools.CheckFloat(Request.Form["Product_Price"]);
        string Product_PriceUnit = tools.CheckStr(Request.Form["Product_PriceUnit"]);
        string Product_Unit = tools.CheckStr(Request.Form["Product_Unit"]);
        int Product_GroupNum = tools.CheckInt(Request.Form["Product_GroupNum"]);
        string Product_Note = tools.CheckStr(Request.Form["Product_Note"]);
        string Product_NoteColor = tools.CheckStr(Request.Form["Product_NoteColor"]);
        double Product_Weight = tools.CheckFloat(Request.Form["Product_Weight"]);
        string Product_Img = tools.CheckStr(Request.Form["Product_Img"]);
        int Product_StockAmount = tools.CheckInt(Request.Form["Product_StockAmount"]);
        int Product_IsInsale = tools.CheckInt(Request.Form["Product_IsInsale"]);
        int Product_IsGroupBuy = tools.CheckInt(Request.Form["Product_IsGroupBuy"]);
        int Product_IsCoinBuy = tools.CheckInt(Request.Form["Product_IsCoinBuy"]);
        int Product_IsFavor = tools.CheckInt(Request.Form["Product_IsFavor"]);
        int Product_IsGift = tools.CheckInt(Request.Form["Product_IsGift"]);
        int Product_IsAudit = tools.CheckInt(Request.Form["Product_IsAudit"]);
        int Product_IsGiftCoin = tools.CheckInt(Request.Form["Product_IsGiftCoin"]);
        double Product_Gift_Coin = tools.CheckFloat(Request.Form["Product_Gift_Coin"]);
        int Product_CoinBuy_Coin = tools.CheckInt(Request.Form["Product_CoinBuy_Coin"]);
        string Product_Intro = tools.CheckHTML(Request.Form["Product_Intro"]);
        int Product_AlertAmount = tools.CheckInt(Request.Form["Product_AlertAmount"]);
        int Product_IsNoStock = tools.CheckInt(Request.Form["Product_IsNoStock"]);
        string Product_Spec = tools.CheckStr(Request.Form["Product_Spec"]);
        string Product_Maker = tools.CheckStr(Request.Form["Product_Maker"]);
        int Product_Sort = tools.CheckInt(Request.Form["Product_Sort"]);
        int Product_QuotaAmount = tools.CheckInt(Request.Form["Product_QuotaAmount"]);
        int Product_SupplierID = tools.NullInt(Session["supplier_id"]);
        int U_Product_SalesByProxy = tools.CheckInt(Request.Form["U_Product_SalesByProxy"]);
        int U_Product_Shipper = tools.CheckInt(Request.Form["U_Product_Shipper"]);
        string Product_Keyword1 = tools.CheckStr(Request.Form["Product_Keyword1"]);
        string Product_Keyword2 = tools.CheckStr(Request.Form["Product_Keyword2"]);
        string Product_Keyword3 = tools.CheckStr(Request.Form["Product_Keyword3"]);
        string Product_Keyword4 = tools.CheckStr(Request.Form["Product_Keyword4"]);
        string Product_Keyword5 = tools.CheckStr(Request.Form["Product_Keyword5"]);
        string Product_Keyword = Product_Keyword1 + "|" + Product_Keyword2 + "|" + Product_Keyword3 + "|" + Product_Keyword4 + "|" + Product_Keyword5;


        double Product_Grade_Price = 0;

        string product_img = tools.CheckStr(Request.Form["product_img"]);
        string product_img_ext_1 = tools.CheckStr(Request.Form["product_img_ext_1"]);
        string product_img_ext_2 = tools.CheckStr(Request.Form["product_img_ext_2"]);
        string product_img_ext_3 = tools.CheckStr(Request.Form["product_img_ext_3"]);
        string product_img_ext_4 = tools.CheckStr(Request.Form["product_img_ext_4"]);

        string ProductTag_ID = tools.CheckStr(Request.Form["ProductTag_ID"]);
        Product_IsFavor = 1;
        Product_IsAudit = 1;
        string ImgPath = product_img + "|" + product_img_ext_1 + "|" + product_img_ext_2 + "|" + product_img_ext_3 + "|" + product_img_ext_4;
        string[] ProductImages = ImgPath.Split('|');

        string[] cateArray = Product_CateID.Split(',');
        string[] tagArray = ProductTag_ID.Split(',');


        string U_Product_DeliveryCycle = tools.CheckStr(Request.Form["U_Product_DeliveryCycle"]);
        int U_Product_MinBook = tools.CheckInt(Request.Form["U_Product_MinBook"]);
        string U_Product_Parameters = tools.CheckHTML(Request.Form["U_Product_Parameters"]);
        int Product_PriceType = tools.CheckInt(Request["Product_PriceType"]);
        double Product_ManualFee = tools.CheckFloat(Request["Product_ManualFee"]);
        string Product_LibraryImg = tools.CheckStr(Request["Product_LibraryImg"]);

        //if (!CheckProductCode(Product_Code, Product_ID))
        //{
        //    pub.Msg("error", "错误信息", "已存在该商品编码，请尝试更换其他编码！", false, "{back}");
        //    return;
        //}

        if (Product_Name.Length == 0)
        {
            pub.Msg("error", "错误信息", "请填写产品名称", false, "{back}");
            return;
        }

        if (Product_Supplier_CommissionCateID == 0 && GetCommissionCateAmount() > 0)
        {
            pub.Msg("error", "错误信息", "请选择佣金分类", false, "{back}");
            return;
        }
        //if (Product_BrandID <= 0)
        //{
        //    pub.Msg("error", "错误信息", "请选择产品品牌", false, "{back}");
        //    return;
        //}

        if (Product_PriceType == 1)
        {
            if (Product_Price <= 0)
            {
                pub.Msg("error", "错误信息", "请填写商品价格", false, "{back}");
                return;
            }
        }
        else
        {
            //    if (Product_ManualFee <= 0)
            //    {
            //        pub.Msg("error", "错误信息", "请填写手工费", false, "{back}");
            //        return;
            //    }
        }

        if (Product_StockAmount <= 0)
        {
            pub.Msg("error", "错误信息", "请填写产品库存", false, "{back}");
            return;
        }

        ProductInfo entity = GetProductByID(Product_ID);
        if (entity == null)
        {
            entity = new ProductInfo();
            pub.Msg("error", "错误信息", "记录不存在！", false, "{back}");
            return;
        }
        if (entity.Product_SupplierID != Product_SupplierID)
        {
            pub.Msg("error", "错误信息", "记录不存在！", false, "{back}");
            return;
        }

        if (U_Product_MinBook < 1)
        {
            pub.Msg("error", "错误信息", "最小起订量必须大于0", false, "{back}");
            return;
        }

        int min_wholeprice = 0;
        int max_wholeprice = 0;
        double product_wholeprice_amount = 0.00;
        int check_num = 0;
        for (int i = 1; i <= 10; i++)
        {
            min_wholeprice = tools.CheckInt(Request["min_product_wholeprice_amount_" + i]);
            max_wholeprice = tools.CheckInt(Request["max_product_wholeprice_amount_" + i]);
            if (max_wholeprice == 0)
            {
                if (i == 1)
                { }
                else
                {
                    if (min_wholeprice == 0)
                    { }
                    else
                    {
                        if (min_wholeprice != check_num + 1)
                        {
                            pub.Msg("error", "错误信息", "批发价格设置不符合规范", false, "{back}");
                            return;
                        }
                    }
                }
                break;
            }
            else
            {
                if (i == 1)
                {

                }
                else
                {
                    if (min_wholeprice != check_num + 1)
                    {
                        pub.Msg("error", "错误信息", "批发价格设置不符合规范", false, "{back}");
                        return;
                    }

                    if (min_wholeprice > max_wholeprice)
                    {
                        pub.Msg("error", "错误信息", "批发价格设置不符合规范", false, "{back}");
                        return;
                    }
                }
            }
            check_num = max_wholeprice;
        }

        entity.Product_ID = Product_ID;
        entity.Product_Code = Product_Code;
        entity.Product_CateID = Product_Cate;
        entity.Product_BrandID = Product_BrandID;
        entity.Product_TypeID = Product_TypeID;
        entity.Product_SupplierID = Product_SupplierID;
        entity.Product_Supplier_CommissionCateID = Product_Supplier_CommissionCateID;
        entity.Product_PurchasingPrice = entity.Product_PurchasingPrice;
        entity.Product_Name = Product_Name;
        entity.Product_NameInitials = Product_NameInitials;
        entity.Product_SubName = Product_SubName;
        entity.Product_SubNameInitials = Product_SubNameInitials;
        entity.Product_MKTPrice = Product_MKTPrice;
        entity.Product_GroupPrice = Product_GroupPrice;
        entity.Product_Price = Product_Price;
        entity.Product_PriceUnit = Product_PriceUnit;
        entity.Product_Unit = Product_Unit;
        entity.Product_GroupNum = Product_GroupNum;
        entity.Product_Note = Product_Note;
        entity.Product_NoteColor = Product_NoteColor;
        entity.Product_Audit_Note = "";
        entity.Product_Weight = Product_Weight;
        entity.Product_Img = Product_Img;
        entity.Product_Publisher = "";
        entity.Product_IsInsale = Product_IsInsale;
        entity.Product_IsGroupBuy = Product_IsGroupBuy;
        entity.Product_IsCoinBuy = Product_IsCoinBuy;
        entity.Product_IsFavor = Product_IsFavor;
        entity.Product_IsGift = Product_IsGift;
        entity.Product_IsAudit = Product_IsAudit;
        entity.Product_IsGiftCoin = Product_IsGiftCoin;
        entity.Product_Gift_Coin = Product_Gift_Coin;
        entity.Product_CoinBuy_Coin = Product_CoinBuy_Coin;
        entity.Product_Intro = Product_Intro;
        //entity.Product_AlertAmount = Product_AlertAmount;
        entity.Product_IsNoStock = 0;
        entity.Product_Spec = Product_Spec;
        entity.Product_Maker = Product_Maker;
        entity.Product_Sort = Product_Sort;
        entity.Product_StockAmount = Product_StockAmount;
        entity.Product_UsableAmount = Product_StockAmount;
        entity.Product_QuotaAmount = Product_QuotaAmount;
        entity.Product_Site = pub.GetCurrentSite();
        entity.U_Product_SalesByProxy = U_Product_SalesByProxy;
        entity.U_Product_Shipper = U_Product_Shipper;
        IList<ProductExtendInfo> extends = ReadProductExtend(Product_ID);
        entity.U_Product_Parameters = U_Product_Parameters;
        entity.U_Product_MinBook = U_Product_MinBook;
        entity.U_Product_DeliveryCycle = U_Product_DeliveryCycle;
        entity.Product_PriceType = Product_PriceType;
        entity.Product_ManualFee = Product_ManualFee;
        entity.Product_LibraryImg = Product_LibraryImg;


        //记录主产品价格调价
        IList<ProductPriceInfo> prices = null;

        if (MyProduct.EditProduct(entity, cateArray, tagArray, ProductImages, extends, pub.CreateUserPrivilege("854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d")))
        {
            #region 批发价格设置
            MySalePrice.DelProductWholeSalePriceByProductID(Product_ID);
            ProductWholeSalePriceInfo saleinfo = new ProductWholeSalePriceInfo(); ;

            for (int i = 1; i <= 10; i++)
            {
                min_wholeprice = tools.CheckInt(Request["min_product_wholeprice_amount_" + i]);
                max_wholeprice = tools.CheckInt(Request["max_product_wholeprice_amount_" + i]);
                product_wholeprice_amount = tools.CheckFloat(Request["product_wholeprice_amount_" + i]);
                if (max_wholeprice != 0)
                {
                    saleinfo.Product_WholeSalePrice_MinAmount = min_wholeprice;
                    saleinfo.Product_WholeSalePrice_MaxAmount = max_wholeprice;
                    saleinfo.Product_WholeSalePrice_Price = product_wholeprice_amount;
                    saleinfo.Product_WholeSalePrice_ProductID = entity.Product_ID;
                    saleinfo.Product_WholeSalePrice_Site = pub.GetCurrentSite();
                    MySalePrice.AddProductWholeSalePrice(saleinfo);
                }
                else
                {
                    if (min_wholeprice > 0 && product_wholeprice_amount > 0)
                    {
                        saleinfo.Product_WholeSalePrice_MinAmount = min_wholeprice;
                        saleinfo.Product_WholeSalePrice_MaxAmount = max_wholeprice;
                        saleinfo.Product_WholeSalePrice_Price = product_wholeprice_amount;
                        saleinfo.Product_WholeSalePrice_ProductID = entity.Product_ID;
                        saleinfo.Product_WholeSalePrice_Site = pub.GetCurrentSite();
                        MySalePrice.AddProductWholeSalePrice(saleinfo);
                    }
                    break;
                }
            }
            #endregion


            MyProduct.SaveProductTag(entity.Product_ID, tagArray);

            MyProductCate.DelSupplierProductCategoryByProductID(Product_ID);
            SupplierProductCategoryInfo productgroup;
            foreach (string subcate in Product_GroupID.Split(','))
            {
                if (tools.CheckInt(subcate) > 0)
                {
                    productgroup = new SupplierProductCategoryInfo();
                    productgroup.Supplier_Product_Cate_CateID = tools.CheckInt(subcate);
                    productgroup.Supplier_Product_Cate_ProductID = Product_ID;
                    MyProductCate.AddSupplierProductCategory(productgroup);
                    productgroup = null;
                }
            }

            //记录各会员等级价格调价
            IList<MemberGradeInfo> grades = null;
            QueryInfo Query = new QueryInfo();

            Query = null;
            MyPrice.DelProductPrice(Product_ID);
            QueryInfo Query1 = new QueryInfo();
            Query1.PageSize = 0;
            Query1.CurrentPage = 1;
            Query1.ParamInfos.Add(new ParamInfo("AND", "str", "MemberGradeInfo.Member_Grade_Site", "=", pub.GetCurrentSite()));
            Query1.OrderInfos.Add(new OrderInfo("MemberGradeInfo.Member_Grade_ID", "asc"));
            grades = MyGrade.GetMemberGrades(Query1, pub.CreateUserPrivilege("1c955ea6-881f-48d8-ba8d-c5aa7ce9cfea"));
            if (grades != null)
            {
                foreach (MemberGradeInfo grade in grades)
                {
                    Product_Grade_Price = tools.CheckFloat(Request.Form["Product_Grade_Price_" + grade.Member_Grade_ID]);
                    if (Product_Grade_Price > 0)
                    {
                        ProductPriceInfo priceinfo = new ProductPriceInfo();
                        priceinfo.Product_Price_ID = 0;
                        priceinfo.Product_Price_MemberGradeID = grade.Member_Grade_ID;
                        priceinfo.Product_Price_Price = Product_Grade_Price;
                        priceinfo.Product_Price_ProcutID = Product_ID;
                        MyPrice.AddProductPrice(priceinfo);
                    }
                }
            }

            #region 更新聚合信息

            string group_productid = "";
            if (entity.Product_GroupCode.Length > 0)
            {
                MyProduct.UpdateProductGroupCode("", entity.Product_GroupCode);
            }
            string Group_Code = "";

            if (group_productid.Length > 0)
            {
                foreach (string subproduct in group_productid.Split(','))
                {
                    if (tools.CheckInt(subproduct) > 0)
                    {
                        MyProduct.UpdateProductGroupInfo("", 1, tools.CheckInt(subproduct));
                    }
                }
            }

            group_productid = tools.NullStr(Request["sltproductid"]);
            if (group_productid.Length > 0)
            {
                Group_Code = tools.NullStr(Guid.NewGuid());
                foreach (string subproductid in group_productid.Split(','))
                {
                    if (tools.CheckInt(subproductid) > 0)
                    {
                        MyProduct.UpdateProductGroupInfo(Group_Code, tools.NullInt(Request["islistshow_" + tools.CheckInt(subproductid)]), tools.CheckInt(subproductid));
                    }
                }
                MyProduct.UpdateProductGroupInfo(Group_Code, tools.NullInt(Request["islistshow_" + Product_ID]), Product_ID);
            }
            else
            {
                MyProduct.UpdateProductGroupInfo("", 1, Product_ID);
            }

            #endregion

            pub.Msg("positive", "操作成功", "操作成功", true, "product_list.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void EditProduct()
    {
        int pro_id = 0;
        int Product_ID;
        int product_id = tools.CheckInt(Request["Product_ID"]);
        int Product_Supplier_CommissionCateID = tools.CheckInt(Request.Form["Product_Supplier_CommissionCateID"]);
        //string Product_Code = tools.CheckStr(Request.Form["Product_Code"]);
        string Product_CateID = tools.CheckStr(Request.Form["Product_CateID"]);
        int Product_Cate = tools.CheckInt(Request.Form["Product_Cate"]);
        string Product_GroupID = tools.CheckStr(Request.Form["Product_GroupID"]);
        int Product_BrandID = tools.CheckInt(Request.Form["Product_BrandID"]);
        int Product_TypeID = tools.CheckInt(Request.Form["Product_TypeID"]);
        string Product_Name = tools.CheckStr(Request.Form["Product_Name"]);
        string Product_NameInitials = pub.GetFirstLetter(Product_Name);
        string Product_SubName = tools.CheckStr(Request.Form["Product_SubName"]);
        string Product_SubNameInitials = pub.GetFirstLetter(Product_SubName);
        double Product_MKTPrice = tools.CheckFloat(Request.Form["Product_MKTPrice"]);
        double Product_GroupPrice = tools.CheckFloat(Request.Form["Product_GroupPrice"]);
        double Product_Price = tools.CheckFloat(Request.Form["Product_Price"]);
        string Product_PriceUnit = tools.CheckStr(Request.Form["Product_PriceUnit"]);
        string Product_Unit = tools.CheckStr(Request.Form["Product_Unit"]);
        int Product_GroupNum = tools.CheckInt(Request.Form["Product_GroupNum"]);
        string Product_Note = tools.CheckStr(Request.Form["Product_Note"]);
        string Product_NoteColor = tools.CheckStr(Request.Form["Product_NoteColor"]);
        double Product_Weight = tools.CheckFloat(Request.Form["Product_Weight"]);
        string Product_Img = tools.CheckStr(Request.Form["Product_Img"]);
        int Product_StockAmount = tools.CheckInt(Request.Form["Product_StockAmount"]);
        int Product_IsInsale = tools.CheckInt(Request.Form["Product_IsInsale"]);
        int Product_IsGroupBuy = tools.CheckInt(Request.Form["Product_IsGroupBuy"]);
        int Product_IsCoinBuy = tools.CheckInt(Request.Form["Product_IsCoinBuy"]);
        int Product_IsFavor = tools.CheckInt(Request.Form["Product_IsFavor"]);
        int Product_IsGift = tools.CheckInt(Request.Form["Product_IsGift"]);
        int Product_IsAudit = tools.CheckInt(Request.Form["Product_IsAudit"]);
        int Product_IsGiftCoin = tools.CheckInt(Request.Form["Product_IsGiftCoin"]);
        double Product_Gift_Coin = tools.CheckFloat(Request.Form["Product_Gift_Coin"]);
        int Product_CoinBuy_Coin = tools.CheckInt(Request.Form["Product_CoinBuy_Coin"]);
        string Product_Intro = tools.CheckHTML(Request.Form["Product_Intro"]);
        int Product_AlertAmount = tools.CheckInt(Request.Form["Product_AlertAmount"]);
        int Product_IsNoStock = tools.CheckInt(Request.Form["Product_IsNoStock"]);
        string Product_Spec = tools.CheckStr(Request.Form["Product_Spec"]);
        string Product_Maker = tools.CheckStr(Request.Form["Product_Maker"]);
        int Product_Sort = tools.CheckInt(Request.Form["Product_Sort"]);
        int Product_QuotaAmount = tools.CheckInt(Request.Form["Product_QuotaAmount"]);
        int Product_SupplierID = tools.NullInt(Session["supplier_id"]);
        int U_Product_SalesByProxy = tools.CheckInt(Request.Form["U_Product_SalesByProxy"]);
        int U_Product_Shipper = tools.CheckInt(Request.Form["U_Product_Shipper"]);
        string Product_Keyword1 = tools.CheckStr(Request.Form["Product_Keyword1"]);
        string Product_Keyword2 = tools.CheckStr(Request.Form["Product_Keyword2"]);
        string Product_Keyword3 = tools.CheckStr(Request.Form["Product_Keyword3"]);
        string Product_Keyword4 = tools.CheckStr(Request.Form["Product_Keyword4"]);
        string Product_Keyword5 = tools.CheckStr(Request.Form["Product_Keyword5"]);
        string Product_Keyword = Product_Keyword1 + "|" + Product_Keyword2 + "|" + Product_Keyword3 + "|" + Product_Keyword4 + "|" + Product_Keyword5;


        double Product_Grade_Price = 0;

        string product_img = tools.CheckStr(Request.Form["product_img"]);
        string product_img_ext_1 = tools.CheckStr(Request.Form["product_img_ext_1"]);
        string product_img_ext_2 = tools.CheckStr(Request.Form["product_img_ext_2"]);
        string product_img_ext_3 = tools.CheckStr(Request.Form["product_img_ext_3"]);
        string product_img_ext_4 = tools.CheckStr(Request.Form["product_img_ext_4"]);

        string ProductTag_ID = tools.CheckStr(Request.Form["ProductTag_ID"]);
        Product_IsFavor = 1;
        Product_IsAudit = 0;
        string ImgPath = product_img + "|" + product_img_ext_1 + "|" + product_img_ext_2 + "|" + product_img_ext_3 + "|" + product_img_ext_4;
        string[] ProductImages = ImgPath.Split('|');

        string[] cateArray = Product_CateID.Split(',');
        string[] tagArray = ProductTag_ID.Split(',');


        string U_Product_DeliveryCycle = tools.CheckStr(Request.Form["U_Product_DeliveryCycle"]);
        int U_Product_MinBook = tools.CheckInt(Request.Form["U_Product_MinBook"]);
        string U_Product_Parameters = tools.CheckHTML(tools.NullStr(Request.Form["U_Product_Parameters"]));
        int Product_PriceType = tools.CheckInt(Request["Product_PriceType"]);
        double Product_ManualFee = tools.CheckFloat(Request["Product_ManualFee"]);
        string Product_LibraryImg = tools.CheckStr(Request["Product_LibraryImg"]);

        string Product_State_Name = tools.CheckStr(Request.Form["Product_State_Name"]);
        string Product_City_Name = tools.CheckStr(Request.Form["Product_City_Name"]);
        string Product_County_Name = tools.CheckStr(Request.Form["Product_County_Name"]);


        string Product_Extends = tools.CheckStr(Request["Product_Extends"]);
        string[] extendArr = Product_Extends.Split(',');

        if (Product_Name.Length == 0)
        {
            Response.Write("请填写产品名称");
            Response.End();
        }

        if (Product_Supplier_CommissionCateID == 0 && GetCommissionCateAmount() > 0)
        {
            Response.Write("请选择佣金分类");
            Response.End();
        }

        //if (Product_BrandID <= 0)
        //{
        //    Response.Write("请选择产品品牌");
        //    Response.End();
        //}

        if (Product_PriceType == 0)
        {
            if (Product_Price <= 0)
            {
                Response.Write("请填写商品价格");
                Response.End();
            }
        }
        else
        {
            //if (Product_ManualFee <= 0)
            //{
            //    Response.Write("请填写手工费");
            //    Response.End();
            //}
        }



        //if (entity == null)
        //{
        //    entity = new ProductInfo();
        //    pub.Msg("error", "错误信息", "记录不存在！", false, "{back}");
        //    return;
        //}
        //if (entity.Product_SupplierID != Product_SupplierID)
        //{
        //    pub.Msg("error", "错误信息", "记录不存在！", false, "{back}");
        //    return;
        //}

        if (U_Product_MinBook < 1)
        {
            Response.Write("最小起订量必须大于0");
            Response.End();
        }

        int min_wholeprice = 0;
        int max_wholeprice = 0;
        double product_wholeprice_amount = 0.00;
        int check_num = 0;
        for (int i = 1; i <= 10; i++)
        {
            min_wholeprice = tools.CheckInt(Request["min_product_wholeprice_amount_" + i]);
            max_wholeprice = tools.CheckInt(Request["max_product_wholeprice_amount_" + i]);
            if (max_wholeprice == 0)
            {
                if (i == 1)
                { }
                else
                {
                    if (min_wholeprice == 0)
                    { }
                    else
                    {
                        if (min_wholeprice != check_num + 1)
                        {
                            Response.Write("批发价格设置不符合规范");
                            Response.End();
                        }
                    }
                }
                break;
            }
            else
            {
                if (i == 1)
                {

                }
                else
                {
                    if (min_wholeprice != check_num + 1)
                    {
                        Response.Write("批发价格设置不符合规范");
                        Response.End();
                    }

                    if (min_wholeprice > max_wholeprice)
                    {
                        Response.Write("批发价格设置不符合规范");
                        Response.End();
                    }
                }
            }
            check_num = max_wholeprice;
        }




        string Group_Code = tools.CheckStr(Request["Product_GroupCode"]);
        int j = 0;

        string[] product_ids = GetGroupProductID(Group_Code).Split(',');
        if (product_ids != null)
        {
            foreach (string items in product_ids)
            {
                MyProduct.DelProductExtendByID(tools.NullInt(items));
            }
        }

        ProductInfo entity = null;
        ProductExtendInfo extendInfo = null;

        if (extendArr != null)
        {
            foreach (string extend in extendArr)
            {
                if (extend != "" && extend != "0")
                {
                    j++;
                    string Product_Code = tools.CheckStr(Request.Form["Product_Code" + extend]);
                    //int Product_StockAmount = tools.CheckInt(Request.Form["Product_StockAmount" + extend]);
                    Product_ID = tools.CheckInt(Request["hidden_extend_product_" + extend]);

                    if (Product_Code == "")
                    {
                        Response.Write("请输入商品编码");
                        Response.End();
                    }
                    else
                    {
                        if (!CheckProductCode(Product_Code, Product_ID))
                        {
                            pub.Msg("error", "错误信息", "商品编码 " + Product_Code + " 已存在，请尝试更换其他编码！", false, "{back}");
                        }
                    }

                    if (Product_StockAmount == 0)
                    {
                        Response.Write("请输入商品库存");
                        Response.End();
                    }

                    entity = GetProductByID(Product_ID);

                    if (entity != null)
                    {
                        entity.Product_ID = Product_ID;
                        entity.Product_Code = Product_Code;
                        entity.Product_CateID = Product_Cate;
                        entity.Product_BrandID = Product_BrandID;
                        entity.Product_TypeID = Product_TypeID;
                        entity.Product_SupplierID = Product_SupplierID;
                        entity.Product_Supplier_CommissionCateID = Product_Supplier_CommissionCateID;
                        entity.Product_PurchasingPrice = entity.Product_PurchasingPrice;
                        entity.Product_Name = Product_Name;
                        entity.Product_NameInitials = Product_NameInitials;
                        entity.Product_SubName = Product_SubName;
                        entity.Product_SubNameInitials = Product_SubNameInitials;
                        entity.Product_MKTPrice = Product_MKTPrice;
                        entity.Product_GroupPrice = Product_GroupPrice;
                        entity.Product_Price = Product_Price;
                        entity.Product_PriceUnit = Product_PriceUnit;
                        entity.Product_Unit = Product_Unit;
                        entity.Product_GroupNum = Product_GroupNum;
                        entity.Product_Note = Product_Note;
                        entity.Product_NoteColor = Product_NoteColor;
                        entity.Product_Audit_Note = "";
                        entity.Product_Weight = Product_Weight;
                        entity.Product_Img = Product_Img;
                        entity.Product_Publisher = "";
                        entity.Product_IsInsale = Product_IsInsale;
                        entity.Product_IsGroupBuy = Product_IsGroupBuy;
                        entity.Product_IsCoinBuy = Product_IsCoinBuy;
                        entity.Product_IsFavor = Product_IsFavor;
                        entity.Product_IsGift = Product_IsGift;
                        entity.Product_IsAudit = Product_IsAudit;
                        entity.Product_IsGiftCoin = Product_IsGiftCoin;
                        entity.Product_Gift_Coin = Product_Gift_Coin;
                        entity.Product_CoinBuy_Coin = Product_CoinBuy_Coin;
                        entity.Product_Intro = Product_Intro;
                        entity.Product_IsNoStock = 0;
                        entity.Product_Spec = Product_Spec;
                        entity.Product_Maker = Product_Maker;
                        entity.Product_Sort = Product_Sort;
                        entity.Product_StockAmount = Product_StockAmount;
                        entity.Product_UsableAmount = Product_StockAmount;
                        entity.Product_QuotaAmount = Product_QuotaAmount;
                        entity.Product_Site = pub.GetCurrentSite();
                        entity.U_Product_SalesByProxy = U_Product_SalesByProxy;
                        entity.U_Product_Shipper = U_Product_Shipper;
                        entity.U_Product_Parameters = U_Product_Parameters;
                        entity.U_Product_MinBook = U_Product_MinBook;
                        entity.U_Product_DeliveryCycle = U_Product_DeliveryCycle;
                        entity.Product_PriceType = Product_PriceType;
                        entity.Product_ManualFee = Product_ManualFee;
                        entity.Product_LibraryImg = Product_LibraryImg;
                        entity.Product_State_Name = Product_State_Name;

                        entity.Product_City_Name = Product_City_Name;
                        entity.Product_County_Name = Product_County_Name;
                        if (product_id == Product_ID)
                        {
                            entity.Product_IsListShow = 1;
                        }
                        else
                        {
                            entity.Product_IsListShow = 0;
                        }

                        IList<ProductExtendInfo> extends = ReadProductExtend(Product_ID);
                        extendInfo = new ProductExtendInfo();
                        extendInfo.Product_ID = Product_ID;
                        extendInfo.Extent_ID = tools.CheckInt(Request["chk_display_extend"]);
                        extendInfo.Extend_Value = tools.CheckStr(extend);
                        extends.Add(extendInfo);

                        //记录主产品价格调价
                        IList<ProductPriceInfo> prices = null;

                        if (MyProduct.EditProduct(entity, cateArray, tagArray, ProductImages, extends, pub.CreateUserPrivilege("854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d")))
                        {
                            #region 批发价格设置
                            MySalePrice.DelProductWholeSalePriceByProductID(Product_ID);
                            ProductWholeSalePriceInfo saleinfo = new ProductWholeSalePriceInfo(); ;

                            for (int i = 1; i <= 10; i++)
                            {
                                min_wholeprice = tools.CheckInt(Request["min_product_wholeprice_amount_" + i]);
                                max_wholeprice = tools.CheckInt(Request["max_product_wholeprice_amount_" + i]);
                                product_wholeprice_amount = tools.CheckFloat(Request["product_wholeprice_amount_" + i]);
                                if (max_wholeprice != 0)
                                {
                                    saleinfo.Product_WholeSalePrice_MinAmount = min_wholeprice;
                                    saleinfo.Product_WholeSalePrice_MaxAmount = max_wholeprice;
                                    saleinfo.Product_WholeSalePrice_Price = product_wholeprice_amount;
                                    saleinfo.Product_WholeSalePrice_ProductID = entity.Product_ID;
                                    saleinfo.Product_WholeSalePrice_Site = pub.GetCurrentSite();
                                    MySalePrice.AddProductWholeSalePrice(saleinfo);
                                }
                                else
                                {
                                    if (min_wholeprice > 0 && product_wholeprice_amount > 0)
                                    {
                                        saleinfo.Product_WholeSalePrice_MinAmount = min_wholeprice;
                                        saleinfo.Product_WholeSalePrice_MaxAmount = max_wholeprice;
                                        saleinfo.Product_WholeSalePrice_Price = product_wholeprice_amount;
                                        saleinfo.Product_WholeSalePrice_ProductID = entity.Product_ID;
                                        saleinfo.Product_WholeSalePrice_Site = pub.GetCurrentSite();
                                        MySalePrice.AddProductWholeSalePrice(saleinfo);
                                    }
                                    break;
                                }
                            }
                            #endregion

                            MyProduct.SaveProductTag(entity.Product_ID, tagArray);

                            MyProductCate.DelSupplierProductCategoryByProductID(Product_ID);
                            SupplierProductCategoryInfo productgroup;
                            foreach (string subcate in Product_GroupID.Split(','))
                            {
                                if (tools.CheckInt(subcate) > 0)
                                {
                                    productgroup = new SupplierProductCategoryInfo();
                                    productgroup.Supplier_Product_Cate_CateID = tools.CheckInt(subcate);
                                    productgroup.Supplier_Product_Cate_ProductID = Product_ID;
                                    MyProductCate.AddSupplierProductCategory(productgroup);
                                    productgroup = null;
                                }
                            }

                            //记录各会员等级价格调价
                            IList<MemberGradeInfo> grades = null;
                            QueryInfo Query = new QueryInfo();

                            Query = null;
                            MyPrice.DelProductPrice(Product_ID);
                            QueryInfo Query1 = new QueryInfo();
                            Query1.PageSize = 0;
                            Query1.CurrentPage = 1;
                            Query1.ParamInfos.Add(new ParamInfo("AND", "str", "MemberGradeInfo.Member_Grade_Site", "=", pub.GetCurrentSite()));
                            Query1.OrderInfos.Add(new OrderInfo("MemberGradeInfo.Member_Grade_ID", "asc"));
                            grades = MyGrade.GetMemberGrades(Query1, pub.CreateUserPrivilege("1c955ea6-881f-48d8-ba8d-c5aa7ce9cfea"));
                            if (grades != null)
                            {
                                foreach (MemberGradeInfo grade in grades)
                                {
                                    Product_Grade_Price = tools.CheckFloat(Request.Form["Product_Grade_Price_" + grade.Member_Grade_ID]);
                                    if (Product_Grade_Price > 0)
                                    {
                                        ProductPriceInfo priceinfo = new ProductPriceInfo();
                                        priceinfo.Product_Price_ID = 0;
                                        priceinfo.Product_Price_MemberGradeID = grade.Member_Grade_ID;
                                        priceinfo.Product_Price_Price = Product_Grade_Price;
                                        priceinfo.Product_Price_ProcutID = Product_ID;
                                        MyPrice.AddProductPrice(priceinfo);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        entity = new ProductInfo();
                        entity.Product_ID = Product_ID;
                        entity.Product_Code = Product_Code;
                        entity.Product_CateID = Product_Cate;
                        entity.Product_BrandID = Product_BrandID;
                        entity.Product_TypeID = Product_TypeID;
                        entity.Product_SupplierID = Product_SupplierID;
                        entity.Product_Supplier_CommissionCateID = Product_Supplier_CommissionCateID;
                        entity.Product_Name = Product_Name;
                        entity.Product_NameInitials = Product_NameInitials;
                        entity.Product_SubName = Product_SubName;
                        entity.Product_SubNameInitials = Product_SubNameInitials;
                        entity.Product_MKTPrice = Product_MKTPrice;
                        entity.Product_GroupPrice = Product_GroupPrice;
                        entity.Product_PurchasingPrice = 0;
                        entity.Product_Price = Product_Price;
                        entity.Product_PriceUnit = Product_PriceUnit;
                        entity.Product_Unit = Product_Unit;
                        entity.Product_GroupNum = Product_GroupNum;
                        entity.Product_Note = Product_Note;
                        entity.Product_NoteColor = Product_NoteColor;
                        entity.Product_Audit_Note = "";
                        entity.Product_Weight = Product_Weight;
                        entity.Product_Img = Product_Img;
                        entity.Product_Publisher = "";
                        entity.Product_StockAmount = Product_StockAmount;
                        entity.Product_SaleAmount = 0;
                        entity.Product_Review_Count = 0;
                        entity.Product_Review_ValidCount = 0;
                        entity.Product_Review_Average = 0;
                        entity.Product_IsInsale = Product_IsInsale;
                        entity.Product_IsGroupBuy = Product_IsGroupBuy;
                        entity.Product_IsCoinBuy = Product_IsCoinBuy;
                        entity.Product_IsFavor = Product_IsFavor;
                        entity.Product_IsGift = Product_IsGift;
                        entity.Product_IsAudit = Product_IsAudit;
                        entity.Product_IsGiftCoin = Product_IsGiftCoin;
                        entity.Product_Gift_Coin = Product_Gift_Coin;
                        entity.Product_CoinBuy_Coin = Product_CoinBuy_Coin;
                        entity.Product_Addtime = DateTime.Now;
                        entity.Product_Intro = Product_Intro;
                        entity.Product_AlertAmount = Product_AlertAmount;
                        entity.Product_UsableAmount = Product_StockAmount;
                        entity.Product_IsNoStock = 0;
                        entity.Product_Spec = Product_Spec;
                        entity.Product_Maker = Product_Maker;
                        entity.Product_Sort = Product_Sort;
                        entity.Product_Hits = 0;
                        entity.Product_QuotaAmount = Product_QuotaAmount;
                        entity.Product_Site = pub.GetCurrentSite();
                        entity.U_Product_SalesByProxy = U_Product_SalesByProxy;
                        entity.U_Product_Shipper = U_Product_Shipper;
                        entity.Product_IsListShow = 0;
                        entity.Product_GroupCode = Group_Code;
                        entity.U_Product_MinBook = U_Product_MinBook;
                        entity.U_Product_DeliveryCycle = U_Product_DeliveryCycle;
                        entity.U_Product_Parameters = U_Product_Parameters;
                        entity.Product_PriceType = Product_PriceType;
                        entity.Product_ManualFee = Product_ManualFee;
                        entity.Product_LibraryImg = Product_LibraryImg;
                        entity.Product_State_Name = Product_State_Name;
                        entity.Product_City_Name = Product_City_Name;
                        entity.Product_County_Name = Product_County_Name;


                        IList<ProductExtendInfo> extends = ReadProductExtend(0);
                        extendInfo = new ProductExtendInfo();
                        extendInfo.Product_ID = Product_ID;
                        extendInfo.Extent_ID = tools.CheckInt(Request["chk_display_extend"]);
                        extendInfo.Extend_Value = tools.CheckStr(extend);
                        extends.Add(extendInfo);

                        if (MyProduct.AddProduct(entity, cateArray, tagArray, ProductImages, extends, pub.CreateUserPrivilege("a8dcfdfb-2227-40b3-a598-9643fd4c7e18")))
                        {
                            ProductWholeSalePriceInfo saleinfo = new ProductWholeSalePriceInfo(); ;
                            for (int i = 1; i <= 10; i++)
                            {
                                min_wholeprice = tools.CheckInt(Request["min_product_wholeprice_amount_" + i]);
                                max_wholeprice = tools.CheckInt(Request["max_product_wholeprice_amount_" + i]);
                                product_wholeprice_amount = tools.CheckFloat(Request["product_wholeprice_amount_" + i]);
                                if (max_wholeprice != 0)
                                {
                                    saleinfo.Product_WholeSalePrice_MinAmount = min_wholeprice;
                                    saleinfo.Product_WholeSalePrice_MaxAmount = max_wholeprice;
                                    saleinfo.Product_WholeSalePrice_Price = product_wholeprice_amount;
                                    saleinfo.Product_WholeSalePrice_ProductID = entity.Product_ID;
                                    saleinfo.Product_WholeSalePrice_Site = pub.GetCurrentSite();
                                    MySalePrice.AddProductWholeSalePrice(saleinfo);
                                }
                                else
                                {
                                    if (min_wholeprice > 0 && product_wholeprice_amount > 0)
                                    {
                                        saleinfo.Product_WholeSalePrice_MinAmount = min_wholeprice;
                                        saleinfo.Product_WholeSalePrice_MaxAmount = max_wholeprice;
                                        saleinfo.Product_WholeSalePrice_Price = product_wholeprice_amount;
                                        saleinfo.Product_WholeSalePrice_ProductID = entity.Product_ID;
                                        saleinfo.Product_WholeSalePrice_Site = pub.GetCurrentSite();
                                        MySalePrice.AddProductWholeSalePrice(saleinfo);
                                    }
                                    break;
                                }
                            }

                            pro_id = entity.Product_ID;
                            ProductInfo productinfo = new ProductInfo();
                            productinfo = MyProduct.GetProductByCode(Product_Code, pub.GetCurrentSite(), pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                            if (productinfo != null)
                            {
                                MyProduct.SaveProductTag(productinfo.Product_ID, tagArray);
                                SupplierProductCategoryInfo productgroup;
                                foreach (string subcate in Product_GroupID.Split(','))
                                {
                                    if (tools.CheckInt(subcate) > 0)
                                    {
                                        productgroup = new SupplierProductCategoryInfo();
                                        productgroup.Supplier_Product_Cate_CateID = tools.CheckInt(subcate);
                                        productgroup.Supplier_Product_Cate_ProductID = productinfo.Product_ID;
                                        MyProductCate.AddSupplierProductCategory(productgroup);
                                        productgroup = null;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    entity = GetProductByID(product_id);
                    if (entity != null)
                    {
                        entity.Product_CateID = Product_Cate;
                        entity.Product_BrandID = Product_BrandID;
                        entity.Product_TypeID = Product_TypeID;
                        entity.Product_Supplier_CommissionCateID = Product_Supplier_CommissionCateID;
                        entity.Product_Name = Product_Name;
                        entity.Product_NameInitials = Product_NameInitials;
                        entity.Product_SubName = Product_SubName;
                        entity.Product_SubNameInitials = Product_SubNameInitials;
                        entity.Product_MKTPrice = Product_MKTPrice;
                        entity.Product_GroupPrice = Product_GroupPrice;
                        entity.Product_Price = Product_Price;
                        entity.Product_PriceUnit = Product_PriceUnit;
                        entity.Product_Unit = Product_Unit;
                        entity.Product_GroupNum = Product_GroupNum;
                        entity.Product_Note = Product_Note;
                        entity.Product_NoteColor = Product_NoteColor;
                        entity.Product_Audit_Note = "";
                        entity.Product_Weight = Product_Weight;
                        entity.Product_Img = Product_Img;
                        entity.Product_Publisher = "";
                        entity.Product_IsInsale = Product_IsInsale;
                        entity.Product_IsGroupBuy = Product_IsGroupBuy;
                        entity.Product_IsCoinBuy = Product_IsCoinBuy;
                        entity.Product_IsFavor = Product_IsFavor;
                        entity.Product_IsGift = Product_IsGift;
                        entity.Product_IsAudit = Product_IsAudit;
                        entity.Product_IsGiftCoin = Product_IsGiftCoin;
                        entity.Product_Gift_Coin = Product_Gift_Coin;
                        entity.Product_CoinBuy_Coin = Product_CoinBuy_Coin;
                        entity.Product_Addtime = DateTime.Now;
                        entity.Product_Intro = Product_Intro;
                        entity.Product_AlertAmount = Product_AlertAmount;
                        entity.Product_Spec = Product_Spec;
                        entity.Product_Maker = Product_Maker;
                        entity.Product_Sort = Product_Sort;
                        entity.Product_QuotaAmount = Product_QuotaAmount;
                        entity.Product_Site = pub.GetCurrentSite();
                        entity.U_Product_SalesByProxy = U_Product_SalesByProxy;
                        entity.U_Product_Shipper = U_Product_Shipper;
                        entity.Product_GroupCode = Group_Code;
                        entity.U_Product_MinBook = U_Product_MinBook;
                        entity.U_Product_DeliveryCycle = U_Product_DeliveryCycle;
                        entity.U_Product_Parameters = U_Product_Parameters;
                        entity.Product_PriceType = Product_PriceType;
                        entity.Product_ManualFee = Product_ManualFee;
                        entity.Product_LibraryImg = Product_LibraryImg;
                        entity.Product_State_Name = Product_State_Name;
                        entity.Product_City_Name = Product_City_Name;
                        entity.Product_County_Name = Product_County_Name;
                        IList<ProductExtendInfo> extends = ReadProductExtend(product_id);

                        if (MyProduct.EditProduct(entity, cateArray, tagArray, ProductImages, extends, pub.CreateUserPrivilege("854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d")))
                        {
                            #region 批发价格设置
                            MySalePrice.DelProductWholeSalePriceByProductID(product_id);
                            ProductWholeSalePriceInfo saleinfo = new ProductWholeSalePriceInfo(); ;

                            for (int i = 1; i <= 10; i++)
                            {
                                min_wholeprice = tools.CheckInt(Request["min_product_wholeprice_amount_" + i]);
                                max_wholeprice = tools.CheckInt(Request["max_product_wholeprice_amount_" + i]);
                                product_wholeprice_amount = tools.CheckFloat(Request["product_wholeprice_amount_" + i]);
                                if (max_wholeprice != 0)
                                {
                                    saleinfo.Product_WholeSalePrice_MinAmount = min_wholeprice;
                                    saleinfo.Product_WholeSalePrice_MaxAmount = max_wholeprice;
                                    saleinfo.Product_WholeSalePrice_Price = product_wholeprice_amount;
                                    saleinfo.Product_WholeSalePrice_ProductID = entity.Product_ID;
                                    saleinfo.Product_WholeSalePrice_Site = pub.GetCurrentSite();
                                    MySalePrice.AddProductWholeSalePrice(saleinfo);
                                }
                                else
                                {
                                    if (min_wholeprice > 0 && product_wholeprice_amount > 0)
                                    {
                                        saleinfo.Product_WholeSalePrice_MinAmount = min_wholeprice;
                                        saleinfo.Product_WholeSalePrice_MaxAmount = max_wholeprice;
                                        saleinfo.Product_WholeSalePrice_Price = product_wholeprice_amount;
                                        saleinfo.Product_WholeSalePrice_ProductID = entity.Product_ID;
                                        saleinfo.Product_WholeSalePrice_Site = pub.GetCurrentSite();
                                        MySalePrice.AddProductWholeSalePrice(saleinfo);
                                    }
                                    break;
                                }
                            }
                            #endregion

                            MyProduct.SaveProductTag(entity.Product_ID, tagArray);

                            MyProductCate.DelSupplierProductCategoryByProductID(product_id);
                            SupplierProductCategoryInfo productgroup;
                            foreach (string subcate in Product_GroupID.Split(','))
                            {
                                if (tools.CheckInt(subcate) > 0)
                                {
                                    productgroup = new SupplierProductCategoryInfo();
                                    productgroup.Supplier_Product_Cate_CateID = tools.CheckInt(subcate);
                                    productgroup.Supplier_Product_Cate_ProductID = product_id;
                                    MyProductCate.AddSupplierProductCategory(productgroup);
                                    productgroup = null;
                                }
                            }

                            //记录各会员等级价格调价
                            IList<MemberGradeInfo> grades = null;
                            QueryInfo Query = new QueryInfo();

                            Query = null;
                            MyPrice.DelProductPrice(product_id);
                            QueryInfo Query1 = new QueryInfo();
                            Query1.PageSize = 0;
                            Query1.CurrentPage = 1;
                            Query1.ParamInfos.Add(new ParamInfo("AND", "str", "MemberGradeInfo.Member_Grade_Site", "=", pub.GetCurrentSite()));
                            Query1.OrderInfos.Add(new OrderInfo("MemberGradeInfo.Member_Grade_ID", "asc"));
                            grades = MyGrade.GetMemberGrades(Query1, pub.CreateUserPrivilege("1c955ea6-881f-48d8-ba8d-c5aa7ce9cfea"));
                            if (grades != null)
                            {
                                foreach (MemberGradeInfo grade in grades)
                                {
                                    Product_Grade_Price = tools.CheckFloat(Request.Form["Product_Grade_Price_" + grade.Member_Grade_ID]);
                                    if (Product_Grade_Price > 0)
                                    {
                                        ProductPriceInfo priceinfo = new ProductPriceInfo();
                                        priceinfo.Product_Price_ID = 0;
                                        priceinfo.Product_Price_MemberGradeID = grade.Member_Grade_ID;
                                        priceinfo.Product_Price_Price = Product_Grade_Price;
                                        priceinfo.Product_Price_ProcutID = product_id;
                                        MyPrice.AddProductPrice(priceinfo);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //pub.Msg("positive", "操作成功", "操作成功", true, "product_list.aspx");

            Response.Write("success");
            Response.End();
        }
        else
        {
            Response.Write("操作失败，请稍后重试");
            Response.End();
            //pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public double GetProductPrice()
    {
        double product_price = 0;

        double weight = tools.CheckFloat(Request["weight"]);
        double fee = tools.CheckFloat(Request["fee"]);

        product_price = Math.Round(pub.GetProductPrice(fee, weight));

        return product_price;
    }

    public void EditProductSEO()
    {
        int Product_ID = tools.CheckInt(Request.Form["Product_ID"]);

        string Product_SEO_Title = tools.CheckStr(Request.Form["Product_SEO_Title"]);
        string Product_SEO_Keyword = tools.CheckStr(Request.Form["Product_SEO_Keyword"]);
        string Product_SEO_Description = tools.CheckStr(Request.Form["Product_SEO_Description"]);
        int Product_SupplierID = tools.NullInt(Session["supplier_id"]);


        ProductInfo entity = GetProductByID(Product_ID);
        if (entity == null)
        {
            entity = new ProductInfo();
            pub.Msg("error", "错误信息", "记录不存在！", false, "{back}");
            return;
        }
        if (entity.Product_SupplierID != Product_SupplierID)
        {
            pub.Msg("error", "错误信息", "记录不存在！", false, "{back}");
            return;
        }
        entity.Product_ID = Product_ID;

        entity.Product_SEO_Title = Product_SEO_Title;
        entity.Product_SEO_Keyword = Product_SEO_Keyword;
        entity.Product_SEO_Description = Product_SEO_Description;


        if (MyProduct.EditProductInfo(entity, pub.CreateUserPrivilege("854d2b3f-8bf6-4f17-9e5e-4bc1fe784a5d")))
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "product_seo_list.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public void DelProduct()
    {
        int product_id = tools.CheckInt(Request.QueryString["product_id"]);
        ProductInfo entity = null;
        ProductInfo productInfo = null;
        entity = GetProductByID(product_id);

        if (entity != null)
        {
            string[] product_ids = GetGroupProductID(entity.Product_GroupCode).Split(',');

            if (product_ids != null)
            {
                foreach (string pid in product_ids)
                {
                    productInfo = GetProductByID(tools.NullInt(pid));

                    if (productInfo != null)
                    {
                        if (productInfo.Product_SupplierID == tools.NullInt(Session["supplier_id"]))
                        {
                            if (MyProduct.DelProduct(productInfo.Product_ID, pub.CreateUserPrivilege("fbb427c5-73ce-4f4d-9a36-6e1d1b4d802f")) == -1)
                            {
                                MyProductCate.DelSupplierProductCategoryByProductID(productInfo.Product_ID);
                                MyPriceAsk.DelSupplierPriceAskByProductID(productInfo.Product_ID, pub.CreateUserPrivilege("48800724-3e45-45d6-9d93-6ad5ab6eb91e"));
                            }
                        }
                    }
                }
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


    public int GetSupplierProductStockAmount(string groupCode)
    {
        string product_ids = GetGroupProductID(groupCode);
        int Amount = 0;

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_SupplierID", "=", tools.NullInt(Session["supplier_id"]).ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", product_ids));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "desc"));
        IList<ProductInfo> entitys = MyProduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (entitys != null)
        {
            foreach (ProductInfo entity in entitys)
            {
                Amount = Amount + entity.Product_StockAmount;
            }
        }
        return Amount;
    }

    public int GetSupplierProductAmount()
    {
        int Amount = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_SupplierID", "=", tools.NullInt(Session["supplier_id"]).ToString()));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "desc"));
        IList<ProductInfo> entitys = MyProduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (entitys != null)
        {
            Amount = entitys.Count - 1;
        }
        return Amount;
    }

    public int GetSupplierProductAmount(int supplier_id)
    {
        int Amount = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_SupplierID", "=", supplier_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "desc"));
        IList<ProductInfo> entitys = MyProduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (entitys != null)
        {
            Amount = entitys.Count - 1;
        }
        return Amount;
    }

    public int GetSupplierProductLimit()
    {
        int Limit = 0;
        int Grade_ID = GetSupplierGrade(tools.NullInt(Session["supplier_id"]));
        SupplierShopGradeInfo gradeinfo = MyShopGrade.GetSupplierShopGradeByID(Grade_ID, pub.CreateUserPrivilege("c558f983-68ec-4a91-a330-1c1f04ebdf01"));
        if (gradeinfo != null)
        {
            Limit = gradeinfo.Shop_Grade_ProductLimit;
        }
        return Limit;
    }

    public ProductInfo GetProductByID(int product_id)
    {
        return MyProduct.GetProductByID(product_id, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
    }

    public string GetProductNameByID(int product_id)
    {
        string Product_Name = "--";
        ProductInfo entity = GetProductByID(product_id);
        if (entity != null)
        {
            Product_Name = entity.Product_Name;
        }
        return Product_Name;
    }

    public string GetGroupProductID(string Group_Code)
    {
        return MyProduct.GetGroupProductID(Group_Code);
    }

    public string[] GetProductImg(int product_id)
    {
        string ipaths = MyProduct.GetProductImg(product_id);
        string[] ipathArr = { "/images/detail_no_pic.gif", "/images/detail_no_pic.gif", "/images/detail_no_pic.gif", "/images/detail_no_pic.gif", "/images/detail_no_pic.gif" };
        ipathArr = ipaths.Split(',');
        return ipathArr;
    }

    public IList<ProductPriceInfo> GetProductPrices(int product_id)
    {
        return MyPrice.GetProductPrices(product_id);
    }

    public string GetProductCategory(int product_id)
    {
        return MyProduct.GetProductCategory(product_id);
    }

    public string GetProductGroup(int product_id)
    {
        string cateid = "";
        IList<SupplierProductCategoryInfo> entitys = MyProductCate.GetSupplierProductCategorysByProductID(product_id);
        if (entitys != null)
        {
            foreach (SupplierProductCategoryInfo entity in entitys)
            {
                if (cateid.Length == 0)
                {
                    cateid = entity.Supplier_Product_Cate_CateID.ToString();
                }
                else
                {
                    cateid = cateid + "," + entity.Supplier_Product_Cate_CateID.ToString();
                }
            }
        }
        return cateid;
    }

    public string Get_All_SubCate(int Cate_id)
    {
        string Cate_Arry = MyCBLL.Get_All_SubCateID(Cate_id);
        return Cate_Arry;
    }

    //获取指定类别下全部产品编号
    public string Get_All_CateProductID(string Cate_Arry)
    {
        string ProudctID_Arry = "";
        ProudctID_Arry = MyProduct.GetCateProductID(Cate_Arry);
        return ProudctID_Arry;
    }

    //产品列表
    public void Supplier_Product_List_bak(string type)
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        Pageurl = "?action=list";
        if (curpage < 1)
        {
            curpage = 1;
        }
        string keyword, defaultkey;
        int product_cate;
        product_cate = tools.CheckInt(Request["product_cate"]);
        if (product_cate == 0) { product_cate = tools.CheckInt(Request["product_cate_parent"]); }
        keyword = tools.CheckStr(Request["product_keyword"]);
        if (keyword != "输入商品编码、商品名称进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入商品编码、商品名称进行搜索";
        }
        if (keyword == "输入商品编码、商品名称进行搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }
        SupplierShopInfo shopinfo = MyShop.GetSupplierShopBySupplierID(supplier_id);
        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\">产品编号</th>");
        Response.Write("  <th align=\"center\" valign=\"middle\">产品名称</th>");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\">所属分类</th>");
        Response.Write("  <th width=\"70\" align=\"center\" valign=\"middle\">价格</th>");
        Response.Write("  <th width=\"60\" align=\"center\" valign=\"middle\">库存</th>");
        if (shopinfo != null)
        {
            if (shopinfo.Shop_Type == 1)
            {
                Response.Write("  <th width=\"60\" align=\"center\" valign=\"middle\">点击量</th>");
            }
        }
        Response.Write("  <th width=\"40\" align=\"center\" valign=\"middle\">上架</th>");
        Response.Write("  <th width=\"80\" align=\"center\" valign=\"middle\">操作</th>");
        Response.Write("</tr>");
        string productURL = string.Empty;
        string parentname = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));
        if (product_cate > 0)
        {
            string subCates = Get_All_SubCate(product_cate);
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", subCates));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + subCates + ")"));
            //string product_idstr = Get_All_CateProductID(subCates);
            //Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "IN", product_idstr));
        }
        if (defaultkey.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_Name", "like", defaultkey));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_Code", "like", defaultkey));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_SupplierID", "=", tools.NullInt(Session["supplier_id"]).ToString()));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "desc"));
        IList<ProductInfo> entitys = MyProduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        PageInfo page = MyProduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        if (entitys != null)
        {
            foreach (ProductInfo entity in entitys)
            {
                i = i + 1;
                parentname = "";
                CategoryInfo parent = MyCBLL.GetCategoryByID(entity.Product_CateID, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
                if (parent != null)
                {
                    parentname = parent.Cate_Name;
                }
                Response.Write("<tr bgcolor=\"#ffffff\">");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + entity.Product_Code + "</td>");
                Response.Write("<td height=\"35\" align=\"left\" class=\"comment_td_bg\" style=\"padding-left:10px;\" valign=\"middle\">" + entity.Product_Name + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + parentname + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + pub.FormatCurrency(entity.Product_Price) + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + entity.Product_StockAmount + "</td>");
                if (shopinfo != null)
                {
                    if (shopinfo.Shop_Type == 1)
                    {
                        Response.Write("  <td align=\"center\" class=\"comment_td_bg\" valign=\"middle\">" + entity.Product_Hits + "</td>");
                    }
                }
                if (entity.Product_IsInsale == 1)
                {
                    Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">是</td>");
                }
                else
                {
                    Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">否</td>");
                }
                Response.Write("<td height=\"35\" align=\"center\" class=\"comment_td_bg\" valign=\"middle\">");
                if (type == "product")
                {
                    //Response.Write("<a href=\"/supplier/Product_view.aspx?product_ID=" + entity.Product_ID + "\" style=\"color:#3366cc;\" target=\"_blank\"><img src=\"/images/icon_vieworder.gif\" border=\"0\" alt=\"查看\"></a>");
                    Response.Write(" <a href=\"/supplier/Product_Edit.aspx?product_ID=" + entity.Product_ID + "\" style=\"color:#3366cc;\"><img src=\"/images/btn_edit.gif\" border=\"0\" alt=\"修改\"></a>");
                    Response.Write(" <a href=\"/supplier/Product_do.aspx?action=del&product_ID=" + entity.Product_ID + "\" style=\"color:#3366cc;\"><img src=\"/images/btn_del.gif\" border=\"0\" alt=\"删除\"></a>");
                }
                if (type == "seo")
                {
                    Response.Write("<a href=\"/supplier/Product_SEO_Edit.aspx?product_ID=" + entity.Product_ID + "\" style=\"color:#3366cc;\"><img src=\"/images/btn_edit.gif\" border=\"0\" alt=\"SEO优化\"></a>");
                }

                //Response.Write(" <a href=\"/supplier/keywordbidding_add.aspx?Product_ID=" + entity.Product_ID + "\" style=\"color:#3366cc;\"><img src=\"/images/btn_add.gif\" border=\"0\" alt=\"选择竞价\"></a>");

                Response.Write("</td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" align=\"center\" cellpadding=\"2\"><tr><td align=\"right\" height=\"10\"></td></tr><tr><td align=\"right\"><div class=\"page\" style=\"float:right;\">");
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
            Response.Write("</div></td></tr></table>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#ffffff\"><td width=\"70\" height=\"35\" colspan=\"8\" align=\"center\" valign=\"middle\">没有记录</td></tr>");
        }
        Response.Write("</table>");
    }

    public void Supplier_Product_List(string type)
    {
        StringBuilder strHTML = new StringBuilder();

        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        Pageurl = "?action=list";
        if (curpage < 1)
        {
            curpage = 1;
        }
        string keyword, defaultkey;
        int product_cate;
        product_cate = tools.CheckInt(Request["product_cate"]);
        if (product_cate == 0) { product_cate = tools.CheckInt(Request["product_cate_parent"]); }
        keyword = tools.CheckStr(Request["product_keyword"]);
        if (keyword != "商品名称/商品编号" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "商品名称/商品编号";
        }
        if (keyword == "商品名称/商品编号")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }

        strHTML.Append("<div class=\"blk04_sz\">");
        strHTML.Append("<form id=\"frm_product\" name=\"frm_product\" method=\"post\" action=\"/supplier/product_list.aspx\" >");
        strHTML.Append(" <span id=\"main_cate\">" + Product_Category_Select(product_cate, "main_cate") + "</span>");

        strHTML.Append("<input name=\"product_keyword\" id=\"product_keyword\" type=\"text\"");
        if (keyword != "")
        {
            strHTML.Append(" value=\"" + keyword + "\"");
        }
        else
        {
            strHTML.Append(" value=\"商品名称/商品编号\"");
        }
        strHTML.Append(" onfocus=\"if (this.value==this.defaultValue) this.value=''\" onblur=\"if (this.value=='') this.value=this.defaultValue\" /><a href=\"javascript:void(0);\" onclick=\"$('#frm_product').submit();\">搜 索</a>");
        strHTML.Append("<a style=\"float:right\" href=\"/supplier/product_seo_list.aspx\" >商品SEO</a>" +
            " <a  style=\"float:right\" href=\"/supplier/product_category_list.aspx\" >店内分类</a>");
        strHTML.Append("</form>");
        strHTML.Append("</div>");

        strHTML.Append("<div class=\"b14_1_main\" style=\"margin-top: 15px;\">");

        strHTML.Append("<table width=\"973\" border=\"0\" cellspacing=\"2\" cellpadding=\"0\" class=\"table02\">");
        strHTML.Append("<tr>");

        //strHTML.Append("<td width=\"120\" class=\"name\">商品编号</td>");
        strHTML.Append("<td class=\"name\">商品名称</td>");
        strHTML.Append("<td width=\"120\" class=\"name\">所属分类</td>");
        strHTML.Append("<td width=\"140\" class=\"name\">价格</td>");
        strHTML.Append("<td width=\"140\" class=\"name\">库存</td>");
        strHTML.Append("<td width=\"140\" class=\"name\">上架</td>");
        strHTML.Append("<td width=\"120\" class=\"name\">操作</td>");
        strHTML.Append("</tr>");

        string productURL = string.Empty;
        string parentname = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));
        if (product_cate > 0)
        {
            string subCates = Get_All_SubCate(product_cate);
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", subCates));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + subCates + ")"));
        }

        if (defaultkey.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_Name", "like", defaultkey));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_Code", "like", defaultkey));
        }
        //聚合是否列表显示 暂时屏蔽掉
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsListShow", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_SupplierID", "=", tools.NullInt(Session["supplier_id"]).ToString()));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "desc"));
        IList<ProductInfo> entitys = MyProduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        PageInfo page = MyProduct.GetPageInfo(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));

        if (entitys != null)
        {
            foreach (ProductInfo entity in entitys)
            {
                productURL = pageurl.FormatURL(pageurl.product_detail, entity.Product_ID.ToString());
                parentname = "";
                CategoryInfo parent = MyCBLL.GetCategoryByID(entity.Product_CateID, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
                if (parent != null)
                {
                    parentname = parent.Cate_Name;
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

                strHTML.Append("<td><span><a href=\"" + productURL + "\"  target=\"_blank\">" + entity.Product_Name + "</a></span></td>");
                strHTML.Append("<td>" + parentname + "</td>");
                //if (entity.Product_PriceType == 1)
                //{
                strHTML.Append("<td>" + pub.FormatCurrency(entity.Product_Price) + "</td>");
                //}
                //else
                //{
                //    strHTML.Append("<td>" + pub.FormatCurrency(pub.GetProductPrice(entity.Product_ManualFee, entity.Product_Weight)) + "</td>");
                //}

                strHTML.Append("<td>" + GetSupplierProductStockAmount(entity.Product_GroupCode) + "</td>");

                if (entity.Product_IsInsale == 1)
                {
                    strHTML.Append("<td>是</td>");
                }
                else
                {
                    strHTML.Append("<td>否</td>");
                }

                strHTML.Append("<td>");
                if (type == "product")
                {
                    strHTML.Append("<span><a href=\"/supplier/Product_Edit.aspx?product_ID=" + entity.Product_ID + "\" class=\"a05\">修改</a></span>");
                    strHTML.Append("<span><a href=\"javascript:;\" onclick=\"ajax_product_del(" + entity.Product_ID + ");\">删除</a></span");
                }
                if (type == "seo")
                {
                    strHTML.Append("<span><a href=\"/supplier/Product_SEO_Edit.aspx?product_ID=" + entity.Product_ID + "\" class=\"a05\">SEO优化</a></span");
                }
                strHTML.Append("</td>");
                strHTML.Append("</tr>");
            }

            Response.Write(strHTML.ToString());
            Response.Write("</table>");
            //Response.Write("<input name=\"chk_all_products\" id=\"chk_all_products\"  onclick=\"check_SupplierCenter_ProductAll();\" type=\"checkbox\"   checked=\"checked\" />全选   <a href=\"javascript:;\" onclick=\"BatchInsaleProduct();\">上架</a> <a href=\"javascript:;\" onclick=\"BatchCancelInsaleProduct();\">下架</a> </div>");
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
            Response.Write("</div>");
        }
        else
        {
            strHTML.Append("<tr>");
            strHTML.Append("<td colspan=\"7\">暂无商品数据！</td>");
            strHTML.Append("</tr>");
            strHTML.Append("</table>");
            strHTML.Append("</div>");
            Response.Write(strHTML.ToString());
        }
    }

    public string GetProductWholeSales(ProductInfo productInfo)
    {
        int amount = 0;
        string price_str = "";
        int i = 0;
        IList<ProductWholeSalePriceInfo> entitys = MySalePrice.GetProductWholeSalePriceByProductID(productInfo.Product_ID);
        if (entitys != null)
        {
            foreach (ProductWholeSalePriceInfo entity in entitys)
            {
                i++;

                price_str += "<div>";
                if (i == 1)
                {
                    price_str += " <div style=\"display:inline-block;width:60px;text-align:right;\">数量" + i + "：</div><input type=\"text\" id=\"min_product_wholeprice_amount_" + i + "\" name=\"min_product_wholeprice_amount_" + i + "\" value=\"" + entity.Product_WholeSalePrice_MinAmount + "\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" size=\"10\"  autocomplete=\"off\" />&nbsp;&nbsp;";
                }
                else
                {
                    price_str += " <div style=\"display:inline-block;width:60px;text-align:right;\">数量" + i + "：</div><input type=\"text\" readonly=\"readonly\" id=\"min_product_wholeprice_amount_" + i + "\" name=\"min_product_wholeprice_amount_" + i + "\" value=\"" + entity.Product_WholeSalePrice_MinAmount + "\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" size=\"10\" autocomplete=\"off\" />&nbsp;&nbsp;";
                }
                price_str += " <div style=\"display:inline-block;width:60px;text-align:right;\">数量" + i + "：</div><input type=\"text\" id=\"max_product_wholeprice_amount_" + i + "\" name=\"max_product_wholeprice_amount_" + i + "\" value=\"" + entity.Product_WholeSalePrice_MaxAmount + "\" onkeyup=\"if(isNaN(value))execCommand('undo');changeprice('" + i + "');\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" size=\"10\" autocomplete=\"off\" />&nbsp;&nbsp;";

                if (productInfo.Product_PriceType == 1)
                {
                    price_str += " <div style=\"display:inline-block;width:60px;text-align:right;\">价格" + i + "：</div><input type=\"text\"  name=\"product_wholeprice_amount_" + i + "\" id=\"product_wholeprice_amount_" + i + "\" value=\"" + entity.Product_WholeSalePrice_Price + "\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" size=\"10\" autocomplete=\"off\" /> </div>";
                }
                else
                {
                    price_str += " <div style=\"display:inline-block;width:60px;text-align:right;\">价格" + i + "：</div><input type=\"text\"  name=\"product_wholeprice_amount_" + i + "\" id=\"product_wholeprice_amount_" + i + "\" value=\"" + pub.GetProductPrice(productInfo.Product_ManualFee, productInfo.Product_Weight) + "\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" size=\"10\" autocomplete=\"off\" readonly=\"readonly\" /> </div>";
                }
                price_str += "<br/>";
            }
        }
        else
        {
            for (int n = 1; n <= 10; n++)
            {
                price_str += "<div>";
                if (n == 1)
                {
                    price_str += " <div style=\"display:inline-block;width:60px;text-align:right;\">数量" + n + "：</div><input type=\"text\"  id=\"min_product_wholeprice_amount_" + n + "\" name=\"min_product_wholeprice_amount_" + n + "\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" size=\"10\" autocomplete=\"off\" />&nbsp;&nbsp;";
                }
                else
                {
                    price_str += " <div style=\"display:inline-block;width:60px;text-align:right;\">数量" + n + "：</div><input type=\"text\" readonly=\"readonly\" id=\"min_product_wholeprice_amount_" + n + "\" name=\"min_product_wholeprice_amount_" + n + "\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" size=\"10\" autocomplete=\"off\" />&nbsp;&nbsp;";
                }
                price_str += " <div style=\"display:inline-block;width:60px;text-align:right;\">数量" + n + "：</div><input type=\"text\" id=\"max_product_wholeprice_amount_" + n + "\" name=\"max_product_wholeprice_amount_" + n + "\" onkeyup=\"if(isNaN(value))execCommand('undo');changeprice('" + n + "');\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" size=\"10\" autocomplete=\"off\" />&nbsp;&nbsp;";

                if (productInfo.Product_PriceType == 1)
                {
                    price_str += " <div style=\"display:inline-block;width:60px;text-align:right;\">价格" + n + "：</div><input type=\"text\"  name=\"product_wholeprice_amount_" + n + "\" id=\"product_wholeprice_amount_" + n + "\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" size=\"10\" autocomplete=\"off\" /> </div>";
                }
                else
                {
                    price_str += " <div style=\"display:inline-block;width:60px;text-align:right;\">价格" + n + "：</div><input type=\"text\"  name=\"product_wholeprice_amount_" + n + "\" id=\"product_wholeprice_amount_" + n + "\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" size=\"10\" autocomplete=\"off\"  readonly=\"readonly\" value=\"" + pub.GetProductPrice(productInfo.Product_ManualFee, productInfo.Product_Weight) + "\" /> </div>";
                }
                price_str += "<br/>";
            }

        }
        if (i >= 1 && i < 10)
        {
            for (int m = i + 1; m <= 10; m++)
            {
                price_str += "<div>";
                if (m == 1)
                {
                    price_str += " <div style=\"display:inline-block;width:60px;text-align:right;\">数量" + m + "：</div><input type=\"text\" id=\"min_product_wholeprice_amount_" + m + "\" name=\"min_product_wholeprice_amount_" + m + "\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" size=\"10\" autocomplete=\"off\" />&nbsp;&nbsp;";
                }
                else
                {
                    price_str += " <div style=\"display:inline-block;width:60px;text-align:right;\">数量" + m + "：</div><input type=\"text\" readonly=\"readonly\" id=\"min_product_wholeprice_amount_" + m + "\" name=\"min_product_wholeprice_amount_" + m + "\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" size=\"10\" autocomplete=\"off\" />&nbsp;&nbsp;";
                }
                price_str += " <div style=\"display:inline-block;width:60px;text-align:right;\">数量" + m + "：</div><input type=\"text\" id=\"max_product_wholeprice_amount_" + m + "\" name=\"max_product_wholeprice_amount_" + m + "\" onkeyup=\"if(isNaN(value))execCommand('undo');changeprice('" + m + "');\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" size=\"10\" autocomplete=\"off\" />&nbsp;&nbsp;";
                if (productInfo.Product_PriceType == 1)
                {
                    price_str += " <div style=\"display:inline-block;width:60px;text-align:right;\">价格" + m + "：</div><input type=\"text\"  name=\"product_wholeprice_amount_" + m + "\" id=\"product_wholeprice_amount_" + m + "\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" size=\"10\" autocomplete=\"off\" /> </div>";
                }
                else
                {
                    price_str += " <div style=\"display:inline-block;width:60px;text-align:right;\">价格" + m + "：</div><input type=\"text\"  name=\"product_wholeprice_amount_" + m + "\" id=\"product_wholeprice_amount_" + m + "\" onkeyup=\"if(isNaN(value))execCommand('undo')\" onafterpaste=\"if(isNaN(value))execCommand('undo')\" size=\"10\" autocomplete=\"off\"  readonly=\"readonly\" value=\"" + pub.GetProductPrice(productInfo.Product_ManualFee, productInfo.Product_Weight) + "\" /> </div>";
                }
                price_str += "<br/>";
            }
        }
        return price_str;
    }

    //产品限时促销列表
    public void Supplier_Limit_List()
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        Pageurl = "?action=list";
        if (curpage < 1)
        {
            curpage = 1;
        }
        string keyword, defaultkey;
        keyword = Request["product_keyword"];
        if (keyword != "输入商品编码、商品名称、通用名、生产企业、拼音首字母进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "输入商品编码、商品名称、通用名、生产企业、拼音首字母进行搜索";
        }
        if (keyword == "输入商品编码、商品名称、通用名、生产企业、拼音首字母进行搜索")
        {
            defaultkey = "";
        }
        else
        {
            defaultkey = keyword;
        }
        Response.Write("<table width=\"973\" border=\"0\" cellspacing=\"2\" cellpadding=\"0\" class=\"table02\">");
        Response.Write("<tr>");
        Response.Write("  <td width=\"220\" class=\"name\">产品名称</td>");
        Response.Write("  <td width=\"167\" class=\"name\">限时价</td>");
        Response.Write("  <td width=\"216\" class=\"name\">开始时间</td>");
        Response.Write("  <td width=\"220\" class=\"name\">结束时间</td>");
        Response.Write("  <td width=\"141\" class=\"name\">操作</td>");
        Response.Write("</tr>");
        string productURL = string.Empty;
        string parentname = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionLimitInfo.Promotion_Limit_Site", "=", pub.GetCurrentSite()));
        if (defaultkey.Length > 0)
        {
            Pageurl += "&product_keyword=" + defaultkey;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "PromotionLimitInfo.Promotion_Limit_ProductID", "in", "select product_id from product_basic where product_code like '%" + defaultkey + "%' or product_name like '%" + defaultkey + "%'"));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "PromotionLimitInfo.Promotion_Limit_GroupID", "=", supplier_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("PromotionLimitInfo.Promotion_Limit_ID", "Desc"));
        IList<PromotionLimitInfo> entitys = MyLimit.GetPromotionLimits(Query, pub.CreateUserPrivilege("22d21441-155a-4dc5-aec6-dcf5bdedd5cf"));
        PageInfo page = MyLimit.GetPageInfo(Query, pub.CreateUserPrivilege("22d21441-155a-4dc5-aec6-dcf5bdedd5cf"));
        ProductInfo product = null;
        if (entitys != null)
        {
            foreach (PromotionLimitInfo entity in entitys)
            {
                i = i + 1;
                parentname = "";

                if (i % 2 == 0)
                {
                    Response.Write("<tr class=\"bg\">");
                }
                else
                {
                    Response.Write("<tr>");
                }

                product = GetProductByID(entity.Promotion_Limit_ProductID);
                if (product != null)
                {
                    Response.Write("<td title=\"" + product.Product_Name + "\">" + tools.CutStr(product.Product_Name, 30) + "</td>");
                }
                Response.Write("<td>" + pub.FormatCurrency(entity.Promotion_Limit_Price) + "</td>");
                Response.Write("<td>" + entity.Promotion_Limit_Starttime.ToString("yyyy-MM-dd") + "</td>");
                Response.Write("<td>" + entity.Promotion_Limit_Endtime.ToString("yyyy-MM-dd") + "</td>");
                Response.Write("<td>");
                Response.Write("<span><a href=\"/supplier/Product_Limit_Edit.aspx?limit_ID=" + entity.Promotion_Limit_ID + "\" class=\"a12\">修改</a></span>");
                Response.Write(" <span><a href=\"/supplier/Product_Limit_do.aspx?action=remove&limit_ID=" + entity.Promotion_Limit_ID + "\">删除</a></span>");
                Response.Write("</td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");

            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
        }
        else
        {
            Response.Write("<tr><td  colspan=\"5\" align=\"center\" valign=\"middle\">没有记录</td></tr>");
        }
        Response.Write("</table>");
    }

    public string Select_Supplier_Product(int Product_ID)
    {
        string defaultkey = tools.NullStr(Request["keyword"]);
        string Html_Str = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));

        if (defaultkey.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_Name", "like", defaultkey));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_Code", "like", defaultkey));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_SupplierID", "=", tools.NullInt(Session["supplier_id"]).ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.U_Product_Shipper", "=", "0"));
        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "desc"));
        IList<ProductInfo> entitys = MyProduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        Html_Str += "<select name=\"Product_ID\" onchange=\"$('#product_area').load('Product_Limit_do.aspx?action=showproduct&product_id='+encodeURI($(this).val())+'&timer='+Math.random());\">";
        Html_Str += "<option value=\"0\">选择产品</option>";
        if (entitys != null)
        {
            foreach (ProductInfo entity in entitys)
            {
                if (Product_ID == entity.Product_ID)
                {
                    Html_Str += "<option value=\"" + entity.Product_ID + "\" selected>" + entity.Product_Name + "</option>";
                }
                else
                {
                    Html_Str += "<option value=\"" + entity.Product_ID + "\">" + entity.Product_Name + "</option>";
                }
            }
        }
        Html_Str += "</select>";
        return Html_Str;
    }

    public void Show_ProductInfo(int product_id)
    {
        if (product_id == 0)
        {
            product_id = tools.CheckInt(Request["product_id"]);
        }
        ProductInfo entity = GetProductByID(product_id);
        if (entity != null)
        {
            if (tools.NullInt(Session["supplier_id"]) == entity.Product_SupplierID)
            {
                //Response.Write("产品编号：" + entity.Product_Code + "<br>");
                //Response.Write("市场价格：" + pub.FormatCurrency(entity.Product_MKTPrice) + "<br>");
                Response.Write("产品价格：" + pub.FormatCurrency(entity.Product_Price));
            }
        }
    }

    public virtual void AddPromotionLimit()
    {
        int Promotion_Limit_ID = 0;
        int product_id = tools.CheckInt(Request.Form["product_id"]);
        int Promotion_Limit_GroupID = tools.NullInt(Session["supplier_id"]);
        DateTime Promotion_Limit_Starttime = tools.NullDate(Request.Form["Promotion_Limit_Starttime"]);
        DateTime Promotion_Limit_Endtime = tools.NullDate(Request.Form["Promotion_Limit_Endtime"]);
        string favor_gradeid = tools.CheckStr(Request.Form["Member_Grade"]);
        if (tools.NullDate(Promotion_Limit_Starttime).Year < 1900 || tools.NullDate(Promotion_Limit_Endtime).Year < 1900)
        {
            pub.Msg("error", "错误信息", "时间设置错误", false, "{back}");
        }
        if (product_id == 0)
        {
            pub.Msg("error", "错误信息", "请选择产品信息", false, "{back}");
        }

        PromotionLimitInfo entity = null;
        entity = new PromotionLimitInfo();
        entity.Promotion_Limit_ID = Promotion_Limit_ID;
        entity.Promotion_Limit_GroupID = Promotion_Limit_GroupID;
        entity.Promotion_Limit_ProductID = product_id;
        entity.Promotion_Limit_Price = tools.CheckFloat(Request.Form["Promotion_Limit_Price"]);
        entity.Promotion_Limit_Amount = 0;
        entity.Promotion_Limit_Limit = 0;
        entity.Promotion_Limit_Starttime = Promotion_Limit_Starttime;
        entity.Promotion_Limit_Endtime = Promotion_Limit_Endtime;
        entity.Promotion_Limit_Site = pub.GetCurrentSite();
        if (MyLimit.AddPromotionLimit(entity, pub.CreateUserPrivilege("33713ac1-24a8-40af-b122-b60c1109f347")))
        {
            entity = null;
            entity = MyLimit.GetLastPromotionLimit(pub.CreateUserPrivilege("22d21441-155a-4dc5-aec6-dcf5bdedd5cf"));
            if (entity.Promotion_Limit_ProductID == product_id)
            {
                //添加适用会员等级
                PromotionLimitMemberGradeInfo limitgrade;
                if (favor_gradeid.Length == 0)
                {
                    limitgrade = new PromotionLimitMemberGradeInfo();
                    limitgrade.Promotion_Limit_MemberGrade_ID = 0;
                    limitgrade.Promotion_Limit_MemberGrade_Grade = 0;
                    limitgrade.Promotion_Limit_MemberGrade_LimitID = entity.Promotion_Limit_ID;
                    MyLimit.AddPromotionLimitMemberGrade(limitgrade);
                    limitgrade = null;
                }

            }
        }
        pub.Msg("positive", "操作成功", "操作成功", true, "Product_Limit_add.aspx");
    }

    public virtual void EditPromotionLimit()
    {

        int Promotion_Limit_ID = tools.CheckInt(Request.Form["Promotion_Limit_ID"]);
        int Promotion_Limit_GroupID = tools.NullInt(Session["supplier_id"]);
        int Promotion_Limit_ProductID = tools.CheckInt(Request.Form["product_id"]);
        double Promotion_Limit_Price = tools.CheckFloat(Request.Form["Promotion_Limit_Price"]);
        int Promotion_Limit_Amount = tools.CheckInt(Request.Form["Promotion_Limit_Amount"]);
        int Promotion_Limit_Limit = tools.CheckInt(Request.Form["Promotion_Limit_Limit"]);
        DateTime Promotion_Limit_Starttime = tools.NullDate(Request.Form["Promotion_Limit_Starttime"]);
        DateTime Promotion_Limit_Endtime = tools.NullDate(Request.Form["Promotion_Limit_Endtime"]);
        string favor_gradeid = tools.CheckStr(Request.Form["Member_Grade"]);
        if (tools.NullDate(Promotion_Limit_Starttime).Year < 1900 || tools.NullDate(Promotion_Limit_Endtime).Year < 1900)
        {
            pub.Msg("error", "错误信息", "时间设置错误", false, "{back}");
        }
        PromotionLimitInfo entity = MyLimit.GetPromotionLimitByID(Promotion_Limit_ID, pub.CreateUserPrivilege("22d21441-155a-4dc5-aec6-dcf5bdedd5cf"));
        if (entity != null)
        {
            entity.Promotion_Limit_ID = Promotion_Limit_ID;
            entity.Promotion_Limit_GroupID = Promotion_Limit_GroupID;
            entity.Promotion_Limit_ProductID = Promotion_Limit_ProductID;
            entity.Promotion_Limit_Price = Promotion_Limit_Price;
            entity.Promotion_Limit_Amount = Promotion_Limit_Amount;
            entity.Promotion_Limit_Limit = Promotion_Limit_Limit;
            entity.Promotion_Limit_Starttime = Promotion_Limit_Starttime;
            entity.Promotion_Limit_Endtime = Promotion_Limit_Endtime;
            entity.Promotion_Limit_Site = pub.GetCurrentSite();

            if (MyLimit.EditPromotionLimit(entity, pub.CreateUserPrivilege("34b7b99f-451c-4c0b-8da1-e3ba000891a8")))
            {
                MyLimit.DelPromotionLimitMemberGrade(entity.Promotion_Limit_ID);
                //添加适用会员等级
                PromotionLimitMemberGradeInfo limitgrade;
                if (favor_gradeid.Length == 0)
                {
                    limitgrade = new PromotionLimitMemberGradeInfo();
                    limitgrade.Promotion_Limit_MemberGrade_ID = 0;
                    limitgrade.Promotion_Limit_MemberGrade_Grade = 0;
                    limitgrade.Promotion_Limit_MemberGrade_LimitID = entity.Promotion_Limit_ID;
                    MyLimit.AddPromotionLimitMemberGrade(limitgrade);
                    limitgrade = null;
                }

                pub.Msg("positive", "操作成功", "操作成功", true, "Product_Limit_list.aspx");
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

    public virtual void DelPromotionLimit()
    {
        int Promotion_Limit_ID = tools.CheckInt(Request.QueryString["limit_ID"]);
        if (MyLimit.DelPromotionLimit(Promotion_Limit_ID, pub.CreateUserPrivilege("470c7741-f942-42df-9973-c36555c8d2e6")) > 0)
        {
            MyLimit.DelPromotionLimitMemberGrade(Promotion_Limit_ID);
            pub.Msg("positive", "操作成功", "操作成功", true, "Product_Limit_list.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual PromotionLimitInfo GetPromotionLimitByID(int cate_id)
    {
        return MyLimit.GetPromotionLimitByID(cate_id, pub.CreateUserPrivilege("22d21441-155a-4dc5-aec6-dcf5bdedd5cf"));
    }

    public string ShowProduct()
    {
        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("<table  width=\"973\" border=\"0\" cellspacing=\"2\" cellpadding=\"0\">");
        jsonBuilder.Append("    <tr>");
        jsonBuilder.Append("        <td class=\"name\" width=\"60\"><input type=\"button\" value=\"添加\" onclick=\"SelectProduct()\" class=\"bt_orange\"></td>");
        jsonBuilder.Append("        <td class=\"name\" width=\"60\">列表显示</td>");
        jsonBuilder.Append("        <td class=\"name\">商品编码</td>");
        jsonBuilder.Append("        <td class=\"name\">商品名称</td>");
        jsonBuilder.Append("        <td class=\"name\">规格</td>");
        jsonBuilder.Append("        <td class=\"name\">生产企业</td>");
        jsonBuilder.Append("        <td class=\"name\">本站价格</td>");
        jsonBuilder.Append("    </tr>");

        string strProid = tools.NullStr(Session["selected_productid"]);
        IList<ProductInfo> entitys = null;
        if (strProid != null && strProid.Length > 0)
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_SupplierID", "=", tools.NullInt(Session["supplier_id"]).ToString()));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_ID", "in", strProid));
            Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "DESC"));
            entitys = MyProduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        }

        if (entitys != null)
        {
            foreach (ProductInfo entity in entitys)
            {
                jsonBuilder.Append("    <tr>");
                jsonBuilder.Append("        <td><input type=\"hidden\" name=\"sltproductid\" value=\"" + entity.Product_ID + "\"><a href=\"javascript:product_del(" + entity.Product_ID + ");\"><img src=\"/images/btn_move.gif\" border=\"0\" alt=\"删除\" style=\"display:inline;\"></a></td>");

                if (entity.Product_IsListShow == 0)
                {
                    jsonBuilder.Append("<td align=\"center\"><input type=\"checkbox\" name=\"islistshow_" + entity.Product_ID + "\" value=\"1\"></td>");
                }
                else
                {
                    jsonBuilder.Append("<td align=\"center\"><input type=\"checkbox\" name=\"islistshow_" + entity.Product_ID + "\" value=\"1\" checked></td>");
                }

                jsonBuilder.Append("        <td align=\"left\">" + tools.NullStr(entity.Product_Code) + "</td>");
                jsonBuilder.Append("        <td align=\"left\">" + tools.NullStr(entity.Product_Name) + "</td>");
                jsonBuilder.Append("        <td align=\"center\">" + tools.NullStr(entity.Product_Spec) + "</td>");
                jsonBuilder.Append("        <td align=\"center\">" + tools.NullStr(entity.Product_Maker) + "</td>");
                jsonBuilder.Append("        <td align=\"center\">" + tools.NullDbl(entity.Product_Price) + "</td>");
                jsonBuilder.Append("    </tr>");
            }
        }
        entitys = null;
        jsonBuilder.Append("</table>");

        return jsonBuilder.ToString();
    }

    public string SelectProduct()
    {
        int i = 0;
        string keyword = tools.CheckStr(Request["keyword"]);
        if (keyword != "输入商品编码、商品名称进行搜索" && keyword != null)
        {
            keyword = keyword;
        }
        else
        {
            keyword = "";
        }
        int product_cate = tools.CheckInt(Request["product_cate"]);
        if (product_cate == 0) { product_cate = tools.CheckInt(Request["product_cate_parent"]); }

        string productSelected = tools.NullStr(Session["selected_productid"]);

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_SupplierID", "=", tools.NullInt(Session["supplier_id"]).ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.U_Product_Shipper", "=", "0"));

        if (product_cate > 0)
        {
            string subCates = Get_All_SubCate(product_cate);
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", subCates));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + subCates + ")"));
        }
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_Name", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_Code", "like", keyword));
        }

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));

        if (productSelected.Length > 0)
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "not in", productSelected));

        Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "DESC"));

        IList<ProductInfo> entitys = null;
        if (Query.ParamInfos.Count > 5)
        {
            entitys = MyProduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
        }

        StringBuilder jsonBuilder = new StringBuilder();
        jsonBuilder.Append("<form method=\"post\"  id=\"frmadd\" name=\"frmadd\">");
        jsonBuilder.Append("<table width=\"973\" border=\"0\" cellspacing=\"2\" cellpadding=\"0\">");
        jsonBuilder.Append("    <tr>");
        jsonBuilder.Append("        <td class=\"name\" width=\"30\"></td>");
        jsonBuilder.Append("        <td class=\"name\" width=\"80\">库存</td>");
        jsonBuilder.Append("        <td class=\"name\">商品编码</td>");
        jsonBuilder.Append("        <td class=\"name\">商品名称</td>");
        jsonBuilder.Append("        <td class=\"name\">规格</td>");
        jsonBuilder.Append("        <td class=\"name\">生产企业</td>");
        jsonBuilder.Append("        <td class=\"name\">本站价格</td>");
        jsonBuilder.Append("    </tr>");

        if (entitys != null)
        {
            foreach (ProductInfo entity in entitys)
            {
                i++;

                if (i % 2 == 0)
                {
                    jsonBuilder.Append("    <tr>");
                }
                else
                {
                    jsonBuilder.Append("    <tr class=\"bg\">");
                }

                jsonBuilder.Append("        <td><input type=\"checkbox\" id=\"product_id\" name=\"product_id\" value=\"" + entity.Product_ID + "\" /></td>");
                jsonBuilder.Append("        <td>" + entity.Product_SaleAmount + "</td>");
                jsonBuilder.Append("        <td>" + entity.Product_Code + "</td>");
                jsonBuilder.Append("        <td align=\"left\">" + tools.NullStr(entity.Product_Name) + "</td>");
                jsonBuilder.Append("        <td align=\"center\">" + tools.NullStr(entity.Product_Spec) + "</td>");
                jsonBuilder.Append("        <td align=\"center\">" + tools.NullStr(entity.Product_Maker) + "</td>");
                jsonBuilder.Append("        <td align=\"center\">" + tools.NullDbl(entity.Product_Price) + "</td>");
                jsonBuilder.Append("    </tr>");
            }
            entitys = null;
        }
        else
        {
            jsonBuilder.Append("<tr class=\"list_td_bg\"><td colspan=\"7\">选择分类或输入关键词搜索</td></tr>");
        }
        jsonBuilder.Append("    <tr class=\"list_td_bg\">");
        jsonBuilder.Append("        <td><input type=\"checkbox\" id=\"checkbox\" name=\"chkall\" onclick=\"javascript:CheckAll(this.form)\" /></td>");
        jsonBuilder.Append("        <td colspan=\"6\" align=\"left\"><input type=\"button\" name=\"btn_ok\" value=\"确定\" class=\"bt_orange\" onclick=\"javascript:product_add('product_id');\" /></td>");
        jsonBuilder.Append("    </tr>");
        jsonBuilder.Append("</table>");
        jsonBuilder.Append("</form>");

        return jsonBuilder.ToString();
    }

    #endregion

    #region "虚拟账户"

    //会员积分消费
    public void Supplier_Coin_AddConsume(int coin_amount, string coin_reason, int supplier_id, bool is_return)
    {
        int Supplier_CoinRemain = 0;
        SupplierInfo supplierinfo = GetSupplierByID(supplier_id);
        if (supplierinfo != null)
        {
            Supplier_CoinRemain = supplierinfo.Supplier_CoinRemain;
            SupplierConsumptionInfo consumption = new SupplierConsumptionInfo();
            consumption.Consump_ID = 0;
            consumption.Consump_SupplierID = supplier_id;
            consumption.Consump_Coin = coin_amount;
            consumption.Consump_CoinRemain = Supplier_CoinRemain + coin_amount;
            consumption.Consump_Reason = coin_reason;
            consumption.Consump_Addtime = DateTime.Now;

            MyConsumption.AddSupplierConsumption(consumption);

            if (coin_amount > 0)
            {
                if (is_return)
                {
                    supplierinfo.Supplier_CoinRemain = Supplier_CoinRemain + coin_amount;
                }
                else
                {
                    supplierinfo.Supplier_CoinRemain = Supplier_CoinRemain + coin_amount;
                    supplierinfo.Supplier_CoinCount = supplierinfo.Supplier_CoinCount + coin_amount;
                }
            }
            else
            {
                supplierinfo.Supplier_CoinRemain = Supplier_CoinRemain + coin_amount;
            }

            MyBLL.EditSupplier(supplierinfo, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f"));
        }
    }

    /// <summary>
    /// 查询当前积分余额
    /// </summary>
    /// <returns></returns>
    public int Get_SupplierCoin()
    {
        int account_value = 0;
        try
        {
            SupplierInfo entity = GetSupplierByID();

            if (entity != null)
                account_value = entity.Supplier_CoinRemain;
        }
        catch (Exception ex) { throw ex; }
        return account_value;
    }

    //积分明细
    public void Supplier_Coin_List(string action, string date_start, string date_end)
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        string BgColor = "";
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));

        if (action == "history")
        {
            Pageurl = "?action=" + action + "&date_start=" + date_start + "&date_end=" + date_end;
        }
        else
        {
            Pageurl = "?action=" + action;
        }

        if (curpage < 1)
        {
            curpage = 1;
        }


        Response.Write("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"border:1px solid #dadada;\" class=\"pingjia\">");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <td width=\"150\" align=\"center\" valign=\"middle\">时间</td>");
        Response.Write("  <td width=\"88\" align=\"center\" valign=\"middle\" >收入</td>");
        Response.Write("  <td width=\"88\" align=\"center\" valign=\"middle\">支出</td>");
        Response.Write("  <td width=\"110\" align=\"center\" valign=\"middle\">" + Application["Coin_Name"].ToString() + "余额</td>");
        Response.Write("  <td align=\"center\" valign=\"middle\">备注</td>");
        Response.Write("</tr>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 8;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierConsumptionInfo.Consump_SupplierID", "=", supplier_id.ToString()));
        if (date_start != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{SupplierConsumptionInfo.Consump_Addtime}, '" + tools.NullDate(date_start, DateTime.Now.ToShortDateString()) + "')", "<=", "0"));
        }
        if (date_end != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{SupplierConsumptionInfo.Consump_Addtime}, '" + tools.NullDate(date_end, DateTime.Now.ToShortDateString()) + "')", ">=", "0"));
        }
        Query.OrderInfos.Add(new OrderInfo("SupplierConsumptionInfo.Consump_ID", "Desc"));
        IList<SupplierConsumptionInfo> consumptions = MyConsumption.GetSupplierConsumptions(Query);
        PageInfo page = MyConsumption.GetPageInfo(Query);
        if (consumptions != null)
        {
            foreach (SupplierConsumptionInfo entity in consumptions)
            {
                if (BgColor == "#FFFFFF")
                {
                    BgColor = "#FFFFFF";
                }
                else
                {
                    BgColor = "#FFFFFF";
                }

                Response.Write("<tr bgcolor=\"" + BgColor + "\">");
                Response.Write("  <td width=\"150\" height=\"35\" align=\"center\" valign=\"middle\">" + entity.Consump_Addtime + "</td>");
                if (entity.Consump_Coin > 0)
                {
                    Response.Write("  <td width=\"88\" height=\"35\" align=\"center\" valign=\"middle\">" + entity.Consump_Coin + "</td>");
                    Response.Write("  <td width=\"88\" height=\"35\" align=\"center\" valign=\"middle\">&nbsp;</td>");
                }
                else
                {
                    Response.Write("  <td width=\"88\" height=\"35\" align=\"center\" valign=\"middle\">&nbsp;</td>");
                    Response.Write("  <td width=\"88\" height=\"35\" align=\"center\" valign=\"middle\">" + entity.Consump_Coin + "</td>");
                }

                Response.Write("  <td width=\"110\" height=\"35\" align=\"center\" valign=\"middle\">" + entity.Consump_CoinRemain + " <img src=\"/Images/icon_coin.gif\" width=\"16\" height=\"16\" align=\"absbottom\" /></td>");
                Response.Write("  <td height=\"35\" align=\"center\" valign=\"middle\">" + entity.Consump_Reason + "</td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            Response.Write("<tr><td height=\"8\"></td></tr>");
            Response.Write("<tr><td align=\"right\"><div class=\"yz_b14_info_right yz_b14_info_right_2\" style=\"float:right;\">");
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
            Response.Write("</div></td></tr>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#FFFFFF\">");
            Response.Write("<td colspan=\"5\" align=\"center\" height=\"35\">暂无记录</td>");
            Response.Write("</tr>");
        }
        Response.Write("</table>");
    }

    //会员费明细
    public void Get_Account_Log_List(int Account_Type, string action, string date_start, string date_end)
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);

        string Pageurl;
        int i = 0;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        string BgColor = "";
        if (action == "history")
        {
            Pageurl = "?action=" + action;
        }
        else
        {
            Pageurl = "?action=" + action + "&date_start=" + date_start + "&date_end=" + date_end;
        }

        if (curpage < 1)
        {
            curpage = 1;
        }
 
       Response.Write("<table  width=\"975\" border=\"0\" cellspacing=\"2\" cellpadding=\"0\" class=\"table02\">");
        Response.Write("<tr>");
        Response.Write("  <td width=\"164\" class=\"name\">订单号</td>");
        Response.Write("  <td width=\"110\" class=\"name\">参加时间</td>");
        Response.Write("  <td width=\"106\" class=\"name\">保证金金额</td>");
        Response.Write("  <td width=\"361\" class=\"name\">参加会员</td>");
        Response.Write("  <td width=\"220\" class=\"name\">操作</td>");
        Response.Write("</tr>");
        QueryInfo Query1 = new QueryInfo();
        Query1.PageSize = 0;
        Query1.CurrentPage = 0;
        Query1.ParamInfos.Add(new ParamInfo("AND", "int", "BidInfo.Bid_MemberID", "=", supplier_id.ToString()));
        IList<BidInfo> bidinfo = MyBid.GetBids(Query1, pub.CreateUserPrivilege("32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2"));
      QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 0;
        if (bidinfo == null)
        {
            pub.Msg("error", "错误信息", "系统错误请稍后重试", false, "{back}");
        }
        foreach (var item in bidinfo)
        {
            Query.ParamInfos.Add(new ParamInfo("OR", "int", "TenderInfo.Tender_SupplierID", "=", item.Bid_SupplierID.ToString()));

        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_IsWin", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "TenderInfo.Tender_Status", "=", "0"));
        IList<TenderInfo> entitys = MyTender.GetTenders(Query);

        //QueryInfo Query = new QueryInfo();
        //Query.PageSize = 10;
        //Query.CurrentPage = curpage;
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierAccountLogInfo.Account_Log_SupplierID", "=", supplier_id.ToString()));
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierAccountLogInfo.Account_Log_Type", "=", Account_Type.ToString()));
        //if (date_start != "")
        //{
        //    Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{SupplierAccountLogInfo.Account_Log_Addtime}, '" + tools.NullDate(date_start) + "')", "<=", "0"));
        //}
        //if (date_end != "")
        //{
        //    Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{SupplierAccountLogInfo.Account_Log_Addtime}, '" + tools.NullDate(date_end) + "')", ">=", "0"));
        //}
        //Query.OrderInfos.Add(new OrderInfo("SupplierAccountLogInfo.Account_Log_ID", "Desc"));
        //IList<SupplierAccountLogInfo> entitys = MyAccountLog.GetSupplierAccountLogs(Query);
        //PageInfo page = MyAccountLog.GetPageInfo(Query);
        if (entitys!=null)
        {

        
        if (entitys.Where(p => p.Tender_Status == 0) != null)
        {
            foreach (TenderInfo entity in entitys.Where(p => p.Tender_Status == 0))
            {
                i++;
                if (i % 2 == 0)
                {
                    BgColor = " class=\"bg\" ";
                }
                else
                {
                    BgColor = "";
                }

                Response.Write("<tr " + BgColor + ">");
                Response.Write("  <td >" + entity.Tender_SN + "</td>");

           
                    Response.Write("  <td>" + entity.Tender_Addtime.ToString("yyyy-MM-dd") + "</td>");
                    //Response.Write("  <td>&nbsp;</td>");
              

                Response.Write("  <td>" + pub.FormatCurrency(entity.Tender_AllPrice) + "</td>");

                Response.Write("  <td >" + GetSupplierByID(entity.Tender_SupplierID).Supplier_CompanyName + "</td>");
                Response.Write("  <td><a href=\"/pay/pay_do_new.aspx?action=refunddeposit&&Tender_ID="+entity.Tender_ID+"\">退款</a></td>");


                Response.Write("</tr>");
            }
            Response.Write("</table>");

            //pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
        }
        }
        else
        {
            Response.Write("<table  width=\"973\" border=\"0\" cellspacing=\"2\" cellpadding=\"0\" class=\"table02\">");
            Response.Write("<tr>");
            Response.Write("<td colspan=\"5\">暂无记录</td>");
            Response.Write("</tr>");
            Response.Write("</table>");
        }


    }
    /// <summary>
    /// 退还保证金
    /// </summary>
    public void RefundDeposit()
    {
        string strResult = string.Empty;
        int Tender_ID = tools.CheckInt(Request["Tender_ID"].ToString());
        if (Tender_ID == 0)
        {
            pub.Msg("error", "错误信息", "系统错误请稍后重试", false, "{back}");
        }
        TenderInfo tenderinfo = MyTender.GetTenderByID(Tender_ID);
        BidInfo bidinfo = MyBid.GetBidByID(tenderinfo.Tender_BidID, pub.CreateUserPrivilege("32aa37b4-916e-4ffd-81cb-aaa27fc3aaa2"));
        if (tenderinfo!=null)
        {
            SupplierInfo supplierinfo = GetSupplierByID(tenderinfo.Tender_SupplierID);
            if (supplierinfo!=null)
            {
                ZhongXinInfo PayAccountInfo = new ZhongXin().GetZhongXinBySuppleir(tenderinfo.Tender_SupplierID);

                if (PayAccountInfo != null && bidinfo!=null)
                {
                    if (sendmessages.Transfer(PayAccountInfo.SubAccount, bidguaranteeaccno, bidguaranteeaccnm, "商家:" + supplierinfo.Supplier_CompanyName + "完成订单,退回" + bidinfo.Bid_Bond + "元保证金", bidinfo.Bid_Bond, ref strResult, supplierinfo.Supplier_CompanyName))
                    {
                        tenderinfo.Tender_Status = 2;
                        if (MyTender.EditTender(tenderinfo))
                        {        
                        Supplier_Account_Log(supplierinfo.Supplier_ID, 1, bidinfo.Bid_Bond, "报名" + bidinfo.Bid_Title+ bidinfo.Bid_Type == "0" ? "招标项目.返还保证金": "拍卖项目.返还保证金");
                        pub.Msg("info", "系统消息", "退款成功", false, "{back}");
                        }
                        else
                        {
                            
                                pub.Msg("error", "错误信息", "系统错误请稍后重试", false, "{back}");
                            
                        }
                    }
                }
                else
                {
                    pub.Msg("error", "错误信息", "系统错误请稍后重试", false, "{back}");
                }
            }
            else
            {
                pub.Msg("error", "错误信息", "系统错误请稍后重试", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "系统错误请稍后重试", false, "{back}");
        }

    }

    //信用额度明细
    public void Supplier_CreditLimit_List(string action, string date_start, string date_end)
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        string BgColor = "";
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));

        if (action == "history")
        {
            Pageurl = "?action=" + action + "&date_start=" + date_start + "&date_end=" + date_end;
        }
        else
        {
            Pageurl = "?action=" + action;
        }

        if (curpage < 1)
        {
            curpage = 1;
        }
        SupplierInfo supplierinfo = GetSupplierByID();
        if (supplierinfo == null)
        {
            Response.Redirect("/supplier/index.aspx");
        }

        Response.Write("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"border:1px solid #dadada;\" class=\"pingjia\">");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");


        if (action == "payable")
        {
            Response.Write("  <td width=\"120\" align=\"center\" valign=\"middle\">支付时间</td>");
            Response.Write("  <td width=\"120\" align=\"center\" valign=\"middle\">还款期限</td>");
            Response.Write("  <td width=\"80\" align=\"center\" valign=\"middle\">信用类型</td>");
            Response.Write("  <td width=\"88\" align=\"center\" valign=\"middle\">支付金额</td>");
        }
        else
        {
            Response.Write("  <td width=\"120\" align=\"center\" valign=\"middle\">时间</td>");
            Response.Write("  <td width=\"80\" align=\"center\" valign=\"middle\">信用类型</td>");
            Response.Write("  <td width=\"88\" align=\"center\" valign=\"middle\" >收入</td>");
            Response.Write("  <td width=\"88\" align=\"center\" valign=\"middle\">支出</td>");
        }
        Response.Write("  <td width=\"110\" align=\"center\" valign=\"middle\">余额</td>");
        Response.Write("  <td align=\"center\" valign=\"middle\">备注</td>");
        if (action == "payable")
        {
            Response.Write("  <td width=\"80\" align=\"center\" valign=\"middle\">操作</td>");
        }
        Response.Write("</tr>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 8;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCreditLimitLogInfo.CreditLimit_Log_SupplierID", "=", supplier_id.ToString()));
        if (date_start != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{SupplierCreditLimitLogInfo.CreditLimit_Log_Addtime}, '" + tools.NullDate(date_start, DateTime.Now.ToShortDateString()) + "')", "<=", "0"));
        }
        if (date_end != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{SupplierCreditLimitLogInfo.CreditLimit_Log_Addtime}, '" + tools.NullDate(date_end, DateTime.Now.ToShortDateString()) + "')", ">=", "0"));
        }
        if (action == "payable")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCreditLimitLogInfo.CreditLimit_Log_Amount", "<", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCreditLimitLogInfo.CreditLimit_Log_PaymentStatus", "=", "0"));
        }
        Query.OrderInfos.Add(new OrderInfo("SupplierCreditLimitLogInfo.CreditLimit_Log_ID", "Desc"));
        IList<SupplierCreditLimitLogInfo> consumptions = myCreditLimitLog.GetSupplierCreditLimitLogs(Query);
        PageInfo page = myCreditLimitLog.GetPageInfo(Query);
        if (consumptions != null)
        {
            foreach (SupplierCreditLimitLogInfo entity in consumptions)
            {
                if (BgColor == "#FFFFFF")
                {
                    BgColor = "#FFFFFF";
                }
                else
                {
                    BgColor = "#FFFFFF";
                }

                Response.Write("<tr bgcolor=\"" + BgColor + "\">");
                Response.Write("  <td width=\"120\" height=\"35\" align=\"center\" valign=\"middle\">" + entity.CreditLimit_Log_Addtime + "</td>");

                if (entity.CreditLimit_Log_Type == 0)
                {
                    if (action == "payable")
                    {
                        Response.Write("  <td width=\"120\" height=\"35\" align=\"center\" valign=\"middle\">" + entity.CreditLimit_Log_Addtime.AddDays(supplierinfo.Supplier_CreditLimit_Expires) + "</td>");
                    }
                    Response.Write("  <td width=\"80\" height=\"35\" align=\"center\" valign=\"middle\">固定信用</td>");
                }
                else
                {
                    if (action == "payable")
                    {
                        Response.Write("  <td width=\"120\" height=\"35\" align=\"center\" valign=\"middle\">" + entity.CreditLimit_Log_Addtime.AddDays(supplierinfo.Supplier_TempCreditLimit_Expires) + "</td>");
                    }
                    Response.Write("  <td width=\"80\" height=\"35\" align=\"center\" valign=\"middle\">临时信用</td>");
                }
                if (action == "payable")
                {
                    Response.Write("  <td width=\"88\" height=\"35\" align=\"center\" valign=\"middle\">" + (0 - entity.CreditLimit_Log_Amount) + "</td>");
                }
                else
                {
                    if (entity.CreditLimit_Log_Amount > 0)
                    {
                        Response.Write("  <td width=\"88\" height=\"35\" align=\"center\" valign=\"middle\">" + entity.CreditLimit_Log_Amount + "</td>");
                        Response.Write("  <td width=\"88\" height=\"35\" align=\"center\" valign=\"middle\">&nbsp;</td>");
                    }
                    else
                    {
                        Response.Write("  <td width=\"88\" height=\"35\" align=\"center\" valign=\"middle\">&nbsp;</td>");
                        Response.Write("  <td width=\"88\" height=\"35\" align=\"center\" valign=\"middle\">" + entity.CreditLimit_Log_Amount + "</td>");
                    }
                }

                Response.Write("  <td width=\"110\" height=\"35\" align=\"center\" valign=\"middle\">" + entity.CreditLimit_Log_AmountRemain + " <img src=\"/Images/icon_coin.gif\" width=\"16\" height=\"16\" align=\"absbottom\" /></td>");
                Response.Write("  <td height=\"35\" align=\"center\" valign=\"middle\">" + entity.CreditLimit_Log_Note + "</td>");
                if (action == "payable")
                {
                    Response.Write("  <td width=\"80\" align=\"center\" valign=\"middle\"><a href=\"account_payable_back.aspx?log=" + entity.CreditLimit_Log_ID + "\" class=\"a_t12_blue\">支付</a></td>");
                }
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            Response.Write("<tr><td height=\"8\"></td></tr>");
            Response.Write("<tr><td align=\"right\"><div class=\"yz_b14_info_right yz_b14_info_right_2\" style=\"float:right;\">");
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
            Response.Write("</div></td></tr>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#FFFFFF\">");
            if (action == "payable")
            {
                Response.Write("<td colspan=\"7\" align=\"center\" height=\"35\">暂无记录</td>");
            }
            else
            {
                Response.Write("<td colspan=\"5\" align=\"center\" height=\"35\">暂无记录</td>");
            }
            Response.Write("</tr>");
        }
        Response.Write("</table>");
    }

    //应付账款汇总
    public double Supplier_CreditLimit_Count(string action)
    {
        double amount = 0;
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCreditLimitLogInfo.CreditLimit_Log_SupplierID", "=", supplier_id.ToString()));
        if (action == "payable")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCreditLimitLogInfo.CreditLimit_Log_Amount", "<", "0"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierCreditLimitLogInfo.CreditLimit_Log_PaymentStatus", "=", "0"));
        }
        Query.OrderInfos.Add(new OrderInfo("SupplierCreditLimitLogInfo.CreditLimit_Log_ID", "Desc"));
        IList<SupplierCreditLimitLogInfo> consumptions = myCreditLimitLog.GetSupplierCreditLimitLogs(Query);
        if (consumptions != null)
        {
            foreach (SupplierCreditLimitLogInfo entity in consumptions)
            {
                amount += entity.CreditLimit_Log_Amount;
            }
        }
        return amount;
    }

    public void Supplier_CreditLimit_Back()
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        int log_id = tools.CheckInt(Request["log_id"]);
        int pay_type = tools.CheckInt(Request["pay_type"]);
        SupplierCreditLimitLogInfo loginfo = GetSupplierCreditLimitLogByID(log_id);
        if (loginfo != null)
        {
            if (loginfo.CreditLimit_Log_Amount > 0 || (loginfo.CreditLimit_Log_PaymentStatus == 1))
            {
                log_id = 0;
            }
            if (loginfo.CreditLimit_Log_SupplierID != tools.NullInt(Session["supplier_id"]))
            {
                pub.Msg("error", "错误信息", "无法执行此操作", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "无法执行此操作", false, "{back}");
        }
        if (pay_type == 1)
        {
            SupplierInfo supplierinfo = GetSupplierByID();
            if (supplierinfo != null)
            {
                if (supplierinfo.Supplier_Account < (0 - loginfo.CreditLimit_Log_Amount))
                {
                    pub.Msg("error", "错误信息", "您的现金账户不足以支付该笔账款", false, "{back}");
                }

                loginfo.CreditLimit_Log_PaymentStatus = 1;
                if (myCreditLimitLog.EditSupplierCreditLimitLog(loginfo))
                {
                    //现金扣除
                    Supplier_Account_Log(tools.NullInt(Session["supplier_id"]), 0, loginfo.CreditLimit_Log_Amount, "支付应付账款，账款日期：" + loginfo.CreditLimit_Log_Addtime + "；金额：" + pub.FormatCurrency(0 - loginfo.CreditLimit_Log_Amount));

                    //信用额度恢复
                    Supplier_CreditLimit_Log(tools.NullInt(Session["supplier_id"]), loginfo.CreditLimit_Log_Type, (0 - loginfo.CreditLimit_Log_Amount), "使用现金账户支付补充【" + loginfo.CreditLimit_Log_Addtime + "】账款");
                    pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/Accounts_payable_list.aspx");
                }
                else
                {
                    pub.Msg("error", "操作失败", "操作失败，请稍后再试", false, "{back}");
                }
            }
        }
    }

    //会员虚拟账户操作日志
    public void Supplier_Account_Log(int Supplier_ID, int Account_Type, double Amount, string Log_note)
    {
        double Member_AccountRemain = 0;
        SupplierInfo supplierinfo = GetSupplierByID(Supplier_ID);
        if (supplierinfo != null)
        {
            if (Account_Type == 0)
            {
                Member_AccountRemain = supplierinfo.Supplier_Account;
            }
            else if (Account_Type == 1)
            {
                Member_AccountRemain = supplierinfo.Supplier_Security_Account;
            }
            else
            {
                Member_AccountRemain = supplierinfo.Supplier_Adv_Account;
            }





            SupplierAccountLogInfo accountLog = new SupplierAccountLogInfo();
            accountLog.Account_Log_ID = 0;
            accountLog.Account_Log_Type = Account_Type;
            accountLog.Account_Log_SupplierID = Supplier_ID;
            accountLog.Account_Log_Amount = Amount;
            accountLog.Account_Log_AmountRemain = Member_AccountRemain + Amount;
            accountLog.Account_Log_Note = Log_note;
            accountLog.Account_Log_Addtime = DateTime.Now;

            MyAccountLog.AddSupplierAccountLog(accountLog);

            if (Amount != 0)
            {
                if (Account_Type == 0)
                {
                    supplierinfo.Supplier_Account = Member_AccountRemain + Amount;
                }
                else if (Account_Type == 1)
                {
                    supplierinfo.Supplier_Security_Account = Member_AccountRemain + Amount;
                }
                else
                {
                    supplierinfo.Supplier_Adv_Account = Member_AccountRemain + Amount;
                }
            }
            if ((Member_AccountRemain + Amount) >= 0)
            {
                MyBLL.EditSupplier(supplierinfo, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f"));
            }

        }
    }

    public void Supplier_Account_Add()
    {
        double Account_Amount = tools.NullDbl(Request["Account_Amount"]);
        string verifycode = tools.CheckStr(Request["verifycode"]);
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        int Account_Type = tools.CheckInt(Request["Account_Type"]);
        string pay_type = tools.CheckStr(Request["pay_type"]);
        if (verifycode != Session["Trade_Verify"].ToString() && verifycode.Length == 0)
        {
            pub.Msg("error", "错误", "验证码输入错误", false, "{back}");
        }
        if (Account_Amount <= 0)
        {
            pub.Msg("error", "错误", "充值金额必须大于等于0", false, "{back}");
        }
        if (supplier_id <= 0)
        {
            pub.Msg("error", "错误", "充值失败！请稍后重试！", false, "{back}");
        }
        MemberAccountOrdersInfo entity = new MemberAccountOrdersInfo();
        entity.Account_Orders_MemberID = 0;
        entity.Account_Orders_AccountType = Account_Type;
        entity.Account_Orders_SN = account_orders_sn();
        entity.Account_Orders_SupplierID = supplier_id;
        entity.Account_Orders_Amount = Account_Amount;
        entity.Account_Orders_Status = 0;
        entity.Account_Orders_Addtime = DateTime.Now;
        if (MyMEMAccountLog.AddMemberAccountOrders(entity))
        {
            Response.Redirect("/pay/pay_account.aspx?sn=" + entity.Account_Orders_SN + "&pay_type=" + pay_type + "&action=account_add");
        }
        else
        {
            pub.Msg("error", "错误", "充值失败！请稍后重试！", false, "{back}");
        }
    }

    //中信保证金充值
    public void Supplier_Account_Add_new()
    {
        ZhongXin mycredit = new ZhongXin();
        double Account_Amount = tools.NullDbl(Request["Account_Amount"]);
        string verifycode = tools.CheckStr(Request["verifycode"]);
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        int Account_Type = tools.CheckInt(Request["Account_Type"]);
        string pay_type = tools.CheckStr(Request["pay_type"]);
        if (verifycode != Session["Trade_Verify"].ToString())
        {
            pub.Msg("error", "错误", "验证码输入错误", false, "{back}");
        }
        if (Account_Amount <= 0)
        {
            pub.Msg("error", "错误", "充值金额必须大于等于0", false, "{back}");
        }
        if (supplier_id <= 0)
        {
            pub.Msg("error", "错误", "充值失败！请稍后重试！", false, "{back}");
        }
        MemberAccountOrdersInfo entity = new MemberAccountOrdersInfo();
        entity.Account_Orders_MemberID = 0;
        entity.Account_Orders_AccountType = Account_Type;
        entity.Account_Orders_SN = account_orders_sn();
        entity.Account_Orders_SupplierID = supplier_id;
        entity.Account_Orders_Amount = Account_Amount;
        entity.Account_Orders_Status = 0;
        entity.Account_Orders_Addtime = DateTime.Now;
        if (MyMEMAccountLog.AddMemberAccountOrders(entity))
        {
            string supplier_name = "";
            SupplierInfo supplierinfo = GetSupplierByID(supplier_id);
            if (supplierinfo != null)
            {
                supplier_name = supplierinfo.Supplier_CompanyName;
            }
            ZhongXinInfo PayAccountInfo = mycredit.GetZhongXinBySuppleir(supplier_id);
            if (PayAccountInfo != null)
            {
                string strResult = string.Empty;

                //商家账号充值到商家保证金账户里
                if (sendmessages.Transfer(PayAccountInfo.SubAccount, merchantguaranteeaccno, merchantguaranteeaccnm, "商家:" + supplier_name + ",充值了" + Account_Amount + "元保证金", Account_Amount, ref strResult, supplier_name))
                {
                    //SupplierInfo supplierinfo1 = GetSupplierByID(supplier_id);
                    //新加字段 商家保证金如果已经交过,则其状态设为1 
                    supplierinfo.Supplier_MerchantMar_Status = 1;
                    MyBLL.EditSupplier(supplierinfo, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f"));
                    pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/supplier_margin_account_Recharge.aspx");
                }
                else
                {
                    //SupplierInfo supplierinfo1 = GetSupplierByID(supplier_id);
                    //新加字段 商家保证金如果已经交过,则其状态设为1 
                    //supplierinfo1.Supplier_MerchantMar_Status = 1;
                    //supplierinfo1.Supplier_SealImg = "1";
                    //MyBLL.EditSupplier(supplierinfo1, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f"));

                    pub.Msg("error", "错误", "" + strResult + "", false, "{back}");
                }
            }
            else
            {
                pub.Msg("error", "错误", "充值失败！请稍后重试！", false, "{back}");
            }

        }
        else
        {
            pub.Msg("error", "错误", "充值失败！请稍后重试！", false, "{back}");
        }
    }

    //生成订单号
    public string account_orders_sn()
    {
        string sn = "";
        int recordcount = 0;
        string count = "";
        bool ismatch = true;
        MemberAccountOrdersInfo ordersinfo = null;
        sn = "CZ" + tools.FormatDate(DateTime.Now, "yyMMdd") + pub.Createvkey(5);
        while (ismatch == true)
        {
            ordersinfo = MyMEMAccountLog.GetMemberAccountOrdersByOrdersSN(sn);
            if (ordersinfo != null)
            {
                sn = "CZ" + tools.FormatDate(DateTime.Now, "yyMMdd") + pub.Createvkey(5);
            }
            else
            {
                ismatch = false;
            }
        }


        return sn;
    }

    /// <summary>
    /// 广告服务费用
    /// </summary>
    /// <param name="Supplier_ID"></param>
    /// <returns></returns>
    public double GetSupplierAdvAccount(int Supplier_ID)
    {
        double CanMoney = 0;
        SupplierInfo SupplierEntity = GetSupplierByID(Supplier_ID);

        if (SupplierEntity != null)
            CanMoney = SupplierEntity.Supplier_Adv_Account;

        return CanMoney;
    }


    public SupplierMarginInfo GetSupplierMarginByType(int type)
    {
        return MyMargin.GetSupplierMarginByTypeID(type);
    }

    #endregion

    #region "邮件处理"


    //邮件模版
    public string mail_template(string template_name, string member_email, string member_password, string member_verifycode)
    {
        string mailbody = "";
        switch (template_name)
        {
            case "emailverify":
                mailbody = "<p>欢迎您注册{sys_config_site_name}！请点击下面的链接进行验证。</p>";
                mailbody = mailbody + "<p><a href=\"{sys_config_site_url}/supplier/register_do.aspx?action=emailverify&VerifyCode={member_verifycode}\" target=\"_blank\">{sys_config_site_url}/supplier/register_do.aspx?action=emailverify&VerifyCode={member_verifycode}</a></p>";
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
                mailbody = mailbody + "<p><a href=\"{sys_config_site_url}/supplier/login_do.aspx?action=verify&VerifyCode={member_verifycode}\" target=\"_blank\">{sys_config_site_url}/supplier/login_do.aspx?action=verify&VerifyCode={member_verifycode}</a></p>";
                mailbody = mailbody + "<p>如果链接无法点击，请将以上链接复制到浏览器地址栏中打开，即可重新设置密码！</p>";
                mailbody = mailbody + "<p>如果有任何疑问，欢迎<a href=\"{sys_config_site_url}/help/feedback.aspx\" target=\"_blank\">给我们留言</a>，我们将尽快给您回复！</p>";
                mailbody = mailbody + "<p><font color=red>为保证您正常接收邮件，建议您将此邮件地址加入到地址簿中。</font></p>";

                break;
            case "getpass_success":
                mailbody = "<p>您的密码已重新设置，请牢记新密码。</p>";
                mailbody = mailbody + "<p><strong><a href=\"{sys_config_site_url}/login.aspx?u_type=1\" target=\"_blank\">点击这里登录您的帐号</a></strong></p>";
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

    #region 广告推广设置

    public void Ad_Position_Select()
    {
        string Pageurl = "?action=list";
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        if (curpage < 1)
            curpage = 1;

        //Response.Write("<table class=\"commontable\">");
        //Response.Write("<tr>");
        Response.Write("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"border:1px solid #dadada;\" class=\"pingjia\">");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <th>广告位置</th>");
        Response.Write("  <th>高度(px)</th>");
        Response.Write("  <th>宽度(px)</th>");
        Response.Write("  <th>费用(元/天)</th>");
        Response.Write("  <th width=\"80\">操作</th>");
        Response.Write("</tr>");

        IADPosition AdPositionBLL = ADPositionFactory.CreateADPosition();

        string productURL = string.Empty;
        string checkstatus = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionInfo.Ad_Position_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionInfo.U_Ad_Position_Marketing", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("ADPositionInfo.Ad_Position_ID", "DESC"));

        string keyword = tools.CheckStr(Request["keyword"]);

        if (keyword != null && keyword.Length > 0)
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADPositionInfo.Ad_Position_Name", "%like%", keyword));

        RBACUserInfo UserInfo = pub.CreateUserPrivilege("d3aa1596-cc86-46c7-80f0-8bf6248ee31e");
        PageInfo page = AdPositionBLL.GetPageInfo(Query, UserInfo);
        IList<ADPositionInfo> entitys = AdPositionBLL.GetADPositions(Query, UserInfo);
        if (entitys != null)
        {
            foreach (ADPositionInfo entity in entitys)
            {
                Response.Write("<tr>");
                Response.Write("<td>" + entity.Ad_Position_Name + "</td>");
                Response.Write("<td>" + entity.Ad_Position_Width + "</td>");
                Response.Write("<td>" + entity.Ad_Position_Height + "</td>");
                Response.Write("<td>" + pub.FormatCurrency(entity.U_Ad_Position_Price) + "</td>");
                Response.Write("<td><a href=\"ad_apply.aspx?ad_position_id=" + entity.Ad_Position_ID + "\">申请</a></td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" align=\"center\" cellpadding=\"2\"><tr><td align=\"right\" height=\"10\"></td></tr><tr><td align=\"right\">");
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
            Response.Write("</td></tr></table>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#ffffff\"><td height=\"35\" colspan=\"5\" align=\"center\">没有记录</td></tr>");
        }
        Response.Write("</table>");
    }

    public void Ad_Apply()
    {
        int Ad_ID = tools.CheckInt(Request.Form["Ad_ID"]);
        string Ad_Title = tools.CheckStr(Request.Form["Ad_Title"]);
        string Ad_Kind = tools.CheckStr(Request.Form["Ad_Kind"]);
        int Ad_MediaKind = 2;
        string Ad_Media = tools.CheckStr(Request.Form["Ad_Media"]);
        string Ad_Mediacode = "";
        string Ad_Link = tools.CheckStr(Request.Form["Ad_Link"]);
        int Ad_Show_Freq = 1;
        int Ad_Show_times = 0;
        int Ad_Hits = 0;
        DateTime Ad_StartDate = tools.NullDate(Request.Form["Ad_StartDate"]);
        DateTime Ad_EndDate = tools.NullDate(Request.Form["Ad_EndDate"]);
        int Ad_IsContain = 0;
        string Ad_Propertys = "";
        int Ad_Sort = 1;
        int Ad_IsActive = 0;
        string Ad_Site = pub.GetCurrentSite();

        if (Ad_Title == "")
        {
            pub.Msg("error", "错误信息", "请填写广告名称", false, "{back}");
        }
        if (Ad_Kind == "")
        {
            pub.Msg("error", "错误信息", "请选择广告位置", false, "{back}");
        }
        if (Request.Form["Ad_EndDate"] == "" || Request.Form["Ad_EndDate"] == "")
        {
            pub.Msg("error", "错误信息", "请选择广告起止时间", false, "{back}");
        }
        if (Ad_Media == "")
        {
            pub.Msg("error", "错误信息", "请设置广告媒体", false, "{back}");
        }

        ADInfo entity = new ADInfo();
        entity.Ad_ID = Ad_ID;
        entity.Ad_Title = Ad_Title;
        entity.Ad_Kind = Ad_Kind;
        entity.Ad_MediaKind = Ad_MediaKind;
        entity.Ad_Media = Ad_Media;
        entity.Ad_Link = Ad_Link;
        entity.Ad_Show_Freq = Ad_Show_Freq;
        entity.Ad_Show_times = Ad_Show_times;
        entity.Ad_Hits = Ad_Hits;
        entity.Ad_StartDate = Ad_StartDate;
        entity.Ad_EndDate = Ad_EndDate;
        entity.Ad_IsContain = Ad_IsContain;
        entity.Ad_Propertys = Ad_Propertys;
        entity.Ad_Sort = Ad_Sort;
        entity.Ad_IsActive = Ad_IsActive;
        entity.Ad_Site = Ad_Site;
        entity.U_Ad_Advertiser = tools.NullInt(Session["supplier_id"]);
        entity.U_Ad_Audit = 0;
        entity.Ad_Addtime = DateTime.Now;



        ADPositionInfo AdPositionEntity = AdPositionBLL.GetAD_PositionByValue(entity.Ad_Kind, pub.CreateUserPrivilege("d3aa1596-cc86-46c7-80f0-8bf6248ee31e"));
        if (AdPositionEntity == null || AdPositionEntity.U_Ad_Position_Marketing != 1)
            Response.Redirect("ad_position_select.aspx");

        double DeductMoney = ((Ad_EndDate - Ad_StartDate).Days + 1) * AdPositionEntity.U_Ad_Position_Price;
        if (GetSupplierAdvAccount(entity.U_Ad_Advertiser) < DeductMoney)
            pub.Msg("error", "错误信息", "账户余额不足", false, "{back}");

        if (AdBLL.AddAD(entity, pub.CreateUserPrivilege("876d73fe-0893-41e7-b44b-062713a6b190")))
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "ad_apply_list.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public ADInfo GetADByID(int ID)
    {
        return AdBLL.GetADByID(ID, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
    }

    public void Ad_Apply_Del()
    {
        int AD_ID = tools.CheckInt(Request["ad_id"]);
        ADInfo entity = AdBLL.GetADByID(AD_ID, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
        if (entity != null)
        {
            if (entity.U_Ad_Advertiser == tools.NullInt(Session["supplier_id"]) && entity.U_Ad_Audit != 1)
            {
                AdBLL.DelAD(AD_ID, pub.CreateUserPrivilege("6087aa59-bd66-4eb5-8fb0-f72da294b1ae"));
                pub.Msg("positive", "操作成功", "操作成功", true, "ad_apply_list.aspx");
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

    public void Ad_Apply_List()
    {
        string Pageurl = "?action=list";
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        if (curpage < 1)
            curpage = 1;

        //Response.Write("<table class=\"commontable\">");
        //Response.Write("<tr>");
        Response.Write("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"border:1px solid #dadada;\" class=\"pingjia\">");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <th>广告名称</th>");
        Response.Write("  <th>广告位置</th>");
        Response.Write("  <th>广告类型</th>");
        Response.Write("  <th>展示次数</th>");
        Response.Write("  <th>点击次数</th>");
        Response.Write("  <th>起止时间</th>");
        Response.Write("  <th>状态</th>");
        Response.Write("  <th>操作</th>");
        Response.Write("</tr>");


        ADPositionInfo ADPositionEntity;

        string productURL = string.Empty;
        string checkstatus = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADInfo.Ad_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADInfo.U_Ad_Advertiser", "=", tools.NullInt(Session["supplier_id"]).ToString()));
        Query.OrderInfos.Add(new OrderInfo("ADInfo.Ad_ID", "DESC"));

        string keyword = tools.CheckStr(Request["keyword"]);

        if (keyword != null && keyword.Length > 0)
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADInfo.Ad_Title", "%like%", keyword));

        RBACUserInfo UserInfo = pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01");
        PageInfo page = AdBLL.GetPageInfo(Query, UserInfo);
        IList<ADInfo> entitys = AdBLL.GetADs(Query, UserInfo);
        if (entitys != null)
        {
            foreach (ADInfo entity in entitys)
            {
                Response.Write("<tr>");
                Response.Write("<td>" + entity.Ad_Title + "</td>");

                #region 显示位置

                ADPositionEntity = AdPositionBLL.GetAD_PositionByValue(entity.Ad_Kind, pub.CreateUserPrivilege("d3aa1596-cc86-46c7-80f0-8bf6248ee31e"));
                Response.Write("<td>");
                if (ADPositionEntity != null)
                    Response.Write(ADPositionEntity.Ad_Position_Name);
                Response.Write("</td>");

                #endregion

                #region 广告类型

                Response.Write("<td>");
                switch (entity.Ad_MediaKind)
                {
                    case 1:
                        Response.Write("文字");
                        break;
                    case 2:
                        Response.Write("图片");
                        break;
                    case 3:
                        Response.Write("Flash");
                        break;
                    default:
                        Response.Write("富媒体");
                        break;
                }
                Response.Write("</td>");

                #endregion

                Response.Write("<td>" + entity.Ad_Show_times + "</td>");
                Response.Write("<td>" + entity.Ad_Hits + "</td>");
                Response.Write("<td>" + entity.Ad_StartDate.ToShortDateString() + " - " + entity.Ad_EndDate.ToShortDateString() + "</td>");
                Response.Write("<td>");
                if (entity.U_Ad_Audit == 1)
                {
                    Response.Write("审核通过");
                }
                else if (entity.U_Ad_Audit == 2)
                {
                    Response.Write("审核失败");
                }
                else
                {
                    Response.Write("未处理");
                }
                Response.Write("</td>");

                Response.Write("<td>");
                Response.Write("<a href=\"ad_apply_view.aspx?ad_id=" + entity.Ad_ID + "\" class=\"a_t12_blue\">查看</a>");
                if (entity.U_Ad_Audit != 1)
                {
                    Response.Write(" <a href=\"ad_apply_do.aspx?action=remove&ad_id=" + entity.Ad_ID + "\" class=\"a_t12_blue\">删除</a>");
                }
                Response.Write("</td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" align=\"center\" cellpadding=\"2\"><tr><td align=\"right\" height=\"10\"></td></tr><tr><td align=\"right\">");
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
            Response.Write("</td></tr></table>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#ffffff\"><td height=\"35\" colspan=\"8\" align=\"center\">没有记录</td></tr>");
        }
        Response.Write("</table>");
    }

    /// <summary>
    /// 判断重复
    /// </summary>
    /// <param name="name"></param>
    /// <param name="productid"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool CheckKeywordBiddinRepeat(string name, int productid, int id)
    {
        int Keyword_ID = 0;
        KeywordBiddingKeywordInfo keywordinfo = MyKeywordBidding.GetKeywordBiddingKeywordByName(name, pub.CreateUserPrivilege("1b86dfa7-32e5-4136-b3d1-a8a670f415ff"));
        if (keywordinfo != null)
        {
            Keyword_ID = keywordinfo.Keyword_ID;
        }
        else
        {
            return false;
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "KeywordBiddingInfo.KeywordBidding_ID", "<>", id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "KeywordBiddingInfo.KeywordBidding_KeywordID", "=", Keyword_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "KeywordBiddingInfo.KeywordBidding_ProductID", "=", productid.ToString()));
        Query.OrderInfos.Add(new OrderInfo("KeywordBiddingInfo.KeywordBidding_ID", "Desc"));
        IList<KeywordBiddingInfo> entitys = MyKeywordBidding.GetKeywordBiddings(Query, pub.CreateUserPrivilege("445e8b4f-4b38-4e4c-9b0f-c69e0b1a6a71"));
        if (entitys != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void AddKeywordBidding()
    {
        int KeywordBidding_ID = tools.CheckInt(Request.Form["KeywordBidding_ID"]);
        int KeywordBidding_SupplierID = tools.NullInt(Session["supplier_id"]);
        int KeywordBidding_ProductID = tools.CheckInt(Request.Form["KeywordBidding_ProductID"]);
        string KeywordBidding_Name = tools.CheckStr(Request.Form["KeywordBidding_Name"]);
        double KeywordBidding_Price = tools.CheckFloat(Request.Form["KeywordBidding_Price"]);
        DateTime KeywordBidding_StartDate = tools.NullDate(Request.Form["KeywordBidding_StartDate"]);
        DateTime KeywordBidding_EndDate = tools.NullDate(Request.Form["KeywordBidding_EndDate"]);
        int KeywordBidding_ShowTimes = tools.CheckInt(Request.Form["KeywordBidding_ShowTimes"]);
        int KeywordBidding_Hits = tools.CheckInt(Request.Form["KeywordBidding_Hits"]);
        int KeywordBidding_Audit = tools.CheckInt(Request.Form["KeywordBidding_Audit"]);
        string KeywordBidding_Site = tools.CheckStr(Request.Form["KeywordBidding_Site"]);
        int KeywordBidding_KeywordID = 0;

        if (KeywordBidding_ProductID == 0)
            pub.Msg("error", "错误信息", "请先选择商品", false, "{back}");

        if (KeywordBidding_Name == null || KeywordBidding_Name.Length == 0)
            pub.Msg("error", "错误信息", "请填写竞价关键词", false, "{back}");

        if (KeywordBidding_Price == null || KeywordBidding_Price <= 0)
            pub.Msg("error", "错误信息", "请填写出价", false, "{back}");

        if (CheckKeywordBiddinRepeat(KeywordBidding_Name, KeywordBidding_ProductID, 0))
            pub.Msg("error", "错误信息", "已有该商品的关键词", false, "{back}");

        if (KeywordBidding_Price < CheckKeyword_Price(KeywordBidding_Name))
        {
            pub.Msg("error", "错误信息", "出价不可低于关键词起步价", false, "{back}");
        }

        if (KeywordBidding_StartDate.Year < 1900)
        {
            KeywordBidding_StartDate = DateTime.Now;
        }
        if (KeywordBidding_EndDate.Year < 1900)
        {
            KeywordBidding_EndDate = DateTime.Now;
        }
        KeywordBiddingKeywordInfo keywordinfo = MyKeywordBidding.GetKeywordBiddingKeywordByName(KeywordBidding_Name, pub.CreateUserPrivilege("1b86dfa7-32e5-4136-b3d1-a8a670f415ff"));
        if (keywordinfo != null)
        {
            KeywordBidding_KeywordID = keywordinfo.Keyword_ID;
        }
        else
        {
            keywordinfo = AddKeywordBiddingKeywordInfo(KeywordBidding_Name);
            if (keywordinfo != null)
            {
                KeywordBidding_KeywordID = keywordinfo.Keyword_ID;
            }
        }
        if (KeywordBidding_KeywordID == 0)
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }

        KeywordBiddingInfo entity = new KeywordBiddingInfo();
        entity.KeywordBidding_ID = KeywordBidding_ID;
        entity.KeywordBidding_SupplierID = KeywordBidding_SupplierID;
        entity.KeywordBidding_ProductID = KeywordBidding_ProductID;
        entity.KeywordBidding_KeywordID = KeywordBidding_KeywordID;
        entity.KeywordBidding_Price = KeywordBidding_Price;
        entity.KeywordBidding_StartDate = KeywordBidding_StartDate;
        entity.KeywordBidding_EndDate = KeywordBidding_EndDate;
        entity.KeywordBidding_ShowTimes = KeywordBidding_ShowTimes;
        entity.KeywordBidding_Hits = KeywordBidding_Hits;
        entity.KeywordBidding_Audit = KeywordBidding_Audit;
        entity.KeywordBidding_Site = pub.GetCurrentSite(); ;

        if (MyKeywordBidding.AddKeywordBidding(entity))
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "KeywordBidding_add.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //添加竞价关键词
    public virtual KeywordBiddingKeywordInfo AddKeywordBiddingKeywordInfo(string Keyword)
    {
        KeywordBiddingKeywordInfo entity = new KeywordBiddingKeywordInfo();
        entity.Keyword_MinPrice = 0;
        entity.Keyword_Name = Keyword;
        MyKeywordBidding.AddKeywordBiddingKeyword(entity);
        return MyKeywordBidding.GetKeywordBiddingKeywordByName(Keyword, pub.CreateUserPrivilege("1b86dfa7-32e5-4136-b3d1-a8a670f415ff"));
    }

    //获取指定竞价关键词
    public virtual string GetKeywordBiddingKeywordByID(int Keyword_ID)
    {
        string Keyword_Name = "";
        KeywordBiddingKeywordInfo entity = MyKeywordBidding.GetKeywordBiddingKeywordByID(Keyword_ID, pub.CreateUserPrivilege("1b86dfa7-32e5-4136-b3d1-a8a670f415ff"));
        if (entity != null)
        {
            Keyword_Name = entity.Keyword_Name;
        }
        return Keyword_Name;
    }

    public virtual void EditKeywordBidding()
    {

        int KeywordBidding_ID = tools.CheckInt(Request.Form["KeywordBidding_ID"]);
        int KeywordBidding_SupplierID = tools.CheckInt(Request.Form["KeywordBidding_SupplierID"]);
        int KeywordBidding_ProductID = tools.CheckInt(Request.Form["KeywordBidding_ProductID"]);
        string KeywordBidding_Name = tools.CheckStr(Request.Form["KeywordBidding_Name"]);
        double KeywordBidding_Price = tools.CheckFloat(Request.Form["KeywordBidding_Price"]);
        DateTime KeywordBidding_StartDate = tools.NullDate(Request.Form["KeywordBidding_StartDate"]);
        DateTime KeywordBidding_EndDate = tools.NullDate(Request.Form["KeywordBidding_EndDate"]);
        int KeywordBidding_ShowTimes = tools.CheckInt(Request.Form["KeywordBidding_ShowTimes"]);
        int KeywordBidding_Hits = tools.CheckInt(Request.Form["KeywordBidding_Hits"]);
        int KeywordBidding_Audit = tools.CheckInt(Request.Form["KeywordBidding_Audit"]);
        string KeywordBidding_Site = tools.CheckStr(Request.Form["KeywordBidding_Site"]);
        int KeywordBidding_KeywordID = 0;

        if (KeywordBidding_ProductID == 0)
            pub.Msg("error", "错误信息", "请先选择商品", false, "{back}");

        if (KeywordBidding_Name == null || KeywordBidding_Name.Length == 0)
            pub.Msg("error", "错误信息", "请填写竞价名称", false, "{back}");

        if (KeywordBidding_Price == null || KeywordBidding_Price <= 0)
            pub.Msg("error", "错误信息", "请填写出价", false, "{back}");

        if (CheckKeywordBiddinRepeat(KeywordBidding_Name, KeywordBidding_ProductID, KeywordBidding_ID))
            pub.Msg("error", "错误信息", "已有该商品的关键词", false, "{back}");

        if (KeywordBidding_Price < CheckKeyword_Price(KeywordBidding_Name))
        {
            pub.Msg("error", "错误信息", "出价不可低于关键词起步价", false, "{back}");
        }

        KeywordBiddingKeywordInfo keywordinfo = MyKeywordBidding.GetKeywordBiddingKeywordByName(KeywordBidding_Name, pub.CreateUserPrivilege("1b86dfa7-32e5-4136-b3d1-a8a670f415ff"));
        if (keywordinfo != null)
        {
            KeywordBidding_KeywordID = keywordinfo.Keyword_ID;
        }
        else
        {
            keywordinfo = AddKeywordBiddingKeywordInfo(KeywordBidding_Name);
            if (keywordinfo != null)
            {
                KeywordBidding_KeywordID = keywordinfo.Keyword_ID;
            }
        }
        if (KeywordBidding_StartDate.Year < 1900)
        {
            KeywordBidding_StartDate = DateTime.Now;
        }
        if (KeywordBidding_EndDate.Year < 1900)
        {
            KeywordBidding_EndDate = DateTime.Now;
        }
        if (KeywordBidding_KeywordID == 0)
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }

        KeywordBiddingInfo entity = GetKeywordBidding(KeywordBidding_ID);
        if (entity != null)
        {
            if (entity.KeywordBidding_SupplierID == tools.NullInt(Session["supplier_id"]))
            {
                entity.KeywordBidding_ID = KeywordBidding_ID;

                entity.KeywordBidding_ProductID = KeywordBidding_ProductID;
                entity.KeywordBidding_KeywordID = KeywordBidding_KeywordID;
                entity.KeywordBidding_Price = KeywordBidding_Price;
                entity.KeywordBidding_StartDate = KeywordBidding_StartDate;
                entity.KeywordBidding_EndDate = KeywordBidding_EndDate;
                entity.KeywordBidding_ShowTimes = KeywordBidding_ShowTimes;
                entity.KeywordBidding_Hits = KeywordBidding_Hits;
                entity.KeywordBidding_Audit = KeywordBidding_Audit;


                if (MyKeywordBidding.EditKeywordBidding(entity))
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "KeywordBidding_list.aspx");
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
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void DelKeywordBidding()
    {
        int KeywordBidding_ID = tools.CheckInt(Request.QueryString["KeywordBidding_ID"]);
        if (MyKeywordBidding.DelKeywordBidding(tools.NullInt(Session["supplier_id"]), KeywordBidding_ID, pub.CreateUserPrivilege("cc52c0d7-188d-4915-955a-7e0857e958bc")) > 0)
        {
            pub.Msg("positive", "操作成功", "操作成功", true, "KeywordBidding_list.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual KeywordBiddingInfo GetKeywordBidding(int KeywordBidding_ID)
    {
        return MyKeywordBidding.GetKeywordBiddingByID(KeywordBidding_ID, pub.CreateUserPrivilege("445e8b4f-4b38-4e4c-9b0f-c69e0b1a6a71"));
    }

    public void KeywordBidding_List()
    {

        //Response.Write("<table class=\"commontable\">");
        //Response.Write("<tr>");
        Response.Write("<table width=\"100%\" cellpadding=\"0\" cellspacing=\"0\" style=\"border:1px solid #dadada;\" class=\"pingjia\">");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <th>商品名称</th>");
        Response.Write("  <th>关键词</th>");
        Response.Write("  <th width=\"50\">出价</th>");
        Response.Write("  <th width=\"50\">展示</th>");
        Response.Write("  <th width=\"50\">点击</th>");
        Response.Write("  <th width=\"80\">起止时间</th>");
        Response.Write("  <th width=\"50\">状态</th>");
        Response.Write("  <th width=\"50\">操作</th>");
        Response.Write("</tr>");

        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        string keyword = tools.CheckStr(Request["keyword"]);
        string PageUrl = "?keyword=" + keyword;
        if (curpage < 1)
        {
            curpage = 1;
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "KeywordBiddingInfo.KeywordBidding_SupplierID", "=", tools.NullInt(Session["supplier_id"]).ToString()));
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "KeywordBiddingInfo.KeywordBidding_KeywordID", "in", "select distinct Keyword_ID from KeywordBidding_Keyword where Keyword_Name like '%" + keyword + "%'"));
        }
        Query.OrderInfos.Add(new OrderInfo("KeywordBiddingInfo.KeywordBidding_ID", "Desc"));
        IList<KeywordBiddingInfo> entitys = MyKeywordBidding.GetKeywordBiddings(Query, pub.CreateUserPrivilege("445e8b4f-4b38-4e4c-9b0f-c69e0b1a6a71"));
        PageInfo page = MyKeywordBidding.GetPageInfo(Query, pub.CreateUserPrivilege("445e8b4f-4b38-4e4c-9b0f-c69e0b1a6a71"));
        if (entitys != null)
        {
            string KeywordBidding_Name = "";
            string Product_Name = "";
            ProductInfo productinfo = null;
            foreach (KeywordBiddingInfo entity in entitys)
            {
                KeywordBidding_Name = "";
                Product_Name = "";
                KeywordBidding_Name = GetKeywordBiddingKeywordByID(entity.KeywordBidding_KeywordID);
                productinfo = GetProductByID(entity.KeywordBidding_ProductID);
                if (productinfo != null)
                {
                    Product_Name = productinfo.Product_Name;
                }
                Response.Write("<tr>");
                Response.Write("<td>" + Product_Name + "</td>");
                Response.Write("<td>" + KeywordBidding_Name + "</td>");
                Response.Write("<td>" + pub.FormatCurrency(entity.KeywordBidding_Price) + "</td>");
                Response.Write("<td>" + entity.KeywordBidding_ShowTimes + "</td>");
                Response.Write("<td>" + entity.KeywordBidding_Hits + "</td>");
                Response.Write("<td>" + entity.KeywordBidding_StartDate.ToShortDateString() + " - " + entity.KeywordBidding_EndDate.ToShortDateString() + "</td>");
                Response.Write("<td>");
                if (entity.KeywordBidding_Audit == 1)
                {
                    Response.Write("审核通过");
                }
                else if (entity.KeywordBidding_Audit == 2)
                {
                    Response.Write("审核失败");
                }
                else
                {
                    Response.Write("未处理");
                }
                Response.Write("</td>");

                Response.Write("<td>");
                Response.Write("<a href=\"/supplier/keywordbidding_edit.aspx?keywordbidding_id=" + entity.KeywordBidding_ID + "\"><img src=\"/images/btn_edit.gif\" border=\"0\" alt=\"修改\"></a>");
                Response.Write(" <a href=\"/supplier/keywordbidding_do.aspx?action=del&keywordbidding_id=" + entity.KeywordBidding_ID + "\"><img src=\"/images/btn_del.gif\" border=\"0\" alt=\"删除\"></a>");
                Response.Write("</td>");

                Response.Write("</tr>");

            }

            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" align=\"center\" cellpadding=\"2\"><tr><td align=\"right\" height=\"10\"></td></tr><tr><td align=\"right\">");
            pub.Page(page.PageCount, page.CurrentPage, PageUrl, page.PageSize, page.RecordCount);
            Response.Write("</td></tr></table>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#ffffff\"><td height=\"35\" colspan=\"8\"  align=\"center\">没有记录</td></tr>");
            Response.Write("</table>");
        }
    }

    public double CheckKeyword_Price(string keyword)
    {
        if (keyword.Length > 0)
        {
            KeywordBiddingKeywordInfo entity = MyKeywordBidding.GetKeywordBiddingKeywordByName(keyword, pub.CreateUserPrivilege("1b86dfa7-32e5-4136-b3d1-a8a670f415ff"));
            if (entity != null)
            {
                return entity.Keyword_MinPrice;
            }
            else
            {
                return tools.NullDbl(Application["Keyword_Adv_MinPrice"]);
            }
        }
        else
        {
            return tools.NullDbl(Application["Keyword_Adv_MinPrice"]);
        }
    }

    #endregion

    #region 收藏


    //会员收藏夹
    public void Supplier_Favorates(string uses, int irowmax)
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
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
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierFavoritesInfo.Supplier_Favorites_SupplierID", "=", supplier_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierFavoritesInfo.Supplier_Favorites_Type", "=", "0"));
        Query.OrderInfos.Add(new OrderInfo("SupplierFavoritesInfo.Supplier_Favorites_ID", "Desc"));
        IList<SupplierFavoritesInfo> favoriates = MySFav.GetSupplierFavoritess(Query);
        if (favoriates != null)
        {
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
            Response.Write("  <tr>");
            foreach (SupplierFavoritesInfo entity in favoriates)
            {
                ProductInfo product = MyProduct.GetProductByID(entity.Supplier_Favorites_TargetID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                if (product != null)
                {
                    if (product.Product_IsInsale == 0 || product.Product_IsAudit == 0)
                    {
                        continue;
                    }
                    productURL = pageurl.FormatURL(pageurl.product_detail, product.Product_ID.ToString());

                    icount = icount + 1;
                    Response.Write("    <td width=\"" + (100 / irowmax) + "%\" align=\"center\"><table width=\"120\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    Response.Write("      <tr>");
                    Response.Write("        <td height=\"120\" align=\"center\" valign=\"middle\"><a href=\"" + productURL + "\" alt=\"" + product.Product_Name + "\" title=\"" + product.Product_Name + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(product.Product_Img, "thumbnail") + "\" border=\"0\" width=\"120\" alt=\"" + product.Product_Name + "\"></a></td>");
                    Response.Write("      </tr>");
                    Response.Write("      <tr>");
                    Response.Write("        <td height=\"30\" align=\"center\"><a href=\"" + productURL + "\" alt=\"" + product.Product_Name + "\" title=\"" + product.Product_Name + "\" target=\"_blank\">" + tools.CutStr(product.Product_Name, 30, "") + "</a>");
                    Response.Write("        </td>");
                    Response.Write("      </tr>");
                    Response.Write("      <tr>");
                    Response.Write("        <td height=\"30\" align=\"center\">");
                    Response.Write("<span class=\"list_price\">" + pub.FormatCurrency(pub.Get_Member_Price(product.Product_ID, product.Product_Price)) + "</span>");


                    Response.Write("        </td>");
                    Response.Write("      </tr>");

                    Response.Write("      <tr>");
                    Response.Write("        <td height=\"30\" valign=\"middle\" align=\"center\">");


                    if (product.Product_UsableAmount > 0 || product.Product_IsNoStock == 1)
                    {
                        Response.Write("<a href=\"" + pageurl.FormatURL(pageurl.product_detail, product.Product_ID.ToString()) + "\"><img src=\"/images/btn_buy.jpg\" align=\"absmiddle\" border=\"0\"  alt=\"添加到购物车\" /></a> ");
                    }
                    else
                    {
                        Response.Write("<a><img src=\"/Images/btn_nostock.jpg\" align=\"absmiddle\" border=\"0\"  alt=\"暂无货\" /></a> ");
                    }

                    Response.Write("<a href=\"/supplier/fav_do.aspx?action=goods_move&id=" + entity.Supplier_Favorites_ID + "\" onclick=\"if(confirm('确定将选择商品从收藏夹中删除吗？')==false){return false;}\"><img src=\"/Images/btn_del.jpg\" align=\"absmiddle\" border=\"0\" alt=\"从收藏中移除该商品\" /></a>");


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
            Response.Write("<tr><td align=\"center\" height=\"30\" class=\"t12_grey\">您还没有收藏商品</td></tr>");
            Response.Write("</table>");
        }
    }

    //商铺收藏
    public void Supplier_Shop_Favorates(string uses, int irowmax)
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
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
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierFavoritesInfo.Supplier_Favorites_SupplierID", "=", supplier_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierFavoritesInfo.Supplier_Favorites_Type", "=", "1"));
        Query.OrderInfos.Add(new OrderInfo("SupplierFavoritesInfo.Supplier_Favorites_ID", "Desc"));
        IList<SupplierFavoritesInfo> favoriates = MySFav.GetSupplierFavoritess(Query);
        if (favoriates != null)
        {
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
            Response.Write("  <tr>");
            foreach (SupplierFavoritesInfo entity in favoriates)
            {
                SupplierShopInfo shopinfo = MyShop.GetSupplierShopByID(entity.Supplier_Favorites_TargetID);
                if (shopinfo != null)
                {

                    if (shopinfo.Shop_Status == 0)
                    {
                        continue;
                    }

                    icount = icount + 1;
                    Response.Write("    <td width=\"" + (100 / irowmax) + "%\" align=\"center\"><table width=\"120\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                    Response.Write("      <tr>");
                    Response.Write("        <td height=\"120\" align=\"center\" valign=\"middle\"><a href=\"http://" + shopinfo.Shop_Domain + GetShopDomain() + "\" alt=\"" + shopinfo.Shop_Name + "\" title=\"" + shopinfo.Shop_Name + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(shopinfo.Shop_Img, "fullpath") + "\" border=\"0\" width=\"120\" alt=\"" + shopinfo.Shop_Name + "\"></a></td>");
                    Response.Write("      </tr>");
                    Response.Write("      <tr>");
                    Response.Write("        <td height=\"30\" align=\"center\"><a href=\"http://" + shopinfo.Shop_Domain + GetShopDomain() + "\" alt=\"" + shopinfo.Shop_Name + "\" title=\"" + shopinfo.Shop_Name + "\" target=\"_blank\">" + tools.CutStr(shopinfo.Shop_Name, 30, "") + "</a>");
                    Response.Write("        </td>");
                    Response.Write("      </tr>");

                    Response.Write("      <tr>");
                    Response.Write("        <td height=\"30\" valign=\"middle\" align=\"center\">");




                    Response.Write("<a href=\"/supplier/fav_do.aspx?action=shop_move&id=" + entity.Supplier_Favorites_ID + "\" onclick=\"if(confirm('确定将选择店铺从收藏夹中删除吗？')==false){return false;}\"><img src=\"/Images/btn_del.jpg\" align=\"absmiddle\" border=\"0\" alt=\"从收藏中移除该店铺\" /></a>");


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
            Response.Write("<tr><td align=\"center\" height=\"30\" class=\"t12_grey\">您还没有收藏店铺</td></tr>");
            Response.Write("</table>");
        }
    }

    //添加到收藏夹
    public void Supplier_Favorites_Add(string action, int targetid)
    {
        if (targetid == 0)
        {
            pub.Msg("info", "信息提示", "请选择要添加到收藏夹的内容", false, "{back}");
        }
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        if (action == "product")
        {
            ProductInfo product = MyProduct.GetProductByID(targetid, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
            if (product != null)
            {
                if (product.Product_IsInsale == 1 && product.Product_IsAudit == 1)
                {
                    SupplierFavoritesInfo favorcheck = MySFav.GetSupplierFavoritesByProductID(supplier_id, 0, targetid);
                    if (favorcheck != null)
                    {
                        pub.Msg("info", "信息提示", "该商品已在您的收藏夹中！", true, "/supplier/supplier_favorites.aspx");
                    }
                    SupplierFavoritesInfo favor = new SupplierFavoritesInfo();
                    favor.Supplier_Favorites_ID = 0;
                    favor.Supplier_Favorites_SupplierID = supplier_id;
                    favor.Supplier_Favorites_Type = 0;
                    favor.Supplier_Favorites_TargetID = targetid;
                    favor.Supplier_Favorites_Addtime = DateTime.Now;
                    favor.Supplier_Favorites_Site = pub.GetCurrentSite();

                    if (MySFav.AddSupplierFavorites(favor))
                    {
                        pub.Msg("positive", "信息提示", "信息收藏成功！", true, "/supplier/supplier_favorites.aspx");
                    }
                    else
                    {
                        pub.Msg("info", "信息提示", "收藏失败，请稍后再试！", false, "{back}");
                    }
                }
                else
                {
                    pub.Msg("info", "信息提示", "收藏失败，请稍后再试！", false, "{back}");
                }
            }
            else
            {
                pub.Msg("info", "信息提示", "收藏失败，请稍后再试！", false, "{back}");
            }
        }
        else
        {
            SupplierShopInfo shopinfo = MyShop.GetSupplierShopByID(targetid);
            if (shopinfo != null)
            {
                if (shopinfo.Shop_Status == 1)
                {
                    SupplierFavoritesInfo favorcheck = MySFav.GetSupplierFavoritesByProductID(supplier_id, 1, targetid);
                    if (favorcheck != null)
                    {
                        pub.Msg("info", "信息提示", "该店铺已在您的收藏夹中！", true, "/supplier/supplier_shop_favorites.aspx");
                    }
                    SupplierFavoritesInfo favor = new SupplierFavoritesInfo();
                    favor.Supplier_Favorites_ID = 0;
                    favor.Supplier_Favorites_SupplierID = supplier_id;
                    favor.Supplier_Favorites_Type = 1;
                    favor.Supplier_Favorites_TargetID = targetid;
                    favor.Supplier_Favorites_Addtime = DateTime.Now;
                    favor.Supplier_Favorites_Site = pub.GetCurrentSite();

                    if (MySFav.AddSupplierFavorites(favor))
                    {
                        pub.Msg("positive", "信息提示", "信息收藏成功！", true, "/supplier/supplier_shop_favorites.aspx");
                    }
                    else
                    {
                        pub.Msg("info", "信息提示", "收藏失败，请稍后再试！", false, "{back}");
                    }
                }
                else
                {
                    pub.Msg("info", "信息提示", "收藏失败，请稍后再试！", false, "{back}");
                }
            }
            else
            {
                pub.Msg("info", "信息提示", "收藏失败，请稍后再试！", false, "{back}");
            }
        }
    }

    //从收藏夹移除
    public void Supplier_Favorites_Del(int ID)
    {
        if (ID == 0)
        {
            pub.Msg("info", "信息提示", "请选择要删除的内容", false, "{back}");
        }
        int supplierid = tools.NullInt(Session["supplier_id"]);
        SupplierFavoritesInfo favor = MySFav.GetSupplierFavoritesByID(ID);
        if (favor != null)
        {
            if (favor.Supplier_Favorites_SupplierID == supplierid)
            {
                MySFav.DelSupplierFavorites(ID);
                if (favor.Supplier_Favorites_Type == 0)
                {
                    Response.Redirect("/supplier/supplier_favorites.aspx");
                }
                else
                {
                    Response.Redirect("/supplier/supplier_shop_favorites.aspx");
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

    #endregion

    #region 子账户

    /// <summary>
    /// 添加子菜单
    /// </summary>
    /// <param name="IsMemberMenu">IsSupplierMenu：0会员子菜单,1商家子菜单</param>
    public void AddSubAccount(int IsSupplierMenu)
    {
        int account_id = tools.NullInt(Session["account_id"]);
        if (account_id > 0)
        {
            if (IsSupplierMenu == 0)
            {
                Response.Redirect("/member/index.aspx");
            }
            else
            {
                Response.Redirect("/supplier/index.aspx");
            }

        }
        string Supplier_SubAccount_Name = tools.CheckStr(Request.Form["Supplier_SubAccount_Name"]);
        string Supplier_SubAccount_Password = tools.CheckStr(Request.Form["Supplier_SubAccount_Password"]);
        string Supplier_SubAccount_Password_confirm = tools.CheckStr(Request.Form["Supplier_SubAccount_Password_confirm"]);
        string Supplier_SubAccount_TrueName = tools.CheckStr(Request.Form["Supplier_SubAccount_TrueName"]);
        string Supplier_SubAccount_Department = tools.CheckStr(Request.Form["Supplier_SubAccount_Department"]);
        string Supplier_SubAccount_Mobile = tools.CheckStr(Request.Form["Supplier_SubAccount_Mobile"]);
        string Supplier_SubAccount_Email = tools.CheckStr(Request.Form["Supplier_SubAccount_Email"]);
        string Supplier_SubAccount_ExpireTime = tools.CheckStr(Request.Form["Supplier_SubAccount_ExpireTime"]);
        string Supplier_SubAccount_Privilege = tools.CheckStr(Request.Form["Supplier_SubAccount_Privilege"]);
        int supplier_id = tools.NullInt(Session["supplier_id"]);


        if (Supplier_SubAccount_Name == "")
        {
            pub.Msg("info", "信息提示", "请输入用户名", false, "{back}");
        }
        else
        {
            if (CheckNickname(Supplier_SubAccount_Name))
            {
                Encoding name_length = System.Text.Encoding.GetEncoding("gb2312");
                byte[] bytes = name_length.GetBytes(Supplier_SubAccount_Name);
                if (bytes.Length < 4 || bytes.Length > 20)
                {
                    pub.Msg("info", "信息提示", "用户名长度应在4-20位字符", false, "{back}");
                }
                if (Check_Supplier_Nickname(Supplier_SubAccount_Name))
                {
                    pub.Msg("info", "信息提示", "该用户名已被使用，请使用其他用户名", false, "{back}");
                }
            }
            else
            {
                pub.Msg("info", "信息提示", "用户名含有特殊字符", false, "{back}");
            }
        }

        if (CheckSsn(Supplier_SubAccount_Password) == false)
        {
            pub.Msg("info", "提示信息", "密码包含特殊字符，只接受A-Z，a-z，0-9，不要输入空格", false, "{back}");
        }
        else
        {
            if (Supplier_SubAccount_Password.Length < 6 || Supplier_SubAccount_Password.Length > 20)
            {
                pub.Msg("info", "提示信息", "请输入6～20位新密码", false, "{back}");
            }
        }

        if (Supplier_SubAccount_Password != Supplier_SubAccount_Password_confirm)
        {
            pub.Msg("info", "提示信息", "两次密码输入不一致，请重新输入", false, "{back}");
        }

        if (Supplier_SubAccount_TrueName == "")
        {
            pub.Msg("info", "提示信息", "请填写子账户姓名", false, "{back}");
        }

        if (Supplier_SubAccount_Mobile != "")
        {
            if (!pub.Checkmobile(Supplier_SubAccount_Mobile))
            {
                pub.Msg("info", "提示信息", "手机格式不正确，请重新输入", false, "{back}");
            }
        }


        if (Supplier_SubAccount_Email != "")
        {
            if (!tools.CheckEmail(Supplier_SubAccount_Email))
            {
                pub.Msg("info", "提示信息", "邮箱格式不正确，请重新输入", false, "{back}");
            }
        }

        if (Supplier_SubAccount_ExpireTime == "")
        {
            pub.Msg("info", "提示信息", "请选择子账号到期时间", false, "{back}");
        }
        else
        {
            if (Supplier_SubAccount_ExpireTime == DateTime.Now.ToString("yyyy-MM-dd"))
            {
                pub.Msg("info", "提示信息", "子账号到期时间不能等于当前时间,请重新选择", false, "{back}");
            }
        }

        Supplier_SubAccount_Password = encrypt.MD5(Supplier_SubAccount_Password);


        SupplierSubAccountInfo subAccountInfo = new SupplierSubAccountInfo();
        subAccountInfo.Supplier_SubAccount_Name = Supplier_SubAccount_Name;
        subAccountInfo.Supplier_SubAccount_Password = Supplier_SubAccount_Password;
        subAccountInfo.Supplier_SubAccount_Mobile = Supplier_SubAccount_Mobile;
        subAccountInfo.Supplier_SubAccount_TrueName = Supplier_SubAccount_TrueName;
        subAccountInfo.Supplier_SubAccount_Department = Supplier_SubAccount_Department;
        subAccountInfo.Supplier_SubAccount_SupplierID = supplier_id;
        subAccountInfo.Supplier_SubAccount_Email = Supplier_SubAccount_Email;
        subAccountInfo.Supplier_SubAccount_ExpireTime = tools.NullDate(Supplier_SubAccount_ExpireTime, DateTime.Now.ToShortDateString());
        DateTime now = DateTime.Now;
        subAccountInfo.Supplier_SubAccount_AddTime = now;
        subAccountInfo.Supplier_SubAccount_lastLoginTime = now;
        subAccountInfo.Supplier_SubAccount_Tel = "";
        subAccountInfo.Supplier_SubAccount_IsActive = 1;
        subAccountInfo.Supplier_SubAccount_Privilege = Supplier_SubAccount_Privilege;

        if (MySubAccount.AddSupplierSubAccount(subAccountInfo))
        {
            if (IsSupplierMenu == 0)
            {
                Response.Redirect("/member/subAccount_list.aspx");
            }
            else
            {
                Response.Redirect("/supplier/subAccount_list.aspx");
            }

        }
        else
        {
            pub.Msg("error", "错误信息", "子账户添加失败，请稍后再试！", false, "{back}");
        }

    }
    /// <summary>
    /// 子账户列表
    /// </summary>
    /// <param name="keyword">搜索关键字</param>
    /// <param name="IsMemberMenu">IsSupplierMenu：0会员子菜单,1商家子菜单</param>
    public void Get_SubAccount_List(string keyword, int IsSupplierMenu)
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        string Pageurl = "?action=list";
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        string BgColor = "";
        int i = 0;

        if (curpage < 1)
        {
            curpage = 1;
        }


        Response.Write("<table  width=\"973\" border=\"0\" cellspacing=\"2\" cellpadding=\"0\" class=\"table02\">");
        Response.Write("<tr>");
        Response.Write("  <td width=\"150\" class=\"name\">用户名</td>");
        Response.Write("  <td  class=\"name\">姓名</th>");
        Response.Write("  <td width=\"110\" class=\"name\">部门</td>");
        Response.Write("  <td width=\"110\" class=\"name\">邮箱</td>");
        Response.Write("  <td width=\"110\" class=\"name\">手机</td>");
        Response.Write("  <td width=\"88\" class=\"name\">到期时间</td>");
        Response.Write("  <td width=\"88\" class=\"name\">状态</td>");
        Response.Write("  <td width=\"110\" class=\"name\">操作</td>");
        Response.Write("</tr>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierSubAccountInfo.Supplier_SubAccount_SupplierID", "=", supplier_id.ToString()));

        if (keyword != "")
        {
            Pageurl += "&subaccountkeyword=" + keyword;
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "SupplierSubAccountInfo.Supplier_SubAccount_Name", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "SupplierSubAccountInfo.Supplier_SubAccount_TrueName", "like", keyword));
        }

        Query.OrderInfos.Add(new OrderInfo("SupplierSubAccountInfo.Supplier_SubAccount_ID", "Desc"));
        IList<SupplierSubAccountInfo> entitys = MySubAccount.GetSupplierSubAccounts(Query);
        PageInfo page = MySubAccount.GetPageInfo(Query);
        if (entitys != null)
        {
            foreach (SupplierSubAccountInfo entity in entitys)
            {
                i++;
                if (i % 2 == 0)
                {
                    BgColor = "class=\"bg\"";
                }
                else
                {
                    BgColor = " ";
                }

                Response.Write("<tr " + BgColor + ">");
                Response.Write("<td >" + entity.Supplier_SubAccount_Name + "</td>");
                Response.Write("<td >" + entity.Supplier_SubAccount_TrueName + "</td>");
                Response.Write("<td >" + entity.Supplier_SubAccount_Department + "</td>");
                Response.Write("<td >" + entity.Supplier_SubAccount_Email + "</td>");
                Response.Write("<td >" + entity.Supplier_SubAccount_Mobile + "</td>");
                Response.Write("<td >" + entity.Supplier_SubAccount_ExpireTime.ToString("yyyy-MM-dd") + "</td>");
                Response.Write("<td >" + (entity.Supplier_SubAccount_IsActive == 1 ? "启用" : "禁用") + "</td>");
                if (IsSupplierMenu == 0)
                {
                    Response.Write("<td ><span><a href=\"/member/subAccount_edit.aspx?id=" + entity.Supplier_SubAccount_ID + "\" class=\"a05\">修改</a> </span><span><a href=\"/member/account_do.aspx?action=subdel&id=" + entity.Supplier_SubAccount_ID + "\">删除</a></span></td>");
                }

                else
                {
                    Response.Write("<td ><span><a href=\"/supplier/subAccount_edit.aspx?id=" + entity.Supplier_SubAccount_ID + "\" class=\"a05\">修改</a> </span><span><a href=\"/supplier/account_do.aspx?action=subdel&id=" + entity.Supplier_SubAccount_ID + "\">删除</a></span></td>");
                }
                Response.Write("</tr>");
            }
            Response.Write("</table>");


            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
        }
        else
        {
            Response.Write("<table width=\"973\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            Response.Write("<tr>");
            Response.Write("<td colspan=\"8\" align=\"center\" height=\"35\">暂无记录</td>");
            Response.Write("</tr>");
            Response.Write("</table>");
        }
    }
    /// <summary>
    /// 子账号删除
    /// </summary>
    /// <param name="IsSupplierMenu">IsSupplierMenu：0会员子菜单,1商家子菜单</param>
    public void delSubAccount(int IsSupplierMenu)
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        int id = tools.CheckInt(Request["id"]);
        int account_id = tools.NullInt(Session["account_id"]);
        if (account_id > 0)
        {
            Response.Redirect("/supplier/index.aspx");
        }
        if (supplier_id > 0 && id > 0)
        {
            SupplierSubAccountInfo subinfo = MySubAccount.GetSupplierSubAccountByID(id);
            if (subinfo != null)
            {
                if (subinfo.Supplier_SubAccount_SupplierID == supplier_id)
                {
                    MySubAccount.DelSupplierSubAccount(id);
                    if (IsSupplierMenu == 0)
                    {
                        Response.Redirect("/member/subAccount_list.aspx");
                    }
                    else
                    {
                        Response.Redirect("/supplier/subAccount_list.aspx");
                    }

                }
                else
                {
                    pub.Msg("error", "错误信息", "子账户删除失败！", false, "{back}");
                }
            }
            else
            {
                pub.Msg("error", "错误信息", "子账户删除失败！", false, "{back}");
            }

        }
        else
        {
            pub.Msg("error", "错误信息", "子账户删除失败！", false, "{back}");
        }

    }

    public SupplierSubAccountInfo GetSupplierSubAccountByID(int id)
    {
        return MySubAccount.GetSupplierSubAccountByID(id);
    }

    public SupplierSubAccountInfo GetSupplierSubAccountByID()
    {
        int account_id = tools.NullInt(Session["account_id"]);

        if (account_id > 0)
        {
            return MySubAccount.GetSupplierSubAccountByID(account_id);
        }
        else
        {
            return null;
        }

    }
    /// <summary>
    /// 主帐号 修改子帐号
    /// </summary>
    /// <param name="IsSupplierMenu">IsSupplierMenu：0会员子菜单,1商家子菜单</param>
    public void EditSubAccount(int IsSupplierMenu)
    {
        int account_id = tools.NullInt(Session["account_id"]);
        if (account_id > 0)
        {
            if (IsSupplierMenu == 0)
            {
                Response.Redirect("/member/index.aspx");
            }
            else
            {
                Response.Redirect("/supplier/index.aspx");
            }

        }
        string Supplier_SubAccount_Password = tools.CheckStr(Request.Form["Supplier_SubAccount_Password"]);
        string Supplier_SubAccount_Password_confirm = tools.CheckStr(Request.Form["Supplier_SubAccount_Password_confirm"]);

        string Supplier_SubAccount_TrueName = tools.CheckStr(Request.Form["Supplier_SubAccount_TrueName"]);
        string Supplier_SubAccount_Department = tools.CheckStr(Request.Form["Supplier_SubAccount_Department"]);
        string Supplier_SubAccount_Mobile = tools.CheckStr(Request.Form["Supplier_SubAccount_Mobile"]);
        string Supplier_SubAccount_Email = tools.CheckStr(Request.Form["Supplier_SubAccount_Email"]);
        string Supplier_SubAccount_ExpireTime = tools.CheckStr(Request.Form["Supplier_SubAccount_ExpireTime"]);
        string Supplier_SubAccount_Privilege = tools.CheckStr(Request.Form["Supplier_SubAccount_Privilege"]);
        int Supplier_SubAccount_IsActive = tools.CheckInt(Request.Form["Supplier_SubAccount_IsActive"]);
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        int subAccountID = tools.CheckInt(Request["subAccountID"]);
        if (supplier_id < 1 || subAccountID < 1)
        {
            if (IsSupplierMenu == 0)
            {
                Response.Redirect("/member/subAccount_list.aspx");
            }
            else
            {
                Response.Redirect("/supplier/subAccount_list.aspx");
            }

        }
        bool editPwd = true;
        if (Supplier_SubAccount_Password == "" && Supplier_SubAccount_Password_confirm == "")
        {
            editPwd = false;
        }
        else
        {

            if (CheckSsn(Supplier_SubAccount_Password) == false)
            {
                pub.Msg("info", "提示信息", "密码包含特殊字符，只接受A-Z，a-z，0-9，不要输入空格", false, "{back}");
            }
            else
            {
                if (Supplier_SubAccount_Password.Length < 6 || Supplier_SubAccount_Password.Length > 20)
                {
                    pub.Msg("info", "提示信息", "请输入6～20位新密码", false, "{back}");
                }
            }

            if (Supplier_SubAccount_Password != Supplier_SubAccount_Password_confirm)
            {
                pub.Msg("info", "提示信息", "两次密码输入不一致，请重新输入", false, "{back}");
            }

            Supplier_SubAccount_Password = encrypt.MD5(Supplier_SubAccount_Password);
        }

        if (Supplier_SubAccount_TrueName == "")
        {
            pub.Msg("info", "提示信息", "请填写子账户姓名", false, "{back}");
        }

        if (Supplier_SubAccount_Mobile != "")
        {
            if (!pub.Checkmobile(Supplier_SubAccount_Mobile))
            {
                pub.Msg("info", "提示信息", "手机格式不正确，请重新输入", false, "{back}");
            }
        }

        if (Supplier_SubAccount_Email != "")
        {
            if (!tools.CheckEmail(Supplier_SubAccount_Email))
            {
                pub.Msg("info", "提示信息", "邮箱格式不正确，请重新输入", false, "{back}");
            }
        }



        SupplierSubAccountInfo subAccountInfo = MySubAccount.GetSupplierSubAccountByID(subAccountID);
        if (subAccountInfo != null)
        {
            if (subAccountInfo.Supplier_SubAccount_SupplierID == supplier_id)
            {
                if (editPwd)
                {
                    subAccountInfo.Supplier_SubAccount_Password = Supplier_SubAccount_Password;
                }

                subAccountInfo.Supplier_SubAccount_Mobile = Supplier_SubAccount_Mobile;
                subAccountInfo.Supplier_SubAccount_TrueName = Supplier_SubAccount_TrueName;
                subAccountInfo.Supplier_SubAccount_Department = Supplier_SubAccount_Department;
                subAccountInfo.Supplier_SubAccount_Email = Supplier_SubAccount_Email;
                subAccountInfo.Supplier_SubAccount_ExpireTime = tools.NullDate(Supplier_SubAccount_ExpireTime, DateTime.Now.ToShortDateString());
                subAccountInfo.Supplier_SubAccount_IsActive = Supplier_SubAccount_IsActive;
                subAccountInfo.Supplier_SubAccount_Privilege = Supplier_SubAccount_Privilege;

                if (MySubAccount.EditSupplierSubAccount(subAccountInfo))
                {
                    if (IsSupplierMenu == 0)
                    {
                        Response.Redirect("/member/subAccount_list.aspx");
                    }
                    else
                    {
                        Response.Redirect("/supplier/subAccount_list.aspx");
                    }
                    //Response.Redirect("/supplier/subAccount_list.aspx");
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
        else
        {
            pub.Msg("error", "错误信息", "子账户信息加载失败，请稍后再试！", false, "{back}");
        }
        //subAccountInfo.Supplier_SubAccount_ExpireTime = tools.NullDate(Supplier_SubAccount_ExpireTime);


    }

    /// <summary>
    ///子帐号 修改资料
    /// </summary>
    public void UpdateSubAccount()
    {

        string Supplier_SubAccount_TrueName = tools.CheckStr(Request.Form["Supplier_SubAccount_TrueName"]);
        string Supplier_SubAccount_Mobile = tools.CheckStr(Request.Form["Supplier_SubAccount_Mobile"]);

        string Supplier_SubAccount_Department = tools.CheckStr(Request.Form["Supplier_SubAccount_Department"]);
        string Supplier_SubAccount_Email = tools.CheckStr(Request.Form["Supplier_SubAccount_Email"]);


        int supplier_id = tools.NullInt(Session["supplier_id"]);
        int account_id = tools.NullInt(Session["account_id"]);
        if (supplier_id < 1 || account_id < 1)
        {
            Response.Redirect("/supplier/subAccount_list.aspx");
        }

        if (Supplier_SubAccount_TrueName == "")
        {
            pub.Msg("info", "提示信息", "请填写真实姓名", false, "{back}");
        }
        if (Supplier_SubAccount_Department == "")
        {
            pub.Msg("info", "提示信息", "请填写部门", false, "{back}");
        }
        if (Supplier_SubAccount_Mobile != "")
        {
            if (!pub.Checkmobile(Supplier_SubAccount_Mobile))
            {
                pub.Msg("info", "提示信息", "手机格式不正确，请重新输入", false, "{back}");
            }
        }

        if (Supplier_SubAccount_Email != "")
        {
            if (!tools.CheckEmail(Supplier_SubAccount_Email))
            {
                pub.Msg("info", "提示信息", "邮箱格式不正确，请重新输入", false, "{back}");
            }
        }

        SupplierSubAccountInfo subAccountInfo = MySubAccount.GetSupplierSubAccountByID(account_id);
        if (subAccountInfo != null)
        {
            if (subAccountInfo.Supplier_SubAccount_SupplierID == supplier_id)
            {
                subAccountInfo.Supplier_SubAccount_TrueName = Supplier_SubAccount_TrueName;
                subAccountInfo.Supplier_SubAccount_Mobile = Supplier_SubAccount_Mobile;
                subAccountInfo.Supplier_SubAccount_Department = Supplier_SubAccount_Department;
                subAccountInfo.Supplier_SubAccount_Email = Supplier_SubAccount_Email;

                if (MySubAccount.EditSupplierSubAccount(subAccountInfo))
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/account_profile.aspx?tip=success");
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


    //子账号 密码修改
    public void UpdateSubAccountPassword()
    {
        string old_pwd = tools.CheckStr(tools.NullStr(Request.Form["Supplier_oldpassword"]));
        string Supplier_password = tools.CheckStr(tools.NullStr(Request.Form["Supplier_password"]));
        string Supplier_password_confirm = tools.CheckStr(tools.NullStr(Request.Form["Supplier_password_confirm"]));
        string verifycode = tools.CheckStr(tools.NullStr(Request.Form["verifycode"]));

        if (verifycode != Session["Trade_Verify"].ToString())
        {
            pub.Msg("info", "提示信息", "验证码输入错误", false, "{back}");
        }

        if (old_pwd == "")
        {
            pub.Msg("info", "提示信息", "请输入6～20位原密码", false, "{back}");
        }

        if (CheckSsn(Supplier_password) == false)
        {
            pub.Msg("info", "提示信息", "密码包含特殊字符，只接受A-Z，a-z，0-9，不要输入空格", false, "{back}");
        }
        else
        {
            if (Supplier_password.Length < 6 || Supplier_password.Length > 20)
            {
                pub.Msg("info", "提示信息", "请输入6～20位新密码", false, "{back}");
            }
        }

        if (Supplier_password != Supplier_password_confirm)
        {
            pub.Msg("info", "提示信息", "两次密码输入不一致，请重新输入", false, "{back}");
        }

        old_pwd = encrypt.MD5(old_pwd);
        Supplier_password = encrypt.MD5(Supplier_password);




        int supplier_id = tools.NullInt(Session["supplier_id"]);
        int account_id = tools.NullInt(Session["account_id"]);
        if (supplier_id < 1 || account_id < 1)
        {
            Response.Redirect("/supplier/index.aspx");
        }


        SupplierSubAccountInfo subAccountInfo = GetSupplierSubAccountByID();




        if (subAccountInfo != null)
        {
            if (subAccountInfo.Supplier_SubAccount_SupplierID == supplier_id)
            {
                string Supplier_SubAccount_Password = subAccountInfo.Supplier_SubAccount_Password;

                subAccountInfo.Supplier_SubAccount_Password = Supplier_password;

                if (old_pwd != Supplier_SubAccount_Password)
                {
                    pub.Msg("info", "提示信息", "原密码输入错误，请重试！", false, "{back}");
                }

                if (MySubAccount.EditSupplierSubAccount(subAccountInfo))
                {
                    Response.Redirect("/Supplier/account_password.aspx?tip=success");
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
    //public string DisplaySubAccountPrivilege(string privilege)
    //{
    //    StringBuilder strHTML = new StringBuilder();
    //    List<string> oTempList = new List<string>(privilege.Split(','));

    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"1\" id=\"checkbox\"  class=\"input03\"");
    //    if (oTempList.Contains("1"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>我的订单</span>");

    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"2\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("2"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>我的发货单</span>");

    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"3\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("3"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>数据统计</span>");

    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"4\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("4"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>店铺基础设置</span>");

    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"5\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("5"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>我的供应商评价</span>");

    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"6\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("6"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>我的商品评价</span>");

    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"7\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("7"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>中信支付管理</span>");

    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"8\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("8"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>中信申请出金</span>");

    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"9\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("9"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>中信交易流水查询</span>");

    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"10\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("10"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>开通店铺</span>");

    //    //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"11\" id=\"checkbox\" class=\"input03\"");
    //    //if (oTempList.Contains("11"))
    //    //{
    //    //    strHTML.Append(" checked=\"checked\" ");
    //    //}
    //    //strHTML.Append("/>报价留言</span>");

    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"12\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("12"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>模版管理</span>");

    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"13\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("13"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>店铺基础设置</span>");

    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"14\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("14"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>公司介绍</span>");

    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"15\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("15"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>联系我们</span>");

    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"16\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("16"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>店铺关闭申请</span>");

    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"17\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("17"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>栏目管理</span>");

    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"18\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("18"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>自定义模块</span>");

    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"19\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("19"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>商品标签管理</span>");

    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"20\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("20"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>活动公告管理</span>");

    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"21\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("21"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>报价留言</span>");

    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"22\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("22"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>咨询管理</span>");

    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"23\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("23"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>在线客服</span>");


    //    #region 新补充商家权限
    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"24\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("24"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>发布商品</span>");


    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"25\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("25"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>管理商品</span>");


    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"26\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("26"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>商品SEO管理</span>");



    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"27\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("27"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>店内分类管理</span>");




    //    //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"28\" id=\"checkbox\" class=\"input03\"");
    //    //if (oTempList.Contains("28"))
    //    //{
    //    //    strHTML.Append(" checked=\"checked\" ");
    //    //}
    //    //strHTML.Append("/>限时促销管理</span>");




    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"29\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("29"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>创建子账户</span>");




    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"30\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("30"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>管理子账户</span>");




    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"31\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("31"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>保证金管理</span>");





    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"32\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("32"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>充值保证金</span>");




    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"33\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("33"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>消息通知</span>");




    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"34\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("34"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>资料管理</span>");




    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"35\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("35"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>添加拍卖</span>");





    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"36\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("36"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>拍卖列表</span>");




    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"37\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("37"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>招标管理</span>");



    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"39\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("39"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>我的投标报价</span>");




    //    strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"40\" id=\"checkbox\" class=\"input03\"");
    //    if (oTempList.Contains("40"))
    //    {
    //        strHTML.Append(" checked=\"checked\" ");
    //    }
    //    strHTML.Append("/>物流列表</span>");

    //    #endregion


    //    return strHTML.ToString();
    //}

    /// <summary>
    /// 子账户权限
    /// </summary>
    /// <param name="privilege"></param>
    /// <returns></returns>
    public string DisplaySubAccountPrivilege(string privilege)
    {
        StringBuilder strHTML = new StringBuilder();
        List<string> oTempList = new List<string>(privilege.Split(','));

        strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"1\" id=\"checkbox\"  class=\"input03\"");
        if (oTempList.Contains("1"))
        {
            strHTML.Append(" checked=\"checked\" ");
        }
        strHTML.Append("/>采购管理</span>");




        strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"2\" id=\"checkbox\" class=\"input03\"");
        if (oTempList.Contains("2"))
        {
            strHTML.Append(" checked=\"checked\" ");
        }
        strHTML.Append("/>卖家管理</span>");

        strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"3\" id=\"checkbox\" class=\"input03\"");
        if (oTempList.Contains("3"))
        {
            strHTML.Append(" checked=\"checked\" ");
        }
        strHTML.Append("/>招标管理</span>");

        strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"4\" id=\"checkbox\" class=\"input03\"");
        if (oTempList.Contains("4"))
        {
            strHTML.Append(" checked=\"checked\" ");
        }
        strHTML.Append("/>拍卖管理</span>");

        strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"5\" id=\"checkbox\" class=\"input03\"");
        if (oTempList.Contains("5"))
        {
            strHTML.Append(" checked=\"checked\" ");
        }
        strHTML.Append("/>店铺管理</span>");


        strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"6\" id=\"checkbox\" class=\"input03\"");
        if (oTempList.Contains("6"))
        {
            strHTML.Append(" checked=\"checked\" ");
        }
        strHTML.Append("/>财务管理</span>");

        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"6\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("6"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>我的商品评价</span>");

        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"7\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("7"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>中信支付管理</span>");

        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"8\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("8"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>中信申请出金</span>");

        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"9\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("9"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>中信交易流水查询</span>");

        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"10\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("10"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>开通店铺</span>");

        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"12\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("12"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>模版管理</span>");

        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"13\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("13"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>店铺基础设置</span>");

        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"14\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("14"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>公司介绍</span>");

        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"15\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("15"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>联系我们</span>");

        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"16\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("16"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>店铺关闭申请</span>");

        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"17\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("17"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>栏目管理</span>");

        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"18\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("18"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>自定义模块</span>");

        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"19\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("19"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>商品标签管理</span>");

        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"20\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("20"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>活动公告管理</span>");

        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"21\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("21"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>报价留言</span>");

        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"22\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("22"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>咨询管理</span>");

        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"23\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("23"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>在线客服</span>");


        //#region 新补充商家权限
        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"24\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("24"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>发布商品</span>");


        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"25\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("25"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>管理商品</span>");


        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"26\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("26"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>商品SEO管理</span>");



        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"27\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("27"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>店内分类管理</span>");




        ////strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"28\" id=\"checkbox\" class=\"input03\"");
        ////if (oTempList.Contains("28"))
        ////{
        ////    strHTML.Append(" checked=\"checked\" ");
        ////}
        ////strHTML.Append("/>限时促销管理</span>");




        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"29\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("29"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>创建子账户</span>");




        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"30\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("30"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>管理子账户</span>");




        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"31\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("31"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>保证金管理</span>");





        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"32\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("32"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>充值保证金</span>");




        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"33\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("33"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>消息通知</span>");




        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"34\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("34"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>资料管理</span>");




        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"35\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("35"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>添加拍卖</span>");





        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"36\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("36"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>拍卖列表</span>");




        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"37\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("37"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>招标管理</span>");



        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"39\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("39"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>我的投标报价</span>");




        //strHTML.Append("<span style=\"float:left; width:120px;\"><input type=\"checkbox\" name=\"Supplier_SubAccount_Privilege\" value=\"40\" id=\"checkbox\" class=\"input03\"");
        //if (oTempList.Contains("40"))
        //{
        //    strHTML.Append(" checked=\"checked\" ");
        //}
        //strHTML.Append("/>物流列表</span>");

        //#endregion


        return strHTML.ToString();
    }

    #endregion

    #region 采购申请

    public void Show_PurchaseReply_Dialog()
    {
        StringBuilder str = new StringBuilder();

        int Purchase_ID = tools.CheckInt(Request["Reply_PurchaseID"]);


        str.Append("<link href=\"/css/index.css\" rel=\"stylesheet\" type=\"text/css\" />");
        str.Append("<script type=\"text/javascript\" src=\"/scripts/jquery-1.8.3.js\"></script>");
        str.Append("<script type=\"text/javascript\" src=\"/scripts/layer/layer.js\"></script>");
        str.Append("<script type=\"text/javascript\" src=\"/scripts/supplier.js\"></script>");




        str.Append("<div class=\"content02\" style=\"background-color: #FFF; width: 800px;\">");
        str.Append("<div class=\"content02_main\" style=\"background-color: #FFF; width: 800px;\">");
        str.Append("<div class=\"partc\">");
        str.Append("<div class=\"pc_right\" style=\"width: 800px;\">");
        str.Append("<div class=\"blk17\" style=\"border:0\">");
        str.Append("<form name=\"frm_reply\" id=\"frm_reply\" method=\"post\" action=\"/supplier/merchants_do.aspx\">");

        str.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" class=\"table_padding_5\">");
        str.Append("<tr>");
        str.Append("<td width=\"92\" class=\"name\">标题</td>");
        str.Append("<td width=\"708\">");
        str.Append("<input name=\"Reply_Title\" type=\"text\" id=\"Reply_Title\" style=\"width: 300px;\" class=\"input01\" /><i>*</i>");
        str.Append("</td>");
        str.Append("</tr>");
        str.Append("<tr>");
        str.Append("<td width=\"92\" class=\"name\">内容</td>");
        str.Append("<td width=\"708\">");
        str.Append("<textarea name=\"Reply_Content\" id=\"Reply_Content\" cols=\"80\" rows=\"5\"></textarea><i>*</i>");
        str.Append("</td>");
        str.Append("</tr>");
        str.Append("<tr>");
        str.Append("<td width=\"92\" class=\"name\">姓名</td>");
        str.Append("<td width=\"708\">");
        str.Append("<input name=\"Reply_Contactman\" type=\"text\" id=\"Reply_Contactman\" style=\"width: 300px;\" class=\"input01\" /><i>*</i>");
        str.Append("</td>");
        str.Append("</tr>");
        str.Append("<tr>");
        str.Append("<td width=\"92\" class=\"name\">手机</td>");
        str.Append("<td width=\"801\">");
        str.Append("<input name=\"Reply_Mobile\" type=\"text\" id=\"Reply_Mobile\" style=\"width: 300px;\" class=\"input01\" /><i>*</i>");
        str.Append("</td>");
        str.Append("</tr>");
        str.Append("<tr>");
        str.Append("<td width=\"92\" class=\"name\">Email</td>");
        str.Append("<td width=\"708\">");
        str.Append("<input name=\"Reply_Email\" type=\"text\" id=\"Reply_Email\" style=\"width: 300px;\" class=\"input01\" />");
        str.Append("</td>");
        str.Append("</tr>");
        str.Append("<tr>");
        str.Append("<td width=\"92\" class=\"name\">&nbsp;</td>");
        str.Append("<td width=\"708\">");
        str.Append("<span class=\"table_v_title\">");
        str.Append("<input name=\"action\" type=\"hidden\" id=\"action\" value=\"purchasereply\" />");
        str.Append("<input name=\"Reply_PurchaseID\" id=\"Reply_PurchaseID\" type=\"hidden\" value=\"" + Purchase_ID + "\" />");
        str.Append("<a href=\"javascript:void(0);\" onclick=\"PurchaseReply_Add();\" class=\"a11\">保 存</a>");
        str.Append("</span>");
        str.Append("</td>");
        str.Append("</tr>");
        str.Append("</table>");
        str.Append("</form>");
        str.Append("</div>");
        str.Append("</div>");
        str.Append("<div class=\"clear\">");
        str.Append("</div>");
        str.Append("</div>");
        str.Append("</div>");
        str.Append("</div>");

        Response.Write(str.ToString());
    }

    public void AddPurchaseReply()
    {
        int Reply_ID = tools.CheckInt(Request.Form["Reply_ID"]);
        int Reply_PurchaseID = tools.CheckInt(Request.Form["Reply_PurchaseID"]);
        int Reply_SupplierID = tools.NullInt(Session["supplier_id"]);
        string Reply_Title = tools.CheckStr(Request.Form["Reply_Title"]);
        string Reply_Content = tools.CheckStr(Request.Form["Reply_Content"]);
        string Reply_Contactman = tools.CheckStr(Request.Form["Reply_Contactman"]);
        string Reply_Mobile = tools.CheckStr(Request.Form["Reply_Mobile"]);
        string Reply_Email = tools.CheckStr(Request.Form["Reply_Email"]);
        DateTime Reply_Addtime = DateTime.Now;

        if (Reply_Title == "")
        {
            Response.Write("请填写报价标题");
            Response.End();
        }

        if (Reply_Contactman == "")
        {
            Response.Write("请填写报价内容");
            Response.End();
        }

        if (Reply_Contactman == "")
        {
            Response.Write("请填写联系人姓名");
            Response.End();
        }


        if (Reply_Mobile == "")
        {
            Response.Write("请填写手机号码");
            Response.End();
        }
        else
        {
            if (!pub.Checkmobile(Reply_Mobile))
            {
                Response.Write("手机格式不正确");
                Response.End();
            }
        }



        MemberPurchaseReplyInfo entity = new MemberPurchaseReplyInfo();
        entity.Reply_ID = Reply_ID;
        entity.Reply_PurchaseID = Reply_PurchaseID;
        entity.Reply_SupplierID = Reply_SupplierID;
        entity.Reply_Title = Reply_Title;
        entity.Reply_Content = Reply_Content;
        entity.Reply_Contactman = Reply_Contactman;
        entity.Reply_Mobile = Reply_Mobile;
        entity.Reply_Email = Reply_Email;
        entity.Reply_Addtime = Reply_Addtime;

        if (MyPurchaseReply.AddMemberPurchaseReply(entity))
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

    public void Supplier_PurchaseReply_List()
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
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
        Response.Write("  <td width=\"200\" class=\"name\">标题</td>");
        Response.Write("  <td width=\"363\" class=\"name\">内容</td>");
        Response.Write("  <td width=\"200\" class=\"name\">姓名</td>");
        Response.Write("  <td width=\"200\" class=\"name\">手机</td>");
        Response.Write("</tr>");
        string productURL = string.Empty;
        string parentname = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberPurchaseReplyInfo.Reply_SupplierID", "=", supplier_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("MemberPurchaseReplyInfo.Reply_ID", "Asc"));
        IList<MemberPurchaseReplyInfo> entitys = MyPurchaseReply.GetMemberPurchaseReplys(Query);
        PageInfo page = MyPurchaseReply.GetPageInfo(Query);
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
                Response.Write("<td>" + entity.Reply_Content + "</td>");
                Response.Write("<td>" + entity.Reply_Contactman + "</td>");
                Response.Write("<td>" + entity.Reply_Mobile + "</td>");
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

    //商家报价留言
    public int Supplier_PurchaseReply_Num()
    {
        int supplier_id = tools.CheckInt(Session["supplier_id"].ToString());
        int Supplier_PurchageReply_Num = tools.CheckInt(DBHelper.ExecuteScalar("select count(*) from Member_Purchase_Reply where Reply_SupplierID=" + supplier_id + "").ToString());
        //int i = 0;
        //string Pageurl;
        //int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        //Pageurl = "?action=list";
        //if (curpage < 1)
        //{
        //    curpage = 1;
        //}

        //string strBG = "";


        //string productURL = string.Empty;
        //string parentname = "";
        //QueryInfo Query = new QueryInfo();
        //Query.PageSize = 20;
        //Query.CurrentPage = curpage;
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberPurchaseReplyInfo.Reply_SupplierID", "=", supplier_id.ToString()));
        //Query.OrderInfos.Add(new OrderInfo("MemberPurchaseReplyInfo.Reply_ID", "Asc"));
        //IList<MemberPurchaseReplyInfo> entitys = MyPurchaseReply.GetMemberPurchaseReplys(Query);
        //PageInfo page = MyPurchaseReply.GetPageInfo(Query);
        //if (entitys != null)
        //{
        //    foreach (MemberPurchaseReplyInfo entity in entitys)
        //    {
        //        i++;
        //    }
        //}
        return Supplier_PurchageReply_Num;
    }






    //获取采购申请分类
    public SupplierPurchaseCategoryInfo GetSupplierPurchaseCategoryByID(int id)
    {
        return MyPurchaseCategory.GetSupplierPurchaseCategoryByID(id, pub.CreateUserPrivilege("7eccd02b-0624-4add-a11f-ce21d405a1d5"));
    }

    //采购申请分类选择
    public string Purchase_Category_Select(int cate_id, string div_name)
    {
        string select_list = "";
        string select_tmp = "";
        int grade = 0;
        int i;
        int parentid = 0;
        string select_name = "";
        string cate_relate = Get_SupplierPurchaseCategory_Relate(cate_id, "");
        cate_relate = cate_relate + ",";
        foreach (string cate in cate_relate.Split(','))
        {
            if (cate.Length > 0)
            {

                QueryInfo Query = new QueryInfo();
                Query.CurrentPage = 1;
                Query.PageSize = 0;
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseCategoryInfo.Cate_ParentID", "=", cate));
                Query.OrderInfos.Add(new OrderInfo("SupplierPurchaseCategoryInfo.Cate_Sort", "Asc"));
                IList<SupplierPurchaseCategoryInfo> categorys = MyPurchaseCategory.GetSupplierPurchaseCategorys(Query, pub.CreateUserPrivilege("7eccd02b-0624-4add-a11f-ce21d405a1d5"));
                if (categorys != null)
                {

                    grade = grade + 1;
                    if (grade == 1)
                    {
                        select_tmp = "<select id=\"Purchase_Cate\" name=\"Purchase_Cate\" onchange=\"change_mainpurchasecate('" + div_name + "','Purchase_Cate');\">";
                        select_tmp = select_tmp + "<option value=\"0\">选择类别</option>";
                    }
                    else
                    {
                        select_name = "Purchase_Cate";
                        for (i = 1; i < grade; i++)
                        {
                            select_name = select_name + "_parent";
                        }
                        select_tmp = "<select id=\"" + select_name + "\" name=\"" + select_name + "\" onchange=\"change_mainpurchasecate('" + div_name + "','" + select_name + "');\">";
                        select_tmp = select_tmp + "<option value=\"0\">选择类别</option>";
                    }

                    foreach (SupplierPurchaseCategoryInfo entity in categorys)
                    {
                        if (entity.Cate_IsActive == 1)
                        {
                            if (parentid == entity.Cate_ID || cate_id == entity.Cate_ID)
                            {
                                select_tmp = select_tmp + "<option value=\"" + entity.Cate_ID + "\" selected>" + entity.Cate_Name + "</option>";
                            }
                            else
                            {
                                select_tmp = select_tmp + "<option value=\"" + entity.Cate_ID + "\">" + entity.Cate_Name + "</option>";
                            }
                        }
                    }
                    select_tmp = select_tmp + "</select> ";
                    parentid = tools.CheckInt(cate);
                }

                Query = null;
                categorys = null;
                select_list = select_tmp + select_list;
            }
        }
        return select_list;
    }

    public string Get_SupplierPurchaseCategory_Relate(int cate_id, string cate_str)
    {

        string cate_relate = cate_id.ToString();
        if (cate_id > 0)
        {
            SupplierPurchaseCategoryInfo category = MyPurchaseCategory.GetSupplierPurchaseCategoryByID(cate_id, pub.CreateUserPrivilege("7eccd02b-0624-4add-a11f-ce21d405a1d5"));
            if (category != null)
            {
                cate_relate = cate_relate + ",";
                cate_relate = cate_str + Get_SupplierPurchaseCategory_Relate(category.Cate_ParentID, cate_relate);
            }
            else
            {
                cate_relate = "0";
            }
        }
        else
        {
            if (cate_str != "")
            {
                cate_relate = cate_str + cate_relate;
            }
        }
        return cate_relate;

    }

    //采购申请状态
    public string SupplierPurchaseStatus(int status)
    {
        string resultstr = string.Empty;
        switch (status)
        {
            case 0:
                resultstr = "草稿"; break;
            case 1:
                resultstr = "未审核"; break;
            case 2:
                resultstr = "已审核"; break;
            case 3:
                resultstr = "审核失败"; break;
        }
        return resultstr;
    }

    //添加采购申请
    public void AddSupplierPurchase(int Purchase_Status)
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        int Purchase_TypeID = tools.CheckInt(Request["Purchase_TypeID"]);
        string Purchase_Title = tools.CheckStr(Request["Purchase_Title"]);
        string Purchase_DeliveryTime = tools.CheckStr(Request["Purchase_DeliveryTime"]);

        string Purchase_Address = tools.CheckStr(Request["Purchase_Address"]);
        string Purchase_Intro = tools.NullStr(Request["Purchase_Intro"]);
        string Purchase_ValidDate = tools.CheckStr(Request["Purchase_ValidDate"]);
        string Purchase_Attachment = tools.CheckStr(Request["Purchase_Attachment"]);
        int Purchase_Amount = tools.CheckInt(Request["Purchase_Amount"]);
        string Purchase_State = tools.CheckStr(Request["s_Purchase_State"]);
        string Purchase_City = tools.CheckStr(Request["s_Purchase_City"]);
        string Purchase_County = tools.CheckStr(Request["s_Purchase_County"]);

        int Purchase_Cate = tools.CheckInt(Request.Form["Purchase_Cate"]);
        if (Purchase_Cate == 0)
        {
            Purchase_Cate = tools.CheckInt(Request.Form["Purchase_Cate_parent"]);
        }
        if (Purchase_Title == "")
        {
            pub.Msg("info", "提示信息", "请填写采购标题", false, "{back}");
        }

        if (Purchase_Cate == 0)
        {
            pub.Msg("info", "提示信息", "请选择采购分类", false, "{back}");
        }
        if (Purchase_DeliveryTime == "")
        {
            pub.Msg("info", "提示信息", "请选择交货时间", false, "{back}");
        }
        if (Purchase_Address == "")
        {
            pub.Msg("info", "提示信息", "请填写交货详细地址", false, "{back}");
        }
        if (Purchase_ValidDate == "")
        {
            pub.Msg("info", "提示信息", "请填写有效期", false, "{back}");
        }

        if (Purchase_County == "")
        {
            pub.Msg("info", "提示信息", "请选择交货所属地区", false, "{back}");
        }


        bool buying = false;
        for (int i = 1; i <= Purchase_Amount; i++)
        {
            string Detail_Name = tools.CheckStr(Request["Detail_Name" + i]);
            string Detail_Spec = tools.CheckStr(Request["Detail_Spec" + i]);
            int Detail_Amount = tools.CheckInt(Request["Detail_Amount" + i]);
            double Detail_Price = tools.CheckFloat(Request["Detail_Price" + i]);
            if (Detail_Name.Length > 0 && Detail_Spec.Length > 0 && Detail_Amount > 0 && Detail_Price > 0)
            {
                buying = true; break;
            }
        }
        if (!buying)
        {
            pub.Msg("info", "提示信息", "请填写采购清单信息！", false, "{back}");
        }


        SupplierPurchaseInfo entity = new SupplierPurchaseInfo();

        entity.Purchase_SupplierID = supplier_id;
        entity.Purchase_TypeID = Purchase_TypeID;
        entity.Purchase_Title = Purchase_Title;
        entity.Purchase_Intro = Purchase_Intro;

        entity.Purchase_DeliveryTime = tools.NullDate(Purchase_DeliveryTime);
        entity.Purchase_ValidDate = tools.NullDate(Purchase_ValidDate);
        entity.Purchase_Addtime = DateTime.Now;

        entity.Purchase_State = Purchase_State;
        entity.Purchase_City = Purchase_City;
        entity.Purchase_County = Purchase_County;
        entity.Purchase_Address = Purchase_Address;
        entity.Purchase_Attachment = Purchase_Attachment;


        entity.Purchase_Site = pub.GetCurrentSite();
        entity.Purchase_Status = Purchase_Status;
        entity.Purchase_IsActive = 1;//非挂起
        entity.Purchase_ActiveReason = "";//
        entity.Purchase_Trash = 0;//非删除

        entity.Purchase_IsRecommend = 0;//不推荐
        entity.Purchase_IsPublic = 0;//非公开
        entity.Purchase_CateID = Purchase_Cate;
        entity.Purchase_SysUserID = 0;

        if (MyPurchase.AddSupplierPurchase(entity, pub.CreateUserPrivilege("59ac1c26-ba95-42c9-b418-fae8465f6e94")))
        {
            QueryInfo Query = new QueryInfo();
            Query.CurrentPage = 1;
            Query.PageSize = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_SupplierID", "=", supplier_id.ToString()));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_Title", "=", Purchase_Title));

            Query.OrderInfos.Add(new OrderInfo("SupplierPurchaseInfo.Purchase_ID", "Desc"));
            IList<SupplierPurchaseInfo> entitys = MyPurchase.GetSupplierPurchases(Query, pub.CreateUserPrivilege("c197743d-e397-4d11-b6fc-07d1d24aa774"));
            if (entitys != null)
            {
                for (int i = 1; i <= 5; i++)
                {
                    string Detail_Name = tools.CheckStr(Request["Detail_Name" + i]);
                    string Detail_Spec = tools.CheckStr(Request["Detail_Spec" + i]);
                    int Detail_Amount = tools.CheckInt(Request["Detail_Amount" + i]);
                    double Detail_Price = tools.CheckFloat(Request["Detail_Price" + i]);
                    if (Detail_Name.Length > 0 && Detail_Spec.Length > 0 && Detail_Amount > 0 && Detail_Price > 0)
                    {
                        SupplierPurchaseDetailInfo detailinfo = new SupplierPurchaseDetailInfo();
                        detailinfo.Detail_Name = Detail_Name;
                        detailinfo.Detail_Spec = Detail_Spec;
                        detailinfo.Detail_Amount = Detail_Amount;
                        detailinfo.Detail_Price = Detail_Price;
                        detailinfo.Detail_PurchaseID = entitys[0].Purchase_ID;
                        MyPurchaseDetail.AddSupplierPurchaseDetail(detailinfo);
                    }
                }

            }
            pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/Shopping_add.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "添加失败！", false, "{back}");
        }


    }

    //修改采购申请
    public void EditSupplierPurchase(int Purchase_Status)
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        int Purchase_TypeID = tools.CheckInt(Request["Purchase_TypeID"]);
        string Purchase_Title = tools.CheckStr(Request["Purchase_Title"]);
        string Purchase_DeliveryTime = tools.CheckStr(Request["Purchase_DeliveryTime"]);

        string Purchase_Address = tools.CheckStr(Request["Purchase_Address"]);
        string Purchase_Intro = tools.NullStr(Request["Purchase_Intro"]);
        string Purchase_ValidDate = tools.CheckStr(Request["Purchase_ValidDate"]);
        string Purchase_Attachment = tools.CheckStr(Request["Purchase_Attachment"]);
        int Purchase_Amount = tools.CheckInt(Request["Purchase_Amount"]);
        string Purchase_State = tools.CheckStr(Request["s_Purchase_State"]);
        string Purchase_City = tools.CheckStr(Request["s_Purchase_City"]);
        string Purchase_County = tools.CheckStr(Request["s_Purchase_County"]);

        int apply_id = tools.CheckInt(Request["apply_id"]);
        int Purchase_Cate = tools.CheckInt(Request.Form["Purchase_Cate"]);
        if (Purchase_Cate == 0)
        {
            Purchase_Cate = tools.CheckInt(Request.Form["Purchase_Cate_parent"]);
        }
        if (Purchase_Title == "")
        {
            pub.Msg("info", "提示信息", "请填写标题", false, "{back}");
        }
        if (Purchase_Cate == 0)
        {
            pub.Msg("info", "提示信息", "请选择采购分类", false, "{back}");
        }
        if (Purchase_DeliveryTime == "")
        {
            pub.Msg("info", "提示信息", "请选择交货时间", false, "{back}");
        }
        if (Purchase_Address == "")
        {
            pub.Msg("info", "提示信息", "请填写交货详细地址", false, "{back}");
        }
        if (Purchase_ValidDate == "")
        {
            pub.Msg("info", "提示信息", "请填写有效期", false, "{back}");
        }

        if (Purchase_County == "")
        {
            pub.Msg("info", "提示信息", "请选择交货所属地区", false, "{back}");
        }


        bool buying = false;
        for (int i = 1; i <= Purchase_Amount; i++)
        {
            string Detail_Name = tools.CheckStr(Request["Detail_Name" + i]);
            string Detail_Spec = tools.CheckStr(Request["Detail_Spec" + i]);
            int Detail_Amount = tools.CheckInt(Request["Detail_Amount" + i]);
            double Detail_Price = tools.CheckFloat(Request["Detail_Price" + i]);
            if (Detail_Name.Length > 0 && Detail_Spec.Length > 0 && Detail_Amount > 0 && Detail_Price > 0)
            {
                buying = true;
                break;
            }
        }
        if (!buying)
        {
            pub.Msg("info", "提示信息", "请填写采购清单信息！", false, "{back}");
        }


        SupplierPurchaseInfo entity = MyPurchase.GetSupplierPurchaseByID(apply_id, pub.CreateUserPrivilege("c197743d-e397-4d11-b6fc-07d1d24aa774"));
        if (entity != null)
        {
            if (entity.Purchase_SupplierID != supplier_id)
            {
                Response.Redirect("/supplier/Shopping_list.aspx");
            }
            entity.Purchase_TypeID = Purchase_TypeID;
            entity.Purchase_Title = Purchase_Title;
            entity.Purchase_Intro = Purchase_Intro;

            entity.Purchase_DeliveryTime = tools.NullDate(Purchase_DeliveryTime);
            entity.Purchase_ValidDate = tools.NullDate(Purchase_ValidDate);

            entity.Purchase_State = Purchase_State;
            entity.Purchase_City = Purchase_City;
            entity.Purchase_County = Purchase_County;
            entity.Purchase_Address = Purchase_Address;
            entity.Purchase_Attachment = Purchase_Attachment;

            entity.Purchase_CateID = Purchase_Cate;

            if (entity.Purchase_Status == 0 || entity.Purchase_Status == 3)
            {
                entity.Purchase_Status = Purchase_Status;
            }

            if (MyPurchase.EditSupplierPurchase(entity, pub.CreateUserPrivilege("aa55fc69-156e-45fe-84fa-f0df964cd3e0")))
            {
                MyPurchaseDetail.DelSupplierPurchaseDetailByPurchaseID(apply_id);
                for (int i = 1; i <= Purchase_Amount; i++)
                {
                    string Detail_Name = tools.CheckStr(Request["Detail_Name" + i]);
                    string Detail_Spec = tools.CheckStr(Request["Detail_Spec" + i]);
                    int Detail_Amount = tools.CheckInt(Request["Detail_Amount" + i]);
                    double Detail_Price = tools.CheckFloat(Request["Detail_Price" + i]);
                    if (Detail_Name.Length > 0 && Detail_Spec.Length > 0 && Detail_Amount > 0 && Detail_Price > 0)
                    {
                        SupplierPurchaseDetailInfo detailinfo = new SupplierPurchaseDetailInfo();
                        detailinfo.Detail_Name = Detail_Name;
                        detailinfo.Detail_Spec = Detail_Spec;
                        detailinfo.Detail_Amount = Detail_Amount;
                        detailinfo.Detail_Price = Detail_Price;
                        detailinfo.Detail_PurchaseID = apply_id;
                        MyPurchaseDetail.AddSupplierPurchaseDetail(detailinfo);
                    }
                }

                pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/Shopping_list.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "修改失败！", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "修改失败！", false, "{back}");
        }


    }

    //删除采购申请
    public void DelSupplierPurchase()
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        int apply_id = tools.NullInt(Request["apply_id"]);

        SupplierPurchaseInfo spinfo = MyPurchase.GetSupplierPurchaseByID(apply_id, pub.CreateUserPrivilege("c197743d-e397-4d11-b6fc-07d1d24aa774"));
        if (spinfo != null)
        {
            if (supplier_id == spinfo.Purchase_SupplierID)
            {

                if (spinfo.Purchase_Status == 2)
                {
                    pub.Msg("error", "错误信息", "发布中的采购不能删除！", false, "/supplier/Shopping_list.aspx");
                }

                MyPurchase.DelSupplierPurchase(apply_id, pub.CreateUserPrivilege("af3d7bc9-1182-4aea-9be5-a826be6a5615"));
                MyPurchaseDetail.DelSupplierPurchaseDetailByPurchaseID(apply_id);
                pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/Shopping_list.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "删除失败！", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "删除失败！", false, "{back}");
        }

    }

    //采购申请列表
    public void SupplierPurchase_List()
    {
        string keyword = tools.CheckStr(Request["applykeyword"]);
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        string paramUrl = "?action=list";
        string BgColor = "";

        if (curpage < 1)
        {
            curpage = 1;
        }


        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        //Response.Write("  <th align=\"center\" valign=\"middle\" width=\"30\"></th>");
        Response.Write("  <th align=\"center\" valign=\"middle\">采购标题</th>");
        Response.Write("  <th width=\"88\" align=\"center\" valign=\"middle\" >采购类型</th>");
        Response.Write("  <th width=\"88\" align=\"center\" valign=\"middle\" >采购分类</th>");
        //Response.Write("  <th width=\"150\" align=\"center\" valign=\"middle\">交货时间</th>");
        Response.Write("  <th width=\"80\" align=\"center\" valign=\"middle\">有效时间</th>");
        Response.Write("  <th width=\"80\" align=\"center\" valign=\"middle\">添加时间</th>");
        Response.Write("  <th width=\"80\" align=\"center\" valign=\"middle\">信息状态</th>");
        Response.Write("  <th width=\"80\" align=\"center\" valign=\"middle\">挂起状态</th>");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\">操作</th>");
        Response.Write("</tr>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_SupplierID", "=", supplier_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_Trash", "=", "0"));
        if (keyword != "")
        {
            paramUrl += "&applykeyword=" + keyword;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_Title", "like", keyword));
        }

        Query.OrderInfos.Add(new OrderInfo("SupplierPurchaseInfo.Purchase_ID", "Desc"));
        IList<SupplierPurchaseInfo> entitys = MyPurchase.GetSupplierPurchasesList(Query, pub.CreateUserPrivilege("c197743d-e397-4d11-b6fc-07d1d24aa774"));
        PageInfo page = MyPurchase.GetPageInfo(Query, pub.CreateUserPrivilege("c197743d-e397-4d11-b6fc-07d1d24aa774"));
        if (entitys != null)
        {
            foreach (SupplierPurchaseInfo entity in entitys)
            {
                if (BgColor == "#FFFFFF")
                {
                    BgColor = "#FFFFFF";
                }
                else
                {
                    BgColor = "#FFFFFF";
                }

                Response.Write("<tr bgcolor=\"" + BgColor + "\" height=\"35\">");
                //Response.Write("        <td align=\"center\"><input type=\"checkbox\" id=\"Purchase_ID\" name=\"Purchase_ID\" value=\"" + entity.Purchase_ID + "\" /></td>");
                Response.Write("<td align=\"center\" valign=\"middle\">" + entity.Purchase_Title + "</td>");
                Response.Write("<td align=\"center\" valign=\"middle\">" + pub.GetPurchaseType(entity.Purchase_TypeID) + "</td>");
                SupplierPurchaseCategoryInfo category = GetSupplierPurchaseCategoryByID(entity.Purchase_CateID);
                string cateName = " -- ";
                if (category != null)
                {
                    cateName = category.Cate_Name;
                }
                Response.Write("<td align=\"center\" valign=\"middle\">" + cateName + "</td>");
                //Response.Write("<td align=\"center\" valign=\"middle\">" + entity.Purchase_DeliveryTime.ToShortDateString() + "</td>");
                Response.Write("<td align=\"center\" valign=\"middle\">" + entity.Purchase_ValidDate.ToShortDateString() + "</td>");
                Response.Write("<td align=\"center\" valign=\"middle\">" + entity.Purchase_Addtime.ToShortDateString() + "</td>");
                Response.Write("<td align=\"center\" valign=\"middle\">" + SupplierPurchaseStatus(entity.Purchase_Status) + "</td>");
                if (entity.Purchase_IsActive == 0)
                {
                    Response.Write("<td align=\"center\" valign=\"middle\">已挂起</td>");
                }
                else
                {
                    Response.Write("<td align=\"center\" valign=\"middle\">正常</td>");
                }

                Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\">");
                if (entity.Purchase_Status != 2)
                {
                    Response.Write("<a href=\"/supplier/Shopping_edit.aspx?apply_id=" + entity.Purchase_ID + "\" class=\"a_t12_blue\">修改</a> ");
                    Response.Write("<a href=\"shopping_do.aspx?action=applydel&apply_id=" + entity.Purchase_ID + "\" class=\"a_t12_blue\">删除</a></td>");
                }
                else
                {
                    Response.Write("<a href=\"/supplier/Shopping_view.aspx?apply_id=" + entity.Purchase_ID + "\" class=\"a_t12_blue\">查看</a> ");

                    Response.Write("<a href=\"/supplier/MyReceivePriceReport_list.aspx?purchase_id=" + entity.Purchase_ID + "\" class=\"a_t12_blue\">查看报价</a></td>");
                }
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            Response.Write("<tr><td height=\"8\"></td></tr>");
            Response.Write("<tr><td align=\"right\">");
            Response.Write("<div class=\"page\">");
            pub.Page(page.PageCount, page.CurrentPage, paramUrl, page.PageSize, page.RecordCount);
            Response.Write("</div>");
            Response.Write("</td></tr>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#FFFFFF\">");
            Response.Write("<td colspan=\"9\" align=\"center\" height=\"35\">暂无记录</td>");
            Response.Write("</tr>");
        }
        Response.Write("</table>");

    }

    //收到的采购
    public void SupplierReceivePurchase_List()
    {
        string keyword = tools.CheckStr(Request["applykeyword"]);
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        string paramUrl = "?action=list";
        string BgColor = "";

        if (curpage < 1)
        {
            curpage = 1;
        }


        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        //Response.Write("  <th align=\"center\" valign=\"middle\" width=\"30\"></th>");
        Response.Write("  <th align=\"center\" valign=\"middle\">采购标题</th>");
        Response.Write("  <th width=\"88\" align=\"center\" valign=\"middle\" >采购类型</th>");
        Response.Write("  <th width=\"150\" align=\"center\" valign=\"middle\">交货时间</th>");
        Response.Write("  <th width=\"110\" align=\"center\" valign=\"middle\">报价状态</th>");
        //Response.Write("  <th width=\"110\" align=\"center\" valign=\"middle\">添加时间</th>");
        //Response.Write("  <th width=\"110\" align=\"center\" valign=\"middle\">信息状态</th>");
        //Response.Write("  <th width=\"88\" align=\"center\" valign=\"middle\" >挂起状态</th>");
        Response.Write("  <th width=\"110\" align=\"center\" valign=\"middle\">操作</th>");
        Response.Write("</tr>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_ID", "in", "select Purchase_Private_PurchaseID from Supplier_Purchase_Private where Purchase_Private_SupplierID=" + supplier_id));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_SupplierID", "<>", supplier_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_Trash", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_IsPublic", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_Status", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseInfo.Purchase_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_ValidDate", ">=", DateTime.Now.ToString("yyyy-MM-dd")));


        if (keyword != "")
        {
            paramUrl += "&applykeyword=" + keyword;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPurchaseInfo.Purchase_Title", "like", keyword));
        }

        Query.OrderInfos.Add(new OrderInfo("SupplierPurchaseInfo.Purchase_ID", "Desc"));
        IList<SupplierPurchaseInfo> entitys = MyPurchase.GetSupplierPurchasesList(Query, pub.CreateUserPrivilege("c197743d-e397-4d11-b6fc-07d1d24aa774"));
        PageInfo page = MyPurchase.GetPageInfo(Query, pub.CreateUserPrivilege("c197743d-e397-4d11-b6fc-07d1d24aa774"));
        if (entitys != null)
        {
            IList<SupplierPriceReportInfo> reportinfos;
            foreach (SupplierPurchaseInfo entity in entitys)
            {
                if (BgColor == "#FFFFFF")
                {
                    BgColor = "#FFFFFF";
                }
                else
                {
                    BgColor = "#FFFFFF";
                }

                Response.Write("<tr bgcolor=\"" + BgColor + "\" height=\"35\">");
                //Response.Write("        <td align=\"center\"><input type=\"checkbox\" id=\"Purchase_ID\" name=\"Purchase_ID\" value=\"" + entity.Purchase_ID + "\" /></td>");
                Response.Write("<td align=\"center\" valign=\"middle\">" + entity.Purchase_Title + "</td>");
                if (entity.Purchase_TypeID == 0)
                {
                    Response.Write("<td align=\"center\" valign=\"middle\">定制采购</td>");
                }
                else
                {
                    Response.Write("<td align=\"center\" valign=\"middle\">代理采购</td>");
                }
                Response.Write("<td align=\"center\" valign=\"middle\">" + entity.Purchase_DeliveryTime.ToShortDateString() + "</td>");
                //Response.Write("<td align=\"center\" valign=\"middle\">" + entity.Purchase_ValidDate.ToShortDateString() + "</td>");
                //Response.Write("<td align=\"center\" valign=\"middle\">" + entity.Purchase_Addtime + "</td>");
                //Response.Write("<td align=\"center\" valign=\"middle\">" + SupplierPurchaseStatus(entity.Purchase_Status) + "</td>");
                reportinfos = GetSupplierPriceReportByPurchaseID(entity.Purchase_ID);
                if (reportinfos == null)
                {
                    Response.Write("<td align=\"center\" valign=\"middle\">未报价</td>");
                }
                else
                {
                    Response.Write("<td align=\"center\" valign=\"middle\">已报价</td>");
                }

                Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\">");
                //if (reportinfos == null)
                //{
                Response.Write("<a href=\"/supplier/Shopping_PriceReport.aspx?apply_id=" + entity.Purchase_ID + "\" class=\"a_t12_blue\">报价</a> ");
                //}
                //else
                //{
                //    Response.Write("<a href=\"/supplier/MyPriceReportDetail.aspx?PriceReport_ID=" + reportinfos[0].PriceReport_ID + "\" class=\"a_t12_blue\">查看</a> ");
                //}
                Response.Write("</td></tr>");
            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            Response.Write("<tr><td height=\"8\"></td></tr>");
            Response.Write("<tr><td align=\"right\">");
            Response.Write("<div class=\"page\">");
            pub.Page(page.PageCount, page.CurrentPage, paramUrl, page.PageSize, page.RecordCount);
            Response.Write("</div>");
            Response.Write("</td></tr>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#FFFFFF\">");
            Response.Write("<td colspan=\"9\" align=\"center\" height=\"35\">暂无记录</td>");
            Response.Write("</tr>");
        }
        Response.Write("</table>");

    }


    /// <summary>
    /// 根据采购ID 获取采购信息
    /// </summary>
    /// <param name="id">采购ID</param>
    /// <returns></returns>
    public SupplierPurchaseInfo GetSupplierPurchaseByID(int id)
    {
        return MyPurchase.GetSupplierPurchaseByID(id, pub.CreateUserPrivilege("c197743d-e397-4d11-b6fc-07d1d24aa774"));
    }

    /// <summary>
    /// 采购明细
    /// </summary>
    /// <param name="applyid">采购ID</param>
    /// <returns></returns>
    public IList<SupplierPurchaseDetailInfo> GetSupplierPurchaseDetailsByApplyID(int applyid)
    {
        return MyPurchaseDetail.GetSupplierPurchaseDetailsByPurchaseID(applyid);
    }

    /// <summary>
    /// 采购明细
    /// </summary>
    /// <param name="id">采购明细ID</param>
    /// <returns></returns>
    public SupplierPurchaseDetailInfo GetSupplierPurchaseDetailByID(int id)
    {
        return MyPurchaseDetail.GetSupplierPurchaseDetailByID(id);
    }

    //采购申请添加报价信息
    public void SupplierPurchase_PriceReport()
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);

        int Purchase_Amount = tools.CheckInt(Request["Purchase_Amount"]);

        int apply_id = tools.CheckInt(Request["apply_id"]);


        string PriceReport_Title = tools.CheckStr(Request["PriceReport_Title"]);
        string PriceReport_Name = tools.CheckStr(Request["PriceReport_Name"]);
        string PriceReport_Phone = tools.CheckStr(Request["PriceReport_Phone"]);
        string PriceReport_DeliveryDate = tools.CheckStr(Request["PriceReport_DeliveryDate"]);
        string PriceReport_Note = tools.CheckStr(Request["PriceReport_Note"]);

        //if (PriceReport_Title == "")
        //{
        //    pub.Msg("info", "提示信息", "请填写报价标题", false, "{back}");
        //}
        if (PriceReport_Name == "")
        {
            pub.Msg("info", "提示信息", "请填写联系人名称", false, "{back}");
        }
        if (!pub.Checkmobile(PriceReport_Phone))
        {
            pub.Msg("info", "提示信息", "手机格式不正确", false, "{back}");
        }

        if (PriceReport_DeliveryDate == "")
        {
            pub.Msg("info", "提示信息", "请选择期望交货时间", false, "{back}");
        }



        for (int i = 1; i < Purchase_Amount; i++)
        {
            int Detail_Amount = tools.CheckInt(Request["Detail_Amount" + i]);
            double Detail_Price = tools.CheckFloat(Request["Detail_Price" + i]);

            if (Detail_Amount == 0 || Detail_Price == 0)
            {
                pub.Msg("info", "提示信息", "商品报价信息填写不完整", false, "{back}");
            }



        }
        SupplierPurchaseInfo entity = MyPurchase.GetSupplierPurchaseByID(apply_id, pub.CreateUserPrivilege("c197743d-e397-4d11-b6fc-07d1d24aa774"));
        if (entity != null)
        {
            if (entity.Purchase_SupplierID == supplier_id)
            {
                Response.Redirect("/Purchase/index.aspx");
            }
            if (entity.Purchase_Status != 2 || entity.Purchase_IsActive != 1 || entity.Purchase_Trash != 0)
            {
                pub.Msg("error", "错误信息", "无效的采购信息！", false, "/Purchase/index.aspx");
            }
            if ((entity.Purchase_ValidDate - DateTime.Now).Days < 0)
            {
                pub.Msg("error", "错误信息", "无效的采购信息！", false, "/Purchase/index.aspx");
            }
            if (entity.Purchase_IsPublic == 0)
            {
                if (!CheckSupplierPurchasePrivates(entity.Purchase_ID, supplier_id))
                {
                    pub.Msg("info", "提示信息", "无效的采购信息", true, "/Purchase/index.aspx");
                }
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "无效的采购信息！", false, "/index.aspx");
        }


        DateTime now = DateTime.Now;

        SupplierPriceReportInfo reportinfo = new SupplierPriceReportInfo();
        reportinfo.PriceReport_Title = PriceReport_Title;
        reportinfo.PriceReport_Name = PriceReport_Name;
        reportinfo.PriceReport_Phone = PriceReport_Phone;
        reportinfo.PriceReport_DeliveryDate = tools.NullDate(PriceReport_DeliveryDate);
        reportinfo.PriceReport_AddTime = now;
        reportinfo.PriceReport_ReplyTime = now;
        reportinfo.PriceReport_ReplyContent = "";
        reportinfo.PriceReport_MemberID = supplier_id;
        reportinfo.PriceReport_IsReply = 0;
        reportinfo.PriceReport_AuditStatus = 0;
        reportinfo.PriceReport_PurchaseID = apply_id;
        reportinfo.PriceReport_Note = PriceReport_Note;

        if (MyPriceReport.AddSupplierPriceReport(reportinfo, pub.CreateUserPrivilege("b482df13-2941-4314-9200-b64db8b358bc")))
        {
            QueryInfo Query = new QueryInfo();
            Query.CurrentPage = 1;
            Query.PageSize = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPriceReportInfo.PriceReport_MemberID", "=", supplier_id.ToString()));
            //Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPriceReportInfo.PriceReport_Title", "like", PriceReport_Title));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPriceReportInfo.PriceReport_PurchaseID", "=", apply_id.ToString()));

            Query.OrderInfos.Add(new OrderInfo("SupplierPriceReportInfo.PriceReport_ID", "Desc"));
            IList<SupplierPriceReportInfo> entitys = MyPriceReport.GetSupplierPriceReports(Query, pub.CreateUserPrivilege("6a12664e-4eeb-4259-b7b5-904044194067"));
            if (entitys != null)
            {
                for (int i = 1; i < Purchase_Amount; i++)
                {
                    int detail_id = tools.CheckInt(Request["detail_" + i]);
                    int Detail_Amount = tools.CheckInt(Request["Detail_Amount" + i]);
                    double Detail_Price = tools.CheckFloat(Request["Detail_Price" + i]);

                    if (detail_id != 0 && Detail_Amount != 0 && Detail_Price != 0)
                    {
                        SupplierPriceReportDetailInfo reportdetailinfo = new SupplierPriceReportDetailInfo();
                        reportdetailinfo.Detail_PurchaseDetailID = detail_id;
                        reportdetailinfo.Detail_PurchaseID = apply_id;
                        reportdetailinfo.Detail_PriceReportID = entitys[0].PriceReport_ID;
                        reportdetailinfo.Detail_Price = Detail_Price;
                        reportdetailinfo.Detail_Amount = Detail_Amount;

                        MyPriceReportDetail.AddSupplierPriceReportDetail(reportdetailinfo, pub.CreateUserPrivilege("b482df13-2941-4314-9200-b64db8b358bc"));
                    }

                }
            }

            pub.Msg("positive", "操作成功", "操作成功", true, "/Purchase/detail.aspx?apply_id=" + apply_id);
        }
        else
        {
            pub.Msg("error", "错误信息", "报价失败！", false, "/index.aspx");
        }




    }

    /// <summary>
    /// 非公开采购
    /// 检查是否存在报价权限
    /// </summary>
    /// <param name="purchase_id"></param>
    /// <param name="supplier_id"></param>
    /// <returns>存在true</returns>
    public bool CheckSupplierPurchasePrivates(int purchase_id, int supplier_id)
    {
        return MyPurchase.GetSupplierPurchasePrivatesByPurchaseSupplier(purchase_id, supplier_id);
    }

    //代理采购协议列表
    public void SupplierAgentProtocal_List()
    {
        string keyword = tools.CheckStr(Request["applykeyword"]);
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        int curpage = tools.CheckInt(Request["page"]);
        int Protocal_Status = tools.CheckInt(Request["Protocal_Status"]);
        string paramUrl = "?action=list&Protocal_Status=" + Protocal_Status;
        string BgColor = "";

        if (curpage < 1)
        {
            curpage = 1;
        }


        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        //Response.Write("  <th align=\"center\" valign=\"middle\" width=\"30\"></th>");
        Response.Write("  <th align=\"center\" valign=\"middle\">采购标题</th>");
        //Response.Write("  <th width=\"88\" align=\"center\" valign=\"middle\" >采购类型</th>");
        //Response.Write("  <th width=\"88\" align=\"center\" valign=\"middle\" >采购分类</th>");
        //Response.Write("  <th width=\"88\" align=\"center\" valign=\"middle\">交货时间</th>");
        Response.Write("  <th width=\"88\" align=\"center\" valign=\"middle\">协议状态</th>");
        Response.Write("  <th width=\"88\" align=\"center\" valign=\"middle\">创建时间</th>");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\">操作</th>");
        Response.Write("</tr>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = curpage;





        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierAgentProtocalInfo.Protocal_Site", "=", pub.GetCurrentSite()));

        if (keyword != "")
        {
            paramUrl += "&applykeyword=" + keyword;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierAgentProtocalInfo.Protocal_PurchaseID", "in", "select Purchase_ID from Supplier_Purchase where Purchase_Title like '%" + keyword + "%'"));
        }

        switch (Protocal_Status)
        {

            case 1:
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierAgentProtocalInfo.Protocal_Status", "=", "0"));
                break;
            case 2:
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierAgentProtocalInfo.Protocal_Status", "=", "1"));
                break;
            case 3:
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierAgentProtocalInfo.Protocal_Status", "=", "2"));
                break;

        }

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierAgentProtocalInfo.Protocal_SupplierID", "=", supplier_id.ToString()));



        Query.OrderInfos.Add(new OrderInfo("SupplierAgentProtocalInfo.Protocal_ID", "Desc"));
        IList<SupplierAgentProtocalInfo> entitys = MyAgentProtocal.GetSupplierAgentProtocals(Query, pub.CreateUserPrivilege("0aab7822-e327-4dcd-bc30-4cbf289067e4"));
        PageInfo page = MyAgentProtocal.GetPageInfo(Query, pub.CreateUserPrivilege("0aab7822-e327-4dcd-bc30-4cbf289067e4"));
        if (entitys != null)
        {
            foreach (SupplierAgentProtocalInfo entity in entitys)
            {
                if (BgColor == "#FFFFFF")
                {
                    BgColor = "#FFFFFF";
                }
                else
                {
                    BgColor = "#FFFFFF";
                }


                Response.Write("<tr bgcolor=\"" + BgColor + "\" height=\"35\">");
                //Response.Write("        <td align=\"center\"><input type=\"checkbox\" id=\"Purchase_ID\" name=\"Purchase_ID\" value=\"" + entity.Purchase_ID + "\" /></td>");

                SupplierPurchaseInfo purchaseinfo = MyPurchase.GetSupplierPurchaseByID(entity.Protocal_PurchaseID, pub.CreateUserPrivilege("c197743d-e397-4d11-b6fc-07d1d24aa774"));
                if (purchaseinfo != null)
                {
                    Response.Write("<td align=\"center\" valign=\"middle\">" + purchaseinfo.Purchase_Title + "</td>");
                }
                else
                {
                    Response.Write("<td align=\"center\" valign=\"middle\"> -- </td>");
                }
                Response.Write("<td align=\"center\" valign=\"middle\">" + ConvertAgentProtocalStatus(entity.Protocal_Status) + "</td>");
                Response.Write("<td align=\"center\" valign=\"middle\">" + entity.Protocal_Addtime.ToString("yyyy-MM-dd") + "</td>");

                Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\">");

                Response.Write("<a href=\"/supplier/agent_protocal_view.aspx?Protocal_ID=" + entity.Protocal_ID + "\" class=\"a_t12_blue\">查看</a></td>");

                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            Response.Write("<tr><td height=\"8\"></td></tr>");
            Response.Write("<tr><td align=\"right\">");
            Response.Write("<div class=\"page\">");
            pub.Page(page.PageCount, page.CurrentPage, paramUrl, page.PageSize, page.RecordCount);
            Response.Write("</div>");
            Response.Write("</td></tr>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#FFFFFF\">");
            Response.Write("<td colspan=\"9\" align=\"center\" height=\"35\">暂无记录</td>");
            Response.Write("</tr>");
        }
        Response.Write("</table>");

    }

    //代理采购合同状态
    public string ConvertAgentProtocalStatus(int status)
    {
        switch (status)
        {
            case 0:
                return "待确认";
            case 1:
                return "已确认";
            case 2:
                return "已完成";
            default:
                return " -- ";
        }
    }

    //获取代理采购协议
    public SupplierAgentProtocalInfo GetSupplierAgentProtocalByID(int ID)
    {
        return MyAgentProtocal.GetSupplierAgentProtocalByID(ID, pub.CreateUserPrivilege("0aab7822-e327-4dcd-bc30-4cbf289067e4"));
    }

    //代理采购协议审核
    public void AuditSupplierAgentProtocal()
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        int Protocal_ID = tools.CheckInt(Request["Protocal_ID"]);

        SupplierAgentProtocalInfo entity = GetSupplierAgentProtocalByID(Protocal_ID);
        if (entity != null)
        {
            if (supplier_id == entity.Protocal_SupplierID)
            {
                if (entity.Protocal_Status == 0)
                {
                    entity.Protocal_Status = 1;//用户确认
                }
                else
                {
                    pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "/supplier/agent_protocal.aspx");
                }
                if (MyAgentProtocal.EditSupplierAgentProtocal(entity, pub.CreateUserPrivilege("7abc095a-d322-4312-861c-aecb6088c3bb")))
                {
                    pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/agent_protocal.aspx");
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
        else
        {
            pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }

    }

    //获取代理协议信息
    public SupplierAgentProtocalInfo GetSupplierAgentProtocalByPurchaseID(int PurchaseID)
    {
        return MyAgentProtocal.GetSupplierAgentProtocalByPurchaseID(PurchaseID, pub.CreateUserPrivilege("0aab7822-e327-4dcd-bc30-4cbf289067e4"));
    }

    //采购申请商品列表
    public string Cart_Purchase_Goods_List(int Purchase_ID, int PriceReport_ID, bool ispreview)
    {

        StringBuilder strHTML = new StringBuilder();
        if (ispreview)
        {
            strHTML.Append("<table width=\"100%\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\">");
        }
        else
        {
            strHTML.Append("<table width=\"740\" border=\"0\" cellspacing=\"1\" cellpadding=\"0\">");
        }
        strHTML.Append("	<tr>");
        if (ispreview)
        {
            strHTML.Append("		<td width=\"50\" class=\"tit\"> 选择 </td>");
        }
        strHTML.Append("		<td width=\"233\" class=\"tit\"> 商品名称 </td>");
        strHTML.Append("		<td width=\"130\" class=\"tit\"> 规格/单位 </td>");
        strHTML.Append("		<td width=\"115\" class=\"tit\"> 采购单价 </td>");
        strHTML.Append("		<td width=\"115\" class=\"tit\"> 采购报价 </td>");
        strHTML.Append("		<td width=\"173\" class=\"tit\"> 供应商 </td>");
        strHTML.Append("		<td width=\"83\" class=\"tit\"> 商品数量 </td>");
        strHTML.Append("		<td width=\"83\" class=\"tit\"> 交货时间 </td>");
        strHTML.Append("	</tr>");

        int SupplyID = 0;
        double total_mktprice = 0;
        double total_price = 0;
        int total_coin = 0;

        //获取交货期
        SupplierPurchaseInfo purchaseinfo = GetSupplierPurchaseByID(Purchase_ID);
        if (purchaseinfo == null)
        {
            Response.Redirect("/supplier/MyReceivePriceReport_list.aspx");
        }
        //获取卖家名称信息
        SupplierPriceReportInfo pricereport = SupplierPriceReportByID(PriceReport_ID);
        if (pricereport != null)
        {
            SupplyID = pricereport.PriceReport_MemberID;
        }
        SupplierInfo sinfo = null;
        if (SupplyID > 0)
        {
            sinfo = GetSupplierByID(SupplyID);
        }
        string Supplier_name = "易耐产业金服";
        if (sinfo != null)
        {
            Supplier_name = sinfo.Supplier_CompanyName;
        }
        string Goods_ID = tools.CheckStr(Request["Goods_ID"]);
        if (Goods_ID.Length == 0)
        {
            Goods_ID = "0";
        }

        //获取采购清单信息
        IList<SupplierPriceReportDetailInfo> reports = GetSupplierPriceReportDetailsByPriceReportID(PriceReport_ID);
        IList<SupplierPurchaseDetailInfo> goodstmps;
        if (ispreview)
        {
            goodstmps = GetSupplierPurchaseDetailsByApplyID(Purchase_ID);
        }
        else
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseDetailInfo.Detail_PurchaseID", "=", Purchase_ID.ToString()));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPurchaseDetailInfo.Detail_ID", "in", Goods_ID.ToString()));
            Query.OrderInfos.Add(new OrderInfo("SupplierPurchaseDetailInfo.Detail_ID", "Desc"));
            goodstmps = MyPurchaseDetail.GetSupplierPurchaseDetails(Query);
        }
        if (goodstmps != null)
        {
            foreach (SupplierPurchaseDetailInfo entity in goodstmps)
            {
                if (reports != null)
                {
                    foreach (SupplierPriceReportDetailInfo report in reports)
                    {
                        if (report.Detail_PurchaseDetailID == entity.Detail_ID)
                        {
                            total_mktprice = total_mktprice + (report.Detail_Amount * report.Detail_Price);
                            total_price = total_price + (report.Detail_Amount * report.Detail_Price);
                            total_coin += pub.Get_Member_Coin((report.Detail_Amount * report.Detail_Price));

                            //开始检查优惠信息
                            Session["total_price"] = total_price;        //商品总价
                            Session["total_coin"] = total_coin;         //商品积分


                            strHTML.Append("	<tr>");
                            if (ispreview)
                            {
                                strHTML.Append("<td align=\"center\"><input type=\"checkbox\" name=\"Goods_ID\" value=\"" + entity.Detail_ID + "\" checked></td>");
                            }
                            else
                            {
                                strHTML.Append("<td align=\"center\" style=\"display:none;\"><input type=\"checkbox\" name=\"Goods_ID\" value=\"" + entity.Detail_ID + "\" checked></td>");
                            }
                            strHTML.Append("<td class=\"td6\" style=\"text-align:left;padding:0px 5px;line-height:20px;\">" + entity.Detail_Name + "</td>");
                            strHTML.Append("		<td>" + entity.Detail_Spec + "</td>");
                            strHTML.Append("		<td>" + pub.FormatCurrency(entity.Detail_Price) + " </td>");
                            strHTML.Append("		<td>" + pub.FormatCurrency(report.Detail_Price) + " </td>");
                            strHTML.Append("		<td>" + Supplier_name + "</td>");
                            strHTML.Append("		<td>" + report.Detail_Amount + "</td>");
                            strHTML.Append("		<td>" + purchaseinfo.Purchase_DeliveryTime.ToString("yyyy-MM-dd") + "</td>");
                            strHTML.Append("	</tr>");
                            break;
                        }
                    }
                }
                else
                {
                    Response.Redirect("/supplier/MyReceivePriceReport_list.aspx");
                }

            }
        }
        else
        {
            Response.Redirect("/supplier/MyReceivePriceReport_list.aspx");
        }
        strHTML.Append("</table>");
        return strHTML.ToString();
    }

    //订单总价
    public void My_Carttotalprice()
    {
        double total_price, delivery_fee, favor_feeprice, total_favprice;
        int total_coin;
        total_price = tools.CheckFloat(Session["total_price"].ToString());
        total_coin = tools.CheckInt(Session["total_coin"].ToString());

        Session["delivery_fee"] = 0; //运费
        //运费优惠
        Session["favor_fee"] = 0;
        Session["favor_policy_price"] = 0;  //优惠费用

        delivery_fee = tools.CheckFloat(Session["delivery_fee"].ToString());
        total_favprice = tools.CheckFloat(Session["favor_coupon_price"].ToString()) + tools.CheckFloat(Session["favor_policy_price"].ToString());
        favor_feeprice = 0;

        StringBuilder strHTML = new StringBuilder();

        total_price = total_price + delivery_fee - favor_feeprice - total_favprice;
        if (total_price < 0)
            total_price = 0;

        strHTML.Append("应付总额：<strong>" + pub.FormatCurrency(total_price) + "</strong>元");

        Response.Write(strHTML.ToString() + "<input type=\"hidden\" id=\"tmp_totalprice\" name=\"tmp_totalprice\" value=\"" + total_price + "\"><input type=\"hidden\" name=\"order_favor_couponid\" value=\"" + tools.NullStr(Session["order_favor_coupon"]) + "\">");
    }

    #endregion

    #region 询价\报价
    //我的询价
    public void SupplierPriceAsk_List()
    {
        // string keyword = tools.CheckStr(Request["applykeyword"]);
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        string BgColor = "";

        if (curpage < 1)
        {
            curpage = 1;
        }


        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <th align=\"center\" valign=\"middle\">产品名称</th>");
        Response.Write("  <th align=\"center\" valign=\"middle\" >询价标题</th>");
        Response.Write("  <th width=\"120\" align=\"center\" valign=\"middle\">询价时间</th>");
        Response.Write("  <th width=\"110\" align=\"center\" valign=\"middle\">联系人</th>");
        Response.Write("  <th width=\"110\" align=\"center\" valign=\"middle\">采购数量</th>");
        Response.Write("  <th width=\"88\" align=\"center\" valign=\"middle\" >采购单价</th>");
        Response.Write("  <th width=\"110\" align=\"center\" valign=\"middle\">操作</th>");
        Response.Write("</tr>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPriceAskInfo.PriceAsk_MemberID", "=", supplier_id.ToString()));
        //if (keyword != "")
        //{
        //    Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPriceAskInfo.PriceAsk_Title", "like", keyword));
        //}

        Query.OrderInfos.Add(new OrderInfo("SupplierPriceAskInfo.PriceAsk_ID", "Desc"));
        IList<SupplierPriceAskInfo> entitys = MyPriceAsk.GetSupplierPriceAsks(Query, pub.CreateUserPrivilege("249d2ad4-45f4-4945-8e78-d18c79053106"));
        PageInfo page = MyPriceAsk.GetPageInfo(Query, pub.CreateUserPrivilege("249d2ad4-45f4-4945-8e78-d18c79053106"));
        if (entitys != null)
        {
            foreach (SupplierPriceAskInfo entity in entitys)
            {
                if (BgColor == "#FFFFFF")
                {
                    BgColor = "#FFFFFF";
                }
                else
                {
                    BgColor = "#FFFFFF";
                }

                Response.Write("<tr bgcolor=\"" + BgColor + "\" height=\"35\">");
                ProductInfo productinfo = MyProduct.GetProductByID(entity.PriceAsk_ProductID, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
                if (productinfo != null)
                {
                    Response.Write("<td align=\"center\" valign=\"middle\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, productinfo.Product_ID.ToString()) + "\" target=\"_blank\">" + productinfo.Product_Name + "</a></td>");
                }
                else
                {
                    Response.Write("<td align=\"center\" valign=\"middle\"> -- </td>");
                }

                Response.Write("<td align=\"center\" valign=\"middle\">" + entity.PriceAsk_Title + "</td>");

                Response.Write("<td align=\"center\" valign=\"middle\">" + entity.PriceAsk_AddTime + "</td>");
                Response.Write("<td align=\"center\" valign=\"middle\">" + entity.PriceAsk_Name + "</td>");
                Response.Write("<td align=\"center\" valign=\"middle\">" + entity.PriceAsk_Quantity + "</td>");

                Response.Write("<td align=\"center\" valign=\"middle\">" + pub.FormatCurrency(entity.PriceAsk_Price) + "</td>");

                Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\"><a class=\"a_t12_blue\" href=\"/supplier/MyInquiryDetail.aspx?PriceAsk_ID=" + entity.PriceAsk_ID + "\">查看</a></td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            Response.Write("<tr><td height=\"8\"></td></tr>");
            Response.Write("<tr><td align=\"right\">");
            Response.Write("<div class=\"page\">");
            pub.Page(page.PageCount, page.CurrentPage, "?1=1", page.PageSize, page.RecordCount);
            Response.Write("</div>");
            Response.Write("</td></tr>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#FFFFFF\">");
            Response.Write("<td colspan=\"9\" align=\"center\" height=\"35\">暂无记录</td>");
            Response.Write("</tr>");
        }
        Response.Write("</table>");

    }

    /// <summary>
    /// 询价查询
    /// </summary>
    /// <param name="id">询价ID</param>
    /// <returns></returns>
    public SupplierPriceAskInfo SupplierPriceAskByID(int id)
    {
        return MyPriceAsk.GetSupplierPriceAskByID(id, pub.CreateUserPrivilege("249d2ad4-45f4-4945-8e78-d18c79053106"));
    }

    public IList<SupplierPriceReportInfo> GetSupplierPriceReportByPurchaseID(int Purchase_ID)
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPriceReportInfo.PriceReport_MemberID", "=", supplier_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPriceReportInfo.PriceReport_PurchaseID", "=", Purchase_ID.ToString()));

        Query.OrderInfos.Add(new OrderInfo("SupplierPriceReportInfo.PriceReport_ID", "Desc"));
        return MyPriceReport.GetSupplierPriceReports(Query, pub.CreateUserPrivilege("6a12664e-4eeb-4259-b7b5-904044194067"));
    }

    //我的报价 
    public void SupplierPriceReport_List()
    {
        string keyword = tools.CheckStr(Request["applykeyword"]);
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        string BgColor = "";

        if (curpage < 1)
        {
            curpage = 1;
        }


        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <th align=\"center\" valign=\"middle\">采购标题</th>");
        Response.Write("  <th width=\"120\" align=\"center\" valign=\"middle\" >采购人</th>");
        Response.Write("  <th width=\"120\" align=\"center\" valign=\"middle\">报价时间</th>");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\">联系人</th>");
        Response.Write("  <th width=\"80\" align=\"center\" valign=\"middle\">回复状态</th>");
        Response.Write("  <th width=\"80\" align=\"center\" valign=\"middle\" >审核状态</th>");
        Response.Write("  <th width=\"110\" align=\"center\" valign=\"middle\">操作</th>");
        Response.Write("</tr>");
        string supplier_name;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPriceReportInfo.PriceReport_MemberID", "=", supplier_id.ToString()));
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPriceReportInfo.PriceReport_Title", "like", keyword));
        }

        Query.OrderInfos.Add(new OrderInfo("SupplierPriceReportInfo.PriceReport_ID", "Desc"));
        IList<SupplierPriceReportInfo> entitys = MyPriceReport.GetSupplierPriceReports(Query, pub.CreateUserPrivilege("6a12664e-4eeb-4259-b7b5-904044194067"));
        PageInfo page = MyPriceReport.GetPageInfo(Query, pub.CreateUserPrivilege("6a12664e-4eeb-4259-b7b5-904044194067"));
        if (entitys != null)
        {
            SupplierInfo supplierinfo = null;
            foreach (SupplierPriceReportInfo entity in entitys)
            {
                if (BgColor == "#FFFFFF")
                {
                    BgColor = "#FFFFFF";
                }
                else
                {
                    BgColor = "#FFFFFF";
                }
                supplier_name = "易耐产业金服";
                Response.Write("<tr bgcolor=\"" + BgColor + "\" height=\"35\">");
                SupplierPurchaseInfo spinfo = MyPurchase.GetSupplierPurchaseByID(entity.PriceReport_PurchaseID, pub.CreateUserPrivilege("c197743d-e397-4d11-b6fc-07d1d24aa774"));
                if (spinfo != null)
                {
                    if (spinfo.Purchase_SupplierID > 0)
                    {
                        supplierinfo = GetSupplierByID(spinfo.Purchase_SupplierID);
                        if (supplierinfo != null)
                        {
                            supplier_name = supplierinfo.Supplier_CompanyName;
                        }
                        else
                        {
                            supplier_name = "--";
                        }
                    }
                    Response.Write("<td align=\"center\" valign=\"middle\"><a href=\"/Purchase/detail.aspx?apply_id=" + spinfo.Purchase_ID + "\" target=\"_blank\"><span>" + spinfo.Purchase_Title + "</span></a></td>");
                }
                else
                {
                    Response.Write("<td align=\"center\" valign=\"middle\"> -- </td>");
                }


                Response.Write("<td align=\"center\" valign=\"middle\">" + supplier_name + "</td>");

                Response.Write("<td align=\"center\" valign=\"middle\">" + entity.PriceReport_AddTime + "</td>");
                Response.Write("<td align=\"center\" valign=\"middle\">" + entity.PriceReport_Name + "</td>");
                Response.Write("<td align=\"center\" valign=\"middle\">" + (entity.PriceReport_IsReply == 1 ? "已回复" : "未回复") + "</td>");

                Response.Write("<td align=\"center\" valign=\"middle\">" + (entity.PriceReport_AuditStatus == 1 ? "已审核" : "未审核") + "</td>");

                Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\"><a href=\"/supplier/MyPriceReportDetail.aspx?PriceReport_ID=" + entity.PriceReport_ID + "\">查看</a></td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            Response.Write("<tr><td height=\"8\"></td></tr>");
            Response.Write("<tr><td align=\"right\">");
            Response.Write("<div class=\"page\">");
            pub.Page(page.PageCount, page.CurrentPage, "?1=1", page.PageSize, page.RecordCount);
            Response.Write("</div>");
            Response.Write("</td></tr>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#FFFFFF\">");
            Response.Write("<td colspan=\"9\" align=\"center\" height=\"35\">暂无记录</td>");
            Response.Write("</tr>");
        }
        Response.Write("</table>");

    }

    //我收到的报价 
    public void SupplierReceivePriceReport_List()
    {
        string keyword = tools.CheckStr(Request["applykeyword"]);
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        int Purchase_ID = tools.CheckInt(Request["Purchase_ID"]);

        string BgColor = "";

        if (curpage < 1)
        {
            curpage = 1;
        }

        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <th align=\"center\" valign=\"middle\">采购标题</th>");
        Response.Write("  <th align=\"center\" valign=\"middle\">采购类型</th>");
        //Response.Write("  <th align=\"center\" valign=\"middle\" >报价标题</th>");
        Response.Write("  <th width=\"120\" align=\"center\" valign=\"middle\">报价时间</th>");
        Response.Write("  <th width=\"100\" align=\"center\" valign=\"middle\">联系人</th>");
        Response.Write("  <th width=\"80\" align=\"center\" valign=\"middle\">回复状态</th>");
        Response.Write("  <th width=\"110\" align=\"center\" valign=\"middle\">操作</th>");
        Response.Write("</tr>");

        int purchase_type = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = curpage;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierPriceReportInfo.PriceReport_AuditStatus", "=", "1"));

        string sqlurl = "select Purchase_ID from Supplier_Purchase where Purchase_SupplierID=" + supplier_id + " AND Purchase_Trash = 0 and Purchase_TypeID={typeid}";

        if (Purchase_ID > 0)
            sqlurl += " AND Purchase_ID = " + Purchase_ID;

        Query.ParamInfos.Add(new ParamInfo("AND((", "int", "SupplierPriceReportInfo.PriceReport_MemberID", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND)", "int", "SupplierPriceReportInfo.PriceReport_PurchaseID", "in", sqlurl.Replace("{typeid}", "0")));
        Query.ParamInfos.Add(new ParamInfo("OR)", "int", "SupplierPriceReportInfo.PriceReport_PurchaseID", "in", sqlurl.Replace("{typeid}", "1")));

        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierPriceReportInfo.PriceReport_Title", "like", keyword));
        }

        Query.OrderInfos.Add(new OrderInfo("SupplierPriceReportInfo.PriceReport_ID", "Desc"));
        IList<SupplierPriceReportInfo> entitys = MyPriceReport.GetSupplierPriceReports(Query, pub.CreateUserPrivilege("6a12664e-4eeb-4259-b7b5-904044194067"));
        PageInfo page = MyPriceReport.GetPageInfo(Query, pub.CreateUserPrivilege("6a12664e-4eeb-4259-b7b5-904044194067"));
        if (entitys != null)
        {
            foreach (SupplierPriceReportInfo entity in entitys)
            {
                purchase_type = 0;
                if (BgColor == "#FFFFFF")
                {
                    BgColor = "#FFFFFF";
                }
                else
                {
                    BgColor = "#FFFFFF";
                }

                Response.Write("<tr bgcolor=\"" + BgColor + "\" height=\"35\">");
                SupplierPurchaseInfo spinfo = MyPurchase.GetSupplierPurchaseByID(entity.PriceReport_PurchaseID, pub.CreateUserPrivilege("c197743d-e397-4d11-b6fc-07d1d24aa774"));
                if (spinfo != null)
                {
                    purchase_type = spinfo.Purchase_TypeID;
                    Response.Write("<td align=\"center\" valign=\"middle\"><span>" + spinfo.Purchase_Title + "</span></td>");
                    if (spinfo.Purchase_TypeID == 0)
                    {
                        Response.Write("<td align=\"center\" valign=\"middle\"><span>定制采购</span></td>");
                    }
                    else
                    {
                        Response.Write("<td align=\"center\" valign=\"middle\"><span>代理采购</span></td>");
                    }
                }
                else
                {
                    Response.Write("<td align=\"center\" valign=\"middle\"> -- </td>");
                    Response.Write("<td align=\"center\" valign=\"middle\"> -- </td>");
                }

                //Response.Write("<td align=\"center\" valign=\"middle\">" + entity.PriceReport_Title + "</td>");

                Response.Write("<td align=\"center\" valign=\"middle\">" + entity.PriceReport_AddTime + "</td>");
                Response.Write("<td align=\"center\" valign=\"middle\">" + entity.PriceReport_Name + "</td>");
                Response.Write("<td align=\"center\" valign=\"middle\">" + (entity.PriceReport_IsReply == 1 ? "已回复" : "未回复") + "</td>");
                Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\">");
                //OrdersInfo ordersinfo = MyOrders.GetOrdersByPriceReportID(entity.PriceReport_ID);
                SupplierAgentProtocalInfo protocalinfo = MyAgentProtocal.GetSupplierAgentProtocalByPurchaseID(entity.PriceReport_PurchaseID, pub.CreateUserPrivilege("0aab7822-e327-4dcd-bc30-4cbf289067e4"));
                if (purchase_type == 0 && spinfo != null)
                {
                    //采购申请未过期、已审核、未删除
                    if (spinfo.Purchase_ValidDate >= tools.NullDate(DateTime.Now.ToShortDateString()) && spinfo.Purchase_Trash == 0 && spinfo.Purchase_Status == 2)
                    {
                        Response.Write(" <a href=\"/cart/my_buycart.aspx?PriceReport_ID=" + entity.PriceReport_ID + "&Purchase_ID=" + entity.PriceReport_PurchaseID + "\" class=\"a_t12_blue\" target=\"_blank\">创建订单</a>");
                    }
                }
                //代理采购未生成订单且已生成采购协议
                if (purchase_type == 1 && protocalinfo != null && spinfo != null)
                {
                    //采购协议双方已确认且采购申请未过期、已审核、未删除
                    if (protocalinfo.Protocal_Status == 2 && spinfo.Purchase_ValidDate >= tools.NullDate(DateTime.Now.ToShortDateString()) && spinfo.Purchase_Trash == 0 && spinfo.Purchase_Status == 2)
                    {
                        Response.Write(" <a href=\"/cart/my_buycart.aspx?PriceReport_ID=" + entity.PriceReport_ID + "&Purchase_ID=" + entity.PriceReport_PurchaseID + "\" class=\"a_t12_blue\" target=\"_blank\">创建订单</a>");
                    }
                }
                Response.Write(" <a href=\"/supplier/MyReceivePriceReportDetail.aspx?PriceReport_ID=" + entity.PriceReport_ID + "\" class=\"a_t12_blue\">查看</a> ");
                Response.Write(" <a href=\"/supplier/order_list.aspx?PriceReport_ID=" + entity.PriceReport_ID + "&Purchase_ID=" + Purchase_ID + "\" class=\"a_t12_blue\">查看订单</a>");

                Response.Write("</td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            Response.Write("<tr><td height=\"8\"></td></tr>");
            Response.Write("<tr><td align=\"right\">");
            Response.Write("<div class=\"page\">");
            pub.Page(page.PageCount, page.CurrentPage, "?purchase_id=" + Purchase_ID, page.PageSize, page.RecordCount);
            Response.Write("</div>");
            Response.Write("</td></tr>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#FFFFFF\">");
            Response.Write("<td colspan=\"9\" align=\"center\" height=\"35\">暂无记录</td>");
            Response.Write("</tr>");
        }
        Response.Write("</table>");

    }

    //报价回复
    public void ReplySupplierPriceReport()
    {
        string PriceReport_ReplyContent = tools.CheckStr(Request["PriceReport_ReplyContent"]);
        int PriceReport_ID = tools.CheckInt(Request["PriceReport_ID"]);
        int supplier_id = tools.NullInt(Session["supplier_id"]);

        if (PriceReport_ID == 0)
        {
            pub.Msg("error", "错误", "报价编号错误", false, "{back}");
        }
        if (PriceReport_ReplyContent == "")
        {
            pub.Msg("info", "提示信息", "请输入回复内容", false, "{back}");
        }
        //报价信息
        SupplierPriceReportInfo entity = MyPriceReport.GetSupplierPriceReportByID(PriceReport_ID, pub.CreateUserPrivilege("6a12664e-4eeb-4259-b7b5-904044194067"));

        if (entity != null)
        {

            SupplierPurchaseInfo spinfo = GetSupplierPurchaseByID(entity.PriceReport_PurchaseID);
            if (spinfo != null)
            {


                if (spinfo.Purchase_SupplierID != supplier_id)
                {
                    Response.Redirect("/supplier/MyReceivePriceReport_list.aspx");
                }
            }

            //非平台报价
            if (entity.PriceReport_MemberID > 0 && spinfo.Purchase_TypeID == 0)
            {
                Response.Redirect("/supplier/MyReceivePriceReport_list.aspx");
            }
            //审核
            if (entity.PriceReport_AuditStatus != 1)
            {
                Response.Redirect("/supplier/MyReceivePriceReport_list.aspx");
            }


            entity.PriceReport_ReplyContent = PriceReport_ReplyContent;
            entity.PriceReport_IsReply = 1;

            if (MyPriceReport.EditSupplierPriceReport(entity, pub.CreateUserPrivilege("d2656c57-1fbb-4928-8bff-41488d5763cc")))
            {
                pub.Msg("info", "提示信息", "回复成功", false, "/supplier/MyReceivePriceReport_list.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "回复失败", false, "/supplier/MyReceivePriceReport_list.aspx");
            }

        }
        else
        {
            pub.Msg("error", "错误信息", "回复失败", false, "/supplier/MyReceivePriceReport_list.aspx");
        }

    }

    /// <summary>
    /// 报价查询
    /// </summary>
    /// <param name="id">报价ID</param>
    /// <returns></returns>
    public SupplierPriceReportInfo SupplierPriceReportByID(int id)
    {
        return MyPriceReport.GetSupplierPriceReportByID(id, pub.CreateUserPrivilege("6a12664e-4eeb-4259-b7b5-904044194067"));
    }

    /// <summary>
    /// 报价明细查询
    /// </summary>
    /// <param name="id">报价ID</param>
    /// <returns></returns>
    public IList<SupplierPriceReportDetailInfo> GetSupplierPriceReportDetailsByPriceReportID(int id)
    {
        return MyPriceReportDetail.GetSupplierPriceReportDetailsByPriceReportID(id);
    }

    #endregion

    #region 留言订阅

    //用户留言添加
    public void AddFeedBack(int Flag)
    {
        int Feedback_ID = 0;
        int Feedback_Type = tools.CheckInt(Request.Form["Feedback_Type"]);
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        //int Feedback_MemberID = tools.CheckInt(Session["member_id"].ToString());
        string Feedback_Name = tools.CheckStr(Request.Form["Feedback_Name"]);
        string Feedback_CompanyName = tools.CheckStr(Request.Form["Feedback_CompanyName"]);
        string Feedback_Attachment = tools.CheckStr(Request.Form["Feedback_Attachment"]);
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

        if (Feedback_Name.Length < 1 || Feedback_Tel.Length < 1 || Feedback_Email.Length < 1 || Feedback_CompanyName.Length < 1)
        {
            pub.Msg("info", "信息提示", "请输入您的联系方式，以便于我们与您联系！", false, "{back}");
        }
        if (Feedback_Content.Length < 1)
        {
            pub.Msg("info", "信息提示", "请输入留言内容！", false, "{back}");
        }

        FeedBackInfo entity = new FeedBackInfo();
        entity.Feedback_ID = Feedback_ID;
        entity.Feedback_Type = Feedback_Type;
        entity.Feedback_SupplierID = supplier_id;
        //entity.Feedback_MemberID = Feedback_MemberID;
        entity.Feedback_Name = Feedback_Name;
        entity.Feedback_Tel = Feedback_Tel;
        entity.Feedback_Email = Feedback_Email;
        entity.Feedback_CompanyName = Feedback_CompanyName;
        entity.Feedback_Content = Feedback_Content;
        entity.Feedback_Attachment = Feedback_Attachment;
        entity.Feedback_Addtime = Feedback_Addtime;
        entity.Feedback_IsRead = Feedback_IsRead;
        entity.Feedback_Reply_IsRead = Feedback_Reply_IsRead;
        entity.Feedback_Reply_Content = Feedback_Reply_Content;
        entity.Feedback_Site = Feedback_Site;

        if (MyFeedback.AddFeedBack(entity, pub.CreateUserPrivilege("8ccafb10-8a4a-425f-8111-a1e4eb46a0b4")))
        {
            if (Flag == 1)
            {
                Response.Redirect("/supplier/feedback.aspx?tip=success");
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

        Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"3\">");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "FeedBackInfo.Feedback_MemberID", "=", member_id.ToString()));
        Query.OrderInfos.Add(new OrderInfo("FeedBackInfo.Feedback_ID", "Desc"));
        IList<FeedBackInfo> entitys = MyFeedback.GetFeedBacks(Query, pub.CreateUserPrivilege("9877a09e-5dda-4b1e-bf6f-042504449eeb"));
        PageInfo page = MyFeedback.GetPageInfo(Query, pub.CreateUserPrivilege("9877a09e-5dda-4b1e-bf6f-042504449eeb"));
        if (entitys != null)
        {
            foreach (FeedBackInfo entity in entitys)
            {
                i = i + 1;
                if (i > 1)
                {
                    Response.Write("<tr><td height=\"10\" colspan=\"3\" valign=\"top\" class=\"dotline_h\" align=\"left\"></td></tr>");
                }

                icon = "/images/feedback_1.gif";
                Response.Write("<tr><td align=\"left\">");
                switch (entity.Feedback_Type)
                {
                    case 1:
                        icon_alt = "简单的留言";
                        break;
                    case 2:
                        icon_alt = "对网站的意见";
                        break;
                    case 3:
                        icon_alt = "对公司的建议";
                        break;
                    case 4:
                        icon_alt = "具有合作意向";
                        break;
                    case 5:
                        icon_alt = "商品投诉";
                        break;
                    case 6:
                        icon_alt = "服务投诉";
                        break;

                }
                Response.Write("<span class=\"t12_orange\">[" + icon_alt + "]</span> 内容：" + entity.Feedback_Content);
                if (entity.Feedback_Attachment.Length > 0)
                {
                    Response.Write(" [<a href=\"" + pub.FormatImgURL(entity.Feedback_Attachment, "fullpath") + "\" target=\"_blank\">查看附件</a>]");
                }
                Response.Write("</td><td width=\"150\" align=\"right\" class=\"info_date\">" + entity.Feedback_Addtime + "</td></tr>");
                if (entity.Feedback_Reply_Content != "")
                {
                    Response.Write("<tr><td valign=\"top\" class=\"t12_red\" align=\"left\"><img src=\"/images/feedback_reply.gif\" alt=\"客服回复\" align=\"absmiddle\" /> 客服回复：" + entity.Feedback_Reply_Content + " 感谢您对" + Application["Site_Name"].ToString() + "的支持！祝您购物愉快！");
                    if (entity.Feedback_Reply_IsRead == 0)
                    {
                        Response.Write("<img src=\"/images/icon_new.gif\">");
                        MyFeedback.EditFeedBackReadStatus(entity.Feedback_ID, entity.Feedback_IsRead, 1, pub.CreateUserPrivilege("02cc2c2c-9ecc-462a-86dc-406f792ac83a"));
                    }
                    Response.Write("</td><td align=\"right\" class=\"info_date\">" + entity.Feedback_Reply_Addtime + "</td></tr>");
                }
            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"2\"><tr><td align=\"right\" height=\"10\"></td></tr><tr><td align=\"right\"><div class=\"page\" style=\"float:right;\">");
            pub.Page(page.PageCount, page.CurrentPage, Pageurl, page.PageSize, page.RecordCount);
            Response.Write("</div></td></tr>");
        }
        else
        {
            Response.Write("<tr><td align=\"center\" class=\"t12_grey\">没有记录</td></tr>");
        }

        Response.Write("</table>");
    }

    //修改邮件订阅状态
    public void UpdateSupplierAllowSysEmail(int status)
    {
        SupplierInfo supplierinfo = GetSupplierByID();
        if (supplierinfo != null)
        {
            supplierinfo.Supplier_AllowSysEmail = status;
            MyBLL.EditSupplier(supplierinfo, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f"));
            Session["Supplier_AllowSysEmail"] = status;
            Response.Redirect("/supplier/email_notify_set.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "邮件通知设置失败，请稍后再试！", false, "{back}");
        }
    }
    //修改邮件订阅状态
    public void UpdateSupplierAllowSysMessage(int status)
    {
        SupplierInfo supplierinfo = GetSupplierByID();
        if (supplierinfo != null)
        {
            supplierinfo.Supplier_AllowSysMessage = status;
            MyBLL.EditSupplier(supplierinfo, pub.CreateUserPrivilege("40f51178-030c-402a-bee4-57ed6d1ca03f"));
            Session["Supplier_AllowSysMessage"] = status;
            Response.Redirect("/supplier/email_notify_set.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "手机通知设置失败，请稍后再试！", false, "{back}");
        }
    }

    #endregion

    #region "收货地址"

    //获取收货地址信息
    public SupplierAddressInfo GetSupplierAddressByID(int ID)
    {
        return MyAddr.GetSupplierAddressByID(ID);
    }

    /// <summary>
    /// 收货地址表单检查
    /// </summary>
    public void Check_Supplier_Address_Form()
    {
        int Supplier_Address_ID = tools.CheckInt(Request.Form["Supplier_Address_ID"]);
        int Supplier_Address_SupplierID = tools.CheckInt(Session["Supplier_id"].ToString());
        string Supplier_Address_Country = tools.CheckStr(Request.Form["Orders_Address_Country"]);
        string Supplier_Address_State = tools.CheckStr(Request.Form["Orders_Address_Province"]);
        string Supplier_Address_City = tools.CheckStr(Request.Form["Orders_Address_City"]);
        string Supplier_Address_County = tools.CheckStr(Request.Form["Orders_Address_District"]);
        string Supplier_Address_StreetAddress = tools.CheckStr(Request.Form["Orders_Address_StreetAddress"]);
        string Supplier_Address_Zip = tools.CheckStr(Request.Form["Orders_Address_Zip"]);
        string Supplier_Address_Name = tools.CheckStr(Request.Form["Orders_Address_Name"]);
        string Supplier_Address_Phone_Countrycode = tools.CheckStr(Request.Form["Orders_Address_Phone_Countrycode"]);
        string Supplier_Address_Phone_Number = tools.CheckStr(Request.Form["Orders_Address_Phone_Number"]);
        string Supplier_Address_Mobile = tools.CheckStr(Request.Form["Orders_Address_Mobile"]);

        if (Supplier_Address_County == "0" || Supplier_Address_County == "")
        {
            Response.Write("请选择省市区信息");
            Response.End();
        }

        if (Supplier_Address_StreetAddress == "")
        {
            Response.Write("请将联系地址填写完整");
            Response.End();
        }

        if (Check_Zip(Supplier_Address_Zip) == false)
        {
            Response.Write("邮编信息应为6位数字");
            Response.End();
        }

        if (Supplier_Address_Name == "")
        {
            Response.Write("请将收货人姓名填写完整");
            Response.End();
        }

        if (Supplier_Address_Phone_Number == "" && Supplier_Address_Mobile == "")
        {
            Response.Write("联系电话与手机请至少填写一项");
            Response.End();
        }

        if (pub.Checkmobile(Supplier_Address_Mobile) == false && Supplier_Address_Mobile != "")
        {
            Response.Write("手机号码错误");
            Response.End();
        }

        Response.Write("success");
    }

    //收货地址添加
    public void Supplier_Address_Add_back(string action)
    {
        int Supplier_Address_ID = 0;
        int Supplier_Address_SupplierID = tools.CheckInt(Session["Supplier_id"].ToString());
        string Supplier_Address_Country = tools.CheckStr(Request.Form["Orders_Address_Country"]);
        string Supplier_Address_State = tools.CheckStr(Request.Form["Orders_Address_Province"]);
        string Supplier_Address_City = tools.CheckStr(Request.Form["Orders_Address_City"]);
        string Supplier_Address_County = tools.CheckStr(Request.Form["Orders_Address_District"]);
        string Supplier_Address_StreetAddress = tools.CheckStr(Request.Form["Orders_Address_StreetAddress"]);
        string Supplier_Address_Zip = tools.CheckStr(Request.Form["Orders_Address_Zip"]);
        string Supplier_Address_Name = tools.CheckStr(Request.Form["Orders_Address_Name"]);
        string Supplier_Address_Phone_Countrycode = tools.CheckStr(Request.Form["Orders_Address_Phone_Countrycode"]);
        string Supplier_Address_Phone_Areacode = "";
        string Supplier_Address_Phone_Number = tools.CheckStr(Request.Form["Orders_Address_Phone_Number"]);
        string Supplier_Address_Mobile = tools.CheckStr(Request.Form["Orders_Address_Mobile"]);
        string Supplier_Address_Site = "CN";


        if (Supplier_Address_County == "0" || Supplier_Address_County == "")
        {
            pub.Msg("info", "提示信息", "请选择省市区信息", false, "{back}");
        }
        if (Supplier_Address_StreetAddress == "")
        {
            pub.Msg("info", "提示信息", "请将联系地址填写完整", false, "{back}");
        }
        if (Check_Zip(Supplier_Address_Zip) == false)
        {
            pub.Msg("info", "提示信息", "邮编信息应为6位数字", false, "{back}");
        }
        if (Supplier_Address_Name == "")
        {
            pub.Msg("info", "提示信息", "请将收货人姓名填写完整", false, "{back}");
        }
        if (Supplier_Address_Phone_Number == "" && Supplier_Address_Mobile == "")
        {
            pub.Msg("info", "提示信息", "联系电话与手机请至少填写一项", false, "{back}");
        }


        if (pub.Checkmobile(Supplier_Address_Mobile) == false && Supplier_Address_Mobile != "")
        {
            pub.Msg("info", "提示信息", "手机号码错误", false, "{back}");
        }



        SupplierAddressInfo entity = new SupplierAddressInfo();
        entity.Supplier_Address_ID = Supplier_Address_ID;
        entity.Supplier_Address_SupplierID = Supplier_Address_SupplierID;
        entity.Supplier_Address_Country = Supplier_Address_Country;
        entity.Supplier_Address_State = Supplier_Address_State;
        entity.Supplier_Address_City = Supplier_Address_City;
        entity.Supplier_Address_County = Supplier_Address_County;
        entity.Supplier_Address_StreetAddress = Supplier_Address_StreetAddress;
        entity.Supplier_Address_Zip = Supplier_Address_Zip;
        entity.Supplier_Address_Name = Supplier_Address_Name;
        entity.Supplier_Address_Phone_Countrycode = Supplier_Address_Phone_Countrycode;
        entity.Supplier_Address_Phone_Areacode = Supplier_Address_Phone_Areacode;
        entity.Supplier_Address_Phone_Number = Supplier_Address_Phone_Number;
        entity.Supplier_Address_Mobile = Supplier_Address_Mobile;
        entity.Supplier_Address_Site = Supplier_Address_Site;

        MyAddr.AddSupplierAddress(entity);
        if (action == "address_add")
        {
            Response.Redirect("/Supplier/order_address.aspx");
        }
        else
        {
            Response.Redirect("/cart/order_delivery.aspx");
        }
    }

    public void Supplier_Address_Add(string action)
    {
        int Supplier_Address_ID = 0;
        int type = tools.CheckInt(Request["type"]);
        int Supplier_Address_SupplierID = tools.CheckInt(Session["Supplier_id"].ToString());
        string Supplier_Address_Country = tools.CheckStr(Request["Orders_Address_Country"]);
        string Supplier_Address_State = tools.CheckStr(Request["Supplier_Address_State"]);
        string Supplier_Address_City = tools.CheckStr(Request["Supplier_Address_City"]);
        string Supplier_Address_County = tools.CheckStr(Request["Supplier_Address_County"]);
        string Supplier_Address_StreetAddress = tools.CheckStr(Request["Orders_Address_StreetAddress"]);
        string Supplier_Address_Zip = tools.CheckStr(Request["Orders_Address_Zip"]);
        string Supplier_Address_Name = tools.CheckStr(Request["Orders_Address_Name"]);
        string Supplier_Address_Phone_Countrycode = tools.CheckStr(Request["Orders_Address_Phone_Countrycode"]);
        string Supplier_Address_Phone_Areacode = "";
        string Supplier_Address_Phone_Number = tools.CheckStr(Request["Orders_Address_Phone_Number"]);
        string Supplier_Address_Mobile = tools.CheckStr(Request["Orders_Address_Mobile"]);
        string Supplier_Address_Site = "CN";
        if (Supplier_Address_County == "0" || Supplier_Address_County == "")
        {
            pub.Msg("info", "提示信息", "请选择省市区信息", false, "{back}");
        }
        if (Supplier_Address_StreetAddress == "")
        {
            pub.Msg("info", "提示信息", "请将联系地址填写完整", false, "{back}");
        }
        if (Check_Zip(Supplier_Address_Zip) == false)
        {
            pub.Msg("info", "提示信息", "邮编信息应为6位数字", false, "{back}");
        }
        if (Supplier_Address_Name == "")
        {
            pub.Msg("info", "提示信息", "请将收货人姓名填写完整", false, "{back}");
        }
        if (Supplier_Address_Phone_Number == "" && Supplier_Address_Mobile == "")
        {
            pub.Msg("info", "提示信息", "联系电话与手机请至少填写一项", false, "{back}");
        }


        if (pub.Checkmobile(Supplier_Address_Mobile) == false && Supplier_Address_Mobile != "")
        {
            pub.Msg("info", "提示信息", "手机号码错误", false, "{back}");
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierAddressInfo.Supplier_Address_Name", "=", Supplier_Address_Name));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierAddressInfo.Supplier_Address_SupplierID", "=", Supplier_Address_SupplierID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierAddressInfo.Supplier_Address_County", "=", Supplier_Address_County));

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierAddressInfo.Supplier_Address_StreetAddress", "=", Supplier_Address_StreetAddress));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierAddressInfo.Supplier_Address_Zip", "=", Supplier_Address_Zip));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierAddressInfo.Supplier_Address_Phone_Number", "=", Supplier_Address_Phone_Number));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SupplierAddressInfo.Supplier_Address_Mobile", "=", Supplier_Address_Mobile));

        IList<SupplierAddressInfo> entitys = MyAddr.GetSupplierAddresss(Query);
        SupplierAddressInfo entity = null;
        if (entitys == null)
        {
            entity = new SupplierAddressInfo();
            entity.Supplier_Address_ID = Supplier_Address_ID;
            entity.Supplier_Address_SupplierID = Supplier_Address_SupplierID;
            entity.Supplier_Address_Country = Supplier_Address_Country;
            entity.Supplier_Address_State = Supplier_Address_State;
            entity.Supplier_Address_City = Supplier_Address_City;
            entity.Supplier_Address_County = Supplier_Address_County;
            entity.Supplier_Address_StreetAddress = Supplier_Address_StreetAddress;
            entity.Supplier_Address_Zip = Supplier_Address_Zip;
            entity.Supplier_Address_Name = Supplier_Address_Name;
            entity.Supplier_Address_Phone_Countrycode = Supplier_Address_Phone_Countrycode;
            entity.Supplier_Address_Phone_Areacode = Supplier_Address_Phone_Areacode;
            entity.Supplier_Address_Phone_Number = Supplier_Address_Phone_Number;
            entity.Supplier_Address_Mobile = Supplier_Address_Mobile;
            entity.Supplier_Address_Site = Supplier_Address_Site;

            MyAddr.AddSupplierAddress(entity);
            Response.Write("true");
            if (type == 1)
            {
                Query = new QueryInfo();
                Query.PageSize = 1;
                Query.CurrentPage = 1;
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierAddressInfo.Supplier_Address_SupplierID", "=", Supplier_Address_SupplierID.ToString()));
                Query.OrderInfos.Add(new OrderInfo("SupplierAddressInfo.Supplier_Address_ID", "DESC"));
                entitys = MyAddr.GetSupplierAddresss(Query);
                if (entitys != null)
                {
                    Session["Orders_Address_ID"] = entitys[0].Supplier_Address_ID;
                }
            }
        }
        else if (type == 1)
        {
            entity = entitys[0];
            if (entity != null)
            {
                entity.Supplier_Address_Zip = Supplier_Address_Zip;
                entity.Supplier_Address_Phone_Number = Supplier_Address_Phone_Number;
                entity.Supplier_Address_Mobile = Supplier_Address_Mobile;
                entity.Supplier_Address_StreetAddress = Supplier_Address_StreetAddress;
                MyAddr.EditSupplierAddress(entity);
            }
            else
            {
                entity = new SupplierAddressInfo();
            }
            Session["Orders_Address_ID"] = entity.Supplier_Address_ID;
            Response.Write("");
        }

    }

    //收货地址修改
    public void Supplier_Address_Edit(string action)
    {
        int Supplier_Address_ID = tools.CheckInt(Request.Form["Supplier_Address_ID"]);
        int Supplier_Address_SupplierID = tools.CheckInt(Session["Supplier_id"].ToString());
        string Supplier_Address_Country = tools.CheckStr(Request.Form["Orders_Address_Country"]);
        string Supplier_Address_State = tools.CheckStr(Request.Form["Orders_Address_Province"]);
        string Supplier_Address_City = tools.CheckStr(Request.Form["Orders_Address_City"]);
        string Supplier_Address_County = tools.CheckStr(Request.Form["Orders_Address_District"]);
        string Supplier_Address_StreetAddress = tools.CheckStr(Request.Form["Orders_Address_StreetAddress"]);
        string Supplier_Address_Zip = tools.CheckStr(Request.Form["Orders_Address_Zip"]);
        string Supplier_Address_Name = tools.CheckStr(Request.Form["Orders_Address_Name"]);
        string Supplier_Address_Phone_Countrycode = tools.CheckStr(Request.Form["Orders_Address_Phone_Countrycode"]);
        string Supplier_Address_Phone_Areacode = "";
        string Supplier_Address_Phone_Number = tools.CheckStr(Request.Form["Orders_Address_Phone_Number"]);
        string Supplier_Address_Mobile = tools.CheckStr(Request.Form["Orders_Address_Mobile"]);
        string Supplier_Address_Site = "CN";


        if (Supplier_Address_County == "0" || Supplier_Address_County == "")
        {
            pub.Msg("info", "提示信息", "请选择省市区信息", false, "{back}");
        }
        if (Supplier_Address_StreetAddress == "")
        {
            pub.Msg("info", "提示信息", "请将联系地址填写完整", false, "{back}");
        }
        if (Check_Zip(Supplier_Address_Zip) == false)
        {
            pub.Msg("info", "提示信息", "邮编信息应为6位数字", false, "{back}");
        }
        if (Supplier_Address_Name == "")
        {
            pub.Msg("info", "提示信息", "请将收货人姓名填写完整", false, "{back}");
        }
        if (Supplier_Address_Phone_Number == "" && Supplier_Address_Mobile == "")
        {
            pub.Msg("info", "提示信息", "联系电话与手机请至少填写一项", false, "{back}");
        }


        if (pub.Checkmobile(Supplier_Address_Mobile) == false && Supplier_Address_Mobile != "")
        {
            pub.Msg("info", "提示信息", "手机号码错误", false, "{back}");
        }



        SupplierAddressInfo entity = MyAddr.GetSupplierAddressByID(Supplier_Address_ID);
        if (entity != null)
        {
            if (entity.Supplier_Address_SupplierID == Supplier_Address_SupplierID)
            {
                entity.Supplier_Address_Country = Supplier_Address_Country;
                entity.Supplier_Address_State = Supplier_Address_State;
                entity.Supplier_Address_City = Supplier_Address_City;
                entity.Supplier_Address_County = Supplier_Address_County;
                entity.Supplier_Address_StreetAddress = Supplier_Address_StreetAddress;
                entity.Supplier_Address_Zip = Supplier_Address_Zip;
                entity.Supplier_Address_Name = Supplier_Address_Name;
                entity.Supplier_Address_Phone_Countrycode = Supplier_Address_Phone_Countrycode;
                entity.Supplier_Address_Phone_Areacode = Supplier_Address_Phone_Areacode;
                entity.Supplier_Address_Phone_Number = Supplier_Address_Phone_Number;
                entity.Supplier_Address_Mobile = Supplier_Address_Mobile;
                MyAddr.EditSupplierAddress(entity);
            }
        }


        if (action == "address_edit")
        {
            Response.Redirect("/Supplier/order_address.aspx");
        }
        else
        {
            Response.Redirect("/cart/order_delivery.aspx?Supplier_address_id=" + Supplier_Address_ID);
        }
    }

    //收货地址列表
    public void Supplier_Address()
    {
        Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"3\">");

        int Supplier_id = tools.CheckInt(Session["Supplier_id"].ToString());

        int icount = 0;

        if (Supplier_id > 0)
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierAddressInfo.Supplier_Address_SupplierID", "=", Supplier_id.ToString()));
            Query.OrderInfos.Add(new OrderInfo("SupplierAddressInfo.Supplier_Address_ID", "Desc"));
            IList<SupplierAddressInfo> entitys = MyAddr.GetSupplierAddresss(Query);
            if (entitys != null)
            {
                foreach (SupplierAddressInfo entity in entitys)
                {
                    icount = icount + 1;
                    Response.Write("  <tr>");
                    Response.Write("    <td width=\"10%\" rowspan=\"4\" align=\"center\" class=\"step_num_off\">" + icount + "</td>");
                    Response.Write("    <td width=\"90%\">收货人：" + entity.Supplier_Address_Name + "</td>");
                    Response.Write("  </tr>");
                    Response.Write("  <tr>");
                    Response.Write("    <td width=\"90%\">收货地址：" + addr.DisplayAddress(entity.Supplier_Address_State, entity.Supplier_Address_City, entity.Supplier_Address_County) + " " + entity.Supplier_Address_StreetAddress + " " + entity.Supplier_Address_Zip + "</td>");
                    Response.Write("  </tr>");
                    Response.Write("  <tr>");
                    Response.Write("    <td width=\"90%\">联系电话：" + entity.Supplier_Address_Phone_Number + " " + entity.Supplier_Address_Mobile + "</td>");
                    Response.Write("  </tr>");
                    Response.Write("  <tr>");
                    Response.Write("    <td width=\"90%\"><input name=\"btn_delete\" type=\"button\" class=\"buttonupload\" id=\"btn_delete\" value=\"修改\" onclick=\"javascript:location.href='/Supplier/order_address_edit.aspx?Supplier_address_id=" + entity.Supplier_Address_ID + "';\" /> <input name=\"btn_delete\" type=\"button\" class=\"buttonupload\" id=\"btn_delete\" value=\"删除\" onclick=\"javascript:if(confirm('您确定删除该收货地址信息吗？')==false){return false;}else{location.href='/Supplier/order_address_do.aspx?action=address_move&Supplier_address_id=" + entity.Supplier_Address_ID + "';}\" /></td>");
                    Response.Write("  </tr>");
                    Response.Write("  <tr>");
                    Response.Write("    <td height=\"20\" colspan=\"2\" align=\"center\" class=\"dotline_h\"></td>");
                    Response.Write("  </tr>");
                }
            }
        }
        Response.Write("</table>");
    }

    //删除收货地址
    public void Supplier_Address_Del(string action)
    {
        int Supplier_address_id = tools.CheckInt(Request.QueryString["Supplier_address_id"]);
        if (Supplier_address_id > 0)
        {
            SupplierAddressInfo address = MyAddr.GetSupplierAddressByID(Supplier_address_id);
            if (address != null)
            {
                if (address.Supplier_Address_SupplierID == tools.CheckInt(Session["Supplier_id"].ToString()))
                {
                    MyAddr.DelSupplierAddress(Supplier_address_id);
                }
            }
        }
        if (action == "address_move")
        {
            Response.Redirect("/Supplier/order_address.aspx");
        }
        else if (action == "cart_address_move")
        {
            Response.Write("true");
        }
    }


    #endregion

    #region 合同模板
    //获取合同模板信息
    public SupplierContractTemplateInfo GetSupplierContractTemplateByID(int ID)
    {
        return MySupplierContractTemplate.GetSupplierContractTemplateByID(ID);
    }

    public void AddSupplierContractTemplate()
    {
        int SupplierID = tools.CheckInt(Session["Supplier_id"].ToString());
        string Contract_Template_Name = tools.CheckStr(Request["Contract_Template_Name"]);
        string Contract_Template_Content = tools.CheckHTML(Request["Contract_Template_Content"]);

        if (Contract_Template_Name == "")
        {
            pub.Msg("info", "提示信息", "请填写模板标题信息", false, "{back}");
        }

        SupplierContractTemplateInfo entity = null;
        entity = new SupplierContractTemplateInfo();
        entity.Contract_Template_Name = Contract_Template_Name;
        entity.Contract_Template_Content = Contract_Template_Content;
        entity.Contract_Template_SupplierID = SupplierID;
        entity.Contract_Template_Addtime = DateTime.Now;
        entity.Contract_Template_Site = pub.GetCurrentSite();

        if (MySupplierContractTemplate.AddSupplierContractTemplate(entity))
        {
            pub.Msg("positive", "操作成功", "模板创建成功！", true, "contract_template.aspx");
        }
        else
        {
            pub.Msg("error", "操作失败", "模板创建失败！", false, "{back}");
        }


    }

    public void EditSupplierContractTemplate()
    {
        int SupplierID = tools.CheckInt(Session["Supplier_id"].ToString());
        int template_id = tools.CheckInt(Request["template_id"]);
        string Contract_Template_Name = tools.CheckStr(Request["Contract_Template_Name"]);
        string Contract_Template_Content = tools.CheckHTML(Request["Contract_Template_Content"]);

        if (Contract_Template_Name == "")
        {
            pub.Msg("info", "提示信息", "请填写模板标题信息", false, "{back}");
        }

        SupplierContractTemplateInfo entity = GetSupplierContractTemplateByID(template_id);
        if (entity != null)
        {
            if (entity.Contract_Template_SupplierID != tools.NullInt(Session["supplier_id"]))
            {
                Response.Redirect("/supplier/contract_template.aspx");
            }
            entity.Contract_Template_Name = Contract_Template_Name;
            entity.Contract_Template_Content = Contract_Template_Content;

            if (MySupplierContractTemplate.EditSupplierContractTemplate(entity))
            {
                pub.Msg("positive", "操作成功", "模板修改成功！", true, "contract_template.aspx");
            }
            else
            {
                pub.Msg("error", "操作失败", "模板修改失败！", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "操作失败", "模板修改失败！", false, "{back}");
        }

    }

    //合同模板列表
    public void SupplierContractTemplate_List()
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        string BgColor = "";

        if (curpage < 1)
        {
            curpage = 1;
        }


        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <th align=\"center\" valign=\"middle\">合同标题</th>");
        Response.Write("  <th width=\"110\" align=\"center\" valign=\"middle\">添加时间</th>");
        Response.Write("  <th width=\"110\" align=\"center\" valign=\"middle\">操作</th>");
        Response.Write("</tr>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = curpage;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierContractTemplateInfo.Contract_Template_SupplierID", "=", supplier_id.ToString()));

        Query.OrderInfos.Add(new OrderInfo("SupplierContractTemplateInfo.Contract_Template_ID", "Desc"));
        IList<SupplierContractTemplateInfo> entitys = MySupplierContractTemplate.GetSupplierContractTemplates(Query);
        PageInfo page = MySupplierContractTemplate.GetPageInfo(Query);
        if (entitys != null)
        {
            foreach (SupplierContractTemplateInfo entity in entitys)
            {
                if (BgColor == "#FFFFFF")
                {
                    BgColor = "#FFFFFF";
                }
                else
                {
                    BgColor = "#FFFFFF";
                }

                Response.Write("<tr bgcolor=\"" + BgColor + "\" height=\"35\">");
                Response.Write("<td valign=\"middle\" style=\"padding:0px 7px;\">" + entity.Contract_Template_Name + "</td>");
                Response.Write("<td align=\"center\" valign=\"middle\">" + entity.Contract_Template_Addtime + "</td>");

                Response.Write("<td height=\"35\" align=\"center\" valign=\"middle\">");
                Response.Write("<a href=\"/supplier/contract_template_edit.aspx?Template_ID=" + entity.Contract_Template_ID + "\">修改</a> ");
                Response.Write("<a href=\"contract_template_do.aspx?action=template_del&Template_ID=" + entity.Contract_Template_ID + "\">删除</a></td>");
                Response.Write("</tr>");
            }
            Response.Write("</table>");
            Response.Write("<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");
            Response.Write("<tr><td height=\"8\"></td></tr>");
            Response.Write("<tr><td align=\"right\">");
            Response.Write("<div class=\"page\">");
            pub.Page(page.PageCount, page.CurrentPage, "?1=1", page.PageSize, page.RecordCount);
            Response.Write("</div>");
            Response.Write("</td></tr>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#FFFFFF\">");
            Response.Write("<td colspan=\"3\" align=\"center\" height=\"35\">暂无记录</td>");
            Response.Write("</tr>");
        }
        Response.Write("</table>");

    }

    public void DelSupplierContractTemplate()
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        int Template_ID = tools.NullInt(Request["Template_ID"]);
        SupplierContractTemplateInfo spinfo = GetSupplierContractTemplateByID(Template_ID);
        if (spinfo != null)
        {
            if (supplier_id == spinfo.Contract_Template_SupplierID)
            {
                MySupplierContractTemplate.DelSupplierContractTemplate(Template_ID);
                pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/contract_template.aspx");
            }
            else
            {
                pub.Msg("error", "错误信息", "删除失败！", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "删除失败！", false, "{back}");
        }

    }

    public string SupplierContractTemplate_Option(int Template_ID)
    {
        int supplier_id = tools.NullInt(Session["supplier_id"]);
        StringBuilder selectstr = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SupplierContractTemplateInfo.Contract_Template_SupplierID", "=", supplier_id.ToString()));

        Query.OrderInfos.Add(new OrderInfo("SupplierContractTemplateInfo.Contract_Template_ID", "Desc"));
        IList<SupplierContractTemplateInfo> entitys = MySupplierContractTemplate.GetSupplierContractTemplates(Query);
        if (entitys != null)
        {
            foreach (SupplierContractTemplateInfo entity in entitys)
            {
                if (Template_ID == entity.Contract_Template_ID)
                {
                    selectstr.Append("<option value=\"" + entity.Contract_Template_ID + "\" selected>" + entity.Contract_Template_Name + "</option>");
                }
                else
                {
                    selectstr.Append("<option value=\"" + entity.Contract_Template_ID + "\">" + entity.Contract_Template_Name + "</option>");
                }
            }
        }
        return selectstr.ToString();
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
                strHTML.Append("<td align=\"center\">" + GetProductByID(entity.Orders_Goods_Product_ID).Product_Unit + "</td>");
                strHTML.Append("<td align=\"center\">" + entity.Orders_Goods_Amount + "</td>");
                strHTML.Append("<td align=\"center\">" + pub.FormatCurrency(entity.Orders_Goods_Product_Price) + "</td>");
                strHTML.Append("<td align=\"center\">" + pub.FormatCurrency(entity.Orders_Goods_Product_Price * entity.Orders_Goods_Amount) + "</td>");
                strHTML.Append("</tr>");

                total_price = total_price + (entity.Orders_Goods_Product_Price * entity.Orders_Goods_Amount);
            }
            //strHTML.Append("<tr>");
            //strHTML.Append("<td colspan=\"5\" align=\"right\" >运费:</td>");
            //strHTML.Append("<td colspan=\"5\">" + pub.FormatCurrency(ordersInfo.Orders_Total_Freight) + "</td>");
            //strHTML.Append("</tr>");
            //strHTML.Append("<tr>");
            //strHTML.Append("<td colspan=\"5\" align=\"right\" >合计:</td>");
            //strHTML.Append("<td >" + pub.FormatCurrency(total_price) + "</td>");
            //strHTML.Append("</tr>");




            strHTML.Append("<tr>");
            strHTML.Append("<td colspan=\"5\" align=\"right\" >订单总金额:</td>");
            strHTML.Append("<td >" + pub.FormatCurrency(ordersInfo.Orders_Total_AllPrice) + "</td>");
            strHTML.Append("</tr>");


            strHTML.Append("</table>");

        }

        return strHTML.ToString();
    }


    /// <summary>
    /// 替换合同模板中的占位符信息
    /// </summary>
    /// <param name="Supplier_ID"></param>
    /// <returns></returns>
    public string ReplaceOrdersContract_SupplierNew(OrdersInfo ordersInfo, int contact_no)
    {
        string Template_Html = "";
        string Contract_Template_TopContent = "";
        string ContractTemOnlyRead_Content = "";
        string Template_Product_Goods = "";
        string ContractTemOnlyEndContent = "";
        string ContractTemEndContent = "";
        string ContractTemResponsible = string.Empty;
        string Time = string.Format("商家未确定订单");

        //string Member_NickName = "";
        string Member_Company = "";
        ContractInfo ContractInfo = null;
        MemberInfo memberInfo = null;
        SupplierInfo Supplierinfo = null;
        if (ordersInfo != null)
        {
            memberInfo = new Member().GetMemberByID(ordersInfo.Orders_BuyerID);

            Supplierinfo = GetSupplierByID(memberInfo.Member_SupplierID);

            if (memberInfo != null)
            {
                //合同买方要使用商家公司名称
                Member_Company = Supplierinfo.Supplier_CompanyName;

            }
        }

        SupplierInfo supplierInfo = GetSupplierByID(ordersInfo.Orders_SupplierID);
        ContractInfo = My_Contract.GetContractByID(contact_no);
        if (ContractInfo != null)
        {
            Template_Product_Goods = new Contract().Contract_Orders_Goods_Print(ContractInfo.Contract_ID);
        }
        //合同模板运输责任
        ContractTemplateInfo ContractTemResponsibleEntity = MyTemplate.GetContractTemplateBySign("Sell_Contract_Template_Responsible", pub.CreateUserPrivilege("d4d58107-0e58-485f-af9e-3b38c7ff9672"));
        if (ContractTemResponsibleEntity != null)
        {
            ContractTemResponsible = ContractTemResponsibleEntity.Contract_Template_Content;
            if (ordersInfo != null)
            {
                ContractTemResponsible = ContractTemResponsible.Replace("{Responsible}", ordersInfo.Orders_Responsible == 1 ? "卖家责任" : "买家责任");
            }
        }
        List<OrdersLogInfo> logs = MyOrdersLog.GetOrdersLogsByOrdersID(ordersInfo.Orders_ID).Where(p => p.Orders_Log_Remark == "供应商确认订单").ToList();

        if (logs.Count>0)
        {
            Time = logs.FirstOrDefault().Orders_Log_Addtime.ToString("yyyy年MM月dd日");
        }
        //合同模板 头文件部分
        ContractTemplateInfo ContractTemplateEntity = MyTemplate.GetContractTemplateBySign("Sell_Contract_Template_TopFuJian", pub.CreateUserPrivilege("d4d58107-0e58-485f-af9e-3b38c7ff9672"));
        if (ContractTemplateEntity != null)
        {
            Contract_Template_TopContent = ContractTemplateEntity.Contract_Template_Content;
            if (supplierInfo != null)
            {
                Contract_Template_TopContent = Contract_Template_TopContent.Replace("{member_name}", Supplierinfo.Supplier_CompanyName);

                Contract_Template_TopContent = Contract_Template_TopContent.Replace("{supplier_name}", supplierInfo.Supplier_CompanyName);

                Contract_Template_TopContent = Contract_Template_TopContent.Replace("{time}", Time);
                Contract_Template_TopContent = Contract_Template_TopContent.Replace("{orders_goodslist}", GetOrdersGoods(ordersInfo));

            }

        }



        //合同模板 条款部分
        ContractTemplateInfo ContractTemOnlyReadEntity = MyTemplate.GetContractTemplateBySign("Sell_Contract_Template_OnlyRead", pub.CreateUserPrivilege("d4d58107-0e58-485f-af9e-3b38c7ff9672"));
        if (ContractTemOnlyReadEntity != null)
        {
            ContractTemOnlyRead_Content = ContractTemOnlyReadEntity.Contract_Template_Content;

        }
        if (ContractInfo != null)
        {
            Template_Product_Goods = new Contract().Contract_Orders_Goods_Print(ContractInfo.Contract_ID);
        }

        //合同条款 尾文件部分
        ContractTemplateInfo ContractTemEndEntity = MyTemplate.GetContractTemplateBySign("Sell_Contract_Template_EndFuJian", pub.CreateUserPrivilege("d4d58107-0e58-485f-af9e-3b38c7ff9672"));
        if (ContractTemEndContent != null)
        {
            ContractTemOnlyEndContent = ContractTemEndEntity.Contract_Template_Content;
            if (supplierInfo != null)
            {


                ContractTemOnlyEndContent = ContractTemOnlyEndContent.Replace("{member_name}", supplierInfo.Supplier_CompanyName);
                ContractTemOnlyEndContent = ContractTemOnlyEndContent.Replace("{member_adress}", supplierInfo.Supplier_Address);
                ContractTemOnlyEndContent = ContractTemOnlyEndContent.Replace("{member_corproate}", supplierInfo.Supplier_Corporate);
                ContractTemOnlyEndContent = ContractTemOnlyEndContent.Replace("{supplier_name}", Supplierinfo.Supplier_CompanyName);
                ContractTemOnlyEndContent = ContractTemOnlyEndContent.Replace("{supplier_adress}", Supplierinfo.Supplier_Address);
                ContractTemOnlyEndContent = ContractTemOnlyEndContent.Replace("{supplier_corproate}", Supplierinfo.Supplier_Corporate);

            }

        }

        if (ordersInfo != null)
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
                    Template_Html = Template_Html.Replace("{member_name}", Member_Company);
                }

                if (memberInfo != null)
                {
                    Template_Html = Template_Html.Replace("{member_name}", memberInfo.MemberProfileInfo.Member_Company);
                    Template_Html = Template_Html.Replace("{member_sealimg}", pub.FormatImgURL(memberInfo.MemberProfileInfo.Member_SealImg, "fullpath"));
                }





                Template_Html = Contract_Template_TopContent + Template_Html + ContractTemResponsible + ordersInfo.Orders_ContractAdd + ContractTemOnlyRead_Content + ContractTemOnlyEndContent;

            }



        }
        return Template_Html;
    }
    #endregion

    #region 会员网关

    #region 个人会员
    /// <summary>
    /// 创建个人会员接口
    /// </summary>
    /// <param name="entity"></param>
    public MemberJsonInfo Create_Personal_Member(SupplierInfo entity)
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
            "mobile="+entity.Supplier_Mobile,
            "email="+entity.Supplier_Email,
            "uid="+ "S"+entity.Supplier_ID,
            "real_name="+entity.Supplier_Contactman,
            "member_name="+entity.Supplier_Nickname,
            "is_active=T"
        };

        sign_type = "MD5";
        sign = pub.ReturnSignStr(parameters, "utf-8", tradesignkey);

        StringBuilder prestr = new StringBuilder();
        prestr.Append("&service=create_personal_member");
        prestr.Append("&version=1.0");
        prestr.Append("&is_active=T");
        prestr.Append("&partner_id=" + partner_id);
        prestr.Append("&mobile=" + entity.Supplier_Mobile);
        prestr.Append("&email=" + entity.Supplier_Email);
        prestr.Append("&uid=" + "S" + entity.Supplier_ID);
        prestr.Append("&real_name=" + entity.Supplier_Contactman);
        prestr.Append("&member_name=" + entity.Supplier_Nickname);
        prestr.Append("&sign=" + sign);
        prestr.Append("&sign_type=" + sign_type);

        string request_url = gateway + prestr.ToString();

        CookieCollection cookies = new CookieCollection();

        string strJson = HttpHelper.GetResponseString(HttpHelper.CreateGetHttpResponse(request_url, 0, "", cookies));

        jsonInfo = JsonHelper.JSONToObject<MemberJsonInfo>(strJson);

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

        return jsonInfo;
    }

    #endregion

    #region 企业会员
    /// <summary>
    /// 创建企业会员接口
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public MemberJsonInfo Create_Enterprise_Member(SupplierInfo entity)
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
            "login_name="+entity.Supplier_Nickname,
            "enterprise_name="+entity.Supplier_CompanyName,
            "member_name="+entity.Supplier_Nickname,
            "uid="+ "S" +entity.Supplier_ID
        };

        sign_type = "MD5";
        sign = pub.ReturnSignStr(parameters, "utf-8", tradesignkey);

        StringBuilder prestr = new StringBuilder();
        prestr.Append("&service=create_enterprise_member");
        prestr.Append("&version=1.0");
        prestr.Append("&partner_id=" + partner_id);
        prestr.Append("&login_name=" + entity.Supplier_Nickname);
        prestr.Append("&enterprise_name=" + entity.Supplier_CompanyName);
        prestr.Append("&member_name=" + entity.Supplier_Nickname);
        prestr.Append("&uid=" + "S" + entity.Supplier_ID);
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

    public MemberJsonInfo Modify_Enterprise_Info(SupplierInfo entity)
    {
        MemberJsonInfo jsonInfo = null;

        string sign, sign_type;

        string gateway = mgs + "?_input_charset=utf-8";

        List<string> liststr = new List<string>();
        liststr.Add("service=modify_enterprise_info");
        liststr.Add("version=1.0");
        liststr.Add("partner_id=" + partner_id);
        liststr.Add("_input_charset=utf-8");
        liststr.Add("identity_no=S" + entity.Supplier_ID);
        liststr.Add("identity_type=UID");
        if (entity.Supplier_CompanyName != "")
        {
            liststr.Add("enterprise_name=" + entity.Supplier_CompanyName);
        }

        if (entity.Supplier_Nickname != "")
        {
            liststr.Add("member_name=" + entity.Supplier_Nickname);
        }

        if (entity.Supplier_Corporate != "")
        {
            liststr.Add("legal_person=" + entity.Supplier_Corporate);
        }

        if (entity.Supplier_CorporateMobile != "")
        {
            liststr.Add("legal_person_phone=" + entity.Supplier_CorporateMobile);
        }

        if (entity.Supplier_State != "" && entity.Supplier_City != "" && entity.Supplier_County != "")
        {
            liststr.Add("address=" + addr.DisplayAddress(entity.Supplier_State, entity.Supplier_City, entity.Supplier_County));
        }

        if (entity.Supplier_BusinessCode != "")
        {
            liststr.Add("license_no=" + entity.Supplier_BusinessCode);
        }

        if (entity.Supplier_Mobile != "")
        {
            liststr.Add("telephone=" + entity.Supplier_Mobile);
        }

        if (entity.Supplier_OrganizationCode != "")
        {
            liststr.Add("organization_no=" + entity.Supplier_OrganizationCode);
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
        prestr.Append("&identity_no=" + "S" + entity.Supplier_ID);
        prestr.Append("&identity_type=UID");
        if (entity.Supplier_CompanyName != "")
        {
            prestr.Append("&enterprise_name=" + entity.Supplier_CompanyName);
        }

        if (entity.Supplier_Nickname != "")
        {
            prestr.Append("&member_name=" + entity.Supplier_Nickname);
        }

        if (entity.Supplier_Corporate != "")
        {
            prestr.Append("&legal_person=" + entity.Supplier_Corporate);
        }

        if (entity.Supplier_CorporateMobile != "")
        {
            prestr.Append("&legal_person_phone=" + entity.Supplier_CorporateMobile);
        }

        if (entity.Supplier_State != "" && entity.Supplier_City != "" && entity.Supplier_County != "")
        {
            prestr.Append("&address=" + addr.DisplayAddress(entity.Supplier_State, entity.Supplier_City, entity.Supplier_County));
        }

        if (entity.Supplier_BusinessCode != "")
        {
            prestr.Append("&license_no=" + entity.Supplier_BusinessCode);
        }

        if (entity.Supplier_Mobile != "")
        {
            prestr.Append("&telephone=" + entity.Supplier_Mobile);
        }

        if (entity.Supplier_OrganizationCode != "")
        {
            prestr.Append("&organization_no=" + entity.Supplier_OrganizationCode);
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



    //public bool CheckProductCode(string code, int id)
    //{
    //    if (code == null || code.Length == 0) { return false; }


    //    ProductInfo Entity = MyProduct.GetProductByCode(code, pub.GetCurrentSite(), pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));
    //    if (Entity != null)
    //    {
    //        if (id == 0) { return false; }

    //        if (Entity.Product_ID == id) { return true; }
    //        else { return false; }
    //    }
    //    else
    //    {
    //        return true;
    //    }
    //}
    #endregion

    #endregion

    #region 消息通知



    public void Supplier_SysMessageList(int num)
    {
        StringBuilder strHTML = new StringBuilder();
        SupplierInfo supplierInfo = null;
        string supplier_name = "系统";
        PageInfo pageInfo = null;
        int supplier_id = tools.NullInt(Session["supplier_id"]);
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
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_UserType", "=", "2"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_ReceiveID", "=", supplier_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_IsHidden", "=", "0"));
        Query.OrderInfos.Add(new OrderInfo("SysMessageInfo.Message_Addtime", "desc"));
        IList<SysMessageInfo> entitys = MySysMessage.GetSysMessages(Query);
        pageInfo = MySysMessage.GetPageInfo(Query);

        strHTML.Append("<table width=\"975\" border=\"0\" cellspacing=\"2\" cellpadding=\"0\">");
        strHTML.Append("<tr>");
        //strHTML.Append("<td width=\"30\" class=\"name\"></td>");
        strHTML.Append("<td width=\"80\" class=\"name\"  style=\"color:#ff6600\"><input name=\"chk_all_messages\" id=\"chk_all_messages\"  onclick=\"check_Cart_All();\" type=\"checkbox\"  />全选</td>");
        strHTML.Append("<td width=\"70\" class=\"name\">消息ID</td>");
        strHTML.Append("<td width=\"500\" class=\"name\">消息内容</td>");
        strHTML.Append("<td width=\"150\" class=\"name\">消息状态</td>");
        strHTML.Append("<td width=\"173\" class=\"name\">发送时间</td>");
        //strHTML.Append("<td width=\"150\" class=\"name\">操作</td>");
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
                strHTML.Append("<td>" + entity.Message_Content + "</td>");



                //strHTML.Append("<td>" + (entity.Message_Status == 0 ? "未读" : "已读") + "</td>");
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
            strHTML.Append("</table>");
            strHTML.Append("<div class=\"messageflag\"> <a href=\"javascript:;\" onclick=\"SupplierMoveMessagesByID();\">删 除</a> <a href=\"javascript:;\" onclick=\"SupplierAllMoveMessagesByID();\">全部删除</a>   <a href=\"javascript:;\" onclick=\"SupplierMessagesIsReadByID();\">标记为已读</a><a href=\"javascript:;\" onclick=\"SupplierMessagesIsUnReadByID();\">标记为未读</a>  <a href=\"javascript:;\" onclick=\"SupplierAllMessagesIsReadByID();\">全部标记为已读</a>   </div>");
            Response.Write(strHTML.ToString());
            pub.Page(pageInfo.PageCount, pageInfo.CurrentPage, page_url, pageInfo.PageSize, pageInfo.RecordCount);
        }
        else
        {
            strHTML.Append("<tr>");
            strHTML.Append("<td colspan=\"5\" style=\"text-align:center;\">暂无消息通知！</td>");
            strHTML.Append("</tr>");
            strHTML.Append("</table>");
            Response.Write(strHTML.ToString());
            //pub.Page(pageInfo.PageCount, pageInfo.CurrentPage, page_url, pageInfo.PageSize, pageInfo.RecordCount);
        }
        //strHTML.Append("<div> <a href=\"javascript:;\" onclick=\"SupplierMoveMessagesByID();\">删除</a> <a href=\"javascript:;\" onclick=\"SupplierAllMoveMessagesByID();\">全部删除</a>    <a href=\"javascript:;\" onclick=\"SupplierMessagesIsReadByID();\">标记为已读</a> <a href=\"javascript:;\" onclick=\"SupplierMessagesIsUnReadByID();\">标记为未读</a>   <a href=\"javascript:;\" onclick=\"SupplierAllMessagesIsReadByID();\">全部标记为已读</a>   </div>");



        //Response.Write(strHTML.ToString());
        //pub.Page(pageInfo.PageCount, pageInfo.CurrentPage, page_url, pageInfo.PageSize, pageInfo.RecordCount);
    }


    //删除选中的消息
    public void SupplierMessges_Del(bool IsAllMessage)
    {
        IList<SysMessageInfo> entitys = null;
        MemberInfo memberinfo = new Member().GetMemberByID();
        int membersupplier_ID = -1;
        if (memberinfo != null)
        {
            membersupplier_ID = memberinfo.Member_SupplierID;
        }


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
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_UserType", "=", "2"));

            Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_ReceiveID", "=", membersupplier_ID.ToString()));


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
    public void SupplierMessges_IsRead(int MessageStatus)
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


            }
        }

    }


    public void AllSupplierMessges_IsRead(int MessageStatus)
    {
        MemberInfo memberinfo = new Member().GetMemberByID();
        int membersupplier_ID = -1;
        if (memberinfo != null)
        {
            membersupplier_ID = memberinfo.Member_SupplierID;
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "SysMessageInfo.Message_Site", "=", pub.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_UserType", "=", "2"));

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "SysMessageInfo.Message_ReceiveID", "=", membersupplier_ID.ToString()));

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
}


