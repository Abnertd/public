<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<% 
    
    Public_Class pub = new Public_Class();
    ITools tools;
    tools = ToolsFactory.CreateTools();
    Pay pay = new Pay();

    string pay_payment;
            pay_payment =Request["payment"];
            string action = Request["action"];
            pay_payment = pay_payment.ToUpper();

            if (pay_payment == "CHINABANK")
            {
                string v_key, v_oid, v_pmode, v_pstatus, v_pstring, v_amount, v_moneytype, remark1, remark2, v_md5str;
                v_key = tools.NullStr(Application["Chinabank_Key"]);
                v_oid = tools.CheckStr(Request["v_oid"]);                               // 商户发送的v_oid定单编号
                v_pmode = Request["v_pmode"];			               // 支付方式（字符串） 
                v_pstatus = Request["v_pstatus"];                      // 支付状态 20（支付成功）;30（支付失败）
                v_pstring = Request["v_pstring"];				       // 支付结果信息 支付完成（当v_pstatus=20时）；失败原因（当v_pstatus=30时）；
                v_amount = Request["v_amount"];                         // 订单实际支付金额
                v_moneytype = Request["v_moneytype"];                   // 订单实际支付币种
                remark1 = Request["remark1"];						   // 备注字段1
                remark2 = Request["remark2"];			               // 备注字段2
                v_md5str = Request["v_md5str"];                           // 网银在线拼凑的Md5校验串

                pay.Pay_Chinabank_Notify(v_key, v_oid, v_pmode, v_pstatus, v_pstring, v_amount, v_moneytype, remark1, remark2, v_md5str);
            }
    //支付宝即时通知
            if (pay_payment == "ALIPAY")
            {
                pay.Pay_Alipay_Notify_Instant();
            }
    //支付宝担保/双接口通知
            if (pay_payment == "ALIPAY_PNT")
            {
                pay.Pay_Alipay_Notify();
            }
    //支付宝充值通知

            if (pay_payment == "TENPAY")
            {
                pay.Pay_tenPay_receive();
            }

            if (pay_payment == "99BILL")
            {
                pay.Quickmoney_Receive(); 
            }
            if (pay_payment == "CHINAPAY")
            {
                pay.Pay_ChinaPay_Notify(); 
            }
            if (pay_payment == "CMPAY")
            {
                //pay.Pay_CMPAY_Reciece();
            }

            if (pay_payment == "YEEPAY")
            {
                pay.YeePay_Reviece();
            }


            if (pay_payment == "V_INSTANT")
            {
                pay.VFINANCE_Trade_Status_Sync();
            }
            
            %>
            
            
          
    
