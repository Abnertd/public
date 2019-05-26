<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<% 
    string Supplier_CompanyName = "";
    string Supplier_Email = "";
    string Supplier_County = "";
    string Supplier_City = "";
    string Supplier_State = "";
    string Supplier_Address = "";
    string Supplier_Phone = "";
    string Supplier_Fax = "";
    string Supplier_Zip = "";
    string Supplier_Contactman = "";
    string Supplier_Mobile = "";
    double Supplier_Account = 0.00;
    double Supplier_Adv_Account = 0.00;
    double Supplier_Security_Account = 0.00;
    int Supplier_Type = 0;
    DateTime Supplier_Addtime = DateTime.Now;
    Supplier Supplier = new Supplier();
    Shop shop = new Shop();
    int Supplier_Shop_ApplyID;
    ITools tools;
    Addr addr = new Addr();
    tools = ToolsFactory.CreateTools();
    Supplier_Shop_ApplyID = tools.CheckInt(Request["Supplier_Shop_ApplyID"]);
    SupplierShopApplyInfo entity = Supplier.GetSupplierShopApplyByID(Supplier_Shop_ApplyID);
    if (entity == null)
    {
        Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
        Response.End();
    }
    else
    {
        SupplierInfo supplierinfo = Supplier.GetSupplierByID(entity.Shop_Apply_SupplierID);
        if (supplierinfo != null)
        {
            Supplier_CompanyName = supplierinfo.Supplier_CompanyName;
            Supplier_Type = supplierinfo.Supplier_Type;
            Supplier_Email = supplierinfo.Supplier_Email;
            Supplier_County = supplierinfo.Supplier_County;
            Supplier_City = supplierinfo.Supplier_City;
            Supplier_State = supplierinfo.Supplier_State;
            Supplier_Phone = supplierinfo.Supplier_Phone;
            Supplier_Fax = supplierinfo.Supplier_Fax;
            Supplier_Zip = supplierinfo.Supplier_Zip;
            Supplier_Address = supplierinfo.Supplier_Address;
            Supplier_Contactman = supplierinfo.Supplier_Contactman;
            Supplier_Mobile = supplierinfo.Supplier_Mobile;
            Supplier_Account = supplierinfo.Supplier_Account;
            Supplier_Adv_Account = supplierinfo.Supplier_Adv_Account;
            Supplier_Security_Account = supplierinfo.Supplier_Security_Account;

            Supplier_Addtime = supplierinfo.Supplier_Addtime;
        }
    }
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>供应商店铺开通申请</title>
    <link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
    <script src="/Scripts/jquery.js" type="text/javascript"></script>
    <script src="/Scripts/common.js" type="text/javascript"></script>
    <link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
    <script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
    <script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        change_inputcss();
        btn_scroll_move();
    </script>
</head>
<body>
    <div class="content_div">
        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">供应商信息</td>
            </tr>
            <tr>
                <td class="content_content">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">

                        <tr>
                            <td class="cell_title">供应商名称</td>
                            <td class="cell_content" width="30%"><%=Supplier_CompanyName %></td>
                            <td class="cell_title">注册邮箱</td>
                            <td class="cell_content"><%=Supplier_Email %></td>
                        </tr>
                        <tr>
                            <td class="cell_title">注册时间</td>
                            <td class="cell_content"><%=Supplier_Addtime %></td>
                            <td class="cell_title">所属城市</td>
                            <td class="cell_content"><%=addr.DisplayAddress(Supplier_State,Supplier_City,Supplier_County) %></td>
                        </tr>
                        <tr>
                            <td class="cell_title">地址</td>
                            <td class="cell_content"><%=Supplier_Address %></td>
                            <td class="cell_title">邮政编码</td>
                            <td class="cell_content"><%=Supplier_Zip %></td>
                        </tr>
                        <tr>
                            <td class="cell_title">联系人</td>
                            <td class="cell_content"><%=Supplier_Contactman %></td>
                            <td class="cell_title">手机</td>
                            <td class="cell_content"><%=Supplier_Mobile%></td>
                        </tr>
                        <tr>
                            <td class="cell_title">电话</td>
                            <td class="cell_content"><%=Supplier_Phone %></td>
                            <td class="cell_title">传真</td>
                            <td class="cell_content"><%=Supplier_Fax %></td>
                        </tr>

                    </table>

                </td>
            </tr>
        </table>

        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
            <tr>
                <td class="content_title">店铺申请信息&nbsp;<span style="color: #cc0000;">[<%if (entity.Shop_Apply_Status == 0)
                                                                                       {%>未审核<%}
                                                                                       else if (entity.Shop_Apply_Status == 2)
                                                                                       { %>审核未通过<%}
                                                                                       else
                                                                                       {%>审核通过<%} %>]</span></td>
            </tr>
            <tr>
                <td class="content_content">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
                        <tr>
                            <td class="cell_title">店铺名称</td>
                            <td class="cell_content" width="30%"><%=entity.Shop_Apply_ShopName %></td>
                            <td class="cell_title">申请人身份证号</td>
                            <td class="cell_content"><%=entity.Shop_Apply_PINCode %></td>
                        </tr>
                        <tr>
                            <td class="cell_title">手机</td>
                            <td class="cell_content"><%=entity.Shop_Apply_Mobile %></td>
                            <td class="cell_title">申请人真实姓名</td>
                            <td class="cell_content"><%=entity.Shop_Apply_Name %></td>
                        </tr>
                        <tr>
                            <td class="cell_title">公司法人代表</td>
                            <td class="cell_content"><%=entity.Shop_Apply_Lawman %></td>
                            <td class="cell_title">公司类型</td>
                            <td class="cell_content"><%=entity.Shop_Apply_CompanyType %></td>
                        </tr>
                        <tr>
                            <td class="cell_title">公司地址</td>
                            <td class="cell_content"><%=entity.Shop_Apply_CompanyAddress%></td>
                            <td class="cell_title">公司联系电话</td>
                            <td class="cell_content"><%=entity.Shop_Apply_CompanyPhone %></td>

                        </tr>
                        <tr>
                            <td class="cell_title">营业执照号码</td>
                            <td class="cell_content"><%=entity.Shop_Apply_CertCode %></td>
                            <td class="cell_title">营业执照所在地</td>
                            <td class="cell_content"><%=entity.Shop_Apply_CertAddress %></td>
                        </tr>
                        <tr>
                            <td class="cell_title">主营品牌</td>
                            <td class="cell_content"><%=entity.Shop_Apply_MainBrand %></td>
                            <%--<td class="cell_title">店铺类型</td>
          <td class="cell_content"><%=shop.Get_Shop_Type(entity.Shop_Apply_ShopType)%></td>--%>
                            <td class="cell_title"></td>
                            <td class="cell_content"></td>
                        </tr>


                    </table>

                </td>
            </tr>
        </table>


        <%--  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">资质信息</td>
    </tr>
    <tr>
      <td class="content_content">
      <div id="cert_show" style=" position:absolute; border:1px solid #000; display:none;" ></div>
      <div id="cert_compare"></div>
      <script>
      function show_cert(url)
      {
      $("#cert_show").html('<img src='+url+' width="600">')
      $("#cert_show").show();
        var ojbfoot=$("#cert_compare").offset().top-$("#cert_show").height()-10;
        var ojbleft=$("#cert_compare").width()/2-($("#cert_show").width()/2);
        $("#cert_show").css("top",ojbfoot);
        $("#cert_show").css("left",ojbleft);
      }
      
      </script>
      <table width="100%" border="0" cellpadding="5" cellspacing="0">
       <tr>
       
       <td width="20%" align="center"><img src="<% =Public.FormatImgURL(entity.Shop_Apply_Certification1, "fullpath") %>" width="200" height="200" onmouseover="show_cert('<% =Public.FormatImgURL(entity.Shop_Apply_Certification1, "fullpath") %>')" onmouseout="$('#cert_show').hide();"/></td>
       <td width="20%" align="center"><img src="<% =Public.FormatImgURL(entity.Shop_Apply_Certification2, "fullpath") %>" width="200" height="200" onmouseover="show_cert('<% =Public.FormatImgURL(entity.Shop_Apply_Certification2, "fullpath") %>')"  onmouseout="$('#cert_show').hide();"/></td>
       <td width="20%" align="center"><img src="<% =Public.FormatImgURL(entity.Shop_Apply_Certification3, "fullpath") %>" width="200" height="200" onmouseover="show_cert('<% =Public.FormatImgURL(entity.Shop_Apply_Certification3, "fullpath") %>')" onmouseout="$('#cert_show').hide();"/></td>
       <td width="20%" align="center"></td>
       <td width="20%" align="center"></td>
        </tr>
        <tr>
       
       <td width="20%" style="text-align:center;" class="cell_title">营业执照</td>
       <td width="20%" style="text-align:center;" class="cell_title">税务登记证</td>
       <td width="20%" style="text-align:center;" class="cell_title">组织机构代码证</td>
       <td width="20%" align="center"></td>
       <td width="20%" align="center"></td>
        </tr>
        
      
      </table>
    
        </td>
    </tr>
  </table>--%>
        <form method="post" action="Supplier_Do.aspx">
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
                <tr>
                    <td class="content_title">审核备注</td>
                </tr>
                <tr>
                    <td class="content_content">
                        <%if (entity.Shop_Apply_Status == 0)
                          { %>
                        <textarea name="Shop_Apply_Note" cols="80" rows="8"><%=entity.Shop_Apply_Note%></textarea>
                        最多不超过200个字符
    <%}
                          else
                          {
                              Response.Write(entity.Shop_Apply_Note + "<br><br>");
                          } %>
                    </td>
                </tr>
            </table>

            <div class="foot_gapdiv">
            </div>
            <div class="float_option_div" id="float_option_div">
                <%if (entity.Shop_Apply_Status == 0)
                  { %>
                <input type="submit" name="Audit" class="bt_orange" value="审核通过" onclick="$('#hd_action').val('applypassaudit');" />
                <input type="submit" name="NotAudit" class="bt_orange" value="审核不通过" onclick="$('#hd_action').val('applynotpassaudit');" />
                <input type="hidden" id="hd_action" name="action" value="applypassaudit" />
                <input type="hidden" id="Hidden1" name="Supplier_Shop_ApplyID" value="<%=Supplier_Shop_ApplyID %>" />
                <%} %>
                <input name="button" type="button" class="bt_orange" id="button1" value="返回" onclick="location = 'Supplier_Shop_Apply.aspx';" />
            </div>
        </form>
    </div>
</body>
</html>
