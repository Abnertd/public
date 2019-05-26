<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">页面标题</td>
    </tr>
    <tr>
      <td class="content_content"><table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">输入框</td>
          <td class="cell_content"><input name="abc" type="text" id="abc" size="50" maxlength="50" /></td>
        </tr>
        <tr>
          <td class="cell_title">选择框</td>
          <td class="cell_content"><select name="abc2" id="abc2">
            <option value="0">选项</option>
          </select>          </td>
        </tr>
        <tr>
          <td class="cell_title">日期</td>
          <td class="cell_content"><input type="text" class="input_calendar" name="datefrom" id="datefrom" maxlength="10" readonly="readonly" value="" />
          	<script>$("#datefrom").datepicker({numberOfMonths:2});</script>
          </td>
        </tr>
        <tr>
          <td class="cell_title">HTML介绍</td>
          <td class="cell_content">
          	<textarea cols="50" id="content" name="content" rows="5">编辑框内容</textarea>
			<script type="text/javascript">
                CKEDITOR.replace( 'content' );
            </script>
          </td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right"><input name="save" type="button" class="bt_orange" id="save" value="主要按钮" /> <input name="button" type="button" class="bt_grey" id="button" value="次要按钮" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" /></td>
          </tr>
        </table></td>
    </tr>
  </table>
</div>
</body>
</html>
