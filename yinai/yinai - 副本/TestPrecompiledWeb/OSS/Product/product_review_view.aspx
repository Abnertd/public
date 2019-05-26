<%@ Page Language="C#" ContentType="text/html" ResponseEncoding="utf-8" %>
<%@ Import Namespace="Glaer.Trade.Util.Tools" %>
<%@ Import Namespace="Glaer.Trade.B2C.Model" %>
<%@ Import Namespace="Glaer.Trade.B2C.DAL.Product" %>
<%@ Import Namespace="Glaer.Trade.B2C.BLL.MEM" %>

<script runat="server">
    private ProductReview review;
    private ITools tools;
    private IProduct Myproduct;
    private IMember Mymem;

    private int Product_Review_ID, Product_Review_ProductID, Product_Review_MemberID, Product_Review_Star, Product_Review_Useful, Product_Review_Useless;
    private int Product_Review_IsShow, Product_Review_IsBuy, Product_Review_IsRecommend;
    private string Product_Review_Subject, Product_Review_Content,Product_Name,Member_Nickname;
    DateTime Product_Review_Addtime;
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        Public.CheckLogin("cb1e9c33-7ac5-4939-8520-a0e192cb0129");

        review = new ProductReview(); 
        tools = ToolsFactory.CreateTools();
        Myproduct = ProductFactory.CreateProduct();
        Mymem = MemberFactory.CreateMember();
        Product_Name = "";
        Member_Nickname = "";
        Product_Review_ID = tools.CheckInt(Request.QueryString["review_ID"]);
        if (Product_Review_ID == 0)
        {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        }

        ProductReviewInfo entity = review.GetProductReviewByID(Product_Review_ID);
        if (entity == null) {
            Public.Msg("error", "错误信息", "记录不存在", false, "{back}");
            Response.End();
        } else {
            Product_Review_ID = entity.Product_Review_ID;
            Product_Review_ProductID = entity.Product_Review_ProductID;
            Product_Review_MemberID = entity.Product_Review_MemberID;
            Product_Review_Star = entity.Product_Review_Star;
            Product_Review_Subject = entity.Product_Review_Subject;
            Product_Review_Content = entity.Product_Review_Content;
            Product_Review_Useful = entity.Product_Review_Useful;
            Product_Review_Useless = entity.Product_Review_Useless;
            Product_Review_Addtime = entity.Product_Review_Addtime;
            Product_Review_IsShow = entity.Product_Review_IsShow;
            Product_Review_IsBuy = entity.Product_Review_IsBuy;
            Product_Review_IsRecommend = entity.Product_Review_IsRecommend;

            entity.Product_Review_IsView = 1;
            review.EditProductReview(entity);

            if (Product_Review_ProductID > 0)
            {
                ProductInfo productinfo = Myproduct.GetProductByID(Product_Review_ProductID);
                    if(productinfo!=null)
                    {
                        Product_Name = productinfo.Product_Name;
                    }
            }
            if (Product_Review_MemberID > 0)
            {
                MemberInfo member = Mymem.GetMemberByID(Product_Review_MemberID, Public.GetUserPrivilege());
                if (member != null)
                {
                    Member_Nickname = member.Member_NickName;
                }
            }
        }
        if (Product_Name == "")
        {
            Product_Name = "未知";
        }
        if (Member_Nickname == "")
        {
            Member_Nickname = "游客或未知";
        }
        
    }
</script>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>无标题文档</title>
<link href="/CSS/style.css" rel="stylesheet" type="text/css"/>
<script src="/Scripts/jquery.js" type="text/javascript"></script>
<link type="text/css" href="/Scripts/jquery-ui/css/jquery-ui.css" rel="stylesheet" />
<script src="/Scripts/jquery-ui/jquery-ui.js" type="text/javascript"></script>
<script src="/Scripts/jquery-ui/jquery.ui.datepicker-zh-CN.js" type="text/javascript"></script>
<script src="/Public/ckeditor/ckeditor.js" type="text/javascript"></script>
</head>
<body>
<div class="content_div">
  <table width="100%" border="0" cellpadding="0" cellspacing="0" class="content_table">
    <tr>
      <td class="content_title">产品评论查看</td>
    </tr>
    <tr>
      <td class="content_content">
      
      <table width="100%" border="0" cellpadding="0" cellspacing="0" class="cell_table">
        <tr>
          <td class="cell_title">主题</td>
          <td class="cell_content"><%=Product_Review_Subject %></td>
        </tr>
        <tr>
          <td class="cell_title">评论产品</td>
          <td class="cell_content"><%=Product_Name %></td>
        </tr>
        <tr>
          <td class="cell_title">评论人</td>
          <td class="cell_content"><%=Member_Nickname %> [<%=Product_Review_Addtime%>]</td>
        </tr>
        <tr>
          <td class="cell_title">评论信息</td>
          <td class="cell_content">
          <b>评分：</b><%=Product_Review_Star %> 
          <b>推荐状态：</b>
          <%
              
              if (Product_Review_IsRecommend == 1)
              {
                  Response.Write("已推荐");
              }
              else
              {
                Response.Write("未推荐");
              }
          
              %>
           <b>审核状态：</b>
          <%
              
              if (Product_Review_IsShow == 1)
              {
                  Response.Write("已审核");
              }
              else
              {
                Response.Write("未审核");
              }
          
              %>
              <b>有用：</b><%=Product_Review_Useful %> <b>无用：</b><%=Product_Review_Useless %>
              </td>
        </tr>
        <tr>
          <td class="cell_title">评论内容</td>
          <td class="cell_content"><%=Product_Review_Content %></td>
        </tr>
        
      </table>
        <table width="100%" border="0" cellspacing="0" cellpadding="5">
          <tr>
            <td align="right">
            
             <input name="button" type="button" class="bt_grey" id="button" value="返回" onmouseover="this.className='bt_orange';" onmouseout="this.className='bt_grey';" onclick="location='product_review_list.aspx'"/></td>
          </tr>
        </table>
        </td>
    </tr>
  </table>
</div>
</body>
</html>
