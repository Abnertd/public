/*中正云通讯短信推送
 *2017年6月22日
 * 唐俊文
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;


    /// <summary>
    /// 短信推送
    /// </summary>
    public class SMSSend
    {
        /// <summary>
        /// 中正短信推送（发送短信）
        /// </summary>
        /// <param name="idString">登录账号</param>
        /// <param name="pwdSting">登录密码</param>
        /// <param name="mobString">发送手机号</param>
        /// <param name="msgString">发送内容</param>
        /// <param name="timeString">发送时间(值为空即时发送)</param>
        /// <returns></returns>
        public string ZhongZhengSMS(String idString,String pwdSting,String mobString,String msgString,String timeString)
        {
            return PostSend("http://service.winic.org:8009/sys_port/gateway/index.asp?", "id=" + idString + "&pwd=" + pwdSting + "&to=" + mobString + "&content=" + msgString + "&time=" +timeString +"");
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="posturlString">请求地址</param>
        /// <param name="dateString">数据</param>
        /// <returns></returns>
        public static string PostSend(string posturlString, string dateString)
        {
            try
            {

                //将strPostUrl"GB2312"解码
                byte[] byteDate = System.Text.Encoding.GetEncoding("GB2312").GetBytes(dateString);

                //准备请求
                HttpWebRequest myposthttpWebRequest = (HttpWebRequest)WebRequest.Create(posturlString);
                //设置参数
                myposthttpWebRequest.Timeout = 30000;//设置超时30S
                myposthttpWebRequest.Method = "Post";//请求方式Post
                myposthttpWebRequest.ContentType = "application/x-www-form-urlencoded";//标头
                myposthttpWebRequest.ContentLength = byteDate.Length;


                //发送数据
                using (Stream responseStream = myposthttpWebRequest.GetRequestStream())
                {
                    responseStream.Write(byteDate, 0, byteDate.Length);
                    responseStream.Close();
                }

                //接口返回数据
                using (System.Net.HttpWebResponse myhttpWebResponse = (HttpWebResponse)myposthttpWebRequest.GetResponse())
                {
                    Stream receiveStream = myhttpWebResponse.GetResponseStream();
                    StringBuilder myreadStingBuilder = new StringBuilder("");
                    Encoding myEncoding = System.Text.Encoding.GetEncoding("GB2312");
                    // Pipes the stream to a higher level stream reader with the required encoding format. 
                    using (StreamReader myStreamReader = new StreamReader(receiveStream, myEncoding))
                    {
                        Char[] readCharArray = new Char[256];
                        int countInt = myStreamReader.Read(readCharArray, 0, 256);
                        while (countInt > 0)
                        {
                            String readString = new String(readCharArray, 0, countInt);
                            myreadStingBuilder.Append(readString);
                            countInt = myStreamReader.Read(readCharArray, 0, 256);
                        }
                        myStreamReader.Close();
                    }
                    myhttpWebResponse.Close();
                    return myreadStingBuilder.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;//抛出异常
            }

        }

    }
