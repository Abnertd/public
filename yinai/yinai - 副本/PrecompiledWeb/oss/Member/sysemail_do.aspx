<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%
    Server.ScriptTimeout = 1000;
  Member memberclass;
  memberclass = new Member();


  memberclass.Member_Sysemail_Send();

     %>


