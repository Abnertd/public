using System;
using System.Data;
using System.Configuration;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using System.Net;
using System.Text;
using System.IO;
using System.Collections.Generic;
using Glaer.Trade.Util.Encrypt;

/// <summary>
///SendMessage 的摘要说明 add wtp 2017年04月19日15:22:09// 华信
/// </summary>
public class NSendMessage
{
    private static IEncrypt encrypt;
    public NSendMessage()
    {
       
        //
        //TODO: 在此处添加构造函数逻辑
        //

    }
  
    private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
    //Encoding myEncoding = Encoding.GetEncoding("UTF-8");
    //private static String url = "http://dx.ipyy.net/sms.aspx?";
    //private static String username = HttpUtility.UrlEncode("xd000717", myEncoding);

    //private static String password = HttpUtility.UrlEncode("xd000717xd", myEncoding);
    public static String SendSmsMessge(String phonelist, String msg, String longnum)
    {
        String result = String.Empty;
        encrypt = EncryptFactory.CreateEncrypt();
        try 
        {
              Encoding myEncoding = Encoding.GetEncoding("UTF-8");
      String url = "http://dx.ipyy.net/sms.aspx?";
      String username = HttpUtility.UrlEncode("xd000717", myEncoding);

      String password = HttpUtility.UrlEncode("xd000717xd", myEncoding);
            WebClient myWebClient = new WebClient();
            //String postdata = String.Format("action={0}&userid={1}&account={2}&password={3}&mobile={4}&content={5}&sendTime={6}&extno={7}"
               // , "send"
               // , ""
               // , username
               // , encrypt.MD5(password)
               // , phonelist
               // , System.Web.HttpUtility.UrlEncode(msg, Encoding.GetEncoding("utf-8"))
               //,""
               //,""


            String postdata = String.Format("action={0}&userid={1}&account={2}&password={3}&mobile={4}&content={5}&sendTime={6}&extno={7}"
              , "send"
              , ""
              , username
              , password
              , HttpUtility.UrlDecode(phonelist, myEncoding)
              , HttpUtility.UrlEncode(msg)
             , ""
             //, ""

                );
            myWebClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            byte[] byteArray = Encoding.Default.GetBytes(postdata);
            byte[] responseArray = myWebClient.UploadData(url, "POST", byteArray);
            result = System.Text.Encoding.Default.GetString(responseArray);
        }
      
        catch (Exception ex)
        {
           //return ex.Message;
        }

      
        return result;
    }


}
