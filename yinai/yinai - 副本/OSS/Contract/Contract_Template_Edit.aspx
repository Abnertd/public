<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    private Contract myAppC;
    private ITools tools;
    int template_id;
    string Contract_Template_Name, Contract_Template_Code, Contract_Template_Content;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("54d1aadd-ca23-4c1a-b56e-793543423a39");
        tools = ToolsFactory.CreateTools();
        template_id = tools.CheckInt(Request["template_id"]);
        myAppC = new Contract();
        ContractTemplateInfo entity = myAppC.GetContractTemplateByID(template_id);
        if (entity != null)
        {
            Contract_Template_Name = entity.Contract_Template_Name;
            Contract_Template_Code = entity.Contract_Template_Code;
            Contract_Template_Content = entity.Contract_Template_Content;
        }
        else
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
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
      <td class="content_title">设置合同模版</td>
    </tr>
    <tr>
      <td class="content_content">
      <form id="formadd" name="formadd" method="post" action="contract_template_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">模版名称</td>
          <td class="cell_content"><input name="Contract_Template_Name" type="text" value="<%=Contract_Template_Name %>" id="Contract_Template_Name" size="50" maxlength="50" /> <span class="t14_red">*</span></td>
        </tr>
        <tr>
          <td class="cell_title">模版标识</td>
          <td class="cell_content"><input name="Contract_Template_Code" type="text" value="<%=Contract_Template_Code %>" id="Contract_Template_Code" size="50" maxlength="50" /> <span class="t14_red">*</span></td>
        </tr>
        <tr>
          <td class="cell_title" valign="top">模版内容</td>
          <td class="cell_content">
          <textarea cols="80" id="Contract_Template_Content" name="Contract_Template_Content" rows="16"><% =Contract_Template_Content%></textarea>
            <script type="text/javascript">
                var Contract_Template_ContentEditor;
                KindEditor.ready(function (K) {
                    Contract_Template_ContentEditor = K.create('#Contract_Template_Content', {
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
            <input type="hidden" id="Contract_Template_id" name="Contract_Template_id" value="<%=template_id %>" />
            <input name="save" type="submit" class="bt_orange" id="save" value="保存" />
             <input name="button" type="button" class="bt_grey" id="button" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='contract_template.aspx';"/></td>
          </tr>
        </table>
        </form>
        </td>
    </tr>
  </table>
</div>
</body>
</html>