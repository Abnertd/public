<%@ Page Language="C#" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Supplier supplier = new Supplier();
       
        string action = Request["action"];

        switch (action)
        {
            //case "account_profile":
            //    //supplier.Supplier_AuditLogin_Check("/supplier/index.aspx");
            //    supplier.UpdateSupplierInfoByMember_Profile();
            //    Response.End();
            //    break;
            case "account_profile":
                supplier.UpdateSupplierProfile();
                break;

            case "account_profile_edit":
                supplier.UpdateSupplierEdit();
                break;  
            case "account_password":
                supplier.Supplier_AuditLogin_Check("/supplier/index.aspx");
                supplier.UpdateSupplierPassword();
                Response.End();
                break;
            
            case "shop_apply":
                supplier.Supplier_Login_Check("/supplier/index.aspx");
                supplier.Supplier_Shop_Apply_Add();
                Response.End();
                break;
            case "shop_cert":
                //supplier.Supplier_Login_Check("/supplier/index.aspx");
                supplier.Supplier_Shop_Cert();
                Response.End();
                break;


          
             
            case "shop_set":
                supplier.Supplier_Login_Check("/supplier/index.aspx");
                supplier.Supplier_Shop_Config();
                Response.End();
                break;
            case "shop_set_model":
                supplier.Supplier_Login_Check("/supplier/index.aspx");
                supplier.Supplier_Shop_Model();
                Response.End();
                break;
            case "shop_set_banner":
                supplier.Supplier_Login_Check("/supplier/index.aspx");
                supplier.Supplier_Shop_Banner();
                Response.End();
                break;
            case "shop_upgrade":
                supplier.Supplier_Login_Check("/supplier/index.aspx");
                supplier.Supplier_Shop_UpGrade();
                Response.End();
                break;
            case "shopview":
                supplier.Supplier_Login_Check("/supplier/index.aspx");
                supplier.Goto_Shop();
                Response.End();
                break;
            case "account_add":
                supplier.Supplier_Login_Check("/supplier/index.aspx");
                supplier.Supplier_Account_Add();
                Response.End();
                break;
                //新加保证金充值
            case "account_add_new":
                supplier.Supplier_Login_Check("/supplier/index.aspx");
                supplier.Supplier_Account_Add_new();
                Response.End();
                break;
                
                
            case "my_account":
                supplier.Supplier_Login_Check("/supplier/index.aspx");
                supplier.AddSupplierBank();
                Response.End();
                break;
            case "shop_certsave":
                //supplier.Supplier_AuditLogin_Check("/supplier/index.aspx");
                supplier.Supplier_Cert_Save();
                Response.End();
                break;

            case "subaccount":
                supplier.Supplier_Login_Check("/supplier/index.aspx");
                supplier.AddSubAccount(1);
                Response.End();
                break;

            case "subdel":
                supplier.Supplier_Login_Check("/supplier/index.aspx");
                supplier.delSubAccount(1);
                Response.End();
                break;

            case "subaccountedit":
                supplier.Supplier_Login_Check("/supplier/index.aspx");
                supplier.EditSubAccount(1);
                Response.End();
                break;
            case "account_profile_sub":
                supplier.Supplier_Login_Check("/supplier/index.aspx");
                supplier.UpdateSubAccount();
                Response.End();
                break;
            case "allowsysemail":
                supplier.Supplier_Login_Check("/supplier/index.aspx");
                supplier.UpdateSupplierAllowSysEmail(1);
                Response.End();
                break;
            case "cancelsysemail":
                supplier.Supplier_Login_Check("/supplier/index.aspx");
                supplier.UpdateSupplierAllowSysEmail(0);
                Response.End();
                break;

            case "allowsysmessage":
                supplier.Supplier_Login_Check("/supplier/index.aspx");
                supplier.UpdateSupplierAllowSysMessage(1);
                Response.End();
                break;
            case "cancelsysmessage":
                supplier.Supplier_Login_Check("/supplier/index.aspx");
                supplier.UpdateSupplierAllowSysMessage(0);
                Response.End();
                break;
                
                
            case "account_password_sub":
                supplier.Supplier_Login_Check("/supplier/index.aspx");
                supplier.UpdateSubAccountPassword();
                Response.End();
                break;
            case "creditlimit_pay":
                supplier.Supplier_Login_Check("/supplier/index.aspx");
                supplier.Supplier_CreditLimit_Back();
                Response.End();
                break;
                
                
                
               //中信银行
            case "zhongxin":
                new ZhongXin().AddZhonghang();
                break;
            case "zhongxinwithdraw":
                new ZhongXin().Withdraw();
                break;

            
            //case "shop_sqssave":
            //    supplier.Supplier_Login_Check("/supplier/index.aspx");
            //    supplier.AddSupplierSQS();
            //    break;
        }
    }
</script>
