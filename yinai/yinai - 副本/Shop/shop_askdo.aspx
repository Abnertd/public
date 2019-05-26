<%@ Page Language="C#" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Register TagPrefix="uc2" TagName="Bottom" Src="Public/Bottom.ascx"  %>
<%@ Register src="Public/Top.ascx" tagname="Top" tagprefix="uc1" %>
<%@ Register src="Public/Left.ascx" tagname="Left" tagprefix="uc3" %>
<%
    
    Public_Class pub = new Public_Class();
    ITools tools;
    tools = ToolsFactory.CreateTools();
    Shop shop = new Shop();
    shop.Shop_Initial();
    string action;
    action=tools.CheckStr(Request["action"]);
    if(action== "ask_add")
    {
        shop.Shopping_Ask_Add();
    }
  
 %>
