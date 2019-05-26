<%@ Page Language="C#" %>
<% Contract MyApp = new Contract();
   Public.CheckLogin("all");
   
    %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>合同查看</title>

</head>
<body>

    <% MyApp.Contract_View();
       if (Request["action"] != "print")
       {
           Response.Write("<style type=\"text/css\">td{font-size:14px;}div{font-size:14px;}.bill td{line-height:20px;font-size:14px;}</style>");
       }
        %>
        <script type="text/javascript">            pagesetup_null();</script>
</body>
</html>
