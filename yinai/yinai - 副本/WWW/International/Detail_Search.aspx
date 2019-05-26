<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/englishtop_simple.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/englishbottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<script runat="server">
    ITools tools = ToolsFactory.CreateTools();
    Public_Class pub = new Public_Class();

    string keyword = "";




    protected void Page_Load(object sender, EventArgs e)
    {
        keyword = pub.CheckXSS(tools.CheckStr(Request["keyword"]));
        if (keyword == "Please enter keywords for search")
        {
            keyword = "";
        }
        if (keyword == "")
        {
            keyword = "Please enter keywords for search";
        }
    }
</script>
<%    Public_Class pub = new Public_Class();
      ITools tools = ToolsFactory.CreateTools();
      Supplier supplier = new Supplier();
      CMS cms = new CMS();


      AD ad = new AD();

      Statistics statistics = new Statistics();
      Session["Position"] = "Detail";

      Product product = new Product();
      Bid MyBid = new Bid();
      Logistics MyLogistics = new Logistics();


    
                                                                  
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>English Detail</title>
    <link rel="stylesheet" type="text/css" href="/css/index.css" />
    <script src="/scripts/1.js" type="text/javascript"></script>
       <style type="text/css">
        .xqy_list_main .page {
            clear: both;
        }

        .page {
            margin-bottom: 18px;
            margin-top: 20px;
            padding-top: 40px;
            text-align: center;

        } 
         html,body,ul,ol,li,p,h1,h2,h3,h4,h5,h6,form,fieldset,img,div,dl,dt,dd{margin:0;padding:0;border:0;font-family:Arial;}
    </style>
    <script type="text/javascript">
        function checksearch() {
            var top_keyword = $("#top_keyword").val();


            if (top_keyword.length == 0 || top_keyword == "Please enter keywords for search") {
                alert('Please enter keywords for search');
                return false;

            }
            else {

                return top_keyword;
            }
        }


        function keywordfocus() {
            var top_keyword = $("#top_keyword").val();
            if (top_keyword == "Please enter keywords for search") {
                $("#top_keyword").val('');
            }
        }
        function keywordblur() {
            var top_keyword = $("#top_keyword").val();
            if (top_keyword.length == 0) {
                $("#top_keyword").val('Please enter keywords for search');
            }
        }



    </script>
        <script type="text/javascript">
            //===========================点击展开关闭效果====================================
            function openShutManager(oSourceObj, oTargetObj, shutAble, oOpenTip, oShutTip) {
                var sourceObj = typeof oSourceObj == "string" ? document.getElementById(oSourceObj) : oSourceObj;
                var targetObj = typeof oTargetObj == "string" ? document.getElementById(oTargetObj) : oTargetObj;
                var openTip = oOpenTip || "";
                var shutTip = oShutTip || "";
                if (targetObj.style.display != "none") {
                    if (shutAble) return;
                    targetObj.style.display = "none";
                    if (openTip && shutTip) {
                        sourceObj.innerHTML = shutTip;
                    }
                } else {
                    targetObj.style.display = "block";
                    if (openTip && shutTip) {
                        sourceObj.innerHTML = openTip;
                    }
                }
            }
    </script>
</head>
<body>
    <uctop:top runat="server" ID="HomeTop" IsIndex="true" />


        <div class="En_info1">
        <div class="En_info1_main">
       <div class="xqy_main">
                <form action="/International/Detail_Search.aspx" method="get" name="frm_search" id="frm_search" onsubmit="return checksearch();">
                    <div class="search2">
                        <span>
                            <input id="top_keyword" name="keyword" value="<%=keyword%>" type="text" onblur="keywordblur()" onfocus="keywordfocus()" /><img src="/images/icon17.png" width="30" height="30" /></span>
                        <span><a href="javascript:void(0);" onclick="$('#frm_search').submit();">Search</a></span>
                    </div>
                </form>

                <div style="float: left;">
                   
                    <div class="pd_left">
                        <div class="pd_left">
                            <div class="menu_1">
                                <h2>Classfication</h2>
                                <%=cms.GetEnlishDetailsLeft(65,"","")%>
                            </div>
                        </div>
                    </div>

                    <div class="xqy_list">
                        <div class="xqy_list_main">
                            <h2><span style="padding-left:10px;">Product list</span></h2>
                          
                                <%cms.GetEnlishDetailsRight(65,"","");%>
                             
                        </div>
                    </div>
                </div>
            </div>
</div></div>
   <%-- <div class="En_info1">
        <div class="En_info1_main">
            <div class="xqy_nav1">
                <div class="xqy_nav1_1">               
                       <%=cms.GetEnlishDetailsLeft(65)%>
                </div>
            </div>
            <div class="xqy_main">
                <form action="/International/Detail_Search.aspx" method="get" name="frm_search" id="frm_search" onsubmit="return checksearch();">
                    <div class="search2">
                        <span>
                            <input id="top_keyword" name="keyword" value="<%=keyword%>" type="text" onblur="keywordblur()" onfocus="keywordfocus()" /><img src="/images/icon17.png" width="30" height="30" /></span>
                        <span><a href="javascript:void(0);" onclick="$('#frm_search').submit();">Search</a></span>
                    </div>
                </form>

                <div style="float: left;">
                    <div class="xqy_nav2">
                        <div class="xqy_nav2_1">                      
                               <%cms.GetEnlishDetailsRight(65);%>
                        </div>
                    </div>
                    <div class="xqy_list">
                        <div class="xqy_list_main">
                            <h2>产品分类</h2>
                            <div class="En_info2_list2">
                                <%=cms.GetEnlishDetails_Search(52,"")%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    <div class="clear"></div>
    <!--尾部 开始-->
    <ucbottom:bottom runat="server" ID="Bottom" />
    <!--尾部 结束-->
</body>
</html>

