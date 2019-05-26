using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.ORD;
using Glaer.Trade.Util.Http;
using System.Text;
using System.Collections.Specialized;
using System.Net;
using System.Security.Cryptography;

/// <summary>
/// Payment 的摘要说明
/// </summary>
public class Payment
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IEncrypt encrypt;
    private IOrders MyOrders;
    private IOrdersLog Mylog;
    private IHttpHelper HttpHelper;
    private IJsonHelper JsonHelper;
    private IOrdersLoanApply MyLoanApply;
    private IOrdersPayment MyPayment;

    private Public_Class pub = new Public_Class();
    private Supplier supplier = new Supplier();
    private PageURL pageurl = new PageURL();
    private Credit credit = new Credit();

    string tradesignkey;
    string mag_url;

	public Payment()
	{
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();

        HttpHelper = HttpHelperFactory.CreateHttpHelper();
        JsonHelper = JsonHelperFactory.CreateJsonHelper();
        MyLoanApply = OrdersFactory.CreateOrdersLoanApply();
        MyOrders = OrdersFactory.CreateOrders();
        Mylog = OrdersLogFactory.CreateOrdersLog();
        encrypt = EncryptFactory.CreateEncrypt();
        MyPayment = OrdersPaymentFactory.CreateOrdersPayment();

        tradesignkey = System.Web.Configuration.WebConfigurationManager.AppSettings["tradesignkey"].ToString();
        mag_url = System.Web.Configuration.WebConfigurationManager.AppSettings["mag"].ToString();
	}

    #region  辅助函数

    public static string GetMD5(string s, string _input_charset)
    {
        /// <summary>
        /// 与ASP兼容的MD5加密算法
        /// </summary>

        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] t = md5.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
        StringBuilder sb = new StringBuilder(32);
        for (int i = 0; i < t.Length; i++)
        {
            sb.Append(t[i].ToString("x").PadLeft(2, '0'));
        }
        return sb.ToString();

    }

    public static string[] BubbleSort(string[] r)
    {
        /// <summary>
        /// 冒泡排序法
        /// </summary>

        int i, j; //交换标志 
        string temp;

        bool exchange;

        for (i = 0; i < r.Length; i++) //最多做R.Length-1趟排序 
        {
            exchange = false; //本趟排序开始前，交换标志应为假

            for (j = r.Length - 2; j >= i; j--)
            {
                if (System.String.CompareOrdinal(r[j + 1], r[j]) < 0)　//交换条件
                {
                    temp = r[j + 1];
                    r[j + 1] = r[j];
                    r[j] = temp;

                    exchange = true; //发生了交换，故将交换标志置为真 
                }
            }

            if (!exchange) //本趟排序未发生交换，提前终止算法 
            {
                break;
            }
        }
        return r;
    }

    public static string CreatUrl(string[] para, string _input_charset, string sign_type, string key)
    {
        int i = 0;

        //进行排序； 
        string[] Sortedstr = BubbleSort(para);


        //构造待md5摘要字符串 ； 

        StringBuilder prestr = new StringBuilder();

        for (i = 0; i <= Sortedstr.Length - 1; i++)
        {

            if (i == Sortedstr.Length - 1)
            {
                prestr.Append(Sortedstr[i]);
            }
            else
            {
                prestr.Append(Sortedstr[i] + "&");

            }
        }

        prestr.Append(key);

        //生成Md5摘要； 
        string sign = GetMD5(prestr.ToString(), _input_charset);
        // 
        // //返回MD5加密串；  
        return sign;
    }

    public static int ReturnStrLength(string strParam)
    {
        return strParam.Length;
    }

    /// <summary>
    /// 维金支付交易列表
    /// </summary>
    /// <param name="ordersInfo"></param>
    /// <returns></returns>
    public string GetTradeList(int orders_id)
    {
        StringBuilder strHTML = new StringBuilder();

        SupplierInfo supplierInfo = null;
        int supplier_id = 0;
        string supplier_mobile = "";
        int i = 0;
        OrdersInfo ordersInfo = MyOrders.GetOrdersByID(orders_id);
        if (ordersInfo != null)
        {
            IList<OrdersGoodsInfo> goodslist = MyOrders.GetGoodsListByOrderID(ordersInfo.Orders_ID);
            if (goodslist != null)
            {
                foreach (OrdersGoodsInfo goodsInfo in goodslist)
                {
                    i++;
                    supplierInfo = supplier.GetSupplierByID(goodsInfo.Orders_Goods_Product_SupplierID);
                    if (supplierInfo != null)
                    {
                        supplier_id = supplierInfo.Supplier_ID;
                        supplier_mobile = supplierInfo.Supplier_Mobile;
                    }

                    if (i == goodslist.Count)
                    {
                        strHTML.Append(ReturnStrLength(ordersInfo.Orders_SN) + ":" + ordersInfo.Orders_SN);//商户网站唯一订单号
                        strHTML.Append("~" + ReturnStrLength(goodsInfo.Orders_Goods_Product_Name) + ":" + goodsInfo.Orders_Goods_Product_Name);//商品名称
                        strHTML.Append("~" + ReturnStrLength(goodsInfo.Orders_Goods_Product_Price.ToString()) + ":" + goodsInfo.Orders_Goods_Product_Price);//商品单价
                        strHTML.Append("~" + ReturnStrLength(goodsInfo.Orders_Goods_Amount.ToString()) + ":" + goodsInfo.Orders_Goods_Amount);//商品数量
                        strHTML.Append("~" + ReturnStrLength(ordersInfo.Orders_Total_AllPrice.ToString()) + ":" + ordersInfo.Orders_Total_AllPrice);//交易金额
                        strHTML.Append("~" + ReturnStrLength("") + ":");//交易金额分润账号集
                        strHTML.Append("~" + ReturnStrLength("s" + supplier_id.ToString()) + ":" + "s" + supplier_id.ToString());//卖家标示ID
                        strHTML.Append("~" + ReturnStrLength("UID") + ":UID");//卖家标示ID类型
                        strHTML.Append("~" + ReturnStrLength(supplier_mobile) + ":" + supplier_mobile);//卖家手机号
                        strHTML.Append("~" + ReturnStrLength("") + ":");//使用订金金额
                        strHTML.Append("~" + ReturnStrLength("") + ":");//订金下订订单号
                        strHTML.Append("~" + ReturnStrLength("") + ":");//商品描述
                        strHTML.Append("~" + ReturnStrLength("") + ":");//商品展示URL
                        strHTML.Append("~" + ReturnStrLength(ordersInfo.Orders_Addtime.ToString("yyyyMMddHHmmss")) + ":" + ordersInfo.Orders_Addtime.ToString("yyyyMMddHHmmss") + "");//商户订单提交时间
                        strHTML.Append("~" + ReturnStrLength("") + ":");//服务器异步通知页面路径
                        strHTML.Append("~" + ReturnStrLength("24h") + ":24h");//支付过期时间
                        strHTML.Append("~" + ReturnStrLength("") + ":");//店铺名称
                        strHTML.Append("~" + ReturnStrLength("Y") + ":Y");//B2B信贷来源
                    }
                    else
                    {
                        strHTML.Append(ReturnStrLength(ordersInfo.Orders_SN) + ":" + ordersInfo.Orders_SN);//商户网站唯一订单号
                        strHTML.Append("~" + ReturnStrLength(goodsInfo.Orders_Goods_Product_Name) + ":" + goodsInfo.Orders_Goods_Product_Name);//商品名称
                        strHTML.Append("~" + ReturnStrLength(goodsInfo.Orders_Goods_Product_Price.ToString()) + ":" + goodsInfo.Orders_Goods_Product_Price);//商品单价
                        strHTML.Append("~" + ReturnStrLength(goodsInfo.Orders_Goods_Amount.ToString()) + ":" + goodsInfo.Orders_Goods_Amount);//商品数量
                        strHTML.Append("~" + ReturnStrLength(ordersInfo.Orders_Total_AllPrice.ToString()) + ":" + ordersInfo.Orders_Total_AllPrice);//交易金额
                        strHTML.Append("~" + ReturnStrLength("") + ":");//交易金额分润账号集
                        strHTML.Append("~" + ReturnStrLength("s" + supplier_id.ToString()) + ":" + "s" + supplier_id.ToString());//卖家标示ID
                        strHTML.Append("~" + ReturnStrLength("UID") + ":UID");//卖家标示ID类型
                        strHTML.Append("~" + ReturnStrLength(supplier_mobile) + ":" + supplier_mobile);//卖家手机号
                        strHTML.Append("~" + ReturnStrLength("") + ":");//使用订金金额
                        strHTML.Append("~" + ReturnStrLength("") + ":");//订金下订订单号
                        strHTML.Append("~" + ReturnStrLength("") + ":");//商品描述
                        strHTML.Append("~" + ReturnStrLength("" + pageurl.FormatURL(pageurl.product_detail, goodsInfo.Orders_Goods_Product_ID.ToString()) + "") + ":" + pageurl.FormatURL(pageurl.product_detail, goodsInfo.Orders_Goods_Product_ID.ToString()) + "");//商品展示URL
                        strHTML.Append("~" + ReturnStrLength(ordersInfo.Orders_Addtime.ToString("yyyyMMddHHmmss")) + ":" + ordersInfo.Orders_Addtime.ToString("yyyyMMddHHmmss") + "");//商户订单提交时间
                        strHTML.Append("~" + ReturnStrLength("") + ":");//服务器异步通知页面路径
                        strHTML.Append("~" + ReturnStrLength("24h") + ":24h");//支付过期时间
                        strHTML.Append("~" + ReturnStrLength("") + ":");//店铺名称
                        strHTML.Append("~" + ReturnStrLength("Y") + ":Y");//B2B信贷来源

                        strHTML.Append("$");
                    }
                }
            }
        }

        return strHTML.ToString();
    }

    public string BindingInstantTradeList(int orders_id, string notify_url)
    {
        StringBuilder strHTML = new StringBuilder();

        SupplierInfo supplierInfo = null;
        int supplier_id = 0;
        string supplier_mobile = "";

        int i = 0;
        OrdersInfo ordersInfo = MyOrders.GetOrdersByID(orders_id);
        if (ordersInfo != null)
        {
            supplierInfo = supplier.GetSupplierByID(ordersInfo.Orders_SupplierID);
            if (supplierInfo != null)
            {
                supplier_id = supplierInfo.Supplier_ID;
                supplier_mobile = supplierInfo.Supplier_Mobile;
            }

            strHTML.Append(ReturnStrLength(ordersInfo.Orders_SN) + ":" + ordersInfo.Orders_SN);//商户网站唯一订单号
            strHTML.Append("~" + ReturnStrLength("订单" + ordersInfo.Orders_SN) + ":订单" + ordersInfo.Orders_SN);//商品名称
            strHTML.Append("~" + ReturnStrLength(ordersInfo.Orders_Total_AllPrice.ToString()) + ":" + ordersInfo.Orders_Total_AllPrice);//商品单价
            strHTML.Append("~" + ReturnStrLength("1") + ":1");//商品数量
            strHTML.Append("~" + ReturnStrLength(ordersInfo.Orders_Total_AllPrice.ToString()) + ":" + ordersInfo.Orders_Total_AllPrice);//交易金额
            strHTML.Append("~" + ReturnStrLength("") + ":");//交易金额分润账号集
            strHTML.Append("~" + ReturnStrLength("s" + supplier_id.ToString()) + ":" + "s" + supplier_id.ToString());//卖家标示ID
            strHTML.Append("~" + ReturnStrLength("UID") + ":UID");//卖家标示ID类型
            strHTML.Append("~" + ReturnStrLength(supplier_mobile) + ":" + supplier_mobile);//卖家手机号
            strHTML.Append("~" + ReturnStrLength("") + ":");//使用订金金额
            strHTML.Append("~" + ReturnStrLength("") + ":");//订金下订订单号
            strHTML.Append("~" + ReturnStrLength("订单" + ordersInfo.Orders_SN) + ":订单" + ordersInfo.Orders_SN + "");//商品描述
            strHTML.Append("~" + ReturnStrLength(Application["Site_URL"] + "/member/order_detail.aspx?orders_sn=" + ordersInfo.Orders_SN) + ":" + Application["Site_URL"] + "/member/order_detail.aspx?orders_sn=" + ordersInfo.Orders_SN);//商品展示URL
            strHTML.Append("~" + ReturnStrLength(ordersInfo.Orders_Addtime.ToString("yyyyMMddHHmmss")) + ":" + ordersInfo.Orders_Addtime.ToString("yyyyMMddHHmmss"));//商户订单提交时间
            strHTML.Append("~" + ReturnStrLength(notify_url) + ":" + notify_url);//服务器异步通知页面路径
            strHTML.Append("~" + ReturnStrLength("24h") + ":24h");//支付过期时间
            strHTML.Append("~" + ReturnStrLength("") + ":");//店铺名称
            strHTML.Append("~" + ReturnStrLength("Y") + ":Y");//B2B信贷来源
        }
        return strHTML.ToString();
    }

    public string BindingEnsureTradeList(int orders_id, string notify_url)
    {
        StringBuilder strHTML = new StringBuilder();

        SupplierInfo supplierInfo = null;
        SupplierShopInfo shopInfo = null;
        int supplier_id = 0;
        string supplier_mobile = "--";
        string Shop_Name = "--";

        int i = 0;
        OrdersInfo ordersInfo = MyOrders.GetOrdersByID(orders_id);
        if (ordersInfo != null)
        {
            supplierInfo = supplier.GetSupplierByID(ordersInfo.Orders_SupplierID);
            if (supplierInfo != null)
            {
                supplier_id = supplierInfo.Supplier_ID;
                supplier_mobile = supplierInfo.Supplier_Mobile;

                shopInfo = supplier.GetSupplierShopBySupplierID(supplierInfo.Supplier_ID);
                if (shopInfo != null)
                {
                    Shop_Name = shopInfo.Shop_Name;
                }
            }

            strHTML.Append(ReturnStrLength(ordersInfo.Orders_SN) + ":" + ordersInfo.Orders_SN);//1商户网站唯一订单号
            strHTML.Append("~" + ReturnStrLength("订单" + ordersInfo.Orders_SN) + ":订单" + ordersInfo.Orders_SN);//2商品名称
            strHTML.Append("~" + ReturnStrLength(ordersInfo.Orders_Total_AllPrice.ToString()) + ":" + ordersInfo.Orders_Total_AllPrice);//3商品单价
            strHTML.Append("~" + ReturnStrLength("1") + ":1");//4商品数量
            strHTML.Append("~" + ReturnStrLength(ordersInfo.Orders_Total_AllPrice.ToString()) + ":" + ordersInfo.Orders_Total_AllPrice);//5交易金额
            strHTML.Append("~" + ReturnStrLength(ordersInfo.Orders_Total_AllPrice.ToString()) + ":" + ordersInfo.Orders_Total_AllPrice);//6担保金额
            strHTML.Append("~" + ReturnStrLength("s" + supplier_id.ToString()) + ":" + "s" + supplier_id.ToString());//7卖家标示ID
            strHTML.Append("~" + ReturnStrLength("UID") + ":UID");//8卖家标示ID类型
            strHTML.Append("~" + ReturnStrLength(supplier_mobile) + ":" + supplier_mobile);//9卖家手机号
            strHTML.Append("~" + ReturnStrLength("") + ":");//10使用订金金额
            strHTML.Append("~" + ReturnStrLength("") + ":");//11订金下订订单号
            strHTML.Append("~" + ReturnStrLength(Application["Site_URL"] + "/member/order_detail.aspx?orders_sn=" + ordersInfo.Orders_SN) + ":" + Application["Site_URL"] + "/member/order_detail.aspx?orders_sn=" + ordersInfo.Orders_SN);//12商品展示URL
            strHTML.Append("~" + ReturnStrLength("订单" + ordersInfo.Orders_SN) + ":订单" + ordersInfo.Orders_SN + "");//13商品描述
            strHTML.Append("~" + ReturnStrLength(ordersInfo.Orders_Addtime.ToString("yyyyMMddHHmmss")) + ":" + ordersInfo.Orders_Addtime.ToString("yyyyMMddHHmmss"));//14商户订单提交时间
            strHTML.Append("~" + ReturnStrLength(notify_url) + ":" + notify_url);//15服务器异步通知页面路径
            strHTML.Append("~" + ReturnStrLength("24h") + ":24h");//16支付过期时间
            strHTML.Append("~" + ReturnStrLength(Shop_Name) + ":" + Shop_Name);//17店铺名称
            strHTML.Append("~" + ReturnStrLength("Y") + ":Y");//18B2B信贷来源
        }
        return strHTML.ToString();
    }

    #endregion

    #region 维金

    //即时到账交易网关接口
    public void Create_Instant_Trade(string request_no, string trade_list, string buyer_id, string buyer_id_type, string buyer_mobile, string return_url)
    {
        string partner_id = tools.NullStr(Application["CreditPayment_Code"]);
        if (partner_id == "")
        {
            pub.Msg("error", "错误提示", "暂时不支持支付", false, 3, "{back}");
            return;
        }

        string sign, sign_type;
        string b2b_token = tools.NullStr(Session["member_token"]);
        string gateway = mag_url + "?_input_charset=utf-8";

        string[] parameters = 
        {
            "service=create_instant_trade",
            "version=1.0",
            "partner_id="+partner_id,
            "_input_charset=utf-8",
            "return_url="+return_url,
            "request_no="+request_no,
            "trade_list="+trade_list,
            "buyer_id="+ buyer_id,
            "buyer_id_type="+buyer_id_type,
            "buyer_mobile="+buyer_mobile,
            "buyer_ip="+Request.ServerVariables["Remote_Addr"],
            "is_web_access=Y",
            "go_cashier=Y",
            "b2b_token="+b2b_token
        };

        sign_type = "MD5";
        sign = CreatUrl(parameters, "utf-8", sign_type, tradesignkey);

        string str = "service=create_instant_trade&partner_id=" + partner_id + "&version=1.0&return_url=" + return_url + "&sign=" + sign + "&sign_type=" + sign_type + "&request_no=" + request_no + "&trade_list=" + trade_list + "&buyer_id=" + buyer_id + "&buyer_id_type=" + buyer_id_type + "&buyer_mobile=" + buyer_mobile + "&buyer_ip=" + Request.ServerVariables["Remote_Addr"] + "&is_web_access=Y&go_cashier=Y&b2b_token=" + b2b_token;

        pub.AddSysInterfaceLog(3, "即时到账交易", "成功", str, "记录交易参数");

        Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
        Response.Write("<head>");
        Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
        Response.Write("<title>PayGate</title>");
        Response.Write("</head>");

        Response.Write("<body onload=\"javascript:document.frm_pay.submit();\">");
        Response.Write("<form action=\"" + gateway + "\" method=\"POST\" name=\"frm_pay\">");

        //支付即时到帐
        Response.Write("<input type=\"hidden\" name=\"service\" value=\"create_instant_trade\">");
        Response.Write("<input type=\"hidden\" name=\"partner_id\" value=\"" + partner_id + "\">");
        Response.Write("<input type=\"hidden\" name=\"version\" value=\"1.0\">");
        Response.Write("<input type=\"hidden\" name=\"return_url\" value=\"" + return_url + "\">");
        Response.Write("<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\">");
        Response.Write("<input type=\"hidden\" name=\"sign_type\" value=\"" + sign_type + "\">");
        Response.Write("<input type=\"hidden\" name=\"request_no\" value=\"" + request_no + "\">");
        Response.Write("<input type=\"hidden\" name=\"trade_list\" value=\"" + trade_list + "\">");
        Response.Write("<input type=\"hidden\" name=\"buyer_id\" value=\"" + buyer_id + "\">");
        Response.Write("<input type=\"hidden\" name=\"buyer_id_type\" value=\"" + buyer_id_type + "\">");
        Response.Write("<input type=\"hidden\" name=\"buyer_mobile\" value=\"" + buyer_mobile + "\">");
        Response.Write("<input type=\"hidden\" name=\"buyer_ip\" value=\"" + Request.ServerVariables["Remote_Addr"] + "\">");
        Response.Write("<input type=\"hidden\" name=\"is_web_access\" value=\"Y\">");
        Response.Write("<input type=\"hidden\" name=\"go_cashier\" value=\"Y\">");
        Response.Write("<input type=\"hidden\" name=\"b2b_token\" value=\"" + b2b_token + "\">");
        Response.Write("</form>");
        Response.Write("</body>");
        Response.Write("</html>");
    }

    //担保交易网关接口
    public void Create_Ensure_Trade(string request_no, string trade_list, string buyer_id, string buyer_id_type, string buyer_mobile, string return_url)
    {
        string partner_id = tools.NullStr(Application["CreditPayment_Code"]);
        if (partner_id == "")
        {
            pub.Msg("error", "错误提示", "暂时不支持支付", false, 3, "{back}");
            return;
        }

        string sign, sign_type;
        string gateway = mag_url + "?_input_charset=utf-8";

        string[] parameters = 
        {
            "service=create_ensure_trade",
            "version=1.0",
            "partner_id="+partner_id,
            "_input_charset=utf-8",
            "return_url="+return_url,
            "request_no="+request_no,
            "trade_list="+trade_list,
            "buyer_id="+ buyer_id,
            "buyer_id_type="+buyer_id_type,
            "buyer_mobile="+buyer_mobile,
            "buyer_ip="+Request.ServerVariables["Remote_Addr"],
            "is_web_access=Y",
            "go_cashier=N",
            "b2b_token="+tools.NullStr(Session["member_token"])
        };

        sign_type = "MD5";
        sign = CreatUrl(parameters, "utf-8", sign_type, tradesignkey);

        Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
        Response.Write("<head>");
        Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
        Response.Write("<title>PayGate</title>");
        Response.Write("</head>");

        Response.Write("<body onload=\"javascript:document.frm_pay.submit();\">");
        Response.Write("<form action=\"" + gateway + "\" method=\"POST\" name=\"frm_pay\">");

        //支付担保交易
        Response.Write("<input type=\"hidden\" name=\"service\" value=\"create_ensure_trade\">");
        Response.Write("<input type=\"hidden\" name=\"partner_id\" value=\"" + partner_id + "\">");
        Response.Write("<input type=\"hidden\" name=\"version\" value=\"1.0\">");
        Response.Write("<input type=\"hidden\" name=\"return_url\" value=\"" + return_url + "\">");
        Response.Write("<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\">");
        Response.Write("<input type=\"hidden\" name=\"sign_type\" value=\"" + sign_type + "\">");
        Response.Write("<input type=\"hidden\" name=\"request_no\" value=\"" + request_no + "\">");
        Response.Write("<input type=\"hidden\" name=\"trade_list\" value=\"" + trade_list + "\">");
        Response.Write("<input type=\"hidden\" name=\"buyer_id\" value=\"" + buyer_id + "\">");
        Response.Write("<input type=\"hidden\" name=\"buyer_id_type\" value=\"" + buyer_id_type + "\">");
        Response.Write("<input type=\"hidden\" name=\"buyer_mobile\" value=\"" + buyer_mobile + "\">");
        Response.Write("<input type=\"hidden\" name=\"buyer_ip\" value=\"" + Request.ServerVariables["Remote_Addr"] + "\">");
        Response.Write("<input type=\"hidden\" name=\"is_web_access\" value=\"Y\">");
        Response.Write("<input type=\"hidden\" name=\"go_cashier\" value=\"N\">");
        Response.Write("<input type=\"hidden\" name=\"b2b_token\" value=\"" + tools.NullStr(Session["member_token"]) + "\">");
        Response.Write("</form>");
        Response.Write("</body>");
        Response.Write("</html>");
    }


    public TradeJsonInfo Create_Ensure_Trade_Get(string request_no, string trade_list, string buyer_id, string buyer_id_type, string buyer_mobile, string return_url)
    {
        TradeJsonInfo JsonInfo = null;
        string partner_id = tools.NullStr(Application["CreditPayment_Code"]);
        if (partner_id == "")
        {
            pub.Msg("error", "错误提示", "暂时不支持支付", false, 3, "{back}");
        }

        string sign, sign_type;
        string gateway = mag_url + "?_input_charset=utf-8";

        string[] parameters = 
        {
            "service=create_ensure_trade",
            "version=1.0",
            "partner_id="+partner_id,
            "_input_charset=utf-8",
            "return_url="+return_url,
            "request_no="+request_no,
            "trade_list="+trade_list,
            "buyer_id="+ buyer_id,
            "buyer_id_type="+buyer_id_type,
            "buyer_mobile="+buyer_mobile,
            "buyer_ip="+Request.ServerVariables["Remote_Addr"],
            "is_web_access=Y",
            "go_cashier=N",
            "b2b_token="+tools.NullStr(Session["member_token"])
        };

        sign_type = "MD5";
        sign = CreatUrl(parameters, "utf-8", sign_type, tradesignkey);

        StringBuilder prestr = new StringBuilder();

        prestr.Append("&service=create_ensure_trade");
        prestr.Append("&version=1.0");
        prestr.Append("&partner_id=" + partner_id);
        prestr.Append("&sign=" + sign);
        prestr.Append("&sign_type=" + sign_type);
        prestr.Append("&return_url=" + return_url);
        prestr.Append("&request_no=" + request_no);
        prestr.Append("&trade_list=" + trade_list);
        prestr.Append("&buyer_id=" + buyer_id);
        prestr.Append("&buyer_id_type=" + buyer_id_type);
        prestr.Append("&buyer_mobile=" + buyer_mobile);
        prestr.Append("&buyer_ip=" + Request.ServerVariables["Remote_Addr"]);
        prestr.Append("&is_web_access=Y");
        prestr.Append("&go_cashier=N");
        prestr.Append("&b2b_token=" + tools.NullStr(Session["member_token"]));

        string request_url = gateway + prestr.ToString();

        CookieCollection cookies = new CookieCollection();

        string strJson = HttpHelper.GetResponseString(HttpHelper.CreateGetHttpResponse(request_url, 0, "", cookies));

        JsonInfo = JsonHelper.JSONToObject<TradeJsonInfo>(strJson);

        if (JsonInfo != null && JsonInfo.Is_success == "T")
        {
            pub.AddSysInterfaceLog(3, "创建担保交易", "成功", request_url, "担保交易创建成功!返回收银台地址："+JsonInfo.Cashier_url);
        }
        else
        {
            pub.AddSysInterfaceLog(3, "创建担保交易", "失败", request_url, JsonInfo.Error_code + ":" + JsonInfo.Error_message);
        }

        return JsonInfo;
    }

    //结算网关接口
    public void Create_Settle(string request_no, string outer_trade_no, string operator_id)
    {
        string partner_id = tools.NullStr(Application["CreditPayment_Code"]);
        if (partner_id == "")
        {
            pub.Msg("error", "错误提示", "暂时不支持支付", false, 3, "{back}");
            return;
        }

        string sign, sign_type;
        string gateway = mag_url + "?_input_charset=utf-8";

        string[] parameters = 
        {
            "service=create_settle",
            "version=1.0",
            "partner_id="+partner_id,
            "_input_charset=utf-8",
            "request_no="+request_no,
            "outer_trade_no="+outer_trade_no,
            "b2b_token="+tools.NullStr(Session["member_token"])
        };

        sign_type = "MD5";
        sign = CreatUrl(parameters, "utf-8", sign_type, tradesignkey);

        Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
        Response.Write("<head>");
        Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
        Response.Write("<title>PayGate</title>");
        Response.Write("</head>");

        Response.Write("<body onload=\"javascript:document.frm_pay.submit();\">");
        Response.Write("<form action=\"" + gateway + "\" method=\"POST\" name=\"frm_pay\">");

        //支付即时到帐
        Response.Write("<input type=\"hidden\" name=\"service\" value=\"create_settle\">");
        Response.Write("<input type=\"hidden\" name=\"partner_id\" value=\"" + partner_id + "\">");
        Response.Write("<input type=\"hidden\" name=\"version\" value=\"1.0\">");
        Response.Write("<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\">");
        Response.Write("<input type=\"hidden\" name=\"sign_type\" value=\"" + sign_type + "\">");
        Response.Write("<input type=\"hidden\" name=\"request_no\" value=\"" + request_no + "\">");
        Response.Write("<input type=\"hidden\" name=\"outer_trade_no\" value=\"" + outer_trade_no + "\">");
        Response.Write("<input type=\"hidden\" name=\"b2b_token\" value=\"" + tools.NullStr(Session["member_token"]) + "\">");
        Response.Write("</form>");
        Response.Write("</body>");
        Response.Write("</html>");
    }

    //退款网关接口
    public void Create_Refund(string outer_trade_no, string orig_outer_trade_no, string refund_amount, string refund_ensure_amount, string royalty_parameters, string operator_id, string notify_url)
    {
        string partner_id = tools.NullStr(Application["CreditPayment_Code"]);
        if (partner_id == "")
        {
            pub.Msg("error", "错误提示", "暂时不支持支付", false, 3, "{back}");
            return;
        }

        string sign, sign_type;
        string gateway = mag_url + "?_input_charset=utf-8";

        string[] parameters = 
        {
            "service=create_refund",
            "version=1.0",
            "partner_id="+partner_id,
            "_input_charset=utf-8",
            "outer_trade_no="+outer_trade_no,
            "orig_outer_trade_no="+orig_outer_trade_no,
            "refund_amount="+refund_amount,
            "b2b_token="+tools.NullStr(Session["member_token"])
        };

        sign_type = "MD5";
        sign = CreatUrl(parameters, "utf-8", sign_type, tradesignkey);

        Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
        Response.Write("<head>");
        Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
        Response.Write("<title>PayGate</title>");
        Response.Write("</head>");

        Response.Write("<body onload=\"javascript:document.frm_pay.submit();\">");
        Response.Write("<form action=\"" + gateway + "\" method=\"POST\" name=\"frm_pay\">");

        //支付即时到帐
        Response.Write("<input type=\"hidden\" name=\"service\" value=\"create_refund\">");
        Response.Write("<input type=\"hidden\" name=\"partner_id\" value=\"" + partner_id + "\">");
        Response.Write("<input type=\"hidden\" name=\"version\" value=\"1.0\">");
        Response.Write("<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\">");
        Response.Write("<input type=\"hidden\" name=\"sign_type\" value=\"" + sign_type + "\">");
        Response.Write("<input type=\"hidden\" name=\"outer_trade_no\" value=\"" + outer_trade_no + "\">");
        Response.Write("<input type=\"hidden\" name=\"orig_outer_trade_no\" value=\"" + orig_outer_trade_no + "\">");
        Response.Write("<input type=\"hidden\" name=\"refund_amount\" value=\"" + refund_amount + "\">");
        Response.Write("<input type=\"hidden\" name=\"b2b_token\" value=\"" + tools.NullStr(Session["member_token"]) + "\">");
        Response.Write("</form>");
        Response.Write("</body>");
        Response.Write("</html>");
    }

    //交易查询接口
    public void Query_Trade(string outer_trade_no, string trade_type, string start_time, string end_time, string inner_trade_no)
    {
        string partner_id = tools.NullStr(Application["CreditPayment_Code"]);
        if (partner_id == "")
        {
            pub.Msg("error", "错误提示", "暂时不支持支付", false, 3, "{back}");
            return;
        }

        string sign, sign_type;
        string gateway = mag_url + "?_input_charset=utf-8";

        string[] parameters = 
        {
            "service=query_trade",
            "version=1.0",
            "partner_id="+partner_id,
            "_input_charset=utf-8",
            "outer_trade_no="+outer_trade_no,
            "trade_type="+trade_type,
            "b2b_token="+tools.NullStr(Session["member_token"])
        };

        sign_type = "MD5";
        sign = CreatUrl(parameters, "utf-8", sign_type, tradesignkey);

        Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
        Response.Write("<head>");
        Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
        Response.Write("<title>PayGate</title>");
        Response.Write("</head>");

        Response.Write("<body onload=\"javascript:document.frm_pay.submit();\">");
        Response.Write("<form action=\"" + gateway + "\" method=\"POST\" name=\"frm_pay\">");

        //支付即时到帐
        Response.Write("<input type=\"hidden\" name=\"service\" value=\"query_trade\">");
        Response.Write("<input type=\"hidden\" name=\"partner_id\" value=\"" + partner_id + "\">");
        Response.Write("<input type=\"hidden\" name=\"version\" value=\"1.0\">");
        Response.Write("<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\">");
        Response.Write("<input type=\"hidden\" name=\"sign_type\" value=\"" + sign_type + "\">");
        Response.Write("<input type=\"hidden\" name=\"outer_trade_no\" value=\"" + outer_trade_no + "\">");
        Response.Write("<input type=\"hidden\" name=\"trade_type\" value=\"" + trade_type + "\">");
        Response.Write("<input type=\"hidden\" name=\"b2b_token\" value=\"" + tools.NullStr(Session["member_token"]) + "\">");
        Response.Write("</form>");
        Response.Write("</body>");
        Response.Write("</html>");
    }

    //交易取消接口
    public void Cancel_Trade(string request_no, string outer_trade_no, string reason)
    {
        string partner_id = tools.NullStr(Application["CreditPayment_Code"]);
        if (partner_id == "")
        {
            pub.Msg("error", "错误提示", "暂时不支持支付", false, 3, "{back}");
            return;
        }

        string sign, sign_type;
        string gateway = mag_url + "?_input_charset=utf-8";

        string[] parameters = 
        {
            "service=cancel_trade",
            "version=1.0",
            "partner_id="+partner_id,
            "_input_charset=utf-8",
            "outer_trade_no="+outer_trade_no,
            "request_no="+request_no,
            "b2b_token="+tools.NullStr(Session["member_token"])
        };

        sign_type = "MD5";
        sign = CreatUrl(parameters, "utf-8", sign_type, tradesignkey);

        Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
        Response.Write("<head>");
        Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
        Response.Write("<title>PayGate</title>");
        Response.Write("</head>");

        Response.Write("<body onload=\"javascript:document.frm_pay.submit();\">");
        Response.Write("<form action=\"" + gateway + "\" method=\"POST\" name=\"frm_pay\">");

        //支付即时到帐
        Response.Write("<input type=\"hidden\" name=\"service\" value=\"cancel_trade\">");
        Response.Write("<input type=\"hidden\" name=\"partner_id\" value=\"" + partner_id + "\">");
        Response.Write("<input type=\"hidden\" name=\"version\" value=\"1.0\">");
        Response.Write("<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\">");
        Response.Write("<input type=\"hidden\" name=\"sign_type\" value=\"" + sign_type + "\">");
        Response.Write("<input type=\"hidden\" name=\"outer_trade_no\" value=\"" + outer_trade_no + "\">");
        Response.Write("<input type=\"hidden\" name=\"request_no\" value=\"" + request_no + "\">");
        Response.Write("<input type=\"hidden\" name=\"b2b_token\" value=\"" + tools.NullStr(Session["member_token"]) + "\">");
        Response.Write("</form>");
        Response.Write("</body>");
        Response.Write("</html>");
    }

    //转账网关接口
    public void Blanace_Transfer(string outer_trade_no)
    {
        string partner_id = tools.NullStr(Application["CreditPayment_Code"]);
        if (partner_id == "")
        {
            pub.Msg("error", "错误提示", "暂时不支持支付", false, 3, "{back}");
            return;
        }

        string sign, sign_type;
        string gateway = mag_url + "?_input_charset=utf-8";

        string[] parameters = 
        {
            "service=cancel_trade",
            "version=1.0",
            "partner_id="+partner_id,
            "_input_charset=utf-8",
            "outer_trade_no="+outer_trade_no,
            "request_no=",
            "b2b_token="+tools.NullStr(Session["member_token"])
        };

        sign_type = "MD5";
        sign = CreatUrl(parameters, "utf-8", sign_type, tradesignkey);

        Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
        Response.Write("<head>");
        Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
        Response.Write("<title>PayGate</title>");
        Response.Write("</head>");

        Response.Write("<body onload=\"javascript:document.frm_pay.submit();\">");
        Response.Write("<form action=\"" + gateway + "\" method=\"POST\" name=\"frm_pay\">");

        //支付即时到帐
        Response.Write("<input type=\"hidden\" name=\"service\" value=\"cancel_trade\">");
        Response.Write("<input type=\"hidden\" name=\"partner_id\" value=\"" + partner_id + "\">");
        Response.Write("<input type=\"hidden\" name=\"version\" value=\"1.0\">");
        Response.Write("<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\">");
        Response.Write("<input type=\"hidden\" name=\"sign_type\" value=\"" + sign_type + "\">");
        Response.Write("<input type=\"hidden\" name=\"outer_trade_no\" value=\"" + outer_trade_no + "\">");
        Response.Write("<input type=\"hidden\" name=\"request_no\" value=\"\">");
        Response.Write("<input type=\"hidden\" name=\"b2b_token\" value=\"" + tools.NullStr(Session["member_token"]) + "\">");
        Response.Write("</form>");
        Response.Write("</body>");
        Response.Write("</html>");
    }


    /// <summary>
    /// 交易状态变更通知
    /// </summary>
    public void VFINANCE_Trade_Status_Sync()
    {
        int i = 0;
        NameValueCollection coll = default(NameValueCollection);
        coll = Request.Form;

        String[] requestarr = coll.AllKeys;
        string _input_charset = "utf-8";

        //进行排序； 
        string[] Sortedstr = BubbleSort(requestarr);

        //构造待md5摘要字符串 ； 
        string prestr = "";
        string param = "";
        for (i = 0; i <= Sortedstr.Length - 1; i++)
        {
            if (Request.Form[Sortedstr[i]] != "" && Sortedstr[i] != "sign" && Sortedstr[i] != "sign_type" && Sortedstr[i] != "payment")
            {
                if (i == Sortedstr.Length - 1)
                {
                    prestr = (prestr + Sortedstr[i] + "=") + Request.Form[Sortedstr[i]];
                }
                else
                {
                    prestr = (prestr + Sortedstr[i] + "=") + Request.Form[Sortedstr[i]] + "&";
                }
            }
        }

        prestr = prestr + tradesignkey;
        string mysign = GetMD5(prestr.ToString(), _input_charset);

        string sign = Request.Form["sign"];
        string Pay_Note = "";
        string v_oid = tools.NullStr(Request.Form["outer_trade_no"]);
        string strPrice = tools.NullStr(Request.Form["trade_amount"]);
        string strTradeStatus = tools.NullStr(Request.Form["trade_status"]);
        string inner_trade_no = tools.NullStr(Request.Form["inner_trade_no"]);
        string issuccess = tools.NullStr(Request.Form["is_success"]);

        string Pay_Detail = null;
        Pay_Detail += "钱包交易号:" + inner_trade_no + "<BR>";
        Pay_Detail += "订单编号:" + v_oid + "<BR>";
        Pay_Detail += "交易金额:" + strPrice + "<BR>";
        Pay_Detail += "交易日期:" + DateTime.Now + "<BR>";
        Pay_Detail += "交易状态:" + strTradeStatus + "<BR>";
        //Pay_Detail += "返回签名:" + sign + "<BR>";
        //Pay_Detail += "签名:" + mysign + "<BR>";
        //Pay_Detail += "接收参数:" + prestr + "<BR>";

        //验证支付发过来的消息，签名是否正确 
        if (mysign == sign)
        {
            //验证交易状态
            if (Request.Form["trade_status"] == "TRADE_FINISHED" || Request.Form["trade_status"] == "TRADE_SUCCESS" || Request.Form["trade_status"] == "PAY_FINISHED")
            {
                //订单处理
                int process_result = Pay_Orders_Process(v_oid, "智联快付", tools.CheckFloat(strPrice), Pay_Detail);
                if (process_result == 1)
                {
                    Response.Write("success");
                }
                else
                {
                    Response.Write("error");
                }
            }
            else
            {
                Pay_Note = "[失败]支付状态错误";
                Pay_Log(v_oid, 0, "智联快付", tools.CheckFloat(strPrice), Pay_Note, Pay_Detail);
                Response.Write("error");
            }
        }
        else
        {
            Pay_Note = "[失败]支付失败,验证错误或签名密不一致";
            Pay_Log(v_oid, 0, "智联快付", tools.CheckFloat(strPrice), Pay_Note, Pay_Detail);
            Response.Write("error");
        }
    }

    /// <summary>
    /// 退款状态变更通知
    /// </summary>
    public void VFINANCE_Refund_Status_Sync()
    {
        int i = 0;
        NameValueCollection coll = default(NameValueCollection);
        coll = Request.Form;

        String[] requestarr = coll.AllKeys;
        string _input_charset = "utf-8";

        //进行排序； 
        string[] Sortedstr = BubbleSort(requestarr);

        //构造待md5摘要字符串 ； 
        string prestr = "";
        for (i = 0; i <= Sortedstr.Length - 1; i++)
        {
            if (Request.Form[Sortedstr[i]] != "" && Sortedstr[i] != "sign" && Sortedstr[i] != "sign_type" && Sortedstr[i] != "notify_type")
            {
                if (i == Sortedstr.Length - 1)
                {
                    prestr = (prestr + Sortedstr[i] + "=") + Request.Form[Sortedstr[i]];
                }
                else
                {
                    prestr = (prestr + Sortedstr[i] + "=") + Request.Form[Sortedstr[i]] + "&";
                }
            }
        }

        prestr = prestr + tradesignkey;
        string mysign = GetMD5(prestr.ToString(), _input_charset);

        string sign = Request.Form["sign"];
        string Pay_Note = "";
        string v_oid = tools.NullStr(Request.Form["orig_outer_trade_no"]);
        string strPrice = tools.NullStr(Request.Form["refund_amount"]);
        string strTradeStatus = tools.NullStr(Request.Form["refund_status"]);
        string inner_trade_no = tools.NullStr(Request.Form["inner_trade_no"]);
        DateTime gmt_refund = tools.NullDate(Request.Form["gmt_refund"]);
        string issuccess = tools.NullStr(Request.Form["is_success"]);

        string Pay_Detail = null;
        Pay_Detail = "";
        Pay_Detail += "钱包退款交易号:" + inner_trade_no + "<BR>";
        Pay_Detail += "订单编号:" + v_oid + "<BR>";
        Pay_Detail += "退款金额:" + strPrice + "<BR>";
        Pay_Detail += "退款日期:" + DateTime.Now + "<BR>";
        Pay_Detail += "退款状态:" + strTradeStatus + "<BR>";

        //验证支付发过来的消息，签名是否正确 
        if (mysign == sign)
        {
            //验证交易状态
            if (Request.Form["refund_status"] == "REFUND_SUCCESS")
            {
                //订单处理
                int process_result = Pay_Orders_Process(v_oid, "智联快付", tools.CheckFloat(strPrice), Pay_Detail);
                if (process_result == 1)
                {
                    Response.Write("success");
                }
                else
                {
                    Response.Write("error");
                }
            }
            else
            {
                Pay_Note = "[失败]退款失败";
                Pay_Log(v_oid, 0, "智联快付", tools.CheckFloat(strPrice), Pay_Note, Pay_Detail);
                Response.Write("error");
            }
        }
        else
        {
            Pay_Note = "[失败]签名密不一致";
            Pay_Log(v_oid, 0, "智联快付", tools.CheckFloat(strPrice), Pay_Note, Pay_Detail);
            Response.Write("error");
        }
    }

    /// <summary>
    /// 转账状态变更通知
    /// </summary>
    public void VFINANCE_Transfer_Status_Sync()
    {
        int i = 0;
        NameValueCollection coll = default(NameValueCollection);
        coll = Request.Form;

        String[] requestarr = coll.AllKeys;
        string _input_charset = "utf-8";

        //进行排序； 
        string[] Sortedstr = BubbleSort(requestarr);

        //构造待md5摘要字符串 ； 
        string prestr = "";
        for (i = 0; i <= Sortedstr.Length - 1; i++)
        {
            if (Request.Form[Sortedstr[i]] != "" && Sortedstr[i] != "sign" && Sortedstr[i] != "sign_type" && Sortedstr[i] != "notify_type")
            {
                if (i == Sortedstr.Length - 1)
                {
                    prestr = (prestr + Sortedstr[i] + "=") + Request.Form[Sortedstr[i]];
                }
                else
                {
                    prestr = (prestr + Sortedstr[i] + "=") + Request.Form[Sortedstr[i]] + "&";
                }
            }
        }

        prestr = prestr + tradesignkey;
        string mysign = GetMD5(prestr.ToString(), _input_charset);

        string sign = Request.Form["sign"];
        string Pay_Note = "";
        string v_oid = tools.NullStr(Request.Form["orig_outer_trade_no"]);
        string strPrice = tools.NullStr(Request.Form["transfer_amount"]);
        string strTradeStatus = tools.NullStr(Request.Form["transfer_status"]);
        string inner_trade_no = tools.NullStr(Request.Form["inner_trade_no"]);
        DateTime gmt_transfer = tools.NullDate(Request.Form["gmt_transfer"]);
        string issuccess = tools.NullStr(Request.Form["is_success"]);

        string Pay_Detail = null;
        Pay_Detail = "";
        Pay_Detail += "钱包转账交易号:" + inner_trade_no + "<BR>";
        Pay_Detail += "订单编号:" + v_oid + "<BR>";
        Pay_Detail += "转账金额:" + strPrice + "<BR>";
        Pay_Detail += "转账日期:" + gmt_transfer + "<BR>";
        Pay_Detail += "转账状态:" + strTradeStatus + "<BR>";

        //验证支付发过来的消息，签名是否正确 
        if (mysign == sign)
        {
            //验证交易状态
            if (Request.Form["transfer_status"] == "TRANSFER_SUCCESS")
            {
                //订单处理
                int process_result = Pay_Orders_Process(v_oid, "智联快付", tools.CheckFloat(strPrice), Pay_Detail);
                if (process_result == 1)
                {
                    Response.Write("success");
                }
                else
                {
                    Response.Write("error");
                }
            }
            else
            {
                Pay_Note = "[失败]转账失败";
                Pay_Log(v_oid, 0, "智联快付", tools.CheckFloat(strPrice), Pay_Note, Pay_Detail);
                Response.Write("error");
            }
        }
        else
        {
            Pay_Note = "[失败]签名密不一致";
            Pay_Log(v_oid, 0, "智联快付", tools.CheckFloat(strPrice), Pay_Note, Pay_Detail);
            Response.Write("error");
        }
    }

    /// <summary>
    /// 创建交易并返回收银台地址
    /// </summary>
    /// <returns></returns>
    public void GetTradeJsonInfo()
    {
        TradeJsonInfo tradeJsonInfo = null;
        string member_id = "M" + tools.NullStr(Session["member_id"]);
        string Orders_SN = tools.CheckStr(Request["orders_sn"]);

        OrdersInfo entity = MyOrders.GetOrdersBySN(Orders_SN);

        if (entity != null)
        {
            string request_no = DateTime.Now.ToString("yyyyMMddHHmmss") + pub.Createvkey(10);
            string notify_url = Application["Site_URL"] + "/pay/payment_notify.aspx";
            string return_url = Application["Site_URL"] + "/member/order_view.aspx?orders_sn=" + entity.Orders_SN;
            string trade_list = BindingEnsureTradeList(entity.Orders_ID, notify_url);
            string buyer_id = "M" + tools.NullStr(Session["member_id"]);
            string buyer_id_type = "UID";
            string buyer_mobile = tools.NullStr(Session["member_mobile"]);

            tradeJsonInfo = Create_Ensure_Trade_Get(request_no, trade_list, buyer_id, buyer_id_type, buyer_mobile, return_url);

            if (tradeJsonInfo != null && tradeJsonInfo.Is_success == "T")
            {
                Session["Cashier_url"] = tradeJsonInfo.Cashier_url;
            }
            else
            {
                Session["Cashier_url"] = "";
            }
        }
        else
        {
            Session["Cashier_url"] = "";
        }
    }

    public void Loan_Apply()
    {
        string member_id = "M" + tools.NullStr(Session["member_id"]);
        string Orders_SN = tools.CheckStr(Request["orders_sn"]);

        LoanApplyJsonInfo JsonInfo = new LoanApplyJsonInfo();

        OrdersInfo entity = MyOrders.GetOrdersBySN(Orders_SN);

        if (entity != null)
        {
            string request_no = DateTime.Now.ToString("yyyyMMddHHmmss") + pub.Createvkey(10);
            string notify_url = Application["Site_URL"] + "/pay/payment_notify.aspx";
            string return_url = Application["Site_URL"] + "/member/order_view.aspx?orders_sn=" + entity.Orders_SN;
            string trade_list = BindingEnsureTradeList(entity.Orders_ID, notify_url);
            string buyer_id = "M" + tools.NullStr(Session["member_id"]);
            string buyer_id_type = "UID";
            string buyer_mobile = tools.NullStr(Session["member_mobile"]);

            TradeJsonInfo tradeJsonInfo = Create_Ensure_Trade_Get(request_no, trade_list, buyer_id, buyer_id_type, buyer_mobile, return_url);

            if (tradeJsonInfo != null && tradeJsonInfo.Is_success == "T")
            {
                JsonInfo = credit.Loan_Apply(member_id, entity);

                if (JsonInfo != null && JsonInfo.Is_success == "T")
                {
                    OrdersLoanApplyInfo loanApplyInfo = new OrdersLoanApplyInfo();
                    loanApplyInfo.ID = 0;
                    loanApplyInfo.MemberID = tools.NullInt(Session["member_id"]);
                    loanApplyInfo.Orders_SN = Orders_SN;
                    loanApplyInfo.Loan_proj_No = JsonInfo.Loan_proj_no;
                    loanApplyInfo.Loan_Amount = JsonInfo.Loan_amount;
                    loanApplyInfo.Interest_Rate = JsonInfo.Interest_rate;
                    loanApplyInfo.Interest_Rate_Unit = JsonInfo.Interest_rate_unit;
                    loanApplyInfo.Trem = JsonInfo.Term;
                    loanApplyInfo.Trem_Unit = JsonInfo.Term_unit;
                    loanApplyInfo.Fee_Amount = JsonInfo.Fee_amount;
                    loanApplyInfo.Repay_Method = JsonInfo.Repay_method;
                    loanApplyInfo.Margin_Amount = JsonInfo.Margin_amount;
                    MyLoanApply.AddOrdersLoanApply(loanApplyInfo);

                    pub.Msg("positive", "提示信息", "贷款申请成功", true, "/member/loan_project.aspx");
                }
                else
                {
                    pub.Msg("info", "提示信息", "操作失败，请稍后再试！", false, "{back}");
                }
            }
            else
            {
                pub.Msg("info", "提示信息", "" + tradeJsonInfo.Error_message + "", false, "{back}");
            }
        }
        else
        {
            pub.Msg("info", "提示信息", "操作失败，请稍后再试！", false, "{back}");
        }
    }


    #endregion
    
    #region "业务处理函数"

    //支付完成后订单处理
    public int Pay_Orders_Process(string Orders_SN, string Payment_Name, double Payment_Price, string Pay_Detail)
    {
        SupplierInfo supplierInfo = null;
        string supplierName = "", supplierMobile = "";
        //0为失败，1为成功
        int Process_Result = 0;
        int pay_status = 0;
        string OrdersLogNote = "";
        string Pay_Note = "";
        int Orders_ID = Pay_CheckAmountNoPay(Orders_SN, Payment_Price * 100);
        if (Orders_ID > 0)
        {
            OrdersInfo entity = MyOrders.GetOrdersByID(Orders_ID);
            if (entity != null)
            {
                pay_status = entity.Orders_PaymentStatus;
                if (pay_status == 0)
                {
                    entity.Orders_PaymentStatus = 1;
                    entity.Orders_PaymentStatus_Time = DateTime.Now;
                    MyOrders.EditOrders(entity);
                }

                supplierInfo = supplier.GetSupplierByID(entity.Orders_SupplierID);
                if (supplierInfo != null)
                {
                    supplierName = supplierInfo.Supplier_CompanyName;
                    supplierMobile = supplierInfo.Supplier_Mobile;
                }
            }
            else
            {
                pay_status = -1;
            }
            entity = null;

            //订单未支付
            if (pay_status == 0)
            {
                //添加付款单
                string Orders_Payment_DocNo = Pay_NewPaymentDocNo();
                int Orders_Payment_ID = Pay_AddPayment(Orders_ID, 1, 0, Orders_Payment_DocNo, Payment_Name, Payment_Price, "通过" + Payment_Name + "支付成功");

                OrdersLogNote = "订单付款，付款金额{order_payment_totalprice}";
                //OrdersLogNote = OrdersLogNote.Replace("{order_payment_sn}", "<a href=\"/orderspayment/orders_payment_view.aspx?orders_payment_id=" + Orders_Payment_ID + "\">" + Orders_Payment_DocNo + "</a>");
                OrdersLogNote = OrdersLogNote.Replace("{order_payment_totalprice}", pub.FormatCurrency(Payment_Price));

                //订单日志添加
                Orders_Log(Orders_ID, "", "付款", "成功", OrdersLogNote);

                //发送短信
                //string[] content = { supplierName, Orders_SN };
                //new SMS().Send(tools.NullStr(supplierMobile), "member_pay_orders_remind", content);

                //记录支付日志
                Pay_Note = "[成功]通过" + Payment_Name + "支付成功";
                Pay_Log(Orders_SN, 1, Payment_Name, Payment_Price, Pay_Note, Pay_Detail);
            }
            Process_Result = 1;
        }
        else
        {
            //记录支付日志
            Pay_Note = "[失败]通过" + Payment_Name + "支付成功，但找不到该订单";
            Pay_Log(Orders_SN, 0, Payment_Name, Payment_Price, Pay_Note, Pay_Detail);
            Process_Result = 0;
        }
        return Process_Result;
    }

    //支付请求
    public void Pay_Request(string pay_payment, string pay_type, int orders_id, string orders_sn)
    {
        double v_amount = 0;
        double alipay_price = 0;
        string Orders_Addtime = "";
        string Orders_Delivery_Name = "";
        double total_freight = 0;
        OrdersInfo orderinfo = null;

        //pay_payment 支付方式名称
        //各支付方式Sign，如99bill,alipay等，在Sys_Pay中定义
        pay_payment = pay_payment.ToUpper();
        string sql_pay = null;
        double Orders_Total_AllPrice = 0;
        switch (pay_type)
        {
            //为订单支付
            case "order_pay":
                orderinfo = MyOrders.GetOrdersByID(orders_id);
                if (orderinfo != null)
                {
                    if (orderinfo.Orders_BuyerID == tools.CheckInt(Session["member_id"].ToString()))
                    {
                        if (orderinfo.Orders_Status < 2 && orderinfo.Orders_PaymentStatus == 0)
                        {
                            alipay_price = tools.CheckFloat(orderinfo.Orders_Total_AllPrice.ToString("0.00"));
                            Orders_Total_AllPrice = tools.CheckFloat((orderinfo.Orders_Total_AllPrice - orderinfo.Orders_Account_Pay).ToString("0.00"));
                            Orders_Addtime = orderinfo.Orders_Addtime.ToString();
                            Orders_Delivery_Name = orderinfo.Orders_Delivery_Name;
                            v_amount = Orders_Total_AllPrice;
                            total_freight = orderinfo.Orders_Total_Freight - orderinfo.Orders_Total_FreightDiscount;
                        }
                        else
                        {
                            orders_id = 0;
                        }
                    }
                    else
                    {
                        orders_id = 0;
                    }
                }
                else
                {
                    orders_id = 0;
                }
                orderinfo = null;
                if (orders_id == 0)
                {
                    Response.Redirect("/member/order_all.aspx");
                    Response.End();
                }
                break;
        }
        //CHINABANK 需要的数据
        string v_mid = "";
        string v_key = "";
        string v_url = "";
        v_mid = tools.NullStr(Application["Chinabank_Code"]);

        v_key = tools.NullStr(Application["Chinabank_Key"]);
        v_url = Application["Site_URL"] + "/pay/pay_result.aspx?payment=" + pay_payment;

        //发出支付请求
        if (v_amount > 0)
        {
            switch (pay_type)
            {
                //为订单支付
                case "order_pay":
                    switch (pay_payment)
                    {
                        case "V_INSTANT":
                            string request_no = DateTime.Now.ToString("yyyyMMddHHmmss") + pub.Createvkey(10);
                            string notify_url = Application["Site_URL"] + "/pay/payment_notify.aspx";
                            string return_url = Application["Site_URL"] + "/member/order_view.aspx?orders_sn=" + orders_sn;
                            Session["request_no"] = request_no;
                            string trade_list = BindingEnsureTradeList(orders_id, notify_url);
                            string buyer_id = "M" + tools.NullStr(Session["member_id"]);
                            string buyer_id_type = "UID";
                            string buyer_mobile = tools.NullStr(Session["member_mobile"]);
                            Create_Ensure_Trade(request_no, trade_list, buyer_id, buyer_id_type, buyer_mobile, return_url);
                            break;
                    }
                    break;
            }
        }

    }

    //支付结果
    public void Pay_Result(string result, string orders_sn)
    {
        if (result == "success")
        {
            Response.Write("<table width=\"980\" border=\"0\" align=\"center\" cellpadding=\"6\" cellspacing=\"0\" class=\"tip_bg_positive\">");
            Response.Write("  <tr>");
            Response.Write("    <td><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\">");
            Response.Write("      <tr>");
            Response.Write("        <td width=\"100\" align=\"center\" valign=\"middle\"><img src=\"/images/info_success_48.gif\"></td>");
            Response.Write("       <td align=\"left\" valign=\"top\">");
            Response.Write("       <h2 class=\"t14_green\">支付成功</h2>");
            Response.Write("       您已经成功支付！点这里");
            if (orders_sn == "")
            {
                Response.Write("       <input name=\"btnview\" type=\"button\" class=\"buttonupload\" style=\" cursor:pointer;\" id=\"btnview\" value=\"查看订单\" onclick=\"location.href='/member/order_all.aspx';\"/>");
            }
            else
            {
                Response.Write("       <input name=\"btnview\" type=\"button\" class=\"buttonupload\" style=\" cursor:pointer;\" id=\"btnview\" value=\"查看订单\" onclick=\"location.href='/member/order_view.aspx?orders_sn=" + orders_sn + "';\"/>");
            }
            Response.Write("       。如果您对本次支付存在疑问，请咨询客服。</td>");
            Response.Write("        </tr>");
            Response.Write("    </table></td>");
            Response.Write("  </tr>");
            Response.Write("</table>");

        }
        else
        {
            Response.Write("<table width=\"980\" border=\"0\" align=\"center\" cellpadding=\"6\" cellspacing=\"0\" class=\"tip_bg_error\">");
            Response.Write("  <tr>");
            Response.Write("    <td><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\">");
            Response.Write("      <tr>");
            Response.Write("        <td width=\"100\" align=\"center\" valign=\"middle\"><img src=\"/Images/info_error_48.gif\" /></td>");
            Response.Write("        <td align=\"left\" valign=\"top\"><h2 class=\"t14_red\">支付失败</h2>");
            Response.Write("          您本次支付失败！您可以选择");
            if (orders_sn == "")
            {
                Response.Write("          <input name=\"btnview\" type=\"button\" class=\"buttonupload\" id=\"btnview\" style=\" cursor:pointer;\" value=\"重新支付\" onclick=\"location.href='/member/order_unprocessed.aspx';\"/>");
            }
            else
            {
                Response.Write("          <input name=\"btnview\" type=\"button\" class=\"buttonupload\" id=\"btnview\" style=\" cursor:pointer;\" value=\"重新支付\" onclick=\"location.href='/pay/pay_index.aspx?sn=" + orders_sn + "';\"/>");
            }
            Response.Write("          。如果您对本次支付存在疑问，请咨询客服。</td>");
            Response.Write("      </tr>");
            Response.Write("    </table></td>");
            Response.Write("  </tr>");
            Response.Write("</table>");

        }

    }

    //支付日志
    public void Pay_Log(string OrderSN, int IsSuccess, string Payway_Sign, double Amount, string Note, string Detail)
    {
        MemberPayLogInfo paylog = new MemberPayLogInfo();
        paylog.Member_Pay_Log_OrderSN = OrderSN;
        paylog.Member_Pay_Log_IsSuccess = IsSuccess;
        paylog.Member_Pay_Log_PaywaySign = Payway_Sign;
        paylog.Member_Pay_Log_Amount = Amount;
        paylog.Member_Pay_Log_Note = Note;
        paylog.Member_Pay_Log_Detail = Detail;
        paylog.Member_Pay_Log_Addtime = DateTime.Now;

        MyPayment.AddMemberPayLog(paylog);
        paylog = null;
    }

    //检查支付金额与订单金额是否相等
    private int Pay_CheckAmount(string Orders_SN, double Amount)
    {
        string Orders_CurrencyCode = "";
        double Orders_Total_AllPrice = 0;
        int ReturnValue = -1;

        OrdersInfo orderinfo = MyOrders.GetOrdersBySN(Orders_SN);
        if (orderinfo != null)
        {

            if (orderinfo.Orders_Status < 2 && orderinfo.Orders_PaymentStatus == 0)
            {
                Orders_Total_AllPrice = tools.CheckFloat(orderinfo.Orders_Total_AllPrice.ToString("0.00"));
                if ((Orders_Total_AllPrice * 100 == Amount))
                {
                    ReturnValue = orderinfo.Orders_ID;
                }
                else
                {
                    ReturnValue = 0;
                }
            }
        }


        return ReturnValue;
    }

    /// <summary>
    /// 检查支付金额与订单金额是否相等
    /// </summary>
    /// <param name="Orders_SN"></param>
    /// <param name="Amount"></param>
    /// <returns></returns>
    private int Pay_CheckAmountNoPay(string Orders_SN, double Amount)
    {
        string Orders_CurrencyCode = "";
        double Orders_Total_AllPrice = 0;
        int ReturnValue = -1;

        OrdersInfo orderinfo = MyOrders.GetOrdersBySN(Orders_SN);
        if (orderinfo != null)
        {

            if (orderinfo.Orders_Status < 2)
            {
                Orders_Total_AllPrice = tools.CheckFloat((orderinfo.Orders_Total_AllPrice - orderinfo.Orders_Account_Pay).ToString("0.00"));
                if ((Orders_Total_AllPrice * 100 == Amount))
                {
                    ReturnValue = orderinfo.Orders_ID;
                }
                else
                {
                    ReturnValue = 0;
                }
            }
        }


        return ReturnValue;
    }

    //生成付款单号
    public string Pay_NewPaymentDocNo()
    {
        string sn = "";
        int recordcount = 0;
        string count = "";
        bool ismatch = true;
        OrdersPaymentInfo paymentinfo = null;
        sn = tools.FormatDate(DateTime.Now, "yyMMdd") + pub.Createvkey(5);
        while (ismatch == true)
        {
            paymentinfo = MyPayment.GetOrdersPaymentBySn(sn);
            if (paymentinfo != null)
            {
                sn = tools.FormatDate(DateTime.Now, "yyMMdd") + pub.Createvkey(5);
            }
            else
            {
                ismatch = false;
            }
        }
        return sn;
    }

    //添加付款单
    public int Pay_AddPayment(int OrdersID, int PaymentStatus, int SysUserID, string DocNo, string Name, double Amount, string Note)
    {
        int payment_id = 0;
        OrdersPaymentInfo orderspayment = new OrdersPaymentInfo();
        orderspayment.Orders_Payment_ID = 0;
        orderspayment.Orders_Payment_OrdersID = OrdersID;
        orderspayment.Orders_Payment_PaymentStatus = PaymentStatus;
        orderspayment.Orders_Payment_SysUserID = SysUserID;
        orderspayment.Orders_Payment_DocNo = DocNo;
        orderspayment.Orders_Payment_Name = Name;
        orderspayment.Orders_Payment_Amount = Amount;
        orderspayment.Orders_Payment_Note = Note;
        orderspayment.Orders_Payment_Addtime = DateTime.Now;
        orderspayment.Orders_Payment_Site = "CN";

        if (MyPayment.AddOrdersPayment(orderspayment))
        {
            OrdersPaymentInfo orderpayment = MyPayment.GetOrdersPaymentBySn(DocNo);
            if (orderpayment != null)
            {
                payment_id = orderpayment.Orders_Payment_ID;
            }
            orderpayment = null;
        }
        orderspayment = null;
        return payment_id;
    }

    //订单日志
    public void Orders_Log(int orders_id, string log_operator, string log_action, string log_result, string log_remark)
    {
        OrdersLogInfo orderslog = new OrdersLogInfo();
        orderslog.Orders_Log_ID = 0;
        orderslog.Orders_Log_Operator = log_operator;
        orderslog.Orders_Log_OrdersID = orders_id;
        orderslog.Orders_Log_Action = log_action;
        orderslog.Orders_Log_Remark = log_remark;
        orderslog.Orders_Log_Result = log_result;
        orderslog.Orders_Log_Addtime = DateTime.Now;
        Mylog.AddOrdersLog(orderslog);
    }

    #endregion





}