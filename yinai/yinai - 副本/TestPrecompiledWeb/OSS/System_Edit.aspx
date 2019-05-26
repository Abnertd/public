<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    
    private Config myApp;
    private ITools tools;

    private string Upload_Server_URL, Upload_Server_Return_WWW, Upload_Server_Return_Admin, Language_Define, Site_DomainName, Site_Name, Site_URL, Site_Logo, Site_Tel, Site_Fax,
        Site_Email, Site_Keyword, Site_Description, Site_Title, Mail_Server, Mail_ServerUserName, Mail_ServerPassWord, Mail_FromName, Mail_From, Mail_Replyto,
        Mail_Encode, Coin_Name, Sys_Config_Site, Chinabank_Code, Chinabank_Key, Alipay_Email, Alipay_Code, Alipay_Key, Sys_Delivery_Code, Upload_Server_Return_Supplier, Tenpay_Code, Tenpay_Key, CreditPayment_Code, Sys_Sensitive_Keyword, Shop_Second_Domain;
    private int Sys_Config_ID, Mail_ServerPort, Mail_EnableSsl, Coin_Rate, Trade_Contract_IsActive, Static_IsEnable, RepidBuy_IsEnable, Coupon_UsedAmount;
    private double Instant_SilverPrice, Instant_GoldPrice;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("ef84a07f-6281-4f54-84f9-c345adf9d765");

        myApp = new Config();
        tools = ToolsFactory.CreateTools();

        int site_id = tools.CheckInt(Request.QueryString["site_id"]);

        ConfigInfo entity = myApp.GetConfigByID(site_id);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else
        {
            Sys_Config_ID = entity.Sys_Config_ID;
            Upload_Server_URL = entity.Upload_Server_URL;
            Upload_Server_Return_WWW = entity.Upload_Server_Return_WWW;
            Upload_Server_Return_Admin = entity.Upload_Server_Return_Admin;
            Upload_Server_Return_Supplier = entity.Upload_Server_Return_Supplier;
            Language_Define = entity.Language_Define;
            Site_DomainName = entity.Site_DomainName;
            Shop_Second_Domain = entity.Shop_Second_Domain;
            Site_Name = entity.Site_Name;
            Site_URL = entity.Site_URL;
            Site_Logo = entity.Site_Logo;
            Site_Tel = entity.Site_Tel;
            Site_Fax = entity.Site_Fax;
            Site_Email = entity.Site_Email;
            Site_Keyword = entity.Site_Keyword;
            Site_Description = entity.Site_Description;
            Site_Title = entity.Site_Title;
            Mail_Server = entity.Mail_Server;
            Mail_ServerPort = entity.Mail_ServerPort;
            Mail_EnableSsl = entity.Mail_EnableSsl;
            Mail_ServerUserName = entity.Mail_ServerUserName;
            Mail_ServerPassWord = entity.Mail_ServerPassWord;
            Mail_FromName = entity.Mail_FromName;
            Mail_From = entity.Mail_From;
            Mail_Replyto = entity.Mail_Replyto;
            Mail_Encode = entity.Mail_Encode;
            Coin_Name = entity.Coin_Name;
            Coin_Rate = entity.Coin_Rate;
            Trade_Contract_IsActive = entity.Trade_Contract_IsActive;
            Sys_Config_Site = entity.Sys_Config_Site;
            Static_IsEnable = entity.Static_IsEnable;
            Chinabank_Code = entity.Chinabank_Code;
            Chinabank_Key = entity.Chinabank_Key;
            Alipay_Email = entity.Alipay_Email;
            Alipay_Code = entity.Alipay_Code;
            Alipay_Key = entity.Alipay_Key;
            Tenpay_Code = entity.Tenpay_Code;
            Tenpay_Key = entity.Tenpay_Key;
            CreditPayment_Code = entity.CreditPayment_Code;
            RepidBuy_IsEnable = entity.RepidBuy_IsEnable;
            Coupon_UsedAmount = entity.Coupon_UsedAmount;
            Sys_Delivery_Code = entity.Sys_Delivery_Code;
            Sys_Sensitive_Keyword = entity.Sys_Sensitive_Keyword;
            Instant_SilverPrice = entity.Instant_SilverPrice;
            Instant_GoldPrice = entity.Instant_GoldPrice;
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>无标题文档</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">系统管理</td>
            </tr>
            <tr>
                <td class="content_content">
                    <form id="formadd" name="formadd" method="post" action="system_do.aspx">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
                            <tr>
                                <td class="cell_title">文件服务器地址</td>
                                <td class="cell_content">
                                    <input name="Upload_Server_URL" type="text" style="width: 300px;" maxlength="100" value="<% =Upload_Server_URL%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">前台上传返回地址</td>
                                <td class="cell_content">
                                    <input name="Upload_Server_Return_WWW" type="text" style="width: 300px;" maxlength="100" value="<% =Upload_Server_Return_WWW%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">OSS上传返回地址</td>
                                <td class="cell_content">
                                    <input name="Upload_Server_Return_Admin" type="text" style="width: 300px;" maxlength="100" value="<% =Upload_Server_Return_Admin%>" /></td>
                            </tr>
                            <%--<tr>
          <td class="cell_title">供应商上传返回地址</td>
          <td class="cell_content"><input name="Upload_Server_Return_Supplier" type="text" style="width:300px;" maxlength="100" value="<% =Upload_Server_Return_Supplier%>" /></td>
        </tr>--%>
                            <tr>
                                <td class="cell_title">站点名称</td>
                                <td class="cell_content">
                                    <input name="Site_Name" type="text" style="width: 300px;" maxlength="50" value="<% =Site_Name%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">站点域名</td>
                                <td class="cell_content">
                                    <input name="Site_DomainName" type="text" style="width: 300px;" maxlength="50" value="<% =Site_DomainName%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">店铺顶级域名</td>
                                <td class="cell_content">
                                    <input name="Shop_Second_Domain" type="text" style="width: 300px;" maxlength="50" value="<% =Shop_Second_Domain%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">站点URl</td>
                                <td class="cell_content">
                                    <input name="Site_URL" type="text" style="width: 300px;" maxlength="50" value="<% =Site_URL%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">电话</td>
                                <td class="cell_content">
                                    <input name="Site_Tel" type="text" style="width: 300px;" maxlength="50" value="<% =Site_Tel%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">传真</td>
                                <td class="cell_content">
                                    <input name="Site_Fax" type="text" style="width: 300px;" maxlength="50" value="<% =Site_Fax%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">Email</td>
                                <td class="cell_content">
                                    <input name="Site_Email" type="text" style="width: 300px;" maxlength="50" value="<% =Site_Email%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">关键词</td>
                                <td class="cell_content">
                                    <textarea cols="60" rows="6" name="Site_Keyword"><% =Site_Keyword%></textarea></td>
                            </tr>
                            <tr>
                                <td class="cell_title">描述</td>
                                <td class="cell_content">
                                    <textarea cols="60" rows="6" name="Site_Description"><% =Site_Description%></textarea></td>
                            </tr>
                            <tr>
                                <td class="cell_title">标题栏文字</td>
                                <td class="cell_content">
                                    <input name="Site_Title" type="text" id="Site_Title" style="width: 300px;" value="<% =Site_Title%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">邮件服务器地址</td>
                                <td class="cell_content">
                                    <input name="Mail_Server" type="text" id="Mail_Server" style="width: 300px;" value="<% =Mail_Server%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">邮件服务器端口</td>
                                <td class="cell_content">
                                    <input name="Mail_ServerPort" type="text" id="Mail_ServerPort" style="width: 300px;" value="<% =Mail_ServerPort%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">使用SSL</td>
                                <td class="cell_content">
                                    <input name="Mail_EnableSsl" type="radio" id="Mail_EnableSsl1" value="1" <% =Public.CheckedRadio(Mail_EnableSsl.ToString(), "1")%> />是
                                    <input type="radio" name="Mail_EnableSsl" id="Mail_EnableSsl2" value="0" <% =Public.CheckedRadio(Mail_EnableSsl.ToString(), "0")%> />否</td>
                            </tr>
                            <tr>
                                <td class="cell_title">邮件编码</td>
                                <td class="cell_content">
                                    <input name="Mail_Encode" type="text" id="Mail_Encode" style="width: 300px;" value="<% =Mail_Encode%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">邮件用户名</td>
                                <td class="cell_content">
                                    <input name="Mail_ServerUserName" type="text" id="Mail_ServerUserName" style="width: 300px;" value="<% =Mail_ServerUserName%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">邮件密码</td>
                                <td class="cell_content">
                                    <input name="Mail_ServerPassWord" type="password" id="Mail_ServerPassWord" style="width: 300px;" value="<% =Mail_ServerPassWord%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">发送者</td>
                                <td class="cell_content">
                                    <input name="Mail_FromName" type="text" id="Mail_FromName" style="width: 300px;" value="<% =Mail_FromName%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">邮件发送地址</td>
                                <td class="cell_content">
                                    <input name="Mail_From" type="text" id="Mail_From" style="width: 300px;" value="<% =Mail_From%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">邮件回复地址</td>
                                <td class="cell_content">
                                    <input name="Mail_Replyto" type="text" id="Mail_Replyto" style="width: 300px;" value="<% =Mail_Replyto%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">虚拟币名称</td>
                                <td class="cell_content">
                                    <input name="Coin_Name" type="text" id="Coin_Name" style="width: 300px;" value="<% =Coin_Name%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">每购物一元可得虚拟币</td>
                                <td class="cell_content">
                                    <input name="Coin_Rate" type="text" id="Coin_Rate" style="width: 300px;" value="<% =Coin_Rate%>" /></td>
                            </tr>
                             <tr>
                                <td class="cell_title">金价</td>
                                <td class="cell_content">
                                    <input name="Instant_GoldPrice" type="text" id="Instant_GoldPrice" style="width: 300px;" value="<% =Instant_GoldPrice%>" /></td>
                            </tr>
                             <tr>
                                <td class="cell_title">银价</td>
                                <td class="cell_content">
                                    <input name="Instant_SilverPrice" type="text" id="Instant_SilverPrice" style="width: 300px;" value="<% =Instant_SilverPrice%>" /></td>
                            </tr>
                            <%--<tr>
                          <td class="cell_title">启用交易合同</td>
                          <td class="cell_content"><input name="Trade_Contract_IsActive" type="radio" id="Radio1" value="1" <% =Public.CheckedRadio(Trade_Contract_IsActive.ToString(), "1")%>/>启用 <input type="radio" name="Trade_Contract_IsActive" id="Trade_Contract_IsActive1" value="0" <% =Public.CheckedRadio(Trade_Contract_IsActive.ToString(), "0")%>/>不启用</td>
                        </tr>--%>
                            <tr>
                                <td class="cell_title">启用虚拟化</td>
                                <td class="cell_content">
                                    <input name="Static_IsEnable" type="radio" value="1" <%=(Static_IsEnable == 1 ? "checked=\"checked\"" : "") %> />启用<input name="Static_IsEnable" type="radio" value="0" <%=(Static_IsEnable == 0 ? "checked=\"checked\"" : "") %> />关闭</td>
                            </tr>
                            <tr>
                                <td class="cell_title">维金合作编号</td>
                                <td class="cell_content">
                                    <input name="CreditPayment_Code" type="password" id="CreditPayment_Code" style="width: 300px;" value="<% =CreditPayment_Code%>" /></td>
                            </tr>
                            <%--<tr>
          <td class="cell_title">网银在线商户编号</td>
          <td class="cell_content"><input name="Chinabank_Code" type="text" id="Chinabank_Code" style="width:300px;" value="<% =Chinabank_Code%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">网银在线商户密钥</td>
          <td class="cell_content"><input name="Chinabank_Key" type="password" id="Chinabank_Key" style="width:300px;" value="<% =Chinabank_Key%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">支付宝账户</td>
          <td class="cell_content"><input name="Alipay_Email" type="text" id="Alipay_Email" style="width:300px;" value="<% =Alipay_Email%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">支付宝合作编号</td>
          <td class="cell_content"><input name="Alipay_Code" type="text" id="Alipay_Code" style="width:300px;" value="<% =Alipay_Code%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">支付宝合作密钥</td>
          <td class="cell_content"><input name="Alipay_Key" type="password" id="Alipay_Key" style="width:300px;" value="<% =Alipay_Key%>" /></td>
        </tr>--%>
                            <%--<tr>
          <td class="cell_title">财付通合作编号</td>
          <td class="cell_content"><input name="Tenpay_Code" type="text" id="Tenpay_Code" style="width:300px;" value="<% =Tenpay_Code%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">财付通合作密钥</td>
          <td class="cell_content"><input name="Tenpay_Key" type="password" id="Tenpay_Key" style="width:300px;" value="<% =Tenpay_Key%>" /></td>
        </tr>--%>
                            <%--<tr>
          <td class="cell_title">启用快速购买</td>
          <td class="cell_content"><input name="RepidBuy_IsEnable" type="radio" id="RepidBuy_IsEnable" value="1" <% =Public.CheckedRadio(RepidBuy_IsEnable.ToString(), "1")%>/>启用 <input type="radio" name="RepidBuy_IsEnable" id="RepidBuy_IsEnable2" value="0" <% =Public.CheckedRadio(RepidBuy_IsEnable.ToString(), "0")%>/>不启用</td>
        </tr>
        <tr>
          <td class="cell_title">购物车可用优惠券</td>
          <td class="cell_content"><input name="Coupon_UsedAmount" type="radio" id="Coupon_UsedAmount" value="1" <% =Public.CheckedRadio(Coupon_UsedAmount.ToString(), "1")%>/>1张 <input type="radio" name="Coupon_UsedAmount" id="Coupon_UsedAmount" value="0" <% =Public.CheckedRadio(Coupon_UsedAmount.ToString(), "0")%>/>多张</td>
        </tr>--%>
                           <%-- <tr>
                                <td class="cell_title">敏感词库
                                </td>
                                <td class="cell_content">
                                    <textarea rows="6" cols="60" name="Sys_Sensitive_Keyword"><%=Sys_Sensitive_Keyword%></textarea>
                                    <span class="tip">关键词之间以|分隔</span>
                                </td>
                            </tr>--%>
                            <tr>
                                <td class="cell_title">快递100授权码</td>
                                <td class="cell_content">
                                    <input name="Sys_Delivery_Code" type="text" id="Sys_Delivery_Code" style="width: 300px;" value="<% =Sys_Delivery_Code%>" /></td>
                            </tr>
                            <tr>
                                <td class="cell_title">站点标识</td>
                                <td class="cell_content">
                                    <input type="text" id="Sys_Config_Site" name="Sys_Config_Site" value="<%=Sys_Config_Site %>" /></td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="right">
                                    <input type="hidden" id="action" name="action" value="renew" />
                                    <input type="hidden" id="Sys_Config_ID" name="Sys_Config_ID" value="<% =Sys_Config_ID%>" />
                                    <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
                                    <input name="button" type="button" class="bt_grey" id="button" value="更新系统缓存" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="window.open('<% =Application["Site_URL"]%>/tools/updateapplication.aspx?timer='+ Math.random());" /></td>
                            </tr>
                        </table>
                    </form>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>
