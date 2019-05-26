<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>

<% 
    
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    Supplier supplier = new Supplier();

    supplier.Supplier_Login_Check("/supplier/Supplier_PayBack_Apply.aspx");

    DateTime Supplier_PayBack_Apply_Addtime = DateTime.Now;
    DateTime Supplier_PayBack_Apply_AdminTime = DateTime.Now;
    string Supplier_PayBack_Apply_Note = "";
    string Supplier_PayBack_Apply_AdminNote = "";
    int Supplier_PayBack_Apply_Type = 0;
    double Supplier_PayBack_Apply_Amount = 0;
    int Supplier_PayBack_Apply_Status = 0;
    double Supplier_PayBack_Apply_AdminAmount = 0;
    string Product_Name = "";
    int Apply_ID = tools.CheckInt(Request["Apply_ID"]);
    if (Apply_ID == 0)
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/Supplier_PayBack_Apply.aspx");
    }
    SupplierPayBackApplyInfo entity = supplier.GetSupplierPayBackApplyByID(Apply_ID);
    if (entity != null)
    {
        Apply_ID = entity.Supplier_PayBack_Apply_ID;
        Supplier_PayBack_Apply_Amount = entity.Supplier_PayBack_Apply_Amount;
        Supplier_PayBack_Apply_Note = entity.Supplier_PayBack_Apply_Note;
        Supplier_PayBack_Apply_Addtime = entity.Supplier_PayBack_Apply_Addtime;
        Supplier_PayBack_Apply_Status = entity.Supplier_PayBack_Apply_Status;
        Supplier_PayBack_Apply_Type = entity.Supplier_PayBack_Apply_Type;
        Supplier_PayBack_Apply_AdminAmount = entity.Supplier_PayBack_Apply_AdminAmount;
        Supplier_PayBack_Apply_AdminNote = entity.Supplier_PayBack_Apply_AdminNote;
        Supplier_PayBack_Apply_AdminTime = entity.Supplier_PayBack_Apply_AdminTime;
    }
    else
    {
        pub.Msg("error", "错误信息", "记录不存在", false, "/supplier/Supplier_PayBack_Apply.aspx");
    }
%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title><%="账户退款申请 - 会员中心 - " + pub.SEO_TITLE()%></title>
         <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
 
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    <script src="/scripts/member.js" type="text/javascript"></script>
    <script src="/scripts/jquery-extend-AdAdvance3.js" type="text/javascript"></script>
   
</head>
<body>

    <uctop:top ID="top1" runat="server" />
    

    
    <!--主体 开始-->
<div class="content">
      <!--位置说明 开始-->
      <div class="position">
		   您现在的位置 > <a href="/">首页</a> > <a href="index.aspx">会员中心</a> > <a href="Supplier_PayBack_Apply.aspx">账户退款申请</a>
	</div>
      <div class="clear"></div>
      <!--位置说明 结束-->
      <div class="parth">
            <div class="ph_left">
                   <% supplier.Get_Supplier_Left_HTML(1, 11); %>
            </div>
            <div class="ph_right">
                  <div class="blk13">
                        <h2>账户退款申请</h2>
                        <div class="main">
                             
                     <form name="formadd" method="post" action="/supplier/Supplier_PayBack_Apply_do.aspx">
      <table width="100%" border="0" cellspacing="0" cellpadding="5" id="apply_1_content" class="table_padding_5">
            <tr>
                <td align="right" width="100" style="line-height: 30px;" class="t12_53">
                    申请退款类型：
                </td>
                <td align="left">
                    <%
                        if (Supplier_PayBack_Apply_Type == 1)
                        {
                            Response.Write("会员费");
                        }
                        else if (Supplier_PayBack_Apply_Type == 2)
                        {
                            Response.Write("保证金");
                        }
                        else
                        {
                            Response.Write("推广费");
                        }
                         %>
                </td>
            </tr>
            <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    退款申请状态：
                </td>
                <td align="left">
                    <%
                        if (Supplier_PayBack_Apply_Status == 1)
                        {
                            Response.Write("已审核");
                        }
                        else if (Supplier_PayBack_Apply_Status == 2)
                        {
                            Response.Write("审核不通过");
                        }
                        else
                        {
                            Response.Write("待审核");
                        }
                        %>
                </td>
            </tr>
            <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    申请退款金额：
                </td>
                <td align="left">
                    <%=pub.FormatCurrency(Supplier_PayBack_Apply_Amount)%>
                </td>
            </tr>
            <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    申请退款备注：
                </td>
                <td align="left">
                    <%=Supplier_PayBack_Apply_Note%>
                    
                </td>
            </tr>
            <%if(Supplier_PayBack_Apply_Status>0)
              { %>
            <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    实际退款金额：
                </td>
                <td align="left">
                    <%=pub.FormatCurrency(Supplier_PayBack_Apply_AdminAmount)%>
                </td>
            </tr>
            <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    平台备注说明：
                </td>
                <td align="left">
                    <%=Supplier_PayBack_Apply_AdminNote%>
                    
                </td>
            </tr>
            <tr>
                <td align="right" style="line-height: 24px;" class="t12_53">
                    处理时间：
                </td>
                <td align="left">
                    <%=Supplier_PayBack_Apply_AdminTime%>
                    
                </td>
            </tr>
            <%} %>
          </table>

          
            </form>
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
