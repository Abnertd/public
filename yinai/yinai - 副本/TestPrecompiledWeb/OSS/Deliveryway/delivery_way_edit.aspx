<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
  
<script runat="server">
    
    private DeliveryWay myApp;
    private ITools tools;

    private string Delivery_Way_Name, Delivery_Way_Url, Delivery_Way_Intro, Delivery_Way_Site, Delivery_Way_Img;
    private int Delivery_Way_ID, Delivery_Way_Sort, Delivery_Way_InitialWeight, Delivery_Way_UpWeight, Delivery_Way_FeeType, Delivery_Way_Status, Delivery_Way_Cod;
    private double Delivery_Way_Fee, Delivery_Way_InitialFee, Delivery_Way_UpFee;

    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("58d92d67-4e0b-4a4c-bd5c-6062c432554d");
        myApp = new DeliveryWay();
        tools = ToolsFactory.CreateTools();

        Delivery_Way_ID = tools.CheckInt(Request.QueryString["Delivery_Way_ID"]);
        DeliveryWayInfo entity = myApp.GetDeliveryWayByID(Delivery_Way_ID);
        if (entity == null)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        else {
            Delivery_Way_ID = entity.Delivery_Way_ID;
            Delivery_Way_Name = entity.Delivery_Way_Name;
            Delivery_Way_Sort = entity.Delivery_Way_Sort;
            Delivery_Way_InitialWeight = entity.Delivery_Way_InitialWeight;
            Delivery_Way_UpWeight = entity.Delivery_Way_UpWeight;
            Delivery_Way_FeeType = entity.Delivery_Way_FeeType;
            Delivery_Way_Fee = entity.Delivery_Way_Fee;
            Delivery_Way_InitialFee = entity.Delivery_Way_InitialFee;
            Delivery_Way_UpFee = entity.Delivery_Way_UpFee;
            Delivery_Way_Status = entity.Delivery_Way_Status;
            Delivery_Way_Cod = entity.Delivery_Way_Cod;
            Delivery_Way_Img = entity.Delivery_Way_Img;
            Delivery_Way_Url = entity.Delivery_Way_Url;
            Delivery_Way_Intro = entity.Delivery_Way_Intro;
            Delivery_Way_Site = entity.Delivery_Way_Site;
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script type="text/javascript" src="/Public/kindeditor/kindeditor-all.js"></script>
<script type="text/javascript" src="/Public/kindeditor/lang/zh_CN.js"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">配送方式添加</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="delivery_way_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">配送方式名称</td>
          <td class="cell_content"><input name="Delivery_Way_Name" type="text" id="Delivery_Way_Name" size="50" maxlength="50" value="<% =Delivery_Way_Name%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">费用方式</td>
          <td class="cell_content"><input type="radio" name="delivery_way_feetype" value="0" onclick="if(this.checked){samefee.style.display='';weightfee.style.display='none';weight.style.display='none';}" <% =Public.CheckedRadio(Delivery_Way_FeeType.ToString(), "0")%> />统一计费 &nbsp; &nbsp; <input type="radio" name="delivery_way_feetype" value="1" <% =Public.CheckedRadio(Delivery_Way_FeeType.ToString(), "1")%> onclick="if(this.checked){samefee.style.display='none';weightfee.style.display='';weight.style.display='';}"/>按重量计费</td>
        </tr>
        <tr id="samefee" name="samefee" <% if (Delivery_Way_FeeType == 1) { Response.Write("style=\"display:none;\""); } %>>
          <td class="cell_title">费用</td>
          <td class="cell_content"><input name="delivery_way_fee" type="text" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="<% =Delivery_Way_Fee%>" /></td>
        </tr>
        <tr id="weight" <% if (Delivery_Way_FeeType == 0) { Response.Write("style=\"display:none;\""); } %>>
          <td class="cell_title">重量</td>
          <td class="cell_content">
          首重重量 
            <select name="Delivery_Way_InitialWeight">
              <option value="500" <% =Public.CheckedSelected(Delivery_Way_InitialWeight.ToString(), "500")%>>500克</option>
              <option value="1000" <% =Public.CheckedSelected(Delivery_Way_InitialWeight.ToString(), "1000")%>>1公斤</option>
              <option value="1200" <% =Public.CheckedSelected(Delivery_Way_InitialWeight.ToString(), "1200")%>>1.2公斤</option>
              <option value="2000" <% =Public.CheckedSelected(Delivery_Way_InitialWeight.ToString(), "2000")%>>2公斤</option>
              <option value="5000" <% =Public.CheckedSelected(Delivery_Way_InitialWeight.ToString(), "5000")%>>5公斤</option>
              <option value="10000" <% =Public.CheckedSelected(Delivery_Way_InitialWeight.ToString(), "10000")%>>10公斤</option>
              <option value="20000" <% =Public.CheckedSelected(Delivery_Way_InitialWeight.ToString(), "20000")%>>20公斤</option>
              <option value="50000" <% =Public.CheckedSelected(Delivery_Way_InitialWeight.ToString(), "50000")%>>50公斤</option>
            </select>
            续费重量 
            <select name="Delivery_Way_UpWeight">
              <option value="500" <% =Public.CheckedSelected(Delivery_Way_UpWeight.ToString(), "500")%>>500克</option>
              <option value="1000" <% =Public.CheckedSelected(Delivery_Way_UpWeight.ToString(), "1000")%>>1公斤</option>
              <option value="1200" <% =Public.CheckedSelected(Delivery_Way_UpWeight.ToString(), "1200")%>>1.2公斤</option>
              <option value="2000" <% =Public.CheckedSelected(Delivery_Way_UpWeight.ToString(), "2000")%>>2公斤</option>
              <option value="5000" <% =Public.CheckedSelected(Delivery_Way_UpWeight.ToString(), "5000")%>>5公斤</option>
              <option value="10000" <% =Public.CheckedSelected(Delivery_Way_UpWeight.ToString(), "10000")%>>10公斤</option>
              <option value="20000" <% =Public.CheckedSelected(Delivery_Way_UpWeight.ToString(), "20000")%>>20公斤</option>
              <option value="50000" <% =Public.CheckedSelected(Delivery_Way_UpWeight.ToString(), "50000")%>>50公斤</option>
            </select>
          </td>
        </tr>
        <tr id="weightfee" <% if (Delivery_Way_FeeType == 0) { Response.Write("style=\"display:none;\""); } %>>
          <td class="cell_title">费用</td>
          <td class="cell_content">首重费用 <input name="Delivery_Way_InitialFee" type="text" id="Delivery_Way_InitialFee" size="9" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="<% =Delivery_Way_InitialFee%>" /> &nbsp; &nbsp; 续重费用 <input name="Delivery_Way_UpFee" type="text" id="Delivery_Way_UpFee"  size="9" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" value="<% =Delivery_Way_UpFee%>"/></td>
        </tr>
        <tr>
          <td class="cell_title">图标</td>
          <td class="cell_content"><iframe id="iframe_upload" src="<% =Application["upload_server_url"]%>/public/uploadify.aspx?App=preview&formname=formadd&frmelement=Delivery_Way_Img&rtvalue=1&rturl=<% =Application["upload_server_return_admin"]%>" width="100%" height="100" frameborder="0" scrolling="no"></iframe></td>
        </tr>
        <tr id="tr_Delivery_Way_Img" style="display:<%if(Delivery_Way_Img==""){Response.Write("none");}%>;">
          <td class="cell_title"></td>
          <td class="cell_content"><img src="<%=Application["upload_server_url"] + Delivery_Way_Img %>" id="img_Delivery_Way_Img" /></td>
        </tr>
        <tr>
          <td class="cell_title">排序</td>
          <td class="cell_content"><input name="Delivery_Way_Sort" type="text" id="Delivery_Way_Sort" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" size="10" maxlength="10" value="<% =Delivery_Way_Sort%>" /></td>
        </tr>
        <tr>
          <td class="cell_title">启用</td>
          <td class="cell_content"><input name="Delivery_Way_Status" type="radio" value="1" <% =Public.CheckedRadio(Delivery_Way_Status.ToString(), "1")%>/>是 <input type="radio" name="Delivery_Way_Status" value="0" <% =Public.CheckedRadio(Delivery_Way_Status.ToString(), "0")%>/>否</td>
        </tr>
        <tr>
          <td class="cell_title">支持货到付款</td>
          <td class="cell_content"><input name="Delivery_Way_Cod" type="radio" value="1" <% =Public.CheckedRadio(Delivery_Way_Cod.ToString(), "1")%>/>是 <input type="radio" name="Delivery_Way_Cod" value="0" <% =Public.CheckedRadio(Delivery_Way_Cod.ToString(), "0")%>/>否</td>
        </tr>
        <tr>
          <td class="cell_title">查询网址</td>
          <td class="cell_content"><input name="Delivery_Way_Url" type="text" id="Delivery_Way_Url" size="50" maxlength="300" value="<%=Delivery_Way_Url %>" /> 物流信息查询网址中物流单号请以 {delivery_sn} 代替</td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">配送方式说明</td>
          <td class="cell_content">
            <textarea cols="80" id="Delivery_Way_Intro" name="Delivery_Way_Intro" rows="16"><% =Delivery_Way_Intro%></textarea>
            <script type="text/javascript">
                var Delivery_Way_IntroEditor;
                KindEditor.ready(function (K) {
                    Delivery_Way_IntroEditor = K.create('#Delivery_Way_Intro', {
                        width: '100%',
                        height: '500px',
                        filterMode: false,
                        afterBlur: function () { this.sync(); }
                    });
                });
            </script>
          </td>
        </tr>
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            <input type="hidden" id="action" name="action" value="renew" />
            <input type="hidden" id="Delivery_Way_ID" name="Delivery_Way_ID" value="<% =Delivery_Way_ID%>" />
            <input name="Delivery_Way_Img" type="hidden" id="Delivery_Way_Img" value="<%=Delivery_Way_Img %>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='delivery_way_list.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>