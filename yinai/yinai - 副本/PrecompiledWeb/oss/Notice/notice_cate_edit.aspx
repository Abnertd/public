<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private NoticeCate myApp;
    private ITools tools;

    private string Notice_Cate_Name, Notice_Cate_Site;
    private int Notice_Cate_ID, Notice_Cate_Sort;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("705ff0e0-daa6-4649-bf27-20142c21ba9e");
        
        myApp = new NoticeCate();
        tools = ToolsFactory.CreateTools();

        Notice_Cate_ID = tools.CheckInt(Request.QueryString["notice_cate_id"]);
        NoticeCateInfo entity = myApp.GetNoticeCateByID(Notice_Cate_ID);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else {
            Notice_Cate_ID = entity.Notice_Cate_ID;
            Notice_Cate_Name = entity.Notice_Cate_Name;
            Notice_Cate_Sort = entity.Notice_Cate_Sort;
            Notice_Cate_Site = entity.Notice_Cate_Site;
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">添加公告类别</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="notice_cate_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">类别名称</td>
          <td class="cell_content"><input name="Notice_Cate_Name" type="text" id="Notice_Cate_Name" size="50" maxlength="50" value="<% =Notice_Cate_Name%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">类别排序</td>
          <td class="cell_content"><input name="Notice_Cate_Sort" type="text" id="Notice_Cate_Sort" size="10" maxlength="10" value="<% =Notice_Cate_Sort%>"/>
            <span class="tip">数字越小越靠前</span></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="renew" />
            <input type="hidden" id="Notice_Cate_ID" name="Notice_Cate_ID" value="<% =Notice_Cate_ID%>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='notice_cate_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>