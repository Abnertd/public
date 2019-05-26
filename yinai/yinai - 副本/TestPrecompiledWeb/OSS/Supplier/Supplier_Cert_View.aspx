<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<% Public.CheckLogin("b4fd0773-e353-43ab-aa6b-3096bc0981f3");
   Supplier Supplier = new Supplier();
   int Supplier_id;
   string Supplier_Bank_Name = "";
   string Supplier_Bank_NetWork = "";
   string Supplier_Bank_SName = "";
   ITools tools;
   Addr addr = new Addr();
   tools = ToolsFactory.CreateTools();
   Supplier_id = tools.CheckInt(Request["Supplier_id"]);
   SupplierInfo supplierinfo = Supplier.GetSupplierByID(Supplier_id);
   string Supplier_Cert;
   int Supplier_Cert_Status, Supplier_CertType;
   IList<SupplierRelateCertInfo> relateinfos = null;
   Supplier_Cert_Status = 0;
   Supplier_CertType = 0;
   if (supplierinfo == null)
   {
       Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
       Response.End();
   }
   else
   {
       Supplier_CertType = supplierinfo.Supplier_CertType;

       relateinfos = supplierinfo.SupplierRelateCertInfos;
       Supplier_Cert_Status = supplierinfo.Supplier_Cert_Status;
   }
  
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>供应商资质信息</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ChangeSupplierAuditStatus(obj) {
            $("#formadd").attr("action", "Supplier_Do.aspx?action=" + obj);
            document.formadd.submit();
        }
    </script>
    <script type="text/javascript">
        change_inputcss();
        btn_scroll_move();
    </script>
</head>
<body>
    <div class="content_div">
        <form id="formadd" name="formadd" method="post" action="/Supplier/Supplier_Do.aspx">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
                <tr>
                    <td class="content_title">供应商资质信息</td>
                </tr>
                <tr>
                    <td class="content_content">




                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
                            <tr>
                                <td class="content_title">资质信息&nbsp;<%--<span style="color:#cc0000;">[<%=Supplier.Get_Cert_Type(Supplier_CertType)%>]</span>&nbsp;--%><span style="color: #cc0000;">[<%=Supplier.Get_Cert_Status(Supplier_Cert_Status)%>]</span></td>
                            </tr>
                            <tr>
                                <td class="content_content">
                                    <table width="100%" border="0" cellpadding="5" cellspacing="0">

                                        <%
                                            IList<SupplierCertInfo> certs = null;
                                            certs = Supplier.GetSupplierCertByType(Supplier_CertType);
                                            if (certs != null)
                                            {
                                                Response.Write("<tr>");
                                                foreach (SupplierCertInfo entity in certs)
                                                {
                                                    if (Supplier_Cert_Status == 1)
                                                    {
                                                        Supplier_Cert = Supplier.Get_Supplier_Certtmp(entity.Supplier_Cert_ID, relateinfos);
                                                    }
                                                    else
                                                    {
                                                        Supplier_Cert = Supplier.Get_Supplier_Cert(entity.Supplier_Cert_ID, relateinfos);
                                                    }
                                        %>

                                        <td width="<%=100/certs.Count %>%" align="center">
                                            <img src="<% =Public.FormatImgURL(Supplier_Cert, "fullpath") %>" width="200" height="200" onmouseover="show_cert('<% =Public.FormatImgURL(Supplier_Cert, "fullpath") %>');" onmouseout="$('#cert_show').hide();" /></td>
                                        <%}
           Response.Write("</tr>");
           Response.Write("<tr>");
           foreach (SupplierCertInfo entity in certs)
           {
               Response.Write("<td style=\"text-align:center;\" class=\"cell_title\">" + entity.Supplier_Cert_Name + "</td>");
           }
           Response.Write("</tr>");
       } %>
                                    </table>

                                    <div id="cert_show" style="position: absolute; z-index: 1000; border: 1px solid #000; display: none;" onmouseover="$(this).show()" onmouseout="$(this).hide()"></div>
                                    <div id="cert_compare"></div>
                                    <script>
                                        function show_cert(url) {
                                            $("#cert_show").html('<img src=' + url + ' onload=AutosizeImage(this,600,600) height=600 width=600>')
                                            $("#cert_show").show();
                                            var ojbfoot = 100;

                                            var ojbleft = $("#cert_compare").width() / 2 - ($("#cert_show").width() / 2);
                                            $("#cert_show").css("top", ojbfoot);
                                            $("#cert_show").css("left", ojbleft);
                                        }

                                    </script>
                                </td>
                            </tr>
                        </table>



                        <div class="foot_gapdiv">
                        </div>
     <%--   <div class="float_option_div" id="float_option_div">
                            <%if (supplierinfo.Supplier_Cert_Status == 1 && Public.CheckPrivilege("b4fd0773-e353-43ab-aa6b-3096bc0981f3"))
                              { %>
                            <input type="button" name="Audit" class="bt_orange" value="审核通过" onclick="ChangeSupplierAuditStatus('certaudit');" />
                            <input type="button" name="NotAudit" class="bt_orange" value="审核不通过" onclick="ChangeSupplierAuditStatus('certdeny');" />

                            <%}  %>
                            <input type="hidden" id="Supplier_id" name="Supplier_id" value="<%=Supplier_id %>" />
                        </div>--%>
        </form>

    </div>
</body>
</html>
