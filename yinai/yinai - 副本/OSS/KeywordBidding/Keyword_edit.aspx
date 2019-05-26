<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    private KeywordBidding myApp;
    private ITools tools;

    private string Keyword_Name;
    private int Keyword_ID;
    private double Keyword_MinPrice;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("0f39c533-9740-427f-ae56-649518a414c3");

        myApp = new KeywordBidding();
        tools = ToolsFactory.CreateTools();

        Keyword_ID = tools.CheckInt(Request.QueryString["Keyword_ID"]);
        KeywordBiddingKeywordInfo entity = myApp.GetKeywordBiddingKeywordID(Keyword_ID);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else {
            Keyword_ID = entity.Keyword_ID;
            Keyword_Name = entity.Keyword_Name;
            Keyword_MinPrice = entity.Keyword_MinPrice;
        }
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">竞价关键词修改</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="Keyword_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">关键词</td>
          <td class="cell_content"><% =Keyword_Name%></td>
        </tr>
        <tr>
          <td class="cell_title">起步价</td>
          <td class="cell_content"><input name="Keyword_MinPrice" type="text" id="Keyword_MinPrice" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="<% =Keyword_MinPrice%>" size="10" maxlength="10" />
        </td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="renew" />
            <input type="hidden" id="Keyword_ID" name="Keyword_ID" value="<% =Keyword_ID%>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='Keyword_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>