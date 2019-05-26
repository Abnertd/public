<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">
    
    private ITools tools;
    private Contract myApp;
    private Member member;
    int Contract_BuyerId, contract_id, CONTRACT_AREAID, Contract_deliveryID, Contract_PaywayID, Contract_Status, Contract_Paymentstatus, Contract_DeliveryStatus, Contract_ConfirmStatus, Contract_SupplierID, Contract_Type, Contract_Source;
    string  Contract_Template,Contract_DeliveryName,Contract_PaywayName,Contract_Sn;
    double Contract_Allprice, Contract_Freight, Contract_Service, Contract_Discount, Contract_Price,Payed_Amount;
    DateTime Contract_Addtime;
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("cd2be0f8-b35a-48ad-908b-b5165c0a1581");

        tools = ToolsFactory.CreateTools();
        myApp = new Contract();
        member = new Member();
        contract_id=tools.CheckInt(Request["contract_id"]);
        ContractInfo contract = myApp.GetContractByID(contract_id);
        if (contract != null)
        {
            Contract_Type = contract.Contract_Type;
            Contract_BuyerId=contract.Contract_BuyerID;
            Contract_SupplierID = contract.Contract_SupplierID;
            Contract_Allprice=contract.Contract_AllPrice;
            Contract_Price = contract.Contract_Price;
            Contract_PaywayID=contract.Contract_Payway_ID;
            Contract_Status=contract.Contract_Status;
            Contract_Paymentstatus=contract.Contract_Payment_Status;
            Contract_DeliveryStatus = contract.Contract_Delivery_Status;
            Contract_ConfirmStatus=contract.Contract_Confirm_Status;
            Contract_Template=contract.Contract_Template;
            Contract_PaywayName=contract.Contract_Payway_Name;
            Contract_Sn=contract.Contract_SN;
            Contract_Freight=contract.Contract_Freight;
            Contract_Service = contract.Contract_ServiceFee;
            Contract_Addtime=contract.Contract_Addtime;
            Contract_Discount = contract.Contract_Discount;
            Contract_Source = contract.Contract_Source;
            Payed_Amount = myApp.Get_Contract_PayedAmount(contract_id);
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
        member = null;

    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>合同详情</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<script src="/Scripts/common.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script type="text/javascript" src="/Public/ckeditor/ckeditor.js"></script>
<link href="/Scripts/jqGrid/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
<script src="/Scripts/jqGrid/grid.locale-zh_CN.js" type="text/javascript"></script>
<script src="/Scripts/jqGrid/jquery.jqGrid.min.js" type="text/javascript"></script>

<style type="text/css">
    
    .tablebg_green{ background:#390;}
    .tablebg_green td{ background:#FFF;}
</style>
<script type="text/javascript">
    function turnnewpage(url) {
        location.href = url
    }
</script>
</head>
<body>

<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">合同管理</td>
    </tr>
    <tr>
      <td class="content_content">
      <table cellspacing="0" cellpadding="0" border="0" width="100%">
        <tr class="orders_tdbg">
           <td width="100"><span class="title">合同操作</span></td>
           <td align="left"><%myApp.Contract_Detail_Button(Contract_Status, Contract_ConfirmStatus, Contract_Paymentstatus, Contract_DeliveryStatus, contract_id, Contract_Addtime, Contract_SupplierID, Contract_Source); %>
           
                &nbsp;&nbsp;打印 : 
                <input name="btn_print" type="button" class="btn_01" id="Button4" value="合同打印" onclick="window.open('Contract_view.aspx?action=print&contract_id=<% =contract_id %>')" />
                <%--<a href="<%=Application["Site_Url"] %>/help/detail.aspx?HelpID=21" style="font-size:12px;color:#4378C8;font-weight:normal;" target="_blank">打印设置</a>--%>
           </td>
        </tr>
        </table>
        <table cellspacing="0" cellpadding="0" border="0" width="100%">
        <tr>
           <td>
           <table border="0" cellspacing="0" cellpadding="5">
             <tr>
               <td><table border="0" cellpadding="5" cellspacing="1" class="tablebg_green">
                   <tr>
                     <td width="70" align="right" class="t12_green">合同编号</td>
                     <td class="t12_red"><% =Contract_Sn%></td>
                   </tr>
                   <tr>
                     <td align="right" class="t12_green">合同状态</td>
                     <td><%=myApp.ContractStatus(Contract_Status)%></td>
                   </tr>
                   <tr>
                     <td align="right" class="t12_green">生成时间</td>
                     <td><%=Contract_Addtime %></td>
                   </tr>
               </table></td>
               
              <td width="50" align="center" valign="middle"><img src="/images/step_arrow.gif" width="26" height="21" /></td>
               <td align="center">
               <table border="0" cellpadding="5" cellspacing="1" class="tablebg_green">
                   <tr>
                     <td width="70" align="right" class="t12_green">
                     确认状态
                     </td>
                     <td><%=myApp.ContractConfirmStatus(Contract_ConfirmStatus)%></td>
                   </tr>
                   <tr>
                     <td align="right" class="t12_green">付款状态</td>
                     <td><%=myApp.ContractPaymentStatus(Contract_Paymentstatus)%></td>
                   </tr>
                   <tr>
                     <td align="right" class="t12_green">配送状态</td>
                     <td>
                     <%=myApp.ContractDeliveryStatus(Contract_DeliveryStatus)%>
                     </td>
               
                   </tr>
               </table></td>
               <%if (Contract_Status > 0)
                 { %>
               <td width="50" align="center" valign="middle"><img src="/images/step_arrow.gif" width="26" height="21" /></td>
               <td><table border="0" cellpadding="5" cellspacing="1" class="tablebg_green">
                   <tr>
                     <td width="70" align="right" class="t12_green">支付方式</td>
                     <td class="t12_red"><% =Contract_PaywayName%></td>
                   </tr>

                   <tr>
                     <td align="right" class="t12_green">合同总金额</td>
                     <td><%=Public.DisplayCurrency(Contract_Allprice)%></td>
                   </tr>
                   <tr>
                     <td align="right" class="t12_green">已支付金额</td>
                     <td><%=Public.DisplayCurrency(Payed_Amount)%></td>
                   </tr>
               </table></td>
               <td width="50" align="center" valign="middle"><img src="/images/step_arrow.gif" width="26" height="21" /></td>
               <td align="center">
               <table border="0" cellpadding="5" cellspacing="1" class="tablebg_green">
                   <tr>
                     <td width="70" align="right" class="t12_green">
                     物流费用
                     </td>
                     <td><%=Public.DisplayCurrency(Contract_Freight)%></td>
                   </tr>
                   <tr>
                     <td align="right" class="t12_green">服务费用</td>
                     <td><%=Public.DisplayCurrency(Contract_Service)%></td>
                   </tr>
                   <tr>
                     <td align="right" class="t12_green">订单金额</td>
                     <td>
                     <%=Public.DisplayCurrency(Contract_Price)%>
                     </td>
               </tr></table>
                </td>
               <%} %>
             </tr>
           </table></td>
        </tr>
        <%
            if (Contract_Status == 0)
            {
                if (Contract_SupplierID == 0) { %>
        <tr class="orders_tdbg">
           <td><span class="title">合同信息</span></td>
        </tr>
        <tr>
           <td>
           
             <form id="frm_contract" name="frm_contract" method="post" action="contract_do.aspx">
              <table width="100%" border="0" cellspacing="0" cellpadding="5">
                <tr>
                  <td width="100" align="right"><strong>合同编辑</strong></td>
                  <td>
                    <textarea  style="display:none;" cols="0" rows="0" name="Contract_Template" id="Contract_Template"><% =Contract_Template%></textarea>
                    <script type="text/javascript">
                        CKEDITOR.replace('Contract_Template');
                    </script>
                  </td>
                </tr>
                <tr>
                <td align="right">物流费用</td>
                <td>
                    <table border="0" cellpadding="5" cellspacing="0">
                    <tr><td><input type="text" name="Contract_Freight" id="Contract_Freight" value="<%=Contract_Freight %>" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" style="text-align:right" /> 元</td>
                    <td align="right">服务费用</td>
                    <td><input type="text" name="Contract_Service" id="Contract_Service" value="<%=Contract_Service %>" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" style="text-align:right" /> 元</td>
                    <td align="right">合同优惠</td>
                    <td><input type="text" name="Contract_Discount" id="Contract_Discount" value="<%=Contract_Discount %>" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" style="text-align:right" /> 元</td>
                    </tr>
                    </table>
                </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td><input name="button" type="submit" class="bt_orange" id="button" value="修改合同" />
                    <input name="action" type="hidden" id="action" value="Contract_Save" />
                    <input name="Contract_id" type="hidden" id="Contract_id" value="<%=contract_id %>" />
                    </td>
                  </tr>
            </table>
            </form>
           </td>
        </tr>
        <%} else { %>
        <tr class="orders_tdbg">
           <td><span class="title">合同信息</span></td>
        </tr>
        <tr>
           <td>
           
             <form id="Form3" name="frm_contract" method="post" action="contract_do.aspx">
              <table width="100%" border="0" cellspacing="0" cellpadding="5">
                <tr>
                  <td width="100" align="right"><strong>合同编辑</strong></td>
                  <td>
                    <textarea  style="display:none;" cols="0" rows="0" name="Contract_Template" id="Textarea1"><% =Contract_Template%></textarea>
                    <script type="text/javascript">
                        CKEDITOR.replace('Contract_Template');
                    </script>
                  </td>
                </tr>
                <tr>
                <td align="right" width="100" >代理服务费用</td>
                <td>
                <table border="0" cellpadding="5" cellspacing="0">
                <tr><td><input type="text" name="Contract_Service" id="Text2" value="<%=Contract_Service %>" onkeyup="if(isNaN(value))execCommand('undo')" onafterpaste="if(isNaN(value))execCommand('undo')" style="text-align:right" /> 元</td>
                
                </tr>
                </table>
                
                </tr>
                
                <tr>
                    <td>&nbsp;</td>
                    <td><input name="button" type="submit" class="bt_orange" id="Submit2" value="修改合同" />
                    <input name="action" type="hidden" id="Hidden1" value="Contract_Save" />
                    <input name="Contract_id" type="hidden" id="Hidden2" value="<%=contract_id %>" />
                    </td>
                  </tr>
            </table>
            </form>
            
             
           </td>
        </tr>
        <%
            }
            }
            %>
             
        <tr class="orders_tdbg">
           <td><span class="title">附加订单</span></td>
        </tr>
        <tr>
           <td><div style="height:5px;"></div><%=myApp.GetContractOrdersByContractsID(contract_id)%><div style="height:5px;"></div></td>
        </tr>
        <tr class="orders_tdbg">
           <td><span class="title">合同总金额</span></td>
        </tr>
        <tr>
           <td class="price_list"><div style="height:5px;"></div><%=Public.DisplayCurrency(Contract_Price) + "<span class=\"tip\">(订单总金额)</span> +" + Public.DisplayCurrency(Contract_Freight) + "<span class=\"tip\">(物流费用)</span> +" + Public.DisplayCurrency(Contract_Service) + "<span class=\"tip\">(代理服务费用)</span> -" + Public.DisplayCurrency(Contract_Discount) + "<span class=\"tip\">(合同优惠)</span>" + " =" + Public.DisplayCurrency(Contract_Allprice)%><div style="height:5px;"></div></td>
        </tr>

        <tr class="orders_tdbg">
           <td><span class="title">合同分期</span></td>
        </tr>
        <tr>
           <td>
           <form id="Form2" name="frm_contract" method="post" action="contract_do.aspx">
           <div style="height:5px;"></div><%
                                              if(Contract_Source>0)
                                              {
                                                  Response.Write(myApp.GetDividedPayByContractsID(contract_id, Contract_Sn, Contract_Status));
                                              }
                                              else
                                              {
                                                  Response.Write(myApp.GetDividedPayByContractsID(contract_id, Contract_Sn, 1));
                                              }%><div style="height:5px;"></div>
           </form>
           </td>
        </tr>
        <tr class="orders_tdbg">
           <td><span class="title">付款单</span></td>
        </tr>
        <tr>
           <td>
           <div style="height:5px;"></div><%=myApp.Contract_Payments(contract_id,Contract_Sn,Contract_SupplierID)%><div style="height:5px;"></div>
           </td>
        </tr>
        <tr class="orders_tdbg">
           <td><span class="title">配送单</span></td>
        </tr>
        <tr>
           <td>
           <div style="height:5px;"></div><%=myApp.Contract_Deliverys(contract_id,Contract_Sn)%><div style="height:5px;"></div>
           </td>
        </tr>
        <tr class="orders_tdbg">
           <td><span class="title">发票信息</span></td>
        </tr>
        <tr>
           <td>
           <div style="height:5px;"></div><%=myApp.GetContractInvoice(contract_id,Contract_Status,Contract_SupplierID)%><div style="height:5px;"></div>
           </td>
        </tr>
        <tr class="orders_tdbg">
           <td><span class="title">开票申请</span></td>
        </tr>
        <tr>
           <td>
           <div style="height:5px;"></div><%=myApp.GetInvoiceApplyssByContractsID(contract_id, Contract_Sn,Contract_SupplierID)%><div style="height:5px;"></div>
           </td>
        </tr>
        <%if (Contract_SupplierID == 0)
          { %>
        <tr class="orders_tdbg">
           <td><span class="title">意见回复</span></td>
        </tr>
        <tr>
           <td>
             <form id="frm_intent" name="frm_contract" method="post" action="contract_do.aspx?contract_sn=<%=Contract_Sn%>">
              <table width="100%" border="0" cellspacing="0" cellpadding="5">
                <tr>
                <td align="right">备注</td>
                <td><textarea cols="50" rows="5" name="Contract_Note" ></textarea></td>
                </tr>
                <tr>
                <td></td>
                <td><iframe id="iframe_upload" src="<%=Application["Upload_Server_URL"] %>/public/uploadify.aspx?App=attachment&formname=frm_intent&frmelement=attachment_file&rtvalue=1&rturl=<%=Application["Upload_Server_Return_Admin"] %>" width="300" height="100" frameborder="0" scrolling="no"></iframe></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td><input name="button" type="submit" class="bt_orange" id="Submit1" value="提交信息" />
                    <input type="hidden" name="attachment_file" id="attachment_file" />
                    <input name="action" type="hidden" id="action" value="contract_noteedit" /></td>
                  </tr>
            </table>
            </form>
           </td>
        </tr>
        <%} %>
        <tr class="orders_tdbg">
           <td><span class="title">合同日志</span></td>
        </tr>
        <tr>
           <td><div style="height:5px;"></div><%=myApp.GetContractLogsByContractsID(contract_id)%></td>
        </tr>
      </table>
      </td>
    </tr>
  </table>
</div>


</body>
</html>