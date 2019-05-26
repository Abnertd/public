<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private MemberGrade myApp;
    private ITools tools;
    
    private string Member_Grade_Name;
    private int Member_Grade_ID, Member_Grade_Percent, Member_Grade_Default, Member_Grade_RequiredCoin;
    private double Member_Grade_CoinRate;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("73df03fa-ef43-486a-9b4c-b9c3e834cbbb");
        myApp = new MemberGrade();
        tools = ToolsFactory.CreateTools();

        Member_Grade_ID = tools.CheckInt(Request.QueryString["Member_Grade_ID"]);
        MemberGradeInfo entity = myApp.GetMemberGradeByID(Member_Grade_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else {
            Member_Grade_ID = entity.Member_Grade_ID;
            Member_Grade_Name = entity.Member_Grade_Name;
            Member_Grade_Percent = entity.Member_Grade_Percent;
            Member_Grade_Default = entity.Member_Grade_Default;
            Member_Grade_RequiredCoin = entity.Member_Grade_RequiredCoin;
            Member_Grade_CoinRate = entity.Member_Grade_CoinRate;
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
      <td class="content_title">会员等级修改</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="member_grade_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">等级名称</td>
          <td class="cell_content"><input name="Member_Grade_Name" type="text" id="Member_Grade_Name" style="width:200px;" maxlength="30" value="<% =Member_Grade_Name%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">优惠百分比</td>
          <td class="cell_content"><input name="Member_Grade_Percent" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" type="text" id="Member_Grade_Percent" style="width:60px;" value="<% =Member_Grade_Percent%>" /> <span class="tip">如果输入80，表示该会员等级以销售价80%的价格购买。</span></td>
        </tr>
        <tr>
          <td class="cell_title">是否为默认等级</td>
          <td class="cell_content"><input name="Member_Grade_Default" type="radio" id="Member_Grade_Default1" value="1" <% =Public.CheckedRadio(Member_Grade_Default.ToString(), "1")%> />是 <input type="radio" name="Member_Grade_Default" id="Member_Grade_Default2" value="0" <% =Public.CheckedRadio(Member_Grade_Default.ToString(), "0")%>/>否</td>
        </tr>
        <tr>
          <td class="cell_title">所需积分</td>
          <td class="cell_content"><input name="Member_Grade_RequiredCoin" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" type="text" id="Member_Grade_RequiredCoin" style="width:60px;" value="<% =Member_Grade_RequiredCoin%>" /> <span class="tip">升为本级所需积分</span></td>
        </tr>
        <tr>
          <td class="cell_title">积分比率</td>
          <td class="cell_content"><input name="Member_Grade_CoinRate" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" type="text" id="Member_Grade_CoinRate" style="width:60px;" value="<% =Member_Grade_CoinRate%>" /> <span class="tip">每购物一元可得虚拟币</span></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="renew" />
            <input type="hidden" id="Member_Grade_ID" name="Member_Grade_ID" value="<% =Member_Grade_ID%>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='member_grade_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>