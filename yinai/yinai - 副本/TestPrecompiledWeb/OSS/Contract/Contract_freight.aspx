<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    
    private ITools tools;
    private Contract myApp;
    int Contract_ID;
    string Paymen_Name, Payment_Note, Contract_SN;
    double Payment_Amount;
    string freight_comarry;
    
    //会员信息定义
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("4b60b2fa-95b4-493a-a0b8-d1a06913b9b4");

        tools = ToolsFactory.CreateTools();
        myApp = new Contract();
       
        Contract_ID = tools.CheckInt(Request["contract_id"]);
        
        if (Contract_ID == 0)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }

        ContractInfo contractinfo = myApp.GetContractByID(Contract_ID);
        if (contractinfo != null)
        {
            if (contractinfo.Contract_Status != 1 || contractinfo.Contract_Delivery_Status >= 3 || contractinfo.Contract_Delivery_Status == 0||contractinfo.Contract_SupplierID>0)
            {
                Public.Msg("error", "错误信息", "该合同不支持此操作", false, "{back}");
                Response.End();
            }
            else
            {
                Contract_SN = contractinfo.Contract_SN;
            }
        }
        else
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }
        freight_comarry = "中通速递,申通,圆通速递,汇通,韵达快递,联邦快递,顺丰速运,EMS特快专递,中邮物流,AAE全球专递,安捷快递,安信达快递,百福东方,彪记快递,BHT,希伊艾斯快递,中国东方,长宇物流,大田物流,德邦物流,DPEX,DHL,D速快递,fedex,飞康达物流,凤凰快递,港中能达物流,广东邮政物流,汇通快运,恒路物流，华夏龙物流,佳怡物流,京广速递,急先达,加运美,快捷速递,联昊通物流,龙邦物流,民航快递,配思货运,全晨快递,全际通物流,全日通快递,全一快递,盛辉物流,速尔物流,盛丰物流,天地华宇,天天快递,TNT,UPS,万家物流,文捷航空速递,伍圆速递,万象物流,新邦物流,信丰物流,星晨急便,鑫飞鸿物流,亚风速递,一邦速递,优速物流,远成物流,源伟丰快递,元智捷诚快递,越丰物流,源安达,运通快递,宅急送,中铁快运";
        
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
      <form id="Form2" name="frm_delivery" method="post" action="contract_do.aspx">
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">			
			  <tr>
				<td width="100" align="right" class="cell_title">物流费用</td>
				<td  class="cell_content"><input type="text" name="Contract_Delivery_Amount" id="Contract_Delivery_Amount" value="0" class="txt_border" /></td>
			  </tr>
			  <tr>
				<td width="100" align="right" class="cell_title">配送方式</td>
				<td class="cell_content"><%=myApp.Delivery_Company_Select("contract_delivery", "")%></td>
			  </tr>
			  <tr>
				<td width="100" align="right" class="cell_title">物流公司</td>
				<td class="cell_content">
				<select name="Contract_Delivery_CompanyName">
             <option value="">请选择</option>
             <%
             foreach(string comsub in freight_comarry.Split(','))
             {
                 Response.Write("<option value=\"" + comsub + "\">" + comsub + "</option>");
             }
              %>
              </select>
				
			  </tr>
			  <tr>
				<td width="100" align="right" class="cell_title">物流单号</td>
				<td class="cell_content"><input type="text" name="Contract_Delivery_Code" class="txt_border" id="Contract_Delivery_Code" value="" /></td>
			  </tr>
			  <tr>
				<td width="100"class="cell_title" align="right">配送备注</td>
				<td class="cell_content"><textarea name="Contract_Delivery_Note" id="Contract_Delivery_Note" cols="50" rows="5"></textarea></td>
			  </tr>
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
			<%=myApp.Contract_Orders_Goods(Contract_ID)%>
			
			
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
        
          <tr>
            <td align="right">
            <input name="button" type="submit" class="bt_orange" id="Submit2" value="保存" />
				<input name="action" type="hidden" id="action" value="contract_freight" />
                <input name="contract_sn" type="hidden" id="contract_sn" value="<%=Contract_SN %>" />
             <input name="button" type="button" class="bt_grey" id="button1" value="取消" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="history.go(-1);"/></td>
          </tr>
        </table>
			</form>
      </td>
    </tr>
  </table>
</div>



</body>
</html>