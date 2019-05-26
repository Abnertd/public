using System;

/// <summary>
/// 地址配置设置
/// </summary>
public class PageURL
{
    public string product_detail = string.Empty;
    public string categorylist = string.Empty;
    public string productindex = string.Empty;

    /// <summary>
    /// 初始化url信息
    /// </summary>
    public PageURL()
    {
        switch (int.Parse(System.Web.HttpContext.Current.Application["Static_IsEnable"].ToString()))
        {
            case 1:
                product_detail = "/product/{1}.htm";
                categorylist = "/product/{1}-{2}-{3}-{4}-{5}.htm";
                break;
            case 0:
                product_detail = "/product/detail.aspx?product_id={1}";
                categorylist = "/product/category.aspx?cate_id={1}&brand_id={2}&isgallerylist={3}&orderby={4}&page={5}";
                break;
            default:
                product_detail = "/product/{1}.htm";
                categorylist = "/product/{1}-{2}-{3}-{4}-{5}.htm";
                break;
        }
    }

    /// <summary>
    /// 初始化url信息
    /// </summary>
    /// <param name="IsURLRewrite"></param>
    public PageURL(int IsURLRewrite)
    {
        switch (IsURLRewrite)
        { 
            case 1:
                product_detail = "/product/{1}.htm";
                categorylist = "/product/{1}-{2}-{3}-{4}-{5}.htm";
                productindex = "/product/cate_{1}.htm";
                break;
            case 0:
                product_detail = "/product/detail.aspx?product_id={1}";
                categorylist = "/product/category.aspx?cate_id={1}&brand_id={2}&isgallerylist={3}&orderby={4}&page={5}";
                productindex = "/product/index.aspx?cate_id={1}";
                break;
            default:
                product_detail = "/product/{1}.htm";
                categorylist = "/product/{1}-{2}-{3}-{4}-{5}.htm";
                productindex = "/product/cate_{1}.htm";
                break;
        }
    }

    /// <summary>
    /// 格式化地址字符串
    /// </summary>
    /// <param name="StrURL"></param>
    /// <param name="Params"></param>
    /// <returns></returns>
    public string FormatURL(string StrURL, params string[] Params)
    {
        for (int n = 0; n < Params.GetLength(0); n++)
        {
            StrURL = StrURL.Replace("{" + (n + 1) + "}", Params[n]);
        }

        return StrURL;
    }


    /// <summary>
    /// 生成参数信息
    /// </summary>
    /// <param name="cate_id"></param>
    /// <param name="brand_id"></param>
    /// <param name="isgallerylist"></param>
    /// <param name="orderby"></param>
    /// <param name="page"></param>
    /// <returns></returns>
    public string[] categoryParam(string cate, string brand, string isgallerylist, string orderby, string page)
    {
        if (cate.Length == 0) cate = "0";
        if (brand.Length == 0) brand = "0";
        if (isgallerylist.Length == 0) isgallerylist = "0";
        if (orderby.Length == 0) orderby = "normal";
        if (page.Length == 0) page = "1";

        return new string[5] { cate, brand, isgallerylist.ToString(), orderby, page.ToString() };
    }


}
