<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/top_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier supplier = new Supplier();
    Session["Cur_Position"] = Session["Home_Position"] = "";
    int menu = tools.NullInt(Request["menu"]);
    string contract_sn = tools.CheckStr(Request["contract_sn"]);
    supplier.Supplier_Login_Check("/supplier/mycontract_detail.aspx?contract_sn=" + contract_sn);

    ContractInfo contractinfo = supplier.GetContractBySn(contract_sn);
    if (contractinfo == null)
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/MyContract_list.aspx");
    }
    else
    {
        if (contractinfo.Contract_SupplierID != tools.NullInt(Session["supplier_id"]))
        {
            pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/MyContract_list.aspx");
        }
    }
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="销售合同详情 - 交易管理 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
        <script src="/scripts/common.js" type="text/javascript"></script>
     <!--弹出菜单 start-->
    <script type="text/javascript">
        $(document).ready(function () {
            var byt = $(".testbox li");
            var box = $(".boxshow")
            byt.hover(
                 function () {
                     $(this).find(".boxshow").show(); $(this).find(".a3").attr("class", "a3 a3h");
                 },
                function () {
                    $(this).find(".boxshow").hide(); $(this).find(".a3h").attr("class", "a3");
                }
            );
        });
    </script>

    <!--弹出菜单 end-->
 <style type="text/css">
 .main table td{padding:3px;}
 </style>
    <script type="text/javascript">
        function turnnewpage(url) {
            location.href = url
        }
    </script>
</head>
<body>

    <uctop:top ID="top1" runat="server" />
    
    
    <!--主体 开始-->
<div class="content">
      <!--位置说明 开始-->
      <div class="position">
		   您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">会员中心</a> > <a href="/supplier/mycontract_detail.aspx?contract_sn=<%=contract_sn %>">销售合同详情</a>
	</div>
      <div class="clear"></div>
      <!--位置说明 结束-->
      <div class="parth">
            <div class="ph_left">
                         <% supplier.Get_Supplier_Left_HTML(4, 3); %>
            </div>
            <div class="ph_right">
                  <div class="blk13">
                        <h2>销售合同详情</h2>
                        <div class="main">
                                <h3 class="zkw_title21">
                                <%
                                    StringBuilder strHTML = new StringBuilder();
                                    strHTML.Append("<ul class=\"zkw_lst31\">");
                                    strHTML.Append("	<li class=\"on\" id=\"contract_1\" onclick=\"Set_Tab('contract',1,5,'on','');\">基本信息</li>");
                                    strHTML.Append("	<li id=\"contract_2\" onclick=\"Set_Tab('contract',2,5,'on','');\">合同内容</li>");
                                    strHTML.Append("	<li id=\"contract_3\" onclick=\"Set_Tab('contract',3,5,'on','');\">合同分期</li>");
                                    strHTML.Append("	<li id=\"contract_5\" onclick=\"Set_Tab('contract',5,5,'on','');\">发票信息</li>");
                                    strHTML.Append("	<li id=\"contract_4\" onclick=\"Set_Tab('contract',4,5,'on','');\">合同日志</li>");
                                    strHTML.Append("</ul><div class=\"clear\"></div>");
                                    Response.Write(strHTML.ToString());
                                %>
                            </h3>
                            
                                <div id="contract_1_content">
                                <% supplier.Contract_Detail(contractinfo); %>
                                </div>
                                <div id="contract_2_content" style="display:none;">
                                <table width="100%" border="0" cellspacing="0" cellpadding="5"
                                class="table_padding_5">
                                <tr>
                                    <td align="left">
                                    <%if (contractinfo.Contract_Status == 0)
                                      { %>
                                      <form action="contract_do.aspx" method="post">
                                        <textarea cols="80" id="Contract_Template" name="Contract_Template" rows="16"><%=contractinfo.Contract_Template%></textarea>
                                        <script type="text/javascript">
                                            CKEDITOR.replace('Contract_Template');
                                        </script>
                                        <center style="margin-top:10px;">
                                         <input name="btn_submit" type="submit" class="buttonSkinA" id="Submit1" value="保存合同内容" />
                                         <input name="contract_sn" type="hidden" value="<%=contract_sn %>" />
                                          <input name="action" type="hidden" value="contractedit" />
                                         </center>
                                          </form>
                                        <%}
                                      else
                                      {
                                          Response.Write(contractinfo.Contract_Template);
                                      } %>
                                    </td>
                                </tr>
                                </table>
                                </div>
                                <div id="contract_3_content" style="display:none; padding-top:10px;">
                                <%
                                    if (contractinfo.Contract_SupplierID == tools.NullInt(Session["supplier_id"]))
                                    {
                                        Response.Write(supplier.Contract_Divided(contractinfo.Contract_ID, contractinfo.Contract_SN, 1));
                                    }
                                    else
                                    {
                                        Response.Write(supplier.Contract_Divided(contractinfo.Contract_ID, contractinfo.Contract_SN, contractinfo.Contract_Status));
                                    }%>
                                </div>
                                <div id="contract_5_content" style="display:none; padding-top:10px;">
                                <%
                                if (contractinfo.Contract_SupplierID == tools.NullInt(Session["supplier_id"]))
                                    {
                                        Response.Write(supplier.Contract_Invoice(contractinfo.Contract_ID, contractinfo.Contract_SN, 1, contractinfo.Contract_Confirm_Status, contractinfo.Contract_SupplierID,contractinfo.Contract_AllPrice));
                                    }
                                    else
                                    {
                                        Response.Write(supplier.Contract_Invoice(contractinfo.Contract_ID, contractinfo.Contract_SN, contractinfo.Contract_Status, contractinfo.Contract_Confirm_Status, contractinfo.Contract_SupplierID, contractinfo.Contract_AllPrice));
                                    }
                                %>
                                </div>
                                <div id="contract_4_content" style="display:none; padding-top:10px;">
                                <%=supplier.Contract_Log_List(contractinfo.Contract_ID) %>
                                </div>
         
                                
                        </div>
                  </div>
            </div>
            <div class="clear"></div>
      </div> 
</div>
<!--主体 结束-->


 
    
  <ucbottom:bottom runat="server" ID="Bottom" />
    
</body>
</html>
