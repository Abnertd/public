using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;

namespace ZhongXinUtil
{
    /// <summary>
    /// PayLogs 的摘要说明
    /// </summary>
    public class PayLogs
    {
        public PayLogs()
        {

        }

        public static void WriteLogs(string strLog)
        {
            if (ConfigurationManager.AppSettings["zhongxin_log"] == null || ConfigurationManager.AppSettings["zhongxin_log"].Length == 0)
            {
                return;
            }

            File.AppendAllText(ConfigurationManager.AppSettings["zhongxin_log"]  +"\\平台→银行log" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "\r\n--------------" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-------------\r\n" + strLog);
        }

        public static void WriteLogs(Exception ex)
        {
            PayLogs.WriteLogs("错误记录：" + ex.Message + "\r\n" + ex.StackTrace);
        }

        public static void WriteLogs3(string strLog)
        {
            if (ConfigurationManager.AppSettings["zhongxin_log"] == null || ConfigurationManager.AppSettings["zhongxin_log"].Length == 0)
            {
                return;
            }

            File.AppendAllText(ConfigurationManager.AppSettings["zhongxin_log"]  +"\\银行→平台log" + DateTime.Now.ToString("yyyyMMdd") + ".txt", "\r\n--------------" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "-------------\r\n" + strLog);
        }

    }
}