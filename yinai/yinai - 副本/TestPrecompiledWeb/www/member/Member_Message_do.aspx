<%@ Page Language="C#" %>

<script runat="server">
    Member member;
    protected void Page_Load(object sender, EventArgs e)
    {
        member = new Member();
        member.Member_Login_Check("/member/message_list.aspx?action=list");
        Member MyApp = new Member();
        string action = Request["action"];
        switch (action)
        {
            //删除选中的消息
            case "messagesmove":
                MyApp.MemberMessges_Del(false);
                break;
            //删除该会员接受的所有的消息
            case "allmessagesmove":
                MyApp.MemberMessges_Del(true);
                break;
            //标记为已读
            case "messagesIsRead":
                MyApp.MemberMessges_IsRead(1);
                break;
            //标记为未读
            case "messagesIsUnRead":
                MyApp.MemberMessges_IsRead(0);
                break;
            //全部标记为已读
            case "allmessagesIsRead":
                MyApp.AllMemberMessges_IsRead(1);
                break;
        }

    }
</script>
