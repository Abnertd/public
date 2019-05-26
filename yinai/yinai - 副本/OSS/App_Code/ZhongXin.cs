using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Glaer.Trade.Util.Tools;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.BLL.MEM;
using System.Xml.XPath;
using System.IO;

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
    ISupplier MySupplier;

    ZhongXinUtil.SendMessages sendmessages;

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

        sendmessages = new ZhongXinUtil.SendMessages();
    }

    public ZhongXinInfo GetZhongXinByID(int ID)
    {
        return MyBLL.GetZhongXinByID(ID);
    }

    public ZhongXinInfo GetZhongXinBySuppleir(int ID)
    {
        return MyBLL.GetZhongXinBySuppleir(ID);
    }

    public string GetZhongXins()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ZhongXinInfo.ID", ">", "0"));

        string keyword = tools.CheckStr(Request["keyword"]);
        if (keyword != "")
        {
            Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ZhongXinInfo.CompanyName", "like", keyword));
            Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ZhongXinInfo.ReceiptAccount", "like", keyword));
        }
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query);

        IList<ZhongXinInfo> entitys = null;
        if (pageinfo.RecordCount > 0)
            entitys = MyBLL.GetZhongXins(Query);

        SupplierInfo supplier = null;
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ZhongXinInfo entity in entitys)
            {
                jsonBuilder.Append("{\"id\":" + entity.ID + ",\"cell\":[");

                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                supplier = MySupplier.GetSupplierByID(entity.SupplierID, Public.GetUserPrivilege());
                if (supplier != null)
                {
                    jsonBuilder.Append(Public.JsonStr(supplier.Supplier_CompanyName));
                }
                else
                {
                    jsonBuilder.Append(entity.SupplierID);
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.CompanyName));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.OpenAccountName));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.SubAccount));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.ReceiptAccount));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.ReceiptBank));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.BankName));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.BankCode));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Audit == 0)
                {
                    jsonBuilder.Append(Public.JsonStr("未审核"));
                }
                else
                {
                    jsonBuilder.Append(Public.JsonStr("已审核"));
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.Register == 0)
                {
                    jsonBuilder.Append(Public.JsonStr("未推送"));
                }
                else
                    if (entity.Register == 1)
                    {
                        jsonBuilder.Append(Public.JsonStr("已推送未绑定"));
                    }
                    else
                    {
                        jsonBuilder.Append(Public.JsonStr("已推送绑定"));
                    }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (entity.Audit == 1)//平台已审核
                {
                    if (entity.Register == 0)
                    {
                        jsonBuilder.Append("<img src=\\\"/images/btn_add.gif\\\" alt=\\\"推送数据\\\" align=\\\"absmiddle\\\"> <a  href=\\\"javascript:void(0);\\\" onclick=\\\"window.open('zhongxin_do.aspx?action=tuisong&id=" + entity.ID + "', '_self')\\\">推送数据</a> ");
                    }
                    if (entity.Register == 1)
                    {
                        jsonBuilder.Append("<img src=\\\"/images/btn_add.gif\\\" alt=\\\"绑定出金账号\\\" align=\\\"absmiddle\\\"> <a  href=\\\"javascript:void(0);\\\" onclick=\\\"window.open('zhongxin_do.aspx?action=editRegister&id=" + entity.ID + "', '_self')\\\">绑定</a> ");
                    }
                }

                jsonBuilder.Append("<img src=\\\"/images/btn_add.gif\\\" alt=\\\"修改\\\" align=\\\"absmiddle\\\"> <a href=\\\"zhongxin_edit.aspx?id=" + entity.ID + "\\\" title=\\\"修改\\\">修改</a> ");
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

    /// <summary>
    /// 签约
    /// </summary>
    public void AccountSign()
    {
        ZhongXinInfo entity = GetZhongXinByID(tools.CheckInt(Request["ID"]));
        ISupplier mysupplierBll = SupplierFactory.CreateSupplier();
        SupplierInfo entitySypplier = mysupplierBll.GetSupplierByID(entity.SupplierID, Public.GetUserPrivilege());
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }

        string strResult = string.Empty;
        if (sendmessages.AccountSign(entity.CompanyName, ref strResult))
        {
            entity.Register = 1;
            entity.SubAccount = strResult;
        }
        else
        {
            Public.Msg("error", "错误信息", strResult, false, "{back}");
        }

        if (MyBLL.EditZhongXin(entity))
        {
            //推送短信
            SMS mySMS = new SMS();
            mySMS.Send(entitySypplier.Supplier_Mobile, entity.CompanyName, "zhongxin_info");
            Public.Msg("positive", "操作成功", "操作成功", true, "zhongxin_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }

    }
    /// <summary>
    /// 签约
    /// </summary>
    public void AccountSign(int idInt, string mobileString)
    {
        ZhongXinInfo entity = GetZhongXinBySuppleir(idInt);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }

        string strResult = string.Empty;
        if (sendmessages.AccountSign(entity.CompanyName, ref strResult))
        {
            entity.Register = 1;
            entity.SubAccount = strResult;
        }
        else
        {
            Public.Msg("error", "错误信息", strResult, false, "{back}");
        }

        if (MyBLL.EditZhongXin(entity))
        {
            //推送短信
            SMS mySMS = new SMS();
            mySMS.Send(mobileString, entity.CompanyName, "zhongxin_info");
            Public.Msg("positive", "操作成功", "操作成功", true, "zhongxin_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }

    }
    public void RegistZhonghang(int id)
    {
        //ZhongXinInfo entity = GetZhongXinByID(tools.CheckInt(Request.Form["id"]));
        ZhongXinInfo entity = GetZhongXinByID(tools.CheckInt(id.ToString()));
        entity.Register = 2;
        if (MyBLL.EditZhongXin(entity))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "zhongxin_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }

    }

    public void EditZhonghang()
    {
        ZhongXinInfo entity = GetZhongXinByID(tools.CheckInt(Request.Form["ID"]));
        if (entity == null)
        {
            Public.Msg("info", "提示信息", "您没有权限", false, "{back}");
        }

        string CompanyName = tools.CheckStr(Request.Form["CompanyName"]);
        string ReceiptAccount = tools.CheckStr(Request.Form["ReceiptAccount"]);
        string ReceiptBank = tools.CheckStr(Request.Form["ReceiptBank"]);
        string BankCode = tools.CheckStr(Request.Form["BankCode"]);
        string BankName = tools.CheckStr(Request.Form["BankName"]);
        string OpenAccountName = tools.CheckStr(Request.Form["OpenAccountName"]);
        int Audit = tools.CheckInt(Request.Form["Audit"]);
        string SubAccount = tools.CheckStr(Request.Form["SubAccount"]);

        if (CompanyName == "")
        {
            Public.Msg("info", "提示信息", "填写公司名称", false, "{back}");
        }

        //if (ReceiptAccount == "")
        //{
        //    Public.Msg("info", "提示信息", "填写出金收款账号须真实有效", false, "{back}");
        //}

        //if (ReceiptBank == "")
        //{
        //    Public.Msg("info", "提示信息", "填写出金收款银行", false, "{back}");
        //}

        //if (BankCode == "")
        //{
        //    Public.Msg("info", "提示信息", "填写银行行号", false, "{back}");
        //}

        //if (BankName == "")
        //{
        //    Public.Msg("info", "提示信息", "填写银行名称", false, "{back}");
        //}

        //if (OpenAccountName == "")
        //{
        //    Public.Msg("info", "提示信息", "填写开户名称", false, "{back}");
        //}

        entity.CompanyName = CompanyName;
        entity.ReceiptAccount = ReceiptAccount;
        entity.ReceiptBank = ReceiptBank;
        entity.BankCode = BankCode;
        entity.BankName = BankName;
        entity.OpenAccountName = OpenAccountName;
        entity.Audit = Audit;
        entity.SubAccount = SubAccount;

        if (MyBLL.EditZhongXin(entity))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "zhongxin_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }

    }

    public void subAccountEdit()
    {
        string subAccNo = tools.CheckStr(Request["subAccNo"]);
        string subAccNm = tools.CheckStr(Request["subAccNm"]);
        if (subAccNo == "")
        {
            Public.Msg("info", "提示信息", "填写附属账号", false, "{back}");
        }
        if (subAccNm == "")
        {
            Public.Msg("info", "提示信息", "填写附属账户名称", false, "{back}");
        }
        string strResult = sendmessages.subAccountEdit(subAccNo, subAccNm);
        if (strResult == "t")
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "subAccount_edit.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", strResult, false, "{back}");
        }
    }

    /// <summary>
    /// 交易流水查询
    /// </summary>
    /// <param name="subAccNo"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    public void Account_List(string subAccNo, DateTime startDate, DateTime endDate)
    {
        int i = 0;
        string Pageurl;
        int curpage = tools.CheckInt(tools.NullStr(Request["page"]));
        Pageurl = "?action=list&SubAccount=" + subAccNo + "&date_start=" + startDate.ToString("yyyy-MM-dd") + "&date_end=" + endDate.ToString("yyyy-MM-dd");
        if (curpage < 1)
        {
            curpage = 1;
        }

        Response.Write("<table width=\"100%\" cellpadding=\"0\" align=\"center\" cellspacing=\"1\" style=\"background:#dadada;\" >");
        Response.Write("<tr style=\"background:url(/images/ping.jpg); height:30px;\">");
        Response.Write("  <th width=\"130\"align=\"center\" valign=\"middle\">交易时间</th>");
        Response.Write("  <th width=\"80\" align=\"center\" valign=\"middle\">借贷标志</th>");
        Response.Write("  <th width=\"80\" align=\"center\" valign=\"middle\">交易金额</th>");
        Response.Write("  <th width=\"80\" align=\"center\" valign=\"middle\">账户余额</th>");
        Response.Write("  <th align=\"center\" valign=\"middle\">对方账户名称</th>");
        Response.Write("  <th width=\"136\" align=\"center\" valign=\"middle\">打印校验码</th>");
        Response.Write("  <th align=\"center\" valign=\"middle\">摘要</th>");
        Response.Write("</tr>");
        if (subAccNo != "")
        {
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
                Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" align=\"center\" cellpadding=\"2\"><tr><td align=\"right\" height=\"10\"></td></tr><tr><td align=\"right\"><div class=\"page\" style=\"float:right;\">");
                if (icount == 10)
                {
                    Response.Write(Public.Page1(curpage + 1, curpage, Pageurl, 10, 10 * (curpage + 1)));
                }
                else
                {
                    Response.Write(Public.Page1(curpage, curpage, Pageurl, 10, 10 * curpage));
                }
                Response.Write("</div></td></tr></table>");
            }
            else
            {
                Response.Write("<tr bgcolor=\"#ffffff\"><td height=\"35\" colspan=\"7\" align=\"center\" valign=\"middle\">没有记录</td></tr>");
            }
        }
        else
        {
            Response.Write("<tr bgcolor=\"#ffffff\"><td height=\"35\" colspan=\"7\" align=\"center\" valign=\"middle\">没有记录</td></tr>");
        }
        Response.Write("</table>");

        //Response.Write("<table width=\"100%\" border=\"0\" cellspacing=\"0\" align=\"center\" cellpadding=\"2\"><tr><td align=\"right\" height=\"10\"></td></tr><tr><td align=\"right\"><div class=\"page\" style=\"float:right;\">");
        //Response.Write(Public.Page1(curpage + 1, curpage, Pageurl, 10, 10 * (curpage + 1)));
        //Response.Write("</div></td></tr></table>");
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

    /// <summary>
    /// 查询会员的担保账户余额（本地记录）
    /// </summary>
    /// <param name="MemberID">会员ID</param>
    /// <returns></returns>
    public double GetZhongXinAccountRemainByMemberID(int MemberID)
    {
        return MyBLL.GetZhongXinAccountRemainByMemberID(MemberID);
    }
}