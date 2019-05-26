<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Page Language="C#" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title> 
</head>

<body>
   <form name="frm_add" action="" method="post">
       <textarea rows="20" cols="200" name="xmldoc">
           <%
               StringBuilder PostData = new StringBuilder();
               
               PostData.Append("<?xml version=\"1.0\" encoding=\"GBK\"?>");
               PostData.Append("<stream>");
               PostData.Append("\n");
               PostData.Append("	<action>DLSBALQR</action>");
               PostData.Append("\n");
               PostData.Append("	<userName>lierzl</userName>");
               PostData.Append("\n");
               PostData.Append("	<mainAccNo>8110701013900006690</mainAccNo>");
               PostData.Append("\n");
               //PostData.Append("	<subAccNo>3110710006721454065</subAccNo>");
               //PostData.Append("	<stt>0</stt>");
               //PostData.Append("	<startDate>20170101</startDate>");
               //PostData.Append("	<endDate>20170316</endDate>");
               PostData.Append("</stream>");
               Response.Write(PostData.ToString());
                %>          
       </textarea>
       <input type="hidden" name="action" value="process" />
       <input type="submit" value="提交" />
   </form>
    <%if(Request["action"]=="process")
      {        
        string xmlResult =  ZhongXinUtil.HttpUtil.SendRequest(Request["xmldoc"], System.Configuration.ConfigurationManager.AppSettings["zhongxin_postserver"], "gb2312");
        Response.Write(Server.HtmlEncode(xmlResult));
      } %>
</body>
</html>
