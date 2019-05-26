using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// 银联支付处理
/// </summary>
public class NetPayClient
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private string priKeyPath;
    private string pubKeyPath;
    private Public_Class pub;

    public NetPayClient()
    {
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        pub = new Public_Class();
        //pub.SetCurrentSite();
        priKeyPath = System.Configuration.ConfigurationManager.AppSettings["priKeyPath"].ToString();
        pubKeyPath = System.Configuration.ConfigurationManager.AppSettings["pubKeyPath"].ToString();
    }

    /// <summary>
    /// 字符串签名
    /// </summary>
    /// <param name="MerId">商户号</param>
    /// <param name="plain">要签名的字符串</param>
    /// <returns></returns>
    public string sign(string MerId, string plain)
    {
        NetPay netPay = new NetPay();
        Boolean flag = netPay.buildKey(MerId, 0, priKeyPath);
        string sign = null;
        if (flag)
        {
            if (netPay.PrivateKeyFlag)
            {
                sign = netPay.Sign(plain);
            }
        }
        return sign;
    }

    /// <summary>
    /// 验签
    /// </summary>
    /// <param name="MerId">商户号</param>
    /// <param name="OrdId">订单号</param>
    /// <param name="TransAmt">订单金额</param>
    /// <param name="CuryId">货币代码</param>
    /// <param name="TransDate">订单日期</param>
    /// <param name="TransType">交易类型</param>
    /// <param name="status">交易转态</param>
    /// <param name="ChkValue">商户数字签名</param>
    /// <returns></returns>
    public bool check(string MerId, string OrdId, string TransAmt, string CuryId, string TransDate, string TransType, string status, string ChkValue)
    {
        NetPay netPay = new NetPay();
        Boolean flag = netPay.buildKey("999999999999999", 0, pubKeyPath);
        if (flag)
        {
            if (netPay.PublicKeyFlag)
            {
                flag = netPay.verifyTransResponse(MerId, OrdId, TransAmt, CuryId, TransDate, TransType, status, ChkValue);
            }
            else
            {
                flag = false;
            }
        }
        else
        {
            flag = false;
        }

        return flag;
    }

    /// <summary>
    /// 发送交易请求
    /// </summary>
    /// <param name="OrdId">订单号</param>
    /// <param name="TransAmt">订单金额</param>
    /// <param name="Priv1">备注</param>
    public void Pay_NetPay_Send(string OrdId, string TransAmt, string Priv1)
    {
        string MerId = Application["U_ChinaPay_Code"] + "";
        string CuryId = "156";
        string TransType = "0001";
        string TransDate = DateTime.Today.ToString("yyyyMMdd");

        //后台/页面返回地址
        string BgRetUrl = Application["Site_URL"] + "/pay/pay_notify.aspx?payment=CHINAPAY";
        string PageRetUrl = Application["Site_URL"] + "/pay/pay_result.aspx?payment=CHINAPAY";

        OrdId = OrdersTransform(OrdId);
        TransAmt = TransAmtTransform(TransAmt);

        //准备签名的数据
        string plain = MerId + OrdId + TransAmt + CuryId + TransDate + TransType + Priv1;

        //开始签名
        string ChkValue = sign(MerId, plain);

        if (ChkValue == null || ChkValue.Length != 256)
        {
            pub.Msg("error", "错误提示", "签名失败！", false, 3, "{back}");
            return;
        }

        Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
        Response.Write("<head>");
        Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
        Response.Write("<title>PayGate</title>");
        Response.Write("</head>");
        Response.Write("<body onload=\"javascript:document.frm_pay_netpay.submit();\">");
        Response.Write("<form action=\"https://payment.ChinaPay.com/pay/TransGet\" method=\"POST\" name=\"frm_pay_netpay\">");
        Response.Write("<input type=\"hidden\" name=\"MerId\" value=\"" + MerId + "\" />");
        Response.Write("<input type=\"hidden\" name=\"Version\" value=\"20070129\" />");
        Response.Write("<input type=\"hidden\" name=\"OrdId\" value=\"" + OrdId + "\" />");
        Response.Write("<input type=\"hidden\" name=\"TransAmt\" value=\"" + TransAmt + "\" />");
        Response.Write("<input type=\"hidden\" name=\"CuryId\" value=\"" + CuryId + "\" />");
        Response.Write("<input type=\"hidden\" name=\"TransDate\" value=\"" + TransDate + "\" />");
        Response.Write("<input type=\"hidden\" name=\"TransType\" value=\"" + TransType + "\" />");
        Response.Write("<input type=\"hidden\" name=\"BgRetUrl\" value=\"" + BgRetUrl + "\" />");
        Response.Write("<input type=\"hidden\" name=\"PageRetUrl\" value=\"" + PageRetUrl + "\" />");
        //Response.Write("<input type=\"hidden\" name=\"GateId\" value=\"0001\" />");
        Response.Write("<input type=\"hidden\" name=\"Priv1\" value=\"" + Priv1 + "\" />");
        Response.Write("<input type=\"hidden\" name=\"ChkValue\" value=\"" + ChkValue + "\" />");
        Response.Write("</form>");
        Response.Write("</body>");
        Response.Write("</html>");
    }

    /// <summary>
    /// 转换交易金额
    /// </summary>
    /// <param name="TransAmt">交易价格</param>
    /// <returns>符合标准的金额</returns>
    public string TransAmtTransform(string TransAmt)
    {
        double money = double.Parse(TransAmt);
        money = Math.Round(money, 2) * 100;

        string retmomey = money.ToString();

        for (int ii = retmomey.Length; ii < 12; ii++)
            retmomey = "0" + retmomey;

        return retmomey;
    }

    /// <summary>
    /// 转换交易订单号
    /// </summary>
    /// <param name="OrdId">订单号</param>
    /// <returns>符合标准的订单号</returns>
    public string OrdersTransform(string OrdId)
    {
        for (int ii = OrdId.Length; ii < 16; ii++)
            OrdId = "0" + OrdId;
        return OrdId;
    }


}
