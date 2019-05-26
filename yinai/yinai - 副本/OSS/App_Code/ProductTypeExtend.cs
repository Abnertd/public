using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using Glaer.Trade.B2C.Model;
using Glaer.Trade.B2C.ORM;
using Glaer.Trade.Util.Encrypt;
using Glaer.Trade.Util.Tools;
using Glaer.Trade.Util.TraceError;
using Glaer.Trade.Util.Mail;
using Glaer.Trade.B2C.BLL.Product;

/// <summary>
///ProductTypeExtend 的摘要说明
/// </summary>
public class ProductTypeExtend
{
    //定义ASP.NET内置对象
    private System.Web.HttpResponse Response;
    private System.Web.HttpRequest Request;
    private System.Web.HttpServerUtility Server;
    private System.Web.SessionState.HttpSessionState Session;
    private System.Web.HttpApplicationState Application;

    private ITools tools;
    private IProductTypeExtend MyBLL;

    public ProductTypeExtend()
    {
        //初始化ASP.NET内置对象
        Response = System.Web.HttpContext.Current.Response;
        Request = System.Web.HttpContext.Current.Request;
        Server = System.Web.HttpContext.Current.Server;
        Session = System.Web.HttpContext.Current.Session;
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyBLL = ProductTypeExtendFactory.CreateProductTypeExtend();
    }

    //添加商品类型扩展属性
    public void AddProductTypeExtend()
    {
        int ProductType_Extend_ID = tools.CheckInt(Request.Form["ProductType_Extend_ID"]);
        int ProductType_Extend_ProductTypeID = tools.CheckInt(Request.Form["ProductType_ID"]);
        string ProductType_Extend_Name = tools.CheckStr(Request.Form["ProductTypeExtend_Name"]);
        string ProductType_Extend_Display = tools.CheckStr(Request.Form["ProductTypeExtend_Display"]);
        int ProductType_Extend_IsSearch = tools.CheckInt(Request.Form["ProductType_Extend_IsSearch"]);
        int ProductType_Extend_Options = tools.CheckInt(Request.Form["ProductTypeExtend_Options"]);
        string ProductType_Extend_Default = tools.Left(tools.CheckStr(Request.Form["ProductTypeExtend_Default"]),500);
        int ProductType_Extend_IsActive = tools.CheckInt(Request.Form["ProductTypeExtend_IsActive"]);
        int ProductType_Extend_Gather = tools.CheckInt(Request.Form["ProductType_Extend_Gather"]);
        int ProductType_Extend_DisplayForm = tools.CheckInt(Request.Form["ProductType_Extend_DisplayForm"]);
        int ProductType_Extend_Sort = tools.CheckInt(Request.Form["ProductTypeExtend_Sort"]);
        string ProductType_Extend_Site = Public.GetCurrentSite();

        switch (ProductType_Extend_Display)
        {
            case "0":
                ProductType_Extend_Display = "select";
                break;
            case "1":
                ProductType_Extend_Display = "Text";
                break;
            case "2":
                ProductType_Extend_Display = "html";
                break;
        }

        if (ProductType_Extend_ProductTypeID == 0)
        {
            Public.Msg("error", "错误信息", "请选择产品类型！", false, "{back}");
        }
        if (ProductType_Extend_Name == "")
        {
            Public.Msg("error", "错误信息", "请输入属性名称！", false, "{back}");
        }

        ProductTypeExtendInfo entity = new ProductTypeExtendInfo();
        entity.ProductType_Extend_ID = ProductType_Extend_ID;
        entity.ProductType_Extend_ProductTypeID = ProductType_Extend_ProductTypeID;
        entity.ProductType_Extend_Name = ProductType_Extend_Name;
        entity.ProductType_Extend_Display = ProductType_Extend_Display;
        entity.ProductType_Extend_IsSearch = ProductType_Extend_IsSearch;
        entity.ProductType_Extend_Options = ProductType_Extend_Options;
        entity.ProductType_Extend_Default = ProductType_Extend_Default;
        entity.ProductType_Extend_IsActive = ProductType_Extend_IsActive;
        entity.ProductType_Extend_Gather = ProductType_Extend_Gather;
        entity.ProductType_Extend_DisplayForm = ProductType_Extend_DisplayForm;
        entity.ProductType_Extend_Sort = ProductType_Extend_Sort;
        entity.ProductType_Extend_Site = ProductType_Extend_Site;

        if (MyBLL.AddProductTypeExtend(entity))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "ProductTypeExtend_Add.aspx?producttype_id=" + ProductType_Extend_ProductTypeID);
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //修改商品类型扩展属性
    public void EditProductTypeExtend()
    {

        int ProductType_Extend_ID = tools.CheckInt(Request.Form["Extend_ID"]);
        int ProductType_Extend_ProductTypeID = tools.CheckInt(Request.Form["ProductType_ID"]);
        string ProductType_Extend_Name = tools.CheckStr(Request.Form["ProductTypeExtend_Name"]);
        string ProductType_Extend_Display = tools.CheckStr(Request.Form["ProductTypeExtend_Display"]);
        int ProductType_Extend_IsSearch = tools.CheckInt(Request.Form["ProductType_Extend_IsSearch"]);
        int ProductType_Extend_Options = tools.CheckInt(Request.Form["ProductTypeExtend_Options"]);
        string ProductType_Extend_Default = tools.Left(tools.CheckStr(Request.Form["ProductTypeExtend_Default"]), 500);
        int ProductType_Extend_IsActive = tools.CheckInt(Request.Form["ProductTypeExtend_IsActive"]);
        int ProductType_Extend_Gather = tools.CheckInt(Request.Form["ProductType_Extend_Gather"]);
        int ProductType_Extend_DisplayForm = tools.CheckInt(Request.Form["ProductType_Extend_DisplayForm"]);
        int ProductType_Extend_Sort = tools.CheckInt(Request.Form["ProductTypeExtend_Sort"]);
        string ProductType_Extend_Site = Public.GetCurrentSite();

        switch (ProductType_Extend_Display)
        {
            case "0":
                ProductType_Extend_Display = "select";
                break;
            case "1":
                ProductType_Extend_Display = "Text";
                break;
            case "2":
                ProductType_Extend_Display = "html";
                break;
        }

        if (ProductType_Extend_ProductTypeID == 0)
        {
            Public.Msg("error", "错误信息", "请选择产品类型！", false, "{back}");
        }
        if (ProductType_Extend_Name == "")
        {
            Public.Msg("error", "错误信息", "请输入属性名称！", false, "{back}");
        }

        ProductTypeExtendInfo entity = new ProductTypeExtendInfo();
        entity.ProductType_Extend_ID = ProductType_Extend_ID;
        entity.ProductType_Extend_ProductTypeID = ProductType_Extend_ProductTypeID;
        entity.ProductType_Extend_Name = ProductType_Extend_Name;
        entity.ProductType_Extend_Display = ProductType_Extend_Display;
        entity.ProductType_Extend_IsSearch = ProductType_Extend_IsSearch;
        entity.ProductType_Extend_Options = ProductType_Extend_Options;
        entity.ProductType_Extend_Default = ProductType_Extend_Default;
        entity.ProductType_Extend_IsActive = ProductType_Extend_IsActive;
        entity.ProductType_Extend_Gather = ProductType_Extend_Gather;
        entity.ProductType_Extend_DisplayForm = ProductType_Extend_DisplayForm;
        entity.ProductType_Extend_Sort = ProductType_Extend_Sort;
        entity.ProductType_Extend_Site = ProductType_Extend_Site;

        if (MyBLL.EditProductTypeExtend(entity))
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "ProductTypeExtend.aspx?producttype_id=" + ProductType_Extend_ProductTypeID);
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //删除商品类型扩展属性
    public void DelProductTypeExtend()
    {
        int cate_id = tools.CheckInt(Request.QueryString["Extend_ID"]);
        if (MyBLL.DelProductTypeExtend(cate_id) > 0)
        {
            Public.Msg("positive", "操作成功", "操作成功", true, "ProductType.aspx");
        }
        else
        {
            Public.Msg("error", "错误信息", "操作失败，请稍后重试", false, "{back}");
        }
    }

    //根据编号获取扩展属性
    public ProductTypeExtendInfo GetProductTypeExtendByID(int cate_id)
    {
        return MyBLL.GetProductTypeExtendByID(cate_id);
    }

    //获取产品类型所属扩展属性
    public string GetProductTypeExtends(int ID)
    {
        IList<ProductTypeExtendInfo> ProductTypeExtends = MyBLL.GetProductTypeExtends(ID);
        if (ProductTypeExtends != null)
        {
            StringBuilder jsonBuilder = new StringBuilder();
            jsonBuilder.Append("{\"page\":1,\"total\":" + ProductTypeExtends.Count + ",\"records\":" + ProductTypeExtends.Count + ",\"rows\"");
            jsonBuilder.Append(":[");
            foreach (ProductTypeExtendInfo entity in ProductTypeExtends)
            {
                jsonBuilder.Append("{\"ProductType.ProductType_ID\":" + entity.ProductType_Extend_ID + ",\"cell\":[");
                //各字段
                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.ProductType_Extend_ID);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.ProductType_Extend_Name);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                switch (entity.ProductType_Extend_Display.ToLower())
                {
                    case "select":
                        jsonBuilder.Append("下拉菜单");
                        break;
                    case "text":
                        jsonBuilder.Append("文本框");
                        break;
                    case "html":
                        jsonBuilder.Append("HTML");
                        break;
                    default:
                        jsonBuilder.Append("--");
                        break;
                }
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                if (entity.ProductType_Extend_IsSearch == 0)
                {
                    jsonBuilder.Append("否");
                }
                else
                {
                    jsonBuilder.Append("是");
                }
                jsonBuilder.Append("\",");


                jsonBuilder.Append("\"");
                jsonBuilder.Append(entity.ProductType_Extend_Sort);
                jsonBuilder.Append("\",");

                jsonBuilder.Append("\"");
                jsonBuilder.Append("<img src=\\\"/images/icon_edit.gif\\\"> <a href=\\\"producttypeExtend_edit.aspx?extend_id=" + entity.ProductType_Extend_ID + "\\\" title=\\\"修改\\\">修改</a> <img src=\\\"/images/icon_del.gif\\\"> <a href=\\\"javascript:void(0);\\\" onclick=\\\"confirmdelete('producttypeextend_do.aspx?action=move&extend_id=" + entity.ProductType_Extend_ID + "')\\\" title=\\\"删除\\\">删除</a>");
                jsonBuilder.Append("\",");

                jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
                jsonBuilder.Append("]},");
            }
            jsonBuilder.Remove(jsonBuilder.Length - 1, 1);
            jsonBuilder.Append("]");
            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
        else
        {
            return null;
        }
    }
    

}
