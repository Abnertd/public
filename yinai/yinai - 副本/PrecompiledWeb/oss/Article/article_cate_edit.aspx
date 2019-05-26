<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private ArticleCate myApp;
    private ITools tools;

    private string Article_Cate_Name, Article_Cate_Site;
    private int Article_Cate_ID, Article_Cate_Sort, Cate_ParentID;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("8e2eb41c-060b-4a1c-9c7c-403d6f1072fa");
        
        myApp = new ArticleCate();
        tools = ToolsFactory.CreateTools();

        Article_Cate_ID = tools.CheckInt(Request.QueryString["Article_cate_id"]);
        ArticleCateInfo entity = myApp.GetArticleCateByID(Article_Cate_ID);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else {
            Article_Cate_ID = entity.Article_Cate_ID;
            Article_Cate_Name = entity.Article_Cate_Name;
            Article_Cate_Sort = entity.Article_Cate_Sort;
            Article_Cate_Site = entity.Article_Cate_Site;
            Cate_ParentID = entity.Article_Cate_ParentID;
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
      <td class="content_title">修改文章类别</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="article_cate_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">类别名称</td>
          <td class="cell_content"><input name="Article_Cate_Name" type="text" id="Article_Cate_Name" size="50" maxlength="50" value="<% =Article_Cate_Name%>" /></td>
        </tr>
         <tr>
          <td class="cell_title">上级分类</td>
          <td class="cell_content">
          <select name="Article_Cate_ParentID" id="Cate_ParentID">
            <option value="0">选择父类别</option>
            <% myApp.getArticleCate(0, 0, Cate_ParentID);%>
          </select></td>
        </tr>
        <tr>
          <td class="cell_title">类别排序</td>
          <td class="cell_content"><input name="Article_Cate_Sort" type="text" id="Article_Cate_Sort" size="10" maxlength="10" value="<% =Article_Cate_Sort%>"/>
            <span class="tip">数字越小越靠前</span></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="renew" />
            <input type="hidden" id="Article_Cate_ID" name="Article_Cate_ID" value="<% =Article_Cate_ID%>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='Article_cate_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>