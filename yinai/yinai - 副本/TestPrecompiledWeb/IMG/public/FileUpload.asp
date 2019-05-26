<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<%
app=trim(Request("app"))
formname=trim(Request("formname"))
frmelement=trim(Request("frmelement"))
rtvalue=trim(Request("rtvalue"))
rturl=trim(Request("rturl"))

Function Tempform
	response.write("<form name=tempform method=post enctype=multipart/form-data action=FileUpload_do.asp?app="&app&"&formname="&formname&"&frmelement="&frmelement&"&rtvalue="&rtvalue&"&rturl="&rturl&">")
	response.write("<input type=file ID=FileImg name=FileImg size=30>")
	Response.write("<input type=button value=上传 style=""width:40px;""  onclick=javascript:uploadfilecheck('FileImg');>")
	response.write("</form>")
End Function
%>
<html>
<head>
<title>Upload</title>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<style type="text/css">
body {
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
}
body,td,th {
	font-family: Tahoma, "宋体";
	font-size: 12px;
	line-height: 150%;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
	margin-left: 0px;
}
form{
	margin:0px;
	padding: 0px;
}
input {
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 12px;
	color:#4C4C4;
	width:200px;
}

</style>
<SCRIPT LANGUAGE="JavaScript">
<!--
function isBlank( s ) //是否是空白字符串
{
  var len=s.length;
  for( i = 0; i < len; i ++ )
  {
    if( s.charAt(i) != " " )
    return false;
  }
  return true;
}

function uploadfilecheck(frmelement){
sfile=document.getElementById(frmelement).value;
if (isBlank(sfile)){
	alert("请选择要上传的图片");
	return false;
}else{
	document.tempform.submit();
	return true;
}
}
//-->
</SCRIPT>
</head>
<body>
<table width="100%" align="center" bgcolor="#ffffff" >
  <tr> 
    <td width="100%" valign="middle" align="left" height="25"><%Tempform%></td>
  </tr>
</table>
</body>
</html>