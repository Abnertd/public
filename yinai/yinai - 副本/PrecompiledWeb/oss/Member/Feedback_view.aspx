<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private Feedback myApp;
    private ITools tools;

    private string Feedback_Name, Feedback_Tel, Feedback_Email, Feedback_Content, Feedback_Addtime, Feedback_Reply_Content,Feedback_Reply_Addtime,  Feedback_Site;
    private int Feedback_ID, Feedback_Type, Feedback_MemberID, Feedback_IsRead,Feedback_Reply_IsRead;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("9877a09e-5dda-4b1e-bf6f-042504449eeb");
        myApp = new Feedback();
        tools = ToolsFactory.CreateTools();

        Feedback_ID = tools.CheckInt(Request.QueryString["Feedback_ID"]);
        FeedBackInfo entity = myApp.GetFeedBackByID(Feedback_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else {
            Feedback_ID = entity.Feedback_ID;
            Feedback_Type = entity.Feedback_Type;
            Feedback_MemberID = entity.Feedback_MemberID;
            Feedback_Name = entity.Feedback_Name;
            Feedback_Tel = entity.Feedback_Tel;
            Feedback_Email = entity.Feedback_Email;
            Feedback_Content = entity.Feedback_Content;

            Feedback_IsRead = entity.Feedback_IsRead;
            Feedback_Reply_IsRead = entity.Feedback_Reply_IsRead;
            Feedback_Addtime = entity.Feedback_Addtime.ToString() ;
            Feedback_Reply_Addtime = entity.Feedback_Reply_Addtime.ToString();
            Feedback_Reply_Content = entity.Feedback_Reply_Content;
            Feedback_Site = entity.Feedback_Site;
            myApp.UpdateFeedBackStatus(Feedback_ID, 1, Feedback_Reply_IsRead);
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
      <td class="content_title">留言信息查看</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="member_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">联系人</td>
          <td class="cell_content"><%=Feedback_Name %></td>
        </tr>
        <tr>
          <td class="cell_title">联系电话</td>
          <td class="cell_content"><%=Feedback_Tel %></td>
        </tr>
        <tr>
          <td class="cell_title">联系Email</td>
          <td class="cell_content"><%=Feedback_Email%></td>
        </tr>
        <tr>
          <td class="cell_title">留言时间</td>
          <td class="cell_content"><%=Feedback_Addtime%></td>
        </tr>
        <tr>
          <td class="cell_title">回复时间</td>
          <td class="cell_content"><%=Feedback_Reply_Addtime%></td>
        </tr>
        <tr>
          <td class="cell_title">留言内容</td>
          <td class="cell_content"><%=Feedback_Content%></td>
        </tr>
        <tr>
          <td class="cell_title">留言回复</td>
          <td class="cell_content"><textarea rows="5" cols="50" name="Feedback_Reply_Content"><%=Feedback_Reply_Content%></textarea></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="feedbackreply" />
            <input type="hidden" id="feedback_id" name="feedback_id" value="<% =Feedback_ID%>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="返回" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='feedback_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>