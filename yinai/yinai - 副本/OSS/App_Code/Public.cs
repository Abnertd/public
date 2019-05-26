using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;


/// <summary>
///Public 的摘要说明
/// </summary>
public class Public
{
    public Public()
    {
    }

    /// <summary>
    /// 信息提示窗口
    /// </summary>
    /// <param name="msgtype">信息类型</param>
    /// <param name="msgtitle">信息头</param>
    /// <param name="msgcontent">信息内容</param>
    /// <param name="autoredirect">自动转向</param>
    /// <param name="autoredirecttime">停留时间</param>
    /// <param name="redirecturl">转向URL</param>
    public static void Msg(string msgtype, string msgtitle, string msgcontent, bool autoredirect, int autoredirecttime, string redirecturl)
    {
        string msgtype_img = "";
        switch (msgtype)
        {
            case "error":
                msgtype_img = "<img src=\"/images/msg-error.gif\">";
                break;
            case "info":
                msgtype_img = "<img src=\"/images/msg-info.gif\">";
                break;
            case "positive":
                msgtype_img = "<img src=\"/images/msg-positive.gif\">";
                break;
        }

        string Mhtml;
        Mhtml = "<head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />";
        Mhtml += "<link rel=\"stylesheet\" href=\"/css/msg.css\" type=\"text/css\">";
        Mhtml += "<title>" + msgtitle + "</title>";
        if (autoredirect)
        {
            Mhtml += "<meta http-equiv=\"refresh\" content=\"" + autoredirecttime + ";URL=" + redirecturl + "\">";
        }

        Mhtml += "</head><body>";
        Mhtml += "<table width=\"100%\" height=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        Mhtml += "  <tr>";
        Mhtml += "    <td align=\"center\" valign=\"middle\"><table width=\"350\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" bgcolor=\"#FFFFFF\">";
        Mhtml += "      <tr><td height=\"30\" class=\"msg_title\">" + msgtitle + "</td></tr>";
        Mhtml += "      <tr>";
        Mhtml += "        <td align=\"left\" class=\"msg_content_border\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
        Mhtml += "          <tr><td height=\"20\" align=\"right\" valign=\"middle\" colspan=\"2\"></td></tr>";
        Mhtml += "          <tr>";
        Mhtml += "            <td width=\"60\" align=\"center\" valign=\"middle\">" + msgtype_img + "</td>";
        Mhtml += "            <td align=\"left\" valign=\"middle\" class=\"msg_content\">" + msgcontent + "</td>";
        Mhtml += "          </tr>";

        if (redirecturl == "{back}")
        {
            Mhtml += "          <tr>";
            Mhtml += "            <td height=\"30\" align=\"right\" valign=\"middle\" colspan=\"2\"><input type=\"button\" name=\"ok\" id=\"btn_ok\" value=\"确定\" class=\"msg_btn\" onclick=\"javascript:history.go(-1);\"></td>";
            Mhtml += "          </tr>";
        }
        else if (redirecturl == "{close}")
        {
            Mhtml += "          <tr>";
            Mhtml += "            <td height=\"30\" align=\"right\" valign=\"middle\" colspan=\"2\"><input type=\"button\" name=\"ok\" id=\"btn_ok\" value=\"确定\" class=\"msg_btn\" onclick=\"javascript:window.open('','_parent','');window.opener=null;window.close();\"></td>";
            Mhtml += "          </tr>";
        }
        else
        {
            Mhtml += "          <tr>";
            Mhtml += "            <td height=\"30\" align=\"right\" valign=\"middle\" colspan=\"2\"><input type=\"button\" name=\"ok\" id=\"btn_ok\" value=\"确定\" class=\"msg_btn\" onclick=\"location.href='" + redirecturl + "';\"></td>";
            Mhtml += "          </tr>";
        }
        Mhtml += "<script type=\"text/javascript\"> ";
        Mhtml += "	function document.onkeydown() { if(event.keyCode==13){document.getElementById(\"btn_ok\").click(); return false;} } ";
        Mhtml += "</script>";
        Mhtml += "        </table></td>";
        Mhtml += "        </tr>";
        Mhtml += "    </table></td>";
        Mhtml += "  </tr>";
        Mhtml += "</table>";
        Mhtml += "</body>";
        Mhtml += "</html>";

        System.Web.HttpContext.Current.Response.Write(Mhtml);
        System.Web.HttpContext.Current.Response.End();
    }

    /// <summary>
    /// 信息提示窗口
    /// </summary>
    /// <param name="msgtype">信息类型</param>
    /// <param name="msgtitle">信息头</param>
    /// <param name="msgcontent">信息内容</param>
    /// <param name="autoredirect">自动转向</param>
    /// <param name="redirecturl">转向URL</param>
    public static void Msg(string msgtype, string msgtitle, string msgcontent, bool autoredirect, string redirecturl)
    {
        Msg(msgtype, msgtitle, msgcontent, autoredirect, 3, redirecturl);
    }

    /// <summary>
    /// ajax提示信息
    /// </summary>
    /// <param name="msgtype">提示类型(error/positive/info)</param>
    /// <param name="msgcontent">提示内容</param>
    /// <returns></returns>
    public static string AjaxTip(string msgtype, string msgcontent)
    {
        string msgtype_img = "";
        string table_class = "";

        switch (msgtype) {
            case "error":
                msgtype_img = "<img src=\"/images/tip-error.gif\" hspace=\"5\" align=\"absmiddle\" />";
                table_class = "tip_bg_error";
                break;
            case "positive":
                msgtype_img = "<img src=\"/images/tip-positive.gif\" hspace=\"5\" align=\"absmiddle\" />";
                table_class = "tip_bg_positive";
                break;
            case "info":
                msgtype_img = "<img src=\"/images/tip-info.png\" hspace=\"5\" align=\"absmiddle\" />";
                table_class = "tip_bg_info";
                break;
        }

        return "<table class=\"tip_table " + table_class + "\"><tr><td>" + msgtype_img + msgcontent + "</td></tr></table>";
    }

    #region "AJAx函数"

    public static string Check_IsBlank(string content)
    {
        string check_result="";
        if (content == "")
        {
            check_result = "<span class=\"tip_bg_error\">信息不可为空！</span>";
        }
        else
        {
            check_result = "<span class=\"tip_bg_positive\">信息输入正确！</span>";
        }
        return check_result;
    }

    #endregion

    /// <summary>
    /// 格式化上传图片地址
    /// </summary>
    /// <param name="urlType"></param>
    /// <param name="urlPath"></param>
    /// <returns></returns>
    public static string FormatImgURL(string urlPath, string urlType) 
    {
        if (urlPath.Length == 0 || urlPath == "/images/detail_no_pic.gif") { return "/images/detail_no_pic.gif"; }

        string fileCompletePath = "";
        try {
            string fileServerURL = System.Web.HttpContext.Current.Application["Upload_Server_URL"].ToString();
            string filePath = urlPath.Substring(0, urlPath.LastIndexOf('/') + 1);
            string fileName = urlPath.Substring(urlPath.LastIndexOf('/') + 1);

            if (fileServerURL.Substring(fileServerURL.Length - 1) == "/") { fileServerURL = fileServerURL.Substring(0, fileServerURL.Length - 1); }

            switch (urlType)
            {
                case "original":
                    fileCompletePath = urlPath;
                    break;
                case "fullpath":
                    fileCompletePath = fileServerURL + urlPath;
                    break;
                case "thumbnail":
                    fileCompletePath = fileServerURL + filePath + "s_" + fileName;
                    break;
            }
        }
        catch (Exception ex) { 
            fileCompletePath = urlPath; 
        }
        return fileCompletePath;
    }

    public static string CheckedRadio(string chk1, string chk2) { 
        return (chk1 == chk2) ? "checked=\"checked\"" : "" ;
    }

    public static string CheckedSelected(string chk1, string chk2)
    {
        return (chk1 == chk2) ? "selected=\"selected\"" : "";
    }

    public static void CheckLogin(string PrivilegeCode)
    {
        object UserLogin = System.Web.HttpContext.Current.Session["UserLogin"];
        if (UserLogin == null || UserLogin.ToString() != "true")
        {
            System.Web.HttpContext.Current.Response.Write("<script type\"text/javascript\">");
            //System.Web.HttpContext.Current.Response.Write("alert('登陆超时或未登录！');");
            System.Web.HttpContext.Current.Response.Write("parent.location.href='/login.aspx?tip=nologin&time='+ new Date().getTime();");
            System.Web.HttpContext.Current.Response.Write("</script>");
            System.Web.HttpContext.Current.Response.End();
        }
        else
        {
            if (PrivilegeCode == "all")
                return;

            if (!CheckPrivilege(PrivilegeCode))
            {
                PrivilegeCode = "没有权限，" + PrivilegeCode + "错误";
                System.Web.HttpContext.Current.Response.Redirect("/unpower.aspx?tip=" + PrivilegeCode);
            }
        }
    }

    /// <summary>
    /// 判断是否有权限
    /// </summary>
    /// <param name="PrivilegeCode">功能所需权限</param>
    /// <returns></returns>
    public static bool CheckPrivilege(string PrivilegeCode)
    {
        try
        {
            if (PrivilegeCode == null || PrivilegeCode.Length == 0)
                return false;

            Glaer.Trade.B2C.RBAC.IRBAC rbac = Glaer.Trade.B2C.RBAC.RBACFactory.CreateRBAC();
            string[] codeArray = System.Text.RegularExpressions.Regex.Split(PrivilegeCode, "/");

            Glaer.Trade.B2C.Model.RBACUserInfo UserPrivilege = (Glaer.Trade.B2C.Model.RBACUserInfo)System.Web.HttpContext.Current.Session["UserPrivilege"];

            foreach (string p in codeArray)
            {
                if (rbac.CheckPrivilege(UserPrivilege, p))
                {
                    return true;
                }
            }
            return false;
        }
        catch {
            return false;
        }
    }

    public static Glaer.Trade.B2C.Model.RBACUserInfo GetUserPrivilege()
    {
        try
        {
            return (Glaer.Trade.B2C.Model.RBACUserInfo)System.Web.HttpContext.Current.Session["UserPrivilege"];
        }
        catch {
            return new Glaer.Trade.B2C.Model.RBACUserInfo();
        }
    }

    public static string DisplayCurrency(double InVal) 
    {
        try { return "￥" + InVal.ToString("0.00"); }
        catch { return "￥0.00"; }
    }

    public static string DisplaySex(int InVal)
    {
        if (InVal == 0) { return "男"; }
        else if (InVal == 1) { return "女"; }
        else { return "--"; }
    }

    public static string CreatevkeyNum(int length)
    {
        string strSource = "0,1,2,3,4,5,6,7,8,9";
        string[] strArray = strSource.Split(',');

        string strKey = "";
        Random ran = new Random();
        for (int i = 0; i < length; i++) { strKey += strArray[ran.Next(9)]; }
        ran = null;

        return strKey;
    }

    /// <summary>
    /// 生成随机码
    /// </summary>
    /// <returns></returns>
    public static string Createvkey(int length)
    {
        string strSource = "0,1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z";
        string[] strArray = strSource.Split(',');

        string strKey = "";
        Random ran = new Random();
        for (int i = 0; i < length; i++) { strKey += strArray[ran.Next(62)]; }
        ran = null;

        return strKey;
    }

    public static DateTime ChangeToDate(string InVal)
    {
        
        if (InVal == null)
            return DateTime.Now;

        try { return Convert.ToDateTime(InVal); }
        catch (Exception ex) { throw ex; return DateTime.Now; }
    }

    public static string GetKuaiDiCom(string typeCom)
    {
        switch (typeCom)
        {
            case "AAE全球专递":
                typeCom = "aae";
                break;
            case "安捷快递":
                typeCom = "anjiekuaidi";
                break;
            case "安信达快递":
                typeCom = "anxindakuaixi";
                break;
            case "百福东方":
                typeCom = "baifudongfang";
                break;
            case "彪记快递":
                typeCom = "biaojikuaidi";
                break;
            case "BHT":
                typeCom = "bht";
                break;
            case "希伊艾斯快递":
                typeCom = "cces";
                break;
            case "中国东方":
                typeCom = "Coe";
                break;
            case "长宇物流":
                typeCom = "changyuwuliu";
                break;
            case "大田物流":
                typeCom = "datianwuliu";
                break;
            case "德邦物流":
                typeCom = "debangwuliu";
                break;
            case "DPEX":
                typeCom = "dpex";
                break;
            case "DHL":
                typeCom = "dhl";
                break;
            case "D速快递":
                typeCom = "dsukuaidi";
                break;
            case "fedex":
                typeCom = "fedex";
                break;
            case "飞康达物流":
                typeCom = "feikangda";
                break;
            case "凤凰快递":
                typeCom = "fenghuangkuaidi";
                break;
            case "港中能达物流":
                typeCom = "ganzhongnengda";
                break;
            case "广东邮政物流":
                typeCom = "guangdongyouzhengwuliu";
                break;
            case "汇通快运":
                typeCom = "huitongkuaidi";
                break;
            case "恒路物流":
                typeCom = "hengluwuliu";
                break;
            case "华夏龙物流":
                typeCom = "huaxialongwuliu";
                break;
            case "佳怡物流":
                typeCom = "jiayiwuliu";
                break;
            case "京广速递":
                typeCom = "jinguangsudikuaijian";
                break;
            case "急先达":
                typeCom = "jixianda";
                break;
            case "加运美":
                typeCom = "jiayunmeiwuliu";
                break;
            case "快捷速递":
                typeCom = "kuaijiesudi";
                break;
            case "联昊通物流":
                typeCom = "lianhaowuliu";
                break;
            case "龙邦物流":
                typeCom = "longbanwuliu";
                break;
            case "民航快递":
                typeCom = "minghangkuaidi";
                break;
            case "配思货运":
                typeCom = "peisihuoyunkuaidi";
                break;
            case "全晨快递":
                typeCom = "quanchenkuaidi";
                break;
            case "全际通物流":
                typeCom = "quanjitong";
                break;
            case "全日通快递":
                typeCom = "quanritongkuaidi";
                break;
            case "全一快递":
                typeCom = "quanyikuaidi";
                break;
            case "盛辉物流":
                typeCom = "shenghuiwuliu";
                break;
            case "速尔物流":
                typeCom = "suer";
                break;
            case "盛丰物流":
                typeCom = "shengfengwuliu";
                break;
            case "天地华宇":
                typeCom = "tiandihuayu";
                break;
            case "天天快递":
                typeCom = "tiantian";
                break;
            case "TNT":
                typeCom = "tnt";
                break;
            case "UPS":
                typeCom = "ups";
                break;
            case "万家物流":
                typeCom = "wanjiawuliu";
                break;
            case "文捷航空速递":
                typeCom = "wenjiesudi";
                break;
            case "伍圆速递":
                typeCom = "wuyuansudi";
                break;
            case "万象物流":
                typeCom = "wanxiangwuliu";
                break;
            case "新邦物流":
                typeCom = "xinbangwuliu";
                break;
            case "信丰物流":
                typeCom = "xinfengwuliu";
                break;
            case "星晨急便":
                typeCom = "xingchengjibian";
                break;
            case "鑫飞鸿物流":
                typeCom = "xinhongyukuaidi";
                break;
            case "亚风速递":
                typeCom = "yafengsudi";
                break;
            case "一邦速递":
                typeCom = "yibangwuliu";
                break;
            case "优速物流":
                typeCom = "youshuwuliu";
                break;
            case "远成物流":
                typeCom = "yuanchengwuliu";
                break;
            case "圆通速递":
                typeCom = "yuantong";
                break;
            case "源伟丰快递":
                typeCom = "yuanweifeng";
                break;
            case "元智捷诚快递":
                typeCom = "yuanzhijiecheng";
                break;
            case "越丰物流":
                typeCom = "yuefengwuliu";
                break;
            case "韵达快递":
                typeCom = "yunda";
                break;
            case "源安达":
                typeCom = "yuananda";
                break;
            case "运通快递":
                typeCom = "yuntongkuaidi";
                break;
            case "宅急送":
                typeCom = "zhaijisong";
                break;
            case "中铁快运":
                typeCom = "zhongtiewuliu";
                break;
            case "中通速递":
                typeCom = "zhongtong";
                break;
            case "中邮物流":
                typeCom = "zhongyouwuliu";
                break;
            case "申通":
                typeCom = "shentong";
                break;
            case "汇通":
                typeCom = "huitongkuaidi";
                break;
            case "联邦快递":
                typeCom = "fedex";
                break;
            case "顺丰速运":
                typeCom = "shunfeng";
                break;
            case "EMS特快专递":
                typeCom = "ems";
                break;
        }
        return typeCom;
    }


    #region 取中文首字母

    /// <summary>
    /// 得到汉字首字母
    /// </summary>
    /// <param name="paramChinese"></param>
    /// <returns></returns>
    public static string GetFirstLetter(string paramChinese)
    {
        string strTemp = "";
        int iLen = paramChinese.Length;
        int i = 0;

        for (i = 0; i <= iLen - 1; i++)
        {
            strTemp += GetCharSpellCode(paramChinese.Substring(i, 1));
        }

        return strTemp;

    }

    /// <summary>    
    /// 得到一个汉字的拼音第一个字母，如果是一个英文字母则直接返回大写字母    
    /// </summary>    
    /// <param name="CnChar">单个汉字</param>    
    /// <returns>单个大写字母</returns>    
    private static string GetCharSpellCode(string paramChar)
    {
        long iCnChar;

        byte[] ZW = System.Text.Encoding.Default.GetBytes(paramChar);

        //如果是字母，则直接返回    
        if (ZW.Length == 1)
        {
            return paramChar.ToUpper();
        }
        else
        {
            // get the array of byte from the single char    
            int i1 = (short)(ZW[0]);
            int i2 = (short)(ZW[1]);
            iCnChar = i1 * 256 + i2;
        }

        //expresstion    
        //table of the constant list    
        // 'A'; //45217..45252    
        // 'B'; //45253..45760    
        // 'C'; //45761..46317    
        // 'D'; //46318..46825    
        // 'E'; //46826..47009    
        // 'F'; //47010..47296    
        // 'G'; //47297..47613    

        // 'H'; //47614..48118    
        // 'J'; //48119..49061    
        // 'K'; //49062..49323    
        // 'L'; //49324..49895    
        // 'M'; //49896..50370    
        // 'N'; //50371..50613    
        // 'O'; //50614..50621    
        // 'P'; //50622..50905    
        // 'Q'; //50906..51386    

        // 'R'; //51387..51445    
        // 'S'; //51446..52217    
        // 'T'; //52218..52697    
        //没有U,V    
        // 'W'; //52698..52979    
        // 'X'; //52980..53640    
        // 'Y'; //53689..54480    
        // 'Z'; //54481..55289    

        // iCnChar match the constant    
        if ((iCnChar >= 45217) && (iCnChar <= 45252))
        {
            return "A";
        }
        else if ((iCnChar >= 45253) && (iCnChar <= 45760))
        {
            return "B";
        }
        else if ((iCnChar >= 45761) && (iCnChar <= 46317))
        {
            return "C";
        }
        else if ((iCnChar >= 46318) && (iCnChar <= 46825))
        {
            return "D";
        }
        else if ((iCnChar >= 46826) && (iCnChar <= 47009))
        {
            return "E";
        }
        else if ((iCnChar >= 47010) && (iCnChar <= 47296))
        {
            return "F";
        }
        else if ((iCnChar >= 47297) && (iCnChar <= 47613))
        {
            return "G";
        }
        else if ((iCnChar >= 47614) && (iCnChar <= 48118))
        {
            return "H";
        }
        else if ((iCnChar >= 48119) && (iCnChar <= 49061))
        {
            return "J";
        }
        else if ((iCnChar >= 49062) && (iCnChar <= 49323))
        {
            return "K";
        }
        else if ((iCnChar >= 49324) && (iCnChar <= 49895))
        {
            return "L";
        }
        else if ((iCnChar >= 49896) && (iCnChar <= 50370))
        {
            return "M";
        }

        else if ((iCnChar >= 50371) && (iCnChar <= 50613))
        {
            return "N";
        }
        else if ((iCnChar >= 50614) && (iCnChar <= 50621))
        {
            return "O";
        }
        else if ((iCnChar >= 50622) && (iCnChar <= 50905))
        {
            return "P";
        }
        else if ((iCnChar >= 50906) && (iCnChar <= 51386))
        {
            return "Q";
        }
        else if ((iCnChar >= 51387) && (iCnChar <= 51445))
        {
            return "R";
        }
        else if ((iCnChar >= 51446) && (iCnChar <= 52217))
        {
            return "S";
        }
        else if ((iCnChar >= 52218) && (iCnChar <= 52697))
        {
            return "T";
        }
        else if ((iCnChar >= 52698) && (iCnChar <= 52979))
        {
            return "W";
        }
        else if ((iCnChar >= 52980) && (iCnChar <= 53688))
        {
            return "X";
        }
        else if ((iCnChar >= 53689) && (iCnChar <= 54480))
        {
            return "Y";
        }
        else if ((iCnChar >= 54481) && (iCnChar <= 55289))
        {
            return "Z";
        }
        else return ("?");
    }

    /// <summary>
    /// 取第一个字的首字母
    /// </summary>
    /// <param name="paramChinese"></param>
    /// <returns></returns>
    public static string GetFirstWordLetter(string paramChinese)
    {
        if (paramChinese != null && paramChinese.Length > 0)
        {
            return GetCharSpellCode(paramChinese.Substring(0, 1));
        }
        else
        {
            return paramChinese;
        }
    }

    #endregion 

    #region 导出Excel

    public static void toExcel(DataTable td)
    {
        System.Web.UI.WebControls.DataGrid dgrid = null;
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        System.IO.StringWriter strOur = null;
        System.Web.UI.HtmlTextWriter htmlWriter = null;

        if (td == null) { return; }

        context.Response.Write(htmlWriter);
        context.Response.ContentType = "application/vnd.ms-excel";
        context.Response.ContentEncoding = System.Text.Encoding.UTF8;
        context.Response.Charset = "";

        context.Response.AddHeader("Content-Disposition", "attachment;filename="+ DateTime.Now.ToString("yyyyMMddHHmmss") +".xls");

        strOur = new System.IO.StringWriter();
        htmlWriter = new System.Web.UI.HtmlTextWriter(strOur);
        dgrid = new DataGrid();
        dgrid.DataSource = td.DefaultView;
        dgrid.AllowPaging = false;
        dgrid.DataBind();

        dgrid.RenderControl(htmlWriter);
        context.Response.Write("<head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /><style>td{vnd.ms-excel.numberformat:@}</style></head>");
        context.Response.Write(strOur.ToString());
        context.Response.End();
    }

    #endregion

    /// <summary>
    /// 获得当前站点标识
    /// </summary>
    /// <returns></returns>
    public static string GetCurrentSite()
    {
        if (HttpContext.Current.Session["CurrentSite"] != null)
            return HttpContext.Current.Session["CurrentSite"].ToString();
        else
            return "CN";
    }

    //json字符串转换
    public static string JsonStr(string Input_Str)
    {
        string List_Str;
        List_Str = Input_Str;
        //List_Str = List_Str.Replace("!", "！");
        List_Str = List_Str.Replace("'", "&acute;");
        List_Str = List_Str.Replace("\"", "&quot;");
        List_Str = List_Str.Replace("\\", "＼");
        List_Str = List_Str.Replace(",", "&sbquo;");
        List_Str = List_Str.Replace("\r\n", " ");
        return List_Str;
    }

    //选项卡
    public static string Page_Option(string Page_Url, string Opt_Name)
    {
        return "<div class=\"left\"></div><div class=\"center\" onclick=\"location='" + Page_Url + "';\">" + Opt_Name + "</div><div class=\"right\"></div>";
    }

    //脚本选项卡
    public static string Page_ScriptOption(string Action_Script, string Opt_Name)
    {
        return "<div class=\"left\"></div><div class=\"center\" onclick=\"" + Action_Script + "\">" + Opt_Name + "</div><div class=\"right\"></div>";
    }

    public static string page(int pagecount, int currentpage, string pageurl, int pagesize, int recordcount)
    {
	    int ipage = 0;
        string Page_Str = "";
	    Page_Str+="<table border='0' cellspacing='1' cellpadding='0' height='26'>";
	    Page_Str+="<tr align='center' valign='middle'>";
	    Page_Str+="<td align='center'>每页<span style=\"color:#FF0000;\">" + pagesize + "</span>条记录，共<span style=\"color:#FF0000;\">" + recordcount + "</span>条记录&nbsp;&nbsp;</td>";
	    Page_Str+="<td class=";
	    if (currentpage <= 1) {
		    Page_Str+="page_off";
		    Page_Str+=">";
		    Page_Str+="&#171; 上一页";
	    } else {
		    Page_Str+="page_on";
		    Page_Str+=">";
		    Page_Str+="<a href='" + pageurl + "&page=" + (currentpage - 1) + "' class='page_on_t'>";
		    Page_Str+="&#171; 上一页";
		    Page_Str+="</a>";
	    }
	    Page_Str+="</td>";
	    if (pagecount <= 12) {
		    for (ipage = 1; ipage <= pagecount; ipage++) {
			    Page_Str+="<td class=";
			    if (currentpage == ipage) {
				    Page_Str+="page_current";
			    } else {
				    Page_Str+="page_num";
			    }
			    Page_Str+=">";
			    if (currentpage == ipage) {
				    Page_Str+=ipage;
			    } else {
				    Page_Str+="<a href='" + pageurl + "&page=" + ipage + "' class='page_num_t'>" + ipage + "</a>";
			    }
			    Page_Str+="</td>";
		    }
	    } else if (pagecount > 12 & pagecount < 16) {
		    if (currentpage < 9) {
			    for (ipage = 1; ipage <= 10; ipage++) {
				    Page_Str+="<td class=";
				    if (currentpage == ipage) {
					    Page_Str+="page_current";
				    } else {
					    Page_Str+="page_num";
				    }
				    Page_Str+=">";
				    if (currentpage == ipage) {
					    Page_Str+=ipage;
				    } else {
					    Page_Str+="<a href='" + pageurl + "&page=" + ipage + "' class='page_num_t'>" + ipage + "</a>";
				    }
				    Page_Str+="</td>";
			    }
			    Page_Str+="<td class=page_omit>";
			    Page_Str+="&#8230;";
			    Page_Str+="</td>";
			    for (ipage = pagecount - 1; ipage <= pagecount; ipage++) {
				    Page_Str+="<td class=";
				    if (currentpage == ipage) {
					    Page_Str+="page_current";
				    } else {
					    Page_Str+="page_num";
				    }
				    Page_Str+=">";
				    if (currentpage == ipage) {
					    Page_Str+=ipage;
				    } else {
					    Page_Str+="<a href='" + pageurl + "&page=" + ipage + "' class='page_num_t'>" + ipage + "</a>";
				    }
				    Page_Str+="</td>";
			    }
		    } else {
			    for (ipage = 1; ipage <= 2; ipage++) {
				    Page_Str+="<td class=";
				    if (currentpage == ipage) {
					    Page_Str+="page_current";
				    } else {
					    Page_Str+="page_num";
				    }
				    Page_Str+=">";
				    if (currentpage == ipage) {
					    Page_Str+=ipage;
				    } else {
					    Page_Str+="<a href='" + pageurl + "&page=" + ipage + "' class='page_num_t'>" + ipage + "</a>";
				    }
				    Page_Str+="</td>";
			    }
			    Page_Str+="<td class=page_omit>";
			    Page_Str+="&#8230;";
			    Page_Str+="</td>";
			    for (ipage = pagecount - 9; ipage <= pagecount; ipage++) {
				    Page_Str+="<td class=";
				    if (currentpage == ipage) {
					    Page_Str+="page_current";
				    } else {
					    Page_Str+="page_num";
				    }
				    Page_Str+=">";
				    if (currentpage == ipage) {
					    Page_Str+=ipage;
				    } else {
					    Page_Str+="<a href='" + pageurl + "&page=" + ipage + "' class='page_num_t'>" + ipage + "</a>";
				    }
				    Page_Str+="</td>";
			    }
		    }
	    } else if (pagecount >= 16) {
		    if (currentpage < 9) {
			    for (ipage = 1; ipage <= 10; ipage++) {
				    Page_Str+="<td class=";
				    if (currentpage == ipage) {
					    Page_Str+="page_current";
				    } else {
					    Page_Str+="page_num";
				    }
				    Page_Str+=">";
				    if (currentpage == ipage) {
					    Page_Str+=ipage;
				    } else {
					    Page_Str+="<a href='" + pageurl + "&page=" + ipage + "' class='page_num_t'>" + ipage + "</a>";
				    }
				    Page_Str+="</td>";
			    }
			    Page_Str+="<td class=page_omit>";
			    Page_Str+="&#8230;";
			    Page_Str+="</td>";
			    for (ipage = pagecount - 1; ipage <= pagecount; ipage++) {
				    Page_Str+="<td class=";
				    if (currentpage == ipage) {
					    Page_Str+="page_current";
				    } else {
					    Page_Str+="page_num";
				    }
				    Page_Str+=">";
				    if (currentpage == ipage) {
					    Page_Str+=ipage;
				    } else {
					    Page_Str+="<a href='" + pageurl + "&page=" + ipage + "' class='page_num_t'>" + ipage + "</a>";
				    }
				    Page_Str+="</td>";
			    }
		    } else if (currentpage + 7 > pagecount) {
			    for (ipage = 1; ipage <= 2; ipage++) {
				    Page_Str+="<td class=";
				    if (currentpage == ipage) {
					    Page_Str+="page_current";
				    } else {
					    Page_Str+="page_num";
				    }
				    Page_Str+=">";
				    if (currentpage == ipage) {
					    Page_Str+=ipage;
				    } else {
					    Page_Str+="<a href='" + pageurl + "&page=" + ipage + "' class='page_num_t'>" + ipage + "</a>";
				    }
				    Page_Str+="</td>";
			    }
			    Page_Str+="<td class=page_omit>";
			    Page_Str+="&#8230;";
			    Page_Str+="</td>";
			    for (ipage = pagecount - 9; ipage <= pagecount; ipage++) {
				    Page_Str+="<td class=";
				    if (currentpage == ipage) {
					    Page_Str+="page_current";
				    } else {
					    Page_Str+="page_num";
				    }
				    Page_Str+=">";
				    if (currentpage == ipage) {
					    Page_Str+=ipage;
				    } else {
					    Page_Str+="<a href='" + pageurl + "&page=" + ipage + "' class='page_num_t'>" + ipage + "</a>";
				    }
				    Page_Str+="</td>";
			    }
		    } else {
			    for (ipage = 1; ipage <= 2; ipage++) {
				    Page_Str+="<td class=";
				    if (currentpage == ipage) {
					    Page_Str+="page_current";
				    } else {
					    Page_Str+="page_num";
				    }
				    Page_Str+=">";
				    if (currentpage == ipage) {
					    Page_Str+=ipage;
				    } else {
					    Page_Str+="<a href='" + pageurl + "&page=" + ipage + "' class='page_num_t'>" + ipage + "</a>";
				    }
				    Page_Str+="</td>";
			    }
			    Page_Str+="<td class=page_omit>";
			    Page_Str+="&#8230;";
			    Page_Str+="</td>";
			    for (ipage = currentpage - 5; ipage <= currentpage + 4; ipage++) {
				    Page_Str+="<td class=";
				    if (currentpage == ipage) {
					    Page_Str+="page_current";
				    } else {
					    Page_Str+="page_num";
				    }
				    Page_Str+=">";
				    if (currentpage == ipage) {
					    Page_Str+=ipage;
				    } else {
					    Page_Str+="<a href='" + pageurl + "&page=" + ipage + "' class='page_num_t'>" + ipage + "</a>";
				    }
				    Page_Str+="</td>";
			    }
			    Page_Str+="<td class=page_omit>";
			    Page_Str+="&#8230;";
			    Page_Str+="</td>";
			    for (ipage = pagecount - 1; ipage <= pagecount; ipage++) {
				    Page_Str+="<td class=";
				    if (currentpage == ipage) {
					    Page_Str+="page_current";
				    } else {
					    Page_Str+="page_num";
				    }
				    Page_Str+=">";
				    if (currentpage == ipage) {
					    Page_Str+=ipage;
				    } else {
					    Page_Str+="<a href='" + pageurl + "&page=" + ipage + "' class='page_num_t'>" + ipage + "</a>";
				    }
				    Page_Str+="</td>";
			    }
		    }
	    }
	    Page_Str+="<td class=";
	    if (currentpage == pagecount) {
		    Page_Str+="page_off";
		    Page_Str+=">";
		    Page_Str+="下一页 &#187;";
	    } else {
		    Page_Str+="page_on";
		    Page_Str+=">";
		    Page_Str+="<a href='" + pageurl + "&page=" + (currentpage + 1) + "' class='page_on_t'>";
		    Page_Str+="下一页 &#187;";
		    Page_Str+="</a>";
	    }
	    Page_Str+="</td>";
	    Page_Str+="</tr>";
	    Page_Str+="</table>";
        return Page_Str;
    }




    /// <summary>
    /// 分页（div）
    /// </summary>
    /// <param name="pagecount"></param>
    /// <param name="currentpage"></param>
    /// <param name="pageurl"></param>
    /// <param name="pagesize"></param>
    /// <param name="recordcount"></param>
    public static string Page1(int pagecount, int currentpage, string pageurl, int pagesize, int recordcount)
    {
        int ipage = 0;

        StringBuilder strHTML = new StringBuilder();
        strHTML.Append("<div class=\"page\">");
        strHTML.Append("<span>第<strong> " + currentpage + " </strong>页</span> <span> 共<strong> " + pagecount + " </strong>页</span>");

        if (currentpage <= 1)
        {
            strHTML.Append("<a>上一页</a>");
        }
        else
        {
            strHTML.Append("<a href='" + pageurl + "&page=" + (currentpage - 1).ToString() + "'>");
            strHTML.Append("上一页");
            strHTML.Append("</a>");
        }
        if (pagecount <= 6)
        {
            for (ipage = 1; ipage <= pagecount; ipage++)
            {

                if (currentpage == ipage)
                {
                    strHTML.Append("<a href='" + pageurl + "&page=" + ipage + "' class='on'>" + ipage + "</a>");
                }
                else
                {
                    strHTML.Append("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                }
            }
        }

        else if (pagecount > 6)
        {
            if (currentpage < 4)
            {
                for (ipage = 1; ipage <= 6; ipage++)
                {

                    if (currentpage == ipage)
                    {
                        strHTML.Append("<a href='" + pageurl + "&page=" + ipage + "' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        strHTML.Append("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }
            }
            else
            {

                int upper = pagecount;
                if (pagecount >= (currentpage + 2)) upper = (currentpage + 2);

                for (ipage = currentpage - 3; ipage <= upper; ipage++)
                {
                    if (currentpage == ipage)
                    {
                        strHTML.Append("<a href='" + pageurl + "&page=" + ipage + "' class='on'>" + ipage + "</a>");
                    }
                    else
                    {
                        strHTML.Append("<a href='" + pageurl + "&page=" + ipage + "'>" + ipage + "</a>");
                    }
                }

            }
        }
        if (currentpage == pagecount)
        {
            strHTML.Append("<a>下一页</a>");
        }
        else
        {
            strHTML.Append("<a href='" + pageurl + "&page=" + (currentpage + 1).ToString() + "' class='on'>");
            strHTML.Append("下一页");
            strHTML.Append("</a>");
        }
        strHTML.Append("</div>");
        return strHTML.ToString();
    }
    //检查手机号或固定电话
    public static bool Checkmobile(string check_str)
    {
        bool result = true;
        if (check_str.Length < 11)
        {
            result = false;
        }
        if (result)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"(^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$)|(^((\(\d{3}\))|(\d{3}\-))?(1[358]\d{9})$)");
            result = regex.IsMatch(check_str);
        }
        return result;
    }



    /// <summary>
    /// 获取即时金价
    /// </summary>
    /// <returns></returns>
    public static double GetGoldPrice()
    {
        double goldPrice = 306;

        return goldPrice;
    }

    /// <summary>
    /// 获取即时银价
    /// </summary>
    /// <returns></returns>
    public static double GetSilverPrice()
    {
        double SilverPrice = 0;

        return SilverPrice;
    }

    /// <summary>
    /// 获取即时铂金价格
    /// </summary>
    /// <returns></returns>
    public static double GetPlatinumPrice()
    {
        double price = 0;

        return price;
    }

    public static double GetProductPrice(double Product_ManualFee, double Product_Weight)
    {
        double product_price = 0;

        product_price = (GetGoldPrice() + Product_ManualFee) * Product_Weight;

        return product_price;
    }



  

}

