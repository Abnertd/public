<%@ Page Language="C#" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Member member = new Member();
        Supplier supplier = new Supplier();
        string action = Request["action"];

        switch (action)
        {
            case "account_profile":
                member.UpdateMemberProfile();
                break;

            case "account_profile_edit":
                member.UpdateMemberProfileEdit();
                break;  
                
                
            case"account_profile_SupplierIntro"   :
                member.UpdateMemberProfileSupplierIntro();
                break;
            case "account_profile_SupplierContract":
                member.UpdateMemberProfileSupplierContract();
                break; 
                
                
            case "account_password":
                member.UpdateMemberPassword();
                break;
                

            case "account_profile_sub":
                member.UpdateSubAccount();
                break;
                
            //case "subaccount":
            //    member.AddSubAccount();
            //    break;
            //case "subaccountedit":
            //    member.EditSubAccount();
            //    break;
            //case "subdel":
            //    member.DelSubAccount();
            //    break;
            case "account_password_sub":

                break;
            case "loan_account":
                member.GetMemberLoanAccount();
                break;
                
            case "certsave":
                member.Member_Cert_Save();
                break;

            case "merchantsreply":
                member.Merchants_Message_Add();
                break;
            case "show_reply_dialog":
                member.Show_MerchantsReply_Dialog();
                break;
                
            case "erp_binding":
                member.Erp_Binding();
                break;
                
            case "update_session":
                member.Update_ERPParam_OrderID();
                Response.End();
                break;
            case "get_erpparam_orderid":
                member.Get_ERPParam_OrderID();
                Response.End();
                break;


            case "feedback":
                member.Check_FeedBackName();
                break;
                
                
                
                //商家子账号添加
            case "subaccount":
                supplier.Supplier_Login_Check("/supplier/index.aspx");
                supplier.AddSubAccount(0);
                Response.End();
                break;


            case "subaccountedit":
                supplier.Supplier_Login_Check("/supplier/index.aspx");
                supplier.EditSubAccount(0);
                Response.End();
                break;
            case "subdel":
                supplier.Supplier_Login_Check("/supplier/index.aspx");
                supplier.delSubAccount(0);
                Response.End();
                break;
        }
    }
</script>
