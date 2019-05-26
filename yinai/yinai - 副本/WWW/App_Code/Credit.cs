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
using Glaer.Trade.Util.Http;
using System.Net;
using System.Collections;

/// <summary>
/// Credit 的摘要说明
/// </summary>
public class Credit
{

    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    ITools tools;
    IHttpHelper HttpHelper;
    IJsonHelper JsonHelper;
    IOrdersLoanApply MyLoanApply;
    string loan_url;
    Public_Class pub;

    string partner_id;
    string tradesignkey;

	public Credit()
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

        pub = new Public_Class();

        tradesignkey = System.Web.Configuration.WebConfigurationManager.AppSettings["tradesignkey"].ToString();
        partner_id = tools.NullStr(Application["CreditPayment_Code"]);
        loan_url = System.Web.Configuration.WebConfigurationManager.AppSettings["loan-lgs"].ToString();
	}

    

    #region ---用户信贷信息查询---

    /// <summary>
    /// 用户信贷信息查询
    /// </summary>
    /// <param name="Uid"></param>
    /// <returns></returns>
    public QueryMemberLoanJsonInfo Query_Member_Loan(string Uid)
    {
        QueryMemberLoanJsonInfo JsonInfo = null;

        string sign, sign_type;

        string gateway = loan_url + "?_input_charset=utf-8";

        string[] parameters = 
        {
            "service=query_member_loan",
            "version=1.0",
            "partner_id="+partner_id,
            "_input_charset=utf-8",
            "uid="+Uid,
            "need_margin=true"
        };

        sign_type = "MD5";
        sign = pub.ReturnSignStr(parameters, "utf-8", tradesignkey);

        StringBuilder prestr = new StringBuilder();
        prestr.Append("&service=query_member_loan");
        prestr.Append("&version=1.0");
        prestr.Append("&partner_id=" + partner_id);
        prestr.Append("&sign=" + sign);
        prestr.Append("&sign_type=" + sign_type);
        prestr.Append("&uid=" + Uid);
        prestr.Append("&need_margin=true");

        string request_url = gateway + prestr.ToString();

        CookieCollection cookies = new CookieCollection();

        string strJson = HttpHelper.GetResponseString(HttpHelper.CreateGetHttpResponse(request_url, 0, "", cookies));

        JsonInfo = JsonHelper.JSONToObject<QueryMemberLoanJsonInfo>(strJson);

        if (JsonInfo != null && JsonInfo.Is_success == "T")
        {
            pub.AddSysInterfaceLog(2, "用户信贷信息查询", "成功", request_url, "授信总额：" + JsonInfo.Credit_limit + ";当前申请额度：" + JsonInfo.Apply_credit + ";已借贷款额度：" + JsonInfo.Used_credit + ";可用额度：" + JsonInfo.Available_credit);
        }
        else
        {
            pub.AddSysInterfaceLog(2, "用户信贷信息查询", "失败", request_url, JsonInfo.Error_code + ":" + JsonInfo.Error_message);
        }

        return JsonInfo;
    }

    #endregion 

    #region ---信贷产品信息查询---

    public QueryLoanProductJsonInfo Query_Loan_Product(string Uid)
    {
        QueryLoanProductJsonInfo JsonInfo = null;

        try
        {
            string sign, sign_type;

            string gateway = loan_url + "?_input_charset=utf-8";

            string[] parameters = 
            {
                "service=query_loan_product",
                "version=1.0",
                "partner_id="+partner_id,
                "_input_charset=utf-8",
                "uid="+Uid
            };

            sign_type = "MD5";
            sign = pub.ReturnSignStr(parameters, "utf-8", tradesignkey);

            StringBuilder prestr = new StringBuilder();
            prestr.Append("&service=query_loan_product");
            prestr.Append("&version=1.0");
            prestr.Append("&partner_id=" + partner_id);
            prestr.Append("&sign=" + sign);
            prestr.Append("&sign_type=" + sign_type);
            prestr.Append("&uid=" + Uid);

            string request_url = gateway + prestr.ToString();

            CookieCollection cookies = new CookieCollection();

            string strJson = HttpHelper.GetResponseString(HttpHelper.CreateGetHttpResponse(request_url, 0, "", cookies));

            JsonInfo = JsonHelper.JSONToObject<QueryLoanProductJsonInfo>(strJson);

            if (JsonInfo != null && JsonInfo.Is_success == "T")
            {
                pub.AddSysInterfaceLog(2, "信贷产品信息查询", "成功", request_url, "信贷产品信息查询成功");
            }
            else
            {
                pub.AddSysInterfaceLog(2, "信贷产品信息查询", "失败", request_url, JsonInfo.Error_code + ":" + JsonInfo.Error_message);
            }
        }
        catch (Exception ex)
        {
            JsonInfo = null;
        }

        return JsonInfo;
    }

    #endregion

    #region ---信贷申请---

    /// <summary>
    /// 信贷申请
    /// </summary>
    /// <param name="UID">用户ID</param>
    /// <param name="ordersInfo">订单信息</param>
    /// <param name="agreement_no">信贷产品信息协议号</param>
    /// <param name="loan_term_id">利率期限ID</param>
    /// <param name="repay_method_id">计息方式ID</param>
    /// <returns></returns>
    public LoanApplyJsonInfo Loan_Apply(string UID, OrdersInfo ordersInfo)
    {
        LoanApplyJsonInfo JsonInfo = null;

        string sign, sign_type;
        double loan_amount = 0;

        if (ordersInfo.Orders_ApplyCreditAmount > 0)
        {
            loan_amount = ordersInfo.Orders_ApplyCreditAmount;
        }
        else
        {
            loan_amount = ordersInfo.Orders_Total_AllPrice;
        }

        string gateway = loan_url + "?_input_charset=utf-8";

        string[] parameters = 
        {
            "service=loan_apply",
            "version=1.0",
            "partner_id="+partner_id,
            "_input_charset=utf-8",
            "uid="+UID,
            "outer_order_no="+ordersInfo.Orders_SN,
            "order_amount="+ordersInfo.Orders_Total_AllPrice,
            "ori_amount="+ordersInfo.Orders_Total_Price,
            "loan_amount="+loan_amount,
            "agreement_no="+ordersInfo.Orders_AgreementNo,
            "loan_term_id="+ordersInfo.Orders_LoanTermID,
            "repay_method_id="+ordersInfo.Orders_LoanMethodID
        };

        sign_type = "MD5";
        sign = pub.ReturnSignStr(parameters, "utf-8", tradesignkey);

        StringBuilder prestr = new StringBuilder();

        prestr.Append("&service=loan_apply");
        prestr.Append("&version=1.0");
        prestr.Append("&partner_id=" + partner_id);
        prestr.Append("&sign=" + sign);
        prestr.Append("&sign_type=" + sign_type);
        prestr.Append("&uid=" + UID);
        prestr.Append("&outer_order_no=" + ordersInfo.Orders_SN);
        prestr.Append("&order_amount=" + ordersInfo.Orders_Total_AllPrice);
        prestr.Append("&ori_amount=" + ordersInfo.Orders_Total_Price);
        prestr.Append("&loan_amount=" + loan_amount);
        prestr.Append("&agreement_no=" + ordersInfo.Orders_AgreementNo);
        prestr.Append("&loan_term_id=" + ordersInfo.Orders_LoanTermID);
        prestr.Append("&repay_method_id=" + ordersInfo.Orders_LoanMethodID);

        string request_url = gateway + prestr.ToString();

        CookieCollection cookies = new CookieCollection();

        string strJson = HttpHelper.GetResponseString(HttpHelper.CreateGetHttpResponse(request_url, 0, "", cookies));

        JsonInfo = JsonHelper.JSONToObject<LoanApplyJsonInfo>(strJson);

        if (JsonInfo != null && JsonInfo.Is_success == "T")
        {
            pub.AddSysInterfaceLog(2, "信贷申请", "成功", request_url, "信贷申请成功");
        }
        else
        {
            pub.AddSysInterfaceLog(2, "信贷申请", "失败", request_url, JsonInfo.Error_code + ":" + JsonInfo.Error_message);
        }

        return JsonInfo;
    }

    #endregion

    #region ---贷款推进---

    public LoanPushJsonInfo Loan_Push(string UID,string Orders_SN)
    {
        LoanPushJsonInfo JsonInfo = null;

        string sign, sign_type;

        string gateway = loan_url + "?_input_charset=utf-8";

        string[] parameters = 
        {
            "service=loan_push",
            "version=1.0",
            "partner_id=" + partner_id,
            "_input_charset=utf-8",
            "uid="+UID,
            "outer_order_no=" + Orders_SN
        };

        sign_type = "MD5";
        sign = pub.ReturnSignStr(parameters, "utf-8", tradesignkey);

        StringBuilder prestr = new StringBuilder();

        prestr.Append("&service=loan_push");
        prestr.Append("&version=1.0");
        prestr.Append("&partner_id=" + partner_id);
        prestr.Append("&sign=" + sign);
        prestr.Append("&sign_type=" + sign_type);
        prestr.Append("&uid=" + UID);
        prestr.Append("&outer_order_no=" + Orders_SN);

        string request_url = gateway + prestr.ToString();

        CookieCollection cookies = new CookieCollection();

        string strJson = HttpHelper.GetResponseString(HttpHelper.CreateGetHttpResponse(request_url, 0, "", cookies));

        JsonInfo = JsonHelper.JSONToObject<LoanPushJsonInfo>(strJson);

        if (JsonInfo != null && JsonInfo.Is_success == "T")
        {
            pub.AddSysInterfaceLog(2, "贷款推进", "成功", prestr.ToString(), "贷款编号：" + JsonInfo.Loan_proj_no + ";推进链接：" + JsonInfo.Push_url);
        }
        else
        {
            pub.AddSysInterfaceLog(2, "贷款推进", "失败", prestr.ToString(), JsonInfo.Error_code + ":" + JsonInfo.Error_message);
        }

        return JsonInfo;
    }

    #endregion

    #region ---信贷信息查询---

    public QueryLoanProjectJsonInfo QueryLoanProject(string UID, string loan_proj_no, string loan_status, int current_page, int page_size)
    {
        QueryLoanProjectJsonInfo JsonInfo = null;

        string sign, sign_type;

        string gateway = loan_url + "?_input_charset=utf-8";

        #region  拼接参数并转换成数组
        List<string> liststr = new List<string>();
        liststr.Add("service=query_loan_project");
        liststr.Add("version=1.0");
        liststr.Add("partner_id="+partner_id);
        liststr.Add("_input_charset=utf-8");
        liststr.Add("uid="+UID);
        if (loan_proj_no != "")
        {
            liststr.Add("loan_proj_no=" + loan_proj_no);
        }

        if (loan_status != "" && loan_status != "NORMAL")
        {
            liststr.Add("loan_status=" + loan_status);
        }

        if (current_page > 0)
        {
            liststr.Add("current_page=" + current_page);
        }

        if (page_size > 0)
        {
            liststr.Add("page_size=" + page_size);
        }
        #endregion

        string[] parameters = liststr.ToArray();

        sign_type = "MD5";
        sign = pub.ReturnSignStr(parameters, "utf-8", tradesignkey);

        #region 拼接传递参数
        StringBuilder prestr = new StringBuilder();

        prestr.Append("&service=query_loan_project");
        prestr.Append("&version=1.0");
        prestr.Append("&partner_id=" + partner_id);
        prestr.Append("&sign=" + sign);
        prestr.Append("&sign_type=" + sign_type);
        prestr.Append("&uid=" + UID);
        if (loan_proj_no != "")
        {
            prestr.Append("&loan_proj_no=" + loan_proj_no);
        }

        if (loan_status != "" && loan_status != "NORMAL")
        {
            prestr.Append("&loan_status=" + loan_status);
        }

        if (current_page > 0)
        {
            prestr.Append("&current_page=" + current_page);
        }

        if (page_size > 0)
        {
            prestr.Append("&page_size=" + page_size);
        }
        #endregion

        string request_url = gateway + prestr.ToString();

        CookieCollection cookies = new CookieCollection();

        string strJson = HttpHelper.GetResponseString(HttpHelper.CreateGetHttpResponse(request_url, 0, "", cookies));

        JsonInfo = JsonHelper.JSONToObject<QueryLoanProjectJsonInfo>(strJson);

        if (JsonInfo != null && JsonInfo.Is_success == "T")
        {
            pub.AddSysInterfaceLog(2, "信贷信息查询", "成功", request_url, "信贷信息查询成功");
        }
        else
        {
            pub.AddSysInterfaceLog(2, "信贷信息查询", "失败", request_url, JsonInfo.Error_code + ":" + JsonInfo.Error_message);
        }

        return JsonInfo;
    }

    public string ReturnQueryLoanProject(string UID, string loan_proj_no, string loan_status, int current_page, int page_size)
    {
        string sign, sign_type;

        string gateway = loan_url + "?_input_charset=utf-8";

        #region  拼接参数并转换成数组
        List<string> liststr = new List<string>();
        liststr.Add("service=query_loan_project");
        liststr.Add("version=1.0");
        liststr.Add("partner_id=" + partner_id);
        liststr.Add("_input_charset=utf-8");
        liststr.Add("uid=" + UID);
        if (loan_proj_no != "")
        {
            liststr.Add("loan_proj_no=" + loan_proj_no);
        }

        if (loan_status != "" && loan_status != "NORMAL")
        {
            liststr.Add("loan_status=" + loan_status);
        }

        if (current_page > 0)
        {
            liststr.Add("current_page=" + current_page);
        }

        if (page_size > 0)
        {
            liststr.Add("page_size=" + page_size);
        }
        #endregion

        string[] parameters = liststr.ToArray();

        sign_type = "MD5";
        sign = pub.ReturnSignStr(parameters, "utf-8", tradesignkey);


        return sign;
    }

    #endregion

    #region ---信贷分期详情查询---

    public QueryProjectDetailJsonInfo QueryProjectDetail(string UID, string loan_proj_no)
    {
        QueryProjectDetailJsonInfo JsonInfo = null;

        string sign, sign_type;

        string gateway = loan_url + "?_input_charset=utf-8";

        string[] parameters = 
        {
            "service=query_project_detail",
            "version=1.0",
            "partner_id=" + partner_id,
            "_input_charset=utf-8",
            "uid="+UID,
            "loan_proj_no=" + loan_proj_no
        };

        sign_type = "MD5";
        sign = pub.ReturnSignStr(parameters, "utf-8", tradesignkey);

        StringBuilder prestr = new StringBuilder();

        prestr.Append("&service=query_project_detail");
        prestr.Append("&version=1.0");
        prestr.Append("&partner_id=" + partner_id);
        prestr.Append("&sign=" + sign);
        prestr.Append("&sign_type=" + sign_type);
        prestr.Append("&uid=" + UID);
        prestr.Append("&loan_proj_no=" + loan_proj_no);

        string request_url = gateway + prestr.ToString();

        CookieCollection cookies = new CookieCollection();

        string strJson = HttpHelper.GetResponseString(HttpHelper.CreateGetHttpResponse(request_url, 0, "", cookies));

        JsonInfo = JsonHelper.JSONToObject<QueryProjectDetailJsonInfo>(strJson);

        if (JsonInfo != null && JsonInfo.Is_success == "T")
        {
            pub.AddSysInterfaceLog(2, "信贷分期详情查询", "成功", request_url, "信贷分期详情查询成功");
        }
        else
        {
            pub.AddSysInterfaceLog(2, "信贷分期详情查询", "失败", request_url, JsonInfo.Error_code + ":" + JsonInfo.Error_message);
        }

        return JsonInfo;
    }

    #endregion

    #region ---贷款还款---

    public LoanRepaymentJsonInfo LoanRepayment(string req_no, string UID, string loan_proj_no, double repay_amount)
    {
        LoanRepaymentJsonInfo JsonInfo = null;

        string sign, sign_type;

        string gateway = loan_url + "?_input_charset=utf-8";

        string[] parameters = 
        {
            "service=loan_repayment",
            "version=1.0",
            "partner_id=" + partner_id,
            "_input_charset=utf-8",
            "req_no=" + req_no,
            "uid=" + UID,
            "loan_proj_no=" + loan_proj_no,
            "repay_amount=" + repay_amount
        };

        sign_type = "MD5";
        sign = pub.ReturnSignStr(parameters, "utf-8", tradesignkey);

        StringBuilder prestr = new StringBuilder();

        prestr.Append("&service=loan_repayment");
        prestr.Append("&version=1.0");
        prestr.Append("&partner_id=" + partner_id);
        prestr.Append("&sign=" + sign);
        prestr.Append("&sign_type=" + sign_type);
        prestr.Append("&req_no="+req_no);
        prestr.Append("&uid=" + UID);
        prestr.Append("&loan_proj_no=" + loan_proj_no);
        prestr.Append("&repay_amount=" + repay_amount);

        string request_url = gateway + prestr.ToString();

        CookieCollection cookies = new CookieCollection();

        string strJson = HttpHelper.GetResponseString(HttpHelper.CreateGetHttpResponse(request_url, 0, "", cookies));

        JsonInfo = JsonHelper.JSONToObject<LoanRepaymentJsonInfo>(strJson);

        if (JsonInfo != null && JsonInfo.Is_success == "T")
        {
            pub.AddSysInterfaceLog(2, "贷款还款", "成功", request_url, "贷款编号：" + JsonInfo.Loan_proj_no + ";还款url：" + JsonInfo.Repay_url);
        }
        else
        {
            pub.AddSysInterfaceLog(2, "贷款还款", "失败", request_url, JsonInfo.Error_code + ":" + JsonInfo.Error_message);
        }

        return JsonInfo;
    }

    #endregion

    #region ---贷款撤销---

    public LoanCancelJsonInfo LoanCancel(string UID, string loan_proj_no, string cause, string channel)
    {
        LoanCancelJsonInfo JsonInfo = null;
         
        string sign, sign_type;

        string gateway = loan_url + "?_input_charset=utf-8";

        string[] parameters = 
        {
            "service=loan_cancel",
            "version=1.0",
            "partner_id=" + partner_id,
            "_input_charset=utf-8",
            "uid=" + UID,
            "loan_proj_no=" + loan_proj_no,
            "cause=" + cause,
            "channel=" + channel
        };

        sign_type = "MD5";
        sign = pub.ReturnSignStr(parameters, "utf-8", tradesignkey);

        StringBuilder prestr = new StringBuilder();

        prestr.Append("&service=loan_cancel");
        prestr.Append("&version=1.0");
        prestr.Append("&partner_id=" + partner_id);
        prestr.Append("&sign=" + sign);
        prestr.Append("&sign_type=" + sign_type);
        prestr.Append("&uid=" + UID);
        prestr.Append("&loan_proj_no=" + loan_proj_no);
        prestr.Append("&cause=" + cause);
        prestr.Append("&channel=" + channel);

        string request_url = gateway + prestr.ToString();

        CookieCollection cookies = new CookieCollection();

        string strJson = HttpHelper.GetResponseString(HttpHelper.CreateGetHttpResponse(request_url, 0, "", cookies));

        JsonInfo = JsonHelper.JSONToObject<LoanCancelJsonInfo>(strJson);

        if (JsonInfo != null && JsonInfo.Is_success == "T")
        {
            pub.AddSysInterfaceLog(2, "贷款撤销", "成功", request_url, "贷款撤销成功");
        }
        else
        {
            pub.AddSysInterfaceLog(2, "贷款撤销", "失败", request_url, JsonInfo.Error_code + ":" + JsonInfo.Error_message);
        }

        return JsonInfo;
    }

    #endregion

    #region ---保证金转余额---

    public MarginToBalanceJsonInfo MarginToBalance(string UID, double amount)
    {
        MarginToBalanceJsonInfo JsonInfo = null;

        string sign, sign_type;

        string gateway = loan_url + "?_input_charset=utf-8";

        string[] parameters = 
        {
            "service=margin_to_balance",
            "version=1.0",
            "partner_id=" + partner_id,
            "_input_charset=utf-8",
            "uid=" + UID,
            "amount=" + amount
        };

        sign_type = "MD5";
        sign = pub.ReturnSignStr(parameters, "utf-8", tradesignkey);

        StringBuilder prestr = new StringBuilder();

        prestr.Append("&service=margin_to_balance");
        prestr.Append("&version=1.0");
        prestr.Append("&partner_id=" + partner_id);
        prestr.Append("&sign=" + sign);
        prestr.Append("&sign_type=" + sign_type);
        prestr.Append("&uid=" + UID);
        prestr.Append("&amount=" + amount);

        string request_url = gateway + prestr.ToString();

        CookieCollection cookies = new CookieCollection();

        string strJson = HttpHelper.GetResponseString(HttpHelper.CreateGetHttpResponse(request_url, 0, "", cookies));

        JsonInfo = JsonHelper.JSONToObject<MarginToBalanceJsonInfo>(strJson);

        if (JsonInfo != null && JsonInfo.Is_success == "T")
        {
            pub.AddSysInterfaceLog(2, "保证金转余额", "成功", prestr.ToString(), "贷款撤销成功");
        }
        else
        {
            pub.AddSysInterfaceLog(2, "保证金转余额", "失败", prestr.ToString(), JsonInfo.Error_code + ":" + JsonInfo.Error_message);
        }

        return JsonInfo;
    }

    #endregion
}