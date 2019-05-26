<%@ Page Language="C#" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="../Public/Top.ascx" TagName="Top" TagPrefix="uc1" %>
<%@ Register Src="../Public/Bottom.ascx" TagName="Bottom" TagPrefix="uc2" %>
<% 
    
    Public_Class pub = new Public_Class();
    ITools tools;
    tools = ToolsFactory.CreateTools();
    Pay pay = new Pay();
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%="订单支付 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <style type="text/css">
        .img_border {
            border: 1px solid #d5d5d5;
        }

        .table_filter {
            margin-top: 10px;
        }

        .pay_intro {
            line-height: 25px;
            padding: 10px;
        }

        .table_filter td {
            padding: 6px;
        }
    </style>
</head>
<body>
    <uc1:Top ID="Page_Top" runat="server" />
    <div class="content02" style="background-color: #FFF;">
        <div class="content02_main" style="background-color: #FFF;">
            <!--位置说明 开始-->
            <div class="position"><a href="/">首页</a> > 支付结果 </div>

            <div class="partd">
                <table width="980" align="center" border="0" cellspacing="0" cellpadding="5" class="table_filter">
                    <tr>
                        <td>

                            <%
                                //引用类


                                string pay_payment;
                                string payresult;
                                payresult = Request["payresult"];
                                pay_payment = Request["payment"];
                                pay_payment = pay_payment.ToUpper();
                                if (pay_payment == "CHINABANK")
                                {
                                    string v_key, v_oid, v_pmode, v_pstatus, v_pstring, v_amount, v_moneytype, remark1, remark2, v_md5str;
                                    v_key = tools.NullStr(Application["Chinabank_Key"]);
                                    v_oid = v_oid = tools.CheckStr(Request["v_oid"]); ;                               // 商户发送的v_oid定单编号
                                    v_pmode = Request["v_pmode"];			               // 支付方式（字符串） 
                                    v_pstatus = Request["v_pstatus"];                      // 支付状态 20（支付成功）;30（支付失败）
                                    v_pstring = Request["v_pstring"];				       // 支付结果信息 支付完成（当v_pstatus=20时）；失败原因（当v_pstatus=30时）；
                                    v_amount = Request["v_amount"];                         // 订单实际支付金额
                                    v_moneytype = Request["v_moneytype"];                   // 订单实际支付币种
                                    remark1 = Request["remark1"];						   // 备注字段1
                                    remark2 = Request["remark2"];			               // 备注字段2
                                    v_md5str = Request["v_md5str"];                           // 网银在线拼凑的Md5校验串

                                    pay.Pay_Chinabank_Receive(v_key, v_oid, v_pmode, v_pstatus, v_pstring, v_amount, v_moneytype, remark1, remark2, v_md5str);
                                }
                                if (pay_payment == "BCOM")
                                {
                                    pay.Pay_Bocomm_Receive();
                                }
                                if (pay_payment == "ALIPAY")
                                {
                                    pay.Pay_Alipay_Reviece_Instant();
                                }
                                if (pay_payment == "ALIPAY_DOU")
                                {
                                    pay.Pay_Alipay_Reviece();
                                }


                                if (pay_payment == "ACCOUNT")
                                {
                                    if (Request["result"] == "success")
                                    {
                                        pay.Pay_Result("success", tools.NullStr(Request["orders_sn"]));
                                    }
                                    else
                                    {
                                        pay.Pay_Result("failed", "");
                                    }
                                }
                                if (pay_payment == "TENPAY")
                                {
                                    if (Request["payresult"] == "success")
                                    {
                                        pay.Pay_Result("success", tools.NullStr(Request["orders_sn"]));
                                    }
                                    else
                                    {
                                        pay.Pay_Result("failed", "");
                                    }
                                }
                                if (pay_payment == "99BILL")
                                {
                                    if (Request["result"] == "success")
                                    {
                                        pay.Pay_Result("success", tools.NullStr(Request["orders_sn"]));
                                    }
                                    else
                                    {
                                        pay.Pay_Result("failed", "");
                                    }
                                }
                                if (pay_payment == "CHINAPAY")
                                {
                                    pay.Pay_ChinaPay_Reciece();
                                }
                                if (pay_payment == "CMPAY")
                                {
                                    //pay.Pay_CMPAY_Reciece();
                                }
                                if (pay_payment == "YEEPAY")
                                {
                                    if (payresult == "success")
                                    {
                                        pay.Pay_Result("success", tools.NullStr(Request["orders_sn"]));
                                    }
                                    else
                                    {
                                        pay.Pay_Result("failed", "");
                                    }
                                }

                                //if (pay_payment == "V_INSTANT")
                                //{
                                //    pay.VFINANCE_Trade_Status_Reviece();
                                //}
                            %>
            
                        </td>
                    </tr>
                </table>


                <table border="0" cellpadding="0" cellspacing="0" width="980" align="center">
                    <tr>
                        <td height="10"></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <uc2:Bottom ID="Page_Bottom" runat="server" />

</body>
</html>

