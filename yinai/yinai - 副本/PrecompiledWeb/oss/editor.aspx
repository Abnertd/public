<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <script type="text/javascript" src="/Public/ckeditor/ckeditor.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <textarea cols="80" id="editor1" name="editor1" rows="10">创建编辑框</textarea>
    <script type="text/javascript">
        CKEDITOR.replace( 'editor1' );
    </script>
    </div>
    </form>
</body>
</html>
