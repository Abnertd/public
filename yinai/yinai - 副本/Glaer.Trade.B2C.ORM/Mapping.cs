using System;
using System.Collections;

namespace Glaer.Trade.B2C.ORM
{
    public class Mapping
    {
        public Hashtable Relation;
        public Mapping()
        {
            Relation = new Hashtable();

            //ZhongXin
            Relation["ZhongXinInfo.ID"] = "ZhongXin.ID";
            Relation["ZhongXinInfo.SupplierID"] = "ZhongXin.SupplierID";
            Relation["ZhongXinInfo.CompanyName"] = "ZhongXin.CompanyName";
            Relation["ZhongXinInfo.ReceiptAccount"] = "ZhongXin.ReceiptAccount";
            Relation["ZhongXinInfo.ReceiptBank"] = "ZhongXin.ReceiptBank";
            Relation["ZhongXinInfo.BankCode"] = "ZhongXin.BankCode";
            Relation["ZhongXinInfo.BankName"] = "ZhongXin.BankName";
            Relation["ZhongXinInfo.OpenAccountName"] = "ZhongXin.OpenAccountName";
            Relation["ZhongXinInfo.SubAccount"] = "ZhongXin.SubAccount";
            Relation["ZhongXinInfo.Audit"] = "ZhongXin.Audit";
            Relation["ZhongXinInfo.Register"] = "ZhongXin.Register";
            Relation["ZhongXinInfo.Addtime"] = "ZhongXin.Addtime";

            //系统配置
            Relation["ConfigInfo.Sys_Config_ID"] = "Sys_Config.Sys_Config_ID";
            Relation["ConfigInfo.Site_DomainName"] = "Sys_Config.Site_DomainName";
            Relation["ConfigInfo.Site_Name"] = "Sys_Config.Site_Name";
            Relation["ConfigInfo.Site_URL"] = "Sys_Config.Site_URL";
            Relation["ConfigInfo.Sys_Config_Site"] = "Sys_Config.Sys_Config_Site";

            //Sys_State
            Relation["SysStateInfo.Sys_State_ID"] = "Sys_State.Sys_State_ID";
            Relation["SysStateInfo.Sys_State_CountryCode"] = "Sys_State.Sys_State_CountryCode";
            Relation["SysStateInfo.Sys_State_Code"] = "Sys_State.Sys_State_Code";
            Relation["SysStateInfo.Sys_State_CN"] = "Sys_State.Sys_State_CN";
            Relation["SysStateInfo.Sys_State_IsActive"] = "Sys_State.Sys_State_IsActive";


            //Sys_City
            Relation["SysCityInfo.Sys_City_ID"] = "Sys_City.Sys_City_ID";
            Relation["SysCityInfo.Sys_City_StateCode"] = "Sys_City.Sys_City_StateCode";
            Relation["SysCityInfo.Sys_City_Code"] = "Sys_City.Sys_City_Code";
            Relation["SysCityInfo.Sys_City_CN"] = "Sys_City.Sys_City_CN";
            Relation["SysCityInfo.Sys_City_IsActive"] = "Sys_City.Sys_City_IsActive";

            //Sys_County
            Relation["SysCountyInfo.Sys_County_ID"] = "Sys_County.Sys_County_ID";
            Relation["SysCountyInfo.Sys_County_CityCode"] = "Sys_County.Sys_County_CityCode";
            Relation["SysCountyInfo.Sys_County_Code"] = "Sys_County.Sys_County_Code";
            Relation["SysCountyInfo.Sys_County_CN"] = "Sys_County.Sys_County_CN";
            Relation["SysCountyInfo.Sys_County_IsActive"] = "Sys_County.Sys_County_IsActive";
            
            //Category
            Relation["CategoryInfo.Cate_ID"] = "Category.Cate_ID";
            Relation["CategoryInfo.Cate_ParentID"] = "Category.Cate_ParentID";
            Relation["CategoryInfo.Cate_Name"] = "Category.Cate_Name";
            Relation["CategoryInfo.Cate_TypeID"] = "Category.Cate_TypeID";
            Relation["CategoryInfo.Cate_Img"] = "Category.Cate_Img";
            Relation["CategoryInfo.Cate_ProductTypeID"] = "Category.Cate_ProductTypeID";
            Relation["CategoryInfo.Cate_Sort"] = "Category.Cate_Sort";
            Relation["CategoryInfo.Cate_IsFrequently"] = "Category.Cate_IsFrequently";
            Relation["CategoryInfo.Cate_IsActive"] = "Category.Cate_IsActive";
            Relation["CategoryInfo.Cate_Count_Brand"] = "Category.Cate_Count_Brand";
            Relation["CategoryInfo.Cate_Count_Product"] = "Category.Cate_Count_Product";
            Relation["CategoryInfo.Cate_SEO_Path"] = "Category.Cate_SEO_Path";
            Relation["CategoryInfo.Cate_SEO_Title"] = "Category.Cate_SEO_Title";
            Relation["CategoryInfo.Cate_SEO_Keyword"] = "Category.Cate_SEO_Keyword";
            Relation["CategoryInfo.Cate_SEO_Description"] = "Category.Cate_SEO_Description";
            Relation["CategoryInfo.Cate_Site"] = "Category.Cate_Site";

            //Brand
            Relation["BrandInfo.Brand_ID"] = "Brand.Brand_ID";
            Relation["BrandInfo.Brand_Name"] = "Brand.Brand_Name";
            Relation["BrandInfo.Brand_Img"] = "Brand.Brand_Img";
            Relation["BrandInfo.Brand_URL"] = "Brand.Brand_URL";
            Relation["BrandInfo.Brand_Description"] = "Brand.Brand_Description";
            Relation["BrandInfo.Brand_Sort"] = "Brand.Brand_Sort";
            Relation["BrandInfo.Brand_IsRecommend"] = "Brand.Brand_IsRecommend";
            Relation["BrandInfo.Brand_IsActive"] = "Brand.Brand_IsActive";
            Relation["BrandInfo.Brand_Site"] = "Brand.Brand_Site";

            //ProductType
            Relation["ProductType.ProductType_ID"] = "ProductType.ProductType_ID";
            Relation["ProductType.ProductType_Name"] = "ProductType.ProductType_Name";
            Relation["ProductType.ProductType_Sort"] = "ProductType.ProductType_Sort";
            Relation["ProductType.ProductType_IsActive"] = "ProductType.ProductType_IsActive";
            Relation["ProductType.ProductType_Site"] = "ProductType.ProductType_Site";

            //ProductType_Extend
            Relation["ProductTypeExtendInfo.ProductType_Extend_ID"] = "ProductType_Extend.ProductType_Extend_ID";
            Relation["ProductTypeExtendInfo.ProductType_Extend_ProductTypeID"] = "ProductType_Extend.ProductType_Extend_ProductTypeID";
            Relation["ProductTypeExtendInfo.ProductType_Extend_Name"] = "ProductType_Extend.ProductType_Extend_Name";
            Relation["ProductTypeExtendInfo.ProductType_Extend_Display"] = "ProductType_Extend.ProductType_Extend_Display";
            Relation["ProductTypeExtendInfo.ProductType_Extend_IsSearch"] = "ProductType_Extend.ProductType_Extend_IsSearch";
            Relation["ProductTypeExtendInfo.ProductType_Extend_Options"] = "ProductType_Extend.ProductType_Extend_Options";
            Relation["ProductTypeExtendInfo.ProductType_Extend_Default"] = "ProductType_Extend.ProductType_Extend_Default";
            Relation["ProductTypeExtendInfo.ProductType_Extend_IsActive"] = "ProductType_Extend.ProductType_Extend_IsActive";
            Relation["ProductTypeExtendInfo.ProductType_Extend_Sort"] = "ProductType_Extend.ProductType_Extend_Sort";
            Relation["ProductTypeExtendInfo.ProductType_Extend_Site"] = "ProductType_Extend.ProductType_Extend_Site";

            //Product_Review
            Relation["ProductReviewInfo.Product_Review_ID"] = "Product_Review.Product_Review_ID";
            Relation["ProductReviewInfo.Product_Review_ProductID"] = "Product_Review.Product_Review_ProductID";
            Relation["ProductReviewInfo.Product_Review_MemberID"] = "Product_Review.Product_Review_MemberID";
            Relation["ProductReviewInfo.Product_Review_Star"] = "Product_Review.Product_Review_Star";
            Relation["ProductReviewInfo.Product_Review_Subject"] = "Product_Review.Product_Review_Subject";
            Relation["ProductReviewInfo.Product_Review_Content"] = "Product_Review.Product_Review_Content";
            Relation["ProductReviewInfo.Product_Review_Useful"] = "Product_Review.Product_Review_Useful";
            Relation["ProductReviewInfo.Product_Review_Useless"] = "Product_Review.Product_Review_Useless";
            Relation["ProductReviewInfo.Product_Review_Addtime"] = "Product_Review.Product_Review_Addtime";
            Relation["ProductReviewInfo.Product_Review_IsShow"] = "Product_Review.Product_Review_IsShow";
            Relation["ProductReviewInfo.Product_Review_IsBuy"] = "Product_Review.Product_Review_IsBuy";
            Relation["ProductReviewInfo.Product_Review_IsGift"] = "Product_Review.Product_Review_IsGift";
            Relation["ProductReviewInfo.Product_Review_IsView"] = "Product_Review.Product_Review_IsView";
            Relation["ProductReviewInfo.Product_Review_IsRecommend"] = "Product_Review.Product_Review_IsRecommend";
            Relation["ProductReviewInfo.Product_Review_Site"] = "Product_Review.Product_Review_Site";

            //Product_Review_Config
            Relation["ProductReviewConfigInfo.Product_Review_Config_ID"] = "Product_Review_Config.Product_Review_Config_ID";
            Relation["ProductReviewConfigInfo.Product_Review_Config_ProductCount"] = "Product_Review_Config.Product_Review_Config_ProductCount";
            Relation["ProductReviewConfigInfo.Product_Review_Config_ListCount"] = "Product_Review_Config.Product_Review_Config_ListCount";
            Relation["ProductReviewConfigInfo.Product_Review_Config_Power"] = "Product_Review_Config.Product_Review_Config_Power";
            Relation["ProductReviewConfigInfo.Product_Review_giftcoin"] = "Product_Review_Config.Product_Review_giftcoin";
            Relation["ProductReviewConfigInfo.Product_Review_Recommendcoin"] = "Product_Review_Config.Product_Review_Recommendcoin";
            Relation["ProductReviewConfigInfo.Product_Review_Config_NoRecordTip"] = "Product_Review_Config.Product_Review_Config_NoRecordTip";
            Relation["ProductReviewConfigInfo.Product_Review_Config_VerifyCode_IsOpen"] = "Product_Review_Config.Product_Review_Config_VerifyCode_IsOpen";
            Relation["ProductReviewConfigInfo.Product_Review_Config_ManagerReply_Show"] = "Product_Review_Config.Product_Review_Config_ManagerReply_Show";
            Relation["ProductReviewConfigInfo.Product_Review_Config_Show_SuccessTip"] = "Product_Review_Config.Product_Review_Config_Show_SuccessTip";
            Relation["ProductReviewConfigInfo.Product_Review_Config_IsActive"] = "Product_Review_Config.Product_Review_Config_IsActive";
            Relation["ProductReviewConfigInfo.Product_Review_Config_Site"] = "Product_Review_Config.Product_Review_Config_Site";

            ////product_tag
            Relation["ProductTagInfo.Product_Tag_ID"] = "product_tag.Product_Tag_ID";
            Relation["ProductTagInfo.Product_Tag_Name"] = "product_tag.Product_Tag_Name";
            Relation["ProductTagInfo.Product_Tag_IsSupplier"] = "product_tag.Product_Tag_IsSupplier";
            Relation["ProductTagInfo.Product_Tag_SupplierID"] = "product_tag.Product_Tag_SupplierID";
            Relation["ProductTagInfo.Product_Tag_IsActive"] = "product_tag.Product_Tag_IsActive";
            Relation["ProductTagInfo.Product_Tag_Sort"] = "product_tag.Product_Tag_Sort";
            Relation["ProductTagInfo.Product_Tag_Site"] = "product_tag.Product_Tag_Site";
             

            //Product_Basic
            Relation["ProductInfo.Product_ID"] = "Product_Basic.Product_ID";
            Relation["ProductInfo.Product_Code"] = "Product_Basic.Product_Code";
            Relation["ProductInfo.Product_CateID"] = "Product_Basic.Product_CateID";
            Relation["ProductInfo.Product_BrandID"] = "Product_Basic.Product_BrandID";
            Relation["ProductInfo.Product_TypeID"] = "Product_Basic.Product_TypeID";
            Relation["ProductInfo.Product_SupplierID"] = "Product_Basic.Product_SupplierID";
            Relation["ProductInfo.Product_Supplier_CommissionCateID"] = "Product_Basic.Product_Supplier_CommissionCateID";
            Relation["ProductInfo.Product_Name"] = "Product_Basic.Product_Name";
            Relation["ProductInfo.Product_NameInitials"] = "Product_Basic.Product_NameInitials";
            Relation["ProductInfo.Product_SubName"] = "Product_Basic.Product_SubName";
            Relation["ProductInfo.Product_SubNameInitials"] = "Product_Basic.Product_SubNameInitials";
            Relation["ProductInfo.Product_MKTPrice"] = "Product_Basic.Product_MKTPrice";
            Relation["ProductInfo.Product_GroupPrice"] = "Product_Basic.Product_GroupPrice";
            Relation["ProductInfo.Product_PurchasingPrice"] = "Product_Basic.Product_PurchasingPrice";
            Relation["ProductInfo.Product_Price"] = "Product_Basic.Product_Price";
            Relation["ProductInfo.Product_PriceUnit"] = "Product_Basic.Product_PriceUnit";
            Relation["ProductInfo.Product_Unit"] = "Product_Basic.Product_Unit";
            Relation["ProductInfo.Product_GroupNum"] = "Product_Basic.Product_GroupNum";
            Relation["ProductInfo.Product_Note"] = "Product_Basic.Product_Note";
            Relation["ProductInfo.Product_Weight"] = "Product_Basic.Product_Weight";
            Relation["ProductInfo.Product_Img"] = "Product_Basic.Product_Img";
            Relation["ProductInfo.Product_Publisher"] = "Product_Basic.Product_Publisher";
            Relation["ProductInfo.Product_StockAmount"] = "Product_Basic.Product_StockAmount";
            Relation["ProductInfo.Product_SaleAmount"] = "Product_Basic.Product_SaleAmount";
            Relation["ProductInfo.Product_Review_Count"] = "Product_Basic.Product_Review_Count";
            Relation["ProductInfo.Product_Review_ValidCount"] = "Product_Basic.Product_Review_ValidCount";
            Relation["ProductInfo.Product_Review_Average"] = "Product_Basic.Product_Review_Average";
            Relation["ProductInfo.Product_IsInsale"] = "Product_Basic.Product_IsInsale";
            Relation["ProductInfo.Product_IsGroupBuy"] = "Product_Basic.Product_IsGroupBuy";
            Relation["ProductInfo.Product_IsCoinBuy"] = "Product_Basic.Product_IsCoinBuy";
            Relation["ProductInfo.Product_IsFavor"] = "Product_Basic.Product_IsFavor";
            Relation["ProductInfo.Product_IsGift"] = "Product_Basic.Product_IsGift";
            Relation["ProductInfo.Product_IsGiftCoin"] = "Product_Basic.Product_IsGiftCoin";
            Relation["ProductInfo.Product_Gift_Coin"] = "Product_Basic.Product_Gift_Coin";
            Relation["ProductInfo.Product_CoinBuy_Coin"] = "Product_Basic.Product_CoinBuy_Coin";
            Relation["ProductInfo.Product_IsAudit"] = "Product_Basic.Product_IsAudit";
            Relation["ProductInfo.Product_Addtime"] = "Product_Basic.Product_Addtime";
            Relation["ProductInfo.Product_Intro"] = "Product_Basic.Product_Intro";
            Relation["ProductInfo.Product_AlertAmount"] = "Product_Basic.Product_AlertAmount";
            Relation["ProductInfo.Product_UsableAmount"] = "Product_Basic.Product_UsableAmount";
            Relation["ProductInfo.Product_IsNoStock"] = "Product_Basic.Product_IsNoStock";
            Relation["ProductInfo.Product_Spec"] = "Product_Basic.Product_Spec";
            Relation["ProductInfo.Product_Maker"] = "Product_Basic.Product_Maker";
            Relation["ProductInfo.Product_Description"] = "Product_Basic.Product_Description";
            Relation["ProductInfo.Product_Sort"] = "Product_Basic.Product_Sort";
            Relation["ProductInfo.Product_QuotaAmount"] = "Product_Basic.Product_QuotaAmount";
            Relation["ProductInfo.Product_IsListShow"] = "Product_Basic.Product_IsListShow";
            Relation["ProductInfo.Product_GroupCode"] = "Product_Basic.Product_GroupCode";
            Relation["ProductInfo.Product_Hits"] = "Product_Basic.Product_Hits";
            Relation["ProductInfo.Product_Site"] = "Product_Basic.Product_Site";
            Relation["ProductInfo.Product_SEO_Title"] = "Product_Basic.Product_SEO_Title";
            Relation["ProductInfo.Product_SEO_Keyword"] = "Product_Basic.Product_SEO_Keyword";
            Relation["ProductInfo.Product_SEO_Description"] = "Product_Basic.Product_SEO_Description";
            Relation["ProductInfo.NewID()"] = "NEWID()";
            Relation["ProductInfo.U_Product_Parameters"] = "Product_Basic.U_Product_Parameters";
            Relation["ProductInfo.U_Product_SalesByProxy"] = "Product_Basic.U_Product_SalesByProxy";
            Relation["ProductInfo.U_Product_Shipper"] = "Product_Basic.U_Product_Shipper";
            Relation["ProductInfo.U_Product_DeliveryCycle"] = "Product_Basic.U_Product_DeliveryCycle";
            Relation["ProductInfo.U_Product_MinBook"] = "Product_Basic.U_Product_MinBook";
            Relation["ProductInfo.Product_PriceType"] = "Product_Basic.Product_PriceType";
            Relation["ProductInfo.Product_ManualFee"] = "Product_Basic.Product_ManualFee";
            Relation["ProductInfo.Product_LibraryImg"] = "Product_Basic.Product_LibraryImg";
            Relation["LEN(ProductInfo.Product_LibraryImg)"] = "LEN(Product_Basic.Product_LibraryImg)";
            Relation["ProductInfo.Product_LibraryImg"] = "Product_Basic.Product_LibraryImg";
            Relation["ProductInfo.Product_State_Name"] = "Product_Basic.Product_State_Name";
            Relation["ProductInfo.Product_City_Name"] = "Product_Basic.Product_City_Name";
            Relation["ProductInfo.Product_County_Name"] = "Product_Basic.Product_County_Name";

           
        

            //Product_Price
            Relation["ProductPriceInfo.Product_Price_ID"] = "Product_Price.Product_Price_ID";
            Relation["ProductPriceInfo.Product_Price_ProcutID"] = "Product_Price.Product_Price_ProcutID";
            Relation["ProductPriceInfo.Product_Price_MemberGradeID"] = "Product_Price.Product_Price_MemberGradeID";
            Relation["ProductPriceInfo.Product_Price_Price"] = "Product_Price.Product_Price_Price";

            //Product_Audit_Reason
            Relation["ProductAuditReasonInfo.Product_Audit_Reason_ID"] = "Product_Audit_Reason.Product_Audit_Reason_ID";
            Relation["ProductAuditReasonInfo.Product_Audit_Reason_Note"] = "Product_Audit_Reason.Product_Audit_Reason_Note";

            //Product_Extend
            Relation["ProductExtendInfo.Product_Extend_ID"] = "Product_Extend.Product_Extend_ID";
            Relation["ProductExtendInfo.Product_Extend_ProductID"] = "Product_Extend.Product_Extend_ProductID";
            Relation["ProductExtendInfo.Product_Extend_ExtendID"] = "Product_Extend.Product_Extend_ExtendID";
            Relation["ProductExtendInfo.Product_Extend_Value"] = "Product_Extend.Product_Extend_Value";
            Relation["ProductExtendInfo.Product_Extend_Img"] = "Product_Extend.Product_Extend_Img";

            //Product_HistoryPrice
            Relation["ProductHistoryPriceInfo.History_ID"] = "Product_HistoryPrice.History_ID";
            Relation["ProductHistoryPriceInfo.History_SysName"] = "Product_HistoryPrice.History_SysName";
            Relation["ProductHistoryPriceInfo.History_ProductID"] = "Product_HistoryPrice.History_ProductID";
            Relation["ProductHistoryPriceInfo.History_PriceType"] = "Product_HistoryPrice.History_PriceType";
            Relation["ProductHistoryPriceInfo.History_Price_Original"] = "Product_HistoryPrice.History_Price_Original";
            Relation["ProductHistoryPriceInfo.History_Price_New"] = "Product_HistoryPrice.History_Price_New";
            Relation["ProductHistoryPriceInfo.History_Addtime"] = "Product_HistoryPrice.History_Addtime";

            //Shopping_Ask
            Relation["ShoppingAskInfo.Ask_ID"] = "Shopping_Ask.Ask_ID";
            Relation["ShoppingAskInfo.Ask_Type"] = "Shopping_Ask.Ask_Type";
            Relation["ShoppingAskInfo.Ask_Contact"] = "Shopping_Ask.Ask_Contact";
            Relation["ShoppingAskInfo.Ask_Content"] = "Shopping_Ask.Ask_Content";
            Relation["ShoppingAskInfo.Ask_Reply"] = "Shopping_Ask.Ask_Reply";
            Relation["ShoppingAskInfo.Ask_Addtime"] = "Shopping_Ask.Ask_Addtime";
            Relation["ShoppingAskInfo.Ask_SupplierID"] = "Shopping_Ask.Ask_SupplierID";
            Relation["ShoppingAskInfo.Ask_MemberID"] = "Shopping_Ask.Ask_MemberID";
            Relation["ShoppingAskInfo.Ask_ProductID"] = "Shopping_Ask.Ask_ProductID";
            Relation["ShoppingAskInfo.Ask_Pleasurenum"] = "Shopping_Ask.Ask_Pleasurenum";
            Relation["ShoppingAskInfo.Ask_Displeasure"] = "Shopping_Ask.Ask_Displeasure";
            Relation["ShoppingAskInfo.Ask_Isreply"] = "Shopping_Ask.Ask_Isreply";
            Relation["ShoppingAskInfo.Ask_IsCheck"] = "Shopping_Ask.Ask_IsCheck";
            Relation["ShoppingAskInfo.Ask_Site"] = "Shopping_Ask.Ask_Site";

            //Stockout_Booking
            Relation["StockoutBookingInfo.Stockout_ID"] = "Stockout_Booking.Stockout_ID";
            Relation["StockoutBookingInfo.Stockout_Product_Name"] = "Stockout_Booking.Stockout_Product_Name";
            Relation["StockoutBookingInfo.Stockout_Product_Describe"] = "Stockout_Booking.Stockout_Product_Describe";
            Relation["StockoutBookingInfo.Stockout_Member_Name"] = "Stockout_Booking.Stockout_Member_Name";
            Relation["StockoutBookingInfo.Stockout_Member_Tel"] = "Stockout_Booking.Stockout_Member_Tel";
            Relation["StockoutBookingInfo.Stockout_Member_Email"] = "Stockout_Booking.Stockout_Member_Email";
            Relation["StockoutBookingInfo.Stockout_IsRead"] = "Stockout_Booking.Stockout_IsRead";
            Relation["StockoutBookingInfo.Stockout_Addtime"] = "Stockout_Booking.Stockout_Addtime";
            Relation["StockoutBookingInfo.Stockout_Site"] = "Stockout_Booking.Stockout_Site";


            //RBAC_User
            Relation["RBACUserInfo.RBAC_User_ID"] = "RBAC_User.RBAC_User_ID";
            Relation["RBACUserInfo.RBAC_User_GroupID"] = "RBAC_User.RBAC_User_GroupID";
            Relation["RBACUserInfo.RBAC_User_Name"] = "RBAC_User.RBAC_User_Name";
            Relation["RBACUserInfo.RBAC_User_Password"] = "RBAC_User.RBAC_User_Password";
            Relation["RBACUserInfo.RBAC_User_LastLogin"] = "RBAC_User.RBAC_User_LastLogin";
            Relation["RBACUserInfo.RBAC_User_LastLoginIP"] = "RBAC_User.RBAC_User_LastLoginIP";
            Relation["RBACUserInfo.RBAC_User_Addtime"] = "RBAC_User.RBAC_User_Addtime";
            Relation["RBACUserInfo.RBAC_User_Site"] = "RBAC_User.RBAC_User_Site";

            //RBAC_UserGroup
            Relation["RBACUserGroupInfo.RBAC_UserGroup_ID"] = "RBAC_UserGroup.RBAC_UserGroup_ID";
            Relation["RBACUserGroupInfo.RBAC_UserGroup_Name"] = "RBAC_UserGroup.RBAC_UserGroup_Name";
            Relation["RBACUserGroupInfo.RBAC_UserGroup_ParentID"] = "RBAC_UserGroup.RBAC_UserGroup_ParentID";
            Relation["RBACUserGroupInfo.RBAC_UserGroup_Site"] = "RBAC_UserGroup.RBAC_UserGroup_Site";

            //Notice_Cate
            Relation["NoticeCateInfo.Notice_Cate_ID"] = "Notice_Cate.Notice_Cate_ID";
            Relation["NoticeCateInfo.Notice_Cate_Name"] = "Notice_Cate.Notice_Cate_Name";
            Relation["NoticeCateInfo.Notice_Cate_Sort"] = "Notice_Cate.Notice_Cate_Sort";
            Relation["NoticeCateInfo.Notice_Cate_Site"] = "Notice_Cate.Notice_Cate_Site";

            //Notice
            Relation["NoticeInfo.Notice_ID"] = "Notice.Notice_ID";
            Relation["NoticeInfo.Notice_Cate"] = "Notice.Notice_Cate";
            Relation["NoticeInfo.Notice_IsHot"] = "Notice.Notice_IsHot";
            Relation["NoticeInfo.Notice_IsAudit"] = "Notice.Notice_IsAudit";
            Relation["NoticeInfo.Notice_SysUserID"] = "Notice.Notice_SysUserID";
            Relation["NoticeInfo.Notice_SellerID"] = "Notice.Notice_SellerID";
            Relation["NoticeInfo.Notice_Title"] = "Notice.Notice_Title";
            Relation["NoticeInfo.Notice_Content"] = "Notice.Notice_Content";
            Relation["NoticeInfo.Notice_Addtime"] = "Notice.Notice_Addtime";
            Relation["NoticeInfo.Notice_Site"] = "Notice.Notice_Site";

            //About
            Relation["AboutInfo.About_ID"] = "About.About_ID";
            Relation["AboutInfo.About_IsActive"] = "About.About_IsActive";
            Relation["AboutInfo.About_Title"] = "About.About_Title";
            Relation["AboutInfo.About_Sign"] = "About.About_Sign";
            Relation["AboutInfo.About_Content"] = "About.About_Content";
            Relation["AboutInfo.About_Sort"] = "About.About_Sort";
            Relation["AboutInfo.About_Site"] = "About.About_Site";

            //Help_Cate
            Relation["HelpCateInfo.Help_Cate_ID"] = "Help_Cate.Help_Cate_ID";
            Relation["HelpCateInfo.Help_Cate_ParentID"] = "Help_Cate.Help_Cate_ParentID";
            Relation["HelpCateInfo.Help_Cate_Name"] = "Help_Cate.Help_Cate_Name";
            Relation["HelpCateInfo.Help_Cate_Sort"] = "Help_Cate.Help_Cate_Sort";
            Relation["HelpCateInfo.Help_Cate_Site"] = "Help_Cate.Help_Cate_Site";

            //Help
            Relation["HelpInfo.Help_ID"] = "Help.Help_ID";
            Relation["HelpInfo.Help_CateID"] = "Help.Help_CateID";
            Relation["HelpInfo.Help_IsFAQ"] = "Help.Help_IsFAQ";
            Relation["HelpInfo.Help_IsActive"] = "Help.Help_IsActive";
            Relation["HelpInfo.Help_Title"] = "Help.Help_Title";
            Relation["HelpInfo.Help_Content"] = "Help.Help_Content";
            Relation["HelpInfo.Help_Sort"] = "Help.Help_Sort";
            Relation["HelpInfo.Help_Site"] = "Help.Help_Site";

            //AD_Position
            Relation["ADPositionInfo.Ad_Position_ID"] = "AD_Position.Ad_Position_ID";
            Relation["ADPositionInfo.Ad_Position_ChannelID"] = "AD_Position.Ad_Position_ChannelID";
            Relation["ADPositionInfo.Ad_Position_Name"] = "AD_Position.Ad_Position_Name";
            Relation["ADPositionInfo.Ad_Position_Value"] = "AD_Position.Ad_Position_Value";
            Relation["ADPositionInfo.Ad_Position_Width"] = "AD_Position.Ad_Position_Width";
            Relation["ADPositionInfo.Ad_Position_Height"] = "AD_Position.Ad_Position_Height";
            Relation["ADPositionInfo.Ad_Position_IsActive"] = "AD_Position.Ad_Position_IsActive";
            Relation["ADPositionInfo.Ad_Position_Site"] = "AD_Position.Ad_Position_Site";
            Relation["ADPositionInfo.U_Ad_Position_Marketing"] = "AD_Position.U_Ad_Position_Marketing";
            Relation["ADPositionInfo.U_Ad_Position_Price"] = "AD_Position.U_Ad_Position_Price";

            //AD
            Relation["ADInfo.Ad_ID"] = "AD.Ad_ID";
            Relation["ADInfo.Ad_Title"] = "AD.Ad_Title";
            Relation["ADInfo.Ad_Kind"] = "AD.Ad_Kind";
            Relation["ADInfo.Ad_MediaKind"] = "AD.Ad_MediaKind";
            Relation["ADInfo.Ad_Media"] = "AD.Ad_Media";
            Relation["ADInfo.Ad_Link"] = "AD.Ad_Link";
            Relation["ADInfo.Ad_Show_Freq"] = "AD.Ad_Show_Freq";
            Relation["ADInfo.Ad_Show_times"] = "AD.Ad_Show_times";
            Relation["ADInfo.Ad_Hits"] = "AD.Ad_Hits";
            Relation["ADInfo.Ad_StartDate"] = "AD.Ad_StartDate";
            Relation["ADInfo.Ad_EndDate"] = "AD.Ad_EndDate";
            Relation["ADInfo.Ad_IsContain"] = "AD.Ad_IsContain";
            Relation["ADInfo.Ad_Propertys"] = "AD.Ad_Propertys";
            Relation["ADInfo.Ad_Sort"] = "AD.Ad_Sort";
            Relation["ADInfo.Ad_IsActive"] = "AD.Ad_IsActive";
            Relation["ADInfo.Ad_Site"] = "AD.Ad_Site";
            Relation["ADInfo.U_Ad_Audit"] = "AD.U_Ad_Audit";
            Relation["ADInfo.U_Ad_Advertiser"] = "AD.U_Ad_Advertiser";
            Relation["ADInfo.Ad_Addtime"] = "AD.Ad_Addtime";

            //AD_Position_Channel
            Relation["ADPositionChannelInfo.AD_Position_Channel_ID"] = "AD_Position_Channel.AD_Position_Channel_ID";
            Relation["ADPositionChannelInfo.AD_Position_Channel_Name"] = "AD_Position_Channel.AD_Position_Channel_Name";
            Relation["ADPositionChannelInfo.AD_Position_Channel_Note"] = "AD_Position_Channel.AD_Position_Channel_Note";
            Relation["ADPositionChannelInfo.AD_Position_Channel_Site"] = "AD_Position_Channel.AD_Position_Channel_Site";

            //FriendlyLink_Cate
            Relation["FriendlyLinkCateInfo.FriendlyLink_Cate_ID"] = "FriendlyLink_Cate.FriendlyLink_Cate_ID";
            Relation["FriendlyLinkCateInfo.FriendlyLink_Cate_Name"] = "FriendlyLink_Cate.FriendlyLink_Cate_Name";
            Relation["FriendlyLinkCateInfo.FriendlyLink_Cate_Sort"] = "FriendlyLink_Cate.FriendlyLink_Cate_Sort";
            Relation["FriendlyLinkCateInfo.FriendlyLink_Cate_Site"] = "FriendlyLink_Cate.FriendlyLink_Cate_Site";

            //FriendlyLink
            Relation["FriendlyLinkInfo.FriendlyLink_ID"] = "FriendlyLink.FriendlyLink_ID";
            Relation["FriendlyLinkInfo.FriendlyLink_CateID"] = "FriendlyLink.FriendlyLink_CateID";
            Relation["FriendlyLinkInfo.FriendlyLink_Name"] = "FriendlyLink.FriendlyLink_Name";
            Relation["FriendlyLinkInfo.FriendlyLink_Img"] = "FriendlyLink.FriendlyLink_Img";
            Relation["FriendlyLinkInfo.FriendlyLink_URL"] = "FriendlyLink.FriendlyLink_URL";
            Relation["FriendlyLinkInfo.FriendlyLink_IsActive"] = "FriendlyLink.FriendlyLink_IsActive";
            Relation["FriendlyLinkInfo.FriendlyLink_IsImg"] = "FriendlyLink.FriendlyLink_IsImg";
            Relation["FriendlyLinkInfo.FriendlyLink_Site"] = "FriendlyLink.FriendlyLink_Site";
            Relation["FriendlyLinkInfo.FriendlyLink_Sort"] = "FriendlyLink.FriendlyLink_Sort";

            //RBAC_ResourceGroup
            Relation["RBACResourceGroupInfo.RBAC_ResourceGroup_ID"] = "RBAC_ResourceGroup.RBAC_ResourceGroup_ID";
            Relation["RBACResourceGroupInfo.RBAC_ResourceGroup_Name"] = "RBAC_ResourceGroup.RBAC_ResourceGroup_Name";
            Relation["RBACResourceGroupInfo.RBAC_ResourceGroup_ParentID"] = "RBAC_ResourceGroup.RBAC_ResourceGroup_ParentID";
            Relation["RBACResourceGroupInfo.RBAC_ResourceGroup_Site"] = "RBAC_ResourceGroup.RBAC_ResourceGroup_Site";

            //RBAC_Resource
            Relation["RBACResourceInfo.RBAC_Resource_ID"] = "RBAC_Resource.RBAC_Resource_ID";
            Relation["RBACResourceInfo.RBAC_Resource_GroupID"] = "RBAC_Resource.RBAC_Resource_GroupID";
            Relation["RBACResourceInfo.RBAC_Resource_Name"] = "RBAC_Resource.RBAC_Resource_Name";
            Relation["RBACResourceInfo.RBAC_Resource_Site"] = "RBAC_Resource.RBAC_Resource_Site";

            //RBAC_Privilege
            Relation["RBACPrivilegeInfo.RBAC_Privilege_ID"] = "RBAC_Privilege.RBAC_Privilege_ID";
            Relation["RBACPrivilegeInfo.RBAC_Privilege_ResourceID"] = "RBAC_Privilege.RBAC_Privilege_ResourceID";
            Relation["RBACPrivilegeInfo.RBAC_Privilege_Name"] = "RBAC_Privilege.RBAC_Privilege_Name";
            Relation["RBACPrivilegeInfo.RBAC_Privilege_IsActive"] = "RBAC_Privilege.RBAC_Privilege_IsActive";
            Relation["RBACPrivilegeInfo.RBAC_Privilege_Addtime"] = "RBAC_Privilege.RBAC_Privilege_Addtime";

            //RBAC_Role
            Relation["RBACRoleInfo.RBAC_Role_ID"] = "RBAC_Role.RBAC_Role_ID";
            Relation["RBACRoleInfo.RBAC_Role_Name"] = "RBAC_Role.RBAC_Role_Name";
            Relation["RBACRoleInfo.RBAC_Role_Description"] = "RBAC_Role.RBAC_Role_Description";
            Relation["RBACRoleInfo.RBAC_Role_IsSystem"] = "RBAC_Role.RBAC_Role_IsSystem";
            Relation["RBACRoleInfo.RBAC_Role_Site"] = "RBAC_Role.RBAC_Role_Site";

            //Member
            Relation["MemberInfo.Member_ID"] = "Member.Member_ID";
            Relation["MemberInfo.Member_Type"] = "Member.Member_Type";
            Relation["MemberInfo.Member_Email"] = "Member.Member_Email";
            Relation["MemberInfo.Member_Emailverify"] = "Member.Member_Emailverify";
            Relation["MemberInfo.Member_LoginMobile"] = "Member.Member_LoginMobile";
            Relation["MemberInfo.Member_LoginMobileverify"] = "Member.Member_LoginMobileverify";
            Relation["MemberInfo.Member_NickName"] = "Member.Member_NickName";
            Relation["MemberInfo.Member_Password"] = "Member.Member_Password";
            Relation["MemberInfo.Member_VerifyCode"] = "Member.Member_VerifyCode";
            Relation["MemberInfo.Member_LoginCount"] = "Member.Member_LoginCount";
            Relation["MemberInfo.Member_LastLogin_IP"] = "Member.Member_LastLogin_IP";
            Relation["MemberInfo.Member_LastLogin_Time"] = "Member.Member_LastLogin_Time";
            Relation["MemberInfo.Member_CoinCount"] = "Member.Member_CoinCount";
            Relation["MemberInfo.Member_CoinRemain"] = "Member.Member_CoinRemain";
            Relation["MemberInfo.Member_Addtime"] = "Member.Member_Addtime";
            Relation["MemberInfo.Member_Trash"] = "Member.Member_Trash";
            Relation["MemberInfo.Member_Grade"] = "Member.Member_Grade";
            Relation["MemberInfo.Member_Account"] = "Member.Member_Account";
            Relation["MemberInfo.Member_Frozen"] = "Member.Member_Frozen";
            Relation["MemberInfo.Member_AllowSysEmail"] = "Member.Member_AllowSysEmail";
            Relation["MemberInfo.Member_Site"] = "Member.Member_Site";
            Relation["MemberInfo.Member_Source"] = "Member.Member_Source";
            Relation["MemberInfo.Member_RegIP"] = "Member.Member_RegIP";
            Relation["MemberInfo.Member_Status"] = "Member.Member_Status";
            Relation["MemberInfo.Member_AuditStatus"] = "Member.Member_AuditStatus";
            Relation["MemberInfo.Member_Cert_Status"] = "Member.Member_Cert_Status";
            Relation["MemberInfo.Member_VfinanceID"] = "Member.Member_VfinanceID";
            Relation["MemberInfo.Member_ERP_StoreID"] = "Member.Member_ERP_StoreID";
            Relation["MemberInfo.Member_SupplierID"] = "Member.Member_SupplierID";
            Relation["MemberInfo.Member_Company_Introduce"] = "Member.Member_Company_Introduce";
            Relation["MemberInfo.Member_Company_Contact"] = "Member.Member_Company_Contact";

            //Member_Profile
            Relation["MemberProfileInfo.Member_Profile_ID"] = "Member_Profile.Member_Profile_ID";
            Relation["MemberProfileInfo.Member_Profile_MemberID"] = "Member_Profile.Member_Profile_MemberID";
            Relation["MemberProfileInfo.Member_Name"] = "Member_Profile.Member_Name";
            Relation["MemberProfileInfo.Member_Sex"] = "Member_Profile.Member_Sex";
            Relation["MemberProfileInfo.Member_Birthday"] = "Member_Profile.Member_Birthday";
            Relation["MemberProfileInfo.Member_Occupational"] = "Member_Profile.Member_Occupational";
            Relation["MemberProfileInfo.Member_Education"] = "Member_Profile.Member_Education";
            Relation["MemberProfileInfo.Member_Income"] = "Member_Profile.Member_Income";
            Relation["MemberProfileInfo.Member_StreetAddress"] = "Member_Profile.Member_StreetAddress";
            Relation["MemberProfileInfo.Member_County"] = "Member_Profile.Member_County";
            Relation["MemberProfileInfo.Member_City"] = "Member_Profile.Member_City";
            Relation["MemberProfileInfo.Member_State"] = "Member_Profile.Member_State";
            Relation["MemberProfileInfo.Member_Country"] = "Member_Profile.Member_Country";
            Relation["MemberProfileInfo.Member_Zip"] = "Member_Profile.Member_Zip";
            Relation["MemberProfileInfo.Member_Phone_Countrycode"] = "Member_Profile.Member_Phone_Countrycode";
            Relation["MemberProfileInfo.Member_Phone_Areacode"] = "Member_Profile.Member_Phone_Areacode";
            Relation["MemberProfileInfo.Member_Phone_Number"] = "Member_Profile.Member_Phone_Number";
            Relation["MemberProfileInfo.Member_Mobile"] = "Member_Profile.Member_Mobile";
            Relation["MemberProfileInfo.Member_Company"] = "Member_Profile.Member_Company";
            Relation["MemberProfileInfo.Member_Fax"] = "Member_Profile.Member_Fax";
            Relation["MemberProfileInfo.Member_QQ"] = "Member_Profile.Member_QQ";
            Relation["MemberProfileInfo.Member_OrganizationCode"] = "Member_Profile.Member_OrganizationCode";
            Relation["MemberProfileInfo.Member_BusinessCode"] = "Member_Profile.Member_BusinessCode";
            Relation["MemberProfileInfo.Member_SealImg"] = "Member_Profile.Member_SealImg";
            Relation["MemberProfileInfo.Member_Corporate"] = "Member_Profile.Member_Corporate";
            Relation["MemberProfileInfo.Member_CorporateMobile"] = "Member_Profile.Member_CorporateMobile";
            Relation["MemberProfileInfo.Member_RegisterFunds"] = "Member_Profile.Member_RegisterFunds";
            Relation["MemberProfileInfo.Member_TaxationCode"] = "Member_Profile.Member_TaxationCode";
            Relation["MemberProfileInfo.Member_BankAccountCode"] = "Member_Profile.Member_BankAccountCode";
            Relation["MemberProfileInfo.Member_HeadImg"] = "Member_Profile.Member_HeadImg";

            //Member_Log
            Relation["MemberLogInfo.Log_ID"] = "Member_Log.Log_ID";
            Relation["MemberLogInfo.Log_Member_ID"] = "Member_Log.Log_Member_ID";
            Relation["MemberLogInfo.Log_Member_Action"] = "Member_Log.Log_Member_Action";
            Relation["MemberLogInfo.Log_Addtime"] = "Member_Log.Log_Addtime";


            //Member_Grade
            Relation["MemberGradeInfo.Member_Grade_ID"] = "Member_Grade.Member_Grade_ID";
            Relation["MemberGradeInfo.Member_Grade_Name"] = "Member_Grade.Member_Grade_Name";
            Relation["MemberGradeInfo.Member_Grade_Percent"] = "Member_Grade.Member_Grade_Percent";
            Relation["MemberGradeInfo.Member_Grade_Default"] = "Member_Grade.Member_Grade_Default";
            Relation["MemberGradeInfo.Member_Grade_RequiredCoin"] = "Member_Grade.Member_Grade_RequiredCoin";
            Relation["MemberGradeInfo.Member_Grade_CoinRate"] = "Member_Grade.Member_Grade_CoinRate";
            Relation["MemberGradeInfo.Member_Grade_Addtime"] = "Member_Grade.Member_Grade_Addtime";
            Relation["MemberGradeInfo.Member_Grade_Site"] = "Member_Grade.Member_Grade_Site";


            //Member_Consumption
            Relation["MemberConsumptionInfo.Consump_ID"] = "Member_Consumption.Consump_ID";
            Relation["MemberConsumptionInfo.Consump_MemberID"] = "Member_Consumption.Consump_MemberID";
            Relation["MemberConsumptionInfo.Consump_CoinRemain"] = "Member_Consumption.Consump_CoinRemain";
            Relation["MemberConsumptionInfo.Consump_Coin"] = "Member_Consumption.Consump_Coin";
            Relation["MemberConsumptionInfo.Consump_Reason"] = "Member_Consumption.Consump_Reason";
            Relation["MemberConsumptionInfo.Consump_Addtime"] = "Member_Consumption.Consump_Addtime";

            //Member_Favorites
            Relation["MemberFavoritesInfo.Member_Favorites_ID"] = "Member_Favorites.Member_Favorites_ID";
            Relation["MemberFavoritesInfo.Member_Favorites_MemberID"] = "Member_Favorites.Member_Favorites_MemberID";
            Relation["MemberFavoritesInfo.Member_Favorites_Type"] = "Member_Favorites.Member_Favorites_Type";
            Relation["MemberFavoritesInfo.Member_Favorites_TargetID"] = "Member_Favorites.Member_Favorites_TargetID";
            Relation["MemberFavoritesInfo.Member_Favorites_Addtime"] = "Member_Favorites.Member_Favorites_Addtime";
            Relation["MemberFavoritesInfo.Member_Favorites_Site"] = "Member_Favorites.Member_Favorites_Site";

            //Feedback
            Relation["FeedBackInfo.Feedback_ID"] = "Feedback.Feedback_ID";
            Relation["FeedBackInfo.Feedback_Type"] = "Feedback.Feedback_Type";
            Relation["Feedbackinfo.Feedback_SupplierID"] = "Feedback.Feedback_SupplierID";//
            Relation["FeedBackInfo.Feedback_MemberID"] = "Feedback.Feedback_MemberID";
            Relation["FeedBackInfo.Feedback_Name"] = "Feedback.Feedback_Name";
            Relation["FeedBackInfo.Feedback_Tel"] = "Feedback.Feedback_Tel";
            Relation["FeedBackInfo.Feedback_Email"] = "Feedback.Feedback_Email";
            Relation["FeedBackInfo.Feedback_CompanyName"] = "Feedback.Feedback_CompanyName";
            Relation["FeedBackInfo.Feedback_Content"] = "Feedback.Feedback_Content";
            Relation["FeedBackInfo.Feedback_Attachment"] = "Feedback.Feedback_Attachment";
            Relation["FeedBackInfo.Feedback_Addtime"] = "Feedback.Feedback_Addtime";
            Relation["FeedBackInfo.Feedback_IsRead"] = "Feedback.Feedback_IsRead";
            Relation["FeedBackInfo.Feedback_Reply_IsRead"] = "Feedback.Feedback_Reply_IsRead";
            Relation["FeedBackInfo.Feedback_Reply_Content"] = "Feedback.Feedback_Reply_Content";
            Relation["FeedBackInfo.Feedback_Reply_Addtime"] = "Feedback.Feedback_Reply_Addtime";
            Relation["FeedBackInfo.Feedback_Site"] = "Feedback.Feedback_Site";



            Relation["FeedBackInfo.Feedback_Address"] = "Feedback.Feedback_Address";
            Relation["FeedBackInfo.Feedback_Amount"] = "Feedback.Feedback_Amount";
            Relation["FeedBackInfo.Feedback_Note"] = "Feedback.Feedback_Note";

            //Member_Address
            Relation["MemberAddressInfo.Member_Address_ID"] = "Member_Address.Member_Address_ID";
            Relation["MemberAddressInfo.Member_Address_MemberID"] = "Member_Address.Member_Address_MemberID";
            Relation["MemberAddressInfo.Member_Address_Country"] = "Member_Address.Member_Address_Country";
            Relation["MemberAddressInfo.Member_Address_State"] = "Member_Address.Member_Address_State";
            Relation["MemberAddressInfo.Member_Address_City"] = "Member_Address.Member_Address_City";
            Relation["MemberAddressInfo.Member_Address_County"] = "Member_Address.Member_Address_County";
            Relation["MemberAddressInfo.Member_Address_StreetAddress"] = "Member_Address.Member_Address_StreetAddress";
            Relation["MemberAddressInfo.Member_Address_Zip"] = "Member_Address.Member_Address_Zip";
            Relation["MemberAddressInfo.Member_Address_Name"] = "Member_Address.Member_Address_Name";
            Relation["MemberAddressInfo.Member_Address_Phone_Countrycode"] = "Member_Address.Member_Address_Phone_Countrycode";
            Relation["MemberAddressInfo.Member_Address_Phone_Areacode"] = "Member_Address.Member_Address_Phone_Areacode";
            Relation["MemberAddressInfo.Member_Address_Phone_Number"] = "Member_Address.Member_Address_Phone_Number";
            Relation["MemberAddressInfo.Member_Address_Mobile"] = "Member_Address.Member_Address_Mobile";
            Relation["MemberAddressInfo.Member_Address_IsDefault"] = "Member_Address.Member_Address_IsDefault";
            Relation["MemberAddressInfo.Member_Address_Site"] = "Member_Address.Member_Address_Site";

            //Member_Invoice
            Relation["MemberInvoiceInfo.Invoice_ID"] = "Member_Invoice.Invoice_ID";
            Relation["MemberInvoiceInfo.Invoice_MemberID"] = "Member_Invoice.Invoice_MemberID";
            Relation["MemberInvoiceInfo.Invoice_Type"] = "Member_Invoice.Invoice_Type";
            Relation["MemberInvoiceInfo.Invoice_Title"] = "Member_Invoice.Invoice_Title";
            Relation["MemberInvoiceInfo.Invoice_Details"] = "Member_Invoice.Invoice_Details";
            Relation["MemberInvoiceInfo.Invoice_FirmName"] = "Member_Invoice.Invoice_FirmName";
            Relation["MemberInvoiceInfo.Invoice_VAT_FirmName"] = "Member_Invoice.Invoice_VAT_FirmName";
            Relation["MemberInvoiceInfo.Invoice_VAT_Code"] = "Member_Invoice.Invoice_VAT_Code";
            Relation["MemberInvoiceInfo.Invoice_VAT_RegAddr"] = "Member_Invoice.Invoice_VAT_RegAddr";
            Relation["MemberInvoiceInfo.Invoice_VAT_RegTel"] = "Member_Invoice.Invoice_VAT_RegTel";
            Relation["MemberInvoiceInfo.Invoice_VAT_Bank"] = "Member_Invoice.Invoice_VAT_Bank";
            Relation["MemberInvoiceInfo.Invoice_VAT_BankAccount"] = "Member_Invoice.Invoice_VAT_BankAccount";
            Relation["MemberInvoiceInfo.Invoice_VAT_Content"] = "Member_Invoice.Invoice_VAT_Content";
            Relation["MemberInvoiceInfo.Invoice_Address"] = "Member_Invoice.Invoice_Address";
            Relation["MemberInvoiceInfo.Invoice_Name"] = "Member_Invoice.Invoice_Name";
            Relation["MemberInvoiceInfo.Invoice_ZipCode"] = "Member_Invoice.Invoice_ZipCode";
            Relation["MemberInvoiceInfo.Invoice_Tel"] = "Member_Invoice.Invoice_Tel";
            Relation["MemberInvoiceInfo.Invoice_PersonelName"] = "Member_Invoice.Invoice_PersonelName";
            Relation["MemberInvoiceInfo.Invoice_PersonelCard"] = "Member_Invoice.Invoice_PersonelCard";
            Relation["MemberInvoiceInfo.Invoice_VAT_Cert"] = "Member_Invoice.Invoice_VAT_Cert";

            //Member_SubAccount
            Relation["MemberSubAccountInfo.ID"] = "Member_SubAccount.ID";
            Relation["MemberSubAccountInfo.MemberID"] = "Member_SubAccount.MemberID";
            Relation["MemberSubAccountInfo.AccountName"] = "Member_SubAccount.AccountName";
            Relation["MemberSubAccountInfo.Password"] = "Member_SubAccount.Password";
            Relation["MemberSubAccountInfo.Name"] = "Member_SubAccount.Name";
            Relation["MemberSubAccountInfo.Mobile"] = "Member_SubAccount.Mobile";
            Relation["MemberSubAccountInfo.Email"] = "Member_SubAccount.Email";
            Relation["MemberSubAccountInfo.Addtime"] = "Member_SubAccount.Addtime";
            Relation["MemberSubAccountInfo.LastLoginTime"] = "Member_SubAccount.LastLoginTime";
            Relation["MemberSubAccountInfo.IsActive"] = "Member_SubAccount.IsActive";
            Relation["MemberSubAccountInfo.Privilege"] = "Member_SubAccount.Privilege";

            //Member_SubAccount_Log
            Relation["MemberSubAccountLogInfo.ID"] = "Member_SubAccount_Log.ID";
            Relation["MemberSubAccountLogInfo.MemberID"] = "Member_SubAccount_Log.MemberID";
            Relation["MemberSubAccountLogInfo.AccountID"] = "Member_SubAccount_Log.AccountID";
            Relation["MemberSubAccountLogInfo.Action"] = "Member_SubAccount_Log.Action";
            Relation["MemberSubAccountLogInfo.Note"] = "Member_SubAccount_Log.Note";
            Relation["MemberSubAccountLogInfo.Addtime"] = "Member_SubAccount_Log.Addtime";

            //Member_Cert
            Relation["MemberCertInfo.Member_Cert_ID"] = "Member_Cert.Member_Cert_ID";
            Relation["MemberCertInfo.Member_Cert_Type"] = "Member_Cert.Member_Cert_Type";
            Relation["MemberCertInfo.Member_Cert_Name"] = "Member_Cert.Member_Cert_Name";
            Relation["MemberCertInfo.Member_Cert_Note"] = "Member_Cert.Member_Cert_Note";
            Relation["MemberCertInfo.Member_Cert_Sort"] = "Member_Cert.Member_Cert_Sort";
            Relation["MemberCertInfo.Member_Cert_Addtime"] = "Member_Cert.Member_Cert_Addtime";
            Relation["MemberCertInfo.Member_Cert_Site"] = "Member_Cert.Member_Cert_Site";

            //Pay_Way
            Relation["PayWayInfo.Pay_Way_ID"] = "Pay_Way.Pay_Way_ID";
            Relation["PayWayInfo.Pay_Way_Type"] = "Pay_Way.Pay_Way_Type";
            Relation["PayWayInfo.Pay_Way_Name"] = "Pay_Way.Pay_Way_Name";
            Relation["PayWayInfo.Pay_Way_Sort"] = "Pay_Way.Pay_Way_Sort";
            Relation["PayWayInfo.Pay_Way_Status"] = "Pay_Way.Pay_Way_Status";
            Relation["PayWayInfo.Pay_Way_Cod"] = "Pay_Way.Pay_Way_Cod";
            Relation["PayWayInfo.Pay_Way_Img"] = "Pay_Way.Pay_Way_Img";
            Relation["PayWayInfo.Pay_Way_Intro"] = "Pay_Way.Pay_Way_Intro";
            Relation["PayWayInfo.Pay_Way_Site"] = "Pay_Way.Pay_Way_Site";

            //Delivery_Way
            Relation["DeliveryWayInfo.Delivery_Way_ID"] = "Delivery_Way.Delivery_Way_ID";
            Relation["DeliveryWayInfo.Delivery_Way_SupplierID"] = "Delivery_Way.Delivery_Way_SupplierID";
            Relation["DeliveryWayInfo.Delivery_Way_Name"] = "Delivery_Way.Delivery_Way_Name";
            Relation["DeliveryWayInfo.Delivery_Way_Sort"] = "Delivery_Way.Delivery_Way_Sort";
            Relation["DeliveryWayInfo.Delivery_Way_InitialWeight"] = "Delivery_Way.Delivery_Way_InitialWeight";
            Relation["DeliveryWayInfo.Delivery_Way_UpWeight"] = "Delivery_Way.Delivery_Way_UpWeight";
            Relation["DeliveryWayInfo.Delivery_Way_FeeType"] = "Delivery_Way.Delivery_Way_FeeType";
            Relation["DeliveryWayInfo.Delivery_Way_Fee"] = "Delivery_Way.Delivery_Way_Fee";
            Relation["DeliveryWayInfo.Delivery_Way_InitialFee"] = "Delivery_Way.Delivery_Way_InitialFee";
            Relation["DeliveryWayInfo.Delivery_Way_UpFee"] = "Delivery_Way.Delivery_Way_UpFee";
            Relation["DeliveryWayInfo.Delivery_Way_Status"] = "Delivery_Way.Delivery_Way_Status";
            Relation["DeliveryWayInfo.Delivery_Way_Cod"] = "Delivery_Way.Delivery_Way_Cod";
            Relation["DeliveryWayInfo.Delivery_Way_Img"] = "Delivery_Way.Delivery_Way_Img";
            Relation["DeliveryWayInfo.Delivery_Way_Intro"] = "Delivery_Way.Delivery_Way_Intro";
            Relation["DeliveryWayInfo.Delivery_Way_Site"] = "Delivery_Way.Delivery_Way_Site";

            //Delivery_Way_District
            Relation["DeliveryWayDistrictInfo.District_ID"] = "Delivery_Way_District.District_ID";
            Relation["DeliveryWayDistrictInfo.District_DeliveryWayID"] = "Delivery_Way_District.District_DeliveryWayID";
            Relation["DeliveryWayDistrictInfo.District_Country"] = "Delivery_Way_District.District_Country";
            Relation["DeliveryWayDistrictInfo.District_State"] = "Delivery_Way_District.District_State";
            Relation["DeliveryWayDistrictInfo.District_City"] = "Delivery_Way_District.District_City";
            Relation["DeliveryWayDistrictInfo.District_County"] = "Delivery_Way_District.District_County";

            //Delivery_Time
            Relation["DeliveryTimeInfo.Delivery_Time_ID"] = "Delivery_Time.Delivery_Time_ID";
            Relation["DeliveryTimeInfo.Delivery_Time_Name"] = "Delivery_Time.Delivery_Time_Name";
            Relation["DeliveryTimeInfo.Delivery_Time_Sort"] = "Delivery_Time.Delivery_Time_Sort";
            Relation["DeliveryTimeInfo.Delivery_Time_IsActive"] = "Delivery_Time.Delivery_Time_IsActive";
            Relation["DeliveryTimeInfo.Delivery_Time_Site"] = "Delivery_Time.Delivery_Time_Site";


            //Orders_Goods_tmp
            Relation["OrdersGoodsTmpInfo.Orders_Goods_ID"] = "Orders_Goods_tmp.Orders_Goods_ID";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Type"] = "Orders_Goods_tmp.Orders_Goods_Type";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_BuyerID"] = "Orders_Goods_tmp.Orders_Goods_BuyerID";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_SessionID"] = "Orders_Goods_tmp.Orders_Goods_SessionID";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_ParentID"] = "Orders_Goods_tmp.Orders_Goods_ParentID";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_ID"] = "Orders_Goods_tmp.Orders_Goods_Product_ID";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_SupplierID"] = "Orders_Goods_tmp.Orders_Goods_Product_SupplierID";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_Code"] = "Orders_Goods_tmp.Orders_Goods_Product_Code";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_CateID"] = "Orders_Goods_tmp.Orders_Goods_Product_CateID";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_BrandID"] = "Orders_Goods_tmp.Orders_Goods_Product_BrandID";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_Name"] = "Orders_Goods_tmp.Orders_Goods_Product_Name";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_Img"] = "Orders_Goods_tmp.Orders_Goods_Product_Img";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_Price"] = "Orders_Goods_tmp.Orders_Goods_Product_Price";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_MKTPrice"] = "Orders_Goods_tmp.Orders_Goods_Product_MKTPrice";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_Maker"] = "Orders_Goods_tmp.Orders_Goods_Product_Maker";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_Spec"] = "Orders_Goods_tmp.Orders_Goods_Product_Spec";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_DeliveryDate"] = "Orders_Goods_tmp.Orders_Goods_Product_DeliveryDate";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_AuthorizeCode"] = "Orders_Goods_tmp.Orders_Goods_Product_AuthorizeCode";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_brokerage"] = "Orders_Goods_tmp.Orders_Goods_Product_brokerage";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_SalePrice"] = "Orders_Goods_tmp.Orders_Goods_Product_SalePrice";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_PurchasingPrice"] = "Orders_Goods_tmp.Orders_Goods_Product_PurchasingPrice";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_Coin"] = "Orders_Goods_tmp.Orders_Goods_Product_Coin";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_IsFavor"] = "Orders_Goods_tmp.Orders_Goods_Product_IsFavor";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Product_UseCoin"] = "Orders_Goods_tmp.Orders_Goods_Product_UseCoin";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Amount"] = "Orders_Goods_tmp.Orders_Goods_Amount";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_Addtime"] = "Orders_Goods_tmp.Orders_Goods_Addtime";
            Relation["OrdersGoodsTmpInfo.Orders_Goods_OrdersID"] = "Orders_Goods_tmp.Orders_Goods_OrdersID";


            //Orders
            Relation["OrdersInfo.Orders_ID"] = "Orders.Orders_ID";
            Relation["OrdersInfo.Orders_SN"] = "Orders.Orders_SN";
            Relation["OrdersInfo.Orders_Type"] = "Orders.Orders_Type";
            Relation["OrdersInfo.Orders_ContractID"] = "Orders.Orders_ContractID";
            Relation["OrdersInfo.Orders_BuyerType"] = "Orders.Orders_BuyerType";
            Relation["OrdersInfo.Orders_BuyerID"] = "Orders.Orders_BuyerID";
            Relation["OrdersInfo.Orders_SysUserID"] = "Orders.Orders_SysUserID";
            Relation["OrdersInfo.Orders_Status"] = "Orders.Orders_Status";
            Relation["OrdersInfo.Orders_ERPSyncStatus"] = "Orders.Orders_ERPSyncStatus";
            Relation["OrdersInfo.Orders_PaymentStatus"] = "Orders.Orders_PaymentStatus";
            Relation["OrdersInfo.Orders_PaymentStatus_Time"] = "Orders.Orders_PaymentStatus_Time";
            Relation["OrdersInfo.Orders_DeliveryStatus"] = "Orders.Orders_DeliveryStatus";
            Relation["OrdersInfo.Orders_DeliveryStatus_Time"] = "Orders.Orders_DeliveryStatus_Time";
            Relation["OrdersInfo.Orders_InvoiceStatus"] = "Orders.Orders_InvoiceStatus";
            Relation["OrdersInfo.Orders_Fail_SysUserID"] = "Orders.Orders_Fail_SysUserID";
            Relation["OrdersInfo.Orders_Fail_Note"] = "Orders.Orders_Fail_Note";
            Relation["OrdersInfo.Orders_Fail_Addtime"] = "Orders.Orders_Fail_Addtime";
            Relation["OrdersInfo.Orders_IsReturnCoin"] = "Orders.Orders_IsReturnCoin";
            Relation["OrdersInfo.Orders_Total_MKTPrice"] = "Orders.Orders_Total_MKTPrice";
            Relation["OrdersInfo.Orders_Total_Price"] = "Orders.Orders_Total_Price";
            Relation["OrdersInfo.Orders_Total_Freight"] = "Orders.Orders_Total_Freight";
            Relation["OrdersInfo.Orders_Total_Coin"] = "Orders.Orders_Total_Coin";
            Relation["OrdersInfo.Orders_Total_UseCoin"] = "Orders.Orders_Total_UseCoin";
            Relation["OrdersInfo.Orders_Total_PriceDiscount"] = "Orders.Orders_Total_PriceDiscount";
            Relation["OrdersInfo.Orders_Total_FreightDiscount"] = "Orders.Orders_Total_FreightDiscount";
            Relation["OrdersInfo.Orders_Total_PriceDiscount_Note"] = "Orders.Orders_Total_PriceDiscount_Note";
            Relation["OrdersInfo.Orders_Total_FreightDiscount_Note"] = "Orders.Orders_Total_FreightDiscount_Note";
            Relation["OrdersInfo.Orders_Total_AllPrice"] = "Orders.Orders_Total_AllPrice";
            Relation["OrdersInfo.Orders_Address_ID"] = "Orders.Orders_Address_ID";
            Relation["OrdersInfo.Orders_Address_Country"] = "Orders.Orders_Address_Country";
            Relation["OrdersInfo.Orders_Address_State"] = "Orders.Orders_Address_State";
            Relation["OrdersInfo.Orders_Address_City"] = "Orders.Orders_Address_City";
            Relation["OrdersInfo.Orders_Address_County"] = "Orders.Orders_Address_County";
            Relation["OrdersInfo.Orders_Address_StreetAddress"] = "Orders.Orders_Address_StreetAddress";
            Relation["OrdersInfo.Orders_Address_Zip"] = "Orders.Orders_Address_Zip";
            Relation["OrdersInfo.Orders_Address_Name"] = "Orders.Orders_Address_Name";
            Relation["OrdersInfo.Orders_Address_Phone_Countrycode"] = "Orders.Orders_Address_Phone_Countrycode";
            Relation["OrdersInfo.Orders_Address_Phone_Areacode"] = "Orders.Orders_Address_Phone_Areacode";
            Relation["OrdersInfo.Orders_Address_Phone_Number"] = "Orders.Orders_Address_Phone_Number";
            Relation["OrdersInfo.Orders_Address_Mobile"] = "Orders.Orders_Address_Mobile";
            Relation["OrdersInfo.Orders_Delivery_Time_ID"] = "Orders.Orders_Delivery_Time_ID";
            Relation["OrdersInfo.Orders_Delivery"] = "Orders.Orders_Delivery";
            Relation["OrdersInfo.Orders_Delivery_Name"] = "Orders.Orders_Delivery_Name";
            Relation["OrdersInfo.Orders_Payway"] = "Orders.Orders_Payway";
            Relation["OrdersInfo.Orders_Payway_Name"] = "Orders.Orders_Payway_Name";
            Relation["OrdersInfo.Orders_PayType"] = "Orders.Orders_PayType";
            Relation["OrdersInfo.Orders_PayType_Name"] = "Orders.Orders_PayType_Name";
            Relation["OrdersInfo.Orders_Note"] = "Orders.Orders_Note";
            Relation["OrdersInfo.Orders_Admin_Note"] = "Orders.Orders_Admin_Note";
            Relation["OrdersInfo.Orders_Admin_Sign"] = "Orders.Orders_Admin_Sign";
            Relation["OrdersInfo.Orders_Site"] = "Orders.Orders_Site";
            Relation["OrdersInfo.Orders_SourceType"] = "Orders.Orders_SourceType";
            Relation["OrdersInfo.Orders_Source"] = "Orders.Orders_Source";
            Relation["OrdersInfo.Orders_VerifyCode"] = "Orders.Orders_VerifyCode";
            Relation["OrdersInfo.U_Orders_IsMonitor"] = "Orders.U_Orders_IsMonitor";
            Relation["OrdersInfo.Orders_Addtime"] = "Orders.Orders_Addtime";
            Relation["OrdersInfo.Orders_From"] = "Orders.Orders_From";
            Relation["OrdersInfo.Orders_Account_Pay"] = "Orders.Orders_Account_Pay";
            Relation["OrdersInfo.Orders_IsEvaluate"] = "Orders.Orders_IsEvaluate";
            Relation["OrdersInfo.Orders_IsSettling"] = "Orders.Orders_IsSettling";
            Relation["OrdersInfo.Orders_SupplierID"] = "Orders.Orders_SupplierID";
            Relation["OrdersInfo.Orders_PurchaseID"] = "Orders.Orders_PurchaseID";
            Relation["OrdersInfo.Orders_PriceReportID"] = "Orders.Orders_PriceReportID";
            Relation["OrdersInfo.Orders_MemberStatus"] = "Orders.Orders_MemberStatus";
            Relation["OrdersInfo.Orders_MemberStatus_Time"] = "Orders.Orders_MemberStatus_Time";
            Relation["OrdersInfo.Orders_SupplierStatus"] = "Orders.Orders_SupplierStatus";
            Relation["OrdersInfo.Orders_SupplierStatus_Time"] = "Orders.Orders_SupplierStatus_Time";
            Relation["OrdersInfo.Orders_ContractAdd"] = "Orders.Orders_ContractAdd";
            Relation["OrdersInfo.Orders_ApplyCreditAmount"] = "Orders.Orders_ApplyCreditAmount";
            Relation["OrdersInfo.Orders_AgreementNo"] = "Orders.Orders_AgreementNo";
            Relation["OrdersInfo.Orders_LoanTermID"] = "Orders.Orders_LoanTermID";
            Relation["OrdersInfo.Orders_LoanMethodID"] = "Orders.Orders_LoanMethodID";
            Relation["OrdersInfo.Orders_Fee"] = "Orders.Orders_Fee";
            Relation["OrdersInfo.Orders_MarginFee"] = "Orders.Orders_MarginFee";
            Relation["OrdersInfo.Orders_FeeRate"] = "Orders.Orders_FeeRate";
            Relation["OrdersInfo.Orders_MarginRate"] = "Orders.Orders_MarginRate";
            Relation["OrdersInfo.Orders_cashier_url"] = "Orders.Orders_cashier_url";
            Relation["OrdersInfo.Orders_Loan_proj_no"] = "Orders.Orders_Loan_proj_no";
            Relation["OrdersInfo.Orders_Responsible"] = "Orders.Orders_Responsible";
            Relation["OrdersInfo.Orders_IsShow"] = "Orders.Orders_IsShow";

            //Orders_Goods
            Relation["OrdersGoodsInfo.Orders_Goods_ID"] = "Orders_Goods.Orders_Goods_ID";
            Relation["OrdersGoodsInfo.Orders_Goods_Type"] = "Orders_Goods.Orders_Goods_Type";
            Relation["OrdersGoodsInfo.Orders_Goods_ParentID"] = "Orders_Goods.Orders_Goods_ParentID";
            Relation["OrdersGoodsInfo.Orders_Goods_OrdersID"] = "Orders_Goods.Orders_Goods_OrdersID";
            Relation["OrdersGoodsInfo.Orders_Goods_Product_ID"] = "Orders_Goods.Orders_Goods_Product_ID";
            Relation["OrdersGoodsInfo.Orders_Goods_Product_SupplierID"] = "Orders_Goods.Orders_Goods_Product_SupplierID";
            Relation["OrdersGoodsInfo.Orders_Goods_Product_Code"] = "Orders_Goods.Orders_Goods_Product_Code";
            Relation["OrdersGoodsInfo.Orders_Goods_Product_CateID"] = "Orders_Goods.Orders_Goods_Product_CateID";
            Relation["OrdersGoodsInfo.Orders_Goods_Product_BrandID"] = "Orders_Goods.Orders_Goods_Product_BrandID";
            Relation["OrdersGoodsInfo.Orders_Goods_Product_Name"] = "Orders_Goods.Orders_Goods_Product_Name";
            Relation["OrdersGoodsInfo.Orders_Goods_Product_Img"] = "Orders_Goods.Orders_Goods_Product_Img";
            Relation["OrdersGoodsInfo.Orders_Goods_Product_Price"] = "Orders_Goods.Orders_Goods_Product_Price";
            Relation["OrdersGoodsInfo.Orders_Goods_Product_MKTPrice"] = "Orders_Goods.Orders_Goods_Product_MKTPrice";
            Relation["OrdersGoodsInfo.Orders_Goods_Product_Maker"] = "Orders_Goods.Orders_Goods_Product_Maker";
            Relation["OrdersGoodsInfo.Orders_Goods_Product_Spec"] = "Orders_Goods.Orders_Goods_Product_Spec";
            Relation["OrdersGoodsInfo.Orders_Goods_Product_AuthorizeCode"] = "Orders_Goods.Orders_Goods_Product_AuthorizeCode";
            Relation["OrdersGoodsInfo.U_Orders_Goods_Product_BatchCode"] = "Orders_Goods.U_Orders_Goods_Product_BatchCode";
            Relation["OrdersGoodsInfo.U_Orders_Goods_Product_BuyChannel"] = "Orders_Goods.U_Orders_Goods_Product_BuyChannel";
            Relation["OrdersGoodsInfo.U_Orders_Goods_Product_BuyAmount"] = "Orders_Goods.U_Orders_Goods_Product_BuyAmount";
            Relation["OrdersGoodsInfo.U_Orders_Goods_Product_BuyPrice"] = "Orders_Goods.U_Orders_Goods_Product_BuyPrice";
            Relation["OrdersGoodsInfo.Orders_Goods_Product_brokerage"] = "Orders_Goods.Orders_Goods_Product_brokerage";
            Relation["OrdersGoodsInfo.Orders_Goods_Product_SalePrice"] = "Orders_Goods.Orders_Goods_Product_SalePrice";
            Relation["OrdersGoodsInfo.Orders_Goods_Product_PurchasingPrice"] = "Orders_Goods.Orders_Goods_Product_PurchasingPrice";
            Relation["OrdersGoodsInfo.Orders_Goods_Product_Coin"] = "Orders_Goods.Orders_Goods_Product_Coin";
            Relation["OrdersGoodsInfo.Orders_Goods_Product_IsFavor"] = "Orders_Goods.Orders_Goods_Product_IsFavor";
            Relation["OrdersGoodsInfo.Orders_Goods_Product_UseCoin"] = "Orders_Goods.Orders_Goods_Product_UseCoin";
            Relation["OrdersGoodsInfo.Orders_Goods_Amount"] = "Orders_Goods.Orders_Goods_Amount";

            //Orders_Delivery
            Relation["OrdersDeliveryInfo.Orders_Delivery_ID"] = "Orders_Delivery.Orders_Delivery_ID";
            Relation["OrdersDeliveryInfo.Orders_Delivery_OrdersID"] = "Orders_Delivery.Orders_Delivery_OrdersID";
            Relation["OrdersDeliveryInfo.Orders_Delivery_DeliveryStatus"] = "Orders_Delivery.Orders_Delivery_DeliveryStatus";
            Relation["OrdersDeliveryInfo.Orders_Delivery_SysUserID"] = "Orders_Delivery.Orders_Delivery_SysUserID";
            Relation["OrdersDeliveryInfo.Orders_Delivery_DocNo"] = "Orders_Delivery.Orders_Delivery_DocNo";
            Relation["OrdersDeliveryInfo.Orders_Delivery_Name"] = "Orders_Delivery.Orders_Delivery_Name";
            Relation["OrdersDeliveryInfo.Orders_Delivery_companyName"] = "Orders_Delivery.Orders_Delivery_companyName";
            Relation["OrdersDeliveryInfo.Orders_Delivery_Code"] = "Orders_Delivery.Orders_Delivery_Code";
            Relation["OrdersDeliveryInfo.Orders_Delivery_Status"] = "Orders_Delivery.Orders_Delivery_Status";
            Relation["OrdersDeliveryInfo.Orders_Delivery_Amount"] = "Orders_Delivery.Orders_Delivery_Amount";
            Relation["OrdersDeliveryInfo.Orders_Delivery_Note"] = "Orders_Delivery.Orders_Delivery_Note";
            Relation["OrdersDeliveryInfo.Orders_Delivery_ReceiveStatus"] = "Orders_Delivery.Orders_Delivery_ReceiveStatus";
            Relation["OrdersDeliveryInfo.Orders_Delivery_Addtime"] = "Orders_Delivery.Orders_Delivery_Addtime";
            Relation["OrdersDeliveryInfo.Orders_Delivery_Site"] = "Orders_Delivery.Orders_Delivery_Site";

            //运输单详情新加字段
            Relation["OrdersDeliveryInfo.Orders_Delivery_DriverMobile"] = "Orders_Delivery.Orders_Delivery_DriverMobile";
            Relation["OrdersDeliveryInfo.Orders_Delivery_PlateNum"] = "Orders_Delivery.Orders_Delivery_PlateNum";
            Relation["OrdersDeliveryInfo.Orders_Delivery_TransportType"] = "Orders_Delivery.Orders_Delivery_TransportType";


            //Orders_Payment
            Relation["OrdersPaymentInfo.Orders_Payment_ID"] = "Orders_Payment.Orders_Payment_ID";
            Relation["OrdersPaymentInfo.Orders_Payment_OrdersID"] = "Orders_Payment.Orders_Payment_OrdersID";
            Relation["OrdersPaymentInfo.Orders_Payment_MemberID"] = "Orders_Payment.Orders_Payment_MemberID";
            Relation["OrdersPaymentInfo.Orders_Payment_PaymentStatus"] = "Orders_Payment.Orders_Payment_PaymentStatus";
            Relation["OrdersPaymentInfo.Orders_Payment_SysUserID"] = "Orders_Payment.Orders_Payment_SysUserID";
            Relation["OrdersPaymentInfo.Orders_Payment_DocNo"] = "Orders_Payment.Orders_Payment_DocNo";
            Relation["OrdersPaymentInfo.Orders_Payment_Name"] = "Orders_Payment.Orders_Payment_Name";
            Relation["OrdersPaymentInfo.Orders_Payment_ApplyAmount"] = "Orders_Payment.Orders_Payment_ApplyAmount";
            Relation["OrdersPaymentInfo.Orders_Payment_Amount"] = "Orders_Payment.Orders_Payment_Amount";
            Relation["OrdersPaymentInfo.Orders_Payment_Status"] = "Orders_Payment.Orders_Payment_Status";
            Relation["OrdersPaymentInfo.Orders_Payment_Note"] = "Orders_Payment.Orders_Payment_Note";
            Relation["OrdersPaymentInfo.Orders_Payment_Addtime"] = "Orders_Payment.Orders_Payment_Addtime";
            Relation["OrdersPaymentInfo.Orders_Payment_Site"] = "Orders_Payment.Orders_Payment_Site";

            //Package
            Relation["PackageInfo.Package_ID"] = "Package.Package_ID";
            Relation["PackageInfo.Package_Name"] = "Package.Package_Name";
            Relation["PackageInfo.Package_IsInsale"] = "Package.Package_IsInsale";
            Relation["PackageInfo.Package_StockAmount"] = "Package.Package_StockAmount";
            Relation["PackageInfo.Package_Weight"] = "Package.Package_Weight";
            Relation["PackageInfo.Package_Price"] = "Package.Package_Price";
            Relation["PackageInfo.Package_Sort"] = "Package.Package_Sort";
            Relation["PackageInfo.Package_Addtime"] = "Package.Package_Addtime";
            Relation["PackageInfo.Package_Site"] = "Package.Package_Site";
            Relation["PackageInfo.Package_SupplierID"] = "Package.Package_SupplierID";

            //Favor_Fee
            //Promotion_Favor_Fee
            Relation["PromotionFavorFeeInfo.Promotion_Fee_ID"] = "Promotion_Favor_Fee.Promotion_Fee_ID";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_Title"] = "Promotion_Favor_Fee.Promotion_Fee_Title";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_Target"] = "Promotion_Favor_Fee.Promotion_Fee_Target";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_Payline"] = "Promotion_Favor_Fee.Promotion_Fee_Payline";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_Manner"] = "Promotion_Favor_Fee.Promotion_Fee_Manner";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_Price"] = "Promotion_Favor_Fee.Promotion_Fee_Price";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_Starttime"] = "Promotion_Favor_Fee.Promotion_Fee_Starttime";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_Endtime"] = "Promotion_Favor_Fee.Promotion_Fee_Endtime";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_Sort"] = "Promotion_Favor_Fee.Promotion_Fee_Sort";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_IsActive"] = "Promotion_Favor_Fee.Promotion_Fee_IsActive";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_IsChecked"] = "Promotion_Favor_Fee.Promotion_Fee_IsChecked";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_Note"] = "Promotion_Favor_Fee.Promotion_Fee_Note";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_Addtime"] = "Promotion_Favor_Fee.Promotion_Fee_Addtime";
            Relation["PromotionFavorFeeInfo.Promotion_Fee_Site"] = "Promotion_Favor_Fee.Promotion_Fee_Site";


            //Promotion
            Relation["PromotionInfo.Promotion_ID"] = "Promotion.Promotion_ID";
            Relation["PromotionInfo.Promotion_Title"] = "Promotion.Promotion_Title";
            Relation["PromotionInfo.Promotion_Type"] = "Promotion.Promotion_Type";
            Relation["PromotionInfo.Promotion_TopHtml"] = "Promotion.Promotion_TopHtml";
            Relation["PromotionInfo.Promotion_Addtime"] = "Promotion.Promotion_Addtime";
            Relation["PromotionInfo.Promotion_Site"] = "Promotion.Promotion_Site";

            //Promotion_Group
            Relation["PromotionGroupInfo.Promotion_Group_ID"] = "Promotion_Group.Promotion_Group_ID";
            Relation["PromotionGroupInfo.Promotion_Group_Title"] = "Promotion_Group.Promotion_Group_Title";
            Relation["PromotionGroupInfo.Promotion_Group_PromotionID"] = "Promotion_Group.Promotion_Group_PromotionID";
            Relation["PromotionGroupInfo.Promotion_Group_Addtime"] = "Promotion_Group.Promotion_Group_Addtime";
            Relation["PromotionGroupInfo.Promotion_Group_Site"] = "Promotion_Group.Promotion_Group_Site";

            //Promotion_Limit_Group
            Relation["PromotionLimitGroupInfo.Promotion_Limit_Group_ID"] = "Promotion_Limit_Group.Promotion_Limit_Group_ID";
            Relation["PromotionLimitGroupInfo.Promotion_Limit_Group_Name"] = "Promotion_Limit_Group.Promotion_Limit_Group_Name";
            Relation["PromotionLimitGroupInfo.Promotion_Limit_Group_Site"] = "Promotion_Limit_Group.Promotion_Limit_Group_Site";

            //Promotion_Limit
            Relation["PromotionLimitInfo.Promotion_Limit_ID"] = "Promotion_Limit.Promotion_Limit_ID";
            Relation["PromotionLimitInfo.Promotion_Limit_GroupID"] = "Promotion_Limit.Promotion_Limit_GroupID";
            Relation["PromotionLimitInfo.Promotion_Limit_ProductID"] = "Promotion_Limit.Promotion_Limit_ProductID";
            Relation["PromotionLimitInfo.Promotion_Limit_Price"] = "Promotion_Limit.Promotion_Limit_Price";
            Relation["PromotionLimitInfo.Promotion_Limit_Amount"] = "Promotion_Limit.Promotion_Limit_Amount";
            Relation["PromotionLimitInfo.Promotion_Limit_Limit"] = "Promotion_Limit.Promotion_Limit_Limit";
            Relation["PromotionLimitInfo.Promotion_Limit_Starttime"] = "Promotion_Limit.Promotion_Limit_Starttime";
            Relation["PromotionLimitInfo.Promotion_Limit_Endtime"] = "Promotion_Limit.Promotion_Limit_Endtime";
            Relation["PromotionLimitInfo.Promotion_Limit_Sort"] = "(select product_sort from product_basic where product_id=Promotion_Limit_ProductID)";
            Relation["PromotionLimitInfo.Promotion_Limit_Site"] = "Promotion_Limit.Promotion_Limit_Site";

            //Promotion_WholeSale_Group
            Relation["PromotionWholeSaleGroupInfo.Promotion_WholeSale_Group_ID"] = "Promotion_WholeSale_Group.Promotion_WholeSale_Group_ID";
            Relation["PromotionWholeSaleGroupInfo.Promotion_WholeSale_Group_Name"] = "Promotion_WholeSale_Group.Promotion_WholeSale_Group_Name";
            Relation["PromotionWholeSaleGroupInfo.Promotion_WholeSale_Group_Site"] = "Promotion_WholeSale_Group.Promotion_WholeSale_Group_Site";

            //Promotion_WholeSale
            Relation["PromotionWholeSaleInfo.Promotion_WholeSale_ID"] = "Promotion_WholeSale.Promotion_WholeSale_ID";
            Relation["PromotionWholeSaleInfo.Promotion_WholeSale_GroupID"] = "Promotion_WholeSale.Promotion_WholeSale_GroupID";
            Relation["PromotionWholeSaleInfo.Promotion_WholeSale_ProductID"] = "Promotion_WholeSale.Promotion_WholeSale_ProductID";
            Relation["PromotionWholeSaleInfo.Promotion_WholeSale_Price"] = "Promotion_WholeSale.Promotion_WholeSale_Price";
            Relation["PromotionWholeSaleInfo.Promotion_WholeSale_MinAmount"] = "Promotion_WholeSale.Promotion_WholeSale_MinAmount";
            Relation["PromotionWholeSaleInfo.Promotion_WholeSale_Site"] = "Promotion_WholeSale.Promotion_WholeSale_Site";

            //Promotion_Favor_Coupon
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_ID"] = "Promotion_Favor_Coupon.Promotion_Coupon_ID";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Title"] = "Promotion_Favor_Coupon.Promotion_Coupon_Title";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Target"] = "Promotion_Favor_Coupon.Promotion_Coupon_Target";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Payline"] = "Promotion_Favor_Coupon.Promotion_Coupon_Payline";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Manner"] = "Promotion_Favor_Coupon.Promotion_Coupon_Manner";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Price"] = "Promotion_Favor_Coupon.Promotion_Coupon_Price";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Percent"] = "Promotion_Favor_Coupon.Promotion_Coupon_Percent";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Amount"] = "Promotion_Favor_Coupon.Promotion_Coupon_Amount";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Starttime"] = "Promotion_Favor_Coupon.Promotion_Coupon_Starttime";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Endtime"] = "Promotion_Favor_Coupon.Promotion_Coupon_Endtime";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Member_ID"] = "Promotion_Favor_Coupon.Promotion_Coupon_Member_ID";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Code"] = "Promotion_Favor_Coupon.Promotion_Coupon_Code";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Verifycode"] = "Promotion_Favor_Coupon.Promotion_Coupon_Verifycode";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Isused"] = "Promotion_Favor_Coupon.Promotion_Coupon_Isused";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_UseAmount"] = "Promotion_Favor_Coupon.Promotion_Coupon_UseAmount";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Display"] = "Promotion_Favor_Coupon.Promotion_Coupon_Display";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_OrdersID"] = "Promotion_Favor_Coupon.Promotion_Coupon_OrdersID";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Note"] = "Promotion_Favor_Coupon.Promotion_Coupon_Note";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Addtime"] = "Promotion_Favor_Coupon.Promotion_Coupon_Addtime";
            Relation["PromotionFavorCouponInfo.Promotion_Coupon_Site"] = "Promotion_Favor_Coupon.Promotion_Coupon_Site";

            //Sources
            Relation["SourcesInfo.Sources_ID"] = "Sources.Sources_ID";
            Relation["SourcesInfo.Sources_Name"] = "Sources.Sources_Name";
            Relation["SourcesInfo.Sources_Code"] = "Sources.Sources_Code";
            Relation["SourcesInfo.Sources_Site"] = "Sources.Sources_Site";

            //Promotion_Coupon_Rule
            Relation["PromotionCouponRuleInfo.Coupon_Rule_ID"] = "Promotion_Coupon_Rule.Coupon_Rule_ID";
            Relation["PromotionCouponRuleInfo.Coupon_Rule_Title"] = "Promotion_Coupon_Rule.Coupon_Rule_Title";
            Relation["PromotionCouponRuleInfo.Coupon_Rule_Target"] = "Promotion_Coupon_Rule.Coupon_Rule_Target";
            Relation["PromotionCouponRuleInfo.Coupon_Rule_Payline"] = "Promotion_Coupon_Rule.Coupon_Rule_Payline";
            Relation["PromotionCouponRuleInfo.Coupon_Rule_Manner"] = "Promotion_Coupon_Rule.Coupon_Rule_Manner";
            Relation["PromotionCouponRuleInfo.Coupon_Rule_Price"] = "Promotion_Coupon_Rule.Coupon_Rule_Price";
            Relation["PromotionCouponRuleInfo.Coupon_Rule_Percent"] = "Promotion_Coupon_Rule.Coupon_Rule_Percent";
            Relation["PromotionCouponRuleInfo.Coupon_Rule_Amount"] = "Promotion_Coupon_Rule.Coupon_Rule_Amount";
            Relation["PromotionCouponRuleInfo.Coupon_Rule_Valid"] = "Promotion_Coupon_Rule.Coupon_Rule_Valid";
            Relation["PromotionCouponRuleInfo.Coupon_Rule_Note"] = "Promotion_Coupon_Rule.Coupon_Rule_Note";
            Relation["PromotionCouponRuleInfo.Coupon_Rule_Site"] = "Promotion_Coupon_Rule.Coupon_Rule_Site";

            //Promotion_Favor_Policy
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_ID"] = "Promotion_Favor_Policy.Promotion_Policy_ID";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Title"] = "Promotion_Favor_Policy.Promotion_Policy_Title";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Target"] = "Promotion_Favor_Policy.Promotion_Policy_Target";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Payline"] = "Promotion_Favor_Policy.Promotion_Policy_Payline";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Manner"] = "Promotion_Favor_Policy.Promotion_Policy_Manner";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_CouponRuleID"] = "Promotion_Favor_Policy.Promotion_Policy_CouponRuleID";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Price"] = "Promotion_Favor_Policy.Promotion_Policy_Price";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Percent"] = "Promotion_Favor_Policy.Promotion_Policy_Percent";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Group"] = "Promotion_Favor_Policy.Promotion_Policy_Group";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Limit"] = "Promotion_Favor_Policy.Promotion_Policy_Limit";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_IsRepeat"] = "Promotion_Favor_Policy.Promotion_Policy_IsRepeat";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Starttime"] = "Promotion_Favor_Policy.Promotion_Policy_Starttime";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Endtime"] = "Promotion_Favor_Policy.Promotion_Policy_Endtime";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Sort"] = "Promotion_Favor_Policy.Promotion_Policy_Sort";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_IsActive"] = "Promotion_Favor_Policy.Promotion_Policy_IsActive";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_IsChecked"] = "Promotion_Favor_Policy.Promotion_Policy_IsChecked";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Note"] = "Promotion_Favor_Policy.Promotion_Policy_Note";
            Relation["PromotionFavorPolicyInfo.Promotion_Policy_Site"] = "Promotion_Favor_Policy.Promotion_Policy_Site";

            //Product_Notify
            Relation["ProductNotifyInfo.Product_Notify_ID"] = "Product_Notify.Product_Notify_ID";
            Relation["ProductNotifyInfo.Product_Notify_MemberID"] = "Product_Notify.Product_Notify_MemberID";
            Relation["ProductNotifyInfo.Product_Notify_Email"] = "Product_Notify.Product_Notify_Email";
            Relation["ProductNotifyInfo.Product_Notify_ProductID"] = "Product_Notify.Product_Notify_ProductID";
            Relation["ProductNotifyInfo.Product_Notify_IsNotify"] = "Product_Notify.Product_Notify_IsNotify";
            Relation["ProductNotifyInfo.Product_Notify_Addtime"] = "Product_Notify.Product_Notify_Addtime";
            Relation["ProductNotifyInfo.Product_Notify_Site"] = "Product_Notify.Product_Notify_Site";


            //Promotion_Favor_Gift
            Relation["PromotionFavorGiftInfo.Promotion_Gift_ID"] = "Promotion_Favor_Gift.Promotion_Gift_ID";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_Title"] = "Promotion_Favor_Gift.Promotion_Gift_Title";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_Target"] = "Promotion_Favor_Gift.Promotion_Gift_Target";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_Group"] = "Promotion_Favor_Gift.Promotion_Gift_Group";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_Limit"] = "Promotion_Favor_Gift.Promotion_Gift_Limit";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_Starttime"] = "Promotion_Favor_Gift.Promotion_Gift_Starttime";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_Endtime"] = "Promotion_Favor_Gift.Promotion_Gift_Endtime";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_Addtime"] = "Promotion_Favor_Gift.Promotion_Gift_Addtime";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_Sort"] = "Promotion_Favor_Gift.Promotion_Gift_Sort";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_IsRepeat"] = "Promotion_Favor_Gift.Promotion_Gift_IsRepeat";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_IsActive"] = "Promotion_Favor_Gift.Promotion_Gift_IsActive";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_IsChecked"] = "Promotion_Favor_Gift.Promotion_Gift_IsChecked";
            Relation["PromotionFavorGiftInfo.Promotion_Gift_Site"] = "Promotion_Favor_Gift.Promotion_Gift_Site";


            //Orders_BackApply
            Relation["OrdersBackApplyInfo.Orders_BackApply_ID"] = "Orders_BackApply.Orders_BackApply_ID";
            Relation["OrdersBackApplyInfo.Orders_BackApply_OrdersCode"] = "Orders_BackApply.Orders_BackApply_OrdersCode";
            Relation["OrdersBackApplyInfo.Orders_BackApply_MemberID"] = "Orders_BackApply.Orders_BackApply_MemberID";
            Relation["OrdersBackApplyInfo.Orders_BackApply_Name"] = "Orders_BackApply.Orders_BackApply_Name";
            Relation["OrdersBackApplyInfo.Orders_BackApply_Type"] = "Orders_BackApply.Orders_BackApply_Type";
            Relation["OrdersBackApplyInfo.Orders_BackApply_DeliveryWay"] = "Orders_BackApply.Orders_BackApply_DeliveryWay";
            Relation["OrdersBackApplyInfo.Orders_BackApply_AmountBackType"] = "Orders_BackApply.Orders_BackApply_AmountBackType";
            Relation["OrdersBackApplyInfo.Orders_BackApply_Amount"] = "Orders_BackApply.Orders_BackApply_Amount";
            Relation["OrdersBackApplyInfo.Orders_BackApply_Note"] = "Orders_BackApply.Orders_BackApply_Note";
            Relation["OrdersBackApplyInfo.Orders_BackApply_Account"] = "Orders_BackApply.Orders_BackApply_Account";
            Relation["OrdersBackApplyInfo.Orders_BackApply_SupplierNote"] = "Orders_BackApply.Orders_BackApply_SupplierNote";
            Relation["OrdersBackApplyInfo.Orders_BackApply_AdminNote"] = "Orders_BackApply.Orders_BackApply_AdminNote";
            Relation["OrdersBackApplyInfo.Orders_BackApply_SupplierTime"] = "Orders_BackApply.Orders_BackApply_SupplierTime";
            Relation["OrdersBackApplyInfo.Orders_BackApply_AdminTime"] = "Orders_BackApply.Orders_BackApply_AdminTime";
            Relation["OrdersBackApplyInfo.Orders_BackApply_Status"] = "Orders_BackApply.Orders_BackApply_Status";
            Relation["OrdersBackApplyInfo.Orders_BackApply_Addtime"] = "Orders_BackApply.Orders_BackApply_Addtime";
            Relation["OrdersBackApplyInfo.Orders_BackApply_Site"] = "Orders_BackApply.Orders_BackApply_Site";

            //Orders_BackApply_Product
            Relation["OrdersBackApplyProductInfo.Orders_BackApply_Product_ID"] = "Orders_BackApply_Product.Orders_BackApply_Product_ID";
            Relation["OrdersBackApplyProductInfo.Orders_BackApply_Product_ProductID"] = "Orders_BackApply_Product.Orders_BackApply_Product_ProductID";
            Relation["OrdersBackApplyProductInfo.Orders_BackApply_Product_ApplyID"] = "Orders_BackApply_Product.Orders_BackApply_Product_ApplyID";
            Relation["OrdersBackApplyProductInfo.Orders_BackApply_Product_ApplyAmount"] = "Orders_BackApply_Product.Orders_BackApply_Product_ApplyAmount";

            //Sys_Menu
            Relation["SysMenuInfo.Sys_Menu_ID"] = "Sys_Menu.Sys_Menu_ID";
            Relation["SysMenuInfo.Sys_Menu_Channel"] = "Sys_Menu.Sys_Menu_Channel";
            Relation["SysMenuInfo.Sys_Menu_Name"] = "Sys_Menu.Sys_Menu_Name";
            Relation["SysMenuInfo.Sys_Menu_ParentID"] = "Sys_Menu.Sys_Menu_ParentID";
            Relation["SysMenuInfo.Sys_Menu_Privilege"] = "Sys_Menu.Sys_Menu_Privilege";
            Relation["SysMenuInfo.Sys_Menu_Icon"] = "Sys_Menu.Sys_Menu_Icon";
            Relation["SysMenuInfo.Sys_Menu_Url"] = "Sys_Menu.Sys_Menu_Url";
            Relation["SysMenuInfo.Sys_Menu_Target"] = "Sys_Menu.Sys_Menu_Target";
            Relation["SysMenuInfo.Sys_Menu_IsSystem"] = "Sys_Menu.Sys_Menu_IsSystem";
            Relation["SysMenuInfo.Sys_Menu_IsDefault"] = "Sys_Menu.Sys_Menu_IsDefault";
            Relation["SysMenuInfo.Sys_Menu_IsCommon"] = "Sys_Menu.Sys_Menu_IsCommon";
            Relation["SysMenuInfo.Sys_Menu_IsActive"] = "Sys_Menu.Sys_Menu_IsActive";
            Relation["SysMenuInfo.Sys_Menu_Sort"] = "Sys_Menu.Sys_Menu_Sort";
            Relation["SysMenuInfo.Sys_Menu_Site"] = "Sys_Menu.Sys_Menu_Site";

            //Supplier
            Relation["SupplierInfo.Supplier_ID"] = "Supplier.Supplier_ID";
            Relation["SupplierInfo.Supplier_Type"] = "Supplier.Supplier_Type";
            Relation["SupplierInfo.Supplier_GradeID"] = "Supplier.Supplier_GradeID";
            Relation["SupplierInfo.Supplier_Nickname"] = "Supplier.Supplier_Nickname";
            Relation["SupplierInfo.Supplier_Email"] = "Supplier.Supplier_Email";
            Relation["SupplierInfo.Supplier_Password"] = "Supplier.Supplier_Password";
            Relation["SupplierInfo.Supplier_CompanyName"] = "Supplier.Supplier_CompanyName";
            Relation["SupplierInfo.Supplier_County"] = "Supplier.Supplier_County";
            Relation["SupplierInfo.Supplier_City"] = "Supplier.Supplier_City";
            Relation["SupplierInfo.Supplier_State"] = "Supplier.Supplier_State";
            Relation["SupplierInfo.Supplier_Country"] = "Supplier.Supplier_Country";
            Relation["SupplierInfo.Supplier_Address"] = "Supplier.Supplier_Address";
            Relation["SupplierInfo.Supplier_Phone"] = "Supplier.Supplier_Phone";
            Relation["SupplierInfo.Supplier_Fax"] = "Supplier.Supplier_Fax";
            Relation["SupplierInfo.Supplier_Zip"] = "Supplier.Supplier_Zip";
            Relation["SupplierInfo.Supplier_Contactman"] = "Supplier.Supplier_Contactman";
            Relation["SupplierInfo.Supplier_Mobile"] = "Supplier.Supplier_Mobile";
            Relation["SupplierInfo.Supplier_IsHaveShop"] = "Supplier.Supplier_IsHaveShop";
            Relation["SupplierInfo.Supplier_IsApply"] = "Supplier.Supplier_IsApply";
            Relation["SupplierInfo.Supplier_ShopType"] = "Supplier.Supplier_ShopType";
            Relation["SupplierInfo.Supplier_Mode"] = "Supplier.Supplier_Mode";
            Relation["SupplierInfo.Supplier_DeliveryMode"] = "Supplier.Supplier_DeliveryMode";
            Relation["SupplierInfo.Supplier_Account"] = "Supplier.Supplier_Account";
            Relation["SupplierInfo.Supplier_Adv_Account"] = "Supplier.Supplier_Adv_Account";
            Relation["SupplierInfo.Supplier_Security_Account"] = "Supplier.Supplier_Security_Account";
            Relation["SupplierInfo.Supplier_CreditLimit"] = "Supplier.Supplier_CreditLimit";
            Relation["SupplierInfo.Supplier_CreditLimitRemain"] = "Supplier.Supplier_CreditLimitRemain";
            Relation["SupplierInfo.Supplier_CreditLimit_Expires"] = "Supplier.Supplier_CreditLimit_Expires";
            Relation["SupplierInfo.Supplier_TempCreditLimit"] = "Supplier.Supplier_TempCreditLimit";
            Relation["SupplierInfo.Supplier_TempCreditLimitRemain"] = "Supplier.Supplier_TempCreditLimitRemain";
            Relation["SupplierInfo.Supplier_TempCreditLimit_ContractSN"] = "Supplier.Supplier_TempCreditLimit_ContractSN";
            Relation["SupplierInfo.Supplier_TempCreditLimit_Expires"] = "Supplier.Supplier_TempCreditLimit_Expires";
            Relation["SupplierInfo.Supplier_CoinCount"] = "Supplier.Supplier_CoinCount";
            Relation["SupplierInfo.Supplier_CoinRemain"] = "Supplier.Supplier_CoinRemain";
            Relation["SupplierInfo.Supplier_Status"] = "Supplier.Supplier_Status";
            Relation["SupplierInfo.Supplier_AuditStatus"] = "Supplier.Supplier_AuditStatus";
            Relation["SupplierInfo.Supplier_Cert_Status"] = "Supplier.Supplier_Cert_Status";
            Relation["SupplierInfo.Supplier_CertType"] = "Supplier.Supplier_CertType";
            Relation["SupplierInfo.Supplier_LoginCount"] = "Supplier.Supplier_LoginCount";
            Relation["SupplierInfo.Supplier_LoginIP"] = "Supplier.Supplier_LoginIP";
            Relation["SupplierInfo.Supplier_Lastlogintime"] = "Supplier.Supplier_Lastlogintime";
            Relation["SupplierInfo.Supplier_VerifyCode"] = "Supplier.Supplier_VerifyCode";
            Relation["SupplierInfo.Supplier_RegIP"] = "Supplier.Supplier_RegIP";
            Relation["SupplierInfo.Supplier_Addtime"] = "Supplier.Supplier_Addtime";
            Relation["SupplierInfo.Supplier_AllowSysMessage"] = "Supplier.Supplier_AllowSysMessage";
            Relation["SupplierInfo.Supplier_AllowSysEmail"] = "Supplier.Supplier_AllowSysEmail";
            Relation["SupplierInfo.Supplier_AllowOrderEmail"] = "Supplier.Supplier_AllowOrderEmail";
            Relation["SupplierInfo.Supplier_SysMobile"] = "Supplier.Supplier_SysMobile";
            Relation["SupplierInfo.Supplier_SysEmail"] = "Supplier.Supplier_SysEmail";
            Relation["SupplierInfo.Supplier_Trash"] = "Supplier.Supplier_Trash";
            Relation["SupplierInfo.Supplier_FavorMonth"] = "Supplier.Supplier_FavorMonth";
            Relation["SupplierInfo.Supplier_AgentRate"] = "Supplier.Supplier_AgentRate";
            Relation["SupplierInfo.Supplier_AllowOrderEmail"] = "Supplier.Supplier_AllowOrderEmail";
            Relation["SupplierInfo.Supplier_Site"] = "Supplier.Supplier_Site";
            Relation["SupplierInfo.Supplier_Emailverify"] = "Supplier.Supplier_Emailverify";
            Relation["SupplierInfo.Supplier_ContractID"] = "Supplier.Supplier_ContractID";
            Relation["SupplierInfo.Supplier_SealImg"] = "Supplier.Supplier_SealImg";
            Relation["SupplierInfo.Supplier_VfinanceID"] = "Supplier.Supplier_VfinanceID";
            Relation["SupplierInfo.Supplier_Corporate"] = "Supplier.Supplier_Corporate";
            Relation["SupplierInfo.Supplier_CorporateMobile"] = "Supplier.Supplier_CorporateMobile";
            Relation["SupplierInfo.Supplier_RegisterFunds"] = "Supplier.Supplier_RegisterFunds";
            Relation["SupplierInfo.Supplier_BusinessCode"] = "Supplier.Supplier_BusinessCode";
            Relation["SupplierInfo.Supplier_OrganizationCode"] = "Supplier.Supplier_OrganizationCode";
            Relation["SupplierInfo.Supplier_TaxationCode"] = "Supplier.Supplier_TaxationCode";
            Relation["SupplierInfo.Supplier_BankAccountCode"] = "Supplier.Supplier_BankAccountCode";
            Relation["SupplierInfo.Supplier_IsAuthorize"] = "Supplier.Supplier_IsAuthorize";
            Relation["SupplierInfo.Supplier_IsTrademark"] = "Supplier.Supplier_IsTrademark";
            Relation["SupplierInfo.Supplier_ServicesPhone"] = "Supplier.Supplier_ServicesPhone";
            Relation["SupplierInfo.Supplier_OperateYear"] = "Supplier.Supplier_OperateYear";
            Relation["SupplierInfo.Supplier_ContactEmail"] = "Supplier.Supplier_ContactEmail";
            Relation["SupplierInfo.Supplier_ContactQQ"] = "Supplier.Supplier_ContactQQ";
            Relation["SupplierInfo.Supplier_Category"] = "Supplier.Supplier_Category";
            Relation["SupplierInfo.Supplier_SaleType"] = "Supplier.Supplier_SaleType";
            Relation["SupplierInfo.Supplier_MerchantMar_Status"] = "Supplier.Supplier_MerchantMar_Status";
            //Relation["SupplierInfo.Supplier_Introduce"] = "Supplier.Supplier_Introduce";


            //Supplier_Account_Log
            Relation["SupplierAccountLogInfo.Account_Log_ID"] = "Supplier_Account_Log.Account_Log_ID";
            Relation["SupplierAccountLogInfo.Account_Log_Type"] = "Supplier_Account_Log.Account_Log_Type";
            Relation["SupplierAccountLogInfo.Account_Log_SupplierID"] = "Supplier_Account_Log.Account_Log_SupplierID";
            Relation["SupplierAccountLogInfo.Account_Log_Amount"] = "Supplier_Account_Log.Account_Log_Amount";
            Relation["SupplierAccountLogInfo.Account_Log_AmountRemain"] = "Supplier_Account_Log.Account_Log_AmountRemain";
            Relation["SupplierAccountLogInfo.Account_Log_Note"] = "Supplier_Account_Log.Account_Log_Note";
            Relation["SupplierAccountLogInfo.Account_Log_Addtime"] = "Supplier_Account_Log.Account_Log_Addtime";

            //Supplier_CreditLimit_Log
            Relation["SupplierCreditLimitLogInfo.CreditLimit_Log_ID"] = "Supplier_CreditLimit_Log.CreditLimit_Log_ID";
            Relation["SupplierCreditLimitLogInfo.CreditLimit_Log_Type"] = "Supplier_CreditLimit_Log.CreditLimit_Log_Type";
            Relation["SupplierCreditLimitLogInfo.CreditLimit_Log_SupplierID"] = "Supplier_CreditLimit_Log.CreditLimit_Log_SupplierID";
            Relation["SupplierCreditLimitLogInfo.CreditLimit_Log_Amount"] = "Supplier_CreditLimit_Log.CreditLimit_Log_Amount";
            Relation["SupplierCreditLimitLogInfo.CreditLimit_Log_AmountRemain"] = "Supplier_CreditLimit_Log.CreditLimit_Log_AmountRemain";
            Relation["SupplierCreditLimitLogInfo.CreditLimit_Log_Note"] = "Supplier_CreditLimit_Log.CreditLimit_Log_Note";
            Relation["SupplierCreditLimitLogInfo.CreditLimit_Log_Addtime"] = "Supplier_CreditLimit_Log.CreditLimit_Log_Addtime";
            Relation["SupplierCreditLimitLogInfo.CreditLimit_Log_PaymentStatus"] = "Supplier_CreditLimit_Log.CreditLimit_Log_PaymentStatus";

            //Supplier_Shop
            Relation["SupplierShopInfo.Shop_ID"] = "Supplier_Shop.Shop_ID";
            Relation["SupplierShopInfo.Shop_Code"] = "Supplier_Shop.Shop_Code";
            Relation["SupplierShopInfo.Shop_Type"] = "Supplier_Shop.Shop_Type";
            Relation["SupplierShopInfo.Shop_Name"] = "Supplier_Shop.Shop_Name";
            Relation["SupplierShopInfo.Shop_SupplierID"] = "Supplier_Shop.Shop_SupplierID";
            Relation["SupplierShopInfo.Shop_Img"] = "Supplier_Shop.Shop_Img";
            Relation["SupplierShopInfo.Shop_Css"] = "Supplier_Shop.Shop_Css";
            Relation["SupplierShopInfo.Shop_Banner"] = "Supplier_Shop.Shop_Banner";
            Relation["SupplierShopInfo.Shop_Banner_Title"] = "Supplier_Shop.Shop_Banner_Title";
            Relation["SupplierShopInfo.Shop_Banner_Title_Family"] = "Supplier_Shop.Shop_Banner_Title_Family";
            Relation["SupplierShopInfo.Shop_Banner_Title_Size"] = "Supplier_Shop.Shop_Banner_Title_Size";
            Relation["SupplierShopInfo.Shop_Banner_Title_LeftPadding"] = "Supplier_Shop.Shop_Banner_Title_LeftPadding";
            Relation["SupplierShopInfo.Shop_banner_Title_color"] = "Supplier_Shop.Shop_banner_Title_color";
            Relation["SupplierShopInfo.Shop_Banner_Img"] = "Supplier_Shop.Shop_Banner_Img";
            Relation["SupplierShopInfo.Shop_Domain"] = "Supplier_Shop.Shop_Domain";
            Relation["SupplierShopInfo.Shop_MainProduct"] = "Supplier_Shop.Shop_MainProduct";
            Relation["SupplierShopInfo.Shop_SEO_Title"] = "Supplier_Shop.Shop_SEO_Title";
            Relation["SupplierShopInfo.Shop_SEO_Keyword"] = "Supplier_Shop.Shop_SEO_Keyword";
            Relation["SupplierShopInfo.Shop_SEO_Description"] = "Supplier_Shop.Shop_SEO_Description";
            Relation["SupplierShopInfo.Shop_Addtime"] = "Supplier_Shop.Shop_Addtime";
            Relation["SupplierShopInfo.Shop_Evaluate"] = "Supplier_Shop.Shop_Evaluate";
            Relation["SupplierShopInfo.Shop_Recommend"] = "Supplier_Shop.Shop_Recommend";
            Relation["SupplierShopInfo.Shop_Status"] = "Supplier_Shop.Shop_Status";
            Relation["SupplierShopInfo.Shop_Hits"] = "Supplier_Shop.Shop_Hits";
            Relation["SupplierShopInfo.Shop_Site"] = "Supplier_Shop.Shop_Site";
            Relation["SupplierShopInfo.Shop_Banner_IsActive"] = "Supplier_Shop.Shop_Banner_IsActive";
            Relation["SupplierShopInfo.Shop_Top_IsActive"] = "Supplier_Shop.Shop_Top_IsActive";
            Relation["SupplierShopInfo.Shop_TopNav_IsActive"] = "Supplier_Shop.Shop_TopNav_IsActive";
            Relation["SupplierShopInfo.Shop_Info_IsActive"] = "Supplier_Shop.Shop_Info_IsActive";
            Relation["SupplierShopInfo.Shop_LeftSearch_IsActive"] = "Supplier_Shop.Shop_LeftSearch_IsActive";
            Relation["SupplierShopInfo.Shop_LeftCate_IsActive"] = "Supplier_Shop.Shop_LeftCate_IsActive";
            Relation["SupplierShopInfo.Shop_LeftSale_IsActive"] = "Supplier_Shop.Shop_LeftSale_IsActive";
            Relation["SupplierShopInfo.Shop_Left_IsActive"] = "Supplier_Shop.Shop_Left_IsActive";
            Relation["SupplierShopInfo.Shop_Right_IsActive"] = "Supplier_Shop.Shop_Right_IsActive";
            Relation["SupplierShopInfo.Shop_RightProduct_IsActive"] = "Supplier_Shop.Shop_RightProduct_IsActive";

            //Supplier_Shop_Domain
            Relation["SupplierShopDomainInfo.Shop_Domain_ID"] = "Supplier_Shop_Domain.Shop_Domain_ID";
            Relation["SupplierShopDomainInfo.Shop_Domain_SupplierID"] = "Supplier_Shop_Domain.Shop_Domain_SupplierID";
            Relation["SupplierShopDomainInfo.Shop_Domain_Type"] = "Supplier_Shop_Domain.Shop_Domain_Type";
            Relation["SupplierShopDomainInfo.Shop_Domain_ShopID"] = "Supplier_Shop_Domain.Shop_Domain_ShopID";
            Relation["SupplierShopDomainInfo.Shop_Domain_Name"] = "Supplier_Shop_Domain.Shop_Domain_Name";
            Relation["SupplierShopDomainInfo.Shop_Domain_Status"] = "Supplier_Shop_Domain.Shop_Domain_Status";
            Relation["SupplierShopDomainInfo.Shop_Domain_Addtime"] = "Supplier_Shop_Domain.Shop_Domain_Addtime";
            Relation["SupplierShopDomainInfo.Shop_Domain_Site"] = "Supplier_Shop_Domain.Shop_Domain_Site";


            //Supplier_Shop_Apply
            Relation["SupplierShopApplyInfo.Shop_Apply_ID"] = "Supplier_Shop_Apply.Shop_Apply_ID";
            Relation["SupplierShopApplyInfo.Shop_Apply_SupplierID"] = "Supplier_Shop_Apply.Shop_Apply_SupplierID";
            Relation["SupplierShopApplyInfo.Shop_Apply_ShopType"] = "Supplier_Shop_Apply.Shop_Apply_ShopType";
            Relation["SupplierShopApplyInfo.Shop_Apply_Name"] = "Supplier_Shop_Apply.Shop_Apply_Name";
            Relation["SupplierShopApplyInfo.Shop_Apply_PINCode"] = "Supplier_Shop_Apply.Shop_Apply_PINCode";
            Relation["SupplierShopApplyInfo.Shop_Apply_Mobile"] = "Supplier_Shop_Apply.Shop_Apply_Mobile";
            Relation["SupplierShopApplyInfo.Shop_Apply_ShopName"] = "Supplier_Shop_Apply.Shop_Apply_ShopName";
            Relation["SupplierShopApplyInfo.Shop_Apply_CompanyType"] = "Supplier_Shop_Apply.Shop_Apply_CompanyType";
            Relation["SupplierShopApplyInfo.Shop_Apply_Lawman"] = "Supplier_Shop_Apply.Shop_Apply_Lawman";
            Relation["SupplierShopApplyInfo.Shop_Apply_CertCode"] = "Supplier_Shop_Apply.Shop_Apply_CertCode";
            Relation["SupplierShopApplyInfo.Shop_Apply_CertAddress"] = "Supplier_Shop_Apply.Shop_Apply_CertAddress";
            Relation["SupplierShopApplyInfo.Shop_Apply_CompanyAddress"] = "Supplier_Shop_Apply.Shop_Apply_CompanyAddress";
            Relation["SupplierShopApplyInfo.Shop_Apply_CompanyPhone"] = "Supplier_Shop_Apply.Shop_Apply_CompanyPhone";
            Relation["SupplierShopApplyInfo.Shop_Apply_Certification1"] = "Supplier_Shop_Apply.Shop_Apply_Certification1";
            Relation["SupplierShopApplyInfo.Shop_Apply_Certification2"] = "Supplier_Shop_Apply.Shop_Apply_Certification2";
            Relation["SupplierShopApplyInfo.Shop_Apply_Certification3"] = "Supplier_Shop_Apply.Shop_Apply_Certification3";
            Relation["SupplierShopApplyInfo.Shop_Apply_Certification4"] = "Supplier_Shop_Apply.Shop_Apply_Certification4";
            Relation["SupplierShopApplyInfo.Shop_Apply_Certification5"] = "Supplier_Shop_Apply.Shop_Apply_Certification5";
            Relation["SupplierShopApplyInfo.Shop_Apply_MainBrand"] = "Supplier_Shop_Apply.Shop_Apply_MainBrand";
            Relation["SupplierShopApplyInfo.Shop_Apply_Status"] = "Supplier_Shop_Apply.Shop_Apply_Status";
            Relation["SupplierShopApplyInfo.Shop_Apply_Note"] = "Supplier_Shop_Apply.Shop_Apply_Note";
            Relation["SupplierShopApplyInfo.Shop_Apply_Addtime"] = "Supplier_Shop_Apply.Shop_Apply_Addtime";

            //Supplier_CloseShop_Apply
            Relation["SupplierCloseShopApplyInfo.CloseShop_Apply_ID"] = "Supplier_CloseShop_Apply.CloseShop_Apply_ID";
            Relation["SupplierCloseShopApplyInfo.CloseShop_Apply_SupplierID"] = "Supplier_CloseShop_Apply.CloseShop_Apply_SupplierID";
            Relation["SupplierCloseShopApplyInfo.CloseShop_Apply_Note"] = "Supplier_CloseShop_Apply.CloseShop_Apply_Note";
            Relation["SupplierCloseShopApplyInfo.CloseShop_Apply_Status"] = "Supplier_CloseShop_Apply.CloseShop_Apply_Status";
            Relation["SupplierCloseShopApplyInfo.CloseShop_Apply_AdminNote"] = "Supplier_CloseShop_Apply.CloseShop_Apply_AdminNote";
            Relation["SupplierCloseShopApplyInfo.CloseShop_Apply_Addtime"] = "Supplier_CloseShop_Apply.CloseShop_Apply_Addtime";
            Relation["SupplierCloseShopApplyInfo.CloseShop_Apply_AdminTime"] = "Supplier_CloseShop_Apply.CloseShop_Apply_AdminTime";
            Relation["SupplierCloseShopApplyInfo.CloseShop_Apply_Site"] = "Supplier_CloseShop_Apply.CloseShop_Apply_Site";

            //Supplier_Cert_Type
            Relation["SupplierCertTypeInfo.Cert_Type_ID"] = "Supplier_Cert_Type.Cert_Type_ID";
            Relation["SupplierCertTypeInfo.Cert_Type_Name"] = "Supplier_Cert_Type.Cert_Type_Name";
            Relation["SupplierCertTypeInfo.Cert_Type_Sort"] = "Supplier_Cert_Type.Cert_Type_Sort";
            Relation["SupplierCertTypeInfo.Cert_Type_IsActive"] = "Supplier_Cert_Type.Cert_Type_IsActive";
            Relation["SupplierCertTypeInfo.Cert_Type_Site"] = "Supplier_Cert_Type.Cert_Type_Site";

            //Supplier_Cert
            Relation["SupplierCertInfo.Supplier_Cert_ID"] = "Supplier_Cert.Supplier_Cert_ID";
            Relation["SupplierCertInfo.Supplier_Cert_Type"] = "Supplier_Cert.Supplier_Cert_Type";
            Relation["SupplierCertInfo.Supplier_Cert_Name"] = "Supplier_Cert.Supplier_Cert_Name";
            Relation["SupplierCertInfo.Supplier_Cert_Note"] = "Supplier_Cert.Supplier_Cert_Note";
            Relation["SupplierCertInfo.Supplier_Cert_Addtime"] = "Supplier_Cert.Supplier_Cert_Addtime";
            Relation["SupplierCertInfo.Supplier_Cert_Sort"] = "Supplier_Cert.Supplier_Cert_Sort";
            Relation["SupplierCertInfo.Supplier_Cert_Site"] = "Supplier_Cert.Supplier_Cert_Site";

            //KeywordBidding
            Relation["KeywordBiddingInfo.KeywordBidding_ID"] = "KeywordBidding.KeywordBidding_ID";
            Relation["KeywordBiddingInfo.KeywordBidding_SupplierID"] = "KeywordBidding.KeywordBidding_SupplierID";
            Relation["KeywordBiddingInfo.KeywordBidding_ProductID"] = "KeywordBidding.KeywordBidding_ProductID";
            Relation["KeywordBiddingInfo.KeywordBidding_KeywordID"] = "KeywordBidding.KeywordBidding_KeywordID";
            Relation["KeywordBiddingInfo.KeywordBidding_Price"] = "KeywordBidding.KeywordBidding_Price";
            Relation["KeywordBiddingInfo.KeywordBidding_StartDate"] = "KeywordBidding.KeywordBidding_StartDate";
            Relation["KeywordBiddingInfo.KeywordBidding_EndDate"] = "KeywordBidding.KeywordBidding_EndDate";
            Relation["KeywordBiddingInfo.KeywordBidding_ShowTimes"] = "KeywordBidding.KeywordBidding_ShowTimes";
            Relation["KeywordBiddingInfo.KeywordBidding_Hits"] = "KeywordBidding.KeywordBidding_Hits";
            Relation["KeywordBiddingInfo.KeywordBidding_Audit"] = "KeywordBidding.KeywordBidding_Audit";
            Relation["KeywordBiddingInfo.KeywordBidding_Site"] = "KeywordBidding.KeywordBidding_Site";

            //KeywordBidding_Keyword
            Relation["KeywordBiddingKeywordInfo.Keyword_ID"] = "KeywordBidding_Keyword.Keyword_ID";
            Relation["KeywordBiddingKeywordInfo.Keyword_Name"] = "KeywordBidding_Keyword.Keyword_Name";
            Relation["KeywordBiddingKeywordInfo.Keyword_MinPrice"] = "KeywordBidding_Keyword.Keyword_MinPrice";

            //Supplier_PayBack_Apply
            Relation["SupplierPayBackApplyInfo.Supplier_PayBack_Apply_ID"] = "Supplier_PayBack_Apply.Supplier_PayBack_Apply_ID";
            Relation["SupplierPayBackApplyInfo.Supplier_PayBack_Apply_SupplierID"] = "Supplier_PayBack_Apply.Supplier_PayBack_Apply_SupplierID";
            Relation["SupplierPayBackApplyInfo.Supplier_PayBack_Apply_Type"] = "Supplier_PayBack_Apply.Supplier_PayBack_Apply_Type";
            Relation["SupplierPayBackApplyInfo.Supplier_PayBack_Apply_Amount"] = "Supplier_PayBack_Apply.Supplier_PayBack_Apply_Amount";
            Relation["SupplierPayBackApplyInfo.Supplier_PayBack_Apply_Note"] = "Supplier_PayBack_Apply.Supplier_PayBack_Apply_Note";
            Relation["SupplierPayBackApplyInfo.Supplier_PayBack_Apply_Addtime"] = "Supplier_PayBack_Apply.Supplier_PayBack_Apply_Addtime";
            Relation["SupplierPayBackApplyInfo.Supplier_PayBack_Apply_Status"] = "Supplier_PayBack_Apply.Supplier_PayBack_Apply_Status";
            Relation["SupplierPayBackApplyInfo.Supplier_PayBack_Apply_AdminAmount"] = "Supplier_PayBack_Apply.Supplier_PayBack_Apply_AdminAmount";
            Relation["SupplierPayBackApplyInfo.Supplier_PayBack_Apply_AdminNote"] = "Supplier_PayBack_Apply.Supplier_PayBack_Apply_AdminNote";
            Relation["SupplierPayBackApplyInfo.Supplier_PayBack_Apply_AdminTime"] = "Supplier_PayBack_Apply.Supplier_PayBack_Apply_AdminTime";
            Relation["SupplierPayBackApplyInfo.Supplier_PayBack_Apply_Site"] = "Supplier_PayBack_Apply.Supplier_PayBack_Apply_Site";

            //Supplier_Shop_Banner
            Relation["SupplierShopBannerInfo.Shop_Banner_ID"] = "Supplier_Shop_Banner.Shop_Banner_ID";
            Relation["SupplierShopBannerInfo.Shop_Banner_Type"] = "Supplier_Shop_Banner.Shop_Banner_Type";
            Relation["SupplierShopBannerInfo.Shop_Banner_Name"] = "Supplier_Shop_Banner.Shop_Banner_Name";
            Relation["SupplierShopBannerInfo.Shop_Banner_Url"] = "Supplier_Shop_Banner.Shop_Banner_Url";
            Relation["SupplierShopBannerInfo.Shop_Banner_IsActive"] = "Supplier_Shop_Banner.Shop_Banner_IsActive";
            Relation["SupplierShopBannerInfo.Shop_Banner_Site"] = "Supplier_Shop_Banner.Shop_Banner_Site";

            //Supplier_Shop_Css
            Relation["SupplierShopCssInfo.Shop_Css_ID"] = "Supplier_Shop_Css.Shop_Css_ID";
            Relation["SupplierShopCssInfo.Shop_Css_Title"] = "Supplier_Shop_Css.Shop_Css_Title";
            Relation["SupplierShopCssInfo.Shop_Css_Target"] = "Supplier_Shop_Css.Shop_Css_Target";
            Relation["SupplierShopCssInfo.Shop_Css_GapColor"] = "Supplier_Shop_Css.Shop_Css_GapColor";
            Relation["SupplierShopCssInfo.Shop_Css_Img"] = "Supplier_Shop_Css.Shop_Css_Img";
            Relation["SupplierShopCssInfo.Shop_Css_IsActive"] = "Supplier_Shop_Css.Shop_Css_IsActive";
            Relation["SupplierShopCssInfo.Shop_Css_Site"] = "Supplier_Shop_Css.Shop_Css_Site";

            //Supplier_Shop_Pages
            Relation["SupplierShopPagesInfo.Shop_Pages_ID"] = "Supplier_Shop_Pages.Shop_Pages_ID";
            Relation["SupplierShopPagesInfo.Shop_Pages_Title"] = "Supplier_Shop_Pages.Shop_Pages_Title";
            Relation["SupplierShopPagesInfo.Shop_Pages_SupplierID"] = "Supplier_Shop_Pages.Shop_Pages_SupplierID";
            Relation["SupplierShopPagesInfo.Shop_Pages_Sign"] = "Supplier_Shop_Pages.Shop_Pages_Sign";
            Relation["SupplierShopPagesInfo.Shop_Pages_Content"] = "Supplier_Shop_Pages.Shop_Pages_Content";
            Relation["SupplierShopPagesInfo.Shop_Pages_Ischeck"] = "Supplier_Shop_Pages.Shop_Pages_Ischeck";
            Relation["SupplierShopPagesInfo.Shop_Pages_IsActive"] = "Supplier_Shop_Pages.Shop_Pages_IsActive";
            Relation["SupplierShopPagesInfo.Shop_Pages_Sort"] = "Supplier_Shop_Pages.Shop_Pages_Sort";
            Relation["SupplierShopPagesInfo.Shop_Pages_Addtime"] = "Supplier_Shop_Pages.Shop_Pages_Addtime";
            Relation["SupplierShopPagesInfo.Shop_Pages_Site"] = "Supplier_Shop_Pages.Shop_Pages_Site";

            //Supplier_Shop_Article
            Relation["SupplierShopArticleInfo.Shop_Article_ID"] = "Supplier_Shop_Article.Shop_Article_ID";
            Relation["SupplierShopArticleInfo.Shop_Article_SupplierID"] = "Supplier_Shop_Article.Shop_Article_SupplierID";
            Relation["SupplierShopArticleInfo.Shop_Article_Title"] = "Supplier_Shop_Article.Shop_Article_Title";
            Relation["SupplierShopArticleInfo.Shop_Article_Content"] = "Supplier_Shop_Article.Shop_Article_Content";
            Relation["SupplierShopArticleInfo.Shop_Article_Hits"] = "Supplier_Shop_Article.Shop_Article_Hits";
            Relation["SupplierShopArticleInfo.Shop_Article_Addtime"] = "Supplier_Shop_Article.Shop_Article_Addtime";
            Relation["SupplierShopArticleInfo.Shop_Article_IsActive"] = "Supplier_Shop_Article.Shop_Article_IsActive";
            Relation["SupplierShopArticleInfo.Shop_Article_Site"] = "Supplier_Shop_Article.Shop_Article_Site";

            //Supplier_Shop_Evaluate
            Relation["SupplierShopEvaluateInfo.Shop_Evaluate_ID"] = "Supplier_Shop_Evaluate.Shop_Evaluate_ID";
            Relation["SupplierShopEvaluateInfo.Shop_Evaluate_SupplierID"] = "Supplier_Shop_Evaluate.Shop_Evaluate_SupplierID";
            Relation["SupplierShopEvaluateInfo.Shop_Evaluate_ContractID"] = "Supplier_Shop_Evaluate.Shop_Evaluate_ContractID";
            Relation["SupplierShopEvaluateInfo.Shop_Evaluate_Productid"] = "Supplier_Shop_Evaluate.Shop_Evaluate_Productid";
            Relation["SupplierShopEvaluateInfo.Shop_Evaluate_MemberId"] = "Supplier_Shop_Evaluate.Shop_Evaluate_MemberId";
            Relation["SupplierShopEvaluateInfo.Shop_Evaluate_Product"] = "Supplier_Shop_Evaluate.Shop_Evaluate_Product";
            Relation["SupplierShopEvaluateInfo.Shop_Evaluate_Service"] = "Supplier_Shop_Evaluate.Shop_Evaluate_Service";
            Relation["SupplierShopEvaluateInfo.Shop_Evaluate_Delivery"] = "Supplier_Shop_Evaluate.Shop_Evaluate_Delivery";
            Relation["SupplierShopEvaluateInfo.Shop_Evaluate_Note"] = "Supplier_Shop_Evaluate.Shop_Evaluate_Note";
            Relation["SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck"] = "Supplier_Shop_Evaluate.Shop_Evaluate_Ischeck";
            Relation["SupplierShopEvaluateInfo.Shop_Evaluate_Recommend"] = "Supplier_Shop_Evaluate.Shop_Evaluate_Recommend";
            Relation["SupplierShopEvaluateInfo.Shop_Evaluate_Ischeck"] = "Supplier_Shop_Evaluate.Shop_Evaluate_Ischeck";
            Relation["SupplierShopEvaluateInfo.Shop_Evaluate_Addtime"] = "Supplier_Shop_Evaluate.Shop_Evaluate_Addtime";
            Relation["SupplierShopEvaluateInfo.Shop_Evaluate_Site"] = "Supplier_Shop_Evaluate.Shop_Evaluate_Site";

            //Supplier_Category
            Relation["SupplierCategoryInfo.Supplier_Cate_ID"] = "Supplier_Category.Supplier_Cate_ID";
            Relation["SupplierCategoryInfo.Supplier_Cate_Name"] = "Supplier_Category.Supplier_Cate_Name";
            Relation["SupplierCategoryInfo.Supplier_Cate_Parentid"] = "Supplier_Category.Supplier_Cate_Parentid";
            Relation["SupplierCategoryInfo.Supplier_Cate_SupplierID"] = "Supplier_Category.Supplier_Cate_SupplierID";
            Relation["SupplierCategoryInfo.Supplier_Cate_Sort"] = "Supplier_Category.Supplier_Cate_Sort";
            Relation["SupplierCategoryInfo.Supplier_Cate_Site"] = "Supplier_Category.Supplier_Cate_Site";

            //Supplier_Commission_Category
            Relation["SupplierCommissionCategoryInfo.Supplier_Commission_Cate_ID"] = "Supplier_Commission_Category.Supplier_Commission_Cate_ID";
            Relation["SupplierCommissionCategoryInfo.Supplier_Commission_Cate_SupplierID"] = "Supplier_Commission_Category.Supplier_Commission_Cate_SupplierID";
            Relation["SupplierCommissionCategoryInfo.Supplier_Commission_Cate_Name"] = "Supplier_Commission_Category.Supplier_Commission_Cate_Name";
            Relation["SupplierCommissionCategoryInfo.Supplier_Commission_Cate_Amount"] = "Supplier_Commission_Category.Supplier_Commission_Cate_Amount";
            Relation["SupplierCommissionCategoryInfo.Supplier_Commission_Cate_Site"] = "Supplier_Commission_Category.Supplier_Commission_Cate_Site";

            //Supplier_Message
            Relation["SupplierMessageInfo.Supplier_Message_ID"] = "Supplier_Message.Supplier_Message_ID";
            Relation["SupplierMessageInfo.Supplier_Message_SupplierID"] = "Supplier_Message.Supplier_Message_SupplierID";
            Relation["SupplierMessageInfo.Supplier_Message_Title"] = "Supplier_Message.Supplier_Message_Title";
            Relation["SupplierMessageInfo.Supplier_Message_Content"] = "Supplier_Message.Supplier_Message_Content";
            Relation["SupplierMessageInfo.Supplier_Message_Addtime"] = "Supplier_Message.Supplier_Message_Addtime";
            Relation["SupplierMessageInfo.Supplier_Message_IsRead"] = "Supplier_Message.Supplier_Message_IsRead";
            Relation["SupplierMessageInfo.Supplier_Message_Site"] = "Supplier_Message.Supplier_Message_Site";
          

            //Supplier_Tag
            Relation["SupplierTagInfo.Supplier_Tag_ID"] = "Supplier_Tag.Supplier_Tag_ID";
            Relation["SupplierTagInfo.Supplier_Tag_Name"] = "Supplier_Tag.Supplier_Tag_Name";
            Relation["SupplierTagInfo.Supplier_Tag_Img"] = "Supplier_Tag.Supplier_Tag_Img";
            Relation["SupplierTagInfo.Supplier_Tag_Content"] = "Supplier_Tag.Supplier_Tag_Content";
            Relation["SupplierTagInfo.Supplier_Tag_Site"] = "Supplier_Tag.Supplier_Tag_Site";

            //Member_Account_Log
            Relation["MemberAccountLogInfo.Account_Log_ID"] = "Member_Account_Log.Account_Log_ID";
            Relation["MemberAccountLogInfo.Account_Log_MemberID"] = "Member_Account_Log.Account_Log_MemberID";
            Relation["MemberAccountLogInfo.Account_Log_Amount"] = "Member_Account_Log.Account_Log_Amount";
            Relation["MemberAccountLogInfo.Account_Log_Remain"] = "Member_Account_Log.Account_Log_Remain";
            Relation["MemberAccountLogInfo.Account_Log_Note"] = "Member_Account_Log.Account_Log_Note";
            Relation["MemberAccountLogInfo.Account_Log_Addtime"] = "Member_Account_Log.Account_Log_Addtime";
            Relation["MemberAccountLogInfo.Account_Log_Site"] = "Member_Account_Log.Account_Log_Site";

            //Supplier_Shop_Grade
            Relation["SupplierShopGradeInfo.Shop_Grade_ID"] = "Supplier_Shop_Grade.Shop_Grade_ID";
            Relation["SupplierShopGradeInfo.Shop_Grade_Name"] = "Supplier_Shop_Grade.Shop_Grade_Name";
            Relation["SupplierShopGradeInfo.Shop_Grade_ProductLimit"] = "Supplier_Shop_Grade.Shop_Grade_ProductLimit";
            Relation["SupplierShopGradeInfo.Shop_Grade_DefaultCommission"] = "Supplier_Shop_Grade.Shop_Grade_DefaultCommission";
            Relation["SupplierShopGradeInfo.Shop_Grade_IsActive"] = "Supplier_Shop_Grade.Shop_Grade_IsActive";
            Relation["SupplierShopGradeInfo.Shop_Grade_Site"] = "Supplier_Shop_Grade.Shop_Grade_Site";

            //Article
            Relation["ArticleInfo.Article_ID"] = "Article.Article_ID";
            Relation["ArticleInfo.Article_CateID"] = "Article.Article_CateID";
            Relation["ArticleInfo.Article_Title"] = "Article.Article_Title";
            Relation["ArticleInfo.Article_Source"] = "Article.Article_Source";
            Relation["ArticleInfo.Article_Author"] = "Article.Article_Author";
            Relation["ArticleInfo.Article_Img"] = "Article.Article_Img";
            Relation["ArticleInfo.Article_Keyword"] = "Article.Article_Keyword";
            Relation["ArticleInfo.Article_Intro"] = "Article.Article_Intro";
            Relation["ArticleInfo.Article_Content"] = "Article.Article_Content";
            Relation["ArticleInfo.Article_Addtime"] = "Article.Article_Addtime";
            Relation["ArticleInfo.Article_Hits"] = "Article.Article_Hits";
            Relation["ArticleInfo.Article_IsRecommend"] = "Article.Article_IsRecommend";
            Relation["ArticleInfo.Article_IsAudit"] = "Article.Article_IsAudit";
            Relation["ArticleInfo.Article_Sort"] = "Article.Article_Sort";
            Relation["ArticleInfo.Article_Site"] = "Article.Article_Site";

            //Article_Cate
            Relation["ArticleCateInfo.Article_Cate_ID"] = "Article_Cate.Article_Cate_ID";
            Relation["ArticleCateInfo.Article_Cate_ParentID"] = "Article_Cate.Article_Cate_ParentID";
            Relation["ArticleCateInfo.Article_Cate_Name"] = "Article_Cate.Article_Cate_Name";
            Relation["ArticleCateInfo.Article_Cate_Sort"] = "Article_Cate.Article_Cate_Sort";
            Relation["ArticleCateInfo.Article_Cate_Site"] = "Article_Cate.Article_Cate_Site";

            //Article_Label
            Relation["Article_LabelInfo.Article_Label_ID"] = "Article_Label.Article_Label_ID";
            Relation["Article_LabelInfo.Article_Label_ArticleID"] = "Article_Label.Article_Label_ArticleID";
            Relation["Article_LabelInfo.Article_Label_LabelID"] = "Article_Label.Article_Label_LabelID";

            //Product_Label
            Relation["Product_LabelInfo.Product_Label_ID"] = "Product_Label.Product_Label_ID";
            Relation["Product_LabelInfo.Product_Label_ProductID"] = "Product_Label.Product_Label_ProductID";
            Relation["Product_LabelInfo.Product_Label_LabelID"] = "Product_Label.Product_Label_LabelID";

            //Product_Article_Label
            Relation["Product_Article_LabelInfo.Product_Article_LabelID"] = "Product_Article_Label.Product_Article_LabelID";
            Relation["Product_Article_LabelInfo.Product_Article_LabelName"] = "Product_Article_Label.Product_Article_LabelName";

            //Supplier_Bank
            Relation["SupplierBankInfo.Supplier_Bank_ID"] = "Supplier_Bank.Supplier_Bank_ID";
            Relation["SupplierBankInfo.Supplier_Bank_Name"] = "Supplier_Bank.Supplier_Bank_Name";
            Relation["SupplierBankInfo.Supplier_Bank_NetWork"] = "Supplier_Bank.Supplier_Bank_NetWork";
            Relation["SupplierBankInfo.Supplier_Bank_SName"] = "Supplier_Bank.Supplier_Bank_SName";
            Relation["SupplierBankInfo.Supplier_Bank_Account"] = "Supplier_Bank.Supplier_Bank_Account";
            Relation["SupplierBankInfo.Supplier_Bank_SupplierID"] = "Supplier_Bank.Supplier_Bank_SupplierID";

            //Supplier_Online
            Relation["SupplierOnlineInfo.Supplier_Online_ID"] = "Supplier_Online.Supplier_Online_ID";
            Relation["SupplierOnlineInfo.Supplier_Online_SupplierID"] = "Supplier_Online.Supplier_Online_SupplierID";
            Relation["SupplierOnlineInfo.Supplier_Online_Type"] = "Supplier_Online.Supplier_Online_Type";
            Relation["SupplierOnlineInfo.Supplier_Online_Name"] = "Supplier_Online.Supplier_Online_Name";
            Relation["SupplierOnlineInfo.Supplier_Online_Code"] = "Supplier_Online.Supplier_Online_Code";
            Relation["SupplierOnlineInfo.Supplier_Online_Sort"] = "Supplier_Online.Supplier_Online_Sort";
            Relation["SupplierOnlineInfo.Supplier_Online_IsActive"] = "Supplier_Online.Supplier_Online_IsActive";
            Relation["SupplierOnlineInfo.Supplier_Online_Addtime"] = "Supplier_Online.Supplier_Online_Addtime";
            Relation["SupplierOnlineInfo.Supplier_Online_Site"] = "Supplier_Online.Supplier_Online_Site";


            //Home_Left_Cate
            Relation["HomeLeftCateInfo.Home_Left_Cate_ID"] = "Home_Left_Cate.Home_Left_Cate_ID";
            Relation["HomeLeftCateInfo.Home_Left_Cate_ParentID"] = "Home_Left_Cate.Home_Left_Cate_ParentID";
            Relation["HomeLeftCateInfo.Home_Left_Cate_CateID"] = "Home_Left_Cate.Home_Left_Cate_CateID";
            Relation["HomeLeftCateInfo.Home_Left_Cate_Name"] = "Home_Left_Cate.Home_Left_Cate_Name";
            Relation["HomeLeftCateInfo.Home_Left_Cate_URL"] = "Home_Left_Cate.Home_Left_Cate_URL";
            Relation["HomeLeftCateInfo.Home_Left_Cate_Img"] = "Home_Left_Cate.Home_Left_Cate_Img";
            Relation["HomeLeftCateInfo.Home_Left_Cate_Sort"] = "Home_Left_Cate.Home_Left_Cate_Sort";
            Relation["HomeLeftCateInfo.Home_Left_Cate_Active"] = "Home_Left_Cate.Home_Left_Cate_Active";
            Relation["HomeLeftCateInfo.Home_Left_Cate_Site"] = "Home_Left_Cate.Home_Left_Cate_Site";

            //Sys_IP
            Relation["SysIPInfo.onip"] = "Sys_IP.onip";
            Relation["SysIPInfo.oniptxt"] = "Sys_IP.oniptxt";
            Relation["SysIPInfo.offip"] = "Sys_IP.offip";
            Relation["SysIPInfo.offiptxt"] = "Sys_IP.offiptxt";
            Relation["SysIPInfo.ProvinceID"] = "Sys_IP.ProvinceID";
            Relation["SysIPInfo.CityID"] = "Sys_IP.CityID";
            Relation["SysIPInfo.CountyID"] = "Sys_IP.CountyID";
            Relation["SysIPInfo.country"] = "Sys_IP.country";
            Relation["SysIPInfo.city"] = "Sys_IP.city";

            //U_Supplier_Favorites
            Relation["SupplierFavoritesInfo.Supplier_Favorites_ID"] = "U_Supplier_Favorites.Supplier_Favorites_ID";
            Relation["SupplierFavoritesInfo.Supplier_Favorites_SupplierID"] = "U_Supplier_Favorites.Supplier_Favorites_SupplierID";
            Relation["SupplierFavoritesInfo.Supplier_Favorites_Type"] = "U_Supplier_Favorites.Supplier_Favorites_Type";
            Relation["SupplierFavoritesInfo.Supplier_Favorites_TargetID"] = "U_Supplier_Favorites.Supplier_Favorites_TargetID";
            Relation["SupplierFavoritesInfo.Supplier_Favorites_Addtime"] = "U_Supplier_Favorites.Supplier_Favorites_Addtime";
            Relation["SupplierFavoritesInfo.Supplier_Favorites_Site"] = "U_Supplier_Favorites.Supplier_Favorites_Site";

            //Supplier_SubAccount
            Relation["SupplierSubAccountInfo.Supplier_SubAccount_ID"] = "Supplier_SubAccount.Supplier_SubAccount_ID";
            Relation["SupplierSubAccountInfo.Supplier_SubAccount_SupplierID"] = "Supplier_SubAccount.Supplier_SubAccount_SupplierID";
            Relation["SupplierSubAccountInfo.Supplier_SubAccount_Name"] = "Supplier_SubAccount.Supplier_SubAccount_Name";
            Relation["SupplierSubAccountInfo.Supplier_SubAccount_Password"] = "Supplier_SubAccount.Supplier_SubAccount_Password";
            Relation["SupplierSubAccountInfo.Supplier_SubAccount_TrueName"] = "Supplier_SubAccount.Supplier_SubAccount_TrueName";
            Relation["SupplierSubAccountInfo.Supplier_SubAccount_Department"] = "Supplier_SubAccount.Supplier_SubAccount_Department";
            Relation["SupplierSubAccountInfo.Supplier_SubAccount_Tel"] = "Supplier_SubAccount.Supplier_SubAccount_Tel";
            Relation["SupplierSubAccountInfo.Supplier_SubAccount_Mobile"] = "Supplier_SubAccount.Supplier_SubAccount_Mobile";
            Relation["SupplierSubAccountInfo.Supplier_SubAccount_Email"] = "Supplier_SubAccount.Supplier_SubAccount_Email";
            Relation["SupplierSubAccountInfo.Supplier_SubAccount_ExpireTime"] = "Supplier_SubAccount.Supplier_SubAccount_ExpireTime";
            Relation["SupplierSubAccountInfo.Supplier_SubAccount_AddTime"] = "Supplier_SubAccount.Supplier_SubAccount_AddTime";
            Relation["SupplierSubAccountInfo.Supplier_SubAccount_lastLoginTime"] = "Supplier_SubAccount.Supplier_SubAccount_lastLoginTime";
            Relation["SupplierSubAccountInfo.Supplier_SubAccount_IsActive"] = "Supplier_SubAccount.Supplier_SubAccount_IsActive";
            Relation["SupplierSubAccountInfo.Supplier_SubAccount_Privilege"] = "Supplier_SubAccount.Supplier_SubAccount_Privilege";


            //Supplier_SubAccount_Log
            Relation["SupplierSubAccountLogInfo.Log_ID"] = "Supplier_SubAccount_Log.Log_ID";
            Relation["SupplierSubAccountLogInfo.Log_Supplier_ID"] = "Supplier_SubAccount_Log.Log_Supplier_ID";
            Relation["SupplierSubAccountLogInfo.Log_SubAccount_ID"] = "Supplier_SubAccount_Log.Log_SubAccount_ID";
            Relation["SupplierSubAccountLogInfo.Log_SubAccount_Action"] = "Supplier_SubAccount_Log.Log_SubAccount_Action";
            Relation["SupplierSubAccountLogInfo.Log_SubAccount_Note"] = "Supplier_SubAccount_Log.Log_SubAccount_Note";
            Relation["SupplierSubAccountLogInfo.Log_Addtime"] = "Supplier_SubAccount_Log.Log_Addtime";

            //Supplier_Purchase
            Relation["SupplierPurchaseInfo.Purchase_ID"] = "Supplier_Purchase.Purchase_ID";
            Relation["SupplierPurchaseInfo.Purchase_TypeID"] = "Supplier_Purchase.Purchase_TypeID";
            Relation["SupplierPurchaseInfo.Purchase_SupplierID"] = "Supplier_Purchase.Purchase_SupplierID";
            Relation["SupplierPurchaseInfo.Purchase_Title"] = "Supplier_Purchase.Purchase_Title";
            Relation["SupplierPurchaseInfo.Purchase_DeliveryTime"] = "Supplier_Purchase.Purchase_DeliveryTime";
            Relation["SupplierPurchaseInfo.Purchase_State"] = "Supplier_Purchase.Purchase_State";
            Relation["SupplierPurchaseInfo.Purchase_City"] = "Supplier_Purchase.Purchase_City";
            Relation["SupplierPurchaseInfo.Purchase_County"] = "Supplier_Purchase.Purchase_County";
            Relation["SupplierPurchaseInfo.Purchase_Address"] = "Supplier_Purchase.Purchase_Address";
            Relation["SupplierPurchaseInfo.Purchase_Intro"] = "Supplier_Purchase.Purchase_Intro";
            Relation["SupplierPurchaseInfo.Purchase_Addtime"] = "Supplier_Purchase.Purchase_Addtime";
            Relation["SupplierPurchaseInfo.Purchase_Status"] = "Supplier_Purchase.Purchase_Status";
            Relation["SupplierPurchaseInfo.Purchase_IsActive"] = "Supplier_Purchase.Purchase_IsActive";
            Relation["SupplierPurchaseInfo.Purchase_ActiveReason"] = "Supplier_Purchase.Purchase_ActiveReason";
            Relation["SupplierPurchaseInfo.Purchase_Trash"] = "Supplier_Purchase.Purchase_Trash";
            Relation["SupplierPurchaseInfo.Purchase_ValidDate"] = "Supplier_Purchase.Purchase_ValidDate";
            Relation["SupplierPurchaseInfo.Purchase_Attachment"] = "Supplier_Purchase.Purchase_Attachment";
            Relation["SupplierPurchaseInfo.Purchase_Site"] = "Supplier_Purchase.Purchase_Site";
            Relation["SupplierPurchaseInfo.Purchase_IsRecommend"] = "Supplier_Purchase.Purchase_IsRecommend";
            Relation["SupplierPurchaseInfo.Purchase_IsPublic"] = "Supplier_Purchase.Purchase_IsPublic";
            Relation["SupplierPurchaseInfo.Purchase_CateID"] = "Supplier_Purchase.Purchase_CateID";
            Relation["SupplierPurchaseInfo.Purchase_SysUserID"] = "Supplier_Purchase.Purchase_SysUserID";

            //Supplier_Purchase_Detail
            Relation["SupplierPurchaseDetailInfo.Detail_ID"] = "Supplier_Purchase_Detail.Detail_ID";
            Relation["SupplierPurchaseDetailInfo.Detail_PurchaseID"] = "Supplier_Purchase_Detail.Detail_PurchaseID";
            Relation["SupplierPurchaseDetailInfo.Detail_Name"] = "Supplier_Purchase_Detail.Detail_Name";
            Relation["SupplierPurchaseDetailInfo.Detail_Spec"] = "Supplier_Purchase_Detail.Detail_Spec";
            Relation["SupplierPurchaseDetailInfo.Detail_Amount"] = "Supplier_Purchase_Detail.Detail_Amount";
            Relation["SupplierPurchaseDetailInfo.Detail_Price"] = "Supplier_Purchase_Detail.Detail_Price";

            //Supplier_Purchase_Category
            Relation["SupplierPurchaseCategoryInfo.Cate_ID"] = "Supplier_Purchase_Category.Cate_ID";
            Relation["SupplierPurchaseCategoryInfo.Cate_Name"] = "Supplier_Purchase_Category.Cate_Name";
            Relation["SupplierPurchaseCategoryInfo.Cate_ParentID"] = "Supplier_Purchase_Category.Cate_ParentID";
            Relation["SupplierPurchaseCategoryInfo.Cate_Sort"] = "Supplier_Purchase_Category.Cate_Sort";
            Relation["SupplierPurchaseCategoryInfo.Cate_IsActive"] = "Supplier_Purchase_Category.Cate_IsActive";
            Relation["SupplierPurchaseCategoryInfo.Cate_Site"] = "Supplier_Purchase_Category.Cate_Site";




            //Supplier_Grade
            Relation["SupplierGradeInfo.Supplier_Grade_ID"] = "Supplier_Grade.Supplier_Grade_ID";
            Relation["SupplierGradeInfo.Supplier_Grade_Name"] = "Supplier_Grade.Supplier_Grade_Name";
            Relation["SupplierGradeInfo.Supplier_Grade_Percent"] = "Supplier_Grade.Supplier_Grade_Percent";
            Relation["SupplierGradeInfo.Supplier_Grade_Default"] = "Supplier_Grade.Supplier_Grade_Default";
            Relation["SupplierGradeInfo.Supplier_Grade_RequiredCoin"] = "Supplier_Grade.Supplier_Grade_RequiredCoin";
            Relation["SupplierGradeInfo.Supplier_Grade_Site"] = "Supplier_Grade.Supplier_Grade_Site";

            //Contract
            Relation["ContractInfo.Contract_ID"] = "Contract.Contract_ID";
            Relation["ContractInfo.Contract_Type"] = "Contract.Contract_Type";
            Relation["ContractInfo.Contract_BuyerID"] = "Contract.Contract_BuyerID";
            Relation["ContractInfo.Contract_BuyerName"] = "Contract.Contract_BuyerName";
            Relation["ContractInfo.Contract_SupplierID"] = "Contract.Contract_SupplierID";
            Relation["ContractInfo.Contract_SupplierName"] = "Contract.Contract_SupplierName";
            Relation["ContractInfo.Contract_SN"] = "Contract.Contract_SN";
            Relation["ContractInfo.Contract_Name"] = "Contract.Contract_Name";
            Relation["ContractInfo.Contract_Status"] = "Contract.Contract_Status";
            Relation["ContractInfo.Contract_Confirm_Status"] = "Contract.Contract_Confirm_Status";
            Relation["ContractInfo.Contract_Payment_Status"] = "Contract.Contract_Payment_Status";
            Relation["ContractInfo.Contract_Delivery_Status"] = "Contract.Contract_Delivery_Status";
            Relation["ContractInfo.Contract_AllPrice"] = "Contract.Contract_AllPrice";
            Relation["ContractInfo.Contract_Price"] = "Contract.Contract_Price";
            Relation["ContractInfo.Contract_Freight"] = "Contract.Contract_Freight";
            Relation["ContractInfo.Contract_ServiceFee"] = "Contract.Contract_ServiceFee";
            Relation["ContractInfo.Contract_Discount"] = "Contract.Contract_Discount";
            Relation["ContractInfo.Contract_Delivery_ID"] = "Contract.Contract_Delivery_ID";
            Relation["ContractInfo.Contract_Delivery_Name"] = "Contract.Contract_Delivery_Name";
            Relation["ContractInfo.Contract_Payway_ID"] = "Contract.Contract_Payway_ID";
            Relation["ContractInfo.Contract_Payway_Name"] = "Contract.Contract_Payway_Name";
            Relation["ContractInfo.Contract_Note"] = "Contract.Contract_Note";
            Relation["ContractInfo.Contract_Template"] = "Contract.Contract_Template";
            Relation["ContractInfo.Contract_Addtime"] = "Contract.Contract_Addtime";
            Relation["ContractInfo.Contract_Site"] = "Contract.Contract_Site";
            Relation["ContractInfo.Contract_Source"] = "Contract.Contract_Source";
            Relation["ContractInfo.Contract_IsEvaluate"] = "Contract.Contract_IsEvaluate";

            //Contract_Template
            Relation["ContractTemplateInfo.Contract_Template_ID"] = "Contract_Template.Contract_Template_ID";
            Relation["ContractTemplateInfo.Contract_Template_Name"] = "Contract_Template.Contract_Template_Name";
            Relation["ContractTemplateInfo.Contract_Template_Code"] = "Contract_Template.Contract_Template_Code";
            Relation["ContractTemplateInfo.Contract_Template_Content"] = "Contract_Template.Contract_Template_Content";
            Relation["ContractTemplateInfo.Contract_Template_Addtime"] = "Contract_Template.Contract_Template_Addtime";
            Relation["ContractTemplateInfo.Contract_Template_Site"] = "Contract_Template.Contract_Template_Site";


            //Supplier_PriceAsk
            Relation["SupplierPriceAskInfo.PriceAsk_ID"] = "Supplier_PriceAsk.PriceAsk_ID";
            Relation["SupplierPriceAskInfo.PriceAsk_ProductID"] = "Supplier_PriceAsk.PriceAsk_ProductID";
            Relation["SupplierPriceAskInfo.PriceAsk_MemberID"] = "Supplier_PriceAsk.PriceAsk_MemberID";
            Relation["SupplierPriceAskInfo.PriceAsk_Title"] = "Supplier_PriceAsk.PriceAsk_Title";
            Relation["SupplierPriceAskInfo.PriceAsk_Name"] = "Supplier_PriceAsk.PriceAsk_Name";
            Relation["SupplierPriceAskInfo.PriceAsk_Phone"] = "Supplier_PriceAsk.PriceAsk_Phone";
            Relation["SupplierPriceAskInfo.PriceAsk_Quantity"] = "Supplier_PriceAsk.PriceAsk_Quantity";
            Relation["SupplierPriceAskInfo.PriceAsk_Price"] = "Supplier_PriceAsk.PriceAsk_Price";
            Relation["SupplierPriceAskInfo.PriceAsk_DeliveryDate"] = "Supplier_PriceAsk.PriceAsk_DeliveryDate";
            Relation["SupplierPriceAskInfo.PriceAsk_Content"] = "Supplier_PriceAsk.PriceAsk_Content";
            Relation["SupplierPriceAskInfo.PriceAsk_AddTime"] = "Supplier_PriceAsk.PriceAsk_AddTime";
            Relation["SupplierPriceAskInfo.PriceAsk_ReplyContent"] = "Supplier_PriceAsk.PriceAsk_ReplyContent";
            Relation["SupplierPriceAskInfo.PriceAsk_ReplyTime"] = "Supplier_PriceAsk.PriceAsk_ReplyTime";
            Relation["SupplierPriceAskInfo.PriceAsk_IsReply"] = "Supplier_PriceAsk.PriceAsk_IsReply";

            //Supplier_PriceReport
            Relation["SupplierPriceReportInfo.PriceReport_ID"] = "Supplier_PriceReport.PriceReport_ID";
            Relation["SupplierPriceReportInfo.PriceReport_PurchaseID"] = "Supplier_PriceReport.PriceReport_PurchaseID";
            Relation["SupplierPriceReportInfo.PriceReport_MemberID"] = "Supplier_PriceReport.PriceReport_MemberID";
            Relation["SupplierPriceReportInfo.PriceReport_Title"] = "Supplier_PriceReport.PriceReport_Title";
            Relation["SupplierPriceReportInfo.PriceReport_Name"] = "Supplier_PriceReport.PriceReport_Name";
            Relation["SupplierPriceReportInfo.PriceReport_Phone"] = "Supplier_PriceReport.PriceReport_Phone";
            Relation["SupplierPriceReportInfo.PriceReport_DeliveryDate"] = "Supplier_PriceReport.PriceReport_DeliveryDate";
            Relation["SupplierPriceReportInfo.PriceReport_AddTime"] = "Supplier_PriceReport.PriceReport_AddTime";
            Relation["SupplierPriceReportInfo.PriceReport_ReplyContent"] = "Supplier_PriceReport.PriceReport_ReplyContent";
            Relation["SupplierPriceReportInfo.PriceReport_ReplyTime"] = "Supplier_PriceReport.PriceReport_ReplyTime";
            Relation["SupplierPriceReportInfo.PriceReport_IsReply"] = "Supplier_PriceReport.PriceReport_IsReply";
            Relation["SupplierPriceReportInfo.PriceReport_AuditStatus"] = "Supplier_PriceReport.PriceReport_AuditStatus";
            Relation["SupplierPriceReportInfo.PriceReport_Note"] = "Supplier_PriceReport.PriceReport_Note";

            //Supplier_PriceReport_Detail
            Relation["SupplierPriceReportDetailInfo.Detail_ID"] = "Supplier_PriceReport_Detail.Detail_ID";
            Relation["SupplierPriceReportDetailInfo.Detail_PriceReportID"] = "Supplier_PriceReport_Detail.Detail_PriceReportID";
            Relation["SupplierPriceReportDetailInfo.Detail_PurchaseID"] = "Supplier_PriceReport_Detail.Detail_PurchaseID";
            Relation["SupplierPriceReportDetailInfo.Detail_PurchaseDetailID"] = "Supplier_PriceReport_Detail.Detail_PurchaseDetailID";
            Relation["SupplierPriceReportDetailInfo.Detail_Amount"] = "Supplier_PriceReport_Detail.Detail_Amount";
            Relation["SupplierPriceReportDetailInfo.Detail_Price"] = "Supplier_PriceReport_Detail.Detail_Price";

            //Supplier_Address
            Relation["SupplierAddressInfo.Supplier_Address_ID"] = "Supplier_Address.Supplier_Address_ID";
            Relation["SupplierAddressInfo.Supplier_Address_SupplierID"] = "Supplier_Address.Supplier_Address_SupplierID";
            Relation["SupplierAddressInfo.Supplier_Address_Country"] = "Supplier_Address.Supplier_Address_Country";
            Relation["SupplierAddressInfo.Supplier_Address_State"] = "Supplier_Address.Supplier_Address_State";
            Relation["SupplierAddressInfo.Supplier_Address_City"] = "Supplier_Address.Supplier_Address_City";
            Relation["SupplierAddressInfo.Supplier_Address_County"] = "Supplier_Address.Supplier_Address_County";
            Relation["SupplierAddressInfo.Supplier_Address_StreetAddress"] = "Supplier_Address.Supplier_Address_StreetAddress";
            Relation["SupplierAddressInfo.Supplier_Address_Zip"] = "Supplier_Address.Supplier_Address_Zip";
            Relation["SupplierAddressInfo.Supplier_Address_Name"] = "Supplier_Address.Supplier_Address_Name";
            Relation["SupplierAddressInfo.Supplier_Address_Phone_Countrycode"] = "Supplier_Address.Supplier_Address_Phone_Countrycode";
            Relation["SupplierAddressInfo.Supplier_Address_Phone_Areacode"] = "Supplier_Address.Supplier_Address_Phone_Areacode";
            Relation["SupplierAddressInfo.Supplier_Address_Phone_Number"] = "Supplier_Address.Supplier_Address_Phone_Number";
            Relation["SupplierAddressInfo.Supplier_Address_Mobile"] = "Supplier_Address.Supplier_Address_Mobile";
            Relation["SupplierAddressInfo.Supplier_Address_Site"] = "Supplier_Address.Supplier_Address_Site";

            //Pay_Type
            Relation["PayTypeInfo.Pay_Type_ID"] = "Pay_Type.Pay_Type_ID";
            Relation["PayTypeInfo.Pay_Type_Name"] = "Pay_Type.Pay_Type_Name";
            Relation["PayTypeInfo.Pay_Type_Sort"] = "Pay_Type.Pay_Type_Sort";
            Relation["PayTypeInfo.Pay_Type_IsActive"] = "Pay_Type.Pay_Type_IsActive";
            Relation["PayTypeInfo.Pay_Type_Site"] = "Pay_Type.Pay_Type_Site";

            //Supplier_Contract_Template
            Relation["SupplierContractTemplateInfo.Contract_Template_ID"] = "Supplier_Contract_Template.Contract_Template_ID";
            Relation["SupplierContractTemplateInfo.Contract_Template_Name"] = "Supplier_Contract_Template.Contract_Template_Name";
            Relation["SupplierContractTemplateInfo.Contract_Template_SupplierID"] = "Supplier_Contract_Template.Contract_Template_SupplierID";
            Relation["SupplierContractTemplateInfo.Contract_Template_Content"] = "Supplier_Contract_Template.Contract_Template_Content";
            Relation["SupplierContractTemplateInfo.Contract_Template_Addtime"] = "Supplier_Contract_Template.Contract_Template_Addtime";
            Relation["SupplierContractTemplateInfo.Contract_Template_Site"] = "Supplier_Contract_Template.Contract_Template_Site";

            //Contract_Invoice_Apply
            Relation["ContractInvoiceApplyInfo.Invoice_Apply_ID"] = "Contract_Invoice_Apply.Invoice_Apply_ID";
            Relation["ContractInvoiceApplyInfo.Invoice_Apply_ContractID"] = "Contract_Invoice_Apply.Invoice_Apply_ContractID";
            Relation["ContractInvoiceApplyInfo.Invoice_Apply_InvoiceID"] = "Contract_Invoice_Apply.Invoice_Apply_InvoiceID";
            Relation["ContractInvoiceApplyInfo.Invoice_Apply_ApplyAmount"] = "Contract_Invoice_Apply.Invoice_Apply_ApplyAmount";
            Relation["ContractInvoiceApplyInfo.Invoice_Apply_Amount"] = "Contract_Invoice_Apply.Invoice_Apply_Amount";
            Relation["ContractInvoiceApplyInfo.Invoice_Apply_Status"] = "Contract_Invoice_Apply.Invoice_Apply_Status";
            Relation["ContractInvoiceApplyInfo.Invoice_Apply_Note"] = "Contract_Invoice_Apply.Invoice_Apply_Note";
            Relation["ContractInvoiceApplyInfo.Invoice_Apply_Addtime"] = "Contract_Invoice_Apply.Invoice_Apply_Addtime";

            //Contract_Invoice
            Relation["ContractInvoiceInfo.Invoice_ID"] = "Contract_Invoice.Invoice_ID";
            Relation["ContractInvoiceInfo.Invoice_ContractID"] = "Contract_Invoice.Invoice_ContractID";
            Relation["ContractInvoiceInfo.Invoice_Type"] = "Contract_Invoice.Invoice_Type";
            Relation["ContractInvoiceInfo.Invoice_Title"] = "Contract_Invoice.Invoice_Title";
            Relation["ContractInvoiceInfo.Invoice_Content"] = "Contract_Invoice.Invoice_Content";
            Relation["ContractInvoiceInfo.Invoice_FirmName"] = "Contract_Invoice.Invoice_FirmName";
            Relation["ContractInvoiceInfo.Invoice_VAT_FirmName"] = "Contract_Invoice.Invoice_VAT_FirmName";
            Relation["ContractInvoiceInfo.Invoice_VAT_Code"] = "Contract_Invoice.Invoice_VAT_Code";
            Relation["ContractInvoiceInfo.Invoice_VAT_RegAddr"] = "Contract_Invoice.Invoice_VAT_RegAddr";
            Relation["ContractInvoiceInfo.Invoice_VAT_RegTel"] = "Contract_Invoice.Invoice_VAT_RegTel";
            Relation["ContractInvoiceInfo.Invoice_VAT_Bank"] = "Contract_Invoice.Invoice_VAT_Bank";
            Relation["ContractInvoiceInfo.Invoice_VAT_BankAccount"] = "Contract_Invoice.Invoice_VAT_BankAccount";
            Relation["ContractInvoiceInfo.Invoice_VAT_Content"] = "Contract_Invoice.Invoice_VAT_Content";
            Relation["ContractInvoiceInfo.Invoice_Address"] = "Contract_Invoice.Invoice_Address";
            Relation["ContractInvoiceInfo.Invoice_Name"] = "Contract_Invoice.Invoice_Name";
            Relation["ContractInvoiceInfo.Invoice_ZipCode"] = "Contract_Invoice.Invoice_ZipCode";
            Relation["ContractInvoiceInfo.Invoice_Tel"] = "Contract_Invoice.Invoice_Tel";
            Relation["ContractInvoiceInfo.Invoice_Status"] = "Contract_Invoice.Invoice_Status";
            Relation["ContractInvoiceInfo.Invoice_PersonelName"] = "Contract_Invoice.Invoice_PersonelName";
            Relation["ContractInvoiceInfo.Invoice_PersonelCard"] = "Contract_Invoice.Invoice_PersonelCard";
            Relation["ContractInvoiceInfo.Invoice_VAT_Cert"] = "Contract_Invoice.Invoice_VAT_Cert";

            //Supplier_Invoice
            Relation["SupplierInvoiceInfo.Invoice_ID"] = "Supplier_Invoice.Invoice_ID";
            Relation["SupplierInvoiceInfo.Invoice_SupplierID"] = "Supplier_Invoice.Invoice_SupplierID";
            Relation["SupplierInvoiceInfo.Invoice_Type"] = "Supplier_Invoice.Invoice_Type";
            Relation["SupplierInvoiceInfo.Invoice_Title"] = "Supplier_Invoice.Invoice_Title";
            Relation["SupplierInvoiceInfo.Invoice_Content"] = "Supplier_Invoice.Invoice_Content";
            Relation["SupplierInvoiceInfo.Invoice_FirmName"] = "Supplier_Invoice.Invoice_FirmName";
            Relation["SupplierInvoiceInfo.Invoice_VAT_FirmName"] = "Supplier_Invoice.Invoice_VAT_FirmName";
            Relation["SupplierInvoiceInfo.Invoice_VAT_Code"] = "Supplier_Invoice.Invoice_VAT_Code";
            Relation["SupplierInvoiceInfo.Invoice_VAT_RegAddr"] = "Supplier_Invoice.Invoice_VAT_RegAddr";
            Relation["SupplierInvoiceInfo.Invoice_VAT_RegTel"] = "Supplier_Invoice.Invoice_VAT_RegTel";
            Relation["SupplierInvoiceInfo.Invoice_VAT_Bank"] = "Supplier_Invoice.Invoice_VAT_Bank";
            Relation["SupplierInvoiceInfo.Invoice_VAT_BankAccount"] = "Supplier_Invoice.Invoice_VAT_BankAccount";
            Relation["SupplierInvoiceInfo.Invoice_VAT_Content"] = "Supplier_Invoice.Invoice_VAT_Content";
            Relation["SupplierInvoiceInfo.Invoice_Address"] = "Supplier_Invoice.Invoice_Address";
            Relation["SupplierInvoiceInfo.Invoice_Name"] = "Supplier_Invoice.Invoice_Name";
            Relation["SupplierInvoiceInfo.Invoice_ZipCode"] = "Supplier_Invoice.Invoice_ZipCode";
            Relation["SupplierInvoiceInfo.Invoice_Tel"] = "Supplier_Invoice.Invoice_Tel";
            Relation["SupplierInvoiceInfo.Invoice_PersonelName"] = "Supplier_Invoice.Invoice_PersonelName";
            Relation["SupplierInvoiceInfo.Invoice_PersonelCard"] = "Supplier_Invoice.Invoice_PersonelCard";
            Relation["SupplierInvoiceInfo.Invoice_VAT_Cert"] = "Supplier_Invoice.Invoice_VAT_Cert";

            //Contract_Divided_Payment
            Relation["ContractDividedPaymentInfo.Payment_ID"] = "Contract_Divided_Payment.Payment_ID";
            Relation["ContractDividedPaymentInfo.Payment_ContractID"] = "Contract_Divided_Payment.Payment_ContractID";
            Relation["ContractDividedPaymentInfo.Payment_Name"] = "Contract_Divided_Payment.Payment_Name";
            Relation["ContractDividedPaymentInfo.Payment_Amount"] = "Contract_Divided_Payment.Payment_Amount";
            Relation["ContractDividedPaymentInfo.Payment_PaymentLine"] = "Contract_Divided_Payment.Payment_PaymentLine";
            Relation["ContractDividedPaymentInfo.Payment_PaymentStatus"] = "Contract_Divided_Payment.Payment_PaymentStatus";
            Relation["ContractDividedPaymentInfo.Payment_PaymentTime"] = "Contract_Divided_Payment.Payment_PaymentTime";
            Relation["ContractDividedPaymentInfo.Payment_Note"] = "Contract_Divided_Payment.Payment_Note";

            //Contract_Delivery
            Relation["ContractDeliveryInfo.Contract_Delivery_ID"] = "Contract_Delivery.Contract_Delivery_ID";
            Relation["ContractDeliveryInfo.Contract_Delivery_ContractID"] = "Contract_Delivery.Contract_Delivery_ContractID";
            Relation["ContractDeliveryInfo.Contract_Delivery_DeliveryStatus"] = "Contract_Delivery.Contract_Delivery_DeliveryStatus";
            Relation["ContractDeliveryInfo.Contract_Delivery_SysUserID"] = "Contract_Delivery.Contract_Delivery_SysUserID";
            Relation["ContractDeliveryInfo.Contract_Delivery_DocNo"] = "Contract_Delivery.Contract_Delivery_DocNo";
            Relation["ContractDeliveryInfo.Contract_Delivery_Name"] = "Contract_Delivery.Contract_Delivery_Name";
            Relation["ContractDeliveryInfo.Contract_Delivery_CompanyName"] = "Contract_Delivery.Contract_Delivery_CompanyName";
            Relation["ContractDeliveryInfo.Contract_Delivery_Code"] = "Contract_Delivery.Contract_Delivery_Code";
            Relation["ContractDeliveryInfo.Contract_Delivery_Amount"] = "Contract_Delivery.Contract_Delivery_Amount";
            Relation["ContractDeliveryInfo.Contract_Delivery_Note"] = "Contract_Delivery.Contract_Delivery_Note";
            Relation["ContractDeliveryInfo.Contract_Delivery_AccpetNote"] = "Contract_Delivery.Contract_Delivery_AccpetNote";
            Relation["ContractDeliveryInfo.Contract_Delivery_Addtime"] = "Contract_Delivery.Contract_Delivery_Addtime";
            Relation["ContractDeliveryInfo.Contract_Delivery_Site"] = "Contract_Delivery.Contract_Delivery_Site";

            //Contract_Payment
            Relation["ContractPaymentInfo.Contract_Payment_ID"] = "Contract_Payment.Contract_Payment_ID";
            Relation["ContractPaymentInfo.Contract_Payment_ContractID"] = "Contract_Payment.Contract_Payment_ContractID";
            Relation["ContractPaymentInfo.Contract_Payment_PaymentStatus"] = "Contract_Payment.Contract_Payment_PaymentStatus";
            Relation["ContractPaymentInfo.Contract_Payment_SysUserID"] = "Contract_Payment.Contract_Payment_SysUserID";
            Relation["ContractPaymentInfo.Contract_Payment_DocNo"] = "Contract_Payment.Contract_Payment_DocNo";
            Relation["ContractPaymentInfo.Contract_Payment_Name"] = "Contract_Payment.Contract_Payment_Name";
            Relation["ContractPaymentInfo.Contract_Payment_Amount"] = "Contract_Payment.Contract_Payment_Amount";
            Relation["ContractPaymentInfo.Contract_Payment_Note"] = "Contract_Payment.Contract_Payment_Note";
            Relation["ContractPaymentInfo.Contract_Payment_Addtime"] = "Contract_Payment.Contract_Payment_Addtime";
            Relation["ContractPaymentInfo.Contract_Payment_Site"] = "Contract_Payment.Contract_Payment_Site";


            //Supplier_Agent_Protocal
            Relation["SupplierAgentProtocalInfo.Protocal_ID"] = "Supplier_Agent_Protocal.Protocal_ID";
            Relation["SupplierAgentProtocalInfo.Protocal_Code"] = "Supplier_Agent_Protocal.Protocal_Code";
            Relation["SupplierAgentProtocalInfo.Protocal_PurchaseID"] = "Supplier_Agent_Protocal.Protocal_PurchaseID";
            Relation["SupplierAgentProtocalInfo.Protocal_SupplierID"] = "Supplier_Agent_Protocal.Protocal_SupplierID";
            Relation["SupplierAgentProtocalInfo.Protocal_Status"] = "Supplier_Agent_Protocal.Protocal_Status";
            Relation["SupplierAgentProtocalInfo.Protocal_Template"] = "Supplier_Agent_Protocal.Protocal_Template";
            Relation["SupplierAgentProtocalInfo.Protocal_Addtime"] = "Supplier_Agent_Protocal.Protocal_Addtime";
            Relation["SupplierAgentProtocalInfo.Protocal_Site"] = "Supplier_Agent_Protocal.Protocal_Site";

            //Supplier_Consumption
            Relation["SupplierConsumptionInfo.Consump_ID"] = "Supplier_Consumption.Consump_ID";
            Relation["SupplierConsumptionInfo.Consump_SupplierID"] = "Supplier_Consumption.Consump_SupplierID";
            Relation["SupplierConsumptionInfo.Consump_CoinRemain"] = "Supplier_Consumption.Consump_CoinRemain";
            Relation["SupplierConsumptionInfo.Consump_Coin"] = "Supplier_Consumption.Consump_Coin";
            Relation["SupplierConsumptionInfo.Consump_Reason"] = "Supplier_Consumption.Consump_Reason";
            Relation["SupplierConsumptionInfo.Consump_Addtime"] = "Supplier_Consumption.Consump_Addtime";

            //Orders_Contract
            Relation["OrdersContractInfo.ID"] = "Orders_Contract.ID";
            Relation["OrdersContractInfo.SN"] = "Orders_Contract.SN";
            Relation["OrdersContractInfo.Name"] = "Orders_Contract.Name";
            Relation["OrdersContractInfo.Orders_ID"] = "Orders_Contract.Orders_ID";
            Relation["OrdersContractInfo.Path"] = "Orders_Contract.Path";
            Relation["OrdersContractInfo.Addtime"] = "Orders_Contract.Addtime";
            Relation["OrdersContractInfo.Site"] = "Orders_Contract.Site";

            //Orders_Accompanying
            Relation["OrdersAccompanyingInfo.Accompanying_ID"] = "Orders_Accompanying.Accompanying_ID";
            Relation["OrdersAccompanyingInfo.Accompanying_OrdersID"] = "Orders_Accompanying.Accompanying_OrdersID";
            Relation["OrdersAccompanyingInfo.Accompanying_DeliveryID"] = "Orders_Accompanying.Accompanying_DeliveryID";
            Relation["OrdersAccompanyingInfo.Accompanying_SN"] = "Orders_Accompanying.Accompanying_SN";
            Relation["OrdersAccompanyingInfo.Accompanying_Name"] = "Orders_Accompanying.Accompanying_Name";
            Relation["OrdersAccompanyingInfo.Accompanying_Amount"] = "Orders_Accompanying.Accompanying_Amount";
            Relation["OrdersAccompanyingInfo.Accompanying_Unit"] = "Orders_Accompanying.Accompanying_Unit";
            Relation["OrdersAccompanyingInfo.Accompanying_Price"] = "Orders_Accompanying.Accompanying_Price";
            Relation["OrdersAccompanyingInfo.Accompanying_Status"] = "Orders_Accompanying.Accompanying_Status";
            Relation["OrdersAccompanyingInfo.Accompanying_Addtime"] = "Orders_Accompanying.Accompanying_Addtime";
            Relation["OrdersAccompanyingInfo.Accompanying_Site"] = "Orders_Accompanying.Accompanying_Site";

            //Orders_Accompanying_Goods
            Relation["OrdersAccompanyingGoodsInfo.Goods_ID"] = "Orders_Accompanying_Goods.Goods_ID";
            Relation["OrdersAccompanyingGoodsInfo.Goods_GoodsID"] = "Orders_Accompanying_Goods.Goods_GoodsID";
            Relation["OrdersAccompanyingGoodsInfo.Goods_DeliveryID"] = "Orders_Accompanying_Goods.Goods_DeliveryID";
            Relation["OrdersAccompanyingGoodsInfo.Goods_Amount"] = "Orders_Accompanying_Goods.Goods_Amount";
            Relation["OrdersAccompanyingGoodsInfo.Goods_AcceptAmount"] = "Orders_Accompanying_Goods.Goods_AcceptAmount";

            //Orders_Accompanying_Img
            Relation["OrdersAccompanyingImgInfo.Img_ID"] = "Orders_Accompanying_Img.Img_ID";
            Relation["OrdersAccompanyingImgInfo.Img_AccompanyingID"] = "Orders_Accompanying_Img.Img_AccompanyingID";
            Relation["OrdersAccompanyingImgInfo.Img_Path"] = "Orders_Accompanying_Img.Img_Path";

            //Product_WholeSalePrice
            Relation["ProductWholeSalePriceInfo.Product_WholeSalePrice_ID"] = "Product_WholeSalePrice.Product_WholeSalePrice_ID";
            Relation["ProductWholeSalePriceInfo.Product_WholeSalePrice_ProductID"] = "Product_WholeSalePrice.Product_WholeSalePrice_ProductID";
            Relation["ProductWholeSalePriceInfo.Product_WholeSalePrice_MinAmount"] = "Product_WholeSalePrice.Product_WholeSalePrice_MinAmount";
            Relation["ProductWholeSalePriceInfo.Product_WholeSalePrice_MaxAmount"] = "Product_WholeSalePrice.Product_WholeSalePrice_MaxAmount";
            Relation["ProductWholeSalePriceInfo.Product_WholeSalePrice_Price"] = "Product_WholeSalePrice.Product_WholeSalePrice_Price";
            Relation["ProductWholeSalePriceInfo.Product_WholeSalePrice_IsActive"] = "Product_WholeSalePrice.Product_WholeSalePrice_IsActive";
            Relation["ProductWholeSalePriceInfo.Product_WholeSalePrice_Site"] = "Product_WholeSalePrice.Product_WholeSalePrice_Site";

            //Member_Cert_Type
            Relation["MemberCertTypeInfo.Member_Cert_Type_ID"] = "Member_Cert_Type.Member_Cert_Type_ID";
            Relation["MemberCertTypeInfo.Member_Cert_Type_Name"] = "Member_Cert_Type.Member_Cert_Type_Name";
            Relation["MemberCertTypeInfo.Member_Cert_Type_Sort"] = "Member_Cert_Type.Member_Cert_Type_Sort";
            Relation["MemberCertTypeInfo.Member_Cert_Type_IsActive"] = "Member_Cert_Type.Member_Cert_Type_IsActive";
            Relation["MemberCertTypeInfo.Member_Cert_Type_Site"] = "Member_Cert_Type.Member_Cert_Type_Site";

            //Sys_Message
            Relation["SysMessageInfo.Message_ID"] = "Sys_Message.Message_ID";
            Relation["SysMessageInfo.Message_Type"] = "Sys_Message.Message_Type";
            Relation["SysMessageInfo.Message_UserType"] = "Sys_Message.Message_UserType";
            Relation["SysMessageInfo.Message_ReceiveID"] = "Sys_Message.Message_ReceiveID";
            Relation["SysMessageInfo.Message_SendID"] = "Sys_Message.Message_SendID";
            Relation["SysMessageInfo.Message_Content"] = "Sys_Message.Message_Content";
            Relation["SysMessageInfo.Message_Addtime"] = "Sys_Message.Message_Addtime";
            Relation["SysMessageInfo.Message_Status"] = "Sys_Message.Message_Status";
            Relation["SysMessageInfo.Message_Site"] = "Sys_Message.Message_Site";
            Relation["SysMessageInfo.Message_IsHidden"] = "Sys_Message.Message_IsHidden";

            //Supplier_Merchants
            Relation["SupplierMerchantsInfo.Merchants_ID"] = "Supplier_Merchants.Merchants_ID";
            Relation["SupplierMerchantsInfo.Merchants_SupplierID"] = "Supplier_Merchants.Merchants_SupplierID";
            Relation["SupplierMerchantsInfo.Merchants_Name"] = "Supplier_Merchants.Merchants_Name";
            Relation["SupplierMerchantsInfo.Merchants_Validity"] = "Supplier_Merchants.Merchants_Validity";
            Relation["SupplierMerchantsInfo.Merchants_Channel"] = "Supplier_Merchants.Merchants_Channel";
            Relation["SupplierMerchantsInfo.Merchants_Advantage"] = "Supplier_Merchants.Merchants_Advantage";
            Relation["SupplierMerchantsInfo.Merchants_Trem"] = "Supplier_Merchants.Merchants_Trem";
            Relation["SupplierMerchantsInfo.Merchants_Intro"] = "Supplier_Merchants.Merchants_Intro";
            Relation["SupplierMerchantsInfo.Merchants_Img"] = "Supplier_Merchants.Merchants_Img";
            Relation["SupplierMerchantsInfo.Merchants_AddTime"] = "Supplier_Merchants.Merchants_AddTime";
            Relation["SupplierMerchantsInfo.Merchants_Site"] = "Supplier_Merchants.Merchants_Site";

            //Supplier_Merchants_Message
            Relation["SupplierMerchantsMessageInfo.Message_ID"] = "Supplier_Merchants_Message.Message_ID";
            Relation["SupplierMerchantsMessageInfo.Message_MemberID"] = "Supplier_Merchants_Message.Message_MemberID";
            Relation["SupplierMerchantsMessageInfo.Message_MerchantsID"] = "Supplier_Merchants_Message.Message_MerchantsID";
            Relation["SupplierMerchantsMessageInfo.Message_Content"] = "Supplier_Merchants_Message.Message_Content";
            Relation["SupplierMerchantsMessageInfo.Message_Contactman"] = "Supplier_Merchants_Message.Message_Contactman";
            Relation["SupplierMerchantsMessageInfo.Message_ContactMobile"] = "Supplier_Merchants_Message.Message_ContactMobile";
            Relation["SupplierMerchantsMessageInfo.Message_ContactEmail"] = "Supplier_Merchants_Message.Message_ContactEmail";
            Relation["SupplierMerchantsMessageInfo.Message_AddTime"] = "Supplier_Merchants_Message.Message_AddTime";

            //Member_Purchase
            Relation["MemberPurchaseInfo.Purchase_ID"] = "Member_Purchase.Purchase_ID";
            Relation["MemberPurchaseInfo.Purchase_MemberID"] = "Member_Purchase.Purchase_MemberID";
            Relation["MemberPurchaseInfo.Purchase_Title"] = "Member_Purchase.Purchase_Title";
            Relation["MemberPurchaseInfo.Purchase_Img"] = "Member_Purchase.Purchase_Img";
            Relation["MemberPurchaseInfo.Purchase_Amount"] = "Member_Purchase.Purchase_Amount";
            Relation["MemberPurchaseInfo.Purchase_Unit"] = "Member_Purchase.Purchase_Unit";
            Relation["MemberPurchaseInfo.Purchase_Validity"] = "Member_Purchase.Purchase_Validity";
            Relation["MemberPurchaseInfo.Purchase_Intro"] = "Member_Purchase.Purchase_Intro";
            Relation["MemberPurchaseInfo.Purchase_Status"] = "Member_Purchase.Purchase_Status";
            Relation["MemberPurchaseInfo.Purchase_Addtime"] = "Member_Purchase.Purchase_Addtime";
            Relation["MemberPurchaseInfo.Purchase_Site"] = "Member_Purchase.Purchase_Site";

            //Member_Purchase_Reply
            Relation["MemberPurchaseReplyInfo.Reply_ID"] = "Member_Purchase_Reply.Reply_ID";
            Relation["MemberPurchaseReplyInfo.Reply_PurchaseID"] = "Member_Purchase_Reply.Reply_PurchaseID";
            Relation["MemberPurchaseReplyInfo.Reply_SupplierID"] = "Member_Purchase_Reply.Reply_SupplierID";
            Relation["MemberPurchaseReplyInfo.Reply_Title"] = "Member_Purchase_Reply.Reply_Title";
            Relation["MemberPurchaseReplyInfo.Reply_Content"] = "Member_Purchase_Reply.Reply_Content";
            Relation["MemberPurchaseReplyInfo.Reply_Contactman"] = "Member_Purchase_Reply.Reply_Contactman";
            Relation["MemberPurchaseReplyInfo.Reply_Mobile"] = "Member_Purchase_Reply.Reply_Mobile";
            Relation["MemberPurchaseReplyInfo.Reply_Email"] = "Member_Purchase_Reply.Reply_Email";
            Relation["MemberPurchaseReplyInfo.Reply_Addtime"] = "Member_Purchase_Reply.Reply_Addtime";

            //Keywords_Ranking
            Relation["KeywordsRankingInfo.ID"] = "Keywords_Ranking.ID";
            Relation["KeywordsRankingInfo.Type"] = "Keywords_Ranking.Type";
            Relation["KeywordsRankingInfo.Keyword"] = "Keywords_Ranking.Keyword";
            Relation["KeywordsRankingInfo.addtime"] = "Keywords_Ranking.addtime";
            Relation["KeywordsRankingInfo.Site"] = "Keywords_Ranking.Site";

            //Member_Token
            Relation["MemberTokenInfo.Token"] = "Member_Token.Token";
            Relation["MemberTokenInfo.Type"] = "Member_Token.Type";
            Relation["MemberTokenInfo.MemberID"] = "Member_Token.MemberID";
            Relation["MemberTokenInfo.IP"] = "Member_Token.IP";
            Relation["MemberTokenInfo.CreateTime"] = "Member_Token.CreateTime";
            Relation["MemberTokenInfo.UpdateTime"] = "Member_Token.UpdateTime";
            Relation["MemberTokenInfo.ExpiredTime"] = "Member_Token.ExpiredTime";

            //Sys_Interface_Log
            Relation["SysInterfaceLogInfo.Log_ID"] = "Sys_Interface_Log.Log_ID";
            Relation["SysInterfaceLogInfo.Log_Type"] = "Sys_Interface_Log.Log_Type";
            Relation["SysInterfaceLogInfo.Log_Action"] = "Sys_Interface_Log.Log_Action";
            Relation["SysInterfaceLogInfo.Log_Result"] = "Sys_Interface_Log.Log_Result";
            Relation["SysInterfaceLogInfo.Log_Parameters"] = "Sys_Interface_Log.Log_Parameters";
            Relation["SysInterfaceLogInfo.Log_Remark"] = "Sys_Interface_Log.Log_Remark";
            Relation["SysInterfaceLogInfo.Log_Addtime"] = "Sys_Interface_Log.Log_Addtime";

            //Orders_LoanApply
            Relation["OrdersLoanApplyInfo.ID"] = "Orders_LoanApply.ID";
            Relation["OrdersLoanApplyInfo.MemberID"] = "Orders_LoanApply.MemberID";
            Relation["OrdersLoanApplyInfo.Orders_SN"] = "Orders_LoanApply.Orders_SN";
            Relation["OrdersLoanApplyInfo.Loan_proj_No"] = "Orders_LoanApply.Loan_proj_No";
            Relation["OrdersLoanApplyInfo.Loan_Amount"] = "Orders_LoanApply.Loan_Amount";
            Relation["OrdersLoanApplyInfo.Interest_Rate"] = "Orders_LoanApply.Interest_Rate";
            Relation["OrdersLoanApplyInfo.Interest_Rate_Unit"] = "Orders_LoanApply.Interest_Rate_Unit";
            Relation["OrdersLoanApplyInfo.Trem"] = "Orders_LoanApply.Trem";
            Relation["OrdersLoanApplyInfo.Trem_Unit"] = "Orders_LoanApply.Trem_Unit";
            Relation["OrdersLoanApplyInfo.Fee_Amount"] = "Orders_LoanApply.Fee_Amount";
            Relation["OrdersLoanApplyInfo.Repay_Method"] = "Orders_LoanApply.Repay_Method";
            Relation["OrdersLoanApplyInfo.Margin_Amount"] = "Orders_LoanApply.Margin_Amount";

            //Supplier_Margin
            Relation["SupplierMarginInfo.Supplier_Margin_ID"] = "Supplier_Margin.Supplier_Margin_ID";
            Relation["SupplierMarginInfo.Supplier_Margin_Type"] = "Supplier_Margin.Supplier_Margin_Type";
            Relation["SupplierMarginInfo.Supplier_Margin_Amount"] = "Supplier_Margin.Supplier_Margin_Amount";
            Relation["SupplierMarginInfo.Supplier_Margin_Site"] = "Supplier_Margin.Supplier_Margin_Site";

            //Bid
            //Bid
            Relation["BidInfo.Bid_ID"] = "Bid.Bid_ID";
            Relation["BidInfo.Bid_MemberID"] = "Bid.Bid_MemberID";
            Relation["BidInfo.Bid_MemberCompany"] = "Bid.Bid_MemberCompany";
            Relation["BidInfo.Bid_SupplierID"] = "Bid.Bid_SupplierID";
            Relation["BidInfo.Bid_SupplierCompany"] = "Bid.Bid_SupplierCompany";
            Relation["BidInfo.Bid_Title"] = "Bid.Bid_Title";
            Relation["BidInfo.Bid_EnterStartTime"] = "Bid.Bid_EnterStartTime";
            Relation["BidInfo.Bid_EnterEndTime"] = "Bid.Bid_EnterEndTime";
            Relation["BidInfo.Bid_BidStartTime"] = "Bid.Bid_BidStartTime";
            Relation["BidInfo.Bid_BidEndTime"] = "Bid.Bid_BidEndTime";
            Relation["BidInfo.Bid_AddTime"] = "Bid.Bid_AddTime";
            Relation["BidInfo.Bid_Bond"] = "Bid.Bid_Bond";
            Relation["BidInfo.Bid_Number"] = "Bid.Bid_Number";
            Relation["BidInfo.Bid_Status"] = "Bid.Bid_Status";
            Relation["BidInfo.Bid_Content"] = "Bid.Bid_Content";
            Relation["BidInfo.Bid_ProductType"] = "Bid.Bid_ProductType";
            Relation["BidInfo.Bid_AllPrice"] = "Bid.Bid_AllPrice";
            Relation["BidInfo.Bid_Type"] = "Bid.Bid_Type";
            Relation["BidInfo.Bid_Contract"] = "Bid.Bid_Contract";
            Relation["BidInfo.Bid_IsAudit"] = "Bid.Bid_IsAudit";
            Relation["BidInfo.Bid_AuditTime"] = "Bid.Bid_AuditTime";
            Relation["BidInfo.Bid_AuditRemarks"] = "Bid.Bid_AuditRemarks";
            Relation["BidInfo.Bid_ExcludeSupplierID"] = "Bid.Bid_ExcludeSupplierID";
            Relation["BidInfo.Bid_SN"] = "Bid.Bid_SN";
            Relation["BidInfo.Bid_DeliveryTime"] = "Bid.Bid_DeliveryTime";
            Relation["BidInfo.Bid_IsOrders"] = "Bid.Bid_IsOrders";
            Relation["BidInfo.Bid_OrdersTime"] = "Bid.Bid_OrdersTime";
            Relation["BidInfo.Bid_OrdersSN"] = "Bid.Bid_OrdersSN";
            Relation["BidInfo.Bid_FinishTime"] = "Bid.Bid_FinishTime";
            Relation["BidInfo.Bid_IsShow"] = "Bid.Bid_IsShow";




            //Bid_Product
            Relation["BidProductInfo.Bid_Product_ID"] = "Bid_Product.Bid_Product_ID";
            Relation["BidProductInfo.Bid_BidID"] = "Bid_Product.Bid_BidID";
            Relation["BidProductInfo.Bid_Product_Sort"] = "Bid_Product.Bid_Product_Sort";
            Relation["BidProductInfo.Bid_Product_Code"] = "Bid_Product.Bid_Product_Code";
            Relation["BidProductInfo.Bid_Product_Name"] = "Bid_Product.Bid_Product_Name";
            Relation["BidProductInfo.Bid_Product_Spec"] = "Bid_Product.Bid_Product_Spec";
            Relation["BidProductInfo.Bid_Product_Brand"] = "Bid_Product.Bid_Product_Brand";
            Relation["BidProductInfo.Bid_Product_Unit"] = "Bid_Product.Bid_Product_Unit";
            Relation["BidProductInfo.Bid_Product_Amount"] = "Bid_Product.Bid_Product_Amount";
            Relation["BidProductInfo.Bid_Product_Delivery"] = "Bid_Product.Bid_Product_Delivery";
            Relation["BidProductInfo.Bid_Product_Remark"] = "Bid_Product.Bid_Product_Remark";
            Relation["BidProductInfo.Bid_Product_StartPrice"] = "Bid_Product.Bid_Product_StartPrice";
            Relation["BidProductInfo.Bid_Product_Img"] = "Bid_Product.Bid_Product_Img";
            Relation["BidProductInfo.Bid_Product_ProductID"] = "Bid_Product.Bid_Product_ProductID";

            //Bid_Attachments
            Relation["BidAttachmentsInfo.Bid_Attachments_ID"] = "Bid_Attachments.Bid_Attachments_ID";
            Relation["BidAttachmentsInfo.Bid_Attachments_Sort"] = "Bid_Attachments.Bid_Attachments_Sort";
            Relation["BidAttachmentsInfo.Bid_Attachments_Name"] = "Bid_Attachments.Bid_Attachments_Name";
            Relation["BidAttachmentsInfo.Bid_Attachments_format"] = "Bid_Attachments.Bid_Attachments_format";
            Relation["BidAttachmentsInfo.Bid_Attachments_Size"] = "Bid_Attachments.Bid_Attachments_Size";
            Relation["BidAttachmentsInfo.Bid_Attachments_Remarks"] = "Bid_Attachments.Bid_Attachments_Remarks";
            Relation["BidAttachmentsInfo.Bid_Attachments_Path"] = "Bid_Attachments.Bid_Attachments_Path";
            Relation["BidAttachmentsInfo.Bid_Attachments_BidID"] = "Bid_Attachments.Bid_Attachments_BidID";


            //Bid_Enter
            Relation["BidEnterInfo.Bid_Enter_ID"] = "Bid_Enter.Bid_Enter_ID";
            Relation["BidEnterInfo.Bid_Enter_BidID"] = "Bid_Enter.Bid_Enter_BidID";
            Relation["BidEnterInfo.Bid_Enter_SupplierID"] = "Bid_Enter.Bid_Enter_SupplierID";
            Relation["BidEnterInfo.Bid_Enter_Bond"] = "Bid_Enter.Bid_Enter_Bond";
            Relation["BidEnterInfo.Bid_Enter_Type"] = "Bid_Enter.Bid_Enter_Type";
            Relation["BidEnterInfo.Bid_Enter_IsShow"] = "Bid_Enter.Bid_Enter_IsShow";

            //Tender
            Relation["TenderInfo.Tender_ID"] = "Tender.Tender_ID";
            Relation["TenderInfo.Tender_SupplierID"] = "Tender.Tender_SupplierID";
            Relation["TenderInfo.Tender_BidID"] = "Tender.Tender_BidID";
            Relation["TenderInfo.Tender_Addtime"] = "Tender.Tender_Addtime";
            Relation["TenderInfo.Tender_IsWin"] = "Tender.Tender_IsWin";
            Relation["TenderInfo.Tender_Status"] = "Tender.Tender_Status";
            Relation["TenderInfo.Tender_AllPrice"] = "Tender.Tender_AllPrice";
            Relation["TenderInfo.Tender_IsRefund"] = "Tender.Tender_IsRefund";
            Relation["TenderInfo.Tender_SN"] = "Tender.Tender_SN";
            Relation["TenderInfo.Tender_IsProduct"] = "Tender.Tender_IsProduct";
            Relation["TenderInfo.Tender_IsShow"] = "Tender.Tender_IsShow";
            Relation["TenderInfo.Tender_ANote"] = "Tender.Tender_ANote";
            Relation["TenderInfo.Tender_BNote"] = "Tender.Tender_BNote";


            //Tender_Product
            Relation["TenderProductInfo.Tender_Product_ID"] = "Tender_Product.Tender_Product_ID";
            Relation["TenderProductInfo.Tender_Product_ProductID"] = "Tender_Product.Tender_Product_ProductID";
            Relation["TenderProductInfo.Tender_TenderID"] = "Tender_Product.Tender_TenderID";
            Relation["TenderProductInfo.Tender_Product_BidProductID"] = "Tender_Product.Tender_Product_BidProductID";
            Relation["TenderProductInfo.Tender_Product_Name"] = "Tender_Product.Tender_Product_Name";
            Relation["TenderProductInfo.Tender_Price"] = "Tender_Product.Tender_Price";

            //Logistics
            Relation["LogisticsInfo.Logistics_ID"] = "Logistics.Logistics_ID";
            Relation["LogisticsInfo.Logistics_NickName"] = "Logistics.Logistics_NickName";
            Relation["LogisticsInfo.Logistics_Password"] = "Logistics.Logistics_Password";
            Relation["LogisticsInfo.Logistics_CompanyName"] = "Logistics.Logistics_CompanyName";
            Relation["LogisticsInfo.Logistics_Name"] = "Logistics.Logistics_Name";
            Relation["LogisticsInfo.Logistics_Tel"] = "Logistics.Logistics_Tel";
            Relation["LogisticsInfo.Logistics_Status"] = "Logistics.Logistics_Status";
            Relation["LogisticsInfo.Logistics_Addtime"] = "Logistics.Logistics_Addtime";
            Relation["LogisticsInfo.Logistics_Lastlogin_Time"] = "Logistics.Logistics_Lastlogin_Time";

            //Supplier_Logistics
            Relation["SupplierLogisticsInfo.Supplier_Logistics_ID"] = "Supplier_Logistics.Supplier_Logistics_ID";
            Relation["SupplierLogisticsInfo.Supplier_SupplierID"] = "Supplier_Logistics.Supplier_SupplierID";
            Relation["SupplierLogisticsInfo.Supplier_OrdersID"] = "Supplier_Logistics.Supplier_OrdersID";
            Relation["SupplierLogisticsInfo.Supplier_LogisticsID"] = "Supplier_Logistics.Supplier_LogisticsID";
            Relation["SupplierLogisticsInfo.Supplier_Status"] = "Supplier_Logistics.Supplier_Status";
            Relation["SupplierLogisticsInfo.Supplier_Orders_Address_Country"] = "Supplier_Logistics.Supplier_Orders_Address_Country";
            Relation["SupplierLogisticsInfo.Supplier_Orders_Address_State"] = "Supplier_Logistics.Supplier_Orders_Address_State";
            Relation["SupplierLogisticsInfo.Supplier_Orders_Address_City"] = "Supplier_Logistics.Supplier_Orders_Address_City";
            Relation["SupplierLogisticsInfo.Supplier_Orders_Address_County"] = "Supplier_Logistics.Supplier_Orders_Address_County";
            Relation["SupplierLogisticsInfo.Supplier_Orders_Address_StreetAddress"] = "Supplier_Logistics.Supplier_Orders_Address_StreetAddress";
            Relation["SupplierLogisticsInfo.Supplier_Address_Country"] = "Supplier_Logistics.Supplier_Address_Country";
            Relation["SupplierLogisticsInfo.Supplier_Address_State"] = "Supplier_Logistics.Supplier_Address_State";
            Relation["SupplierLogisticsInfo.Supplier_Address_City"] = "Supplier_Logistics.Supplier_Address_City";
            Relation["SupplierLogisticsInfo.Supplier_Address_County"] = "Supplier_Logistics.Supplier_Address_County";
            Relation["SupplierLogisticsInfo.Supplier_Address_StreetAddress"] = "Supplier_Logistics.Supplier_Address_StreetAddress";
            Relation["SupplierLogisticsInfo.Supplier_Logistics_Name"] = "Supplier_Logistics.Supplier_Logistics_Name";
            Relation["SupplierLogisticsInfo.Supplier_Logistics_Number"] = "Supplier_Logistics.Supplier_Logistics_Number";
            Relation["SupplierLogisticsInfo.Supplier_Logistics_DeliveryTime"] = "Supplier_Logistics.Supplier_Logistics_DeliveryTime";
            Relation["SupplierLogisticsInfo.Supplier_Logistics_IsAudit"] = "Supplier_Logistics.Supplier_Logistics_IsAudit";
            Relation["SupplierLogisticsInfo.Supplier_Logistics_AuditTime"] = "Supplier_Logistics.Supplier_Logistics_AuditTime";
            Relation["SupplierLogisticsInfo.Supplier_Logistics_AuditRemarks"] = "Supplier_Logistics.Supplier_Logistics_AuditRemarks";
            Relation["SupplierLogisticsInfo.Supplier_Logistics_FinishTime"] = "Supplier_Logistics.Supplier_Logistics_FinishTime";
            Relation["SupplierLogisticsInfo.Supplier_Logistics_TenderID"] = "Supplier_Logistics.Supplier_Logistics_TenderID";
            Relation["SupplierLogisticsInfo.Supplier_Logistics_Price"] = "Supplier_Logistics.Supplier_Logistics_Price";

            //Logistics_Tender
            Relation["LogisticsTenderInfo.Logistics_Tender_ID"] = "Logistics_Tender.Logistics_Tender_ID";
            Relation["LogisticsTenderInfo.Logistics_Tender_LogisticsID"] = "Logistics_Tender.Logistics_Tender_LogisticsID";
            Relation["LogisticsTenderInfo.Logistics_Tender_SupplierLogisticsID"] = "Logistics_Tender.Logistics_Tender_SupplierLogisticsID";
            Relation["LogisticsTenderInfo.Logistics_Tender_OrderID"] = "Logistics_Tender.Logistics_Tender_OrderID";
            Relation["LogisticsTenderInfo.Logistics_Tender_Price"] = "Logistics_Tender.Logistics_Tender_Price";
            Relation["LogisticsTenderInfo.Logistics_Tender_AddTime"] = "Logistics_Tender.Logistics_Tender_AddTime";
            Relation["LogisticsTenderInfo.Logistics_Tender_IsWin"] = "Logistics_Tender.Logistics_Tender_IsWin";
            Relation["LogisticsTenderInfo.Logistics_Tender_FinishTime"] = "Logistics_Tender.Logistics_Tender_FinishTime";


  
           
            //新加 付款信息表Payment_Information
            Relation["PaymentInformationInfo.Payment_ID"] = "Payment_Information.Payment_ID";
            Relation["PaymentInformationInfo.Payment_PayingTeller"] = "Payment_Information.Payment_PayingTeller";
            Relation["PaymentInformationInfo.Payment_Account"] = "Payment_Information.Payment_Account";
            Relation["PaymentInformationInfo.Payment_Receivable"] = "Payment_Information.Payment_Receivable";
            Relation["PaymentInformationInfo.Payment_Account_Receivable"] = "Payment_Information.Payment_Account_Receivable";
            Relation["PaymentInformationInfo.Payment_Type"] = "Payment_Information.Payment_Type";
            Relation["PaymentInformationInfo.Payment_Amount"] = "Payment_Information.Payment_Amount";
            Relation["PaymentInformationInfo.Payment_Remarks"] = "Payment_Information.Payment_Remarks";
            Relation["PaymentInformationInfo.Payment_Account_Time"] = "Payment_Information.Payment_Account_Time";
            Relation["PaymentInformationInfo.Payment_Status"] = "Payment_Information.Payment_Status";
            Relation["PaymentInformationInfo.Payment_Flow"] = "Payment_Information.Payment_Flow";
            Relation["PaymentInformationInfo.Payment_Remarks1"] = "Payment_Information.Payment_Remarks1";






            //新加表 物流路线
            //Logistics_Line
            Relation["LogisticsLineInfo.Logistics_Line_ID"] = "Logistics_Line.Logistics_Line_ID";
            Relation["LogisticsLineInfo.Logistics_Line_Contact"] = "Logistics_Line.Logistics_Line_Contact";
            Relation["LogisticsLineInfo.Logistics_Line_CarType"] = "Logistics_Line.Logistics_Line_CarType";
            Relation["LogisticsLineInfo.Logistics_Line_Delivery_Address"] = "Logistics_Line.Logistics_Line_Delivery_Address";
            Relation["LogisticsLineInfo.Logistics_Line_Receiving_Address"] = "Logistics_Line.Logistics_Line_Receiving_Address";
            Relation["LogisticsLineInfo.Logistics_Line_DeliverTime"] = "Logistics_Line.Logistics_Line_DeliverTime";
            Relation["LogisticsLineInfo.Logistics_Line_Deliver_Price"] = "Logistics_Line.Logistics_Line_Deliver_Price";
            Relation["LogisticsLineInfo.Logistics_Line_Note"] = "Logistics_Line.Logistics_Line_Note";
            Relation["LogisticsLineInfo.Logistics_ID"] = "Logistics_Line.Logistics_ID";




            //Bid_Up_Require_Quick  快速发布需求           
            Relation["BidUpRequireQuickInfo.Bid_Up_ID"] = "Bid_Up_Require_Quick.Bid_Up_ID";
            Relation["BidUpRequireQuickInfo.Bid_Up_ContractMan"] = "Bid_Up_Require_Quick.Bid_Up_ContractMan";
            Relation["BidUpRequireQuickInfo.Bid_Up_ContractMobile"] = "Bid_Up_Require_Quick.Bid_Up_ContractMobile";
            Relation["BidUpRequireQuickInfo.Bid_Up_ContractContent"] = "Bid_Up_Require_Quick.Bid_Up_ContractContent";
            Relation["BidUpRequireQuickInfo.Bid_Up_Type"] = "Bid_Up_Require_Quick.Bid_Up_Type";
            Relation["BidUpRequireQuickInfo.Bid_Up_Note"] = "Bid_Up_Require_Quick.Bid_Up_Note";
            Relation["BidUpRequireQuickInfo.Bid_Up_Note1"] = "Bid_Up_Require_Quick.Bid_Up_Note1";
            Relation["BidUpRequireQuickInfo.Bid_Up_AddTime"] = "Bid_Up_Require_Quick.Bid_Up_AddTime";



        }
    }
}