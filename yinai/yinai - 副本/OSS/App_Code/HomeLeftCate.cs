using System;
using System.Data;
using System.Data.SqlClient;
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
using System.Text;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.Product;

/// <summary>
/// 首页左侧类别
/// </summary>
public class HomeLeftCate
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IHomeLeftCate MyBLL;
    private ICategory MyCate;

    public HomeLeftCate()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = HomeLeftCateFactory.CreateHomeLeftCate();
        MyCate = CategoryFactory.CreateCategory();
    }

    public virtual void AddHomeLeftCate()
    {
        int Home_Left_Cate_ID = tools.CheckInt(Request.Form["Home_Left_Cate_ID"]);
        int Home_Left_Cate_ParentID = tools.CheckInt(Request.Form["Home_Left_Cate_ParentID"]);
        int Home_Left_Cate_CateID = tools.CheckInt(Request.Form["Home_Left_Cate_CateID"]);
        string Home_Left_Cate_Name = tools.CheckStr(Request.Form["Home_Left_Cate_Name"]);
        string Home_Left_Cate_URL = tools.CheckStr(Request.Form["Home_Left_Cate_URL"]);
        string Home_Left_Cate_Img = tools.CheckStr(Request.Form["Home_Left_Cate_Img"]);
        int Home_Left_Cate_Sort = tools.CheckInt(Request.Form["Home_Left_Cate_Sort"]);
        int Home_Left_Cate_Active = tools.CheckInt(Request.Form["Home_Left_Cate_Active"]);
        string Home_Left_Cate_Site = Public.GetCurrentSite();
        if (Home_Left_Cate_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写类别名称", false, "{back}");
        }
        HomeLeftCateInfo entity = new HomeLeftCateInfo();
        entity.Home_Left_Cate_ID = Home_Left_Cate_ID;
        entity.Home_Left_Cate_ParentID = Home_Left_Cate_ParentID;
        entity.Home_Left_Cate_CateID = Home_Left_Cate_CateID;
        entity.Home_Left_Cate_Name = Home_Left_Cate_Name;
        entity.Home_Left_Cate_URL = Home_Left_Cate_URL;
        entity.Home_Left_Cate_Img = Home_Left_Cate_Img;
        entity.Home_Left_Cate_Sort = Home_Left_Cate_Sort;
        entity.Home_Left_Cate_Active = Home_Left_Cate_Active;
        entity.Home_Left_Cate_Site = Home_Left_Cate_Site;

        if (MyBLL.AddHomeLeftCate(entity,Public.GetUserPrivilege()))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "homeleftcate_add.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void EditHomeLeftCate()
    {

        int Home_Left_Cate_ID = tools.CheckInt(Request.Form["Home_Left_Cate_ID"]);
        int Home_Left_Cate_ParentID = tools.CheckInt(Request.Form["Home_Left_Cate_ParentID"]);
        int Home_Left_Cate_CateID = tools.CheckInt(Request.Form["Home_Left_Cate_CateID"]);
        string Home_Left_Cate_Name = tools.CheckStr(Request.Form["Home_Left_Cate_Name"]);
        string Home_Left_Cate_URL = tools.CheckStr(Request.Form["Home_Left_Cate_URL"]);
        string Home_Left_Cate_Img = tools.CheckStr(Request.Form["Home_Left_Cate_Img"]);
        int Home_Left_Cate_Sort = tools.CheckInt(Request.Form["Home_Left_Cate_Sort"]);
        int Home_Left_Cate_Active = tools.CheckInt(Request.Form["Home_Left_Cate_Active"]);
        if (Home_Left_Cate_Name.Length == 0)
        {
            Public.Msg("error", "错误信息", "请填写类别名称", false, "{back}");
        }
        HomeLeftCateInfo entity = GetHomeLeftCateByID(Home_Left_Cate_ID);
        if (entity != null)
        {
            entity.Home_Left_Cate_ParentID = Home_Left_Cate_ParentID;
            entity.Home_Left_Cate_CateID = Home_Left_Cate_CateID;
            entity.Home_Left_Cate_Name = Home_Left_Cate_Name;
            entity.Home_Left_Cate_URL = Home_Left_Cate_URL;
            entity.Home_Left_Cate_Img = Home_Left_Cate_Img;
            entity.Home_Left_Cate_Sort = Home_Left_Cate_Sort;
            entity.Home_Left_Cate_Active = Home_Left_Cate_Active;


            if (MyBLL.EditHomeLeftCate(entity, Public.GetUserPrivilege()))
            {
                Public.Msg("positive", "操作成功", "操作成功", true, "homeleftcate_list.aspx");
            }
            else
            {
                Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual void DelHomeLeftCate()
    {
        int Home_Left_Cate_ID = tools.CheckInt(Request.QueryString["Home_Left_Cate_ID"]);
        if (MyBLL.DelHomeLeftCate(Home_Left_Cate_ID, Public.GetUserPrivilege()) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "homeleftcate_list.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    public virtual HomeLeftCateInfo GetHomeLeftCateByID(int cate_id)
    {
        return MyBLL.GetHomeLeftCateByID(cate_id, Public.GetUserPrivilege());
    }

    public string GetHomeLeftCates()
    {
        int Home_Left_Cate_ParentID = tools.CheckInt(Request.QueryString["Parent_ID"]);
        QueryInfo Query = new QueryInfo();
        Query.PageSize = tools.CheckInt(Request["rows"]);
        Query.CurrentPage = tools.CheckInt(Request["page"]);
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "HomeLeftCateInfo.Home_Left_Cate_Site", "=", Public.GetCurrentSite()));
        Query.OrderInfos.Add(new OrderInfo(tools.CheckStr(Request["sidx"]), tools.CheckStr(Request["sord"])));

        Query.ParamInfos.Add(new ParamInfo("AND", "int", "HomeLeftCateInfo.Home_Left_Cate_ParentID", "=", Home_Left_Cate_ParentID.ToString()));
        PageInfo pageinfo = MyBLL.GetPageInfo(Query, Public.GetUserPrivilege());

        IList<HomeLeftCateInfo> entitys = MyBLL.GetHomeLeftCates(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
                StringBuilder jsonBuilder = new StringBuilder();
                jsonBuilder.Append("{\"page\":" + pageinfo.CurrentPage + ",\"total\":" + pageinfo.PageCount + ",\"records\":" + pageinfo.RecordCount + ",\"rows\"");
                jsonBuilder.Append(":[");
                foreach (HomeLeftCateInfo entity in entitys)
                {

                    jsonBuilder.Append("{\"id\":\"" + entity.Home_Left_Cate_ID + "\",\"cell\":[");
                    //各字段
                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(entity.Home_Left_Cate_ID);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append("<a href=\\\"homeleftcate_list.aspx?Parent_ID="+entity.Home_Left_Cate_ID+"\\\">" + entity.Home_Left_Cate_Name + "<a>");
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");
                    jsonBuilder.Append(entity.Home_Left_Cate_Sort);
                    jsonBuilder.Append("\",");

                    jsonBuilder.Append("\"");

                    if (Public.CheckPrivilege("de88931b-4a5b-4bb7-8f68-4975ad26e59c"))
                    {
                        jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\" alt=\\\"修改\\\"> <a href=\\\"homeleftcate_edit.aspx?Home_Left_Cate_ID=" + entity.Home_Left_Cate_ID + "\\\" title=\\\"修改\\\">修改</a>");
                    }

                    if (Public.CheckPrivilege("4196e669-c4c0-4209-bbed-bc99a951c2c8"))
                    {
                        jsonBuilder.Append(" <img src=\\\"/images/icon_del.gif\\\"  alt=\\\"删除\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('homeleftcate_do.aspx?action=move&Home_Left_Cate_ID=" + entity.Home_Left_Cate_ID + "')\\\" title=\\\"删除\\\">删除</a>");
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
        else
        {
            return null;
        }
    }

    public string CateOption(int Parent_ID, int selectValue, string appendTag, int shieldID)
    {
        string strHTML = "";
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "HomeLeftCateInfo.Home_Left_Cate_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "HomeLeftCateInfo.Home_Left_Cate_ParentID", "=", Parent_ID.ToString()));
        Query.OrderInfos.Add(new OrderInfo("HomeLeftCateInfo.Home_Left_Cate_ID", "Desc"));
        IList<HomeLeftCateInfo> entitys = MyBLL.GetHomeLeftCates(Query, Public.GetUserPrivilege());
        if (entitys != null)
        {
            foreach (HomeLeftCateInfo entity in entitys)
            {
                if (entity.Home_Left_Cate_ID == shieldID)
                    continue;
                if (entity.Home_Left_Cate_ID == selectValue)
                {
                    strHTML += "<option value=\"" + entity.Home_Left_Cate_ID + "\" selected=\"selected\">" + appendTag + entity.Home_Left_Cate_Name + "</option>";
                }
                else
                {
                    strHTML += "<option value=\"" + entity.Home_Left_Cate_ID + "\">" + appendTag + entity.Home_Left_Cate_Name + "</option>";
                }
                strHTML += CateOption(entity.Home_Left_Cate_ID, selectValue, appendTag, shieldID);
            }
        }

        
        return strHTML;
    }

    public string CateRecursion(int Cate_ID, string HrefURL)
    {
        string Cate_Name = ""; int ParentID = 0;
        HomeLeftCateInfo entity = GetHomeLeftCateByID(Cate_ID);
        if (entity != null)
        {
            ParentID = entity.Home_Left_Cate_ParentID;
            Cate_Name = entity.Home_Left_Cate_Name;
        }
        else
        {
            Cate_ID = 0;
        }
        

        string CateNameStr = "";
        if (ParentID > 0) { CateNameStr = CateRecursion(ParentID, HrefURL); }
        if (CateNameStr.Length > 0) { CateNameStr += " &gt; "; }
        if (HrefURL.Length > 0) { CateNameStr += "<a href=\"" + HrefURL.Replace("{cate_id}", Cate_ID.ToString()) + "\">" + Cate_Name + "</a>"; }
        else { CateNameStr += Cate_Name; }

        return CateNameStr;
    }

    public virtual void HomeLeftCate_Input()
    {
        MyBLL.DelHomeLeftCateAll(Public.GetUserPrivilege());
        HomeLeftCateInfo entity=null;
        HomeLeftCateInfo LastCate = null;
        HomeLeftCateInfo LastCate1 = null;
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", Public.GetCurrentSite()));
        Query.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ParentID", "=", "0"));
        Query.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_ID", "ASC"));
        IList<CategoryInfo> Categorys = MyCate.GetCategorys(Query, Public.GetUserPrivilege());
        if (Categorys != null)
        {
            foreach (CategoryInfo Category in Categorys)
            { 
                entity = new HomeLeftCateInfo();
                entity.Home_Left_Cate_ParentID = Category.Cate_ParentID;
                entity.Home_Left_Cate_CateID = Category.Cate_ID;
                entity.Home_Left_Cate_Name = Category.Cate_Name;
                entity.Home_Left_Cate_URL = "/product/category.aspx?cate_id=" + Category.Cate_ID;
                entity.Home_Left_Cate_Img = Category.Cate_Img;
                entity.Home_Left_Cate_Sort = Category.Cate_Sort;
                entity.Home_Left_Cate_Active = 1;
                entity.Home_Left_Cate_Site = Public.GetCurrentSite();
                MyBLL.AddHomeLeftCate(entity,Public.GetUserPrivilege());
                entity = null;
                LastCate = MyBLL.GetHomeLeftCateByLastID(Public.GetUserPrivilege());
                if (LastCate != null)
                {
                    if (LastCate.Home_Left_Cate_Name == Category.Cate_Name)
                    {
                        QueryInfo Query1 = new QueryInfo();
                        Query1.PageSize = 0;
                        Query1.CurrentPage = 1;
                        Query1.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", Public.GetCurrentSite()));
                        Query1.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ParentID", "=", Category.Cate_ID.ToString()));
                        Query1.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_ID", "ASC"));
                        IList<CategoryInfo> Categorys1 = MyCate.GetCategorys(Query1, Public.GetUserPrivilege());
                        if (Categorys1 != null)
                        {
                            foreach (CategoryInfo Category1 in Categorys1)
                            {
                                entity = new HomeLeftCateInfo();
                                entity.Home_Left_Cate_ParentID = LastCate.Home_Left_Cate_ID;
                                entity.Home_Left_Cate_CateID = Category1.Cate_ID;
                                entity.Home_Left_Cate_Name = Category1.Cate_Name;
                                entity.Home_Left_Cate_URL = "/product/category.aspx?cate_id=" + Category1.Cate_ID;
                                entity.Home_Left_Cate_Img = Category1.Cate_Img;
                                entity.Home_Left_Cate_Sort = Category1.Cate_Sort;
                                entity.Home_Left_Cate_Active = 1;
                                entity.Home_Left_Cate_Site = Public.GetCurrentSite();
                                MyBLL.AddHomeLeftCate(entity, Public.GetUserPrivilege());
                                entity = null;
                                LastCate1 = MyBLL.GetHomeLeftCateByLastID(Public.GetUserPrivilege());
                                if (LastCate1 != null)
                                {
                                    if (LastCate1.Home_Left_Cate_Name == Category1.Cate_Name)
                                    {
                                        QueryInfo Query2 = new QueryInfo();
                                        Query2.PageSize = 0;
                                        Query2.CurrentPage = 1;
                                        Query2.ParamInfos.Add(new ParamInfo("AND", "str", "CategoryInfo.Cate_Site", "=", Public.GetCurrentSite()));
                                        Query2.ParamInfos.Add(new ParamInfo("AND", "int", "CategoryInfo.Cate_ParentID", "=", Category1.Cate_ID.ToString()));
                                        Query2.OrderInfos.Add(new OrderInfo("CategoryInfo.Cate_ID", "ASC"));
                                        IList<CategoryInfo> Categorys2 = MyCate.GetCategorys(Query2, Public.GetUserPrivilege());
                                        if (Categorys2 != null)
                                        {
                                            foreach (CategoryInfo Category2 in Categorys2)
                                            {
                                                entity = new HomeLeftCateInfo();
                                                entity.Home_Left_Cate_ParentID = LastCate1.Home_Left_Cate_ID;
                                                entity.Home_Left_Cate_CateID = Category.Cate_ID;
                                                entity.Home_Left_Cate_Name = Category2.Cate_Name;
                                                entity.Home_Left_Cate_URL = "/product/category.aspx?cate_id=" + Category2.Cate_ID;
                                                entity.Home_Left_Cate_Img = Category2.Cate_Img;
                                                entity.Home_Left_Cate_Sort = Category2.Cate_Sort;
                                                entity.Home_Left_Cate_Active = 1;
                                                entity.Home_Left_Cate_Site = Public.GetCurrentSite();
                                                MyBLL.AddHomeLeftCate(entity, Public.GetUserPrivilege());
                                                entity = null;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        Public.Msg("positive", "操作成功", "操作成功", true, "homeleftcate_list.aspx");
    }

}
