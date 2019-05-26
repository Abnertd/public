<%@ Control Language="C#" ClassName="Top" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Register Src="~/Public/public_top.ascx" TagPrefix="pubtop" TagName="publictop" %>

<link href="/css/index.css" rel="stylesheet" type="text/css" />
<link href="/css/index2.css" rel="stylesheet" type="text/css" />
<link href="/css/flash01.css" rel="stylesheet" type="text/css" />

<%
    ITools tools = ToolsFactory.CreateTools();
    Product product = new Product();
    AD ad = new AD();
    Cart cart = new Cart();


    string keyword = tools.CheckStr(Request["keyword"]);
    keyword = Server.UrlDecode(keyword);
    if (keyword == "输入关键词进行搜索")
    {
        keyword = "";
    }
    if (keyword == "")
    {
        keyword = "输入关键词进行搜索";
    }
    keyword = keyword.Replace("\"", "&quot;").ToString();

    int search_type_id = tools.CheckInt(Request["search_type_id"]);
    
%>

<!--头部 开始-->
<div class="sz_head">

    <pubtop:publictop ID="top1" runat="server" />

   <%-- <div class="nav">
        <div class="nav_main">
            
            <%
                switch (Request.Path.ToLower())
                {
                    case "/tradeindex.aspx":
                        Response.Write("<div class=\"nav_left\">");
                        Response.Write("<span>行业市场</span>");
                        Response.Write("<div class=\"menu\">");
                        break;
                    default:
                        Response.Write("<div class=\"nav_left02\"  style=\"background-color:#b1191a;\">");
                        Response.Write("<span>行业市场</span>");
                        Response.Write("<div class=\"menu menu02\">");
                        break;
                }
            %>
          
            <div class="dropList" id="0">
                <%=product.Home_Left_Cate() %>
            </div>
        </div>
    </div>--%>
     <div class="nav_left">
                  <strong><img src="images/icon07.jpg">全部商品分类</strong>
                  <div class="nav_info">
                                              <div class="testbox">
                                                  <ul>
                                                      <li>
                                                         <dl class="a3" style="border-top:none;">
                                                               <dt><a href="#" target="_blank"><span style="float:left;"><img src="images/icon29.jpg" width="13" height="23" /></span><span style="float:left; font-size:14px; margin-left:5px; font-weight:bold;">耐火原料</span><span>></span></a></dt>
                                                               <dd>
                                                                   <a href="#" target="_blank">铝硅质耐火原料</a> <a href="#" target="_blank">镁质耐火原料</a>
                                                               </dd>
                                                         </dl>                                  
                                                         <div class="boxshow" id="xs01">
                                                            <div class="boxshow_left">
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  
                                                            </div>
                                                            <div class="boxshow_right">
                                                                  <div class="b_r_box">
                                                                        <h3 class="title04">推荐品牌</h3>
                                                                        <div class="fox">
                                                                              <a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a>
                                                                              
                                                                        </div>
                                                                  </div>
                                                            </div>
                                                            <div class="clear"></div>
                                                        </div>
                                                    </li>
                                                      <li>
                                                         <dl class="a3">
                                                               <dt><a href="#" target="_blank"><span style="float:left;"><img src="images/icon29.jpg" width="13" height="23" /></span><span style="float:left; font-size:14px; margin-left:5px; font-weight:bold;">耐火制品</span><span>></span></a></dt>
                                                               <dd>
                                                                   <a href="#" target="_blank">特殊耐火材料</a> <a href="#" target="_blank">隔热耐火制品</a>
                                                               </dd>
                                                         </dl>                                  
                                                         <div class="boxshow" id="Div1">
                                                            <div class="boxshow_left">
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  
                                                            </div>
                                                            <div class="boxshow_right">
                                                                  <div class="b_r_box">
                                                                        <h3 class="title04">推荐品牌</h3>
                                                                        <div class="fox">
                                                                              <a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a>
                                                                              
                                                                        </div>
                                                                  </div>
                                                            </div>
                                                            <div class="clear"></div>
                                                        </div>
                                                    </li>
                                                      <li>
                                                         <dl class="a3">
                                                               <dt><a href="#" target="_blank"><span style="float:left;"><img src="images/icon29.jpg" width="13" height="23" /></span><span style="float:left; font-size:14px; margin-left:5px; font-weight:bold;">回收再生料</span><span>></span></a></dt>
                                                               <dd>
                                                                   <a href="#" target="_blank">ABS树脂</a> <a href="#" target="_blank">PP再生料</a> <a href="#" target="_blank">PVC再生料</a>
                                                               </dd>
                                                         </dl>                                  
                                                         <div class="boxshow" id="Div2">
                                                            <div class="boxshow_left">
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  
                                                            </div>
                                                            <div class="boxshow_right">
                                                                  <div class="b_r_box">
                                                                        <h3 class="title04">推荐品牌</h3>
                                                                        <div class="fox">
                                                                              <a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a>
                                                                              
                                                                        </div>
                                                                  </div>
                                                            </div>
                                                            <div class="clear"></div>
                                                        </div>
                                                    </li>
                                                      <li>
                                                         <dl class="a3">
                                                               <dt><a href="#" target="_blank"><span style="float:left;"><img src="images/icon29.jpg" width="13" height="23" /></span><span style="float:left; font-size:14px; margin-left:5px; font-weight:bold;">包装物资</span><span>></span></a></dt>
                                                               <dd>
                                                                   <a href="#" target="_blank">纸箱</a> <a href="#" target="_blank">塑料包装箱</a> <a href="#" target="_blank">铁质包装箱</a>
                                                               </dd>
                                                         </dl>                                  
                                                         <div class="boxshow" id="Div3">
                                                            <div class="boxshow_left">
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  
                                                            </div>
                                                            <div class="boxshow_right">
                                                                  <div class="b_r_box">
                                                                        <h3 class="title04">推荐品牌</h3>
                                                                        <div class="fox">
                                                                              <a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a>
                                                                              
                                                                        </div>
                                                                  </div>
                                                            </div>
                                                            <div class="clear"></div>
                                                        </div>
                                                    </li>
                                                      <li>
                                                         <dl class="a3">
                                                               <dt><a href="#" target="_blank"><span style="float:left;"><img src="images/icon29.jpg" width="13" height="23" /></span><span style="float:left; font-size:14px; margin-left:5px; font-weight:bold;">设备备品备件</span><span>></span></a></dt>
                                                               <dd>
                                                                   <a href="#" target="_blank">紧固件</a> <a href="#" target="_blank">密封件</a> <a href="#" target="_blank">塑料件</a> <a href="#" target="_blank">传动件</a>
                                                               </dd>
                                                         </dl>                                  
                                                         <div class="boxshow" id="Div4">
                                                            <div class="boxshow_left">
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  
                                                            </div>
                                                            <div class="boxshow_right">
                                                                  <div class="b_r_box">
                                                                        <h3 class="title04">推荐品牌</h3>
                                                                        <div class="fox">
                                                                              <a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a>
                                                                              
                                                                        </div>
                                                                  </div>
                                                            </div>
                                                            <div class="clear"></div>
                                                        </div>
                                                    </li>
                                                      <li>
                                                         <dl class="a3">
                                                               <dt><a href="#" target="_blank"><span style="float:left;"><img src="images/icon29.jpg" width="13" height="23" /></span><span style="float:left; font-size:14px; margin-left:5px; font-weight:bold;">冶金辅料</span><span>></span></a></dt>
                                                               <dd>
                                                                   <a href="#" target="_blank">脱氧剂</a> <a href="#" target="_blank">引流砂</a> <a href="#" target="_blank">脱硫剂</a> <a href="#" target="_blank">覆盖剂</a>
                                                               </dd>
                                                         </dl>                                  
                                                         <div class="boxshow" id="Div5">
                                                            <div class="boxshow_left">
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  <dl class="dst5">
                                                                       <dt><a href="#" target="_blank">商品分类</a></dt>
                                                                       <dd><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a><a href="#" target="_blank">商品分类</a></dd>
                                                                       <div class="clear"></div>
                                                                  </dl>
                                                                  
                                                            </div>
                                                            <div class="boxshow_right">
                                                                  <div class="b_r_box">
                                                                        <h3 class="title04">推荐品牌</h3>
                                                                        <div class="fox">
                                                                              <a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a><a href="#" target="_blank">leneta</a>
                                                                              
                                                                        </div>
                                                                  </div>
                                                            </div>
                                                            <div class="clear"></div>
                                                        </div>
                                                    </li>
                                                  </ul>
                                                  
                                              </div>
                                              <p style="border-top:1px solid #fd8a61;"><a href="#" style="color:#fff;" target="_blank">查看全部分类 ></a></p>
                                        </div>
            </div>

    <div class="nav_right">
        <ul>           
            <li class="on"><a href="#" target="_blank">首 页</a></li>
            <li><a href="#" target="_blank">商城选购</a></li>
            <li><a href="#" target="_blank">招标拍卖</a></li>
            <li><a href="#" target="_blank">仓储物流</a></li>
            <li><a href="#" target="_blank">金融中心</a><img src="images/icon10.jpg"></li>
            <li><a href="#" target="_blank">行情资讯</a></li>
            <li class="tel">400-8108-802</li>
        </ul>
        <div class="clear"></div>
    </div>
    <div class="clear"></div>
</div>
</div>
</div>
<!--头部 结束-->
