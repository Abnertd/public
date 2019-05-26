using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
using Glaer.Trade.B2C.BLL.AD;

using System.Reflection;


/// <summary>
///AD 的摘要说明
/// </summary>
public class AD
{
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private IAD WebAD;
    private IADPosition WebADPosition;
    private ITools tools;
    private Public_Class pub = new Public_Class();

    public AD()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;
        WebAD = ADFactory.CreateAD();
        WebADPosition = ADPositionFactory.CreateADPosition();
        tools = ToolsFactory.CreateTools();
    }

    public void AD_Hits()
    {
        int hits_ad;
        int adv_id = tools.CheckInt(Request["adv_ID"]);
        if (adv_id > 0)
        {
            ADInfo entity = WebAD.GetADByID(adv_id, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
            if (entity != null)
            {
                if (entity.Ad_IsActive == 1 && (DateTime.Today - entity.Ad_StartDate).TotalDays >= 0 && (DateTime.Today - entity.Ad_EndDate).TotalDays <= 0)
                {
                    hits_ad = WebAD.Adv_Show_Hits_Add(adv_id, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
                    Response.Redirect(entity.Ad_Link);
                }
                else
                {
                    Response.Redirect("/index.aspx");
                }
            }
            else
            {
                Response.Redirect("/index.aspx");
            }
        }
        else
        {
            Response.Redirect("/index.aspx");
        }
    }

    public string AD_Show(string AD_Position, string Propertys, string DisplayStyle, int Col_Num)
    {
        string sys_Install_Path = "/XC/xchits.aspx";
        string Ad_String = "";
        ADPositionInfo Positioninfo = WebADPosition.GetAD_PositionByValue(AD_Position, pub.CreateUserPrivilege("d3aa1596-cc86-46c7-80f0-8bf6248ee31e"));
        if (Positioninfo != null)
        {
            if (Positioninfo.Ad_Position_IsActive == 0)
            {
                return Ad_String;
            }
            else
            {
                QueryInfo Query = new QueryInfo();
                Query.PageSize = 0;
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADInfo.Ad_Kind", "=", AD_Position));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ADInfo.U_Ad_Audit", "=", "1"));
                Query.ParamInfos.Add(new ParamInfo("AND", "int", "ADInfo.Ad_IsActive", "=", "1"));
                Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{ADInfo.Ad_StartDate}, GETDATE())", ">=", "0"));
                Query.ParamInfos.Add(new ParamInfo("AND", "funint", "DATEDIFF(d,{ADInfo.Ad_EndDate}, GETDATE())", "<=", "0"));
                Query.ParamInfos.Add(new ParamInfo("AND", "str", "ADInfo.Ad_Site", "=", "CN"));
                Query.OrderInfos.Add(new OrderInfo("ADInfo.Ad_Sort", "asc"));
                IList<ADInfo> ADs = WebAD.GetADs(Query, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
                if (ADs != null)
                {
                    switch (DisplayStyle)
                    {
                        case "cycle":
                            Ad_String = AD_Cycle_Show(ADs, sys_Install_Path, Propertys, Positioninfo.Ad_Position_Width, Positioninfo.Ad_Position_Height);
                            break;
                        case "keyword":
                            Ad_String = AD_Keyword_Show(ADs, sys_Install_Path, Propertys);
                            break;
                        case "keyword7":
                            Ad_String = AD_Keyword_Show7(ADs, sys_Install_Path, Propertys);
                            break;
                        case "textlist":
                            Ad_String = AD_Home_TextList_Show(ADs, sys_Install_Path, Propertys);
                            break;
                        case "list":
                            Ad_String = AD_List_Show(ADs, sys_Install_Path, Propertys, Positioninfo.Ad_Position_Width, Positioninfo.Ad_Position_Height, Col_Num);
                            break;
                        case "list2":
                            Ad_String = AD_List_Show2(ADs, sys_Install_Path, Propertys, Positioninfo.Ad_Position_Width, Positioninfo.Ad_Position_Height, Col_Num);
                            break;
                        case "scroll":
                            Ad_String = AD_Scroll(ADs, sys_Install_Path, Propertys, Positioninfo.Ad_Position_Width, Positioninfo.Ad_Position_Height);
                            break;
                        case "scroll3":
                            Ad_String = AD_Scroll3(ADs, sys_Install_Path, Propertys, Positioninfo.Ad_Position_Width, Positioninfo.Ad_Position_Height);
                            break;
                        case "layout":
                            Ad_String = AD_Layout(ADs, sys_Install_Path, Propertys, Positioninfo.Ad_Position_Width, Positioninfo.Ad_Position_Height);
                            break;
                        case "productimglist":
                            Ad_String = AD_ProductImg_Show(ADs, sys_Install_Path, Propertys, Positioninfo.Ad_Position_Width, Positioninfo.Ad_Position_Height, Col_Num);
                            break;
                        case "new_textlist":
                            Ad_String = AD_Promotion_Text(ADs, sys_Install_Path, Propertys);
                            break;
                        case "scroll_limit":
                            Ad_String = AD_Scroll_PromotionLimit(ADs, sys_Install_Path, Propertys, Positioninfo.Ad_Position_Width, Positioninfo.Ad_Position_Height);
                            break;
                        case "scroll_tag":
                            Ad_String = AD_Scroll_Tag(ADs, sys_Install_Path, Propertys, Positioninfo.Ad_Position_Width, Positioninfo.Ad_Position_Height);
                            break;
                        case "cycletext":
                            Ad_String = AD_Cycle_ShowText(ADs, sys_Install_Path, Propertys);
                            break;
                        case "hometopad":
                            Ad_String = AD_Home_TOPAD(ADs, sys_Install_Path, Propertys, Positioninfo.Ad_Position_Width, Positioninfo.Ad_Position_Height);
                            break;
                        case "leftfloat":
                            Ad_String = AD_Left_Float(ADs, sys_Install_Path, Propertys, Positioninfo.Ad_Position_Width, Positioninfo.Ad_Position_Height);
                            break;
                        case "rightfloat":
                            Ad_String = AD_Right_Float(ADs, sys_Install_Path, Propertys, Positioninfo.Ad_Position_Width, Positioninfo.Ad_Position_Height);
                            break;

                    }
                }
                return Ad_String;
            }
        }
        else
        {
            return Ad_String;
        }

    }

    public string AD_Cycle_Show(IList<ADInfo> ADs, string sys_Install_Path, string Propertys, int Ad_Width, int Ad_Height)
    {
        string Ad_Show_Code = "";

        int show_time_ad;
        //按照播放频率在固定位置循环投放
        int i = ADs.Count;

        int[,] Freq = new int[i, 2];
        int adv_id = 0;
        int j, Min_ID, Min_Freq;
        j = 0;
        if (i == 1)
        {
            if (Propertys == "" || (Propertys != "" && ADs[0].Ad_IsContain == 1 && ADs[0].Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && ADs[0].Ad_IsContain == 0 && ADs[0].Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
            {
                adv_id = ADs[0].Ad_ID;
                Freq[0, 0] = adv_id;
                Freq[0, 1] = 0;
            }
        }
        else
        {
            foreach (ADInfo entity in ADs)
            {
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    if (entity.Ad_Show_Freq > 0)
                    {
                        Freq[j, 0] = entity.Ad_ID;
                        Freq[j, 1] = entity.Ad_Show_times / entity.Ad_Show_Freq;
                    }
                    else
                    {
                        Freq[j, 0] = 99999999;
                        Freq[j, 1] = 99999999;
                    }
                    //j++;
                }
                else
                {
                    Freq[j, 0] = 99999999;
                    Freq[j, 1] = 99999999;
                }
                j++;
            }
            Min_ID = Freq[0, 0];
            Min_Freq = Freq[0, 1];

            for (j = 1; j < i; j++)
            {
                if (Min_Freq > Freq[j, 1] && Freq[j, 1] >= 0)
                {
                    Min_ID = Freq[j, 0];
                    Min_Freq = Freq[j, 1];
                }
            }
            adv_id = Min_ID;
        }

        foreach (ADInfo entity in ADs)
        {
            if (entity.Ad_ID == adv_id)
            {
                if (entity.Ad_MediaKind == 2)
                {
                    if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                    {
                        show_time_ad = WebAD.Adv_Show_Times_Add(adv_id, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
                        Ad_Show_Code = "<a href=\"" + sys_Install_Path + "?Adv_ID=" + adv_id + "\" target=\"_blank\">";
                        Ad_Show_Code = Ad_Show_Code + "<img src=\"" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "\" border=\"0\"";
                        if (Ad_Width > 0)
                        {
                            Ad_Show_Code = Ad_Show_Code + " width=\"" + Ad_Width + "\"";
                        }
                        if (Ad_Height > 0)
                        {
                            Ad_Show_Code = Ad_Show_Code + " height=\"" + Ad_Height + "\"";
                        }
                        Ad_Show_Code = Ad_Show_Code + " alt=\"" + entity.Ad_Title + "\"></a>";
                    }
                }
                else if (entity.Ad_MediaKind == 3)
                {
                    if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                    {
                        show_time_ad = WebAD.Adv_Show_Times_Add(adv_id, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
                        if (entity.Ad_Link != "")
                        {
                            Ad_Show_Code = "<a href=\"" + sys_Install_Path + "?Adv_ID=" + adv_id + "\" target=\"_blank\">";
                        }

                        Ad_Show_Code = Ad_Show_Code + "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0\" ";
                        if (Ad_Width > 0)
                        {
                            Ad_Show_Code = Ad_Show_Code + " width=\"" + Ad_Width + "\"";
                        }
                        if (Ad_Height > 0)
                        {
                            Ad_Show_Code = Ad_Show_Code + " height=\"" + Ad_Height + "\"";
                        }
                        Ad_Show_Code = Ad_Show_Code + "><param name=\"movie\" value=\"" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "\"><param name=\"quality\" value=\"high\"><param name=\"wmode\" value=\"opaque\"></object>";
                        if (entity.Ad_Link != "")
                        {
                            Ad_Show_Code = Ad_Show_Code + "</a>";
                        }
                    }
                }
                else if (entity.Ad_MediaKind == 4)
                {
                    if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                    {
                        show_time_ad = WebAD.Adv_Show_Times_Add(adv_id, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
                        if (entity.Ad_Link != "")
                        {
                            Ad_Show_Code = "<a href=\"" + sys_Install_Path + "?Adv_ID=" + adv_id + "\" target=\"_blank\">";
                        }

                        Ad_Show_Code = Ad_Show_Code + entity.Ad_Media;
                        if (entity.Ad_Link != "")
                        {
                            Ad_Show_Code = Ad_Show_Code + "</a>";
                        }
                    }
                }
            }
        }
        return Ad_Show_Code;
    }

    public string AD_Cycle_ShowText(IList<ADInfo> ADs, string sys_Install_Path, string Propertys)
    {
        string Ad_Show_Code = "";

        int show_time_ad;
        //按照播放频率在固定位置循环投放
        int i = ADs.Count;

        int[,] Freq = new int[i, 2];
        int adv_id = 0;
        int j, Min_ID, Min_Freq;
        j = 0;
        if (i == 1)
        {
            if (Propertys == "" || (Propertys != "" && ADs[0].Ad_IsContain == 1 && ADs[0].Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && ADs[0].Ad_IsContain == 0 && ADs[0].Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
            {
                adv_id = ADs[0].Ad_ID;
                Freq[0, 0] = adv_id;
                Freq[0, 1] = 0;
            }
        }
        else
        {
            foreach (ADInfo entity in ADs)
            {
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    if (entity.Ad_Show_Freq > 0)
                    {
                        Freq[j, 0] = entity.Ad_ID;
                        Freq[j, 1] = entity.Ad_Show_times / entity.Ad_Show_Freq;
                    }
                    else
                    {
                        Freq[j, 0] = 99999999;
                        Freq[j, 1] = 99999999;
                    }
                }
                else
                {
                    Freq[j, 0] = 99999999;
                    Freq[j, 1] = 99999999;
                }
                j++;
            }
            Min_ID = Freq[0, 0];
            Min_Freq = Freq[0, 1];

            for (j = 1; j < i; j++)
            {
                if (Min_Freq > Freq[j, 1] && Freq[j, 1] >= 0)
                {
                    Min_ID = Freq[j, 0];
                    Min_Freq = Freq[j, 1];
                }
            }
            adv_id = Min_ID;
        }

        foreach (ADInfo entity in ADs)
        {
            if (entity.Ad_ID == adv_id)
            {
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    show_time_ad = WebAD.Adv_Show_Times_Add(adv_id, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
                    //Ad_Show_Code = "<a href=\"" + sys_Install_Path + "?Adv_ID=" + adv_id + "\" target=\"_blank\">";
                    //Ad_Show_Code = Ad_Show_Code + "<img src=\"" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "\" border=\"0\"";
                    //if (Ad_Width > 0)
                    //{
                    //    Ad_Show_Code = Ad_Show_Code + " width=\"" + Ad_Width + "\"";
                    //}
                    //if (Ad_Height > 0)
                    //{
                    //    Ad_Show_Code = Ad_Show_Code + " height=\"" + Ad_Height + "\"";
                    //}
                    //Ad_Show_Code = Ad_Show_Code + " alt=\"" + entity.Ad_Title + "\"></a>";
                    Ad_Show_Code = entity.Ad_Title;
                }
            }
        }
        return Ad_Show_Code;
    }

    public string AD_Keyword_Show(IList<ADInfo> ADs, string sys_Install_Path, string Propertys)
    {
        int i = 0;
        string Ad_Show_Code = "";
        List<ADInfo> ListADs = new List<ADInfo>();
        ListADs = (List<ADInfo>)ADs;
        foreach (ADInfo entity in ADs)
        {
            if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
            {
                i++;
                Ad_Show_Code = Ad_Show_Code + " | <a href=\"" + Application["Site_URL"] + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\">" + entity.Ad_Title + "</a>";
            }
            if (i >= 3)
                break;
        }

        return Ad_Show_Code.Substring(3);
    }

    public string AD_Keyword_Show7(IList<ADInfo> ADs, string sys_Install_Path, string Propertys)
    {
        string Ad_Show_Code = "";
        List<ADInfo> ListADs = new List<ADInfo>();
        ListADs = (List<ADInfo>)ADs;
        foreach (ADInfo entity in ADs)
        {
            if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
            {
                Ad_Show_Code = Ad_Show_Code + "<li><a href=\"" + Application["Site_URL"] + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\">" + tools.CutStr(entity.Ad_Title, 22) + "</a></li>";
            }
        }
        return Ad_Show_Code;
    }

    public string AD_Home_TextList_Show(IList<ADInfo> ADs, string sys_Install_Path, string Propertys)
    {
        string Ad_Show_Code = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\">";
        foreach (ADInfo entity in ADs)
        {
            if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
            {
                Ad_Show_Code = Ad_Show_Code + "<tr><td height=\"27\" align=\"left\" class=\"t12\"><img src=\"images/ico_arro.gif\" align=\"absmiddle\" /> <a href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\">";
                Ad_Show_Code = Ad_Show_Code + entity.Ad_Title + "</a> </td></tr>";
            }

        }
        Ad_Show_Code = Ad_Show_Code + "</table>";
        return Ad_Show_Code;
    }

    public string AD_List_Show(IList<ADInfo> ADs, string sys_Install_Path, string Propertys, int Ad_Width, int Ad_Height, int Col_Num)
    {
        string Ad_Show_Code = "";

        int show_time_ad;
        int num = 0;
        Ad_Show_Code = Ad_Show_Code + "<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";
        Ad_Show_Code = Ad_Show_Code + "<tr>";
        foreach (ADInfo entity in ADs)
        {
            if (entity.Ad_MediaKind == 2)
            {
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    num = num + 1;

                    if (num == 1)
                    {
                        Ad_Show_Code = Ad_Show_Code + "<td align=\"center\">";
                    }
                    else
                    {
                        Ad_Show_Code = Ad_Show_Code + "<td align=\"center\" style=\"padding-left:20px;\">";
                    }
                    show_time_ad = WebAD.Adv_Show_Times_Add(entity.Ad_ID, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
                    Ad_Show_Code = Ad_Show_Code + "<a href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\">";
                    Ad_Show_Code = Ad_Show_Code + "<img src=\"" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "\" border=\"0\"";
                    if (Ad_Width > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + " width=\"" + Ad_Width + "\"";
                    }
                    if (Ad_Height > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + " height=\"" + Ad_Height + "\"";
                    }
                    Ad_Show_Code = Ad_Show_Code + " alt=\"" + entity.Ad_Title + "\"></a>";
                    Ad_Show_Code = Ad_Show_Code + "</td>";
                    if (num % Col_Num == 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + "</tr><tr>";
                    }
                }
            }
            if (num >= 4)
                break;
        }
        Ad_Show_Code = Ad_Show_Code + "</tr>";
        Ad_Show_Code = Ad_Show_Code + "</table>";
        return Ad_Show_Code;
    }

    public string AD_List_Show2(IList<ADInfo> ADs, string sys_Install_Path, string Propertys, int Ad_Width, int Ad_Height, int Col_Num)
    {
        string Ad_Show_Code = "";

        int show_time_ad;
        int num = 0;
        RBACUserInfo userInfo = pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01");

        foreach (ADInfo entity in ADs)
        {
            if (entity.Ad_MediaKind == 2)
            {
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    num = num + 1;

                    show_time_ad = WebAD.Adv_Show_Times_Add(entity.Ad_ID, userInfo);
                    Ad_Show_Code = Ad_Show_Code + "<div class=\"left_pic\"><a href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\">";
                    Ad_Show_Code = Ad_Show_Code + "<img src=\"" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "\" border=\"0\"";
                    if (Ad_Width > 0)
                        Ad_Show_Code = Ad_Show_Code + " width=\"" + Ad_Width + "\"";
                    if (Ad_Height > 0)
                        Ad_Show_Code = Ad_Show_Code + " height=\"" + Ad_Height + "\"";
                    Ad_Show_Code = Ad_Show_Code + " alt=\"" + entity.Ad_Title + "\"></a></div>";
                }
            }
            if (num >= 4)
                break;
        }
        return Ad_Show_Code;
    }

    //图片紧凑格式
    public string AD_ProductImg_Show(IList<ADInfo> ADs, string sys_Install_Path, string Propertys, int Ad_Width, int Ad_Height, int Col_Num)
    {
        string Ad_Show_Code = "";

        int show_time_ad;
        int num = 0;
        int row = 1;
        Ad_Show_Code = Ad_Show_Code + "<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">";
        Ad_Show_Code = Ad_Show_Code + "<tr>";
        foreach (ADInfo entity in ADs)
        {
            if (entity.Ad_MediaKind == 2)
            {
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    num = num + 1;

                    if (row == 1 && num == 1)
                    {
                        Ad_Show_Code = Ad_Show_Code + "<td align=\"center\">";
                    }
                    else if (row == 1 && num > 1)
                    {
                        Ad_Show_Code = Ad_Show_Code + "<td align=\"center\" style=\"padding-left:0px;\">";
                    }
                    else if (row > 1 && num == 1)
                    {
                        Ad_Show_Code = Ad_Show_Code + "<td align=\"center\" style=\"padding-top:0px;\">";
                    }
                    else if (row > 1 && num > 1)
                    {
                        Ad_Show_Code = Ad_Show_Code + "<td align=\"center\" style=\"padding-top:0px;padding-left:0px;\">";
                    }
                    show_time_ad = WebAD.Adv_Show_Times_Add(entity.Ad_ID, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
                    Ad_Show_Code = Ad_Show_Code + "<a href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\">";
                    Ad_Show_Code = Ad_Show_Code + "<img src=\"" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "\" border=\"0\"";
                    if (Ad_Width > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + " width=\"" + Ad_Width + "\"";
                    }
                    if (Ad_Height > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + " height=\"" + Ad_Height + "\"";
                    }
                    Ad_Show_Code = Ad_Show_Code + " alt=\"" + entity.Ad_Title + "\"></a>";
                    Ad_Show_Code = Ad_Show_Code + "</td>";
                    if (num % Col_Num == 0)
                    {
                        row += 1;
                        num = 0;
                        Ad_Show_Code = Ad_Show_Code + "</tr><tr>";
                    }
                }
            }
        }
        Ad_Show_Code = Ad_Show_Code + "</tr>";
        Ad_Show_Code = Ad_Show_Code + "</table>";
        return Ad_Show_Code;
    }

    public string AD_Scroll(IList<ADInfo> ADs, string sys_Install_Path, string Propertys, int Ad_Width, int Ad_Height)
    {
        string Ad_Show_Code = "";
        int show_time_ad;
        Ad_Show_Code = "<script type=\"text/javascript\">";

        Ad_Show_Code += "var moveStyle;";
        Ad_Show_Code += "var rand =parseInt(Math.random()*4);";
        Ad_Show_Code += "switch(rand){";
        Ad_Show_Code += "case 0:	moveStyle=\"left\" ;break;";
        Ad_Show_Code += "case 1:	moveStyle=\"right\" ;break;";
        Ad_Show_Code += "case 2:	moveStyle=\"down\" ;break;";
        Ad_Show_Code += "case 3:	moveStyle=\"up\" ;break;";
        Ad_Show_Code += "}";
        Ad_Show_Code += "$(function(){";
        Ad_Show_Code += "	$(\"#KinSlideshow1\").KinSlideshow({moveStyle:moveStyle,isHasTitleFont:false,isHasBtn:true,isHasTitleBar:false,btn:{btn_bgColor:\"#FFFFFF\",btn_bgHoverColor:\"#1072aa\",btn_fontColor:\"#000000\",btn_fontHoverColor:\"#FFFFFF\",btn_borderColor:\"#aaaaaa\",btn_borderHoverColor:\"#1188c0\",btn_borderWidth:1,btn_bgAlpha:100}});";
        Ad_Show_Code += "})";
        Ad_Show_Code += "</script>";
        Ad_Show_Code += "<style type=\"text/css\">";
        Ad_Show_Code += "#KinSlideshow{ overflow:hidden; width:" + Ad_Width + "px; height:" + Ad_Height + "px;}";
        Ad_Show_Code += "</style>";

        Ad_Show_Code += "<div id=\"KinSlideshow1\" style=\"visibility:hidden;overflow:auto;\">";
        foreach (ADInfo entity in ADs)
        {
            if (entity.Ad_MediaKind == 2)
            {
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    show_time_ad = WebAD.Adv_Show_Times_Add(entity.Ad_ID, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
                    Ad_Show_Code = Ad_Show_Code + "<a href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\">";
                    Ad_Show_Code = Ad_Show_Code + "<img src=\"" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "\" border=\"0\"";
                    if (Ad_Width > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + " width=\"" + Ad_Width + "\"";
                    }
                    if (Ad_Height > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + " height=\"" + Ad_Height + "\"";
                    }
                    Ad_Show_Code = Ad_Show_Code + " alt=\"" + entity.Ad_Title + "\"></a>";
                }
            }
        }
        Ad_Show_Code += "</div>";
        return Ad_Show_Code;
    }

    public string AD_Scroll3(IList<ADInfo> ADs, string sys_Install_Path, string Propertys, int Ad_Width, int Ad_Height)
    {
        string Ad_Show_Code = "";
        int show_time_ad;
        Ad_Show_Code = Ad_Show_Code + "<div class=\"wrapper\">";
        Ad_Show_Code = Ad_Show_Code + "<div id=\"focus\">";

        Ad_Show_Code = Ad_Show_Code + "<ul>";

        int i = 0;
        foreach (ADInfo entity in ADs)
        {
            if (entity.Ad_MediaKind == 2)
            {
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    Ad_Show_Code = Ad_Show_Code + "<li  style=\"left:0; top:0; width:" + Ad_Width + "; height:" + Ad_Height + ";\"><a href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\">";
                    Ad_Show_Code = Ad_Show_Code + "<img src=\"" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "\" border=\"0\"";
                    if (Ad_Width > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + " width=\"" + Ad_Width + "\"";
                    }
                    if (Ad_Height > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + " height=\"" + Ad_Height + "\"";
                    }
                    Ad_Show_Code = Ad_Show_Code + " alt=\"" + entity.Ad_Title + "\"></a></li>";

                }

            }
        }

        Ad_Show_Code = Ad_Show_Code + "</ul></div></div>";
        return Ad_Show_Code;
    }

    public string AD_Scroll_Tag(IList<ADInfo> ADs, string sys_Install_Path, string Propertys, int Ad_Width, int Ad_Height)
    {
        string Ad_Show_Code = "";
        int show_time_ad;

        int i = 0;
        foreach (ADInfo entity in ADs)
        {
            if (entity.Ad_MediaKind == 2)
            {
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    Ad_Show_Code = Ad_Show_Code + "<h3 id=\"ad_text_" + Propertys + "\" class=\"texta\">";
                    Ad_Show_Code = Ad_Show_Code + "<a href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\">" + entity.Ad_Title + "</a>";
                    Ad_Show_Code = Ad_Show_Code + "</h3>";
                    Ad_Show_Code = Ad_Show_Code + "<div id=\"ad_img_" + Propertys + "\" class=\"img\">";
                    Ad_Show_Code = Ad_Show_Code + "<a href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\">";
                    Ad_Show_Code = Ad_Show_Code + "<img src=\"" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "\" border=\"0\"";
                    if (Ad_Width > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + " width=\"" + Ad_Width + "\"";
                    }
                    if (Ad_Height > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + " height=\"" + Ad_Height + "\"";
                    }
                    Ad_Show_Code = Ad_Show_Code + " alt=\"" + entity.Ad_Title + "\"></a>";
                    Ad_Show_Code = Ad_Show_Code + "</div>";


                }

            }
        }

        return Ad_Show_Code;
    }

    public string AD_Layout(IList<ADInfo> ADs, string sys_Install_Path, string Propertys, int Ad_Width, int Ad_Height)
    {
        if (Request.Cookies["layoutad"] != null)
        {
            if (tools.NullDate(Request.Cookies["layoutad"].Value).Year > 1900)
            {
                if (tools.NullDate(Request.Cookies["layoutad"].Value).AddDays(1) > DateTime.Now)
                {
                    return "";
                }
            }
        }
        string Ad_Show_Code = "";
        int show_time_ad;

        foreach (ADInfo entity in ADs)
        {
            if (entity.Ad_MediaKind == 2)
            {
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    show_time_ad = WebAD.Adv_Show_Times_Add(entity.Ad_ID, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
                    Ad_Show_Code = "<div id=\"layout_ad\" style=\"border:1px solid #B9C9EF;padding:1px;background-color:#DDF5FF;POSITION: fixed; TEXT-ALIGN: center; LINE-HEIGHT: " + (Ad_Height + 30) + "px; WIDTH: " + Ad_Width + "px; BOTTOM: 0px; HEIGHT: " + (Ad_Height + 30) + "px; FONT-SIZE: 12px; CURSOR: pointer; RIGHT: 0px; _position: absolute; _right: auto\"><div class=\"tit\">" + entity.Ad_Title + "<span onclick=\"$('#layout_ad').hide();$('#cookies_div').load('/product/ask_do.aspx?action=layoutad&timer='+Math.random());\">X</span></div><div><a href='" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "' target='_blank'>";
                    Ad_Show_Code = Ad_Show_Code + "<img src='" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "' border='0'";
                    if (Ad_Width > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + " width='" + Ad_Width + "'";
                    }
                    if (Ad_Height > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + " height='" + Ad_Height + "'";
                    }
                    Ad_Show_Code = Ad_Show_Code + " alt='" + entity.Ad_Title + "'></a></div></div><script>goTopEx();</script>";
                    break;
                }

            }
            else if (entity.Ad_MediaKind == 4)
            {
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    show_time_ad = WebAD.Adv_Show_Times_Add(entity.Ad_ID, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
                    Ad_Show_Code = "<div id=\"layout_ad\"><a href='" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "' target='_blank'>";
                    Ad_Show_Code = Ad_Show_Code + entity.Ad_Media + "</a></div><script>goTopEx();</script>";
                    break;
                }
            }
        }




        return Ad_Show_Code;
    }

    public string AD_Promotion_Text(IList<ADInfo> ADs, string sys_Install_Path, string Propertys)
    {
        string Ad_Show_Code = "";
        foreach (ADInfo entity in ADs)
        {
            if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
            {
                Ad_Show_Code = Ad_Show_Code + "<tr><td class=\"td_content\"><a href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\">";
                Ad_Show_Code = Ad_Show_Code + entity.Ad_Title + "</a></td></tr>";
            }

        }
        return Ad_Show_Code;
    }

    public string AD_Scroll_PromotionLimit(IList<ADInfo> ADs, string sys_Install_Path, string Propertys, int Ad_Width, int Ad_Height)
    {
        string Ad_Show_Code = "";
        int show_time_ad;
        Ad_Show_Code = "<script type=\"text/javascript\">";
        string id = DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
        Ad_Show_Code += "var moveStyle;";
        Ad_Show_Code += "var rand =3;";
        Ad_Show_Code += "switch(rand){";
        Ad_Show_Code += "case 0:	moveStyle=\"left\" ;break;";
        Ad_Show_Code += "case 1:	moveStyle=\"right\" ;break;";
        Ad_Show_Code += "case 2:	moveStyle=\"down\" ;break;";
        Ad_Show_Code += "case 3:	moveStyle=\"up\" ;break;";
        Ad_Show_Code += "}";
        Ad_Show_Code += "$(function(){";
        Ad_Show_Code += "	$(\"#KinSlideshow_" + id + "\").KinSlideshow({moveStyle:moveStyle,isHasTitleFont:false,isHasBtn:false,isHasTitleBar:false,btn:{btn_bgColor:\"#FFFFFF\",btn_bgHoverColor:\"#1072aa\",btn_fontColor:\"#000000\",btn_fontHoverColor:\"#FFFFFF\",btn_borderColor:\"#aaaaaa\",btn_borderHoverColor:\"#1188c0\",btn_borderWidth:1,btn_bgAlpha:100}});";
        Ad_Show_Code += "})";
        Ad_Show_Code += "</script>";
        //Ad_Show_Code += "<style type=\"text/css\">";
        //Ad_Show_Code += "#KinSlideshow{ overflow:hidden; width:" + Ad_Width + "px; height:" + Ad_Height + "px;}";
        //Ad_Show_Code += "</style>";

        Ad_Show_Code += "<div id=\"KinSlideshow_" + id + "\" style=\"visibility:hidden;overflow:auto;\">";
        foreach (ADInfo entity in ADs)
        {
            if (entity.Ad_MediaKind == 2)
            {
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    show_time_ad = WebAD.Adv_Show_Times_Add(entity.Ad_ID, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
                    Ad_Show_Code = Ad_Show_Code + "<a href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\">";
                    Ad_Show_Code = Ad_Show_Code + "<img src=\"" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "\" border=\"0\"";
                    if (Ad_Width > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + " width=\"" + Ad_Width + "\"";
                    }
                    if (Ad_Height > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + " height=\"" + Ad_Height + "\"";
                    }
                    Ad_Show_Code = Ad_Show_Code + " alt=\"" + entity.Ad_Title + "\"></a>";
                }
            }
        }
        Ad_Show_Code += "</div>";
        return Ad_Show_Code;
    }

    public string AD_Home_TOPAD(IList<ADInfo> ADs, string sys_Install_Path, string Propertys, int Ad_Width, int Ad_Height)
    {
        if (Request.Cookies["topad"] != null)
        {
            if (tools.NullDate(Request.Cookies["topad"].Value).Year < 1900)
            {
                Response.Cookies["topad"].Value = DateTime.Now.ToString();
                Response.Cookies["topad"].Expires = DateTime.Now.AddMonths(1);
            }
            else
            {
                if (tools.NullDate(Request.Cookies["topad"].Value).AddDays(1) > DateTime.Now)
                {
                    return "";
                }
            }
        }
        else
        {
            Response.Cookies["topad"].Value = DateTime.Now.ToString();
            Response.Cookies["topad"].Expires = DateTime.Now.AddMonths(1);
        }
        string Ad_Show_Code = "";
        int show_time_ad;
        Ad_Show_Code = "<script type=\"text/javascript\">";
        foreach (ADInfo entity in ADs)
        {
            if (entity.Ad_MediaKind == 2)
            {
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    show_time_ad = WebAD.Adv_Show_Times_Add(entity.Ad_ID, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));

                    Ad_Show_Code = Ad_Show_Code + "var gg960ShowTime = 8000; ";
                    Ad_Show_Code = Ad_Show_Code + "var gg960Time = null;";

                    Ad_Show_Code = Ad_Show_Code + "function open_gg960(showBtn){";
                    Ad_Show_Code = Ad_Show_Code + "    $('.gg_full .gg_fcon').html(gg960Con).slideDown(300,function(){";
                    Ad_Show_Code = Ad_Show_Code + "        if(showBtn !== false){";
                    Ad_Show_Code = Ad_Show_Code + "            $('.gg_full .gg_fbtn').fadeIn();";
                    Ad_Show_Code = Ad_Show_Code + "        }";
                    Ad_Show_Code = Ad_Show_Code + "        if(gg960Time){clearTimeout(gg960Time);}";
                    Ad_Show_Code = Ad_Show_Code + "        gg960Time = setTimeout(close_gg960,gg960ShowTime);";
                    Ad_Show_Code = Ad_Show_Code + "    });";
                    Ad_Show_Code = Ad_Show_Code + "}";

                    Ad_Show_Code = Ad_Show_Code + "function close_gg960(){";
                    Ad_Show_Code = Ad_Show_Code + "    $('.gg_full .gg_fcon').slideUp(500,function(){";
                    Ad_Show_Code = Ad_Show_Code + "        $(this).html('');";
                    Ad_Show_Code = Ad_Show_Code + "        $('.gg_fclose').hide();";
                    Ad_Show_Code = Ad_Show_Code + "        $('.gg_freplay').show();";
                    Ad_Show_Code = Ad_Show_Code + "    });";
                    Ad_Show_Code = Ad_Show_Code + "}";

                    Ad_Show_Code = Ad_Show_Code + "var gg960Con;";
                    Ad_Show_Code = Ad_Show_Code + "gg960Con = \"";
                    Ad_Show_Code = Ad_Show_Code + "<a href='" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "' target='_blank'>";
                    Ad_Show_Code = Ad_Show_Code + "<img src='" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "' border='0'";
                    if (Ad_Width > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + " width='" + Ad_Width + "'";
                    }
                    if (Ad_Height > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + " height='" + Ad_Height + "'";
                    }
                    Ad_Show_Code = Ad_Show_Code + " alt='" + entity.Ad_Title + "'></a>\";";
                    Ad_Show_Code = Ad_Show_Code + "setTimeout(open_gg960,1500);";


                    break;
                }

            }
        }
        Ad_Show_Code += "</script>";



        return Ad_Show_Code;
    }

    public string AD_Left_Float(IList<ADInfo> ADs, string sys_Install_Path, string Propertys, int Ad_Width, int Ad_Height)
    {
        string Ad_Show_Code = "";
        int show_time_ad;
        if (Request.Cookies["leftad"] != null)
        {
            if (tools.NullDate(Request.Cookies["leftad"].Value).Year > 1900)
            {
                if (tools.NullDate(Request.Cookies["leftad"].Value).AddDays(1) > DateTime.Now)
                {
                    return "";
                }
            }
        }

        foreach (ADInfo entity in ADs)
        {
            if (entity.Ad_MediaKind == 2)
            {
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    show_time_ad = WebAD.Adv_Show_Times_Add(entity.Ad_ID, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));

                    Ad_Show_Code = Ad_Show_Code + "<SCRIPT language=\"JavaScript\">";
                    //显示样式
                    Ad_Show_Code = Ad_Show_Code + "document.writeln(\"<style type=\\\"text\\/css\\\">\");";
                    Ad_Show_Code = Ad_Show_Code + "document.writeln(\"#leftDiv{";
                    if (Ad_Width > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + "width:" + Ad_Width + "px;";
                    }
                    if (Ad_Height > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + "height:" + Ad_Height + "px;";
                    }
                    Ad_Show_Code = Ad_Show_Code + "background-color:#fff;position:absolute;}\");";
                    Ad_Show_Code = Ad_Show_Code + "document.writeln(\".itemFloatLeft{width:" + Ad_Width + "px;height:auto;line-height:5px}\");";
                    Ad_Show_Code = Ad_Show_Code + "document.writeln(\"<\\/style>\");";

                    Ad_Show_Code = Ad_Show_Code + "</SCRIPT>";
                    Ad_Show_Code = Ad_Show_Code + "<div id=\"leftDiv\" style=\"top:40px;left:5px\">";
                    Ad_Show_Code = Ad_Show_Code + "<div id=\"left1\" class=\"itemFloatLeft\">";
                    Ad_Show_Code = Ad_Show_Code + "<a href='" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "' target='_blank'>";
                    Ad_Show_Code = Ad_Show_Code + "<img src='" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "' border='0'";
                    if (Ad_Width > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + " width='" + Ad_Width + "'";
                    }
                    if (Ad_Height > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + " height='" + Ad_Height + "'";
                    }
                    Ad_Show_Code = Ad_Show_Code + " alt='" + entity.Ad_Title + "'></a>";
                    Ad_Show_Code = Ad_Show_Code + "<br><a href=\"javascript:void(0);\" onclick=\"$('#leftDiv').hide();$('#cookies_div').load('/product/ask_do.aspx?action=leftad&timer='+Math.random());\"  style=\"display:block; color:#333333; background-color:#cccccc;line-height:20px;padding-left:10px;\">关闭</a>";
                    Ad_Show_Code = Ad_Show_Code + "</div>";
                    Ad_Show_Code = Ad_Show_Code + "</div>";




                    break;
                }

            }
        }



        return Ad_Show_Code;
    }

    public string AD_Right_Float(IList<ADInfo> ADs, string sys_Install_Path, string Propertys, int Ad_Width, int Ad_Height)
    {
        string Ad_Show_Code = "";
        int show_time_ad;
        if (Request.Cookies["rightad"] != null)
        {
            if (tools.NullDate(Request.Cookies["rightad"].Value).Year > 1900)
            {
                if (tools.NullDate(Request.Cookies["rightad"].Value).AddDays(1) > DateTime.Now)
                {
                    return "";
                }
            }
        }
        foreach (ADInfo entity in ADs)
        {
            if (entity.Ad_MediaKind == 2)
            {
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    show_time_ad = WebAD.Adv_Show_Times_Add(entity.Ad_ID, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));

                    Ad_Show_Code = Ad_Show_Code + "<SCRIPT language=\"JavaScript\">";
                    //显示样式
                    Ad_Show_Code = Ad_Show_Code + "document.writeln(\"<style type=\\\"text\\/css\\\">\");";
                    Ad_Show_Code = Ad_Show_Code + "document.writeln(\"#rightDiv{";
                    if (Ad_Width > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + "width:" + Ad_Width + "px;";
                    }
                    if (Ad_Height > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + "height:" + Ad_Height + "px;";
                    }
                    Ad_Show_Code = Ad_Show_Code + "background-color:#fff;position:absolute;}\");";
                    Ad_Show_Code = Ad_Show_Code + "document.writeln(\".itemFloatRight{width:" + Ad_Width + "px;height:auto;line-height:5px}\");";
                    Ad_Show_Code = Ad_Show_Code + "document.writeln(\"<\\/style>\");";

                    Ad_Show_Code = Ad_Show_Code + "</SCRIPT>";
                    Ad_Show_Code = Ad_Show_Code + "<div id=\"rightDiv\" style=\"top:40px;right:5px\">";
                    Ad_Show_Code = Ad_Show_Code + "<div id=\"right1\" class=\"itemFloatRight\">";
                    Ad_Show_Code = Ad_Show_Code + "<a href='" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "' target='_blank'>";
                    Ad_Show_Code = Ad_Show_Code + "<img src='" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "' border='0'";
                    if (Ad_Width > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + " width='" + Ad_Width + "'";
                    }
                    if (Ad_Height > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + " height='" + Ad_Height + "'";
                    }
                    Ad_Show_Code = Ad_Show_Code + " alt='" + entity.Ad_Title + "'></a>";
                    Ad_Show_Code = Ad_Show_Code + "<br><a href=\"javascript:void(0);\" onclick=\"$('#rightDiv').hide();$('#cookies_div').load('/product/ask_do.aspx?action=rightad&timer='+Math.random());\"  style=\"display:block; color:#333333; background-color:#cccccc;line-height:20px;padding-left:10px;\">关闭</a>";
                    Ad_Show_Code = Ad_Show_Code + "</div>";
                    Ad_Show_Code = Ad_Show_Code + "</div>";




                    break;
                }

            }
        }



        return Ad_Show_Code;
    }

}