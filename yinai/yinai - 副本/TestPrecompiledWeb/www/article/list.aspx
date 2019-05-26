<%@ Page Language="C#" %>

<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register Src="~/Public/Top.ascx" TagPrefix="uctop" TagName="top" %>
<%@ Register Src="~/Public/bottom_simple.ascx" TagPrefix="ucbottom" TagName="bottom" %>
<% 
    CMS cms = new CMS();
    Public_Class pub = new Public_Class();
    ITools tools = ToolsFactory.CreateTools();
    //Product product = new Product();

    ArticleCateInfo cateinfo = null;
    Session["Position"] = "";
    string cate_title = "资讯中心";
    int help_id = 0, cate_id = 0;
    string hkeyword = "";
    hkeyword = tools.CheckStr(Request["hkeyword"]);
    string default_key = "";
    if (hkeyword.Length == 0)
    {
        default_key = "输入需要查询的关键词";
    }
    else
    {
        default_key = hkeyword;
    }
    if (hkeyword == "输入需要查询的关键词")
    {
        hkeyword = "";
    }
    cate_id = tools.CheckInt(Request["cate_id"]);
    //cate_id = tools.CheckInt(Request["cate_id"]);
    cateinfo = cms.GetArticleCateInfoByID(cate_id);
    if (cateinfo != null)
    {
        cate_title = cateinfo.Article_Cate_Name;
    }
    else
    {
        Response.Redirect("/index.aspx");

    }    
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>
        <%=cate_title + " - " + pub.SEO_TITLE()%></title>
          <meta name="Keywords" content="<% = Application["Site_Keyword"]%>"/>
    <meta name="Description" content="<%=Application["Site_Description"]%>" />
    <link href="/css/index.css" rel="stylesheet" type="text/css" />
    <link href="/css/index_newadd.css" rel="stylesheet" type="text/css" />
    <link href="/css/index2.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/scripts/jquery.js"></script>
    
   <!--layer弹出框.js .css引用 开始-->
 <link href="../layer.m/layer.m.css" rel="stylesheet" />
    <script type="text/javascript" src="/scripts/jquery-1.5.1.min.js" charset="gb2312"></script>
       <script type="text/javascript" src="/scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="/scripts/layer/layer.js" ></script>
  <!--layer弹出框.js .css引用 结束-->
     
   <!--示范一个公告层 开始-->
  <script type="text/javascript">
      function SignUpNow() {
          layer.open({
              type: 2
 , title: false //不显示标题栏
              //, closeBtn: false
 , area: ['480px;', '340px']

 , shade: 0.8
 , id: 'LAY_layuipro' //设定一个id，防止重复弹出
 , resize: false
 , btnAlign: 'c'
 , moveType: 1 //拖拽模式，0或者1              
              , content: ("/Bid/SignUpPopup.aspx")
          });
      }
    </script>
   <!--示范一个公告层 结束-->
</head>
<body>
    <uctop:top ID="top1" runat="server" />
    <!--主体 开始-->
    <div class="content">
        <!--位置说明 开始-->
        <div class="position">
            当前位置 > <a href="/index.aspx">首页</a> > <span><%=cate_title%></span></div>
        <div class="clear">
        </div>
        <!--位置说明 结束-->
        <div class="parth">
            <div class="ph_left">
                <div class="blk14">
                    <h2>
                        资讯中心</h2>
                    <div class="blk14_main">
                        <%=cms.Article_Left_Nav(0,cate_id)%>
                    </div>
                </div>
            </div>
            <div class="ph_right">

                    <div class="pi_right_box">
                        
                        <h2>
                            <strong>
                                <%=cate_title %></strong></h2>
                        <div class="main" style="">
                            <%
                                cms.GetArticles(cate_id);
                                    
                            %>
                        </div>
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
    </div>

  <ucbottom:bottom runat="server" ID="Bottom" />
</body>
</html>
