using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;

namespace ZhongXinUtil
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpUtil
    {

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="postURL">请求地址</param>
        /// <returns></returns>
        public static string SendRequest(string postData, string postURL, string encodingStr)
        {
            PayLogs.WriteLogs("请求：" + postURL + "\r\n" + postData);

            try
            {
                WebRequest req = WebRequest.Create(postURL);
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded;charset=" + encodingStr;
                req.Timeout = 100000;

                using (StreamWriter sw = new StreamWriter(req.GetRequestStream(), Encoding.GetEncoding(encodingStr)))
                {
                    sw.Write(postData);
                }

                using (WebResponse response = req.GetResponse())
                {
                    using (Stream sr = response.GetResponseStream())
                    {
                        byte[] tmpByte = new byte[1024];
                        int n = 1;
                        MemoryStream stmMemory = new MemoryStream();

                        while (n > 0)
                        {
                            n = sr.Read(tmpByte, 0, tmpByte.Length);
                            stmMemory.Write(tmpByte, 0, n);
                        }

                        string receiveData = Encoding.GetEncoding(encodingStr).GetString(stmMemory.ToArray());

                        PayLogs.WriteLogs("返回：" + postURL + "\r\n" + receiveData);

                        return receiveData;
                    }
                }
            }
            catch (Exception ex)
            {
                PayLogs.WriteLogs(ex);
                return null;
            }
        }

    }
}