using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.ORD;
using System.Security.Cryptography.X509Certificates;
using System.Collections;

/// <summary>
///支付类
/// </summary>
public class Paybak 
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;
     
    private ITools tools;
    private Public_Class pub = new Public_Class();
    private IEncrypt encrypt;
    private IOrders MyOrders;
    private IOrdersLog Mylog;
    private Addr addr = new Addr();
    private IDeliveryWay MyDelivery;
    private IPayWay MyPayway;
    private IOrdersPayment MyPayment;
    private Member MyMEM;
    private NetPayClient mynetpay = new NetPayClient();

    public Paybak() 
    {
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        encrypt = EncryptFactory.CreateEncrypt();
        MyOrders = OrdersFactory.CreateOrders();
        Mylog = OrdersLogFactory.CreateOrdersLog();
        MyDelivery = DeliveryWayFactory.CreateDeliveryWay();
        MyPayway = PayWayFactory.CreatePayWay();
        MyPayment = OrdersPaymentFactory.CreateOrdersPayment();
        MyMEM = new Member();
    }

    #region "支付辅助函数"
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
        // //返回支付Url；  
        return sign;
    }

    //获取远程服务器ATN结果
    public String Get_Http(String a_strUrl, int timeout)
    {
        string strResult;
        try
        {
            
            HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(a_strUrl);
            myReq.Timeout = timeout;
            HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
            Stream myStream = HttpWResp.GetResponseStream();
            StreamReader sr = new StreamReader(myStream, Encoding.Default);
            StringBuilder strBuilder = new StringBuilder();
            while (-1 != sr.Peek())
            {
                strBuilder.Append(sr.ReadLine());
            }

            strResult = strBuilder.ToString();
        }
        catch (Exception exp)
        {

            strResult = "错误：" + exp.Message;
        }

        return strResult;
    }

    //构建指定长度的随机字符串
    public static string BuildRandomStr(int length)
    {
        Random rand = new Random();

        int num = rand.Next();

        string str = num.ToString();

        if (str.Length > length)
        {
            str = str.Substring(0, length);
        }
        else if (str.Length < length)
        {
            int n = length - str.Length;
            while (n > 0)
            {
                str.Insert(0, "0");
                n -= 1;
            }
        }

        return str;
    }

    public static string Tenpay_CreateSign(string cmdno, string spbill_create_ip, string datestr, string bargainor_id, string transaction_id, string sp_billno, string total_fee, string fee_type, string return_url, string attach,
    string _input_charset, string key)
    {
        //获取参数


        //组织签名
        StringBuilder sb = new StringBuilder();

        sb.Append("cmdno=" + cmdno + "&");
        sb.Append("date=" + datestr + "&");
        sb.Append("bargainor_id=" + bargainor_id + "&");
        sb.Append("transaction_id=" + transaction_id + "&");
        sb.Append("sp_billno=" + sp_billno + "&");
        sb.Append("total_fee=" + Convert.ToString(total_fee) + "&");
        sb.Append("fee_type=" + fee_type + "&");
        sb.Append("return_url=" + return_url + "&");
        sb.Append("attach=" + attach + "&");
        sb.Append("spbill_create_ip=" + spbill_create_ip + "&");

        sb.Append("key=" + key);

        //算出摘要
        string sign = GetMD5(sb.ToString(), _input_charset).ToUpper();
        //Dim sign As String = sb.ToString()
        return sign;

    }
    #endregion

    #region "支付业务执行"

    //虚拟账户支付
    public void Virtual_Account_Pay(string orders_sn, double v_amount)
    {
        int pay_status, member_id;
        string Pay_Note, Pay_Detail;
        member_id = 0;
        int ReturnValue = Pay_CheckAmountNoPay(orders_sn, tools.NullDbl(v_amount * 100));
        if (ReturnValue > 0)
        {
            OrdersInfo entity = MyOrders.GetOrdersByID(ReturnValue);
            if (entity != null)
            {
                pay_status = entity.Orders_PaymentStatus;
                member_id = entity.Orders_BuyerID;
                if (pay_status == 0)
                {
                    entity.Orders_PaymentStatus = 1;
                    entity.Orders_PaymentStatus_Time = DateTime.Now;
                    MyOrders.EditOrders(entity);
                }
            }
            else
            {
                pay_status = -1;
            }
            entity = null;
            if (pay_status == 0)
            {
                
                MyMEM.Member_Account_Log(member_id, 0 - v_amount, "订单支付，支付订单号：" + orders_sn + "");
                
                //添加付款单
                string Orders_Payment_DocNo = Pay_NewPaymentDocNo();
                int Orders_Payment_ID = Pay_AddPayment(ReturnValue, 1, 0, Orders_Payment_DocNo, "虚拟账户", v_amount, "通过虚拟账户支付成功");
                string OrdersLogNote = "";
                OrdersLogNote = "订单付款，付款单号{order_payment_sn} &nbsp;金额{order_payment_totalprice}";
                OrdersLogNote = OrdersLogNote.Replace("{order_payment_sn}", "<a href=\"/orderspayment/orders_payment_view.aspx?orders_payment_id=" + Orders_Payment_ID + "\">" + Orders_Payment_DocNo + "</a>");
                OrdersLogNote = OrdersLogNote.Replace("{order_payment_totalprice}", pub.FormatCurrency(v_amount));
                Pay_Detail = OrdersLogNote;
                Pay_Detail = "虚拟余额支付，支付时间：" + DateTime.Now;
                Orders_Log(ReturnValue, "", "付款", "成功", OrdersLogNote);
                //记录支付日志
                Pay_Note = "[成功]通过虚拟账户支付成功";
                Pay_Log(orders_sn, 1, "虚拟账户", v_amount, Pay_Note, Pay_Detail);

            }
            Response.Redirect("/pay/pay_result.aspx?payment=ACCOUNT&result=success&orders_sn=" + orders_sn);
        }
        else
        {
            //记录支付日志
            Pay_Note = "[失败]通过虚拟账户支付成功，但找不到该订单或重复支付订单";
            Pay_Detail = Pay_Note;
            Pay_Detail = Pay_Detail + "，支付时间：" + DateTime.Now;
            Pay_Log(orders_sn, 0, "虚拟账户", v_amount, Pay_Note, Pay_Detail);
            Response.Redirect("/pay/pay_result.aspx?payment=ACCOUNT&result=failed&orders_sn=" + orders_sn);
        }
    }

    #region "网银在线支付"

    //网银在线支付信息发送
    public void Pay_Chinabank_Send(string v_mid, string v_key, string v_url, string v_oid, double v_amount, string v_moneytype)
    {
        string v_md5info = "";
        v_md5info = v_amount.ToString("0.00") + v_moneytype + v_oid + v_mid + v_url + v_key;
        v_md5info = encrypt.MD5(v_md5info, 32);
        v_md5info = v_md5info.ToUpper();

        Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
        Response.Write("<head>");
        Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\" />");
        Response.Write("<title>PayGate</title>");
        Response.Write("</head>");

        Response.Write("<body onload=\"javascript:document.frm_pay_chinabank.submit();\">");
        Response.Write("<form action=\"https://pay3.chinabank.com.cn/PayGate\" method=\"POST\" name=\"frm_pay_chinabank\">");
        Response.Write("<input type=\"hidden\" name=\"v_md5info\"    value=\"" + v_md5info + "\">");
        Response.Write("<input type=\"hidden\" name=\"v_mid\"        value=\"" + v_mid + "\">");
        Response.Write("<input type=\"hidden\" name=\"v_oid\"        value=\"" + v_oid + "\">");
        Response.Write("<input type=\"hidden\" name=\"v_amount\"     value=\"" + v_amount.ToString("0.00") + "\">");
        Response.Write("<input type=\"hidden\" name=\"v_moneytype\"  value=\"" + v_moneytype + "\">");
        Response.Write("<input type=\"hidden\" name=\"v_url\"        value=\"" + v_url + "\">");
        Response.Write("</form>");
        Response.Write("</body>");
        Response.Write("</html>");
    }

    //网银在线支付返回
    public void Pay_Chinabank_Receive(string v_key, string v_oid, string v_pmode, string v_pstatus, string v_pstring, string v_amount, string v_moneytype, string remark1, string remark2, string v_md5str)
    {
        string v_md5info = "";
        string sql_pay = "";
        int pay_status = 0;
        int Orders_UseCoin = 0;
        double Orders_Total_Price = 0;
        double Orders_Total_Freight = 0;
        double Orders_Total_AllPrice = 0;
        int orders_payment_type = 0;
        string Log_note = "";
        string Pay_Detail = null;
        string Pay_Note = null;


        orders_payment_type = -1;
        v_md5info = v_oid + v_pstatus + v_amount + v_moneytype + v_key;
        v_md5info = encrypt.MD5(v_md5info, 32);
        v_md5info = v_md5info.ToUpper();
        Pay_Detail += "ORDERID:" + v_oid + "<BR>";
        Pay_Detail += "AMOUNT:" + v_amount + "<BR>";
        Pay_Detail += "TIME:" + DateTime.Now + "<BR>";
        Pay_Detail += "STATUS:" + v_pstatus + "<BR>";
        if (v_md5info != v_md5str)
        {
            //效验有误
            //functionReturnValue = -1;
            //记录到支付日志表
            Pay_Note = "[失败]返回值效验失败";
            Pay_Log(v_oid, 0, "网银在线", tools.CheckFloat(v_amount), Pay_Note, Pay_Detail);
            //返回用户提示窗口
            Pay_Result("failed", "");
        }
        else
        {
            if (v_pstatus == "20")
            {
                //支付成功
               
                //订单处理
                int process_result = Pay_Orders_Process(v_oid, "网银在线", tools.CheckFloat(v_amount), Pay_Detail);
                if (process_result == 1)
                {
                    Pay_Result("success", v_oid);
                }
                else
                {
                    Pay_Result("failed", "");
                }
            }
            else if (v_pstatus == "30")
            {
                //支付失败
                //functionReturnValue = 0;
                //记录到支付日志表
                Pay_Note = "[失败]通过网银在线支付失败";
                Pay_Log(v_oid, 0, "网银在线", tools.CheckFloat(v_amount), Pay_Note, Pay_Detail);
                //返回用户提示窗口
                Pay_Result("failed", "");
            }
        }
    }

    //网银在线支付通知
    public void Pay_Chinabank_Notify(string v_key, string v_oid, string v_pmode, string v_pstatus, string v_pstring, string v_amount, string v_moneytype, string remark1, string remark2, string v_md5str)
    {
        string v_md5info = "";
        string sql_pay = "";
        int pay_status = 0;
        int Orders_UseCoin = 0;
        double Orders_Total_Price = 0;
        double Orders_Total_Freight = 0;
        double Orders_Total_AllPrice = 0;
        int orders_payment_type = 0;
        string Log_note = "";
        string Pay_Detail = null;
        string Pay_Note = null;


        orders_payment_type = -1;
        v_md5info = v_oid + v_pstatus + v_amount + v_moneytype + v_key;
        v_md5info = encrypt.MD5(v_md5info, 32);
        v_md5info = v_md5info.ToUpper();
        Pay_Detail += "ORDERID:" + v_oid + "<BR>";
        Pay_Detail += "AMOUNT:" + v_amount + "<BR>";
        Pay_Detail += "TIME:" + DateTime.Now + "<BR>";
        Pay_Detail += "STATUS:" + v_pstatus + "<BR>";
        if (v_md5info != v_md5str)
        {
            //效验有误
            //functionReturnValue = -1;
            //记录到支付日志表
            Pay_Note = "[失败]返回值效验失败";
            Pay_Log(v_oid, 0, "网银在线", tools.CheckFloat(v_amount), Pay_Note, Pay_Detail);
            //返回用户提示窗口
            Response.Write("error");
        }
        else
        {
            if (v_pstatus == "20")
            {
                //支付成功
                //订单处理
                int process_result = Pay_Orders_Process(v_oid, "网银在线", tools.CheckFloat(v_amount), Pay_Detail);
                if (process_result == 1)
                {
                    Response.Write("ok");
                }
                else
                {
                    Response.Write("error");
                }
            }
            else if (v_pstatus == "30")
            {
                //支付失败
                //functionReturnValue = 0;
                //记录到支付日志表
                Pay_Note = "[失败]通过网银在线支付失败";
                Pay_Log(v_oid, 0, "网银在线", tools.CheckFloat(v_amount), Pay_Note, Pay_Detail);
                //返回用户提示窗口
                Response.Write("error");
            }
        }
    }
    
    #endregion

    #region "交通银行支付"

    //交通银行支付信息发送
    public void Pay_Bocomm_Send(int orders_id, string orders_sn, double v_amount, string v_url, string Orders_Addtime, string Orders_Delivery_Name, string v_moneytype)
    {
        //Response.Redirect("http://www.bankcomm.com/");
        string interfaceVersion = "1.0.0.0";
        string orderid = orders_sn;
        string orderDate = tools.FormatDate(DateTime.Now, "yyyyMMdd");
        string orderTime = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        string tranType = "0";
        string amount = v_amount.ToString("0.00");
        string curType = v_moneytype;
        string orderContent = "";
        string orderMono = "";

        string phdFlag = "1";
        string notifyType = "0";
        string merURL = v_url;
        string goodsURL = v_url;
        string jumpSeconds = "0";
        string payBatchNo = "";
        string proxyMerName = "";
        string proxyMerType = "";
        string proxyMercredentials = "";
        string netType = "0";
        string issBankNo = "";

        B2CCLIENTCOMCTRLLib.B2CClientCOM client = new B2CCLIENTCOMCTRLLib.B2CClientCOM();
        int ret = 0;
        ret = client.Initialize("C:\\\\bocomm\\\\ini\\\\B2CMerchant.xml");
        if (ret != 0)
        {
            pub.Msg("error", "错误提示", "初始化失败，请稍后再试！", false, 3, "{back}");
        }
        string merID = client.GetMerchantID();
        string sourcemsg = null;
        sourcemsg = interfaceVersion + "|" + merID + "|" + orderid + "|" + orderDate + "|" + orderTime + "|" + tranType + "|" + amount + "|" + curType + "|" + orderContent + "|" + orderMono + "|" + phdFlag + "|" + notifyType + "|" + merURL + "|" + goodsURL + "|" + jumpSeconds + "|" + payBatchNo + "|" + proxyMerName + "|" + proxyMerType + "|" + proxyMercredentials + "|" + netType;
        string signData = null;
        signData = client.Sign_detachsign(sourcemsg);
        Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
        Response.Write("<head>");
        Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\" />");
        Response.Write("<title>PayGate</title>");
        Response.Write("</head>");

        Response.Write("<body onload=\"javascript:document.frm_pay_bcom.submit();\">");
        Response.Write("<form name=\"frm_pay_bcom\" method=\"post\" action =\"" + client.GetOrderReqURL() + "\">");
        Response.Write("<input type = \"hidden\" name = \"interfaceVersion\" value = \"" + interfaceVersion + "\"/>");
        Response.Write("\t<input type = \"hidden\" name = \"merID\" value = \"" + merID + "\"/>");
        Response.Write("\t<input type = \"hidden\" name = \"orderid\" value = \"" + orderid + "\"/>");
        Response.Write("\t<input type = \"hidden\" name = \"orderDate\" value = \"" + orderDate + "\"/>");
        Response.Write("\t<input type = \"hidden\" name = \"orderTime\" value = \"" + orderTime + "\"/>");
        Response.Write("\t<input type = \"hidden\" name = \"tranType\" value = \"" + tranType + "\"/>");
        Response.Write("\t<input type = \"hidden\" name = \"amount\" value = \"" + amount + "\"/>");
        Response.Write("\t<input type = \"hidden\" name = \"curType\" value = \"" + curType + "\"/>");
        Response.Write("\t<input type = \"hidden\" name = \"orderContent\" value = \"" + orderContent + "\"/>");
        Response.Write("\t<input type = \"hidden\" name = \"orderMono\" value = \"" + orderMono + "\"/>");
        Response.Write("\t<input type = \"hidden\" name = \"phdFlag\" value = \"" + phdFlag + "\"/>");
        Response.Write("\t<input type = \"hidden\" name = \"notifyType\" value = \"" + notifyType + "\"/>");
        Response.Write("\t<input type = \"hidden\" name = \"merURL\" value = \"" + merURL + "\"/>");
        Response.Write("\t<input type = \"hidden\" name = \"goodsURL\" value = \"" + goodsURL + "\"/>");
        Response.Write("\t<input type = \"hidden\" name = \"jumpSeconds\" value = \"" + jumpSeconds + "\"/>");
        Response.Write("\t<input type = \"hidden\" name = \"payBatchNo\" value = \"" + payBatchNo + "\"/>");
        Response.Write("\t<input type = \"hidden\" name = \"proxyMerName\" value = \"" + proxyMerName + "\"/>");
        Response.Write("\t<input type = \"hidden\" name = \"proxyMerType\" value = \"" + proxyMerType + "\"/>");
        Response.Write("\t<input type = \"hidden\" name = \"proxyMerCredentials\" value = \"" + proxyMercredentials + "\"/>");
        Response.Write("\t<input type = \"hidden\" name = \"netType\" value = \"" + netType + "\"/>");
        Response.Write("\t<input type = \"hidden\" name = \"merSignMsg\" value = \"" + signData + "\"/>");
        Response.Write("\t<input type = \"hidden\" name = \"issBankNo\" value = \"" + issBankNo + "\"/>");
        Response.Write("</form>");
        Response.Write("</body>");
        Response.Write("</html>");
    }

    //交通银行支付返回
    public void Pay_Bocomm_Receive()
    {
        B2CCLIENTCOMCTRLLib.B2CClientCOM client = new B2CCLIENTCOMCTRLLib.B2CClientCOM();
        int ret = 0;
        ret = client.Initialize("C:\\\\bocomm\\\\ini\\\\B2CMerchant.xml");
        if (ret != 0)
        {
            pub.Msg("error", "错误提示", "初始化失败，请稍后再试！", false, 3, "{back}");
            Response.End();
        }
        string notifyMsg = Request["notifyMsg"];
        //Response.Write(notifyMsg & "<br>")
        int lastIndex = notifyMsg.LastIndexOf("|");
        string srcMsg = notifyMsg.Substring(0, lastIndex + 1);
        //原文
        string signMsg = notifyMsg.Substring(lastIndex + 1);
        //签名
        //Response.Write(srcMsg & "<br>" & signMsg)
        //Response.Write("<br>" & client.Sign_detachsign("301310063009501|1008090021|17.82|CNY|20110305| |20110308|211835|00012776|1|0.09|0|dbtnMdkExxWS6SR7CBfYaG5I3A==^622260**********869| |221.221.17.130|221.221.17.130|"))
        int verifyCode = client.Sign_detachverify(srcMsg, signMsg);

        if (verifyCode != 0)
        {
            string errCode = client.GetErrorMessage();
            //Call Pub.Msg("error", "错误提示", "商户端验证签名失败:" & verifyCode, False, 3, "/index.aspx")
            Pay_Result("failed", "");
        }
        string[] strs;
        strs = new string[20];
        int i = 0;
        foreach (string substr in notifyMsg.Split('|'))
        {
            strs[i] = substr;
            i = i + 1;
        }

        //Response.Write("商户客户号：" & strs(0) & "<br>")
        //Response.Write("订单编号：" & strs(1) & "<br>")
        //Response.Write("交易金额：" & strs(2) & "<br>")
        //Response.Write("交易币种：" & strs(3) & "<br>")
        //Response.Write("平台批次号：" & strs(4) & "<br>")
        //Response.Write("商户批次号：" & strs(5) & "<br>")
        //Response.Write("交易日期：" & strs(6) & "<br>")
        //Response.Write("交易时间：" & strs(7) & "<br>")
        //Response.Write("交易流水号：" & strs(8) & "<br>")
        //Response.Write("交易结果：" & strs(9) & "<br>")
        //Response.Write("手续费总额：" & strs(10) & "<br>")
        //Response.Write("银行卡类型：" & strs(11) & "<br>")
        //Response.Write("银行备注：" & strs(12) & "<br>")
        //Response.Write("错误信息描述：" & strs(13) & "<br>")
        //Response.Write("IP：" & strs(14) & "<br>")
        //Response.Write("Referer：" & strs(15) & "<br>")
        //Response.End()
        string Pay_Detail = "";
        Pay_Detail += "商户客户号:" + strs[0] + "<BR>";
        Pay_Detail += "订单编号:" + strs[1] + "<BR>";
        Pay_Detail += "交易金额:" + strs[2] + "<BR>";
        Pay_Detail += "交易日期:" + strs[6] + "<BR>";
        Pay_Detail += "交易结果:" + strs[9] + "<BR>";
        //支付成功
        //更新订单数据

        //订单处理
        int process_result = Pay_Orders_Process(strs[1], "交通银行", tools.CheckFloat(strs[2]), Pay_Detail);
        if (process_result == 1)
        {
            Pay_Result("success", strs[1]);
        }
        else
        {
            Pay_Result("failed", "");
        }
        

    }

    #endregion

    #region "支付宝支付"

    ///双接口支付宝支付信息发送
    public void Pay_Alipay_Send(string alipay_notify_url, string alipay_return_url, string alipay_subject, string alipay_show_url, string alipay_body, string alipay_out_trade_no, string alipay_price, string payment_type, string paymethod, string defaultbank, string total_freight)
    {

        string alipay_partner = tools.NullStr(Application["Alipay_Code"]);
        string alipay_seller_email = tools.NullStr(Application["Alipay_Email"]);
        string alipay_key = tools.NullStr(Application["Alipay_Key"]);

        //if (alipay_key=="" || alipay_seller_email=="" || alipay_partner=="") {
        //    pub.Msg("error", "错误提示", "暂时不支付支付", false, 3, "{back}");
        //    return;
        //}
        string alipay_sign;
        string alipay_sign_type;
        string notify_url = alipay_notify_url;
        string gateway = "https://mapi.alipay.com/gateway.do?";


        string logistics_type = "EXPRESS";
        string logistics_fee = "0";
        string logistics_payment = "BUYER_PAY";


        string[] para = {
            //支付双接口
	        "service=trade_create_by_buyer",
	        "partner=" + alipay_partner,
	        "seller_email=" + alipay_seller_email,
	        "out_trade_no=" + alipay_out_trade_no,
	        "subject=" + alipay_subject,
	        "body=" + alipay_body,
	        "price=" + alipay_price,
	        "show_url=" + alipay_show_url,
	        "payment_type=1",
	        "return_url=" + alipay_return_url,
	        "notify_url=" + alipay_notify_url,
	        "_input_charset=utf-8",
	        "quantity=1",
	        "logistics_type=" + logistics_type,
	        "logistics_fee=" + logistics_fee,
	        "logistics_payment=" + logistics_payment,
            "agent=isv^gl22"
        };
        alipay_sign_type = "MD5";
        alipay_sign = CreatUrl(para, "utf-8", alipay_sign_type, alipay_key);
        Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
        Response.Write("<head>");
        Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
        Response.Write("<title>PayGate</title>");
        Response.Write("</head>");

        Response.Write("<body onload=\"javascript:document.frm_pay_alipay.submit();\">");
        Response.Write("<form action=\"https://mapi.alipay.com/gateway.do?_input_charset=utf-8\" method=\"POST\" name=\"frm_pay_alipay\">");
        //支付双接口
        Response.Write("<input type=\"hidden\" name=\"service\" value=\"trade_create_by_buyer\">");
        Response.Write("<input type=\"hidden\" name=\"partner\" value=\"" + alipay_partner + "\">");
        Response.Write("<input type=\"hidden\" name=\"notify_url\" value=\"" + alipay_notify_url + "\">");
        Response.Write("<input type=\"hidden\" name=\"return_url\" value=\"" + alipay_return_url + "\">");
        Response.Write("<input type=\"hidden\" name=\"sign\" value=\"" + alipay_sign + "\">");
        Response.Write("<input type=\"hidden\" name=\"sign_type\" value=\"" + alipay_sign_type + "\">");
        Response.Write("<input type=\"hidden\" name=\"subject\" value=\"" + alipay_subject + "\">");
        Response.Write("<input type=\"hidden\" name=\"show_url\" value=\"" + alipay_show_url + "\">");
        Response.Write("<input type=\"hidden\" name=\"body\" value=\"" + alipay_body + "\">");
        Response.Write("<input type=\"hidden\" name=\"out_trade_no\" value=\"" + alipay_out_trade_no + "\">");
        Response.Write("<input type=\"hidden\" name=\"price\" value=\"" + alipay_price + "\">");
        Response.Write("<input type=\"hidden\" name=\"payment_type\" value=\"1\">");
        Response.Write("<input type=\"hidden\" name=\"quantity\" value=\"1\">");
        Response.Write("<input type=\"hidden\" name=\"logistics_type\" value=\"" + logistics_type + "\">");
        Response.Write("<input type=\"hidden\" name=\"logistics_fee\" value=\"" + logistics_fee + "\">");
        Response.Write("<input type=\"hidden\" name=\"logistics_payment\" value=\"" + logistics_payment + "\">");
        Response.Write("<input type=\"hidden\" name=\"seller_email\" value=\"" + alipay_seller_email + "\">");
        Response.Write("<input type=\"hidden\" name=\"agent\" value=\"isv^gl22\">");
        Response.Write("</form>");
        Response.Write("</body>");
        Response.Write("</html>");

    }

    //纯担保交易支付宝支付信息发送
    public void Pay_Alipay_Agent_Send(string alipay_notify_url, string alipay_return_url, string alipay_subject, string alipay_show_url, string alipay_body, string alipay_out_trade_no, string alipay_price, string payment_type, string paymethod, string defaultbank, string total_freight)
    {

        string alipay_partner = tools.NullStr(Application["Alipay_Code"]);
        string alipay_seller_email = tools.NullStr(Application["Alipay_Email"]);
        string alipay_key = tools.NullStr(Application["Alipay_Key"]);

        //if (alipay_key=="" || alipay_seller_email=="" || alipay_partner=="") {
        //    pub.Msg("error", "错误提示", "暂时不支付支付", false, 3, "{back}");
        //    return;
        //}
        string alipay_sign;
        string alipay_sign_type;
        string notify_url = alipay_notify_url;
        string gateway = "https://mapi.alipay.com/gateway.do?";


        string logistics_type = "EXPRESS";
        string logistics_fee = "0";
        string logistics_payment = "BUYER_PAY";


        string[] para = {
            //纯担保交易
            "service=create_partner_trade_by_buyer",
	        "partner=" + alipay_partner,
	        "seller_email=" + alipay_seller_email,
	        "out_trade_no=" + alipay_out_trade_no,
	        "subject=" + alipay_subject,
	        "body=" + alipay_body,
	        "price=" + alipay_price,
	        "show_url=" + alipay_show_url,
	        "payment_type=1",
	        "return_url=" + alipay_return_url,
	        "notify_url=" + alipay_notify_url,
	        "_input_charset=utf-8",
	        "quantity=1",
	        "logistics_type=" + logistics_type,
	        "logistics_fee=" + logistics_fee,
	        "logistics_payment=" + logistics_payment,
            "agent=isv^gl22"
        };
        alipay_sign_type = "MD5";
        alipay_sign = CreatUrl(para, "utf-8", alipay_sign_type, alipay_key);
        Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
        Response.Write("<head>");
        Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
        Response.Write("<title>PayGate</title>");
        Response.Write("</head>");

        Response.Write("<body onload=\"javascript:document.frm_pay_alipay.submit();\">");
        Response.Write("<form action=\"https://mapi.alipay.com/gateway.do?_input_charset=utf-8\" method=\"POST\" name=\"frm_pay_alipay\">");
        //纯担保交易
        Response.Write("<input type=\"hidden\" name=\"service\" value=\"create_partner_trade_by_buyer\">");
        Response.Write("<input type=\"hidden\" name=\"partner\" value=\"" + alipay_partner + "\">");
        Response.Write("<input type=\"hidden\" name=\"notify_url\" value=\"" + alipay_notify_url + "\">");
        Response.Write("<input type=\"hidden\" name=\"return_url\" value=\"" + alipay_return_url + "\">");
        Response.Write("<input type=\"hidden\" name=\"sign\" value=\"" + alipay_sign + "\">");
        Response.Write("<input type=\"hidden\" name=\"sign_type\" value=\"" + alipay_sign_type + "\">");
        Response.Write("<input type=\"hidden\" name=\"subject\" value=\"" + alipay_subject + "\">");
        Response.Write("<input type=\"hidden\" name=\"show_url\" value=\"" + alipay_show_url + "\">");
        Response.Write("<input type=\"hidden\" name=\"body\" value=\"" + alipay_body + "\">");
        Response.Write("<input type=\"hidden\" name=\"out_trade_no\" value=\"" + alipay_out_trade_no + "\">");
        Response.Write("<input type=\"hidden\" name=\"price\" value=\"" + alipay_price + "\">");
        Response.Write("<input type=\"hidden\" name=\"payment_type\" value=\"1\">");
        Response.Write("<input type=\"hidden\" name=\"quantity\" value=\"1\">");
        Response.Write("<input type=\"hidden\" name=\"logistics_type\" value=\"" + logistics_type + "\">");
        Response.Write("<input type=\"hidden\" name=\"logistics_fee\" value=\"" + logistics_fee + "\">");
        Response.Write("<input type=\"hidden\" name=\"logistics_payment\" value=\"" + logistics_payment + "\">");
        Response.Write("<input type=\"hidden\" name=\"seller_email\" value=\"" + alipay_seller_email + "\">");
        Response.Write("<input type=\"hidden\" name=\"agent\" value=\"isv^gl22\">");
        Response.Write("</form>");
        Response.Write("</body>");
        Response.Write("</html>");

    }

    //双接口/纯担保支付宝支付通知
    public void Pay_Alipay_Notify()
    {
        string alipayNotifyURL = "https://mapi.alipay.com/gateway.do?service=notify_verify";
        string alipay_partner = tools.NullStr(Application["Alipay_Code"]);
        string alipay_seller_email = tools.NullStr(Application["Alipay_Email"]);
        string alipay_key = tools.NullStr(Application["Alipay_Key"]);

        //partner 的对应交易安全校验码（必须填写） 
        alipayNotifyURL = alipayNotifyURL + "&partner=" + alipay_partner + "&notify_id=" + Request.Form["notify_id"];

        //获取支付宝ATN返回结果，true是正确的订单信息，false 是无效的 
        string responseTxt = Get_Http(alipayNotifyURL, 120000);

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
        prestr = prestr + alipay_key;
        string mysign = GetMD5(prestr.ToString(), _input_charset);

        string sign = Request.Form["sign"];
        string Pay_Note = "";
        string v_oid = Request.Form["out_trade_no"];
        string strPrice = Request.Form["total_fee"];
        string strTradeStatus = Request.Form["trade_status"];
        string stralipaytradeno = Request.Form["trade_no"];
        string issuccess = Request.Form["is_success"];
        int pay_status;

        string Pay_Detail = null;
        Pay_Detail = "";
        Pay_Detail += "ALIPAYTRADENO:" + stralipaytradeno + "<BR>";
        Pay_Detail += "ORDERID:" + v_oid + "<BR>";
        Pay_Detail += "AMOUNT:" + strPrice + "<BR>";
        Pay_Detail += "TIME:" + DateTime.Now + "<BR>";
        Pay_Detail += "STATUS:" + strTradeStatus + "<BR>";

        if (mysign == sign & responseTxt == "true")
        {
            //验证支付发过来的消息，签名是否正确 
            if (Request.Form["trade_status"] == "WAIT_BUYER_CONFIRM_GOODS" || Request.Form["trade_status"] == "WAIT_BUYER_PAY")
            {
                Response.Write("success");

            }
            else if (Request.Form["trade_status"] == "WAIT_SELLER_SEND_GOODS" || Request.Form["trade_status"] == "TRADE_FINISHED")
            {
                //订单处理
                int process_result = Pay_Orders_Process(v_oid, "支付宝通知", tools.CheckFloat(strPrice), Pay_Detail);
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

                Pay_Note = "[失败]通过支付宝支付失败,订单状态错误";
                Pay_Log(v_oid, 0, "支付宝", tools.CheckFloat(strPrice), Pay_Note, Pay_Detail);
                Response.Write("error");

            }
        }
        else
        {
            Pay_Note = "[失败]通过支付宝支付失败,验证错误或加密不一致";
            Pay_Log(v_oid, 0, "支付宝", tools.CheckFloat(strPrice), Pay_Note, Pay_Detail);
            Response.Write("error");
        }
    }

    //双接口/纯担保支付宝支付返回
    public void Pay_Alipay_Reviece()
    {
        string alipayNotifyURL = "https://mapi.alipay.com/gateway.do?service=notify_verify";
        string alipay_partner = tools.NullStr(Application["Alipay_Code"]);
        string alipay_seller_email = tools.NullStr(Application["Alipay_Email"]);
        string alipay_key = tools.NullStr(Application["Alipay_Key"]);

        //partner 的对应交易安全校验码（必须填写） 
        alipayNotifyURL = alipayNotifyURL + "&partner=" + alipay_partner + "&notify_id=" + Request["notify_id"];

        //获取支付宝ATN返回结果，true是正确的订单信息，false 是无效的 
        string responseTxt = Get_Http(alipayNotifyURL, 120000);
        //responseTxt = "true";
        int i = 0;
        NameValueCollection coll = default(NameValueCollection);
        coll = Request.QueryString;

        String[] requestarr = coll.AllKeys;
        string _input_charset = "utf-8";


        //进行排序； 
        string[] Sortedstr = BubbleSort(requestarr);



        //构造待md5摘要字符串 ； 
        string prestr = "";
        for (i = 0; i <= Sortedstr.Length - 1; i++)
        {
            if (Request.QueryString[Sortedstr[i]] != "" && Sortedstr[i] != "sign" && Sortedstr[i] != "sign_type" && Sortedstr[i] != "payment")
            {
                if (i == Sortedstr.Length - 1)
                {
                    prestr = (prestr + Sortedstr[i] + "=") + Request.QueryString[Sortedstr[i]];
                }
                else
                {
                    prestr = (prestr + Sortedstr[i] + "=") + Request.QueryString[Sortedstr[i]] + "&";
                }
            }
        }
        prestr = prestr + alipay_key;
        string mysign = GetMD5(prestr.ToString(), _input_charset);

        string sign = Request.QueryString["sign"];
        string Pay_Note = "";
        string v_oid = Request.QueryString["out_trade_no"];
        string strPrice = Request.QueryString["total_fee"];
        string strTradeStatus = Request.QueryString["trade_status"];
        string stralipaytradeno = Request.QueryString["trade_no"];
        string issuccess = Request.QueryString["is_success"];
        int pay_status;

        string Pay_Detail = null;
        Pay_Detail = "";
        Pay_Detail += "ALIPAYTRADENO:" + stralipaytradeno + "<BR>";
        Pay_Detail += "ORDERID:" + v_oid + "<BR>";
        Pay_Detail += "AMOUNT:" + strPrice + "<BR>";
        Pay_Detail += "TIME:" + DateTime.Now + "<BR>";
        Pay_Detail += "STATUS:" + strTradeStatus + "<BR>";
        //Response.Write(prestr.ToString());
        if (mysign == sign & responseTxt == "true")
        {
            //验证支付发过来的消息，签名是否正确 

            if (Request.QueryString["trade_status"] == "WAIT_SELLER_SEND_GOODS" || Request.QueryString["trade_status"] == "TRADE_FINISHED")
            {
                //订单处理
                int process_result = Pay_Orders_Process(v_oid, "支付宝", tools.CheckFloat(strPrice), Pay_Detail);
                if (process_result == 1)
                {
                    Pay_Result("success", v_oid);
                }
                else
                {
                    Pay_Result("failed", "");
                }
            }
            else
            {

                Pay_Note = "[失败]通过支付宝支付失败,订单状态错误";
                Pay_Log(v_oid, 0, "支付宝", tools.CheckFloat(strPrice), Pay_Note, Pay_Detail);
                Pay_Result("failed", "");

            }
        }
        else
        {
            Pay_Note = "[失败]通过支付宝支付失败,验证错误或加密不一致";
            Pay_Log(v_oid, 0, "支付宝", tools.CheckFloat(strPrice), Pay_Note, Pay_Detail);
            Pay_Result("failed", "");
        }
    }

    //即时到账支付发送
    public void Pay_Alipay_Send_Instant(string alipay_notify_url, string alipay_return_url, string alipay_subject, string alipay_show_url, string alipay_body, string alipay_out_trade_no, string alipay_price, string payment_type, string paymethod, string defaultbank, string total_freight)
    {
        string alipay_partner = tools.NullStr(Application["Alipay_Code"]);
        string alipay_seller_email = tools.NullStr(Application["Alipay_Email"]);
        string alipay_key = tools.NullStr(Application["Alipay_Key"]);

        if (alipay_key == "" || alipay_seller_email == "" || alipay_partner == "")
        {
            pub.Msg("error", "错误提示", "暂时不支持支付", false, 3, "{back}");
            return;
        }
        string alipay_sign;
        string alipay_sign_type;
        string notify_url = alipay_notify_url;
        string gateway = "https://mapi.alipay.com/gateway.do?";

        string[] para = {
            //支付双接口
	        "service=create_direct_pay_by_user",
	        "partner=" + alipay_partner,
	        "seller_email=" + alipay_seller_email,
	        "out_trade_no=" + alipay_out_trade_no,
	        "subject=" + alipay_subject,
	        "body=" + alipay_body,
	        "total_fee=" + alipay_price,
	        "show_url=" + alipay_show_url,
	        "payment_type=1",
	        "return_url=" + alipay_return_url,
	        "notify_url=" + alipay_notify_url,
	        "_input_charset=utf-8",
            "agent=isv^gl22"
        };
        alipay_sign_type = "MD5";
        alipay_sign = CreatUrl(para, "utf-8", alipay_sign_type, alipay_key);
        Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
        Response.Write("<head>");
        Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
        Response.Write("<title>PayGate</title>");
        Response.Write("</head>");

        Response.Write("<body onload=\"javascript:document.frm_pay_alipay.submit();\">");
        Response.Write("<form action=\"https://mapi.alipay.com/gateway.do?_input_charset=utf-8\" method=\"POST\" name=\"frm_pay_alipay\">");
        //Response.Write("<form action=\"https://mapi.alipay.com/gateway.do?service=create_direct_pay_by_user&partner=" + alipay_partner + "\" method=\"POST\" name=\"frm_pay_alipay\">");
        //支付双接口
        Response.Write("<input type=\"hidden\" name=\"service\" value=\"create_direct_pay_by_user\">");
        Response.Write("<input type=\"hidden\" name=\"partner\" value=\"" + alipay_partner + "\">");
        Response.Write("<input type=\"hidden\" name=\"notify_url\" value=\"" + alipay_notify_url + "\">");
        Response.Write("<input type=\"hidden\" name=\"return_url\" value=\"" + alipay_return_url + "\">");
        Response.Write("<input type=\"hidden\" name=\"sign\" value=\"" + alipay_sign + "\">");
        Response.Write("<input type=\"hidden\" name=\"sign_type\" value=\"" + alipay_sign_type + "\">");
        Response.Write("<input type=\"hidden\" name=\"subject\" value=\"" + alipay_subject + "\">");
        Response.Write("<input type=\"hidden\" name=\"show_url\" value=\"" + alipay_show_url + "\">");
        Response.Write("<input type=\"hidden\" name=\"body\" value=\"" + alipay_body + "\">");
        Response.Write("<input type=\"hidden\" name=\"out_trade_no\" value=\"" + alipay_out_trade_no + "\">");
        Response.Write("<input type=\"hidden\" name=\"total_fee\" value=\"" + alipay_price + "\">");
        Response.Write("<input type=\"hidden\" name=\"payment_type\" value=\"1\">");
        Response.Write("<input type=\"hidden\" name=\"seller_email\" value=\"" + alipay_seller_email + "\">");
        //Response.Write("<input type=\"hidden\" name=\"defaultbank\" value=\"CCBBTB\">");
        Response.Write("<input type=\"hidden\" name=\"agent\" value=\"isv^gl22\">");
        Response.Write("</form>");
        Response.Write("</body>");
        Response.Write("</html>");
    }

    //支付宝即时到账支付返回
    public void Pay_Alipay_Reviece_Instant()
    {
        string alipayNotifyURL = "https://mapi.alipay.com/gateway.do?service=notify_verify";
        string alipay_partner = tools.NullStr(Application["Alipay_Code"]);
        string alipay_seller_email = tools.NullStr(Application["Alipay_Email"]);
        string alipay_key = tools.NullStr(Application["Alipay_Key"]);

        //partner 的对应交易安全校验码（必须填写） 
        alipayNotifyURL = alipayNotifyURL + "&partner=" + alipay_partner + "&notify_id=" + Request["notify_id"];

        //获取支付宝ATN返回结果，true是正确的订单信息，false 是无效的 
        string responseTxt = Get_Http(alipayNotifyURL, 120000);
        //responseTxt = "true";
        int i = 0;
        NameValueCollection coll = default(NameValueCollection);
        coll = Request.QueryString;

        String[] requestarr = coll.AllKeys;
        string _input_charset = "utf-8";

        //进行排序； 
        string[] Sortedstr = BubbleSort(requestarr);

        //构造待md5摘要字符串 ； 
        string prestr = "";
        for (i = 0; i <= Sortedstr.Length - 1; i++)
        {
            if (Request.QueryString[Sortedstr[i]] != "" && Sortedstr[i] != "sign" && Sortedstr[i] != "sign_type" && Sortedstr[i] != "payment")
            {
                if (i == Sortedstr.Length - 1)
                {
                    prestr = (prestr + Sortedstr[i] + "=") + Request.QueryString[Sortedstr[i]];
                }
                else
                {
                    prestr = (prestr + Sortedstr[i] + "=") + Request.QueryString[Sortedstr[i]] + "&";
                }
            }
        }
        prestr = prestr + alipay_key;
        string mysign = GetMD5(prestr.ToString(), _input_charset);

        string sign = Request.QueryString["sign"];
        string Pay_Note = "";
        string v_oid = tools.NullStr(Request.QueryString["out_trade_no"]);
        string strPrice = tools.NullStr(Request.QueryString["total_fee"]);
        string strTradeStatus = tools.NullStr(Request.QueryString["trade_status"]);
        string stralipaytradeno = tools.NullStr(Request.QueryString["trade_no"]);
        string issuccess = tools.NullStr(Request.QueryString["is_success"]);
        int pay_status;

        string Pay_Detail = null;
        Pay_Detail = "";
        Pay_Detail += "ALIPAYTRADENO:" + stralipaytradeno + "<BR>";
        Pay_Detail += "ORDERID:" + v_oid + "<BR>";
        Pay_Detail += "AMOUNT:" + strPrice + "<BR>";
        Pay_Detail += "TIME:" + DateTime.Now + "<BR>";
        Pay_Detail += "STATUS:" + strTradeStatus + "<BR>";

        //Response.Write(prestr.ToString());
        if (mysign == sign && responseTxt == "true")
        {
            //验证支付发过来的消息，签名是否正确 
            if (Request.QueryString["trade_status"] == "TRADE_FINISHED" || Request.QueryString["trade_status"] == "TRADE_SUCCESS")
            {

                //订单处理
                int process_result = Pay_Orders_Process(v_oid, "支付宝", tools.CheckFloat(strPrice), Pay_Detail);
                if (process_result == 1)
                {
                    Pay_Result("success", v_oid);
                }
                else
                {
                    Pay_Result("failed", "");
                }
            }
            else
            {

                Pay_Note = "[失败]通过支付宝支付失败,订单状态错误";
                Pay_Log(v_oid, 0, "支付宝", tools.CheckFloat(strPrice), Pay_Note, Pay_Detail);
                Pay_Result("failed", "");
            }
        }
        else
        {
            Pay_Note = "[失败]通过支付宝支付失败,验证错误或加密不一致";
            Pay_Log(v_oid, 0, "支付宝", tools.CheckFloat(strPrice), Pay_Note, Pay_Detail);
            Pay_Result("failed", "");
        }
    }

    //支付宝即时到账通知返回
    public void Pay_Alipay_Notify_Instant()
    {
        string alipayNotifyURL = "https://mapi.alipay.com/gateway.do?service=notify_verify";
        string alipay_partner = tools.NullStr(Application["Alipay_Code"]);
        string alipay_seller_email = tools.NullStr(Application["Alipay_Email"]);
        string alipay_key = tools.NullStr(Application["Alipay_Key"]);

        //partner 的对应交易安全校验码（必须填写） 
        alipayNotifyURL = alipayNotifyURL + "&partner=" + alipay_partner + "&notify_id=" + Request.Form["notify_id"];

        //获取支付宝ATN返回结果，true是正确的订单信息，false 是无效的 
        string responseTxt = Get_Http(alipayNotifyURL, 120000);

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
        prestr = prestr + alipay_key;
        string mysign = GetMD5(prestr.ToString(), _input_charset);

        string sign = Request.Form["sign"];
        string Pay_Note = "";
        string v_oid = tools.NullStr(Request.Form["out_trade_no"]);
        string strPrice = tools.NullStr(Request.Form["total_fee"]);
        string strTradeStatus = tools.NullStr(Request.Form["trade_status"]);
        string stralipaytradeno = tools.NullStr(Request.Form["trade_no"]);
        string issuccess = tools.NullStr(Request.Form["is_success"]);
        int pay_status = 0;

        string Pay_Detail = null;
        Pay_Detail = "";
        Pay_Detail += "ALIPAYTRADENO:" + stralipaytradeno + "<BR>";
        Pay_Detail += "ORDERID:" + v_oid + "<BR>";
        Pay_Detail += "AMOUNT:" + strPrice + "<BR>";
        Pay_Detail += "TIME:" + DateTime.Now + "<BR>";
        Pay_Detail += "STATUS:" + strTradeStatus + "<BR>";

        if (mysign == sign && responseTxt == "true")
        {
            //验证支付发过来的消息，签名是否正确 
            if (Request.Form["trade_status"] == "TRADE_FINISHED" || Request.Form["trade_status"] == "TRADE_SUCCESS")
            {
                //订单处理
                int process_result = Pay_Orders_Process(v_oid, "支付宝", tools.CheckFloat(strPrice), Pay_Detail);
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
                Pay_Note = "[失败]通过支付宝支付状态错误";
                Pay_Log(v_oid, 0, "支付宝", tools.CheckFloat(strPrice), Pay_Note, Pay_Detail);
                Response.Write("error");
            }
        }
        else
        {
            Pay_Note = "[失败]通过支付宝支付失败,验证错误或加密不一致";
            Pay_Log(v_oid, 0, "支付宝", tools.CheckFloat(strPrice), Pay_Note, Pay_Detail);
            Response.Write("error");
        }
    }

    #endregion

    #region "财付通支付"

    //财付通即时到账信息发送
    public void Pay_TenPay_Send(string return_url, string desc, string order_sn, double tenpay_price)
    {
        string bargainor_id = tools.NullStr(Application["Tenpay_Code"]);
        string tenpay_key = tools.NullStr(Application["Tenpay_Key"]);

        if (tenpay_key == "" || bargainor_id == "")
        {
            pub.Msg("error", "错误提示", "暂时不支持支付", false, 3, "{back}");
            return;
        }

        //当前时间 yyyyMMdd
        string datestr = DateTime.Now.ToString("yyyyMMdd");

        //生成订单10位序列号，此处用时间和随机数生成，商户根据自己调整，保证唯一
        string strReq = "" + DateTime.Now.ToString("HHmmss") + BuildRandomStr(4);

        //商户订单号，不超过32位，财付通只做记录，不保证唯一性
        string sp_billno = order_sn;

        tenpay_price = tenpay_price * 100;

        //财付通订单号，10位商户号+8位日期+10位序列号，需保证全局唯一
        string transaction_id = bargainor_id + datestr + strReq;

        string attach = "";
        //附言

        string spbill_create_ip = tools.NullStr(Request.ServerVariables["remote_addr"]);

        string sign = Tenpay_CreateSign("1", spbill_create_ip, datestr, bargainor_id, transaction_id, sp_billno, ((int)tenpay_price).ToString(), "1", return_url, attach,
        "utf-8", tenpay_key);

        Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
        Response.Write("<head>");
        Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
        Response.Write("<title>PayGate</title>");
        Response.Write("</head>");

        Response.Write("<body onload=\"javascript:document.frm_pay_tenpay.submit();\">");
        Response.Write("<form action=\"http://service.tenpay.com/cgi-bin/v3.0/payservice.cgi\" method=\"POST\" name=\"frm_pay_tenpay\">");
        Response.Write("<input type=\"hidden\" name=\"spbill_create_ip\" value=\"" + spbill_create_ip + "\">");
        Response.Write("<input type=\"hidden\" name=\"date\" value=\"" + datestr + "\">");
        Response.Write("<input type=\"hidden\" name=\"cs\" value=\"utf-8\">");
        Response.Write("<input type=\"hidden\" name=\"transaction_id\" value=\"" + transaction_id + "\">");
        Response.Write("<input type=\"hidden\" name=\"bargainor_id\" value=\"" + bargainor_id + "\">");
        Response.Write("<input type=\"hidden\" name=\"total_fee\" value=\"" + tenpay_price + "\">");
        Response.Write("<input type=\"hidden\" name=\"desc\" value=\"" + desc + "\">");
        Response.Write("<input type=\"hidden\" name=\"fee_type\" value=\"1\">");
        Response.Write("<input type=\"hidden\" name=\"return_url\" value=\"" + return_url + "\">");
        Response.Write("<input type=\"hidden\" name=\"attach\" value=\"" + attach + "\">");
        Response.Write("<input type=\"hidden\" name=\"bank_type\" value=\"0\">");
        Response.Write("<input type=\"hidden\" name=\"sign\" value=\"" + sign + "\">");
        Response.Write("<input type=\"hidden\" name=\"cmdno\" value=\"1\">");
        Response.Write("<input type=\"hidden\" name=\"sp_billno\" value=\"" + sp_billno + "\">");
        Response.Write("</form>");
        Response.Write("</body>");
        Response.Write("</html>");

    }
    
    //财付通即时到账支付返回
    public void Pay_tenPay_receive()
    {
        string Pay_Note;
        string Pay_Detail;

        string bargainor_id = tools.NullStr(Application["Tenpay_Code"]);
        string tenpay_key = tools.NullStr(Application["Tenpay_Key"]);
        int pay_status = 0;
        if (isTenpaySign("utf-8", tenpay_key))
        {
            //交易单号
            string sp_billno = Request["sp_billno"];
            //财付通交易号
            string transaction_id = Request["transaction_id"];
            //金额金额,以分为单位
            string total_fee = Request["total_fee"];

            double totalprice = (double.Parse(total_fee) / 100);

            //支付结果
            string pay_result = Request["pay_result"];
            Pay_Detail = "财付通交易号：" + transaction_id + "<br/>";
            Pay_Detail += "交易单号：" + sp_billno + "<br/>";
            Pay_Detail += "金额：" + totalprice + "<br/>";
            Pay_Detail += "结果：" + pay_result + "<br/>";
            Pay_Detail += "时间：" + DateTime.Now + "<br/>";

            if (pay_result == "0")
            {
                //订单处理
                int process_result = Pay_Orders_Process(sp_billno, "财付通", totalprice, Pay_Detail);
                if (process_result == 1)
                {
                    Response.Write("<html>");
                    Response.Write("<head>");
                    Response.Write("<meta name=\"TENCENT_ONELINE_PAYMENT\" content=\"China TENCENT\">");

                    Response.Write("<script type=\"text/javascript\">");
                    Response.Write("window.location.href='/pay/pay_result.aspx?payment=TENPAY&payresult=success&orders_sn=" + sp_billno + "';");
                    Response.Write("</script>");

                    Response.Write("</head><body>");
                    Response.Write("</body></html>");
                    Response.End();
                }
                else
                {
                    Response.Write("<html>");
                    Response.Write("<head>");
                    Response.Write("<script type=\"text/javascript\">");
                    Response.Write("window.location.href='/pay/pay_result.aspx?payment=TENPAY&payresult=failed&orders_sn=" + sp_billno + "';");
                    Response.Write("</script>");
                    Response.Write("</head><body></body></html>");
                    Response.End();
                }
                
            }
            else
            {
                //记录支付日志
                Pay_Note = "[失败]通过财付通支付失败";
                Pay_Log(sp_billno, 0, "财付通", totalprice, Pay_Note, Pay_Detail);
                //Pay_Result("failed", "");

                Response.Write("<html>");
                Response.Write("<head>");
                Response.Write("<script type=\"text/javascript\">");
                Response.Write("window.location.href='/pay/pay_result.aspx?payment=TENPAY&payresult=failed&orders_sn=" + sp_billno + "';");
                Response.Write("</script>");
                Response.Write("</head><body></body></html>");
                Response.End();
            }
        }
        else
        {
            //记录支付日志
            Pay_Note = "[失败]通过财付通支付失败";
            Pay_Detail = "[失败]通过财付通支付失败";
            Pay_Log(Request["sp_billno"].ToString(), 0, "财付通", double.Parse(Request["total_fee"].ToString()) / 100, Pay_Note, Pay_Detail);
            //Pay_Result("failed", "");

            Response.Write("<html>");
            Response.Write("<head>");
            Response.Write("<script type=\"text/javascript\">");
            Response.Write("window.location.href='/pay/pay_result.aspx?payment=TENPAY&payresult=failed';");
            Response.Write("</script>");
            Response.Write("</head><body></body></html>");
            Response.End();
        }
    }

    public bool isTenpaySign(string getCharset, string tenpaykey)
    {
        //获取参数
        string cmdno = Request["cmdno"];
        string pay_result = Request["pay_result"];
        string datestr = Request["date"];
        string transaction_id = Request["transaction_id"];
        string sp_billno = Request["sp_billno"];
        string total_fee = Request["total_fee"];
        string fee_type = Request["fee_type"];
        string attach = Request["attach"];
        string tenpaySign = Request["sign"].ToUpper();
        string key = tenpaykey;

        //组织签名串
        StringBuilder sb = new StringBuilder();
        sb.Append("cmdno=" + cmdno + "&");
        sb.Append("pay_result=" + pay_result + "&");
        sb.Append("date=" + datestr + "&");
        sb.Append("transaction_id=" + transaction_id + "&");
        sb.Append("sp_billno=" + sp_billno + "&");
        sb.Append("total_fee=" + total_fee + "&");
        sb.Append("fee_type=" + fee_type + "&");
        sb.Append("attach=" + attach + "&");
        sb.Append("key=" + key);

        string sign = GetMD5(sb.ToString(), getCharset);
        sign = sign.ToUpper();

        return sign.Equals(tenpaySign);
    }

    public static string getRealIp()
    {
        string UserIP;
        if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null) //得到穿过代理服务器的ip地址
        {
            UserIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
        }
        else
        {
            UserIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
        }
        return UserIP;
    }

    #endregion

    #region 快钱证书支付

    public static string CerRSASignature(string OriginalString, string prikey_path, string CertificatePW, int SignType)
    {
        byte[] OriginalByte = System.Text.Encoding.UTF8.GetBytes(OriginalString);
        X509Certificate2 x509_Cer1 = new X509Certificate2(prikey_path, CertificatePW, X509KeyStorageFlags.MachineKeySet);
        RSACryptoServiceProvider rsapri = (RSACryptoServiceProvider)x509_Cer1.PrivateKey;
        RSAPKCS1SignatureFormatter f = new RSAPKCS1SignatureFormatter(rsapri);
        byte[] result;
        switch (SignType)
        {
            case 1:
                f.SetHashAlgorithm("MD5");//摘要算法MD5
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                result = md5.ComputeHash(OriginalByte);//摘要值
                break;
            default:
                f.SetHashAlgorithm("SHA1");//摘要算法SHA1
                SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
                result = sha.ComputeHash(OriginalByte);//摘要值
                break;
        }
        string SignData = System.Convert.ToBase64String(f.CreateSignature(result)).ToString();

        return SignData;
    }

    public void Quickmoney_Send(string orderId, double orderAmount)
    {
        string inputCharset = "1";
        string bgUrl = Application["Site_URL"] + "/pay/pay_notify.aspx?payment=99BILL&";
        string merchantAcctId = tools.NullStr(Application["U_99BILL_Code"]); //商户编号
        string CertificatePW = tools.NullStr(Application["U_99BILL_Key"]);   //商户私钥密钥
        if (merchantAcctId == "" || CertificatePW == "")
        {
            pub.Msg("error", "错误提示", "暂不支持该支付方式", false, 3, "/index.aspx");
            return;
        }

        string prikey_path = Convert.ToString(ConfigurationManager.AppSettings["99BILL_priKeyPath"]);   //商户私钥证书路径

        string payType = "00";//00 14 15-1
        string bankId = "";
        bankId = tools.CheckStr(Request["pay_bank"]);
        if (bankId != "")
        {
            payType = "10";
        }

        orderAmount = Math.Round(orderAmount, 2) * 100;
        string orderTime = DateTime.Now.ToString("yyyyMMddHHmmss");

        string signMsgVal = "";
        signMsgVal = appendParam(signMsgVal, "inputCharset", inputCharset);
        signMsgVal = appendParam(signMsgVal, "pageUrl", "");
        signMsgVal = appendParam(signMsgVal, "bgUrl", bgUrl);
        signMsgVal = appendParam(signMsgVal, "version", "v2.0");
        signMsgVal = appendParam(signMsgVal, "language", "1");
        signMsgVal = appendParam(signMsgVal, "signType", "4");
        signMsgVal = appendParam(signMsgVal, "merchantAcctId", merchantAcctId);
        signMsgVal = appendParam(signMsgVal, "payerName", "");
        signMsgVal = appendParam(signMsgVal, "payerContactType", "1");
        signMsgVal = appendParam(signMsgVal, "payerContact", "");
        signMsgVal = appendParam(signMsgVal, "orderId", orderId);
        signMsgVal = appendParam(signMsgVal, "orderAmount", orderAmount.ToString());
        signMsgVal = appendParam(signMsgVal, "orderTime", orderTime);
        signMsgVal = appendParam(signMsgVal, "productName", "");
        signMsgVal = appendParam(signMsgVal, "productNum", "");
        signMsgVal = appendParam(signMsgVal, "productId", "");
        signMsgVal = appendParam(signMsgVal, "productDesc", "");
        signMsgVal = appendParam(signMsgVal, "ext1", "");
        signMsgVal = appendParam(signMsgVal, "ext2", "");
        signMsgVal = appendParam(signMsgVal, "payType", payType);
        signMsgVal = appendParam(signMsgVal, "bankId", bankId);
        signMsgVal = appendParam(signMsgVal, "redoFlag", "0");
        signMsgVal = appendParam(signMsgVal, "pid", "");

        string signMsg = CerRSASignature(signMsgVal, prikey_path, CertificatePW, 2);

        StringBuilder strHTML = new StringBuilder();
        strHTML.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
        strHTML.Append("<head>");
        strHTML.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
        strHTML.Append("<title>PayGate</title>");
        strHTML.Append("</head>");
        strHTML.Append("<body onload=\"javascript:document.kqPay.submit();\">");
        strHTML.Append("<form name=\"kqPay\" method=\"get\" action=\"https://www.99bill.com/gateway/recvMerchantInfoAction.htm\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"inputCharset\" value=\"" + inputCharset + "\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"bgUrl\"  value=\"" + bgUrl + "\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"pageUrl\"  value=\"\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"version\"  value=\"v2.0\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"language\"  value=\"1\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"signType\"  value=\"4\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"signMsg\"  value=\"" + signMsg + "\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"merchantAcctId\"  value=\"" + merchantAcctId + "\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"payerName\"  value=\"\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"payerContactType\"  value=\"1\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"payerContact\"  value=\"\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"orderId\"  value=\"" + orderId + "\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"orderAmount\"  value=\"" + orderAmount + "\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"orderTime\"  value=\"" + orderTime + "\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"productName\"  value=\"\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"productNum\"  value=\"\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"productId\"  value=\"\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"productDesc\"  value=\"\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"ext1\"  value=\"\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"ext2\"  value=\"\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"payType\"  value=\"" + payType + "\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"bankId\"  value=\"" + bankId + "\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"redoFlag\"  value=\"0\"/>");
        strHTML.Append("	<input type=\"hidden\" name=\"pid\"  value=\"\"/>");
        strHTML.Append("</form>");
        strHTML.Append("</body>");
        strHTML.Append("</html>");

        Response.Write(strHTML.ToString());
        Response.End();
    }

    public void Quickmoney_Receive()
    {
        #region 读取返回提交信息

        string merchantAcctId = Request["merchantAcctId"].ToString().Trim();
        string version = Request["version"].ToString().Trim();
        string language = Request["language"].ToString().Trim();
        string signType = Request["signType"].ToString().Trim();
        string payType = Request["payType"].ToString().Trim();
        string bankId = Request["bankId"].ToString().Trim();
        string orderId = Request["orderId"].ToString().Trim();
        string orderTime = Request["orderTime"].ToString().Trim();
        string orderAmount = Request["orderAmount"].ToString().Trim();
        string dealId = Request["dealId"].ToString().Trim();
        string bankDealId = Request["bankDealId"].ToString().Trim();
        string dealTime = Request["dealTime"].ToString().Trim();
        string payAmount = Request["payAmount"].ToString().Trim();
        string fee = Request["fee"].ToString().Trim();
        string ext1 = Request["ext1"].ToString().Trim();
        string ext2 = Request["ext2"].ToString().Trim();
        string payResult = Request["payResult"].ToString().Trim();
        string errCode = Request["errCode"].ToString().Trim();
        string signMsg = Request["signMsg"].ToString().Trim();

        #endregion

        #region 开始签名

        string merchantSignMsgVal = "";
        merchantSignMsgVal = appendParam(merchantSignMsgVal, "merchantAcctId", merchantAcctId);
        merchantSignMsgVal = appendParam(merchantSignMsgVal, "version", version);
        merchantSignMsgVal = appendParam(merchantSignMsgVal, "language", language);
        merchantSignMsgVal = appendParam(merchantSignMsgVal, "signType", signType);
        merchantSignMsgVal = appendParam(merchantSignMsgVal, "payType", payType);
        merchantSignMsgVal = appendParam(merchantSignMsgVal, "bankId", bankId);
        merchantSignMsgVal = appendParam(merchantSignMsgVal, "orderId", orderId);
        merchantSignMsgVal = appendParam(merchantSignMsgVal, "orderTime", orderTime);
        merchantSignMsgVal = appendParam(merchantSignMsgVal, "orderAmount", orderAmount);
        merchantSignMsgVal = appendParam(merchantSignMsgVal, "dealId", dealId);
        merchantSignMsgVal = appendParam(merchantSignMsgVal, "bankDealId", bankDealId);
        merchantSignMsgVal = appendParam(merchantSignMsgVal, "dealTime", dealTime);
        merchantSignMsgVal = appendParam(merchantSignMsgVal, "payAmount", payAmount);
        merchantSignMsgVal = appendParam(merchantSignMsgVal, "fee", fee);
        merchantSignMsgVal = appendParam(merchantSignMsgVal, "ext1", ext1);
        merchantSignMsgVal = appendParam(merchantSignMsgVal, "ext2", ext2);
        merchantSignMsgVal = appendParam(merchantSignMsgVal, "payResult", payResult);
        merchantSignMsgVal = appendParam(merchantSignMsgVal, "errCode", errCode);

        #endregion

        //转换为需要的订单
        double strPrice = double.Parse(orderAmount) / 100;
        string ordersSN = orderId;

        string Pay_Detail = string.Empty;
        Pay_Detail += "ORDERID:" + ordersSN + "<BR />";
        Pay_Detail += "AMOUNT:" + strPrice + "<BR />";
        Pay_Detail += "TIME:" + DateTime.Now + "<BR />";
        Pay_Detail += "STATUS:" + payResult + "<BR />";
        Pay_Detail += "TRANSDATE:" + orderTime + "<BR />";
        string Pay_Note = string.Empty;

        string pubkey_path = Convert.ToString(ConfigurationManager.AppSettings["99BILL_pubKeyPath"]);   //快钱公钥证书路径
        string CertificatePW = "";  //存放公钥的证书密码
        string redirecturl = Application["Site_URL"] + "/pay/pay_result.aspx?payment=99BILL";

        if (CerRSAVerifySignature(merchantSignMsgVal, signMsg, pubkey_path, CertificatePW, 2))
        {
            switch (payResult)
            {
                case "10":

                    #region 交易成功时处理

                    int ReturnValue = Pay_CheckAmountNoPay(ordersSN, strPrice * 100);
                    if (ReturnValue > 0)
                    {

                        #region 取出订单状态

                        int pay_status;
                        OrdersInfo entity = MyOrders.GetOrdersByID(ReturnValue);
                        if (entity != null)
                        {
                            pay_status = entity.Orders_PaymentStatus;
                            if (pay_status == 0)
                            {
                                entity.Orders_PaymentStatus = 1;
                                entity.Orders_PaymentStatus_Time = DateTime.Now;
                                MyOrders.EditOrders(entity);
                            }
                        }
                        else
                        {
                            pay_status = -1;
                        }
                        entity = null;

                        #endregion

                        #region 订单为未支付时处理

                        if (pay_status == 0)
                        {
                            //添加付款单
                            string Orders_Payment_DocNo = Pay_NewPaymentDocNo();

                            int Orders_Payment_ID = Pay_AddPayment(ReturnValue, 1, 0, Orders_Payment_DocNo, "快钱", strPrice, "通过快钱支付成功");
                            string OrdersLogNote = "";
                            OrdersLogNote = "订单付款，付款单号{order_payment_sn} &nbsp;金额{order_payment_totalprice}";
                            OrdersLogNote = OrdersLogNote.Replace("{order_payment_sn}", "<a href=\"/orderspayment/orders_payment_view.aspx?orders_payment_id=" + Orders_Payment_ID + "\">" + Orders_Payment_DocNo + "</a>");
                            OrdersLogNote = OrdersLogNote.Replace("{order_payment_totalprice}", pub.FormatCurrency(strPrice));

                            Orders_Log(ReturnValue, "", "付款", "成功", OrdersLogNote);
                            //记录支付日志
                            Pay_Note = "[成功]通过快钱支付成功";
                            Pay_Log(ordersSN, 1, "快钱", strPrice, Pay_Note, Pay_Detail);
                        }

                        #endregion

                        Response.Write("<result>1</result><redirecturl>" + redirecturl + "&result=success&orders_sn=" + orderId + "</redirecturl>");
                    }
                    else
                    {
                        //记录支付日志
                        Pay_Note = "[失败]通过快钱支付成功，但找不到该订单或重复支付订单";
                        Pay_Log(ordersSN, 0, "快钱", strPrice, Pay_Note, Pay_Detail);

                        Response.Write("<result>1</result><redirecturl>" + redirecturl + "&result=failed&orders_sn=" + orderId + "</redirecturl>");
                    }

                    #endregion

                    break;
                default:

                    //记录支付日志
                    Pay_Note = "[失败]通过快钱支付失败，状态错误";
                    Pay_Log(ordersSN, 0, "快钱", strPrice, Pay_Note, Pay_Detail);

                    Response.Write("<result>1</result><redirecturl>" + redirecturl + "&result=failed&orders_sn=" + orderId + "</redirecturl>");
                    break;
            }
        }
        else
        {
            //记录支付日志
            Pay_Note = "[失败]通过快钱支付失败，签名错误";
            Pay_Log(ordersSN, 0, "快钱", strPrice, Pay_Note, Pay_Detail);

            Response.Write("<result>1</result><redirecturl>" + redirecturl + "&result=failed&orders_sn=" + orderId + "</redirecturl>");
        }
    }

    public static bool CerRSAVerifySignature(string OriginalString, string SignatureString, string pubkey_path, string CertificatePW, int SignType)
    {
        byte[] OriginalByte = System.Text.Encoding.UTF8.GetBytes(OriginalString);
        byte[] SignatureByte = Convert.FromBase64String(SignatureString);
        X509Certificate2 x509_Cer1 = new X509Certificate2(pubkey_path, CertificatePW, X509KeyStorageFlags.MachineKeySet);
        RSACryptoServiceProvider rsapub = (RSACryptoServiceProvider)x509_Cer1.PublicKey.Key;
        rsapub.ImportCspBlob(rsapub.ExportCspBlob(false));
        RSAPKCS1SignatureDeformatter f = new RSAPKCS1SignatureDeformatter(rsapub);
        byte[] HashData;
        switch (SignType)
        {
            case 1:
                f.SetHashAlgorithm("MD5");//摘要算法MD5
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                HashData = md5.ComputeHash(OriginalByte);
                break;
            default:
                f.SetHashAlgorithm("SHA1");//摘要算法SHA1
                SHA1CryptoServiceProvider sha = new SHA1CryptoServiceProvider();
                HashData = sha.ComputeHash(OriginalByte);
                break;
        }
        if (f.VerifySignature(HashData, SignatureByte))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private string appendParam(String returnStr, String paramId, String paramValue)
    {
        if (returnStr != "")
        {
            if (paramValue != "")
            {
                returnStr += "&" + paramId + "=" + paramValue;
            }
        }
        else
        {
            if (paramValue != "")
            {
                returnStr = paramId + "=" + paramValue;
            }
        }
        return returnStr;
    }

    #endregion

    #region 银联

    public void Pay_ChinaPay_Reciece()
    {
        string MerId = Request["MerId"];//商户号
        string OrdId = Request["OrderNo"];//订单号
        string TransAmt = Request["Amount"];//订单金额
        string CuryId = Request["CurrencyCode"];//货币代码
        string TransDate = Request["TransDate"];//订单日期
        string TransType = Request["TransType"];//交易类型
        string Priv1 = Request["Priv1"];//备注
        string GateId = Request["GateId"];//网关
        string status = Request["status"];

        string CheckValue = Request["checkvalue"];//签名数据   

        //验证该交易应答为ChinaPay所发送
        bool res = mynetpay.check(MerId, OrdId, TransAmt, CuryId, TransDate, TransType, status, CheckValue);

        //转换为需要的订单
        double strPrice = double.Parse(TransAmt) / 100;
        string ordersSN = OrdId.Substring(5);

        string Pay_Detail = string.Empty;
        Pay_Detail += "ORDERID:" + ordersSN + "<BR />";
        Pay_Detail += "AMOUNT:" + strPrice + "<BR />";
        Pay_Detail += "TIME:" + DateTime.Now + "<BR />";
        Pay_Detail += "STATUS:" + status + "<BR />";
        Pay_Detail += "TRANSDATE:" + TransDate + "<BR />";
        string Pay_Note = string.Empty;

        if (res)
        {
            //交易成功
            if (status == "1001")
            {

                #region 交易成功时处理

                int ReturnValue = Pay_CheckAmountNoPay(ordersSN, strPrice * 100);
                if (ReturnValue > 0)
                {

                    #region 取出订单状态

                    int pay_status;
                    OrdersInfo entity = MyOrders.GetOrdersByID(ReturnValue);
                    if (entity != null)
                    {
                        pay_status = entity.Orders_PaymentStatus;
                        if (pay_status == 0)
                        {
                            entity.Orders_PaymentStatus = 1;
                            entity.Orders_PaymentStatus_Time = DateTime.Now;
                            MyOrders.EditOrders(entity);
                        }
                    }
                    else
                    {
                        pay_status = -1;
                    }
                    entity = null;

                    #endregion

                    #region 订单为未支付时处理

                    if (pay_status == 0)
                    {
                        //添加付款单
                        string Orders_Payment_DocNo = Pay_NewPaymentDocNo();

                        int Orders_Payment_ID = Pay_AddPayment(ReturnValue, 1, 0, Orders_Payment_DocNo, "银联", strPrice, "通过银联支付成功");
                        string OrdersLogNote = "";
                        OrdersLogNote = "订单付款，付款单号{order_payment_sn} &nbsp;金额{order_payment_totalprice}";
                        OrdersLogNote = OrdersLogNote.Replace("{order_payment_sn}", "<a href=\"/orderspayment/orders_payment_view.aspx?orders_payment_id=" + Orders_Payment_ID + "\">" + Orders_Payment_DocNo + "</a>");
                        OrdersLogNote = OrdersLogNote.Replace("{order_payment_totalprice}", pub.FormatCurrency(strPrice));

                        Orders_Log(ReturnValue, "", "付款", "成功", OrdersLogNote);
                        //记录支付日志
                        Pay_Note = "[成功]通过银联支付成功";
                        Pay_Log(ordersSN, 1, "银联", strPrice, Pay_Note, Pay_Detail);
                    }

                    #endregion

                    Pay_Result("success", ordersSN);
                }
                else
                {
                    //记录支付日志
                    Pay_Note = "[失败]通过银联支付成功，但找不到该订单或重复支付订单";
                    Pay_Log(ordersSN, 0, "银联", strPrice, Pay_Note, Pay_Detail);
                    Pay_Result("failed", "");
                }

                #endregion

            }
            else
            {
                Pay_Note = "[失败]通过银联支付失败,交易状态错误";
                Pay_Log(ordersSN, 0, "银联", strPrice, Pay_Note, Pay_Detail);
                Pay_Result("failed", "");
            }
        }
        else
        {
            Pay_Note = "[失败]通过银联支付失败,验证签名错误";
            Pay_Log(ordersSN, 0, "银联", strPrice, Pay_Note, Pay_Detail);
            Pay_Result("failed", "");
        }


    }

    public void Pay_ChinaPay_Notify()
    {
        string MerId = Request["MerId"];//商户号
        string OrdId = Request["OrderNo"];//订单号
        string TransAmt = Request["Amount"];//订单金额
        string CuryId = Request["CurrencyCode"];//货币代码
        string TransDate = Request["TransDate"];//订单日期
        string TransType = Request["TransType"];//交易类型
        string Priv1 = Request["Priv1"];//备注
        string GateId = Request["GateId"];//网关
        string status = Request["status"];

        string CheckValue = Request["checkvalue"];//签名数据   

        //验证该交易应答为ChinaPay所发送
        bool res = mynetpay.check(MerId, OrdId, TransAmt, CuryId, TransDate, TransType, status, CheckValue);

        //转换为需要的订单
        double strPrice = double.Parse(TransAmt) / 100;
        string ordersSN = OrdId.Substring(5);

        string Pay_Detail = string.Empty;
        Pay_Detail += "ORDERID:" + ordersSN + "<BR />";
        Pay_Detail += "AMOUNT:" + strPrice + "<BR />";
        Pay_Detail += "TIME:" + DateTime.Now + "<BR />";
        Pay_Detail += "STATUS:" + status + "<BR />";
        Pay_Detail += "TRANSDATE:" + TransDate + "<BR />";
        string Pay_Note = string.Empty;

        if (res)
        {
            //交易成功
            if (status == "1001")
            {

                #region 交易成功时处理

                int ReturnValue = Pay_CheckAmountNoPay(ordersSN, strPrice * 100);
                if (ReturnValue > 0)
                {

                    #region 取出订单状态

                    int pay_status;
                    OrdersInfo entity = MyOrders.GetOrdersByID(ReturnValue);
                    if (entity != null)
                    {
                        pay_status = entity.Orders_PaymentStatus;
                        if (pay_status == 0)
                        {
                            entity.Orders_PaymentStatus = 1;
                            entity.Orders_PaymentStatus_Time = DateTime.Now;
                            MyOrders.EditOrders(entity);
                        }
                    }
                    else
                    {
                        pay_status = -1;
                    }
                    entity = null;

                    #endregion

                    #region 订单为未支付时处理

                    if (pay_status == 0)
                    {
                        //添加付款单
                        string Orders_Payment_DocNo = Pay_NewPaymentDocNo();

                        int Orders_Payment_ID = Pay_AddPayment(ReturnValue, 1, 0, Orders_Payment_DocNo, "银联", strPrice, "通过银联支付成功");
                        string OrdersLogNote = "";
                        OrdersLogNote = "订单付款，付款单号{order_payment_sn} &nbsp;金额{order_payment_totalprice}";
                        OrdersLogNote = OrdersLogNote.Replace("{order_payment_sn}", "<a href=\"/orderspayment/orders_payment_view.aspx?orders_payment_id=" + Orders_Payment_ID + "\">" + Orders_Payment_DocNo + "</a>");
                        OrdersLogNote = OrdersLogNote.Replace("{order_payment_totalprice}", pub.FormatCurrency(strPrice));

                        Orders_Log(ReturnValue, "", "付款", "成功", OrdersLogNote);
                        //记录支付日志
                        Pay_Note = "[成功]通过银联支付成功";
                        Pay_Log(ordersSN, 1, "银联", strPrice, Pay_Note, Pay_Detail);
                    }

                    #endregion

                    Response.Write("success");
                }
                else
                {
                    //记录支付日志
                    Pay_Note = "[失败]通过银联支付成功，但找不到该订单或重复支付订单";
                    Pay_Log(ordersSN, 0, "银联", strPrice, Pay_Note, Pay_Detail);
                    Response.Write("failed");
                }

                #endregion

            }
            else
            {
                Pay_Note = "[失败]通过银联支付失败,交易状态错误";
                Pay_Log(ordersSN, 0, "银联", strPrice, Pay_Note, Pay_Detail);
                Response.Write("failed");
            }
        }
        else
        {
            Pay_Note = "[失败]通过银联支付失败,验证签名错误";
            Pay_Log(ordersSN, 0, "银联", strPrice, Pay_Note, Pay_Detail);
            Response.Write("failed");
        }


    }

    #endregion

    #region 中行支付

    /// <summary>
    /// 中行支付
    /// </summary>
    public void BOC_Send(string OrdersSN, string OrdersAmount)
    {
        //string merchantNo = ConfigurationManager.AppSettings["merchantNo"].ToString();
        //string TransDate = DateTime.Now.ToString("yyyyMMddHHmmss");
        //string orderUrl = Application["Site_URL"] + "/pay/pay_result.aspx?payment=BOC";
        //OrdersAmount = double.Parse(OrdersAmount).ToString("0.00");

        //string plaindata = OrdersSN + "|" + TransDate + "|001|" + OrdersAmount + "|" + merchantNo;
        //byte[] dataTobeSign = System.Text.Encoding.UTF8.GetBytes(plaindata);
        //string signData = P7Verify_CSharp.PKCS7Tool.SignatureMessage(ConfigurationManager.AppSettings["certFileName"].ToString(), ConfigurationManager.AppSettings["certPassword"].ToString(), dataTobeSign);

        //Response.Write("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
        //Response.Write("<head>");
        //Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
        //Response.Write("<title>PayGate</title>");
        //Response.Write("</head>");
        //Response.Write("<body onload=\"javascript:document.frm_pay_netpay.submit();\">");
        //Response.Write("<form action=\"https://180.168.146.75:81/PGWPortal/RecvOrder.do\" method=\"POST\" name=\"frm_pay_netpay\">");

        //Response.Write("<input type=\"hidden\" name=\"merchantNo\" value=\"" + merchantNo + "\" />");
        //Response.Write("<input type=\"hidden\" name=\"payType\" value=\"1\" />");
        //Response.Write("<input type=\"hidden\" name=\"orderNo\" value=\"" + OrdersSN + "\" />");
        //Response.Write("<input type=\"hidden\" name=\"curCode\" value=\"001\" />");
        //Response.Write("<input type=\"hidden\" name=\"orderAmount\" value=\"" + OrdersAmount + "\" />");
        //Response.Write("<input type=\"hidden\" name=\"orderTime\" value=\"" + TransDate + "\" />");
        //Response.Write("<input type=\"hidden\" name=\"orderNote\" value=\"易种购物\" />");
        //Response.Write("<input type=\"hidden\" name=\"orderUrl\" value=\"" + orderUrl + "\" />");
        //Response.Write("<input type=\"hidden\" name=\"dn\" value=\"CN=测试商户, OU=TEST, O=BANK OF CHINA, C=CN\" />");
        //Response.Write("<input type=\"hidden\" name=\"plaindata\" value=\"" + plaindata + "\" />");
        //Response.Write("<textarea name=\"signData\" style=\"display:none;\" />" + signData + "</textarea>");
        //Response.Write("</form>");
        //Response.Write("</body>");
        //Response.Write("</html>");
    }

    /// <summary>
    /// 中行返回处理
    /// </summary>
    public void BOC_Reviece()
    {
        //string merchantNo = ConfigurationManager.AppSettings["merchantNo"].ToString();
        //string orderNo = Request["orderNo"];
        //string orderSeq = Request["orderSeq"];
        //string cardTyp = Request["cardTyp"];
        //string payTime = Request["payTime"];
        //string orderStatus = Request["orderStatus"];
        //string payAmount = Request["payAmount"];
        //string orderIp = Request["orderIp"];
        //string orderRefer = Request["orderRefer"];
        //string bankTranSeq = Request["bankTranSeq"];
        //string returnActFlag = Request["returnActFlag"];
        //string signData = Request["signData"];

        //double strPrice = double.Parse(payAmount);
        //string ordersSN = orderNo;

        //string Pay_Detail = string.Empty;
        //Pay_Detail += "ORDERID:" + ordersSN + "<BR />";
        //Pay_Detail += "AMOUNT:" + strPrice + "<BR />";
        //Pay_Detail += "TIME:" + DateTime.Now + "<BR />";
        //Pay_Detail += "STATUS:" + orderStatus + "<BR />";
        //Pay_Detail += "TRANSDATE:" + payTime + "<BR />";
        //string Pay_Note = string.Empty;

        //byte[] dataTobeSign = System.Text.Encoding.UTF8.GetBytes(merchantNo + "|" + orderNo + "|"
        //    + orderSeq + "|" + cardTyp + "|" + payTime + "|" + orderStatus + "|" + payAmount);

        //string dn = "CN=测试商户, OU=TEST, O=BANK OF CHINA, C=CN";

        //if (!P7Verify_CSharp.PKCS7Tool.Verify(Encoding.UTF8.GetBytes(signData), dataTobeSign, dn))
        //{
        //    Pay_Note = "[失败]通过中行支付失败,验签错误";
        //    Pay_Log(ordersSN, 0, "中行", strPrice, Pay_Note, Pay_Detail);
        //    Pay_Result("failed", "");
        //}

        //if (orderStatus != "1")
        //{
        //    Pay_Note = "[失败]通过中行支付失败,状态错误";
        //    Pay_Log(ordersSN, 0, "中行", strPrice, Pay_Note, Pay_Detail);
        //    Pay_Result("failed", "");
        //}

        //int process_result = Pay_Orders_Process(ordersSN, "中行", strPrice, Pay_Detail);
        //if (process_result == 1)
        //{
        //    Pay_Result("success", orderNo);
        //}
        //else
        //{
        //    Pay_Result("failed", "");
        //}

    }

    #endregion 


    #endregion

    #region "业务处理函数"

    //支付完成后订单处理
    public int Pay_Orders_Process(string Orders_SN, string Payment_Name, double Payment_Price, string Pay_Detail)
    {
        //0为失败，1为成功
        int Process_Result=0;
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
               
                OrdersLogNote = "订单付款，付款单号{order_payment_sn} &nbsp;金额{order_payment_totalprice}";
                OrdersLogNote = OrdersLogNote.Replace("{order_payment_sn}", "<a href=\"/orderspayment/orders_payment_view.aspx?orders_payment_id=" + Orders_Payment_ID + "\">" + Orders_Payment_DocNo + "</a>");
                OrdersLogNote = OrdersLogNote.Replace("{order_payment_totalprice}", pub.FormatCurrency(Payment_Price));
                
                //订单日志添加
                Orders_Log(Orders_ID, "", "付款", "成功", OrdersLogNote);
                
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
                        case "CHINABANK":
                            Pay_Chinabank_Send(v_mid, v_key, v_url, orders_sn, v_amount, "CNY");
                            break;
                        case "BCOM":
                            //Response.Redirect("https://pbank.95559.com.cn/personbank/common_logon.jsp");
                            Pay_Bocomm_Send(orders_id, orders_sn, v_amount, v_url, Orders_Addtime, Orders_Delivery_Name, "CNY");
                            break;
                        case "ALIPAY":
                            string alipay_out_trade_no = orders_sn;
                            string alipay_return_url = Application["Site_URL"] + "/pay/pay_result.aspx?payment=" + pay_payment;
                            string alipay_show_url = Application["Site_URL"] + "/member/order_detail.aspx?orders_sn=" + orders_sn;
                            string alipay_body = "点击上面的商品名称链接查看订单详情";
                            string paymethod = "directPay";
                            string alipay_notify_url = Application["Site_URL"] + "/pay/Pay_Notify.aspx?payment=" + pay_payment;
                            Pay_Alipay_Send_Instant(alipay_notify_url, alipay_return_url, "订单:" + orders_sn, alipay_show_url, alipay_body, orders_sn, v_amount.ToString(), "1", paymethod, "", total_freight.ToString());
                            break;
                        case "ALIPAY_PNT":
                            alipay_out_trade_no = orders_sn;
                            alipay_return_url = Application["Site_URL"] + "/pay/pay_result.aspx?payment=" + pay_payment;
                            alipay_show_url = Application["Site_URL"] + "/member/order_detail.aspx?orders_sn=" + orders_sn;
                            alipay_body = "点击上面的商品名称链接查看订单详情";
                            paymethod = "TradePay";
                            alipay_notify_url = Application["Site_URL"] + "/pay/Pay_Notify.aspx?payment=alipay_pnt";
                            Pay_Alipay_Agent_Send(alipay_notify_url, alipay_return_url, "订单:" + orders_sn, alipay_show_url, alipay_body, orders_sn, v_amount.ToString(), "1", paymethod, "", total_freight.ToString());
                            break;
                        case "ALIPAY_DOU":
                            alipay_out_trade_no = orders_sn;
                            alipay_return_url = Application["Site_URL"] + "/pay/pay_result.aspx?payment=" + pay_payment;
                            alipay_show_url = Application["Site_URL"] + "/member/order_detail.aspx?orders_sn=" + orders_sn;
                            alipay_body = "点击上面的商品名称链接查看订单详情";
                            paymethod = "PartnerPay";
                            alipay_notify_url = Application["Site_URL"] + "/pay/Pay_Notify.aspx?payment=alipay_pnt";
                            Pay_Alipay_Send(alipay_notify_url, alipay_return_url, "订单:" + orders_sn, alipay_show_url, alipay_body, orders_sn, v_amount.ToString(), "1", paymethod, "", total_freight.ToString());
                            break;
                        case "TENPAY":
                            string return_url = Application["Site_URL"] + "/pay/Pay_Notify.aspx?payment=" + pay_payment;
                            string show_url = Application["Site_URL"] + "/member/order_detail.aspx?orders_sn=" + orders_sn;
                            string tenpay_body = "订单号：" + orders_sn;
                            Pay_TenPay_Send(return_url, tenpay_body, orders_sn, v_amount);
                            break;
                        case "99BILL":
                            Quickmoney_Send(orders_sn, v_amount);
                            break;
                        case "CHINAPAY":
                            NetPayClient NetPay = new NetPayClient();
                            NetPay.Pay_NetPay_Send(orders_sn, v_amount.ToString(), "");
                            break;
                    }
                    break;
            }
        }

    }

    //支付结果
    public void Pay_Result(string result, string orders_sn)
    {
        if(result=="success")
        {
            Response.Write("<table width=\"980\" border=\"0\" align=\"center\" cellpadding=\"6\" cellspacing=\"0\" class=\"tip_bg_positive\">");
            Response.Write("  <tr>");
            Response.Write("    <td><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\">");
            Response.Write("      <tr>");
            Response.Write("        <td width=\"100\" align=\"center\" valign=\"middle\"><img src=\"/images/info_success_48.gif\"></td>");
            Response.Write("       <td align=\"left\" valign=\"top\">");
            Response.Write("       <h2 class=\"t14_green\">支付成功</h2>");
            Response.Write("       您已经成功支付！点这里");
            if (orders_sn=="") {
                Response.Write("       <input name=\"btnview\" type=\"button\" class=\"buttonupload\" style=\" cursor:pointer;\" id=\"btnview\" value=\"查看订单\" onclick=\"location.href='/member/order_all.aspx';\"/>");
            } else {
                Response.Write("       <input name=\"btnview\" type=\"button\" class=\"buttonupload\" style=\" cursor:pointer;\" id=\"btnview\" value=\"查看订单\" onclick=\"location.href='/member/order_detail.aspx?orders_sn=" + orders_sn + "';\"/>");
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

    //检查支付金额与订单金额是否相等
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
    public int Pay_AddPayment(int OrdersID, int PaymentStatus, int SysUserID , string DocNo, string Name, double Amount, string Note)
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
