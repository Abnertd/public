using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using System.Xml;
using System.Xml.XPath;
using Glaer.Trade.B2C.BLL.ORD;
using Glaer.Trade.B2C.Model;

namespace ZhongXinUtil
{
    /// <summary>
    /// 报文信息
    /// </summary>
    public class SendMessages
    {
        string userName = string.Empty;
        string mainAccNo = string.Empty;
        string mngNode = string.Empty;
        string encodingStr = string.Empty;
        string PostServer = string.Empty;


        private IPaymentInformation MyPayInfo;

        public SendMessages()
        {
            userName = System.Configuration.ConfigurationManager.AppSettings["zhongxin_username"];
            mainAccNo = System.Configuration.ConfigurationManager.AppSettings["zhongxin_mainaccno"];
            mngNode = System.Configuration.ConfigurationManager.AppSettings["zhongxin_mngnode"];
            encodingStr = "gb2312";
            PostServer = System.Configuration.ConfigurationManager.AppSettings["zhongxin_postserver"];


            MyPayInfo = PaymentInformationFactory.CreatePaymentInformation();
        }

        /// <summary>
        /// 状态码
        /// </summary>
        /// <param name="xmlResult"></param>
        /// <returns></returns>
        public string StatusCode(string xmlResult)
        {
            XPathDocument xmlDoc = new XPathDocument(new StringReader(xmlResult));
            XPathNavigator navigator = xmlDoc.CreateNavigator();
            XPathNodeIterator nodes = navigator.Select("/stream/status");
            if (nodes != null)
            {
                while (nodes.MoveNext())
                {
                    if (nodes.Current.InnerXml == "AAAAAAA")
                    {
                        return "t";
                    }
                    else
                    {
                        return nodes.Current.InnerXml;
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 解析XML
        /// </summary>
        /// <param name="xmlResult"></param>
        /// <returns></returns>
        public string GetXMLElement(string xmlResult, string xpath)
        {
            XPathDocument xmlDoc = new XPathDocument(new StringReader(xmlResult));
            XPathNavigator navigator = xmlDoc.CreateNavigator();
            XPathNodeIterator nodes = navigator.Select(xpath);
            if (nodes != null)
            {
                while (nodes.MoveNext())
                {
                    return nodes.Current.InnerXml;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 附属账户预签约
        /// </summary>
        /// <returns></returns>
        public bool AccountSign(string AccountName, ref string strResult)
        {
            StringBuilder PostData = new StringBuilder();
            PostData.Append("<?xml version=\"1.0\" encoding=\"GBK\" ?>");
            PostData.Append("<stream>");
            PostData.Append("	<action>DLBREGST</action>");
            PostData.Append("	<userName>" + userName + "</userName>");
            PostData.Append("	<mainAccNo>" + mainAccNo + "</mainAccNo>");
            PostData.Append("	<appFlag>2</appFlag>");
            PostData.Append("	<accGenType>0</accGenType>");
            PostData.Append("	<subAccNo></subAccNo>");
            PostData.Append("	<subAccNm>" + AccountName + "</subAccNm>");
            PostData.Append("	<accType>03</accType>");
            PostData.Append("	<calInterestFlag>1</calInterestFlag>");
            PostData.Append("	<interestRate>0</interestRate>");
            PostData.Append("	<overFlag>0</overFlag>");
            PostData.Append("	<overAmt>0</overAmt>");
            PostData.Append("	<overRate>0</overRate>");
            PostData.Append("	<autoAssignInterestFlag>0</autoAssignInterestFlag>");
            PostData.Append("	<autoAssignTranFeeFlag>0</autoAssignTranFeeFlag>");
            PostData.Append("	<feeType>0</feeType>");
            PostData.Append("	<realNameParm>0</realNameParm>");
            PostData.Append("	<subAccPrintParm>0</subAccPrintParm>");
            PostData.Append("   <mngNode>" + mngNode + "</mngNode>");
            PostData.Append("</stream>");

            string xmlResult = HttpUtil.SendRequest(PostData.ToString(), PostServer, encodingStr);
            if (xmlResult == null || xmlResult.Length == 0)
            {
                strResult = "请求失败";
                return false;
            }

            if (StatusCode(xmlResult) == "t")
            {
                strResult = GetXMLElement(xmlResult, "/stream/subAccNo");
                return true;
            }
            else
            {
                strResult = GetXMLElement(xmlResult, "/stream/statusText");
                return false;
            }
        }

        /// <summary>
        /// 强制转账
        /// </summary>
        /// <param name="payAccNo">付款账号</param>
        /// <param name="recvAccNo">收款账号</param>
        /// <param name="recvAccNm">收款账号名</param>
        /// <param name="memo">备注</param>
        /// <param name="tranAmt">金额</param>
        /// <param name="strResult">返回说明</param>
        /// <returns></returns>
        public bool Transfer(string payAccNo, string recvAccNo, string recvAccNm, string memo, double tranAmt, ref string strResult, string supplier_name)
        {
            StringBuilder PostData = new StringBuilder();
            string Payment_Flow = DateTime.Now.ToString("yyyyMMddHHmmss") + new Public_Class().Createvkey(6);
            PostData.Append("<?xml version=\"1.0\" encoding=\"GBK\" ?>");
            PostData.Append("<stream>");
            PostData.Append("	<action>DLMDETRN</action>");
            PostData.Append("	<userName>" + userName + "</userName>");
            //PostData.Append("	<clientID>" + DateTime.Now.ToString("yyyyMMddHHmmss") + new Public_Class().Createvkey(6) + "</clientID>");
            PostData.Append("	<clientID>" + Payment_Flow + "</clientID>");
            PostData.Append("	<accountNo>" + mainAccNo + "</accountNo>");
            PostData.Append("	<payAccNo>" + payAccNo + "</payAccNo>");
            PostData.Append("	<tranType>BF</tranType>");
            PostData.Append("	<recvAccNo>" + recvAccNo + "</recvAccNo>");
            PostData.Append("	<recvAccNm>" + recvAccNm + "</recvAccNm>");
            PostData.Append("	<tranAmt>" + tranAmt + "</tranAmt>");
            PostData.Append("	<freezeNo></freezeNo>");
            PostData.Append("	<ofreezeamt></ofreezeamt>");
            PostData.Append("	<memo>" + memo + "</memo>");
            PostData.Append("	<tranFlag>1</tranFlag>");
            PostData.Append("</stream>");

            string xmlResult = HttpUtil.SendRequest(PostData.ToString(), PostServer, encodingStr);

            DateTime CurTime = DateTime.Now;
            PaymentInformationInfo payInfoEntity = new PaymentInformationInfo();
            //付款账户
            payInfoEntity.Payment_PayingTeller = supplier_name;
            //付款账号
            payInfoEntity.Payment_Account = payAccNo;
            //收款账号
            payInfoEntity.Payment_Receivable = recvAccNo;
            //收款账户
            payInfoEntity.Payment_Account_Receivable = recvAccNm;

            //担保类型付款类型 1担保付款 2货款结算  3出金   4收取交易佣金  5支付投标保证金  6退还投标保证金  7支付商家保证金 8退还商家保证金 9其它

            payInfoEntity.Payment_Type = 0;
            //付款金额
            payInfoEntity.Payment_Amount = tranAmt;
            //备注信息
            payInfoEntity.Payment_Remarks = memo;
            //付款时间
            payInfoEntity.Payment_Account_Time = CurTime;




            if (xmlResult == null || xmlResult.Length == 0)
            {
                //付款状态
                payInfoEntity.Payment_Status = 2;
                payInfoEntity.Payment_Flow = Payment_Flow;
                payInfoEntity.Payment_Remarks1 = "中信支付备注说明:付款账户:" + supplier_name + ",付款账号:" + payAccNo + ",收款账户:" + recvAccNm + ",收款账号:" + recvAccNo + ",付款类型:" + "" + memo + "" + ", 付款金额:" + tranAmt + ",备注信息:" + memo + ",付款时间:" + CurTime + ",请求失败(没有返回数据),流水号:" + Payment_Flow + "";
                PayLogs.WriteLogs(PostData.ToString());
                strResult = "请求失败";

                MyPayInfo.AddPaymentInformation(payInfoEntity);
                return false;
            }

            if (StatusCode(xmlResult) == "t")
            {
                strResult = GetXMLElement(xmlResult, "/stream/statusText");

                //付款状态
                payInfoEntity.Payment_Status = 1;
                payInfoEntity.Payment_Flow = Payment_Flow;
                payInfoEntity.Payment_Remarks1 = "中信支付备注说明:付款账户:" + supplier_name + ",付款账号:" + payAccNo + ",收款账户:" + recvAccNm + ",收款账号:" + recvAccNo + ",付款类型:" + "" + memo + "" + ", 付款金额:" + tranAmt + ",备注信息:" + memo + ",付款时间:" + CurTime + ",付款成功:,流水号:" + Payment_Flow + "";
                PayLogs.WriteLogs(PostData.ToString());
                MyPayInfo.AddPaymentInformation(payInfoEntity);
                return true;
            }
            else if (tranAmt == 0)
            {
                strResult = GetXMLElement(xmlResult, "/stream/statusText");

                //付款状态
                payInfoEntity.Payment_Status = 1;
                payInfoEntity.Payment_Flow = Payment_Flow;
                payInfoEntity.Payment_Remarks1 = "中信支付备注说明:付款账户:" + supplier_name + ",付款账号:" + payAccNo + ",收款账户:" + recvAccNm + ",收款账号:" + recvAccNo + ",付款类型:" + "" + memo + "" + ", 付款金额:" + tranAmt + ",备注信息:" + memo + ",付款时间:" + CurTime + ",付款成功:,流水号:" + Payment_Flow + "";
                PayLogs.WriteLogs(PostData.ToString());
                MyPayInfo.AddPaymentInformation(payInfoEntity);
                return true;
            }
            else
            {
                strResult = GetXMLElement(xmlResult, "/stream/statusText");
                payInfoEntity.Payment_Status = 0;
                payInfoEntity.Payment_Flow = Payment_Flow;
                payInfoEntity.Payment_Remarks1 = "中信支付备注说明:付款账户:" + supplier_name + ",付款账号:" + payAccNo + ",收款账户:" + recvAccNm + ",收款账号:" + recvAccNo + ",付款类型:" + "" + memo + "" + ", 付款金额:" + tranAmt + ",备注信息:" + memo + ",付款时间:" + CurTime + ",付款状态:付款失败,流水号:" + Payment_Flow + "";
                PayLogs.WriteLogs(PostData.ToString());
                MyPayInfo.AddPaymentInformation(payInfoEntity);
                return false;
            }
        }



        /// <summary>
        /// 充钱
        /// </summary>
        /// <param name="payAccNo">付款账号</param>
        /// <param name="recvAccNo">收款账号</param>
        /// <param name="recvAccNm">收款账号名</param>
        /// <param name="memo">备注</param>
        /// <param name="tranAmt">金额</param>
        /// <param name="strResult">返回说明</param>
        /// <returns></returns>
        public bool Transfer1(int member_id)
        {
            StringBuilder PostData = new StringBuilder();
            string Payment_Flow = DateTime.Now.ToString("yyyyMMddHHmmss") + new Public_Class().Createvkey(6);
            PostData.Append("<?xml version=\"1.0\" encoding=\"GBK\"?>");
            PostData.Append("<stream>");
            PostData.Append("<action>DLFNDINI</action>");
            PostData.Append("<userName>lierzl</userName>");
            //PostData.Append("	<clientID>" + DateTime.Now.ToString("yyyyMMddHHmmss") + new Public_Class().Createvkey(6) + "</clientID>");
            PostData.Append("	<clientID>" + Payment_Flow + "</clientID>");
            PostData.Append("<accountNo>3110710006721454065</accountNo>");
            PostData.Append("<subAccNo>3110710006721529449</subAccNo>");
            PostData.Append("<subAccNm>富士康</subAccNm>");
            PostData.Append("<tranAmt>5000.00</tranAmt>");
            PostData.Append("<memo></memo>");
            PostData.Append("</stream>");

            string xmlResult = HttpUtil.SendRequest(PostData.ToString(), PostServer, encodingStr);





            if (xmlResult == null || xmlResult.Length == 0)
            {
                return false;
            }

            if (StatusCode(xmlResult) == "t")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 平台出金
        /// </summary>
        /// <param name="payAccNo">提现子账号</param>
        /// <param name="outAccNo">出金收款账号</param>
        /// <param name="outAccName">开户名</param>
        /// <param name="outBankCode">银行行号</param>
        /// <param name="outBankName">银行名称</param>
        /// <param name="tranAmt">金额</param>
        /// <param name="strResult">返回值</param>
        /// <returns></returns>
        public bool Withdraw(string payAccNo, string outAccNo, string outAccName, string outBankCode, string outBankName, decimal tranAmt, string sameBank, string memo, ref string strResult)
        {
            StringBuilder PostData = new StringBuilder();
            PostData.Append("<?xml version=\"1.0\" encoding=\"GBK\" ?>");
            PostData.Append("<stream>");
            PostData.Append("<action>DLFCSOUT</action>");
            PostData.Append("<userName>" + userName + "</userName>");
            PostData.Append("<clientID>" + DateTime.Now.ToString("yyyyMMddHHmmss") + new Public_Class().Createvkey(6) + "</clientID>");
            PostData.Append("<accountNo>" + payAccNo + "</accountNo>");
            PostData.Append("<recvAccNo>" + outAccNo + "</recvAccNo>");
            PostData.Append("<recvAccNm>" + outAccName + "</recvAccNm>");
            PostData.Append("<tranAmt>" + tranAmt + "</tranAmt>");
            PostData.Append("<sameBank>" + (sameBank == "中信银行" ? "0" : "1") + "</sameBank>");
            PostData.Append("<recvTgfi>" + outBankCode + "</recvTgfi>");
            PostData.Append("<recvBankNm>" + outBankName + "</recvBankNm>");
            PostData.Append("<memo>" + memo + "</memo>");
            PostData.Append("<preFlg>0</preFlg>");
            PostData.Append("<preDate></preDate>");
            PostData.Append("<preTime></preTime>");

            #region 预约时间

            //if (DateTime.Now.Hour < 10)
            //{
            //    PostData.Append("<preDate>" + DateTime.Today.ToString("yyyyMMdd") + "</preDate>");
            //    PostData.Append("<preTime>100000</preTime>");
            //}
            //else if (DateTime.Now.Hour < 12)
            //{
            //    PostData.Append("<preDate>" + DateTime.Today.ToString("yyyyMMdd") + "</preDate>");
            //    PostData.Append("<preTime>120000</preTime>");
            //}
            //else if (DateTime.Now.Hour < 14)
            //{
            //    PostData.Append("<preDate>" + DateTime.Today.ToString("yyyyMMdd") + "</preDate>");
            //    PostData.Append("<preTime>140000</preTime>");
            //}
            //else if (DateTime.Now.Hour < 16)
            //{
            //    PostData.Append("<preDate>" + DateTime.Today.ToString("yyyyMMdd") + "</preDate>");
            //    PostData.Append("<preTime>160000</preTime>");
            //}
            //else
            //{
            //    PostData.Append("<preDate>" + DateTime.Today.AddDays(1).ToString("yyyyMMdd") + "</preDate>");
            //    PostData.Append("<preTime>100000</preTime>");
            //}

            #endregion

            PostData.Append("</stream>");

            string xmlResult = HttpUtil.SendRequest(PostData.ToString(), PostServer, encodingStr);
            if (xmlResult == null || xmlResult.Length == 0)
            {
                strResult = "请求失败";
                return false;
            }

            string statusCode = StatusCode(xmlResult);
            if (statusCode == "AAAAAAE")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 获取账户金额
        /// </summary>
        /// <param name="subAccNo">附属子账号</param>
        /// <returns></returns>
        public decimal GetAmount(string subAccNo)
        {
            StringBuilder PostData = new StringBuilder();
            PostData.Append("<?xml version=\"1.0\" encoding=\"GBK\" ?>");
            PostData.Append("<stream>");
            PostData.Append("	<action>DLSBALQR</action>");
            PostData.Append("	<userName>" + userName + "</userName>");
            PostData.Append("	<accountNo>" + mainAccNo + "</accountNo>");
            PostData.Append("	<subAccNo>" + subAccNo + "</subAccNo>");
            PostData.Append("</stream>");

            string xmlResult = HttpUtil.SendRequest(PostData.ToString(), PostServer, encodingStr);
            if (xmlResult == null || xmlResult.Length == 0)
            {
                return 0;
            }

            if (StatusCode(xmlResult) == "t")
            {
                return Convert.ToDecimal(GetXMLElement(xmlResult, "/stream/list/row[1]/KYAMT"));
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 账户明细
        /// </summary>
        /// <param name="subAccNo"></param>
        /// <returns></returns>
        public string AccountDetail(string subAccNo, string startDate, string endDate, int startRecord)
        {
            StringBuilder PostData = new StringBuilder();
            PostData.Append("<?xml version=\"1.0\" encoding=\"GBK\" ?>");
            PostData.Append("<stream>");
            PostData.Append("<action>DLPTDTQY</action>");
            PostData.Append("<userName>" + userName + "</userName>");
            PostData.Append("<mainAccNo>" + mainAccNo + "</mainAccNo>");
            PostData.Append("<subAccNo>" + subAccNo + "</subAccNo>");
            PostData.Append("<startDate>" + startDate + "</startDate>");
            PostData.Append("<endDate>" + endDate + "</endDate>");
            PostData.Append("<startRecord>" + startRecord + "</startRecord>");
            PostData.Append("<pageNumber>10</pageNumber>");
            PostData.Append("</stream>");

            string xmlResult = HttpUtil.SendRequest(PostData.ToString(), PostServer, encodingStr);
            if (xmlResult == null || xmlResult.Length == 0)
            {
                return string.Empty;
            }

            if (StatusCode(xmlResult) == "t")
            {
                return xmlResult;
            }
            else
            {
                return string.Empty;
            }
        }

    }
}