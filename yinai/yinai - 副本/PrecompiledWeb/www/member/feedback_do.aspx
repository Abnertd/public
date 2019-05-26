<%@ Page Language="C#" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        Member member = new Member();
        
        string action = Request["action"];
        if (action=="add_fin")
        {

        }
        else
        {
            member.Member_Login_Check("/member/feedback.aspx");  
        }  
      
        
        
        switch (action)
        {
            case "add":
                member.AddFeedBack(1);
                break;



            case "add_fin":
                member.AddFeedBack_Fin(1);
                break; 
                
            case "applyadd":
                //member.Member_WholeSale_ApplyAdd();
                break;
            
           
        }

    }
</script>
