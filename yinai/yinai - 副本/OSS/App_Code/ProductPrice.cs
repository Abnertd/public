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
using Glaer.Trade.B2C.BLL.MEM;
using Glaer.Trade.B2C.BLL.Product;

/// <summary>
///ProductPrice 的摘要说明
/// </summary>
public class ProductPrice
{
    private ITools tools;
    private IMember MyMem;
    private IProductPrice Myprice;
    private IMemberGrade Mygrade;

    private System.Web.HttpApplicationState Application;

    public ProductPrice()
    {
        Application = System.Web.HttpContext.Current.Application;

        tools = ToolsFactory.CreateTools();
        MyMem = MemberFactory.CreateMember();
        Myprice = ProductPriceFactory.CreateProductPrice();
        Mygrade = MemberGradeFactory.CreateMemberGrade();
    }


    //获取会员等级信息
    public IList<MemberGradeInfo> GetMemberGrades()
    {
        QueryInfo Query = new QueryInfo();
        Query.PageSize = 0;
        Query.CurrentPage = 1;
        Query.ParamInfos.Add(new ParamInfo("AND", "str", "MemberGradeInfo.Member_Grade_Site", "=", Public.GetCurrentSite()));
        IList<MemberGradeInfo> membergrade = Mygrade.GetMemberGrades(Query, Public.GetUserPrivilege());
        return membergrade;
    }

    //获取会员获取积分
    public int Get_Member_Coin(int member_id,double Product_Price)
    {
        int coin_amount = 0;
        coin_amount = (int)(Product_Price * tools.CheckFloat(Application["Coin_Rate"].ToString()));

        int member_grade;
        if (member_id > 0)
        {

            MemberInfo member = MyMem.GetMemberByID(member_id, Public.GetUserPrivilege());
            if (member != null)
            {
                member_grade = member.Member_Grade;
                //检查会员等级优惠
                IList<MemberGradeInfo> membergrade = GetMemberGrades();
                if (membergrade != null)
                {
                    foreach (MemberGradeInfo entity in membergrade)
                    {
                        if (member_grade == entity.Member_Grade_ID)
                        {
                            coin_amount = (int)(Product_Price * entity.Member_Grade_CoinRate);
                        }
                    }
                }
            }
        }
        return coin_amount;
    }

    //获取会员价格
    public double Get_Member_Price(int member_id,int product_id, double product_price)
    {
        double member_price = product_price;
        int member_grade = 0;
        if (member_id > 0)
        {

            MemberInfo member = MyMem.GetMemberByID(member_id, Public.GetUserPrivilege());
            if (member != null)
            {
                member_grade = member.Member_Grade;
                //检查会员等级优惠
                IList<MemberGradeInfo> membergrade = GetMemberGrades();
                if (membergrade != null)
                {
                    foreach (MemberGradeInfo entity in membergrade)
                    {
                        if (member_grade == entity.Member_Grade_ID)
                        {
                            member_price = (product_price * entity.Member_Grade_Percent) / 100;
                        }
                    }
                }
                //检查产品等级价格
                if (product_id > 0)
                {
                    IList<ProductPriceInfo> productprice = Myprice.GetProductPrices(product_id);
                    if (productprice != null)
                    {
                        foreach (ProductPriceInfo entity in productprice)
                        {
                            if (member_grade == entity.Product_Price_MemberGradeID)
                            {
                                member_price = entity.Product_Price_Price;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            IList<MemberGradeInfo> membergrade = GetMemberGrades();
            if (membergrade != null)
            {
                foreach (MemberGradeInfo entity in membergrade)
                {
                    if (entity.Member_Grade_Default == 1)
                    {
                        member_grade = entity.Member_Grade_ID;
                        member_price = (product_price * entity.Member_Grade_Percent) / 100;
                    }
                }
            }

            //检查产品等级价格
            if (product_id > 0)
            {
                IList<ProductPriceInfo> productprice = Myprice.GetProductPrices(product_id);
                if (productprice != null)
                {
                    foreach (ProductPriceInfo entity in productprice)
                    {
                        if (member_grade == entity.Product_Price_MemberGradeID)
                        {
                            member_price = entity.Product_Price_Price;
                        }
                    }
                }
            }
        }

        return tools.CheckFloat(member_price.ToString("0.00"));
    }



    //获取会员等级价格
    public double Get_MemberGrade_Price(int member_Grade, double product_price)
    {
        double member_price = product_price;
        int member_grade = 0;
        IList<MemberGradeInfo> membergrade = GetMemberGrades();
        if (membergrade != null)
        {
            foreach (MemberGradeInfo entity in membergrade)
            {
                if (entity.Member_Grade_ID == member_Grade)
                {
                    member_grade = entity.Member_Grade_ID;
                    member_price = (product_price * entity.Member_Grade_Percent) / 100;
                }
            }
        }
        return tools.CheckFloat(member_price.ToString("0.00"));
    }
}
