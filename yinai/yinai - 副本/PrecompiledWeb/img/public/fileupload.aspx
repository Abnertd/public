<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    string app, formname, frmelement, rtvalue, rturl;

    string actURL = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app = Request["app"];
        formname = Request["formname"];
        frmelement = Request["frmelement"];
        rtvalue = Request["rtvalue"];
        rturl = Request["rturl"];
        
        actURL = "fileupload_do.aspx?app=" + app + "&formname=" + formname + "&frmelement=" + frmelement + "&rtvalue=" + rtvalue + "&rturl=" + rturl;
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题页</title>
    <script type="text/javascript">
        function checkForm()
        {
            if (form1.File1.value.length == 0)
            {
                alert("请选择上传文件");
                return false;
            }
            else
            {
                return true;
            }
        }
    </script>
    <style type="text/css">
        body {margin:0px;}
        body,td {
	        font-family: "微软雅黑";
	        font-size: 12px;
	        line-height: 150%;
	        margin:0px;
        }
        form{
	        margin:0px;
	        padding:0px;
        }
        input {
	        font-family: Verdana, Arial, Helvetica, sans-serif;
	        font-size: 12px;
	        width:200px;
        }
    </style>
</head>
<body>
    <form enctype="multipart/form-data" id="form1" method="post" action="<% =actURL%>" onsubmit="return checkForm()">
    <table cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td><input type="file" id="File1" name="File1"/></td>
            <td><input type="submit" id="submit" name="submit" value="上传" style="width:40px;"/></td>
        </tr>
    </table>
    </form>
</body>
</html>
