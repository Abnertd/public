<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("603eef98-3a55-46d6-9e8e-81772645adeb");
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
      <td class="content_title">会员等级添加</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="member_grade_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">等级名称</td>
          <td class="cell_content"><input name="Member_Grade_Name" type="text" id="Member_Grade_Name" style="width:200px;" maxlength="30" /></td>
        </tr>
        <tr>
          <td class="cell_title">优惠百分比</td>
          <td class="cell_content"><input name="Member_Grade_Percent" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" type="text" id="Member_Grade_Percent" style="width:60px;" value="100" /> <span class="tip">如果输入80，表示该会员等级以销售价80%的价格购买。</span></td>
        </tr>
        <tr>
          <td class="cell_title">是否为默认等级</td>
          <td class="cell_content"><input name="Member_Grade_Default" type="radio" id="Member_Grade_Default1" value="1"/>是 <input type="radio" name="Member_Grade_Default" id="Member_Grade_Default2" value="0" checked="checked"/>否</td>
        </tr>
        <tr>
          <td class="cell_title">所需积分</td>
          <td class="cell_content"><input name="Member_Grade_RequiredCoin" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" type="text" id="Member_Grade_RequiredCoin" style="width:60px;" value="0" /> <span class="tip">升为本级所需积分</span></td>
        </tr>
        <tr>
          <td class="cell_title">积分比率</td>
          <td class="cell_content"><input name="Member_Grade_CoinRate" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" type="text" id="Member_Grade_CoinRate" style="width:60px;" value="1" /> <span class="tip">每购物一元可得虚拟币</span></td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="new" />
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