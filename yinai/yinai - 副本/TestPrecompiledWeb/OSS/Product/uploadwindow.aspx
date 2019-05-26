<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<% string elementName = Request.QueryString["elementName"].ToString();%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>上传图片</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script type="text/javascript">

    var webURL = '<% =Application["upload_server_url"]%>';
    
	function InputParentImg(imgpath){
	    window.opener.MM_findObj('<%=elementName %>').value = imgpath;
	    window.opener.MM_findObj('img_<%=elementName %>').src = webURL + imgpath;
		window.close();
	}
</script>
</head>
<body>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
  <tr>
   <td class="cell_title" style="width:100px;" height="100">上传图片</td>
   <td class="cell_content"><iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=pextend&formname=formadd&frmelement=<%=elementName %>&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe></td>
  </tr>
</table>
</body>
</html>
