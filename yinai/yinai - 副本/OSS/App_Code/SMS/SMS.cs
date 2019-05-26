using System;
using System.IO;
using System.Text;
using System.Net;
using System.Web;
using System.Configuration;
using System.Xml.XPath;
using System.Text.RegularExpressions;

/// <summary>
/// 短信服务
/// </summary>
public class SMS
{
    XPathDocument xmldoc;
    XPathNavigator navigator;
    string smsSn = string.Empty, smsPwd = string.Empty, smsLog = string.Empty, smsNo = string.Empty;

    public SMS()
    {
        xmldoc = new XPathDocument(HttpContext.Current.Server.MapPath("/app_data/smsconfig.xml"));
        navigator = xmldoc.CreateNavigator();

        XPathNodeIterator nodes = navigator.Select("/smsconfig/config/sn");
        if (nodes != null)
        {
            while (nodes.MoveNext())
            {
                smsSn = nodes.Current.InnerXml;
            }
        }
        nodes = navigator.Select("/smsconfig/config/account");
        if (nodes != null)
        {
            while (nodes.MoveNext())
            {
                smsNo = nodes.Current.InnerXml;
            }

        }
        nodes = navigator.Select("/smsconfig/config/pwd");
        if (nodes != null)
        {
            while (nodes.MoveNext())
            {
                smsPwd = nodes.Current.InnerXml;
            }
        }

        nodes = navigator.Select("/smsconfig/config/log");
        if (nodes != null)
        {
            while (nodes.MoveNext())
            {
                smsLog = nodes.Current.InnerXml;
            }
        }
    }

    /// <summary>
    /// 获取短信模板
    /// </summary>
    /// <param name="type">模板标示</param>
    /// <returns></returns>
    public string GetTemplates(string type)
    {
        XPathNodeIterator nodes = navigator.Select("/smsconfig/templates/template[@key='" + type + "']");
        if (nodes != null)
        {
            while (nodes.MoveNext())
            {
                return nodes.Current.InnerXml;
            }
        }
        return string.Empty;
    }

    public string Send(string Phone, string Content, string type)
    {
        //MsgAPI.Service1SoapClient ws = new MsgAPI.Service1SoapClient();
        //return ws.SendMessages("lifengyue", "lifengyue", Phone, Content, "");
        string[] ContentStringArray = Content.Split(',');
        SMSSend SMS = new SMSSend();
        string ContentString = string.Empty;
        switch (ContentStringArray.Length)
        {
            case 1:
                ContentString = string.Format(this.GetTemplates(type), ContentStringArray[0]);
                break;
            case 2:
                ContentString = string.Format(this.GetTemplates(type), ContentStringArray[0], ContentStringArray[1]);
                break;
            case 3:
                ContentString = string.Format(this.GetTemplates(type), ContentStringArray[0], ContentStringArray[1], ContentStringArray[2]);
                break;
            case 4:
                ContentString = string.Format(this.GetTemplates(type), ContentStringArray[0], ContentStringArray[1], ContentStringArray[2], ContentStringArray[3]);
                break;

            default:
                break;
        }
        return SMS.ZhongZhengSMS(smsNo, smsPwd, Phone, ContentString, "");

    }

    //public string Send(string Phone, string Content)
    //{
    //    MsgAPI.Service1SoapClient ws = new MsgAPI.Service1SoapClient();
    //    return ws.SendMessages("lifengyue", "lifengyue", Phone, Content, "");
    //}

}