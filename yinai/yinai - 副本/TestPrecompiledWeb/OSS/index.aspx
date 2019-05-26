<%@ Page Language="C#" CodePage="65001"%>

<% 
    Public.CheckLogin("all");

    string site = Request.QueryString["site"];

    if (site != null && site.Length > 0)
    {
        Session["CurrentSite"] = site;
        //初始化系统变量
        Config config = new Config();
        config.Sys_UpdateApplication(Session["CurrentSite"].ToString());
        config = null;
    }
    %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Frameset//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
<title>运营支撑系统 Operation Support System</title>
<link href="/public/style.css" rel="stylesheet" type="text/css" />
</head>
<frameset rows="130,*" cols="*" frameborder="NO" border="0" framespacing="0"> 
  <frame name="top" scrolling="NO" noresize src="/public/top.aspx" >
  <frameset name="mainframeset" id="mainframeset" cols="210,*" frameborder="NO" border="0" framespacing="0"> 
    <frame name="left" scrolling="yes" src="/public/left.aspx" />
    <frame name="main" src="/main.aspx" scrolling="YES" />
  </frameset>
</frameset>
<noframes><body bgcolor="#FFFFFF" text="#000000">
您的浏览器不支持，请升级浏览器！
</body></noframes>
</html>