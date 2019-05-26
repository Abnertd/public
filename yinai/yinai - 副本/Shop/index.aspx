<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.ORM" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Register Src="~/public/top.ascx" TagName="top" TagPrefix="uctop" %>
<%@ Register Src="~/public/bottom.ascx" TagName="bottom" TagPrefix="ucbottom" %>

<%@ Register Src="~/public/left.ascx" TagName="left" TagPrefix="ucleft" %>

<% 
    ITools tools = ToolsFactory.CreateTools();
    Public_Class pub = new Public_Class();
    Product product = new Product();
    Shop shop = new Shop();
    Glaer.Trade.B2C.BLL.MEM.IMember MyMember = Glaer.Trade.B2C.BLL.MEM.MemberFactory.CreateMember();
    Glaer.Trade.B2C.BLL.MEM.ISupplier supplier=Glaer.Trade.B2C.BLL.MEM.SupplierFactory.CreateSupplier();
    shop.Shop_Initial();
    string index_content1 = "";
    string Member_Company_Introduce = "";
    string Member_Company_Contact = "";
    SupplierShopPagesInfo pageinfo;

    int cate_id = tools.CheckInt(Request["cate_id"]);
    //MemberInfo memberinfo = new Member().GetMemberByID();
   string orderby = tools.CheckStr(Request["orderby"]);
   


  int supplier_id=  tools.NullInt(Session["shop_supplier_id"]);
  SupplierInfo supplierinfo = supplier.GetSupplierByID(supplier_id, pub.CreateUserPrivilege("1392d14a-6746-4167-804a-d04a2f81d226"));
  if (supplierinfo!=null)
  {
      QueryInfo Query = new QueryInfo();
      Query.PageSize = 0;
      Query.CurrentPage = 1;
      Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_SupplierID", "=", supplierinfo.Supplier_ID.ToString()));
      Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberInfo.Member_Site", "=", "CN"));
      Query.ParamInfos.Add(new ParamInfo("AND", "int", "MemberInfo.Member_Trash", "=", "0"));
      IList<MemberInfo> entityList = MyMember.GetMembers(Query, pub.CreateUserPrivilege("3a9a9cdf-ef00-407d-98ef-44e23be397e8"));
      if (entityList!=null)
      {
          int i = 0;
          if (i==0)
          {
              foreach (MemberInfo MemberInfo in entityList)
              {
                  i++;
                  Member_Company_Introduce =Server.HtmlDecode( MemberInfo.Member_Company_Introduce);
                  Member_Company_Contact = MemberInfo.Member_Company_Contact;
              }  
          }
         
      }
  }

   
    
    SupplierShopPagesInfo pageinfo1 = shop.GetSupplierShopPagesByIDSign("Index", tools.NullInt(Session["shop_supplier_id"]));
    if (pageinfo1 != null)
    {
        index_content1 = pageinfo1.Shop_Pages_Content;
        if (index_content1.Length>0)
        {
            
        }
        else
        {
          
          
             index_content1 = "<img src=\"http://img.easynai.com/shop/banner/default/img18.jpg\" />";
        
        }

    }
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%=shop.Shop_Name + shop.Shop_Title %></title>
       
    <meta name="Keywords" content="<%=shop.Shop_Keyword%>" />
    <meta name="description" content="<%=shop.Shop_Intro%>" />

    <link href="css/index0<%=shop.Shop_Css %>.css" rel="stylesheet" type="text/css" /> 
    <link href="css/Scroll_Shop.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js"></script>
    <script type="text/javascript" src="/scripts/common.js"></script>
    <style type="text/css">
        .pl_left .index_left_img img {
            width: 210px;
        }

        .list_06_text div p {
            color: #666;
            font-size: 14px;
            margin: 6px;
        }
    </style>

</head>
<body>
    <uctop:top ID="top" runat="server" />

    <%
        if (shop.Shop_Top_IsActive == 1)
        {
            string banner_url = "/shop/banner/default/banner2.png";
            string index_top = string.Empty;
            pageinfo = shop.GetSupplierShopPagesByIDSign("INDEXTOP", tools.NullInt(Session["shop_supplier_id"]));
            Response.Write("<div class=\"sz_head\">");
            if (pageinfo != null)
            {
                index_top = pageinfo.Shop_Pages_Content;
                if (index_top.Length>0)
                {
                    Response.Write("    <div class=\"bannner\">" + index_top + "</div>");
                }

                else
                {
                   

                    Response.Write("<div class=\"ad03\"  style=\"background-image:url(" + pub.FormatImgURL(banner_url, "fullpath") + "); background-repeat:round; background-position:top center; height:348px; width:1200px; margin:0 auto; \">");

                    Response.Write("	<div class=\"ad03_main\" style=\" background-image:url(" + pub.FormatImgURL(banner_url, "fullpath") + "); background-repeat:round; background-position:top center; height:348px; width:1200px; margin:0 auto; \"></div>");
                    Response.Write( "</div>");
                }

                //banner_url = "/shop/banner/default/banner1.png";
                
            }
            else
            {
                Response.Write("    <div class=\"bannner\">" + index_top + "</div>");
            }

         
           
            Response.Write("</div>");
        } 
    %>

    <!--主体 开始-->
    <div class="content02" style="background-color: #FFF;">
        <div class="content02_main" style="background-color: #FFF;">
            <div class="partl" style="margin-top: 15px;">

                <div class="pl_left">
                    <ucleft:left ID="left" runat="server" />

                </div>



                <div class="pl_right">
                    <!--轮播 开始-->
                    <%if (shop.Shop_Right_IsActive == 1)
                      { %>
                    <div class="dp_fla" style="overflow: hidden; width: 975px;">
                        <%=index_content1%>
                    </div>
                    <%} %>


                      <div class="list_06_text" style="margin-bottom: 14px; border: 1px solid #cccccc; font-size: 15px; margin-top:15px;">
                        <div>
                            <h3 style="border-bottom: 1px solid #211212; padding: 5px; ">公司介绍</h3>
                          
                            <p> <%=Member_Company_Introduce %> </p>
                          
                        </div>
                    </div>


                    <!--轮播 结束-->
                    <%if (shop.Shop_RightProduct_IsActive == 1)
                      { %>
                    <!--列表 开始-->
                    <% product.Home_Product_List(16); %>
                    <!--列表 结束-->
                    <%} %>


                       <div class="blk30" style="margin-top: 15px;">
                        <h2 style="font-size: 14px;" id="CurPosition">所有商品</h2>
                        <% shop.product_filter_Home(); %>
                    </div>

                    <!--排序 开始-->
                    <div class="blk31">
                        <%=shop.Product_View_Mode_Home() %>
                        <div class="clear"></div>
                    </div>
                    <!--排序 结束-->

                    <!--列表 开始-->
                    <div class="list_06" style="width: 989px;">
                        <% shop.product_list_Home("", cate_id, 4); %>
                    </div>
                    <!--列表 结束-->

                </div>
                <div class="clear"></div>
            </div>
        </div>
    </div>
    <!--主体 结束-->


    <ucbottom:bottom ID="bottom" runat="server" />

</body>
</html>
