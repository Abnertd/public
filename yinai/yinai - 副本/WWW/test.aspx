<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Page Language="C#" %>

<%
    MsgAPI.Service1SoapClient ws = new MsgAPI.Service1SoapClient();
    string resutl=ws.SendMessages("lifengyue", "lifengyue", "13601212969", "订单号:123553", "");

    Response.Write(resutl);
     %>
