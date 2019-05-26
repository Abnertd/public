<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <style type="text/css">
        
        body { margin:0; padding:0; }
        .up_success{
	        font-size: 12px;
	        line-height: 150%;
	        font-weight: bold;
	        color: #009900;
	        text-decoration: none;
            margin-top:18px;
        }
    </style>
    <script src="/Scripts/jquery.js" type="text/javascript"></script> 
    <script type="text/javascript">
    function hide_cert()
{
    $('#mask_div',parent.document).hide();
    $('#upload_div',parent.document).hide();
    $("body",parent.document).css({overflow:""}); 
}
    </script>
</head>
<body>

<%
    ITools tools = ToolsFactory.CreateTools();
    string msgtype, msg, rtvalue, app, formname, frmelement, s_msg;

    msgtype = tools.CheckStr(Request.QueryString["msgtype"]);
    msg = tools.CheckStr(Request.QueryString["msg"]);
    rtvalue = tools.CheckStr(Request.QueryString["rtvalue"]);
    app = tools.CheckStr(Request.QueryString["app"]);
    formname = tools.CheckStr(Request.QueryString["formname"]);
    frmelement = tools.CheckStr(Request.QueryString["frmelement"]);
    s_msg = Application["upload_server_url"] + msg.Substring(0, msg.LastIndexOf("/") + 1) + "" + msg.Substring(msg.LastIndexOf("/") + 1);

    switch (msgtype)
    {
        case "error":
            Response.Write(msg);
            break;
        case "success":
            if (rtvalue == "1")
            {
                switch (app)
                {
                    case "shopcert":
                        Response.Write("<script type=\"text/javascript\">");
                        Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                        Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" + s_msg + "';");
                        Response.Write("parent.$('#td_upload').hide();");
                        Response.Write("</script>");
                        break;
                    case "sealimg":
                        Response.Write("<script type=\"text/javascript\">");
                        Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                        Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" + s_msg + "';");
                        Response.Write("location.href=\"" + Application["upload_server_url"] + "Public/uploadify.aspx?App=" + app + "&formname=" + formname + "&frmelement=" + frmelement + "&rtvalue=" + rtvalue + "&rturl=" + Application["upload_server_return_WWW"] + "\";");
                        Response.Write("</script>");
                        break;
                    case "headimg":
                        Response.Write("<script type=\"text/javascript\">");
                        Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                        Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" + s_msg + "';");
                        Response.Write("location.href=\"" + Application["upload_server_url"] + "Public/uploadify.aspx?App=" + app + "&formname=" + formname + "&frmelement=" + frmelement + "&rtvalue=" + rtvalue + "&rturl=" + Application["upload_server_return_WWW"] + "\";");
                        Response.Write("</script>");
                        break;
                    case "accompanyingimg":
                        Response.Write("<script type=\"text/javascript\">");
                        Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                        Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" + s_msg + "';");
                        Response.Write("parent.$('#td_upload').hide();");
                        Response.Write("</script>");
                        break;
                    case "shoppages":
                        Response.Write("<script type=\"text/javascript\">");
                        foreach (string str in msg.Split('|'))
                        {
                            if (str.Length > 0)
                            {
                                //Response.Write("parent.KindEditor.instances." + frmelement + ".insertHtml('<img src=\"" + Application["upload_server_url"] + str + "\">');");
                                Response.Write("parent." + frmelement + "Editor.insertHtml('<img src=\"" + Application["upload_server_url"] + str + "\">');");
                            }
                        }
                        Response.Write("location.href=\"" + Application["upload_server_url"] + "Public/uploadify.aspx?App=" + app + "&formname=" + formname + "&frmelement=" + frmelement + "&rtvalue=" + rtvalue + "&rturl=" + Application["Upload_Server_Return_WWW"] + "\";");
                        Response.Write("</script>");
                        break;
                    case "content":
                        Response.Write("<script type=\"text/javascript\">");
                        foreach (string str in msg.Split('|'))
                        {
                            if (str.Length > 0)
                            {
                                //Response.Write("parent.CKEDITOR.instances." + frmelement + ".insertHtml('<img src=\"" + Application["upload_server_url"] + str + "\">');");
                                Response.Write("parent." + frmelement + "Editor.insertHtml('<img src=\"" + Application["upload_server_url"] + str + "\">');");
                            }
                        }
                        Response.Write("location.href=\"" + Application["upload_server_url"] + "Public/uploadify.aspx?App=" + app + "&formname=" + formname + "&frmelement=" + frmelement + "&rtvalue=" + rtvalue + "&rturl=" + Application["Upload_Server_Return_WWW"] + "\";");
                        Response.Write("hide_cert();</script>");
                        break;
                    case "product":
                        Response.Write("<script type=\"text/javascript\">");
                        Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                        Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" + s_msg + "';");
                        Response.Write("parent.$('#td_upload').hide();");
                        Response.Write("</script>");
                        break;
                    case "merchants":
                        Response.Write("<script type=\"text/javascript\">");
                        Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                        Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" + s_msg + "';");
                        Response.Write("parent.$('#tr_" + frmelement + "').show();");
                        Response.Write("</script>");
                        break;
                    case "supplier":
                        Response.Write("<script type=\"text/javascript\">");
                        Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                        Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" + s_msg + "';");
                        Response.Write("location.href=\"" + Application["upload_server_url"] + "Public/uploadify.aspx?App=" + app + "&formname=" + formname + "&frmelement=" + frmelement + "&rtvalue=" + rtvalue + "&rturl=" + Application["upload_server_return_WWW"] + "\";");
                        Response.Write("</script>");
                        break;
                    case "shopbanner":
                        Response.Write("<script type=\"text/javascript\">");
                        Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                        Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" + Application["upload_server_url"] + msg + "';");
                        Response.Write("</script>");
                        break;
                    case "AD":
                        Response.Write("<script type=\"text/javascript\">");
                        Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                        Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" + Application["upload_server_url"] + msg + "';");
                        Response.Write("parent.$('#tr_" + frmelement + "').show();");
                        Response.Write("</script>");
                        break;
                    case "attachment":
                        Response.Write("<script type=\"text/javascript\">");
                        Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                        //Response.Write("location.href=\"" + Application["upload_server_url"] + "Public/uploadify.aspx?App=" + app + "&formname=" + formname + "&frmelement=" + frmelement + "&rtvalue=" + rtvalue + "&rturl=" + Application["Upload_Server_Return_WWW"] + "\";");
                        Response.Write("</script>");
                        break;
                    case "productgifts":
                        Response.Write("<script type=\"text/javascript\">");
                        Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                        Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" + Application["upload_server_url"] + msg + "';");
                        Response.Write("parent.$('#tr_" + frmelement + "').show();");
                        Response.Write("</script>");
                        break;

                    case "Bid":
                        Response.Write("<script type=\"text/javascript\">");
                        Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                        Response.Write("</script>");
                        break;

                    case "BidProduct":
                        Response.Write("<script type=\"text/javascript\">");
                        Response.Write("parent.document." + formname + "." + frmelement + ".value='" + msg + "';");
                        Response.Write("parent.document." + formname + ".img_" + frmelement + ".src='" + Application["upload_server_url"] + msg + "';");
                        Response.Write("parent.$('#tr_" + frmelement + "').show();");
                        Response.Write("</script>");
                        break;
                }
                Response.Write("<span class=\"up_success\">上传成功！</span>");
            }
            break;
    }
%>

</body>
</html>
