<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    
    private ITools tools;
    private Contract myApp;
    private Product MyProduct;
    int Contract_ID, freight_id, freight_status, deliveryid;
    string Paymen_Name, Payment_Note, Contract_SN;
    double freight_Amount;
    string freight_sn, freight_name, freight_code, freight_company, freight_note,PACKAGECODE, acceptnote;
    string freight_addtime;
    bool isaccept = true;
    
    //会员信息定义
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("all");
        freight_sn = "";
        freight_name = "";
        freight_status = 0;
        freight_code = "";
        freight_addtime = "";
        freight_Amount = 0;
        freight_note = "";
        freight_company = "";
        acceptnote = "";
        PACKAGECODE = "";
        tools = ToolsFactory.CreateTools();
        myApp = new Contract();
        MyProduct = new Product();
       
        Contract_ID = tools.CheckInt(Request["contract_id"]);
        freight_id = tools.CheckInt(Request["contract_delivery"]);
        if (Contract_ID == 0 || freight_id==0)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }

        ContractInfo contractinfo = myApp.GetContractByID(Contract_ID);
        if (contractinfo != null)
        {
            Contract_SN=contractinfo.Contract_SN;
            ContractDeliveryInfo entity = myApp.GetContractDeliveryByID(freight_id);
            if (entity != null)
            {
                freight_status = entity.Contract_Delivery_DeliveryStatus;
                freight_sn = entity.Contract_Delivery_DocNo;
                freight_name = entity.Contract_Delivery_Name;
                freight_code = entity.Contract_Delivery_Code;
                freight_addtime = entity.Contract_Delivery_Addtime.ToString();
                freight_Amount = entity.Contract_Delivery_Amount;
                freight_company = entity.Contract_Delivery_CompanyName;
                freight_note = entity.Contract_Delivery_Note;
                acceptnote = entity.Contract_Delivery_AccpetNote;
                deliveryid = entity.Contract_Delivery_ID;
            }
            else
            {
                Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
                Response.End();
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        
        
    }

    protected void Page_UnLoad(object sender, EventArgs e) {
        tools = null;
        myApp = null;
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>

<style type="text/css">
    
    .tablebg_green{ background:#390;}
    .tablebg_green td{ background:#FFF;}
</style>

</head>
<body>

<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">合同[<%=Contract_SN%>]发货信息</td>
    </tr>
    <tr>
      <td class="content_content">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
			<tr>
				<td width="100" height="23" align="right" class="cell_title">配送单号</td>
				<td align="left" class="cell_content"><%=freight_sn%></td>
			  </tr>
			  <tr>
				<td width="100" height="23" align="right" class="cell_title">配送方式</td>
				<td align="left" class="cell_content"><%=myApp.GetDeliveryName(deliveryid) %></td>
			  </tr>
			  <tr>
				<td width="100" height="23" align="right" class="cell_title">配送费用</td>
				<td align="left" class="cell_content"><%=Public.DisplayCurrency(freight_Amount)%></td>
			  </tr>
			  
			<tr>
				<td width="100" height="23" align="right" class="cell_title">物流单号</td>
				<td align="left" class="cell_content"><%=freight_code %></td>
			  </tr>
			  <tr>
				<td width="100" height="23" align="right" class="cell_title">物流公司</td>
				<td align="left" class="cell_content"><%=freight_name %></td>
			  </tr>
			  <tr>
				<td width="100" height="23" align="right" class="cell_title">备注</td>
				<td align="left" class="cell_content"><%=freight_note %></td>
			  </tr>
			  <%--<tr>
				<td width="100" height="23" align="right" class="cell_title">签收备注</td>
				<td align="left" class="cell_content"><%=acceptnote%></td>
			  </tr>--%>
			  <tr>
				<td></td>
				<td align="left">
				
				</td>
			  </tr>
			</table>
			<table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
              <td height="10"></td>
            </tr>
            </table>
            <form id="Form1" name="frm_delivery" method="post" action="contract_do.aspx">
			<%//=myApp.Contract_Delivery_Goods(freight_id)
                isaccept = myApp.Contract_Delivery_Goods_List(Contract_ID, deliveryid,0);
			    	    %>
			
			
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
        
          <tr>
            <td align="right">
            <%
                    if (!isaccept)
                    {
                        Response.Write("<input type=\"submit\" name=\"bt_orange\" value=\" 保存 \" class=\"bt_orange\" />");
                        Response.Write(" <input type=\"button\" name=\"bt_all\" value=\" 全部签收 \" class=\"bt_orange\" onclick=\"location='contract_do.aspx?action=contract_allaccept&Contract_SN=" + Contract_SN + "&contract_delivery=" + freight_sn + "';\" />");
                    }
                 %>
                 <input type="hidden" name="action" value="contract_accept" />
                <input type="hidden" name="Contract_SN" value="<%=Contract_SN %>" />
                <input type="hidden" name="contract_delivery" value="<%=freight_sn %>" />
             <input name="button" type="button" class="bt_grey" id="button1" value="返回" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='/Contract/contract_detail.aspx?contract_id=<%=Contract_ID %>';"/></td>
          </tr>
        </table>
        </form>
      </td>
    </tr>
  </table>
</div>



</body>
</html>