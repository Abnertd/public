<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>

<%
        ITools tools;
        Promotion myApp;
        string product_id,keyword;
        string cate_id, brand_id, delivery_id, payway_id, member_id;
        int select_cate, product_cate;
        tools = ToolsFactory.CreateTools();
        myApp = new Promotion();
        string action = Request["action"];
        string tag = Request["tag"];
        switch (action)
        {
            case "showcate":
                cate_id = tools.NullStr(Session["selected_cateid"]);
                if (cate_id.Length > 0)
                {
                    Response.Write(myApp.ShowCategory(cate_id));
                }
                else
                {
                    Response.Write("<span class=\"pickertip\">已选择类别</span>");
                }
                break;
            case "delcate":
                cate_id = tools.CheckStr(Request.QueryString["cate_id"]);
                select_cate = tools.CheckInt(Request["cid"]); 
                if (cate_id.Length > 0)
                {
                    Response.Write(myApp.ShowCategory(cate_id));
                }
                else
                {
                    Response.Write("<span class=\"pickertip\">已选择类别</span>");
                }
                break;
            case "savecateid":
                Session["selected_cateid"] = tools.NullStr(Request["cate_id"]);
                if (tools.NullStr(Request["cate_id"]) == "0,")
                {
                    Session["selected_cateid"] = "";
                }
                break;
            case "brandlist":
                Response.Write(myApp.SelectBrand());
                Response.End();
                break;
            case "refresh_brand":
                brand_id = tools.NullStr(Session["selected_brandid"]);
                keyword = Request["keyword"];
                string cateid = Request["cateid"];
                Response.Write("<input type=\"hidden\" id=\"all_flag\" value=\"0\" />");
                Response.Write("<input type=\"hidden\" id=\"allids\" value=\""+myApp.Get_BrandList_IDs()+"\" />");
                Response.Write("<input type=\"hidden\" id=\"selarrow\" value=\"0," + brand_id + "\" />");
                Response.Write("<div class=\"list_tip_div\" id=\"list_seltip\"></div>");
                Response.Write("<table id=\"list\"></table>");
                Response.Write("<div id=\"pager\"></div>");
                Response.Write("<script type=\"text/javascript\">");
                Response.Write("jQuery(\"#list\").jqGrid({");
                Response.Write("url: 'picker_do.aspx?action=brandlist&cateid=" + cateid + "&keyword=" + Server.UrlEncode(keyword) + "',");
			    Response.Write("    datatype: \"json\",");
                Response.Write("    colNames: ['ID','商品品牌名称'],");
                Response.Write("    colModel: [");
                Response.Write("        {width:30,align:'center', name: 'id', index: 'id',sortable:false},");
				Response.Write("        {align:'left', name: 'BrandInfo.Brand_Name', index: 'BrandInfo.Brand_Name'}");
			    Response.Write("    ],");
                Response.Write("    sortname: 'BrandInfo.Brand_ID',");
			    Response.Write("    sortorder: \"desc\",");
			    Response.Write("    rowNum: 10,");
			    //Response.Write("    rowList:[10,20,40], ");
			    Response.Write("    pager: 'pager', ");
			    Response.Write("    multiselect: true,");
                Response.Write("    viewrecords:true,");
			    Response.Write("    viewsortcols: [false,'horizontal',true],");
			    Response.Write("    width: 597,");
			    Response.Write("    height: \"100%\",");
                Response.Write("    onSelectRow: function(id,status){  ");
                Response.Write("    jqgrid_rowclick(id,status);");
                Response.Write("    jqgrid_seltip_display();");
                Response.Write("    }, ");
			    Response.Write("    loadComplete:function(){");
                Response.Write("        jqgrid_selarry();");
                Response.Write("        jqgrid_seltip_display();");
                Response.Write("    }");
            
                Response.Write("    });");
                Response.Write("    jqgrid_allclick();");
                Response.Write("</script>");
                break;
            case "showbrand":
                brand_id = tools.NullStr(Session["selected_brandid"]);
                if (brand_id.Length > 0)
                {
                    Response.Write(myApp.ShowBrand(brand_id));
                }
                else
                {
                    Response.Write("<span class=\"pickertip\">已选择品牌</span>");
                }
                break;
            case "savebrandid":
                Session["selected_brandid"] = tools.NullStr(Request["brandid"]);
                if (tools.NullStr(Request["brandid"]) == "0,")
                {
                    Session["selected_brandid"] = "";
                }
                break;
            case "memberlist":
                Response.Write(myApp.SelectMember());
                Response.End();
                break;
            case "refresh_member":
                keyword = Request["keyword"];
                member_id = tools.NullStr(Session["selected_memberid"]);
                Response.Write("<input type=\"hidden\" id=\"all_flag\" value=\"0\" />");
                Response.Write("<input type=\"hidden\" id=\"allids\" value=\"" + myApp.Get_MemberList_IDs() + "\" />");
                Response.Write("<input type=\"hidden\" id=\"selarrow\" value=\"0," + member_id + "\" />");
                Response.Write("<div class=\"list_tip_div\" id=\"list_seltip\"></div>");
                Response.Write("<table id=\"list\"></table>");
                Response.Write("<div id=\"pager\"></div>");
                Response.Write("<script type=\"text/javascript\">");
                Response.Write("jQuery(\"#list\").jqGrid({");
                Response.Write("url: 'picker_do.aspx?action=memberlist&keyword=" + Server.UrlEncode(keyword) + "',");
                Response.Write("    datatype: \"json\",");
                Response.Write("    colNames: ['ID','昵称','注册邮箱','会员等级'],");
                Response.Write("    colModel: [");
                Response.Write("        {width:30,align:'center', name: 'id', index: 'id',sortable:false},");
                Response.Write("        {align:'left', name: 'MemberInfo.Member_NickName', index: 'MemberInfo.Member_NickName'},");
                Response.Write("        {align:'left', name: 'MemberInfo.Member_Email', index: 'MemberInfo.Member_Email'},");
                Response.Write("        {align:'left', name: 'MemberInfo.Member_Grade', index: 'MemberInfo.Member_Grade'}");
                Response.Write("    ],");
                Response.Write("    sortname: 'MemberInfo.Member_ID',");
                Response.Write("    sortorder: \"desc\",");
                Response.Write("    rowNum: 10,");
                //Response.Write("    rowList:[10,20,40], ");
                Response.Write("    pager: 'pager', ");
                Response.Write("    multiselect: true,");
                Response.Write("    viewrecords:true,");
                Response.Write("    viewsortcols: [false,'horizontal',true],");
                Response.Write("    width: 597,");
                Response.Write("    height: \"100%\",");
                Response.Write("    onSelectRow: function(id,status){  ");
                Response.Write("    jqgrid_rowclick(id,status);");
                Response.Write("    jqgrid_seltip_display();");
                Response.Write("    }, ");
                Response.Write("    loadComplete:function(){");
                Response.Write("        jqgrid_selarry();");
                Response.Write("        jqgrid_seltip_display();");
                Response.Write("    }");

                Response.Write("    });");
                Response.Write("    jqgrid_allclick();");
                Response.Write("</script>");
                break;
            case "showmember":
                member_id = tools.NullStr(Session["selected_memberid"]);
                if (member_id.Length > 0)
                {
                    Response.Write(myApp.ShowMember(member_id));
                }
                else
                {
                    Response.Write("<span class=\"pickertip\">已选择会员</span>");
                }
                break;
            case "savememberid":
                Session["selected_memberid"] = tools.NullStr(Request["memberid"]);
                if (tools.NullStr(Request["memberid"]) == "0,")
                {
                    Session["selected_memberid"] = "";
                }
                break;
            case "productlist":
                Response.Write(myApp.SelectProduct());
                Response.End();
                break;
            case "refresh_product":
                keyword = Request["keyword"];
                product_cate = 0;
                if (tools.NullInt(Request["product_cate"]) == 0)
                {
                    product_cate = tools.NullInt(Request["product_cate_parent"]);
                }
                else
                {
                    product_cate = tools.NullInt(Request["product_cate"]);
                }
                product_id = tools.NullStr(Session["selected_productid"]);
                Response.Write("<input type=\"hidden\" id=\"all_flag\" value=\"0\" />");
                Response.Write("<input type=\"hidden\" id=\"allids\" value=\"" + myApp.Get_ProductList_IDs() + "\" />");
                Response.Write("<input type=\"hidden\" id=\"selarrow\" value=\"0," + product_id + "\" />");
                Response.Write("<div class=\"list_tip_div\" id=\"list_seltip\"></div>");
                Response.Write("<table id=\"list\"></table>");
                Response.Write("<div id=\"pager\"></div>");
                Response.Write("<script type=\"text/javascript\">");
                Response.Write("jQuery(\"#list\").jqGrid({");
                Response.Write("url: '/promotion/picker_do.aspx?action=productlist&product_cate=" + product_cate + "&tag="+tag+"&keyword=" + Server.UrlEncode(keyword) + "',");
                Response.Write("    datatype: \"json\",");
                Response.Write("    colNames: ['ID','商品编码','商品名称','生产企业'],");
                Response.Write("    colModel: [");
                Response.Write("        {width:30,align:'center', name: 'id', index: 'id',sortable:false},");
                Response.Write("        {align:'left', name: 'ProductInfo.Product_Code', index: 'ProductInfo.Product_Code',sortable:false},");
                Response.Write("        {align:'left', name: 'ProductInfo.Product_Name', index: 'ProductInfo.Product_Name',sortable:false},");
                Response.Write("        {align:'left', name: 'ProductInfo.Product_Maker', index: 'ProductInfo.Product_Maker',sortable:false}");
                Response.Write("    ],");
                Response.Write("    sortname: 'ProductInfo.Product_ID',");
                Response.Write("    sortorder: \"desc\",");
                Response.Write("    rowNum: 10,");
                //Response.Write("    rowList:[10,20,40], ");
                Response.Write("    pager: 'pager', ");
                Response.Write("    multiselect: true,");
                Response.Write("    viewrecords:true,");
                Response.Write("    viewsortcols: [false,'horizontal',true],");
                Response.Write("    width: 597,");
                Response.Write("    height: \"100%\",");
                Response.Write("    onSelectRow: function(id,status){  ");
                Response.Write("    jqgrid_rowclick(id,status);");
                Response.Write("    jqgrid_seltip_display();");
                Response.Write("    }, ");
                Response.Write("    loadComplete:function(){");
                Response.Write("        jqgrid_selarry();");
                Response.Write("        jqgrid_seltip_display();");
                Response.Write("    }");

                Response.Write("    });");
                Response.Write("    jqgrid_allclick();");
                Response.Write("</script>");
                break;
            case "showproduct":
                product_id = tools.NullStr(Session["selected_productid"]);
                int limit,group;
                limit = tools.CheckInt(Request["limit"]);
                group = tools.CheckInt(Request["group"]);
                if (group == 0)
                {
                    if (limit == 0)
                    {
                        if (product_id.Length > 0)
                        {
                            Response.Write(myApp.ShowProduct(product_id));
                        }
                        else
                        {
                            Response.Write("<span class=\"pickertip\">已选择产品</span>");
                        }
                    }
                    else
                    {
                        Response.Write(myApp.Limit_ShowProduct());
                    }
                }
                else
                {
                    Response.Write(myApp.WholeSale_ShowProduct());
                }
                break;
            case "saveproductid":
                Session["selected_productid"] = tools.NullStr(Request["productid"]);
                if (tools.NullStr(Request["productid"]) == "0,")
                {
                    Session["selected_productid"] = "";
                }
                break;
            case "deliverylist":
                Response.Write(myApp.SelectDelivery());
                Response.End();
                break;
            case "refresh_delivery":
                keyword = Request["keyword"];
                Response.Write("<input type=\"hidden\" id=\"all_flag\" value=\"0\" />");
                Response.Write("<input type=\"hidden\" id=\"allids\" value=\"" + myApp.Get_Delivery_IDs() + "\" />");
                Response.Write("<input type=\"hidden\" id=\"selarrow\" value=\"0,\" />");
                Response.Write("<div class=\"list_tip_div\" id=\"list_seltip\"></div>");
                Response.Write("<table id=\"list\"></table>");
                Response.Write("<div id=\"pager\"></div>");
                Response.Write("<script type=\"text/javascript\">");
                Response.Write("jQuery(\"#list\").jqGrid({");
                Response.Write("url: 'picker_do.aspx?action=deliverylist&keyword=" + Server.UrlEncode(keyword) + "',");
                Response.Write("    datatype: \"json\",");
                Response.Write("    colNames: ['ID','配送方式名称'],");
                Response.Write("    colModel: [");
                Response.Write("        {width:30,align:'center', name: 'id', index: 'id',sortable:false},");
                Response.Write("        {align:'left', name: 'DeliveryWayInfo.Delivery_Way_Name', index: 'DeliveryWayInfo.Delivery_Way_Name',sortable:false}");
                Response.Write("    ],");
                Response.Write("    sortname: 'DeliveryWayInfo.Delivery_Way_ID',");
                Response.Write("    sortorder: \"desc\",");
                Response.Write("    rowNum: 10,");
                //Response.Write("    rowList:[10,20,40], ");
                Response.Write("    pager: 'pager', ");
                Response.Write("    multiselect: true,");
                Response.Write("    viewrecords:true,");
                Response.Write("    viewsortcols: [false,'horizontal',true],");
                Response.Write("    width: 597,");
                Response.Write("    height: \"100%\",");
                Response.Write("    onSelectRow: function(id,status){  ");
                Response.Write("    jqgrid_rowclick(id,status);");
                Response.Write("    jqgrid_seltip_display();");
                Response.Write("    }, ");
                Response.Write("    loadComplete:function(){");
                Response.Write("        jqgrid_selarry();");
                Response.Write("        jqgrid_seltip_display();");
                Response.Write("    }");

                Response.Write("    });");
                Response.Write("    jqgrid_allclick();");
                Response.Write("</script>");
                break;
            case "showdelivery":
                delivery_id = tools.NullStr(Session["selected_deliveryid"]);

                if (delivery_id.Length > 0)
                {
                    Response.Write(myApp.ShowDelivery(delivery_id));
                }
                else
                {
                    Response.Write("<span class=\"pickertip\">已选择配送方式</span>");
                }
                break;
            case "savedeliveryid":
                Session["selected_deliveryid"] = tools.NullStr(Request["deliveryid"]);
                if (tools.NullStr(Request["deliveryid"]) == "0,")
                {
                    Session["selected_deliveryid"] = "";
                }
                break;
            case "paywaylist":
                Response.Write(myApp.SelectPayway());
                Response.End();
                break;
            case "refresh_payway":
                keyword = Request["keyword"];
                Response.Write("<input type=\"hidden\" id=\"all_flag\" value=\"0\" />");
                Response.Write("<input type=\"hidden\" id=\"allids\" value=\"" + myApp.Get_Payway_IDs() + "\" />");
                Response.Write("<input type=\"hidden\" id=\"selarrow\" value=\"0,\" />");
                Response.Write("<div class=\"list_tip_div\" id=\"list_seltip\"></div>");
                Response.Write("<table id=\"list\"></table>");
                Response.Write("<div id=\"pager\"></div>");
                Response.Write("<script type=\"text/javascript\">");
                Response.Write("jQuery(\"#list\").jqGrid({");
                Response.Write("url: 'picker_do.aspx?action=paywaylist&keyword=" + Server.UrlEncode(keyword) + "',");
                Response.Write("    datatype: \"json\",");
                Response.Write("    colNames: ['ID','支付方式名称'],");
                Response.Write("    colModel: [");
                Response.Write("        {width:30,align:'center', name: 'id', index: 'id',sortable:false},");
                Response.Write("        {align:'left', name: 'paywayWayInfo.payway_Way_Name', index: 'paywayWayInfo.payway_Way_Name',sortable:false}");
                Response.Write("    ],");
                Response.Write("    sortname: 'PayWayInfo.Pay_Way_ID',");
                Response.Write("    sortorder: \"desc\",");
                Response.Write("    rowNum: 10,");
                //Response.Write("    rowList:[10,20,40], ");
                Response.Write("    pager: 'pager', ");
                Response.Write("    multiselect: true,");
                Response.Write("    viewrecords:true,");
                Response.Write("    viewsortcols: [false,'horizontal',true],");
                Response.Write("    width: 597,");
                Response.Write("    height: \"100%\",");
                Response.Write("    onSelectRow: function(id,status){  ");
                Response.Write("    jqgrid_rowclick(id,status);");
                Response.Write("    jqgrid_seltip_display();");
                Response.Write("    }, ");
                Response.Write("    loadComplete:function(){");
                Response.Write("        jqgrid_selarry();");
                Response.Write("        jqgrid_seltip_display();");
                Response.Write("    }");

                Response.Write("    });");
                Response.Write("    jqgrid_allclick();");
                Response.Write("</script>");
                break;
            case "showpayway":
                payway_id = tools.NullStr(Session["selected_paywayid"]);

                if (payway_id.Length > 0)
                {
                    Response.Write(myApp.ShowPayway(payway_id));
                }
                else
                {
                    Response.Write("<span class=\"pickertip\">已选择支付方式</span>");
                }
                break;
            case "savepaywayid":
                Session["selected_paywayid"] = tools.NullStr(Request["paywayid"]);
                if (tools.NullStr(Request["paywayid"]) == "0,")
                {
                    Session["selected_paywayid"] = "";
                }
                break;
        }
    myApp=null;
 %>
