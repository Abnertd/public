<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    string app, formname, frmelement, rtvalue, rturl;

    //string actURL = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        app = Request["app"];
        formname = Request["formname"];
        frmelement = Request["frmelement"];
        rtvalue = Request["rtvalue"];
        rturl = Request["rturl"];
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题页</title>
    <link href="/scripts/uploadify/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="/scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="/scripts/uploadify/jquery.uploadify.js" type="text/javascript"></script>
    <script src="/scripts/uploadify/swfobject.js" type="text/javascript"></script>
    <script type="text/javascript">
        document.domain = 'jd.com'
    </script>
</head>
<body>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#file_upload").uploadify({
                //开启调试
                'debug': false,
                //是否自动上传
                'auto': true,
                //超时时间
                'successTimeout': 99999,
                //附带值
                'formData': {
                    'app': '<%=app %>',
                    'formname': '<%=formname %>',
                    'frmelement': '<%=frmelement %>',
                    'rtvalue': '<%=rtvalue %>',
                    'rturl': '<%=rturl %>'
                },
                //flash
                'swf': "/scripts/uploadify/uploadify.swf",
                //不执行默认的onSelect事件
                //                'overrideEvents': ['onDialogClose'],
                //文件选择后的容器ID
                'queueID': 'uploadfileQueue',
                //服务器端脚本使用的文件对象的名称 $_FILES个['upload']
                'fileObjName': 'upload',
                //上传处理程序
                'uploader': '/public/UploadHandler.ashx',
                //浏览按钮的背景图片路径
                //                'buttonImage': '/scripts/uploadify/upbutton.gif',
                //浏览按钮上显示文字
                'buttonText': '上传',
                //浏览按钮的宽度
                'width': '67',
                //浏览按钮的高度
                'height': '20',
                //expressInstall.swf文件的路径。
                'expressInstall': '/scripts/uploadify/expressInstall.swf',
                //在浏览窗口底部的文件类型下拉菜单中显示的文本
                'fileTypeDesc': '支持的格式：',
                //允许上传的文件后缀
                'fileTypeExts': '*.jpg;*.jpge;*.gif;*.png;*.doc;*.docx;*.xls;*.xlsx',
                //上传文件的大小限制
                'fileSizeLimit': '3MB',
                //上传数量
                'queueSizeLimit': 1,
                //每次更新上载的文件的进展
                'onUploadProgress': function (file, bytesUploaded, bytesTotal, totalBytesUploaded, totalBytesTotal) {
                    //有时候上传进度什么想自己个性化控制，可以利用这个方法
                    //使用方法见官方说明
                },
                //选择上传文件后调用
                'onSelect': function (file) {
                },
                //返回一个错误，选择文件的时候触发
                'onSelectError': function (file, errorCode, errorMsg) {
                    switch (errorCode) {
                        case -100:
                            alert("上传的文件数量已经超出系统限制的" + $('#file_upload').uploadify('settings', 'queueSizeLimit') + "个文件！");
                            break;
                        case -110:
                            alert("文件 [" + file.name + "] 大小超出系统限制的" + $('#file_upload').uploadify('settings', 'fileSizeLimit') + "大小！");
                            break;
                        case -120:
                            alert("文件 [" + file.name + "] 大小异常！");
                            break;
                        case -130:
                            alert("文件 [" + file.name + "] 类型不正确！");
                            break;
                    }
                },
                //检测FLASH失败调用
                'onFallback': function () {
                    alert("您未安装FLASH控件，无法上传图片！请安装FLASH控件后再试。");
                },
                //上传到服务器，服务器返回相应信息到data里
                'onUploadSuccess': function (file, data, response) {                    
                    if (data == 'error') {
                        alert("文件上传失败，请重新上传！");
                    }
                    else {
                        location.href = data;
                    }

                    //alert('The file ' + file.name + ' was successfully uploaded with a response of ' + response + ':' + data);
                }
            });
        });
    </script>

   <input id="file_upload" name="file_upload" type="file" />
   <div id="uploadfileQueue"></div>
</body>
</html>
