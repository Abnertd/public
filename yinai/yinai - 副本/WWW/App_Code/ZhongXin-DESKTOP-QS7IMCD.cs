using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.XPath;

using Glaer.Trade.Util.Tools;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.Util.SQLHelper;

/// <summary>
///  中信银行支付配置
/// </summary>
public class ZhongXin
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    ITools tools;
    IZhongXin MyBLL;
    ISQLHelper DBHelper;
    ISupplier MySupplier;

    Public_Class pub;
    ZhongXinUtil.SendMessages sendmessages;

    int Supplier_ID;
    string CommissionAccNo;
    string CommissionAccNm;
    string GuaranteeAccNo;
    string GuaranteeAccNm;

    public ZhongXin()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = ZhongXinFactory.Create();
        MySupplier = SupplierFactory.CreateSupplier();
        DBHelper = SQLHelperFactory.CreateSQLHelper();

        pub = new Public_Class();
        sendmessages = new ZhongXinUtil.SendMessages();

        //Supplier_ID = tools.CheckInt(Session["supplier_id"].ToString());
        //佣金
        CommissionAccNo = System.Configuration.ConfigurationManager.AppSettings["zhongxin_commissionaccno"];
        CommissionAccNm = System.Configuration.ConfigurationManager.AppSettings["zhongxin_commissionaccnm"];
        //交易保证金
        GuaranteeAccNo = System.Configuration.ConfigurationManager.AppSettings["zhongxin_dealguaranteeaccno"];
        GuaranteeAccNm = System.Configuration.ConfigurationManager.AppSettings["zhongxin_dealguaranteeaccnm"];
    }

    public ZhongXinInfo GetZhongXinBySuppleir(int ID)
    {
        ZhongXinInfo zhongxininfo = MyBLL.GetZhongXinBySuppleir(ID);
        //if (zhongxininfo.BankCode == null || zhongxininfo.BankName == null || zhongxininfo.SubAccount == null)
        //{
        //    pub.Msg("error", "错误信息", "请维护出金账户信息", false, "/supplier/index.aspx");
        //}
        return zhongxininfo;
    }

    public void AddZhonghang()
    {
        Supplier_ID = tools.CheckInt(Session["supplier_id"].ToString());
        ZhongXinInfo zhongxininfo = GetZhongXinBySuppleir(Supplier_ID);
        if (zhongxininfo == null)
        {
            pub.Msg("info", "提示信息", "系统繁忙，若多次出现请联系管理员", false, "{back}");
        }

        SupplierInfo entitys = MySupplier.GetSupplierByID(Supplier_ID, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
        if (entitys != null)
        {
            string CompanyName = tools.CheckStr(Request.Form["CompanyName"]);
            string ReceiptAccount = tools.CheckStr(Request.Form["ReceiptAccount"]);
            string ReceiptBank = tools.CheckStr(Request.Form["ReceiptBank"]);
            string BankCode = tools.CheckStr(Request.Form["BankCode"]);
            string BankName = tools.CheckStr(Request.Form["BankName"]);
            string OpenAccountName = tools.CheckStr(Request.Form["OpenAccountName"]);

            if (CompanyName == "")
            {
                pub.Msg("info", "提示信息", "填写公司名称", false, "{back}");
            }

            if (ReceiptAccount == "")
            {
                pub.Msg("info", "提示信息", "填写出金收款账号须真实有效", false, "{back}");
            }

            if (ReceiptBank == "")
            {
                pub.Msg("info", "提示信息", "填写出金收款银行", false, "{back}");
            }

            if (BankCode == "")
            {
                pub.Msg("info", "提示信息", "填写银行行号", false, "{back}");
            }

            if (BankName == "")
            {
                pub.Msg("info", "提示信息", "填写银行名称", false, "{back}");
            }

            if (OpenAccountName == "")
            {
                pub.Msg("info", "提示信息", "填写开户名称", false, "{back}");
            }

            ZhongXinInfo entity = MyBLL.GetZhongXinBySuppleir(Supplier_ID);

            entity.ReceiptAccount = ReceiptAccount;
            entity.ReceiptBank = ReceiptBank;
            entity.BankCode = BankCode;
            entity.BankName = BankName;
            entity.Audit = 0;
            entity.Register = 0;

            if (MyBLL.EditZhongXin(entity))
            {
                pub.Msg("positive", "操作成功", "操作成功", true, "/supplier/ZhongXin.aspx?tip=success");
            }
            else
            {
                pub.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            pub.Msg("error", "错误信息", "信息保存失败，请稍后再试！", false, "{back}");
        }

    }

    /// <summary>
    /// 转账
    /// </summary>
    /// <param name="PayAccountID"></param>
    /// <param name="RecvAccount"></param>
    /// <param name="tranAmt"></param>
    /// <param name="Notes"></param>
    /// <returns></returns>
    public string Transfer(int PayAccountID, int RecvAccount, double tranAmt, string Notes)
    {
        string supplier_name = "";
        ZhongXinInfo PayAccountInfo = GetZhongXinBySuppleir(PayAccountID);
        ZhongXinInfo RecvAccountInfo = GetZhongXinBySuppleir(RecvAccount);
        if (PayAccountInfo == null) return "记录不存在";
        if (RecvAccountInfo == null) return "记录不存在";

        string strResult = string.Empty;
        if (sendmessages.Transfer(PayAccountInfo.SubAccount, RecvAccountInfo.SubAccount, RecvAccountInfo.CompanyName, Notes, tranAmt, ref strResult, supplier_name))
        {
            return "true";
        }
        else
        {
            return strResult;
        }

    }

    /// <summary>
    /// 提现
    /// </summary>
    /// <param name="WithdrawAccountID"></param>
    /// <param name="tranAmt"></param>
    /// <param name="Notes"></param>
    /// <returns></returns>
    public void Withdraw()
    {
        decimal tranAmt = (decimal)tools.CheckFloat(Request.Form["Withdraw"]);
        string Notes = "自助提交出金申请";

        ZhongXinInfo PayAccountInfo = GetZhongXinBySuppleir(tools.NullInt(Session["supplier_id"]));
        if (PayAccountInfo == null)
        {
            pub.Msg("error", "错误信息", "您尚未绑定中信支付", false, "{back}");
        }

        decimal accountAmount = GetAmount(PayAccountInfo.SubAccount);
        decimal withdrawFee = WithdrawFee(tranAmt, PayAccountInfo.ReceiptBank);
        if (accountAmount < (withdrawFee + tranAmt))
        {
            pub.Msg("error", "错误信息", "本次出金金额应小于账户剩余金额", false, "{back}");
        }

        string strResult = string.Empty;
        if (sendmessages.Withdraw(PayAccountInfo.SubAccount, PayAccountInfo.ReceiptAccount, PayAccountInfo.OpenAccountName, PayAccountInfo.BankCode, PayAccountInfo.BankName, tranAmt, PayAccountInfo.ReceiptBank, Notes, ref strResult))
        {
            pub.Msg("positive", "操作成功", "您的出金申请已经提交，具体银行到账时间可能会有延迟，下午16点以后的出金申请将于次日处理，请耐心等待，不要重复提交出金。", true, "/supplier/zhongxin.aspx");
        }
        else
        {
            pub.Msg("error", "错误信息", "您的出金申请提交失败，请核实您的出金收款银行账号、联行号、出金银行名称是否正确。", false, "{back}");
            //pub.Msg("error", "错误信息", strResult, false, "{back}");
        }

    }

    /// <summary>
    /// 扣除佣金
    /// </summary>
    /// <param name="PayAccountID"></param>
    /// <param name="tranAmt"></param>
    /// <param name="Notes"></param>
    /// <returns></returns>
    public string DeductingCommission(int RecvAccountID, double tranAmt, string Notes)
    {
        string supplier_name = "";
        ZhongXinInfo RecvAccountInfo = GetZhongXinBySuppleir(RecvAccountID);
        if (RecvAccountInfo == null) return "记录不存在";

        string strResult = string.Empty;
        if (sendmessages.Transfer(RecvAccountInfo.SubAccount, CommissionAccNo, CommissionAccNm, Notes, tranAmt, ref strResult, supplier_name))
        {
            return "true";
        }
        else
        {
            return strResult;
        }

    }

    /// <summary>
    /// 转账到担保账户
    /// </summary>
    /// <param name="PayAccountID"></param>
    /// <param name="tranAmt"></param>
    /// <param name="Notes"></param>
    /// <returns></returns>
    public string ToGuaranteeAccount(int PayAccountID, double tranAmt, string Notes, int Supplier_ID, string supplier_name)
    {
        ZhongXinInfo PayAccountInfo = GetZhongXinBySuppleir(Supplier_ID);
        if (PayAccountInfo == null) return "记录不存在";

        string strResult = string.Empty;
        if (sendmessages.Transfer(PayAccountInfo.SubAccount, GuaranteeAccNo, GuaranteeAccNm, Notes, tranAmt, ref strResult, supplier_name))
        {
            return "true";
        }
        else
        {
            return strResult;
        }

    }
    /// <summary>
    /// 设置初始起始资金
    /// </summary>
    /// <param name="PayAccountID"></param>
    /// <param name="tranAmt"></param>
    /// <param name="Notes"></param>
    /// <returns></returns>
    public string ToGuaranteeAccount1(int member_id)
    {
        //ZhongXinInfo PayAccountInfo = GetZhongXinBySuppleir(Supplier_ID);
        //if (PayAccountInfo == null) return "记录不存在";

        string strResult = string.Empty;
        if (sendmessages.Transfer1(member_id))
        {
            return "true";
        }
        else
        {
            return strResult;
        }

    }


    /// <summary>
    /// 转账出担保账户
    /// </summary>
    /// <param name="RecvAccountID"></param>
    /// <param name="tranAmt"></param>
    /// <param name="Notes"></param>
    /// <returns></returns>
    public string OutGuaranteeAccount(int RecvAccountID, double tranAmt, string Notes)
    {
        string supplier_name = "";
        ZhongXinInfo RecvAccountInfo = GetZhongXinBySuppleir(RecvAccountID);
        if (RecvAccountInfo == null) return "记录不存在";

        string strResult = string.Empty;
        if (sendmessages.Transfer(GuaranteeAccNo, RecvAccountInfo.SubAccount, RecvAccountInfo.CompanyName, Notes, tranAmt, ref strResult, supplier_name))
        {
            return "true";
        }
        else
        {
            return strResult;
        }

    }

    /// <summary>
    /// 签收时转账操作
    /// </summary>
    /// <param name="Contract_BuyerID">合同购买人</param>
    /// <param name="Contract_SupplierID">合同销售人</param>
    /// <param name="PaymentPrice">对应本次发货的付款单金额</param>
    /// <param name="AcceptPrice">本次签收金额</param>
    /// <param name="CommissionPrice">佣金</param>
    /// <param name="Notes">备注</param>
    /// <param name="DeliveryDocNo">发货单号</param>
    /// <param name="ContractSN">合同单号</param>PaymentDocNo
    /// <param name="PaymentDocNo">付款单号</param>
    /// <returns></returns>
    public string ContractDeliveryAccept(int Contract_BuyerID, int Contract_SupplierID, double GuarantPrice, double AcceptPrice, double CommissionPrice, string Notes, string DeliveryDocNo, string ContractSN)
    {
        //if (tools.NullInt(DBHelper.ExecuteScalar("select IsRefund from Contract_Payment where Contract_Payment_DocNo='" + PaymentDocNo + "'")) != 0)
        //{
        //    return "false";
        //}
        string strResult = "";
        if (GuarantPrice > 0)
        {
            if (GuarantPrice > 0)
            {
                strResult = OutGuaranteeAccount(Contract_BuyerID, GuarantPrice, "[转出到原账户]" + Notes);
            }
        }
        if ((strResult == "true" && GuarantPrice > 0) || GuarantPrice == 0)
        {

            IList<ZhongXinBreakInfo> entitys = new List<ZhongXinBreakInfo>();
            //按照签收金额转账到卖家
            string strResultsub = Transfer(Contract_BuyerID, Contract_SupplierID, GuarantPrice + AcceptPrice, "[转账到卖家]" + Notes);
            if (strResultsub == "true")
            {
                //扣除佣金
                strResultsub = DeductingCommission(Contract_SupplierID, CommissionPrice, "[扣佣金]" + Notes);
                if (strResultsub == "true")
                {
                }
                else
                {
                    #region 记录

                    entitys.Add(new ZhongXinBreakInfo()
                    {
                        ContractSN = ContractSN,
                        DeliveryDocNo = DeliveryDocNo,
                        NodeName = "扣除佣金",
                        PaymentPrice = AcceptPrice,
                        AcceptPrice = GuarantPrice + AcceptPrice,
                        CommissionPrice = CommissionPrice,
                        Notes = "[扣佣金]" + Notes,
                        Addtime = DateTime.Now
                    });

                    #endregion
                }
            }
            else
            {
                #region 记录

                entitys.Add(new ZhongXinBreakInfo()
                {
                    ContractSN = ContractSN,
                    DeliveryDocNo = DeliveryDocNo,
                    NodeName = "转账到卖家",
                    PaymentPrice = AcceptPrice,
                    AcceptPrice = GuarantPrice + AcceptPrice,
                    CommissionPrice = CommissionPrice,
                    Notes = "[转账到卖家]" + Notes,
                    Addtime = DateTime.Now
                });

                #endregion
            }
            //记录到中断操作
            AddBreaks(entitys);
        }
        else
        {
            if (GuarantPrice > 0)
            {
                //SaveZhongXinAccountLog(Contract_BuyerID, AcceptPrice, "[撤销][转出到原账户]" + Notes);
            }
        }
        return strResult;

    }

    /// <summary>
    /// 签收时中信请求错误节点
    /// </summary>
    /// <param name="entitys"></param>
    public void AddBreaks(IList<ZhongXinBreakInfo> entitys)
    {
        string SqlAdd = null;
        DataTable DtAdd = null;
        DataRow DrAdd = null;
        SqlAdd = "SELECT TOP 0 * FROM ZhongXin_Break";
        DtAdd = DBHelper.Query(SqlAdd);

        foreach (ZhongXinBreakInfo entity in entitys)
        {
            DrAdd = DtAdd.NewRow();
            DrAdd["ContractSN"] = entity.ContractSN;
            DrAdd["DeliveryDocNo"] = entity.DeliveryDocNo;
            DrAdd["NodeName"] = entity.NodeName;
            DrAdd["PaymentPrice"] = entity.PaymentPrice;
            DrAdd["AcceptPrice"] = entity.AcceptPrice;
            DrAdd["CommissionPrice"] = entity.CommissionPrice;
            DrAdd["Notes"] = entity.Notes;
            DrAdd["Addtime"] = entity.Addtime;
            DtAdd.Rows.Add(DrAdd);
        }
        try
        {
            DBHelper.SaveChanges(SqlAdd, DtAdd);
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

    /// <summary>
    /// 获取账户金额
    /// </summary>
    /// <param name="subAccNo">附属子账号</param>
    /// <returns></returns>
    public decimal GetAmount(string subAccNo)
    {
        return sendmessages.GetAmount(subAccNo);
    }

    /// <summary>
    /// 账户明细
    /// </summary>
    /// <param name="subAccNo"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    public void Account_List(string subAccNo, DateTime startDate, DateTime endDate)
    {
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        Pageurl = "?action=list&date_start=" + startDate.ToString("yyyy-MM-dd") + "&date_end=" + endDate.ToString("yyyy-MM-dd");
        if (curpage < 1)
        {
            curpage = 1;
        }
        //Response.Write("<div  class=\"blk05_sz\" >");
        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <td width=\"130\"align=\"center\" valign=\"middle\">交易时间</th>");
        Response.Write("  <td width=\"80\" align=\"center\" valign=\"middle\">借贷标志</th>");
        Response.Write("  <td width=\"80\" align=\"center\" valign=\"middle\">交易金额</th>");
        Response.Write("  <td width=\"80\" align=\"center\" valign=\"middle\">账户余额</th>");
        Response.Write("  <td align=\"center\" valign=\"middle\">对方账户名称</th>");
        Response.Write("  <td width=\"136\" align=\"center\" valign=\"middle\">打印校验码</th>");
        Response.Write("  <td align=\"center\" valign=\"middle\">摘要</th>");
        Response.Write("</tr>");

        int startRecord = (curpage - 1) * 10 + 1;
        string strXml = strXml = sendmessages.AccountDetail(subAccNo, startDate.ToString("yyyyMMdd"), endDate.ToString("yyyyMMdd"), startRecord);
        int icount = 0;
        if (strXml.Length > 0)
        {
            XPathDocument xmlDoc = new XPathDocument(new StringReader(strXml));
            XPathNavigator navigator = xmlDoc.CreateNavigator();
            XPathNodeIterator nodes = navigator.Select("/stream/list[@name='userDataList']/row");
            if (nodes != null)
            {
                while (nodes.MoveNext())
                {
                    icount++;
                    Response.Write("<tr bgcolor=\"#ffffff\">");
                    Response.Write("<td height=\"35\" align=\"left\" class=\"comment_td_bg\" style=\"padding:0px 5px;text-align:center;\" valign=\"middle\">" + DateTime.ParseExact(nodes.Current.SelectSingleNode("tranDate").InnerXml + nodes.Current.SelectSingleNode("tranTime").InnerXml, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture).ToString("yyyy-MM-dd HH:mm:ss") + "</td>");
                    Response.Write("<td height=\"35\" class=\"comment_td_bg\" style=\"padding:0px 5px;\" >" + (nodes.Current.SelectSingleNode("loanFlag").InnerXml == "D" ? "借" : "贷") + "</td>");
                    Response.Write("<td height=\"35\" class=\"comment_td_bg\" style=\"padding:0px 5px;\" >" + nodes.Current.SelectSingleNode("tranAmt").InnerXml + "</td>");
                    Response.Write("<td height=\"35\" class=\"comment_td_bg\" style=\"padding:0px 5px;\" >" + nodes.Current.SelectSingleNode("accBalAmt").InnerXml + "</td>");
                    Response.Write("<td height=\"35\" class=\"comment_td_bg\" style=\"padding:0px 5px;\" >" + nodes.Current.SelectSingleNode("accountNm").InnerXml + "</td>");
                    Response.Write("<td height=\"35\" class=\"comment_td_bg\" style=\"padding:0px 5px;text-align:center;\" >" + nodes.Current.SelectSingleNode("verifyCode").InnerXml + "</td>");
                    Response.Write("<td height=\"35\" class=\"comment_td_bg\" style=\"padding:0px 5px;\" >" + nodes.Current.SelectSingleNode("memo").InnerXml + "</td>");
                    Response.Write("</tr>");
                }
            }

            Response.Write("</table>");
            //Response.Write("</div>");
            Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" align=\"center\" cellpadding=\"2\"><tr><td align=\"right\" height=\"10\"></td></tr><tr><td align=\"right\"><div class=\"page\" style=\"float:right;\">");
            if (icount == 10)
            {
                pub.Page(curpage + 1, curpage, Pageurl, 10, 10 * (curpage + 1));
            }
            else
            {
                pub.Page(curpage, curpage, Pageurl, 10, 10 * curpage);
            }

            Response.Write("</div></td></tr></table>");
        }
        else
        {
            Response.Write("<tr bgcolor=\"#ffffff\"><td height=\"35\" colspan=\"7\" align=\"center\" valign=\"middle\">没有记录</td></tr>");
        }

        Response.Write("</table>");
    }

    /// <summary>
    /// 出金手续费
    /// </summary>
    /// <param name="Withdraw">出金金额</param>
    /// <param name="BandName">出金银行</param>
    /// <returns></returns>
    public decimal WithdrawFee(decimal Withdraw, string BandName)
    {
        if (BandName == "中信银行")
        {
            #region 中信银行费率标准

            if (Withdraw <= 10000)
            {
                return 5;
            }
            else if (Withdraw > 10000 & Withdraw <= 100000)
            {
                return 10;
            }
            else if (Withdraw > 100000 & Withdraw <= 500000)
            {
                return 15;
            }
            else if (Withdraw > 500000 & Withdraw <= 1000000)
            {
                return 20;
            }
            else if (Withdraw > 1000000)
            {
                if (Withdraw >= 10000000)
                {
                    return 200;
                }
                else
                {
                    return (Withdraw * (decimal)0.00002);
                }
            }
            else
            {
                return Withdraw;
            }

            #endregion
        }
        else
        {
            #region 其他银行费率标准

            if (Withdraw <= 10000)
            {
                return (decimal)5.5;
            }
            else if (Withdraw > 10000 & Withdraw <= 100000)
            {
                return (decimal)10.5;
            }
            else if (Withdraw > 100000 & Withdraw <= 500000)
            {
                return (decimal)15.5;
            }
            else if (Withdraw > 500000 & Withdraw <= 1000000)
            {
                return (decimal)20.5;
            }
            else if (Withdraw > 1000000)
            {
                if (Withdraw >= 10000000)
                {
                    return (decimal)200.5;
                }
                else
                {
                    return (Withdraw * (decimal)0.00002) + (decimal)0.5;
                }
            }
            else
            {
                return Withdraw;
            }

            #endregion
        }
    }

    /// <summary>
    /// 查询会员的担保账户余额（本地记录）
    /// </summary>
    /// <param name="MemberID">会员ID</param>
    /// <returns></returns>
    public double GetZhongXinAccountRemainByMemberID(int MemberID)
    {
        return MyBLL.GetZhongXinAccountRemainByMemberID(MemberID);
    }

    /// <summary>
    /// 会员担保账户操作日志
    /// </summary>
    /// <param name="Member_ID">会员ID</param>
    /// <param name="Amount">金额</param>
    /// <param name="Log_note">备注</param>
    /// <returns></returns>
    public bool SaveZhongXinAccountLog(int Member_ID, double Amount, string Log_note)
    {
        double Account_Log_Remain = GetZhongXinAccountRemainByMemberID(Member_ID);

        ZhongXinAccountLogInfo accountLog = new ZhongXinAccountLogInfo();
        accountLog.Account_Log_ID = 0;
        accountLog.Account_Log_MemberID = Member_ID;
        accountLog.Account_Log_Amount = Amount;
        accountLog.Account_Log_Remain = Account_Log_Remain + Amount;
        accountLog.Account_Log_Note = Log_note;
        accountLog.Account_Log_Addtime = DateTime.Now;
        accountLog.Account_Log_Site = "CN";

        if (accountLog.Account_Log_Remain < 0)
        {
            return false;
        }
        else
        {
            return MyBLL.SaveZhongXinAccountLog(accountLog);
        }
    }
}

public class ZhongXinBreakInfo
{
    public int ID { get; set; }
    public string ContractSN { get; set; }
    public string DeliveryDocNo { get; set; }
    public string NodeName { get; set; }
    public double PaymentPrice { get; set; }
    public double AcceptPrice { get; set; }
    public double CommissionPrice { get; set; }
    public string Notes { get; set; }
    public DateTime Addtime { get; set; }
}
