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
                    if (entity.Ad_Link.Length > 0 && entity.Ad_Link != "http://" && entity.Ad_Link != "https://")
                    {
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
                        case "keyword_article":
                            Ad_String = AD_Keyword_Show_Article(ADs, sys_Install_Path, Propertys);
                            break;
                        case "keyword7":
                            Ad_String = AD_Keyword_Show7(ADs, sys_Install_Path, Propertys);
                            break;
                        case "keyword8":
                            Ad_String = AD_Keyword_Show8(ADs, sys_Install_Path, Propertys);
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
                        case "scroll1":
                            Ad_String = AD_Scroll1(ADs, sys_Install_Path, Propertys, Positioninfo.Ad_Position_Width, Positioninfo.Ad_Position_Height);
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
                        case "home_scroll":
                            Ad_String = AD_Home_Scroll(ADs, sys_Install_Path, Propertys);
                            break;
                        case "tradeindex_scroll":
                            Ad_String = AD_TradeIndex_Scroll(ADs, sys_Install_Path, Propertys);
                            break;
                        case "yunjin_scroll":
                            Ad_String = AD_Yunjin_Scroll(ADs, sys_Install_Path, Propertys);
                            break;
                        case "productlist_scroll":
                            Ad_String = AD_ProductList_Scroll(ADs, sys_Install_Path, Propertys);
                            break;
                        case "memberlogin_ad":
                            Ad_String = AD_MemberLogin(ADs, sys_Install_Path, Propertys);
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


    public string AD_Scroll1(IList<ADInfo> ADs, string sys_Install_Path, string Propertys, int Ad_Width, int Ad_Height)
    {
        string Ad_Show_Code = "";
        string title = "'";
        string imgs = "'";
        string url = "'";
        int show_time_ad;
        Ad_Show_Code = Ad_Show_Code + "<div align=\"center\">";
        Ad_Show_Code = Ad_Show_Code + "<script language=\"javascript\" type=\"text/javascript\" src=\"/scripts/swfobject_source.js\"></script>";

        Ad_Show_Code = Ad_Show_Code + "<DIV id=dplayer2 style=\"PADDING-RIGHT: 0px; PADDING-LEFT: 0px; BACKGROUND: #ffffff; PADDING-BOTTOM: 0px; MARGIN: 0px auto; WIDTH: 310; PADDING-TOP: 0px; HEIGHT: 211\"></DIV>";

        Ad_Show_Code = Ad_Show_Code + "<SCRIPT language=javascript type=text/javascript>";

        int i = 0;
        foreach (ADInfo entity in ADs)
        {
            if (entity.Ad_MediaKind == 2)
            {
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    i++;
                    if (i == 1)
                    {
                        imgs += "" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "";
                        //title += "" + entity.Ad_Title + "";
                        url += "" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "";
                    }
                    else
                    {
                        imgs += "|" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "";
                        //title += "|" + entity.Ad_Title + "";
                        url += "|" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "";
                    }
                    //show_time_ad = WebAD.Adv_Show_Times_Add(entity.Ad_ID, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01")); 
                }

            }
        }
        imgs += "';";
        title += "';";
        url += "';";
        Ad_Show_Code += "var titles =" + title;
        Ad_Show_Code += "var imgs=" + imgs;
        Ad_Show_Code += "var urls=" + url;
        Ad_Show_Code += "var pw = 308;";
        Ad_Show_Code = Ad_Show_Code + "var ph = 270;";
        Ad_Show_Code = Ad_Show_Code + "var sizes = 12;";
        Ad_Show_Code += "var Times = 4000;";
        Ad_Show_Code = Ad_Show_Code + "var umcolor = 0xFFFFFF;";
        Ad_Show_Code = Ad_Show_Code + "var btnbg =0xcc0000;";
        Ad_Show_Code = Ad_Show_Code + "var txtcolor =0xFFFFFF;";
        Ad_Show_Code = Ad_Show_Code + "var txtoutcolor =0x000000;";
        Ad_Show_Code = Ad_Show_Code + "var flash = new SWFObject('/flash/focus1.swf', 'mymovie', pw, ph, '7', '');";
        Ad_Show_Code = Ad_Show_Code + "flash.addParam('allowFullScreen', 'false');";
        Ad_Show_Code = Ad_Show_Code + "flash.addParam('allowScriptAccess', 'always');";
        Ad_Show_Code = Ad_Show_Code + "flash.addParam('quality', 'high');";
        Ad_Show_Code = Ad_Show_Code + "flash.addParam('wmode', 'Transparent');";
        Ad_Show_Code = Ad_Show_Code + "flash.addVariable('pw', pw);";
        Ad_Show_Code = Ad_Show_Code + "flash.addVariable('ph', ph);";
        Ad_Show_Code = Ad_Show_Code + "flash.addVariable('sizes', sizes);";
        Ad_Show_Code = Ad_Show_Code + "flash.addVariable('umcolor', umcolor);";
        Ad_Show_Code = Ad_Show_Code + "flash.addVariable('btnbg', btnbg);";
        Ad_Show_Code = Ad_Show_Code + "flash.addVariable('txtcolor', txtcolor);";
        Ad_Show_Code = Ad_Show_Code + "flash.addVariable('txtoutcolor', txtoutcolor);";
        Ad_Show_Code = Ad_Show_Code + "flash.addVariable('urls', urls);";
        Ad_Show_Code = Ad_Show_Code + "flash.addVariable('Times', Times);";
        Ad_Show_Code = Ad_Show_Code + "flash.addVariable('titles', titles);";
        Ad_Show_Code = Ad_Show_Code + "flash.addVariable('imgs', imgs);";
        Ad_Show_Code = Ad_Show_Code + "flash.write('dplayer2');";
        Ad_Show_Code = Ad_Show_Code + "</SCRIPT>";
        Ad_Show_Code = Ad_Show_Code + "</div>";
        return Ad_Show_Code;
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
                        Ad_Show_Code = Ad_Show_Code + "></a>";
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

    //public string AD_Keyword_Show(IList<ADInfo> ADs, string sys_Install_Path, string Propertys)
    //{
    //    string Ad_Show_Code = "";
    //    List<ADInfo> ListADs = new List<ADInfo>();
    //    ListADs = (List<ADInfo>)ADs;
    //    int i = 0;
    //    foreach (ADInfo entity in ADs)
    //    {
    //        if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
    //        {
    //            i++;
    //            Ad_Show_Code = Ad_Show_Code + " | <a href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\">" + entity.Ad_Title + "</a>";
    //        }
    //        if (i >= 3)
    //            break;

    //    }

    //    return Ad_Show_Code.Substring(3);
    //}
    public string AD_Keyword_Show(IList<ADInfo> ADs, string sys_Install_Path, string Propertys)
    {
        StringBuilder strHTML = new StringBuilder();

        //int i = 0;
        //List<ADInfo> ListADs = new List<ADInfo>();
        //ListADs = (List<ADInfo>)ADs;
        foreach (ADInfo entity in ADs)
        {
            if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
            {
                //i++;                
                strHTML.Append("<a href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\">");
                strHTML.Append(entity.Ad_Title + "</a> ");
                //if (i >= 6)
                //{
                //    break;
                //}
            }

        }
        return strHTML.ToString();
    }


    public string AD_Keyword_Show_Article(IList<ADInfo> ADs, string sys_Install_Path, string Propertys)
    {
        string Ad_Show_Code = "";
        List<ADInfo> ListADs = new List<ADInfo>();
        ListADs = (List<ADInfo>)ADs;
        int i = 0;
        foreach (ADInfo entity in ADs)
        {
            if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
            {
                i++;
                Ad_Show_Code = Ad_Show_Code + " <a href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\">" + entity.Ad_Title + "</a>";
            }
            if (i >= 6)
                break;

        }

        return Ad_Show_Code;
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
                Ad_Show_Code = Ad_Show_Code + "<li><a href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\">" + entity.Ad_Title + "</a></li>";
            }
        }
        return Ad_Show_Code;
    }
    public string AD_Keyword_Show8(IList<ADInfo> ADs, string sys_Install_Path, string Propertys)
    {
        string Ad_Show_Code = "";
        List<ADInfo> ListADs = new List<ADInfo>();
        ListADs = (List<ADInfo>)ADs;
        foreach (ADInfo entity in ADs)
        {
            if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
            {
                Ad_Show_Code = Ad_Show_Code + "<tr><td><a href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\">" + entity.Ad_Title + "</a></td></tr>";
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
        foreach (ADInfo entity in ADs)
        {
            if (entity.Ad_MediaKind == 2)
            {
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    show_time_ad = WebAD.Adv_Show_Times_Add(entity.Ad_ID, pub.CreateUserPrivilege("237da5cb-1fa2-4862-be25-d83077adeb01"));
                    Ad_Show_Code = Ad_Show_Code + "<li><a href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\">";
                    Ad_Show_Code = Ad_Show_Code + "<img src=\"" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "\" border=\"0\"";
                    if (Ad_Width > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + " width=\"" + Ad_Width + "\"";
                    }
                    if (Ad_Height > 0)
                    {
                        Ad_Show_Code = Ad_Show_Code + " height=\"" + Ad_Height + "\"";
                    }
                    Ad_Show_Code = Ad_Show_Code + " ></a></li>";
                }
            }
        }

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
        Ad_Show_Code = Ad_Show_Code + "<script type=\"text/javascript\">";
        Ad_Show_Code = Ad_Show_Code + " var focus_width905=539;";

        Ad_Show_Code = Ad_Show_Code + " var focus_height905=208;";
        Ad_Show_Code = Ad_Show_Code + " var text_height905=0;";
        Ad_Show_Code = Ad_Show_Code + "  var swf_height905 = focus_height905+text_height905;";
        string pics = "";
        string links = "";
        int i = 0;
        foreach (ADInfo entity in ADs)
        {
            if (entity.Ad_MediaKind == 2)
            {
                if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
                {
                    i++;
                    if (i == 1)
                    {
                        pics += pub.FormatImgURL(entity.Ad_Media, "fullpath");
                        links += "" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "";
                    }
                    else
                    {
                        pics += "|" + pub.FormatImgURL(entity.Ad_Media, "fullpath");
                        links += "|" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "";
                    }

                }

            }
        }
        Ad_Show_Code = Ad_Show_Code + " var pics905='" + pics + "';";
        Ad_Show_Code = Ad_Show_Code + " var links905='" + links + "';";

        Ad_Show_Code = Ad_Show_Code + " document.write('<object classid=\"clsid:d27cdb6e-ae6d-11cf-96b8-444553540000\" codebase=\"http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\" width=\"'+ focus_width905 +'\" height=\"'+ swf_height905 +'\">');";
        Ad_Show_Code = Ad_Show_Code + " document.write('<param name=\"allowScriptAccess\" value=\"sameDomain\"><param name=\"movie\" value=\"Flash.swf\"><param name=\"quality\" value=\"high\"><param name=\"bgcolor\" value=\"#FFFFFF\">');";
        Ad_Show_Code = Ad_Show_Code + " document.write('<param name=\"menu\" value=\"false\"><param name=wmode value=\"opaque\">');";
        Ad_Show_Code = Ad_Show_Code + " document.write('<param name=\"FlashVars\" value=\"pics='+pics905+'&links='+links905+'&borderwidth='+focus_width905+'&borderheight='+focus_height905+'\">');";
        Ad_Show_Code = Ad_Show_Code + " document.write('<embed src=\"../Flash.swf\" wmode=\"opaque\" FlashVars=\"pics='+pics905+'&links='+links905+'&borderwidth='+focus_width905+'&borderheight='+focus_height905+'\" menu=\"false\" bgcolor=\"#FFFFFF\" quality=\"high\" width=\"'+ focus_width905 +'\" height=\"'+ swf_height905 +'\" allowScriptAccess=\"sameDomain\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" />');";
        Ad_Show_Code = Ad_Show_Code + " document.write('</object>');";

        Ad_Show_Code = Ad_Show_Code + "  </script>";

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
                    Ad_Show_Code = "<div id=\"layout_ad\"style=\"border:1px solid #B9C9EF;padding:1px;background-color:#DDF5FF;POSITION: fixed; TEXT-ALIGN: center; LINE-HEIGHT: " + (Ad_Height + 30) + "px; WIDTH: " + Ad_Width + "px; BOTTOM: 0px; HEIGHT: " + (Ad_Height + 30) + "px; FONT-SIZE: 12px; CURSOR: pointer; RIGHT: 0px; _position: absolute; _right: auto\"><div class=\"tit\">" + entity.Ad_Title + "<span onclick=\"$('#layout_ad').hide();$('#layout_ad').empty();$('#cookies_div').load('/product/ask_do.aspx?action=layoutad&timer='+Math.random());\">X</span></div><a href='" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "' target='_blank'>";
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


    public string AD_Home_Scroll(IList<ADInfo> ADs, string sys_Install_Path, string Propertys)
    {
        StringBuilder strHTML = new StringBuilder();
        StringBuilder strIMG = new StringBuilder();
        StringBuilder strBAR = new StringBuilder();
        strIMG.Append("<div id=\"flash02\">");
        strBAR.Append("<div class=\"flash_bar\">");
        int i = 0;
        foreach (ADInfo entity in ADs)
        {
            if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
            {
                i++;
                if (i == 1)
                {
                    strIMG.Append("<div style=\"display:block;\" id=\"flash" + i + "\" name=\"FFFFFF\"><a style=\"background-image:url(" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + ");background-repeat:no-repeat; background-position:certer;  width:100%;height:450px;margin:0 auto; display:block; background-position:center;\" href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\"  target=\"_blank\"></a></div>");
                    strBAR.Append("<div class=\"dq\" id=\"f" + i + "\" onclick=\"changeflash(" + i + ")\"></div>");
                }
                else
                {
                    strIMG.Append("<div style=\"display:none;\" id=\"flash" + i + "\" name=\"FFFFFF\"><a style=\"background-image:url(" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + ");background-repeat:no-repeat; background-position:certer;  width:100%;height:450px;margin:0 auto; display:block; background-position:center;\" href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\"></a></div>");
                    strBAR.Append("<div class=\"no\" id=\"f" + i + "\" onclick=\"changeflash(" + i + ")\"></div>");
                }
            }
        }
        strIMG.Append("</div>");
        strBAR.Append("</div>");
        strHTML.Append(strIMG.ToString());
        strHTML.Append("<script type=\"text/javascript\">");
        strHTML.Append("var currentindex = 1;");
        strHTML.Append("$('#flash').css('background-color', $('#flash1').attr('name'));");
        strHTML.Append("function changeflash(i) {");
        strHTML.Append("  currentindex = i;");
        strHTML.Append("  for (j = 1; j <= " + i + "; j++) {");
        strHTML.Append("    if (j == i) {");
        strHTML.Append("      $('#flash' + j).fadeIn('normal');");
        strHTML.Append("      $('#flash' + j).css('display', 'block');");
        strHTML.Append("      $('#f' + j).removeClass();");
        strHTML.Append("      $('#f' + j).addClass('dq');");
        strHTML.Append("      $('#flash').css('background-color', $('#flash' + j).attr('name'));");
        strHTML.Append("    }");
        strHTML.Append("    else {");
        strHTML.Append("      $('#flash' + j).css('display', 'none');");
        strHTML.Append("      $('#f' + j).removeClass();");
        strHTML.Append("      $('#f' + j).addClass('no');");
        strHTML.Append("    }");
        strHTML.Append("  }");
        strHTML.Append("}");
        strHTML.Append("function startAm() {");
        strHTML.Append("  timerID = setInterval('timer_tick()', 3000);");//4000代表间隔时间设置
        strHTML.Append("}");
        strHTML.Append("function stopAm() {");
        strHTML.Append("  clearInterval(timerID);");
        strHTML.Append("}");
        strHTML.Append("function timer_tick() {");
        strHTML.Append("  currentindex = currentindex >= " + i + " ? 1 : currentindex + 1;");
        strHTML.Append("  changeflash(currentindex);");
        strHTML.Append("}");
        strHTML.Append("$(document).ready(function () {");
        strHTML.Append("  $('.flash_bar div').mouseover(function () { stopAm(); }).mouseout(function () { startAm(); });");
        strHTML.Append("  startAm();");
        strHTML.Append("});");
        strHTML.Append("</script>");
        strHTML.Append(strBAR.ToString());
        return strHTML.ToString();
    }

    public string AD_TradeIndex_Scroll(IList<ADInfo> ADs, string sys_Install_Path, string Propertys)
    {
        StringBuilder strHTML = new StringBuilder();
        StringBuilder strIMG = new StringBuilder();
        StringBuilder strBAR = new StringBuilder();
        
        int i = 0;
        foreach (ADInfo entity in ADs)
        {
            if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
            {
                i++;
                if (i == 1)
                {
                    strHTML.Append("<div style=\"display: block;\" onmouseout=\"lwt_out()\" onmouseover=\"lwt(this.id)\"  id=\"flash" + i + "\"><a href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\" style=\"background-image:url(" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + ");\"></a></div>");
                }
                else
                {
                    strHTML.Append("<div style=\"display: none;\" onmouseout=\"lwt_out()\" onmouseover=\"lwt(this.id)\" id=\"flash" + i + "\"><a href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\" style=\"background-image:url(" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + ");\"></a></div>");
                }
                if (i == 5)
                {
                break;
                }
                
                            
            }
        }
            strHTML.Append("<div class=\"flash_bar\">");
        i = 0;
        foreach (ADInfo entity in ADs)
        {
            if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
            {
                i++;
                if (i == 1)
                {
                    strHTML.Append("<div class=\"dq\" id=\"f" + i + "\" onclick=\"changeflash(" + i + ")\">" + i + "</div>");
                }
                else
                {
                    strHTML.Append("<div class=\"no\" id=\"f" + i + "\" onclick=\"changeflash(" + i + ")\">" + i + "</div>");
                }
                if(i==5)
                {
                break;
                }
                
                            
            }
        }
           strHTML.Append("</div>");                     
        strHTML.Append(strBAR.ToString());
        return strHTML.ToString();
    }

    public string AD_Yunjin_Scroll(IList<ADInfo> ADs, string sys_Install_Path, string Propertys)
    {
        StringBuilder strHTML = new StringBuilder();
        StringBuilder strIMG = new StringBuilder();
        StringBuilder strBAR = new StringBuilder();
        strIMG.Append("<div id=\"flash02\">");
        strBAR.Append("<div class=\"flash_bar\">");
        int i = 0;
        foreach (ADInfo entity in ADs)
        {
            if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
            {
                i++;
                if (i == 1)
                {
                    strIMG.Append("<div style=\"display:block;\" id=\"flash" + i + "\" name=\"FFFFFF\"><a style=\"background-image:url(" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + ");background-repeat:no-repeat; background-position:certer;  width:100%;height:352px;margin:0 auto; display:block; background-position:center;\" href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\"  target=\"_blank\"></a></div>");
                    strBAR.Append("<div class=\"dq\" id=\"f" + i + "\" onclick=\"changeflash(" + i + ")\"></div>");
                }
                else
                {
                    strIMG.Append("<div style=\"display:none;\" id=\"flash" + i + "\" name=\"FFFFFF\"><a style=\"background-image:url(" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + ");background-repeat:no-repeat; background-position:certer;  width:100%;height:352px;margin:0 auto; display:block; background-position:center;\" href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\"></a></div>");
                    strBAR.Append("<div class=\"no\" id=\"f" + i + "\" onclick=\"changeflash(" + i + ")\"></div>");
                }
            }
        }
        strIMG.Append("</div>");
        strBAR.Append("</div>");
        strHTML.Append(strIMG.ToString());
        strHTML.Append("<script type=\"text/javascript\">");
        strHTML.Append("var currentindex = 1;");
        strHTML.Append("$('#flash').css('background-color', $('#flash1').attr('name'));");
        strHTML.Append("function changeflash(i) {");
        strHTML.Append("  currentindex = i;");
        strHTML.Append("  for (j = 1; j <= " + i + "; j++) {");
        strHTML.Append("    if (j == i) {");
        strHTML.Append("      $('#flash' + j).fadeIn('normal');");
        strHTML.Append("      $('#flash' + j).css('display', 'block');");
        strHTML.Append("      $('#f' + j).removeClass();");
        strHTML.Append("      $('#f' + j).addClass('dq');");
        strHTML.Append("      $('#flash').css('background-color', $('#flash' + j).attr('name'));");
        strHTML.Append("    }");
        strHTML.Append("    else {");
        strHTML.Append("      $('#flash' + j).css('display', 'none');");
        strHTML.Append("      $('#f' + j).removeClass();");
        strHTML.Append("      $('#f' + j).addClass('no');");
        strHTML.Append("    }");
        strHTML.Append("  }");
        strHTML.Append("}");
        strHTML.Append("function startAm() {");
        strHTML.Append("  timerID = setInterval('timer_tick()', 4000);");//4000代表间隔时间设置
        strHTML.Append("}");
        strHTML.Append("function stopAm() {");
        strHTML.Append("  clearInterval(timerID);");
        strHTML.Append("}");
        strHTML.Append("function timer_tick() {");
        strHTML.Append("  currentindex = currentindex >= " + i + " ? 1 : currentindex + 1;");
        strHTML.Append("  changeflash(currentindex);");
        strHTML.Append("}");
        strHTML.Append("$(document).ready(function () {");
        strHTML.Append("  $('.flash_bar div').mouseover(function () { stopAm(); }).mouseout(function () { startAm(); });");
        strHTML.Append("  startAm();");
        strHTML.Append("});");
        strHTML.Append("</script>");
        strHTML.Append(strBAR.ToString());
        return strHTML.ToString();
    }

    public string AD_ProductList_Scroll(IList<ADInfo> ADs, string sys_Install_Path, string Propertys)
    {
        StringBuilder strHTML = new StringBuilder();
        StringBuilder strIMG = new StringBuilder();
        StringBuilder strBAR = new StringBuilder();

        strIMG.Append("<div id=\"flash\">");
        strBAR.Append("<div class=\"flash_bar\">");
        int i = 0;
        foreach (ADInfo entity in ADs)
        {
            if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
            {
                i++;
                if (i == 1)
                {
                    strIMG.Append("<div style=\"display: block;\" id=\"flash" + i + "\" target=\"_blank\"><a style=\"background-image: url(" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + ");background-repeat: no-repeat;background-position: certer;width: 744px;height: 206px;margin: 0 auto;display: block;background-position: center;\" href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\"></a></div>");
                    strBAR.Append("<div class=\"dq\" id=\"f" + i + "\" onclick=\"changeflash(" + i + ")\"></div>");
                }
                else
                {
                    strIMG.Append("<div style=\"display: none;\" id=\"flash" + i + "\" target=\"_blank\"><a  style=\"background-image: url(" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + ");background-repeat: no-repeat;background-position: certer;width: 744px;height: 206px;margin: 0 auto;display: block;background-position: center;\" href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\"></a></div>");
                    strBAR.Append("<div class=\"no\" id=\"f" + i + "\" onclick=\"changeflash(" + i + ")\"></div>");
                }
            }
        }
        strIMG.Append("</div>");
        strBAR.Append("</div>");

        strHTML.Append(strIMG);
        strHTML.Append("<div class=\"flash_bar2\"></div>");
        strHTML.Append(strBAR);

        strHTML.Append("<script type=\"text/javascript\">");

        strHTML.Append("$(document).ready(function () {");
        strHTML.Append("$(\".item1\").hover(function () { $(\"#tit_fc1\").slideDown(\"normal\"); }, function () { $(\"#tit_fc1\").slideUp(\"fast\"); });");
        strHTML.Append("$(\".item2\").hover(function () { $(\"#tit_fc2\").slideDown(\"normal\"); }, function () { $(\"#tit_fc2\").slideUp(\"fast\"); });");
        strHTML.Append("$(\".item3\").hover(function () { $(\"#tit_fc3\").slideDown(\"normal\"); }, function () { $(\"#tit_fc3\").slideUp(\"fast\"); });");
        strHTML.Append("});");
        strHTML.Append("var currentindex = 1;");
        strHTML.Append("$(\"#flashBg\").css(\"background-color\", $(\"#flash1\").attr(\"name\"));");
        strHTML.Append("function changeflash(i) {");
        strHTML.Append("currentindex = i;");
        strHTML.Append("for (j = 1; j <= "+i+"; j++) {");
        strHTML.Append("if (j == i) {");
        strHTML.Append("$(\"#flash\" + j).fadeIn(\"normal\");");
        strHTML.Append("$(\"#flash\" + j).css(\"display\",\"block\");");
        strHTML.Append("$(\"#f\" + j).removeClass();");
        strHTML.Append("$(\"#f\" + j).addClass(\"dq\");");
        strHTML.Append("$(\"#flashBg\").css(\"background-color\", $(\"#flash\" + j).attr(\"name\"));");
        strHTML.Append("}");
        strHTML.Append("else {");
        strHTML.Append("$(\"#flash\" + j).css(\"display\", \"none\");");
        strHTML.Append("$(\"#f\" + j).removeClass();");
        strHTML.Append(" $(\"#f\" + j).addClass(\"no\");");
        strHTML.Append("}");
        strHTML.Append(" }");
        strHTML.Append("}");
        strHTML.Append("function startAm() {");
        strHTML.Append("timerID = setInterval(\"timer_tick()\", 4000);");
        strHTML.Append("}");
        strHTML.Append("function stopAm() {");
        strHTML.Append(" clearInterval(timerID);");
        strHTML.Append(" }");
        strHTML.Append("function timer_tick() {");
        strHTML.Append("currentindex = currentindex >= "+i+" ? 1 : currentindex + 1;");
        strHTML.Append("changeflash(currentindex);");
        strHTML.Append("}");
        strHTML.Append("$(document).ready(function () {");
        strHTML.Append("$(\".flash_bar div\").mouseover(function () { stopAm(); }).mouseout(function () { startAm(); });");
        strHTML.Append("startAm();");
        strHTML.Append("});");
        strHTML.Append("</script>");
        return strHTML.ToString();
    }

    public string AD_MemberLogin(IList<ADInfo> ADs, string sys_Install_Path, string Propertys)
    {
        StringBuilder strHTML = new StringBuilder();

        foreach (ADInfo entity in ADs)
        {
            if (Propertys == "" || (Propertys != "" && entity.Ad_IsContain == 1 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") >= 0) || (Propertys != "" && entity.Ad_IsContain == 0 && entity.Ad_Propertys.IndexOf("|" + Propertys + "|") < 0))
            {
                strHTML.Append("<div class=\"banner04_main\"><a style=\"width:100%; margin:0 auto; background-image:url(" + pub.FormatImgURL(entity.Ad_Media, "fullpath") + "); background-repeat:no-repeat; background-position:center; height:478px; text-align:left;display: block;\" href=\"" + sys_Install_Path + "?Adv_ID=" + entity.Ad_ID + "\" target=\"_blank\"></a></div>");
            }
        }

        return strHTML.ToString();
    }

}