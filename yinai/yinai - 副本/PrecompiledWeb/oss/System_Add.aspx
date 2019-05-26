<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    
    private Config myApp;
    private ITools tools;

    private string Upload_Server_URL, Upload_Server_Return_WWW, Upload_Server_Return_Admin, Language_Define, Site_DomainName, Site_Name, Site_URL, Site_Logo, Site_Tel, Site_Fax,
        Site_Email, Site_Keyword, Site_Description, Site_Title, Mail_Server, Mail_ServerUserName, Mail_ServerPassWord, Mail_FromName, Mail_From, Mail_Replyto,
        Mail_Encode, Coin_Name, Sys_Config_Site, Chinabank_Code, Chinabank_Key, Alipay_Email, Alipay_Code, Alipay_Key, Sys_Delivery_Code, Tenpay_Code, Tenpay_Key, CreditPayment_Code, Upload_Server_Return_Supplier;
    private int Sys_Config_ID, Mail_ServerPort, Mail_EnableSsl, Coin_Rate, Trade_Contract_IsActive, Static_IsEnable, RepidBuy_IsEnable, Coupon_UsedAmount;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("ef84a07f-6281-4f54-84f9-c345adf9d765");

        myApp = new Config();
        tools = ToolsFactory.CreateTools();

        Sys_Config_ID = 0;
        Upload_Server_URL = string.Empty;
        Upload_Server_Return_WWW = string.Empty;
        Upload_Server_Return_Admin = string.Empty;
        Upload_Server_Return_Supplier = string.Empty;
        Language_Define = string.Empty;
        Site_DomainName = string.Empty;
        Site_Name = string.Empty;
        Site_URL = string.Empty;
        Site_Logo = string.Empty;
        Site_Tel = string.Empty;
        Site_Fax = string.Empty;
        Site_Email = string.Empty;
        Site_Keyword = string.Empty;
        Site_Description = string.Empty;
        Site_Title = string.Empty;
        Mail_Server = string.Empty;
        Mail_ServerPort = 0;
        Mail_EnableSsl = 1;
        Mail_ServerUserName = string.Empty;
        Mail_ServerPassWord = string.Empty;
        Mail_FromName = string.Empty;
        Mail_From = string.Empty;
        Mail_Replyto = string.Empty;
        Mail_Encode = string.Empty;
        Coin_Name = string.Empty;
        Coin_Rate = 1;
        Trade_Contract_IsActive = 1;
        Sys_Config_Site = "CN";
        Static_IsEnable = 0;
        Chinabank_Code = string.Empty;
        Chinabank_Key = string.Empty;
        Alipay_Email = string.Empty;
        Alipay_Code = string.Empty;
        Alipay_Key = string.Empty;
        Tenpay_Code = string.Empty;
        Tenpay_Key = string.Empty;
        CreditPayment_Code = string.Empty;
        RepidBuy_IsEnable = 0;
        Sys_Delivery_Code = "";
        Coupon_UsedAmount = 1;
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
                                    <input name="Shop_Second_Domain" type="text" style="width: 300px;" maxlength="50" value="" /></td>
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
                                    <input name="Instant_GoldPrice" type="text" id="Instant_GoldPrice" style="width: 300px;" value="<% =Coin_Rate%>" /></td>
                            </tr>
                             <tr>
                                <td class="cell_title">银价</td>
                                <td class="cell_content">
                                    <input name="Instant_SilverPrice" type="text" id="Instant_SilverPrice" style="width: 300px;" value="<% =Coin_Rate%>" /></td>
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
        </tr>
                            --%>
                            <%--<tr>
                                <td class="cell_title">敏感词库
                                </td>
                                <td class="cell_content">
                                    <textarea rows="6" cols="60" name="Sys_Sensitive_Keyword"></textarea>
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
                                    <input type="text" id="Sys_Config_Site" name="Sys_Config_Site" /></td>
                            </tr>
                        </table>
                        <table width="100%" border="0" cellspacing="0" cellpadding="5">
                            <tr>
                                <td align="right">
                                    <input type="hidden" id="action" name="action" value="new" />
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
