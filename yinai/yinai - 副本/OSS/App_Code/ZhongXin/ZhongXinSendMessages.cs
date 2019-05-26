using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using System.Xml;
using System.Xml.XPath;

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

        public SendMessages()
        {
            userName = System.Configuration.ConfigurationManager.AppSettings["zhongxin_username"];
            mainAccNo = System.Configuration.ConfigurationManager.AppSettings["zhongxin_mainaccno"];
            mngNode = System.Configuration.ConfigurationManager.AppSettings["zhongxin_mngnode"];
            encodingStr = "gb2312";
            PostServer = System.Configuration.ConfigurationManager.AppSettings["zhongxin_postserver"];
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
            PostData.Append("	<calInterestFlag>2</calInterestFlag>");
            PostData.Append("	<interestRate>0.0035</interestRate>");
            PostData.Append("	<overFlag>0</overFlag>");
            PostData.Append("	<overAmt>0</overAmt>");
            PostData.Append("	<overRate>0</overRate>");
            PostData.Append("	<autoAssignInterestFlag>1</autoAssignInterestFlag>");
            PostData.Append("	<autoAssignTranFeeFlag>1</autoAssignTranFeeFlag>");
            PostData.Append("	<feeType>1</feeType>");
            PostData.Append("	<realNameParm>1</realNameParm>");
            PostData.Append("	<subAccPrintParm>0</subAccPrintParm>");
            PostData.Append("   <mngNode>" + mngNode + "</mngNode>");
            PostData.Append("</stream>");

            string xmlResult = HttpUtil.SendRequest(PostData.ToString(), PostServer, encodingStr);
            if (xmlResult == null)
            {
                strResult = "请求失败";
                return false;
            }
            if (xmlResult.Length == 0)
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

        /// <summary>
        /// 附属账户修改
        /// </summary>
        /// <param name="subAccNo">附属帐号</param>
        /// <param name="subAccNm">附属帐号名称</param>
        /// <returns></returns>
        public string subAccountEdit(string subAccNo, string subAccNm)
        {
            StringBuilder PostData = new StringBuilder();
            PostData.Append("<?xml version=\"1.0\" encoding=\"GBK\" ?>");
            PostData.Append("<stream>");
            PostData.Append("<action>DLSBAMAN</action>");
            PostData.Append("<userName>" + userName + "</userName>");
            PostData.Append("<mainAccNo>" + mainAccNo + "</mainAccNo>");
            PostData.Append("<subAccNo>" + subAccNo + "</subAccNo>");
            PostData.Append("<subAccNm>" + subAccNm + "</subAccNm>");
            PostData.Append("<autoAssignInterestFlag>1</autoAssignInterestFlag>");
            PostData.Append("<calInterestFlag>2</calInterestFlag>");
            PostData.Append("<interestRate>0.0035000</interestRate>");
            PostData.Append("<autoAssignTranFeeFlag>1</autoAssignTranFeeFlag>");
            PostData.Append("<feeType>1</feeType>");
            PostData.Append("</stream>");

            string xmlResult = HttpUtil.SendRequest(PostData.ToString(), PostServer, encodingStr);
            if (xmlResult == null || xmlResult.Length == 0)
            {
                return string.Empty;
            }

            if (StatusCode(xmlResult) == "t")
            {
                return "t";
            }
            else
            {
                return GetXMLElement(xmlResult, "/stream/statusText");
            }
        }
    }
}