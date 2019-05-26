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
        // MyApplication = U_LinksApplicationFactory.CreateU_LinksApplication();
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
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "NoticeInfo.Notice_Cate", "=", Cate_ID.ToString()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "NoticeInfo.Notice_IsAudit", "=", "1"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "NoticeInfo.Notice_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("NoticeInfo.Notice_ID", "Desc"));
        IList<NoticeInfo> Notices = Webnotice.GetNoticeList(Query, pub.CreateUserPrivilege("9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7"));
        if (Notices != null)
        {
            foreach (NoticeInfo entity in Notices)
            {
                notice_list = notice_list + " <li><a href=\"/notice/detail.aspx?notice_id=" + entity.Notice_ID + "&cate_id=" + entity.Notice_Cate + "\" target=\"_blank\">" + tools.CutStr(entity.Notice_Title, 36) + "</a></li>";
            }
        }

        return notice_list;
    }

    /// <summary>
    /// 帮助中心左侧列表
    /// </summary>
    /// <param name="cateid"></param>
    /// <param name="helpid"></param>
    /// <returns></returns>
    public string Help_Left_Nav(int cateid, int helpid)
    {
        string nav_string = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "HelpCateInfo.Help_Cate_ParentID", "=", "0"));
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "HelpCateInfo.Help_Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("HelpCateInfo.Help_Cate_Sort", "ASC"));
        IList<HelpCateInfo> helpcate = MyHelpCate.GetHelpCates(Query, pub.CreateUserPrivilege("e2e6aec7-ff11-407b-9c3a-6317b06b1a7e"));
        if (helpcate != null)
        {
            int i = 0;
            foreach (HelpCateInfo entity in helpcate)
            {
                i++;
                if (i == 1 && cateid == 0)
                {
                    cateid = entity.Help_Cate_ID;
                }
                nav_string = nav_string + "<dl>";

                if (cateid == entity.Help_Cate_ID)
                    nav_string += "<dt><p class=\"dropList3-hover\"><span>" + entity.Help_Cate_Name + "</span></p></dt><dd style=\"display:block;\">";
                else
                    nav_string += "<dt><p><span>" + entity.Help_Cate_Name + "</span></p></dt><dd>";

                nav_string += Help_Left_Sub_Nav(entity.Help_Cate_ID, helpid, cateid);
                nav_string = nav_string + "</dd></dl>";
            }
        }

        return nav_string;
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
        string nav_string = "";
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
            nav_string += "  <ul class=\"lst20\">";

            foreach (HelpInfo entity in helps)
            {
                if (helpid == entity.Help_ID)
                {
                    nav_string += "<li class=\"on\"><a href=\"/help/index.aspx?help_id=" + entity.Help_ID + "\">" + entity.Help_Title + "</a></li>";
                }
                else
                {
                    nav_string += "<li><a href=\"/help/index.aspx?help_id=" + entity.Help_ID + "\">" + entity.Help_Title + "</a></li>";
                }
            }
            nav_string += "  </ul>";
        }
        return nav_string;
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
        Query.PageSize = 6;
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
                nav_string = nav_string + "	<p><a href=\"" + tools.NullStr(Application["Site_URL"]).TrimEnd('/') + "/help/index.aspx?help_id=" + entity.Help_ID + "\">" + entity.Help_Title + "</a></p>";

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
        StringBuilder strHTML = new StringBuilder("<ul class=\"ullist\">");

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
            foreach (HelpInfo entity in entitys)
                strHTML.Append("<li><a href=\"/help/index.aspx?help_id=" + entity.Help_ID + "\">" + entity.Help_Title + "</a></li>");
        }

        strHTML.Append("</ul>");

        Response.Write(strHTML.ToString());
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

    //网站公告左侧列表 /**/
    public string Notice_Nav(int cateid)
    {
        string nav_string = "";
        nav_string += "<div id=\"help-left\">";
        nav_string += "  <h4>网站公告</h4>";
        nav_string += "  <div id=\"help-zong\">";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "NoticeCateInfo.Notice_Cate_Site", "=", "CN"));
        Query.OrderInfos.Add(new OrderInfo("NoticeCateInfo.Notice_Cate_Sort", "ASC"));
        IList<NoticeCateInfo> entitys = MyNoticeCate.GetNoticeCates(Query, pub.CreateUserPrivilege("fb3e87ba-3d4d-480d-934e-80048bcc0100"));
        nav_string = nav_string + "<div id=\"help-list1\">";

        nav_string += "	 <h3>网站公告</h3>";

        if (entitys != null)
        {
            nav_string += "  <ul>";
            foreach (NoticeCateInfo entity in entitys)
            {
                if (cateid == entity.Notice_Cate_ID)
                {
                    nav_string += "<li class=\"yy\"><a href=\"/notice/index.aspx?cate_id=" + entity.Notice_Cate_ID + "\">" + entity.Notice_Cate_Name + "</a></li>";
                }
                else
                {
                    nav_string += "<li><a href=\"/notice/index.aspx?cate_id=" + entity.Notice_Cate_ID + "\">" + entity.Notice_Cate_Name + "</a></li>";
                }
            }
            nav_string += "  </ul>";
        }
        nav_string = nav_string + "</div>";
        nav_string += "</div>";
        nav_string += "</div>";
        return nav_string;
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
            Response.Write("<div id=\"help-right\">");
            Response.Write("  <h3>" + CateInfo.Notice_Cate_Name + "</h3>");
            Response.Write("  <div id=\"help-rightwen1\">");
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
                foreach (NoticeInfo entity in notices)
                {
                    Response.Write("<li><span style=\"color:#535353;\">[ " + entity.Notice_Addtime.ToString("yy-MM-dd") + " ]</span><a href=\"/notice/detail.aspx?notice_id=" + entity.Notice_ID + "\">" + tools.CutStr(entity.Notice_Title, 35) + "</a></li>");
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
                Response.Write("<div style=\" float:right; padding-right:30px; padding-bottom:5px;\">");
                pub.Page(pageinfo.PageCount, pageinfo.CurrentPage, url, pageinfo.PageSize, pageinfo.RecordCount);
                Response.Write("</div>");
            }

            Response.Write("  </div>");
            Response.Write("</div>");
        }
        else
        {
            Response.Write("<div id=\"help-right\">");
            Response.Write("  <h3>热点公告</h3>");
            Response.Write("  <div id=\"help-rightwen1\">");
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
            Response.Write("  </div>");
            Response.Write("</div>");
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

    public NoticeInfo GetNoticeByID(int ID)
    {
        return Webnotice.GetNoticeByID(ID, pub.CreateUserPrivilege("9d4d1366-35ab-4eb6-b88e-e49e6bfae9d7"));
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

}
