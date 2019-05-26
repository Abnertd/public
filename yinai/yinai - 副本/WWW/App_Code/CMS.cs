using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.CMS;
using Glaer.Trade.B2C.BLL.SAL;
using Glaer.Trade.B2C.BLL.Product;
using Glaer.Trade.Util.SQLHelper;
//using Glaer.Trade.B2C.BLL.U_SAL;

/// <summary>
///Product 的摘要说明
/// </summary>
public class CMS
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private INotice Webnotice;
    private INoticeCate MyNoticeCate;
    private ITools tools;
    private IHelpCate MyHelpCate;
    private IHelp MyHelp;
    private IAbout MyAbout;
    private Public_Class pub;
    private IFriendlyLink mylink;
    private IFriendlyLinkCate mylinkcate;
    private IArticleCate MyArticleCate;
    private IArticle MyArticle;
    //private ICategory MyCate;
    //private Category 
    private ICategory MyCate;
    private IProduct Myproduct;
    private IProductTag MyTag;
    private PageURL pageurl;

    //private U_ILinksApplication MyApplication;

    public CMS()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;
        Webnotice = NoticeFactory.CreateNotice();
        tools = ToolsFactory.CreateTools();
        MyHelpCate = HelpFactory.CreateHelpCate();
        MyHelp = HelpFactory.CreateHelp();
        MyAbout = AboutFactory.CreateAbout();
        mylink = FriendlyLinkFactory.CreateFriendlyLink();
        pub = new Public_Class();
        mylinkcate = FriendlyLinkFactory.CreateFriendlyLinkCate();
        MyNoticeCate = NoticeFactory.CreateNoticeCate();
        MyArticleCate = ArticleFactory.CreateArticleCate();
        MyArticle = ArticleFactory.CreateArticle();
        //MyCate = CategoryFactory.CreateCategory();
        // MyApplication = U_LinksApplicationFactory.CreateU_LinksApplication();
        MyCate = CategoryFactory.CreateCategory();
        Myproduct = ProductFactory.CreateProduct();
        MyTag = ProductTagFactory.CreateProductTag();
        pageurl = new PageURL(int.Parse(Application["Static_IsEnable"].ToString()));
    }

    /// <summary>
    /// 首页公告列表
    /// </summary>
    /// <param name="Show_Num"></param>
    /// <param name="Cate_ID"></param>
    /// <returns></returns>
    public string Home_Top_Notice(int Show_Num, int Cate_ID)
    {
        string notice_list = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = Show_Num;
        Query.CurrentPage = 1;
        if (Cate_ID > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "NoticeInfo.Notice_Cate", "=", Cate_ID.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "NoticeInfo.Notice_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "NoticeInfo.Notice_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("NoticeInfo.Notice_Addtime", "Desc"));
        IList<NoticeInfo> Notices = Webnotice.GetNoticeList(Query, pub.CreateUserPrivilege("9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7"));
        if (Notices != null)
        {
            foreach (NoticeInfo entity in Notices)
            {
                notice_list = notice_list + " <li><a href=\"/notice/detail.aspx?notice_id=" + entity.Notice_ID + "\" target=\"_blank\" title=\"" + entity.Notice_Title + "\">" + tools.CutStr(entity.Notice_Title, 28) + "</a></li>";
            }
        }

        return notice_list;
    }

    public string Home_Partners(int show_Num)
    {
        StringBuilder strHTML = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "FriendlyLinkInfo.FriendlyLink_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "FriendlyLinkInfo.FriendlyLink_CateID", "=", "8"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "FriendlyLinkInfo.FriendlyLink_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("FriendlyLinkInfo.FriendlyLink_Sort", "ASC"));

        IList<FriendlyLinkInfo> entitys = mylink.GetFriendlyLinks(Query, pub.CreateUserPrivilege("2f32fa4c-cb10-4ee8-8c28-ee18cd2a70e5"));
        if (entitys != null)
        {
            foreach (FriendlyLinkInfo entity in entitys)
            {
                if (entity.FriendlyLink_IsImg == 1)
                {
                    strHTML.Append("<li><img src=\"" + pub.FormatImgURL(entity.FriendlyLink_Img, "fullpath") + "\" align=\"absmiddle\" /></li>");
                }
                else
                {
                    strHTML.Append("<li>" + entity.FriendlyLink_Name + "</li>");
                }
            }
        }

        return strHTML.ToString();
    }

    /// <summary>
    /// 帮助中心左侧列表
    /// </summary>
    /// <param name="cateid"></param>
    /// <param name="helpid"></param>
    /// <returns></returns>
    public string Help_Left_Nav(int cateid, int helpid)
    {

        StringBuilder nav_string = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "HelpCateInfo.Help_Cate_ParentID", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "HelpCateInfo.Help_Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("HelpCateInfo.Help_Cate_Sort", "ASC"));
        IList<HelpCateInfo> helpcate = MyHelpCate.GetHelpCates(Query, pub.CreateUserPrivilege("e2e6aec7-ff11-407b-9c3a-6317b06b1a7e"));
        nav_string.Append("<div class=\"menu_1\">");
        nav_string.Append("   <h2>帮助中心</h2>");
        if (helpcate != null)
        {
            int i = 0;
            foreach (HelpCateInfo entity in helpcate)
            {
                i++;

                if (i == 1)
                {
                    nav_string.Append("<div class=\"b07_info\">");
                    nav_string.Append("<h3><span onclick=\"openShutManager(this,'box')\" ><a id=\"1\" href=\"#\"  class=\"hidecontent\" onClick=\"switchTag(1);\">" + entity.Help_Cate_Name + "</a></span></h3>");

                    nav_string.Append("<div class=\"b07_info_main\">");


                    nav_string.Append("<ul id=\"box\">");
                }
                else
                {
                    nav_string.Append("<div class=\"b07_info\">");
                    nav_string.Append("<h3><span onclick=\"openShutManager(this,'box" + i + "')\" ><a id=\"" + i + "\" href=\"#\"  class=\"hidecontent\" onClick=\"switchTag(" + i + ");\">" + entity.Help_Cate_Name + "</a></span></h3>");

                    nav_string.Append("<div class=\"b07_info_main\">");

                    nav_string.Append("<ul id=\"box" + i + "\">");
                }

                nav_string.Append(Help_Left_Sub_Nav(entity.Help_Cate_ID, helpid, cateid));

                nav_string.Append("</ul>");
                nav_string.Append("</div>");
                nav_string.Append("</div>");
            }
        }
        nav_string.Append("</div>");
        return nav_string.ToString();
    }

    /// <summary>
    /// 帮助中心左侧列表
    /// </summary>
    /// <param name="Cate_ID"></param>
    /// <param name="helpid"></param>
    /// <param name="cateid"></param>
    /// <returns></returns>
    public string Help_Left_Sub_Nav(int Cate_ID, int helpid, int cateid)
    {
        StringBuilder nav_string = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "HelpInfo.Help_CateID", "=", Cate_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "HelpInfo.Help_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "HelpInfo.Help_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("HelpInfo.Help_ID", "asc"));
        IList<HelpInfo> helps = MyHelp.GetHelps(Query, pub.CreateUserPrivilege("a015e960-173c-429d-98d2-69e5a023b5dc"));
        if (helps != null)
        {
            foreach (HelpInfo entity in helps)
            {
                if (helpid == entity.Help_ID)
                {
                    nav_string.Append("<li class=\"on\"><a href=\"/help/index.aspx?help_id=" + entity.Help_ID + "\">" + entity.Help_Title + "</a></li>");
                }
                else
                {
                    nav_string.Append("<li><a href=\"/help/index.aspx?help_id=" + entity.Help_ID + "\">" + entity.Help_Title + "</a></li>");
                }
            }
        }
        return nav_string.ToString();
    }

    /// <summary>
    /// 底部帮助显示
    /// </summary>
    /// <param name="Cate_ID"></param>
    /// <returns></returns>
    public string Help_Left_Sub_Nav1(int Cate_ID)
    {
        string nav_string = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 5;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "HelpInfo.Help_CateID", "=", Cate_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "HelpInfo.Help_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "HelpInfo.Help_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("HelpInfo.Help_ID", "asc"));
        IList<HelpInfo> helps = MyHelp.GetHelps(Query, pub.CreateUserPrivilege("a015e960-173c-429d-98d2-69e5a023b5dc"));
        if (helps != null)
        {
            foreach (HelpInfo entity in helps)
            {
                nav_string = nav_string + "<p><a href=\"/help/index.aspx?help_id=" + entity.Help_ID + "\">" + entity.Help_Title + "</a></p>";

            }
        }
        return nav_string;
    }

    //  /**/
    public HelpInfo GetHelpByIDorCateID(int help_id, int Cate_ID)
    {
        HelpInfo entity = MyHelp.GetHelpByID(help_id, pub.CreateUserPrivilege("a015e960-173c-429d-98d2-69e5a023b5dc"));

        return entity;
    }

    /// <summary>
    /// 常见问题
    /// </summary>
    public void Help_FAQ(int Quantity)
    {
        Response.Write("<ul>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = Quantity;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "HelpInfo.Help_IsActive", "=", "1"));
        //Query.ParamInfos.Add(new ParamInfo("AND", "int", "HelpInfo.Help_IsFAQ", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "HelpInfo.Help_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("HelpInfo.Help_ID", "asc"));
        IList<HelpInfo> entitys = MyHelp.GetHelps(Query, pub.CreateUserPrivilege("a015e960-173c-429d-98d2-69e5a023b5dc"));


        if (entitys != null)
        {
            int count = 0;
            foreach (HelpInfo entity in entitys)
            {
                count++;
                Response.Write("<li " + (count % 5 == 0 ? "class=\"bar\"" : "") + "><a href=\"/help/index.aspx?help_id=" + entity.Help_ID + "\">" + tools.CutStr(entity.Help_Title, 50) + "</a></li>");
            }
        }
        else
        {
            Response.Write("<div height=\"50\"  style=\" height:50px; text-align:center;line-height:50px; color:#707070;\">信息正在维护中...</div>");
        }
        Response.Write("</ul>");

        //PageInfo pageinfo = MyHelp.GetPageInfo(Query, pub.CreateUserPrivilege("a015e960-173c-429d-98d2-69e5a023b5dc"));
        //if (pageinfo != null && pageinfo.RecordCount >= 1)
        //{
        //    string url = "/help/index.aspx?1=1";
        //    Response.Write("<div>");
        //    pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, url, pageinfo.PageSize, pageinfo.RecordCount);
        //    Response.Write("</div>");
        //}


    }

    /// <summary>
    /// 关于我们左侧列表
    /// </summary>
    /// <param name="type"></param>
    /// <param name="sign"></param>
    /// <returns></returns>
    public string Help_About_Nav(string sign)
    {
        StringBuilder strHTML = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "AboutInfo.About_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "AboutInfo.About_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("AboutInfo.About_Sort", "ASC"));
        IList<AboutInfo> abouts = MyAbout.GetAbouts(Query, pub.CreateUserPrivilege("db8de73b-9ac0-476e-866e-892dd35589c5"));
        Query = null;
        if (abouts != null)
        {
            foreach (AboutInfo entity in abouts)
            {
                if (sign == entity.About_Sign)
                {
                    strHTML.Append("<li class=\"on\"><a href=\"/about/index.aspx?sign=" + entity.About_Sign + "\">" + entity.About_Title + "</a></li>");
                }
                else
                {
                    strHTML.Append("<li><a href=\"/about/index.aspx?sign=" + entity.About_Sign + "\">" + entity.About_Title + "</a></li>");
                }
            }
        }

        return strHTML.ToString();
    }


    /// <summary>
    /// 关于我们左侧列表
    /// </summary>
    /// <param name="type"></param>
    /// <param name="sign"></param>
    /// <returns></returns>
    public string Help_About_LinkNav(string sign)
    {
        StringBuilder strHTML = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "AboutInfo.About_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "AboutInfo.About_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("AboutInfo.About_Sort", "ASC"));
        IList<AboutInfo> abouts = MyAbout.GetAbouts(Query, pub.CreateUserPrivilege("db8de73b-9ac0-476e-866e-892dd35589c5"));
        Query = null;
        if (abouts != null)
        {
            foreach (AboutInfo entity in abouts)
            {
                //if (sign == entity.About_Sign)
                //{
                //    strHTML.Append("<li class=\"on\"><a href=\"/about/index.aspx?sign=" + entity.About_Sign + "\">" + entity.About_Title + "</a></li>");
                //}
                //else
                //{
                strHTML.Append("<li><a href=\"/about/index.aspx?sign=" + entity.About_Sign + "\">" + entity.About_Title + "</a></li>");
                //}
            }
        }

        return strHTML.ToString();
    }




    //网站公告左侧列表 /**/
    public string Notice_Nav(int cateid)
    {
        StringBuilder nav_string = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "NoticeCateInfo.Notice_Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("NoticeCateInfo.Notice_Cate_Sort", "ASC"));
        IList<NoticeCateInfo> entitys = MyNoticeCate.GetNoticeCates(Query, pub.CreateUserPrivilege("fb3e87ba-3d4d-480d-934e-80048bcc0100"));

        if (entitys != null)
        {
            foreach (NoticeCateInfo entity in entitys)
            {
                if (cateid == entity.Notice_Cate_ID)
                {
                    nav_string.Append("<li class=\"on\"><a href=\"/notice/index.aspx?cate_id=" + entity.Notice_Cate_ID + "\">" + entity.Notice_Cate_Name + "</a></li>");
                }
                else
                {
                    nav_string.Append("<li><a href=\"/notice/index.aspx?cate_id=" + entity.Notice_Cate_ID + "\">" + entity.Notice_Cate_Name + "</a></li>");
                }
            }
        }

        return nav_string.ToString();
    }

    //网站公告右侧列表 /**/
    public void Notice_List(int cateid)
    {
        int page = tools.CheckInt(Request["page"]);
        if (page < 1)
        {
            page = 1;
        }
        NoticeCateInfo CateInfo = MyNoticeCate.GetNoticeCateByID(cateid, pub.CreateUserPrivilege("fb3e87ba-3d4d-480d-934e-80048bcc0100"));
        if (CateInfo != null)
        {
            Response.Write("  <ul>");
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 20;
            Query.CurrentPage = page;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "NoticeInfo.Notice_IsAudit", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "NoticeInfo.Notice_Site", "=", "CN"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "NoticeInfo.Notice_Cate", "=", CateInfo.Notice_Cate_ID.ToString()));
            Query.OrderInfos.Add(new OrderInfo("NoticeInfo.Notice_ID", "DESC"));
            IList<NoticeInfo> notices = Webnotice.GetNotices(Query, pub.CreateUserPrivilege("9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7"));
            if (notices != null)
            {
                int count = 0;
                foreach (NoticeInfo entity in notices)
                {
                    count++;
                    Response.Write("<li " + (count % 5 == 0 ? "class=\"bar\"" : "") + "><span>" + entity.Notice_Addtime.ToString("yy-MM-dd") + "</span><a href=\"/notice/detail.aspx?notice_id=" + entity.Notice_ID + "\">" + tools.CutStr(entity.Notice_Title, 35) + "</a></li>");
                }
            }
            else
            {
                Response.Write("<div height=\"50\"  style=\" height:50px; text-align:center;line-height:50px; color:#707070;\">信息正在维护中...</div>");
            }
            Response.Write("</ul>");

            PageInfo pageinfo = Webnotice.GetPageInfo(Query, pub.CreateUserPrivilege("9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7"));
            if (pageinfo != null && pageinfo.RecordCount >= 1)
            {
                string url = "/notice/index.aspx?cate_id=" + CateInfo.Notice_Cate_ID;
                Response.Write("<div>");
                pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, url, pageinfo.PageSize, pageinfo.RecordCount);
                Response.Write("</div>");
            }
        }
        else
        {
            Response.Write("  <ul>");
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 20;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "NoticeInfo.Notice_IsAudit", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "NoticeInfo.Notice_IsHot", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "NoticeInfo.Notice_Site", "=", "CN"));
            Query.OrderInfos.Add(new OrderInfo("NoticeInfo.Notice_ID", "DESC"));
            IList<NoticeInfo> notices = Webnotice.GetNotices(Query, pub.CreateUserPrivilege("9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7"));

            if (notices != null)
            {
                foreach (NoticeInfo entity in notices)
                {
                    Response.Write("<li><span>[ " + entity.Notice_Addtime.ToString("yy-MM-dd") + " ]</span><a href=\"/notice/detail.aspx?notice_id=" + entity.Notice_ID + "\">" + tools.CutStr(entity.Notice_Title, 35) + "</a></li>");
                }
            }
            else
            {
                Response.Write("<div height=\"50\"  style=\" height:50px; text-align:center;line-height:50px; color:#707070;\">信息正在维护中...</div>");
            }
            Response.Write("  </ul>");
            PageInfo pageinfo = Webnotice.GetPageInfo(Query, pub.CreateUserPrivilege("9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7"));
            if (pageinfo != null && pageinfo.RecordCount > 0)
            {
                string url = "/notice/index.aspx?cate_id=0";
                Response.Write("<div style=\" float:right; padding-right:30px; padding-bottom:5px;\">");
                pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, url, pageinfo.PageSize, pageinfo.RecordCount);
                Response.Write("</div>");
            }

        }
    }

    public NoticeCateInfo GetNoticeCateInfoByID(int ID)
    {
        return MyNoticeCate.GetNoticeCateByID(ID, pub.CreateUserPrivilege("fb3e87ba-3d4d-480d-934e-80048bcc0100"));
    }

    public AboutInfo GetAboutBySign(string sign)
    {
        return MyAbout.GetAboutBySign(sign, pub.CreateUserPrivilege("db8de73b-9ac0-476e-866e-892dd35589c5"));
    }

    public string Bottom_About_List(int shownum)
    {
        StringBuilder strHTML = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        Query.ParamInfos.Add(new ParamInfo("", "", "", "", ""));


        return strHTML.ToString();
    }

    public FriendlyLinkCateInfo GetFriendlyLinkCateInfoByID(int CateID)
    {
        FriendlyLinkCateInfo entity = mylinkcate.GetFriendlyLinkCateByID(CateID, pub.CreateUserPrivilege("0a9f21bd-88cb-4121-94b8-f865a9de2c3b"));
        if (entity == null)
        {
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 0;
            Query.CurrentPage = 1;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "FriendlyLinkCateInfo.FriendlyLink_Cate_Site", "=", "CN"));
            Query.OrderInfos.Add(new OrderInfo("FriendlyLinkCateInfo.FriendlyLink_Cate_Sort", "ASC"));
            IList<FriendlyLinkCateInfo> entitys = mylinkcate.GetFriendlyLinkCates(Query, pub.CreateUserPrivilege("0a9f21bd-88cb-4121-94b8-f865a9de2c3b"));
            if (entitys != null)
            {
                entity = entitys[0];
            }
        }
        return entity;
    }

    /// <summary>
    /// 友情链接列表
    /// </summary>
    /// <param name="CateInfo"></param>
    public void FriendlyLink_Detail(FriendlyLinkCateInfo CateInfo)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "FriendlyLinkInfo.FriendlyLink_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "FriendlyLinkInfo.FriendlyLink_CateID", "=", CateInfo.FriendlyLink_Cate_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "FriendlyLinkInfo.FriendlyLink_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("FriendlyLinkInfo.FriendlyLink_Sort", "ASC"));

        IList<FriendlyLinkInfo> entitys = mylink.GetFriendlyLinks(Query, pub.CreateUserPrivilege("2f32fa4c-cb10-4ee8-8c28-ee18cd2a70e5"));
        if (entitys != null)
        {
            Response.Write("<ul>");
            foreach (FriendlyLinkInfo entity in entitys)
            {
                if (entity.FriendlyLink_IsImg == 1)
                {
                    Response.Write("<li><a href=\"" + entity.FriendlyLink_URL + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.FriendlyLink_Img, "fullpath") + "\" align=\"absmiddle\" /></a></li>");
                }
                else
                {
                    Response.Write("<li><a href=\"" + entity.FriendlyLink_URL + "\" target=\"_blank\">" + entity.FriendlyLink_Name + "</a></li>");
                }
            }
            Response.Write("</ul>");
        }
    }


    /// <summary>
    /// 友情链接列表
    /// </summary>
    /// <param name="CateInfo"></param>
    public void FriendlyLink_Detail1(FriendlyLinkCateInfo CateInfo)
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "FriendlyLinkInfo.FriendlyLink_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "FriendlyLinkInfo.FriendlyLink_CateID", "=", CateInfo.FriendlyLink_Cate_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "FriendlyLinkInfo.FriendlyLink_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("FriendlyLinkInfo.FriendlyLink_Sort", "ASC"));

        IList<FriendlyLinkInfo> entitys = mylink.GetFriendlyLinks(Query, pub.CreateUserPrivilege("2f32fa4c-cb10-4ee8-8c28-ee18cd2a70e5"));
        if (entitys != null)
        {
            Response.Write("<ul>");
            foreach (FriendlyLinkInfo entity in entitys)
            {
                string FriendlyLink_Name = "";
                if (entity.FriendlyLink_IsImg == 1)
                {
                    Response.Write("<li><a href=\"" + entity.FriendlyLink_URL + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.FriendlyLink_Img, "fullpath") + "\" align=\"absmiddle\" /></a></li>");
                }
                else
                {
                    if (entity.FriendlyLink_Name.Length > 15)
                    {
                        FriendlyLink_Name = entity.FriendlyLink_Name.Substring(0, 15);
                    }
                    else
                    {
                        FriendlyLink_Name = entity.FriendlyLink_Name;
                    }
                    Response.Write("<li><a href=\"" + entity.FriendlyLink_URL + "\" target=\"_blank\" title=" + entity.FriendlyLink_Name + ">" + FriendlyLink_Name + "</a></li>");
                }
            }
            Response.Write("</ul>");
        }
    }

    public NoticeInfo GetNoticeByID(int ID)
    {
        return Webnotice.GetNoticeByID(ID, pub.CreateUserPrivilege("9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7"));
    }

    public HelpCateInfo GetHelpCateByID(int ID)
    {
        return MyHelpCate.GetHelpCateByID(ID, pub.CreateUserPrivilege("e2e6aec7-ff11-407b-9c3a-6317b06b1a7e"));
    }


    /// <summary>
    /// 首页底部友情链接
    /// </summary>
    /// <param name="link_type"></param>
    /// <param name="Show_num"></param>
    /// <param name="link_cate"></param>
    public void Foot_Poplink(int link_type, int Show_num, int link_cate)
    {
        int icount = 1;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = Show_num;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "FriendlyLinkInfo.FriendlyLink_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "FriendlyLinkInfo.FriendlyLink_CateID", "=", link_cate.ToString()));
        //Query.ParamInfos.Add(new ParamInfo("AND", "str", "FriendlyLinkInfo.FriendlyLink_IsImg", "=", link_type.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "FriendlyLinkInfo.FriendlyLink_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("FriendlyLinkInfo.FriendlyLink_Sort", "asc"));
        IList<FriendlyLinkInfo> entitys = mylink.GetFriendlyLinks(Query, pub.CreateUserPrivilege("2f32fa4c-cb10-4ee8-8c28-ee18cd2a70e5"));
        if (entitys != null)
        {
            foreach (FriendlyLinkInfo entity in entitys)
            {
                if (link_type == 1)
                {
                    Response.Write("<li><a href=\"" + entity.FriendlyLink_URL + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.FriendlyLink_Img, "fullpath") + "\" /></a></li>");
                }
                else
                {
                    if (icount == 1)
                        Response.Write("<a href=\"" + entity.FriendlyLink_URL + "\" target=\"_blank\">" + entity.FriendlyLink_Name + "</a>");
                    else
                        Response.Write(" | <a href=\"" + entity.FriendlyLink_URL + "\" target=\"_blank\">" + entity.FriendlyLink_Name + "</a>");

                    icount++;
                }
            }
        }
    }

    public string Get_Protocal()
    {
        string Protocal = "";
        AboutInfo entity;
        entity = GetAboutBySign("contract");
        if (entity != null)
        {
            Protocal = entity.About_Content;
        }
        return Protocal;
    }

    /// <summary>
    /// 添加友情链接申请
    /// </summary>
    public void LinksApplication_Add()
    {
        string name = tools.CheckStr(Request["name"]);
        string url = tools.CheckStr(Request["url"]);
        string email = tools.CheckStr(Request["email"]);
        string intro = tools.CheckStr(Request["intro"]);
        if (name == "" || name.Length > 200)
        {
            pub.Msg("info", "信息提示", "请填写网站名称，200字符以内！", false, "{back}");
        }
        if (url == "" || url == "http://")
        {
            pub.Msg("info", "信息提示", "请填写网址！", false, "{back}");
        }
        if (email == "")
        {
            pub.Msg("info", "信息提示", "请填写电子邮箱！", false, "{back}");
        }
        if (intro == "" || intro.Length > 500)
        {
            pub.Msg("info", "信息提示", "请填写网站介绍，500字符以内！", false, "{back}");
        }
        //U_LinksApplicationInfo entity = new U_LinksApplicationInfo();
        //entity.Links_Name = name;
        //entity.Links_Url = url;
        //entity.Links_Intro = intro;
        //entity.Links_Email = email;
        //entity.Links_Addtime = DateTime.Now;

        //MyApplication.AddU_LinksApplication(entity, pub.CreateUserPrivilege("0c5dc9c5-bb97-435e-846b-1947abc87752"));

        //pub.Msg("positive", "操作成功", "申请已提交，请等待管理员审核！", false, "{back}");

    }

    #region 文章
    public string GetArticleCates(int cate_id)
    {
        string article_cate = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 6;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_ParentID", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_Sort", "asc"));
        IList<ArticleCateInfo> entitys = MyArticleCate.GetArticleCates(Query, pub.CreateUserPrivilege("1a3208d0-70a4-49dd-8010-400f1254535a"));
        if (entitys != null)
        {
            foreach (ArticleCateInfo entity in entitys)
            {
                if (entity.Article_Cate_ID == cate_id)
                {
                    article_cate += "<li class=\"on\"><a href=\"/article/category.aspx?cate_id=" + entity.Article_Cate_ID + "\" >" + entity.Article_Cate_Name + "</a></li>";
                }
                else
                {
                    article_cate += "<li><a href=\"/article/category.aspx?cate_id=" + entity.Article_Cate_ID + "\">" + entity.Article_Cate_Name + "</a></li>";
                }

            }
        }
        return article_cate;

    }

    public string GetArticlesByCateLeft(int cate_id)
    {
        StringBuilder strHTML = new StringBuilder();
        strHTML.Append("<div class=\"new_blk03\">");
        ArticleCateInfo cateinfo = MyArticleCate.GetArticleCateByID(cate_id, pub.CreateUserPrivilege("1a3208d0-70a4-49dd-8010-400f1254535a"));
        if (cateinfo != null)
        {
            strHTML.Append("<h2><a href=\"/article/category.aspx?cate_id=" + cateinfo.Article_Cate_ID + "\">更多>></a>" + cateinfo.Article_Cate_Name + "</h2>");

            QueryInfo Query = new QueryInfo();
            Query.PageSize = 8;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_CateID", "=", cate_id.ToString()));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsAudit", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "asc"));
            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));
            IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
            if (entitys != null)
            {
                strHTML.Append("<div class=\"main\">");
                strHTML.Append("<ul>");
                foreach (ArticleInfo entity in entitys)
                {
                    strHTML.Append("<li><a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\" title=\"" + entity.Article_Title + "\"  alt=\"" + entity.Article_Title + "\">" + tools.CutStr(entity.Article_Title, 45) + "</a></li>");
                }
                strHTML.Append("</ul>");
                strHTML.Append("</div>");
            }
        }
        strHTML.Append("    </div>");
        return strHTML.ToString();
    }

    public string GetArticlesRecommond()
    {
        StringBuilder strHTML = new StringBuilder();
        strHTML.Append("<div class=\"new_blk01\">");

        int i = 0;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 6;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsRecommend", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        //Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "asc"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {
            strHTML.Append("<div class=\"main\">");

            foreach (ArticleInfo entity in entitys)
            {
                i++;
                if (i == 1)
                {
                    strHTML.Append("<dl><dt>");
                    strHTML.Append("<a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\">" + entity.Article_Title + "</a></dt>");
                    strHTML.Append("<dd style=\"height:60px;\"><p>" + tools.CutStr(entity.Article_Intro, 160) + " <a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\">[全文]</a></p>");
                    strHTML.Append("</dd>");
                    strHTML.Append("</dl>");
                }
                else
                {

                    if (i == 2)
                    {
                        strHTML.Append("<div class=\"new_blk01_box\">");
                        strHTML.Append("<ul>");
                    }
                    strHTML.Append("  <li><span>" + entity.Article_Addtime.ToShortDateString() + "</span><a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\">" + tools.CutStr(entity.Article_Title, 26) + "</a></li>");

                }

            }
            if (i >= 2)
            {
                strHTML.Append("</ul>");
                strHTML.Append("</div>");
            }
            strHTML.Append("</div>");
        }
        strHTML.Append("</div>");
        return strHTML.ToString();

    }

    public string GetArticlesCenter(int cate_id)
    {
        StringBuilder strHTML = new StringBuilder();
        strHTML.Append("<div class=\"new_blk04\">");
        ArticleCateInfo cateinfo = MyArticleCate.GetArticleCateByID(cate_id, pub.CreateUserPrivilege("1a3208d0-70a4-49dd-8010-400f1254535a"));
        if (cateinfo != null)
        {
            strHTML.Append("<h2><a href=\"/article/category.aspx?cate_id=" + cateinfo.Article_Cate_ID + "\">更多>></a>" + cateinfo.Article_Cate_Name + "</h2>");

            int i = 0;
            QueryInfo Query = new QueryInfo();
            Query.PageSize = 6;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_CateID", "=", cate_id.ToString()));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsAudit", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "asc"));
            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));
            IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
            if (entitys != null)
            {
                strHTML.Append("<div class=\"main\">");

                foreach (ArticleInfo entity in entitys)
                {
                    i++;
                    if (i == 1)
                    {
                        strHTML.Append("<dl><dt>");
                        strHTML.Append("<a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\" width=\"110\" height=\"110\"/  onload=\"javascript:AutosizeImage(this,110,110);\"></a></dt>");
                        strHTML.Append("<dd><b><a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\">" + tools.CutStr(entity.Article_Title, 16) + "</a></b><p>" + tools.CutStr(entity.Article_Intro, 120) + " <a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\">[全文]</a></p>");
                        strHTML.Append("</dd>");
                        strHTML.Append(" <div class=\"clear\"></div>");
                        strHTML.Append("</dl>");
                    }
                    else
                    {
                        if (i == 2)
                        {
                            strHTML.Append("<div class=\"blk04_box\">");
                            strHTML.Append("<ul>");
                        }
                        strHTML.Append("  <li><a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\">" + tools.CutStr(entity.Article_Title, 26) + "</a></li>");
                    }

                }
                if (i >= 2)
                {
                    strHTML.Append("</ul>");
                    strHTML.Append("<div class=\"clear\"></div>");
                    strHTML.Append("</div>");
                }
                strHTML.Append("</div>");
            }
        }
        strHTML.Append("</div>");
        return strHTML.ToString();

    }

    public string GetArticlesNew()
    {
        StringBuilder strHTML = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 8;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "DESC"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {

            foreach (ArticleInfo entity in entitys)
            {
                strHTML.Append("<li><a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\" title=\"" + entity.Article_Title + "\"  alt=\"" + entity.Article_Title + "\">" + tools.CutStr(entity.Article_Title, 30) + "</a></li>");
            }
        }

        return strHTML.ToString();
    }

    public string GetArticlesIndex()
    {
        StringBuilder strHTML = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 7;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {

            foreach (ArticleInfo entity in entitys)
            {
                strHTML.Append("<li><a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\">" + entity.Article_Title + "</a></li>");
            }
        }

        return strHTML.ToString();
    }

    public string GetProductListArticles()
    {
        StringBuilder strHTML = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 5;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsRecommend", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "ASC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {
            foreach (ArticleInfo entity in entitys)
            {
                strHTML.Append("<li><a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\">" + tools.CutStr(entity.Article_Title, 30) + "</a></li>");
            }
        }

        return strHTML.ToString();
    }

    public string GetArticlesByCateRight(int cate_id)
    {
        StringBuilder strHTML = new StringBuilder();

        ArticleCateInfo cateinfo = MyArticleCate.GetArticleCateByID(cate_id, pub.CreateUserPrivilege("1a3208d0-70a4-49dd-8010-400f1254535a"));
        if (cateinfo != null)
        {
            strHTML.Append("<div class=\"new_blk02\" style=\" margin-top:10px;\">");
            strHTML.Append("<h2><a href=\"/article/category.aspx?cate_id=" + cateinfo.Article_Cate_ID + "\">更多>></a>" + cateinfo.Article_Cate_Name + "</h2>");

            QueryInfo Query = new QueryInfo();
            Query.PageSize = 8;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_CateID", "=", cate_id.ToString()));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsAudit", "=", "1"));
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
            //Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "asc"));
            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));
            IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
            if (entitys != null)
            {
                strHTML.Append("<div class=\"main\">");
                strHTML.Append("<ul>");
                foreach (ArticleInfo entity in entitys)
                {
                    strHTML.Append("<li><a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\" title=\"" + entity.Article_Title + "\"  alt=\"" + entity.Article_Title + "\">" + tools.CutStr(entity.Article_Title, 30) + "</a></li>");
                }
                strHTML.Append("</ul>");
                strHTML.Append("</div>");
            }
            strHTML.Append("    </div>");
        }

        return strHTML.ToString();
    }

    public ArticleCateInfo GetArticleCateInfoByID(int cate_id)
    {
        return MyArticleCate.GetArticleCateByID(cate_id, pub.CreateUserPrivilege("1a3208d0-70a4-49dd-8010-400f1254535a"));
    }


    public void GetArticles(int cate_id)
    {
        int i = 0;
        string keyword = tools.CheckStr(Request["keyword"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 15;
        int page = tools.CheckInt(Request["page"]);
        if (page <= 1)
        {
            page = 1;
        }
        string page_url = Request.Path + "?";
        Query.CurrentPage = page;
        if (cate_id > 0)
        {
            page_url += "&cate_id=" + cate_id;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_CateID", "=", cate_id.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        if (keyword != "")
        {
            page_url += "&keyword=" + keyword;
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Title", "like", keyword));
        }
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "asc"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        PageInfo pageinfo = MyArticle.GetPageInfo(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {
            Response.Write("<div class=\"new_list_main\">");
            Response.Write(" <ul>");
            foreach (ArticleInfo entity in entitys)
            {
                i++;
                if (i % 5 == 0)
                {
                    Response.Write("<li class=\"br\"><span>" + entity.Article_Addtime.ToShortDateString() + "</span><a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\">" + entity.Article_Title + "</a></li>");
                }
                else
                {
                    Response.Write("<li><span>" + entity.Article_Addtime.ToShortDateString() + "</span><a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\">" + entity.Article_Title + "</a></li>");
                }
            }
            Response.Write("</ul>");
            Response.Write("</div>");

            Response.Write("<div class=\"page\" style=\"float:right;\">");
            pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, page_url, pageinfo.PageSize, pageinfo.RecordCount);
            Response.Write("</div>");
        }
        else
        {
            Response.Write("<div class=\"new_list_main\">");
            Response.Write(" <ul>");
            Response.Write("<li>暂无相关信息</li>");
            Response.Write("</ul>");
            Response.Write("</div>");
        }

    }


    public ArticleInfo GetArticleInfoByID(int id)
    {
        return MyArticle.GetArticleByID(id, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
    }

    public string GetHome_Articles(int Show_Num, int cate_id)
    {
        StringBuilder strHTML = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = Show_Num;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "=", cate_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "DESC"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {

            foreach (ArticleInfo entity in entitys)
            {
                strHTML.Append("<li><a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\" title=\"" + entity.Article_Title + "\"  alt=\"" + entity.Article_Title + "\">" + tools.CutStr(entity.Article_Title, 30) + "</a><span>" + entity.Article_Addtime.ToShortDateString() + "</span></li>");
            }
        }

        return strHTML.ToString();

    }

    /// <summary>
    /// 文章左侧列表
    /// </summary>
    /// <param name="cateid"></param>
    /// <param name="helpid"></param>
    /// <returns></returns>
    public string Article_Left_Nav(int parent_id, int cateid)
    {
        StringBuilder nav_string = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_ParentID", "=", parent_id.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_Sort", "asc"));
        IList<ArticleCateInfo> entitys = MyArticleCate.GetArticleCates(Query, pub.CreateUserPrivilege("1a3208d0-70a4-49dd-8010-400f1254535a"));
        if (entitys != null)
        {
            int i = 0;
            foreach (ArticleCateInfo entity in entitys)
            {
                i++;
                if (i == 1 && cateid == 0)
                {
                    cateid = entity.Article_Cate_ID;
                }
                nav_string.Append("<div class=\"blk14_info\">");

                if (cateid == entity.Article_Cate_ID)
                {
                    nav_string.Append("<h3 class=\"on\" ><a href=\"list.aspx?cate_id=" + entity.Article_Cate_ID + "\">" + entity.Article_Cate_Name + "</a></h3>");
                }
                else
                {
                    nav_string.Append("<h3><a href=\"list.aspx?cate_id=" + entity.Article_Cate_ID + "\">" + entity.Article_Cate_Name + "</a></h3>");
                }
                nav_string.Append(Article_Left_Nav(entity.Article_Cate_ID, cateid));
                nav_string.Append("</div>");
            }
        }

        return nav_string.ToString();
    }


    public IList<ArticleInfo> GetArticlesByCateID(int cate_id)
    {
        StringBuilder strHTML = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 5;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "1"));
        if (cate_id > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "=", cate_id.ToString()));
        }
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_IsRecommend", "DESC"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "DESC"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        return entitys;
    }

    public string Index_Article()
    {
        StringBuilder strHTML = new StringBuilder();
        int i = 0;

        IList<ArticleInfo> entitys = GetArticlesByCateID(37);

        if (entitys != null)
        {
            strHTML.Append("<h2>");
            strHTML.Append("<strong>有话说</strong>");
            strHTML.Append("<ul class=\"lst2\">");

            for (int j = 1; j <= entitys.Count; j++)
            {
                strHTML.Append("<li>" + j + "</li>");
            }
            strHTML.Append("</ul>");
            strHTML.Append("<div class=\"clear\"></div>");
            strHTML.Append("</h2>");


            foreach (ArticleInfo entity in entitys)
            {
                i++;

                if (i == 1)
                {
                    strHTML.Append("<div class=\"lst2main\">");
                }
                else
                {
                    strHTML.Append("<div class=\"lst2main\"  style=\"display: none;\">");
                }

                strHTML.Append("<dl>");
                strHTML.Append("<dt><img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\"></dt>");
                //strHTML.Append("<dt><a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\"><img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\"></a></dt>");
                strHTML.Append("<dd>");
                strHTML.Append("<b>" + entity.Article_Author + "<span>" + entity.Article_Source + "</span></b>");
                strHTML.Append("<p>" + entity.Article_Intro + "</p>");
                strHTML.Append("</dd>");
                strHTML.Append("<div class=\"clear\"></div>");
                strHTML.Append("</dl>");
                strHTML.Append("</div>");
            }
        }
        return strHTML.ToString();
    }

    /// <summary>
    /// 上一篇/下一篇
    /// </summary>
    /// <param name="sort">1上一篇/0 下一篇</param>
    /// <param name="cateid"></param>
    /// <returns></returns>
    public string GetPreAndNext(int sort, int cateid)
    {

        int id = tools.CheckInt(Request["article_id"]);


        QueryInfo Query = new QueryInfo();
        Query.CurrentPage = 1;
        Query.PageSize = 1;

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_CateID", "=", cateid.ToString()));

        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsAudit", "=", "1"));
        //if (sort == 1)
        //{
        //    Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_ID", ">", id.ToString()));
        //    Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "Asc"));
        //    Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "ASC"));
        //}
        //else
        //{
        //    Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_ID", "<", id.ToString()));
        //    Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "Asc"));
        //    Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));
        //}
        if (sort == 1)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_ID", "<", id.ToString()));
            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "Desc"));
            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));
        }
        else
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_ID", ">", id.ToString()));
            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "Asc"));
            Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Asc"));
        }

        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));

        StringBuilder sbA = new StringBuilder();

        if (entitys != null)
        {


            ArticleInfo entity = entitys[0];
            string targetUrl;
            targetUrl = "?article_id=" + entity.Article_ID;// pageurl.FormatURL(pageurl.health_detail, entity.Knowledge_ID.ToString());

            string title = entity.Article_Title.Length > 30 ? entity.Article_Title.Substring(0, 30) + "..." : entity.Article_Title;
            if (sort == 1)
            {

                sbA.Append(" <a href=\"" + targetUrl + "\">上一篇：" + title + "</a>");
            }
            else
            {
                sbA.Append("<span><a href=\"" + targetUrl + "\">下一篇：" + title + "</a></span>");

            }
        }
        else
        {
            if (sort == 1)
            {
                //sbA.Append("<li>上一篇：没有了</li>");
                sbA.Append("<span style=\"color:#666;float:left;\">上一篇：没有了</span>");
            }
            else
            {
                //sbA.Append("<span><li>下一篇：没有了</li></span>");
                sbA.Append("<span style=\"color:#666\">下一篇：没有了</span>");
            }

        }
        return sbA.ToString();
    }
    #endregion

    public void Member_Index_Notice()
    {
        StringBuilder strHTML = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 7;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "NoticeInfo.Notice_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "NoticeInfo.Notice_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("NoticeInfo.Notice_ID", "DESC"));
        IList<NoticeInfo> notices = Webnotice.GetNotices(Query, pub.CreateUserPrivilege("9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7"));

        strHTML.Append("<div class=\"b14_main_1\">");
        strHTML.Append("<ul>");
        if (notices != null)
        {
            foreach (NoticeInfo entity in notices)
            {
                strHTML.Append("<li><a href=\"/notice/detail.aspx?notice_id=" + entity.Notice_ID + "\" title=\"" + entity.Notice_Title + "\">" + tools.CutStr(entity.Notice_Title, 30) + "</a></li>");
            }
        }
        strHTML.Append("</ul>");
        strHTML.Append("</div>");
        Response.Write(strHTML.ToString());
    }

    public void Member_Index_Help()
    {
        StringBuilder strHTML = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 7;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "HelpInfo.Help_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "HelpInfo.Help_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("HelpInfo.Help_ID", "asc"));
        IList<HelpInfo> helps = MyHelp.GetHelps(Query, pub.CreateUserPrivilege("a015e960-173c-429d-98d2-69e5a023b5dc"));

        strHTML.Append("<div class=\"b14_main_1\">");
        strHTML.Append("<ul>");
        if (helps != null)
        {
            foreach (HelpInfo entity in helps)
            {
                strHTML.Append("<li><a href=\"/help/index.aspx?help_id=" + entity.Help_ID + "\" title=\"" + entity.Help_Title + "\">" + tools.CutStr(entity.Help_Title, 30) + "</a></li>");
            }
        }
        strHTML.Append("</ul>");
        strHTML.Append("</div>");
        Response.Write(strHTML.ToString());
    }


    public void Login_Bottom_About(int showNum)
    {
        int i = 0;

        StringBuilder strHTML = new StringBuilder();

        QueryInfo Query = new QueryInfo();
        Query.PageSize = showNum;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "AboutInfo.About_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "AboutInfo.About_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("AboutInfo.About_Sort", "ASC"));
        IList<AboutInfo> abouts = MyAbout.GetAbouts(Query, pub.CreateUserPrivilege("db8de73b-9ac0-476e-866e-892dd35589c5"));
        Query = null;
        if (abouts != null)
        {
            foreach (AboutInfo entity in abouts)
            {
                i++;

                strHTML.Append("<a href=\"/about/index.aspx?sign=" + entity.About_Sign + "\">" + entity.About_Title + "</a>");

                if (i >= 1 && i < abouts.Count)
                {
                    strHTML.Append(" / ");
                }
            }
        }

        Response.Write(strHTML.ToString());
    }


    public void GetHangYeInfoByCateID()
    {
        int page = tools.CheckInt(Request["page"]);
        if (page < 1)
        {
            page = 1;
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = page;
        string sql = "(select Article_Cate_ID from Article_Cate where Article_Cate_Name='行情资讯')";
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_CateID", "in", sql));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "DESC"));
        IList<ArticleInfo> ArticleInfos = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));

        if (ArticleInfos != null)
        {
            foreach (ArticleInfo articleinfo in ArticleInfos)
            {
                Response.Write("          <dl>");
                Response.Write("              <dt><a href=\"/article/detail.aspx?article_id=" + articleinfo.Article_ID + "\" ><img src=" + pub.FormatImgURL(articleinfo.Article_Img, "fullpath") + "></a></dt>");
                Response.Write("              <dd>");
                Response.Write("                  <b><a href=\"/article/detail.aspx?article_id=" + articleinfo.Article_ID + "\" >" + articleinfo.Article_Title + "</a></b>");
                Response.Write("                  <p>" + articleinfo.Article_Intro + "</p>");
                Response.Write("                  <i>来源：" + articleinfo.Article_Source + "  |  编辑：" + articleinfo.Article_Author + " |  时间：" + articleinfo.Article_Addtime.ToShortDateString() + "</i>");
                Response.Write("              </dd>");
                Response.Write("              <div class=\"clear\"></div>");
                Response.Write("          </dl>");
            }
        }




        PageInfo pageinfo = MyArticle.GetPageInfo(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (pageinfo != null && pageinfo.RecordCount >= 1)
        {
            string url = "/article/index.aspx?article_id=0";

            pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, url, pageinfo.PageSize, pageinfo.RecordCount);
        }


        //Response.Write(strHTML.ToString());
    }

    //金融服务页面 关于易耐网金融说明
    public string GetFinancialServiceInfoByCateID()
    {
        int page = 0;
        //if (page < 1)
        //{
        //    page = 1;
        //}
        string GetFinancialServiceContent = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = page;
        string sql = "(select Article_Cate_ID from Article_Cate where Article_Cate_Name='金融服务页面')";
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_CateID", "in", sql));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "DESC"));
        IList<ArticleInfo> ArticleInfos = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));

        if (ArticleInfos != null)
        {
            int i = 0;
            foreach (ArticleInfo articleinfo in ArticleInfos)
            {
                i++;
                if (i == 1)
                {
                    GetFinancialServiceContent = articleinfo.Article_Content;
                }

            }
        }



        return GetFinancialServiceContent;
    }


    public void GetHangYeSort()
    {
        StringBuilder strHTML = new StringBuilder();
        IList<ArticleInfo> ArticleInfos = GetArticlesByCateID(0);
        strHTML.Append("  <ul>");
        if (ArticleInfos != null)
        {
            int i = 0;
            foreach (ArticleInfo ArticleInfo in ArticleInfos)
            {
                i++;
                if (i < 4)
                {
                    strHTML.Append(" <li><i class=\"bg\">" + i + "</i><a href=\"/article/detail.aspx?article_id=" + ArticleInfo.Article_ID + "\" >" + tools.CutStr(ArticleInfo.Article_Title, 30) + "</a></li>");
                }
                else
                {
                    strHTML.Append(" <li><i>" + i + "</i><a href=\"/article/detail.aspx?article_id=" + ArticleInfo.Article_ID + "\" >" + tools.CutStr(ArticleInfo.Article_Title, 30) + "</a></li>");
                }
            }
        }
        strHTML.Append("  </ul>");
        Response.Write(strHTML.ToString());
    }

    //相关文章
    public void GetRelateHangYeSort(int max_num)
    {
        StringBuilder strHTML = new StringBuilder();
        IList<ArticleInfo> ArticleInfos = GetAllArticles();
        strHTML.Append("<ul>");
        if (ArticleInfos != null)
        {
            int i = 0;
            foreach (ArticleInfo ArticleInfo in ArticleInfos)
            {
                i++;
                if (i < max_num)
                {

                    strHTML.Append("   <li><a href=\"/article/detail.aspx?article_id=" + ArticleInfo.Article_ID + "\" >" + tools.CutStr(ArticleInfo.Article_Title, 30) + "</a></li>");

                }
            }
        }
        strHTML.Append("  </ul>");
        Response.Write(strHTML.ToString());

    }



    public IList<ArticleInfo> GetAllArticles()
    {
        StringBuilder strHTML = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "DESC"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        return entitys;
    }


    public string Home_Article_List()
    {
        StringBuilder strHTML = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = 1;
        string sql = " (select article_cate_id from Article_Cate where Article_Cate_Name in( '首页4F大图右侧3篇文章','首页4F大图文章下面6篇文章','首页4F大图文章') )";
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", sql));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsRecommend", "=", "1"));

        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "asc"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {
            int n = 0;
            foreach (ArticleInfo entity in entitys)
            {
                n++;
                if (entity.Article_CateID == 41)
                {

                    strHTML.Append("<div class=\"new_left\">");
                    strHTML.Append("    <div class=\"new_1\">");

                    strHTML.Append(new AD().AD_Show("Home_4F_LeftAD", "", "cycle", 0));
                    strHTML.Append("      <a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\">" + tools.CutStr(entity.Article_Title, 40) + "</a>");
                    strHTML.Append("    </div>");
                    strHTML.Append("    <div class=\"new_2\">");
                    strHTML.Append("<ul style=\"float: left;\">");
                }
                if (n > 1 && n < 4)
                {
                    if (entity.Article_CateID == 42)
                    {
                        strHTML.Append("<li><a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\">" + tools.CutStr(entity.Article_Title, 25) + "</a></li>");
                    }


                }
                if (n == 4)
                {
                    if (entity.Article_CateID == 42)
                    {
                        strHTML.Append("<li><a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\">" + tools.CutStr(entity.Article_Title, 25) + "</a></li>");
                        strHTML.Append("    </ul>");
                        strHTML.Append("<ul style=\"float: right;\">");
                    }

                }
                if (n > 4 && n < 7)
                {
                    if (entity.Article_CateID == 42)
                    {
                        strHTML.Append("<li><a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\">" + tools.CutStr(entity.Article_Title, 25) + "</a></li>");
                    }

                }
                if (n == 7)
                {
                    if (entity.Article_CateID == 42)
                    {
                        strHTML.Append("<li><a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\">" + tools.CutStr(entity.Article_Title, 25) + "</a></li>");
                        strHTML.Append("    </ul>");

                        strHTML.Append("    </div>");
                        strHTML.Append("</div>");
                        strHTML.Append("<div class=\"new_right\">");
                        strHTML.Append("    <div class=\"new_3\">");
                        strHTML.Append("        <h2 class=\"new_3_h2\">聚焦</h2>");
                        strHTML.Append("        <div class=\"new_3_list\">");
                        strHTML.Append("            <ul>");
                    }

                }
                if (n > 7)
                {
                    if (entity.Article_CateID == 43)
                    {
                        strHTML.Append("<li>");
                        strHTML.Append("    <span style=\"float: left;\"><a href=\"javascript:void(0);\">");
                        strHTML.Append("        <img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\" width=\"90\" height=\"78\" /></a></span>");
                        strHTML.Append("    <span style=\"width: 368px; float: right;\">");
                        strHTML.Append("        <p><a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\">" + tools.CutStr(entity.Article_Title, 50) + "</a></p>");
                        strHTML.Append("        <p><a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\">" + tools.CutStr(entity.Article_Intro, 150) + "<font>【查看详情】</font></a></p>");
                        strHTML.Append("    </span>");
                        strHTML.Append("</li>");

                    }

                }
            }


            strHTML.Append("        </ul>");
            strHTML.Append("    </div>");
            strHTML.Append("</div>");

            strHTML.Append("</div>");
        }
        return strHTML.ToString();
    }

    //  4F行情资讯
    public string Home_4F_InterMarket()
    {
        StringBuilder strHTML = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = 1;
        string sql = " (select article_cate_id from Article_Cate where Article_Cate_Name in('首页4F大图文章') )";
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", sql));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsRecommend", "=", "1"));

        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "asc"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {
            int i = 0;
            foreach (ArticleInfo entity in entitys)
            {
                i++;
                if (i == 1)
                {
                    strHTML.Append(new AD().AD_Show("Home_4F_LeftAD", "", "cycle", 0));
                    strHTML.Append("      <a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\">" + tools.CutStr(entity.Article_Title, 40) + "</a>");
                }
                else
                {
                    break;
                }

            }
        }


        return strHTML.ToString();
    }

    //4F 行情资讯底部六篇文章
    public string Home_4F_InterMarketBottomArticles(string Article_Cate_Name)
    {
        StringBuilder strHTML = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = 1;
        string sql = " (select article_cate_id from Article_Cate where Article_Cate_Name in('" + Article_Cate_Name + "') )";
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", sql));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsRecommend", "=", "1"));

        //Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "asc"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {
            int i = 0;
            foreach (ArticleInfo entity in entitys)
            {
                i++;
                if (i < 4)
                {
                    strHTML.Append("    <li>  <a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\" title=" + entity.Article_Title + ">" + tools.CutStr(entity.Article_Title, 22) + "</a></li>");
                }
                else
                {
                    break;
                }

            }
        }


        return strHTML.ToString();
    }



    //4F 行情资讯 右侧三篇文章
    public string Home_4F_InterMarketRightArticles(string Article_Cate_Name)
    {
        StringBuilder strHTML = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 10;
        Query.CurrentPage = 1;
        string sql = " (select article_cate_id from Article_Cate where Article_Cate_Name in('" + Article_Cate_Name + "') )";
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", sql));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsRecommend", "=", "1"));

        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "asc"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));

        strHTML.Append("     <div class=\"new_3\">");
        strHTML.Append("                      	<h2 class=\"new_3_h2\">聚焦</h2>");
        strHTML.Append("                          <div class=\"new_3_list\">");
        strHTML.Append("                          	<ul>");

        int j = 0;
        if (entitys != null)
        {

            //if ((Article_Cate_Name == "首页4F大图右侧下面1篇文章"))
            //{

            //    foreach (ArticleInfo entity in entitys)
            //    {
            //        j++;
            //        if (j<3)
            //        {
            //            strHTML.Append(" <li style=\"margin:0px; border:0px;\">");


            //            strHTML.Append("    <span style=\"float: left;\"><a href=\"javascript:void(0);\">");
            //            strHTML.Append("        <img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\" width=\"90\" height=\"78\" /></a></span>");
            //            strHTML.Append("    <span style=\"width: 368px; float: right;\">");
            //            strHTML.Append("        <p><a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\">" + tools.CutStr(entity.Article_Title, 50) + "</a></p>");
            //            strHTML.Append("        <p><a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\">" + tools.CutStr(entity.Article_Intro, 150) + "<font>【查看详情】</font></a></p>");
            //            strHTML.Append("    </span>");
            //            strHTML.Append("</li>");
            //        }


            //    }
            //}
            if ((Article_Cate_Name == "首页4F大图右侧上面3篇文章") && (j < 4))
            {

                foreach (ArticleInfo entity in entitys)
                {
                    j++;
                    if (j < 4)
                    {
                        strHTML.Append("  <li>");

                        strHTML.Append("    <span style=\"float: left;\"><a href=\"javascript:void(0);\">");
                        strHTML.Append("        <img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\" width=\"90\" height=\"78\" /></a></span>");
                        strHTML.Append("    <span style=\"width: 368px; float: right;\">");
                        strHTML.Append("        <p><a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\">" + tools.CutStr(entity.Article_Title, 50) + "</a></p>");
                        strHTML.Append("        <p><a href=\"/article/detail.aspx?article_id=" + entity.Article_ID + "\" target=\"_blank\">" + tools.CutStr(entity.Article_Intro, 150) + "<font>【查看详情】</font></a></p>");
                        strHTML.Append("    </span>");
                        strHTML.Append("</li>");
                    }


                }
            }


        }


        strHTML.Append("       </ul>");
        strHTML.Append("                      </div>");
        strHTML.Append("                  </div>");

        return strHTML.ToString();
    }

    public string Home_Right_Article()
    {
        StringBuilder strHTML = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 1;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "DESC"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {
            strHTML.Append("<span>");
            strHTML.Append("    <img src=\"" + pub.FormatImgURL(entitys[0].Article_Img, "fullpath") + "\" width=\"90\" height=\"78\" /></span>");
            strHTML.Append("<span style=\"width: 158px; margin-left: 7px;\"><a href=\"/article/detail.aspx?article_id=" + entitys[0].Article_ID + "\" target=\"_blank\">" + tools.CutStr(entitys[0].Article_Title, 20) + "</a><br />");
            strHTML.Append("    <a href=\"/article/detail.aspx?article_id=" + entitys[0].Article_ID + "\" target=\"_blank\">" + tools.CutStr(entitys[0].Article_Intro, 40) + "【查看详情】</a></span>");
        }
        return strHTML.ToString();
    }
    //public string Home_Right_Article()
    //{
    //    StringBuilder strHTML = new StringBuilder();
    //    QueryInfo Query = new QueryInfo();
    //    Query.PageSize = 1;
    //    Query.CurrentPage = 1;
    //    Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_IsAudit", "=", "1"));
    //    Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
    //    Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "DESC"));
    //    IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
    //    if (entitys != null)
    //    {
    //        strHTML.Append("<span>");
    //        strHTML.Append("    <img src=\"" + pub.FormatImgURL(entitys[0].Article_Img, "fullpath") + "\" width=\"90\" height=\"78\" /></span>");
    //        strHTML.Append("<span style=\"width: 158px; margin-left: 7px;\"><a href=\"/article/detail.aspx?article_id=" + entitys[0].Article_ID + "\" target=\"_blank\">" + tools.CutStr(entitys[0].Article_Title, 20) + "</a><br />");
    //        strHTML.Append("    <a href=\"/article/detail.aspx?article_id=" + entitys[0].Article_ID + "\" target=\"_blank\">" + tools.CutStr(entitys[0].Article_Intro, 40) + "【查看详情】</a></span>");
    //    }
    //    return strHTML.ToString();
    //}


    public string Home_2F_Scroll(int ArticleCateID)
    {
        StringBuilder strHTML = new StringBuilder();

        //产品
        //状态
        //数量(吨)
        //时间
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 8;
        //string sql="(select Article_Cate_ID from Article_Cate where Article_Cate_Name='首页二楼右侧销量信息')";
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "=", ArticleCateID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "asc"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {
            foreach (var entity in entitys)
            {
                string Article_Title = entity.Article_Title;
                string state_system_String = "";
                string count_system_String = "";
                string adress_system_String = "";
                //string str = entity.Article_Intro;
                string str = entity.Article_Content;
                if (str.Contains("|"))
                {
                    string[] showinfo_sysytem_Arry = str.Split('|');
                    if (showinfo_sysytem_Arry.Length == 3)
                    {
                        state_system_String = tools.NullStr(showinfo_sysytem_Arry[0]);
                        count_system_String = tools.NullStr(showinfo_sysytem_Arry[1]);
                        adress_system_String = tools.NullStr(showinfo_sysytem_Arry[2]);
                    }
                    
                    //int i = str.IndexOf("|");
                    //str1 = str.Substring(0, i);
                    //str2 = str.Substring(i + 1, str.Length - i - 1);
                }
                if (entity.Article_Title.Length > 7)
                {
                    Article_Title = entity.Article_Title.Substring(0, 7);
                }
                strHTML.Append("   <ul>");
                strHTML.Append("                           <li style=\"float: left;width:110px; background-color: #ffffff; color: black; border-bottom: 1px dotted #ff6600;\"><span>" + Article_Title + "</span></li>");
                strHTML.Append("                           <li style=\"float: left;width:40px; background-color: #ffffff; color: black; border-bottom: 1px dotted #ff6600;\"><span>" + state_system_String + "</span></li>");
                strHTML.Append("                          <li style=\"float: left;width:60px; background-color: #ffffff; color: black; border-bottom: 1px dotted #ff6600;\"><span>" + count_system_String + "</span></li>");
                strHTML.Append("                          <li style=\"float: left;width:58px; background-color: #ffffff; color: black; border-bottom: 1px dotted #ff6600;\"><span>" + adress_system_String + "</span></li>");
                strHTML.Append("                       </ul>");
            }

        }
        return strHTML.ToString();
    }

    //英文首页 轮播图下方 内容
    public string GetEnglishHomeBigPicBelow1()
    {
        StringBuilder strHTML = new StringBuilder();
        strHTML.Append("      <ul>");
        strHTML.Append("      	<li>");


        strHTML.Append("          	<p>");

        //strHTML.Append("       <img src=\"/images/englishimg17.png\" width=\"270\" height=\"149\" />");
        strHTML.Append(new AD().AD_Show("EnglishHome_BigPic_Below", "1", "cycle", 0));


        strHTML.Append("       </p>");
        strHTML.Append("              <p><strong>Market Dynamics</strong></p>");
        strHTML.Append("              <p>");
        strHTML.Append("              	<ul class=\"En_info1_list\">");





        //产品
        //状态
        //数量(吨)
        //时间
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 3;
        //string sql="(select Article_Cate_ID from Article_Cate where Article_Cate_Name='首页二楼右侧销量信息')";
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "=", "45"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "asc"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));



        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {
            int i = 0;
            foreach (ArticleInfo entity in entitys)
            {
                i++;
                if (i < 4)
                {
                    strHTML.Append("                  	<li><a href=\"javascript:void(0);\">铝矾土价格浮动</a></li>");

                }
                else
                {
                    break;
                }

            }

        }
        //strHTML.Append("                  	<li><a href=\"javascript:void(0);\">铝矾土价格浮动</a></li>");

        //strHTML.Append("                  	<li><a href=\"javascript:void(0);\">铝矾土价格浮动</a></li>");	

        strHTML.Append("                  </ul>");
        strHTML.Append("              </p>");
        strHTML.Append("          </li>");
        strHTML.Append("          <li>");
        //strHTML.Append("          	<p><img src=\"/images/englishimg18.png\" width=\"270\" height=\"149\" /></p>");
        strHTML.Append(new AD().AD_Show("EnglishHome_BigPic_Below", "2", "cycle", 0));
        strHTML.Append("              <p><strong>Platform Profile</strong></p>");
        strHTML.Append("              <p>易耐网由一群朝气蓬勃的年轻人在2016年7月1日创立，为打造全国最大的耐火材料线上交易平台而努力。公司的愿景是推动传统行业线上与线下相结合，解决目前市场零碎，合同违约，相互拖欠等状况。使命是整合材料行业零碎的生产资源，规范行业的秩序发展。");
        strHTML.Append("               </p>");
        strHTML.Append("          </li>");
        strHTML.Append("          <li>");

        strHTML.Append(new AD().AD_Show("EnglishHome_BigPic_Below", "3", "cycle", 0));
        strHTML.Append("              <p><strong>Corporate Culture</strong></p>");
        strHTML.Append("              <p>文化：诚信，高效，安全，便捷<br />				使命 : 整合行业材料零碎的生产资源，规范行业的秩序发展<br />");
        strHTML.Append("  			愿景：建设质量100%，服务100%物流100%的B2B电子交易平台<br /></p>");
        strHTML.Append("          </li>");
        strHTML.Append("     </ul>");



        return strHTML.ToString();
    }

    //获取英文版主页面下方文章
    public string GetEnglishHomeBigPicBelowArticle()
    {
        StringBuilder strHTML = new StringBuilder();


        strHTML.Append("    <ul>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 3;
        //string sql="(select Article_Cate_ID from Article_Cate where Article_Cate_Name='首页二楼右侧销量信息')";
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "=", "60"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "asc"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));

        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));

        if (entitys != null)
        {
            int j = 0;
            foreach (var entity in entitys)
            {
                j++;
                if (j == 1)
                {
                    strHTML.Append("     <p>");
                    strHTML.Append("         <strong style=\"display: block; font-size: 24px; line-height: 36px; text-align: center; width: 1200px;\">" + entity.Article_Title + "</strong>");
                    strHTML.Append("     </p>");
                    strHTML.Append("     <p style=\"font-size:14px;line-height:26px;text-align:justify;padding-top:5px;\">" + entity.Article_Content + "</p>");

                }





            }
        }






        //</li>
        strHTML.Append("  </ul>");


        return strHTML.ToString();

    }

    public string GetEnglishHomeBigPicBelow()
    {
        StringBuilder strHTML = new StringBuilder();


        strHTML.Append("    <ul>");

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 3;
        //string sql="(select Article_Cate_ID from Article_Cate where Article_Cate_Name='首页二楼右侧销量信息')";
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "=", "48"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "asc"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));

        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));

        if (entitys != null)
        {
            int j = 0;
            foreach (var en in entitys)
            {
                j++;
                if (j < 3)
                {
                    if (j == 2)
                    {
                        strHTML.Append("    	<li style=\"width:477px;float:right;\">");
                        strHTML.Append("        	<p>");
                        strHTML.Append(" <a href=\"/International/detail.aspx?id=" + en.Article_ID + "\"><img src=\"" + pub.FormatImgURL(en.Article_Img, "fullpath") + "\" width=\"270\" height=\"149\" /></a>");
                        strHTML.Append("    </p>");
                        strHTML.Append("            <p style=\"    font-size: 24px;    width: 477px;    text-align: center;    display: block;margin-top:5px;    line-height: 36px;\"><a href=\"/International/detail.aspx?id=" + en.Article_ID + "\">" + en.Article_Title + "</a></p>");
                        strHTML.Append("            <p>");
                        strHTML.Append("            	<ul class=\"En_info1_list\">");
                        QueryInfo Query1 = new QueryInfo();
                        Query1.PageSize = 3;

                        Query1.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "=", "49"));
                        Query1.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsAudit", "=", "1"));
                        Query1.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
                        Query1.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "asc"));
                        Query1.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));
                        IList<ArticleInfo> entitys1 = MyArticle.GetArticles(Query1, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));

                        if (entitys1 != null)
                        {
                            int j1 = 0;

                            foreach (ArticleInfo item in entitys1)
                            {
                                j1++;
                                if (j1 == 1)
                                {
                                    //strHTML.Append("                  	<li><a href=\"javascript:void(0);\">" + item.Article_Title + "</a></li>");
                                    strHTML.Append("                  	<li><a href=\"/International/detail.aspx?id=" + item.Article_ID + "\">" + item.Article_Title + "</a></li>");
                                }
                                else
                                {
                                    break;
                                }

                            }

                        }
                        strHTML.Append("                </ul>");
                        strHTML.Append("            </p>");
                        strHTML.Append("        </li>");
                    }
                    else
                    {

                        if (j == 2)
                        {
                            strHTML.Append("    <li style=\"margin-right:0px;width:477px;\">");
                            strHTML.Append("        	<p>");




                            strHTML.Append(" <a href=\"/International/detail.aspx?id=" + en.Article_ID + "\"> <img src=\"" + pub.FormatImgURL(en.Article_Img, "fullpath") + "\" width=\"477\" height=\"207\" style=\"float:right;\" />");
                            strHTML.Append("   </a> </p>");
                            strHTML.Append("            <p style=\"    font-size: 24px;    width: 477px;    text-align: center;    display: block;margin-top:5px;    line-height: 36px;\"> <a href=\"/International/detail.aspx?id=" + en.Article_ID + "\">" + en.Article_Title + "</a></p>");
                            strHTML.Append("            <p>");
                            strHTML.Append("      <p><a href=\"/International/detail.aspx?id=" + en.Article_ID + "\">" + en.Article_Intro + "");
                            strHTML.Append(" </a></p>");
                            strHTML.Append("    </li>");
                        }
                        else
                        {
                            strHTML.Append("    <li style=\"margin-left:0px;width:477px;\">");
                            strHTML.Append("        	<p>");




                            strHTML.Append(" <a href=\"/International/detail.aspx?id=" + en.Article_ID + "\"><img src=\"" + pub.FormatImgURL(en.Article_Img, "fullpath") + "\" width=\"477\" height=\"207\" style=\"float:right;\" />");
                            strHTML.Append("   </a> </p>");
                            strHTML.Append("            <p style=\"    font-size: 24px;    width: 477px;    text-align: center;    display: block;margin-top:5px;    line-height: 36px;\"><a href=\"/International/detail.aspx?id=" + en.Article_ID + "\">" + en.Article_Title + "</a></p>");
                            strHTML.Append("            <p>");
                            strHTML.Append("      <p> <a href=\"/International/detail.aspx?id=" + en.Article_ID + "\">" + en.Article_Intro + "");
                            strHTML.Append("  </a> </p>");
                            strHTML.Append("    </li>");
                        }
                    }
                }






            }
        }






        //</li>
        strHTML.Append("  </ul>");


        return strHTML.ToString();

    }

    //英文首页 Product 产品分类  
    public string GetEnglishHomeProductCategory()
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("     <div class=\"En_info2_list\">");
        strHTML.Append("  	<ul>");


        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        //string sql="(select Article_Cate_ID from Article_Cate where Article_Cate_Name='首页二楼右侧销量信息')";
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "=", "46"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "asc"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));

        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {
            int i = 0;

            foreach (ArticleInfo entity in entitys)
            {
                i++;

                if (i % 4 == 1)
                {
                    strHTML.Append("    <li style=\"margin-left:0;\">");
                    strHTML.Append("  	<p><img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\" width=\"225\" height=\"165\" /></p>");
                    strHTML.Append("      <p>" + entity.Article_Title + "</p>");
                    strHTML.Append("       <p>" + entity.Article_Intro + "</p>");
                    strHTML.Append("      </li>");
                }
                else if (i % 4 == 0)
                {
                    strHTML.Append("    <li style=\"margin-right:0;\">");

                    strHTML.Append("  	<p><img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\" width=\"225\" height=\"165\" /></p>");
                    strHTML.Append("      <p>" + entity.Article_Title + "</p>");
                    strHTML.Append("       <p>" + entity.Article_Intro + "</p>");
                    strHTML.Append("      </li>");
                }
                else
                {
                    strHTML.Append("    <li>");
                    strHTML.Append("  	<p><img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\" width=\"225\" height=\"165\" /></p>");
                    strHTML.Append("      <p>" + entity.Article_Title + "</p>");
                    strHTML.Append("       <p>" + entity.Article_Intro + "</p>");
                    strHTML.Append("      </li>");
                }




            }
        }
        strHTML.Append("  </ul>");
        strHTML.Append("     </div>");
        return strHTML.ToString();
    }

    //获取所有子分类
    public string Get_All_SubCate(int Cate_id)
    {
        string Cate_Arry = MyCate.Get_All_SubCateID(Cate_id);
        return Cate_Arry;
    }
    //获取指定分类指定标签商品信息(通过标签号)  /**/
    public IList<ProductInfo> GetCateTagProduct_TagID(int Show_Num, int cate_id, int tag_id)
    {
        //获取所有子类编号
        string sub_cate = Get_All_SubCate(cate_id);
        if (sub_cate == "")
        {
            return null;
        }
        else
        {
            //获取推荐标签信息
            ProductTagInfo Taginfo = MyTag.GetProductTagByID(tag_id, pub.CreateUserPrivilege("ed87eb87-dade-4fbc-804c-c139c1cbe9c8"));
            if (Taginfo != null)
            {
                QueryInfo Query = new QueryInfo();
                Query.PageSize = Show_Num;
                Query.CurrentPage = 1;
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", "SELECT Product_RelateTag_ProductID FROM Product_RelateTag WHERE Product_RelateTag_TagID = " + Taginfo.Product_Tag_ID + ""));


                if (sub_cate.Length > 0 && cate_id > 0)
                {
                    Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ProductInfo.Product_CateID", "in", sub_cate));
                    Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ProductInfo.Product_ID", "in", "SELECT Product_Category_ProductID FROM Product_Category WHERE Product_Category_CateID in (" + sub_cate + ")"));
                }
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));

                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Sort", "Asc"));
                Query.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "DESC"));
                IList<ProductInfo> Products = Myproduct.GetProductList(Query, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));

                return Products;

            }
            else
            {
                return null;
            }

        }
    }


    //英文首页  产品展示内容   Home_1Floor
    public string GetEnglishHomeProductShow1()
    {
        int ShowNum = 14;
        string TagName = "首页商品推荐(1F商城中心)";
        StringBuilder strHTML = new StringBuilder();
        strHTML.Append("    <h2><a href=\"/product/category.aspx\" class=\"more\">更多 ></a></a><strong><i>1F</i>商城中心</strong><b>");


        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ID", ">", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ParentID", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_IsActive", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_Sort", "asc"));
        int i = 0;
        IList<CategoryInfo> Categorys = MyCate.GetCategorys(Query, pub.CreateUserPrivilege("2883de94-8873-4c66-8f9a-75d80c004acb"));
        int CategoryNum = Categorys.Count;
        if (Categorys != null)
        {
            foreach (CategoryInfo Category in Categorys)
            {
                i++;
                strHTML.Append("   <a href=\"/product/category.aspx?cate_id=" + Category.Cate_ID + "\" id=\"a0" + i + "\"  >" + Category.Cate_Name + "</a>");

            }

            strHTML.Append("     </b></h2>");


            strHTML.Append("         <div class=\"f_main\">");
            strHTML.Append("             <div class=\"f_info01\" >");
            strHTML.Append("                 <img src=\"/images/ad_pic03.jpg\" style=\"height:513px;width:213px;\"></a><div class=\"f_info01_box\"><b>热门推荐</b><p>");
            //strHTML.Append(adclass.AD_Show("Home_Floor1_Tag", "", "keyword", 0));
            strHTML.Append("              </p>");
            strHTML.Append("                 </div>");
            strHTML.Append("             </div>");



            int ij = 0;
            QueryInfo Query1;
            int jj = 0;
            foreach (CategoryInfo category in Categorys)
            {
                ij++;
                jj++;
                var css = "";
                if (ij >= 2)
                {
                    css = "style='display:none;width:704px!important;'";
                }
                else
                {
                    css = " style='width:704px!important;'";
                }

                strHTML.Append("             <div  class=\"f_info02_2\" " + css + "  id=\"aa0" + ij + "\">");
                strHTML.Append("                 <ul>");
                string GetAllCateProduct = Get_All_SubCate(category.Cate_ID);
                string product_cate = "";
                string productsArray1 = "0";
                IList<ProductInfo> products1 = GetCateTagProduct_TagID(100, 0, 5);
                if (products1 != null)
                {
                    foreach (ProductInfo products11 in products1)
                    {
                        product_cate = Myproduct.GetProductCategory(products11.Product_ID);

                        if (product_cate.Length > 0)
                        {
                            string[] sArrays = product_cate.Split(new char[] { ',' });
                            foreach (string sArray in sArrays)
                            {
                                if (GetAllCateProduct.Contains(sArray))
                                {
                                    productsArray1 += "," + products11.Product_ID;
                                }
                            }
                        }
                    }

                }


                Query1 = null;
                Query1 = new QueryInfo();
                Query1.PageSize = 0;
                Query1.CurrentPage = 1;
                Query1.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_ID", "in", "" + productsArray1 + " "));

                Query1.ParamInfos.Add(new ParamInfo("AND", "str", "ProductInfo.Product_Site", "=", "CN"));
                Query1.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsInsale", "=", "1"));
                Query1.ParamInfos.Add(new ParamInfo("AND", "int", "ProductInfo.Product_IsAudit", "=", "1"));
                Query1.OrderInfos.Add(new OrderInfo("ProductInfo.Product_Sort", "Desc"));
                Query1.OrderInfos.Add(new OrderInfo("ProductInfo.Product_ID", "Desc"));
                IList<ProductInfo> products = Myproduct.GetProducts(Query1, pub.CreateUserPrivilege("ae7f5215-a21a-4af2-8d47-3cda2e1e2de8"));



                if (products != null)
                {
                    int ii = 0;
                    foreach (ProductInfo product in products)
                    {
                        ii++;
                        if (ii < 7)
                        {
                            strHTML.Append("<li>");
                            strHTML.Append("                         <div class=\"logo_box\"><a href=\"" + pageurl.FormatURL(pageurl.product_detail, product.Product_ID.ToString()) + " \">");
                            strHTML.Append("                          <img src=\"" + pub.FormatImgURL(product.Product_Img, "thumbnail") + "\" width=\"180px;\" height=\"180px; padding-left: 0.8px;\" /></a></div>");
                            strHTML.Append("                         <p><a href=\"" + pageurl.FormatURL(pageurl.product_detail, product.Product_ID.ToString()) + "\" target=\"_blank\"   title=" + product.Product_Name + "  style=\"color: #3083e1\">" + tools.CutStr(product.Product_Name, 24) + " </a></p>");
                            //strHTML.Append("                         <strong ><span style=\"color:#ff6600\">单价:" + pub.FormatCurrency(Get_Member_Price(product.Product_ID, product.Product_Price)) + "</span></strong>");
                            strHTML.Append("                     </li>");
                        }
                    }
                }
                strHTML.Append("                 </ul>");
                strHTML.Append("                 <div class=\"clear\"></div>");
                strHTML.Append("             </div>");
            }
        }
        strHTML.Append("         </div>");
        return strHTML.ToString();
    }
    public string GetEnglishHomeProductShow()
    {
        StringBuilder strHTML = new StringBuilder();


        QueryInfo Query1 = new QueryInfo();
        Query1.PageSize = 0;

        Query1.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ParentID", "=", "65"));
        Query1.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_Site", "=", "CN"));
        Query1.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_Sort", "asc"));
        Query1.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_ID", "Asc"));

        IList<ArticleCateInfo> cateinfos = MyArticleCate.GetArticleCates(Query1, pub.CreateUserPrivilege("1a3208d0-70a4-49dd-8010-400f1254535a"));
        string catearrays = "0";
        if (cateinfos != null)
        {

            foreach (var cateinfo in cateinfos)
            {
                catearrays += "," + cateinfo.Article_Cate_ID;
            }
        }

        strHTML.Append("   <div class=\"En_info2_nav\">");
        strHTML.Append("        <span class=\"En_info2_nav_left\" style=\"font-size:30px;\">Product</span>");
        strHTML.Append("         <span class=\"En_info2_nav_left\">");

        if (cateinfos == null)
        {

            strHTML.Append("     </span> ");
            strHTML.Append(" <span class=\"En_info2_nav_right\"><a href=\"/International/Product.aspx\">more ></a></span>");
            strHTML.Append("     </div>");
        }

        else
        {
            int i = 0;
            List<String> list = new List<string>();
            foreach (var cateinfo in cateinfos)
            {
                i++;
                if (i < 7)
                {


                    //strHTML.Append("     <a href=\"/International/Product.aspx?second_cate=" + cateinfo.Article_Cate_ID + "\" id=\"a0" + i + "\">" + cateinfo.Article_Cate_Name + "</a>");
                    strHTML.Append("     <a href=\"/International/Product.aspx\" id=\"a0" + i + "\">" + cateinfo.Article_Cate_Name + "</a>");
                }
                list.Add(cateinfo.Article_Cate_ID.ToString());

            }

            strHTML.Append("     </span> ");
            strHTML.Append(" <span class=\"En_info2_nav_right\"><a href=\"/International/Product.aspx\">more ></a></span>");
            strHTML.Append("     </div>");


            int ij = 0;
            int jj = 0;
            int Article_Cate_ID = 0;
            foreach (var cateinfo in cateinfos)
            {
                ij++;
                jj++;
                if (ij == 1)
                {
                    catearrays = list[0].ToString();
                }
                else if (ij == 2)
                {
                    catearrays = list[1].ToString();
                }
                else if (ij == 3)
                {
                    catearrays = list[2].ToString();
                }
                else if (ij == 4)
                {
                    catearrays = list[3].ToString();
                }
                else if (ij == 5)
                {
                    catearrays = list[4].ToString();
                }
                else if (ij == 6)
                {
                    catearrays = list[5].ToString();
                }
                Article_Cate_ID = cateinfo.Article_Cate_ID;

                if (jj < 7)
                {
                    var css = "";
                    if (ij >= 2)
                    {
                        css = "style='display:none;'";
                    }
                    else
                    {
                        css = "";
                    }
                    strHTML.Append("     <div class=\"En_info2_list\"   " + css + "   id=\"aa0" + ij + "\">");
                    strHTML.Append("  	<ul>");


                    QueryInfo Query = new QueryInfo();
                    Query.PageSize = 0;
                    //首页product块,之前显示所有二级分类 以及三级分类下面产品  改为只显示 二级分类下的产品  
                    //string sql = "select Article_Cate_ID from Article_Cate where Article_Cate_ParentID=" + catearrays + " or Article_Cate_ID=" + catearrays + " ";
                    string sql = "select Article_Cate_ID from Article_Cate where  Article_Cate_ID=" + catearrays + " ";

                    Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", sql));
                    Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsAudit", "=", "1"));
                    Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
                    Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "asc"));
                    Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));

                    IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
                    if (entitys != null)
                    {
                        int i1 = 0;

                        foreach (ArticleInfo entity in entitys)
                        {
                            i1++;

                            //if (i1 % 4 == 1)
                            //{
                            //    strHTML.Append("    <li style=\"margin-left:0; margin: 20px 37px 20px 36px;height:265px;\">");
                            //    strHTML.Append("  	<p><a href=\"/International/detail.aspx?id=" + entity.Article_ID + "\"><img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\" width=\"225\" height=\"165\" /></a></p>");
                            //    strHTML.Append("      <p><a href=\"/International/detail.aspx?id=" + entity.Article_ID + "\" title=\"" + entity.Article_Title + "\" style=\"font-size:14px;\">" + tools.CutStr(entity.Article_Title, 56) + "</a></p>");
                              
                            //    strHTML.Append("      </li>");
                            //}
                            //else if (i1 % 4 == 0)
                            //{
                            //    strHTML.Append("    <li style=\"margin-right:0;margin: 20px 37px 20px 46px;height:265px;\">");

                            //    strHTML.Append("  	<p><a href=\"/International/detail.aspx?id=" + entity.Article_ID + "\"><img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\" width=\"225\" height=\"165\" /></a></p>");
                            //    strHTML.Append("      <p><a href=\"/International/detail.aspx?id=" + entity.Article_ID + "\" title=\"" + entity.Article_Title + "\" style=\"font-size:14px;\">" + tools.CutStr(entity.Article_Title, 56) + "</a></p>");
                              
                            //    strHTML.Append("      </li>");
                            //}
                            //else
                            //{
                            //    strHTML.Append("    <li style=\"margin: 20px 37px 20px 56px;height:265px;\">");
                            //    strHTML.Append("  	<p><a href=\"/International/detail.aspx?id=" + entity.Article_ID + "\"><img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\" width=\"225\" height=\"165\" /></a></p>");
                            //    strHTML.Append("      <p><a href=\"/International/detail.aspx?id=" + entity.Article_ID + "\" title=\"" + entity.Article_Title + "\" style=\"font-size:14px;\">" + tools.CutStr(entity.Article_Title, 56) + "</a></p>");
                            //    //strHTML.Append("       <p><a href=\"/International/detail.aspx?id=" + entity.Article_ID + "\" title=\"" + entity.Article_Intro + "\" style=\"font-size:14px;\">" + tools.CutStr(entity.Article_Intro, 56) + "</a></p>");
                            //    strHTML.Append("      </li>");
                            //}
                            if (i1 % 4 == 1)
                            {
                                strHTML.Append("    <li style=\"margin-left:0; height:245px;width:225px;\">");
                                strHTML.Append("  	<p style=\"margin-top:10px;\"><a href=\"/International/detail.aspx?id=" + entity.Article_ID + "\"><img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\" width=\"225\" height=\"165\" /></a></p>");
                                strHTML.Append("      <p><a href=\"/International/detail.aspx?id=" + entity.Article_ID + "\" title=\"" + entity.Article_Title + "\" style=\"font-size:14px;word-break: break-all; /*支持IE，chrome，FF不支持*/word-wrap:break-word;\">" + tools.CutStr(entity.Article_Title, 56) + "</a></p>");

                                strHTML.Append("      </li>");
                            }
                            else if (i1 % 4 == 0)
                            {
                                strHTML.Append("    <li style=\"margin-right:0;height:245px;width:225px;\">");

                                strHTML.Append("  	<p style=\"margin-top:10px;\"><a href=\"/International/detail.aspx?id=" + entity.Article_ID + "\"><img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\" width=\"225\" height=\"165\" /></a></p>");
                                strHTML.Append("      <p><a href=\"/International/detail.aspx?id=" + entity.Article_ID + "\" title=\"" + entity.Article_Title + "\" style=\"font-size:14px;word-break: break-all; /*支持IE，chrome，FF不支持*/word-wrap:break-word;\">" + tools.CutStr(entity.Article_Title, 56) + "</a></p>");

                                strHTML.Append("      </li>");
                            }
                            else
                            {
                                strHTML.Append("    <li style=\"height:245px;width:225px;\">");
                                strHTML.Append("  	<p style=\"margin-top:10px;\"><a href=\"/International/detail.aspx?id=" + entity.Article_ID + "\"><img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\" width=\"225\" height=\"165\" /></a></p>");
                                strHTML.Append("      <p><a href=\"/International/detail.aspx?id=" + entity.Article_ID + "\" title=\"" + entity.Article_Title + "\" style=\"font-size:14px;word-break: break-all; /*支持IE，chrome，FF不支持*/word-wrap:break-word;\">" + tools.CutStr(entity.Article_Title, 56) + "</a></p>");
                                //strHTML.Append("       <p><a href=\"/International/detail.aspx?id=" + entity.Article_ID + "\" title=\"" + entity.Article_Intro + "\" style=\"font-size:14px;\">" + tools.CutStr(entity.Article_Intro, 56) + "</a></p>");
                                strHTML.Append("      </li>");
                            }




                        }
                    }
                    strHTML.Append("  </ul>");
                    strHTML.Append("     </div>");
                }
            }





        }




        return strHTML.ToString();
    }
    //public string GetEnglishHomeProductShow()
    //{
    //    StringBuilder strHTML = new StringBuilder();


    //    QueryInfo Query1 = new QueryInfo();
    //    Query1.PageSize = 0;

    //    Query1.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ParentID", "=", "65"));
    //    Query1.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleCateInfo.Article_Cate_Site", "=", "CN"));
    //    Query1.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_Sort", "asc"));
    //    Query1.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_ID", "Asc"));

    //    IList<ArticleCateInfo> cateinfos = MyArticleCate.GetArticleCates(Query1, pub.CreateUserPrivilege("1a3208d0-70a4-49dd-8010-400f1254535a"));
    //    string catearrays = "0";
    //    if (cateinfos != null)
    //    {

    //        foreach (var cateinfo in cateinfos)
    //        {
    //            catearrays += "," + cateinfo.Article_Cate_ID;
    //        }
    //    }

    //    strHTML.Append("   <div class=\"En_info2_nav\">");
    //    strHTML.Append("        <span class=\"En_info2_nav_left\" style=\"font-size:30px;\">Product</span>");
    //    strHTML.Append("         <span class=\"En_info2_nav_left\">");

    //    if (cateinfos != null)
    //    {
    //        int i = 0;
    //        //string cateinfoss = "0";
    //        List<String> list = new List<string>();
    //        foreach (var cateinfo in cateinfos)
    //        {
    //            i++;
    //            if (i < 5)
    //            {


    //                strHTML.Append("     <a href=\"/International/Product.aspx\" id=\"a0" + i + "\">" + cateinfo.Article_Cate_Name + "</a>");
    //            }

    //            //cateinfoss += "," + cateinfo.Article_Cate_ID.ToString();

    //            list.Add(cateinfo.Article_Cate_ID.ToString());

    //        }

    //        strHTML.Append("     </span> ");


    //        int ij = 0;
    //        int jj = 0;
    //        int Article_Cate_ID = 0;
    //        foreach (var cateinfo in cateinfos)
    //        {
    //            ij++;
    //            jj++;
    //            if (ij == 1)
    //            {
    //                catearrays = list[0].ToString();
    //            }
    //            else if (ij == 2)
    //            {
    //                catearrays = list[1].ToString();
    //            }
    //            else if (ij == 3)
    //            {
    //                catearrays = list[2].ToString();
    //            }
    //            else if (ij == 4)
    //            {
    //                catearrays = list[3].ToString();
    //            }
    //            Article_Cate_ID = cateinfo.Article_Cate_ID;

    //            if (jj < 5)
    //            {
    //                var css = "";
    //                if (ij >= 2)
    //                {
    //                    css = "style='display:none;'";
    //                }
    //                else
    //                {
    //                    css = "";
    //                }
    //                strHTML.Append("     <div class=\"En_info2_list\"   " + css + "   id=\"aa0" + ij + "\">");
    //                strHTML.Append("  	<ul>");


    //                QueryInfo Query = new QueryInfo();
    //                Query.PageSize = 0;

    //                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", catearrays.ToString()));
    //                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsAudit", "=", "1"));
    //                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
    //                Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "asc"));
    //                Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));

    //                IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
    //                if (entitys != null)
    //                {
    //                    int i1 = 0;

    //                    foreach (ArticleInfo entity in entitys)
    //                    {
    //                        i1++;

    //                        if (i1 % 4 == 1)
    //                        {
    //                            strHTML.Append("    <li style=\"margin-left:0; margin: 20px 37px 20px 56px;\">");
    //                            strHTML.Append("  	<p><img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\" width=\"225\" height=\"165\" /></p>");
    //                            strHTML.Append("      <p>" + entity.Article_Title + "</p>");
    //                            strHTML.Append("       <p>" + entity.Article_Intro + "</p>");
    //                            strHTML.Append("      </li>");
    //                        }
    //                        else if (i1 % 4 == 0)
    //                        {
    //                            strHTML.Append("    <li style=\"margin-right:0;margin: 20px 37px 20px 56px;\">");

    //                            strHTML.Append("  	<p><img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\" width=\"225\" height=\"165\" /></p>");
    //                            strHTML.Append("      <p>" + entity.Article_Title + "</p>");
    //                            strHTML.Append("       <p>" + entity.Article_Intro + "</p>");
    //                            strHTML.Append("      </li>");
    //                        }
    //                        else
    //                        {
    //                            strHTML.Append("    <li style=\"margin: 20px 37px 20px 56px;\">");
    //                            strHTML.Append("  	<p><img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\" width=\"225\" height=\"165\" /></p>");
    //                            strHTML.Append("      <p>" + entity.Article_Title + "</p>");
    //                            strHTML.Append("       <p>" + entity.Article_Intro + "</p>");
    //                            strHTML.Append("      </li>");
    //                        }




    //                    }
    //                }
    //                strHTML.Append("  </ul>");
    //                strHTML.Append("     </div>");
    //            }
    //        }





    //    }


    //    strHTML.Append(" <span class=\"En_info2_nav_right\"><a href=\"/International/Product.aspx\">more ></a></span>");
    //    strHTML.Append("     </div>");

    //    return strHTML.ToString();
    //}

    public string GetEnglishHoneServerDistance()
    {
        StringBuilder strHTML = new StringBuilder();

        strHTML.Append("   <div class=\"En_info3_list\">");
        strHTML.Append("  	<ul>");


        QueryInfo Query = new QueryInfo();
        Query.PageSize = 4;
        //string sql="(select Article_Cate_ID from Article_Cate where Article_Cate_Name='首页二楼右侧销量信息')";
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "=", "47"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "asc"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));

        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {
            foreach (ArticleInfo entity in entitys)
            {


                strHTML.Append("    <li>");
                strHTML.Append("  	<p><img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\" width=\"250\" height=\"250\"  style=\"padding-left: 150px;\"/></p>");
                strHTML.Append("    <p><strong>" + entity.Article_Title + "</strong></p>");
                strHTML.Append("      <p style=\"text-align:justify;\">" + entity.Article_Content + "</p>");
                strHTML.Append("      </li>");
            }
        }
        strHTML.Append("  </ul>");
        strHTML.Append("     </div>");
        return strHTML.ToString();
    }



    //英文市场动态 公告
    public string EnglishMarketDynamicsNotice(int Show_Num, int Cate_ID)
    {
        StringBuilder strHTML = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = Show_Num;
        Query.CurrentPage = 1;
        if (Cate_ID > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "NoticeInfo.Notice_Cate", "=", Cate_ID.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "NoticeInfo.Notice_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "NoticeInfo.Notice_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("NoticeInfo.Notice_Addtime", "Desc"));
        IList<NoticeInfo> Notices = Webnotice.GetNoticeList(Query, pub.CreateUserPrivilege("9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7"));
        strHTML.Append("   <ul>");



        if (Notices != null)
        {
            foreach (NoticeInfo entity in Notices)
            {


                strHTML.Append(" <li><a href=\"/notice/detail.aspx?notice_id=" + entity.Notice_ID + "\" target=\"_blank\" title=\"" + entity.Notice_Title + "\">" + tools.CutStr(entity.Notice_Title, 28) + "</a></li>");
            }
        }
        strHTML.Append("         </ul>");
        return strHTML.ToString();
    }



    //英文市场动态 公告
    public string EnglishMarketDynamicsNoticeBelowArticle(int Show_Num, int Cate_ID)
    {
        StringBuilder strHTML = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = Show_Num;
        Query.CurrentPage = 1;
        if (Cate_ID > 0)
        {
            Query.ParamInfos.Add(new ParamInfo("AND", "int", "NoticeInfo.Notice_Cate", "=", Cate_ID.ToString()));
        }
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "NoticeInfo.Notice_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "NoticeInfo.Notice_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("NoticeInfo.Notice_Addtime", "Desc"));
        IList<NoticeInfo> Notices = Webnotice.GetNoticeList(Query, pub.CreateUserPrivilege("9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7"));
        strHTML.Append("   <ul>");






        if (Notices != null)
        {
            foreach (NoticeInfo entity in Notices)
            {


                strHTML.Append("   <li><a href=\"javascript:void(0);\">" + entity.Notice_Title + "</a></li>");
            }
        }
        strHTML.Append("         </ul>");
        return strHTML.ToString();
    }



    //英文市场动态 公告
    public string GetFinancePageMarketDy()
    {
        StringBuilder strHTML = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 4;
        //string sql="(select Article_Cate_ID from Article_Cate where Article_Cate_Name='首页二楼右侧销量信息')";
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "=", "53"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "asc"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (entitys != null)
        {

            foreach (ArticleInfo entity in entitys)
            {


                strHTML.Append("      <div class=\"scdt_list\">");
                strHTML.Append("     	<div class=\"scdt_list_left\"><a href=\"/International/detail.aspx?id=" + entity.Article_ID + "\">");
                strHTML.Append(" <img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\" width=\"100\" height=\"71\" />");

                strHTML.Append("  </a></div>");
                strHTML.Append("         <div class=\"scdt_list_left2\"><a href=\"/International/detail.aspx?id=" + entity.Article_ID + "\">" + entity.Article_Title + " </a></div>");
                strHTML.Append("         <div class=\"scdt_list_left3\"><a href=\"/International/detail.aspx?id=" + entity.Article_ID + "\">");
                strHTML.Append("         	<p>" + entity.Article_Intro + "</p>");
                strHTML.Append("         	<p>" + entity.Article_Content + "</p>");
                strHTML.Append("         </a></div>");
                strHTML.Append("     </div>");
            }
        }

        //strHTML.Append("         </ul>");
        return strHTML.ToString();
    }






    //英文市场动态 公告
    public string GetEnlishDetailsLeft(int Article_Cate_ParentID, string cate_id, string second_cate)
    {
        StringBuilder strHTML = new StringBuilder();
        QueryInfo Query = new QueryInfo();
        int cateid = 0;
        if (cate_id.Length > 0)
        {
            cateid = Convert.ToInt32(cate_id);
        }

        Query.PageSize = 0;

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ParentID", "=", Article_Cate_ParentID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_Sort", "asc"));
        Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_ID", "asc"));
        Query.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_Site", "CN"));

        IList<ArticleCateInfo> entitys = MyArticleCate.GetArticleCates(Query, pub.CreateUserPrivilege("1a3208d0-70a4-49dd-8010-400f1254535a"));


        if (entitys != null)
        {
            int i = 0;
            foreach (ArticleCateInfo entity in entitys)
            {
                i++;

                strHTML.Append("    <div class=\"b07_info\">");
                //if (entity.Article_Cate_ID.ToString() == second_cate)
                //{
                //    strHTML.Append("                       <h3><span onclick=\"openShutManager(this,'box" + i + "')\" ><a id=\"" + i + "\"  onClick=\"switchTag(" + i + ");\" style=\"font-size:12px;color:red\" title=\"" + entity.Article_Cate_Name + "\">" + tools.CutStr(entity.Article_Cate_Name, 20) + "</a></span></h3>");
                //}
                //else
                //{
                    strHTML.Append("                       <h3><span onclick=\"openShutManager(this,'box" + i + "')\" ><a id=\"" + i + "\"  onClick=\"switchTag(" + i + ");\" style=\"font-size:12px;\" title=\"" + entity.Article_Cate_Name + "\">" + tools.CutStr(entity.Article_Cate_Name, 20) + "</a></span></h3>");
                //}
               



                strHTML.Append("                       <div id=\"box" + i + "\" class=\"b07_info_main\" style=\"display: block;\">");
                strHTML.Append("<ul>");

                QueryInfo Query1 = new QueryInfo();
                Query1.PageSize = 0;


                string sql = "select Article_Cate_ID from Article_Cate where Article_Cate_ParentID=" + entity.Article_Cate_ID + " ";
                Query1.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleCateInfo.Article_Cate_ID", "in", sql));
                Query1.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_Sort", "asc"));
                Query1.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_ID", "asc"));
                Query1.OrderInfos.Add(new OrderInfo("ArticleCateInfo.Article_Cate_Site", "CN"));

                IList<ArticleCateInfo> entitys1 = MyArticleCate.GetArticleCates(Query1, pub.CreateUserPrivilege("1a3208d0-70a4-49dd-8010-400f1254535a"));
                if (entitys1 != null)
                {
                    foreach (ArticleCateInfo articlecateinfo in entitys1)
                    {
                       
                        if (articlecateinfo.Article_Cate_ID == cateid)
                        {
                            strHTML.Append("    	<li class=\"on\"><a href=\"/International/Product.aspx?cate_id=" + articlecateinfo.Article_Cate_ID + "\"  title=\"" + articlecateinfo.Article_Cate_Name + "\">" + tools.CutStr(articlecateinfo.Article_Cate_Name, 20) + "</a></li>");
                        }
                        else
                        {
                            strHTML.Append("    	<li><a href=\"/International/Product.aspx?cate_id=" + articlecateinfo.Article_Cate_ID + "\"  title=\"" + articlecateinfo.Article_Cate_Name + "\">" + tools.CutStr(articlecateinfo.Article_Cate_Name, 20) + "</a></li>");

                        }

                    }
                }
                strHTML.Append("</ul>");
                strHTML.Append("</div>");
                strHTML.Append("</div>");


            }
        }


        return strHTML.ToString();
    }


    //英文 Product 列表页
    public void GetEnlishDetailsRight(int Cate_ID, string cateid, string secondcate)
    {

      

        QueryInfo Query = new QueryInfo();
        Query.PageSize = 20;
        int page = tools.CheckInt(Request["page"]);
        if (page < 1)
        {
            page = 1;
        }
        Query.PageSize = 12;
        Query.CurrentPage = page;


        int cate_id = 0;
        int second_cate = 0;
        if (cateid.Length > 0)
        {
            cate_id = Convert.ToInt32(cateid);
        }

        if (secondcate.Length > 0)
        {
            second_cate = Convert.ToInt32(secondcate);

        }

        //string sql = "select Article_Cate_ID from Article_Cate where Article_Cate_ParentID=" + Cate_ID + " or Article_Cate_ID=" + Cate_ID + " ";
       string sql="";



       if (cate_id > 0)
       {
           Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "=", cate_id.ToString()));
       }

        if (second_cate > 0)
        {
            //sql = "select Article_Cate_ID from Article_Cate where Article_Cate_ParentID in (select Article_Cate_ID from Article_Cate where Article_Cate_ParentID=" + Cate_ID + " or Article_Cate_ID=" + Cate_ID + " ) and (Article_Cate_ParentID=" + second_cate + "  or Article_Cate_ID=" + second_cate + " )";


            sql = "select Article_Cate_ID from Article_Cate where Article_Cate_ParentID in (select Article_Cate_ID from Article_Cate where Article_Cate_ParentID=" + Cate_ID + " ) and ( Article_Cate_ID=" + second_cate + " )";
        }
        else
        {
          
            //sql = "select Article_Cate_ID from Article_Cate where Article_Cate_ParentID in (select Article_Cate_ID from Article_Cate where Article_Cate_ParentID=" + Cate_ID + " or Article_Cate_ID=" + Cate_ID + "  )";
            sql = "select Article_Cate_ID from Article_Cate where Article_Cate_ParentID in (select Article_Cate_ID from Article_Cate where Article_Cate_ParentID=" + Cate_ID + "  )";
        }

         


        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "in", sql));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "asc"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));
        IList<ArticleInfo> entitys1 = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));


        Response.Write("    <div class=\"En_info2_list2\" >");
        Response.Write("         <ul>");

        if (entitys1 != null)
        {

            foreach (ArticleInfo entity in entitys1)
            {
                Response.Write("   <li>");
                Response.Write("                   <p> <a href=\"/International/detail.aspx?id=" + entity.Article_ID + "\"><img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\"/></a></p>");
                Response.Write("                   <p title=\"" + entity.Article_Title + "\"> <a href=\"/International/detail.aspx?id=" + entity.Article_ID + "\"  style=\"font-size:14px;word-break: break-all; /*支持IE，chrome，FF不支持*/word-wrap:break-word;\">" + tools.CutStr(entity.Article_Title, 56) + "</a></p>");
                //Response.Write("                   <p title=\"" + entity.Article_Intro + "\"> <a href=\"/International/detail.aspx?id=" + entity.Article_ID + "\" style=\"font-size:14px;\">" + tools.CutStr(entity.Article_Intro, 56) + "</a></p>");
                Response.Write("               </li>");

            }
        }




        Response.Write("         </ul>");
        Response.Write("    </div>");

        PageInfo pageinfo = MyArticle.GetPageInfo(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        if (pageinfo != null && pageinfo.RecordCount >= 1)
        {
            string url = "/International/product.aspx?product_id=0";

            pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, url, pageinfo.PageSize, pageinfo.RecordCount);
        }




    }



    //英文市场动态 公告
    public string GetEnlishDetails_Search(int Cate_ID, string keyword)
    {
        StringBuilder strHTML = new StringBuilder();

        string page_url = "?";
        string keywords = "";
        keywords = tools.CheckStr(Request["keyword"]);
        if (keyword == "")
        {
            keyword = keywords;
        }
        if (keyword != "")
        {
            page_url = page_url + "&keyword=" + keyword;
        }
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;


      
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "ArticleInfo.Article_CateID", "=", Cate_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "ArticleInfo.Article_Site", "=", "CN"));
        if (keyword != null)
        {
            foreach (string keywordsub in keyword.Split(' '))
            {
                if (keywordsub != "")
                {
                    Query.ParamInfos.Add(new ParamInfo("AND(", "str", "ArticleInfo.Article_Author", "%like%", keywordsub));
                    Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ArticleInfo.Article_Title", "%like%", keywordsub));

                    //Query.ParamInfos.Add(new ParamInfo("OR)", "str", "ArticleInfo.Article_Source", "%like%", keywordsub));

                    //添加竞价商品
                    //GetBiddingProduct(keywordsub, ref BiddingProductList);

                }
            }
        }
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_Sort", "asc"));
        Query.OrderInfos.Add(new OrderInfo("ArticleInfo.Article_ID", "Desc"));
        IList<ArticleInfo> entitys = MyArticle.GetArticles(Query, pub.CreateUserPrivilege("8b1dc4af-f4c3-43b9-b62a-ce99ee4a3276"));
        strHTML.Append("         <ul>");

        if (entitys != null)
        {

            foreach (ArticleInfo entity in entitys)
            {


                strHTML.Append("   <li>");
                strHTML.Append("                   <p><img src=\"" + pub.FormatImgURL(entity.Article_Img, "fullpath") + "\"/></p>");
                strHTML.Append("                   <p>" + entity.Article_Title + "</p>");
                strHTML.Append("                   <p>" + entity.Article_Intro + "</p>");
                strHTML.Append("               </li>");

            }
        }

        strHTML.Append("         </ul>");
        return strHTML.ToString();
    }


   

    //我的商品评论


    //获取我的收藏商品数量
    public int MyFavProductsNum(int type_id)
    {
        int MyProNum = 0;
        int Member_ID = tools.CheckInt(Session["member_id"].ToString());
        SQLHelper DBHelper = new SQLHelper();

        if (Member_ID>0)
        {
//             select * from Product_Basic where Product_ID in (
// SELECT Member_Favorites_TargetID FROM Member_Favorites  
// WHERE Member_Favorites.Member_Favorites_MemberID = 14 
// AND Member_Favorites.Member_Favorites_Type = 0  
//) and Product_IsInsale=1
            //MyProNum = tools.CheckInt(DBHelper.ExecuteScalar("SELECT COUNT(*) FROM Member_Favorites WHERE Member_Favorites_MemberID = " + Member_ID + " and Member_Favorites_Type=" + type_id + "").ToString());
            MyProNum = tools.CheckInt(DBHelper.ExecuteScalar("             select count(*) from Product_Basic where Product_ID in ( SELECT Member_Favorites_TargetID FROM Member_Favorites   WHERE Member_Favorites.Member_Favorites_MemberID =" + Member_ID + "   AND Member_Favorites.Member_Favorites_Type =" + type_id + " ) and Product_IsInsale=1 and Product_IsAudit=1").ToString());
        }
        else
        {
            MyProNum = 0;
        }
        
        return MyProNum;
    }

}
