<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private Article myApp;
    private ITools tools;
    
    private ArticleCate myAppC;

    private string Article_Title, Article_Content, Article_Img, Article_Site, Article_Intro, Article_Keyword, Article_Keyword1, Article_Keyword2, Article_Keyword3, Article_Keyword4, Article_Keyword5, Article_Source, Article_Author;
    private int Article_ID, Article_Cate, Article_IsRecommend, Article_IsAudit,  Article_Sort;
    private DateTime Article_Addtime;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("1daab676-20b6-4073-af76-132ee8874556");

        myApp = new Article();
        myAppC = new ArticleCate();
        tools = ToolsFactory.CreateTools();

        Article_ID = tools.CheckInt(Request.QueryString["Article_id"]);
        ArticleInfo entity = myApp.GetArticleByID(Article_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Article_ID = entity.Article_ID;
            Article_Cate = entity.Article_CateID;
            Article_IsRecommend = entity.Article_IsRecommend;
            Article_IsAudit = entity.Article_IsAudit;
            Article_Img = entity.Article_Img;
            Article_Source = entity.Article_Source;
            Article_Author = entity.Article_Author;
            Article_Intro = entity.Article_Intro;
            Article_Sort = entity.Article_Sort;
            Article_Title = entity.Article_Title;
            Article_Content = entity.Article_Content;
            Article_Addtime = entity.Article_Addtime;
            Article_Site = entity.Article_Site;
            Article_Keyword = entity.Article_Keyword;
            if (Article_Keyword.Length > 0)
            {
                string[] keyword = Article_Keyword.Split('|');
                if (keyword.Length == 5)
                {
                    Article_Keyword1 = keyword[0];
                    Article_Keyword2 = keyword[1];
                    Article_Keyword3 = keyword[2];
                    Article_Keyword4 = keyword[3];
                    Article_Keyword5 = keyword[4];
                }
            }
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/common.js" type="text/javascript"></script>
<script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
<script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>
        <script src="../Scripts/product.js"></script>
    <script type="text/javascript">
        function change_maincate(target_div, obj) {

            $("#" + target_div).load("/article/article_do.aspx?action=change_maincate&target=" + target_div + "&cate_id=" + $("#" + obj).val() + "&timer=" + Math.random());
        }
    </script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">修改文章</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="Article_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">文章标题</td>
          <td class="cell_content"><input name="Article_Title" type="text" id="Article_Title" size="50" maxlength="50" value="<% =Article_Title%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">文章类别</td>
          <td class="cell_content">
          <span id="main_cate"><%=myAppC.Article_Category_Select(Article_Cate, "main_cate")%></span></td>
        </tr>
        <tr>
          <td class="cell_title">文章来源</td>
          <td class="cell_content"><input name="Article_Source" type="text" value="<%=Article_Source %>" id="Article_Source" size="50" maxlength="50" /></td>
        </tr>
        <tr>
          <td class="cell_title">文章作者</td>
          <td class="cell_content"><input name="Article_Author" type="text" id="Article_Author" value="<%=Article_Author %>" size="50" maxlength="50" /></td>
        </tr>
        <tr>
          <td class="cell_title">文章摘要</td>
          <td class="cell_content"><textarea name="Article_Intro" cols="50" rows="5"><%=Article_Intro%></textarea></td>
        </tr>
        <tr>
          <td class="cell_title">预览图片</td>
          <td class="cell_content"><iframe id="iframe2" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=article&formname=formadd&frmelement=Article_Img&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe></td>
        </tr>
        <tr id="tr_Article_Img" <%if(Article_Img.Length==0){Response.Write("style='display:none'");} %>>
          <td class="cell_title"></td>
          <td class="cell_content"><img src="<% =Application["upload_server_url"]+Article_Img%>" id="img_Article_Img" /><input name="Article_Img" value="<%=Article_Img %>" type="hidden" id="Article_Img" /></td>
        </tr>
         <tr>
          <td class="cell_title">上传图片</td>
          <td class="cell_content"><iframe id="iframe1" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=content&formname=formadd&frmelement=Article_Content&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe></td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">文章内容</td>
          <td class="cell_content">
            <textarea cols="80" id="Article_Content" name="Article_Content" rows="16"><% =Article_Content%></textarea>
            <script type="text/javascript">
                var Article_ContentEditor;
                KindEditor.ready(function (K) {
                    Article_ContentEditor = K.create('#Article_Content', {
                        width: '100%',
                        height: '500px',
                        filterMode: false,
                        afterBlur: function () { this.sync(); }
                    });
                });
            </script>
          </td>
        </tr>
        <tr>
          <td class="cell_title">热点</td>
          <td class="cell_content"><input name="Article_IsRecommend" type="radio" id="Article_IsRecommend" value="1" <% =Public.CheckedRadio(Article_IsRecommend.ToString(), "1")%>/>是 <input type="radio" name="Article_IsRecommend" id="Article_IsRecommend1" value="0" <% =Public.CheckedRadio(Article_IsRecommend.ToString(), "0")%>/>否</td>
        </tr>
        <tr>
          <td class="cell_title">是否显示</td>
          <td class="cell_content"><input name="Article_IsAudit" type="radio" id="Article_IsAudit1" value="1" <% =Public.CheckedRadio(Article_IsAudit.ToString(), "1")%>/>是 <input type="radio" name="Article_IsAudit" id="Article_IsAudit2" value="0" <% =Public.CheckedRadio(Article_IsAudit.ToString(), "0")%>/>否</td>
        </tr>

          <tr>
          <td class="cell_title">排序</td>
          <td class="cell_content"><input name="Article_Sort" type="text" id="Article_Sort" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="<%=Article_Sort %>" size="10" maxlength="10" />
            <span class="tip">数字越小越靠前</span></td>
        </tr>


        <tr>
          <td class="cell_title">关键字
            </td>
          <td class="cell_content"><input name="Article_Keyword1" type="text" id="Article_Keyword1" size="20" maxlength="200" value="<%=Article_Keyword1 %>"/>&nbsp;&nbsp;<input name="Article_Keyword2" type="text" id="Article_Keyword2" size="20" maxlength="200" value="<%=Article_Keyword2 %>"/>&nbsp;&nbsp;<input name="Article_Keyword3" type="text" id="Article_Keyword3" size="20" maxlength="200" value="<%=Article_Keyword3 %>"/>&nbsp;&nbsp;<input name="Article_Keyword4" type="text" id="Article_Keyword4" size="20" maxlength="200" value="<%=Article_Keyword4 %>"/>&nbsp;&nbsp;<input name="Article_Keyword5" type="text" id="Article_Keyword5" size="20" maxlength="200" value="<%=Article_Keyword5 %>"/></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="renew" />
            <input type="hidden" id="Article_ID" name="Article_ID" value="<% =Article_ID%>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='Article_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>