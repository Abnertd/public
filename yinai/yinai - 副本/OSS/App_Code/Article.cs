using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.CMS;

/// <summary>
///Notice 的摘要说明
/// </summary>
public class Article
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IArticle MyBLL;
    ArticleCate articleCate;
    private IArticle_Label MyArt_Label;
    private IProduct_Article_Label MyLabel;

    public Article()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = ArticleFactory.CreateArticle();
        MyArt_Label = Article_LabelFactory.CreateArticle_Label();
        MyLabel = Product_Article_LabelFactory.CreateProduct_Article_Label();

        articleCate = new ArticleCate();
    }

    public virtual void AddArticle()
    {
        int art_id;
        int Article_ID = tools.CheckInt(Request.Form["Article_ID"]);
        int Article_CateID = tools.CheckInt(Request.Form["article_cate"]);
        if (Article_CateID == 0)
        {
            Article_CateID = tools.CheckInt(Request.Form["article_cate_parent"]);
        }

        string Article_Title = tools.CheckStr(Request.Form["Article_Title"]);
        string Article_Source = tools.CheckStr(Request.Form["Article_Source"]);
        string Article_Author = tools.CheckStr(Request.Form["Article_Author"]);
        string Article_Img = tools.CheckStr(Request.Form["Article_Img"]);
        string Article_Intro = tools.CheckStr(Request.Form["Article_Intro"]);
        string Article_Content = Request.Form["Article_Content"];
        DateTime Article_Addtime = DateTime.Now;
        int Article_Hits = 0;
        int Article_IsRecommend = tools.CheckInt(Request.Form["Article_IsRecommend"]);
        int Article_IsAudit = tools.CheckInt(Request.Form["Article_IsAudit"]);
        int Article_Sort = tools.CheckInt(Request.Form["Article_Sort"]);
        string Article_Site = Public.GetCurrentSite();
        string Article_Keyword1 = tools.CheckStr(Request.Form["Article_Keyword1"]);
        string Article_Keyword2 = tools.CheckStr(Request.Form["Article_Keyword2"]);
        string Article_Keyword3 = tools.CheckStr(Request.Form["Article_Keyword3"]);
        string Article_Keyword4 = tools.CheckStr(Request.Form["Article_Keyword4"]);
        string Article_Keyword5 = tools.CheckStr(Request.Form["Article_Keyword5"]);


        string Article_Keyword = Article_Keyword1 + "|" + Article_Keyword2 + "|" + Article_Keyword3 + "|" + Article_Keyword4 + "|" + Article_Keyword5;
        if (Article_CateID == 0) { Public.Msg("error", "错误信息", "请选择类别！", false, "{back}"); return; }
        if (Article_Title == "") { Public.Msg("error", "错误信息", "请填写文章标题", false, "{back}"); return; }

        ArticleInfo entity = new ArticleInfo();
        entity.Article_ID = Article_ID;
        entity.Article_CateID = Article_CateID;
        entity.Article_Title = Article_Title;
        entity.Article_Source = Article_Source;
        entity.Article_Author = Article_Author;
        entity.Article_Img = Article_Img;
        entity.Article_Keyword = Article_Keyword;
        entity.Article_Intro = Article_Intro;
        entity.Article_Content = Article_Content;
        entity.Article_Addtime = Article_Addtime;
        entity.Article_Hits = Article_Hits;
        entity.Article_IsRecommend = Article_IsRecommend;
        entity.Article_IsAudit = Article_IsAudit;
        entity.Article_Sort = Article_Sort;
        entity.Article_Site = Article_Site;

        if (MyBLL.AddArticle(entity, Public.GetUserPrivilege()))
        {
            art_id = entity.Article_ID;
            string[] strkeyword = { Article_Keyword1, Article_Keyword2, Article_Keyword3, Article_Keyword4, Article_Keyword5 };
            for (int i = 0; i < strkeyword.Length; i++)
            {
                string name = strkeyword[i].ToString();
                if (name != "")
                {
                    Product_Article_LabelInfo entity1 = MyLabel.GetProduct_Article_LabelByName(name);
                    Article_LabelInfo entity3 = new Article_LabelInfo();
                    if (entity1 == null)
                    {
                        entity1 = new Product_Article_LabelInfo();
                        entity1.Product_Article_LabelName = name;
                        MyLabel.AddProduct_Article_Label(entity1);
                        int LabelID = 0;
                        Product_Article_LabelInfo entity2 = MyLabel.GetProduct_Article_LabelByTopID();
                        if (entity2 != null)
                        {
                            LabelID = entity2.Product_Article_LabelID;
                            entity3.Article_Label_LabelID = LabelID;
                            entity3.Article_Label_ArticleID = art_id;
                            MyArt_Label.AddArticle_Label(entity3);
                        }
                    }
                    else
                    {
                        entity3.Article_Label_LabelID = entity1.Product_Article_LabelID;
                        entity3.Article_Label_ArticleID = art_id;
                        MyArt_Label.AddArticle_Label(entity3);

                    }
                }

            }
            Public.Msg("positive", "操作成功", "操作成功", true, "Article_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }


    public virtual void EditArticle()
    {

        int Article_ID = tools.CheckInt(Request.Form["Article_ID"]);
        int Article_CateID = tools.CheckInt(Request.Form["article_cate"]);
        if (Article_CateID == 0)
        {
            Article_CateID = tools.CheckInt(Request.Form["article_cate_parent"]);
        }
        string Article_Title = tools.CheckStr(Request.Form["Article_Title"]);
        string Article_Source = tools.CheckStr(Request.Form["Article_Source"]);
        string Article_Author = tools.CheckStr(Request.Form["Article_Author"]);
        string Article_Img = tools.CheckStr(Request.Form["Article_Img"]);
        string Article_Intro = tools.CheckStr(Request.Form["Article_Intro"]);
        string Article_Content = Request.Form["Article_Content"];
        int Article_Hits = tools.CheckInt(Request.Form["Article_Hits"]);
        int Article_IsRecommend = tools.CheckInt(Request.Form["Article_IsRecommend"]);
        int Article_IsAudit = tools.CheckInt(Request.Form["Article_IsAudit"]);
        int Article_Sort = tools.CheckInt(Request.Form["Article_Sort"]);
        string Article_Keyword1 = tools.CheckStr(Request.Form["Article_Keyword1"]);
        string Article_Keyword2 = tools.CheckStr(Request.Form["Article_Keyword2"]);
        string Article_Keyword3 = tools.CheckStr(Request.Form["Article_Keyword3"]);
        string Article_Keyword4 = tools.CheckStr(Request.Form["Article_Keyword4"]);
        string Article_Keyword5 = tools.CheckStr(Request.Form["Article_Keyword5"]);

        string Article_Keyword = Article_Keyword1 + "|" + Article_Keyword2 + "|" + Article_Keyword3 + "|" + Article_Keyword4 + "|" + Article_Keyword5;


        if (Article_CateID == 0) { Public.Msg("error", "错误信息", "请选择类别！", false, "{back}"); return; }
        if (Article_Title == "") { Public.Msg("error", "错误信息", "请填写文章标题", false, "{back}"); return; }

        ArticleInfo entity = GetArticleByID(Article_ID);
        if (entity != null)
        {
            entity.Article_CateID = Article_CateID;
            entity.Article_Title = Article_Title;
            entity.Article_Source = Article_Source;
            entity.Article_Author = Article_Author;
            entity.Article_Img = Article_Img;
            entity.Article_Keyword = Article_Keyword;
            entity.Article_Intro = Article_Intro;
            entity.Article_Content = Article_Content;
            entity.Article_Hits = Article_Hits;
            entity.Article_IsRecommend = Article_IsRecommend;
            entity.Article_IsAudit = Article_IsAudit;
            entity.Article_Sort = Article_Sort;
        }

        if (MyBLL.EditArticle(entity, Public.GetUserPrivilege()))
        {
            MyArt_Label.DelArticle_Label_ArticleID(Article_ID);
            string[] strkeyword = { Article_Keyword1, Article_Keyword2, Article_Keyword3, Article_Keyword4, Article_Keyword5 };
            for (int i = 0; i < strkeyword.Length; i++)
            {
                string name = strkeyword[i].ToString();
                if (name != "")
                {
                    Product_Article_LabelInfo entity1 = MyLabel.GetProduct_Article_LabelByName(name);
                    Article_LabelInfo entity3 = new Article_LabelInfo();
                    if (entity1 == null)
                    {
                        entity1 = new Product_Article_LabelInfo();
                        entity1.Product_Article_LabelName = name;
                        MyLabel.AddProduct_Article_Label(entity1);
                        int LabelID = 0;
                        Product_Article_LabelInfo entity2 = MyLabel.GetProduct_Article_LabelByTopID();
                        if (entity2 != null)
                        {
                            LabelID = entity2.Product_Article_LabelID;
                            entity3.Article_Label_LabelID = LabelID;
                            entity3.Article_Label_ArticleID = Article_ID;
                            MyArt_Label.AddArticle_Label(entity3);
                        }
                    }
                    else
                    {
                        entity3.Article_Label_LabelID = entity1.Product_Article_LabelID;
                        entity3.Article_Label_ArticleID = Article_ID;
                        MyArt_Label.AddArticle_Label(entity3);

                    }
                }

            }
            Public.Msg("positive", "操作成功", "操作成功", true, "Article_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }


    public virtual void DelArticle()
    {
        int Article_ID = tools.CheckInt(Request.QueryString["Article_ID"]);
        if (MyBLL.DelArticle(Article_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Article_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual ArticleInfo GetArticleByID(int cate_id)
    {
        return MyBLL.GetArticleByID(cate_id, Public.GetUserPrivilege());
    }


    public string GetArticles()
    {
        string keyword = tools.CheckStr(Request["keyword"]);
        int CateID = tools.CheckInt(Request["CateID"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        if (CateID > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_CateID", "=", CateID.ToString()));
        }
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Title", "like", keyword));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        ArticleCateInfo CateInfo;

        IList<ArticleInfo> entitys = MyBLL.GetArticles(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ArticleInfo entity in entitys)
            {
                CateInfo = articleCate.GetArticleCateByID(entity.Article_CateID);

                jsonBuilder.Append("{\"ArticleInfo.Article_ID\":" + entity.Article_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Article_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Article_Title));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");

                if (CateInfo != null) { jsonBuilder.Append(Public.JsonStr(CateInfo.Article_Cate_Name)); }
                else { jsonBuilder.Append(entity.Article_CateID); }
                jsonBuilder.Append("\",");


                //jsonBuilder.Append("\"");
                //jsonBuilder.Append(Public.JsonStr(entity.Article_CateID.ToString()));              
                //jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Article_Sort);
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Article_Hits);
                jsonBuilder.Append("\",");

                //jsonBuilder.Append("\"");


                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("8e2eb41c-060b-4a1c-9c7c-403d6f1072fa"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"article_edit.aspx?article_id=" + entity.Article_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }

                if (Public.CheckPrivilege("8ad36b15-547d-4ef0-aa55-e4fce614af3c"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('article_do.aspx?action=move&article_id=" + entity.Article_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else { return null; }

    }





}

public class ArticleCate
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IArticleCate MyBLL;

    public ArticleCate()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = ArticleFactory.CreateArticleCate();
    }

    public virtual void AddArticleCate()
    {
        int Article_Cate_ID = tools.CheckInt(Request.Form["Article_Cate_ID"]);
        int Article_Cate_ParentID = tools.CheckInt(Request.Form["Article_Cate_ParentID"]);
        string Article_Cate_Name = tools.CheckStr(Request.Form["Article_Cate_Name"]);
        int Article_Cate_Sort = tools.CheckInt(Request.Form["Article_Cate_Sort"]);
        string Article_Cate_Site = Public.GetCurrentSite();

        if (Article_Cate_Name == "") { Public.Msg("error", "错误信息", "请填写类别名称", false, "{back}"); return; }

        ArticleCateInfo entity = new ArticleCateInfo();
        entity.Article_Cate_ID = Article_Cate_ID;
        entity.Article_Cate_ParentID = Article_Cate_ParentID;
        entity.Article_Cate_Name = Article_Cate_Name;
        entity.Article_Cate_Sort = Article_Cate_Sort;
        entity.Article_Cate_Site = Article_Cate_Site;

        if (MyBLL.AddArticleCate(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Article_Cate_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditArticleCate()
    {

        int Article_Cate_ID = tools.CheckInt(Request.Form["Article_Cate_ID"]);
        int Article_Cate_ParentID = tools.CheckInt(Request.Form["Article_Cate_ParentID"]);
        string Article_Cate_Name = tools.CheckStr(Request.Form["Article_Cate_Name"]);
        int Article_Cate_Sort = tools.CheckInt(Request.Form["Article_Cate_Sort"]);
        string Article_Cate_Site = Public.GetCurrentSite();

        if (Article_Cate_Name == "") { Public.Msg("error", "错误信息", "请填写类别名称", false, "{back}"); return; }

        ArticleCateInfo entity = new ArticleCateInfo();
        entity.Article_Cate_ID = Article_Cate_ID;
        entity.Article_Cate_ParentID = Article_Cate_ParentID;
        entity.Article_Cate_Name = Article_Cate_Name;
        entity.Article_Cate_Sort = Article_Cate_Sort;
        entity.Article_Cate_Site = Article_Cate_Site;


        if (MyBLL.EditArticleCate(entity, Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Article_Cate_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void DelArticleCate()
    {
        int Article_Cate_ID = tools.CheckInt(Request.QueryString["Article_Cate_ID"]);
        if (MyBLL.DelArticleCate(Article_Cate_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "Article_Cate_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual ArticleCateInfo GetArticleCateByID(int cate_id)
    {
        return MyBLL.GetArticleCateByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetArticleCates()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        string keyword = tools.CheckStr(Request["keyword"]);
        if (keyword.Length > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_Name", "like", keyword));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<ArticleCateInfo> entitys = MyBLL.GetArticleCates(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ArticleCateInfo entity in entitys)
            {
                jsonBuilder.Append("{\"ArticleCateInfo.Article_Cate_ID\":" + entity.Article_Cate_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Article_Cate_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(Public.JsonStr(entity.Article_Cate_Name));
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.Article_Cate_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (Public.CheckPrivilege("1daab676-20b6-4073-af76-132ee8874556"))
                {
                    jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"article_cate_edit.aspx?article_cate_id=" + entity.Article_Cate_ID + "\\\" title=\\\"修改\\\">修改</a>");
                }

                if (Public.CheckPrivilege("cc00c494-d211-438c-baef-ac20d419b066"))
                {
                    jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('article_cate_do.aspx?action=move&article_cate_id=" + entity.Article_Cate_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                }

                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else { return null; }

    }

    public void getArticleCate(int id, int a, int cate_id)
    {
        int i = a;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ParentID", "=", id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_Sort", "asc"));

        IList<ArticleCateInfo> entitys = MyBLL.GetArticleCates(Query, Public.GetUserPrivilege());

        if (entitys != null)
        {
            i++;
            foreach (ArticleCateInfo entity in entitys)
            {

                string str = "";
                for (int n = 1; n <= i; n++)
                {
                    str += "&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                Response.Write("<option " + select(entity.Article_Cate_ID, cate_id) + " value=\"" + entity.Article_Cate_ID + "\">" + str + entity.Article_Cate_Name + "</option>");
                getArticleCate(entity.Article_Cate_ID, i, cate_id);
            }
        }
    }

    public string select(int id, int cate_id)
    {
        if (id == cate_id)
            return "Selected";
        else
            return "";
    }
    public string Article_Cate_Select(int cate_id, string div_name)
    {
        string select_list = "";
        string select_tmp = "";
        int grade = 0;
        int i;
        int parentid = 0;
        string select_name = "";
        string cate_relate = Get_Category_Relate(cate_id, "");
        cate_relate = cate_relate + ",";
        foreach (string cate in cate_relate.Split(','))
        {
            if (cate.Length > 0)
            {
                QueryInfo Query = new QueryInfo();
                Query.CurrentPage = 1;
                Query.PageSize = 0;
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_ParentID", "=", cate));
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_ParentID", "=", cate));
                IList<ArticleCateInfo> cates = MyBLL.GetArticleCates(Query, Public.GetUserPrivilege());
                if (cates != null)
                {
                    grade = grade + 1;
                    if (grade == 1)
                    {
                        select_tmp = "<select id=\"article_cate\" name=\"article_cate\" onchange=\"change_maincate1('" + div_name + "','article_cate');\">";
                        select_tmp = select_tmp + "<option value=\"0\">选择类别</option>";
                    }
                    else
                    {
                        select_name = "article_cate";
                        for (i = 1; i < grade; i++)
                        {
                            select_name = select_name + "_parent";
                        }
                        select_tmp = "<select id=\"" + select_name + "\" name=\"" + select_name + "\" onchange=\"change_maincate1('" + div_name + "','" + select_name + "');\">";
                        select_tmp = select_tmp + "<option value=\"0\">选择类别</option>";
                    }

                    foreach (ArticleCateInfo entity in cates)
                    {
                        if (parentid == entity.Article_Cate_ID || cate_id == entity.Article_Cate_ID)
                        {
                            select_tmp = select_tmp + "<option value=\"" + entity.Article_Cate_ID + "\" selected>   " + entity.Article_Cate_Name + "(类别-" + entity.Article_Cate_ID + "- )</option>";
                        }
                        else
                        {
                            select_tmp = select_tmp + "<option value=\"" + entity.Article_Cate_ID + "\"> " + entity.Article_Cate_Name + "(类别-" + entity.Article_Cate_ID + "- )</option>";
                        }
                    }
                    select_tmp = select_tmp + "</select> ";
                    parentid = tools.CheckInt(cate);
                }

                Query = null;
                cates = null;
                select_list = select_tmp + select_list;
            }
        }
        return select_list;
    }

    public string Article_Category_Select(int cate_id, string div_name)
    {
        string select_list = "";
        string select_tmp = "";
        int grade = 0;
        int i;
        int parentid = 0;
        string select_name = "";
        string cate_relate = Get_Category_Relate(cate_id, "");
        cate_relate = cate_relate + ",";
        foreach (string cate in cate_relate.Split(','))
        {
            if (cate.Length > 0)
            {
                QueryInfo Query = new QueryInfo();
                Query.CurrentPage = 1;
                Query.PageSize = 0;
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_ParentID", "=", cate));
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_ParentID", "=", cate));
                IList<ArticleCateInfo> cates = MyBLL.GetArticleCates(Query, Public.GetUserPrivilege());
                if (cates != null)
                {
                    grade = grade + 1;
                    if (grade == 1)
                    {
                        select_tmp = "<select id=\"article_cate\" name=\"article_cate\" onchange=\"change_maincate('" + div_name + "','article_cate');\">";
                        select_tmp = select_tmp + "<option value=\"0\">选择类别</option>";
                    }
                    else
                    {
                        select_name = "article_cate";
                        for (i = 1; i < grade; i++)
                        {
                            select_name = select_name + "_parent";
                        }
                        select_tmp = "<select id=\"" + select_name + "\" name=\"" + select_name + "\" onchange=\"change_maincate('" + div_name + "','" + select_name + "');\">";
                        select_tmp = select_tmp + "<option value=\"0\">选择类别</option>";
                    }

                    foreach (ArticleCateInfo entity in cates)
                    {
                        if (parentid == entity.Article_Cate_ID || cate_id == entity.Article_Cate_ID)
                        {
                            select_tmp = select_tmp + "<option value=\"" + entity.Article_Cate_ID + "\" selected>   " + entity.Article_Cate_Name + "(类别-" + entity.Article_Cate_ID + "- )</option>";
                        }
                        else
                        {
                            select_tmp = select_tmp + "<option value=\"" + entity.Article_Cate_ID + "\"> " + entity.Article_Cate_Name + "(类别-" + entity.Article_Cate_ID + "- )</option>";
                        }
                    }
                    select_tmp = select_tmp + "</select> ";
                    parentid = tools.CheckInt(cate);
                }

                Query = null;
                cates = null;
                select_list = select_tmp + select_list;
            }
        }
        return select_list;
    }
    //public string Article_Category_Select(int cate_id, string div_name)
    //{
    //    string select_list = "";
    //    string select_tmp = "";
    //    int grade = 0;
    //    int i;
    //    int parentid = 0;
    //    string select_name = "";
    //    string cate_relate = Get_Category_Relate(cate_id, "");
    //    cate_relate = cate_relate + ",";
    //    foreach (string cate in cate_relate.Split(','))
    //    {
    //        if (cate.Length > 0)
    //        {
    //            QueryInfo Query = new QueryInfo();
    //            Query.CurrentPage = 1;
    //            Query.PageSize = 0;
    //            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_ParentID", "=", cate));
    //            IList<ArticleCateInfo> cates = MyBLL.GetArticleCates(Query, Public.GetUserPrivilege());
    //            if (cates != null)
    //            {
    //                grade = grade + 1;
    //                if (grade == 1)
    //                {
    //                    select_tmp = "<select id=\"article_cate\" name=\"article_cate\" onchange=\"change_maincate('" + div_name + "','article_cate');\">";
    //                    select_tmp = select_tmp + "<option value=\"0\">选择类别</option>";
    //                }
    //                else
    //                {
    //                    select_name = "article_cate";
    //                    for (i = 1; i < grade; i++)
    //                    {
    //                        select_name = select_name + "_parent";
    //                    }
    //                    select_tmp = "<select id=\"" + select_name + "\" name=\"" + select_name + "\" onchange=\"change_maincate('" + div_name + "','" + select_name + "');\">";
    //                    select_tmp = select_tmp + "<option value=\"0\">选择类别</option>";
    //                }

    //                foreach (ArticleCateInfo entity in cates)
    //                {
    //                    if (parentid == entity.Article_Cate_ID || cate_id == entity.Article_Cate_ID)
    //                    {
    //                        select_tmp = select_tmp + "<option value=\"" + entity.Article_Cate_ID + "\" selected>" + entity.Article_Cate_Name + "</option>";
    //                    }
    //                    else
    //                    {
    //                        select_tmp = select_tmp + "<option value=\"" + entity.Article_Cate_ID + "\">" + entity.Article_Cate_Name + "</option>";
    //                    }
    //                }
    //                select_tmp = select_tmp + "</select> ";
    //                parentid = tools.CheckInt(cate);
    //            }

    //            Query = null;
    //            cates = null;
    //            select_list = select_tmp + select_list;
    //        }
    //    }
    //    return select_list;
    //}

    public string Get_Category_Relate(int cate_id, string cate_str)
    {

        string cate_relate = cate_id.ToString();
        if (cate_id > 0)
        {
            ArticleCateInfo cate = MyBLL.GetArticleCateByID(cate_id, Public.GetUserPrivilege());
            if (cate != null)
            {
                cate_relate = cate_relate + ",";
                cate_relate = cate_str + Get_Category_Relate(cate.Article_Cate_ParentID, cate_relate);
            }
            else
            {
                cate_relate = "0";
            }
        }
        else
        {
            if (cate_str != "")
            {
                cate_relate = cate_str + cate_relate;
            }
        }
        return cate_relate;

    }
}