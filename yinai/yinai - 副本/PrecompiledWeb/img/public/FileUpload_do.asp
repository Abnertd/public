<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<%
'格式化日期
Public Function FormatDate(sDataTime, sReallyDo)
    Dim sLocale
    If Not IsDate(sDataTime) Then sDataTime = Now()
    sDataTime = CDate(sDataTime)
    Select Case sReallyDo & ""
        Case "0", "1", "2", "3", "4"
            FormatDate = FormatDateTime(sDataTime, sReallyDo)
        Case Else
            FormatDate = sReallyDo
            FormatDate = Replace(FormatDate, "YYYY", Year(sDataTime))
            FormatDate = Replace(FormatDate, "MM", Right("0" & Month(sDataTime), 2))
            FormatDate = Replace(FormatDate, "DD", Right("0" & Day(sDataTime), 2))
            FormatDate = Replace(FormatDate, "hh", Right("0" & Hour(sDataTime), 2))
            FormatDate = Replace(FormatDate, "mm", Right("0" & Minute(sDataTime), 2))
            FormatDate = Replace(FormatDate, "ss", Right("0" & Second(sDataTime), 2))
            FormatDate = Replace(FormatDate, "YY", Right(Year(sDataTime), 2))
            FormatDate = Replace(FormatDate, "M", Month(sDataTime))
            FormatDate = Replace(FormatDate, "D", Day(sDataTime))
            FormatDate = Replace(FormatDate, "h", Hour(sDataTime))
            FormatDate = Replace(FormatDate, "m", Minute(sDataTime))
            FormatDate = Replace(FormatDate, "s", Second(sDataTime))
            If InStr(1, FormatDate, "EW", 1) > 0 Then
                sLocale = GetLocale()
                SetLocale("en-gb")
                FormatDate = Replace(FormatDate, "EW", UCase(WeekdayName(Weekday(sDataTime), False)))
                FormatDate = Replace(FormatDate, "eW", WeekdayName(Weekday(sDataTime), False))
                FormatDate = Replace(FormatDate, "Ew", UCase(WeekdayName(Weekday(sDataTime), True)))
                FormatDate = Replace(FormatDate, "ew", WeekdayName(Weekday(sDataTime), True))
                SetLocale(sLocale)
            Else
                FormatDate = Replace(FormatDate, "W", WeekdayName(Weekday(sDataTime), False))
                FormatDate = Replace(FormatDate, "w", WeekdayName(Weekday(sDataTime), True))
            End If
    End Select
End Function

Function makefilename
  dim fname
  fname = FormatDate(Now(),"YYYYMMDDhhmmss")
  fname = int(fname) + int((10-1+1)*Rnd + 1)
  makefilename = fname
End Function

'获得文件的后缀名
function getFileExtName(fileName)
	dim pos
	pos=instrrev(filename,".")
	if pos>0 then 
		getFileExtName=mid(fileName,pos+1)
	else
		getFileExtName=""
	end if
	getFileExtName="."&getFileExtName
end function

function killext(filetype,byval s1) '干掉非法文件后缀
	dim allowext,sl
	'允许的文件后缀
	Select Case filetype
		Case "img"
			allowext=".jpg,.jpeg,.gif,.bmp,.png,.swf"
		Case "file"
			allowext=".rar,.zip,.chm,.doc,.xls,.pdf"
	End Select
	s1=lcase(s1)
	if len(s1)=0 then
		killext=""
	else
		if not chk(allowext,s1,",") then
			killext=".shit"
		else
			killext=s1
		end if
	end if
end function


function chk(byval s1,byval s2,byval fuhao) '检查字符串包含
	dim i,a
	chk=false
	a=split(s1,fuhao)
	for i = 0 to ubound(a)
		if trim(a(i))=trim(s2) then 
			chk=true
			exit for
		end if
	next
end function

Sub FileUpload
  'on error resume next
  Dim upload,FileImg,originaluploadimg,newuploadimg,JPEG,JpegPath
  Server.ScriptTimeout = 1000
  Set upload = Server.CreateObject("Persits.Upload")
  if err then
	Call return_result("error","error_invaildcom",rtvalue,app,formname,frmelement)
  end if
 
	upload.OverwriteFiles = False	
	upload.CreateDirectory path, True 
	upload.CreateDirectory path_temp, True 

	upload.Save() 'to memory

	Set FileImg = upload.Files("FileImg")
	if err then
		Call return_result("error","error_nofile",rtvalue,app,formname,frmelement)
	end If
	Select Case app
		Case "website_file"
			If killext("file",FileImg.ext) = ".shit" Or killext("file",FileImg.ext) = "" then
				Call return_result("error","error_filetype",rtvalue,app,formname,frmelement)
				response.End
			else
				originaluploadimg = makefilename & killext("file",FileImg.ext)
			End if
		Case Else
			If killext("img",FileImg.ext) = ".shit" Or killext("file",FileImg.ext) = "" then
				Call return_result("error","error_filetype",rtvalue,app,formname,frmelement)
				response.End
			else
				originaluploadimg = makefilename & killext("img",FileImg.ext)
			End if
	End Select
	FileImg.SaveAsVirtual path_http & originaluploadimg
	'FileImg.SaveAs path &"\"& originaluploadimg
	if err then
		Call return_result("error","error_save",rtvalue,app,formname,frmelement)
	else
		'上传完毕，进行不同的app进行后续处理
		Dim Thumbnails_Size
		Select Case app
			Case "product"
				Thumbnails_Size = 300
				'生成小图
				newuploadimg = "s_" & originaluploadimg
				Set Jpeg = Server.CreateObject("Persits.Jpeg")
				if err then
					Call return_result("error","error_thumbnailcom",rtvalue,app,formname,frmelement)
				end if
				JpegPath = path & originaluploadimg
				Jpeg.Open JpegPath
				Jpeg.Interpolation = 2
				Jpeg.Quality = 100
				If Jpeg.OriginalWidth > Thumbnails_Size or Jpeg.OriginalHeight > 300 Then
					If Jpeg.OriginalWidth >= Jpeg.OriginalHeight Then
						Jpeg.Width = Thumbnails_Size
						Jpeg.Height = Jpeg.OriginalHeight * Thumbnails_Size / Jpeg.OriginalWidth
					Else
						jpeg.Height = 300
						jpeg.Width = jpeg.OriginalWidth * 300 / jpeg.OriginalHeight
					End if
				Else
					Jpeg.Width = Jpeg.OriginalWidth
					Jpeg.Height = Jpeg.OriginalHeight
				End If
				jpeg.Crop -((Thumbnails_Size-jpeg.Width)/2),-((300-jpeg.Height)/2),jpeg.Width+(Thumbnails_Size-jpeg.Width)/2, jpeg.Height+(300-jpeg.Height)/2
				Jpeg.Save path &"\"& newuploadimg
				if err then
					Call return_result("error","error_thumbnail",rtvalue,app,formname,frmelement)
				end if
				set Jpeg = nothing
				Call return_result("success", path_http & originaluploadimg ,rtvalue,app,formname,frmelement)
			Case "supplierproduct"
				Thumbnails_Size = 300
				'生成小图
				newuploadimg = "s_" & originaluploadimg
				Set Jpeg = Server.CreateObject("Persits.Jpeg")
				if err then
					Call return_result("error","error_thumbnailcom",rtvalue,app,formname,frmelement)
				end if
				JpegPath = path & originaluploadimg
				Jpeg.Open JpegPath
				Jpeg.Interpolation = 2
				Jpeg.Quality = 100
				If Jpeg.OriginalWidth > Thumbnails_Size or Jpeg.OriginalHeight > 300 Then
					If Jpeg.OriginalWidth >= Jpeg.OriginalHeight Then
						Jpeg.Width = Thumbnails_Size
						Jpeg.Height = Jpeg.OriginalHeight * Thumbnails_Size / Jpeg.OriginalWidth
					Else
						jpeg.Height = 300
						jpeg.Width = jpeg.OriginalWidth * 300 / jpeg.OriginalHeight
					End if
				Else
					Jpeg.Width = Jpeg.OriginalWidth
					Jpeg.Height = Jpeg.OriginalHeight
				End If
				jpeg.Crop -((Thumbnails_Size-jpeg.Width)/2),-((300-jpeg.Height)/2),jpeg.Width+(Thumbnails_Size-jpeg.Width)/2, jpeg.Height+(300-jpeg.Height)/2
				Jpeg.Save path &"\"& newuploadimg
				if err then
					Call return_result("error","error_thumbnail",rtvalue,app,formname,frmelement)
				end if
				set Jpeg = nothing
				Call return_result("success", path_http & originaluploadimg ,rtvalue,app,formname,frmelement)
			Case "AD"
				Call return_result("success", path_http & originaluploadimg ,rtvalue,app,formname,frmelement)
			Case "preview"
				Call return_result("success", path_http & originaluploadimg ,rtvalue,app,formname,frmelement)
			Case "category"
				Thumbnails_Size = 100
				Set Jpeg = Server.CreateObject("Persits.Jpeg")
				if err then
					Call return_result("error","error_thumbnailcom",rtvalue,app,formname,frmelement)
				end if
				JpegPath = path & originaluploadimg
				Jpeg.Open JpegPath
				If Jpeg.OriginalWidth > Thumbnails_Size or Jpeg.OriginalHeight > Thumbnails_Size Then
					'生成小图
					newuploadimg = makefilename & killext("img",FileImg.ext)
					
					If Jpeg.OriginalWidth >= Jpeg.OriginalHeight Then
						Jpeg.Width = Thumbnails_Size
						Jpeg.Height = Jpeg.OriginalHeight * Thumbnails_Size / Jpeg.OriginalWidth
					Else
						jpeg.Height = Thumbnails_Size
						jpeg.Width = jpeg.OriginalWidth * Thumbnails_Size / jpeg.OriginalHeight
					End if
					jpeg.Interpolation = 2
				    jpeg.Quality = 100
					Jpeg.Save path &"\"& newuploadimg
					if err then
						Call return_result("error","error_thumbnail",rtvalue,app,formname,frmelement)
					end if
					'删除原有文件
					upload.DeleteFile path & originaluploadimg
					Call return_result("success", path_http & newuploadimg ,rtvalue,app,formname,frmelement)
				Else
					Call return_result("success", path_http & originaluploadimg ,rtvalue,app,formname,frmelement)
				End If
				set Jpeg = Nothing
			Case "friendlylink"
				Thumbnails_Size = 120
				Set Jpeg = Server.CreateObject("Persits.Jpeg")
				if err then
					Call return_result("error","error_thumbnailcom",rtvalue,app,formname,frmelement)
				end if
				JpegPath = path & originaluploadimg
				Jpeg.Open JpegPath
				If Jpeg.OriginalWidth > Thumbnails_Size or Jpeg.OriginalHeight > Thumbnails_Size Then
					'生成小图
					newuploadimg = makefilename & killext("img",FileImg.ext)
					
					If Jpeg.OriginalWidth >= Jpeg.OriginalHeight Then
						Jpeg.Width = Thumbnails_Size
						Jpeg.Height = Jpeg.OriginalHeight * Thumbnails_Size / Jpeg.OriginalWidth
					Else
						jpeg.Height = Thumbnails_Size
						jpeg.Width = jpeg.OriginalWidth * Thumbnails_Size / jpeg.OriginalHeight
					End if
					jpeg.Interpolation = 2
				    jpeg.Quality = 100
					Jpeg.Save path &"\"& newuploadimg
					if err then
						Call return_result("error","error_thumbnail",rtvalue,app,formname,frmelement)
					end if
					'删除原有文件
					upload.DeleteFile path & originaluploadimg
					Call return_result("success", path_http & newuploadimg ,rtvalue,app,formname,frmelement)
				Else
					Call return_result("success", path_http & originaluploadimg ,rtvalue,app,formname,frmelement)
				End If
				set Jpeg = Nothing
			Case "brand"
				Thumbnails_width = 86
				Thumbnails_height = 42
				Set Jpeg = Server.CreateObject("Persits.Jpeg")
				if err then
					Call return_result("error","error_thumbnailcom",rtvalue,app,formname,frmelement)
				end if
				JpegPath = path & originaluploadimg
				Jpeg.Open JpegPath
				If Jpeg.OriginalWidth > Thumbnails_width or Jpeg.OriginalHeight > Thumbnails_height Then
					'生成小图
					newuploadimg = makefilename & killext("img",FileImg.ext)
					
					If Jpeg.OriginalWidth >= Jpeg.OriginalHeight Then
						Jpeg.Width = Thumbnails_width
						Jpeg.Height = Jpeg.OriginalHeight * Thumbnails_height / Jpeg.OriginalHeight
					Else
						jpeg.Height = Thumbnails_height
						jpeg.Width = jpeg.OriginalWidth * Thumbnails_width / jpeg.OriginalWidth
					End if
					jpeg.Interpolation = 2
				    jpeg.Quality = 100
					Jpeg.Save path &"\"& newuploadimg
					if err then
						Call return_result("error","error_thumbnail",rtvalue,app,formname,frmelement)
					end if
					'删除原有文件
					upload.DeleteFile path & originaluploadimg
					Call return_result("success", path_http & newuploadimg ,rtvalue,app,formname,frmelement)
				Else
					Call return_result("success", path_http & originaluploadimg ,rtvalue,app,formname,frmelement)
				End If
				set Jpeg = Nothing
				
			Case "type"
				Thumbnails_Size = 80
				Set Jpeg = Server.CreateObject("Persits.Jpeg")
				if err then
					Call return_result("error","error_thumbnailcom",rtvalue,app,formname,frmelement)
				end if
				JpegPath = path & originaluploadimg
				Jpeg.Open JpegPath
				If Jpeg.OriginalWidth > Thumbnails_Size or Jpeg.OriginalHeight > Thumbnails_Size Then
					'生成小图
					newuploadimg = makefilename & killext("img",FileImg.ext)
					
					If Jpeg.OriginalWidth >= Jpeg.OriginalHeight Then
						Jpeg.Width = Thumbnails_Size
						Jpeg.Height = Jpeg.OriginalHeight * Thumbnails_Size / Jpeg.OriginalWidth
					Else
						jpeg.Height = Thumbnails_Size
						jpeg.Width = jpeg.OriginalWidth * Thumbnails_Size / jpeg.OriginalHeight
					End if
					jpeg.Interpolation = 2
				    jpeg.Quality = 100
					Jpeg.Save path &"\"& newuploadimg
					if err then
						Call return_result("error","error_thumbnail",rtvalue,app,formname,frmelement)
					end if
					'删除原有文件
					upload.DeleteFile path & originaluploadimg
					Call return_result("success", path_http & newuploadimg ,rtvalue,app,formname,frmelement)
				Else
					Call return_result("success", path_http & originaluploadimg ,rtvalue,app,formname,frmelement)
				End If
				set Jpeg = Nothing
		    case "productintro"
		        Call return_result("success", path_http & originaluploadimg ,rtvalue,app,formname,frmelement)
			Case "content"
				Call return_result("success", path_http & originaluploadimg ,rtvalue,app,formname,frmelement)
			Case "article"
				Call return_result("success", path_http & originaluploadimg ,rtvalue,app,formname,frmelement)
			Case "promotion"
				Call return_result("success", path_http & originaluploadimg ,rtvalue,app,formname,frmelement)
			Case "seller"
				Thumbnails_Size = 140
				Set Jpeg = Server.CreateObject("Persits.Jpeg")
				if err then
					Call return_result("error","error_thumbnailcom",rtvalue,app,formname,frmelement)
				end if
				JpegPath = path & originaluploadimg
				Jpeg.Open JpegPath
				If Jpeg.OriginalWidth > Thumbnails_Size or Jpeg.OriginalHeight > Thumbnails_Size Then
					'生成小图
					newuploadimg = makefilename & killext("img",FileImg.ext)
					
					If Jpeg.OriginalWidth >= Jpeg.OriginalHeight Then
						Jpeg.Width = Thumbnails_Size
						Jpeg.Height = Jpeg.OriginalHeight * Thumbnails_Size / Jpeg.OriginalWidth
					Else
						jpeg.Height = Thumbnails_Size
						jpeg.Width = jpeg.OriginalWidth * Thumbnails_Size / jpeg.OriginalHeight
					End if
					jpeg.Interpolation = 2
				    jpeg.Quality = 100
					Jpeg.Save path &"\"& newuploadimg
					if err then
						Call return_result("error","error_thumbnail",rtvalue,app,formname,frmelement)
					end if
					'删除原有文件
					upload.DeleteFile path & originaluploadimg
					Call return_result("success", path_http & newuploadimg ,rtvalue,app,formname,frmelement)
				Else
					Call return_result("success", path_http & originaluploadimg ,rtvalue,app,formname,frmelement)
				End If
				set Jpeg = Nothing
			Case "pextend"
				Call return_result("success", path_http & originaluploadimg ,rtvalue, app, formname, frmelement)
			Case else
				Call return_result("error","error_invaildapp",rtvalue,app,formname,frmelement)
		End Select
	end if

	set upload=nothing
End sub

Function return_result(msgtype,msg,rtvalue,app,formname,frmelement)
	Response.redirect (rturl & "?msgtype="&msgtype&"&msg="&msg&"&rtvalue="&rtvalue&"&app="&app&"&formname="&formname&"&frmelement="&frmelement&"")
	Response.end
End Function

Dim app,formname,frmelement,rtvalue,path,path_temp,path_http
app=trim(Request("app"))
formname=trim(Request("formname"))
frmelement=trim(Request("frmelement"))
rtvalue=trim(Request("rtvalue"))
rturl=trim(Request("rturl"))

path = Server.MapPath ("/") '上传文件路径
path_temp = Server.MapPath ("/temp/") '临时上传路径

select case app
case "product"
	path = path & "\product\"
	path_http = "/product/"
case "supplierproduct"
	path = path & "\product\"
	path_http = "/product/"
case "productintro"
	path = path & "\product\"
	path_http = "/product/"
case "AD"
	path = path & "\XC\"
	path_http = "/XC/"
Case "promotion"
	path = path & "\promotion\"
	path_http = "/promotion/"
Case "category"
	path = path & "\category\"
	path_http = "/category/"
Case "friendlylink"
	path = path & "\friendlylink\"
	path_http = "/friendlylink/"	
Case "brand"
	path = path & "\brand\"
	path_http = "/brand/"
Case "type"
	path = path & "\type\"
	path_http = "/type/"
Case "preview"
	path = path & "\preview\"
	path_http = "/preview/"
Case "content"
	path = path & "\content\"
	path_http = "/content/"
Case "article"
	path = path & "\content\"
	path_http = "/content/"
Case "seller"
	path = path & "\seller\"
	path_http = "/seller/"
Case "pextend"
	path = path & "\pextend\"
	path_http = "/pextend/"
case else
	Call return_result("error","error_invaildapp",rtvalue,app,formname,frmelement)
end Select
%>
<html>
<head>
<title>Upload</title>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
<style type="text/css">
BODY { PADDING-RIGHT: 0px; PADDING-LEFT: 0px; BACKGROUND: #ffffff; MARGIN: 0px; PADDING-TOP: 0px; }
.form { font-size: 12px;}
</style>
</head>
<body bgcolor="#FFFFFF" text="#000000">
<table width="100%" height="25" align="center" bgcolor="#FFFFFF">
  <tr> 
    <td width="100%" height="100%" valign="middle" align="left" height="25" bgcolor="#FFFFFF"><% FileUpload %></td>
  </tr>
</table>
</body>
</html>